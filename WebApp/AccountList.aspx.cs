using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Yuantel.MobileMsg.Model;

public partial class AccountList : System.Web.UI.Page
{
    protected IList<IB_AccountInfo> ib_AccountInfo = null;
    protected string msg = string.Empty;
    protected int totalcount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ib_AccountInfo = Session["IB_AccountInfo_Sms"] as IList<IB_AccountInfo>;
            ShowAccountInfo();
        }
    }

    private void ShowAccountInfo()
    {
        if (ib_AccountInfo != null)
        {           
            totalcount = ib_AccountInfo.Count;
            rpt.DataSource = ib_AccountInfo;
            rpt.DataBind();
            if (ib_AccountInfo.Count <= 0)
            {
                msg = "您没有任何金额账户";
            }           
        }
        else
        {
            msg = "程序异常，请联系管理员！";
        }
    }
}