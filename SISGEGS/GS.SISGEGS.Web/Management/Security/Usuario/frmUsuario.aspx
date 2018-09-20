<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="GS.SISGEGS.Web.Management.Security.Usuario.frmUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Mantenimiento de usuarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramUsuario" runat="server"></telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuario" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapUsuario" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Mantenimiento de usuarios"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                <asp:Label ID="lblTexto" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="col-md-3">
                <telerik:RadAutoCompleteBox ID="acbUsuario" runat="server" Width="100%">
                </telerik:RadAutoCompleteBox>
            </div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar">
                    <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div>

        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" ></asp:Label>
        </div>
    </div>
</asp:Content>
