<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMS.Master" AutoEventWireup="true" CodeBehind="frmOrdenVentaSecAprob2.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Aprobacion.frmOrdenVentaSecAprob2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>
    
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

        

    </script>
    

    <style type="text/css">
        .LabelPaddingStyle {
            padding:0px 10px 0px 0px
        }
    </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <telerik:RadAjaxManager ID="ramPedidoItem" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoItem" LoadingPanelID="ralpPedidoItem" ></telerik:AjaxUpdatedControl>
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPedidoItem" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmOrdenVenta" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel runat="server" ID="rapPedidoItem">
        <telerik:RadPageLayout ID="RadPageLayout4" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
            <Columns>
                <telerik:LayoutColumn>
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Verificación de Deudas"></asp:Label>
                </telerik:LayoutColumn>
            </Columns>
        </telerik:LayoutRow>
        <telerik:LayoutRow CssClass="containerSubTitulo">
            <Content>
                <div class="col-xs-3">
                    <asp:Label ID="Label3" runat="server"  CssClass="subTitulo"></asp:Label>
                </div>
            </Content>
        </telerik:LayoutRow>
        <telerik:LayoutRow>
            <Content>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-xs-4">
                            <telerik:RadTextBox ID="txtRuccliente" runat="server" Label="Ruc :" LabelWidth="30%" ReadOnly="true"></telerik:RadTextBox>
                        </div>
                        <div class="col-xs-8">
                            <telerik:RadTextBox ID="txtCliente" runat="server" Label="Cliente :" Width="100%" LabelWidth="20%" ReadOnly="true"></telerik:RadTextBox>
                        </div>
                        
                    </div>
                    

                    <div class="row">
                        <div class="col-xs-7">
                            <asp:label Text="Deuda Vencida Mas de 10 Días en $" runat="server" CssClass="etiqueta" Width="100%"></asp:label>
                        </div>
                        <div class="col-xs-5">
                            <%--<telerik:RadTextBox ID="txtTotalDeudaVen" runat="server" ></telerik:RadTextBox>--%>
                        </div>
                        
                    </div>
                    <div class="row">

                        <div class="col-xs-4">
                            <asp:Label runat="server" Text="Silvestre" Font-Size="12px" CssClass="LabelPaddingStyle" ></asp:Label>
                            <telerik:RadTextBox ID="txtTotalDeudaVenS" runat="server" Width="50%" ReadOnly="true" ></telerik:RadTextBox>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label runat="server" Text="NeoAgrum" Font-Size="12px" CssClass="LabelPaddingStyle" ></asp:Label>
                            <telerik:RadTextBox ID="txtTotalDeudaVenN" runat="server" Width="50%" ReadOnly="true"></telerik:RadTextBox>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label runat="server" Text="Inatec" Font-Size="12px" CssClass="LabelPaddingStyle"></asp:Label>
                            <telerik:RadTextBox ID="txtTotalDeudaVenI" runat="server" Width="50%" ReadOnly="true"></telerik:RadTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-7">
                            
                            <asp:label Text="Letras x Aceptar mayor a 30 Días en $" runat="server" CssClass="etiqueta"></asp:label>
                        </div>
                        <div class="col-xs-5">
                            <%--<telerik:RadTextBox ID="txtTotalLetras" runat="server" ></telerik:RadTextBox>--%>
                        </div>
                        
                    </div>

                    <div class="row">
                        <div class="col-xs-4">
                            <asp:Label runat="server" Text="Silvestre" Font-Size="12px" CssClass="LabelPaddingStyle" ></asp:Label>
                            <telerik:RadTextBox ID="txtTotalLetrasS" runat="server" Width="50%" ReadOnly="true"></telerik:RadTextBox>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label runat="server" Text="NeoAgrum" Font-Size="12px" CssClass="LabelPaddingStyle"></asp:Label>
                            <telerik:RadTextBox ID="txtTotalLetrasN" runat="server" Width="50%" ReadOnly="true"></telerik:RadTextBox>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label runat="server" Text="Inatec" Font-Size="12px" CssClass="LabelPaddingStyle"></asp:Label>
                            <telerik:RadTextBox ID="txtTotalLetrasI" runat="server" Width="50%" ReadOnly="true"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                             <asp:Label runat="server" Text="Comentarios" Font-Size="12px" CssClass="LabelPaddingStyle" ></asp:Label>
                            <telerik:RadTextBox ID="txtcomentarios" runat="server" Width="100%" TextMode="MultiLine" ></telerik:RadTextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-3">
                            <telerik:RadButton ID="btnAprobar" runat="server" Text="Aprobar" OnClick="btnAprobar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/sign-check-16.png"/>
                            </telerik:RadButton>
                        </div>
                        <div class="col-xs-3">
                               <telerik:RadButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click">
                                    <Icon PrimaryIconUrl="../../Images/Icons/sign-error-16.png"/>
                                </telerik:RadButton>
                        </div>
                        <div class="col-xs-6">

                        </div>
                    </div>
                </div>
                
            </Content>
        </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
        
    </telerik:RadAjaxPanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
</asp:Content>
