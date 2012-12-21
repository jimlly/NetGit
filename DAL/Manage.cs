/**
 * 文件：Manage.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：后台管理数据操作类
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Com.Yuantel.DBUtility;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class Manage
    {
        /// <summary>
        /// 统计每日的发送状况
        /// </summary>
        /// <param name="succeed">成功数量</param>
        /// <param name="failed">失败数量</param>
        /// <param name="sending">待发数量</param>
        /// <returns></returns>
        public static DataSet GetSendStatus(out int succeed,out int failed,out int sending)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "select SubString(Convert(varchar,sendtime, 120), 12, 13) + '点' as TimeZone,sum(case state when 2 then counts end) as Succeed,sum(case state when 3 then counts end) as Failed from (select sendtime=SubString(Convert(varchar,sendtime, 120), 1, 13),state,count(1) as counts from IB_Sms_Send_Detail_Bill where sendtime is not null and Convert(varchar,sendtime, 120) like SubString(Convert(varchar,getdate(), 120), 1, 10) + '%' group by SubString(Convert(varchar,sendtime, 120), 1, 13),state) as temp group by sendtime order by sendtime";
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer,CommandType.Text,sql,null);
                sql = "select isnull(sum(case when state = 2 then counts end),0) as succeed,isnull(sum(case when state = 3 then counts end),0) as failed,isnull(sum(case when state <> 2 and state <> 3 then counts end),0) as sending from (select state,count(state) as counts from IB_Sms_Send_Detail_Bill where sendtime >= SubString(Convert(varchar,getdate(),120),1,10) + ' 00:00:00' and sendtime <= SubString(Convert(varchar,getdate(),120),1,10) + ' 23:59:59' group by state) as Temp";
                DataSet ds2 = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer,CommandType.Text,sql,null);
                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                {
                    succeed = int.Parse(ds2.Tables[0].Rows[0]["succeed"].ToString());
                    failed = int.Parse(ds2.Tables[0].Rows[0]["failed"].ToString());
                    sending = int.Parse(ds2.Tables[0].Rows[0]["sending"].ToString());
                }
                else
                {
                    succeed = 0;
                    failed = 0;
                    sending = 0;
                }
            }
            catch
            {
                ds = null;
                succeed = 0;
                failed = 0;
                sending = 0;
            }
            return ds;
        }

        /// <summary>
        /// 获取某项配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            string config = "";
            try
            {
                string sql = string.Format("select NodeValue from IB_Sms_Config where NodeName = '{0}'",key);
                config = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null).ToString();
            }
            catch 
            {
                config = "";
            }
            return config;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int SetConfig(string key, string value)
        {
            int result = 0;
            try
            {
                string sql = string.Format("update IB_Sms_Config set NodeValue = '{0}' where NodeName = '{1}'", value, key);
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取参数配置值
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetConfig(string nodeName,string key)
        {
            try
            {
                string sql = string.Format("select NodeValue.value('(root/{0})[1]','NVARCHAR(MAX)') from IB_Sms_Config where NodeName = '{1}'", nodeName,key);
                return SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringServer,CommandType.Text,sql,null).ToString();
            }
            catch 
            {
                return "";
            }
        }
    }
}
