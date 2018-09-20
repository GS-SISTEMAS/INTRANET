<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmColaboradorZona.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Mantenedor.frmColaboradorZona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Colaborador
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(objPersonal) {
            window.radopen("frmPersonalZonaMng.aspx?objPersonal=" + objPersonal, "rwPersonal");
            return false;
        }
        function ShowCreateZona(objZona) {
            window.radopen("frmZonaAgregarMng.aspx?objZona=" + objZona, "rwZona");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramColaborador.ClientID %>").ajaxRequest("Rebind");         
            }
            else {
                $find("<%= ramColaborador.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }

        function refreshGridZona(arg) {
            if (!arg) {
                $find("<%= ramColaborador.ClientID %>").ajaxRequest("Rebind");         
            }
            else {
                $find("<%= ramColaborador.ClientID %>").ajaxRequest("UpdateZona," + arg);
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramColaborador" runat="server" OnAjaxRequest="ramColaborador_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapColaborador" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ramColaborador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPersonal" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdPersonal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapColaborador" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="ramColaborador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdZonas" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>



               <telerik:AjaxSetting AjaxControlID="grdZonas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapColaborador" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpColaborador" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmColaborador" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwPersonal" runat="server" Width="420px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>

               <telerik:RadWindow ID="rwZona" runat="server" Width="370px" Height="320px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapColaborador" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Asignación de Zonas"></asp:Label>
            </div>
        </div>
             <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="cboEmpresa" Enabled="false" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <telerik:RadTextBox ID="txtBuscar" runat="server" Width="100%" EmptyMessage="Buscar"></telerik:RadTextBox>
            </div>

            <div class="col-md-1">
               <telerik:RadButton ID="btnBuscar" runat="server"  Text="Buscar" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>

            <div class="col-md-1">


            </div>

            <div class="col-md-5"></div>
           
           </div>
             <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label2" runat="server" Text="Reporte" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                         <telerik:RadComboBox ID="cboReporte"  runat="server"  
                            DataTextField="NombreReporte" DataValueField="idReporte"    ></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-1">
            </div>
            <div class="col-md-2"> 
                
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Agregar Usuario" Visible="true" OnClick="btnNuevo_Click">
                            <Icon PrimaryIconUrl="../../../Images/Icons/sign-add-16.png"/>
                </telerik:RadButton>
            </div>

            <div class="col-md-5">

            </div>
           <div class="col-md-1"> 

           </div>
           </div>
             <div class="row">
                <div class="col-md-6">
                <telerik:RadGrid 
                   ID="grdPersonal" runat="server" Width="100%" AutoGenerateColumns="false" Height="400px" 
                   OnSelectedIndexChanged="grdPersonal_selectedindexchanged"
                   OnItemCommand="grdPersonal_ItemCommand"
                   OnNeedDataSource="grdPersonal_NeedDataSource"
                   AllowSorting="True" ShowFooter="true" >

                     <MasterTableView TableLayout="Fixed" DataKeyNames="ID_Agenda"  >

                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("ID_Agenda") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>

                       <%--    <telerik:GridBoundColumn DataField="nombrePerfil" HeaderText="Perfil" UniqueName="nombrePerfil" Aggregate="Count">
                                <HeaderStyle Width="150px"/>
                            </telerik:GridBoundColumn>--%>

                            <telerik:GridBoundColumn DataField="nroDocumento" HeaderText="NroDocumento" UniqueName="nroDocumento" Aggregate="Count">
                                <HeaderStyle Width="80px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="nombres" HeaderText="Nombres" UniqueName="nombres">
                                <HeaderStyle Width="150px"/>
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado">
                                <HeaderStyle Width="50"/>
                            </telerik:GridBoundColumn>


                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true" >
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
                </div>
                <div class="col-md-6">
                 <telerik:RadGrid ID="grdZonas" runat="server" AutoGenerateColumns="false" Width="100%"  Height="400px" 
                     OnItemCommand="grdZonas_ItemCommand"  ShowFooter="true" AllowSorting="True" 
                     OnNeedDataSource="grdZonas_NeedDataSource"
                      >
                        <MasterTableView>
                            <Columns>

                                <telerik:GridBoundColumn DataField="ID_Zona" HeaderText="ID_Zona" UniqueName="ID_Zona" >
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Zona" HeaderText="Zona" UniqueName="Zona" Aggregate="Count">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                               
                                  <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado"  >
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>

                             <telerik:GridTemplateColumn Display="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("ID_Zona") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>

                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="True" AllowScroll="true"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                             <Resizing AllowRowResize="True" EnableRealTimeResize="True"></Resizing>
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
             <asp:Label ID="lblPersonalDS" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblZonaDS" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
