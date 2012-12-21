using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxPro;
using System.Data;
using Com.Yuantel.MobileMsg.DAL;
using Com.Yuantel.MobileMsg.Model;
using System.Text;

namespace WebApp.Settings
{
    public partial class Setting_Phrase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Setting_Phrase));
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
            DataSet ds = GetPhraseList();
            gvPhrase.DataSource = ds.Tables[0].DefaultView;
            gvPhrase.DataBind();
            for (int i = 0; i < gvPhrase.Rows.Count; i++)
            {
                gvPhrase.Rows[i].Cells[0].Text = SetCells(i);
            }
        }

        private string SetCells(int index)
        {
            int[] arr1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string[] arr2 = new string[] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
            return "短语" + arr2[arr1[index]];
        }

        [AjaxMethod]
        public int AddPhrase(string title, string content)
        {
            Phrase phrase = new Phrase();
            phrase.SeqNo = int.Parse(Session["SeqNo"].ToString());
            phrase.Phrase1 = content;
            return PhraseDal.Insert(phrase);
        }

        [AjaxMethod]
        public DataSet GetPhraseList()
        {
            return PhraseDal.Select(int.Parse(Session["SeqNo"].ToString()));
        }

        //protected void gvSignatrue_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvSignatrue.EditIndex = e.NewEditIndex;
        //    this.txtTitle.Text = gvSignatrue.Rows[gvSignatrue.EditIndex].Cells[1].Text;
        //    this.txtContent.Text = gvSignatrue.Rows[gvSignatrue.EditIndex].Cells[2].Text;
        //}

        protected void gvPhrase_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                Phrase phrase = new Phrase();
                phrase.Id = Convert.ToInt32(e.CommandArgument.ToString());
                if (PhraseDal.Delete(phrase) == 1)
                {
                    MsgBox("删除成功！");
                    BindList();
                }
                else
                {
                    MsgBox("删除失败！");
                }
                if (this.hfID.Value.Length > 0)
                {
                    this.txtContent.Text = "";
                }
            }
            this.hfID.Value = "";
        }
      
        protected void gvPhrase_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.txtContent.Text = gvPhrase.Rows[e.NewEditIndex].Cells[5].Text;
            this.hfID.Value = gvPhrase.Rows[e.NewEditIndex].Cells[4].Text;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            int result = 0;
            Phrase phrase = new Phrase();
            phrase.SeqNo = int.Parse(Session["SeqNo"].ToString());
            phrase.Phrase1 = Request["txtContent"].ToString().Trim();
            try
            {
                phrase.Id = int.Parse(this.hfID.Value);
            }
            catch
            {
                phrase.Id = 0;
            }

            if (phrase.Id == 0)
            {
                if (gvPhrase.Rows.Count >= 10)
                {
                    MsgBox("已达到常用短语条数上限！");
                    return;
                }
                result = PhraseDal.Insert(phrase);
            }
            else
            {
                result = PhraseDal.Update(phrase);
            }

            if (result == 1)
            {
                MsgBox("操作成功！");
                BindList();
                this.txtContent.Text = "";
            }
            else
            {
                MsgBox("操作失败！");
            }
            this.hfID.Value = "";
        }

        protected void gvPhrase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onmouseout", "style.backgroundColor=''");
                //e.Row.Attributes.Add("onmouseover", "style.backgroundColor='#FFFFDF'");
            }
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

        protected void gvPhrase_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

    }
}