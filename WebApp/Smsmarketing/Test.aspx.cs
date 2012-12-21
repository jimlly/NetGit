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

public partial class Smsmarketing_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnTest_Click(object sender, EventArgs e)
    {
        YxMsg msg = new YxMsg();
        msg.SeqNo = 94;
        msg.SendTime = DateTime.Now;
        msg.Message = this.txtContent.Text;
        msg.AreaIndustry = "-1|-1|-1";
        msg.StartID = 1;
        msg.EndID = 50;
        msg.SendCounts = 50;
        YxSms.Send(msg);
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('提交成功！');</script>");
    }
}
