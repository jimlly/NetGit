/**
 * 文件：SendSms.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：提交短信页面
*/

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
using System.Collections.Generic;
using System.Text;

using System.IO;

using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;
using AjaxPro;
//using WebApp.Public;

public partial class SendMsg_SendSms : BasePage
{
    private Msg msg = new Msg();
    private Receiver receiver = new Receiver();
    private BlackDictionary blackDic = new BlackDictionary();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Utility.RegisterTypeForAjax(typeof(SmsAjax));
        Utility.RegisterTypeForAjax(typeof(BlackDictionary));
        //this.txtContent.Attributes.Add("onpropertychange", "textCounter();");
        if (!IsPostBack)
        {
            this.InitPage();
            this.btnSend.Attributes.Add("onclick", "return CheckInput();");
        }
        hfBlackDic.Value = Public.BlackDic.ToString();
        try
        {
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
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.ToString());
        }
    }

    /// <summary>
    /// 初始化页面
    /// </summary>
    private void InitPage()
    {
        try
        {
            if (Request.QueryString["Messge"] != null)
            {
                string str = Request.QueryString["Messge"].ToString();
                string text = Server.UrlDecode(str);
                if (text.Length > 0)
                {
                    this.txtContent.Value = text;
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>textCounter();</script>");
                }
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

    /// <summary>
    /// 发送按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        this.btnSend.Enabled = false;
        DateTime sendTime;
        if (string.IsNullOrEmpty(this.txtSendTime.Value))
        {
            sendTime = DateTime.Now;
        }
        else
        {
            sendTime = DateTime.Parse(this.txtSendTime.Value);
            if (sendTime < DateTime.Now || sendTime > DateTime.Now.AddMonths(1))
            {
                MsgBox("发送时间不能早于当前时间，不能晚于一个月");
                this.btnSend.Enabled = true;
                return;
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

        //发送短信
        SendMsg();
    }

    /// <summary>
    /// 构造短信信息并发送
    /// </summary>
    private void SendMsg()
    {
        try
        {
            //短信信息
            msg = new Msg();
            msg.SeqNo = int.Parse(Session["SEQNO"].ToString());
            msg.Message = this.txtContent.Value;
            if (this.ddlSignatrue.SelectedIndex > 0)
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
            msg.Priority = MsgPriority.Normal;
            msg.Sender = Session["USERNAME"].ToString();
            msg.SendTime = !string.IsNullOrEmpty(this.txtSendTime.Value) ? DateTime.Parse(this.txtSendTime.Value) : DateTime.Now;
            msg.SubmitTime = DateTime.Now;
            if (!Base.IsAcceptSend(msg.SendTime))
            {
                MsgBox("该时段不能发送短信！");
                this.btnSend.Enabled = true;
                return;
            }
            msg.Receivers = GetReceivers();
            if (msg.Receivers == null || msg.Receivers.Count == 0)
            {
                MsgBox("手机号码不合法, 无法发送！");
                this.btnSend.Enabled = true;
                return;
            }
            else
            {
                if (msg.Receivers.Count > Public.PerMaxCount)
                {
                    MsgBox(string.Format("您提交了{0}个有效号码，已超过群发上限值{1}", msg.Receivers.Count.ToString(), Public.PerMaxCount.ToString()));
                    this.btnSend.Enabled = true;
                    return;
                }
                else
                {
                    long totalBalance = 0;  //总实用金额
                    long totalDonatedBalance = 0; //总赠送金额
                    Query.GetUserAccountList(msg.SeqNo, 71, 1, -1, out totalBalance, out totalDonatedBalance);
                    double balance = (Double)Global.ChangeRateFromDb((totalBalance + totalDonatedBalance));
                    List<string> list = Send.SplitMessage(msg.Message,0);
                    if (msg.Receivers.Count * Public.UnitPrice * list.Count > balance)
                    {
                        MsgBox("余额不足！");
                        this.btnSend.Enabled = true;
                        return;
                    }
                }
            }
            //发送短信
            Send.SendMsg(msg);
            Response.Redirect("Sendok.htm", false);
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.ToString());
        }
        finally
        {
            this.btnSend.Enabled = true;
        }
    }

    /// <summary>
    /// 获取联系人
    /// </summary>
    /// <returns></returns>
    private IList<Receiver> GetReceivers()
    {
        IList<Receiver> receivers = new List<Receiver>();
        string mobile = this.txtMobileNums.Text.Trim().Replace("\r\n", "");
        receivers = GetReceiversByPerson(mobile);
        return receivers;
    }

    /// <summary>
    /// 获取号码
    /// </summary>
    /// <param name="persons"></param>
    /// <returns></returns>
    private IList<Receiver> GetReceiversByPerson(string persons)
    {
        IList<Receiver> receivers = new List<Receiver>();

        string[] person = persons.Split(';');
        for (int i = 0; i < person.Length; i++)
        {
            receiver = new Receiver();
            string[] detail = person[i].Split(',');
            if (ValidateCls.IsMobile(detail[0]))
            {
                receiver.Mobile = detail[0];
                receiver.Name = detail.Length == 2 ? detail[1] : "";
                receivers.Add(receiver);
            }
        }
        return receivers;
    }

    /// <summary>
    /// 封装消息框
    /// </summary>
    /// <param name="strMsg"></param>
    private void MsgBox(string strMsg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"<Script language='JavaScript'>");
        sb.Append(@"alert('" + strMsg + "');");
        sb.Append(@"</Script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "", sb.ToString());
        return;
    }
}
