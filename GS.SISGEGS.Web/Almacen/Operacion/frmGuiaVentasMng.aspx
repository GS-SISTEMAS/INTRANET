<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMS.Master" AutoEventWireup="true" CodeBehind="frmGuiaVentasMng.aspx.cs" Inherits="GS.SISGEGS.Web.Almacen.Operacion.frmGuiaVentasMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function TextChanged(sender, e) {
            dateVar = new Date();

            if (sender.value != "")
                dateVar.setDate(dateVar.getDate() + parseInt(sender.value));
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramGuiaVentasMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramGuiaVentasMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
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

    </script>
</asp:Content>
<asp:Content  ID="Content2" ContentPlaceHolderID="body" runat="server" >
    <telerik:RadAjaxManager ID="ramGuiaVentasMng" runat="server" OnAjaxRequest="ramGuiaVentasMng_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGuiaVentasMng" LoadingPanelID="ralpGuiaVentasMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="acbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGuiaVentasMng" LoadingPanelID="ralpGuiaVentasMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpGuiaVentasMng" runat="server" ZIndex="9999" IsSticky="true" Width="550px">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmGuiaVentasMng" runat="server" EnableShadow="true" Width="550px">
        <Windows>
            <telerik:RadWindow ID="rwGuiaVentasMng" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlGuiaVentasMng" runat="server" Width="550px">
        <telerik:RadTabStrip runat="server" ID="stripGuiaVentas" MultiPageID="pagesGuiaVentas" SelectedIndex="0" CssClass="fila" Width="550px">
            <Tabs>
                 <telerik:RadTab Text="Cliente" Selected="True"></telerik:RadTab>
                <telerik:RadTab Text="Rutas"></telerik:RadTab>
                <telerik:RadTab Text="Transporte"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="pagesGuiaVentas" SelectedIndex="0" Width="550px" Height="350px" ScrollBars="Vertical" CssClass="multicontainer">
         
             <telerik:RadPageView runat="server" ID="pageCliente" CssClass="tabcontainer" Width="550px">
              <div class="fila containerSubTitulo">
                  <div class="colum10">
                  <asp:Label ID="Label11" runat="server" Text="Datos Cliente" CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
              <div class="fila">
                  <div class="colum2">
                  <asp:Label ID="Label14" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                  </div>
                  <div class="colum3">
                      <telerik:RadTextBox ID="txtRUCCliente" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC">
                      </telerik:RadTextBox>
                      <asp:HiddenField ID="lblCodigoCliente" runat="server" /></div>
                  <div class="colum5">
                      <div class="colum2">
                      <asp:Label ID="Label15" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
                     </div>
                  <div class="colum8">
                      <telerik:RadTextBox ID="txtNombreCliente" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente">
                      </telerik:RadTextBox>
                  </div>
                  </div>

              </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label18" runat="server" Text="Dirección:" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboFacturacionCliente" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label16" runat="server" Text="Despacho" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboDespacho" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
              <div class="fila containerSubTitulo">
                  <div class="colum10">
                  <asp:Label ID="Label12" runat="server" Text="Datos Fecha" CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
                               <div class="fila">
                     <div class="colum3">
                         <asp:Label ID="Label26" runat="server" Text="Fecha Emisión: " CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum7">
                         <telerik:RadDatePicker ID="dpFechaEmision" runat="server" DateInput-ReadOnly="true" Width="200px" Enabled="false">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                     </div>
                 </div>

              <div class="fila">
                     <div class="colum3">
                         <asp:Label ID="Label23" runat="server" Text="Fecha Despacho: " CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum7">
                         <telerik:RadDatePicker ID="dpFechaDespacho" runat="server" DateInput-ReadOnly="true" Width="200px" >
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                     </div>
                 </div>
              <div class="fila">
                     <div class="colum3">
                         <asp:Label ID="Label25" runat="server" Text="Fecha Traslado: " CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum7">
                         <telerik:RadDatePicker ID="dpFechaTraslado" runat="server" DateInput-ReadOnly="true" Width="200px" >
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                     </div>
                 </div>
             </telerik:RadPageView>
            
              <telerik:RadPageView runat="server" ID="pageRuta" CssClass="tabcontainer" Width="550px"><div class="fila containerSubTitulo"><div class="colum10"><asp:Label ID="lblSubTitulo" runat="server" Text="Datos Origen" CssClass="subTitulo"></asp:Label></div></div><div class="fila"><div class="colum2"><asp:Label ID="lblSelOrigen" runat="server" Text="Selec. Origen" CssClass="etiqueta" Width="100px"></asp:Label></div><div class="colum6"><telerik:RadAutoCompleteBox ID="acbOrigen" runat="server" Width="250px" TextSettings-SelectionMode="Single" InputType="Text"
                            DropDownWidth="300px" DropDownHeight="150px" EmptyMessage="Buscar Origen" AllowCustomEntry="true"><WebServiceSettings Method="Agenda_BuscarOrigen" Path="frmGuiaVentasMng.aspx" /></telerik:RadAutoCompleteBox></div><div class="colum1"></div><div class="colum1"><telerik:RadButton ID="btnBuscarOrgen" runat="server" OnClick="btnBuscarOrigen_Click" Text="Selec."><Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/></telerik:RadButton></div></div><div class="fila"><div class="colum2"><asp:Label ID="lblRUC" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label></div><div class="colum3"><telerik:RadTextBox ID="txtRUCOrigen" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC"></telerik:RadTextBox><asp:HiddenField ID="lblCodigoOrigen" runat="server" /></div><div class="colum5"><div class="colum2"><asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label></div><div class="colum8"><telerik:RadTextBox ID="txtOrigen" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente"></telerik:RadTextBox></div></div></div><div class="fila"><div class="colum2"><asp:Label ID="lblSucursalOrigen" runat="server" Text="Sucursal" CssClass="etiqueta"></asp:Label></div><div class="colum8"><telerik:RadComboBox ID="cboSucursalOrigen" runat="server" Width="100%"></telerik:RadComboBox></div></div><div class="fila"><div class="colum2"><asp:Label ID="Label24" runat="server" CssClass="etiqueta" Text="Referencia: " Width="90px"></asp:Label></div><div class="colum8"><telerik:RadComboBox ID="cboReferenciaOrigen" runat="server" Width="100%"></telerik:RadComboBox></div></div><div class="fila"><div class="colum2"><asp:Label ID="lblDireccion" runat="server" Text="Dirección:" CssClass="etiqueta"></asp:Label></div><div class="colum8"><telerik:RadComboBox ID="cboFacturacionOrigen" runat="server" Width="100%"></telerik:RadComboBox></div></div><div class="fila containerSubTitulo"><div class="colum7"><asp:Label ID="lblDatosGuiaVentas" runat="server" Text="Datos Destino" CssClass="subTitulo"></asp:Label></div></div><div class="fila"><div class="colum2"><asp:Label ID="Label6" runat="server" Text="Selec. Destino" CssClass="etiqueta" Width="100px"></asp:Label></div><div class="colum6"><telerik:RadAutoCompleteBox ID="acbDestino" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                            DropDownWidth="300px" DropDownHeight="150px" EmptyMessage="Buscar Destino" AllowCustomEntry="true"><WebServiceSettings Method="Agenda_BuscarDestino" Path="frmGuiaVentasMng.aspx" /></telerik:RadAutoCompleteBox></div><div class="colum1"></div><div class="colum1"><telerik:RadButton ID="btnBuscarDestino" runat="server" OnClick="btnBuscarDestino_Click" Text="Selec."><Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/></telerik:RadButton></div></div><div class="fila"><div class="colum2"><asp:Label ID="Label1" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label></div><div class="colum3"><telerik:RadTextBox ID="txtRUCDestino" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC"></telerik:RadTextBox><asp:HiddenField ID="lblCodigoDestino" runat="server" /></div><div class="colum5"><div class="colum2"><asp:Label ID="Label2" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label></div><div class="colum8"><telerik:RadTextBox ID="txtDestino" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente"></telerik:RadTextBox></div></div></div><div class="fila"><div class="colum2"><asp:Label ID="Label3" runat="server" Text="Sucursal" CssClass="etiqueta"></asp:Label></div><div class="colum8"><telerik:RadComboBox ID="cboSucursalDestino" runat="server" Width="100%"></telerik:RadComboBox></div></div><div class="fila"><div class="colum2"><asp:Label ID="Label4" runat="server" CssClass="etiqueta" Text="Referencia: " Width="90px"></asp:Label></div><div class="colum8"><telerik:RadComboBox ID="cboReferenciaDestino" runat="server" Width="100%"></telerik:RadComboBox></div></div><div class="fila"><div class="colum2"><asp:Label ID="Label5" runat="server" Text="Dirección:" CssClass="etiqueta"></asp:Label></div><div class="colum8"><telerik:RadComboBox ID="cboFacturacionDestino" runat="server" Width="100%"></telerik:RadComboBox></div></div></telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="pageTransporte" CssClass="tabcontainer" Width="550px">
                <div class="fila containerSubTitulo">
                    <div class="colum10">
                        <asp:Label ID="lblSTTransporte" runat="server" Text="Datos Transporte" CssClass="subTitulo">
                        </asp:Label></div>

                </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="Label17" runat="server" Text="Selec. Transp." CssClass="etiqueta" Width="100px"></asp:Label>

                    </div>
                    <div class="colum6">
                        <telerik:RadAutoCompleteBox ID="acbTransporte" runat="server" AllowCustomEntry="true" DropDownHeight="200px" 
                     DropDownWidth="300px"  EmptyMessage="Buscar transportista" InputType="Text" 
                     OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="100%">
                        <WebServiceSettings Method="Agenda_TransporteBuscar" Path="frmGuiaVentasMng.aspx" />
                   </telerik:RadAutoCompleteBox></div>
                    <div class="colum1">
                    </div>
                    <div class="colum1">
                        <telerik:RadButton ID="btnBuscarTrnasporte" runat="server" OnClick="btnBuscarTransporte_Click" Text="Selec.">
                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/></telerik:RadButton>

                    </div>

                </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="lblTransporte" runat="server" Text="Transporte: " CssClass="etiqueta"></asp:Label>

                    </div>
                    <div class="colum5">
                        <telerik:RadTextBox ID="txtTransporte" runat="server" EmptyMessage="Número Agenda" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                        <asp:HiddenField ID="lblCodigoTransportista" runat="server" /></div><div class="colum1">
                            <asp:Label ID="Label13" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                   </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtRUCTransporte" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum10">
                        <asp:Label ID="Label21" runat="server" Text="Datos Chofer" CssClass="subTitulo">
                        </asp:Label></div>

                </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="Label19" runat="server" Text="Selec. Chofer" CssClass="etiqueta" Width="100px"></asp:Label>

                    </div>
                    <div class="colum6">
                        <telerik:RadAutoCompleteBox ID="acbChofer" runat="server" AllowCustomEntry="true" DropDownHeight="200px" 
                          DropDownWidth="300px"  EmptyMessage="Buscar Chofer" InputType="Text" 
                          OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="100%">
                        <WebServiceSettings Method="Agenda_ChoferBuscar" Path="frmGuiaVentasMng.aspx" />
                   </telerik:RadAutoCompleteBox>
                        <asp:HiddenField ID="lblCodigoChofer" runat="server" />
                    </div>
                    <div class="colum1">
                    </div>
                    <div class="colum1">
                        <telerik:RadButton ID="btnBuscarChofer" runat="server" OnClick="btnBuscarChofer_Click" Text="Selec.">
                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/></telerik:RadButton>

                    </div>

                </div>
                <div class="fila">
                    <div class="colum2"><asp:Label ID="Label7" runat="server" Text="Chofer: " CssClass="etiqueta"></asp:Label>
                   </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtIDChofer" runat="server" EmptyMessage="Nombre Chofer" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum3">
                        <telerik:RadTextBox ID="txtChofer" runat="server" EmptyMessage="Nombre Chofer" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum1"><asp:Label ID="Label8" runat="server" Text="LIC." CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtLicencia" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de Lic."></telerik:RadTextBox>
                       </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum10">
                        <asp:Label ID="Label22" runat="server" Text="Datos Vehiculó" CssClass="subTitulo">
                        </asp:Label></div>

                </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="Label20" runat="server" Text="Selec. Vehic." CssClass="etiqueta" Width="100px"></asp:Label>

                    </div>
                    <div class="colum6">
                        <telerik:RadAutoCompleteBox ID="acbVehiculo" runat="server" AllowCustomEntry="true" DropDownHeight="200px" 
                     DropDownWidth="300px"  EmptyMessage="Buscar Vehículo" InputType="Text" 
                     OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="100%">
                        <WebServiceSettings Method="Agenda_VehiculoBuscar" Path="frmGuiaVentasMng.aspx" />
                   </telerik:RadAutoCompleteBox>
                         <asp:HiddenField ID="lblCodigoVehiculo" runat="server" />
                    </div>
                    <div class="colum1">
                    </div>
                    <div class="colum1">
                        <telerik:RadButton ID="btnBuscarVehiculo" runat="server" OnClick="btnBuscarVehiculo_Click" Text="Selec.">
                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/></telerik:RadButton>

                    </div>

                </div>
                <div class="fila"><div class="colum1"><asp:Label ID="Label9" runat="server" Text="Vehic." CssClass="etiqueta"></asp:Label>
                                  </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtVehiculoPlaca" runat="server" EmptyMessage="Datos Vehiculó" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtVehiculoMarca" runat="server" EmptyMessage="Datos Vehiculó" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtVehiculoModelo" runat="server" EmptyMessage="Datos Vehiculó" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum1"><asp:Label ID="Label10" runat="server" Text="Cert." CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum2"><telerik:RadTextBox ID="txtCertificado" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de Cert.">
           </telerik:RadTextBox></div></div>

            </telerik:RadPageView>
        </telerik:RadMultiPage>

        <div class="fila">
            <div class="colum1">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum6">
                <br />
            </div>
            <div class="colum3">
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
             <asp:HiddenField ID="lblOp" runat="server" />
        </div>
    </div>
</asp:Content>

