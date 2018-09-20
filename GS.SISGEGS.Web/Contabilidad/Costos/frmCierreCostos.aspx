<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmCierreCostos.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Costos.frmCierreCostos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre - Cierre de ingreso de costos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramCierreCosto" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapCierreCosto" LoadingPanelID="ralpCierreCosto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpCierreCosto" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapCierreCosto" runat="server" Height="95%" Width="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblTitulo" runat="server" Text="Cierre de ingreso de costos" CssClass="titulo"></asp:Label>
                            </div>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="row">
                            <div class="col-md-2">
                                <telerik:RadMonthYearPicker ID="mpPeriodo" runat="server" Width="100%">
                                    <DateInput runat="server" Label="Mes/Año" DateFormat="MM-yyyy">
                                    </DateInput>
                                </telerik:RadMonthYearPicker>
                            </div>
                            <div class="col-md-1">
                                <telerik:RadButton ID="btnGuardar" runat="server" Text="Cierre" OnClick="btnGuardar_Click"></telerik:RadButton>
                            </div>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="90%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadGrid ID="grdCierreCosto" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="anho" UniqueName="anho" HeaderText="Año">
                                                <HeaderStyle Width=""/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="mes" UniqueName="mes" HeaderText="Mes">
                                                <HeaderStyle Width=""/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UsuarioRegistro" UniqueName="UsuarioRegistro" HeaderText="Usu.Registro">
                                                <HeaderStyle Width=""/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="fechaRegistro" UniqueName="fechaRegistro" HeaderText="Fec.Registro">
                                                <HeaderStyle Width=""/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UsuarioModifico" UniqueName="UsuarioModifico" HeaderText="Usu.Modifico">
                                                <HeaderStyle Width=""/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="fechaModifico" UniqueName="fechaModifico" HeaderText="Fec.Modifico">
                                                <HeaderStyle Width=""/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridCheckBoxColumn DataField="activo" UniqueName="activo" HeaderText="Est.">
                                            </telerik:GridCheckBoxColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true"/>
                                    </ClientSettings>
                                </telerik:RadGrid>
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
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
