using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions; 
using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;
public class AjaxCityMethod
{
     
    #region GetPovinceList
    public static DataSet GetPovinceList()
    {
        return YxSms.NewGetPovinceList();
    }
    public static DataSet GetHangyeList()
    {
        return YxSms.NewGetHangyeList();
    }
    #endregion

    #region GetAreaList
    [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
    public DataSet GetAreaList(int cityid)
    {
        return YxSms.GetAreaList(cityid);
    }
    #endregion

    #region NewMethod
    public static DataSet NewGetPovinceList()
    {
        return YxSms.NewGetPovinceList();
    }
    public static DataSet NewGetHangyeList()
    {
        return YxSms.NewGetHangyeList();
    }

    [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
    public DataSet NewGetCityList(int povinceid)
    {
        return YxSms.NewGetCityList(povinceid);
    }
    //省 城市和行业合并号码数量
    [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
    public string NewGetNumAmout(string areahy)
    {
        ////areahy="0|0|0" 表是省id|城市id|行业id
        return YxSms.NewGetNumAmout(areahy);
    }

    public static DataSet GetIndustryList(int type)
    {
        return YxSms.GetIndustryList(type);
    }

    [AjaxPro.AjaxMethod]
    public static DataSet GetProvinceListByIndustry(int industryID)
    {
        return YxSms.GetProvinceListByIndustry(industryID);
    }

    [AjaxPro.AjaxMethod]
    public static DataSet GetAreaListByProvinceAndIndustry(int industryID, int provinceID)
    {
        return YxSms.GetAreaListByProvinceAndIndustry(industryID, provinceID);
    }

    [AjaxPro.AjaxMethod]
    public static string GetNumberAmount(string industryID, string provinceID, string areaID)
    {
        return YxSms.GetNumberAmount(int.Parse(industryID), int.Parse(provinceID), int.Parse(areaID));
    }

    #endregion
}