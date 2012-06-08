﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailList.aspx.cs" Inherits="EIP_EmailList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="lbtAll" runat="server" onclick="LinkButton1_Click">全部</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lbtUnRead" runat="server" onclick="lbtUnRead_Click">未读</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lbtReaded" runat="server" onclick="lbtReaded_Click">已读</asp:LinkButton>
        <div class="newslist">
            <ul>
                <% foreach (BP.CCOA.OA_Email item in EmailList)
                   {%>
                <li>
                    <img src="Images/gif/nav_title_sign.gif" />
                    <a href="Email/Add.aspx">
                        <%=item.Addresser %></a>&nbsp; <a href="Email/Show.aspx?id=<%=item.No%>" target="_blank">
                            <%=item.Subject%></a></li>
                <%} %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
