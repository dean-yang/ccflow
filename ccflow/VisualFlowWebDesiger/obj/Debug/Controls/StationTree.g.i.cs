﻿#pragma checksum "D:\ccflow\VisualFlowWebDesigner\Controls\StationTree.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2FB6B0CA4B7D0EA6C541C4C0623FE967"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.225
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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


namespace WF.Controls {
    
    
    public partial class StationTree : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Canvas CvStation;
        
        internal Liquid.Tree TvwStation;
        
        internal Liquid.Menu MuStation;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WF;component/Controls/StationTree.xaml", System.UriKind.Relative));
            this.CvStation = ((System.Windows.Controls.Canvas)(this.FindName("CvStation")));
            this.TvwStation = ((Liquid.Tree)(this.FindName("TvwStation")));
            this.MuStation = ((Liquid.Menu)(this.FindName("MuStation")));
        }
    }
}

