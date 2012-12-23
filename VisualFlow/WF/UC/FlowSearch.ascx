﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowSearch.ascx.cs" Inherits="WF_UC_FlowSearch" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc2" %>
<script type="text/javascript">
    function GroupBarClick(rowIdx) {
        var alt = document.getElementById('Img' + rowIdx).alert;
        var sta = 'block';
        if (alt == 'Max') {
            sta = 'block';
            alt = 'Min';
        } else {
            sta = 'none';
            alt = 'Max';
        }
        document.getElementById('Img' + rowIdx).src = './Img/' + alt + '.gif';
        document.getElementById('Img' + rowIdx).alert = alt;
        var i = 0
        for (i = 0; i <= 5000; i++) {
            if (document.getElementById(rowIdx + '_' + i) == null)
                continue;
            document.getElementById(rowIdx + '_' + i).style.display = sta;
        }
    }
</script>
<div align=center>
    <uc2:Pub ID="Pub1" runat="server" />
    <uc2:Pub ID="Pub2" runat="server" />
    </div>