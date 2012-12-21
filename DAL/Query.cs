/**
 * 文件：Query.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：短信查询数据访问类
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;

using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.DBUtility;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class Query
    {
        /// <summary>
        /// 查询清单
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public static DataSet GetSendList(Com.Yuantel.MobileMsg.Model.Msg msg, out string totalRows)
        {
            string where = "";
            where = string.Format(" IsDelete = {0} and MsgType = 3", msg.IsDelete);
            if (msg.SeqNo > -1)
            {
                where += string.Format(" and SeqNo = {0}",msg.SeqNo);
            }
            if (msg.ClsID == 1)//当天
            {
                where += string.Format(" and SubmitTime > '{0} 00:00:00'",DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else if (msg.ClsID == 2)//一周内
            {
                where += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"));
            }
            else if (msg.ClsID == 3)//一个月以内
            {
                where += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            }
            else if (msg.ClsID == 4)
            {
                if (msg.BeginTime.Length > 0)
                {
                    where += string.Format(" and SubmitTime > '{0} 00:00:00'", msg.BeginTime);
                }
                if (msg.EndTime.Length > 0)
                {
                    if (where.Length > 0)
                    {
                        where += string.Format(" and SubmitTime < '{0} 23:59:59'", msg.EndTime);
                    }
                    else
                    {
                        where += string.Format(" and SubmitTime < '{0} 23:59:59'", msg.EndTime);
                    }
                }
            }
            if (msg.Message.Length > 0)
            {
                where += string.Format(" and Message like '%{0}%'",msg.Message);
            }
            if (msg.Status > -1)
            {
                if (msg.Status == 1)
                {
                    where += " and State not in (0,20,21)";
                }
                else
                {
                    where += string.Format(" and State = {0}", msg.Status);
                }
            }
            string orderby = " order by MsgID desc";
            string sql = string.Format("select count(1) from IB_Sms_Send_Bill where {0}",where);
            totalRows = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString();

            sql = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) rowid, SeqNo,MsgID,SendTime,Message,Convert(int,State) as State,IsDelete,Success,Failed,Convert(Decimal(10,2),Amount * 1.0/10000) as Amount,SendCounts FROM IB_Sms_Send_Bill Where {1}) AS TEMP WHERE rowid BETWEEN {2} AND {3}", orderby, where, (msg.PageIndex - 1) * msg.PageSize + 1, msg.PageIndex * msg.PageSize);

            try
            {
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
                return ds;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="seqNo">用户编号</param>
        /// <param name="msgID">批次号列表</param>
        /// <param name="state">发送状态</param>
        /// <param name="isDelete">是否删除</param>
        /// <returns></returns>
        public static int DeleteSendList(int seqNo,string msgID,int state,int isDelete)
        {
            int affectRows = 0;
            try
            {
                string where = string.Format(" SeqNo = {0}", seqNo);
                if (msgID.Length > 0)
                {
                    where += string.Format(" and MsgID in ('{0}')", Replace(msgID));
                }
                //if (state > -1)
                //{
                //    where += string.Format(" and State = {0}", state);
                //}
                string sql = string.Format("update IB_Sms_Send_Bill set IsDelete = {0} where {1}", isDelete, where);
                affectRows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return affectRows;
        }

        /// <summary>
        /// 字符串中的"'"替换为"','"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Replace(string str)
        {
            if (str.IndexOf(',') > 0)
            {
                string[] strArr = str.Split(',');
                str = "";
                for (int i = 0; i < strArr.Length - 1; i++)
                {
                    str += strArr[i] + "','";
                }
                str = str.Substring(0, str.Length - 3);
            }
            return str;
        }

        /// <summary>
        /// 检查记录状态，是否能重发
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public static int CanResend(string msgID)
        {
            int result = 0;
            try
            {
                string sql = string.Format("select count(1) from IB_Sms_Send_Bill where State = 9 and MsgID = '{0}'", msgID);
                result = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 根据批次号获取各种状态的明细记录数
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int GetRowCount(string msgID, int state)
        {
            string sql = "";
            int result = 0;
            try
            {
                if (state > -1)
                {
                    sql = string.Format("select count(1) from IB_Sms_Send_Detail_Bill where MsgID = '{0}' and State = {1}", msgID, state);
                }
                else
                {
                    sql = string.Format("select count(1) from IB_Sms_Send_Detail_Bill where MsgID = '{0}'", msgID);
                }
                result = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public static double GetBalance(int seqNo)
        {
            double balance = 0.0;
            string sql = string.Format("Select convert(Decimal(20,2),(Balance + DonatedBalance + AddedBalance + AddedDonatedBalance - SubbedBalance - SubbedDonatedBalance)*1.0/10000) as balance From IB_Account Where AccountID = (Select AccountID From IB_SysUser Where SeqNo = {0})", seqNo);
            try
            {
                balance = double.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer_yuantel,CommandType.Text, sql, null).ToString());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return balance;
        }

        #region 获取用户金额账户信息列表2011-3-16
        /// <summary>
        /// 获取用户金额账户信息列表 Create by lining at 2011-3-16
        /// </summary>
        /// <param name="seqNo"></param>
        /// <param name="ProductID"></param>
        /// <param name="CostType">付费类型   0：全部  1：通信费帐户   2：非通信费帐户（租用费帐户或包年费用帐户）</param>
        /// <param name="FeeTypeIndex">通话类型ID 如果为：-1不进行判断 </param>
        /// <param name="TotalBalance">输出参数--充值余额总数 </param>
        /// <param name="TotalDonatedBalance">输出参数--赠送余额总数 </param>
        /// <returns></returns>
        public static IList<IB_AccountInfo> GetUserAccountList(int SeqNo, int ProductID, byte CostType, int FeeTypeIndex, out long TotalBalance, out long TotalDonatedBalance)
        {
            IList<IB_AccountInfo> list = null;
            IB_AccountInfo account = null;
            TotalBalance = 0;
            TotalDonatedBalance = 0;
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@SeqNo", SqlDbType.Int)
                   ,new SqlParameter("@ProductID", SqlDbType.Int)
                   ,new SqlParameter("@CostType", SqlDbType.TinyInt)
                   ,new SqlParameter("@FeeTypeIndex", SqlDbType.Int)
                };
                parameters[0].Value = SeqNo;
                parameters[1].Value = ProductID;
                parameters[2].Value = CostType;
                parameters[3].Value = FeeTypeIndex;

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringServer_yuantel, CommandType.StoredProcedure, "UP_IB_V3_I_GetClientAccountList", parameters))
                {
                    if (rdr != null)
                    {
                        list = new List<IB_AccountInfo>();
                        while (rdr.Read())
                        {
                            account = new IB_AccountInfo();
                            if (!rdr.IsDBNull(0)) account.AccountID = rdr.GetInt32(0);
                            if (!rdr.IsDBNull(1)) account.NickName = rdr.GetString(1);
                            if (!rdr.IsDBNull(2)) account.Percents = rdr.GetDouble(2);

                            if (!rdr.IsDBNull(3)) account.Balance = rdr.GetInt64(3);
                            if (!rdr.IsDBNull(4)) account.DonatedBalance = rdr.GetInt64(4);
                            if (!rdr.IsDBNull(5)) account.CostType = rdr.GetByte(5);
                            if (!rdr.IsDBNull(6)) account.HaveTerm = rdr.GetInt16(6);
                            if (!rdr.IsDBNull(7)) account.EndDate = rdr.GetDateTime(7).ToShortDateString();
                            if (!rdr.IsDBNull(8)) account.FeeTypeIndexDesc = rdr.GetString(8);
                            list.Add(account);

                            TotalBalance += account.Balance;
                            TotalDonatedBalance += account.DonatedBalance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int ApproveMsg(string msgID, int state)
        {
            string sql = "";
            if (msgID.StartsWith("SMS"))
            {
                sql = string.Format("update IB_Sms_Send_Bill set State = {0},ApprovalTime = getdate() where MsgID = '{1}' and State = 0", state, msgID);
            }
            else
            {
                sql = string.Format("update YX_Sms_Send set SendStatus = {0},ApprovalTime = getdate() where ID = {1} and SendStatus = 0", state, msgID);
            }
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="msgIDs"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int BatchApproveMsg(string msgIDs,int state)
        {
            int result = 0;
            string[] msgID = msgIDs.Split(',');
            foreach (string msg in msgID)
            {
                result += ApproveMsg(msg,state);
            }
            return result;
        }

        /// <summary>
        /// 查询明细
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public static DataSet GetDetailList(string msgID,int pageIndex,int pageSize,out int totalRows)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = string.Format("select * from (select row_number() over (order by SendTime) rowid, MsgID,Mobile,Name,SendTime,Convert(int,State) as State FROM IB_Sms_Send_Detail_Bill Where MsgID = '{0}') AS TEMP WHERE rowid BETWEEN {1} AND {2}", msgID, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer,CommandType.Text,sql,null);
                sql = string.Format("select count(1) from IB_Sms_Send_Detail_Bill Where MsgID = '{0}'",msgID);
                totalRows = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
                ds = null;
                totalRows = 0;
            }
            return ds;
        }

        /// <summary>
        /// 获取待审批的记录数
        /// </summary>
        /// <returns></returns>
        public static int GetApproveMsg()
        {
            int result = 0;
            try
            {
                result = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, "select count(1) from IB_Sms_Send_Bill where State = 0").ToString());
                result += int.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, "select count(1) from YX_Sms_Send where SendStatus = 0").ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="ServiceID">所属代理商</param>
        /// <param name="SerFlag">业务类型</param>
        /// <returns>DataSet</returns>
        public static DataSet get_Notice(int ServiceID, int SerFlag)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] parameters = {
				    new SqlParameter("@ServiceID", SqlDbType.Int),
                    new SqlParameter("@SerFlag", SqlDbType.Int)
                };
                parameters[0].Value = ServiceID;
                parameters[1].Value = SerFlag;
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer_yuantel, CommandType.StoredProcedure, "UP_Common_GetNotice", parameters);
            }
            catch (Exception ex)
            {
                ds = null;
                throw new Exception(ex.ToString());
            }
            finally
            {
            }
            return ds;
        }

        /// <summary>
        /// 获取未发送记录数
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public static string GetSendingRows(int seqNo)
        {
            try
            {
                string sql = string.Format("select count(1) from IB_Sms_Send_Bill where SeqNo = {0} and State not in (9,20,21)",seqNo);
                return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer,CommandType.Text,sql,null).ToString();
            }
            catch
            {
                return "0";
            }
        }

        public static string GetUserAccount(int seqNo)
        {
            string userAccount = "";
            string sql = string.Format("Select UserAccount From IB_SerUser Where SeqNo = {0}", seqNo);
            try
            {
                userAccount = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer_yuantel, CommandType.Text, sql, null).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return userAccount;
        }

        /// <summary>
        /// 查询审批记录列表
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        //public static DataSet GetApproveList(Com.Yuantel.MobileMsg.Model.Msg msg, out string totalRows)
        //{
        //    log.Info("GetApproveList Method Begin");
        //    string where1 = "";
        //    string where2 = " 1=1";
        //    where1 = string.Format(" IsDelete = {0} and SmsType = 0", msg.IsDelete);
        //    if (msg.SeqNo > -1)
        //    {
        //        where1 += string.Format(" and SeqNo = {0}", msg.SeqNo);
        //    }
        //    if (msg.ClsID == 1)//当天
        //    {
        //        where1 += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.ToString("yyyy-MM-dd"));
        //        where2 += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.ToString("yyyy-MM-dd"));
        //    }
        //    else if (msg.ClsID == 2)//一周内
        //    {
        //        where1 += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"));
        //        where2 += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"));
        //    }
        //    else if (msg.ClsID == 3)//一个月以内
        //    {
        //        where1 += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
        //        where2 += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
        //    }
        //    else if (msg.ClsID == 4)
        //    {
        //        if (msg.BeginTime.Length > 0)
        //        {
        //            where1 += string.Format(" and SubmitTime > '{0} 00:00:00'", msg.BeginTime);
        //            where2 += string.Format(" and SubmitTime > '{0} 00:00:00'", msg.BeginTime);
        //        }
        //        if (msg.EndTime.Length > 0)
        //        {
        //            if (where1.Length > 0)
        //            {
        //                where1 += string.Format(" and SubmitTime < '{0} 23:59:59'", msg.EndTime);
        //            }
        //            else
        //            {
        //                where1 += string.Format(" and SubmitTime < '{0} 23:59:59'", msg.EndTime);
        //            }
        //            if (where2.Length > 0)
        //            {
        //                where2 += string.Format(" and SubmitTime < '{0} 23:59:59'", msg.EndTime);
        //            }
        //            else
        //            {
        //                where2 += string.Format(" and SubmitTime < '{0} 23:59:59'", msg.EndTime);
        //            }
        //        }
        //    }
        //    if (msg.Message.Length > 0)
        //    {
        //        where1 += string.Format(" and Message like '%{0}%'", msg.Message);
        //        where2 += string.Format(" and Message like '%{0}%'", msg.Message);
        //    }
        //    if (msg.Status > -1)
        //    {
        //        if (msg.Status == 1)
        //        {
        //            where1 += " and State not in (0,20,21)";
        //            where2 += " and SendStatus not in (0,20,21)";
        //        }
        //        else
        //        {
        //            where1 += string.Format(" and State = {0}", msg.Status);
        //            where2 += string.Format(" and SendStatus = {0}", msg.Status);
        //        }
        //    }
        //    string orderby1 = " order by MsgID desc";
        //    string orderby2 = " order by ID desc";
        //    string sql = string.Format("select count(1) from IB_Sms_Send_Bill where {0}", where1);
        //    totalRows = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString();
        //    sql = string.Format("select count(1) from YX_Sms_Send where {0}", where2);
        //    totalRows += SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString();

        //    sql = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) rowid, SeqNo,MsgID,SendTime,Message,Convert(int,State) as State FROM IB_Sms_Send_Bill Where {1} union SELECT ROW_NUMBER() OVER ({4}) rowid, SeqNo,Convert(varchar(20),ID) as MsgID,SendTime,Message,Convert(int,SendStatus) as State FROM YX_Sms_Send Where {5}) AS TEMP WHERE rowid BETWEEN {2} AND {3}", orderby1, where1, (msg.PageIndex - 1) * msg.PageSize + 1, msg.PageIndex * msg.PageSize, orderby2, where2);

        //    try
        //    {
        //        DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message);
        //        return null;
        //    }
        //    log.Info("GetApproveList Method End");
        //}

        /// <summary>
        /// 查询审批记录列表
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public static DataSet GetApproveList(Com.Yuantel.MobileMsg.Model.Msg msg, out string totalRows)
        {
            DataSet ds = null;

            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.VarChar,80),
                    new SqlParameter("@ClsID", SqlDbType.Int),
                    new SqlParameter("@BeginTime", SqlDbType.VarChar,10),
                    new SqlParameter("@EndTime", SqlDbType.VarChar,10),
                    new SqlParameter("@Message", SqlDbType.NVarChar,20),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@TotalRows", SqlDbType.Int)                    
                };
            parameters[0].Value = msg.UserName;
            parameters[1].Value = msg.ClsID;
            parameters[2].Value = msg.BeginTime;
            parameters[3].Value = msg.EndTime;
            parameters[4].Value = msg.Message;
            parameters[5].Value = msg.Status;
            parameters[6].Value = msg.PageIndex;
            parameters[7].Value = msg.PageSize;
            parameters[8].Direction = ParameterDirection.Output;            
            try
            {
                ds =  SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "UP_Sms_GetCheckList", parameters);
                totalRows = Convert.ToString(parameters[8].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                totalRows = "0";
            }

            return ds;            
        }
    }
}
