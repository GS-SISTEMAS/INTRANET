<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmColaboradorZona.aspx.cs" Inherits="GS.SISGEGS.Web.Comision.Mantenimiento.frmColaboradorZona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Colaborador
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(objPersonal) {
            window.radopen("frmPersonalZonaMng.aspx?objPersonal=" + objPersonal, "rwColaborador");
            return false;
        }
        function ShowCreateZona(objZona) {
            window.radopen("frmZonaAgregarMng.aspx?objZona=" + objZona, "rwColaborador");
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

                <telerik:RadButton ID="btnNuevo" runat="server" Text="Agregar Zona" Visible="true" OnClick="btnNuevo_Click">
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
                <telerik:RadGrid ID="grdPersonal" runat="server" Width="100%" AutoGenerateColumns="false" Height="400px" 
                   OnSelectedIndexChanged="grdPersonal_selectedindexchanged"
                   OnItemCommand="grdPersonal_ItemCommand"
                   AllowSorting="True" ShowFooter="true" >

                     <MasterTableView TableLayout="Fixed" DataKeyNames="NroDocumento"  >

                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("NroDocumento") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>

                           <telerik:GridBoundColumn DataField="Cargo" HeaderText="Cargo" UniqueName="Cargo" Aggregate="Count">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroDocumento" HeaderText="NroDocumento" UniqueName="NroDocumento">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="NombreCompleto" UniqueName="NombreCompleto">
                                <HeaderStyle Width="150px"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="porcentaje" DataFormatString="{0:F2}%" HeaderText="Porcentaje" UniqueName="porcentaje">
                                <HeaderStyle Width="100px"/>
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
                      >
                        <MasterTableView>
                            <Columns>

                                <telerik:GridBoundColumn DataField="ID_Zona" HeaderText="ID_Zona" UniqueName="ID_Zona" >
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Zona" HeaderText="Zona" UniqueName="Zona" Aggregate="Count">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PorcentajeZona" DataFormatString="{0:F2}%" HeaderText="PorcentajeZona" UniqueName="PorcentajeZona">
                                        <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>

                             <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("ID") %>'  CommandName="Editar"/>
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
             <asp:Label ID="lblCorrida" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
