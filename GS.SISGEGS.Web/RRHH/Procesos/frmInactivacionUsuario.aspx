<%@ Page Language="C#" Title="Grupo Silvestre" MasterPageFile="~/Security/mstPage.Master"  AutoEventWireup="true" CodeBehind="frmInactivacionUsuario.aspx.cs" Inherits="GS.SISGEGS.Web.RRHH.Procesos.frmInactivacionUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Mantenimiento de usuarios
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function Confirm() {
            var Result = confirm("Esta Seguro?");
            var confirm_value = document.querySelector('[name="confirm_value"]');
            if (Result) {
                return true;
            } else {
                return false;
            }
        }

    </script>


        <script>
            function ShowCreate(objUsuario) {
                window.radopen("frmInactivacionUsuarioMng.aspx?objUsuario=" + objUsuario, "rwUsuario");
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
    <telerik:RadAjaxManager ID="ramUsuario" runat="server" OnAjaxRequest="ramUsuario_AjaxRequest" >
       <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="ibDesactivar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
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
                <asp:Label ID="Label2" runat="server" Text="Inactivación  de Usuarios" CssClass="titulo"></asp:Label>
            </div>
        </div>
     

         <div class="row">
            <div class="col-md-1">
                <asp:Label ID="Label1"    runat="server" Text="Buscar" CssClass="etiqueta" Width="100%"></asp:Label>
            </div>
            <div class="col-md-2">
                <telerik:RadTextBox ID="txtBuscar" runat="server" Width="85%" EmptyMessage="Buscar Nombre/Login"></telerik:RadTextBox>
                
            </div>
             <div class="col-md-2">
                 <telerik:RadButton ID="btnBuscar" runat="server" Width="24px" OnClick="btnBuscar_Click"  >
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" /> 
                </telerik:RadButton>
             </div>
             <div class="col-md-7"></div>
        </div>

          <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdUsuario" runat="server" Width="100%" AutoGenerateColumns="false" Height="810px"  OnItemCommand="grdUsuario_ItemCommand"   OnItemDataBound="grdOrdenVenta_ItemDataBound">
                    <MasterTableView>
                        <Columns>

                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibDesactivar" HeaderText="Estado"  runat="server"   OnClientClick="return Confirm();"  ImageUrl="~/Images/Icons/circle-red-16.png" CommandArgument='<%# Eval("loginUsuario") %>'  CommandName="Desactivar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server"  HeaderText="Editar"  ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("loginUsuario") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn> 

                            <telerik:GridBoundColumn DataField="NomUsuario" HeaderText="NomUsuario" >
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn> 

                            <telerik:GridBoundColumn DataField="loginUsuario" HeaderText="ID" UniqueName="loginUsuario">
                                <HeaderStyle Width="10px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="EMail" HeaderText="EMail" >
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>


                            <telerik:GridCheckBoxColumn  DataField="Silvestre" HeaderText="Silvestre" UniqueName="Silvestre">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridCheckBoxColumn   DataField="NeoAgrum" HeaderText="NeoAgrum" UniqueName="NeoAgrum">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridCheckBoxColumn    DataField="Inatec"  HeaderText="Inatec" UniqueName="Inatec"  >
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridCheckBoxColumn    DataField="Intranet"  HeaderText="Intranet" UniqueName="Intranet"  >
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridCheckBoxColumn    DataField="Ticket"  HeaderText="Ticket" UniqueName="Ticket"  >
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
