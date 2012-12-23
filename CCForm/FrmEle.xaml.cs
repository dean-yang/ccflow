﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Silverlight;

namespace CCForm
{
    public partial class FrmEle : ChildWindow
    {
        public string GetEleType(string eleTypeStr)
        {
            string s = "";
            if (eleTypeStr == "0")
                s = "HandSiganture";

            if (eleTypeStr == "1")
                s = "EleSiganture";

            if (eleTypeStr == "2")
                s = "CheckGroup";

            if (eleTypeStr == "3")
                s = "iFrame";
            return s;
        }
        public int NodeID = 0;
        public Boolean IsNew = false;
        public BPEle HisEle
        {
            get
            {
                string eleType = this.DDL_EleType.SelectedIndex.ToString();
              

                BPEle ele = new BPEle();
                ele.Name = Glo.FK_MapData + "_" + eleType + "_" + this.TB_EleID.Text;
                ele.EleID = this.TB_EleID.Text;
                ele.EleType = eleType;
                ele.EleName = this.TB_EleName.Text;
                return ele;
            }
            set
            {
                return;
            }
        }
        public FrmEle()
        {
            InitializeComponent();
        }
        public void SetBlank()
        {
            this.TB_EleID.Text = "";
            this.TB_EleName.Text = "";
            this.DDL_EleType.SelectedIndex = 0;
            this.CB_IsReadonly.IsChecked = true;

            this.TB_Tag1.Text = "";
            this.TB_Tag2.Text = "";
            this.TB_Tag3.Text = "";
            this.TB_Tag4.Text = "";

            this.IsNew = true;
            this.TB_EleID.IsEnabled = true;
            this.DDL_EleType.IsEnabled = true;
        }
        public void BindData(string mypk)
        {
              FF.CCFormSoapClient da = Glo.GetCCFormSoapClientServiceInstance();
                da.GetXmlDataAsync("FrmEle.xml");
                da.GetXmlDataCompleted += new EventHandler<FF.GetXmlDataCompletedEventArgs>(da_GetXmlDataCompleted);
             da = Glo.GetCCFormSoapClientServiceInstance();
            da.RunSQLReturnTableAsync("SELECT * FROM Sys_FrmEle WHERE MyPK='" + mypk + "'");
            da.RunSQLReturnTableCompleted += new EventHandler<FF.RunSQLReturnTableCompletedEventArgs>(da_RunSQLReturnTableCompleted);
        }

        void da_RunSQLReturnTableCompleted(object sender, FF.RunSQLReturnTableCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                this.SetBlank();
            }
            else
            {
                this.TB_EleID.IsEnabled = false;
                this.DDL_EleType.IsEnabled = false;
                this.IsNew = false;
                this.TB_EleID.Text = dt.Rows[0]["EleID"].ToString();
                this.TB_EleName.Text = dt.Rows[0]["EleName"].ToString();

                int idx = 0;
                if (dt.Rows[0]["EleType"] == "HandSiganture")
                    idx = 0;

                if (dt.Rows[0]["EleType"] == "EleSiganture")
                    idx = 1;

                if (dt.Rows[0]["EleType"] == "CheckGroup")
                    idx = 2;

                if (dt.Rows[0]["EleType"] == "iFrame")
                    idx = 3;

                this.DDL_EleType.SelectedIndex = idx;
                this.TB_Tag1.Text = dt.Rows[0]["Tag1"].ToString();
                this.TB_Tag2.Text = dt.Rows[0]["Tag2"].ToString();
                this.TB_Tag3.Text = dt.Rows[0]["Tag3"].ToString();
                this.TB_Tag4.Text = dt.Rows[0]["Tag4"].ToString();
            }
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string eleType = this.DDL_EleType.SelectedIndex.ToString();
            if (eleType == "0")
                eleType = "HandSiganture";

            if (eleType == "1")
                eleType = "EleSiganture";

            if (eleType == "2")
                eleType = "CheckGroup";

            if (eleType == "3")
                eleType = "iFrame";

            string mypk = Glo.FK_MapData + "_" + eleType + "_" + this.TB_EleID.Text.Trim();
            string strs = "@EnName=BP.Sys.FrmEle@PKVal=" + mypk;
            strs += "@EleType=" + eleType;
            strs += "@EleName=" + this.TB_EleName.Text.Trim();
            strs += "@EleID=" + this.TB_EleID.Text.Trim();
            strs += "@FK_MapData=" + Glo.FK_MapData;

            if (this.CB_IsReadonly.IsChecked == true)
                strs += "@IsEnable=1";
            else
                strs += "@IsEnable=0";

            strs += "@Tag1=" + this.TB_Tag1.Text.Trim();
            strs += "@Tag2=" + this.TB_Tag2.Text.Trim();
            strs += "@Tag3=" + this.TB_Tag3.Text.Trim();
            strs += "@Tag4=" + this.TB_Tag4.Text.Trim();

            FF.CCFormSoapClient da = Glo.GetCCFormSoapClientServiceInstance();
            da.SaveEnAsync(strs);
            da.SaveEnCompleted += new EventHandler<FF.SaveEnCompletedEventArgs>(da_SaveEnCompleted);
        }
        void da_SaveEnCompleted(object sender, FF.SaveEnCompletedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void DDL_EleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // this.ReSetConfig();
        }
        public DataTable dtConfig = null;
        void da_GetXmlDataCompleted(object sender, FF.GetXmlDataCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            this.dtConfig = ds.Tables[0];
        }
        public void ReSetConfig()
        {
            if (dtConfig == null)
            {
                DDL_EleType_SelectionChanged(null, null);
                return;
            }

            string eleType = this.DDL_EleType.SelectedIndex.ToString();
            if (eleType == "0")
                eleType = "HandSiganture";

            if (eleType == "1")
                eleType = "EleSiganture";

            if (eleType == "2")
                eleType = "CheckGroup";

            if (eleType == "3")
                eleType = "iFrame";

            foreach (DataRow dr in dtConfig.Rows)
            {
                if (dr["DFor"].ToString() != eleType)
                    continue;

                for (int i = 1; i < 5; i++)
                {
                    string str = dr["Tag" + i].ToString();
                    if (string.IsNullOrEmpty(str))
                        continue;

                    string[] kvs = str.Split('@');
                    foreach (string k in kvs)
                    {
                        string[] kk = k.Split('=');
                        switch (kk[0])
                        {
                            case "Label":
                                Label lab = this.FindName("Lab_Tag" + i) as Label;
                                lab.Content = kk[1];
                                break;
                            case "DefVal":
                                TextBox tb = this.FindName("TB_Tag" + i) as TextBox;
                                if (tb.Text.Trim() == "")
                                    tb.Text = kk[1];
                                break;
                            case "FType":
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        private void TB_EleName_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void TB_EleName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.TB_EleID.IsEnabled == false)
                return;
            FF.CCFormSoapClient da = Glo.GetCCFormSoapClientServiceInstance();
            da.ParseStringToPinyinAsync(this.TB_EleName.Text.Trim());
            da.ParseStringToPinyinCompleted += new EventHandler<FF.ParseStringToPinyinCompletedEventArgs>(da_ParseStringToPinyinCompleted);
        }

        void da_ParseStringToPinyinCompleted(object sender, FF.ParseStringToPinyinCompletedEventArgs e)
        {
            this.TB_EleID.Text = e.Result;
        }
    }
}

