﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ExpandingApplication/MasterPage.master" AutoEventWireup="true" CodeFile="NodeRule.aspx.cs" Inherits="ExpandingApplication_PRJ_NodeRuleUI" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%">
<tr>
<td align="left" >
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
<td  align="left" valign="top" width="70%">
    <uc1:Pub ID="Pub2" runat="server" />
    </td>

</tr>

</table>
</asp:Content>

