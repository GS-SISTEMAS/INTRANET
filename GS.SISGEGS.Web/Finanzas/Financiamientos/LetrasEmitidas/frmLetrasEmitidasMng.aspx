<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMSS.Master" AutoEventWireup="true" CodeBehind="frmLetrasEmitidasMng.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Financiamientos.LetrasEmitidas.frmLetrasEmitidasMng" %>
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
                $find("<%= ramLetrasEmitidasMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramLetrasEmitidasMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
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
    <telerik:RadAjaxManager ID="ramLetrasEmitidasMng" runat="server" OnAjaxRequest="ramLetrasEmitidasMng_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlLetrasEmitidasMng" LoadingPanelID="ralpLetrasEmitidasMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="acbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlLetrasEmitidasMng" LoadingPanelID="ralpLetrasEmitidasMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpLetrasEmitidasMng" runat="server" ZIndex="9999" IsSticky="true" Width="650px">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmLetrasEmitidasMng" runat="server" EnableShadow="true" Width="750px">
        <Windows>
            <telerik:RadWindow ID="rwLetrasEmitidasMng" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlLetrasEmitidasMng" runat="server" Width="750px">
              <div class="fila containerSubTitulo">
                  <div class="colum10">
                  <asp:Label ID="Label7" runat="server" Text="Datos de financiamiento:" CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
              <div class="fila">
                  <div class="colum2">
                  <asp:Label ID="Label8" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                  </div>
                  <div class="colum3">
                      <telerik:RadTextBox ID="txtRUC" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC">
                      </telerik:RadTextBox>
                      <asp:HiddenField ID="lblidAgenda" runat="server" />
                  </div>
                  <div class="colum5">
                      <div class="colum2">
                      <asp:Label ID="Label9" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
                     </div>
                  <div class="colum8">
                      <telerik:RadTextBox ID="txtClienteNombre" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente">
                      </telerik:RadTextBox>
                  </div>
                  </div>

              </div>
              <div class="fila">
                     <div class="colum2">
                         <asp:Label ID="Label10" runat="server" Text="Dirección:" CssClass="etiqueta"></asp:Label>
                     </div>
                     <div class="colum8">
                         <telerik:RadComboBox ID="cboDireccion" runat="server" Width="100%"></telerik:RadComboBox>
                     </div>
               </div>
              <div class="fila">
                         <div class="colum2">
                             <asp:Label ID="Label20" runat="server" Text="Fecha Canje: " CssClass="etiqueta"></asp:Label>
                         </div>
                         <div class="colum6">
                             <telerik:RadDatePicker ID="dpFechaCanje" runat="server" DateInput-ReadOnly="true" Width="200px" Enabled="false">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                    </DateInput>
                                </telerik:RadDatePicker>
                         </div>
                        <div class="colum2">
                             <asp:ImageButton ID="btnExpPDFDetalle" runat="server"  Visible="false" Height="30px" ImageUrl="~/Images/Icons/24_pdf.png" OnClick="btnExpPDFDetalle_Click" Width="30px" />
                        </div>
                   </div>

        <telerik:RadTabStrip runat="server" ID="stripLetrasEmitidas" MultiPageID="pagesLetrasEmitidas" SelectedIndex="0" CssClass="fila" Width="680px" OnTabClick="stripLetrasEmitidas_TabClick">
            <Tabs>
                 <telerik:RadTab Text="Documentos" Selected="True"></telerik:RadTab>
                <telerik:RadTab Text="Letras" ></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="pagesLetrasEmitidas" SelectedIndex="0" Width="650px" Height="250px" ScrollBars="Vertical" CssClass="multicontainer">
             <telerik:RadPageView runat="server" ID="pageDocumentos" CssClass="tabcontainer" Width="800px">
              <div class="fila containerSubTitulo">
                  <div class="colum10">
                  <asp:Label ID="Label11" runat="server" Text="Relación de documentos canjeados:" CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
              <div class="fila">
                  <div class="colum10">

                  <telerik:RadGrid ID="grdDocuementos" runat="server" AllowMultiRowSelection="false" 
                    AutoGenerateColumns="False" Height="200px"  Width="100%"
                    AllowSorting="True"  ShowFooter="true" >

                    <MasterTableView TableLayout="Fixed" 
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >

                        <Columns>

                            <telerik:GridBoundColumn DataField="TablaOrigen" HeaderText="TablaOrigen" HeaderStyle-Width="70px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Transaccion" HeaderText="Transacción" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Agenda" HeaderText="Agenda" HeaderStyle-Width="200px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaEmision" HeaderStyle-Width="100px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            
                            <telerik:GridBoundColumn DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaVencimiento" HeaderStyle-Width="100px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                               <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="70px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right"/> 
                                </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Saldo" DataField="Saldo" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="70px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right"/> 
                                </telerik:GridBoundColumn>

                                 <telerik:GridBoundColumn HeaderText="Aplicar" DataField="Aplicar" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="70px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right"/> 
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
              
              
             </telerik:RadPageView>
            
             <telerik:RadPageView runat="server" ID="pageLetras" CssClass="tabcontainer" Width="800px">
              <div class="fila containerSubTitulo">
                  <div class="colum10">
                  <asp:Label ID="Label1" runat="server" Text="Relación de letras canjeadas:" CssClass="subTitulo"></asp:Label>
                  </div>
              </div>
              <div class="fila">
                  <div class="colum10">

                  <telerik:RadGrid ID="grdLetras" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="200px"  Width="100%"
                    AllowSorting="True"  ShowFooter="true" >

                    <MasterTableView TableLayout="Fixed" 
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >

                        <Columns>

                           <telerik:GridBoundColumn DataField="NroCuota" HeaderText="NroCuota" HeaderStyle-Width="70px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ID_Amarre" HeaderText="Letra" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Glosa" HeaderText="Glosa" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="FechaEmision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaEmision" HeaderStyle-Width="100px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaVencimiento" HeaderStyle-Width="100px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Signo" HeaderText="Mon." HeaderStyle-Width="50px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right"/> 
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
              
              
             </telerik:RadPageView>

        </telerik:RadMultiPage>

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

