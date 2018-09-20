<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteCobranzas.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.ReporteCobranza.frmReporteCobranzas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de ventas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

        function ShowReportbySalesman(ID_Vendedor, fechaInicial, fechaFinal) {
            window.open("frmReporteCobranzas_Clientes.aspx?ID_Vendedor=" + ID_Vendedor + "&fechaInicio=" + fechaInicial
                + "&fechaFinal=" + fechaFinal, "rwReporteVenta");
            return false;
        }

        $(document).ready(function () {
            var altura = $(document).height() - 132;
            $('#workplace').css("height", altura + "px");
        });

        $(window).resize(function () {
            var altura = $(document).height() - 132;
            $('#workplace').css("height", altura + "px");
        });

        function Resize()
        {
            var altura = $(document).height() - 142;
            $find("<%= ramReporteVenta.ClientID %>").ajaxRequest('ChangePageSize,' + altura);
            };
        window.onresize = window.onload = Resize;
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramReporteVenta" runat="server" OnAjaxRequest="ramReporteVenta_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlReporteVenta" LoadingPanelID="ralpReporteVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramReporteVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocVenta" LoadingPanelID="ralpReporteVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDocVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlReporteVenta" LoadingPanelID="ralpReporteVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpReporteVenta" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmReporteVenta" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwReporteVenta" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlReporteVenta" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Pronostico de Cobranza:" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">
                <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <Icon PrimaryIconUrl="../../../Images/Icons/excel-16.png"/>
                                    </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div id="workplace" class="col-md-12">
                <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%"
                    Orientation="Vertical">
                    <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                        <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                            <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                <div class="fila">
                                    <div class="colum5">
                                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                                    </div>
                                    <div class="colum5">
                                        <telerik:RadMonthYearPicker ID="rmyReporte" runat="server" Width="100%">
                                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                        </telerik:RadMonthYearPicker>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="colum6">
                                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                            <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png"/>
                                        </telerik:RadButton>
                                    </div>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                    <%--<telerik:RadSplitBar ID="RadSplitBar2" runat="server">
            </telerik:RadSplitBar>--%>
                    <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">
                        <div class="row">
                            <div class="col-md-12">
                                 
                            </div>
                            <div class="col-md-12">
                                <%--<telerik:RadGrid ID="grdDocVenta" runat="server" AutoGenerateColumns="false" Width="100%" Height="500px" 
                                    OnNeedDataSource="grdDocVenta_NeedDataSource" 
                                    OnItemCommand="grdDocVenta_ItemCommand" 
                                    OnItemDataBound="grdDocVenta_ItemDataBound"
                                    ShowFooter="true" 
                                    GridLines="None"
                                    
                                    >
                        
                                    <MasterTableView Height="100%"  ShowGroupFooter="true" >
                                             <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                       <telerik:GridGroupByField FieldAlias="" FieldName="Sectorista_Nombre" />
                                                  
                                                    </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="Sectorista_Nombre"  />
                                                  
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>

                                        <Columns>

                            
                                            <telerik:GridBoundColumn HeaderText="Sectorista" DataField="Sectorista_Nombre" Aggregate="Count">
                                                <HeaderStyle Width="200px"/>
                                                  <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Zona" DataField="Zona_nombre"  >
                                                <HeaderStyle Width="20%"/>
                                                  <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn HeaderText="DeudaNoVencido" Display="true" DataField="ImportePendienteNoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn HeaderText="DeudaVencido" Display="true" DataField="ImportePendienteVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Deuda" Display="true" DataField="ImportePendiente" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="12%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>


                                               <telerik:GridBoundColumn HeaderText="Pron.NoVencido" DataField="ImporteProyectadoNoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn HeaderText="Pron.Vencido" DataField="ImporteProyectadoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn HeaderText="Pronostico" DataField="ImporteProyectado" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="8%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                               <telerik:GridBoundColumn HeaderText="CobradoNoVencido" DataField="ImporteCobradoNoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="12%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn HeaderText="CobradoVencido" DataField="ImporteCobradoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Cobrado" DataField="ImporteCobrado" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Diferencia" DataField="Diferencia" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="6%"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avance" DataField="AvanceCobrado" DataFormatString="{0:F0}%">
                                                <HeaderStyle Width="6%"/>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Graf.">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibKPIs" runat="server" CommandArgument='<%#Eval("ID_Zona")+","+ Eval("año")+","+ Eval("mes")%>'
                                                        CommandName="Grafico" ImageUrl="~/Images/Icons/analytics-16.png"/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"/>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                        <Selecting AllowRowSelect="true"/>
                                    </ClientSettings>
                                </telerik:RadGrid>--%>
                            </div>
                            <div class="col-md-12">
                                <telerik:RadGrid  Width="100%" Height="500px"
                                    ID="grdDocVenta"
                                    OnNeedDataSource="grdDocVenta_NeedDataSource" 
                                    OnItemCommand="grdDocVenta_ItemCommand" 
                                    OnItemDataBound="grdDocVenta_ItemDataBound"

                                  
                                    ShowGroupPanel="false" 
                                    AutoGenerateColumns="false" 
                                    AllowFilteringByColumn="false" 
                                    AllowSorting="True"
                                    ShowFooter="True" 
                                    runat="server" 
                                    GridLines="None" 
                                    AllowPaging="false" 
                                    EnableLinqExpressions="false"
                                    >
       
                                    <MasterTableView ShowGroupFooter="true" AllowMultiColumnSorting="true">
                                           <Columns>

                            
                                            <telerik:GridBoundColumn HeaderText="Sectorista" DataField="Sectorista_Nombre" Aggregate="Count">
                                                <HeaderStyle Width="200px"/>
                                                  <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Zona" DataField="Zona_nombre"  >
                                                <HeaderStyle Width="20%"/>
                                                  <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn HeaderText="DeudaNoVencido" Display="true" DataField="ImportePendienteNoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn HeaderText="DeudaVencido" Display="true" DataField="ImportePendienteVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Deuda" Display="true" DataField="ImportePendiente" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="12%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>


                                               <telerik:GridBoundColumn HeaderText="Pron.NoVencido" DataField="ImporteProyectadoNoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn HeaderText="Pron.Vencido" DataField="ImporteProyectadoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="10%"/>
                                                <FooterStyle Font-Bold="true"/>

                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn HeaderText="Pronostico" DataField="ImporteProyectado" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="8%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                           
                                               <telerik:GridBoundColumn HeaderText="Presupuesto" DataField="Presupuesto" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="9%"/>
                                                <FooterStyle Font-Bold="true"/>
                                              </telerik:GridBoundColumn>


                                               <telerik:GridBoundColumn HeaderText="CobradoNoVencido" DataField="ImporteCobradoNoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="14%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                               <telerik:GridBoundColumn HeaderText="CobradoVencido" DataField="ImporteCobradoVencido" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="13%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Cobrado" DataField="ImporteCobrado" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="9%"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>


                                            <telerik:GridBoundColumn HeaderText="Diferencia" DataField="Diferencia" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="8%"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avance" DataField="AvanceCobrado" DataFormatString="{0:F0}%">
                                                <HeaderStyle Width="7%"/>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Graf.">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibKPIs" runat="server" CommandArgument='<%#Eval("ID_Zona")+","+ Eval("año")+","+ Eval("mes")%>'
                                                        CommandName="Grafico" ImageUrl="~/Images/Icons/analytics-16.png"/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"/>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="Sectorista_Nombre"></telerik:GridGroupByField>
                                                </GroupByFields>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="Sectorista_Nombre" HeaderText="Sec"></telerik:GridGroupByField>
                                                </SelectFields>
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>
                                    </MasterTableView>
                                    <ClientSettings AllowDragToGroup="false">
                                          <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                          <Selecting AllowRowSelect="true"/>
                                    </ClientSettings>
                                    <GroupingSettings ShowUnGroupButton="false"></GroupingSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
