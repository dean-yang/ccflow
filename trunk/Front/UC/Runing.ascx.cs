﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Runing : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = this.ToE("OnTheWayWork", "在途工作");

        int colspan = 6;
        this.AddTable("width='90%' align=center");
        this.AddTR();
        this.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.AddTREnd();

        this.AddTR();
        this.Add("<TD class=TitleMsg colspan=" + colspan + "><img src='./Img/Runing.gif' > <b>" + this.ToE("OnTheWayWork", "在途工作") + "</b></TD>");
        this.AddTREnd();

        this.AddTR();
        this.AddTDTitle(this.ToE("IDX", "序"));
        this.AddTDTitle(this.ToE("Name", "名称"));
        this.AddTDTitle(this.ToE("CurrNode", "当前节点"));
        this.AddTDTitle(this.ToE("StartDate", "发起日期"));
        this.AddTDTitle(this.ToE("StartDate", "发起人"));
        this.AddTDTitle(this.ToE("Oper", "操作"));
        this.AddTREnd();

        string sql = "  SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerList B  WHERE A.WorkID=B.WorkID   AND B.FK_EMP='" + BP.Web.WebUser.No + "' AND B.IsEnable=1";
        GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = false;
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.AddTR(is1);
            this.AddTD(i);
            this.AddTD(gwf.FK_FlowText);
            this.AddTD(gwf.FK_NodeText);
            this.AddTD(gwf.RDT);
            this.AddTD(gwf.RecText);
            this.AddTD("[<a href='MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "' ><img src='../images/btn/do.gif' border=0 />" + this.ToE("Do", "执行") + "</a>][<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo.aspx?DoType=UnSend&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>][<a href=\"javascript:WinOpen('WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>]");
            this.AddTREnd();
        }

        this.AddTRSum();
        this.AddTD("colspan=" + colspan, "&nbsp;");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
