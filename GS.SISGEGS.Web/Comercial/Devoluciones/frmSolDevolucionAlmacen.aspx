<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmSolDevolucionAlmacen.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Devoluciones.frmSolDevolucionAlmacen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre - Consultar solicitudes pendientes de guiar
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        //function OnClientEntryAddingHandler(sender, eventArgs) {
        //    if (sender.get_entries().get_count() > 0) {
        //        eventArgs.set_cancel(true);
        //        alert("Solo se puede selecionar un elemento.");
        //    }
        //}
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadWindowManager ID="rwmDevolucion" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwDevolucion" runat="server"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="ramDevolucion" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapDevolucion" LoadingPanelID="ralpDevolucion"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDevolucionSolicitud">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapDevolucion" LoadingPanelID="ralpDevolucion"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpDevolucion" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapDevolucion" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <asp:Label ID="lblTitulo" runat="server" Text="Solicitudes de devolución pendientes de guiar" CssClass="titulo"></asp:Label>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="94%">
                    <Content>
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda" EnableDock="false"
                                        MinWidth="225" MinHeight="225" Scrolling="None">
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaInicio" runat="server" Text="Desde" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaInicio" runat="server" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaFinal" runat="server" Text="Hasta" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaFinal" runat="server" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                                            </div>
                                        </div>
                                       <%-- <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                                    DropDownHeight="150px" EmptyMessage="Buscar cliente" AllowCustomEntry="true" DropDownWidth="350px">
                                                    <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmSolDevolucionConsultar.aspx" />
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                        </div>--%>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblGuia" runat="server" Text="Est.Guia" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadComboBox ID="cboGuia" runat="server">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="" Text="Todos" />
                                                        <telerik:RadComboBoxItem Value="False" Text="Pendiente" Selected="true"/>
                                                        <telerik:RadComboBoxItem Value="True" Text="Realizado" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum7">
                                                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                                </telerik:RadButton>
                                            </div>
                                        </div>
                                    </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                            </telerik:RadPane>
                            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%">
                                <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%">
                                    <Rows>
                                        <telerik:LayoutRow Height="100%">
                                            <Content>
                                                <telerik:RadGrid ID="grdDevolucionSolicitud" runat="server" Width="100%" Height="100%" AutoGenerateColumns="false" AllowFilteringByColumn="True" 
                                                    OnNeedDataSource="grdDevolucionSolicitud_NeedDataSource" OnItemCommand="grdDevolucionSolicitud_ItemCommand">
                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                    <MasterTableView DataKeyNames="idDevolucionSolicitud" ClientDataKeyNames="idDevolucionSolicitud">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Rev." AllowFiltering="false">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ibDevolucion" runat="server" ToolTip="Editar" CommandName="Editar" 
                                                                        CommandArgument='<%# Eval("idDevolucionSolicitud") + "," + Eval("Op") %>' ImageUrl="~/Images/Icons/search-16.png" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px"/>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="idDevolucionSolicitud" HeaderText="ID" UniqueName="idDevolucionSolicitud" AllowFiltering="true" AutoPostBackOnFilter="false">
                                                                <HeaderStyle Width="40px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaSolicitud" HeaderText="Fecha" UniqueName="fechaSolicitud" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                                                                <HeaderStyle Width="65px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaEnvio" HeaderText="Fec.Envío" UniqueName="fechaEnvio" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                                                                <HeaderStyle Width="65px" />
                                                            </telerik:GridBoundColumn>
                                                          <%--  <telerik:GridBoundColumn DataField="flete" HeaderText="Flete" UniqueName="flete" DataFormatString="${0:F2}" AllowFiltering="false">
                                                                <HeaderStyle Width="50px" />
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn DataField="Op" HeaderText="Op" UniqueName="Op" AllowFiltering="true" AutoPostBackOnFilter="false">
                                                                <HeaderStyle Width="50px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="DocVenReferencia" HeaderText="Doc.Referenc." UniqueName="DocVenReferencia" AllowFiltering="true" AutoPostBackOnFilter="false" ShowFilterIcon="false" >
                                                                <HeaderStyle Width="70px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaDocVenta" HeaderText="Fec.Venta" UniqueName="fechaDocVenta" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                                                                <HeaderStyle Width="80px" />
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="Cliente" AllowFiltering="true" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterDelay="2000">
                                                                <HeaderStyle Width="150px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Vendedor" HeaderText="Vendedor" UniqueName="Vendedor" AllowFiltering="true" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterDelay="2000">
                                                                <HeaderStyle Width="150px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Zona" HeaderText="Zona" UniqueName="Zona" AllowFiltering="true" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterDelay="2000">
                                                                <HeaderStyle Width="150px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="NomUsuarioRegistro" HeaderText="NomUsuarioRegistro" UniqueName="NomUsuarioRegistro" AllowFiltering="true" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterDelay="2000">
                                                                <HeaderStyle Width="150px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridCheckBoxColumn DataField="aprobacion1" HeaderText="Aprob." AllowFiltering="false">
                                                                <HeaderStyle Width="30px"/>
                                                            </telerik:GridCheckBoxColumn>
                                                            <telerik:GridCheckBoxColumn DataField="GuiadoDev" HeaderText="Guía" AllowFiltering="false">
                                                                <HeaderStyle Width="30px"/>
                                                            </telerik:GridCheckBoxColumn>
                                                            <%--<telerik:GridCheckBoxColumn DataField="NotaCredito" HeaderText="NotaCred." AllowFiltering="false">
                                                                <HeaderStyle Width="30px"/>
                                                            </telerik:GridCheckBoxColumn>--%>
                                                            <%--<telerik:GridButtonColumn ConfirmText="¿Desea eliminar la solicitud de devolución?" ConfirmDialogType="RadWindow"
                                                                HeaderStyle-Width="30px" HeaderText="Elim." ConfirmTitle="Eliminar" ButtonType="ImageButton"
                                                                CommandName="Delete" ImageUrl="../../Images/Icons/delete-16.png" UniqueName="Elim" />--%>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" FrozenColumnsCount="2"/>
                                                        <Selecting AllowRowSelect="true"/>
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </Content>
                                        </telerik:LayoutRow>
                                    </Rows>
                                </telerik:RadPageLayout>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
