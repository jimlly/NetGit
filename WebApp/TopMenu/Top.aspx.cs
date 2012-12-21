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
using System.Text;

public partial class TopMenu_YXTopTemp : Page
{
    //public User_GetMenu getUserMenu = null;
    //public Sys_Common_MenuInf menuInf = null;
    //Fax_LoginInf faxLoginInf = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page_Init();
        //LogoInit();
    }

    //public void Page_Init()
    //{
    //    string Linenum = SessionInfo.getLineNum();
    //    string Useraccount = SessionInfo.getUseraccount();
    //    string Extension = SessionInfo.getExtension();
    //    int userType = SessionInfo.getUserType();
    //    if (Linenum == "01058851400")
    //    {
    //        Literal1.Text = Useraccount;
    //        Literal2.Text = "";
    //    }
    //    else
    //    {
    //        StringBuilder phone = new StringBuilder();
    //        if (userType == 7)
    //        {
    //            phone.Append("");
    //        }
    //        else
    //        {
    //            phone.Append("<span style=\"color:#022490; font-weight:bold; font-size:12px;\">您的传真号是：</span><span style=\"color:#CB0021;\">" + Linenum);

    //            if (Extension != "" && Extension != "0")
    //            {
    //                if (userType == 2 || userType == 4 || userType == 6 || userType == 11 || userType == 10)
    //                {
    //                    phone.Append("," + Extension);
    //                }
    //            }
    //            phone.Append(" </span>");

    //        }
    //        Literal1.Text = Useraccount;
    //        Literal2.Text = phone.ToString();
    //    }

    //}
    //public void LogoInit()
    //{
    //    string appCode = SessionInfo.getAppCode().ToString();
    //    string ImageUrl = "../Images/tellogo.gif";
    //    if (appCode == "yuantel")
    //    {
    //        ImageUrl = "../Images/logo.gif";
    //    }
    //    else
    //    {
    //        try
    //        {
    //            Sys_Common_AppInf sys_Common_AppInf = Global.Get_Sys_Common_AppInf_Model(appCode);
    //            if (sys_Common_AppInf != null)
    //            {
    //                string logoPath = ConfigurationManager.AppSettings["logoPath"].ToString().Trim();
    //                string LogoFile = sys_Common_AppInf.LogoFile.ToString();
    //                if (LogoFile != "" && LogoFile != null)
    //                {
    //                    ImageUrl = logoPath + LogoFile;
    //                }
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            ImageUrl = "../Images/logo.gif";
    //        }
    //    }
    //    LogoPic.ImageUrl = ImageUrl;
    //}
    protected void LinkLoginOut_Click(object sender, EventArgs e)
    {
    //    Session["faxLoginInf"] = null;
    //    Session.Clear();
    //    Session.RemoveAll();
    //    Response.Redirect("../User/LoginOut.aspx");
    }
}