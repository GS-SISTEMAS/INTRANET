<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmGastos.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Gastos.frmGastos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Gestión de Gastos de los colaboradores
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
                $find("<%= ramEgresosVarios.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramEgresosVarios.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnPDFDetalle") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("ibPDF") >= 0) {
                args.set_enableAjax(false);
            }
        }


        function ShowInsertForm(id) {
            window.radopen("frmGastosMng.aspx?idOperacion=" + id, "rwEgresosVarios");
            return false;
        }

        function ShowResumen(id) {
            window.radopen("frmGastosResumen.aspx?idOperacion=" + id, "rwEgresosVarios");
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
            $find("<%= ramEgresosVarios.ClientID %>").ajaxRequest('ChangePageSize,' + altura);
            };
        window.onresize = window.onload = Resize;
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
            <telerik:RadWindow ID="rwEgresosVarios" runat="server" Width="880px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEgresosVarios" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="row">

            <div class="col-md-6">
                <asp:Label ID="lblTitulo" runat="server" Text="Rendición de Gastos de los colabroadores" CssClass="titulo"></asp:Label>
            </div>

            <div class="col-md-2" style="text-align: right">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png" />
                </telerik:RadButton>
            </div>

            <div class="col-md-2" style="text-align: right">
                           <telerik:RadButton ID="btnPDFDetalle" runat="server" Text="PDF Masivo" OnClick="btnPDFDetalle_Click" >
                                <Icon PrimaryIconUrl="../../Images/Icons/pdf-16.png"/>
                            </telerik:RadButton>        
            </div>



        </div>
        <div class="row">
            <div id="workplace" class="col-md-12">
                <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%"
                    Orientation="Vertical">
                    <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                        <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                            <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="300px" Title="Filtros de Busqueda"
                                EnableDock="false" MinWidth="250" MinHeight="225" Scrolling="None">
                                <div class="fila">
                                    <div class="colum4">
                                        <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio" CssClass="etiqueta"></asp:Label>
                                    </div>
                                    <div class="colum6">
                                        <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" DateInput-ReadOnly="true">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="colum4">
                                        <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final" CssClass="etiqueta"></asp:Label>
                                    </div>
                                    <div class="colum6">
                                        <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%" DateInput-ReadOnly="true">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="colum4">
                                           <asp:Label ID="lblProveedor" runat="server" CssClass="etiqueta" Text="Proveedor:"></asp:Label>
                                    </div>
                                    <div class="colum6">
                                           <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                                DropDownWidth="180px" EmptyMessage="Selec. cliente" InputType="Text" 
                                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="180px">
                                                <WebServiceSettings Method="Agenda_BuscarProveedor" Path="frmGastos.aspx" />
                                            </telerik:RadAutoCompleteBox>
                                    </div>
                                </div>

                                 <div class="fila">
                                    <div class="colum2">
                                           <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Serie:"></asp:Label>
                                    </div>
                                     <div class="colum3">
                                            <telerik:RadTextBox ID="txtSerie" runat="server" Width="100%"></telerik:RadTextBox>
                                     </div>
                                     <div class="colum2">
                                           <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Numero:"></asp:Label>
                                    </div>
                                     <div class="colum3">
                                            <telerik:RadTextBox ID="txtNumero" runat="server" Width="100%"></telerik:RadTextBox>
                                     </div>
                                </div>


                                <div class="fila">
                                    <div class="colum4">
                                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                        </telerik:RadButton>
                                    </div>
                                </div>


                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                    <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">
                        <div class="row">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="grdEgresosVarios" runat="server" AutoGenerateColumns="false" Width="100%" Height="500px" AllowSorting="True" ShowFooter="True" 
                                    OnItemDataBound="grdEgresosVarios_ItemDataBound" OnDeleteCommand="grdEgresosVarios_DeleteCommand" OnItemCommand="grdEgresosVarios_ItemCommand" OnNeedDataSource="grdEgresosVarios_NeedDataSource">
                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                    <MasterTableView Width="1630px" DataKeyNames="Op" ClientDataKeyNames="Op" AllowFilteringByColumn="True">
                                        <Columns>
                                             
                                            <telerik:GridTemplateColumn UniqueName="CheckColumn" HeaderText="Check" HeaderStyle-Width="40px" AllowSorting="true" AllowFiltering="false">
                                              <ItemTemplate>
                                                <asp:CheckBox ID="Check" runat="server" />
                                              </ItemTemplate>
                                             </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Imp." AllowFiltering="false" UniqueName="Imp."  >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibPDF" runat="server" ImageUrl="~/Images/Icons/pdf_22.png" CommandArgument='<%# Eval("Op") %>' CommandName="DescargarPDF" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Edit." AllowFiltering="false" UniqueName="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("Op") %>' CommandName="Editar" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridMaskedColumn DataField="Op" HeaderText="#Op" FilterControlWidth="45px" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" Mask="########" UniqueName="Op">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Height="42px" />
                                            </telerik:GridMaskedColumn>
                                            <telerik:GridBoundColumn DataField="ID_Agenda" HeaderText="Nro.Doc." UniqueName="ID_Agenda">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Agenda" HeaderText="Nombre Colaborador" UniqueName="Agenda" 
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="220px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Transaccion" HeaderText="Transaccion" UniqueName="Transaccion"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Concepto" HeaderText="Concepto" UniqueName="Concepto"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CCosto" HeaderText="Cen.Costo" UniqueName="CCosto"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UGestion" HeaderText="U.Gestion" UniqueName="UGestion"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UProyecto" HeaderText="U.Proyec" UniqueName="UProyecto"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NatGasto" HeaderText="Nat.Gasto" UniqueName="NatGasto"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fec.Aplic." UniqueName="Fecha" AllowFiltering="false"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="fechaInicio" HeaderText="Desde" UniqueName="fechaInicio" AllowFiltering="false"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vcmto" HeaderText="Hasta" UniqueName="Vcmto" AllowFiltering="false"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn DataField="Importe" DataFormatString="{0:F2}" UniqueName="Importe" AllowFiltering="false"
                                                DataType="System.Decimal" HeaderText="Importe" Aggregate="Sum">
                                                <HeaderStyle Width="50px" />
                                                <FooterStyle Font-Bold="true"></FooterStyle>
                                            </telerik:GridNumericColumn>
                                            <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda">
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Ok0" HeaderText="Ok0" UniqueName="Ok0" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Ok1" HeaderText="Ok1" UniqueName="Ok1" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridCheckBoxColumn DataField="Planilla" HeaderText="Planilla" UniqueName="Planilla" AllowFiltering="false">
                                                <HeaderStyle Width="50px"/>
                                            </telerik:GridCheckBoxColumn>
                                            <telerik:GridCheckBoxColumn DataField="Orden" HeaderText="Orden" UniqueName="Orden" AllowFiltering="false">
                                                <HeaderStyle Width="50px"/>
                                            </telerik:GridCheckBoxColumn>
                                            <telerik:GridTemplateColumn HeaderText="Aprob0" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEstado0" runat="server" ToolTip='<%# Eval("UsuarioAprobacion0") %>'/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Aprob1" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEstado1" runat="server" ToolTip='<%# Eval("UsuarioAprobacion1") %>'/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Resum." AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibResumen" runat="server" ImageUrl="~/Images/Icons/money-16.png" CommandArgument='<%# Eval("Op") %>' CommandName="Resumen"/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"/>
                                            </telerik:GridTemplateColumn>
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
                    </telerik:RadPane>
                </telerik:RadSplitter>
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
