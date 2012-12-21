using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Com.Yuantel.DBUtility;
using System.Data.SqlClient;
using Com.Yuantel.MobileMsg.Model;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class PhraseDal
    {
        public static int Insert(Phrase phrase)
        {
            int result = 0;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@SeqNo", phrase.SeqNo),
                    new SqlParameter("@Phrase",phrase.Phrase1)
            };
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "UP_Sms_Phrase_Insert", param);
            }
            catch (Exception ex)
            {
                throw new Exception("PhraseDal Insert Exception : " + ex.ToString());
            }
            return result;
        }

        public static int Delete(Phrase phrase)
        {
            int result = 0;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@Id",phrase.Id)
                };
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "UP_Sms_Phrase_Del", param);
            }
            catch (Exception ex)
            {

                throw new Exception("PhraseDal Delete Exception: " + ex.ToString());
            }
            return result;
        }

        public static int Update(Phrase phrase)
        {
            int result = 0;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@Id", phrase.Id),
                    new SqlParameter("@Phrase",phrase.Phrase1)
            };
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "UP_Sms_Phrase_Update", param);
            }
            catch (Exception ex)
            {
                throw new Exception("PhraseDal Update Exception: " + ex.ToString());
            }
            return result;
        }

        public static DataSet Select(int seqNo)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@SeqNo", seqNo)
                };
                ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringServer, CommandType.StoredProcedure, "UP_Sms_Phrase_Select", param);
            }
            catch (Exception ex)
            {
                throw new Exception("PhraseDal Select Exception: " + ex.ToString());
            }
            return ds;
        }
    }
}
