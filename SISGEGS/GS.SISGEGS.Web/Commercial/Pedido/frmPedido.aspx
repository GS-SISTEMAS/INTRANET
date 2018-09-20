<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmPedido.aspx.cs" Inherits="GS.SISGEGS.Web.Commercial.Pedido.frmPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Gestión de pedidos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function ShowEditForm(id, rowIndex) {
            var grid = $find("<%= grdPedido.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            window.radopen("frmPedidoMng.aspx?idPedido=" + id, "rwPedido");
            return false;
        }

        function ShowInsertForm(id) {
            window.radopen("frmPedidoMng.aspx?idPedido=" + id, "rwPedido");
            return false;
        }

        function DocumentsViewer(id) {
            window.radopen("frmDocumentos.aspx?idPedido=" + id, "rwDocumento");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramPedido.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramPedido.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }

        function RowDblClick(sender, eventArgs) {
            var MasterTable = sender.get_masterTableView();
            var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var cell = MasterTable.getCellByColumnUniqueName(row, "CantGuia");
            if (parseInt(cell.innerText) == 0)
                window.radopen("frmPedidoMng.aspx?idPedido=" + eventArgs.getDataKeyValue("Op"), "rwPedido");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramPedido" runat="server" OnAjaxRequest="ramPedido_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedido" LoadingPanelID="ralpPedido"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramPedido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPedido" LoadingPanelID="ralpPedido"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPedido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedido" LoadingPanelID="ralpPedido"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPedido" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmPedido" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwPedido" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwDocumento" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlPedido" runat="server">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Gestión de pedidos" CssClass="titulo"></asp:Label>
            </div>
        </div>

        <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="500px"
            Orientation="Vertical">
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
                            <div class="colum3">
                                <asp:Label ID="lblTipoDocumento" runat="server" Text="Tip.Doc." CssClass="etiqueta"></asp:Label>
                            </div>
                            <div class="colum7">
                                <telerik:RadComboBox ID="cboTipoDocumento" runat="server" Width="100%"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="colum3">
                                <asp:Label ID="lblFormaPago" runat="server" Text="For.Pago" CssClass="etiqueta"></asp:Label>
                            </div>
                            <div class="colum7">
                                <telerik:RadComboBox ID="cboFormaPago" runat="server" Width="100%"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="colum4">
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="colum10">
                                <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" AllowCustomEntry="true"
                                    DropDownHeight="200px" EmptyMessage="Selec. cliente" OnClientEntryAdding="OnClientEntryAddingHandler" DropDownWidth="150px">
                                    <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmPedido.aspx" />
                                </telerik:RadAutoCompleteBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="colum4">
                                <asp:Label ID="lblVendedor" runat="server" Text="Vendedor" CssClass="etiqueta"></asp:Label>
                            </div>

                        </div>
                        <div class="fila">
                            <div class="colum10">
                                <telerik:RadAutoCompleteBox ID="acbVendedor" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" DropDownWidth="150px"
                                    DropDownHeight="150px" EmptyMessage="Selec. vendedor" AllowCustomEntry="true" OnClientEntryAdding="OnClientEntryAddingHandler">
                                    <WebServiceSettings Method="Agenda_BuscarVendedor" Path="frmPedido.aspx" />
                                </telerik:RadAutoCompleteBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="colum3">
                                <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="etiqueta"></asp:Label>
                            </div>
                            <div class="colum7">
                                <telerik:RadComboBox ID="cboEstado" runat="server" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Todos" Value="-1" Selected="true" />
                                        <telerik:RadComboBoxItem Text="Aprobadas" Value="1" />
                                        <telerik:RadComboBoxItem Text="Por Aprobar" Value="2" />
                                    </Items>
                                </telerik:RadComboBox>
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
            <%--<telerik:RadSplitBar ID="RadSplitBar2" runat="server">
            </telerik:RadSplitBar>--%>
            <telerik:RadPane ID="RadPane2" CssClass="contentPaneDecorator" runat="server" Width="100%" Scrolling="None">
                <div class="row">
                    <div class="col-md-12">
                        <telerik:RadGrid ID="grdPedido" runat="server" Width="100%" AutoGenerateColumns="false" Height="495px" OnItemDataBound="grdPedido_ItemDataBound" OnItemCommand="grdPedido_ItemCommand">
                            <MasterTableView Width="1600px" DataKeyNames="Op" ClientDataKeyNames="Op">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Op" HeaderText="#Op" UniqueName="Op">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NoRegistro" HeaderText="Nro.Registro" UniqueName="NoRegistro">
                                        <HeaderStyle Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FechaOrden" HeaderText="Fecha" UniqueName="FechaOrden">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_Agenda" HeaderText="Cod.Cliente" UniqueName="ID_Agenda">
                                        <HeaderStyle Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Agenda" HeaderText="Agenda" UniqueName="Agenda">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Vendedor" HeaderText="Vendedor" UniqueName="Vendedor">
                                        <HeaderStyle Width="180px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CondicionCredito" HeaderText="Cond.Cred." UniqueName="CondicionCredito">
                                        <HeaderStyle Width="120px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Neto" HeaderText="M.Neto" UniqueName="Neto" DataFormatString="{0:F3}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Dcto" HeaderText="Dcto." UniqueName="Dcto" DataFormatString="{0:F3}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SubTotal" HeaderText="SubTotal" UniqueName="SubTotal" DataFormatString="{0:F3}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Impuestos" HeaderText="Impuesto" UniqueName="Impuestos" DataFormatString="{0:F3}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Total" HeaderText="Total" UniqueName="Total" DataFormatString="{0:F3}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FormaPago" HeaderText="Forma Pago" UniqueName="FormaPago">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AlmacenDespacho" HeaderText="Almacén" UniqueName="AlmacenDespacho">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Transportista" HeaderText="Emp.Trans" UniqueName="Transportista">
                                        <HeaderStyle Width="160px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CantGuia" UniqueName="CantGuia" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CantProd" UniqueName="CantProd" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Est.">
                                        <ItemTemplate>
                                            <asp:Image ID="imgEstado" runat="server" ImageUrl="~/Images/Icons/circle-red-16.png" ToolTip="Sin guía" />
                                            <asp:ImageButton ID="ibEstado" runat="server" CommandName="VerGuia" CommandArgument='<%# Eval("Op") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Apro.">
                                        <ItemTemplate>
                                            <asp:Image ID="imgAprobacion" runat="server" />
                                            <asp:Label ID="Aprobacion1" runat="server" Text='<%# Eval("Aprobacion1") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Elim.">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibEliminar" runat="server" ImageUrl="~/Images/Icons/delete-16.png" CommandName="Eliminar" CommandArgument='<%# Eval("Op") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                <ClientEvents OnRowDblClick="RowDblClick" />
                                <%--<Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />--%>
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
        <div class="row">
            <div class="col-md-3">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png" />
                </telerik:RadButton>
            </div>
            <div class="col-md-6">
            </div>
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Image ID="imgAprobado" runat="server" ImageUrl="~/Images/Icons/sign-check-16.png" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblAprobado" runat="server" Text="Aprobado" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Image ID="imgPorAprobar" runat="server" ImageUrl="~/Images/Icons/sign-error-16.png" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblPorAprobar" runat="server" Text="Pendiente" CssClass="etiqueta"></asp:Label>
                    </div>
                </div>
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