﻿#pragma checksum "D:\ccflow\WorkNode\FrmEvent.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5B48A5671AF2302F0E22D014B25CE2E6"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
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


namespace WorkNode {
    
    
    public partial class FrmEvent : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button CancelButton;
        
        internal System.Windows.Controls.Button OKButton;
        
        internal System.Windows.Controls.RadioButton RB_FrmLoadBefore;
        
        internal System.Windows.Controls.RadioButton RB_FrmLoadAfter;
        
        internal System.Windows.Controls.RadioButton RB_SaveBefore;
        
        internal System.Windows.Controls.RadioButton RB_SaveAfter;
        
        internal System.Windows.Controls.Label label4;
        
        internal System.Windows.Controls.ComboBox DDL_EventType;
        
        internal System.Windows.Controls.Label label1;
        
        internal System.Windows.Controls.TextBox TB_DoDoc;
        
        internal System.Windows.Controls.Label label2;
        
        internal System.Windows.Controls.TextBox TB_MsgOK;
        
        internal System.Windows.Controls.Label label3;
        
        internal System.Windows.Controls.TextBox TB_MsgErr;
        
        internal System.Windows.Controls.Button Btn_Help;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WorkNode;component/FrmEvent.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
            this.OKButton = ((System.Windows.Controls.Button)(this.FindName("OKButton")));
            this.RB_FrmLoadBefore = ((System.Windows.Controls.RadioButton)(this.FindName("RB_FrmLoadBefore")));
            this.RB_FrmLoadAfter = ((System.Windows.Controls.RadioButton)(this.FindName("RB_FrmLoadAfter")));
            this.RB_SaveBefore = ((System.Windows.Controls.RadioButton)(this.FindName("RB_SaveBefore")));
            this.RB_SaveAfter = ((System.Windows.Controls.RadioButton)(this.FindName("RB_SaveAfter")));
            this.label4 = ((System.Windows.Controls.Label)(this.FindName("label4")));
            this.DDL_EventType = ((System.Windows.Controls.ComboBox)(this.FindName("DDL_EventType")));
            this.label1 = ((System.Windows.Controls.Label)(this.FindName("label1")));
            this.TB_DoDoc = ((System.Windows.Controls.TextBox)(this.FindName("TB_DoDoc")));
            this.label2 = ((System.Windows.Controls.Label)(this.FindName("label2")));
            this.TB_MsgOK = ((System.Windows.Controls.TextBox)(this.FindName("TB_MsgOK")));
            this.label3 = ((System.Windows.Controls.Label)(this.FindName("label3")));
            this.TB_MsgErr = ((System.Windows.Controls.TextBox)(this.FindName("TB_MsgErr")));
            this.Btn_Help = ((System.Windows.Controls.Button)(this.FindName("Btn_Help")));
        }
    }
}

