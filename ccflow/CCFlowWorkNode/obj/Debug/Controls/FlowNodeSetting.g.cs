﻿#pragma checksum "C:\Users\imzrh\Desktop\ccflow\ccflow\CCFlowWebDesigner\Controls\FlowNodeSetting.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2FAFB38BF0BF307AA009F4BB0057D806"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Ccflow.Web.UI.Control.Workflow.Setting {
    
    
    public partial class FlowNodeSetting : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Media.Animation.Storyboard sbFlowNodeSettingDisplay;
        
        internal System.Windows.Media.Animation.Storyboard sbFlowNodeSettingClose;
        
        internal System.Windows.Controls.Grid gridContainer;
        
        internal System.Windows.Controls.TextBlock tbFlowNodeName;
        
        internal System.Windows.Controls.TextBlock tbFlowNodeType;
        
        internal System.Windows.Controls.TextBox txtFlowNodeName;
        
        internal System.Windows.Controls.ComboBox cbFlowNodeType;
        
        internal System.Windows.Controls.TextBlock tbMergePictureRepeatDirection;
        
        internal System.Windows.Controls.ComboBox cbMergePictureRepeatDirection;
        
        internal System.Windows.Controls.TextBlock btSubFlow;
        
        internal System.Windows.Controls.ComboBox cbSubFlowList;
        
        internal System.Windows.Controls.Button btnSave;
        
        internal System.Windows.Controls.Button btnAppay;
        
        internal System.Windows.Controls.Button btnClose;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WF;component/Controls/FlowNodeSetting.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.sbFlowNodeSettingDisplay = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbFlowNodeSettingDisplay")));
            this.sbFlowNodeSettingClose = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbFlowNodeSettingClose")));
            this.gridContainer = ((System.Windows.Controls.Grid)(this.FindName("gridContainer")));
            this.tbFlowNodeName = ((System.Windows.Controls.TextBlock)(this.FindName("tbFlowNodeName")));
            this.tbFlowNodeType = ((System.Windows.Controls.TextBlock)(this.FindName("tbFlowNodeType")));
            this.txtFlowNodeName = ((System.Windows.Controls.TextBox)(this.FindName("txtFlowNodeName")));
            this.cbFlowNodeType = ((System.Windows.Controls.ComboBox)(this.FindName("cbFlowNodeType")));
            this.tbMergePictureRepeatDirection = ((System.Windows.Controls.TextBlock)(this.FindName("tbMergePictureRepeatDirection")));
            this.cbMergePictureRepeatDirection = ((System.Windows.Controls.ComboBox)(this.FindName("cbMergePictureRepeatDirection")));
            this.btSubFlow = ((System.Windows.Controls.TextBlock)(this.FindName("btSubFlow")));
            this.cbSubFlowList = ((System.Windows.Controls.ComboBox)(this.FindName("cbSubFlowList")));
            this.btnSave = ((System.Windows.Controls.Button)(this.FindName("btnSave")));
            this.btnAppay = ((System.Windows.Controls.Button)(this.FindName("btnAppay")));
            this.btnClose = ((System.Windows.Controls.Button)(this.FindName("btnClose")));
        }
    }
}

