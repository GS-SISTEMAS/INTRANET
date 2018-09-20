<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmGastosResumen.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Gastos.frmGastosResumen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"></telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="fila">
            <asp:Label ID="lblTitulo" runat="server" Text="Informe de gastos" CssClass="titulo"></asp:Label>
        </div>
        <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblfecha" runat="server" Text="Fecha:" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum1">
                <asp:Label ID="lblNroDocumento" runat="server" Text="Nro Doc.:" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                <asp:Label ID="lblSerieNro" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblVendedor" runat="server" Text="Vendedor: " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum9">
                <asp:Label ID="lblNomVendedor" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblDesde" runat="server" Text="Desde: " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <asp:Label ID="lblFechaInicio" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum1">
                <asp:Label ID="lblHasta" runat="server" Text="Hasta: " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <asp:Label ID="lblFechaFinal" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblProposito" runat="server" Text="Proposito: " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum9">
                <asp:Label ID="lblMotivo" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <telerik:RadPivotGrid ID="grdResumen" runat="server" ShowFilterHeaderZone="false" AllowFiltering="false" Width="720px" Height="290px" 
                    ShowColumnHeaderZone="false" ShowRowHeaderZone="false" ShowDataHeaderZone="false">
                    <Fields>
                        <telerik:PivotGridRowField DataField="Item" ZoneIndex="0">
                            <CellStyle Width="220px"/>
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridColumnField DataField="FechaEmision" DataFormatString="{0:dd/MM}">
                            <CellStyle Width="50px"/>
                        </telerik:PivotGridColumnField>
                        <telerik:PivotGridAggregateField DataField="Importe" Aggregate="Sum"></telerik:PivotGridAggregateField>
                    </Fields>
                    <ClientSettings>
                        <Scrolling AllowVerticalScroll="true"/>
                    </ClientSettings>
                    <ColumnGrandTotalCellStyle Width="50px"/>
                </telerik:RadPivotGrid>
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
