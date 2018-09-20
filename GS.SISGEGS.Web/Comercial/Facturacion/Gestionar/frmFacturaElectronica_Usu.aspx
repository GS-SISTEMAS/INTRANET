<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmFacturaElectronica_Usu.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Facturacion.Gestionar.frmFacturaElectronica_Usu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Facturación Electronica
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function ShowInsertFormLetras(FechaInicio, FechaFin, idOrdenVenta, id_agenda) {
            window.radopen("frmOrdenLetras.aspx?FechaInicio=" + FechaInicio + "&FechaFin=" + FechaFin + "&idOrdenVenta=" + idOrdenVenta + "&id_agenda=" + id_agenda, "rwPedidoLetrasMng");
            return false;
        }


        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExpResumen") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpDetalle") >= 0)
                args.set_enableAjax(false);

            if (args.get_eventTarget().indexOf("ibImprimir") >= 0)
                args.set_enableAjax(false);

            if (args.get_eventTarget().indexOf("ibImprimirLetra") >= 0)
                args.set_enableAjax(false);

        }

        function refreshGrid(arg)
        {
           if (!arg)
            {
                $find("<%= ramFacturaElectronica.ClientID %>").ajaxRequest("Rebind");
            }
            else
            {
                $find("<%=  ramFacturaElectronica.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

      function refreshGridLetras(arg) {
            if (!arg) {
                $find("<%=ramFacturaElectronica.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=ramFacturaElectronica.ClientID %>").ajaxRequest("RebindAndNavigateLetras(" + arg + ")");
            }
        }


        function ShowInsertForm(id) {
            window.radopen("frmFacturaElectronicaMng.aspx?idOperacion=" + id, "rwVidaLey");
            return false;
        }

        function ShowInsertFormImprimir(objFactura) {
            //window.radopen("frmExportarPDF.aspx?idOperacion=" + id, "rwPDF");  open
            window.radopen("frmExportarPDF.aspx?objFactura=" + objFactura, "WindowPopup", "width=600px, height=500px, resizable");
            return false;
        }

 
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramFacturaElectronica" runat="server"  OnAjaxRequest="ramPedidoMng_AjaxRequest">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpFacturaElectronica" ControlID="rapFacturaElectronica"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpFacturaElectronica" runat="server" ></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow 
                ID="rwVidaLey" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

           <telerik:RadWindow ID="rwPedidoLetrasMng" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapFacturaElectronica" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Comprobantes Electronicos: " CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
             <div class="col-md-1">
                   <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
             </div>
             <div class="col-md-2">
                    <telerik:RadMonthYearPicker ID="rmyPeriodo" Runat="server" Width="100%"
                                      OnSelectedDateChanged="rmyPeriodo_SelectedDateChanged"  >
                                    <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                    </telerik:RadMonthYearPicker>
             </div>
              <div class="col-md-4">
                    <asp:Label ID="lblMensajeFecha" runat="server" ForeColor="Red" Width="100%"></asp:Label>
             </div>
             <div class="col-md-5">
             </div>
       </div>

        <div class="row">
             <div class="col-md-1">
                           <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Tipo Documentos: " Width="110px"></asp:Label>
             </div>
             <div class="col-md-2">
                           <telerik:RadComboBox ID="cboTipoDoc" runat="server" Width="200px">
                           </telerik:RadComboBox>
             </div>
             <div class="col-md-9">
             </div>

       </div>

        <div class="row">
             <div class="col-md-1">
                        <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"  Width="110px"></asp:Label>
             </div>
             <div class="col-md-3">
                              <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                TextSettings-SelectionMode="Single" Width="100%">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmFacturaElectronica.aspx" />
                            </telerik:RadAutoCompleteBox>
             </div>
                 

             <div class="col-md-1">
                    <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                    </telerik:RadButton>    
             </div>


             <div class="col-md-1"> 
                      <telerik:RadButton ID="btnLetras" runat="server" OnClick="btnLetras_Click" Text="Planificar Letras">
                                                            <Icon PrimaryIconUrl="~/Images/Icons/calendario_1.png" />
                      </telerik:RadButton>
             </div>
             <div class="col-md-6">

             </div>
            
       </div>

        <div class="row">
          <div class="col-md-12">
                  <telerik:RadGrid ID="grdFacturaElectronica" runat="server" AllowMultiRowSelection="false" AutoGenerateColumns="False" Height="520px"  Width="100%"
                    AllowFilteringByColumn="true" AllowSorting="True" OnNeedDataSource="grdFacturaElectronica_NeedDataSource"  ShowFooter="true" 
                    OnItemDataBound="grdFacturaElectronica_ItemDataBound" OnItemCommand="grdFacturaElectronica_ItemCommand">

                    <GroupingSettings CaseSensitive="false"></GroupingSettings>

                    <MasterTableView DataKeyNames="OpOrigen,DocSunat" ClientDataKeyNames="OpOrigen,DocSunat"  AutoGenerateColumns="false" AllowFilteringByColumn="true" ShowFooter="True" >
                        <Columns>

                              <telerik:GridTemplateColumn UniqueName="CheckColumn" HeaderText="Finan" HeaderStyle-Width="40px" AllowSorting="true" AllowFiltering="false">
                                        <ItemTemplate>
                                        <asp:CheckBox ID="Check" runat="server"  />
                                        </ItemTemplate>
                              </telerik:GridTemplateColumn>
 
                            <telerik:GridTemplateColumn HeaderText="FACT." AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibImprimir"    runat="server" ImageUrl="~/Images/Icons/pdf_22.png" CommandArgument='<%# Eval("Op") %>' CommandName="Imprimir" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>

                             <telerik:GridTemplateColumn HeaderText="LETRAS" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibImprimirLetra"    runat="server" ImageUrl="~/Images/Icons/pdf_22.png" CommandArgument='<%# Eval("OpOrigen") %>' CommandName="ImprimirLetra" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>

                   
                              <telerik:GridTemplateColumn HeaderText="Edit." AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/calendario_1.png" CommandArgument='<%# Eval("OpOrigen") %>' CommandName="Editar" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
 

                             <telerik:GridBoundColumn DataField="Documento" HeaderText="Documento" HeaderStyle-Width="200px" AllowSorting="false" AllowFiltering="false" Visible="true" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SunatEnvio_NroIntento" HeaderText="Proceso" HeaderStyle-Width="200px" AllowSorting="false" AllowFiltering="false" Visible="true" Display="false">
                            </telerik:GridBoundColumn>
                            

                            <telerik:GridBoundColumn DataField="Serie" HeaderText="Serie" HeaderStyle-Width="40px" AutoPostBackOnFilter="true" 
                                                     FilterControlWidth="70px" CurrentFilterFunction="Contains" ShowFilterIcon="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="70px" DataField="Numero" HeaderText="Numero" AutoPostBackOnFilter="true" 
                                                     CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>

                 
                            <telerik:GridCheckBoxColumn DataField="Ok_XML" HeaderText="XML" ToolTip="Ok_XML" UniqueName="Ok_XML" AllowSorting="true" AllowFiltering="false" >
                                <HeaderStyle Width="40px"/>
                            </telerik:GridCheckBoxColumn>
               
                            <telerik:GridCheckBoxColumn DataField="Ok_SunatRpta" HeaderText="SunatRpta"  ToolTip="Ok_SunatRpta" UniqueName="Ok_SunatRpta" AllowSorting="true"     AllowFiltering="false">
                                <HeaderStyle Width="60px"/>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridCheckBoxColumn DataField="OK_Proceso" HeaderText="Proceso"   ToolTip="OK_Proceso" UniqueName="OK_Proceso" AllowSorting="true"   AllowFiltering="false">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridBoundColumn FilterControlWidth="150px" DataField="SunatRpta_NroTicket" HeaderText="C.Crédito" 
                                   AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                  <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>

                            

                            <telerik:GridBoundColumn FilterControlWidth="200px" DataField="RespuestaTCI" HeaderText="RespuestaTCI" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                  <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="250px" DataField="Nombre" HeaderText="Cliente" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="240px" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="250px" DataField="Vendedor" HeaderText="Vendedor" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="220px" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="MonedaSigno" HeaderText="Moneda"  HeaderStyle-Width="50px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="TC" HeaderText="TC" DataFormatString="{0:#,##0.00}" HeaderStyle-Width="30px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="TotalSoles" HeaderText="TotalSoles" HeaderStyle-Width="80px" 
                             DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="TotalDolares" HeaderText="TotalDolares" HeaderStyle-Width="80px" 
                             DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaDoc." 
                                HeaderStyle-Width="70px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaVnc." 
                                HeaderStyle-Width="70px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn DataField="OP_OV"  HeaderText="OP_OV" 
                                HeaderStyle-Width="70px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                                 <telerik:GridBoundColumn DataField="DiasCredito"  HeaderText="DiasCredito" 
                                HeaderStyle-Width="70px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                        

                        </Columns>
                    </MasterTableView>

                     <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                <Selecting AllowRowSelect="True"></Selecting>
                     </ClientSettings>
                </telerik:RadGrid>
          </div>
        </div>

    </telerik:RadAjaxPanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblFechaElectronica" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
