/**
 * 文件：YxSms.cs
 * 作者：朱翔
 * 日期：2009-06-08
 * 描述：短信营销数据访问类
*/

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Com.Yuantel.DBUtility;
using Com.Yuantel.MobileMsg.Model;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class YxSms
    {
        private const string SQL_SMS_SEND_ADD = "Insert Into YX_Sms_Send (SeqNo,Message,SendTime,SendCounts,StartID,EndID) Values (@SeqNo,@Message,@SendTime,@SendCounts,@StartID,@EndID);Select @@identity;";
        private const string SQL_AREA_INDUSTRY_ADD = "Insert Into YX_Sms_Area_Industry (SmsID,ProvinceID,AreaID,IndustryID) Values (@SmsID,@ProvinceID,@AreaID,@IndustryID)";

        private const string sqlprovince = "select AreaName,AreaID from YX_AreaInfo where AreaType=0 order by AreaName";
        private const string sqlindustry = "select MainName,MainIndustryID from YX_MainIndustry order by MainIndustryID";

        private static string PARAM_SEQNO = "@SeqNo";
        private static string PARAM_MESSAGE = "@Message";
        private static string PARAM_SENDTIME = "@SendTime";
        private static string PARAM_SENDCOUNTS = "@SendCounts";
        private static string PARAM_STARTID = "@StartID";
        private static string PARAM_ENDID = "@EndID";
        private static string PARAM_SMSID = "@SmsID";
        private static string PARAM_PROVINCEID = "@ProvinceID";
        private static string PARAM_AREAID = "@AreaID";
        private static string PARAM_INDUSTRY = "@IndustryID";

        /// <summary>
        /// 提交营销记录
        /// </summary>
        /// <param name="msg"></param>
        public static void Send(YxMsg msg)
        {
            using (SqlConnection cn = new SqlConnection(SqlHelper.ConnectionStringServer))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();

                try
                {
                    List<string> messages = SplitMessage(msg.Message,msg.SeqNo);
                    string msgitem = string.Empty;
                    foreach (string mess in messages)
                    {

                        SqlParameter[] param = new SqlParameter[] {
                            new SqlParameter(PARAM_SEQNO,SqlDbType.Int),
                            new SqlParameter(PARAM_MESSAGE,SqlDbType.VarChar),
                            new SqlParameter(PARAM_SENDTIME,SqlDbType.DateTime),
                            new SqlParameter(PARAM_SENDCOUNTS,SqlDbType.Int),
                            new SqlParameter(PARAM_STARTID,SqlDbType.Int),
                            new SqlParameter(PARAM_ENDID,SqlDbType.Int)
                        };
                        param[0].Value = msg.SeqNo;
                        param[1].Value = mess;
                        param[2].Value = msg.SendTime;
                        param[3].Value = msg.SendCounts;
                        param[4].Value = msg.StartID;
                        param[5].Value = msg.EndID;

                        Int64 SmsID = Convert.ToInt64(SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_SMS_SEND_ADD, param).ToString());

                        string[] str1 = msg.AreaIndustry.Split(';');
                        string[] str2 = null;

                        foreach (string str in str1)
                        {
                            str2 = str.Split('|');
                            if (str2.Length < 3)
                            {
                                continue;
                            }
                            cmd = new SqlCommand();
                            param = new SqlParameter[] {
                            new SqlParameter(PARAM_SMSID,SqlDbType.BigInt),
                            new SqlParameter(PARAM_PROVINCEID,SqlDbType.Int),
                            new SqlParameter(PARAM_AREAID,SqlDbType.Int),
                            new SqlParameter(PARAM_INDUSTRY,SqlDbType.Int)
                        };
                            param[0].Value = SmsID;
                            param[1].Value = str2[0];
                            param[2].Value = str2[1];
                            param[3].Value = str2[2];

                            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_AREA_INDUSTRY_ADD, param);
                        }
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
        }

        public static DataSet GetSendList(YxMsg msg, out string totalRows)
        {
            string where = " 1 = 1";
            if (msg.SeqNo > -1)
            {
                where += string.Format(" and SeqNo = {0}", msg.SeqNo);
            }
            if (msg.ClsID == 1)//当天
            {
                where += string.Format(" and SubmitTime > '{0} 00:00:00'", DateTime.Now.ToString("yyyy-MM-dd"));
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
                where += string.Format(" and Message like '%{0}%'", msg.Message);
            }
            string orderby = " order by ID desc";

            try
            {
                string sql = string.Format("select count(1) from YX_Sms_Send where {0}", where);
                totalRows = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString();

                sql = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) rowid, ID,SendTime,[Message],SendStatus,SuccessCounts,FailedCounts,SendCounts,cast(UsedValue as decimal(10,2)) as UsedValue FROM YX_Sms_Send Where {1}) AS TEMP WHERE rowid BETWEEN {2} AND {3}", orderby, where, (msg.PageIndex - 1) * msg.PageSize + 1, msg.PageIndex * msg.PageSize);

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
                return ds;
            }
            catch
            {
                totalRows = "0";
                return null;
            }
        }

        //查询省，地区，行业
        public static DataSet QueryArea(int smsid)
        {
            try
            {
                string sql = string.Format("select case when a.provinceid > -1 then (select areaname from yx_areainfo where areaid = a.provinceid) end as provincename,case when a.areaid > -1 then (select areaname from yx_areainfo where areaid = a.areaid) else '' end as areaname,case when a.industryid > -1 then (select MainName from YX_MainIndustry where MainIndustryID = a.industryid) else '' end as industryname from YX_Sms_Area_Industry a where smsid = {0}", smsid);
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
                return ds;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查询记录详细信息
        /// </summary>
        /// <param name="smsID">编号</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页显示行数</param>
        /// <param name="totalRows">记录总数</param>
        /// <returns></returns>
        public static DataSet QueryDetail(Int64 smsID, int pageIndex, int pageSize, out int totalRows)
        {
            try
            {
                string sql = string.Format("Select * From (Select ROW_NUMBER() Over (Order By ID) rowid, IsNull(SendTime,getdate()) as SendTime,Mobile,State From IB_Sms_Send_Detail_Bill with(nolock) Where SmsID = {0}) As TEMP Where rowid between {1} and {2}", smsID, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
                sql = string.Format("Select Count(1) From  IB_Sms_Send_Detail_Bill with(nolock) Where SmsID = {0}", smsID);
                totalRows = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString());
                return ds;
            }
            catch
            {
                totalRows = 0;
                return null;
            }
        }

        #region GetAreaList
        public static DataSet GetAreaList(int cityid)
        {
            string sql = "select AreaName,AreaID from YX_AreaInfo where AreaParentID=" + cityid + "order by AreaName";
            return GetDataSet(sql);
        }
        #endregion

        #region NewMethod
        public static DataSet NewGetPovinceList()
        {
            return GetAreaDataSet("province");
        }
        public static DataSet NewGetHangyeList()
        {
            return GetAreaDataSet("industry");
        }

        public static DataSet NewGetCityList(int povinceid)
        {
            string sql = "select AreaID,AreaName from YX_AreaInfo where AreaType=1  and [AreaParentID] in(select AreaParentID from YX_AreaInfo where AreaID=" + povinceid + ") order by AreaName";
            return GetDataSet(sql);
        }

        //显示共有多少个可用号码.
        public static string NewGetNumAmout(string areahy)
        {
            ////areahy="0|0|0" 表是省id|城市id|行业id
            string[] sArray = areahy.Split('|');
            int provinceid = Convert.ToInt32(sArray[0]);
            int cityid = Convert.ToInt32(sArray[1]);
            int hangyeid = Convert.ToInt32(sArray[2]);
            string sql = string.Empty;
            //省，地区，行业
            if (cityid != -1 && hangyeid != -1)
            {
                sql = "select isnull(sum(Amount),0) from YX_IndustryArea where  AreaID=" + cityid + " and IndustryID = " + hangyeid;
            }
            //省，地区
            else if (cityid == -1 && hangyeid != -1)
            {
                sql = "select isnull(sum(Amount),0) from YX_IndustryArea where AreaID in (select areaid from YX_AreaInfo where areatype = 1 and AreaParentID = " + provinceid + ") and IndustryID = " + hangyeid;
            }
            //省，行业
            else if (cityid != -1 && hangyeid == -1)
            {
                sql = "select isnull(sum(Amount),0) from YX_IndustryArea where AreaID=" + cityid;
            }
            //省
            else if (cityid == -1 && hangyeid == -1)
            {
                sql = "select isnull(sum(Amount),0) from YX_IndustryArea where AreaID in (select areaid from YX_AreaInfo where areatype = 1 and AreaParentID = " + provinceid + ")";
            }

            return GetNumberCount(sql);
        }
        #endregion

        #region GetDataSet
        private static string GetNumberCount(string sql)
        {
            string num = "0";
            try
            {
                num = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString();
            }
            catch
            { }
            return num;
        }

        private static DataSet GetAreaDataSet(string flag)
        {
            string sqlstr = string.Empty;
            if (flag == "province")
            {
                sqlstr = sqlprovince;
            }
            else if (flag == "industry")
            {
                sqlstr = sqlindustry;
            }

            return GetDataSet(sqlstr);
        }

        public static DataSet GetIndustryList(int type)
        {
            string sql = "select MainName,MainIndustryID from YX_MainIndustry where IndustryType = " + type + " order by MainIndustryID";
            return GetDataSet(sql);
        }

        public static DataSet GetProvinceListByIndustry(int industryID)
        {
            string sql = "Select Distinct AreaID,AreaName From YX_AreaInfo Where AreaID in (Select Distinct AreaParentID From YX_AreaInfo Where AreaID in (Select Distinct AreaID From YX_IndustryArea Where IndustryID = " + industryID + "))";
            return GetDataSet(sql);
        }

        public static DataSet GetAreaListByProvinceAndIndustry(int industryID, int provinceID)
        {
            string sql = "Select b.AreaID,b.AreaName From YX_IndustryArea a join YX_AreaInfo b on a.AreaID = b.AreaID join YX_Industry c on a.IndustryID = c.MainIndustryID Where c.MainIndustryID = " + industryID + " and b.AreaParentID = " + provinceID;
            return GetDataSet(sql);
        }

        public static string GetNumberAmount(int industryID, int provinceID, int areaID)
        {
            string sql = "";
            if (industryID > -1 && provinceID == -1 && areaID == -1)
            {
                sql = "Select IsNull(Sum(Amount),0) From YX_IndustryArea Where IndustryID = " + industryID;
            }
            else if (industryID > -1 && provinceID > -1 && areaID == -1)
            {
                sql = "Select IsNull(Sum(Amount),0) From YX_IndustryArea Where IndustryID = " + industryID + " and AreaID in (Select AreaID From YX_AreaInfo Where AreaParentID = " + provinceID + " and AreaType = 1)";
            }
            else if (industryID > -1 && provinceID > -1 && areaID > -1)
            {
                sql = "Select IsNull(Sum(Amount),0) From YX_IndustryArea Where IndustryID = " + industryID + " and AreaID = " + areaID;
            }
            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString();
        }

        private static DataSet GetDataSet(string sql)
        {

            DataSet ds = null;
            try
            {
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
            }
            catch
            { }
            return ds;
        }
        #endregion

        //返回某地区某行业所拥有的号码数量的数据集
        public static DataSet AreaIndustryNum()
        {
            DataSet ds = null;
            string sql = "Select * From YX_Sms_Statistic";
            try
            {
                ds = GetDataSet(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return ds;

        }

        //更新历史记录
        public static DataSet GetNumbersUpdateLog()
        {
            DataSet ds = null;
            string sql = "Select IndustryName,RecordCount,Convert(varchar(10),CrDate,120) As CrDate From YX_SalesNumber_Log Where Convert(varchar(10),CrDate,120) = (Select Convert(varchar(10),Max(CrDate),120) From YX_SalesNumber_Log)";

            try
            {
                ds = GetDataSet(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// 分割短信内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static List<string> SplitMessage(string message,int seqNo)
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
    }
}
