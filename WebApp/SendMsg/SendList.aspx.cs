/**
 * 文件：SendList.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：发送列表页面
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
using System.Text;

using AjaxPro;
using Com.Yuantel.MobileMsg.DAL;
using Com.Yuantel.MobileMsg.Model;
//using WebApp.Public;

public partial class SendMsg_List : BasePage
{
    protected string msg = "";
    private int IsDelete = 0;
    private static string Types = "allrecords";
    protected bool Isnull = false;
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.RegisterTypeForAjax(typeof(SmsAjax));
        try
        {
            IsDelete = int.Parse(Request.QueryString["IsDelete"]);
        }
        catch(Exception ex)
        {
            IsDelete = 0;
            LogHelper.Error(ex.ToString());
        }
        if (IsDelete == 1)
        {
            BatchToGabage.Visible = false;
            this.Literal1.Text = "短信群发－垃圾箱";
        }
        else
        {
            this.Literal1.Text = "短信群发－发件箱";
        }
        if (!IsPostBack)
        {
            SendBoxBind(Types, 1);
        }
    }

    /// <summary>
    /// 绑定发送记录
    /// </summary>
    /// <param name="mytypes"></param>
    /// <param name="isTotal"></param>
    private void SendBoxBind(string mytypes, int isTotal)
    {
        Msg queryMsg = GetPageParameter(isTotal);
        string TotalRows = "";
        DataSet ds = new DataSet();
        if (mytypes == "" || mytypes == null || mytypes.ToLower() == "allrecords")
        {
            TipDiv.Text = "";
            ds = Query.GetSendList(queryMsg, out TotalRows);
        }
        if (isTotal == 1)
        {
            this.hdTotalRows.Value = TotalRows.ToString();
        }
        else
        {
            TotalRows = this.hdTotalRows.Value;
        }
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GroupSendList.DataSource = ds.Tables[0].DefaultView;
                GroupSendList.DataBind();
                AspNetPager1.RecordCount = Convert.ToInt32(TotalRows);
                AspNetPager1.CustomInfoHTML = "记录总数：<font color=\"blue\"><b>" + AspNetPager1.RecordCount.ToString() + "</b></font>";
                AspNetPager1.CustomInfoHTML += " 总页数：<font color=\"blue\"><b>" + AspNetPager1.PageCount.ToString() + "</b></font>";
                AspNetPager1.CustomInfoHTML += " 当前页：<font color=\"red\"><b>" + AspNetPager1.CurrentPageIndex.ToString() + "</b></font>";
            }
            else
            {
                //GroupSendList.DataSource = null;
                //GroupSendList.DataBind();
                //AspNetPager1.RecordCount = 0;
                //AspNetPager1.DataBind();
                //msg = "<div align=\"center\">没有任何记录！</div>";
                //TipDiv.Text = "没有任何记录！";
                this.GroupSendList.GridLines = GridLines.None;
                this.GroupSendList.DataSource = null;
                this.GroupSendList.DataBind();
                this.AspNetPager1.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "login", "<script>NoneData()</script>");
            }
        }
        else
        {
            msg = "<div align=\"center\">程序执行超时，请刷新重新加载！</div>";
            this.Isnull = true;
        }
        ds.Dispose();
    }

    protected Msg GetPageParameter(int isTotal)
    {
        Msg queryMsg = new Msg();
        queryMsg.BeginTime = txtStartTime.Value.Trim();
        queryMsg.EndTime = txtEndTime.Value.Trim();
        queryMsg.Message = txtMessage.Text.Trim();
        queryMsg.State = BatchState.Default;
        queryMsg.PageSize = AspNetPager1.PageSize;
        queryMsg.PageIndex = AspNetPager1.CurrentPageIndex;
        queryMsg.SeqNo = int.Parse(Session["SEQNO"].ToString());
        queryMsg.IsDelete = IsDelete == 1 ? 1 : 0;
        queryMsg.IsTotal = isTotal;
        queryMsg.ClsID = int.Parse(txtSelectTime.SelectedValue);
        queryMsg.Status = -1;
        return queryMsg;
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void searchbut_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 0;
        SendBoxBind(Types, 1);
    }

    /// <summary>
    /// 页面刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void resf_Click(object sender, EventArgs e)
    {
        SendBoxBind(Types, 1);
    }

    /// <summary>
    /// 绑定分页控件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        SendBoxBind(Types, 0);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void batchdelete_Click(object sender, EventArgs e)
    {
        string IDList = "";

        foreach (GridViewRow rows in GroupSendList.Rows)
        {
            if (((CheckBox)rows.FindControl("CheckBox2")).Checked == true)
            {
                IDList += GroupSendList.DataKeys[rows.RowIndex].Value.ToString() + ",";
            }
        }
        int SeqNo = int.Parse(Session["SEQNO"].ToString());
        int Sts = 0;// Convert.ToInt32(selectArea.SelectedValue);
        int Result = Query.DeleteSendList(SeqNo,IDList,Sts,2);

        if (Result == 0)
        {
            MsgBox("没有批量删除任何记录！");
        }
        else
        {
            MsgBox("成功批量删除" + Result + "条记录！");
        }

        SendBoxBind(Types, 1);
    }

    /// <summary>
    /// 批量重发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BatchReSend_Click(object sender, EventArgs e)
    {
        string IDList = "";

        foreach (GridViewRow rows in GroupSendList.Rows)
        {
            if (((CheckBox)rows.FindControl("CheckBox2")).Checked == true)
            {
                IDList += GroupSendList.DataKeys[rows.RowIndex].Value.ToString() + ",";
            }
        }
        IDList = IDList.Substring(0,IDList.Length - 1);
        int Result = Send.Resend(IDList,0);

        if (Result == 0)
        {
            MsgBox("没有批量重发任何记录！");
        }
        else
        {
            MsgBox("成功批量重发" + IDList.Split(',').Length + "条记录！");
        }

        SendBoxBind(Types, 1);
    }

    /// <summary>
    /// 批量放入垃圾箱
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BatchToGabage_Click(object sender, EventArgs e)
    {
        string IDList = "";

        foreach (GridViewRow rows in GroupSendList.Rows)
        {
            if (((CheckBox)rows.FindControl("CheckBox2")).Checked == true)
            {
                IDList += GroupSendList.DataKeys[rows.RowIndex].Value.ToString() + ",";
            }
        }
        int SeqNo = int.Parse(Session["SEQNO"].ToString());
        int Sts = 0;// Convert.ToInt32(selectArea.SelectedValue);
        int Result = Query.DeleteSendList(SeqNo, IDList, Sts, 1);

        if (Result == 0)
        {
            MsgBox("没有批量放入垃圾箱任何记录！");
        }
        else
        {
            MsgBox("成功批量放入垃圾箱" + Result + "条记录！");
        }

        SendBoxBind(Types, 1);
    }

    #region GridView操作事件

    protected void GroupSendList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int result = 0;
        if (e.CommandName == "Resend")
        {
            result = Send.Resend(GroupSendList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text, int.Parse(((DropDownList)GroupSendList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("ResendState")).SelectedValue));

            if (result > 0)
            {
                MsgBox("重发成功！");
            }
            else
            {
                MsgBox("没有重发任何记录！");
            }
        }
        else if (e.CommandName == "Gabage")
        {
            result = Query.DeleteSendList(int.Parse(Session["SEQNO"].ToString()), GroupSendList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text, -1, 1);

            if (result > 0)
            {
                MsgBox("操作成功！");
            }
            else
            {
                MsgBox("操作失败！");
            }
        }
        else if (e.CommandName == "Delete")
        {
            result = Query.DeleteSendList(int.Parse(Session["SEQNO"].ToString()), GroupSendList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text, -1, 2);

            if (result > 0)
            {
                MsgBox("删除成功！");
            }
            else
            {
                MsgBox("删除失败！");
            }
        }

        SendBoxBind(Types, 1);
    }

    protected void GroupSendList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton link = e.Row.FindControl("lbResend") as LinkButton;
            link.CommandArgument = e.Row.RowIndex.ToString();
            LinkButton link_ = e.Row.FindControl("lbDelete") as LinkButton;
            link_.CommandArgument = e.Row.RowIndex.ToString();
            LinkButton link__ = e.Row.FindControl("lbDeleteToGabage") as LinkButton;
            link__.CommandArgument = e.Row.RowIndex.ToString();
        }
    }

    protected void GroupSendList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseout", "style.backgroundColor=''");
            e.Row.Attributes.Add("onmouseover", "style.backgroundColor='#FFFFDF'");
            if (IsDelete == 1)
            {
                GroupSendList.Columns[10].HeaderStyle.CssClass = "hidden";
                GroupSendList.Columns[10].ItemStyle.CssClass = "hidden";
                GroupSendList.Columns[10].FooterStyle.CssClass = "hidden";
            }
        }
    }

    protected void GroupSendList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion

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
