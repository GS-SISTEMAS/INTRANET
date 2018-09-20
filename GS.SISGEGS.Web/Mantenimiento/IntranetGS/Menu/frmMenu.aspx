<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmMenu.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Menu.frmMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Mantenimiento del Menú
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowEditWin(idMenu) {
            window.radopen("frmMenuMng.aspx?idMenu=" + idMenu, "winMenu");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= mngMenu.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= mngMenu.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="mngMenu" runat="server" OnAjaxRequest="mngMenu_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="mngMenu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="ltbPerfil"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarOpcion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarRaiz">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtvMenu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboEmpresa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMenu" LoadingPanelID="lodMenu"/>
                    <telerik:AjaxUpdatedControl ControlID="ltbPerfil"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="lodMenu" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="wmgMenu" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winMenu" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlMenu" runat="server">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Mantenimiento del Menú" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <telerik:RadButton ID="btnAgregarRaiz" runat="server" Text="Agregar Raíz" Width="70%" OnClick="btnAgregarRaiz_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/sign-add-16.png"/>
                </telerik:RadButton>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-9">
                <asp:Label ID="lblSubTitulo1" runat="server" Text="Menu Item" CssClass="subTitulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <telerik:RadTreeView ID="rtvMenu" runat="server" Width="100%" OnNodeClick="rtvMenu_NodeClick"></telerik:RadTreeView>
            </div>
            <div class="col-md-9">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblIdMenu" runat="server" Visible="false"></asp:Label>
                        <telerik:RadTextBox ID="txtCodigo" runat="server" Enabled="false" Label="Código: " Width="100%" LabelWidth="40%"></telerik:RadTextBox>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadTextBox ID="txtNombre" runat="server" Enabled="false" Label="Nombre: " Width="100%" LabelWidth="30%" MaxLength="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <telerik:RadTextBox ID="txtCodPadre" runat="server" Enabled="false" Label="Cód.Padre: " Width="100%" LabelWidth="40%"></telerik:RadTextBox>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadTextBox ID="txtNomPadre" runat="server" Enabled="false" Label="Nom.Padre: " Width="100%" LabelWidth="30%" MaxLength="20"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7">
                        <telerik:RadTextBox ID="txtURL" runat="server" Enabled="false" Label="URL: " Width="100%" LabelWidth="16%" MaxLength="400"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <telerik:RadButton ID="ckbActivo" runat="server" Text="Activo" ToggleType="CheckBox" ButtonType="ToggleButton" Width="100%"></telerik:RadButton>
                    </div>
                    <div class="col-md-1">
                        <telerik:RadButton ID="ckbDefecto" runat="server" Text="Defecto" ToggleType="CheckBox" ButtonType="ToggleButton" Width="100%"></telerik:RadButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <telerik:RadComboBox ID="cboEmpresa" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa" Enabled="false"
                            AutoPostBack="true" Label="Empresa " Width="100%" OnSelectedIndexChanged="cboEmprsa_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <telerik:RadListBox ID="ltbPerfil" runat="server" RenderMode="Lightweight" CheckBoxes="true" ShowCheckAll="true" Enabled="false"
                            Width="100%" Height="300px" DataTextField="nombrePerfil" DataValueField="idPerfil"></telerik:RadListBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click">
                            <Icon PrimaryIconUrl="../../../Images/Icons/pencil-16.png"/>
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Visible="false" OnClick="btnGuardar_Click">
                            <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png"/>
                        </telerik:RadButton>
                    </div>
                    <div class="col-md-2">
                        <telerik:RadButton ID="btnAgregarOpcion" runat="server" Text="Agregar Opción" Visible="false" OnClick="btnAgregarOpcion_Click">
                            <Icon PrimaryIconUrl="../../../Images/Icons/sign-add-16.png"/>
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click">
                            <Icon PrimaryIconUrl="../../../Images/Icons/sign-ban-16.png"/>
                        </telerik:RadButton>
                    </div>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <telerik:RadAjaxPanel ID="pnlMensaje" runat="server" CssClass="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
