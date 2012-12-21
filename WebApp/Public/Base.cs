/**
 * 文件：Base.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：格式转换基类
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
using System.Text;

/// <summary>
/// Base 的摘要说明
/// </summary>
public class Base 
{
    public Base()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 获取文件后缀名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetExtName(string fileName)
    {
        return fileName.Substring(fileName.LastIndexOf("."));
    }

    /// <summary>
    /// 获取当前日期（年月日时分秒）
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentTime(DateTime time,int type)
    {
        string date = "";
        switch (type)
        {
            case 1:
                date = time.ToString("yyyyMMddHHmmss");
                break;
            case 2:
                date = time.ToString("yyyy-MM-dd HH:mm");
                break;
            default:
                break;
        }
        return date;
    }

    public static string GetCurrentTime(string time)
    {
        if (time.Length > 0)
        {
            DateTime dt = Convert.ToDateTime(time);
            return GetCurrentTime(dt, 2);
        }
        else
        {
            return "";
        }
    }

    public static string GetStatus(string code)
    {
        switch (code)
        { 
            case "0":
                return "等待审核";
            case "1":
                return "审核通过";
            case "2":
                return "审核未通过";
            case "3":
                return "正在计费";
            case "4":
                return "余额不足";
            case "5":
                return "正在发送";
            case "9":
                return "发送完毕";
            case "20":
                return "违规信息未通过";
            case "21":
                return "损害他人利益未通过";
            default:
                return "";
        }
    }

    public static string GetChannelName(int channelID)
    {
        switch (channelID)
        { 
            case 0:
                return "3SDK-EMY-0130-JIYQM";
            case 5:
                return "3SDK-EMY-0130-JJVNQ";
            case 6:
                return "3SDK-EMY-0130-JJXMS";
            case 7:
                return "3SDK-EMY-0130-KFULT";
            case 13:
                return "3SDK-EMY-0130-LBWQT";
            case 14:
                return "3SDK-EMY-0130-LBVSS";
            case 20:
                return "鸿联九五";
            default:
                return "未知";
        }
    }

    public static string GetSendStatus(string code)
    {
        switch (code)
        {
            case "0":
                return "等待发送";
            case "1":
                return "正在发送";
            case "2":
                return "发送成功";
            case "5":
                return "正在发送";
            case "9":
                return "发送失败";
            case "4":
                return "余额不足";
            default:
                return "";
        }
    }

    public static string SubString(string str, int length)
    {
        if (str.Length > length)
        { 
            str = str.Substring(0,length) + "...";
        }
        return str;
    }

    public static bool IsAcceptSend(DateTime date)
    {
        int hour = date.Hour;
        int minute = date.Minute;

        int beginh = int.Parse(Public.BeginTime.Split(':')[0]);
        int beginm = int.Parse(Public.BeginTime.Split(':')[1]);
        int endh = int.Parse(Public.EndTime.Split(':')[0]);
        int endm = int.Parse(Public.EndTime.Split(':')[1]);

        if ((hour == beginh && minute < beginm) || hour < beginh || hour > endh || (hour == endh && minute > endm))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //屏蔽手机号码最后一位
    public static string ShieldMobile(string mobile)
    {
        return mobile.Substring(0, 10) + "*";
    }
        
    //营销短信发件箱详细查看图片
    public static string getPicUrl(string id)
    {
        string PicUrl = string.Empty;
        //5正在发送,//9发送完毕
        if( id == "5" || id == "9" )
        {
            PicUrl = "../Images/xiangxi3.gif";
        }
        else
        {
            PicUrl = "../Images/xiangxi2.gif";
        }
        return PicUrl;
    }
}
