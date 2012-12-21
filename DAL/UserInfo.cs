using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Com.Yuantel.DBUtility;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class UserInfo
    {
        public static DataSet GetAppInfo(string appCode)
        {
            DataSet ds = null;
            try
            {
                string sql = string.Format("Select LogoFile as LogoName,SessionOutUrl as LogoutUrl,LoginErrorUrl,'' as CopyRight From Sys_Common_App Where IAppCode = '{0}'", appCode);
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer_yuantel, CommandType.Text, sql, null);
            }
            catch
            {
                
            }
            return ds;
        }

        public static int ChangePwd(int seqNo, string oldPwd, string newPwd)
        {
            int result = 0;
            try
            { 
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@SeqNo",SqlDbType.Int),
                    new SqlParameter("@OldPwd",SqlDbType.VarChar,32),
                    new SqlParameter("@NewPwd",SqlDbType.VarChar,32),
                    new SqlParameter("@Result",SqlDbType.Int)
                };
                param[0].Value = seqNo;
                param[1].Value = oldPwd;
                param[2].Value = newPwd;
                param[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer_yuantel, CommandType.StoredProcedure, "Sms_SP_ChangePwd", param);
                if (param[3].Value != null)
                {
                    result = int.Parse(param[3].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
