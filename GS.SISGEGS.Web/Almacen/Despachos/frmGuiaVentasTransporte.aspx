<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmGuiaVentasTransporte.aspx.cs" Inherits="GS.SISGEGS.Web.Almacen.Despachos.frmGuiaVentasTransporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Guía de Ventas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script src="Foundation/js/foundation.min.js" type="text/javascript"></script>  
    <link href="Foundation/css/foundation.min.css" rel="stylesheet" type="text/css" /> 

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
                $find("<%= ramGuiaVentasTransporte.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=  ramGuiaVentasTransporte.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
       }


        function ShowInsertForm(id) {
            window.radopen("frmGuiaVentasTransporteMng.aspx?idOperacion=" + id, "rwVidaLey");
            return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramGuiaVentasTransporte" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpGuiaVentasTransporte" ControlID="rapGuiaVentasTransporte"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnRegistrarFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpGuiaVentasTransporte" ControlID="rapGuiaVentasTransporte"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdGuiaVentasTransporte">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGuiaVentasTransporte" LoadingPanelID="ralpGuiaVentasTransporte"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdGuiaVentasTransporte">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGuiaVentasTransporte" LoadingPanelID="ralpGuiaVentasTransporte"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpGuiaVentasTransporte" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapGuiaVentasTransporte" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Registrar fecha de salida de agencia/cliente:" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row"  >
              <div   class="fila">
                  <div class="fila">
                  <div class="colum1">
                     <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Fecha" Width="57px"></asp:Label>
                  </div>
                    <div class="colum1">
                        <asp:Label ID="lblFinalEmision" runat="server" CssClass="etiqueta" Text="Desde:" Width="40px"></asp:Label>
                   </div>
                     <div class="colum2">
                             <telerik:RadDatePicker ID="dpInicio" runat="server" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                    </div>
                     <div class="colum1">
                           <asp:Label ID="lblFinalEmision0" runat="server" CssClass="etiqueta" Text="Hasta: " Width="40px"></asp:Label>
                     </div>
                      <div class="colum2">
                            <telerik:RadDatePicker ID="dpFinal" runat="server" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                     </div>
                      <div class="colum1">
                     </div>
                </div>
                  <div class="fila">
                   <div class="colum1">
                        <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                  </div>
                    <div class="colum6">
                         <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="100%">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmGuiaVentasTransporte.aspx" />
                            </telerik:RadAutoCompleteBox>
                   </div>
                      <div class="colum1">
                     </div>
                </div>
                  <div class="fila">
                       <div class="colum1">
                             <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Vehículo: "></asp:Label>
                       </div>
                          <div class="colum6">
                            <telerik:RadComboBox ID="cboTransporte" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged ="cboTransporte_SelectedIndexChanged">
                            </telerik:RadComboBox>
                       </div>
                          <div class="colum1">
                                                          <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                       </div>
                 </div>
                 <div style="height: 5px">
                      <div class="fila">
                           <div class="colum5">
                                <asp:Label ID="lblMensajeFecha" runat="server" ForeColor="Red" Width="100%"></asp:Label>
                           </div>
                      </div>
                 </div>

                 
                  <div style="height: 5px">
                 </div>
                  <div class="fila">
                      <div class="colum1">
                            <telerik:RadButton ID="btnRegistrarFecha" runat="server" OnClick="btnRegistrarFecha_Click" style="top: 1px; left: 3px" Text="Registrar Fecha" Width="130px">
                                <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                            </telerik:RadButton>
                      </div>
                        <div class="colum7">
                        </div>
                  </div>
                
                 <div class="fila" >

                    <telerik:RadGrid ID="grdGuiasVentas" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="400px"  Width="100%"
                    AllowFilteringByColumn="true" AllowSorting="True"
                    OnNeedDataSource="grdGuiasVentas_NeedDataSource"  ShowFooter="true" 
                    OnItemDataBound="grdGuiasVentas_ItemDataBound" OnItemCommand="grdGuiasVentas_ItemCommand">
                     <GroupingSettings CaseSensitive="false"></GroupingSettings>

                    <MasterTableView DataKeyNames="Op,ID_Agenda" ClientDataKeyNames="Op,ID_Agenda"  AutoGenerateColumns="false" AllowFilteringByColumn="true" ShowFooter="True" >
                    
                        <Columns>

                              <telerik:GridTemplateColumn UniqueName="CheckColumn" HeaderText="Check" HeaderStyle-Width="40px" AllowSorting="true" AllowFiltering="false">
                              <ItemTemplate>
                                <asp:CheckBox ID="Check" runat="server" />
                              </ItemTemplate>
                             </telerik:GridTemplateColumn>

                           <telerik:GridMaskedColumn DataField="Op" HeaderText="#Op" FilterControlWidth="45px" AutoPostBackOnFilter="false" AllowSorting="true"
                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" Mask="#####" UniqueName="Op">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Height="50px"  />
                            </telerik:GridMaskedColumn>

                            <telerik:GridBoundColumn DataField="ID_Almacen" HeaderText="Id_Almacen" AllowSorting="false" Visible="false" AllowFiltering="false" >
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Almacen" HeaderText="Almacén" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ID_AlmacenAnexo" Visible="false" HeaderText="Id_AlmacenAnexo" HeaderStyle-Width="30px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="AlmacenAnexo" HeaderText="AlmacenAnexo" HeaderStyle-Width="270px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn FilterControlWidth="100px" DataField="Transaccion" HeaderText="Documento" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>

                           <telerik:GridBoundColumn DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaGuía" HeaderStyle-Width="90px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="fechaSeguridad"  HeaderText="SalidaAlamcén" HeaderStyle-Width="170px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="ID_Agenda" HeaderText="Cod.Agenda" Visible="false"  HeaderStyle-Width="80px" AllowSorting="false"  AllowFiltering="false">
                                             <ItemStyle HorizontalAlign="Right"  />
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn FilterControlWidth="200px" DataField="Agenda" HeaderText="Cliente" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="320px" />
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="ID_Transportista" HeaderText="cod.Transportista" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Transportista" HeaderText="Transportista" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Id_vehiculo1" HeaderText="Vehiculo" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="TransportistaChofer" HeaderText="Chofer" HeaderStyle-Width="250px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransportistaMarca" HeaderText="VehiculoMarca" HeaderStyle-Width="120px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransportistaModelo" HeaderText="VehiculoModelo" HeaderStyle-Width="120px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransportistaPlaca" HeaderText="VehiculoPlaca" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Chofer" HeaderText="Id_Chofer" HeaderStyle-Width="150px"  Visible="false" AllowSorting="false" AllowFiltering="false">
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
