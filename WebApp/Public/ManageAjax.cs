using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using AjaxPro;
using Com.Yuantel.MobileMsg.DAL;

/// <summary>
/// ManageAjax 的摘要说明
/// </summary>
public class ManageAjax
{
    public ManageAjax()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    [AjaxMethod]
    public int GetSmsCount()
    {
        int count = Query.GetApproveMsg();
        return count;
    }
}
