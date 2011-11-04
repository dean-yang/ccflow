﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Browser;
using WF;
using WF.WS;
using Silverlight;
using WF.Controls;
using Liquid;
using WF.Resources;
using WF.Designer;
using System.IO;

namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    /// <summary>
    /// 设计器主页面
    /// </summary>
    public partial class Designers : UserControl
    {
        #region 全局变量

        private TreeNode firstNodeByFlow = new TreeNode();
        private System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        private string FlowTempleteUrl = "";
        private string title; // 子窗体标题       
        private WSDesignerSoapClient _service = new WSDesignerSoapClient();


        #endregion

        #region 属性

        public static readonly string CustomerId = "CCFlow";
   

        public WSDesignerSoapClient _Service
        {
            get { return _service; }
            set { _service = value; }
        }

        public string FlowID { get; set; }

        private Container SelectedContainer
        {
            get
            {
                var c = (Container) tbDesigner.SelectedContent;
                return c;
            }
        }

        public bool IsRefresh { get; set; }

        #endregion

        #region Enum
        /// <summary>
        /// 窗口打开方式 
        /// </summary>
        public enum WindowModelEnum
        {
            Dialog,
            Window
        } 
        #endregion

        #region Constructs
        /// <summary>
        /// 构造方法
        /// </summary>
        public Designers()
        {
            InitializeComponent();

            var ws = new WSDesignerSoapClient();
            ws.DoAsync("GetSettings", "CompanyId", true);
            ws.DoCompleted += ws_GetCustomerIdCompleted;
            
            bindFlowAndFlowSort();

            loadToolbar();

            // InitDesignerXml
            ws = new WSDesignerSoapClient();
            ws.DoTypeAsync("InitDesignerXml", null, null, null, null, null);
            ws.DoTypeCompleted += ws_DoTypeCompleted;

        }

        void ws_GetCustomerIdCompleted(object sender, DoCompletedEventArgs e)
        {
            var id = e.Result;
            imageLogo.Source = new BitmapImage(new Uri(string.Format("/Images/Icons/{0}.jpg", id), UriKind.Relative));


            var brush = new ImageBrush(); //定义图片画刷
            brush.ImageSource = new BitmapImage(new Uri(string.Format("/Images/Icons/{0}Welcome.jpg", id), UriKind.Relative));
            tbDesigner.Background = brush;

        }

        void ws_DoTypeCompleted(object sender, DoTypeCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);

            this.TvwSysMenu.Nodes.Clear();
            DataTable dt = ds.Tables[0];
            TreeNode liP = new TreeNode();
            foreach (DataRow dr in dt.Rows)
            {
                string no = dr["No"];
                string name = dr["Name"];
                string lab = dr["CH"];
                string w = dr["W"];
                string h = dr["H"];
                string url = dr["Url"];
                string icon = dr["Icon"];

                TreeNode li = new TreeNode();
                if (string.IsNullOrEmpty(icon) == false)
                    li.Icon = icon;

                li.Title = lab;
                li.Tag = dr;
                if (no.Length == 2)
                {
                    liP = li;
                    this.TvwSysMenu.Nodes.Add(li);
                }
                else
                {
                    li.MouseLeftButtonDown += TreeViewSysMenu_li_MouseLeftButtonDown;
                    liP.Nodes.Add(li);
                }
            }

            TvwSysMenu.ExpandAll();


            this.TvwFlowDataEnum.Nodes.Clear();
            dt = ds.Tables[1];
            liP = new TreeNode();
            foreach (DataRow dr in dt.Rows)
            {
                string no = dr["No"];
                string name = dr["Name"];
                string lab = dr["CH"];
                string w = dr["W"];
                string h = dr["H"];
                string url = dr["Url"];
                string icon = dr["Icon"];
                TreeNode li = new TreeNode();
                if (string.IsNullOrEmpty(icon) == false)
                    li.Icon = icon;

                li.Title = lab;
                li.Tag = dr;
                if (no.Length == 2)
                {
                    liP = li;
                    this.TvwFlowDataEnum.Nodes.Add(li);
                }
                else
                {
                    li.MouseLeftButtonDown += TreeViewSysMenu_li_MouseLeftButtonDown;
                    liP.Nodes.Add(li);
                }
            }

            TvwFlowDataEnum.ExpandAll();
        }

        private void TreeViewSysMenu_li_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            TreeNode tn = sender as TreeNode;
            if (tn == null)
                return;
            DataRow dr = tn.Tag as DataRow;
            string w = dr["W"];
            string h = dr["H"];
            string url = dr["Url"];
            Glo.WinOpen(Glo.BPMHost + url, "name", int.Parse(h), int.Parse(w));
        }

        private void TreeViewSysMenu_li_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            return;
        }

 
        #endregion

        #region 方法
        /// <summary>
        /// 语言设置
        /// </summary>
        private void applyContainerCulture()
        {
        }

        /// <summary>
        /// 语言设置
        /// </summary>
        public void ApplyCulture()
        {
            applyContainerCulture();

            //siFlowNodeSetting.ApplyCulture();
            //siDirectionSetting.ApplyCulture();
        }

        #region Flow CRUD related

        /// <summary>
        /// 打开工作流
        /// </summary>
        /// <param name="flowSortId"></param>
        /// <param name="flowId"></param>
        /// <param name="flowName"></param>
        private void OpenFlow(string flowSortId, string flowId, string flowName)
        {
            OpenFlow(flowId, flowName);
        }

        /// <summary>
        /// 打开工作流
        /// </summary>
        /// <param name="flowid">工作流ID</param>
        /// <param name="title">工作流名称</param>
        private void OpenFlow(string flowid, string title)
        {
            foreach (TabItem t in tbDesigner.Items)
            {
                var ct = t.Content as Container;
                if (ct != null && ct.FlowID == flowid)
                {
                    tbDesigner.SelectedItem = t;
                    return;
                }
            }
            var c = new Container();
            c.Designer = this;
            c.FlowID = flowid;
            c.getFlows();

            var ti = new TabItemEx();
            ti.Content = c;
            Button btnClose = new Button();
            btnClose.Opacity = 0.2;
            btnClose.Content = "╳";
            btnClose.MinHeight = 15;
            btnClose.MinWidth = 17;
            btnClose.Click += new RoutedEventHandler(btnClose_Click);
            btnClose.ClickMode = ClickMode.Release;
            TextBlock tbx = new TextBlock();
            tbx.Text = title;
            tbx.Width = title.Length;
            Canvas cs = new Canvas();
            cs.Children.Add(btnClose);
            cs.Children.Add(tbx);
            cs.Height = 20;
            ti.Title = title;
            ti.Width = tbx.ActualWidth + btnClose.ActualWidth + 30;
            ti.Header = cs;
            btnClose.DataContext = ti;
            btnClose.SetValue(Canvas.LeftProperty, ti.Width - btnClose.ActualWidth - 20);
            btnClose.SetValue(Canvas.TopProperty, 0.0);
            tbx.SetValue(Canvas.TopProperty, btnClose.ActualHeight);
            tbx.SetValue(Canvas.LeftProperty, 0.0);
            ti.SetValue(TabControl.WidthProperty, tbx.ActualWidth + btnClose.ActualWidth + 40);

            ti.VerticalAlignment = VerticalAlignment.Top;
            ti.VerticalContentAlignment = VerticalAlignment.Top;

            tbDesigner.Items.Add(ti);
            tbDesigner.SelectedItem = ti;
        }

        /// <summary>
        /// 删除工作流
        /// </summary>
        public void DeleteFlow(string flowid)
        {
            if (HtmlPage.Window.Confirm(Text.DeleteFlow))
            {
                _Service.DoAsync("DelFlow", flowid, true);

                _Service.DoCompleted += Server_DoCompletedToRefreshSortTree;


            }
        }

        void Server_DoCompletedToRefreshSortTree(object sender, DoCompletedEventArgs e)
        {

            _Service.DoCompleted -= Server_DoCompletedToRefreshSortTree;

            foreach (TabItem t in tbDesigner.Items)
            {
                Container ct = t.Content as Container;
                if (ct.FlowID == TvwFlow.Selected.Name)
                {
                    tbDesigner.Items.Remove(t);
                    break;
                }
            }
            bindFlowAndFlowSort();
        }

        /// <summary>
        /// 删除工作流类别
        /// </summary>
        /// <param name="flowsortid">工作流类别ID</param>
        public void DeleteFlowSort(string flowsortid)
        {
            if (HtmlPage.Window.Confirm(Text.DeleteFlowSort))
            {
                _Service.DoAsync("DelFlowSort", flowsortid, true);
                _Service.DoCompleted += Server_DoCompletedToRefreshSortTree;
            }
        }
        /// <summary>
        /// 新建工作流
        /// </summary>
        public void NewFlow(string flowSortId, string flowName)
        {
            _Service.DoAsync("NewFlow", flowSortId + "," + flowName, true);
            loadingWin(true);
            _Service.DoCompleted += _service_DoCompleted;
        }
        #endregion

        #region Window or Dialog open related
        /// <summary>
        /// 设置打开网页窗口的属性
        /// </summary>
        /// <param name="lang">语言</param>
        /// <param name="dotype">窗口类型</param>
        /// <param name="fk_flow">工作流ID</param>
        /// <param name="node1">结点1</param>
        /// <param name="node2">结点2</param>
        public void SetProper(string lang, string dotype, string fk_flow, string node1, string node2)
        {
            _Service.GetRelativeUrlAsync(lang, dotype, fk_flow, node1, node2, true);
            _Service.GetRelativeUrlCompleted += _Service_GetRelativeUrlCompleted;
        }

        public void OpenDialog(string url, string title, int h, int w)
        {
            OpenWindowOrDialog(url, title, string.Format("dialogHeight:{0}px;dialogWidth:{1}px", h, w), WindowModelEnum.Dialog);
        }

        public void OpenWindow(string url, string title, int h, int w)
        {
            OpenWindowOrDialog(url, title, string.Format("height={0},width={1}", h, w), WindowModelEnum.Window);
        }

        public void OpenDialog(string url, string title)
        {
            OpenWindowOrDialog(url, title, "dialogHeight:600px;dialogWidth:800px", WindowModelEnum.Dialog);
        }

        /// <summary>
        /// 弹出网页窗口
        /// </summary>
        /// <param name="url">网页地址</param>
        private void OpenWindowOrDialog(string url, string title, string property, WindowModelEnum windowModel)
        {
            if (WindowModelEnum.Dialog == windowModel )
            {
                HtmlPage.Window.Eval(
                    string.Format(
                        "window.showModalDialog('{0}',window,'dialogHeight:600px;dialogWidth:800px;help:no;scroll:auto;resizable:yes;status:no;');",
                        url));

            }
            else
            {
                HtmlPage.Window.Eval(
                    string.Format(
                        "window.open('{0}','{1}','{2};help=no,resizable=yes,status=no,scrollbars=1');", url,
                        title, property));


            }

            
        } 
        #endregion

        private void releaseToFtp()
        {
            if (SelectedContainer == null)
                return;
            var result = SnapshotCapturer.SaveScreenToString();

            _service = new WSDesignerSoapClient();
            var sortId = SelectedContainer.FK_Flow;
            _Service.DoAsync("ReleaseToFTP", SelectedContainer.FlowID + "," +  result, true);
            _Service.DoCompleted += _service_ReleaseToFTPCompleted;


        }
        
        private void _service_ReleaseToFTPCompleted(object sender, DoCompletedEventArgs e)
        {
            _Service.DoCompleted -= _service_GetFlowsCompleted;


        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleClick_Timer(object sender, EventArgs e)
        {
            _doubleClickTimer.Stop();
        }
       
        /// <summary>
        /// 绑定工作流树
        /// </summary>
        private void bindFlowAndFlowSort()
        {
            _service = new WSDesignerSoapClient();
            _Service.DoAsync("GetFlows", string.Empty, true);
            _Service.DoCompleted += _service_GetFlowsCompleted;
         }
       
        /// <summary>
        /// 获取工作流类型事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _service_GetFlowsCompleted(object sender, DoCompletedEventArgs e)
        {
            DataSet ds = null;
            try
            {
                firstNodeByFlow = new TreeNode();
                TvwFlow.Clear();
                ds = new DataSet();
                ds.FromXml(e.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载流程树时发生了错误,请检查数据库连接信息以及IIS WebService配置错误信息.", "错误", MessageBoxButton.OK);
                return;
            }
           
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode node = new TreeNode();
                node.Title = dr["Name"].ToString();
                node.ID = dr["No"].ToString();
                node.IsFlowSort = true;
                node.Icon = "../Images/MenuItem/FlowSort.png";

                firstNodeByFlow.Nodes.Add(node);
                TvwFlow.Nodes.Add(node);
            }

            foreach (DataRow d in ds.Tables[1].Rows)
            {
                TreeNode node = new TreeNode();
                node.Title = d["Name"].ToString();
                node.ID = d["FK_FlowSort"].ToString();
                node.Name = d["No"].ToString();
                node.IsFlowSort = false;
                if (SelectedContainer != null)
                {
                    if (SelectedContainer.FlowID == node.Name)
                    {
                        TabItemEx te = this.tbDesigner.SelectedItem as TabItemEx;
                        te.Title = node.Title;
                        Canvas cs = te.Header as Canvas;
                        TextBlock tbx = cs.Children[1] as TextBlock;
                        tbx.Text = node.Title;
                    }
                }
                foreach (TreeNode ne in firstNodeByFlow.Nodes)
                {
                    try
                    {
                        if (ne.ID == d["FK_FlowSort"].ToString())
                        {
                            ne.Nodes.Add(node);
                            ne.IsFlowSort = true;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            _Service.DoCompleted -= _service_GetFlowsCompleted;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuFlowTree_ItemSelected(object sender, MenuEventArgs e)
        {
            if (e.Tag == null)
                return;

            switch (e.Tag.ToString())
            {
                case "menuExp":
                    FrmExp exp = new FrmExp();
                    exp.Show();
                    break;
                case "Help":
                    Glo.WinOpen("http://ccflow.org/Help.aspx?wd=设计器", "帮助", 900, 1200);
                    break;
                case "OpenFlow":
                     OpenFlow(TvwFlow.Selected.ID, TvwFlow.Selected.Name, TvwFlow.Selected.Title);
                     break;
                case "NewFlow_Blank":
                     NewFlowHandler(0);
                    break;
                case "NewFlow_Disk":
                    NewFlowHandler(1);
                    break;
                case "NewFlow_CCF":
                    NewFlowHandler(2);
                    break;
                case "NewFlowSort":
                    NewFlowSort newFlowSort = new NewFlowSort(this);
                    newFlowSort.DisplayType = NewFlowSort.DisplayTypeEnum.Add;
                    newFlowSort.ServiceDoCompletedEvent += AddEditFlowSortDoCompletedEventHandler;
                    newFlowSort.Show();
                    break;
                case "Delete":
                    var deleteFlowNode = TvwFlow.Selected as TreeNode;
                    if(null == deleteFlowNode)
                        break;
                    if (!deleteFlowNode.IsFlowSort)
                    {
                        DeleteFlow(TvwFlow.Selected.Name);

                        foreach (TabItem t in tbDesigner.Items)
                        {
                            var ct = t.Content as Container;
                            if (null != ct && ct.FlowID == TvwFlow.Selected.Name)
                            {
                                tbDesigner.Items.Remove(t);
                                break;
                            }
                        }
                        
                    }
                    else
                    {
                        DeleteFlowSort(deleteFlowNode.ID);
                    }
                    break;
                case "Refresh":
                    bindFlowAndFlowSort();
                    break;
                case "Edit":
                    var editFlowSort = new NewFlowSort(this);
                    editFlowSort.InitControl(TvwFlow.Selected.ID, TvwFlow.Selected.EditedTitle);
                    editFlowSort.DisplayType = NewFlowSort.DisplayTypeEnum.Edit;
                    editFlowSort.ServiceDoCompletedEvent += AddEditFlowSortDoCompletedEventHandler;
                    editFlowSort.Show();
                    break;
            }

            MuFlowTree.Hide();
        }

        /// <summary>
        /// 添加流程类别关闭时要执行的动作，一般来说是刷新窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddEditFlowSortDoCompletedEventHandler(object sender, EventArgs e)
        {
            var add = (NewFlowSort)sender;

            if (add.DialogResult == true)
            {
                bindFlowAndFlowSort();
            }
        }

        /// <summary>
        /// 键盘按键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// 键盘按键释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// 在工作流树空白处按下鼠标左键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CvsFlowTree_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MuFlowTree.Hide();
        }

        /// <summary>
        /// 工作流树被选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvwFlow_SelectionChanged(object sender, TreeEventArgs e)
        {
            MuFlowTree.Hide();
        }

        /// <summary>
        /// 右击工作流树事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvwFlow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point position = e.GetPosition(TvwFlow);
            TreeNode node = TvwFlow.GetNodeAtPoint(position) as TreeNode;
            if (node != null)
            {
                Point MuFlowTreePos = TvwFlow.TransformToVisual(TvwFlow).Transform(position);
                node.IsSelected = true;
                TvwFlow.ClearSelected();
                TvwFlow.SetSelected(node);

                // 调整x,y 值 ，以防止菜单被遮盖住
                var x = MuFlowTreePos.X;
                var y = MuFlowTreePos.Y;
                var menuHeight = 250;
                var menuWidth = 170;
                if (x + menuWidth > 220)
                {
                    x = x - (x + menuWidth - 220);
                }
                if (y + menuHeight > Application.Current.Host.Content.ActualHeight)
                {
                    y = y - (y + menuHeight - Application.Current.Host.Content.ActualHeight);
                }
                MuFlowTree.SetValue(Canvas.LeftProperty, x);
                MuFlowTree.SetValue(Canvas.TopProperty, y);
                MuFlowTree.Show();
                if (node.isRoot)
                {
                    MuFlowTree.Get("OpenFlow").IsEnabled = false;
                    //MuFlowTree.Get("NewFlow").IsEnabled = false;
                    MuFlowTree.Get("Delete").IsEnabled = false;
                    MuFlowTree.Get("Edit").IsEnabled = false;
                }
                else
                {
                    MuFlowTree.Get("OpenFlow").IsEnabled = true;
                  //  MuFlowTree.Get("NewFlow").IsEnabled = true;
                    MuFlowTree.Get("Delete").IsEnabled = true;
                    MuFlowTree.Get("Edit").IsEnabled = true;
                }
                if (node.IsFlowSort)
                {
                    // 只有节点的兄弟数大于1时允许删除
                    if (node.ParentNode.Nodes.Count > 1)
                    {
                        MuFlowTree.Get("Delete").IsEnabled = true;
                    }
                    else
                    {
                        MuFlowTree.Get("Delete").IsEnabled = false;
                    }
                    MuFlowTree.Get("OpenFlow").IsEnabled = false;
                    MuFlowTree.Get("Edit").IsEnabled = true;
                }
                else
                {
                    MuFlowTree.Get("Edit").IsEnabled = false;
                    MuFlowTree.Get("OpenFlow").IsEnabled = true;
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// 双击工作流树事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvwFlow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_doubleClickTimer.IsEnabled)
            {
                _doubleClickTimer.Stop();


                // 如果双击的是流程类型结点就返回，什么也不做
                if( null == TvwFlow.Selected || ((TreeNode)TvwFlow.Selected).IsFlowSort)
                {
                    return;
                }
                OpenFlow(TvwFlow.Selected.Name, TvwFlow.Selected.Title);
            }
            else
            {
                _doubleClickTimer.Start();
            }
        }
       
        /// <summary>
        /// 报表设计事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDesignerTable_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedContainer != null)
            {
                SelectedContainer.btnDesignerTable();
            }
        }
       
        private void _service_FlowTemplete_GenerCompleted(object sender, FlowTemplete_GenerCompletedEventArgs e)
        {
            try
            {
                FlowTempleteUrl = e.Result;
                _service.GetRelativeUrlCompleted += getUploadedFileUriCompleted;
                _service.GetRelativeUrlAsync(string.Empty, "FileHandler", string.Empty, string.Empty, string.Empty, true);
            }
            catch
            {
                MessageBox.Show("生成失败！");
            }
            _service.FlowTemplete_GenerCompleted -= _service_FlowTemplete_GenerCompleted;
        }

        private void getUploadedFileUriCompleted(object sender, GetRelativeUrlCompletedEventArgs e)
        {

            string suburl = HtmlPage.Document.DocumentUri.ToString();
            string url = suburl.Substring(0, suburl.LastIndexOf('/'));
            url += e.Result;
            var address = new Uri(String.Format("{1}?filePath={0}", FlowTempleteUrl, url), UriKind.RelativeOrAbsolute);
            HtmlPage.Window.Navigate(address);
            _service.GetRelativeUrlCompleted -= getUploadedFileUriCompleted;
        }
        
        public void NewFlowHandler(int tabIdx)
        {
            FrmNewFlow fu = new FrmNewFlow();
            fu.CurrentDesinger = this;
            fu.tabControl.TabIndex = tabIdx;

            if (TvwFlow.Selected != null &&  ((TreeNode)TvwFlow.Selected).IsFlowSort)
            {
                fu.CurrentFlowSortName = TvwFlow.Selected.Title;

            }

            fu.FlowTempleteLoadCompeletedEventHandler += (eSender, eArgs) =>
            {
                try
                {
                    var result = eArgs.Result.Split(',');
                    // 返回值的格式为FlowSortID,FlowId,FlowName  
                    if (3 != result.Length)
                    {
                        MessageBox.Show(eArgs.Result, "错误", MessageBoxButton.OK);
                        return;
                    }

                    bindFlowAndFlowSort();
                    OpenFlow(result[0], result[1], result[2]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK);
                }
            };
            fu.Show();
        }
        /// <summary>
        /// 关闭选项卡事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedContainer != null)
            {
                if (SelectedContainer.IsNeedSave)
                {
                    if(HtmlPage.Window.Confirm(Text.IsSave))
                    {
                        if (SelectedContainer.Save())
                        {
                            this.tbDesigner.Items.Remove(this.tbDesigner.SelectedItem);
                        }
                    }
                    else
                    {
                        tbDesigner.Items.Remove(this.tbDesigner.SelectedItem);
                    }
                }
                else
                {
                    tbDesigner.Items.Remove(this.tbDesigner.SelectedItem);

                }
            }
        }

        /// <summary>
        /// 执行新建工作流事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _service_DoCompleted(object sender, DoCompletedEventArgs e)
        {
            loadingWin(false);
            if(e.Result.IndexOf(";") < 0)
            {
                MessageBox.Show(e.Result, "错误", MessageBoxButton.OK);
                return;
            }
            string[] flow = e.Result.Split(';');
            FlowID = flow[0];
            _Service.DoCompleted -= _service_DoCompleted;
            bindFlowAndFlowSort();
            OpenFlow(flow[0], flow[1]);
     }

        /// <summary>
        /// 弹出网页窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Service_GetRelativeUrlCompleted(object sender, GetRelativeUrlCompletedEventArgs e)
        {
            string suburl = HtmlPage.Document.DocumentUri.ToString();
            string url = suburl.Substring(0, suburl.LastIndexOf('/'));
            OpenDialog(url + e.Result, title);

            _Service.GetRelativeUrlCompleted -= _Service_GetRelativeUrlCompleted;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            HtmlPage.RegisterScriptableObject("Designer", this);

            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, SystemConst.DoubleClickTime);
            _doubleClickTimer.Tick += DoubleClick_Timer;
            ApplyCulture();
            try
            {
                LayoutRoot.Height = Application.Current.Host.Content.ActualHeight;
                TbcFDS.Height = LayoutRoot.Height - 75;
                TvwFlow.Height = Application.Current.Host.Content.ActualHeight - 35 - 100;
                tbDesigner.Height = Application.Current.Host.Content.ActualHeight - 35;
                tbDesigner.Width = Application.Current.Host.Content.ActualWidth - 227;
            }
            catch
            {
            }
            Application.Current.Host.Content.FullScreenChanged += Content_FullScreenChanged;
        }

        /// <summary>
        /// Asp.net网页关闭时要执行的事件
        /// </summary>
        [ScriptableMember]
        public void OnAspNetPageClosed(string type, string para)
        {
            switch (type)
            {
                case "FlowNodeProperty":
                    var paras = para.Split(','); //id,namevalue
                    
                    foreach (TabItem t in tbDesigner.Items)
                    {
                        Container ct = t.Content as Container;

                        foreach (var flowNode in ct.FlowNodeCollections)
                        {
                            if(paras[0] == flowNode.FlowNodeID && paras[0] != flowNode.FlowNodeName)
                            {
                                ct.IsNeedSave = true;
                                flowNode.FlowNodeName = paras[1];
                                break;
                            }
                        }
                    }

                    break;

                case "FlowProperty":
                    paras = para.Split(','); //id,namevalue
                    var oldValue = string.Empty;
                    int index = 0;
                     
                    foreach (TabItem t in tbDesigner.Items)
                    {
                        Container ct = t.Content as Container;
                        oldValue = (t as TabItemEx).Title;
                        if(paras[0] == ct.FlowID && paras[1] != oldValue)
                        {
                            var tabItem = tbDesigner.Items[index] as TabItemEx;
                            Button btnClose = new Button();
                            btnClose.Opacity = 0.2;
                            btnClose.Content = "╳";
                            btnClose.MinHeight = 15;
                            btnClose.MinWidth = 17;
                            btnClose.Click += new RoutedEventHandler(btnClose_Click);
                            btnClose.ClickMode = ClickMode.Release;
                            TextBlock tbx = new TextBlock();
                            tbx.Text = paras[1];
                            tbx.Width = paras[1].Length;
                            Canvas cs = new Canvas();
                            cs.Children.Add(btnClose);
                            cs.Children.Add(tbx);
                            cs.Height = 20;
                            tabItem.Title = paras[1];
                            tabItem.Width = tbx.ActualWidth + btnClose.ActualWidth + 30;
                            tabItem.Header = cs;
                            btnClose.DataContext = tabItem;
                            btnClose.SetValue(Canvas.LeftProperty, tabItem.Width - btnClose.ActualWidth - 20);
                            btnClose.SetValue(Canvas.TopProperty, 0.0);
                            tbx.SetValue(Canvas.TopProperty, btnClose.ActualHeight);
                            tbx.SetValue(Canvas.LeftProperty, 0.0);
                            tabItem.SetValue(TabControl.WidthProperty, tbx.ActualWidth + btnClose.ActualWidth + 40);

                            tabItem.VerticalAlignment = VerticalAlignment.Top;
                            tabItem.VerticalContentAlignment = VerticalAlignment.Top;
                            break;

                        }
                        index ++;
                    }

                    bindFlowAndFlowSort();

                    break;

            }
           
        }

        private void Content_FullScreenChanged(object sender, EventArgs e)
        {
            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, SystemConst.DoubleClickTime);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
            ApplyCulture();
            try
            {
                LayoutRoot.Height = Application.Current.Host.Content.ActualHeight;
                TbcFDS.Height = LayoutRoot.Height - 75;
                TvwFlow.Height = Application.Current.Host.Content.ActualHeight - 35 - 100;
                TvwFlowDataEnum.Height = TvwSysMenu.Height = TvwFlow.Height;

                tbDesigner.Height = Application.Current.Host.Content.ActualHeight - 35;
                tbDesigner.Width = Application.Current.Host.Content.ActualWidth - 227;
            }
            catch
            {
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MuFlowTree.Hide();
        }

        private void TbcFDS_MouseLeave(object sender, MouseEventArgs e)
        {
            MuFlowTree.Hide();
        }

        #region Toolbar related
        private void loadToolbar()
        {
            #region 生成toolbar .
            List<ToolbarItem> ens = new List<ToolbarItem>();
            ens = ToolbarItem.Instance.GetLists();
            foreach (ToolbarItem en in ens)
            {
                ToolbarButton btn = new ToolbarButton();
                btn.Name = "Btn_" + en.No;
                btn.Click += new RoutedEventHandler(ToolBar_Click);

                StackPanel mysp = new StackPanel();
                mysp.Orientation = Orientation.Horizontal;
                mysp.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                mysp.Name = "sp" + en.No;

                Image img = new Image();
                BitmapImage png = new BitmapImage(new Uri("/Images/" + en.No + ".png", UriKind.Relative));
                img.Source = png;
                mysp.Children.Add(img);

                TextBlock tb = new TextBlock();
                tb.Name = "tbT" + en.No;
                tb.Text = en.Name;
                tb.FontSize = 12;
                mysp.Children.Add(tb);

                btn.Content = mysp;
                this.toolbar1.AddBtn(btn);
            }
            #endregion 生成toolbar .

        }

        private void ToolBar_Click(object sender, RoutedEventArgs e)
        {
            var control = sender as ToolbarButton;
            if (null == control)
            {
                return;
            }

            switch (control.Name)
            {
                case "Btn_ToolBarNewNode":
                    if (SelectedContainer != null)
                    {
                        SelectedContainer.AddFlowNode();
                        SelectedContainer.IsNeedSave = true;
                    }
                    break;
                case "Btn_ToolBarNewLine": // 添加线事件.
                    if (SelectedContainer != null)
                    {
                        SelectedContainer.AddDirection();
                        SelectedContainer.IsNeedSave = true;
                    }
                    break;
                case "Btn_ToolBarNewLabel":
                    if (null != SelectedContainer)
                    {
                        SelectedContainer.AddLabel(10,10);
                        SelectedContainer.IsNeedSave = true;
                    }
                    break;
                case "Btn_ToolBarSave":
                    if (SelectedContainer != null)
                    {
                        var passed = SelectedContainer.Save();

                        if (passed)
                        {
                            SelectedContainer.IsNeedSave = false;
                        }
                    }
                    break;
                case "Btn_ToolBarDesignReport":
                    btnDesignerTable_Click(sender, e);
                    break;
                case "Btn_ToolBarCheck":
                    if (SelectedContainer != null)
                        SelectedContainer.btnCheck();
                    break;
                case "Btn_ToolBarRun":
                    if (SelectedContainer != null)
                        SelectedContainer.btnRun();
                    break;
                case "Btn_ToolBarEditFlow":
                    if (SelectedContainer != null)
                        SelectedContainer.Edit();
                    break;
                case "Btn_ToolBarDeleteFlow":
                    if (SelectedContainer != null)
                        DeleteFlow(SelectedContainer.FlowID);
                    break;
                case "Btn_ToolBarHelp":
                    Glo.WinOpen("http://ccflow.org/Help.aspx","Help", 1000, 1200);
                    break;
                case "Btn_ToolBarGenerateModel":
                    if (SelectedContainer != null)
                    {
                        _Service.FlowTemplete_GenerAsync(SelectedContainer.FlowID, true);
                        _service.FlowTemplete_GenerCompleted += _service_FlowTemplete_GenerCompleted;
                    }
                    break;
                case "Btn_ToolBarLoadModel":
                    NewFlowHandler(0);
                    break;
                case "Btn_ToolBarReleaseToFTP":
                    releaseToFtp();
                    break;
            }
        } 
        #endregion

        /// <summary>
        /// 当前流程变化时，确保只有当前流程才显示关闭按钮，这样可以避免“当前流程是A，然后点击B的关闭按钮
        /// 表示用户想关闭B，但关闭的是A”的情况。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDesigner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (TabItem t in tbDesigner.Items)
            {
                if (t.IsSelected)
                {
                    var canvas = t.Header as Canvas;
                    if (canvas != null)
                    {
                        canvas.Children[0].Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    var canvas = t.Header as Canvas;
                    if (canvas != null)
                    {
                        canvas.Children[0].Visibility = Visibility.Collapsed;
                    }
                }

            }

        }

        /// <summary>
        ///  diable the default silverlight rightmenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region 进度指示
        LoadingWindow cwLoading = new LoadingWindow();
        /// <summary>
        /// 显示Loadingp窗体
        /// </summary>
        private void loadingWin(bool showIt)
        {
            if (!showIt)
            {
                cwLoading.Close();
                return;
            }
            cwLoading.Show();
        } 
        #endregion

       

    }
}