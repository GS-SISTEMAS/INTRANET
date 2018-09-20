<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpM.Master" AutoEventWireup="true" CodeBehind="EstadoLetrasPieMng.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.EstadoCuenta.EstadoLetrasPieMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showLoadingSign() {
            GetRadWindow()._onWindowUrlChanging();
        }

        function CloseAndRebind(args) {

            Sys.Application.add_load(function () {

                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(args);
                rWindow.close();
            });
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <table style="width:50%;">
        <tr>
            <td></td>
            <td>
                 <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="500" Height="400" Transitions="true" Skin="Silk">
                      <ChartTitle Text="Letras Emitidas">
                            <Appearance Align="Center" Position="Top">
                            </Appearance>
                        </ChartTitle>
                        <Legend>
                            <Appearance Position="Right" Visible="true">
                            </Appearance>
                        </Legend>
                      <PlotArea>
                              <Series>
                                  <telerik:PieSeries StartAngle="90" DataFieldY="SerieDecimal" NameField="AxisX">
                                      
                                      <LabelsAppearance DataFormatString="{0}" Position="OutsideEnd"></LabelsAppearance>
                                      <TooltipsAppearance DataFormatString="{0}" Color="White"></TooltipsAppearance>
                                  </telerik:PieSeries>
                              </Series>
                          </PlotArea>
                  </telerik:RadHtmlChart>
            </td>
            <td>
                 <telerik:RadHtmlChart runat="server" ID="RadHtmlChart2" Width="500" Height="400" Transitions="true" Skin="Silk">
                      <ChartTitle Text="Letras Emitidas">
                            <Appearance Align="Center" Position="Top">
                            </Appearance>
                        </ChartTitle>
                        <Legend>
                            <Appearance Position="Right" Visible="true">
                            </Appearance>
                        </Legend>
                      <PlotArea>
                              <Series>
                                  <telerik:PieSeries StartAngle="90" DataFieldY="SerieDecimal" NameField="AxisX">
                                      
                                      <LabelsAppearance DataFormatString="{0}%" Position="OutsideEnd"></LabelsAppearance>
                                      <TooltipsAppearance DataFormatString="{0}" Color="White"></TooltipsAppearance>
                                  </telerik:PieSeries>
                              </Series>
                          </PlotArea>
                  </telerik:RadHtmlChart>
            </td>
        </tr>
      <%--  <tr>
            <td></td>
            
            <td>&nbsp;</td>
        </tr>--%>
   </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
