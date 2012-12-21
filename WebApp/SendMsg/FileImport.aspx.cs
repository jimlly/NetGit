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
using System.IO;

public partial class SendMsg_FileImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string mobiles = "";
        try
        {
            if (string.IsNullOrEmpty(this.SrcFile.FileName))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='JavaScript'>alert('导入文件不能为空!');</script>");
                return;
            }
            HttpPostedFile file = this.Request.Files[0];
            string addressFileName = Base.GetCurrentTime(DateTime.Now, 1) + Base.GetExtName(file.FileName);
            if (Base.GetExtName(file.FileName).ToLower() != ".csv" && Base.GetExtName(file.FileName).ToLower() != ".txt")
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='JavaScript'>alert('不支持的文件类型,请重新选择文件!');</script>");
                return;
            }
            if (!Directory.Exists(Public.AddressPath))
            {
                Directory.CreateDirectory(Public.AddressPath);
            }
            string addressFilePath = Public.AddressPath + addressFileName;
            file.SaveAs(addressFilePath);

            mobiles = GetMobileFromFile(addressFilePath).Replace("\r\n","");
        }
        catch
        {
            mobiles = "-3";
        }
        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='JavaScript'>window.returnValue = \"" + mobiles + "\";window.close(); </script>");
    }

    private string GetMobileFromFile(string file)
    {
        StringBuilder sb = new StringBuilder();
        string filePath = "", fileName = "";
        filePath = file.Substring(0, file.LastIndexOf("\\"));
        fileName = file.Substring(file.LastIndexOf("\\") + 1);
        DataSet ds = ImportUtil.OleDbFromCSV(filePath, fileName);
        if (ds == null)
        {
            return "-1";//文件无效
        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                return "-2";//文件中没有合法的联系人数据
            }
            else
            {
                if (!ValidateCls.IsMobile(ds.Tables[0].Rows[0][0].ToString()))
                {
                    ds.Tables[0].Rows[0].Delete();
                    ds.AcceptChanges();
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.Append(dr[0].ToString());

                    if (ds.Tables[0].Columns.Count > 1 && dr[1].ToString().Trim().Length > 0)
                    {
                        sb.Append(",").Append(dr[1].ToString());
                    }

                    if (sb.ToString().EndsWith(";"))
                    {
                        sb.Append("\r\n");
                    }
                    else
                    {
                        sb.Append(";\r\n");
                    }
                }
                return sb.ToString();
            }
        }
    }
}
