/**
 * 文件：CheckList.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：短信审核页面
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

using Com.Yuantel.MobileMsg.DAL;
using Com.Yuantel.MobileMsg.Model;
using AjaxPro;

public partial class SendMsg_CheckList : System.Web.UI.Page
{
    protected string msg = "";
    private static string Types = "allrecords";

    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.RegisterTypeForAjax(typeof(ManageAjax));
        if (!IsPostBack)
        {
            SendBoxBind(Types, 1);
        }
    }

    private void SendBoxBind(string mytypes, int isTotal)
    {
        Msg queryMsg = GetPageParameter(isTotal);
        string TotalRows = "";
        DataSet ds = new DataSet();
        if (mytypes == "" || mytypes == null || mytypes.ToLower() == "allrecords")
        {
            TipDiv.Text = "";
            ds = Query.GetApproveList(queryMsg, out TotalRows);
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
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = "SendTime desc";
                GroupSendList.DataSource = dv;
                GroupSendList.DataBind();
                AspNetPager1.RecordCount = Convert.ToInt32(TotalRows);
                AspNetPager1.CustomInfoHTML = "记录总数：<font color=\"blue\"><b>" + AspNetPager1.RecordCount.ToString() + "</b></font>";
                AspNetPager1.CustomInfoHTML += " 总页数：<font color=\"blue\"><b>" + AspNetPager1.PageCount.ToString() + "</b></font>";
                AspNetPager1.CustomInfoHTML += " 当前页：<font color=\"red\"><b>" + AspNetPager1.CurrentPageIndex.ToString() + "</b></font>";
            }
            else
            {
                GroupSendList.DataSource = null;
                GroupSendList.DataBind();
                AspNetPager1.RecordCount = 0;
                AspNetPager1.DataBind();
                msg = "<div align=\"center\">没有任何记录！</div>";
                TipDiv.Text = "没有任何记录！";
            }
        }
        else
        {
            msg = "<div align=\"center\">程序执行超时，请刷新重新加载！</div>";
        }
        ds.Dispose();
        selectArea.SelectedIndex = 0;
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
        //queryMsg.SeqNo = int.Parse(Session["SEQNO"].ToString());
        queryMsg.IsDelete = 0;
        queryMsg.IsTotal = isTotal;
        queryMsg.ClsID = int.Parse(txtSelectTime.SelectedValue);
        queryMsg.Status = int.Parse(ddlStatus.SelectedValue);
        queryMsg.UserName = txtUserName.Text.Trim();
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
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void resf_Click(object sender, EventArgs e)
    {
        SendBoxBind(Types, 1);
    }

    protected void GroupSendList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int result = 0;
        int state = 0;
        if (e.CommandName == "Pass")
        {
            state = 1;
        }
        else if (e.CommandName == "Refuse")
        {
            state = int.Parse(((DropDownList)GroupSendList.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("ddlReason")).SelectedValue);
        }
        result = Query.ApproveMsg(GroupSendList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text, state);

        if (result > 0)
        {
            MsgBox("操作成功！");
        }
        else
        {
            MsgBox("操作失败！");
        }
        SendBoxBind(Types, 1);
    }

    protected void GroupSendList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton link = e.Row.FindControl("lbPass") as LinkButton;
            link.CommandArgument = e.Row.RowIndex.ToString();
            LinkButton link_ = e.Row.FindControl("lbRefuse") as LinkButton;
            link_.CommandArgument = e.Row.RowIndex.ToString();
        }
    }

    protected void GroupSendList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseout", "style.backgroundColor=''");
            e.Row.Attributes.Add("onmouseover", "style.backgroundColor='#F8F7F7'");

            int sendstatus = int.Parse(((DataRowView)e.Row.DataItem)["State"].ToString());
            if (sendstatus > 0)
            {
                LinkButton lbaccept = (LinkButton)e.Row.FindControl("lbPass");
                lbaccept.Enabled = false;
                LinkButton lbrefuse = (LinkButton)e.Row.FindControl("lbRefuse");
                lbrefuse.Enabled = false;
                DropDownList ddlReason = (DropDownList)e.Row.FindControl("ddlReason");
                ddlReason.SelectedValue = sendstatus.ToString();
                ddlReason.Enabled = false;
                CheckBox cb = (CheckBox)e.Row.FindControl("CheckBox2");
                cb.Enabled = false;
            }
            else
            {
                LinkButton lbaccept = (LinkButton)e.Row.FindControl("lbPass");
                //lbaccept.Attributes.Add("onclick", "return confirm('信息内容为【" + ((DataBoundLiteralControl)e.Row.Cells[5].Controls[0]).Text.Replace("\r\n", "").Trim() + "】,确定要通过吗?')");
                lbaccept.Attributes.Add("onclick", "return confirm('信息内容为【" + e.Row.Cells[5].Text.Replace("\r\n", "") + "】,确定要通过吗?')");
                LinkButton lbrefuse = (LinkButton)e.Row.FindControl("lbRefuse");
                lbrefuse.Attributes.Add("onclick", "return confirm('信息内容为【" + e.Row.Cells[5].Text.Replace("\r\n", "") + "】,确定要拒绝吗?')");
            }
        }
    }
    protected void GroupSendList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    /// <summary>
    /// 分页控件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        SendBoxBind(Types, 0);
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

    /// <summary>
    /// 批量审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBatch_Click(object sender, EventArgs e)
    {
        string IDList = "";

        foreach (GridViewRow rows in GroupSendList.Rows)
        {
            if (((CheckBox)rows.FindControl("CheckBox2")).Checked == true)
            {
                IDList += GroupSendList.DataKeys[rows.RowIndex].Value.ToString() + ",";
            }
        }
        IDList = IDList.Substring(0, IDList.Length - 1);
        int Sts = Convert.ToInt32(selectArea.SelectedValue);
        int Result = Query.BatchApproveMsg(IDList, Sts);

        if (Result == 0)
        {
            MsgBox("没有批量审核任何记录！");
        }
        else
        {
            MsgBox("成功批量审核" + Result + "条记录！");
        }
        SendBoxBind(Types, 1);
    }
}
