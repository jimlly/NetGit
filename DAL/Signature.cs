using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Data;
using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.DBUtility;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class SignatureDal
    {
        public static int Insert(Signature sign)
        {
            int result = 0;
            try
            {
                string sql = string.Format("Insert Into IB_Sms_Signature(SeqNo,SignTitle,SignContent) Values ({0},'{1}','{2}')",sign.SeqNo,sign.Title,sign.Content);
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
            }
            catch (Exception ex)
            {
                throw new Exception("SignatureDal Insert Exception: " + ex.ToString());
            }
            return result;
        }

        public static int Delete(Signature sign)
        {
            int result = 0;
            try
            {
                string sql = string.Format("Delete From IB_Sms_Signature Where ID = {0}", sign.ID);
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
            }
            catch (Exception ex)
            {

                throw new Exception("SignatureDal Delete Exception: " + ex.ToString());
            }
            return result;
        }

        public static int Update(Signature sign)
        {
            int result = 0;
            try
            {
                string sql = string.Format("Update IB_Sms_Signature Set SignTitle = '{0}',SignContent = '{1}' Where ID = {2}", sign.Title,sign.Content,sign.ID);
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
            }
            catch (Exception ex)
            {
                throw new Exception("SignatureDal Update Exception: " + ex.ToString());
            }
            return result;
        }

        public static DataSet Select(int seqNo)
        {
            DataSet ds = null;
            try
            {
                string sql = string.Format("Select ID,SignTitle,SignContent From IB_Sms_Signature Where SeqNo = {0}",seqNo);
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.Text, sql, null);
            }
            catch (Exception ex)
            {
                throw new Exception("SignatureDal Select Exception: " + ex.ToString());
            }
            return ds;
        }
    }
}
