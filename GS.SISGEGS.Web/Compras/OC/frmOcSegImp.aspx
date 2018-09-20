<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOcSegImp.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.OC.frmOcSegImp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Creacion de Seguimiento de Importacion
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>

    <%--<script type="text/javascript">
    function Blur(s, e)
    {
        

        <%--var dtpfechaeta = $find("<%= dtpfechaeta.ClientID%>");
        var txtdiasSE = $find("<%= txtdiaslibresSe.ClientID%>");
        var dtpfechalibrese = $find("<%= dtpfechadiaslibrese.ClientID%>");
        dtpfechalibrese.clear();
        var dias = 0;
        var fecha = new Date();

        fecha = dtpfechaeta.get_selectedDate();

        dias = txtdiasSE.get_displayValue();
        fecha.setDate(fecha.getDate() + dias);

        dtpfechalibrese.set_selectedDate(new Date(fecha.getFullYear(), fecha.getMonth(), fecha.getDay()));--%>



        



    <%--<style type="text/css">
        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#<%=btnguardar.ClientID %>').append(
                '<div id="alert_div" class="alert ' + cssclass + '" alert-dismissible fade in text-center role="alert"><button type="button" class="close" data-dismiss="alert">&times;</button><span>' + message + '</span></div>');
        }
    </script>--%>
    <script type="text/javascript">
        

        function AvisoOk(args) {
            //showError("hola");
            showSuccess(args);
        }
        function AvisoError(args) {
            //showError("hola");
            showError(args);
        }

        function ShowDocumentsForm(id) {
            window.radopen("frmOCDoc.aspx?Id_SegImp=" + id, "rwSeg");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramSeg" runat="server"> <%-- OnAjaxRequest="ramPedidoMng_AjaxRequest"--%>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="gvwseguimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            


            <telerik:AjaxSetting AjaxControlID="gvwocparcial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="gvwocparcialsel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ramSeg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gvwocparcial" LoadingPanelID="ralpSeg" />
                    <telerik:AjaxUpdatedControl ControlID="gvwseguimiento"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="btnagregarparcial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                
            <telerik:AjaxSetting AjaxControlID="btnseleccionaroc">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpSeg" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmSeg" runat="server" EnableShadow="true">
        <Windows>
             <telerik:RadWindow ID="rwSeg" runat="server" Width="847px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <%--<telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" 
            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500"
            Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true" 
            VisibleOnPageLoad="true" LoadContentOn="EveryShow" ShowInterval="6500" KeepOnMouseOver="false"
            OnCallbackUpdate="RadNotification1_CallbackUpdate" OnClientUpdated="telerikDemo.onClientUpdated" 
            OnClientShowing="telerikDemo.onClientShowing"
     >
    </telerik:RadNotification>--%>


    <telerik:RadAjaxPanel ID="pnlSeg" runat="server" Width="100%" Height="100%" >
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registro de Seguimiento de Importación"></asp:Label>
                            <asp:Label ID="lblnroseguimiento" runat="server" CssClass="titulo" Text=""></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png"/>
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                
                <telerik:LayoutRow Height="85%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadTabStrip runat="server" ID="stripOC" MultiPageID="radmultipage" SelectedIndex="1" Style="position: relative; z-index: 1000" >
                                <Tabs>
                                    <telerik:RadTab Text="Parciales Disponibles" Selected="True"></telerik:RadTab>
                                    <telerik:RadTab Text="Registro de Seguimiento"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="radmultipage" SelectedIndex="0" Height="93%" Width="100%"  CssClass="bordetab" BorderColor="DarkGray" BorderStyle="Solid"
                                BorderWidth="1px">
                                <telerik:RadPageView runat="server" ID="pageoc" Height="100%">
                                    <telerik:RadPageLayout ID="RadPageLayout4" runat="server" Width="100%" Height="100%">
                                        <Rows>
                                            <%--<telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-3">
                                                        <telerik:RadTextBox ID="txtidagenda" runat="server" ReadOnly="true" Width="100%"  Label="Codigo Agenda" LabelWidth="33%"   ></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <telerik:RadTextBox ID="txtagendanombre" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre Proveedor" Label="Nombre Proveedor"  LabelWidth="18%" ></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>--%>
                                            <telerik:LayoutRow >
                                                <Content>
                                                    <div class="col-md-3">
                                                        <telerik:RadTextBox ID="txtproveedor" runat="server" Width="100%" EmptyMessage="Nombre del Proveedor" Label="Proveedor" LabelWidth="33%"   ></telerik:RadTextBox>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <telerik:RadDatePicker ID="dtpfechainicial" runat="server" Width="100%"  
                                                            DateInput-Label="Fecha Inicial" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="30%">
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <telerik:RadDatePicker ID="dtpfechafinal" runat="server" Width="100%"  
                                                            DateInput-Label="Fecha Final" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="30%">
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    
                                                    <div class="col-md-2">
                                                        <telerik:RadButton ID="btnbuscarocparcial" runat="server" Text="Buscar" OnClick="btnbuscarocparcial_Click" >
                                                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                                                        </telerik:RadButton>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <telerik:RadButton ID="btnseleccionaroc" runat="server" Text="Seleccionar OC" OnClick="btnseleccionaroc_Click" >
                                                            <Icon PrimaryIconUrl="../../Images/Icons/sign-ban-16.png"/>
                                                        </telerik:RadButton>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            
                                            

                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label3" runat="server"  CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                           
                                                
                                            <telerik:LayoutRow >
                                                    <Content>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:label Text="OC Disponibles" CssClass="etiqueta" runat="server" ></asp:label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <telerik:RadGrid ID="gvwocparcial" runat="server" Width="100%" Height="350px" AutoGenerateColumns="false"
                                                                         OnItemCommand="gvwocparcial_ItemCommand" OnItemDataBound="gvwocparcial_ItemDataBound" >
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" ></Scrolling>
                                                                                <Selecting AllowRowSelect="True"></Selecting>
                                                                            </ClientSettings>
                                                                            <MasterTableView>
                                                                                <Columns>
                                                                                    <telerik:GridTemplateColumn HeaderText="Parcial" AllowFiltering="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("No_RegistroParcial")+ "," + Eval("Op_OC") %>' CommandName="Selparcial"
                                                                                                ImageUrl="~/Images/Icons/sign-check-16.png" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="30px" />
                                                                                    </telerik:GridTemplateColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Op_OC" DataField="Op_OC" UniqueName="Op_OC">
                                                                                        <HeaderStyle Width="30px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn UniqueName="FechaOrden" DataField="FechaOrden" HeaderText="FechaOrden" AllowFiltering="false"
                                                                                        HeaderStyle-Width="50px" DataFormatString="{0:dd/MM/yyyy}">
                                                                                        <HeaderStyle Width="50px" />
                                                                                    </telerik:GridBoundColumn>


                                                                                    <telerik:GridBoundColumn HeaderText="No Parcial" DataField="No_RegistroParcial" UniqueName="No_RegistroParcial">
                                                                                        <HeaderStyle Width="70px" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn HeaderText="NroParcial" DataField="NroParcial" UniqueName="NroParcial" Visible="false">
                                                                                        <HeaderStyle Width="70px" />
                                                                                    </telerik:GridBoundColumn>
                                                                   
                                                                                    <telerik:GridBoundColumn HeaderText="Neto" DataField="Neto" UniqueName="Neto" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Subtotal" DataField="Subtotal" UniqueName="Subtotal" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Impuestos" DataField="Impuestos" UniqueName="Impuestos" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Total" DataField="Total" UniqueName="Total">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Id_SegImp" DataField="Id_SegImp" UniqueName="Id_SegImp" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="AgendaNombre" DataField="AgendaNombre" UniqueName="AgendaNombre" Visible="true">
                                                                                        <HeaderStyle Width="150px" />
                                                                                    </telerik:GridBoundColumn>


                                                                                </Columns>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="col-md-6">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:label Text="OC Seleccionados" CssClass="etiqueta" runat="server" ></asp:label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <%--<telerik:RadListBox RenderMode="Lightweight" runat="server" ID="lbocseleccionados" Height="200px" Width="230px">
                                                                        </telerik:RadListBox>
                                                                        <telerik:RadButton ID="btneliminaroc" runat="server" OnClick="btneliminaroc_Click">
                                                                            <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                                                                        </telerik:RadButton>--%>

                                                                        <telerik:RadGrid ID="gvwocparcialsel" runat="server" Width="100%" Height="350px" AutoGenerateColumns="false"
                                                                         OnItemCommand="gvwocparcialsel_ItemCommand" OnItemDataBound="gvwocparcialsel_ItemDataBound" >
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" ></Scrolling>
                                                                                <Selecting AllowRowSelect="True"></Selecting>
                                                                            </ClientSettings>
                                                                            <MasterTableView>
                                                                                <Columns>
                                                                                    <telerik:GridTemplateColumn HeaderText="Parcial" AllowFiltering="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("No_RegistroParcial")+ "," + Eval("Op_OC") %>' CommandName="DelOCSel"
                                                                                                ImageUrl="~/Images/Icons/delete-16.png" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="30px" />
                                                                                    </telerik:GridTemplateColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Op_OC" DataField="Op_OC" UniqueName="Op_OC">
                                                                                        <HeaderStyle Width="30px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn UniqueName="FechaOrden" DataField="FechaOrden" HeaderText="FechaOrden" AllowFiltering="false"
                                                                                        HeaderStyle-Width="50px" DataFormatString="{0:dd/MM/yyyy}">
                                                                                        <HeaderStyle Width="50px" />
                                                                                    </telerik:GridBoundColumn>


                                                                                    <telerik:GridBoundColumn HeaderText="No Parcial" DataField="No_RegistroParcial" UniqueName="No_RegistroParcial">
                                                                                        <HeaderStyle Width="70px" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn HeaderText="NroParcial" DataField="NroParcial" UniqueName="NroParcial" Visible="false">
                                                                                        <HeaderStyle Width="70px" />
                                                                                    </telerik:GridBoundColumn>
                                                                   
                                                                                    <telerik:GridBoundColumn HeaderText="Neto" DataField="Neto" UniqueName="Neto" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Subtotal" DataField="Subtotal" UniqueName="Subtotal" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Impuestos" DataField="Impuestos" UniqueName="Impuestos" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Total" DataField="Total" UniqueName="Total">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="Id_SegImp" DataField="Id_SegImp" UniqueName="Id_SegImp" Visible="false">
                                                                                        <HeaderStyle Width="60px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                    <telerik:GridBoundColumn HeaderText="AgendaNombre" DataField="AgendaNombre" UniqueName="AgendaNombre" Visible="true">
                                                                                        <HeaderStyle Width="150px" />
                                                                                    </telerik:GridBoundColumn>

                                                                                </Columns>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </Content>
                                            </telerik:LayoutRow>


                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="pageparcial" CssClass="col-md-12" Height="100%" >
                                    <telerik:RadPageLayout ID="RadPageLayout5" runat="server" Height="100%" Width="100%">
                                        <Rows>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12 containerSubTitulo">
                                                        <asp:Label ID="Label4" runat="server" Text="Detalle de Seguimiento de las OC Seleccionadas" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-1">
                                                            <asp:label Text="Estado" runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <telerik:RadComboBox ID="cboEstado" runat="server" Width="80%" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged"></telerik:RadComboBox>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <telerik:RadAutoCompleteBox ID="acbAgente" runat="server" Width="80%" TextSettings-SelectionMode="Single" InputType="Text"
                                                                DropDownHeight="300px" EmptyMessage="Buscar Agente" AllowCustomEntry="true" Label="Agente" DropDownWidth="350px" LabelWidth="30%">
                                                                <WebServiceSettings Method="Agenda_BuscarAgente" Path="frmOcSegImp.aspx" />
                                                            </telerik:RadAutoCompleteBox>
                                                        </div>
                                                        <div class="col-md-4"></div>
                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <%--<asp:Label ID="Label1" runat="server" Text="Adición de Items" CssClass="subTitulo"></asp:Label>--%>
                                                            <telerik:RadDatePicker ID="dtpfechaetdaprox" runat="server" Width="80%"  
                                                                DateInput-Label="ETD Aprox" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="52%" DateInput-DisplayDateFormat="dd/MM/yyyy" >
                                                            </telerik:RadDatePicker>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <telerik:RadDatePicker ID="dtpfechaetdconfirmado" runat="server" Width="80%"  
                                                                DateInput-Label="ETD Confirmado" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="58%" DateInput-DisplayDateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </div>

                                                        
                                                        <div class="col-md-6"></div>
                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    
                                                        <div class="col-md-12 containerSubTitulo">
                                                            <asp:Label ID="Label1" runat="server" Text="Datos de Arribo" CssClass="subTitulo"></asp:Label>
                                                        </div>
                                               
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    
                                                    <div class="row">
                                                        
                                                        <div class="col-md-3">
                                                            <telerik:RadDatePicker ID="dtpfechaeta" runat="server" Width="80%"  
                                                                DateInput-Label="ETA" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="52%"  DateInput-DisplayDateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </div>
                                                        <%--<div class="col-md-1">
                                                            <asp:label Text="Dias Libres SE" runat="server" CssClass="etiqueta" ></asp:label>
                                                        </div>--%>
                                                        <div class="col-md-3">
                                                            <%--<telerik:RadTextBox ID="txtdiaslibresSe" runat="server" Width="60%" Label="Dias Libres SE" LabelWidth="70%" Type="Number" NumberFormat-DecimalDigits="4" MaxLength="12" MinValue="0" >
                                                                <ClientEvents OnBlur="Blur" />
                                                            </telerik:RadTextBox>--%>
                                                            <telerik:RadNumericTextBox ID="txtdiaslibresSe2" runat="server" Width="60%" Label="Dias Libres SE" LabelWidth="70%" Type="Number" NumberFormat-DecimalDigits="0" MaxLength="12" MinValue="0" ></telerik:RadNumericTextBox>
                                                            
                                                        </div>

                                                        <%--<div class="col-md-2">
                                                            <asp:label Text="Fecha Libre SE" runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>--%>
                                                        <div class="col-md-3">
                                                            <telerik:RadDatePicker ID="dtpfechadiaslibrese" DateInput-ReadOnly="true" runat="server" Width="80%" Culture = "en-US" DateInput-DateFormat="dd/MM/yyyy" DateInput-Label="Fecha Libre SE" DateInput-LabelWidth="50%">
                                                            </telerik:RadDatePicker>
                                                        </div>
                                                        <div class="col-md-3"></div>
                                                 </div>
                                                 <div class="row">
                                                     <div class="col-md-3"></div>
                                                     <div class="col-md-3">
                                                            <telerik:RadNumericTextBox ID="txtdiasalmacenaje2" runat="server" Width="60%" Label="Dias Almacenaje" LabelWidth="70%" Type="Number" 
                                                                NumberFormat-DecimalDigits="0" MaxLength="12" MinValue="0"></telerik:RadNumericTextBox>
                                                     </div>
                                                     <div class="col-md-3">
                                                            <telerik:RadDatePicker ID="dtpfechaalmacenaje" DateInput-ReadOnly="true" runat="server" Width="80%" Culture = "en-US" DateInput-DateFormat="dd/MM/yyyy" DateInput-Label="Fecha Libre Alm." DateInput-LabelWidth="50%">
                                                            </telerik:RadDatePicker>
                                                        </div>
                                                        <div class="col-md-3"></div>
                                                 </div> 
                                                    
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    
                                                        <div class="col-md-12 containerSubTitulo">
                                                            <asp:Label ID="Label2" runat="server" Text="Ingreso de MP" CssClass="subTitulo"></asp:Label>
                                                        </div>
                                               
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <telerik:RadDatePicker ID="dtpfechaingreso" runat="server" Width="80%"  
                                                                DateInput-Label="Fecha Ingreso" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="52%" DateInput-DisplayDateFormat="dd/MM/yyyy">
                                                            </telerik:RadDatePicker>
                                                        </div>

                                                        <%--<div class="col-md-2">
                                                            <asp:label Text="Dias Sobre Estadía" runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>--%>
                                                        <div class="col-md-3">
                                                            <telerik:RadTextBox ID="txtdiasSe" runat="server" ReadOnly="true" Width="60%" Label="Dias Sobre Estadía" LabelWidth="70%"></telerik:RadTextBox>
                                                        </div>
                                                        <%--<div class="col-md-2">
                                                            <asp:label Text="Estado Sobre Estadía" runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>--%>
                                                        <div class="col-md-3">
                                                            <telerik:RadTextBox ID="txtestadoSe" runat="server" ReadOnly="true" Width="60%" Label="Sobre Estaída" LabelWidth="60%"></telerik:RadTextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>

                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>


                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                       
                                                        <div class="col-md-1">
                                                            <asp:label Text="Tipo Vía" runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <telerik:RadComboBox ID="cbotipovia" runat="server"  Width="60%" AutoPostBack="true" OnSelectedIndexChanged="cbotipovia_SelectedIndexChanged"></telerik:RadComboBox>
                                                        </div>

                                                        
                                                        <div class="col-md-3">
                                                            <telerik:RadNumericTextBox ID="txtnrocontenedores" runat="server" Width="60%" Label="Contenedores" LabelWidth="70%" Type="Number" 
                                                                NumberFormat-DecimalDigits="0" MaxLength="12" MinValue="0"></telerik:RadNumericTextBox>
                                                        </div>
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-1"></div>
                                                        
                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    
                                                        <div class="col-md-12 containerSubTitulo">
                                                            <asp:Label ID="Label5" runat="server" Text="Datos de Aduana" CssClass="subTitulo"></asp:Label>
                                                        </div>
                                               
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-1">
                                                        <asp:label Text="Número Dua" runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <telerik:RadTextBox ID="txtnrodua" runat="server" Width="50%"></telerik:RadTextBox>
                                                        </div>

                                                        <%--<div class="col-md-1">
                                                            <asp:label Text="Número BL" runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>--%>

                                                        <div class="col-md-3">
                                                            <telerik:RadTextBox ID="txtnrobl" runat="server" Width="100%" Label="Numero BL" LabelWidth="43%"></telerik:RadTextBox>
                                                        </div>

                                                        <div class="col-md-6"></div>
                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-1">
                                                            <asp:label Text="Observ." runat="server" CssClass="etiqueta"></asp:label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <telerik:RadTextBox ID="txtlinkdua" runat="server" Width="600px"
                                                                TextMode="MultiLine" Resize="Both" ></telerik:RadTextBox>
                                                        
                                                        </div>
                                                        <div class="col-md-11">
                                                         
                                                        </div>
                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>
                                             <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            
                                                        </div>
                                                        <div class="col-md-1">
                                                            <telerik:RadButton ID="btnadjuntos" runat="server" Text="Adjuntos" OnClick="btnadjuntos_Click" >
                                                                <Icon PrimaryIconUrl="../../Images/Icons/folder-document-16.png"/>
                                                            </telerik:RadButton>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <telerik:RadButton ID="btnliquidacion" runat="server" Text="Liquidación" OnClick="btnliquidacion_Click" >
                                                                <Icon PrimaryIconUrl="../../Images/Icons/sign-check-16.png"/>
                                                            </telerik:RadButton>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <telerik:RadButton ID="btnguardar" runat="server" Text="Guardar" OnClick="btnguardar_Click" >
                                                                <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png"/>
                                                            </telerik:RadButton>
                                                        </div>
                                                        <div class="col-md-1">
                                                             <telerik:RadButton ID="btneliminarseg" runat="server" Text="Eliminar" OnClick="btneliminarseg_Click"  >
                                                                <Icon PrimaryIconUrl="../../Images/Icons/delete-16.png"/>
                                                            </telerik:RadButton>
                                                        </div>
                                                        
                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>
                                           

                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>

                            </telerik:RadMultiPage>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>                

            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>

