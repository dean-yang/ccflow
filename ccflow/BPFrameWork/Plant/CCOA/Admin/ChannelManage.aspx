﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="ChannelManage.aspx.cs" Inherits="CCOA_Admin_ChannelManage" %>

<%@ Register Src="~/CCOA/Admin/ChannelForm.ascx" TagName="ChannelForm" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<uc:ChannelForm ID="ChannelForm1" runat="server" />
</asp:Content>
