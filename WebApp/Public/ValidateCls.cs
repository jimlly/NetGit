/**
 * 文件：ValidateCls.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：数据验证通用类
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
using System.Text.RegularExpressions;

/// <summary>
/// ValidateCls 的摘要说明
/// </summary>
public class ValidateCls
{
    public ValidateCls()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 验证是否为手机号码
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static bool IsMobile(string mobile)
    {
        try
        {
            Regex exp = new Regex("^(13|15|18)\\d{9}$");
            return exp.Match(mobile).Success;
        }
        catch
        {
            return false;
        }
    }
}
