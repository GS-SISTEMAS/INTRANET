<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmAnularOC.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.OC.frmAnularOC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Anulación de órdenes de compra
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

        function RadConfirm(sender, args) {
            var callBackFunction = function (shouldSubmit) {
                if (shouldSubmit) {
                    //initiate the original postback again
                    sender.click();
                    if (Telerik.Web.Browser.ff) { //work around a FireFox issue with form submission, see http://www.telerik.com/support/kb/aspnet-ajax/window/details/form-will-not-submit-after-radconfirm-in-firefox
                        sender.get_element().click();
                    }
                }
            };

            var text = "Está seguro de anular las Ordenes de Compra seleccionadas?";
            radconfirm(text, callBackFunction, 300, 130, null, "Anular OC");
            //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
            args.set_cancel(true);
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpReporte" ControlID="rapReporte"/>                    
                    <telerik:AjaxUpdatedControl ControlID="ajaxMensaje" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAnular">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpReporte" ControlID="rapReporte"/>                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="width: 100%; height:100%;" >
        <%-- LOADER --%>
        <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>
        <%-- FIN LOADER --%>

        <%-- LAYOUT --%>
        <telerik:RadAjaxPanel ID="rapReporte" runat="server" Height="95%" Width="100%" ClientEvents-OnRequestStart="requestStart">
            <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
                <Rows>
                    <telerik:LayoutRow >
                        <Content>
                            <div class="col-md-9">
                                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Anulación de órdenes de compra"></asp:Label>
                            </div>
                            <div class="col-md-3" style="text-align:right;">
                                <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                    <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                                </telerik:RadButton>
                                &nbsp;
                                <telerik:RadButton ID="btnAnular" runat="server" Text="Anular" OnClick="btnAnular_Click" OnClientClicking="RadConfirm" >                                    
                                </telerik:RadButton>
                                <telerik:RadWindowManager RenderMode="Lightweight" ID="windowManager1" runat="server" Style="z-index: 100001">
                                </telerik:RadWindowManager>
                            </div>
                        </Content>                        
                    </telerik:LayoutRow>
                    <telerik:LayoutRow runat="server" Height="95%" >
                        <Columns>
                            <telerik:LayoutColumn Span="12" Height="100%">
                                <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Vertical">
                                    <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                        <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                            <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                                EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                                <div class="fila">
                                                    <div class="colum4">
                                                        <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Desde" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                    <div class="colum6">
                                                        <telerik:RadDatePicker ID="dpFecDesde" runat="server" RenderMode="Lightweight" Width="100%" DateInput-DateFormat="dd/MM/yyyy">
                                                            <Calendar runat="server" ShowRowHeaders="false"></Calendar>
                                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"></DateInput>
                                                        </telerik:RadDatePicker>                                                       
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                    <div class="colum4">
                                                        <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Hasta" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                    <div class="colum6">
                                                        <telerik:RadDatePicker ID="dpFecHasta" runat="server" RenderMode="Lightweight" Width="100%">
                                                            <Calendar runat="server" ShowRowHeaders="false"></Calendar>
                                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"></DateInput>
                                                        </telerik:RadDatePicker>                                                       
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                    <div class="colum4">
                                                        <asp:Label ID="lblProveedor" runat="server" Text="Proveedor" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                    <div class="colum6">
                                                        <telerik:RadTextBox ID="txtProveedor" runat="server" RenderMode="Lightweight" Width="100%"></telerik:RadTextBox>
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                    <div class="colum4">
                                                        <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                    <div class="colum6">
                                                        <telerik:RadDropDownList ID="ddlEstados" runat="server" RenderMode="Lightweight" Width="100%">
                                                            <Items>
                                                                <telerik:DropDownListItem Value="0" Text="Todos" Selected="true" />
                                                                <telerik:DropDownListItem Value="1" Text="Aprobadas" />
                                                                <telerik:DropDownListItem Value="2" Text="Por Aprobar" />
                                                                <telerik:DropDownListItem Value="3" Text="Anuladas" />
                                                            </Items>
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                    <div class="colum4">
                                                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </div>
                                            </telerik:RadSlidingPane>
                                        </telerik:RadSlidingZone>
                                    </telerik:RadPane>
                                    <telerik:RadPane ID="RadPane2" runat="server"  Scrolling="X" Height="100%">                                        
                                        <telerik:RadGrid ID="grdDocumentos" runat="server"  AutoGenerateColumns="false" Width="100%" Height="100%" OnItemDataBound="grdDocumentos_ItemDataBound"
                                            AllowMultiRowSelection="true">
                                            <ExportSettings Excel-Format="Html" OpenInNewWindow="true"></ExportSettings>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />      
                                                <Selecting AllowRowSelect="True"></Selecting>
                                            </ClientSettings>
                                            <MasterTableView TableLayout="Fixed">
                                                <Columns>

                                                    <telerik:GridTemplateColumn UniqueName="CheckColumn" HeaderText="" HeaderStyle-Width="50px" AllowSorting="true" AllowFiltering="false">
                                                    <ItemTemplate>
                                                    <asp:CheckBox ID="Check" runat="server"  />
                                                    </ItemTemplate>
                                                    </telerik:GridTemplateColumn>


                                                    <telerik:GridClientSelectColumn UniqueName="chkSeleccion" Display="false">
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle Width="20px" />
                                                    </telerik:GridClientSelectColumn>

                                                    <telerik:GridBoundColumn DataField="OP" HeaderText="OP">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="id_agenda" HeaderText="Cod. Agenda">
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Agenda" HeaderText="Agenda"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="noregistro" HeaderText="Nro. OC">
                                                        <HeaderStyle Width="80px" />
                                                        <ItemStyle Width="80px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="fechaOrden" HeaderText="Fecha Orden" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle Width="70px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="fechaentrega" HeaderText="Fecha Entrega" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle Width="70px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda">
                                                        <HeaderStyle Width="120px" />
                                                        <ItemStyle Width="120px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="neto" HeaderText="Neto" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="dcto" HeaderText="Descuento" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="subtotal" HeaderText="Subtotal" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="impuestos" HeaderText="Impuestos" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="total" HeaderText="Total" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="observaciones" HeaderText="Observaciones">
                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle Width="150px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="nombreestado" HeaderText="Estado">
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Anulable" HeaderText="Anulable" DataType="System.Int32" Visible="false" ></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>                                        
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:LayoutColumn>
                        </Columns>
                        <%--<Content>
                            <div style="margin-top:25px;">
                                
                            </div>                            
                        </Content>--%>
                    </telerik:LayoutRow>
                </Rows>                
            </telerik:RadPageLayout>
        </telerik:RadAjaxPanel>

       <%-- FIN LAYOUT --%>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <telerik:RadAjaxPanel ID="ajaxMensaje" runat="server">
        <asp:Label ID="lblRegistros" runat="server"></asp:Label>
        <telerik:RadWindowManager ID="rwmReporte" runat="server"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>    
</asp:Content>
