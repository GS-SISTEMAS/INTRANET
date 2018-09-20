<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmControlCostos.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Costos.frmControlCostos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre - Control de costos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
                args.set_enableAjax(false);
        }

        function ShowForm(kardex, periodo) {
            window.radopen("frmControlCostosPlanProd.aspx?kardex=" + kardex + "&periodo=" + periodo, "rwControl");
            return false;
        }

        function ShowHistorial(kardex, periodo) {
            window.radopen("frmControlCostosHistorial.aspx?kardex=" + kardex + "&periodo=" + periodo, "rwControl");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramControl" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapControl" LoadingPanelID="ralpControl"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdControlCostos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapControl" LoadingPanelID="ralpControl"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpControl" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwnControl" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwControl" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapControl" runat="server" Height="100%" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow Height="4%">
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" Text="Control de costos" CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <asp:ImageButton ID="ibExcel" runat="server" Height="20px" Width="20px" ImageUrl="~/Images/Icons/24_excel.png" OnClick="ibExcel_Click"
                                AlternateText="ExcelML" ToolTip="Descargar Excel" />
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="94%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                    <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                        <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda" EnableDock="false"
                                            MinWidth="225" MinHeight="225" Scrolling="None" IconUrl="~/Images/Icons/filter-16.png">
                                            <div class="fila">
                                                <div class="colum3">
                                                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum7">
                                                    <telerik:RadComboBox ID="cboPeriodo" runat="server" Width="100%" DataTextField="periodo" DataValueField="codigo">
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
                                    <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%" Width="100%">
                                        <Rows>
                                            <telerik:LayoutRow Height="100%">
                                                <Content>
                                                    <telerik:RadGrid ID="grdControlCostos" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%" 
                                                        OnNeedDataSource="grdControlCostos_NeedDataSource" OnItemDataBound="grdControlCostos_ItemDataBound"
                                                        OnItemCommand="grdControlCostos_ItemCommand">
                                                        <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Kardex" UniqueName="Kardex" HeaderText="Kardex" AllowFiltering="true" FilterDelay="2000" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                                                    <HeaderStyle Width="60px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Descripcion" AllowFiltering="true" FilterDelay="2000" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                                    <HeaderStyle Width="450px" />
                                                                </telerik:GridBoundColumn>
                                                                <%--<telerik:GridBoundColumn DataField="KgLt" UniqueName="KgLt" HeaderText="KgLt" AllowFiltering="false">
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridBoundColumn>--%>
                                                                <telerik:GridBoundColumn DataField="CostoVentaReal" UniqueName="CostoVentaReal" HeaderText="Costo real(CR)" DataFormatString="${0:F3}" AllowFiltering="false">
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CostoVentaEstandar" UniqueName="CostoVentaEstandar" HeaderText="Costo Std.(CE)" DataFormatString="${0:F3}" AllowFiltering="false">
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="CostoVentaCalculado" UniqueName="CostoVentaCalculado" HeaderText="Costo Cal.(CC)" DataFormatString="${0:F3}" AllowFiltering="false">
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PorcEstandar" UniqueName="PorcEstandar" HeaderText="CR/CE" DataFormatString="{0:F0}%" AllowFiltering="false">
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PorcCalculado" UniqueName="PorcCalculado" HeaderText="CR/CC" DataFormatString="{0:F0}%" AllowFiltering="false">
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PorcEstandar" UniqueName="PSTD" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PorcCalculado" UniqueName="PCAL" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Ord.Prod." AllowFiltering="false">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ibOrdenProd" runat="server" ImageUrl="~/Images/Icons/notepad-16.png" CommandArgument='<%# Eval("Kardex") %>' CommandName="OrdenProd"/>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="40px"/>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="His.Costo" AllowFiltering="false">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ibHistCosto" runat="server" ImageUrl="~/Images/Icons/analytics-16.png" CommandArgument='<%# Eval("Kardex") %>' CommandName="HistControl"/>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="40px"/>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings>
                                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </Content>
                                            </telerik:LayoutRow>
                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" ></asp:Label>
        </div>
    </div>
</asp:Content>
