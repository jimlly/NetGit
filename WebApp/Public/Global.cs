using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///Global 的摘要说明
/// </summary>
public class Global
{
	public Global()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    #region 产生验证码
    private const string CheckCodeKey = "CheckCode";
    /// <summary>
    /// 产生一个新的验证码
    /// </summary>
    /// <returns></returns>
    public static string CheckCode()
    {
        int number;
        char code;
        string checkCode = String.Empty;

        System.Random random = new Random();

        for (int i = 0; i < 4; i++) // 控制长度
        {
            number = random.Next();

            /* if (number % 2 == 0)
                 code = (char)('0' + (char)(number % 10));
             else*/
            code = (char)('0' + (char)(number % 10)); // 使用数字

            //code = (char)('A' + (char)(number % 26)); // 使用字母

            checkCode += code.ToString();
        }

        System.Web.SessionState.HttpSessionState Session = System.Web.HttpContext.Current.Session;
        Session[CheckCodeKey] = checkCode;

        return checkCode;
    }
    /// <summary>
    /// 获取已产生的验证码
    /// </summary>
    /// <returns></returns>
    public static string GetCheckCode()
    {

        System.Web.SessionState.HttpSessionState Session = System.Web.HttpContext.Current.Session;
        Object o = Session[CheckCodeKey];
        if (o != null) return (string)o;
        else return "";

    }

    #endregion
    #region 获取客户端信息
    /// <summary>
    /// 获取客户端IP
    /// </summary>
    /// <returns></returns>
    public static String GetClientIP()
    {

        string result = "";
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"] == null)
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        else
        {
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
        }


        if (result == "")
        {
            return "";
        }
        return result;
    }

    /// <summary>
    /// 获取客户端访问来源Url
    /// </summary>
    /// <returns></returns>
    public static String GetLinkFromUrl()
    {
        String requestUrl = "";
        if (HttpContext.Current.Request.UrlReferrer != null)
        {
            requestUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
        }
        return requestUrl;
    }

    /// <summary>
    /// 获取客户端Mac地址
    /// </summary>
    /// <returns></returns>
    public static String GetClientMac()
    {
        String requestUrl = "";
        return requestUrl;
    }
    #endregion

    #region 获取格式化后的金额(从DB取出)
    public static Decimal ChangeRateFromDb(long _lMoney)
    {
        Decimal _dMoney = 0;

        try
        {
            _dMoney = Convert.ToDecimal(Convert.ToDouble(_lMoney) / 10000);
        }
        catch
        {
        }
        return _dMoney;
    }
    #endregion

    #region 获取格式化后的金额(存入DB)
    public static long ChangeRateToDb(Decimal _dMoney)
    {
        long _lMoney = 0;

        try
        {
            _lMoney = Convert.ToInt64(_dMoney * 10000);
        }
        catch
        {
        }
        return _lMoney;
    }
    #endregion
}