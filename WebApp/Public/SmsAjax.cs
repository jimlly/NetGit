/**
 * 文件：SmsAjax.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：短信ajax类
*/

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
using System.Text;

/// <summary>
/// SmsAjax 的摘要说明
/// </summary>
public class SmsAjax
{
    public SmsAjax()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 获取短信资费
    /// </summary>
    /// <returns></returns>
    [AjaxMethod]
    public static double GetUnitPrice()
    {
        return Public.UnitPrice;
    }

    /// <summary>
    /// 是否能重发
    /// </summary>
    /// <param name="msgID"></param>
    /// <returns>0-不能重发；1-能重发</returns>
    [AjaxMethod]
    public static int CanResend(string msgID)
    {
        return Query.CanResend(msgID);
    }

    /// <summary>
    /// 获取对应状态的记录数
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    [AjaxMethod]
    public static int GetRowCount(string msgID, int state)
    {
        return Query.GetRowCount(msgID,state);
    }

    [AjaxMethod]
    public static string GetMobileFromFile(string file)
    {
        StringBuilder sb = new StringBuilder();
        string filePath = "", fileName = "";
        filePath = file.Substring(0, file.LastIndexOf("\\"));
        fileName = file.Substring(file.LastIndexOf("\\")+1);
        DataSet ds = ImportUtil.OleDbFromCSV(filePath, fileName);
        if (ds == null)
        {
            return "-1";//文件无效
        }
        else 
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                return "-2";//文件中没有合法的联系人数据
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.Append(dr["手机号码"].ToString()).Append(",").Append(dr["姓名"].ToString()).Append(";\r\n");
                }
                return sb.ToString().Substring(0, sb.ToString().Length - 1);
            }
        }
    }

    [AjaxMethod]
    public static string GetCharCount()
    {
        return Public.CharCount.ToString();
    }
}
