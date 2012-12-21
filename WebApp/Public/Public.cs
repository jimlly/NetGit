/**
 * 文件：Public.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：全局变量类
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
using System.Collections;

using Com.Yuantel.MobileMsg.DAL;

/// <summary>
/// Public 的摘要说明
/// </summary>
public class Public
{
    public Public()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region 用户信息
    /// <summary>
    /// 用户信息
    /// </summary>
    public struct UserInfo
    {
        public static int SEQNO;
        public static string USERNAME;
    }
    #endregion

    #region 系统设置
    /// <summary>
    /// 禁用关键字
    /// </summary>
    private ArrayList _keyWords;
    public ArrayList KeyWords
    {
        get { return this._keyWords; }
        set { this._keyWords = value; }
    }

    /// <summary>
    /// 一次提交的最大条数
    /// </summary>
    public static readonly int PerMaxCount = Manage.GetConfig("Count", "MaxCount").Trim().Equals("") ? int.MaxValue : int.Parse(Manage.GetConfig("Count", "MaxCount").Trim());

    /// <summary>
    /// 开放开始时间
    /// </summary>
    public static readonly string BeginTime = Manage.GetConfig("BeginTime", "OpenTime").Trim().Equals("") ? "0:00" : Manage.GetConfig("BeginTime", "OpenTime").Trim();

    /// <summary>
    /// 开放结束时间
    /// </summary>
    public static readonly string EndTime = Manage.GetConfig("EndTime", "OpenTime").Trim().Equals("") ? "23:59" : Manage.GetConfig("EndTime", "OpenTime").Trim();

    /// <summary>
    /// 群发录文件保存路径
    /// </summary>
    public static readonly string AddressPath = ConfigurationManager.AppSettings["AddressPath"].ToString();

    /// <summary>
    /// 短信资费
    /// </summary>
    public static readonly double UnitPrice = double.Parse(ConfigurationManager.AppSettings["UnitPrice"].ToString());

    /// <summary>
    /// 短信最大字符数
    /// </summary>
    public static readonly int CharCount = int.Parse(ConfigurationManager.AppSettings["CharCount"].ToString());

    /// <summary>
    /// 黑字典过滤开关
    /// </summary>
    public static readonly int BlackDic = int.Parse(ConfigurationManager.AppSettings["BlackDic"].ToString());

    #endregion
}