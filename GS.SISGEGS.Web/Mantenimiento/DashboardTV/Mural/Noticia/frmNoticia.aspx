<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmNoticia.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.DashboardTV.Mural.Noticia.frmNoticia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Noticias
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(idNoticia) {
            window.radopen("frmNoticiaMng.aspx?idNoticia=" + idNoticia, "rwNoticias");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramNoticias.ClientID %>").ajaxRequest("Rebind");         
            }
            else {
                $find("<%= ramNoticias.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramNoticias" runat="server" OnAjaxRequest="ramNoticias_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapNoticias" LoadingPanelID="ralpNoticias"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramNoticias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdNoticias" LoadingPanelID="ralpNoticias"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdNoticias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapNoticias" LoadingPanelID="ralpNoticias"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpNoticias" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmNoticias" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwNoticias" runat="server" Width="660px" Height="500px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapNoticias" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-12">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Noticias"></asp:Label>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
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
                                <Icon PrimaryIconUrl="../../../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </div>
                        <div class="col-md-5"></div>
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                                <Icon PrimaryIconUrl="../../../../Images/Icons/file-empty-16.png" />
                            </telerik:RadButton>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-12">
                            <telerik:RadGrid ID="grdNoticias" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" OnItemCommand="grdNoticias_ItemCommand" OnDeleteCommand="grdNoticias_DeleteCommand">
                                <MasterTableView DataKeyNames="idNoticia">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("idNoticia") %>' CommandName="Editar" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="nombreComercial" HeaderText="Empresa" UniqueName="nombreComercial">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="idNoticia" HeaderText="ID" UniqueName="idNoticia">
                                            <HeaderStyle Width="10px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="titulo" HeaderText="Titulo">
                                            <HeaderStyle Width="500px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="texto" HeaderText="Texto">
                                            <HeaderStyle Width="500px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cant.">
                                            <HeaderStyle Width="500px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="fechaPublicacion" HeaderText="Fecha Publicación" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle Width="500px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="fechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle Width="500px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridCheckBoxColumn DataField="activo" HeaderText="Activo" UniqueName="activo">
                                            <HeaderStyle Width="30px" />
                                        </telerik:GridCheckBoxColumn>
                                        <telerik:GridButtonColumn ConfirmText="¿Desea eliminar la noticia?" ConfirmDialogType="RadWindow"
                                            HeaderStyle-Width="40px" HeaderText="Elim." ConfirmTitle="Eliminar" ButtonType="ImageButton"
                                            CommandName="Delete" ImageUrl="~/Images/Icons/delete-16.png" UniqueName="Elim" />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
