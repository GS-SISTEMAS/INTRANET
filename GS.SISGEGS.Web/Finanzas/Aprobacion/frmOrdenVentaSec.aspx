<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOrdenVentaSec.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Aprobacion.frmOrdenVentaSec" %>

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

        function ShowEstadoCuenta(id) {
            window.radopen("frmEstadoCuentaDetalleMng.aspx?aprobacion=" + id, "rwDocumento");
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

        function ShowInsertForm(Id_Cliente, IdPedido, Op, idSectorista, comentario, ValorVenta, Id_moneda, Aprobacion, Guia, totalDeudaVencida, totalletras) {
            window.radopen("frmOrdenVentaSecAprob2.aspx?Id_Cliente=" + Id_Cliente +
                "&IdPedido=" + IdPedido + "&Op=" + Op + "&idSectorista=" + idSectorista+
                "&comentario=" + comentario + "&ValorVenta=" + ValorVenta + "&Id_moneda=" + Id_moneda + "&Aprobacion=" + Aprobacion
                + "&Guia=" + Guia + "&totalDeudaVencida=" + totalDeudaVencida + "&totalletras=" + totalletras, "rwOrdenVentaAprobMng");
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramOrdenVenta" runat="server" OnAjaxRequest="ramOrdenVenta_AjaxRequest">
        <AjaxSettings>

           <telerik:AjaxSetting AjaxControlID="btnAprobar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOrdenVenta" LoadingPanelID="ralpOrdenVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOrdenVenta" LoadingPanelID="ralpOrdenVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdOrdenVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrdenVenta" LoadingPanelID="ralpOrdenVenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>  


            <telerik:AjaxSetting AjaxControlID="grdOrdenVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOrdenVenta" LoadingPanelID="ralpOrdenVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

           <%-- <telerik:AjaxSetting AjaxControlID="rwmOrdenVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrdenVenta" LoadingPanelID="grdOrdenVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>

            <telerik:AjaxSetting AjaxControlID="ramOrdenVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrdenVenta" LoadingPanelID="ralpOrdenVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpOrdenVenta" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmOrdenVenta" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwOrdenVenta" runat="server" Width="1030px" Height="525px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwDocumento" runat="server" Width="1030px" Height="515px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwOrdenVentaAprobMng" runat="server" Width="630px" Height="350px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlOrdenVenta" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="12">
                             <asp:Label ID="lblTitulo" runat="server" Text="Gestión de Pedidos Sectorista: " CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="6">
                          
                                                <telerik:RadButton ID="btnAprobar" runat="server" OnClick="btnAprobar_Click" style="top: 1px; left: 3px" 
                                                    Text="Aprobar pedido" Width="130px">
                                                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                                                </telerik:RadButton>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="4">
                        </telerik:LayoutColumn>

                         <telerik:LayoutColumn Span="2">
 
                           <li style="float: left; margin-right:15px; color:red; font-size:x-small; height:auto"><span style="width: 15px; margin-right: 5px; background: red; display: block; border: solid 1px red; float: left">&nbsp;</span>
                              Clientes Bloqueados
                           </li>

                        </telerik:LayoutColumn>

                    </Columns>
                </telerik:LayoutRow>




                <telerik:LayoutRow Height="95%">
                    <Content>
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="400px" Title="Filtros de Busqueda"
                                        EnableDock="false" MinWidth="375" MinHeight="375" Scrolling="None">
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
                                                <asp:Label ID="Label1" runat="server" Text="Sectorista" CssClass="etiqueta" Visible="false"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadComboBox ID="cbSectorista" runat="server"  Visible="false" Width="250px"  >
                                                </telerik:RadComboBox>
                                            </div>

                                        </div>

                                        <div class="fila">
                                           <div class="colum3">
                                                <asp:Label ID="Label3" runat="server" Text="Tipo Pago:" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadComboBox ID="cboTipoPago" runat="server"  Width="250px"  >
                                                </telerik:RadComboBox>
                                            </div>

                                        </div>

                                          <div class="fila">
                                           <div class="colum3">
                                                <asp:Label ID="Label2" runat="server" Text="Estado:" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadComboBox ID="cboEstado" runat="server"  Width="250px"  >
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
                            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%">
                                <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%">
                                    <telerik:LayoutRow Height="100%">
                                        <Content>

                                                <telerik:RadGrid ID="grdOrdenVenta" runat="server" AllowFilteringByColumn="True" ShowFooter="True"
                                                AllowSorting="True" Width="100%" 
                                                AutoGenerateColumns="false" Height="500px" OnNeedDataSource="grdOrdenVenta_NeedDataSource" 
                                                OnDeleteCommand="grdOrdenVenta_DeleteCommand"
                                                OnItemCommand="grdOrdenVenta_ItemCommand" OnItemDataBound="grdOrdenVenta_ItemDataBound">

                                                <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                <MasterTableView ShowFooter="True" DataKeyNames="Op" ClientDataKeyNames="Op" Width="2060px">


                                                    <Columns>
                                                       
                                                          <telerik:GridBoundColumn UniqueName="id_moneda" DataField="id_moneda" Display="false">
                                                        </telerik:GridBoundColumn>

                                                         <telerik:GridBoundColumn UniqueName="Creditos" DataField="Creditos" Display="false">
                                                        </telerik:GridBoundColumn>


                                                       <telerik:GridTemplateColumn UniqueName="CheckColumn" HeaderText="Aprobar" HeaderStyle-Width="60px" AllowSorting="true" AllowFiltering="false">
                                                          <ItemTemplate>
                                                            <asp:CheckBox ID="Check" runat="server"  />
                                                          </ItemTemplate>
                                                         </telerik:GridTemplateColumn>
                                                        
                                                         <telerik:GridButtonColumn ConfirmText="¿Desea desaprobar el pedido?" ConfirmDialogType="RadWindow"
                                                            HeaderStyle-Width="100px"   ConfirmTitle="Desaprobar" ButtonType="ImageButton"
                                                            CommandName="Delete" ImageUrl="../../Images/Icons/deasprobar.png" UniqueName="Elim" />


                                                        <telerik:GridBoundColumn UniqueName="Aprobacion" DataField="Aprobacion2" Display="false">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Guia" DataField="Guia" Display="false">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Factura" DataField="Factura" Display="false">
                                                        </telerik:GridBoundColumn>
                                                      

                                                       <telerik:GridTemplateColumn HeaderText="Aprob." AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgAprobacion" runat="server" Width="16px" Height="16px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
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
                                                        
                                                       <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/search-16.png" CommandArgument='<%# Eval("ID_Agenda") %>'  CommandName="Estado"/>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px"/>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn FilterControlWidth="300px" DataField="Motivo" HeaderText="Aviso" UniqueName="Motivo"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="300px" />
                                                        </telerik:GridBoundColumn>



                                                        <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="ID" AllowFiltering="false" Display="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridMaskedColumn DataField="Op" HeaderText="#Op" FilterControlWidth="45px" AutoPostBackOnFilter="false" AllowSorting="true" Aggregate="Count"
                                                                    CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" Mask="#####" UniqueName="Op">
                                                                    <HeaderStyle Width="80px" />
                                                                    
                                                        </telerik:GridMaskedColumn>


                                                        <telerik:GridBoundColumn DataField="NoRegistro" HeaderText="NoRegistro" FilterDelay="2000"  Display="false"
                                                             FilterControlWidth="150px"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="FechaOrden" DataField="FechaOrden" HeaderText="Fecha" AllowFiltering="false"
                                                            HeaderStyle-Width="70px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="ID_Agenda" DataField="ID_Agenda" HeaderText="ID_Agenda" AllowFiltering="false" Display="false">
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridCheckBoxColumn HeaderText="Bloq."  ItemStyle-HorizontalAlign="Right"  UniqueName="Bloqueado" DataField="Bloqueado" ShowFilterIcon="false" AllowFiltering="false">
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridCheckBoxColumn>

                                                        <telerik:GridBoundColumn UniqueName="BloqueadoEstado" DataField="Bloqueado" Display="false">
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridTemplateColumn HeaderText="Estado" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:label ID="lblEstado"  runat="server" Width="80px" ></asp:label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridBoundColumn DataField="Agenda" HeaderText="Cliente" FilterDelay="2000" 
                                                             FilterControlWidth="250px"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="250px" />
                                                        </telerik:GridBoundColumn>
                                                       
                                                     


                                                        <telerik:GridBoundColumn FilterControlWidth="120px" DataField="Vendedor" HeaderText="Vendedor"
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridBoundColumn>

                                                          <telerik:GridBoundColumn   DataField="FormaPago" HeaderText="Forma Pago" UniqueName="FormaPago"
                                                           AllowFiltering="false"  ShowFilterIcon="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterControlWidth="100px" DataField="CondicionCredito" HeaderText="Cod.Cred."
                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
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

                                                        <telerik:GridBoundColumn   DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda"
                                                             AllowFiltering="false"  ShowFilterIcon="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn  DataField="AlmacenDespacho" HeaderText="Almacén" UniqueName="AlmacenDespacho"
                                                            AllowFiltering="false"  ShowFilterIcon="false">
                                                            <HeaderStyle Width="220px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn  DataField="Transportista" HeaderText="Emp.Trans" UniqueName="Transportista"
                                                            AllowFiltering="false"  ShowFilterIcon="false">
                                                            <HeaderStyle Width="220px" />
                                                        </telerik:GridBoundColumn>

                                                        
                                                    
        
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings>
                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" ></Scrolling>
                                                            <Selecting AllowRowSelect="True"></Selecting>
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
