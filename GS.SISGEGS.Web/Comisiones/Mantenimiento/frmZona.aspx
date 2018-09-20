<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmZona.aspx.cs" Inherits="GS.SISGEGS.Web.Comision.Mantenimiento.frmZona" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Zona
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(objZona) {
            window.radopen("frmZonaMng.aspx?objZona=" + objZona, "rwZona");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramZona.ClientID %>").ajaxRequest("Rebind");         
            }
            else {
                $find("<%= ramZona.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramZona" runat="server" OnAjaxRequest="ramZona_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapZona" LoadingPanelID="ralpZona"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramZona">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdZona" LoadingPanelID="ralpZona"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdZona">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapZona" LoadingPanelID="ralpZona"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpZona" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmZona" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwZona" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapZona" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Zona"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblZona" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="cboEmpresa" Enabled="false" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <telerik:RadButton ID="btnBuscar" runat="server" Width="24px" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
            <div class="col-md-5"></div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" Visible="false">
                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdZona" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" OnItemCommand="grdZona_ItemCommand">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("id_zona") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="id_zona" HeaderText="Id_Zona" UniqueName="id_zona">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="zona" HeaderText="Zona" UniqueName="zona">
                                <HeaderStyle Width="150px"/>
                            </telerik:GridBoundColumn>
                     
                            <telerik:GridBoundColumn DataField="porcentajeZona" HeaderText="Porcentaje" DataFormatString="{0:F0}%" UniqueName="porcentajeZona">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="usuarioCreacion" HeaderText="usuarioCreacion" UniqueName="usuarioCreacion">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="fechacreacion" HeaderText="fechacreacion" UniqueName="fechacreacion">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado">
                                <HeaderStyle Width="30px"/>
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
