﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using BP.Web.Controls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Sys;
using BP.En;
using BP.En;
using BP.Web;
using BP.Web.UC;

public partial class Comm_MapDef_EditTable : BP.Web.PageBase
{
    /// <summary>
    /// 执行类型
    /// </summary>
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string FType
    {
        get
        {
            return this.Request.QueryString["FType"];
        }
    }
    public string IDX
    {
        get
        {
            return this.Request.QueryString["IDX"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "编辑外键类型";
        MapAttr attr = new MapAttr(this.RefOID);
        BindTable(attr);
    }
    public void BindTable(MapAttr mapAttr)
    {
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeftTX(this.GetCaption);
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(this.ToE("Item", "项目"));
        this.Pub1.AddTDTitle(this.ToE("Value", "值"));
        this.Pub1.AddTDTitle(this.ToE("Desc","描述") );
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("FEName", "字段英文名称")); // "字段英文名称"
        TB tb = new TB();
        tb.ID = "TB_KeyOfEn";
        tb.Text = mapAttr.KeyOfEn;
        if (this.RefOID > 0)
            tb.Enabled = false;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("DefaultVal", "默认值")); // "默认值"

        DDL ddl = new DDL();
        ddl.ID = "DDL";
        ddl.BindEntities(mapAttr.HisEntitiesNoName);
        ddl.SetSelectItem(mapAttr.DefVal);
        this.Pub1.AddTD(ddl);


        if (mapAttr.UIBindKey.Contains(".") == false)
        {
            CheckBox cb = new CheckBox();
            cb.Text = ToE("DefNull", "默认值为空"); //"默认值为空";
            cb.ID = "CB_IsDefValNull";
            if (mapAttr.DefVal == null || mapAttr.DefVal == "")
                cb.Checked = true;
            else
                cb.Checked = false;
            this.Pub1.AddTD(cb);
        }
        else
        {
            string ensName = mapAttr.HisEntitiesNoName.ToString();
            SFTable sf = new SFTable(ensName);
            if (sf.DefVal.Contains("@") == false)
            {
                CheckBox cb1 = new CheckBox();
                cb1.Text = ToE("DefNull", "默认值为空");//"默认值为空";
                cb1.ID = "CB_IsDefValNull";
                if (mapAttr.DefVal == null || mapAttr.DefVal == "")
                    cb1.Checked = true;
                else
                    cb1.Checked = false;
                this.Pub1.AddTD(cb1);
            }
            else
            {
                ddl = new DDL();
                ddl.ID = "DDL_DefVal";
                string[] defval = sf.DefVal.Split('@');
                foreach (string s in defval)
                {
                    if (s == null || s == "")
                        continue;

                    string[] labs = s.Split('=');
                    ddl.Items.Add(new ListItem(labs[1], "@" + labs[0]));
                }
                ddl.Items.Add(new ListItem("默认值为空", ""));
                ddl.Items.Add(new ListItem("默认当前选择", "@Select"));


                string rel = mapAttr.GetValStrByKey(MapAttrAttr.DefVal);
                if (rel.Contains("@") == false && rel != "")
                    rel = "@Select";

                ddl.SetSelectItem(rel);
                this.Pub1.AddTD(ddl);
            }
        }
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("FLabel", "字段中文名称")); // 字段中文名称
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = mapAttr.Name;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("IsCanEdit", "是否可编辑"));
        this.Pub1.Add("<TD>");
        RadioButton rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_0";
        rb.Text = this.ToE("UnCanEdit", "不可编辑");  //"不可编辑";
        rb.GroupName = "s";
        if (mapAttr.UIIsEnable)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_1";
        rb.Text = this.ToE("CanEdit", "可编辑"); //"可编辑";
        rb.GroupName = "s";

        if (mapAttr.UIIsEnable)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.Add("</TD>");

        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();


        #region 是否可单独行显示
        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("CtlShowWay", "呈现方式")); //呈现方式;
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_0";
        rb.Text = this.ToE("Row2", "两列显示"); // 两行
        rb.GroupName = "sa";
        if (mapAttr.UIIsLine)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_1";
        rb.Text = this.ToE("Row1", "整行显示"); // "一行";
        rb.GroupName = "sa";

        if (mapAttr.UIIsLine)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTD("控制该它在表单的显示方式");
        this.Pub1.AddTREnd();
        #endregion 是否可编辑


        this.Pub1.AddTRSum();
        this.Pub1.Add("<TD colspan=3>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = this.ToE("Save","保存");
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndClose1";
        btn.Text = this.ToE("Close","关闭"); ;
        btn.Attributes["onclick"] = " window.close(); return false;";
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndClose";
        btn.Text = this.ToE("SaveAndClose", "保存并关闭"); //"保存并关闭";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);   

        btn = new Button();
        btn.ID = "Btn_SaveAndNew";
        btn.Text = this.ToE("SaveAndNew", "保存并新建");
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        if (this.RefOID > 0)
        {
            btn = new Button();
            btn.ID = "Btn_AutoFull";
            btn.Text = this.ToE("AutoFull", "自动填写");
            //  btn.Click += new EventHandler(btn_Save_Click);
            btn.Attributes["onclick"] = "javascript:WinOpen('AutoFill.aspx?RefOID=" + this.RefOID + "',''); return false;";
            this.Pub1.Add(btn);

            if (mapAttr.HisEditType == EditType.Edit)
            {
                btn = new Button();
                btn.ID = "Btn_Del";
                btn.Text = this.ToE("Del", "删除");
                btn.Click += new EventHandler(btn_Save_Click);
                btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
                this.Pub1.Add(btn);
            }
        }

        string url = "Do.aspx?DoType=AddF&MyPK=" + mapAttr.FK_MapData + "&IDX=" + mapAttr.IDX;
        this.Pub1.Add("<a href='" + url + "'><img src='../../Images/Btn/Insert.gif' border=0>" + this.ToE("New", "新建") + "</a></TD>");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
    void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            switch (btn.ID)
            {
                case "Btn_Del":
                    this.Response.Redirect("Do.aspx?DoType=Del&MyPK=" + this.MyPK + "&RefOID=" + this.RefOID, true);
                    return;
                default:
                    break;
            }

            MapAttr attr = new MapAttr();
            attr.OID = this.RefOID;
            attr.Retrieve();
            attr = (MapAttr)this.Pub1.Copy(attr);
            attr.FK_MapData = this.MyPK;
            if (this.Pub1.IsExit("CB_IsDefValNull"))
            {
                if (this.Pub1.GetCBByID("CB_IsDefValNull").Checked == false)
                    attr.DefVal = this.Pub1.GetDDLByID("DDL").SelectedItemStringVal;
                else
                    attr.DefVal = "";
            }
            else
            {
                string s = this.Pub1.GetDDLByID("DDL_DefVal").SelectedItemStringVal;
                if (s == "@Select")
                {
                    attr.DefVal = this.Pub1.GetDDLByID("DDL").SelectedItemStringVal;
                }
                else
                {
                    attr.DefVal = s;
                }
            }

            attr.Update();

            switch (btn.ID)
            {
                case "Btn_SaveAndClose":
                    this.WinClose();
                    return;
                case "Btn_SaveAndNew":
                    this.Response.Redirect("Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&IDX=" + attr.IDX, true);
                    return;
                default:
                    break;
            }
            this.Response.Redirect("EditTable.aspx?DoType=Edit&MyPK=" + this.MyPK + "&RefOID=" + attr.OID, true);
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    public string GetCaption
    {
        get
        {
            if (this.DoType == "Add")
                return this.ToE("GuideNewField", "增加新字段向导") + "  - <a href='Do.aspx?DoType=ChoseFType'>" + this.ToE("ChoseType", "选择类型") + "</a> -" + this.ToE("EditField", "编辑字段");
            else
                return this.ToE("EditField", "编辑字段"); // "编辑字段";
        }
    }
}
