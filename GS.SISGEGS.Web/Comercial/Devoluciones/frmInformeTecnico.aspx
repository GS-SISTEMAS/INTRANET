<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmInformeTecnico.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Devoluciones.frmInformeTecnico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Informe técnico
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramInforme" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="" LoadingPanelID="ralpInforme"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpInforme" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmInforme" runat="server"></telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapInforme" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-12">
                            <asp:Label ID="lblTitulo" runat="server" Text="Informe técnico" CssClass="titulo"></asp:Label>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-1">
                            <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <telerik:RadTextBox ID="txtCliente" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblMotivo" runat="server" Text="Motivo" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-5">
                            <telerik:RadTextBox ID="txtMotivo" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-1">
                            <asp:Label ID="lblGuia" runat="server" Text="Guia" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <telerik:RadTextBox ID="txtGuia" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblFecRecepcion" runat="server" Text="Fec.Recepción" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <telerik:RadDatePicker ID="dpFecRecep" runat="server" DateInput-DateFormat="dd/MM/yyyy" Width="100%"></telerik:RadDatePicker>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-1">
                            <asp:Label ID="lblEvaluador" runat="server" Text="Evaluador" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-md-5">
                            <telerik:RadTextBox ID="txtEvaluador" runat="server" Width="100%"></telerik:RadTextBox>
                        </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
