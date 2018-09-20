<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmGeneralContratos.aspx.cs" Inherits="GS.SISGEGS.Web.Contratos.Reportes.frmGeneralContratos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramRegistroContrato.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramRegistroContrato.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function ShowInsertForm(id) {
            window.radopen("frmContratosMng.aspx?idContrato=" + id, "rwRegistroContrato");
            return false;
        }

       

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
   

    <telerik:RadAjaxManager ID="ramRegistroContrato" runat="server" OnAjaxRequest="ramContratos_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramRegistroContrato">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGeneralContratos" LoadingPanelID="ralpRegistroContrato"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
           <%-- 
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEgresosVarios" LoadingPanelID="ralpEgresosVarios"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEgresosVarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEgresosVarios" LoadingPanelID="ralpEgresosVarios"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGeneralContratos" LoadingPanelID="ralpRegistroContrato"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpRegistroContrato" runat="server" >
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmRegistroContrato" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwRegistroContrato" runat="server" Width="847px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlGeneralContratos" runat="server" Width="100%" Height="700px" >
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte General de Contratos" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">

            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblArea" Text="Area" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadComboBox ID="cboArea" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblMateria" Text="Materia Contrato" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-1">
                    <telerik:RadComboBox ID="cboMateriaContrato" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                 <div class="col-md-1">
                    <asp:Label runat="server" ID="lblTipoContrato" Text="Tipo de Contrato" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadComboBox ID="cboTipoContrato" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblFecha" Text="Fecha Suscripción" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-1">
                    <telerik:RadDatePicker ID="dpFechaDesde" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
                <div class="col-md-1">
                    <telerik:RadDatePicker ID="dpFechaHasta" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
                <div class ="col-md-4">
                    <telerik:LayoutColumn Span="6">
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" >
                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                        </telerik:RadButton>
                    </telerik:LayoutColumn>
                </div>
                <div class="col-md-2" style="text-align: right">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png" />
                </telerik:RadButton>
            </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblEstado" Text="Estado" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadComboBox ID="cboEstado" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>

                <div class="col-md-1">
                    <asp:Label runat="server" ID="Label1" Text="Fecha Vencimiento" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-1">
                    <telerik:RadDatePicker ID="dpFechanVencDesde" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
                <div class="col-md-1">
                    <telerik:RadDatePicker ID="dpFechanVencHasta"  runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
               

                <div class="col-md-12">
                    <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" Visible="false" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdGeneralContratos" runat="server" AutoGenerateColumns="false" Height="600px" Width="100%"
                     OnDeleteCommand="grdGeneralContratos_DeleteCommand"  OnItemCommand="grdGeneralContratos_ItemCommand">
        <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true" />
                                        <MasterTableView Width="100%" DataKeyNames="idContrato" ClientDataKeyNames="idContrato">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Edit." AllowFiltering="false" UniqueName="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("idContrato") %>' CommandName="Editar" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="idContrato" UniqueName="idContrato" HeaderText="idContrato" Display="false">
                                                    <HeaderStyle Width="10" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CodigoContrato" UniqueName="CodigoContrato" HeaderText="Código">
                                                    <HeaderStyle Width="50" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombreMateria" UniqueName="nombreMateria" HeaderText="Materia">
                                                    <HeaderStyle Width="100" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombreTipo" UniqueName="nombreTipo" HeaderText="Tipo">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Renovar" UniqueName="Renovar" HeaderText="Renovar">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombreProveedor" UniqueName="nombreProveedor" HeaderText="Cliente / Proveedor">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Contratante" UniqueName="Contratante" HeaderText="Contratante">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AreaResponsable" UniqueName="AreaResponsable" HeaderText="Area Responsable">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaSuscripcion" UniqueName="FechaSuscripcion" HeaderText="Fecha Suscripción" >
                                                    <HeaderStyle Width="70" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaVencimiento" UniqueName="FechaVencimiento" HeaderText="Fecha Vencimiento" >
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ObjetoContrato" UniqueName="ObjetoContrato" HeaderText="Objeto del Contrato" >
                                                    <HeaderStyle Width="250" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Renovacion" UniqueName="Renovacion" HeaderText="Renovaciones" >
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Monto" UniqueName="Monto" HeaderText="Monto" >
                                                    <HeaderStyle Width="150" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombreEstado" UniqueName="nombreEstado" HeaderText="Estado" >
                                                    <HeaderStyle Width="100" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el registro?" ConfirmDialogType="RadWindow" HeaderStyle-Width="30px" HeaderText="Elim."
                                                ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" ImageUrl="../../Images/Icons/delete-16.png" UniqueName="Elim" />
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
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
