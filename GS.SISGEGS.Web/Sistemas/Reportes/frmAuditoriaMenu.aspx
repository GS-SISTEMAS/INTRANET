<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmAuditoriaMenu.aspx.cs" Inherits="GS.SISGEGS.Web.Sistemas.Reportes.frmAuditoriaMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de auditoría por páginas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramRepAuditoriaPag" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapRepAuditoriaPag" LoadingPanelID="ralpRepAuditoriaPag"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpRepAuditoriaPag" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapRepAuditoriaPag" runat="server" Height="100%" Width="100%">
        <telerik:RadPageLayout ID="rplAuditoriaMenu" runat="server" Height="95%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <asp:Label ID="lblTitulo" runat="server" Text="Reporte de auditoría de páginas" CssClass="titulo"></asp:Label>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="100%">
                    <Content>
                        <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Vertical">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                        EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                        <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Width="100%" Height="100%">
                                            <Rows>
                                                <telerik:LayoutRow>
                                                    <Columns>
                                                        <telerik:LayoutColumn Span="3">
                                                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
                                                        </telerik:LayoutColumn>
                                                        <telerik:LayoutColumn Span="9">
                                                            <telerik:RadComboBox ID="cboEmpresa" runat="server" DataValueField="idEmpresa" DataTextField="nombreComercial"></telerik:RadComboBox>
                                                        </telerik:LayoutColumn>
                                                    </Columns>
                                                </telerik:LayoutRow>
                                                <telerik:LayoutRow>
                                                    <Columns>
                                                        <telerik:LayoutColumn Span="3">
                                                            <asp:Label ID="lblFechaInicio" runat="server" Text="Desde:" CssClass="etiqueta"></asp:Label>
                                                        </telerik:LayoutColumn>
                                                        <telerik:LayoutColumn Span="9">
                                                            <telerik:RadDatePicker ID="dpFechaInicio" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </telerik:LayoutColumn>
                                                    </Columns>
                                                </telerik:LayoutRow>
                                                <telerik:LayoutRow>
                                                    <Columns>
                                                        <telerik:LayoutColumn Span="3">
                                                            <asp:Label ID="lblFechaFinal" runat="server" Text="Hasta:" CssClass="etiqueta"></asp:Label>
                                                        </telerik:LayoutColumn>
                                                        <telerik:LayoutColumn Span="9">
                                                            <telerik:RadDatePicker ID="dpFechaFinal" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </telerik:LayoutColumn>
                                                    </Columns>
                                                </telerik:LayoutRow>
                                                <telerik:LayoutRow>
                                                    <Columns>
                                                        <telerik:LayoutColumn Span="6">
                                                            <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                                <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                                                            </telerik:RadButton>
                                                        </telerik:LayoutColumn>
                                                    </Columns>
                                                </telerik:LayoutRow>
                                            </Rows>
                                        </telerik:RadPageLayout>
                                    </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                            </telerik:RadPane>
                            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%" Scrolling="None">
                                <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
                                    <Rows>
                                        <telerik:LayoutRow Height="100%">
                                            <Columns>
                                                <telerik:LayoutColumn Height="100%" Span="12">
                                                    <telerik:RadPivotGrid ID="rpgAuditoriaMenu" runat="server" Width="100%" Height="100%" AllowFiltering="false" ShowFilterHeaderZone="false" 
                                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true">
                                                        <Fields>
                                                            <telerik:PivotGridRowField DataField="Usuario" ZoneIndex="0">
                                                            </telerik:PivotGridRowField>
                                                            <telerik:PivotGridRowField DataField="Pagina" ZoneIndex="1">
                                                            </telerik:PivotGridRowField>
                                                            <telerik:PivotGridColumnField DataField="Fecha" DataFormatString="{0:dd/MM}">
                                                            </telerik:PivotGridColumnField>
                                                            <telerik:PivotGridAggregateField DataField="idMenu" Aggregate="Count">
                                                            </telerik:PivotGridAggregateField>
                                                        </Fields>
                                                        <TotalsSettings GrandTotalsVisibility="None"/>
                                                        <ClientSettings EnableFieldsDragDrop="false">
                                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                                            <Resizing AllowColumnResize="true" EnableRealTimeResize="true"/>
                                                        </ClientSettings>
                                                    </telerik:RadPivotGrid>
                                                </telerik:LayoutColumn>
                                            </Columns>
                                        </telerik:LayoutRow>
                                    </Rows>
                                </telerik:RadPageLayout>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </Content>
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
