<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmVentasCobranzas.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Reportes.frmVentasCobranzas" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Estado de cuenta</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
            if (args.get_eventTarget().indexOf("btnExpPDFDetalle") >= 0)
                args.set_enableAjax(false);
        }

        function ShowInsertForm(variable) {
            window.radopen("frmExportarPDFPopup.aspx?strFileNombre=" + variable, "rwExportarPDF");
            return false;
        }

        function AbrirNuevoVentana(variable)
        {
            strCodigoSobre = codSobre;
            strCodigoPais = codPais;

            var surl = 'frmExportarPDFPopup.aspx?strFileNombre=' + variable
            window.open(surl, "", "left=0px,top=0px,height=730x,width=1160px,status=no,toolbar=no,menubar=no,location=no,resizable=yes");
        }

        function ShowCreateViewVenta(cliente, vendedor, year, mes) {
            window.radopen("frmDetalleVenta.aspx?strCliente=" + cliente + "&strVendedor=" + vendedor + "&strYear=" + year + "&strMes=" + mes, "rwDetalleFecha");
            return false;
        }

        function ShowCreateView(cliente,vendedor,year,mes) {
            window.radopen("frmDetalle.aspx?strCliente=" + cliente + "&strVendedor=" + vendedor + "&strYear=" + year + "&strMes=" + mes, "rwDetalleFecha");
            return false;
        }

        function ShowCreateViewVencido(cliente, vendedor, year, mes) {
            window.radopen("frmDetalleVencido.aspx?strCliente=" + cliente + "&strVendedor=" + vendedor + "&strYear=" + year + "&strMes=" + mes, "rwDetalleFecha");
            return false;
        }

        function ShowCreateViewVencidoAfiliado(cliente, vendedor, year, mes) {
            window.radopen("frmDetalleVencidoAfiliado.aspx?strCliente=" + cliente + "&strVendedor=" + vendedor + "&strYear=" + year + "&strMes=" + mes, "rwDetalleFecha");
            return false;
        }
        function ShowCreateViewVencidoLegal(cliente, vendedor, year, mes) {
            window.radopen("frmDetalleVencidoLegal.aspx?strCliente=" + cliente + "&strVendedor=" + vendedor + "&strYear=" + year + "&strMes=" + mes, "rwDetalleFecha");
            return false;
        }

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEstadoCuenta" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdResumenCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdResumenCliente" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpEstadoCuenta" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwDetalleFecha" runat="server" Width="360px" Height="360px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwExportarPDF" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEstadoCuenta" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar ventas x cobranzas" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">

                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Periodo: " Width="116px"></asp:Label>
                        </td>

                        <td >
                            <telerik:RadComboBox ID="cboAnhos" Runat="server" >
                            </telerik:RadComboBox>
                        </td>
                       <td class="auto-style1">
                            &nbsp;</td>
                        <td class="auto-style1">   
                            &nbsp;</td>

                        <td colspan="3"  class="auto-style1">
                            <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </td>

                        <td class="auto-style1">
                            &nbsp;</td>
                        <td class="auto-style1">
                            <asp:ImageButton ID="btnExpDetalle" runat="server" Height="30px" ImageUrl="~/Images/Icons/24_excel.png" OnClick="btnExpDetalle_Click" Visible="False" Width="30px" />
                             <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente / Todos" InputType="Text" 
                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="400px" Visible="false"> 
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmVentasCobranzas.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                        <td class="auto-style1">
                            <asp:ImageButton ID="btnExpPDFDetalle" runat="server"  Height="30px" ImageUrl="~/Images/Icons/24_pdf.png" OnClick="btnExpPDFDetalle_Click" Width="30px" Visible="False" />
                                                    <telerik:RadAutoCompleteBox ID="acbVendedor" runat="server" AllowCustomEntry="true" DropDownHeight="150px" DropDownWidth="300px" EmptyMessage="Selec. vendedor / Todos" 
                                InputType="Text" OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="400px" Visible="false">
                                <WebServiceSettings Method="Agenda_BuscarVendedor" Path="frmVentasCobranzas.aspx" />
                            </telerik:RadAutoCompleteBox>
                        
                        </td>
                    </tr>
                </table>
                <table>
                   <tr>
                        <td>
                           <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Detalle: " Width="116px"></asp:Label>
                           <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                        <td>
                            &nbsp;</td>
                         <td> 
                            
                             &nbsp;&nbsp;</td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
           </div>
           <div class="col-md-12">
                <telerik:RadGrid ID="grdVentasCobranzas" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="280px"  Width="90%"
                    OnNeedDataSource="grdVentasCobranzas_NeedDataSource" AllowSorting="True"
                    OnItemDataBound="grdVentasCobranzas_ItemDataBound"  >

                    <MasterTableView TableLayout="Fixed" DataKeyNames="id_cliente"
                        AllowMultiColumnSorting="true"    >
                        <Columns>
                            <telerik:GridBoundColumn DataField="conceptos" HeaderText="" HeaderStyle-Width="130px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridHyperLinkColumn UniqueName="enero" HeaderText="Enero" DataTextField="enero" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center"   AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right" /> 
                            </telerik:GridHyperLinkColumn>

                             <telerik:GridHyperLinkColumn DataTextField="febrero" HeaderText="Febrero" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center"  
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                            <telerik:GridHyperLinkColumn DataTextField="marzo" HeaderText="Marzo" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center"  
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                             <telerik:GridHyperLinkColumn DataTextField="abril" HeaderText="Abril" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center"  
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                            <telerik:GridHyperLinkColumn DataTextField="mayo" HeaderText="Mayo" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                            <telerik:GridHyperLinkColumn DataTextField="junio" HeaderText="Junio" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center"  
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                            <telerik:GridHyperLinkColumn DataTextField="julio" HeaderText="Julio" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                           <telerik:GridHyperLinkColumn DataTextField="agosto" HeaderText="Agosto" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                           <telerik:GridHyperLinkColumn DataTextField="septiembre" HeaderText="Septiembre" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center"  
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>
                             
                           <telerik:GridHyperLinkColumn DataTextField="octubre" HeaderText="Octubre" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                         <telerik:GridHyperLinkColumn DataTextField="noviembre" HeaderText="Noviembre" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                          <telerik:GridHyperLinkColumn DataTextField="diciembre" HeaderText="Diciembre" HeaderStyle-Width="80px" 
                                DataTextFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                            <telerik:GridBoundColumn DataField="total" HeaderText="Total" HeaderStyle-Width="80px" 
                                DataFormatString="{0:$ #,##0.00}" HeaderStyle-HorizontalAlign="Center"  
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>

                     <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>
            </div>

       <div class="col-md-12">
        <telerik:RadHtmlChart runat="server" ID="rhcVentaCobranza" Width="700px" Height="340px" Skin="Office2010Silver"  >
            <ChartTitle Text="Ventas vs. Cobranzas mes">
                      <Appearance Align="Center" BackgroundColor="Transparent" Position="Top"></Appearance>
            </ChartTitle>
              <PlotArea>
                 <Series >
                    <telerik:ScatterLineSeries ColorField="Red" Name="Ventas" DataFieldX="mes" DataFieldY="ventas" >
                          <MarkersAppearance MarkersType="Circle" />
                        <TooltipsAppearance Color="Black" DataFormatString="{1:$ #,##0.00} en {0} mes"></TooltipsAppearance>
                        <LabelsAppearance  Visible="false">
                        </LabelsAppearance>
                         <MarkersAppearance MarkersType="Circle" />
                    </telerik:ScatterLineSeries>

                    <telerik:ScatterLineSeries  ColorField="Green" Name="Cobranzas" DataFieldX="mes" DataFieldY="cobranzas">
                        <TooltipsAppearance Color="Black" DataFormatString="{1:$ #,##0.00} en {0} mes"></TooltipsAppearance>
                        <LabelsAppearance Visible="false">
                        </LabelsAppearance>
                        <MarkersAppearance MarkersType="Square" />
                    </telerik:ScatterLineSeries>

                </Series>
                  <XAxis MaxValue="12"  >
                    <LabelsAppearance  DataFormatString="{0}"  />
                    <TitleAppearance Text="Mes" />
                    <MajorGridLines Color="#EFEFEF" Width="0" />
                    <MinorGridLines Color="#F7F7F7" Width="0" />
                </XAxis>
                <YAxis>
                    <LabelsAppearance DataFormatString="{0:$ #,##0}" />
                    <TitleAppearance Text="Dollars" />
                    <MajorGridLines Color="#EFEFEF" Width="0" />
                    <MinorGridLines Color="#F7F7F7" Width="0" />
                </YAxis>
            </PlotArea>

        </telerik:RadHtmlChart>
    </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
