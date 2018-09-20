 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grupo Silvestre: Intranet</title>
	<script type="text/javascript">
	    function openWin() {
	        var left = (screen.width / 2) - (1360 / 1.95);
	        var top = (screen.height / 2) - (660 / 1.7);
	        myWindow = window.open("frmLogin.aspx", "myWindow", "width=1360px, height=660px, top=" + top + ", left=" + left + ",resizable=no,toolbar=no");    // Opens a new window
	    }
    </script>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
</head>
<body onload="openWin()">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <script type="text/javascript">
        //Put your JavaScript code here.
    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div>

    </div>
    </form>
</body>
</html>
