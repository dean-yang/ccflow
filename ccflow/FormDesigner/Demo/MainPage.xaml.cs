﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text;
using Demo.Controls;

namespace Demo
{
    public partial class MainPage : UserControl
    {
        #region 全局变量
        bool b = false;//在主面板上判断当前鼠标的状态是否按下
        bool be = false;//在绿点上判断当前鼠标的状态是否按下
        bool bl = false;//判断LABEL当前鼠标的状态是否按下
        bool btxt = false;
        string selectType = "";//当前工具选择类型 hand line1 line2 label txt cannel
        Line l;//当前线
        TextBlock tb;//当前标签
        TextBox txt;//当前文本框
        Ellipse e1;//选中线后出现的绿点1
        Ellipse e2;//选中线后出现的绿点2
        Ellipse eCurrent;//选中的绿点
        Grid g = new Grid();//遮罩图层
        private DateTime _lastTime;//获取双击label的时间间隔
        //删除框
        StackPanel spDel = new StackPanel();
        DropShadowEffect dse = new DropShadowEffect();
        TextBlock tbDel = new TextBlock();
        TextBlock tbDelLine = new TextBlock();
        TextBlock tbDelCannel = new TextBlock();
        UIElement ui;//要删除的对象
        #endregion

        public MainPage()
        {
            InitializeComponent();

            #region 构造
            spDel.Width = 80;
            spDel.Height = 26;
            spDel.Background = new SolidColorBrush(Colors.Brown);
            spDel.Opacity = 0.6;
            spDel.Orientation = Orientation.Horizontal;
            dse.Color = Colors.Gray;
            spDel.Effect = dse;
            tbDel.Text = "   删除 ";
            tbDel.Margin = new Thickness(0, 5, 0, 0);
            tbDelLine.Margin = new Thickness(0, 5, 0, 0);
            tbDelCannel.Margin = new Thickness(0, 5, 0, 0);
            tbDel.Foreground = new SolidColorBrush(Colors.White);
            tbDelLine.Foreground = tbDel.Foreground;
            tbDelCannel.Foreground = tbDel.Foreground;
            tbDel.Cursor = Cursors.Hand;
            tbDelLine.Text = "|";
            tbDelCannel.Text = " 取消 ";
            tbDelCannel.Cursor = Cursors.Hand;
            spDel.Children.Add(tbDel);
            spDel.Children.Add(tbDelLine);
            spDel.Children.Add(tbDelCannel);
            tbDel.MouseLeftButtonDown += new MouseButtonEventHandler(tbDelete_MouseLeftButtonDown);
            tbDelCannel.MouseLeftButtonDown += new MouseButtonEventHandler(tbDelete_MouseLeftButtonDown);

            setTools("hand");
            e1 = new Ellipse();
            e1.Tag = "e1";
            e1.Cursor = Cursors.Hand;
            e1.MouseLeftButtonDown += new MouseButtonEventHandler(e_MouseLeftButtonDown);
            e2 = new Ellipse();
            e2.Tag = "e2";
            e2.Cursor = Cursors.Hand;
            e2.MouseLeftButtonDown += new MouseButtonEventHandler(e_MouseLeftButtonDown);
            e1.Width = 9;
            e1.Height = 9;
            e1.Fill = new SolidColorBrush(Colors.Green);
            e2.Width = 9;
            e2.Height = 9;
            e2.Fill = new SolidColorBrush(Colors.Green);
            #endregion
        }

        #region 拖拽线上的绿点时间集合
        void e_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            be = true;
            eCurrent = sender as Ellipse;
            eCurrent.Fill = new SolidColorBrush(Colors.Red);
        }
        #endregion


        //鼠标单击主面板事件
        private void canvasMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            b = true;
            delPoint();
            #region 线一
            if (selectType == "line1")
            {
                l = new Line();
                l.Cursor = Cursors.Hand;
                l.StrokeThickness = 2;
                l.Stroke = new SolidColorBrush(Colors.Black);
                l.X1 = l.X2 = e.GetPosition(this.canvasMain).X;
                l.Y1 = l.Y2 = e.GetPosition(this.canvasMain).Y;
                this.canvasMain.Children.Add(l);
                setTools("hand");
            }
            #endregion
            #region 线二
            if (selectType == "line2")
            {
                l = new Line();
                l.Cursor = Cursors.Hand;
                l.StrokeThickness = 2;
                l.Stroke = new SolidColorBrush(Colors.Black);
                l.X1 = l.X2 = e.GetPosition(this.canvasMain).X;
                l.Y1 = l.Y2 = e.GetPosition(this.canvasMain).Y;
                this.canvasMain.Children.Add(l);
                
                l.MouseLeftButtonDown += (s, a) =>
                {
                    if (selectType == "hand")
                    {
                        l = s as Line;
                        a.Handled = true;
                        if (!canvasMain.Children.Contains(e1) && !canvasMain.Children.Contains(e2))
                        {
                            e1.SetValue(Canvas.LeftProperty, l.X1 - 4);
                            e1.SetValue(Canvas.TopProperty, l.Y1 - 4);
                            e2.SetValue(Canvas.LeftProperty, l.X2 - 4);
                            e2.SetValue(Canvas.TopProperty, l.Y2 - 4);
                            this.canvasMain.Children.Add(e1);
                            this.canvasMain.Children.Add(e2);
                        }
                    }
                };
                l.MouseRightButtonDown += (s, a) =>
                    {
                        a.Handled = true;
                        if (selectType == "hand")
                        {
                            if (!this.canvasMain.Children.Contains(spDel))
                            {
                                this.canvasMain.Children.Add(spDel);
                                spDel.SetValue(Canvas.LeftProperty, a.GetPosition(this.canvasMain).X);
                                spDel.SetValue(Canvas.TopProperty, a.GetPosition(this.canvasMain).Y);
                                ui = s as Line;
                            }
                        }
                    };
            }
            #endregion
            #region 标签
            if (selectType == "label")
            {
                tb = new TextBlock();
                tb.Text = "Label";
                tb.Cursor = Cursors.Hand;
                tb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                tb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                this.canvasMain.Children.Add(tb);
                canvasWin.Visibility = Visibility.Visible;
                gVisable.Visibility = Visibility.Visible;
                txtLabel.Text = "Label";
                cbSize.SelectedIndex = 4;
                cbWight.IsChecked = false;
                
                tb.MouseLeftButtonDown += (s, a) =>
                {
                    a.Handled = true;
                    bl = true;
                    tb = s as TextBlock;

                    if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300)
                    {
                        canvasWin.Visibility = Visibility.Visible;
                        gVisable.Visibility = Visibility.Visible;
                        txtLabel.Text = tb.Text;
                        foreach (ComboBoxItem cbi in cbSize.Items)
                        {
                            if (cbi.Content.ToString() == tb.FontSize.ToString())
                                cbi.IsSelected = true;
                        }
                        if (tb.FontWeight == FontWeights.Bold)
                        {
                            cbWight.IsChecked = true;
                        }
                        else
                        {
                            cbWight.IsChecked = false;
                        }
                    }
                    // reset the time 
                    _lastTime = DateTime.Now;

                };
                tb.MouseRightButtonDown += (s, a) =>
                    {
                        a.Handled = true;
                        if (selectType == "hand")
                        {
                            if (!this.canvasMain.Children.Contains(spDel))
                            {
                                this.canvasMain.Children.Add(spDel);
                                spDel.SetValue(Canvas.LeftProperty, a.GetPosition(this.canvasMain).X);
                                spDel.SetValue(Canvas.TopProperty, a.GetPosition(this.canvasMain).Y);
                                ui = s as TextBlock;
                            }
                        }
                    };
            }
            #endregion
            #region 文本
            if (selectType == "txt")
            {
                txt = new TextBox();
                txt.Width = 100;
                txt.Height = 23;
                txt.Cursor = Cursors.Hand;
                txt.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                txt.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                this.canvasMain.Children.Add(txt);

                canvasWinTxt.Visibility = Visibility.Visible;
                gVisable.Visibility = Visibility.Visible;
                txtWidth.Text = "100";
                txtHeight.Text = "20";
                

                #region 左键点击
                txt.MouseLeftButtonDown += (s, a) =>
                {
                    a.Handled = true;
                    btxt = true;
                    txt = s as TextBox;

                    if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300)
                    {
                        canvasWinTxt.Visibility = Visibility.Visible;
                        gVisable.Visibility = Visibility.Visible;
                        txtWidth.Text = txt.Width.ToString();
                        txtHeight.Text = txt.Height.ToString();
                    }
                    // reset the time 
                    _lastTime = DateTime.Now;
                };
                #endregion
                #region 右键点击
                txt.MouseRightButtonDown += (s, a) =>
                {
                    a.Handled = true;
                    if (selectType == "hand")
                    {
                        if (!this.canvasMain.Children.Contains(spDel))
                        {
                            this.canvasMain.Children.Add(spDel);
                            spDel.SetValue(Canvas.LeftProperty, a.GetPosition(this.canvasMain).X);
                            spDel.SetValue(Canvas.TopProperty, a.GetPosition(this.canvasMain).Y);
                            ui = s as TextBox;
                        }
                    }
                };
                #endregion
                #region  键盘点击
                txt.KeyDown += (s, a) =>
                    {
                        if (a.Key == Key.Down)
                        {

                        }
                    };
                #endregion
            }
            #endregion
        }

        //鼠标松开主面板事件
        private void canvasMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            b = false;
            be = false;
            bl = false;
            btxt = false;
            setTools("hand");
            if (eCurrent != null)
                eCurrent.Fill = new SolidColorBrush(Colors.Green);
        }

        //鼠标在主面板上移动事件
        private void canvasMain_MouseMove(object sender, MouseEventArgs e)
        {
            #region 画线线
            if (b)
            {
                #region 线一
                if (selectType == "line1")
                {
                    l.X2 = e.GetPosition(this.canvasMain).X;
                    l.Y2 = e.GetPosition(this.canvasMain).Y;
                }
                #endregion
                #region 线二
                else if (selectType == "line2")
                {
                    double x = e.GetPosition(this.canvasMain).X - l.X1;
                    double y = e.GetPosition(this.canvasMain).Y - l.Y1;

                    if (Math.Abs(x) > Math.Abs(y))
                    {
                        l.X2 = e.GetPosition(this.canvasMain).X;
                        l.Y2 = l.Y1;
                    }
                    else
                    {
                        l.X2 = l.X1;
                        l.Y2 = e.GetPosition(this.canvasMain).Y;
                    }
                }
                #endregion
            }
            #endregion
            if (selectType == "hand")
            {
                #region 改变线的长度
                if (be)
                {
                    if (eCurrent.Tag.ToString() == "e1")
                    {
                        double x = e.GetPosition(this.canvasMain).X - l.X2;
                        double y = e.GetPosition(this.canvasMain).Y - l.Y2;
                        if (Math.Abs(x) > Math.Abs(y))
                        {
                            l.X1 = e.GetPosition(this.canvasMain).X;
                            l.Y1 = l.Y2;
                            eCurrent.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X - 4);
                            eCurrent.SetValue(Canvas.TopProperty, l.Y2 - 4);
                        }
                        else
                        {
                            l.X1 = l.X2;
                            l.Y1 = e.GetPosition(this.canvasMain).Y;
                            eCurrent.SetValue(Canvas.LeftProperty, l.X2 - 4);
                            eCurrent.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y - 4);
                        }
                    }
                    else //if (eCurrent.Tag.ToString() == "e2")
                    {
                        double x = e.GetPosition(this.canvasMain).X - l.X1;
                        double y = e.GetPosition(this.canvasMain).Y - l.Y1;
                        if (Math.Abs(x) > Math.Abs(y))
                        {
                            l.X2 = e.GetPosition(this.canvasMain).X;
                            l.Y2 = l.Y1;
                            eCurrent.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X - 4);
                            eCurrent.SetValue(Canvas.TopProperty, l.Y1 - 4);
                        }
                        else
                        {
                            l.X2 = l.X1;
                            l.Y2 = e.GetPosition(this.canvasMain).Y;
                            eCurrent.SetValue(Canvas.LeftProperty, l.X1 - 4);
                            eCurrent.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y - 4);
                        }
                    }

                }
                #endregion
                #region 拖拉label
                if (bl)
                {
                    tb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    tb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                }
                #endregion
                #region 拖拉txt
                if (btxt)
                {
                    txt.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    txt.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                }
                #endregion
            }
        }

        //工具选择触发事件
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.Name == "tbHand")
            {
                setTools("hand");
            }
            else if (tb.Name == "tbLine")
            {
                setTools("line1");
            }
            else if (tb.Name == "tbLine2")
            {
                setTools("line2");
            }
            else if (tb.Name == "tbLabel")
            {
                setTools("label");
            }
            else if (tb.Name == "tbTextBox")
            {
                setTools("txt");
            }
            else if (tb.Name == "tbCannel")
            {
                setTools("cannel");

            }
        }

        //标签赋值 X 事件
        private void tbClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasWin.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;
        }

        //标签赋值确定事件
        private void tbSure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb.Text = txtLabel.Text;
            tb.FontSize = double.Parse(cbSize.SelectionBoxItem.ToString());
            if (cbWight.IsChecked == true)
                tb.FontWeight = FontWeights.Bold;
            else
                tb.FontWeight = FontWeights.Normal;
            canvasWin.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;
        }

        //文本框长度赋值事件
        private void tbSureTxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txtWidth.Text == "" || txtHeight.Text == "")
            {
                MessageBox.Show("请输入要设置的长度！");
                return;
            }
            double i = 0;
            double j = 0;
            if (!double.TryParse(txtWidth.Text, out i))
            {
                MessageBox.Show("请输入整型数字！");
                return;
            }
            if (!double.TryParse(txtHeight.Text, out j))
            {
                MessageBox.Show("请输入整型数字！");
                return;
            }
            if (i > 1000 || j > 500)
            {
                MessageBox.Show("请输入1000之内的数字！");
                return;
            }
            txt.Width = double.Parse(txtWidth.Text);
            txt.Height = double.Parse(txtHeight.Text);
            canvasWinTxt.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;

        }
        //文本框 X 事件
        private void tbCloseTxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasWinTxt.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;
        }

        //删除操作
        private void tbDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.Text == "   删除 ")
            {
                if (this.canvasMain.Children.Contains(ui))
                    this.canvasMain.Children.Remove(ui);
            }
            if (this.canvasMain.Children.Contains(spDel))
                this.canvasMain.Children.Remove(spDel);
        }

        #region 私有方法

        private void setTools(string select)
        {
            selectType = select;
            tbHand.Foreground = new SolidColorBrush(Colors.White);
            tbLine.Foreground = new SolidColorBrush(Colors.White);
            tbLabel.Foreground = new SolidColorBrush(Colors.White);
            tbLine2.Foreground = new SolidColorBrush(Colors.White);
            tbTextBox.Foreground = new SolidColorBrush(Colors.White);
            tbCannel.Foreground = new SolidColorBrush(Colors.White);
            if (select == "hand")
                //tbHand.FontWeight = FontWeights.Bold;
                tbHand.Foreground = new SolidColorBrush(Colors.Cyan); 
            else if (select == "line1")
                tbLine.Foreground = new SolidColorBrush(Colors.Cyan);
            else if (select == "line2")
                tbLine2.Foreground = new SolidColorBrush(Colors.Cyan);
            else if (select == "label")
                tbLabel.Foreground = new SolidColorBrush(Colors.Cyan);
            else if (select == "txt")
                tbTextBox.Foreground = new SolidColorBrush(Colors.Cyan);
            else if (select == "cannel")
                tbCannel.Foreground = new SolidColorBrush(Colors.Cyan);

        }
        //删除主面板上线上的点
        private void delPoint()
        {
            if (canvasMain.Children.Contains(e1))
                this.canvasMain.Children.Remove(e1);
            if (canvasMain.Children.Contains(e2))
                this.canvasMain.Children.Remove(e2);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int il = 0;
            int ll = 0;
            foreach (UIElement u in canvasMain.Children)
            {
                if (u is Line)
                {
                    il++;
                }
                else if (u is TextBlock)
                {
                    ll++;
                }
            }

            MessageBox.Show(il.ToString() + "---------" + ll.ToString());

            //Test.SYN_ALTERFILER
        }

        private void tbHand_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void tbTool_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.Opacity = 1;
        }

        private void tbTool_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.Opacity = 0.6;
        }
    }
}
