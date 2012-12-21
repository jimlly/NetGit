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
using System.IO;
using System.Text;
using AjaxPro;
using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;
using System.Collections.Generic;
//using WebApp.Public;

public partial class Sms_SendSms : BasePage
{
    private enum SendTimeType
    {
        PriorToNow, // 比当前时间早
        EqualToNow, // 当前时间
        AMonthAfterNow, // 比当前时间大一个月
        WithinAMonthAfterNow // 当前时间后一个月内
    }

    private int seqNo;
    private BlackDictionary blackDic = new BlackDictionary();

    BasePage basePage = new BasePage();
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.RegisterTypeForAjax(typeof(SmsAjax));
        Utility.RegisterTypeForAjax(typeof(BlackDictionary));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxCityMethod));

        try
        {
            if (!IsPostBack)
            {
                InitDDL();
            }
            ////发送
            btnSend.Attributes.Add("onclick", "return CheckForm();");
            this.seqNo = Convert.ToInt32(Session["SEQNO"].ToString());

            hfBlackDic.Value = Public.BlackDic.ToString();
            if (Public.BlackDic == 1)
            {
                StreamReader objReader = new StreamReader(Server.MapPath("../Doc/sdk_blackdict.txt"), Encoding.GetEncoding("gb2312"));
                string sLine = "";
                ArrayList arrText = new ArrayList();

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (!string.IsNullOrEmpty(sLine))
                        blackDic.AddWord(sLine);
                }
                objReader.Close();
            }

            this.ddlSignatrue.Items.Insert(0, new ListItem("===设置短信签名===", "0"));
            DataSet ds = SignatureDal.Select(int.Parse(Session["SEQNO"].ToString()));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.ddlSignatrue.Items.Add(new ListItem(ds.Tables[0].Rows[i]["SignContent"].ToString(), ds.Tables[0].Rows[i]["SignContent"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.ToString());
        }
    }
    protected void InitDDL()
    {
        DataSet province = AjaxCityMethod.NewGetPovinceList();
        DataSet hangye = AjaxCityMethod.GetIndustryList(1);
        for (int i = 1; i <= 5; i++)
        {
            DropDownList DDL_Provice = (DropDownList)Page.FindControl("DDL_Provice" + i.ToString());
            if (province != null && province.Tables.Count > 0 && province.Tables[0].Rows.Count > 0)
            {
                DDL_Provice.DataSource = province;
                DDL_Provice.DataTextField = "AreaName";
                DDL_Provice.DataValueField = "AreaID";
                DDL_Provice.DataBind();
            }
            DDL_Provice.Items.Insert(0, new ListItem("请选择省份", "-1"));
            DDL_Provice.Attributes.Add("onchange", "cityResult(" + DDL_Provice.ClientID + "," + i.ToString() + ");");//DDL_Provice.ClientID 
            DropDownList ddl_hangye = (DropDownList)Page.FindControl("ddl_hangye" + i.ToString());
            if (hangye != null && hangye.Tables.Count > 0 && hangye.Tables[0].Rows.Count > 0)
            {
                ddl_hangye.DataSource = hangye;
                ddl_hangye.DataTextField = "MainName";
                ddl_hangye.DataValueField = "MainIndustryID";
                ddl_hangye.DataBind();
            }
            ddl_hangye.Items.Insert(0, new ListItem("全部行业", "-1"));
            ddl_hangye.Attributes.Add("onchange", "GethangyeNumCount(" + ddl_hangye.ClientID + "," + i.ToString() + ");");

            DropDownList DDL_City = (DropDownList)Page.FindControl("DDL_City" + i.ToString());
            DDL_City.Items.Add(new ListItem("全部城市", "-1"));
            DDL_City.Attributes.Add("onchange", "GetCityNumCount(" + DDL_City.ClientID + "," + i.ToString() + ");");
        }

        DataSet industryList = AjaxCityMethod.GetIndustryList(2);
        if (industryList != null && industryList.Tables.Count > 0 && industryList.Tables[0].Rows.Count > 0)
        {
            for (int i = 1; i <= 5; i++)
            {
                DropDownList ddlIndustry = (DropDownList)Page.FindControl("ddlIndustry" + i.ToString());
                if (ddlIndustry != null)
                {
                    ddlIndustry.DataSource = industryList;
                    ddlIndustry.DataTextField = "MainName";
                    ddlIndustry.DataValueField = "MainIndustryID";
                    ddlIndustry.DataBind();
                }
                ddlIndustry.Items.Insert(0, new ListItem("请选择行业", "-1"));
                ddlIndustry.Attributes.Add("onchange", "GetProvinceByIndustry(" + ddlIndustry.ClientID + "," + i.ToString() + ");");
                DropDownList ddlProvince = (DropDownList)Page.FindControl("ddlProvince" + i.ToString());
                ddlProvince.Items.Insert(0, new ListItem("全部省份", "-1"));
                ddlProvince.Attributes.Add("onchange", "GetAreaByProvinceAndIndustry(" + ddlIndustry.ClientID + "," + ddlProvince.ClientID + "," + i.ToString() + ");");
                DropDownList ddlArea = (DropDownList)Page.FindControl("ddlArea" + i.ToString());
                ddlArea.Items.Insert(0, new ListItem("全部地区", "-1"));
                ddlArea.Attributes.Add("onchange", "GetNumberAmountByIPA(" + ddlIndustry.ClientID + "," + ddlProvince.ClientID + "," + ddlArea.ClientID + "," + i.ToString() + ");");
            }
        }
    }

    private void Refresh()
    {
        InitDDL();
        this.txtEnd.Text = "";
        this.hd1.Value = "";
        this.hd2.Value = "";
        this.hd3.Value = "";
        this.hd4.Value = "";
        this.hd5.Value = "";
        this.Hidmg.Value = "";
    }

    //发送按钮的单击事件
    protected void bt_Send_Click(object sender, EventArgs e)
    {
        this.btnSend.Enabled = false;
        int start = int.Parse(Request.Form["txtStart"].ToString().Trim());
        int end = int.Parse(Request.Form["txtEnd"].ToString().Trim());
        int num = end - start + 1;
        string area = this.Hidmg.Value;//Request.Form["Hidmg"].ToString();

        List<string> list = Send.SplitMessage(this.txtContent.Value.ToString(),0);
        int count = list.Count * num;
        if (!BalanceIsFull(count))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发送失败,您的余额不足,请充值！');</script>");
            this.btnSend.Enabled = true;
            Refresh();
            return;
        }

        DateTime sendTime;
        if (string.IsNullOrEmpty(this.txtSendTime.Value))
        {
            sendTime = DateTime.Now;
        }
        else
        {
            sendTime = DateTime.Parse(this.txtSendTime.Value);
            switch (CheckTime(sendTime))
            {
                case SendTimeType.EqualToNow:
                    sendTime = DateTime.Now;
                    break;
                case SendTimeType.AMonthAfterNow:
                case SendTimeType.PriorToNow:
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('发送时间不能早于当前时间，不能晚于一个月');</script>");
                    this.btnSend.Enabled = true;
                    return;
                case SendTimeType.WithinAMonthAfterNow:
                    break;
            }
        }

        TimeSpan TimeSend = sendTime.TimeOfDay;
        TimeSpan TimeBegin = TimeSpan.Parse(Public.BeginTime);
        TimeSpan TimeEnd = TimeSpan.Parse(Public.EndTime);
        if (!(TimeSend >= TimeBegin && TimeSend < TimeEnd))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "time", string.Format("<script>alert('可以正常发送的时间段为{0}—{1}');</script>", TimeBegin.ToString(), TimeEnd.ToString()));
            this.btnSend.Enabled = true;
            return;
        }

        YxMsg msg = new YxMsg();
        msg.SeqNo = this.seqNo;
        msg.SendTime = sendTime;
        msg.Message = this.txtContent.Value;
        if (this.ddlSignatrue.SelectedItem.Value != "0")
        {
            msg.Message += "【" + this.ddlSignatrue.SelectedItem.Value + "】";
        }
        else
        {
            msg.Message = msg.Message.Replace('[', '【');
            msg.Message = msg.Message.Replace(']', '】');
        }
        msg.Message = msg.Message.Replace('#', '＃');
        msg.Message = msg.Message.Replace("'", "''");
        msg.AreaIndustry = area;
        msg.StartID = start;
        msg.EndID = end;
        msg.SendCounts = num;
        YxSms.Send(msg);
        this.btnSend.Enabled = true;
        Response.Redirect("Sendok.htm", false);
    }

    //预判断余额够不够
    private bool BalanceIsFull(int count)
    {
        long totalBalance = 0;  //总实用金额
        long totalDonatedBalance = 0; //总赠送金额         
        Query.GetUserAccountList(this.seqNo, 71, 1, -1, out totalBalance, out totalDonatedBalance);
        double balance = (Double)Global.ChangeRateFromDb((totalBalance + totalDonatedBalance));

        double expectedSum = count * Public.UnitPrice;
        if (balance < expectedSum)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //检查发送时间
    private SendTimeType CheckTime(DateTime sendTime)
    {
        SendTimeType result;
        DateTime dtNow = DateTime.Now;
        if (sendTime.CompareTo(dtNow) < 0)
        {
            result = SendTimeType.PriorToNow;
        }
        else
        {
            if (sendTime.CompareTo(dtNow) == 0)
            {
                result = SendTimeType.EqualToNow;
            }
            else
            {
                TimeSpan temp = sendTime - dtNow;
                if (temp.Days > 30)
                {
                    result = SendTimeType.AMonthAfterNow;
                }
                else
                {
                    result = SendTimeType.WithinAMonthAfterNow;
                }
            }
        }
        return result;
    }

    //号码分布图.
    protected void NumAreaLinkButton_Click(object sender, EventArgs e)
    {
        DataSet ds = YxSms.AreaIndustryNum();

        DataSet historyds = YxSms.GetNumbersUpdateLog();
        ExcelToExport excelToExport = new ExcelToExport();
        excelToExport.ExportToExcelByHtml(Page, ds, historyds);
    }
}
