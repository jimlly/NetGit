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

using AjaxPro;
using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;

public partial class Settings_Signature : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.RegisterTypeForAjax(typeof(Settings_Signature));
        if (!IsPostBack)
        {
            try
            {
                BindList();
                this.btnOK.Attributes.Add("onclick", "return CheckInput();");
            }
            catch
            { }
        }
    }

    private void BindList()
    {
        DataSet ds = GetSignatrueList();
        gvSignatrue.DataSource = ds.Tables[0].DefaultView;
        gvSignatrue.DataBind();
        for (int i = 0; i < gvSignatrue.Rows.Count; i++)
        {
            gvSignatrue.Rows[i].Cells[0].Text = SetCells(i);
        }
    }

    private string SetCells(int index)
    {
        int[] arr1 = new int[] { 0,1,2,3,4,5,6,7,8,9};
        string[] arr2 = new string[] { "一","二","三","四","五","六","七","八","九","十"};
        return "签名" + arr2[arr1[index]];
    }

    [AjaxMethod]
    public int AddSignatrue(string title, string content)
    {
        Signature sign = new Signature();
        sign.SeqNo = int.Parse(Session["SeqNo"].ToString());
        sign.Title = "";
        sign.Content = content;
        return SignatureDal.Insert(sign);
    }

    [AjaxMethod]
    public DataSet GetSignatrueList()
    {
        return SignatureDal.Select(int.Parse(Session["SeqNo"].ToString()));
    }

    //protected void gvSignatrue_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvSignatrue.EditIndex = e.NewEditIndex;
    //    this.txtTitle.Text = gvSignatrue.Rows[gvSignatrue.EditIndex].Cells[1].Text;
    //    this.txtContent.Text = gvSignatrue.Rows[gvSignatrue.EditIndex].Cells[2].Text;
    //}

    protected void gvSignatrue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Signature sign = new Signature();
            sign.ID = Convert.ToInt32(e.CommandArgument.ToString());
            if (SignatureDal.Delete(sign) == 1)
            {
                RegisterClientScriptBlock("", "<script>alert('删除成功！')</script>");
                BindList();
            }
            else
            {
                RegisterClientScriptBlock("", "<script>alert('删除失败！')</script>");
            }
            if (this.hfID.Value.Length > 0)
            {
                //this.txtTitle.Text = "";
                this.txtContent.Text = "";
            }
        }
        else if (e.CommandName == "Eidt")
        {

        }
        //this.txtTitle.ReadOnly = false;
        this.hfID.Value = "";
    }

    protected void gvSignatrue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvSignatrue_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //this.txtTitle.Text = gvSignatrue.Rows[e.NewEditIndex].Cells[1].Text;
        //this.txtTitle.ReadOnly = true;
        this.txtContent.Text = gvSignatrue.Rows[e.NewEditIndex].Cells[5].Text;
        this.hfID.Value = gvSignatrue.Rows[e.NewEditIndex].Cells[4].Text;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        int result = 0;
        Signature sign = new Signature();
        sign.SeqNo = int.Parse(Session["SeqNo"].ToString());
        sign.Title = "";
        sign.Content = Request["txtContent"].ToString().Trim();
        try
        {
            sign.ID = int.Parse(this.hfID.Value);
        }
        catch
        {
            sign.ID = 0;
        }

        if (sign.ID == 0)
        {
            if (gvSignatrue.Rows.Count >= 10)
            {
                RegisterClientScriptBlock("", "<script>alert('已达到短信签名条数上限！')</script>");
                return;
            }
            result = SignatureDal.Insert(sign);
        }
        else
        {
            result = SignatureDal.Update(sign);
        }

        if (result == 1)
        {
            RegisterClientScriptBlock("", "<script>alert('操作成功！')</script>");
            BindList();
            this.txtContent.Text = "";
            //this.txtTitle.Text = "";
        }
        else
        {
            RegisterClientScriptBlock("", "<script>alert('操作失败！')</script>");
        }
        //this.txtTitle.ReadOnly = false;
        this.hfID.Value = "";
    }

    protected void gvSignatrue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes.Add("onmouseout", "style.backgroundColor=''");
            //e.Row.Attributes.Add("onmouseover", "style.backgroundColor='#FFFFDF'");
        }
    }
}
