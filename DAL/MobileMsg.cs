using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;

using Com.Yuantel.DBUtility;

namespace Com.Yuantel.Msg.DAL
{
    public class MobileMsg : IMobileMsg
    {

        #region Send
        private int ApproveMaxCount = int.Parse(ConfigurationManager.AppSettings["ApproveMaxCount"].ToString());//最大条数
        private int ApproveState = int.Parse(ConfigurationManager.AppSettings["ApproveState"].ToString());//状态
        private  string SQL_INSERT_MSG = "INSERT INTO IB_Sms_Send_Bill(MsgId,SeqNo,SubmitTime,SendTime,Message,Sender,State,SubSerFlag) VALUES(@MsgId,@SeqNo,@SubmitTime,@SendTime,@Message,'',1,7);";
        private  string SQL_INSERT_MSG_V1 = "INSERT INTO IB_Sms_Send_Bill(MsgId,SeqNo,SubmitTime,SendTime,Message,Sender,State,Priority,SubSerFlag) VALUES(@MsgId,@SeqNo,@SubmitTime,@SendTime,@Message,'',1,9,7);";
        private const string SQL_INSERT_MSGDETAIL = "INSERT INTO IB_Sms_Send_Detail_Bill(MsgId,Mobile) VALUES(@MsgId,@Mobile);";
        private const string PARM_MSGID = "@MsgId";
        private const string PARM_SEQNO = "@SeqNo";
        private const string PARM_SUBMITTIME = "@SubmitTime";
        private const string PARM_SENDTIME = "@SendTime";
        private const string PARM_MESSAGE = "@Message";
        private const string PARM_MOBILE = "@Mobile";
        
        public string Send(int seqNo, string mobiles, string msgs)
        {
            string msgId_v1 = "";
            List<string> messages = SplitMessage(msgs);
            string[] nums = mobiles.Split(new Char[] { ',' });
            if (nums.Length>ApproveMaxCount)
            {
                //ApproveState = 1;
                SQL_INSERT_MSG = "INSERT INTO IB_Sms_Send_Bill(MsgId,SeqNo,SubmitTime,SendTime,Message,Sender,State,SubSerFlag) VALUES(@MsgId,@SeqNo,@SubmitTime,@SendTime,@Message,'',"+ApproveState+",7);";
                //SQL_INSERT_MSG_V1 = "INSERT INTO IB_Sms_Send_Bill(MsgId,SeqNo,SubmitTime,SendTime,Message,Sender,State,Priority,SubSerFlag) VALUES(@MsgId,@SeqNo,@SubmitTime,@SendTime,@Message,''," + ApproveState + ",9,7);";

            }
            
            foreach (string msg in messages)
            {
                
                string msgId = GetMsgId();

                // Create the connection to the database
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringServer))
                {
                    SqlParameter[] parms;

                    SqlCommand cmd = new SqlCommand();

                   
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    //SqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        //string seqNoHigh = ConfigurationManager.AppSettings["SeqNoHighPriority"].ToString();
                        //if(seqNoHigh.Contains(seqNo.ToString()))
                        //写明细表
                        cmd.CommandText = SQL_INSERT_MSGDETAIL;

                        foreach (string mobile in nums)
                        {
                            // Get the cached parameters
                            parms = GetDetailParameters();
                            // Fill the parameters;
                            parms[0].Value = msgId;
                            parms[1].Value = mobile;
                            // Bind each parameter
                            foreach (SqlParameter parm in parms)
                                cmd.Parameters.Add(parm);
                            // Execute it
                            cmd.ExecuteNonQuery();
                            // Clear parameters
                            cmd.Parameters.Clear();
                        }

                        //写主表
                        // Get the cached parameters
                        parms = GetMsgParameters();

                        // Fill the parameters;
                        parms[0].Value = msgId;
                        parms[1].Value = seqNo;
                        parms[2].Value = DateTime.Now;
                        parms[3].Value = DateTime.Now;
                        parms[4].Value = msg;

                        // Bind each parameter
                        foreach (SqlParameter parm in parms)
                            cmd.Parameters.Add(parm);

                        if (seqNo == 178784 || seqNo == 100545)
                        {
                            cmd.CommandText = SQL_INSERT_MSG_V1;
                        }
                        else
                        {
                            cmd.CommandText = SQL_INSERT_MSG;
                        }
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        //trans.Commit();
                    }
                    catch (Exception ea)
                    {
                        //trans.Rollback();
                        throw;
                    }

                }
                msgId_v1 = msgId + "," + msgId_v1;
            }
            return msgId_v1;
        }

        private SqlParameter[] GetMsgParameters()
        {

            SqlParameter[] parms = SqlHelper.GetCachedParameters(SQL_INSERT_MSG);

            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter(PARM_MSGID,SqlDbType.VarChar, 20),
                    new SqlParameter(PARM_SEQNO, SqlDbType.Int),
                    new SqlParameter(PARM_SUBMITTIME, SqlDbType.DateTime, 8),
                    new SqlParameter(PARM_SENDTIME, SqlDbType.DateTime, 8),
                    new SqlParameter(PARM_MESSAGE, SqlDbType.VarChar, 200)
                };
                SqlHelper.CacheParameters(SQL_INSERT_MSG, parms);
            }

            return parms;
        }

        private SqlParameter[] GetDetailParameters()
        {

            SqlParameter[] parms = SqlHelper.GetCachedParameters(SQL_INSERT_MSGDETAIL);

            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter(PARM_MSGID,SqlDbType.VarChar, 20),
                    new SqlParameter(PARM_MOBILE, SqlDbType.VarChar, 50)
                };
                SqlHelper.CacheParameters(SQL_INSERT_MSGDETAIL, parms);
            }

            return parms;
        }

        #endregion

        #region QueryState

        private const string SQL_SELECT_STATE = "SELECT Success,Failed,Amount FROM IB_Sms_Send_Bill WHERE MsgId=@MsgId AND State>0";
//        private const string SQL_SELECT_DETAIL = "SELECT Mobile,Cast(State as int) as State FROM IB_Sms_Send_Detail_Bill WHERE MsgId=@MsgId";
        private const string SQL_SELECT_DETAIL = "SELECT Mobile,State FROM IB_Sms_Send_Detail_Bill WHERE MsgId=@MsgId";

        public string GetStateDesc(string msgId)
        {
            StringBuilder sb = new StringBuilder();

            // Create the connection to the database
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringServer))
            {
                SqlParameter[] parms;

                SqlCommand cmd = new SqlCommand();

                // Get the cached parameters
                parms = GetStateDescParameters();

                // Fill the parameters;
                parms[0].Value = msgId;

                // Bind each parameter
                foreach (SqlParameter parm in parms)
                    cmd.Parameters.Add(parm);

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL_SELECT_STATE;

                sb.Append("<ReturnInfo>");
                sb.Append("<ErrCode>0</ErrCode>");
                sb.Append("<ErrMsg></ErrMsg>");
                sb.Append("<SmsId>").Append(msgId).Append("</SmsId>");
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        sb.Append("<Success>").Append(rdr.GetInt32(0).ToString()).Append("</Success>");
                        sb.Append("<Failed>").Append(rdr.GetInt32(1).ToString()).Append("</Failed>");
                        sb.Append("<Amount>").Append(rdr.GetInt32(2).ToString()).Append("</Amount>");
                    }
                }

                cmd.CommandText = SQL_SELECT_DETAIL;
                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    sb.Append("<Details>");
                    while (rdr.Read())
                    {
                        sb.Append("<Item>");
                        sb.Append("<Mobile>").Append(rdr.GetString(0)).Append("</Mobile>");
                        sb.Append("<State>").Append(rdr.GetByte(1)).Append("</State>");
                        sb.Append("</Item>");
                    }
                    sb.Append("</Details>");
                }

                sb.Append("</ReturnInfo>");

                return sb.ToString();
            }
        }

        private SqlParameter[] GetStateDescParameters()
        {

            SqlParameter[] parms = SqlHelper.GetCachedParameters(SQL_SELECT_STATE);

            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter(PARM_MSGID, SqlDbType.VarChar, 20)
                };
                SqlHelper.CacheParameters(SQL_SELECT_STATE, parms);
            }

            return parms;
        }

        #endregion

        #region GetMsgId

        private const string SQL_GETMSGID = "UP_Sys_Common_Code_CreateNew";
        private const string PARM_CLSID = "@ClsID";
        private const string PARM_BATCHNO = "@BatchNo";

        public string GetMsgId()
        {
            // Create the connection to the database
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringServer))
            {
                SqlParameter[] parms;

                SqlCommand cmd = new SqlCommand();

                // Get the cached parameters
                parms = GetMsgIdParameters();

                // Fill the parameters;
                parms[0].Value = 20;
                parms[1].Direction = ParameterDirection.Output;

                // Bind each parameter
                foreach (SqlParameter parm in parms)
                    cmd.Parameters.Add(parm);

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = SQL_GETMSGID;

                cmd.ExecuteNonQuery();

                return (string)cmd.Parameters[1].Value;
            }
        }

        private SqlParameter[] GetMsgIdParameters()
        {

            SqlParameter[] parms = SqlHelper.GetCachedParameters(SQL_GETMSGID);

            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter(PARM_CLSID,SqlDbType.Int),
                    new SqlParameter(PARM_BATCHNO, SqlDbType.VarChar, 50)
                };
                SqlHelper.CacheParameters(SQL_GETMSGID, parms);
            }

            return parms;
        }

        #endregion

        #region 2010-10-15 by lixinghua
        /// <summary>
        /// 分割短信内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static List<string> SplitMessage(string message)
        {
            List<string> Messages = new List<string>();
            //if (message.Length <= 69)
            //{
            //    Messages.Add(message);
            //}
            //else
            //{
            //    int count = int.Parse(Convert.ToString(Math.Ceiling(message.Length * 1.0 / 64)));
            //    string header = "";
            //    string body = "";
            //    for (int i = 0; i < count; i++)
            //    {
            //        header = string.Format("({0}/{1})", i+1, count);
            //        body = i < count - 1 ? message.Substring(i * 64, 64) : message.Substring(i * 64);
            //        Messages.Add(header + body);
            //    }
            //}
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, string.Format("Select * From SplitMessage('{0}')", message), null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Messages.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            return Messages;
        }
        #endregion
    }
}
