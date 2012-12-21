using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Com.Yuantel.MobileMsg.DAL;

namespace WebApp.Settings
{
    public partial class PhraseSelect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindList();
                }
                catch
                { }
            }
        }

        private void BindList()
        {
            DataSet ds = GetPhraseList();
            rpPhrase.DataSource = ds.Tables[0].DefaultView;
            rpPhrase.DataBind();
        }

        public DataSet GetPhraseList()
        {
            return PhraseDal.Select(int.Parse(Session["SeqNo"].ToString()));
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='JavaScript'>window.close(); </script>");
        }
    }
}