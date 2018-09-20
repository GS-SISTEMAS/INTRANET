<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="GS.SISGEGS.Web.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>Grupo Silvestre: Portal Interno</title>
    <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />

    <link type="text/css" href="Styles/bootstrap.css" rel="stylesheet"/>
    <link type="text/css" href="Styles/style.css" rel="stylesheet"/>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>

    <script type="text/javascript">
        function ShowPasswordChange(objUsuario) {
            window.radopen("frmPasswordMng.aspx?objUsuario=" + objUsuario, "rwLogin");
            return false;
        }

        function refreshGrid(arg) {
            $find("<%= ramLogin.ClientID %>").ajaxRequest(arg);
        }
    </script>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <telerik:RadAjaxManager ID="ramLogin" runat="server" OnAjaxRequest="ramLogin_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnLogin">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl LoadingPanelID="ldnLogin" ControlID="pnlLogin"/>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ramLogin">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl LoadingPanelID="ldnLogin" ControlID="pnlLogin"/>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadWindowManager ID="rwmLogin" runat="server" EnableShadow="true">
            <Windows>
                <telerik:RadWindow ID="rwLogin" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                    ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

        <telerik:RadAjaxLoadingPanel ID="ldnLogin" runat="server"></telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="pnlLogin" runat="server" CssClass="container">
            <div class="row" style="height:200px">
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-2">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/Logos/grupo.png"/>
                </div>
                <div class="col-md-3">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblEmrpesa" runat="server" Text="Empresa: " CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <telerik:RadComboBox ID="cboEmpresa" runat="server"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblusuario" runat="server" Text="Usuario: " CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <telerik:RadTextBox ID="txtUsuario" runat="server" Width="100%"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblContrasena" runat="server" Text="Contraseña: " CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <telerik:RadTextBox ID="txtContrasena" runat="server" TextMode="Password" Width="100%"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                            <telerik:RadButton ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click">
                                <Icon PrimaryIconUrl="Images/Icons/user-id-16.png"/>
                            </telerik:RadButton>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    
                </div>
                <div class="col-md-9"></div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    
                </div>
                <div class="col-md-9"></div>
            </div>
            
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>