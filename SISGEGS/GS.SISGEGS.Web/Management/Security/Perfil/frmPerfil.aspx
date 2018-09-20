<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmPerfil.aspx.cs" Inherits="GS.SISGEGS.Web.Management.Security.Perfil.frmPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de perfil
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxPanel ID="rapPerfil" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Perfil"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="cboEmpresa" runat="server"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <telerik:RadTextBox ID="txtBuscar" runat="server" Width="85%" EmptyMessage="Buscar"></telerik:RadTextBox>
                <telerik:RadButton ID="btnBuscar" runat="server" Width="24px">
                    <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdPerfil" runat="server" Width="100%" AutoGenerateColumns="false">
                    <MasterTableView Width="1000px">
                        <Columns>
                            <telerik:GridBoundColumn DataField="" HeaderText="">
                                <HeaderStyle Width="10px"/>
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
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
