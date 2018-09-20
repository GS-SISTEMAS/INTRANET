<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmLetrasEmitidas.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Financiamientos.LetrasEmitidas.frmLetrasEmitidas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Letras Emitidas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExpResumen") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpDetalle") >= 0)
                args.set_enableAjax(false);
        }
       function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramLetrasEmitidas.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=  ramLetrasEmitidas.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
       }
        function ShowInsertForm(id) {
            window.radopen("frmLetrasEmitidasMng.aspx?idOperacion=" + id, "rwVidaLey");
            return false;
        }
        function ShowInsertFormImprimir(id) {
            //window.radopen("frmExportarPDF.aspx?idOperacion=" + id, "rwPDF");
            window.open("frmExportarPDF.aspx?idOperacion=" + id, "WindowPopup", "width=600px, height=500px, resizable");
            return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramLetrasEmitidas" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpLetrasEmitidas" ControlID="rapLetrasEmitidas"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdLetrasEmitidas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLetrasEmitidas" LoadingPanelID="ralpLetrasEmitidas"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdLetrasEmitidas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapLetrasEmitidas" LoadingPanelID="ralpLetrasEmitidas"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpLetrasEmitidas" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="850px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

             <telerik:RadWindow ID="rwPDF" runat="server" Width="850px" Height="600px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapLetrasEmitidas" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
           <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consultar letras emitidas" CssClass="titulo"></asp:Label>
                </div>
            </div>
           <div class="row">
            
            <div class="col-md-1">
                  <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Fecha" Width="100%"></asp:Label>
             </div>
                <div class="col-md-1">
                     <asp:Label ID="lblFinalEmision" runat="server" CssClass="etiqueta" Text="Desde:" Width="100%"></asp:Label>
                </div>
                <div class="col-md-1">
                              <telerik:RadDatePicker ID="dpInicio" runat="server" DateInput-ReadOnly="true" Width="110px" >
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
                <div class="col-md-1">
                     <asp:Label ID="lblFinalEmision0" runat="server" CssClass="etiqueta" Text="Hasta: " Width="100%"></asp:Label>
                </div>
                <div class="col-md-1">
                         <telerik:RadDatePicker ID="dpFinal" runat="server" DateInput-ReadOnly="true" Width="110px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
             <div class="col-md-7"></div>
             
          </div>
           <div class="row">
                 <div class="col-md-1">
                      <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                 </div>
                <div class="col-md-3">
                      <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="300px">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmLetrasEmitidas.aspx" />
                            </telerik:RadAutoCompleteBox>
                </div>
                <div class="col-md-8"></div>
           </div>
           <div class="row">
                 <div class="col-md-1">
                       <asp:Label ID="lblOpFinan" runat="server" CssClass="etiqueta" Text="Op Finan."></asp:Label>
                 </div>
                <div class="col-md-3">
                       <telerik:RadTextBox ID="txtOpFin" Width="300" Runat="server">
                            </telerik:RadTextBox>
                </div>
                <div class="col-md-1">
                           <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                            </telerik:RadButton>
                </div>
               <div class="col-md-7"></div>
           </div>
           <div class="row">
             <div class="col-md-12">
                <telerik:RadGrid ID="grdLetrasEmitidas" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="450px"  Width="100%"
                    AllowFilteringByColumn="true" AllowSorting="True"
                    OnNeedDataSource="grdLetrasEmitidas_NeedDataSource"  ShowFooter="true" 
                    OnItemDataBound="grdLetrasEmitidas_ItemDataBound" OnItemCommand="grdLetrasEmitidas_ItemCommand">
                     <GroupingSettings CaseSensitive="false"></GroupingSettings>

                    <MasterTableView DataKeyNames="OpFinanciamiento" ClientDataKeyNames="OpFinanciamiento" 
                        AutoGenerateColumns="false" AllowFilteringByColumn="true"
                                 ShowFooter="True" >
                        <Columns>

                             <telerik:GridTemplateColumn HeaderText="Impr." AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibImprimir" runat="server" ImageUrl="~/Images/Icons/pdf_22.png" CommandArgument='<%# Eval("OpFinanciamiento") %>' CommandName="Imprimir" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>


                           <telerik:GridTemplateColumn HeaderText="Ver." AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/search-20.png" CommandArgument='<%# Eval("OpFinanciamiento") %>' CommandName="Editar" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>


                            <telerik:GridBoundColumn FilterControlWidth="60px" DataField="OpFinanciamiento" HeaderText="#OpFinan" 
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="60px" />
                             </telerik:GridBoundColumn>



                            <telerik:GridBoundColumn FilterControlWidth="200px" DataField="Documentos" HeaderText="Documentos"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="200px" />
                             </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="NroPeriodos" HeaderText="Periodos" HeaderStyle-Width="50px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Total" HeaderText ="ImporteFinan"  DataFormatString="${0:#,##0.00}"  HeaderStyle-Width="80px" AllowSorting="false" AllowFiltering="false">
                           <ItemStyle HorizontalAlign="Right"/> 
                           </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="100px" DataField="ID_Amarre" HeaderText="NroLetra"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="100px" />
                             </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="Signo" HeaderText="Mon." HeaderStyle-Width="50px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                               <telerik:GridBoundColumn HeaderText="ImporteLetra" DataField="Importe" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="70px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right"/> 
                                </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="NroCuota" HeaderText="NroCuota" HeaderStyle-Width="70px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="230px" DataField="AgendaNombre" HeaderText="Cliente"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="230px" />
                             </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="FechaEmision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaEmision" HeaderStyle-Width="100px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaVencimiento" HeaderStyle-Width="100px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Estado" HeaderText="EstadoLetra" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Glosa" HeaderText="Glosa" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroRenovacion" HeaderText="NroRenovacion" HeaderStyle-Width="90px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LetraRenovada" HeaderText="LetraRenovada" HeaderStyle-Width="90px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OpRenovado" HeaderText="OpRenovado" HeaderStyle-Width="90px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>


                        </Columns>
                    </MasterTableView>

                     <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3"></Scrolling>
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
        </div>
    </div>
</asp:Content>
