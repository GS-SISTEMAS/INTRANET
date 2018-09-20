<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpItem.Master" AutoEventWireup="true" CodeBehind="frmOrdenCompraItem.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.Pedido.frmOrdenCompraItem" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(args);
                rWindow.close();
            });
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();
        }

        function CalcularImporte() {

            var txtStock = $find("<%= txtStock.ClientID %>");

            var txtPrecio = $find("<%= txtPrecio.ClientID %>");
            var txtCantidad = $find("<%= txtCantidad.ClientID %>");
            var txtImporte = $find("<%= txtImporte.ClientID %>");
            var txtPrecioInicial = $find("<%= PrecioInicial.ClientID %>");
            var txtDescuento = $find("<%= txtDescuento.ClientID %>");
            var txtDescuentoInicial = $find("<%= DescuentoInicial.ClientID %>");
            var hfCostoUnitario = document.getElementById('<%= hfCostoUnitario.ClientID%>');
            var txtRentabilidad = $find("<%= txtRentabilidad.ClientID %>");

            var precioFinal = txtPrecio.get_textBoxValue().replace(',', '');
            var cantidad = txtCantidad.get_textBoxValue();
            txtCantidad.set_value(Math.round(parseFloat(cantidad)));
            txtStock.set_value(Math.round(parseFloat(cantidad)));

            cantidad = txtCantidad.get_textBoxValue();
            var precioInicial = txtPrecioInicial.get_textBoxValue();
            var descuentoFinal = txtDescuento.get_textBoxValue();
            var descuentoInicial = txtDescuentoInicial.get_textBoxValue();

            if (parseFloat(precioInicial) > parseFloat(precioFinal) || parseFloat(precioFinal) < 0) {
                txtPrecio.set_value(parseFloat(precioInicial));
                precioFinal = precioInicial;
            }

            if (parseFloat(descuentoInicial) < parseFloat(descuentoFinal) || parseFloat(descuentoFinal) < 0) {
                txtDescuento.set_value(parseFloat(descuentoInicial));
                descuentoFinal = descuentoInicial;
            }
            
            var importe = ((1 - parseFloat(descuentoFinal) / 100) * parseFloat(precioFinal)) * parseFloat(cantidad);
            txtImporte.set_value(importe);
            if (precioFinal == 0 || precioFinal == 0)
                txtRentabilidad.set_value(0);
            else
                txtRentabilidad.set_value(parseFloat((precioFinal - hfCostoUnitario.value) * 100 / precioFinal));
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramPedidoItem" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoItem" LoadingPanelID="ralpPedidoItem" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPedidoItem" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="pnlPedidoItem" runat="server">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum4">
                <telerik:RadTextBox ID="txtCodigo" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblKardex" runat="server" Text="Kardex" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadTextBox ID="txtKardex" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblDescripcion" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <telerik:RadTextBox ID="txtNombre" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblPrecio" runat="server" Text="Precio" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtPrecio" runat="server" Type="Number" NumberFormat-DecimalDigits="2" MinValue="0"
                     MaxLength="9" Width="100%" onblur="CalcularImporte();">
                </telerik:RadNumericTextBox>
            </div>
            <div class="colum1">
                <asp:Label ID="lblMonedaPrecio" runat="server" CssClass="etiqueta"></asp:Label>
                <asp:Label ID="idMoneda" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="colum2">
                <asp:Label ID="lblFactor" runat="server" Text="Factor" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadTextBox ID="txtFactor" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
            <div class="colum1">
                <asp:Label ID="lblUnidadFactor" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Type="Number" NumberFormat-DecimalDigits="0" MinValue="0"
                    NumberFormat-GroupSeparator="" MaxLength="6" Width="100%" onblur="CalcularImporte();">
                </telerik:RadNumericTextBox>
            </div>
            <div class="colum1">
                <asp:Label ID="lblUnidadCantidad" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <asp:Label ID="lblStock" runat="server" Text="Stock" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtStock" runat="server" Type="Number" NumberFormat-DecimalDigits="0" Width="100%" Enabled="true">
                </telerik:RadNumericTextBox>
            </div>
            <div class="colum1">
                <asp:Label ID="lblUnidadStock" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblImporte" runat="server" Text="Importe" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtImporte" runat="server" Type="Number" NumberFormat-DecimalDigits="2" Width="100%" ReadOnly="true">
                </telerik:RadNumericTextBox>
            </div>
            <div class="colum1">
                <asp:Label ID="lblMonedaImporte" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <asp:Label ID="lblDescuento" runat="server" Text="Descuento" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtDescuento" runat="server" Type="Number" NumberFormat-DecimalDigits="0" Width="100%" 
                    MaxLength="2" MinValue="0"  onblur="CalcularImporte();">
                </telerik:RadNumericTextBox>
            </div>
            <div class="colum1">
                <asp:Label ID="lblPorcentaje" runat="server" CssClass="etiqueta" Text="%"></asp:Label>
            </div>            
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblRentabilidad" runat="server" Text="Rentabilidad" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtRentabilidad" runat="server" Type="Number" NumberFormat-DecimalDigits="2" Width="100%" ReadOnly="true">
                </telerik:RadNumericTextBox>
                <asp:HiddenField ID="hfCostoUnitario" runat="server" />
            </div>
            <div class="colum1"> 
                <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="%"></asp:Label>  
            </div> 
            <div class="colum2">
                <asp:Label ID="Label1" runat="server" Text="TC" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtTC" runat="server" Type="Number" NumberFormat-DecimalDigits="2" Width="100%" ReadOnly="true">
                </telerik:RadNumericTextBox>
               
            </div>
            <div class="colum1">   
            </div> 
        </div>

        <div class="fila">
            <div class="colum10">
                <telerik:RadAutoCompleteBox ID="acbCCosto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                    DropDownHeight="150px" EmptyMessage="Buscar Centro Costos" AllowCustomEntry="true"  Label="CCostos" LabelWidth="20%" DropDownWidth="70%">
                    <WebServiceSettings Method="Agenda_BuscarCCosto" Path="frmOrdenCompraItem.aspx" />
                </telerik:RadAutoCompleteBox>
            </div>
        </div>

         <div class="fila">
            <div class="colum10">
                    <asp:RadioButtonList ID="rbtParcial" runat="server" Font-Size="8pt" Width="80px">
                            <asp:ListItem Value="0" Selected="True">Total</asp:ListItem>
                            <asp:ListItem Value="1">Parcial</asp:ListItem>
                    </asp:RadioButtonList>
            </div>
        </div>


         <div class="fila">
            <div class="colum3">
                <telerik:RadDatePicker ID="dpFechaParcial" runat="server" DateInput-ReadOnly="true" Width="200px" Visible="false">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
            </div>

            <div class="colum2">
                <asp:Label ID="Label3" runat="server" Text="Cantidad" CssClass="etiqueta"></asp:Label>
            </div>

            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtCantidadParcial" runat="server" Type="Number" NumberFormat-DecimalDigits="2" Width="100%" ReadOnly="true"  onblur="CalcularImporte();" >
                </telerik:RadNumericTextBox>
            </div>

            <div class="colum3">
                <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" Visible="false" >
                    <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                </telerik:RadButton>
            </div>
         </div>

        <div class="fila">
             <div class="colum10">
                    <telerik:RadGrid ID="grdEntregas" runat="server" AllowMultiRowSelection="false" Visible="false"
                    AutoGenerateColumns="False" Height="200px"  Width="100%"
                    AllowSorting="True" 
                    OnItemDataBound="grdVencidos_ItemDataBound1"
                    OnNeedDataSource="grdVencidos_NeedDataSource" >
                    <MasterTableView TableLayout="Fixed"
                        AllowMultiColumnSorting="true" >
                        <Columns>

                            <telerik:GridBoundColumn DataField="correlativo" HeaderText="N°" HeaderStyle-Width="30px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" HeaderStyle-Width="100px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="precio" HeaderText="Precio" HeaderStyle-Width="80px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" HeaderStyle-Width="80px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" HeaderStyle-Width="80px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>

                      <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                     </ClientSettings>
                     <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>
            </div>
           </div>


        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblObservacion" runat="server" Text="Observación" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <telerik:RadTextBox ID="txtObservacion" runat="server" Width="100%" TextMode="MultiLine" Height="40px"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" UseSubmitBehavior="false">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum2">
                <telerik:RadTextBox ID="PrecioInicial" runat="server" style="visibility:hidden" Width="0%"></telerik:RadTextBox>
                <telerik:RadTextBox ID="txtCorrelativo" runat="server" style="visibility:hidden" Width="0%"></telerik:RadTextBox>
                <telerik:RadTextBox ID="txtCantidadRestante" runat="server" style="visibility:hidden" Width="0%"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <telerik:RadTextBox ID="DescuentoInicial" runat="server" style="visibility:hidden" Width="0%"></telerik:RadTextBox>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>