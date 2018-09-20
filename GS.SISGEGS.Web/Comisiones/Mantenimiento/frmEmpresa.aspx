<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmEmpresa.aspx.cs" Inherits="GS.SISGEGS.Web.Comision.Mantenimiento.frmEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Empresa
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(objEmpresa) {
            window.radopen("frmEmpresaMng.aspx?objEmpresa=" + objEmpresa, "rwEmpresa");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramEmpresa.ClientID %>").ajaxRequest("Rebind");         
            }
            else {
                $find("<%= ramEmpresa.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEmpresa" runat="server" OnAjaxRequest="ramEmpresa_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEmpresa" LoadingPanelID="ralpEmpresa"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramEmpresa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpresa" LoadingPanelID="ralpEmpresa"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEmpresa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEmpresa" LoadingPanelID="ralpEmpresa"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpEmpresa" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmEmpresa" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwEmpresa" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEmpresa" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Empresa"></asp:Label>
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
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
            <div class="col-md-5"></div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" Visible="false">
                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdEmpresa" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" OnItemCommand="grdEmpresa_ItemCommand">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" Visible="true" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("idEmpresa") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="idEmpresa" HeaderText="ID" UniqueName="idEmpresa">
                                <HeaderStyle Width="10px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="razonSocial" HeaderText="Empresa" UniqueName="nombreEmpresa">
                                <HeaderStyle Width="500px"/>
                            </telerik:GridBoundColumn>
                     
                            <telerik:GridBoundColumn DataField="Provision" HeaderText="Provisión" DataFormatString="{0:F0}%"  UniqueName="Provision">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridBoundColumn>

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
