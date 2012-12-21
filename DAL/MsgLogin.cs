/**
 * 文件：MsgLogin.cs
 * 作者：朱孟峰
 * 日期：2009-02-27
 * 描述：
*/

using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Security;
using System.Web;


using Com.Yuantel.DBUtility;

namespace Com.Yuantel.Msg.DAL
{
    public class MsgLogin : ILogin
    {

        #region Login

        private static readonly int serFlag = 7;

        private const string SQL_LOGIN = "UP_Common_NewLogin";
        private const string PARM_COMPANYNO = "@CompanyNo";
        private const string PARM_ACCOUNT = "@UserAccount";
        private const string PARM_PWD = "@Password";
        private const string PARM_SERFLAG = "@SerFlag";
        private const string PARM_SEQNO = "@SeqNo";
        private const string PARM_COMPID = "@CompId";
        private const string PARM_USERNAME = "@UserName";
        private const string PARM_COMPANYNAME = "@CompanyName";
        private const string PARM_APPCODE = "@AppCode";
        private const string PARM_RETVALUE = "ReturnValue";

        public SessionInfo Login(string userName, string pwd)
        {
            // Create the connection to the database
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringServer_yuantel))
            {
                SqlParameter[] parms;

                SqlCommand cmd = new SqlCommand();

                // Get the cached parameters
                parms = GetLoginParameters();

                // Fill the parameters;
                parms[0].Value = "";
                parms[1].Value = userName;
                parms[2].Value = pwd;
                parms[3].Value = serFlag;
                parms[4].Direction = ParameterDirection.Output;
                parms[5].Direction = ParameterDirection.Output;
                parms[6].Direction = ParameterDirection.Output;
                parms[7].Direction = ParameterDirection.Output;
                parms[8].Direction = ParameterDirection.Output;

                // Bind each parameter
                foreach (SqlParameter parm in parms)
                    cmd.Parameters.Add(parm);

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = SQL_LOGIN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                int returnCode = (int)cmd.Parameters[PARM_RETVALUE].Value;
                switch (returnCode)
                {
                    case 1:
                        SessionInfo session = new SessionInfo();
                        session.SessionId = System.Guid.NewGuid().ToString();
                        session.Account = userName;
                        session.ExpiredTime = DateTime.Now;
                        session.SeqNo = (int)cmd.Parameters[4].Value;
                        session.CompId = (int)cmd.Parameters[5].Value;
                        session.UserName = (string)cmd.Parameters[6].Value;
                        session.CompanyName = (string)cmd.Parameters[7].Value;
                        session.AppCode = (string)cmd.Parameters[8].Value;
                        return session;
                    case -1:
                        throw new PwdErrorException("用户账号或密码错误");
                    case 0:
                        throw new AccountExpiredException("帐号不可用");
                    case -3:
                        throw new PermissionException("权限不足");
                }

                cmd.Parameters.Clear();
            }

            return null;
        }

        private SqlParameter[] GetLoginParameters()
        {

            SqlParameter[] parms = SqlHelper.GetCachedParameters(SQL_LOGIN);

            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter(PARM_COMPANYNO,SqlDbType.VarChar, 20),
                    new SqlParameter(PARM_ACCOUNT, SqlDbType.VarChar, 80),
                    new SqlParameter(PARM_PWD, SqlDbType.VarChar, 32),
                    new SqlParameter(PARM_SERFLAG, SqlDbType.Int),
                    new SqlParameter(PARM_SEQNO, SqlDbType.Int),
                    new SqlParameter(PARM_COMPID, SqlDbType.Int),
                    new SqlParameter(PARM_USERNAME, SqlDbType.VarChar, 50),
                    new SqlParameter(PARM_COMPANYNAME, SqlDbType.VarChar, 50),
                    new SqlParameter(PARM_APPCODE, SqlDbType.VarChar, 50),
                    new SqlParameter(PARM_RETVALUE, SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelper.CacheParameters(SQL_LOGIN, parms);
            }

            return parms;
        }

        #endregion
    }
}
