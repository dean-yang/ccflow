﻿#pragma checksum "C:\ccflow\CCFlowWebDesigner\Designer\FlowNodeMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D9493DF28A51AFA002424AD57A6593FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Liquid;
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
        
        internal System.Windows.Controls.StackPanel spContentMenu;
        
        internal Liquid.Menu MuContentMenu;
        
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
            this.spContentMenu = ((System.Windows.Controls.StackPanel)(this.FindName("spContentMenu")));
            this.MuContentMenu = ((Liquid.Menu)(this.FindName("MuContentMenu")));
        }
    }
}

