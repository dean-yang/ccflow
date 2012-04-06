﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web.Controls;
using BP.Sys;
using BP.En;
using BP.Web;
using BP.Web.UC;
using BP.DA;

public partial class WF_MapDef_AutoFull : BP.Web.WebPage
{
    #region 属性
    /// <summary>
    /// 执行类型
    /// </summary>
    public new string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public int FType
    {
        get
        {
            return int.Parse(this.Request.QueryString["FType"]);
        }
    }
    public string IDX
    {
        get
        {
            return this.Request.QueryString["IDX"];
        }
    }
    public string FK_MapData
    {
        get
        {
            return this.Request.QueryString["FK_MapData"];
        }
    }
    #endregion

    #region 属性
    public void BindTop()
    {
        // this.Pub1.Add("\t\n<div id='tabsJ'  align='center'>");
        MapExtXmls fss = new MapExtXmls();
        fss.RetrieveAll();

        this.Left.Add("<a href='http://ccflow.org' target=_blank  ><img src='../../DataUser/ICON/"+BP.SystemConfig.CompanyID+"/LogBiger.png' style='width:180px;' /></a><hr>");

        //this.Left.Add("<a href='http://ccflow.org' target=_blank ><img src='../../DataUser/LogBiger.png' /></a><hr>");

        this.Left.AddUL();
        foreach (MapExtXml fs in fss)
        {
            if (this.PageID == fs.No)
                this.Left.AddLiB(fs.URL + "&FK_MapData=" + this.FK_MapData + "&ExtType=" + fs.No + "&RefNo=" + this.RefNo, "<span>" + fs.Name + "</span>");
            else
                this.Left.AddLi(fs.URL + "&FK_MapData=" + this.FK_MapData + "&ExtType=" + fs.No + "&RefNo=" + this.RefNo, "<span>" + fs.Name + "</span>");
        }
        this.Left.AddLi("<a href='MapExt.aspx?FK_MapData=" + this.FK_MapData + "&RefNo=" + this.RefNo + "'><span>帮助</span></a>");
        this.Left.AddULEnd();
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindTop();

        if (this.RefNo == null)
        {
            /*请选择要设置的字段*/
            MapAttrs mattrs = new MapAttrs();
            mattrs.Retrieve(MapAttrAttr.FK_MapData, this.FK_MapData);

            this.Pub1.AddFieldSet("请选择要设置的字段");
            this.Pub1.AddUL("class=''");
            foreach (MapAttr en in mattrs)
            {
              
                if (en.UIVisible == false && en.UIIsEnable==false)
                    continue;

                this.Pub1.AddLi("?FK_MapData=" + this.FK_MapData + "&RefNo=" +  en.MyPK + "&ExtType=AutoFull", en.KeyOfEn + " - " + en.Name);
            }
            this.Pub1.AddULEnd();
            this.Pub1.AddFieldSetEnd();
            return;
        }

        MapAttr mattr = new MapAttr(this.RefNo);
        Attr attr = mattr.HisAttr;
        this.Title = "为[" + mattr.KeyOfEn + "][" + mattr.Name + "]设置自动完成"; // this.ToE("GuideNewField");

        switch (attr.MyDataType)
        {
            case BP.DA.DataType.AppRate:
            case BP.DA.DataType.AppMoney:
            case BP.DA.DataType.AppInt:
            case BP.DA.DataType.AppFloat:
                BindNumType(mattr);
                break;
            case BP.DA.DataType.AppString:
                BindStringType(mattr);
                break;
            default:
                BindStringType(mattr);
                break;
        }

    }
    public void BindStringType(MapAttr mattr)
    {
        BindNumType(mattr);
    }
    public void BindNumType(MapAttr mattr)
    {
        this.Pub1.AddTable("align=left");
        this.Pub1.AddCaptionLeft("数据获取 - 当一个字段值需要从其它表中得到时，请设置此功能。");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD>");
        RadioBtn rb = new RadioBtn();
        rb.GroupName = "s";
        rb.Text = this.ToE("Way0", "方式0：不做任何设置。");
        rb.ID = "RB_Way_0";
        if (mattr.HisAutoFull == AutoFullWay.Way0)
            rb.Checked = true;
        this.Pub1.AddFieldSet(rb);
        this.Pub1.Add(this.ToE("Way0D", "不做任何设置。"));
        this.Pub1.AddFieldSetEnd();

        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.Add("<TD>");

        rb = new RadioBtn();
        rb.GroupName = "s";
        rb.Text = this.ToE("Way1", "方式1：本表单中数据计算。"); //"";
        rb.ID = "RB_Way_1";
        if (mattr.HisAutoFull == AutoFullWay.Way1_JS)
            rb.Checked = true;
        this.Pub1.AddFieldSet(rb);
        this.Pub1.Add(this.ToE("Way1D", "比如:@单价*@数量"));
        this.Pub1.AddBR();

        TextBox tb = new TextBox();
        tb.ID = "TB_JS";
        tb.Width = 450;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 5;
        if (mattr.HisAutoFull == AutoFullWay.Way1_JS)
        {
            tb.Text = mattr.AutoFullDoc;
        }
        this.Pub1.Add(tb);
        this.Pub1.AddFieldSetEnd();
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        // 方式2 利用SQL自动填充
        this.Pub1.AddTR();
        this.Pub1.Add("<TD>");

        rb = new RadioBtn();
        rb.GroupName = "s";
        rb.Text = this.ToE("Way2", "方式2：利用SQL自动填充。");
        rb.ID = "RB_Way_2";
        if (mattr.HisAutoFull == AutoFullWay.Way2_SQL)
            rb.Checked = true;

        this.Pub1.AddFieldSet(rb);
        this.Pub1.Add(this.ToE("Way2D", "比如:Select Addr From 商品表 WHERE No=@FK_Pro  FK_Pro是本表中的任意字段名") + "<BR>");

        tb = new TextBox();
        tb.ID = "TB_SQL";
        tb.Width = 450;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 5;
        if (mattr.HisAutoFull == AutoFullWay.Way2_SQL)
            tb.Text = mattr.AutoFullDoc;

        this.Pub1.Add(tb);

        this.Pub1.AddFieldSetEnd();
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        // 方式3 本表单中外键列
        this.Pub1.AddTR();
        this.Pub1.Add("<TD>");
        rb = new RadioBtn();
        rb.GroupName = "s";
        rb.Text = this.ToE("Way3", "方式3：本表单中外键列。");
        // rb.Text = "方式3：本表单中外键列</font></b>";
        rb.ID = "RB_Way_3";
        if (mattr.HisAutoFull == AutoFullWay.Way3_FK)
            rb.Checked = true;

        this.Pub1.AddFieldSet(rb);
        this.Pub1.Add(this.ToE("Way3D", "比如:表单中有商品编号列,需要填充商品地址、供应商电话。"));
        this.Pub1.AddBR();


        // 让它等于外键表的一个值。
        Attrs attrs = null;
        MapData md = new MapData();
        md.No = mattr.FK_MapData;
        if (md.RetrieveFromDBSources() == 0)
        {
            attrs = md.GenerHisMap().HisFKAttrs;
        }
        else
        {
            MapDtl mdtl = new MapDtl();
            mdtl.No = mattr.FK_MapData;
            attrs = mdtl.GenerMap().HisFKAttrs;
        }

        if (attrs.Count > 0)
        {
        }
        else
        {
            rb.Enabled = false;
            if (rb.Checked)
                rb.Checked = false;
            this.Pub1.Add("@本表没有外键字段。");
        }

        foreach (Attr attr in attrs)
        {
            if (attr.IsRefAttr)
                continue;

            rb = new RadioBtn();
            rb.Text = attr.Desc;
            rb.ID = "RB_FK_" + attr.Key;
            rb.GroupName = "sd";

            if (mattr.AutoFullDoc.Contains(attr.Key))
                rb.Checked = true;

            this.Pub1.Add(rb);
            DDL ddl = new DDL();
            ddl.ID = "DDL_" + attr.Key;

            string sql = "";
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle9i:
                    continue;
                    sql = "Select fname as 'No' ,fDesc as 'Name' FROM Sys_FieldDesc WHERE tableName='" + attr.HisFKEn.EnMap.PhysicsTable + "'";
                    break;
                default:
                    sql = "Select name as 'No' ,Name as 'Name' from syscolumns WHERE ID=OBJECT_ID('" + attr.HisFKEn.EnMap.PhysicsTable + "')";
                    break;
            }

            //  string sql = "Select fname as 'No' ,fDesc as 'Name' FROM Sys_FieldDesc WHERE tableName='" + attr.HisFKEn.EnMap.PhysicsTable + "'";
            //string sql = "Select NO , NAME  FROM Port_Emp ";

            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                //  ddl.Items.Add(new ListItem(this.ToE("Field") + dr[0].ToString() + " " + this.ToE("Desc") + " " + dr[1].ToString(), dr[0].ToString()));
                ListItem li = new ListItem(dr[0].ToString() + "；" + dr[1].ToString(), dr[0].ToString());
                if (mattr.AutoFullDoc.Contains(dr[0].ToString()))
                    li.Selected = true;

                ddl.Items.Add(li);
            }

            this.Pub1.Add(ddl);
            this.Pub1.AddBR();
        }

        this.Pub1.AddFieldSetEnd();
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        // 方式3 本表单中外键列
        this.Pub1.AddTR();
        this.Pub1.Add("<TD>");
        rb = new RadioBtn();
        rb.GroupName = "s";
        rb.Text = this.ToE("Way4", "方式4：对一个明细表的列求值。");
        rb.ID = "RB_Way_4";
        if (mattr.HisAutoFull == AutoFullWay.Way4_Dtl)
            rb.Checked = true;

        this.Pub1.AddFieldSet(rb);
        this.Pub1.Add(this.ToE("Way4D", "比如:对明细表中的列求值。"));
        this.Pub1.AddBR();

        // 让它对一个明细表求和、求平均、求最大、求最小值。
        MapDtls dtls = new MapDtls(mattr.FK_MapData);
        if (dtls.Count > 0)
        {
        }
        else
        {
            rb.Enabled = false;
            if (rb.Checked)
                rb.Checked = false;
            // this.Pub1.Add("@没有明细表。");
        }
        foreach (MapDtl dtl in dtls)
        {
            DDL ddlF = new DDL();
            ddlF.ID = "DDL_" + dtl.No + "_F";
            MapAttrs mattrs1 = new MapAttrs(dtl.No);
            int count = 0;
            foreach (MapAttr mattr1 in mattrs1)
            {
                if (mattr1.LGType != FieldTypeS.Normal)
                    continue;

                if (mattr1.KeyOfEn == MapAttrAttr.MyPK)
                    continue;

                if (mattr1.IsNum == false)
                    continue;
                switch (mattr1.KeyOfEn)
                {
                    case "OID":
                    case "RefOID":
                    case "FID":
                        continue;
                    default:
                        break;
                }
                count++;
                ListItem li = new ListItem(mattr1.Name, mattr1.KeyOfEn);
                if (mattr.HisAutoFull == AutoFullWay.Way4_Dtl)
                    if (mattr.AutoFullDoc.Contains("=" + mattr1.KeyOfEn))
                        li.Selected = true;
                ddlF.Items.Add(li);
            }
            if (count == 0)
                continue;

            rb = new RadioBtn();
            rb.Text = dtl.Name;
            rb.ID = "RB_" + dtl.No;
            rb.GroupName = "dtl";
            if (mattr.AutoFullDoc.Contains(dtl.No))
                rb.Checked = true;

            this.Pub1.Add(rb);

            DDL ddl = new DDL();
            ddl.ID = "DDL_" + dtl.No + "_Way";
            ddl.Items.Add(new ListItem("求合计", "SUM"));
            ddl.Items.Add(new ListItem("求平均", "AVG"));
            ddl.Items.Add(new ListItem("求最大", "MAX"));
            ddl.Items.Add(new ListItem("求最小", "MIN"));
            this.Pub1.Add(ddl);

            if (mattr.HisAutoFull == AutoFullWay.Way4_Dtl)
            {
                if (mattr.AutoFullDoc.Contains("SUM"))
                    ddl.SetSelectItem("SUM");
                if (mattr.AutoFullDoc.Contains("AVG"))
                    ddl.SetSelectItem("AVG");
                if (mattr.AutoFullDoc.Contains("MAX"))
                    ddl.SetSelectItem("MAX");
                if (mattr.AutoFullDoc.Contains("MIN"))
                    ddl.SetSelectItem("MIN");
            }

            this.Pub1.Add(ddlF);
            this.Pub1.AddBR();
        }

        this.Pub1.AddFieldSetEnd();
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();


        #region 方式5
        //this.Pub1.AddTD();
        //this.Pub1.AddTR();

        //this.Pub1.AddFieldSet(rb);
        //this.Pub1.Add(this.ToE("Way2D", "嵌入的JS"));
        //tb = new TextBox();
        //tb.ID = "TB_JS";
        //tb.Width = 450;
        //tb.TextMode = TextBoxMode.MultiLine;
        //tb.Rows = 5;
        //if (mattr.HisAutoFull == AutoFullWay.Way5_JS)
        //    tb.Text = mattr.AutoFullDoc;
        //this.Pub1.Add(tb);
        //this.Pub1.AddFieldSetEnd();

        //this.Pub1.AddTDEnd();
        //this.Pub1.AddTREnd();
        #endregion 方式5




        this.Pub1.AddTRSum();
        this.Pub1.AddTDBegin("aligen=center");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " " + this.ToE("Save", "保存") + " ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndClose";
        btn.Text = " " + this.ToE("SaveAndClose", "保存并关闭") + " ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        return;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btn_Click(object sender, EventArgs e)
    {
        MapAttr mattr = new MapAttr(this.RefNo);
        if (this.Pub1.GetRadioButtonByID("RB_Way_0").Checked)
        {
            mattr.HisAutoFull = AutoFullWay.Way0;
        }

        // JS 方式。
        if (this.Pub1.GetRadioButtonByID("RB_Way_1").Checked)
        {
            mattr.HisAutoFull = AutoFullWay.Way1_JS;
            mattr.AutoFullDoc = this.Pub1.GetTextBoxByID("TB_JS").Text;
        }

        // 外键方式。
        if (this.Pub1.GetRadioButtonByID("RB_Way_2").Checked)
        {
            mattr.HisAutoFull = AutoFullWay.Way2_SQL;
            mattr.AutoFullDoc = this.Pub1.GetTextBoxByID("TB_SQL").Text;
        }

        // 本表单中外键列。
        string doc = "";
        if (this.Pub1.GetRadioButtonByID("RB_Way_3").Checked)
        {
            mattr.HisAutoFull = AutoFullWay.Way3_FK;
            MapData md = new MapData(mattr.FK_MapData);
            Attrs attrs = md.GenerHisMap().HisFKAttrs;
            foreach (Attr attr in attrs)
            {
                if (attr.IsRefAttr)
                    continue;

                if (this.Pub1.GetRadioButtonByID("RB_FK_" + attr.Key).Checked == false)
                    continue;
                // doc = " SELECT " + this.Pub1.GetDDLByID("DDL_" + attr.Key).SelectedValue + " FROM " + attr.HisFKEn.EnMap.PhysicsTable + " WHERE NO=@" + attr.Key;
                doc = "@AttrKey=" + attr.Key + "@Field=" + this.Pub1.GetDDLByID("DDL_" + attr.Key).SelectedValue + "@Table=" + attr.HisFKEn.EnMap.PhysicsTable;
            }
            mattr.AutoFullDoc = doc;
        }

        // 本表单中明细表列。
        if (this.Pub1.GetRadioButtonByID("RB_Way_4").Checked)
        {
            MapDtls dtls = new MapDtls(mattr.FK_MapData);
            mattr.HisAutoFull = AutoFullWay.Way4_Dtl;
            foreach (MapDtl dtl in dtls)
            {
                try
                {
                    if (this.Pub1.GetRadioButtonByID("RB_" + dtl.No).Checked == false)
                        continue;
                }
                catch
                {
                    continue;
                }
                //  doc = "SELECT " + this.Pub1.GetDDLByID( "DDL_"+dtl.No + "_Way").SelectedValue + "(" + this.Pub1.GetDDLByID("DDL_"+dtl.No+"_F").SelectedValue + ") FROM " + dtl.No + " WHERE REFOID=@OID";
                doc = "@Table=" + dtl.No + "@Field=" + this.Pub1.GetDDLByID("DDL_" + dtl.No + "_F").SelectedValue + "@Way=" + this.Pub1.GetDDLByID("DDL_" + dtl.No + "_Way").SelectedValue;
            }
            mattr.AutoFullDoc = doc;
        }

        try
        {
            mattr.DoCheckFullWay();
            mattr.Update();
        }
        catch (Exception ex)
        {
            this.ResponseWriteRedMsg(ex);
            return;
        }

        this.Alert(this.ToE("SaveOK", "保存成功"));
        this.Pub1.Clear();
        Button btn = sender as Button;
        if (btn.ID.Contains("Close"))
        {
            this.WinClose();
            return;
        }
        else
        {
            this.Response.Redirect(this.Request.RawUrl, true);
        }
    }
    public void BindStringType()
    {
    }
    public string GetCaption
    {
        get
        {
            if (this.DoType == "Add")
                return this.ToE("GuideNewField", "新增向导") + " - <a href='Do.aspx?DoType=ChoseFType'>" + this.ToE("ChoseType", "选择类型") + "</a> - " + this.ToE("Edit", "编辑");
            else
                return "<a href='Do.aspx?DoType=ChoseFType&MyPK=" + this.MyPK + "&RefNo=" + this.RefNo + "'>" + this.ToE("ChoseType", "选择类型") + "</a> - " + this.ToE("Edit", "编辑");
        }
    }
}