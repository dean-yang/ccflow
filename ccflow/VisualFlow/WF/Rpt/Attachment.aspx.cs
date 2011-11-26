﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Web.Controls;

public partial class WF_Rpt_Attachment : WebPage
{
    #region 属性.
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public int OID
    {
        get
        {
            return int.Parse( this.Request.QueryString["OID"]);
        }
    }
    public int FID
    {
        get
        {
            return int.Parse( this.Request.QueryString["FID"] );
        }
    }
    #endregion 属性.

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 处理风格
        this.Page.RegisterClientScriptBlock("s",
         "<link href='./../../Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");
        if (this.Request.QueryString["PageIdx"] == null)
            this.PageIdx = 1;
        else
            this.PageIdx = int.Parse(this.Request.QueryString["PageIdx"]);
        #endregion 处理风格

        string flowID = int.Parse(this.FK_Flow).ToString();
        string sql = "SELECT * FROM Sys_FrmAttachmentDB WHERE FK_FrmAttachment IN (SELECT MyPK FROM Sys_FrmAttachment WHERE FK_MapData LIKE 'ND" + flowID + "%' AND IsUpload=1) AND RefPKVal='" + this.OID + "'";
        DataTable dt = DBAccess.RunSQLReturnTable(sql);
        if (dt.Rows.Count == 0)
        {
            this.Pub1.AddMsgInfo("提示", "当前流程没有数据，或者该流程没有附件。");
            return;
        }

        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft("附件");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("附件编号");
        this.Pub1.AddTDTitle("名称");
        this.Pub1.AddTDTitle("大小(kb)");
        this.Pub1.AddTDTitle("上传人");
        this.Pub1.AddTDTitle("上传日期");
        this.Pub1.AddTREnd();
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            i++;
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(i);
            this.Pub1.AddTD(dr["FK_FrmAttachment"].ToString());
            this.Pub1.AddTD("<a href='../FreeFrm/AttachmentUpload.aspx?DoType=Down&MyPK="+dr["MyPK"]+"' target=_sd ><img src='../../Images/FileType/" + dr["FileExts"] + ".gif' border=0/>" + dr["FileName"].ToString() + "</a>");
            this.Pub1.AddTD(dr["FileSize"].ToString());
            this.Pub1.AddTD(dr["RecName"].ToString());
            this.Pub1.AddTD(dr["RDT"].ToString());
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }
}