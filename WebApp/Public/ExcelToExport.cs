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
using System.IO;
//using WebApp.Public;

/// <summary>
/// ExcelToExport 的摘要说明
/// </summary>
public class ExcelToExport
{
//    #region 自定义HTML导出Excel
//    /// <summary>
//    /// 自定义HTML导出Excel(支持大数据量)
//    /// </summary>
//    /// <param name="ds">填充的数据集</param>
//    /// <param name="filename">文件名</param>
//    /// <param name="page">当前页</param>
//    /// <param name="DoType">1：接收；2：发送；3：通讯录</param>
//    public void ExportToExcelByHtml(System.Web.UI.Page page, DataSet ds, DataSet historyds)
//    {
//        try
//        {
//            DataTable dt;
//            string FileName = "SmsMarketNumberMap";
//            int dsColCount = ds.Tables[0].Columns.Count;
//            int historydsColCount = historyds.Tables[0].Columns.Count;
//            StringBuilder sb = new StringBuilder();
//            foreach (System.Data.DataTable tb in ds.Tables)
//            {
//                sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
//                sb.AppendLine(@"<style> .Text { mso-number-format:\@ }                              
//                                  .csstitle{font-size:20.0pt;font-weight:700;font-family:黑体;text-align:center;border-bottom:0px;border-right:0px;border-left:0px;}
//                                  .css1{border-top:0px;border-bottom:0px;border-right:0px;border-left:0px;}
//                                  .css2{border-top:1.5pt solid windowtext; background-color:#AFAFAF;font-weight:bold;}
//                                </script> ");
//                sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"0\" rules=\"all\" border=\"1\">");
//                //写标题
//                sb.AppendLine("<tr><td class='csstitle' colspan=" + dsColCount.ToString() + ">短信营销号码分布图<span style='mso-spacerun:yes'>&nbsp; </span></td></tr>");
//                //写历史表
//                dt = historyds.Tables[0];
//                int tempCol = dsColCount - 1;
//                foreach (DataRow dr in dt.Rows)
//                {
//                    string colDateTemp = DateTime.Today.ToShortDateString();
//                    sb.Append("<tr>");
//                    foreach (DataColumn col in dt.Columns)
//                    {
//                        if (col.ColumnName == "IndustryName")
//                        {
//                            sb.Append("<td class='css1'>" + dr[col.ColumnName].ToString() + "</td>");
//                        }
//                        if (col.ColumnName == "RecordCount")
//                        {
//                            sb.Append("<td class='css1' colspan=" + tempCol.ToString() + ">新增" + dr[col.ColumnName].ToString() + "个号码</td>");
//                        }
//                        if (col.ColumnName == "CrDate")
//                        {
//                            colDateTemp = dr[col.ColumnName].ToString();
//                        }
//                    }
//                    sb.AppendLine("</tr>");
//                    //FileName += colDateTemp;
//                }

//                sb.AppendLine("<tr><td class='css1' colspan=" + dsColCount.ToString() + " style='mso-ignore:colspan'></td></tr>");

//                //写数据表
//                dt = ds.Tables[0];

//                sb.AppendLine("<tr>");
//                foreach (DataColumn col in dt.Columns)
//                {
//                    if (col.ColumnName.ToString().ToUpper().Equals("INDUSTRYNAME"))
//                    {
//                        sb.AppendLine("<td class='css2'>行业\\地区</td>");
//                        continue;
//                    }
//                    sb.AppendLine("<td class=css2>" + col.ColumnName.ToString() + "</td>");
//                }
//                sb.AppendLine("</tr>");

//                foreach (DataRow dr in dt.Rows)
//                {
//                    sb.AppendLine("<tr>");
//                    foreach (DataColumn col in dt.Columns)
//                    {
//                        sb.AppendLine("<td>" + dr[col.ColumnName].ToString() + "</td>");
//                    }
//                    sb.AppendLine("</tr>");
//                }

//                sb.AppendLine("</table>");
//            }
//            page.EnableViewState = false;
//            page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
//            page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.GetEncoding("GB2312")) + ".xls");
//            page.Response.Write(sb.ToString());
//        }
//        catch (Exception ex)
//        {
//            LogHelper.Error("导出Excel数据失败：" + ex.ToString());
//        }
//        finally
//        {
//            GC.Collect();
//            HttpContext.Current.ApplicationInstance.CompleteRequest();
//        }
//    }
//    #endregion

    /// <summary>
    /// 自定义HTML导出Excel(支持大数据量)
    /// </summary>
    /// <param name="ds">填充的数据集</param>
    /// <param name="filename">文件名</param>
    /// <param name="page">当前页</param>
    /// <param name="DoType">1：接收；2：发送；3：通讯录</param>
    public void ExportToExcelByHtml(System.Web.UI.Page page, DataSet ds, DataSet historyds)
    {
        try
        {
            DataTable dt;
            string FileName = "SmsMarketNumberMap";
            int dsColCount = ds.Tables[0].Columns.Count;
            int historydsColCount = historyds.Tables[0].Columns.Count;
            StringBuilder sb = new StringBuilder();
            foreach (System.Data.DataTable tb in ds.Tables)
            {
                sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
                sb.AppendLine(@"<style> .Text { mso-number-format:\@ }                              
                                  .csstitle{font-size:20.0pt;font-weight:700;font-family:黑体;text-align:center;border-bottom:0px;border-right:0px;border-left:0px;}
                                  .css1{border-top:0px;border-bottom:0px;border-right:0px;border-left:0px;}
                                  .css2{border-top:1.5pt solid windowtext; background-color:#AFAFAF;font-weight:bold;}
                                </script> ");
                sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"0\" rules=\"all\" border=\"1\">");
                //写标题
                sb.AppendLine("<tr><td class='csstitle' colspan=" + dsColCount.ToString() + ">短信营销号码分布图<span style='mso-spacerun:yes'>&nbsp; </span>" + (historydsColCount > 0 ? "（" + historyds.Tables[0].Rows[0][2].ToString() + "）" : "") + "</td></tr>");
                //写历史表
                dt = historyds.Tables[0];
                sb.AppendLine("<tr>");
                int tempCol = dsColCount - 1;
                foreach (DataRow dr in dt.Rows)
                {
                    string colDateTemp = DateTime.Today.ToShortDateString();
                    sb.Append("");
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "IndustryName")
                        {
                            sb.Append("<td class='css1'>" + dr[col.ColumnName].ToString() + "</td>");
                        }
                        if (col.ColumnName == "RecordCount")
                        {
                            sb.Append("<td class='css1' colspan=" + tempCol.ToString() + ">新增" + dr[col.ColumnName].ToString() + "个号码</td>");
                        }
                        if (col.ColumnName == "CrDate")
                        {
                            colDateTemp = dr[col.ColumnName].ToString();
                        }
                    }
                    sb.AppendLine("</tr>");
                    //FileName += colDateTemp;
                }
                sb.AppendLine("</tr>");

                sb.AppendLine("<tr><td class='css1' colspan=" + dsColCount.ToString() + " style='mso-ignore:colspan'></td></tr>");

                //写数据表
                dt = ds.Tables[0];


                sb.AppendLine("<tr>");
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName.ToString().ToUpper().Equals("INDUSTRYNAME"))
                    {
                        sb.AppendLine("<td class='css2'>行业\\地区</td>");
                        continue;
                    }
                    sb.AppendLine("<td class=css2>" + col.ColumnName.ToString() + "</td>");
                }
                sb.AppendLine("</tr>");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.AppendLine("<tr>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        sb.AppendLine("<td>" + dr[col.ColumnName].ToString() + "</td>");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table>");
            }
            page.EnableViewState = false;
            page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            page.Response.Write(sb.ToString());
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            LogHelper.Debug("导出Excel数据失败：" + ex.Message);
        }
        finally
        {
            GC.Collect();
            page.Response.End();
        }
    }
}
