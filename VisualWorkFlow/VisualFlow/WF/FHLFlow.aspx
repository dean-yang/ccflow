﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Style/WinOpen.master" AutoEventWireup="true" CodeFile="FHLFlow.aspx.cs" Inherits="WF_FHLFlow" %>
<%@ Register src="UC/FHLFlow.ascx" tagname="FHLFlow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Comm/Style/Table.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FHLFlow ID="FHLFlow1" runat="server" />
</asp:Content>

