﻿#pragma checksum "D:\VisualWorkFlow\VisualFlowWebDesiger\Designer\FlowNodeMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BC0A66F644B5BE4F1C5EDE58C47900E0"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.225
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


namespace Ccflow.Web.UI.Control.Workflow.Designer {
    
    
    public partial class FlowNodeMenu : System.Windows.Controls.UserControl {
        
        internal System.Windows.Media.Animation.Storyboard sbShowMenu;
        
        internal System.Windows.Media.Animation.Storyboard sbCloseMenu;
        
        internal System.Windows.Controls.StackPanel spContentMenu;
        
        internal System.Windows.Controls.HyperlinkButton btnShowFlowNodeSetting;
        
        internal System.Windows.Controls.HyperlinkButton btnNodeProper;
        
        internal System.Windows.Controls.HyperlinkButton btnNodeTable;
        
        internal System.Windows.Controls.HyperlinkButton btnNodeStation;
        
        internal System.Windows.Controls.HyperlinkButton btnFlowConditions;
        
        internal System.Windows.Controls.HyperlinkButton btnDelete;
        
        internal System.Windows.Controls.HyperlinkButton btnFlowProper;
        
        internal System.Windows.Controls.HyperlinkButton btnCopy;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WF;component/Designer/FlowNodeMenu.xaml", System.UriKind.Relative));
            this.sbShowMenu = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbShowMenu")));
            this.sbCloseMenu = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbCloseMenu")));
            this.spContentMenu = ((System.Windows.Controls.StackPanel)(this.FindName("spContentMenu")));
            this.btnShowFlowNodeSetting = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnShowFlowNodeSetting")));
            this.btnNodeProper = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnNodeProper")));
            this.btnNodeTable = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnNodeTable")));
            this.btnNodeStation = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnNodeStation")));
            this.btnFlowConditions = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnFlowConditions")));
            this.btnDelete = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnDelete")));
            this.btnFlowProper = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnFlowProper")));
            this.btnCopy = ((System.Windows.Controls.HyperlinkButton)(this.FindName("btnCopy")));
        }
    }
}

