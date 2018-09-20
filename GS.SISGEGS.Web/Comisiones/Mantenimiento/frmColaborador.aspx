<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmColaborador.aspx.cs" Inherits="GS.SISGEGS.Web.Comision.Mantenimiento.frmColaborador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Colaborador
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowCreate(objPersonal) {
            window.radopen("frmPersonalMng.aspx?objPersonal=" + objPersonal, "rwColaborador");
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
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Colaborador"></asp:Label>
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
            <div class="col-md-5"></div>
            <div class="col-md-1">
               
            </div>
           </div>
             <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label2" runat="server" Text="Reporte" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="cboReporte" runat="server" DataTextField="descripcion" DataValueField="id" ></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
            </div>
            <div class="col-md-1"> 
                 <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                     <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png"/>
                </telerik:RadButton>
            </div>


            <div class="col-md-5"></div>
            <div class="col-md-1"> 
                
            </div>
           </div>
             <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdPersonal" runat="server" Width="100%" AutoGenerateColumns="false" Height="500px" OnItemCommand="grdPersonal_ItemCommand"
                    OnNeedDataSource="grdPersonal_NeedDataSource" AllowSorting="True" ShowFooter="true" >

                    <MasterTableView TableLayout="Fixed" DataKeyNames="reporte"
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >
                         <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                   <telerik:GridGroupByField FieldAlias="reporte" FieldName="reporte" />
                                    <telerik:GridGroupByField FieldAlias="ReporteDes" FieldName="ReporteDes" />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="reporte"  />
                                    <telerik:GridGroupByField FieldName="ReporteDes"/>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>

                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("NroDocumento") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>


                              <telerik:GridBoundColumn DataField="zona" HeaderText="Zona" UniqueName="zona" Aggregate="Count" >
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>

                           <telerik:GridBoundColumn DataField="Cargo" HeaderText="Cargo" UniqueName="Cargo">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroDocumento" HeaderText="NroDocumento" UniqueName="NroDocumento">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ApPaterno" HeaderText="ApPaterno" UniqueName="ApPaterno">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>
                     
                            <telerik:GridBoundColumn DataField="ApMaterno" HeaderText="ApMaterno" UniqueName="ApMaterno">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" UniqueName="Nombres">
                                <HeaderStyle Width="200px"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="porcentaje"  DataFormatString="{0:F2}%" HeaderText="Porcentaje" UniqueName="porcentaje">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                           
                             <telerik:GridBoundColumn DataField="CodEmpresaRH" HeaderText="CodEmpresa" UniqueName="CodEmpresa">
                                <HeaderStyle Width="80px"/>
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
             <asp:Label ID="lblCorrida" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
