<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpL.Master" AutoEventWireup="true" CodeBehind="frmDocumentos.aspx.cs" Inherits="GS.SISGEGS.Web.Commercial.Pedido.frmDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CancelEdit() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramDocumento" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="ralpDocumento" runat="server" ZIndex="9999">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="rapDocumento" runat="server" Width="100%">
        <div class="fila containerSubTitulo">
            <div class="colum7">
                <asp:Label ID="lblTituloGuia" runat="server" Text="Estado del Pedido" CssClass="subTitulo"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <telerik:RadGrid ID="grdDocGuia" runat="server" AutoGenerateColumns="false" Width="100%" Height="435px">
                    <MasterTableView Width="1060px">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nom. Producto" UniqueName="Nombre">
                                <HeaderStyle Width="200px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OpPedido" HeaderText="OpPedido" UniqueName="OpPedido">
                                <HeaderStyle Width="60px" />
                                <ItemStyle BackColor="#D1EAF6"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NroPedido" HeaderText="NroPed" UniqueName="NroPedido">
                                <HeaderStyle Width="100px" />
                                <ItemStyle BackColor="#D1EAF6"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaPedido" HeaderText="FecPed" UniqueName="FechaPedido">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#D1EAF6"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CantPedido" HeaderText="CantPed" UniqueName="CantPedido" DataFormatString="{0:F0}">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#D1EAF6"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ImporteOrden" HeaderText="Imp.Orden" UniqueName="ImporteOrden" DataFormatString="{0:F3}">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#D1EAF6"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NroGuia" HeaderText="NroGuia" UniqueName="NroGuia">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FFFAF0"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaGuia" HeaderText="FecGuia" UniqueName="FechaGuia">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FFFAF0"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CantGuia" HeaderText="CantGuia" UniqueName="CantGuia" DataFormatString="{0:F0}">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FFFAF0"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NroFactura" HeaderText="NroFact" UniqueName="NroFactura">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FDE4EA"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaFactura" HeaderText="FecFact" UniqueName="FechaFactura">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FDE4EA"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CantFact" HeaderText="CantFact" UniqueName="CantFact" DataFormatString="{0:F0}">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FDE4EA"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ImporteFact" HeaderText="Imp.Fact" UniqueName="ImporteFact" DataFormatString="{0:F2}">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FDE4EA"/>
                            </telerik:GridBoundColumn>                            
                            <telerik:GridBoundColumn DataField="NetoFact" HeaderText="NetoFact" UniqueName="NetoFact">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FDE4EA"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TotalFact" HeaderText="TotalFact" UniqueName="TotalFact">
                                <HeaderStyle Width="80px"/>
                                <ItemStyle BackColor="#FDE4EA"/>
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>
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
