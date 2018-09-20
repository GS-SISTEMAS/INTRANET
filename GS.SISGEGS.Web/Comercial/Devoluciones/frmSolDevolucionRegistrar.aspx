<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmSolDevolucionRegistrar.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Devoluciones.frmSolDevolucionRegistrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        <%--function CobrarFlete(sender, args) {
            var txtFlete = $find("<%=txtFlete.ClientID %>");
            if (args.get_checked()) {
                txtFlete.enable();
            }
            else {
                txtFlete.set_value(0);
                txtFlete.disable();
            }
        }--%>

        function CalcularDevolucion(rowIndex) {
            var grid = $find("<%= grdDocVentaDetalle.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var txtImporte = grid.get_masterTableView().get_dataItems()[rowIndex].findElement("txtImporte")
            var txtPrecio = grid.get_masterTableView().get_dataItems()[rowIndex].findElement("txtPrecio")
            var txtCantidad = grid.get_masterTableView().get_dataItems()[rowIndex].findElement("txtCantidad")


            var str = ((parseFloat(txtCantidad.value) * parseFloat(txtPrecio.value))).toFixed(4); // => '10.23'
            var number = Number(str); // => 10.23

            txtImporte.value = number;

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramDevolucion" runat="server">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="btnCobrarFlete">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="rapDevolucion" LoadingPanelID="ralpDevolucion"/>
                   <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
               </UpdatedControls>
           </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="rapDevolucion" LoadingPanelID="ralpDevolucion"/>
                   <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
               </UpdatedControls>
           </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAprobar">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="rapDevolucion" LoadingPanelID="ralpDevolucion"/>
                   <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
               </UpdatedControls>
           </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpDevolucion" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapDevolucion" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Solicitud de devolución"></asp:Label>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="85%">
                    <Rows>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblMotivo" runat="server" Text="Motivo" CssClass="etiqueta"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="cboMotivo" runat="server" DataValueField="idDevolucionMotivo" DataTextField="nombreDevolucionMotivo"
                                                Width="100%" EmptyMessage="Seleccionar motivo">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6"></div>
                                <div class="col-md-2">
                                    <telerik:RadTextBox ID="txtNroDocumento" runat="server" Label="Nro." LabelWidth="20%" Width="100%" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblAlmacen" runat="server" Text="Almacén" CssClass="etiqueta"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <telerik:RadComboBox ID="cboAlmacen" runat="server" DataValueField="ID_AlmacenAnexo" DataTextField="AlmacenAnexo"
                                                Width="100%" EmptyMessage="Seleccionar almacén">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-12 containerSubTitulo">
                                    <asp:Label ID="lblSubTitulo1" runat="server" CssClass="subTitulo" Text="Información primaria"></asp:Label>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="txtNroFactura" runat="server" Label="Nro.Doc." LabelWidth="20%" Width="100%" Enabled="false"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="txtCliente" runat="server" Label="Cliente" LabelWidth="20%" Width="100%" Enabled="false"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="txtZona" runat="server" Label="Zona" LabelWidth="20%" Width="100%" Enabled="false"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadDatePicker ID="dpFechaVenta" runat="server" DateInput-Label="Fecha venta" DateInput-DateFormat="dd/MM/yyyy" Width="100%" 
                                        DateInput-LabelWidth="40%" Enabled="false" DatePopupButton-Visible="false"></telerik:RadDatePicker>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-3">
                                    <telerik:RadDatePicker ID="dpFechaDevolucion" runat="server" DateInput-Label="Fecha Solicitud" Width="70%" 
                                        DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="50%" Enabled="false"></telerik:RadDatePicker>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="txtVendedor" runat="server" Label="Rep.Venta" LabelWidth="20%" Width="100%" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-12 containerSubTitulo">
                                    <asp:Label ID="lblSubTitulo2" runat="server" CssClass="subTitulo" Text="Información de venta"></asp:Label>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="txtNroGuiaCliente" runat="server" Label="Guía Cliente" LabelWidth="40%" Width="100%" MaxLength="20"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="txtNroGuiaTransportista" runat="server" Label="Guía Transport." LabelWidth="40%" Width="100%" MaxLength="20"></telerik:RadTextBox>
                                </div>
                               <%-- <div class="col-md-2">
                                    <telerik:RadButton ID="btnCobrarFlete" runat="server" Text="¿Se cobra flete?" ToggleType="CheckBox" ButtonType="ToggleButton" 
                                        AutoPostBack="false" OnClientCheckedChanged="CobrarFlete"></telerik:RadButton>
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadNumericTextBox ID="txtFlete" runat="server" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="," 
                                        Label="Flete" LabelWidth="40%" Width="100%" Enabled="false" Value="0"></telerik:RadNumericTextBox>
                                </div>--%>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-12 containerSubTitulo">
                                    <asp:Label ID="lblSubTitulo3" runat="server" CssClass="subTitulo" Text="Información del transportista"></asp:Label>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-4">
                                    <telerik:RadAutoCompleteBox ID="acbTransporte" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" AllowCustomEntry="true"
                                        DropDownHeight="200px" EmptyMessage="Buscar transportista" OnClientEntryAdding="OnClientEntryAddingHandler" DropDownWidth="300px" Label="Transportista" >
                                        <WebServiceSettings Method="Agenda_TransporteBuscar" Path="frmSolDevolucionRegistrar.aspx" />
                                    </telerik:RadAutoCompleteBox>
                                     <asp:Label ID="lblTransporte" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadDatePicker ID="dpFechaEnvio" runat="server" Width="100%" DateInput-Label="Fecha Envio" DateInput-LabelWidth="50%" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow Height="45%">
                            <Content>
                                <telerik:RadGrid ID="grdDocVentaDetalle" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%"
                                    OnItemCreated="grdDocVentaDetalle_ItemCreated" OnItemDataBound="grdDocVentaDetalle_ItemDataBound" ShowFooter="true">
                                    <MasterTableView ShowFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID_Amarre" UniqueName="ID_Amarre" HeaderText="ID_Amarre" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GuiaReferencia" UniqueName="GuiaReferencia" HeaderText="Guía">
                                                <HeaderStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Marca" UniqueName="Marca" HeaderText="Marca">
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Item" UniqueName="Item" HeaderText="Presentación">
                                                <HeaderStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cantidad" UniqueName="CantidadLote" HeaderText="Cantidad" DataFormatString="{0:F0}" Aggregate="Sum">
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CantLote" UniqueName="CantLote" HeaderText="Cant.Lote">
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn DataField="FechaVencimiento" UniqueName="FechaVencimiento" HeaderText="Fecha Vecm." DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn DataField="Precio" UniqueName="Precio" HeaderText="Prec.Unit." DataFormatString="${0:F2}">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Importe" UniqueName="Importe" HeaderText="Prec.Total" DataFormatString="${0:F2}" Aggregate="Sum">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="CantEdit" HeaderText="Cant.Dev.">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtCantidad" runat="server" NumberFormat-DecimalDigits="4" Width="100%">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="70px"/>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="PrecioEdit" HeaderText="Prec.Dev">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtPrecio" runat="server" NumberFormat-DecimalDigits="4" Width="100%">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="70px"/>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="ImporEdit" HeaderText="Impor.Dev">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtImporte" runat="server" NumberFormat-DecimalDigits="4" Enabled="false" Value="0" Width="100%">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="70px"/>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="40px"/>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-7">
                                    <telerik:RadTextBox ID="txtObservacion" runat="server" Label="Observación" Width="100%" LabelWidth="10%" 
                                        TextMode="MultiLine" Height="40px" MaxLength="400">
                                    </telerik:RadTextBox>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow>
                            <Content>
                                <div class="col-md-1">
                                    <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png"/>
                                    </telerik:RadButton>
                                </div>
                                <div class="col-md-1">
                                    <telerik:RadButton ID="btnAprobar" runat="server" Text="Aprobar" OnClick="btnAprobar_Click" Visible="false">
                                        <Icon PrimaryIconUrl="../../Images/Icons/sign-check-16.png"/>
                                    </telerik:RadButton>
                                </div>
                            </Content>
                        </telerik:LayoutRow>
                    </Rows>
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