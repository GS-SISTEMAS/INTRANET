<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmMantenimientoModulos.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Planificacion.frmMantenimientoModulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Modulos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowUpdateMod(objMantModulos) {
            window.radopen("frmMantModulosMng.aspx?objMantModulos=" + objMantModulos, "rwMantModulos");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                debugger;
                $find("<%= ramMantModulos.ClientID %>").ajaxRequest("Rebind"); 
            }
            else {
                $find("<%= ramMantModulos.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramMantModulos" runat="server" OnAjaxRequest="ramMantModulos_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdMantModulos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMantModulos" LoadingPanelID="ralpMantModulos"></telerik:AjaxUpdatedControl>                    
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpMantModulos" runat="server">
    </telerik:RadAjaxLoadingPanel>
    
    <telerik:RadWindowManager ID="rwmMantModulos" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwMantModulos" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapMantModulos" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de MantModulos"></asp:Label>
            </div>
        </div>
       
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdMantModulos" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" OnItemCommand="grdMantModulos_ItemCommand">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("id_Modulo") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="nombre" HeaderText="Modulo" UniqueName="nombre">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="id_Modulo" HeaderText="id_Modulo" UniqueName="id_Modulo">
                                <HeaderStyle Width="10px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Detalle" HeaderText="Detalle" UniqueName="Detalle">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="id_Agenda" HeaderText="id_Agenda" UniqueName="id_Agenda" Visible="False">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AgendaNombre" HeaderText="Responsable" UniqueName="AgendaNombre">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="id_Estado" HeaderText="id_Estado" UniqueName="id_Estado" Visible="False">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>
                            
                            
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
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
