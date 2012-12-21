using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

namespace Monitor
{
    public partial class MonitorService : ServiceBase
    {
        public MonitorService()
        {
            InitializeComponent();
        }

        private string MobileList = ConfigurationManager.AppSettings["MobileList"].ToString();
        private Thread subThread = null;

        protected override void OnStart(string[] args)
        {
            subThread = new Thread(new ThreadStart(DoAction));
            subThread.Start();
        }

        protected override void OnStop()
        {
            try
            {
                subThread.Abort();
            }
            catch
            { }
        }

        /// <summary>
        /// 获取批次号
        /// </summary>
        /// <param name="_clsid"></param>
        /// <returns></returns>
        private string GetBatchNo()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ytSys"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UP_Sys_Common_Code_CreateNew";
                cmd.Parameters.Add("@ClsID", SqlDbType.Int);
                cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar, 50);
                cmd.Parameters[0].Value = 20;
                cmd.Parameters[1].Direction = ParameterDirection.Output;
                cmd.ExecuteScalar();
                return cmd.Parameters[1].Value.ToString();
            }
        }

        private void DoAction()
        {
            while (true)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Monitor"].ConnectionString))
                    {
                        cn.Open();

                        SqlDataAdapter da = new SqlDataAdapter("Select ID,EventTime,Content From TellMe", cn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            SqlCommand cmd = null;
                            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ytSms"].ConnectionString);
                            cnn.Open();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                try
                                {
                                    string batchno = GetBatchNo();
                                    cmd = new SqlCommand();
                                    cmd.Connection = cnn;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = string.Format("Insert into IB_Sms_Send_Bill (MsgID,SeqNo,Sender,Priority,SubmitTime,SendTime,Message,State,MsgType) values ('{0}',{1},'{2}',{3},'{4}','{5}','{6}',{7},1)", batchno, 0, "Solarwinds", 2, DateTime.Now, DateTime.Now, ds.Tables[0].Rows[i]["Content"].ToString(), 1);
                                    cmd.ExecuteNonQuery();

                                    string[] mobile = MobileList.Split(',');
                                    for (int j = 0; j < mobile.Length; j++)
                                    {
                                        cmd.CommandText = string.Format("insert into IB_Sms_Send_Detail_Bill (MsgID,Mobile,Name) values ('{0}','{1}','{2}')", batchno, mobile[j], "");
                                        cmd.ExecuteNonQuery();
                                    }

                                    cmd.Connection = cn;
                                    cmd.CommandText = string.Format("delete from TellMe where id = {0}", ds.Tables[0].Rows[i]["ID"].ToString());
                                    cmd.ExecuteNonQuery();
                                }
                                catch
                                { }
                            }
                            cnn.Close();
                        }
                    }
                }
                catch
                { }

                Thread.Sleep(1000);
            }
        }
    }
}
