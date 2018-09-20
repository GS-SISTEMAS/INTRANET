<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpL.Master" AutoEventWireup="true" CodeBehind="frmControlCostosHistorial.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Costos.frmControlCostosHistorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="fila">
        <div class="colum10">
            <telerik:RadHtmlChart ID="rhcCostos" runat="server" Width="100%" Height="450px" Skin="Bootstrap" Transitions="true">
                <ChartTitle Text="Historial de costo real vs costo estandar">
                    <Appearance Align="Center" Position="Top">
                    </Appearance>
                </ChartTitle>
                <Legend>
                    <Appearance BackgroundColor="Transparent" Position="Bottom">
                    </Appearance>
                </Legend>
                <PlotArea>
                    <XAxis DataLabelsField="Periodo">
                        <TitleAppearance Text="Periodo">
                        </TitleAppearance>
                    </XAxis>
                    <YAxis>
                        <TitleAppearance Text="Costo">
                        </TitleAppearance>
                    </YAxis>
                    <Series>
                        <telerik:LineSeries DataFieldY="CostoVentaReal" Name="Costo Real">
                        </telerik:LineSeries>
                        <telerik:LineSeries DataFieldY="CostoVentaEstandar" Name="Costo Estandar">
                        </telerik:LineSeries>
                    </Series>
                </PlotArea>
            </telerik:RadHtmlChart>
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
