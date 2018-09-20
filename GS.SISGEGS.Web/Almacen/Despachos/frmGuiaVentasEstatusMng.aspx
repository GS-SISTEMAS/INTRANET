<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMS.Master" AutoEventWireup="true" CodeBehind="frmGuiaVentasEstatusMng.aspx.cs" Inherits="GS.SISGEGS.Web.Almacen.Despachos.frmGuiaVentasEstatusMng" %>
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
                $find("<%= ramGuiaVentasEstatusMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramGuiaVentasEstatusMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
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
    <telerik:RadAjaxManager ID="ramGuiaVentasEstatusMng" runat="server" OnAjaxRequest="ramGuiaVentasEstatusMng_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGuiaVentasEstatusMng" LoadingPanelID="ralpGuiaVentasEstatusMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="acbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGuiaVentasEstatusMng" LoadingPanelID="ralpGuiaVentasEstatusMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpGuiaVentasEstatusMng" runat="server" ZIndex="9999" IsSticky="true" Width="550px">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmGuiaVentasEstatusMng" runat="server" EnableShadow="true" Width="550px">
        <Windows>
            <telerik:RadWindow ID="rwGuiaVentasEstatusMng" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlGuiaVentasEstatusMng" runat="server" Width="550px">
        <telerik:RadTabStrip runat="server" ID="stripGuiaVentasEstatus" MultiPageID="pagesGuiaVentasEstatus" SelectedIndex="0" CssClass="fila" Width="550px">
            <Tabs>
                 <telerik:RadTab Text="Cliente" Selected="True"></telerik:RadTab>
                <telerik:RadTab Text="Rutas"></telerik:RadTab>
                <telerik:RadTab Text="Transporte"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="pagesGuiaVentasEstatus" SelectedIndex="0" Width="650px" Height="300px" ScrollBars="Vertical" CssClass="multicontainer">
             <telerik:RadPageView runat="server" ID="pageCliente" CssClass="tabcontainer" Width="650px">
              <div class="fila containerSubTitulo">
                  <div class="colum10">
                  <asp:Label ID="Label11" runat="server" Text="Datos Cliente" CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
              <div class="fila">
                  <div class="colum2">
                  <asp:Label ID="Label14" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                  </div>
                  <div class="colum2">
                      <telerik:RadTextBox ID="txtRUCCliente" runat="server" Enabled="false" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC">
                      </telerik:RadTextBox>
                      <asp:HiddenField ID="lblCodigoCliente" runat="server" />
                  </div>
                  <div class="colum1">
                      <asp:Label ID="Label15" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
                  </div>
                  <div class="colum5">
                      <telerik:RadTextBox ID="txtNombreCliente" runat="server" Enabled="false"  ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente">
                      </telerik:RadTextBox>
                  </div>
                 

              </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label18" runat="server" Text="Dirección:" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboFacturacionCliente" Enabled="false" ReadOnly="true" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label16" runat="server" Text="Despacho" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboDespacho" Enabled="false" ReadOnly="true" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
                <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label6" runat="server" Text="Transacción:" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                      <telerik:RadTextBox ID="txtTransaccion" runat="server" Enabled="false"  ReadOnly="true" Width="100%" EmptyMessage="Transacción">
                      </telerik:RadTextBox>
                     </div>
                 </div>

              <div class="fila containerSubTitulo">
                  <div class="colum10">
                  <asp:Label ID="Label12" runat="server" Text="Datos Fecha" CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label26" runat="server" Text="Fecha Emisión: " CssClass="etiqueta"></asp:Label>
                         <asp:HiddenField ID="lblUsuE" runat="server" />
                     </div>
                     <div class="colum2">
                         <telerik:RadDatePicker ID="dpFechaEmision" runat="server" DateInput-ReadOnly="true" Width="100%" Enabled="false">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                          </telerik:RadDatePicker>
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboHoraE" runat="server" Enabled="false"  Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label27" runat="server" Text="Horas" CssClass="etiqueta"></asp:Label>
                      
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboMinE" runat="server" Enabled="false" Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label28" runat="server" Text="Minutos" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboSegE" runat="server" Enabled="false" Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label29" runat="server" Text="Segundos" CssClass="etiqueta"></asp:Label>
                     </div>
              </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label23" runat="server" Text="Fecha Seguridad:" CssClass="etiqueta"></asp:Label>
                          <asp:HiddenField ID="lblUsuS" runat="server" />
                     </div>
                     <div class="colum2">
                         <telerik:RadDatePicker ID="dpFechaSeguridad" runat="server" DateInput-ReadOnly="true" Width="100%" >
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboHoraS" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label30" runat="server" Text="Horas" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboMinS" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label31" runat="server" Text="Minutos" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboSegS" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label32" runat="server" Text="Segundos" CssClass="etiqueta"></asp:Label>
                     </div>
                 </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label25" runat="server" Text="Fecha Cliente: " CssClass="etiqueta"></asp:Label>
                          <asp:HiddenField ID="lblUsuC" runat="server" />
                     </div>
                     <div class="colum2">
                         <telerik:RadDatePicker ID="dpFechaCliente" runat="server" DateInput-ReadOnly="true" Width="100%" >
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                     </div>
                     
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboHoraC" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>

                     <div class="colum1">
                         <asp:Label ID="Label33" runat="server" Text="Horas" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboMinC" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label34" runat="server" Text="Minutos" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum1">
                         <telerik:RadComboBox ID="cboSegC" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                     <div class="colum1">
                         <asp:Label ID="Label35" runat="server" Text="Segundos" CssClass="etiqueta"></asp:Label>
                     </div>

                 </div>

              <div class="fila">
                       <div class="colum2">
                             <asp:Label ID="Label17" runat="server" CssClass="etiqueta" Text="Vehículo: "></asp:Label>
                       </div>
                          <div class="colum8">
                            <telerik:RadComboBox ID="cboTransporte" runat="server" Width="100%" >
                            </telerik:RadComboBox>
                       </div>
                          <div class="colum1">
                       </div>
                 </div>

             </telerik:RadPageView>
             <telerik:RadPageView runat="server" ID="pageRuta" CssClass="tabcontainer" Width="650px">
                 <div class="fila containerSubTitulo">
                     <div class="colum10">
                     <asp:Label ID="lblSubTitulo" runat="server" Text="Datos Origen" CssClass="subTitulo">
                     </asp:Label>
                     </div>
                 </div>

                 <div class="fila">
                     <div class="colum2">
                       <asp:Label ID="lblRUC" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                    </div>
                     <div class="colum3">
                         <telerik:RadTextBox ID="txtRUCOrigen" runat="server" Enabled="false"  ReadOnly="true" Width="100%" EmptyMessage="Número de RUC"></telerik:RadTextBox><asp:HiddenField ID="lblCodigoOrigen" runat="server" />
                     </div>
                     <div class="colum5">
                         <div class="colum2">
                             <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
                         </div>
                         <div class="colum8">
                             <telerik:RadTextBox ID="txtOrigen" Enabled="false"  runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente"></telerik:RadTextBox>
                         </div>
                     </div>
                 </div>
                 <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="lblSucursalOrigen" runat="server" Text="Sucursal" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboSucursalOrigen" Enabled="false"  runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
                 <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label24" runat="server" CssClass="etiqueta" Text="Referencia: " Width="90px"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboReferenciaOrigen" Enabled="false"  runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
                 <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboFacturacionOrigen" Enabled="false"  runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
                 <div class="fila containerSubTitulo">
                     <div class="colum7">
                         <asp:Label ID="lblDatosGuiaVentasEstatus" runat="server" Text="Datos Destino" CssClass="subTitulo"></asp:Label>
                     </div>
                 </div>
                 <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label1" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum3">
                         <telerik:RadTextBox ID="txtRUCDestino" runat="server" Enabled="false"  ReadOnly="true" Width="100%" EmptyMessage="Número de RUC"></telerik:RadTextBox><asp:HiddenField ID="lblCodigoDestino" runat="server" />
                     </div>
                     <div class="colum5">
                         <div class="colum2">
                             <asp:Label ID="Label2" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum8">
                        <telerik:RadTextBox ID="txtDestino" runat="server" Enabled="false"  ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente"></telerik:RadTextBox></div></div></div><div class="fila"><div class="colum2">
                            <asp:Label ID="Label3" runat="server" Text="Sucursal" CssClass="etiqueta"></asp:Label></div>
                         <div class="colum8">
                             <telerik:RadComboBox ID="cboSucursalDestino" Enabled="false"  runat="server" Width="100%"></telerik:RadComboBox>
                         </div></div>
                 <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label4" runat="server" CssClass="etiqueta" Text="Referencia: " Width="90px"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboReferenciaDestino" Enabled="false"  runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
                 <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label5" runat="server" Text="Dirección:" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboFacturacionDestino"  Enabled="false"  runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
                 </div>
             </telerik:RadPageView>
             <telerik:RadPageView runat="server" ID="pageTransporte" CssClass="tabcontainer" Width="650px">
                <div class="fila containerSubTitulo">
                    <div class="colum10">
                        <asp:Label ID="lblSTTransporte" runat="server" Text="Datos Transporte" CssClass="subTitulo">
                        </asp:Label></div>

                </div>

                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="lblTransporte" runat="server" Text="Transporte: " CssClass="etiqueta"></asp:Label>

                    </div>
                    <div class="colum5">
                        <telerik:RadTextBox ID="txtTransporte" runat="server" Enabled="false"  EmptyMessage="Número Agenda" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                        <asp:HiddenField ID="lblCodigoTransportista" runat="server" /></div>
                    <div class="colum1">
                            <asp:Label ID="Label13" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                   </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtRUCTransporte" runat="server" Enabled="false"  ReadOnly="true" Width="100%" EmptyMessage="Número de RUC"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum10">
                        <asp:Label ID="Label21" runat="server" Text="Datos Chofer" CssClass="subTitulo">
                        </asp:Label></div>

                </div>
                
                <div class="fila">
                    <div class="colum2"><asp:Label ID="Label7" runat="server" Text="Chofer: " CssClass="etiqueta"></asp:Label>
                   </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtIDChofer" Enabled="false"  runat="server" EmptyMessage="Nombre Chofer" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum3">
                        <telerik:RadTextBox ID="txtChofer" runat="server" Enabled="false"  EmptyMessage="Nombre Chofer" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum1"><asp:Label ID="Label8" runat="server" Text="LIC." CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtLicencia" runat="server" Enabled="false"  ReadOnly="true" Width="100%" EmptyMessage="Número de Lic."></telerik:RadTextBox>
                       </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum10">
                        <asp:Label ID="Label22" runat="server" Text="Datos Vehiculó" CssClass="subTitulo">
                        </asp:Label></div>

                </div>

                <div class="fila"><div class="colum1"><asp:Label ID="Label9" runat="server" Text="Vehic." CssClass="etiqueta"></asp:Label>
                                  </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtVehiculoPlaca" runat="server" Enabled="false"  EmptyMessage="Datos Vehiculó" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtVehiculoMarca" runat="server" Enabled="false"  EmptyMessage="Datos Vehiculó" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum2">
                        <telerik:RadTextBox ID="txtVehiculoModelo" runat="server" Enabled="false"  EmptyMessage="Datos Vehiculó" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                    </div>
                    <div class="colum1"><asp:Label ID="Label10" runat="server" Text="Cert." CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum2"><telerik:RadTextBox ID="txtCertificado" runat="server" Enabled="false"   ReadOnly="true" Width="100%" EmptyMessage="Número de Cert.">
           </telerik:RadTextBox></div></div>

            </telerik:RadPageView>
        </telerik:RadMultiPage>

        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum2">
                <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/pencil-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum2">
                <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/sign-error-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum2">
                <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" Visible="true">
                    <Icon PrimaryIconUrl="../../Images/Icons/delete-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum2">
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

