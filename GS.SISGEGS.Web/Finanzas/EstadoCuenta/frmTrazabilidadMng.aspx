<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpM.Master" AutoEventWireup="true" CodeBehind="frmTrazabilidadMng.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.EstadoCuenta.frmTrazabilidadMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcelMng") >= 0)
                args.set_enableAjax(false);

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
  <telerik:RadAjaxManager ID="ramTrazabilidad" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="grdHistorial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpTrazabilidad" ControlID="rapTrazabilidad"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnExcelMng">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpTrazabilidad" ControlID="rapTrazabilidad"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <telerik:RadAjaxLoadingPanel ID="ralpTrazabilidad" runat="server"></telerik:RadAjaxLoadingPanel>

  <telerik:RadAjaxPanel ID="rapTrazabilidad" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
    <%--<table style="width:50%;">
        <tr>
            <td></td>
            <td>
                 <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="500px" Height="200px" Visible="True">
                      <PlotArea>
                              <Series>
                                  <telerik:ColumnSeries  Name="Wooden Table" Stacked="false" Gap="1.5" Spacing="0.4" DataFieldY="SerieDecimal">
                                      <Appearance>
                                          <FillStyle BackgroundColor="#d5a2bb"></FillStyle>
                                      </Appearance>
                                      <LabelsAppearance DataFormatString="{0}" Position="OutsideEnd"></LabelsAppearance>
                                      <TooltipsAppearance DataFormatString="{0}" Color="White"></TooltipsAppearance>
                  
                                  </telerik:ColumnSeries>
                  
                              </Series>
                              <Appearance>
                                  <FillStyle BackgroundColor="Transparent"></FillStyle>
                              </Appearance>
                              <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                  Reversed="false" DataLabelsField="AxisX">
                  
                                  <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                                  <TitleAppearance Position="Center" RotationAngle="0" Text="">
                                  </TitleAppearance>
                              </XAxis>
                              <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                  MinorTickType="None" Reversed="false">
                                  <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                                  <TitleAppearance Position="Center" RotationAngle="0" Text="">
                                  </TitleAppearance>
                              </YAxis>
                          </PlotArea>
                      <Legend>
                          <Appearance Visible="false"></Appearance>
                      </Legend>
                      <ChartTitle Text="Histograma"></ChartTitle>
                  </telerik:RadHtmlChart>
            </td>
            <td>
                <telerik:RadButton ID="btnExcelMng" runat="server" Text="Excel" OnClick="btnExcel_OnClick" >
                    <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                </telerik:RadButton>
            </td>
        </tr>
   </table>--%>
    <div class="col-md-10">
                <telerik:RadGrid ID="grdHistorial" runat="server" Width="100%" AutoGenerateColumns="false" Height="200px">
                    <MasterTableView EditMode="PopUp">
                        <Columns>
                            
                            <telerik:GridBoundColumn DataField="id_Letra" HeaderText="id_Letra" UniqueName="id_Letra">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroLetra" HeaderText="NroLetra" UniqueName="NroLetra">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="docOrigen" HeaderText="docOrigen" UniqueName="docOrigen">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NroCuota" HeaderText="NroCuota" UniqueName="NroCuota">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>
                           <%-- <telerik:GridBoundColumn DataField="NroRefinanciaiento" HeaderText="NroRefinanciaiento" UniqueName="NroRefinanciaiento">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" UniqueName="Importe" DataFormatString="${0:##,###0.##}">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            
                        </Columns>
                    </MasterTableView>
                    <ExportSettings Excel-Format="Html" OpenInNewWindow="true"/>
                    <ClientSettings>
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
      </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
