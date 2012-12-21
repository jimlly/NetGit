/**
 * 文件：Query.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：短信发送数据访问类
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.DBUtility;
using System.Configuration;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class Send
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="msg"></param>
        public static void SendMsg(Com.Yuantel.MobileMsg.Model.Msg msg)
        {
            using (SqlConnection cn = new SqlConnection(SqlHelper.ConnectionStringServer))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    List<string> messages = SplitMessage(msg.Message,msg.SeqNo);
                    string sql = "insert into IB_Sms_Send_Detail_Bill (MsgID,Mobile,Name) values ('{0}','{1}','{2}')";
                    string sqlFull = "";
                    int count = int.Parse(ConfigurationManager.AppSettings["ApproveMaxCount"].ToString());
                    int state = msg.Receivers.Count > count ? 0 : 1;

                    string msgitem = string.Empty;
                    foreach (string mess in messages)
                    {
                        msgitem = mess.Replace("'", "''");
                        msg.MsgId = GetBatchNo(20);
                        foreach (Receiver receiver in msg.Receivers)
                        {
                            sqlFull = string.Format(sql, msg.MsgId, receiver.Mobile, receiver.Name);
                            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlFull, null);
                        }
                        sqlFull = string.Format("insert into IB_Sms_Send_Bill (MsgID,SeqNo,Sender,Priority,SubmitTime,SendTime,Message,State,SendCounts,SubSerFlag) values ('{0}',{1},'{2}',{3},'{4}','{5}','{6}',{7},{8},7)", msg.MsgId, msg.SeqNo, msg.Sender, Convert.ToInt16(msg.Priority), msg.SubmitTime, msg.SendTime, msgitem, state, msg.Receivers.Count);
                        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlFull, null);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                    tran.Rollback();
                }
            }
        }

        /// <summary>
        /// 分割短信内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static List<string> SplitMessage(string message,int seqNo)
        {
            List<string> Messages = new List<string>();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, string.Format("Select * From SplitMessageBySeqNo('{0}',{1})", message, seqNo), null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Messages.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            return Messages;
        }

        /// <summary>
        /// 获取批次号
        /// </summary>
        /// <param name="_clsid"></param>
        /// <returns></returns>
        public static string GetBatchNo(int _clsid)
        {
            string BatchNo = "";
            SqlParameter[] parameters = {
					new SqlParameter("@ClsID", SqlDbType.Int,4),//编号类型
                    new SqlParameter("@BatchNo",SqlDbType.VarChar,50)
					};
            parameters[0].Value = _clsid;
            parameters[1].Direction = ParameterDirection.Output;
            try
            {
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "UP_Sys_Common_Code_CreateNew", parameters);
                BatchNo = parameters[1].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return BatchNo;
        }

        /// <summary>
        /// 重发短信
        /// </summary>
        /// <param name="msgID"></param>
        public static int Resend(string msgID, int state)
        {
            int affectedRows = 0;
            using (SqlConnection cn = new SqlConnection(SqlHelper.ConnectionStringServer))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    string[] msgs = msgID.Split(',');
                    string newMsgID = "";
                    string sql = "";
                    int count = int.Parse(ConfigurationManager.AppSettings["ApproveMaxCount"].ToString());
                    for (int i = 0; i < msgs.Length; i++)
                    {
                        newMsgID = GetBatchNo(20);
                        sql = string.Format("select count(1) from IB_Sms_Send_Detail_Bill where MsgID = '{0}'", msgs[i]);
                        int sendcounts = int.Parse(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());
                        int status = sendcounts > count ? 0 : 1;
                        if (state == 0)//发送完成
                        {
                            sql = string.Format("insert into IB_Sms_Send_Detail_Bill (MsgID,Mobile,Name) select '{0}',Mobile,Name from IB_Sms_Send_Detail_Bill where MsgID = '{1}' and State >= 2", newMsgID, msgs[i]);
                        }
                        else if (state == 2)//发送成功
                        {
                            sql = string.Format("insert into IB_Sms_Send_Detail_Bill (MsgID,Mobile,Name) select '{0}',Mobile,Name from IB_Sms_Send_Detail_Bill where MsgID = '{1}' and State = 2", newMsgID, msgs[i], state);
                        }
                        else //发送失败
                        {
                            sql = string.Format("insert into IB_Sms_Send_Detail_Bill (MsgID,Mobile,Name) select '{0}',Mobile,Name from IB_Sms_Send_Detail_Bill where MsgID = '{1}' and State >= 4", newMsgID, msgs[i], state);
                        }

                        affectedRows = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
                        if (affectedRows > 0)
                        {
                            sql = string.Format("insert into IB_Sms_Send_Bill (MsgID,SeqNo,Sender,Priority,SubmitTime,SendTime,Message,State,SendCounts,SubSerFlag) select '{0}',SeqNo,Sender,Priority,getdate(),getdate(),Message,{2},{3},SubSerFlag from IB_Sms_Send_Bill where MsgID = '{1}'", newMsgID, msgs[i], status, affectedRows);
                            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
                        }
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                    tran.Rollback();
                }
            }

            return affectedRows;
        }
    }
}
