<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmIngresoPersonal.aspx.cs" Inherits="GS.SISGEGS.Web.RRHH.Reportes.frmIngresoPersonal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" >
        
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server">
        <Rows>
            <telerik:LayoutRow>
                <Content>
                    <asp:Label ID="Label1" runat="server" Text="Ingreso de Personal" CssClass="titulo"></asp:Label>
                </Content>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn Span="2">
                        <telerik:RadDatePicker ID="dpFecha" runat="server"></telerik:RadDatePicker>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="1">
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"></telerik:RadButton>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Content>
                    <telerik:RadGrid ID="grdAsistencia" runat="server" AutoGenerateColumns="false" 
                        OnSelectedIndexChanged="grdAsistencia_selectedindexchanged">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Codigo" UniqueName="Codigo" Display="false"> </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Area" HeaderText="Áreas" UniqueName="Area">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Minutos" HeaderText="Minutos" UniqueName="Minutos">
                                    <HeaderStyle Width="5px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnablePostBackOnRowClick="true">
                            <Selecting AllowRowSelect="true" />

                        </ClientSettings>
                    </telerik:RadGrid>
                </Content>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                 <Content>
                    <telerik:RadGrid ID="grdAsistenciaDetalle" runat="server" AutoGenerateColumns="false" >
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ingreso" HeaderText="Ingreso" UniqueName="Ingreso">
                                    <HeaderStyle Width="5px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Turno" HeaderText="Turno" UniqueName="Turno">
                                        <HeaderStyle Width="5px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Minutos" HeaderText="Minutos" UniqueName="Minutos">
                                    <HeaderStyle Width="5px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </Content>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Content>
                    <telerik:RadGrid ID="grdAsistenciaPermisos" runat="server" AutoGenerateColumns="false">
                        <MasterTableView>
                            <Columns>
                                 <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ingreso" HeaderText="Ingreso" UniqueName="Ingreso">
                                    <HeaderStyle Width="5px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Turno" HeaderText="Turno" UniqueName="Turno">
                                        <HeaderStyle Width="5px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Minutos" HeaderText="Minutos" UniqueName="Minutos">
                                    <HeaderStyle Width="5px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />

                        </ClientSettings>
                    </telerik:RadGrid>
                </Content>
            </telerik:LayoutRow>

        </Rows>
    </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
