<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmGastos.aspx.cs" Inherits="GS.SISGEGS.Web.Commercial.Gastos.frmGastos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Gestión de Gastos de los colaboradores
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function RowDblClick(sender, eventArgs) {
            var MasterTable = sender.get_masterTableView();
            var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var cell = MasterTable.getCellByColumnUniqueName(row, "Ok1");
            if (cell.innerText == "False")
                window.radopen("frmGastosMng.aspx?idOperacion=" + eventArgs.getDataKeyValue("Op"), "rwEgresosVarios");
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramEgresosVarios.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramEgresosVarios.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function ShowInsertForm(id) {
            window.radopen("frmGastosMng.aspx?idOperacion=" + id, "rwEgresosVarios");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEgresosVarios" runat="server" OnAjaxRequest="ramEgresosVarios_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramEgresosVarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEgresosVarios" LoadingPanelID="ralpEgresosVarios"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
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
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEgresosVarios" LoadingPanelID="ralpEgresosVarios"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpEgresosVarios" runat="server" >
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmEgresosVarios" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwEgresosVarios" runat="server" Width="847px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEgresosVarios" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Rendición de Gastos de los colabroadores" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-5">
                        <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" DateInput-ReadOnly="true">
                            <DateInput runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-5">
                        <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%" DateInput-ReadOnly="true">
                            <DateInput runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                </telerik:RadButton>
            </div>
            <div class="col-md-3">
            </div>
            <div class="col-md-2" style="text-align:right">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdEgresosVarios" runat="server" AutoGenerateColumns="false" Width="100%" Height="500px" 
                    OnItemDataBound="grdEgresosVarios_ItemDataBound" OnDeleteCommand="grdEgresosVarios_DeleteCommand">
                    <MasterTableView Width="1200px" DataKeyNames="Op" ClientDataKeyNames="Op">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Op" HeaderText="Cod.Op." UniqueName="Op">
                                <HeaderStyle Width="40px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_Agenda" HeaderText="Nro.Doc." UniqueName="ID_Agenda">
                                <HeaderStyle Width="70px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Agenda" HeaderText="Nombre Colaborador" UniqueName="Agenda">
                                <HeaderStyle Width="120px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Concepto" HeaderText="Concepto" UniqueName="Concepto">
                                <HeaderStyle Width="120px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Transaccion" HeaderText="Transaccion" UniqueName="Transaccion">
                                <HeaderStyle Width="60px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fec.Registro" UniqueName="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Vcmto" HeaderText="Fec.Vencim." UniqueName="Vcmto" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" UniqueName="Importe" DataFormatString="{0:F3}">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ok1" HeaderText="Ok1" UniqueName="Ok1" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Aprob.">
                                <ItemTemplate>
                                    <asp:Image ID="imgEstado" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el registro?" ConfirmDialogType="RadWindow" HeaderStyle-Width="30px" HeaderText="Elim."
                                ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" ImageUrl="../../Images/Icons/delete-16.png" UniqueName="Elim"/>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                        <ClientEvents OnRowDblClick="RowDblClick" />
                    </ClientSettings>
                </telerik:RadGrid>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" />
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
