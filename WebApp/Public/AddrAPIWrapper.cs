/*
 * 作者：朱翔
 * 日期：2009-05-12
 * 描述：通讯录封装类
 * 修改：2009-05-26 by 洪弟：修改类，细分不同的调用，以适用所有业务使用
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
using System.Net;
using System.IO;
using Newtonsoft.Json;
/// <summary>
/// 通讯录接口的访问的封装
/// </summary>
public class AddrAPIWrapper
{
    string sessionID = "";
    string sessionKey = "";
    public AddrAPIWrapper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public void SetIdentityInfo(string tsessionID, string tsessionKey)
    {
        sessionID = tsessionID;
        sessionKey = tsessionKey;
    }
    public void GetIdentityInfo(out string tsessionID, out string tsessionKey)
    {
        //如果没有预先设置sessionID和sessionKey的值，则从cookie中获取
        if (sessionID.Equals("") || sessionKey.Equals(""))
        {
            Yuantel.Authentication.Web.UserAuthentication.GetSessionKey(out sessionKey, out sessionID);
            //sessionID = Guid.NewGuid().ToString();
            //sessionKey = Guid.NewGuid().ToString();
            //Yuantel.Authentication.Web.UserAuthentication.CreateSession(61183, sessionID, sessionKey, 7, "", "");
        }
        tsessionID = sessionID;
        tsessionKey = sessionKey;
    }
    //判断sessionID、sessionKey是否有效。当this.sessionID=""或者this.sessionKey=""时，则先从cookie获取值，并设置下sessionKey、sessionID，然后再判断有效性。
    public bool IsIdentityInfoValid()
    {
        bool ret = false;
        if (sessionID.Equals("") || sessionKey.Equals(""))
        {
            Yuantel.Authentication.Web.UserAuthentication.GetSessionKey(out sessionKey, out sessionID);
            //sessionID = Guid.NewGuid().ToString();
            //sessionKey = Guid.NewGuid().ToString();
            //Yuantel.Authentication.Web.UserAuthentication.CreateSession(61183, sessionID, sessionKey, 7, "", "");
        }
        //验证是否为null，如果为null，则返回用户验证未通过
        if (sessionID == null || sessionKey == null){
            sessionID = ""; sessionKey = "";
            ret = false; }
        
        //再次验证是否为空字符串，如果还是为空，则返回用户验证未通过
        if (sessionID.Equals("") || sessionKey.Equals("")){ ret = false; }
        //两个变量都有值，则返回true
        else { ret = true; }

        return ret;
    }
    /// <summary>
    /// 通讯录接口调用入口
    /// type = 1;//从所有联系人中获取联系人
    /// type = 2;//获取群发组列表
    /// type = 3;//获取群发组中详细信息，支持从多个组中返回数据
    /// </summary>
    public DataTable AddrAPIEntryPoint(int type, int start, int pageSize, int searchType, string searchContent, string serFlagList, string sendGroupIDs,string phonePropertys, out int totalRows)
    {
        if (type == 1) return GetContactorsFromAllContactors(start, pageSize, searchType, searchContent, out totalRows);
        else if (type == 2) return GetSendGroups(start, pageSize, searchType, searchContent, serFlagList, out totalRows);
        else if (type == 3) return GetSendGroupsDetail(start, pageSize, searchType, searchContent, serFlagList, sendGroupIDs,phonePropertys, out totalRows);
        else { totalRows = 0; return null; }
    }

    /// <summary>
    /// 获取群发组列表，以DataTable返回
    /// </summary>
    private DataTable GetSendGroups(int start, int pageSize, int searchType, string searchContent, string serFlagList, out int totalRows)
    {
        DataTable dt = new DataTable();
        string json = GetSGroupsAPI(start, pageSize, searchType, searchContent, serFlagList);
        dt = CreateDataTableSendGroup(json, out totalRows);
        return dt;
    }

    /// <summary>
    /// 获取群发组详细，以DataTable返回，支持返回多个群发组中的详细数据
    /// 调用时，sendGroupIDs包含多个组时，认为要返回的是组内的所有联系方式，因此，应该要确保start设置为1
    /// </summary>
    private DataTable GetSendGroupsDetail(int start, int pageSize, int searchType, string searchContent, string serFlagList, string sendGroupIDs, string phonePropertys, out int totalRows)
    {
        DataTable dt = new DataTable();
        string json = GetSGroupsDetailAPI(start, pageSize, searchType, searchContent, serFlagList, sendGroupIDs, phonePropertys);
        dt = CreateDataTableSGroupsDetail(json, out totalRows);
        return dt;
    }
    /// <summary>
    /// 获取群发组详细，以DataTable返回，支持返回多个群发组中的详细数据
    /// 调用时，sendGroupIDs包含多个组时，认为要返回的是组内的所有联系方式，因此，应该要确保start设置为1
    /// </summary>
    private DataTable GetContactorsFromAllContactors(int start, int pageSize, int searchType, string searchContent, out int totalRows)
    {
        DataTable dt = new DataTable();
        string json = GetContactorsFromAllContactorsAPI(start, pageSize, searchType, searchContent);
        dt = CreateDataTableContactorsFromAllContactors(json, out totalRows);
        return dt;
    }
    /// <summary>
    /// 从接口获取群发组列表，以Json格式的字符串返回
    /// </summary>
    private string GetSGroupsAPI(int start, int pageSize, int searchType, string searchContent, string serFlagList)
    {        
        if(!IsIdentityInfoValid())
        {
            return "{\"results\":null,\"totalRow\":0,\"code\":\"5001\",\"msg\":\"用户验证未通过\"}";
        }

        string contentType = "application/yt-addrCode-getSGroup";
        string param = "{\"sessionId\":\"" + sessionID + "\",\"sessionKey\":\"" + sessionKey + "\",\"start\":\"" + start.ToString() 
            + "\",\"pageSize\":\"" + pageSize.ToString() + "\",\"searchType\":\"" + searchType.ToString() + "\",\"searchContent\":\"" 
            + searchContent + "\",\"serFlagList\":\"" + serFlagList + "\"}";
        string url = ConfigurationManager.AppSettings["RequestUrl"];
        Encoding encoding = Encoding.GetEncoding("utf-8");
        byte[] postdata = encoding.GetBytes(param); //所有要传参数拼装
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "POST";
        myRequest.ContentType = contentType;
        myRequest.ContentLength = postdata.Length;
        Stream newStream = myRequest.GetRequestStream();
        // Send the data.
        newStream.Write(postdata, 0, postdata.Length);
        newStream.Close();
        // Get response
        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
        string content = reader.ReadToEnd();
        return content;
    }

    /// <summary>
    /// 从接口获取群发组中的联系方式，以Json格式的字符串返回，支持从多个组中获取
    /// </summary>
    private string GetSGroupsDetailAPI(int start, int pageSize, int searchType, string searchContent, string serFlagList, string sendGroupIDs,string phonePropertys)
    {
        //针对返回多个组数据的需要进行专门处理
        string content = "";//return value
        if (sendGroupIDs == "")
        {
            return "{\"results\":null,\"totalRow\":0,\"code\":\"2000\",\"msg\":\"参数错误\"}";
        }

        if (!IsIdentityInfoValid())
        {
            return "{\"results\":null,\"totalRow\":0,\"code\":\"5001\",\"msg\":\"用户验证未通过\"}";
        }

        string url = ConfigurationManager.AppSettings["RequestUrl"];
        Encoding encoding = Encoding.GetEncoding("utf-8");
        string contentType = "application/yt-addrCode-getSGroupDetail";

        string[] arrSGIDs = sendGroupIDs.Split(',');
        int count = arrSGIDs.GetLength(0);
        int totalRow = 0;
        string tempContent = "";
        foreach (string strSingleSGID in arrSGIDs)
        {
            if (strSingleSGID == "") continue;

            string param = "{\"sessionId\":\"" + sessionID + "\",\"sessionKey\":\"" + sessionKey + "\",\"start\":\"" + start.ToString()
                + "\",\"pageSize\":\"" + pageSize.ToString() + "\",\"searchType\":\"" + searchType.ToString() + "\",\"searchContent\":\""
                + searchContent + "\",\"sendGroupId\":\"" + strSingleSGID.ToString() + "\",\"propertys\":\"" + phonePropertys.ToString() + "\"}";
            byte[] postdata = encoding.GetBytes(param); //所有要传参数拼装
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentType = contentType;
            myRequest.ContentLength = postdata.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postdata, 0, postdata.Length);
            newStream.Close();
            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            content = reader.ReadToEnd();
            if( count > 1 )
            {//对于多个组的，返回成功时，需要对content进行分析处理
                JavaScriptObject obj = JavaScriptConvert.DeserializeObject(content) as JavaScriptObject;
                if (obj != null&&obj["code"].ToString() == "1000")
                {
                    int nStart = content.IndexOf('[') + 1;
                    int length = content.IndexOf(']') - nStart;                    
                    tempContent = tempContent + content.Substring( nStart, length) + ",";
                    totalRow = totalRow + Convert.ToInt32(obj["totalRow"]);
                }
            }
        }

        if( count > 1 && totalRow > 0 )//只有在有返回数据时，才有特殊处理，都没有返回数据时，content用最后一次的返回即可
        {
            content = "{\"results\":[" + tempContent.Substring(0, tempContent.Length - 1 ) + "],\"totalRow\":" + totalRow.ToString() + ",\"code\":\"1000\",\"msg\":\"接口调用成功\"}";
        }

        return content;
    }

    /// <summary>
    /// 从接口获取所有联系人中的联系人信息，以Json格式的字符串返回
    /// </summary>
    private string GetContactorsFromAllContactorsAPI(int start, int pageSize, int searchType, string searchContent)
    {
        string content = "";//return value

        if (!IsIdentityInfoValid())
        {
            return "{\"results\":null,\"totalRow\":0,\"code\":\"5001\",\"msg\":\"用户验证未通过\"}";
        }

        string url = ConfigurationManager.AppSettings["RequestUrl"];
        Encoding encoding = Encoding.GetEncoding("utf-8");
        string contentType = "application/yt-addrCode-getContactors";
        //注意拼字符串中，searchType与searchType、searchContent与searchContent的区别
        string param = "{\"sessionId\":\"" + sessionID + "\",\"sessionKey\":\"" + sessionKey + "\",\"start\":\"" + start.ToString()
            + "\",\"pageSize\":\"" + pageSize.ToString() + "\",\"searchType\":\"" + searchType.ToString() + "\",\"searchContent\":\""
            + searchContent + "\"}";//?SessionID=" + sessionID + "&SessionKey=" + sessionKey + "&start=1&pageSize=100";
        byte[] postdata = encoding.GetBytes(param); //所有要传参数拼装
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "POST";
        myRequest.ContentType = contentType;
        myRequest.ContentLength = postdata.Length;
        Stream newStream = myRequest.GetRequestStream();
        // Send the data.
        newStream.Write(postdata, 0, postdata.Length);
        newStream.Close();
        // Get response
        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
        content = reader.ReadToEnd();

        return content;
    }

    /// <summary>
    /// 从接口获取的组列表，转换为DataTable
    /// </summary>
    private DataTable CreateDataTableSendGroup(string json, out int totalRows)
    {
        DataTable dt = new DataTable();
        JavaScriptObject obj = JavaScriptConvert.DeserializeObject(json) as JavaScriptObject;
        if (obj != null && obj["code"].ToString() == "1000")
        {
            JavaScriptArray arry = obj["results"] as JavaScriptArray;
            dt.Columns.Add("GroupID", typeof(int));
            dt.Columns.Add("GroupName", typeof(string));
            dt.Columns.Add("MemberCount", typeof(int));
            dt.Columns.Add("ShareType", typeof(int));
            for (int i = 0; i < arry.Count; i++)
            {
                JavaScriptObject list = arry[i] as JavaScriptObject;
                //if (int.Parse(list["meberCount"].ToString()) > 0)
                //{
                    DataRow dr = dt.NewRow();
                    dr["GroupID"] = list["sendGroupId"];
                    dr["GroupName"] = list["groupName"].ToString();
                    dr["MemberCount"] = list["meberCount"];//注意这里返回的"meberCount"
                    dr["ShareType"] = list["shareType"];
                    dt.Rows.Add(dr);
                //}
            }
            totalRows = int.Parse(obj["totalRow"].ToString());
        }
        else
        {
            totalRows = 0;
        }
        return dt;
    }
    /// <summary>
    /// 从接口获取的组详细列表，转换为DataTable
    /// </summary>
    private DataTable CreateDataTableSGroupsDetail(string json, out int totalRows)
    {
        DataTable dt = new DataTable();
        JavaScriptObject obj = JavaScriptConvert.DeserializeObject(json) as JavaScriptObject;
        if (obj != null && obj["code"].ToString() == "1000")
        {
            JavaScriptArray arry = obj["results"] as JavaScriptArray;
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("PhoneNo", typeof(string));
            dt.Columns.Add("Company", typeof(string));
            dt.Columns.Add("Property", typeof(string));
            dt.Columns.Add("MemberID", typeof(string));
            dt.Columns.Add("ShareType", typeof(string));
            for (int i = 0; i < arry.Count; i++)
            {
                JavaScriptObject list = arry[i] as JavaScriptObject;
                DataRow dr = dt.NewRow();
                dr["Name"] = list["name"];
                dr["PhoneNo"] = list["phoneNo"];
                dr["Company"] = list["company"];
                dr["Property"] = list["property"];
                dr["MemberID"] = list["memberId"];
                dr["ShareType"] = list["shareType"];
                dt.Rows.Add(dr);
            }
            totalRows = int.Parse(obj["totalRow"].ToString());
        }
        else
        {
            totalRows = 0;
        }
        return dt;
    }

    /// <summary>
    /// 从接口获取的组详细列表，转换为DataTable
    /// </summary>
    private DataTable CreateDataTableContactorsFromAllContactors(string json, out int totalRows)
    {
        DataTable dt = new DataTable();
        JavaScriptObject obj = JavaScriptConvert.DeserializeObject(json) as JavaScriptObject;
        if (obj != null && obj["code"].ToString() == "1000")
        {
            JavaScriptArray arry = obj["results"] as JavaScriptArray;
            dt.Columns.Add("SeqNo", typeof(string));
            dt.Columns.Add("CompID", typeof(string));
            dt.Columns.Add("ContactorId", typeof(string));
            dt.Columns.Add("Company", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("JobPhone", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("JobFax", typeof(string));
            dt.Columns.Add("HomePhone", typeof(string));
            dt.Columns.Add("ShareType", typeof(string));
            for (int i = 0; i < arry.Count; i++)
            {
                JavaScriptObject list = arry[i] as JavaScriptObject;
                DataRow dr = dt.NewRow();
                dr["SeqNo"] = list["seqNo"];
                dr["CompID"] = list["compId"];
                dr["ContactorId"] = list["contactorId"];
                dr["Company"] = list["company"];
                dr["Name"] = list["name"];
                dr["JobPhone"] = list["jobPhone"];
                dr["Mobile"] = list["moblie"];
                dr["JobFax"] = list["jobFax"];
                dr["HomePhone"] = list["homePhone"];
                dr["ShareType"] = list["shareType"];
                dt.Rows.Add(dr);
            }
            totalRows = int.Parse(obj["totalRow"].ToString());
        }
        else
        {
            totalRows = 0;
        }
        return dt;
    }
}