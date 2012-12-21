using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Com.Yuantel.DBUtility;

namespace Com.Yuantel.HL95
{
    public class UserRecSmsDB
    {
        public DataSet GetNeedSyncUserListDB(string type)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@Type", SqlDbType.Int)
            };
                parameters[0].Value = type;
                return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "Sms_GetNeedSyncUserList", parameters);
            }
            catch (Exception ea)
            {
                throw ea;
            }
        }
        public DataSet GetNeedSyncSmsListDB(int SubCode)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@SubCode", SqlDbType.Int)
            };
                parameters[0].Value = SubCode;
                return SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "Sms_GetNeedSync_RecvSmsData", parameters);
            }
            catch (Exception ea)
            {
                throw ea;
            }
        }
        public void UpdateSucList(string SPNumber, int Types)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@SPNumber",SqlDbType.VarChar,20),
            new SqlParameter("@Type",SqlDbType.Int)
            };
                parameters[0].Value = SPNumber;
                parameters[1].Value = Types;
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "Sms_GetNeedSync_UpdateRecvDoFlag", parameters);
            }
            catch (Exception ea)
            {
                throw ea;
            }
        }
    }
}
