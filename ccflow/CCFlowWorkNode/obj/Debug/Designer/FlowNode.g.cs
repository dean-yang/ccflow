﻿#pragma checksum "C:\Users\imzrh\Desktop\ccflow\ccflow\CCFlowWebDesigner\Designer\FlowNode.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "12415E49B634AFAFC52271BA33D83079"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Ccflow.Web.UI.Control.Workflow.Designer;
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
    
    
    public partial class FlowNode : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Canvas container;
        
        internal System.Windows.Controls.ToolTip ttFlowNodeTip;
        
        internal System.Windows.Media.Animation.Storyboard sbDisplay;
        
        internal System.Windows.Media.Animation.Storyboard sbClose;
        
        internal System.Windows.Shapes.Ellipse eiCenterEllipse;
        
        internal Ccflow.Web.UI.Control.Workflow.Designer.FlowNodePictureContainer sdPicture;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WF;component/Designer/FlowNode.xaml", System.UriKind.Relative));
            this.container = ((System.Windows.Controls.Canvas)(this.FindName("container")));
            this.ttFlowNodeTip = ((System.Windows.Controls.ToolTip)(this.FindName("ttFlowNodeTip")));
            this.sbDisplay = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbDisplay")));
            this.sbClose = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbClose")));
            this.eiCenterEllipse = ((System.Windows.Shapes.Ellipse)(this.FindName("eiCenterEllipse")));
            this.sdPicture = ((Ccflow.Web.UI.Control.Workflow.Designer.FlowNodePictureContainer)(this.FindName("sdPicture")));
        }
    }
}

