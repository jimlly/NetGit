/**
 * 文件：Manage.aspx.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：后台管理页面
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
using System.IO;
using System.Xml;

using Com.Yuantel.MobileMsg.DAL;

public partial class Manage_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = int.Parse(Menu1.SelectedValue);
        if (Menu1.SelectedValue == "0")
        {
            int allCount = 0;
            int successCount = 0;
            int failedCount = 0;
            int sendingCount = 0;
            DataSet ds = Manage.GetSendStatus(out successCount, out failedCount, out sendingCount);
            gvStatus.DataSource = ds.Tables[0].DefaultView;
            gvStatus.DataBind();
            allCount = successCount + failedCount + sendingCount;
            this.Literal1.Text = string.Format("今日发送总条数:{0}，成功数量:{1}，失败数量:{2}，待发数量:{3}", allCount, successCount, failedCount, sendingCount);
            this.btnSubmit.Visible = false;
        }
        else if (Menu1.SelectedValue == "1")
        {
            ddlBeginTime.Items.Clear();
            ddlEndTime.Items.Clear();
            for (int i = 0; i <= 24; i++)
            {
                if (i == 0)
                {
                    ddlBeginTime.Items.Add(new ListItem(i.ToString() + ":00"));
                }
                else if (i == 24)
                {
                    ddlEndTime.Items.Add(new ListItem(i.ToString() + ":00"));
                }
                else
                {
                    ddlBeginTime.Items.Add(new ListItem(i.ToString() + ":00"));
                    ddlEndTime.Items.Add(new ListItem(i.ToString() + ":00"));
                }
            }
            DataSet ds = AnalysisXml(Manage.GetConfig("OpenTime"));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBeginTime.Text = ds.Tables[0].Rows[0][0].ToString();
                ddlEndTime.Text = ds.Tables[0].Rows[0][1].ToString();
            }
            this.btnSubmit.Visible = true;
        }
        else if (Menu1.SelectedValue == "2")
        {
            DataSet ds = AnalysisXml(Manage.GetConfig("MaxCount"));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                this.txtCount.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            this.btnSubmit.Visible = true;
        }
        else if (Menu1.SelectedValue == "3")
        {
            GridViewBind();
            this.btnSubmit.Visible = false;
        }
    }

    private void GridViewBind()
    {
        DataSet ds = AnalysisXml(Manage.GetConfig("KeyWords"));
        if (ds == null || ds.Tables.Count == 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("KeyWord", typeof(string)));
            ds.Tables.Add(dt);
        }
        ds.Tables[0].Columns[0].ColumnName = "KeyWord";
        gvKeyWords.DataSource = ds.Tables[0].DefaultView;
        gvKeyWords.DataBind();
    }

    /// <summary>
    /// 解析XML
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    private DataSet AnalysisXml(string xml)
    {
        DataSet ds = new DataSet();
        try
        {
            ds.ReadXml(new StringReader(xml));
        }
        catch
        {
            ds = null;
        }
        return ds;
    }

    /// <summary>
    /// 构造XML
    /// </summary>
    /// <returns></returns>
    private string ConstructXml()
    {
        string xml = "<root></root>";
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        try
        {
            XmlNode root = doc.GetElementsByTagName("root")[0];
            XmlElement element = null;

            if (Menu1.SelectedValue == "1")
            {
                if (int.Parse(ddlBeginTime.Text.Substring(0, ddlBeginTime.Text.IndexOf(':'))) >= int.Parse(ddlEndTime.Text.Substring(0, ddlEndTime.Text.IndexOf(':'))))
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language = 'JavaScript'>alert('结束时间必须大于开始时间');</script>");
                    return null;
                }
                element = doc.CreateElement("BeginTime");
                element.InnerText = ddlBeginTime.Text;
                root.AppendChild(element);
                element = doc.CreateElement("EndTime");
                element.InnerText = ddlEndTime.Text;
                root.AppendChild(element);
                xml = doc.OuterXml;
            }
            else if (Menu1.SelectedValue == "2")
            {
                element = doc.CreateElement("Count");
                element.InnerText = int.Parse(this.txtCount.Text.Trim()).ToString();
                root.AppendChild(element);
                xml = doc.OuterXml;
            }
            else if (Menu1.SelectedValue == "3")
            {
                for (int i = 0; i < gvKeyWords.Rows.Count; i++)
                {
                    element = doc.CreateElement("KeyWord");
                    element.InnerText = gvKeyWords.Rows[i].Cells[0].Text;
                    root.AppendChild(element);
                }
                xml = doc.OuterXml;
            }
        }
        catch
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language = 'JavaScript'>alert('参数值格式不正确, 请重新填写');</script>");
        }
        return xml;
    }

    /// <summary>
    /// 提交按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int result = 0;
        result = Manage.SetConfig(Menu1.SelectedItem.Target, ConstructXml());
        if (result == 1)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language = 'JavaScript'>alert('操作成功');</script>");
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language = 'JavaScript'>alert('操作失败');</script>");
        }
    }

    protected void gvKeyWords_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvKeyWords.EditIndex = e.NewEditIndex;
        GridViewBind();
    }
    protected void gvKeyWords_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvKeyWords.EditIndex = -1;
        GridViewBind();
    }

    protected void gvKeyWords_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string xml = "<root></root>";
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        XmlNode root = doc.GetElementsByTagName("root")[0];
        XmlElement element = null;

        for (int i = 0; i < gvKeyWords.Rows.Count; i++)
        {
            element = doc.CreateElement("KeyWord");
            if (gvKeyWords.Rows[i].RowIndex != e.RowIndex)
            {
                element.InnerText = ((DataBoundLiteralControl)gvKeyWords.Rows[i].Cells[0].Controls[0]).Text.Trim();
            }
            else
            {
                element.InnerText = ((TextBox)gvKeyWords.Rows[i].FindControl("txtKeyWord")).Text;
            }
            root.AppendChild(element);
        }
        xml = doc.OuterXml;
        Manage.SetConfig("KeyWords", xml);
        gvKeyWords.EditIndex = -1;
        GridViewBind();
    }

    protected void gvKeyWords_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string xml = "<root></root>";
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        XmlNode root = doc.GetElementsByTagName("root")[0];
        XmlElement element = null;

        for (int i = 0; i < gvKeyWords.Rows.Count; i++)
        {
            if (gvKeyWords.Rows[i].RowIndex != e.RowIndex)
            {
                element = doc.CreateElement("KeyWord");
                element.InnerText = ((DataBoundLiteralControl)gvKeyWords.Rows[i].Cells[0].Controls[0]).Text.Trim();
                root.AppendChild(element);
            }
        }
        xml = doc.OuterXml;
        Manage.SetConfig("KeyWords", xml);

        gvKeyWords.EditIndex = -1;
        GridViewBind();
    }

    /// <summary>
    /// 添加关键词
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string xml = "<root></root>";
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        XmlNode root = doc.GetElementsByTagName("root")[0];
        XmlElement element = null;

        for (int i = 0; i < gvKeyWords.Rows.Count; i++)
        {
            element = doc.CreateElement("KeyWord");
            element.InnerText = ((DataBoundLiteralControl)gvKeyWords.Rows[i].Cells[0].Controls[0]).Text.Trim();
            root.AppendChild(element);
        }
        element = doc.CreateElement("KeyWord");
        element.InnerText = this.txtAdd.Text.Trim();
        root.AppendChild(element);
        xml = doc.OuterXml;

        int result = Manage.SetConfig("KeyWords", xml);
        if (result == 1)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language = 'JavaScript'>alert('操作成功');</script>");
            this.txtAdd.Text = "";
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language = 'JavaScript'>alert('操作失败');</script>");
        }

        GridViewBind();
    }
}
