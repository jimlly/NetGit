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

public partial class SendMsg_AddressBook : System.Web.UI.Page
{
    protected string serviceUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        serviceUrl = "http://addr.yuantel.net/Service_AddFromSendGroups.aspx?" +
         "SerFlagList=" + AddrPublic.md5Encrypt("7") +
         "&PhonePropertys=" + AddrPublic.md5Encrypt("") +
         "&SessionID=" + AddrPublic.md5Encrypt(Session["SessionID"].ToString()) +
         "&SessionKey=" + AddrPublic.md5Encrypt(Session["SessionKey"].ToString()) +
         "&OriSerFlag=" + AddrPublic.md5Encrypt("7");
        Response.Redirect(serviceUrl);
    }
}
