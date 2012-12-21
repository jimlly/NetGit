//==================================================================
// Copyright (C) 2008-2009 北京远特通信技术有限公司
// 文件名:Public.cs
// 作 者：王清雅
// 日 期：2009/03/17
// 描 述：公共处理类
// 版 本：1.0.0.0
// 修改历史纪录
// 版 本  修改时间  修改人 修改内容
// 1.0.0.1 2009-04-22 骆进  增加产生验证码和字符串进行MD5加密
//==================================================================
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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.SessionState;
using System.Security.Cryptography;
//using WebApp.Public;

public class AddrPublic
{
    public AddrPublic() { }

    #region 字符串进行MD5加密
    /// <summary>
    /// 字符串进行MD5加密
    /// </summary>
    /// <param name="NotEncryptString"></param>
    /// <returns>进行MD5加密后的字符串</returns>
    public static string GetMD5String(string NotEncryptString)
    {
        string MD5Str;
        try
        {
            MD5Str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(NotEncryptString, "MD5");
        }
        catch(Exception ex)
        {
            MD5Str = "";
            //记录错误信息
            LogHelper.Error("Public GetMD5String() 字符串进行MD5加密失败 " + ex.ToString());

        }
        return MD5Str;
    }
    #endregion

    #region girdview数据绑定DataSet
    public static void BindGridView(System.Web.UI.WebControls.GridView GV, DataSet ds)
    {
        GV.DataSource = ds.Tables[0].DefaultView;
        GV.DataBind();
    }
    #endregion

    #region girdview数据绑定DataTable
    public static void BindGridViewDt(System.Web.UI.WebControls.GridView GV, DataTable dt)
    {
        GV.DataSource = dt.DefaultView;
        GV.DataBind();
    }
    #endregion

    #region 分页控件列表绑定
    public static void pagerbind(Wuqi.Webdiyer.AspNetPager AspNetPager1, int allcount, int pagecount)
    {

        AspNetPager1.RecordCount = allcount;
        AspNetPager1.CustomInfoHTML = "&nbsp;&nbsp;记录总数：<font color=\"blue\"><b>" + AspNetPager1.RecordCount.ToString() + "</b></font>";
        AspNetPager1.CustomInfoHTML += "&nbsp; 总页数：<font color=\"blue\"><b>" + AspNetPager1.PageCount.ToString() + "</b></font>";
        AspNetPager1.CustomInfoHTML += "&nbsp; 当前页：<font color=\"red\"><b>" + AspNetPager1.CurrentPageIndex.ToString() + "</b></font>";
    }
    #endregion

    public static byte[] getbyte(string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

    ///
    ///md5加密解密   
    ///<param   name="strkey">密钥不限定位数</param>   
    public static string md5Encrypt(String plainText)
    {
        string strkey = "wang";
        if (plainText.Trim() == "") return "";
        string encrypted = null;
        byte[] key = getbyte(strkey);
        try
        {
            byte[] inputBytes = getbyte(plainText);
            byte[] pwdhash = null;
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            pwdhash = hashmd5.ComputeHash(key);
            hashmd5 = null;
            //   Create   a   new   TripleDES   service   provider     
            TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
            tdesProvider.Key = pwdhash;
            tdesProvider.Mode = CipherMode.ECB;

            encrypted = Convert.ToBase64String(tdesProvider.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
        }
        catch (Exception e)
        {
            throw new Exception("Public md5Encrypt() md5加密解密 " + e.ToString());
        }
        return encrypted.Replace("+", "%2B");
    }
    ///   <summary>   
    ///   md5解密   
    ///   </summary>   
    ///   <param   name="encryptedString"></param>   
    ///   <param   name="strkey">密钥</param>   
    ///   <returns></returns>   
    public static string md5Decrypt(string encryptedString)
    {
        string strkey = "wang";
        if (encryptedString.Trim() == "") return "";
        string decyprted = null;
        byte[] inputBytes = null;
        byte[] key = getbyte(strkey);
        try
        {
            encryptedString = encryptedString.Replace("%2B","+");
            inputBytes = Convert.FromBase64String(encryptedString);

            byte[] pwdhash = null;
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            pwdhash = hashmd5.ComputeHash(key);
            hashmd5 = null;
            TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
            tdesProvider.Key = pwdhash;
            tdesProvider.Mode = CipherMode.ECB;
            decyprted = ASCIIEncoding.UTF8.GetString(tdesProvider.CreateDecryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
        }
        catch (Exception e)
        {
            throw new Exception("Public md5Decrypt()  md5解密失败 " + e.ToString());
        } 
        return decyprted;
    }

    #region 根据Property返回属性名
    public static string getPropertyName(string property)
    {
        string propertyName = "无属性";
        if (property == "1")
            propertyName = "办公电话";
        else if (property == "2")
            propertyName = "手机号码";
        else if (property == "3")
            propertyName = "传真号码";
        else if (property == "4")
            propertyName = "家庭电话";
        return propertyName;
    }
    #endregion

    #region 组共享状态的图片 (left页面的组图标)
    public static string getGroupPicUrl(string ShareType)//0-自己非共享联系组 1-自己共享联系组 2-他人共享联系组
    {
        string PicUrl = "../Images/g_self_nonShared.gif";//自己非共享联系组
        if (ShareType == "1")
            PicUrl = "../Images/g_self_shared.gif";//自己共享联系组
        else if (ShareType == "2")
            PicUrl = "../Images/g_other_shared.gif";//他人共享联系组

        return PicUrl;
    }
    #endregion


    #region 群发组共享图片的alt信息
    public static string getSendGroupAlt(string flag)
    {
        string alt = "自己的非共享群发组";
        if (flag == "1")
            alt = "自己的共享群发组";
        else if (flag == "2")
            alt = "他人的共享群发组";
        return alt;
    }
    #endregion

    #region 群发组用户共享状态的图片（SendGroup_List页面的联系人图标）

    // ShareType 0 - 该联系方式与自己的非共享联系人关联 1 - 该联系方式与自己的共享联系人关联
    // 2 - 该联系方式与他人的共享联系人关联    3 - 该联系方式属于自己，但还未添加为联系人
    //4 - 该联系方式属于他人，且还未添加为联系人  5 - 该联系方式与他人的非共享联系人关联
    public static string getSGContactorPicUrl(string ShareType)
    {
        //ShareType: 0-自有非共享群发组 1-自有共享群发组 2-他人共享群发组
        string PicUrl = "../Images/sgc_self_nonshared.gif";
        if (ShareType == "0")
        {
            // 0 - 该联系方式与自己的非共享联系人关联 
            PicUrl = "../Images/sgc_self_nonshared.gif";
        }
        else if (ShareType == "1")
        {
            // 1 - 该联系方式与自己的共享联系人关联
            PicUrl = "../Images/sgc_self_shared.gif";
        }
        else if (ShareType == "2")
        {
            //2-该联系方式与他人的共享联系人关联
            PicUrl = "../Images/sgc_other_shared.gif";
        }
        else if (ShareType == "3")
        {
            //3-该联系方式属于自己，但还未添加为联系人
            PicUrl = "../Images/sgc_self_notContactor.gif";
        }
        else if (ShareType == "4")
        {
            //4-该联系方式属于他人，且还未添加为联系人
            PicUrl = "../Images/sgc_other_notContactor.gif";
        }
        else if (ShareType == "5")
        {
            //5 - 该联系方式与他人的非共享联系人关联
            PicUrl = "../Images/sgc_other_nonshared.gif";
        }
        return PicUrl;
    }
    #endregion

    #region 群发组用户共享图片的alt信息（SendGroup_List页面的联系人alt）

    // ShareType 0 - 该联系方式与自己的非共享联系人关联 1 - 该联系方式与自己的共享联系人关联
    // 2 - 该联系方式与他人的共享联系人关联    3 - 该联系方式属于自己，但还未添加为联系人
    //4 - 该联系方式属于他人，且还未添加为联系人  5 - 该联系方式与他人的非共享联系人关联
    public static string getSendGroupContactorAlt(string flag)
    {
        string alt = "该联系方式与自有联系人关联";
        if (flag == "0")
        {
            alt = "该联系方式与自有联系人关联";
        }
        if (flag == "1")
        {
            alt = "该联系方式与自有联系人关联";
        }
        if (flag == "2")
        {
            alt = "该联系方式与他人联系人关联";
        }
        if (flag == "3")
        {
            alt = "该联系方式属于自己，但还未添加为联系人";
        }
        if (flag == "4")
        {
            alt = "该联系方式属于他人，且还未添加为联系人";
        }
        if (flag == "5")
        {
            alt = "该联系方式与他人联系人关联";
        }
        return alt;
        
    }
    #endregion
}
