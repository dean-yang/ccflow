﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Lizard.Common;
using System.Drawing;
using LTP.Accounts.Bus;
using BP.DA;
using BP.CCOA;
namespace Lizard.OA.Web.OA_Notice
{
    public partial class Manage : BasePage
    {
        BP.CCOA.OA_Notice bll = new BP.CCOA.OA_Notice();

        private int m_PageIndex = 1;

        private int m_PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());

        string[] columns = { 
                   OA_NoticeAttr.Author,
                   OA_NoticeAttr.NoticeTitle,
                   OA_NoticeAttr.NoticeSubTitle,
                   OA_NoticeAttr.NoticeType
                   };
        BP.CCOA.OA_Notice OA_Notice = new BP.CCOA.OA_Notice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                //gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

                int rowsCount = this.GetQueryRowsCount();
                this.XPager1.InitControl(this.m_PageSize, rowsCount);

                BindData();
            }
        }

        protected void XPager1_PagerChanged(object sender, CurrentPageEventArgs e)
        {
            m_PageIndex = e.pageSize;
            m_PageIndex = e.currentPage;
            this.BindData();
        }

        private int GetQueryRowsCount()
        {
            string searchValue = Request.QueryString["searchvalue"];
            IDictionary<string, object> whereConditions = this.GetWhereConditon();
            return XQueryTool.GetRowCount<BP.CCOA.OA_Notice>(OA_Notice, columns, searchValue, whereConditions);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //string idlist = GetSelIDlist();
            //if (idlist.Trim().Length == 0) 
            //    return;
            //bll.DeleteList(idlist);
            //BindData();
        }

        #region gridView

        public void BindData()
        {
            #region
            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    return;
            //}
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            //if (user.HasPermissionID(PermId_Modify))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}
            #endregion
            string searchValue = Request.QueryString["searchvalue"];
            DataTable OA_NoticeTable = new DataTable();

            string noticeTime = this.xdpCreateDate.Text;
            IDictionary<string, object> whereConditions = this.GetWhereConditon();
            OA_NoticeTable = XQueryTool.Query(OA_Notice, columns, searchValue, m_PageIndex,
                m_PageSize, whereConditions);

            gridView.DataSource = OA_NoticeTable;
            gridView.DataBind();

        }

        private IDictionary<string, object> GetWhereConditon()
        {
            IDictionary<string, object> whereConditions = new Dictionary<string, object>();

            string sendTime = this.xdpCreateDate.Text;
            if (sendTime != string.Empty)
            {
                whereConditions.Add("SUBSTR(" + OA_NoticeAttr.CreateTime + ",1,10)", sendTime);
            }

            string userId = CurrentUser.No;
            whereConditions.Add(OA_MessageAttr.Author, userId);

            return whereConditions;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Cells[0].Text = "<input id='Checkbox2' type='checkbox' onclick='CheckAll()'/><label></label>";
            }
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                linkbtnDel.Attributes.Add("onclick", "return confirm(\"你确认要删除吗\")");

                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[1].Text = obj1.ToString();
                //}
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //#warning 代码生成警告：请检查确认真实主键的名称和类型是否正确
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //bll.Delete(ID);
            //gridView.OnBind();
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl("DeleteThis");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #endregion
        protected void lbtReaded_Click(object sender, EventArgs e)
        {
            string userId = CurrentUser.No;
            bool isSuccess = OA_Notice.SetAllRead(userId);
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            this.BindData();
        }
    }
}
