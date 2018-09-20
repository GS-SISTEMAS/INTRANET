<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmMetaPresupuestoResumen.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmMetaPresupuestoResumen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Resumen de Presupuesto
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>

    <script type="text/javascript">
             function requestStart(sender, args) {
                 
                 if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                     args.set_enableAjax(false);
                 if (args.get_eventTarget().indexOf("btnExcelZona") >= 0)
                     args.set_enableAjax(false);
        }
     </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpPre" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmPre" runat="server" EnableShadow="true">
        <Windows>
             <telerik:RadWindow ID="rwPre" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlPre" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart"  >
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow >
                    <Content>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Resumen de Presupuestos Confirmados"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" Style="top: 1px; left: 3px" Width="100px">
                                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                    </telerik:RadButton>
                                </div>

                            </div>
                        </div>
                        

                    </Content>
                    <%--<Columns>
                        <telerik:LayoutColumn Span="11">
                            
                        </telerik:LayoutColumn>
                       

                        
                    </Columns>--%>
                </telerik:LayoutRow>

                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn>

                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="550px" Skin="Office2010Silver">
                                <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None" Skin="Office2010Silver">
                                    <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px" Skin="Office2010Silver">
                                        <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="300px" Title="Filtros de Busqueda"
                                            EnableDock="true" MinWidth="225" MinHeight="225" Scrolling="None" Skin="Office2010Silver">
                                            <div class="fila">
                                                <div class="colum3">
                                                    <asp:Label ID="lblFechaInicio" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum7">
                                                    <telerik:RadMonthYearPicker ID="rmyPre" runat="server">
                                                                <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                                    </telerik:RadMonthYearPicker>
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
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="550px">
                                    <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%">

                                        <telerik:LayoutRow Height="100%">
                                            <Content>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <telerik:RadPivotGrid AllowPaging="true" PageSize="100" Height="530px"
                                                            ID="rpgResumenZona" runat="server" RowGroupsDefaultExpanded="false"
                                                            AutoGenerateColumns="False" ShowFooter="true" OnNeedDataSource="rpgResumenZona_NeedDataSource"
                                                             OnCellDataBound="rpgResumenZona_CellDataBound" OnPivotGridCellExporting="rpgResumenZona_PivotGridCellExporting"
                                                            Skin="Office2010Silver" >
                                                            <ClientSettings EnableFieldsDragDrop="false">
                                                                <Scrolling AllowVerticalScroll="true"></Scrolling>

                                                            </ClientSettings>
                                                            <Fields>
                                                                <telerik:PivotGridRowField DataField="Zona" ZoneIndex="0" CellStyle-Width="250px">
                                                                    <CellStyle Width="230px" Font-Size="X-Small" />
                                                                </telerik:PivotGridRowField>

                                                                <telerik:PivotGridRowField DataField="Vendedor" ZoneIndex="1" CellStyle-Width="250px">
                                                                    <CellStyle Width="230px" Font-Size="X-Small" />
                                                                </telerik:PivotGridRowField>

                                                                <%--<telerik:PivotGridColumnField DataField="NombrePromotor">
                                                                    <CellStyle Width="100px" Font-Size="X-Small" />
                                                                </telerik:PivotGridColumnField>--%>


                                                                <telerik:PivotGridAggregateField DataField="TotalActual" Aggregate="Sum" DataFormatString="${0:N}" Caption="Presupuesto $" GrandTotalAggregateFormatString="${0:N}">
                                                                    <CellStyle Width="100px" Font-Size="X-Small" />
                                                                    <HeaderCellTemplate>
                                                                        $ Total Confirmado
                                                                    </HeaderCellTemplate>
                                                                    <ColumnGrandTotalHeaderCellTemplate>
                                                                        Total
                                                                    </ColumnGrandTotalHeaderCellTemplate>
                                                                </telerik:PivotGridAggregateField>

                                                                <telerik:PivotGridAggregateField DataField="TotalAnnoAnterior" Aggregate="Sum" DataFormatString="${0:N}" Caption="Ventas Año Anterior" GrandTotalAggregateFormatString="${0:N}">
                                                                    <CellStyle Width="100px" Font-Size="X-Small" />
                                                                    <HeaderCellTemplate>
                                                                        $ Ventas Año Anterior
                                                                    </HeaderCellTemplate>
                                                                    <ColumnGrandTotalHeaderCellTemplate>
                                                                        Total
                                                                    </ColumnGrandTotalHeaderCellTemplate>
                                                                </telerik:PivotGridAggregateField>

                                                                <telerik:PivotGridAggregateField DataField="TotalOficial" Aggregate="Sum" DataFormatString="${0:N}" Caption="Presupuesto Oficial" GrandTotalAggregateFormatString="${0:N}">
                                                                    <CellStyle Width="100px" Font-Size="X-Small" />
                                                                    <HeaderCellTemplate>
                                                                        Presupuesto Oficial
                                                                    </HeaderCellTemplate>
                                                                    <ColumnGrandTotalHeaderCellTemplate>
                                                                        Total
                                                                    </ColumnGrandTotalHeaderCellTemplate>
                                                                </telerik:PivotGridAggregateField>


                                                                <telerik:PivotGridAggregateField DataField="Variacion" Aggregate="Sum" DataFormatString="{0:N}%" Caption="Crecimiento" GrandTotalAggregateFormatString="%{0:N}">
                                                                    <CellStyle Width="100px" Font-Size="X-Small" />
                                                                    <HeaderCellTemplate>
                                                                        % Crecimiento
                                                                    </HeaderCellTemplate>
                                                                    <ColumnGrandTotalHeaderCellTemplate>
                                                                        Total
                                                                    </ColumnGrandTotalHeaderCellTemplate>
                                                                </telerik:PivotGridAggregateField>
                                                            </Fields>
                                        
                                                        </telerik:RadPivotGrid>
                                                    </div>
                                                </div>

                                            </Content>
                                        </telerik:LayoutRow>

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
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
