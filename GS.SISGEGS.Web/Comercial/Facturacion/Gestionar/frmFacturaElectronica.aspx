<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmFacturaElectronica.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Facturacion.Gestionar.frmFacturaElectronica" %>

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

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExpResumen") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpDetalle") >= 0)
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


        function ShowInsertForm(id) {
            window.radopen("frmFacturaElectronicaMng.aspx?idOperacion=" + id, "rwVidaLey");
            return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramFacturaElectronica" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpFacturaElectronica" ControlID="rapFacturaElectronica"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnRegistrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpFacturaElectronica" ControlID="rapFacturaElectronica"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdFacturaElectronica">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdFacturaElectronica" LoadingPanelID="ralpFacturaElectronica"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>  

            <telerik:AjaxSetting AjaxControlID="grdFacturaElectronica">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapFacturaElectronica" LoadingPanelID="ralpFacturaElectronica"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpFacturaElectronica" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapFacturaElectronica" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Envío electrónico de comprobantes de pago: " CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            
            <div class="col-md-12">

                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Fecha" Width="57px"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:Label ID="lblFinalEmision" runat="server" CssClass="etiqueta" Text="Desde:" Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpInicio" runat="server" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker></td>
                        <td class="auto-style1">
                            <asp:Label ID="lblFinalEmision0" runat="server" CssClass="etiqueta" Text="Hasta: " Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <telerik:RadDatePicker ID="dpFinal" runat="server" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td class="auto-style1" colspan="5">
                             <asp:Label ID="lblMensajeFecha" runat="server" ForeColor="Red" Width="100%"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="auto-style1" colspan="2">
                             <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Tipo Documentos: " Width="110px"></asp:Label>
                        </td>
                        <td class="auto-style1" colspan="2">
                          <telerik:RadComboBox ID="cboTipoDoc" runat="server" Width="200px">
                            </telerik:RadComboBox>
                        </td>
                        <td class="auto-style1">
                             <asp:TextBox ID="txtCorrelativo" runat="server" Width="100px" ToolTip="Correlativo"  ></asp:TextBox>
                        </td>
                        
                        <td class="auto-style1">
                            <asp:TextBox ID="txtSerie" runat="server" Width="101px" ToolTip="Serie de documento"></asp:TextBox>
                        </td>

                        <td class="auto-style1">
                            <asp:TextBox ID="txtNumero" runat="server" Width="101px" ToolTip="Número de documento"></asp:TextBox>
                        </td>
                        <td class="auto-style1">
                             <asp:TextBox ID="txtSerieREF" runat="server" Width="101px" ToolTip="Serie Referencia"></asp:TextBox>
                        </td>

                        <td class="auto-style1">
                             <asp:TextBox ID="txtNumeroREF" runat="server" Width="101px" ToolTip="Número Referencia" ></asp:TextBox>
                        </td>

                        <td class="auto-style1">
                             <asp:TextBox ID="txtIdCliente" runat="server" Width="101px" ToolTip="RUC_EMPRESA" ></asp:TextBox>
                        </td>

                        <td class="auto-style1">
                             <asp:TextBox ID="txtTipoCambio" runat="server" Width="101px" ToolTip="TIPO_CAMBIO" ></asp:TextBox>
                        </td>



                        <td class="auto-style1">
                             <asp:TextBox ID="txtCliente" runat="server" Width="101px" ToolTip="Nombre Cliente"></asp:TextBox>
                              <asp:Label ID="lblCaso" runat="server" CssClass="etiqueta" Text="Cliente:"  Width="110px"></asp:Label>
                        </td>

                         <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpFechaReferencia" runat="server" ToolTip="Fecha Referencia" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                         </td>

                        <td class="auto-style1">
                         <telerik:RadDatePicker ID="dpFechaFinal" runat="server" ToolTip="Fecha Final" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="1">
                            <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"  Width="110px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                TextSettings-SelectionMode="Single" Width="100%">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmFacturaElectronica.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                        <td>
                          <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../Images/Icons/search-16.png" />
                            </telerik:RadButton>    
                        </td>
                        <td>
                             <telerik:RadButton ID="btnRegistrar" runat="server" OnClick="btnRegistrar_Click" style="top: 1px; left: 3px" Text="Enviar FE" Width="130px">
                                <Icon PrimaryIconUrl="../Images/Icons/box-out-16.png" />
                            </telerik:RadButton>
                        </td>
                        <td> 
                            <telerik:RadButton ID="btnActualizarEstados" runat="server" OnClick="btnActualizarEstados_Click" style="top: 1px; left: 3px" Text="Metodos FE" Width="130px">
                                <Icon PrimaryIconUrl="../Images/Icons/pencil-16.png" />
                            </telerik:RadButton>

                        </td>
                        <td>
                           <telerik:RadButton ID="btnDarBaja" runat="server" OnClick="btnDarBaja_Click" style="top: 1px; left: 3px" Text="Dar Baja" Width="130px">
                                <Icon PrimaryIconUrl="../Images/Icons/pencil-16.png" />
                            </telerik:RadButton>
                        </td>

                         <td>
                           <telerik:RadButton ID="btnEnviarResumen" runat="server" OnClick="btnEnviarResumen_Click" style="top: 1px; left: 3px" Text="Enviar resumen" Width="130px">
                                <Icon PrimaryIconUrl="../Images/Icons/pencil-16.png" />
                            </telerik:RadButton>
                        </td>

                         <td>

                           <telerik:RadButton ID="btnBuscarRegistros" runat="server" OnClick="btnBuscarRegistros_Click" style="top: 1px; left: 3px" Text="Buscar resumen" Width="130px">
                                <Icon PrimaryIconUrl="../Images/Icons/pencil-16.png" />
                            </telerik:RadButton>

                        </td>

                        <td>

                           <telerik:RadButton ID="btnTipoCambio" runat="server" OnClick="btnTipoCambio_Click" style="top: 1px; left: 3px" Text="Registrar TC" Width="130px">
                                <Icon PrimaryIconUrl="../Images/Icons/pencil-16.png" />
                            </telerik:RadButton>

                        </td>



                    </tr>
                    
                </table>
                 <div style="height: 5px">
                 </div>

                <telerik:RadGrid ID="grdFacturaElectronica" runat="server" AllowMultiRowSelection="false" AutoGenerateColumns="False" Height="520px"  Width="100%"
                    AllowFilteringByColumn="true" AllowSorting="True" OnNeedDataSource="grdFacturaElectronica_NeedDataSource"  ShowFooter="true" 
                    OnItemDataBound="grdFacturaElectronica_ItemDataBound" OnItemCommand="grdFacturaElectronica_ItemCommand">

                    <GroupingSettings CaseSensitive="false"></GroupingSettings>

                    <MasterTableView DataKeyNames="OpOrigen,DocSunat" ClientDataKeyNames="OpOrigen,DocSunat"  AutoGenerateColumns="false" AllowFilteringByColumn="true" ShowFooter="True" >
                        <Columns>

                              <telerik:GridTemplateColumn UniqueName="CheckColumn" HeaderText="Check" HeaderStyle-Width="40px" AllowSorting="true" AllowFiltering="false">
                              <ItemTemplate>
                                <asp:CheckBox ID="Check" runat="server" />
                              </ItemTemplate>
                             </telerik:GridTemplateColumn>

                           <telerik:GridMaskedColumn DataField="OpOrigen" HeaderText="#Op" FilterControlWidth="45px" AutoPostBackOnFilter="false" AllowSorting="true"
                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" Mask="#####" UniqueName="Op">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Height="50px"  />
                            </telerik:GridMaskedColumn>

                            <telerik:GridBoundColumn DataField="TablaOrigen" HeaderText="TablaOrigen" HeaderStyle-Width="90px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Documento" HeaderText="Documento" HeaderStyle-Width="130px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Serie" HeaderText="Serie" HeaderStyle-Width="40px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="70px" DataField="Numero" HeaderText="Numero" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>

                            <telerik:GridCheckBoxColumn DataField="Ok_XML" HeaderText="XML" ToolTip="Ok_XML" UniqueName="Ok_XML" AllowSorting="true" AllowFiltering="false">
                                <HeaderStyle Width="40px"/>
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="Ok_SunatEnvio" HeaderText="Sunat" ToolTip="Ok_SunatEnvio"  UniqueName="Ok_SunatEnvio" AllowSorting="true" AllowFiltering="false">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="Ok_SunatRpta" HeaderText="SunatRpta"  ToolTip="Ok_SunatRpta" UniqueName="Ok_SunatRpta" AllowSorting="true" AllowFiltering="false">
                                <HeaderStyle Width="60px"/>
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="Ok_Cliente" HeaderText="Cliente"   ToolTip="Ok_Cliente" UniqueName="Ok_Cliente" AllowSorting="true" AllowFiltering="false">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridBoundColumn FilterControlWidth="150px" DataField="RespuestaTCI" HeaderText="RespuestaTCI" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                  <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="ClienteCodigo" Visible="false" HeaderText="Cod.Cliente" HeaderStyle-Width="80px" AllowSorting="false"  AllowFiltering="false">
                                             <ItemStyle HorizontalAlign="Right"  />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="250px" DataField="ClienteNombre" HeaderText="Cliente" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
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
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="TotalDolares" HeaderText="TotalDolares" HeaderStyle-Width="80px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaDoc." HeaderStyle-Width="70px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaVnc." HeaderStyle-Width="70px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ReferenciaDocumentoTipo" HeaderText="RefDocTipo"  HeaderStyle-Width="120px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ReferenciaDocumentoNombre" HeaderText="RefDocNombre"  HeaderStyle-Width="120px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ReferenciaDocumentoSerie" HeaderText="RefDocSerie"  HeaderStyle-Width="120px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ReferenciaDocumentoNumero" HeaderText="RefDocNumero"  HeaderStyle-Width="120px" AllowSorting="false" AllowFiltering="false" Visible="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ReferenciaDocumentoFecha" HeaderText="RefDocFecha"  HeaderStyle-Width="120px" AllowSorting="false" AllowFiltering="false" Visible="true">
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
            <asp:Label ID="lblFechaElectronica" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
