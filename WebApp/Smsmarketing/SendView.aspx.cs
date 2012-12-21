using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxPro;
using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Com.Yuantel.DBUtility;
using Com.Yuantel.MobileMsg.Model;

public partial class Smsmarketing_SendView : BasePage
{
    int seqno;
    DataSet dsResult;
    protected void Page_Load(object sender, EventArgs e)
    {
        seqno = Convert.ToInt32(Session["SEQNO"].ToString());

        if (!this.IsPostBack)
        {
            this.PopulateData();
        }
    }

    //smsList绑定数据
    private void PopulateData()
    {
        string total = string.Empty;
        try
        {
            dsResult = YxSms.GetSendList(GetPageParameter(), out total);
            if ((null != dsResult) && (0 != dsResult.Tables.Count) && (0 != dsResult.Tables[0].Rows.Count))
            {
                this.smsList.GridLines = GridLines.Horizontal;
                this.AspNetPager1.Visible = true;
                this.smsList.DataSource = dsResult;
                this.smsList.DataBind();
                BindPager(this.AspNetPager1, Convert.ToInt32(total));
            }
            else
            {
                this.smsList.GridLines = GridLines.None;
                this.smsList.DataSource = null;
                this.smsList.DataBind();
                this.AspNetPager1.Visible = false;
            }
            SetCellValue();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    //地区绑定
    private void SetCellValue()
    {
        DataSet areaDs = null;
        try
        {
            foreach (GridViewRow rows in smsList.Rows)
            {
                string areastr = string.Empty;
                int smsid = Convert.ToInt32(dsResult.Tables[0].Rows[rows.RowIndex]["ID"].ToString());
                areaDs = YxSms.QueryArea(smsid);
                for (int i = 0; i < areaDs.Tables[0].Rows.Count; i ++ )
                {
                    string areaname = areaDs.Tables[0].Rows[i]["areaname"].ToString();
                    string industryname = areaDs.Tables[0].Rows[i]["industryname"].ToString();
                    if (areaname == "")
                    {
                        areaname = string.Empty;
                    }
                    else
                    {
                        areaname = " " + areaname;
                    }
                    if (industryname == "")
                    {
                        industryname = "全部行业";
                    }
                    areastr += areaDs.Tables[0].Rows[i]["provincename"].ToString() + areaname + " " + industryname + "<br/>";

                }
                if ( areastr.Length > 0 )
                {
                    areastr = areastr.Substring(0,areastr.Length - 5);
                    smsList.Rows[rows.RowIndex].Cells[2].Text = areastr;
                }
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected YxMsg GetPageParameter()
    {
        YxMsg yxmsg = new YxMsg();
        yxmsg.BeginTime = this.txtStartTime.Value.Trim();
        yxmsg.EndTime = this.txtEndTime.Value.Trim();
        yxmsg.PageIndex = AspNetPager1.CurrentPageIndex;
        yxmsg.PageSize = AspNetPager1.PageSize;
        yxmsg.ClsID = int.Parse(this.txtSelectTime.SelectedValue);
        yxmsg.Message = this.txtMessage.Text.Trim();
        yxmsg.SeqNo = this.seqno;
        return yxmsg;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.PopulateData();
    }

    #region 分页控件列表绑定
    public static void BindPager(Wuqi.Webdiyer.AspNetPager pager, int allCount)
    {
        pager.RecordCount = allCount;
        pager.CustomInfoHTML = "&nbsp;&nbsp;记录总数：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>";
        pager.CustomInfoHTML += "&nbsp; 总页数：<font color=\"blue\"><b>" + pager.PageCount.ToString() + "</b></font>";
        pager.CustomInfoHTML += "&nbsp; 当前页：<font color=\"red\"><b>" + pager.CurrentPageIndex.ToString() + "</b></font>";
    }
    #endregion

    //搜索
    protected void searchbut_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        this.PopulateData();
    }

    //刷新
    protected void resf_Click(object sender, EventArgs e)
    {
        this.PopulateData();
    }

    protected void smsList_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#fffdd7'");
        }
    }
}
