/**
 * 文件：ImportUtil.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：读取文件数据到缓存
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.Odbc;
using System.Data.OleDb;

/// <summary>
/// ImportUtil 的摘要说明
/// </summary>
public class ImportUtil
{
    public ImportUtil()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static DataSet OdbcFromCSV(string filePath, string fileName)
    {
        string strConn = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=";
        strConn += filePath;
        strConn += ";Extensions=csv,txt;HDR=No;";
        OdbcConnection objConn = new OdbcConnection(strConn);
        DataSet dsCSV = new DataSet();
        try
        {
            string strSql = "select * from " + fileName;
            OdbcDataAdapter odbcCSVDataAdapter = new OdbcDataAdapter(strSql, objConn);
            odbcCSVDataAdapter.Fill(dsCSV);
            return dsCSV;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DataSet OleDbFromCSV(string filePath, string fileName)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"text;HDR=No;FMT=Delimited\"";
        OleDbConnection objConn = new OleDbConnection(strConn);
        DataSet dsCSV = new DataSet();
        try
        {
            string strSql = "select * from " + fileName;
            OleDbDataAdapter oledb = new OleDbDataAdapter(strSql, objConn);
            oledb.Fill(dsCSV);
            return dsCSV;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
