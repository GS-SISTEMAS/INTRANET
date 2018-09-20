<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpSXX.Master" AutoEventWireup="true" CodeBehind="frmHistoricoGestion.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.Proyeccion.frmHistoricoGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

          function TextChanged(sender, e) {
            dateVar = new Date();ñ

            if (sender.value != "")
                dateVar.setDate(dateVar.getDate() + parseInt(sender.value));
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramUsuarioMngEdit.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramUsuarioMngEdit.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramUsuarioMngEdit" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuarioMngEdit" LoadingPanelID="ralpUsuarioMngEdit"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdVencidos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuarioMngEdit" LoadingPanelID="ralpUsuarioMngEdit"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ramUsuarioMngEdit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVencidos" LoadingPanelID="ralpUsuarioMngEdit"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuarioMngEdit" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapUsuarioMngEdit" runat="server" Width="100%">
              <div class="fila containerSubTitulo">
                  <div class="colum10">
                       <asp:Label ID="Label1" runat="server" Text="Agregar gestión: " CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo: " CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum3">
                        <telerik:RadMonthYearPicker ID="rmyReporte" runat="server" Width="100%">
                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                        </telerik:RadMonthYearPicker>
                    </div>
                    <div class="colum2">
                        <asp:Label ID="Label3" runat="server" Text="Semana: " CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum3">
                        <telerik:RadComboBox ID="cboSemana" runat="server" Width="100%">
                        </telerik:RadComboBox>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="Label18" runat="server" Text="Estado:" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum8">
                        <telerik:RadComboBox ID="cboEstado" runat="server" Width="100%"></telerik:RadComboBox>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum5">
                        <asp:Label ID="Label2" runat="server" Text="Observación: " CssClass="etiqueta"></asp:Label>
                    </div>
                     <div class="colum5">
                        <asp:Label ID="Label4" runat="server" Text="Documentos Pronosticados: " CssClass="etiqueta"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                   <div class="colum5">
                        <telerik:RadTextBox ID="txtObservacion" runat="server" Width="100%" TextMode="MultiLine" Height="150px" MaxLength="1000"></telerik:RadTextBox>               
                    </div>
                    <div class="colum5">
                        
                          <telerik:RadGrid ID="grdDocumentos" runat="server" 
                    AutoGenerateColumns="False" Height="150px"  Width="100%"
                    AllowSorting="True" ShowFooter="true"
                 
                     >
                    <MasterTableView TableLayout="Fixed" >
                        <Columns>

                            <telerik:GridBoundColumn DataField="TablaOrigen" HeaderText="TablaOrigen"  Display="false" HeaderStyle-Width="50px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OpOrigen" HeaderText="OpOrigen" Display="false" HeaderStyle-Width="50px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Periodo" HeaderText="Periodo" Display="false" HeaderStyle-Width="50px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="CheckColumn" HeaderText="Sel." HeaderStyle-Width="30px" AllowSorting="true" AllowFiltering="false">
                                                          <ItemTemplate>
                                                            <asp:CheckBox ID="Check" runat="server"  />
                                                          </ItemTemplate>
                             </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="Tipo" HeaderStyle-Width="90px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroDocumento" HeaderText="NroDoc." HeaderStyle-Width="95px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Signo" HeaderText="Signo" HeaderStyle-Width="30" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                                  <telerik:GridBoundColumn DataField="ImportePendiente" HeaderText="Pendiente" HeaderStyle-Width="90" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                                  <telerik:GridBoundColumn DataField="Proyectado" HeaderText="Pronostico" HeaderStyle-Width="90" AllowSorting="false"  >
                            </telerik:GridBoundColumn>


                        </Columns>
                    </MasterTableView>

                 <ClientSettings EnablePostBackOnRowClick="true" >
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                 
                </telerik:RadGrid>
                                       
                    </div>

                </div>
                <div class="fila">
                   <div class="colum2">
                            <telerik:RadButton ID="btnAgregar" runat="server" Text="Guardar" OnClick="btnAgregar_Click">
                                <Icon PrimaryIconUrl="~/Images/Icons/floppy-16.png" />
                            </telerik:RadButton>
                   </div>
                
                      <div class="colum2">
                         <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" Visible="true" ToolTip="CERRAR"> 
                            <Icon PrimaryIconUrl="~/Images/Icons/delete-16.png" />
                        </telerik:RadButton>
                      </div>

                        <div class="colum6">
                          <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </div>

                </div>

               <div class="fila containerSubTitulo">
                  <div class="colum10" >
                  <asp:Label ID="Label11" runat="server" Text="Seguimiento de gestión: "  CssClass="subTitulo">
                  </asp:Label>
                 </div>
              </div>

           <div class="fila">
             <div class="colum10">
                    <telerik:RadGrid ID="grdVencidos" runat="server" 
                    AutoGenerateColumns="False" Height="150px"  Width="100%"
                    AllowSorting="True" ShowFooter="true"
                    OnSelectedIndexChanged="grdVencidos_selectedindexchanged" >
                    <MasterTableView TableLayout="Fixed" >
                        <Columns>


                            <telerik:GridBoundColumn DataField="TablaOrigen" HeaderText="TablaOrigen" Display="false" HeaderStyle-Width="50px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="OpOrigen" HeaderText="OpOrigen" Display="false" HeaderStyle-Width="50px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                          

                            <telerik:GridBoundColumn DataField="periodo" HeaderText="Periodo" HeaderStyle-Width="50px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="id_semana" HeaderText="Semana" HeaderStyle-Width="50px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="TipoDocumento"   HeaderStyle-Width="100px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn DataField="NroDocumento" HeaderText="NroDocumento"   HeaderStyle-Width="90px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NombreEstaus" HeaderText="Estado" HeaderStyle-Width="140px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="observacion" HeaderText="Observación" HeaderStyle-Width="250px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="fechaModificacion" HeaderText="Registro" HeaderStyle-Width="130px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>

                 <ClientSettings EnablePostBackOnRowClick="true" >
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                 
                </telerik:RadGrid>
            </div>
           </div>
            
        
        <div class="fila">
            <div class="colum10">
                  <asp:HiddenField ID="lblGrilla" runat="server" />
                 <asp:HiddenField ID="HiddenField1" runat="server" />
                  <asp:HiddenField ID="HiddenField2" runat="server" />
                 <asp:HiddenField ID="HiddenField3" runat="server" />
                 <asp:HiddenField ID="lblClaveUsuario" runat="server" />
                  <asp:HiddenField ID="lblCodigoUsuario" runat="server" />
                 <asp:HiddenField ID="lblIdUsuario" runat="server" />
            </div>

        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
          
        </div>
    </div>
</asp:Content>
