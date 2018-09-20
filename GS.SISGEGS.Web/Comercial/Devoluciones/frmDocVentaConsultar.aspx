<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmDocVentaConsultar.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Devoluciones.frmDocVentaConsultar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre - Generar solicitud de devolución
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramDevolucion" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapDevolucion" LoadingPanelID="ralpDevolucion"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDocVenta">
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Generar solicitud de devolución" CssClass="titulo"></asp:Label>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="94%">
                    <Content>
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="90%">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda" EnableDock="false"
                                        MinWidth="225" MinHeight="225" Scrolling="None" IconUrl="~/Images/Icons/file-empty-16.png">
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
                                                    <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmDocVentaConsultar.aspx" />
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                        </div>--%>
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
                                                <telerik:RadGrid ID="grdDocVenta" runat="server" Width="100%" Height="100%" AutoGenerateColumns="false" OnItemCommand="grdDocVenta_ItemCommand" OnNeedDataSource="grdDocVenta_NeedDataSource">
                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                    <MasterTableView AllowFilteringByColumn="True">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn  AllowFiltering="false" HeaderText="Dev.">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ibDevolucion" runat="server" ToolTip="Devolución" CommandName="Devolucion" 
                                                                        CommandArgument='<%# Eval("Op") %>' ImageUrl="~/Images/Icons/box-out-16.png" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px"/>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="Op" HeaderText="Op" UniqueName="Op" AllowFiltering="true" AutoPostBackOnFilter="false" FilterDelay="2000" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                                                <HeaderStyle Width="50px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="false">
                                                                <HeaderStyle Width="80px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Documento" HeaderText="Documento" UniqueName="Documento" AllowFiltering="false">
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Transaccion" HeaderText="Transaccion" UniqueName="Transaccion" AllowFiltering="true" AutoPostBackOnFilter="false" FilterDelay="2000" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Agenda" HeaderText="Cliente" UniqueName="Agenda" AllowFiltering="true" AutoPostBackOnFilter="false" FilterDelay="2000" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                                <HeaderStyle Width="200px" />
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda" AllowFiltering="false">
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Vendedor" HeaderText="Vendedor" UniqueName="Vendedor" AllowFiltering="true" AutoPostBackOnFilter="false" FilterDelay="2000" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                                <HeaderStyle Width="200px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CondicionCredito" HeaderText="Cond.Cred." UniqueName="CondicionCredito" AllowFiltering="false">
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Neto" HeaderText="Neto" UniqueName="Neto" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="SubTotal" HeaderText="SubTotal" UniqueName="SubTotal" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Impuestos" HeaderText="Impuestos" UniqueName="Impuestos" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Total" HeaderText="Total" UniqueName="Total" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Pagado" HeaderText="Pagado" UniqueName="Pagado" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Financiado" HeaderText="Financiado" UniqueName="Financiado" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Pendiente" HeaderText="Pendiente" UniqueName="Pendiente" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridBoundColumn>
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