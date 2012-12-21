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

public partial class Smsmarketing_AreaIndustryNumWUC : System.Web.UI.UserControl
{
    //smsList绑定数据
    public void PopulateData(DataSet dsResult)
    {
        try
        {
            if ((null != dsResult) && (0 != dsResult.Tables.Count) && (0 != dsResult.Tables[0].Rows.Count))
            {
                this.NumList.GridLines = GridLines.Both;
                this.NumList.DataSource = dsResult;
                this.NumList.DataBind();
            }
            else
            {
                this.NumList.GridLines = GridLines.Horizontal;
                this.NumList.DataSource = null;
                this.NumList.DataBind();
            }
            NumList.Attributes.Add("style", "table-layout:fixed");
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    //重新生成表头
    protected void NumList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //判断创建的行是不是标题行
            if (e.Row.RowType == DataControlRowType.Header)
            {
                TableCellCollection tcl = e.Row.Cells;
                //清除自动生成的表头
                tcl.Clear();

                //添加新的表头
                tcl.Add(new TableHeaderCell());
                //tcl[0].ColumnSpan = 2;
                tcl[0].Text = "行业\\地区";
            }
        }
        catch (Exception ex)
        { }
    }

    protected void NumList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#fffdd7'");
        }
    }
}
