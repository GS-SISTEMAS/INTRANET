<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmPromotorZona.aspx.cs" Inherits="GS.SISGEGS.Web.Comision.Mantenimiento.frmPromotorZona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Colaborador
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(objZona) {
            window.radopen("frmZonaPMng.aspx?objZona=" + objZona, "rwColaborador");
            return false;
        }
        function ShowCreatePromotor(objPromotor) {
            window.radopen("frmPromotorZonaMng.aspx?objPromotor=" + objPromotor, "rwColaborador");
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
                    <telerik:AjaxUpdatedControl ControlID="grdZona" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdZona">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapColaborador" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="ramColaborador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPromotores" LoadingPanelID="ralpColaborador"></telerik:AjaxUpdatedControl>
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
            <telerik:RadWindow ID="rwColaborador" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
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
            <div class="col-md-3">
                <telerik:RadTextBox ID="txtBuscar" runat="server" Width="85%" EmptyMessage="Buscar"></telerik:RadTextBox>
                <telerik:RadButton ID="btnBuscar" runat="server" Width="24px" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
            <div class="col-md-1">

                <telerik:RadButton ID="btnNuevo" runat="server" Text="Agregar Promotor" Visible="true" OnClick="btnNuevo_Click">
                            <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png"/>
            </telerik:RadButton>

            </div>

            <div class="col-md-5"></div>
           
           </div>
             <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label2" runat="server" Text="Cargo" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                         <telerik:RadComboBox ID="cboReporte"  runat="server"  
                            DataTextField="descripcion" DataValueField="id"    ></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
            </div>
            <div class="col-md-5"></div>
            <div class="col-md-1"> 
            </div>
           </div>
             <div class="row">
                <div class="col-md-6">
                <telerik:RadGrid ID="grdZona" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" 
                   OnSelectedIndexChanged="grdZona_selectedindexchanged"
                   OnItemCommand="grdZona_ItemCommand"
                   AllowSorting="True" ShowFooter="true" >

                     <MasterTableView TableLayout="Fixed" DataKeyNames="id_zona"  >

                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" Visible="false" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("id_zona") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="id_zona" HeaderText="Id_Zona" UniqueName="id_zona">
                                <HeaderStyle Width="30px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="zona" HeaderText="Zona" UniqueName="zona">
                                <HeaderStyle Width="150px"/>
                            </telerik:GridBoundColumn>
                     
                            <telerik:GridBoundColumn DataField="porcentajeZona"   DataFormatString="{0:F0}%" HeaderText="Porcentaje" UniqueName="porcentajeZona">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado">
                                <HeaderStyle Width="30px"/>
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
                 <telerik:RadGrid ID="grdPromotores" runat="server" AutoGenerateColumns="false" Width="100%"  Height="500px" 
                     OnItemCommand="grdPromotores_ItemCommand"  AllowSorting="True" ShowFooter="true" >
                        <MasterTableView TableLayout="Fixed">
                            <Columns>

                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" 
                                        CommandArgument='<%# Eval("NroDocumento") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>


                             <telerik:GridBoundColumn DataField="Cargo" HeaderText="Cargo" UniqueName="Cargo" Aggregate="Count" FooterText="Total: " >
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroDocumento" HeaderText="NroDocumento" UniqueName="NroDocumento">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="NombreCompleto" UniqueName="NombreCompleto">
                                <HeaderStyle Width="300px"/>
                            </telerik:GridBoundColumn>
                     
  
                             <telerik:GridBoundColumn DataField="porcentaje" HeaderText="Porcentaje" DataFormatString="{0:F2}%"  Aggregate="Sum"   UniqueName="porcentaje">
                                <HeaderStyle Width="75px"/>
                             </telerik:GridBoundColumn>
 

                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="true"/>
                            <Selecting AllowRowSelect="true" />
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
             <asp:Label ID="lblCorrida" Visible="false" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
