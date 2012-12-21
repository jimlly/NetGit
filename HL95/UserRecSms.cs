using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Com.Yuantel.DBUtility;
using System.Net;
using System.IO;

namespace Com.Yuantel.HL95
{
    public class UserRecSms
    {
        UserRecSmsDB URDB = null;

        public DataSet GetNeedSyncUserList(string type)
        {
            URDB = new UserRecSmsDB();
            return URDB.GetNeedSyncUserListDB(type);
        }
        public bool ChkStr(Object obj)
        {
            try
            {
                if (obj.Equals("") || obj == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }

        }

        public DataSet GetNeedSyncSmsList(int SubCode)
        {
            URDB = new UserRecSmsDB();
            return URDB.GetNeedSyncSmsListDB(SubCode);
        }
        public void UpdateSucList(string SPNumber,int Types)
        {
            URDB = new UserRecSmsDB();
            URDB.UpdateSucList(SPNumber,Types);
        }

        public string GetWebRequest(DataRow dr,string url,string userAccount)
        {
            try
            {
                string mobile = dr["Mobile"].ToString();
                string recvTime = dr["RecvTime"].ToString();
                string contents = dr["Message"].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("Mobile=").Append(mobile)
                  .Append("&RecvTime=").Append(recvTime)
                  .Append("&UserName=").Append(userAccount)
                  .Append("&Contents=").Append(contents);

                string postData = sb.ToString();

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url + "?" + postData));

                //接收数据
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                string resp = sr.ReadToEnd();
                return resp;
            }
            catch (Exception ea)
            {
                throw ea;
            }
        }
    }
}
