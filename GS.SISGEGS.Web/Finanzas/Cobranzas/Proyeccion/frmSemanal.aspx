<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpSM.Master" AutoEventWireup="true" CodeBehind="frmSemanal.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.Proyeccion.frmSemanal" %>

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
                $find("<%= ramUsuarioMngEdit.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramUsuarioMngEdit.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGridProyectado(args);
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

            <telerik:AjaxSetting AjaxControlID="grdCobranza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuarioMngEdit" LoadingPanelID="ralpUsuarioMngEdit"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ramUsuarioMngEdit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCobranza" LoadingPanelID="ralpUsuarioMngEdit"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuarioMngEdit" runat="server"  ></telerik:RadAjaxLoadingPanel>



    <telerik:RadAjaxPanel ID="rapUsuarioMngEdit" runat="server" Width="100%">
                <div class="fila containerSubTitulo">
                  <div class="colum10" >
                       <asp:Label ID="Label1" runat="server" Text="Agregar planificación : " CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
                <div class="fila">
                    <div class="colum3">
                        <asp:Label ID="lblDocumento" runat="server" Text="" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum4">
                         <asp:Label ID="lblNumero" runat="server" Text="" CssClass="etiqueta"></asp:Label>
                    </div>
                     <div class="colum1">
                        <asp:Label ID="lblMoneda" runat="server" Text="" CssClass="etiqueta" ></asp:Label>
                    </div>
                    <div class="colum2">
                        <asp:Label ID="lblImporte" runat="server" Text="" CssClass="etiqueta" ></asp:Label>
                    </div>
                    
                </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo: " CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum3">
                        <telerik:RadMonthYearPicker ID="rmyReporte" runat="server" Width="100%" Enabled="false">
                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                        </telerik:RadMonthYearPicker>
                    </div>
                    <div class="colum2">
                        <asp:Label ID="Label3" runat="server" Text="Semana: " CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum3">
                        <telerik:RadComboBox ID="cboSemana" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged = "OnSelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum2">
                        <asp:Label ID="Label2" runat="server" Text="Importe: " CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum1"    >
                        <asp:Label ID="lblMonedaImporte" runat="server" Text="" CssClass="etiqueta" ></asp:Label>
                    </div>
                    <div class="colum2">
                         <asp:TextBox ID="txtImporte" runat="server"  Width="100%"  ></asp:TextBox>
                        
                    </div>
                       <div class="colum5">
                     
                    </div>


                </div>
                <div class="fila">
                    <div class="colum10">
                        <asp:Label ID="lblMaximo" runat="server" Text=""  ></asp:Label>
                    </div>
                </div>
                <div class="fila">
                   <div class="colum2">
                            <telerik:RadButton ID="btnAgregar" runat="server" Text="Guardar" OnClick="btnAgregar_Click">
                                <Icon PrimaryIconUrl="~/Images/Icons/floppy-16.png" />
                            </telerik:RadButton>
                   </div>
                    <div class="colum5">
                        <asp:Label ID="Label5" runat="server" Text=""  Width="100%"  ></asp:Label>
                    </div>
                    <div class="colum2">
                        <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" > 
                              <Icon PrimaryIconUrl="~/Images/Icons/delete-16.png" />
                        </telerik:RadButton>
                    </div>

                </div>
                <div class="fila containerSubTitulo">
                  <div class="colum10" >
                  <asp:Label ID="Label11" runat="server" Text="Cobranza:"  CssClass="subTitulo">
                  </asp:Label>
                 </div>
              </div>

           <div class="fila">
             <div class="colum10">
                    <telerik:RadGrid ID="grdCobranza" runat="server" 
                    AutoGenerateColumns="False" Height="160px"  Width="100%"
                    AllowSorting="True" ShowFooter="true"
                     OnNeedDataSource="grdCobranza_NeedDataSource" 
                     OnSelectedIndexChanged="grdCobranza_selectedindexchanged" 
                        OnItemCommand="grdCobranza_ItemCommand"
                        >
                    <MasterTableView TableLayout="Fixed" DataKeyNames="id_semana" >
                        <Columns>

                                <telerik:GridTemplateColumn HeaderText="Elim.">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idProyectado") %>'
                                                ImageUrl="~/Images/Icons/trashcan-16.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                  </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="id_semana" HeaderText="Semana" HeaderStyle-Width="80px" AllowSorting="false"
                                    Aggregate="Count" FooterText="Total:">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" HeaderStyle-Width="100px" AllowSorting="false"
                                    Aggregate="Sum" FooterText=""  >
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="fechaCreacion" HeaderText="Creación" HeaderStyle-Width="200px" AllowSorting="false"  >
                                </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="fechaModificacion" HeaderText="Modificación" HeaderStyle-Width="200px" AllowSorting="false"  >
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
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
