﻿using System;
using BP.GE.Ctrl;
using BP.GE;
public partial class GE_Comment_GEPJ : BP.Web.WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.MaintainScrollPositionOnPostBack = true;
        if (string.IsNullOrEmpty(BP.Web.WebUser.Name) || BP.Web.WebUser.Name.Trim().ToLower() == "admin")
        {
            txtUserName.Value = "匿名";
        }
        else
        {
            txtUserName.Value = BP.Web.WebUser.Name;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string GroupKey = Convert.ToString(Request.QueryString["GroupKey"]);
        string RefOID = Convert.ToString(Request.QueryString["RefOID"]);
        bool ISAllowAnony = Convert.ToBoolean(Request.QueryString["ISAllowAnony"]);

        if (CheckCodeIsRight())
        {
            
           if(CheckIsHaveRights(ISAllowAnony))
            {
                Comment comment = new Comment();
                comment.IP = GeFun.getIp();
                comment.Title = string.Empty;
                comment.GroupKey = GroupKey;
                comment.RefOID = RefOID;
                comment.FK_Dept = BP.Web.WebUser.FK_Dept;
                comment.FK_Emp = BP.Web.WebUser.No;
                comment.FK_EmpT = txtUserName.Value;

                //脏话过滤
                //@0=禁止提交@1=替换字串@2=不处理
                string StrContent = this.txtContent.Value;
                BadWords badwords = new BadWords();
                badwords.RetrieveAll();
                foreach (BadWord badword in badwords)
                {
                    if (StrContent.Contains(badword.Name))
                    {
                        //处理方式
                        switch (Convert.ToInt32(badword.BadWordDealWay))
                        {
                            
                            case 0:
                                BP.GE.GeFun.ShowMessage(this.Page, "strScript3", "评论内容被禁止提交!");
                                return;
                            case 1:
                                StrContent = StrContent.Replace(badword.Name, badword.ReplaceWord);
                                break;
                            case 2:
                                break;
                        }
                    }
                }

                comment.Doc = StrContent;
                comment.RDT = DateTime.Now.ToShortDateString();
                comment.Insert();
                this.ClientScript.RegisterStartupScript(this.GetType(), "myJS", "mySucess()", true);
            }
        }
    }

    /// <summary>
    /// 判断验证码是否正确
    /// </summary>
    /// <param name="page">控件所在页</param>
    /// <param name="strText">用户输入的验证码</param>
    /// <returns>验证结果</returns>
    public bool CheckCodeIsRight()
    {
        if (Convert.ToString(Session["GeCheckCode"]).ToLower() == txtCheckCode.Value.ToLower())
        {
            return true;
        }
        else
        {
            BP.GE.GeFun.ShowMessage(this, "strScript", "验证码输入错误!");
            return false;
        }
    }
    /// <summary>
    /// 判断是是否有权评论
    /// </summary>
    /// <param name="ISAllowAnony"></param>
    /// <returns></returns>
    public bool CheckIsHaveRights(bool ISAllowAnony)
    {
        if (ISAllowAnony == true)
        {
            return true;
        }
        else
        {
            if (string.IsNullOrEmpty(BP.Web.WebUser.No))
            {
                BP.GE.GeFun.ShowMessage(this, "strScript2", "对不起请先登陆!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}