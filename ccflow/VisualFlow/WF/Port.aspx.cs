using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;

namespace BP.Web.Port
{
	/// <summary>
	/// Port 的摘要说明。
	/// </summary>
    public partial class Port : System.Web.UI.Page
    {
        #region 必须传递参数
        public string DoWhat
        {
            get
            {
                return this.Request.QueryString["DoWhat"];
            }
        }
        public string UserNo
        {
            get
            {
                return this.Request.QueryString["UserNo"];
            }
        }
        public string SID
        {
            get
            {
                return this.Request.QueryString["SID"];
            }
        }
        #endregion

        #region  可选择的参数
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        public string FK_Node
        {
            get
            {
                return this.Request.QueryString["FK_Node"];
            }
        }
        public string WorkID
        {
            get
            {
                return this.Request.QueryString["WorkID"];
            }
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.AddHeader("P3P", "CP=CAO PSA OUR");
            //if (this.UserNo != null && this.SID != null)
            //{
            //    string sql = "SELECT SID  FROM Port_Emp WHERE No='" + this.UserNo + "'";
            //    string sid = BP.DA.DBAccess.RunSQLReturnVal(sql) as string;
            //    if (sid != this.SID)
            //    {
            //        this.Response.Write("非法的访问，请与管理员联系。sid=" + sid);
            //        //this.UCSys1.AddMsgOfWarning("错误：", "非法的访问，请与管理员联系。");
            //        return;
            //    }
            //    else
            //    {
            //        Emp em = new Emp(this.UserNo);
            //        WebUser.Token = this.Session.SessionID;
            //        WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);
            //    }
            //    //  this.Response.Redirect("EmpWorks.aspx", true);
            //    //  return;
            //}

            BP.SystemConfig.DoClearCash();
            Emp em = new Emp(this.UserNo);
            WebUser.Token = this.Session.SessionID;
            WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);

            string paras = "";
            foreach (string str in this.Request.QueryString)
            {
                string val = this.Request.QueryString[str];
                if (val.IndexOf('@') != -1)
                    throw new Exception("您没有能参数: [ " + str + " ," + val + " ] 给值 ，URL 将不能被执行。");

                switch (str)
                {
                    case DoWhatList.DoNode:
                    case DoWhatList.Emps:
                    case DoWhatList.EmpWorks:
                    case DoWhatList.EmpWorksSmall:
                    case DoWhatList.EmpWorksSmallSingle:
                    case DoWhatList.FlowSearch:
                    case DoWhatList.Login:
                    case DoWhatList.MyFlow:
                    case DoWhatList.MyWork:
                    case DoWhatList.Start:
                    case DoWhatList.StartSmall:
                    case DoWhatList.FlowFX:
                    case DoWhatList.DealWork:
                    case DoWhatList.DealWorkInSmall:
                    case "FK_Flow":
                    case "WorkID":
                    case "FK_Node":
                    case "SID":
                        break;
                    default:
                        paras += "&" + str + "=" + val;
                        break;
                }
            }

            if (this.IsPostBack == false)
            {
                this.IsCanLogin();
                BP.Port.Emp emp = new BP.Port.Emp(this.UserNo);
                BP.Web.WebUser.SignInOfGener(emp); //开始执行登陆。


                string nodeID = int.Parse(this.FK_Flow + "01").ToString();
                switch (this.DoWhat)
                {
                    case DoWhatList.Start: // 发起工作
                        if (this.FK_Flow == null)
                            this.Response.Redirect("Start.aspx", true);
                        else
                            this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.StartSmall: // 发起工作　小窗口
                        if (this.FK_Flow == null)
                            this.Response.Redirect("StartSmall.aspx?FK_Flow=" + this.FK_Flow + paras, true);
                        else
                            this.Response.Redirect("MyFlowSmall.aspx?FK_Flow=" + this.FK_Flow + paras, true);
                        break;
                    case DoWhatList.StartSmallSingle: // 发起工作单独小窗口
                        if (this.FK_Flow == null)
                            this.Response.Redirect("StartSmall.aspx?FK_Flow=" + this.FK_Flow + paras + "&IsSingle=1" + "&FK_Node=" + nodeID, true);
                        else
                            this.Response.Redirect("MyFlowSmallSingle.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.RuningSmall: // 在途中工作
                        this.Response.Redirect("RuningSmall.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case "RuningSmallSingle": // 在途中工作
                        this.Response.Redirect("RuningSmallSingle.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.Tools: // 工具栏目。
                        this.Response.Redirect("Tools.aspx", true);
                        break;
                    case DoWhatList.ToolsSmall: // 小工具栏目。
                        this.Response.Redirect("ToolsSmall.aspx?RefNo=" + this.Request["RefNo"], true);
                        break;
                    case DoWhatList.EmpWorksSmall: // 我的工作小窗口
                        if (this.FK_Flow == null || this.FK_Flow == "")
                            this.Response.Redirect("EmpWorksSmall.aspx", true);
                        else
                            this.Response.Redirect("EmpWorksSmall.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.EmpWorksSmallSingle: // 我的工作小窗口独立的
                        if (this.FK_Flow == null || this.FK_Flow == "")
                            this.Response.Redirect("EmpWorksSmallSingle.aspx", true);
                        else
                            this.Response.Redirect("EmpWorksSmallSingle.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.EmpWorks: // 我的工作。
                    case DoWhatList.Login:
                        if (this.FK_Flow == null)
                            this.Response.Redirect("EmpWorks.aspx", true);
                        else
                            this.Response.Redirect("EmpWorks.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.Emps: // 通讯录。
                        this.Response.Redirect("Emps.aspx", true);
                        break;
                    case DoWhatList.FlowSearch: // 流程查询。
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearch.aspx", true);
                        else
                            this.Response.Redirect("../Comm/PanelEns.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowSearchSmall: // 流程查询。
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearchSmall.aspx", true);
                        else
                            this.Response.Redirect("../Comm/PanelEns.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowSearchSmallSingle: // 流程查询。
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearchSmallSingle.aspx", true);
                        else
                            this.Response.Redirect("../Comm/PanelEns.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowFX: // 流程查询。
                        if (this.FK_Flow == null)
                            throw new Exception("@没有参数流程编号。");

                        this.Response.Redirect("../Comm/GroupEnsMNum.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.DealWork:
                        if (this.FK_Flow == null || this.WorkID == null)
                            throw new Exception("@参数 FK_Flow 或者 WorkID 为Null 。");
                        this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&o2=1" + paras, true);
                        break;
                    case DoWhatList.DealWorkInSmall:
                        if (this.FK_Flow == null || this.WorkID == null)
                            throw new Exception("@参数 FK_Flow 或者 WorkID 为Null 。");
                        this.Response.Redirect("MyFlowSmall.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&o2=1" + paras, true);
                        break;
                    default:
                        this.ToErrorPage("没有约定的标记:DoWhat=" + this.DoWhat);
                        break;
                }
            }
        }
        public void ShowMsg(string msg)
        {
            this.Response.Write(msg);
        }
        public bool IsCanLogin()
        {
            return true;
            //if (this.RequestKey != this.GetKey())
            //{
            //    if (SystemConfig.IsDebug)
            //        return true;
            //    //throw new Exception("钥匙无效，不能执行您你请求。key="+this.GetKey() );
            //    else
            //        throw new Exception("钥匙无效，不能执行您你请求。");
            //}
            //return true;
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion

        public void ToMsgPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + "/Port/InfoPage.aspx", true);
            return;
        }
        /// <summary>
        /// 切换到信息也面。
        /// </summary>
        /// <param name="mess"></param>
        public void ToErrorPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + "/Comm/Port/InfoPage.aspx");
            return;
        }
    }
}
