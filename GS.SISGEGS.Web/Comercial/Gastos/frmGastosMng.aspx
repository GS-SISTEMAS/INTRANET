<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmGastosMng.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Gastos.frmGastosMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(args);
                rWindow.close();
            });
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnPDFDetalle") >= 0)
                args.set_enableAjax(false);
        }

        function ShowInsertForm(objRecibo) {
            if (objRecibo != 0)
                window.radopen("frmGastosEdt.aspx?objRecibo=" + JSON.stringify(objRecibo), "rwGastosMng");
            else
                window.radopen("frmGastosEdt.aspx?objRecibo=" + objRecibo, "rwGastosMng");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramGastosMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramGastosMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramGastosMng" runat="server" OnAjaxRequest="ramGastosMng_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramGastosMng">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGastosMng" LoadingPanelID="ralpGastosMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRecibos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGastosMng" LoadingPanelID="ralpGastosMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGastosMng" LoadingPanelID="ralpGastosMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAprobar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGastosMng" LoadingPanelID="ralpGastosMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGastosMng" LoadingPanelID="ralpGastosMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpGastosMng" runat="server" ZIndex="9999">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmGastosMng" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwGastosMng" runat="server" Width="450px" Height="450px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapGastosMng" runat="server" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblTransaccion" runat="server" Text="Transac." CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum1">
                <telerik:RadTextBox ID="txtSerie" runat="server" Width="100%" ReadOnly="true"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <telerik:RadTextBox ID="txtNumero" runat="server" Width="100%" ReadOnly="true"></telerik:RadTextBox>
            </div>
               <div class="colum1" style="text-align: right">
                    <telerik:RadButton ID="btnPDFDetalle" runat="server" Text="Imprimir PDF" OnClick="btnPDFDetalle_Click" >
                        <Icon PrimaryIconUrl="../../Images/Icons/pdf-16.png"/>
                    </telerik:RadButton>        
                </div>
        </div>
        <div class="fila">
            <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblNroDoc" runat="server" Text="Nro.Doc." CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadTextBox ID="txtNroDoc" runat="server" Width="100%" ReadOnly="true"></telerik:RadTextBox>
                </div>
            </div>
            <div class="colum5">
                <div class="colum2">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum8">
                    <telerik:RadTextBox ID="txtNombre" runat="server" Width="100%" ReadOnly="true"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="fila">
            <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblFechaReg" runat="server" Text="Fecha Apli." CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadDatePicker ID="dpFecRegistro" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE" Enabled="false">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
            </div>
            <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblFechaInicio" runat="server" Text="Desde" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
            </div>
            <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblFechaVcmt" runat="server" Text="Hasta" CssClass="etiqueta">
                    </asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadDatePicker ID="dpFecVencimiento" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
            </div>
            <div class="colum1">
                <asp:Label ID="lblIDAgenda" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblMoneda" runat="server" Text="Moneda" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadComboBox ID="cboMoneda" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
            </div>
        </div>
        <div class="fila">
            <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblCentroCosto" runat="server" Text="C.Costo" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadComboBox ID="cboCentroCosto" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblUnidGestion" runat="server" Text="U.Gestión" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadComboBox ID="cboUnidGestion" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
            </div>
        </div>
        <div class="fila">
             <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblUnidProy" runat="server" Text="U.Proyecto" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadComboBox ID="cboUnidProy" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
            </div>
             <div class="colum3">
                <div class="colum3">
                    <asp:Label ID="lblNatGasto" runat="server" Text="Nat.Gasto" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum7">
                    <telerik:RadComboBox ID="cboNatGasto" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
             

            </div>
        </div>
        <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblConcepto" runat="server" Text="Concepto" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum4">
                <telerik:RadTextBox ID="txtConcepto" runat="server" TextMode="MultiLine" Width="100%" Height="40px"></telerik:RadTextBox>
            </div>
            <div class="colum1">
                <br />
            </div>
            <div class="colum2" style="text-align:right; vertical-align:bottom">
                <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <telerik:RadGrid ID="grdRecibos" runat="server" AutoGenerateColumns="false" Height="210px" Width="100%" 
                    OnNeedDataSource="grdRecibos_NeedDataSource"  OnDeleteCommand="grdRecibos_DeleteCommand" OnItemCommand="grdRecibos_ItemCommand">
                    <MasterTableView DataKeyNames="ID_Amarre" Width="1000px"  ClientDataKeyNames="ID_Amarre">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("ID_Amarre") %>' CommandName="Editar" 
                                        Width="16px" Height="16px" ImageUrl="~/Images/Icons/pencil-16.png"/>
                                </ItemTemplate>
                                <HeaderStyle Width="30px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="NombreDocumento" HeaderText="Documento" UniqueName="NombreDocumento">
                                <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_Amarre" HeaderText="ID" UniqueName="ID_Amarre" Visible="false">
                                <HeaderStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_Agenda" HeaderText="Código" UniqueName="ID_Agenda">
                                <HeaderStyle Width="60px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Agenda" HeaderText="Nombre" UniqueName="Agenda">
                                <HeaderStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_Item" HeaderText="Cód.Gasto" UniqueName="ID_Item">
                                <HeaderStyle Width="60px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Item" HeaderText="Descripción" UniqueName="Item">
                                <HeaderStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaEmision" HeaderText="Fecha" UniqueName="FechaEmision" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="60px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Serie" HeaderText="Serie" UniqueName="Serie">
                                <HeaderStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Numero" HeaderText="Número" UniqueName="Numero">
                                <HeaderStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" UniqueName="Importe" DataFormatString="{0:F2}">
                                <HeaderStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ImporteBaseIGV" HeaderText="Base" UniqueName="ImporteBaseIGV" DataFormatString="{0:F2}">
                                <HeaderStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ImporteIGV" HeaderText="IGV" UniqueName="ImporteIGV" DataFormatString="{0:F2}">
                                <HeaderStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ImporteInafecto" HeaderText="Inafecto" UniqueName="ImporteInafecto" DataFormatString="{0:F2}">
                                <HeaderStyle Width="40px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" UniqueName="Observaciones">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el registro?" ConfirmDialogType="RadWindow" HeaderStyle-Width="30px" HeaderText="Elim."
                                ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" ImageUrl="../../Images/Icons/delete-16.png"/>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="4"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" />
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum2">
                <telerik:RadButton ID="btnAprobJI" runat="server" Text="Aprob. Jef.Inm." OnClick="btnAprobJI_Click" Visible="false">
                    <Icon PrimaryIconUrl="../../Images/Icons/sign-check-16.png" />
                </telerik:RadButton>
                <telerik:RadButton ID="btnAprobConta" runat="server" Text="Aprob. Contable" Visible="false" OnClick="btnAprobConta_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/sign-check-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum3">
                <br />
            </div>
            <div class="colum3">
                <asp:Label ID="lblTotal" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
