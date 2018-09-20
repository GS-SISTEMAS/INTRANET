<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOrdenCompra.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.Pedido.frmOrdenCompra
    " %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Gestión de pedidos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowForm(id) {
            window.radopen("frmOrdenVentaMng.aspx?idOrdenVenta=" + id, "rwOrdenVenta");
            return false;
        }

        function ShowDocuments(id) {
            window.radopen("frmOrdenVentaDocs.aspx?idOrdenVenta=" + id, "rwDocumento");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramOrdenVenta.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramOrdenVenta.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramOrdenVenta" runat="server" OnAjaxRequest="ramOrdenVenta_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOrdenVenta" LoadingPanelID="ralpOrdenVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOrdenVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOrdenVenta" LoadingPanelID="ralpOrdenVenta" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpOrdenVenta" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmOrdenVenta" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwOrdenVenta" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwDocumento" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlOrdenVenta" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" Text="Gestión de pedidos" CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png" />
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="95%">
                    <Content>
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                        EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaInicio" runat="server" Text="Fec.Ini." CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" DateInput-ReadOnly="true">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fec.Fin." CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%" DateInput-ReadOnly="true">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum4">
                                                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                                </telerik:RadButton>
                                            </div>
                                        </div>
                                    </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                            </telerik:RadPane>
                            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%">
                                <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%">
                                    <telerik:LayoutRow Height="100%">
                                        <Content>
                                            <telerik:RadGrid ID="grdOrdenVenta" runat="server" AllowFilteringByColumn="True" ShowFooter="True" AllowSorting="True" Width="100%" 
                                                AutoGenerateColumns="false" Height="100%" OnNeedDataSource="grdOrdenVenta_NeedDataSource" OnDeleteCommand="grdOrdenVenta_DeleteCommand"
                                                OnItemCommand="grdOrdenVenta_ItemCommand" OnItemDataBound="grdOrdenVenta_ItemDataBound">
                                                <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                <MasterTableView ShowFooter="True" DataKeyNames="Op" ClientDataKeyNames="Op" Width="2060px">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Edit." AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("Op") %>' CommandName="AbrirPedido"
                                                                    ImageUrl="~/Images/Icons/pencil-16.png" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="ID" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridMaskedColumn DataField="Op" HeaderText="#Op" FilterControlWidth="45px" AutoPostBackOnFilter="false"
                                                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" Mask="########" UniqueName="Op">
                                                            <HeaderStyle Width="50px" />
                                                            <ItemStyle Height="42px" />
                                                        </telerik:GridMaskedColumn>
                                                        <telerik:GridMaskedColumn DataField="NoRegistro" HeaderText="NoRegistro" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                                                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" Mask="#####">
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridMaskedColumn>
                                                        <telerik:GridBoundColumn UniqueName="FechaOrden" DataField="FechaOrden" HeaderText="Fecha" AllowFiltering="false"
                                                            HeaderStyle-Width="70px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="150px" DataField="Agenda" HeaderText="Cliente" FilterDelay="2000"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="200px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridCheckBoxColumn HeaderText="Bloq." UniqueName="Bloqueado" DataField="Bloqueado" ShowFilterIcon="false" AllowFiltering="false">
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridCheckBoxColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="120px" DataField="Vendedor" HeaderText="Vendedor"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="100px" DataField="CondicionCredito" HeaderText="Cod.Cred."
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Neto" AllowFiltering="false" UniqueName="Neto"
                                                            DataType="System.Decimal" HeaderText="M.Neto" Aggregate="Sum" DataFormatString="{0:#,#.##}">
                                                            <FooterStyle Font-Bold="true"></FooterStyle>
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Dcto" AllowFiltering="false" UniqueName="Dcto"
                                                            DataType="System.Decimal" HeaderText="Dcto." Aggregate="Sum" DataFormatString="{0:#,#.##}">
                                                            <FooterStyle Font-Bold="true"></FooterStyle>
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="SubTotal" AllowFiltering="false" UniqueName="SubTotal"
                                                            DataType="System.Decimal" HeaderText="SubTotal" Aggregate="Sum" DataFormatString="{0:#,#.##}">
                                                            <FooterStyle Font-Bold="true"></FooterStyle>
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Impuestos" AllowFiltering="false" UniqueName="Impuestos"
                                                            DataType="System.Decimal" HeaderText="Impuesto" Aggregate="Sum" DataFormatString="{0:#,#.##}">
                                                            <FooterStyle Font-Bold="true"></FooterStyle>
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Total" AllowFiltering="false" UniqueName="Total"
                                                            DataType="System.Decimal" HeaderText="Total" Aggregate="Sum" DataFormatString="{0:#,#.##}">
                                                            <FooterStyle Font-Bold="true"></FooterStyle>
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="80px" DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="80px" DataField="FormaPago" HeaderText="Forma Pago" UniqueName="FormaPago"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="200px" DataField="AlmacenDespacho" HeaderText="Almacén" UniqueName="AlmacenDespacho"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="220px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="200px" DataField="Transportista" HeaderText="Emp.Trans" UniqueName="Transportista"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="220px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Guia" DataField="Guia" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Factura" DataField="Factura" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Aprobacion" DataField="Aprobacion1" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Guía" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgGuia" runat="server" Width="16px" Height="16px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Fact." AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgFactura" runat="server" Width="16px" Height="16px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Docs." AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgDocuments" runat="server" Width="16px" Height="16px"
                                                                    CommandName="Documentos" CommandArgument='<%# Eval("Op") %>' ImageUrl="~/Images/Icons/folder-document-16.png" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Aprob." AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgAprobacion" runat="server" Width="16px" Height="16px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn FilterControlWidth="300px" DataField="Motivo" HeaderText="Aviso" UniqueName="Motivo"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="300px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el pedido?" ConfirmDialogType="RadWindow"
                                                            HeaderStyle-Width="30px" HeaderText="Elim." ConfirmTitle="Eliminar" ButtonType="ImageButton"
                                                            CommandName="Delete" ImageUrl="../../Images/Icons/delete-16.png" UniqueName="Elim" />
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Scrolling UseStaticHeaders="True" FrozenColumnsCount="3" AllowScroll="true"></Scrolling>
                                                    <Selecting AllowRowSelect="True"></Selecting>
                                                    <Resizing AllowRowResize="True" EnableRealTimeResize="True"></Resizing>
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </Content>
                                    </telerik:LayoutRow>
                                </telerik:RadPageLayout>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
</asp:Content>
