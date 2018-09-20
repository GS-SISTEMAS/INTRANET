<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmPerfil.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Perfil.frmPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de perfil
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(objPerfil) {
            window.radopen("frmPerfilMng.aspx?objPerfil=" + objPerfil, "rwPerfil");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramPerfil.ClientID %>").ajaxRequest("Rebind");         
            }
            else {
                $find("<%= ramPerfil.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramPerfil" runat="server" OnAjaxRequest="ramPerfil_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPerfil" LoadingPanelID="ralpPerfil"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramPerfil">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPerfil" LoadingPanelID="ralpPerfil"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPerfil">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPerfil" LoadingPanelID="ralpPerfil"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPerfil" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmPerfil" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwPerfil" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapPerfil" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Perfil"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="cboEmpresa" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <telerik:RadTextBox ID="txtBuscar" runat="server" Width="85%" EmptyMessage="Buscar"></telerik:RadTextBox>
                <telerik:RadButton ID="btnBuscar" runat="server" Width="24px" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
            <div class="col-md-5"></div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/file-empty-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdPerfil" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" OnItemCommand="grdPerfil_ItemCommand">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("idPerfil") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="nombreComercial" HeaderText="Empresa" UniqueName="nombreComercial">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="idPerfil" HeaderText="ID" UniqueName="idPerfil">
                                <HeaderStyle Width="10px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="nombrePerfil" HeaderText="Nombre" UniqueName="nombrePerfil">
                                <HeaderStyle Width="500px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="modificarPedido" HeaderText="Ver Pedidos" UniqueName="activo">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="aprobarPlanilla0" HeaderText="Aprob.Jef.Inm" UniqueName="activo">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="aprobarPlanilla1" HeaderText="Aprob.Conta." UniqueName="activo">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="activo" HeaderText="Activo" UniqueName="activo">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
