<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Usuario.frmUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Mantenimiento de usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <script>
            function ShowCreate(objUsuario) {
            window.radopen("frmUsuarioMng.aspx?objUsuario=" + objUsuario, "rwUsuario");
            return false;
            }

            function ShowCreateEdit(objUsuario) {
                window.radopen("frmUsuarioMngEdit.aspx?objUsuario=" + objUsuario, "rwUsuario");
                return false;
            } 

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramUsuario.ClientID %>").ajaxRequest("Rebind");         
            }
            else {
                $find("<%= ramUsuario.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramUsuario" runat="server" OnAjaxRequest="ramUsuario_AjaxRequest">
       <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="ramUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="grdUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

       </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuario" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmUsuario" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwUsuario" runat="server" Width="410px" Height="450px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <telerik:RadAjaxPanel ID="rapUsuario" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Mantenimiento de usuarios"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="cboEmpresa" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
            </div>
            </div>
         </div>

         <div class="row">
              <div class="col-md-12">
                <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblPerfil" runat="server" Text="Perfil" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="cboPerfil" runat="server" DataTextField="nombrePerfil" DropDownWidth="200px" DataValueField="idPerfil"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
                <div class="col-md-3">
                <telerik:RadTextBox ID="txtBuscar" runat="server" Width="85%" EmptyMessage="Buscar DNI/Nombre/Login"></telerik:RadTextBox>
                <telerik:RadButton ID="btnBuscar" runat="server" Width="24px" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
                <div class="col-md-5">
               </div>
                <div class="col-md-1">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/file-empty-16.png"/>
                </telerik:RadButton>
                </div>
            </div>
        </div>

          <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdUsuario" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" OnItemCommand="grdUsuario_ItemCommand">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("idUsuario") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="NombreEmpresa" HeaderText="Empresa" >
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="idUsuario" HeaderText="idUsuario" Visible="false">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="nroDocumento" HeaderText="NroDocumento" >
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="nombres" HeaderText="Usuario" >
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="loginUsuario" HeaderText="Login" >
                                <HeaderStyle Width="80px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="idPerfil" HeaderText="ID"  Visible="false">
                                <HeaderStyle Width="10px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="nombrePerfil" HeaderText="Perfil" >
                                <HeaderStyle Width="150px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridCheckBoxColumn DataField="activo" HeaderText="Activo" >
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>
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
            <asp:Label ID="lblMensaje" runat="server" ></asp:Label>
        </div>
    </div>
</asp:Content>
