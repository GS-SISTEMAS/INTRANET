<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpL.Master" AutoEventWireup="true" CodeBehind="frmControlCostosPlanProd.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Costos.frmControlCostosPlanProd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="fila">
        <div class="colum10 containerSubTitulo">
            <asp:Label ID="lblTitulo" runat="server" Text="Planes de producción del mes" CssClass="subTitulo"></asp:Label>
        </div>
    </div>
    <div class="fila">
        <div class="colum10">
            <telerik:RadGrid ID="grdMateriaPrima" runat="server" AutoGenerateColumns="false" Width="100%" Height="430px">
                <MasterTableView ShowFooter="true">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Op" UniqueName="Op" HeaderText="Op">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Concepto" UniqueName="Concepto" HeaderText="Concepto">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FechaHora_Registro" UniqueName="FechaHora_Registro" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nro_OrdenProduccion" UniqueName="Nro_OrdenProduccion" HeaderText="">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="No_Sucursal" UniqueName="No_Sucursal" HeaderText="">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Kardex" UniqueName="Kardex" HeaderText="Kardex">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CodigoProducto" UniqueName="CodigoProducto" HeaderText="Cod.Producto">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Descripcion">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FactorConversion" UniqueName="FactorConversion" HeaderText="Factor" DataFormatString="{0:F0}">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Unidad" UniqueName="Unidad" HeaderText="Unid.">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Cantidad" UniqueName="Cantidad" HeaderText="Cant." DataFormatString="{0:F0}">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CostoTotalSoles" UniqueName="CostoTotalSoles" HeaderText="C.T.Soles" DataFormatString="{0:F2}" Aggregate="Sum">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CostoTotalDolares" UniqueName="CostoTotalDolares" HeaderText="C.T.Dolar" DataFormatString="{0:F2}" Aggregate="Sum">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CostoXUnidad_Soles" UniqueName="CostoXUnidad_Soles" HeaderText="C.U.Soles" DataFormatString="{0:F2}" Aggregate="Sum">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CostoXUnidad_Dolares" UniqueName="CostoXUnidad_Dolares" HeaderText="C.U.Dolar" DataFormatString="{0:F2}" Aggregate="Sum">
                            <HeaderStyle Width=""/>
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                    <Selecting AllowRowSelect="true"/>
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server" ></asp:Label>
        </div>
    </div>
</asp:Content>
