/**
 * 文件：Home.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：当前账户信息页面
*/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;
using System.Collections.Generic;
using System.Reflection;

public partial class Home : BasePage
{
    private int seqNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            seqNo = int.Parse(Session["SEQNO"].ToString());

            long totalBalance = 0;  //总实用金额
            long totalDonatedBalance = 0; //总赠送金额
            try
            {
                IList<IB_AccountInfo> iB_AccountInf = Query.GetUserAccountList(seqNo, 71, 0, -1, out totalBalance, out totalDonatedBalance);
                Session["IB_AccountInfo_Sms"] = iB_AccountInf;
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserAccountList：" + ex.ToString());
            }            
            this.lt_Balance.Text = Global.ChangeRateFromDb((totalBalance + totalDonatedBalance)).ToString();
            try
            {
                this.lt_UserName.Text = Query.GetUserAccount(int.Parse(Session["SEQNO"].ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception("GetUserAccount：" + ex.ToString());
            } 

            try
            {
                //this.lt_SendingRows.Text = Query.GetSendingRows(int.Parse(Session["SEQNO"].ToString()));
                //DataSet dsNotice = Query.get_Notice(0, 7);
                //if (dsNotice != null && dsNotice.Tables.Count > 0 && dsNotice.Tables[0].Rows.Count > 0)
                //{
                //    this.lt_Title.Text = dsNotice.Tables[0].Rows[0]["Title"].ToString();
                //    this.lt_Notice.Text = dsNotice.Tables[0].Rows[0]["Content"].ToString();
                //    this.lt_Footer.Text = dsNotice.Tables[0].Rows[0]["Foot"].ToString();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("get_Notice：" + ex.ToString());
            }
        }
    }
}
