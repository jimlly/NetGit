using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using Com.Yuantel.DBUtility;
using System.Data;
using System.Configuration;

namespace Com.Yuantel.HL95
{
    public class SmsReceive : ISmsReceive
    {
        //private const string SQL_INSERT_MSG = "INSERT INTO IB_Sms_Receive(SPNumber,SubCode,Mobile,Message,RecvTime,DoFlag,DoDate) VALUES(@SPNumber,@SubCode,@Mobile,@Message,@RecvTime,@DoFlag,@DoDate);";
        private const string SQL_INSERT_MSG = "INSERT INTO IB_Sms_Receive(SPNumber,SubCode,Mobile,Message,RecvTime,DoFlag) VALUES(@SPNumber,@SubCode,@Mobile,@Message,@RecvTime,@DoFlag);";
        private const string PARM_SPNumber = "@SPNumber";
        private const string PARM_SubCode = "@SubCode";
        private const string PARM_Mobile = "@Mobile";
        private const string PARM_Message = "@Message";
        private const string PARM_RecvTime = "@RecvTime";
        private const string PARM_DoFlag = "@DoFlag";
        private const string PARM_DoDate = "@DoDate";

        public string ReceiveSms(string Mobiles, string Content, string SpNumber)
        {
            string subCode = "";
            string result = "200";
            subCode = SpNumber.Substring(17);
            if (subCode.Equals(""))
            {
                subCode = "-1";
            }
            string ConnectionString = SqlHelper.ConnectionStringServer;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlParameter[] parms;

                SqlCommand cmd = new SqlCommand();

                // Get the cached parameters
                parms = GetMsgParameters();

                // Fill the parameters;
                parms[0].Value = SpNumber;
                parms[1].Value = Convert.ToInt32(subCode);
                parms[2].Value = Mobiles;
                parms[3].Value = Content;
                parms[4].Value = DateTime.Now;
                parms[5].Value = 0;
                //parms[6].Value = DateTime.Now;

                // Bind each parameter
                foreach (SqlParameter parm in parms)
                    cmd.Parameters.Add(parm);

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                //SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    cmd.CommandText = SQL_INSERT_MSG;
                    
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    result = "100";
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return result;
        }
        private SqlParameter[] GetMsgParameters()
        {

            SqlParameter[] parms = SqlHelper.GetCachedParameters(SQL_INSERT_MSG);

            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter(PARM_SPNumber,SqlDbType.VarChar, 20),
                    new SqlParameter(PARM_SubCode,SqlDbType.Int),
                    new SqlParameter(PARM_Mobile, SqlDbType.VarChar,20),
                    new SqlParameter(PARM_Message, SqlDbType.VarChar, 200),
                    new SqlParameter(PARM_RecvTime, SqlDbType.DateTime, 8),
                    new SqlParameter(PARM_DoFlag, SqlDbType.Int)//,
                   // new SqlParameter(PARM_DoDate,SqlDbType.DateTime,8)
                };
                SqlHelper.CacheParameters(SQL_INSERT_MSG, parms);
            }

            return parms;
        }
    }
}
