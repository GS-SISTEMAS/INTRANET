<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmProductoCliente.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Producto.frmProductoCliente1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Mantenimiento de producto
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramProducto.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramProducto.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }

        function OpenWindowMng(id) {
            var oWnd = $find("<%= rwProducto.ClientID %>");
            if (id == 0) {
                window.radopen("frmProductoClienteMngLista.aspx", "rwProducto");

                oWnd.setSize(900, 500);

            }
            //window.radopen("frmProductoClienteMng.aspx?id=" + id, "rwProducto");
            else {
                window.radopen("frmProductoClienteMng.aspx?id=" + id, "rwProducto");
                oWnd.setSize(400, 400);
            }
            oWnd.Center(); 
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramProducto" runat="server" OnAjaxRequest="ramProducto_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpProducto" ControlID="rapProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpProducto" ControlID="rapProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpProducto" ControlID="rapProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpProducto" ControlID="rapProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpProducto" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmProducto" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwProducto" runat="server"  ReloadOnShow="true" 
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapProducto" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Mantenimiento de Producto" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/file-empty-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="530px"
            Orientation="Vertical">
            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                        EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                        <div class="fila">
                            <div class="colum3">
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
                            </div>
                            <div class="colum7">
                                <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single"
                                    InputType="Text" EmptyMessage="Buscar cliente" AllowCustomEntry="true" DropDownWidth="150px">
                                    <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmProductoCliente.aspx" />
                                </telerik:RadAutoCompleteBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="colum3">
                                <asp:Label ID="lblProducto" runat="server" Text="Producto" CssClass="etiqueta"></asp:Label>
                            </div>
                            <div class="colum7">
                                <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="100%" MaxLength="50"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="colum4">
                                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                    <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
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
                        <telerik:RadGrid ID="grdProducto" runat="server" Width="100%" Height="525px" AutoGenerateColumns="false" AllowSorting="false"
                            AllowMultiRowSelection="false" ShowGroupPanel="false" OnItemCommand="grdProducto_ItemCommand" OnDeleteCommand="grdProducto_DeleteCommand">
                            <MasterTableView Width="100%" DataKeyNames="ID" ClientDataKeyNames="ID">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Edit.">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Editar" ImageUrl="~/Images/Icons/pencil-16.png"/>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px"/>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_Cliente" HeaderText="Cod.Cliente">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente">
                                        <HeaderStyle Width="250px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Kardex" HeaderText="Kardex" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Codigo" HeaderText="Cod.Producto">
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Producto">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PrecioBase" HeaderText="P.Base">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PrecioEspecial" HeaderText="P.Esp.">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DctoImporte" HeaderText="Dscto">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VigenciaDesde" HeaderText="Vig.Desde" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VigenciaTermino" HeaderText="Vig.Term." DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el producto por cliente?" ConfirmDialogType="RadWindow" 
                                        HeaderStyle-Width="30px" HeaderText="Elim." ConfirmTitle="Eliminar" ButtonType="ImageButton" 
                                        CommandName="Delete" ImageUrl="../../../Images/Icons/delete-16.png" UniqueName="Elim" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                <Selecting AllowRowSelect="True"></Selecting>
                                <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                    ResizeGridOnColumnResize="False"></Resizing>
                            </ClientSettings>
                        </telerik:RadGrid>
                        <%--<telerik:RadWindowManager ID="RadWindowManager1" runat="server" />--%>
                    </div>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
