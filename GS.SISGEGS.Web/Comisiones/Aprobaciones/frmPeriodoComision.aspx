<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmPeriodoComision.aspx.cs" Inherits="GS.SISGEGS.Web.Comisiones.Reporte.frmPeriodoComision" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Gestión de Comisiones de los colaboradores
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramPeriodoComisiones.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramPeriodoComisiones.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnPDFDetalle") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("ibPDF") >= 0) {
                args.set_enableAjax(false);
            }
        }

        function ShowResumen(id) {
            window.radopen("frmGastosResumen.aspx?idOperacion=" + id, "rwPeriodoComisiones");
            return false;
        }

        $(document).ready(function () {
            var altura = $(document).height() - 132;
            $('#workplace').css("height", altura + "px");
        });

        $(window).resize(function () {
            var altura = $(document).height() - 132;
            $('#workplace').css("height", altura + "px");
        });

        function Resize()
        {
            var altura = $(document).height() - 142;
            $find("<%= ramPeriodoComisiones.ClientID %>").ajaxRequest('ChangePageSize,' + altura);
            };
        window.onresize = window.onload = Resize;
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramPeriodoComisiones" runat="server" OnAjaxRequest="ramPeriodoComisiones_AjaxRequest">
       
        <AjaxSettings>
         

            <telerik:AjaxSetting AjaxControlID="ramPeriodoComisiones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPeriodoComisiones" LoadingPanelID="ralpPeriodoComisiones"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPeriodoComisiones" LoadingPanelID="ralpPeriodoComisiones"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPeriodoComisiones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPeriodoComisiones" LoadingPanelID="ralpPeriodoComisiones"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPeriodoComisiones" LoadingPanelID="ralpPeriodoComisiones"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPeriodoComisiones" runat="server" >
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmPeriodoComisiones" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwPeriodoComisiones" runat="server" Width="880px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapPeriodoComisiones" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="row">

            <div class="col-md-6">
                <asp:Label ID="lblTitulo" runat="server" Text="Aprobación de Comisión de Colaboradores " CssClass="titulo"></asp:Label>
            </div>


            <div class="col-md-2" style="text-align: right; display:NONE" >
                           <telerik:RadButton ID="btnPDFDetalle" runat="server" Text="PDF Masivo" OnClick="btnPDFDetalle_Click" >
                                <Icon PrimaryIconUrl="../../Images/Icons/pdf-16.png"/>
                            </telerik:RadButton>        
            </div>

        </div>
       <div class="row">  
                    <div  class="col-xs-12">
                    <div class="row">
                        <div class="col-xs-1">
                            <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                        </div>
                          <div class="col-xs-1">
                           <telerik:RadComboBox ID="cboAnhos" Width="90%" Runat="server" >
                            </telerik:RadComboBox>
                        </div>

                        <div class="col-xs-1 bottom-left"  >
                            <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </div>

                        <div class="col-xs-1 bottom-left">
                            <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png" />
                            </telerik:RadButton>
                        </div>
                          <div class="col-xs-8">
                        
                        </div>

                    </div>
                    </div>
        </div>
        <div class="row">
         <div  class="col-xs-12">

                                <telerik:RadGrid ID="grdPeriodoComisiones" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%" 
                                    AllowSorting="false" ShowFooter="false" 
                                    OnItemDataBound="grdPeriodoComisiones_ItemDataBound" OnDeleteCommand="grdPeriodoComisiones_DeleteCommand" 
                                    OnItemCommand="grdPeriodoComisiones_ItemCommand" OnNeedDataSource="grdPeriodoComisiones_NeedDataSource">
                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                    <MasterTableView  DataKeyNames="Id_periodo" ClientDataKeyNames="Id_periodo" AllowFilteringByColumn="false">
                                        <Columns>

                                            <telerik:GridTemplateColumn HeaderText="Edit." AllowFiltering="false" UniqueName="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("Id_periodo") %>' CommandName="Editar" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridMaskedColumn DataField="Id_periodo" HeaderText="ID"  AutoPostBackOnFilter="true"
                                                 UniqueName="Id">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Height="42px" />
                                            </telerik:GridMaskedColumn>

                                             <telerik:GridBoundColumn DataField="Periodo" HeaderText="Periodo" UniqueName="Periodo">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="FechaCreacion" HeaderText="FechaCreacion" UniqueName="FechaCreacion"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="FechaAprobacion" HeaderText="FechaAprobacion" UniqueName="FechaAprobacion"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" >
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                      
                                             <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado" 
                                                 AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el registro?" ConfirmDialogType="RadWindow" HeaderStyle-Width="30px" HeaderText="Elim."
                                                ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" ImageUrl="../../Images/Icons/delete-16.png" UniqueName="Elim" />
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                        <Selecting AllowRowSelect="True"></Selecting>
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
