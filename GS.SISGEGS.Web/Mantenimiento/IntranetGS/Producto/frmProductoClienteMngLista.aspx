<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMSS.Master" AutoEventWireup="true" CodeBehind="frmProductoClienteMngLista.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Producto.frmProductoClienteMngLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //function CloseAndRebind(args) {
        //    Sys.Application.add_load(function () {
        //        var rWindow = GetRadWindow();
        //        rWindow.BrowserWindow.refreshGrid(args);
        //        rWindow.close();
        //    });
        //}

        function StringText() {
            setTimeout(function ()
              {
                var txtproducto = $find("<%= acbProducto.ClientID%>");
               
                txtproducto.get_entries().clear();
                txtproducto.focus();
              }, 0);
        }

        function CloseAndRebind(args) {
            setTimeout(function ()
              {
                   GetRadWindow().close();
              }, 0);
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
    <telerik:RadAjaxManager ID="ranProductoMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscarProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapProductoMng" LoadingPanelID="ralpProductoMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapProductoMng" LoadingPanelID="ralpProductoMng"/>
                    <telerik:AjaxUpdatedControl ControlID="rapProductoMng" LoadingPanelID="ralpProductoMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="acbProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="acbProducto" LoadingPanelID="ralpProductoMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpProductoMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmProductoMant" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwProductoMant" runat="server"  ReloadOnShow="true" 
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapProductoMng" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                 <telerik:LayoutRow >
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registro de Precios por Producto"></asp:Label>
                            
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow CssClass="containerSubTitulo">
                    <Content>
                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server"  CssClass="subTitulo"></asp:Label>
                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow >
                    <Content>
                        <div class="container-fluid">
                            <div class="row">
                                <%--<div class="col-md-5">
                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
                                </div>--%>
                                <div class="col-sm-6">
                                    <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                        DropDownHeight="150px" EmptyMessage="Buscar cliente" AllowCustomEntry="true" DropDownWidth="260px" Label="Cliente">
                                        <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmProductoClienteMng.aspx" />
                                    </telerik:RadAutoCompleteBox>
                                </div>
                                <div class="col-sm-5">
                                    <telerik:RadAutoCompleteBox ID="acbProducto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" 
                                        DropDownHeight="150px" AllowCustomEntry="true" DropDownWidth="260px" Label="Producto">
                                        <WebServiceSettings Method="Item_BuscarProducto" Path="frmProductoClienteMng.aspx" />
                                    </telerik:RadAutoCompleteBox>
                                </div>
                                <div class="col-sm-1">
                                    <telerik:RadButton ID="btnBuscarProducto" runat="server" Text="Image Button" Width="16px" Height="16px" OnClick="btnBuscarProducto_Click">
                                        <Image ImageUrl="../../../Images/Icons/search-16.png" />
                                    </telerik:RadButton>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3 col-md-4">
                                    <telerik:RadTextBox ID="txtKardex" runat="server" Enabled="false" Label="Kardex" Width="100%" LabelWidth="27%" ></telerik:RadTextBox>
                                </div>
            
                                <div class="col-sm-6 col-md-5">
                                    <telerik:RadTextBox ID="txtUnidad" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <telerik:RadComboBox ID="cboMoneda" runat="server" Enabled="false" Width="150px"></telerik:RadComboBox>
                                </div>
                                
                            </div>

                            <div class="row">
                                <div class="col-sm-3 col-md-3">
                                    <telerik:RadTextBox ID="txtPrecio" runat="server" Enabled="false" Width="100%" Label="Precio" LabelWidth="27%"></telerik:RadTextBox>
                                </div>
                                 <div class="col-sm-4 col-md-3">
                                     <telerik:RadNumericTextBox ID="txtPrecEspecial" runat="server" NumberFormat-DecimalDigits="6" Width="100%" Label="Precio Especial" LabelWidth="45%"></telerik:RadNumericTextBox>
                                 </div>
                                <div class="col-sm-3 col-md-3">
                                    <telerik:RadButton ID="btnTermino" runat="server" Text="Sin Termino" ToggleType="CheckBox" 
                                        ButtonType="ToggleButton" OnCheckedChanged="btnTermino_CheckedChanged"></telerik:RadButton>
                                </div>
                                <div class="col-sm-2 col-md-3"></div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 col-md-3">
                                    <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" >
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" Label="F.Inicio" LabelWidth="32%"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
             
                                <div class="col-sm-4 col-md-3">
                                    <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%">
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" Label="F.Final" LabelWidth="50%"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-sm-2 col-md-1">
                                    <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click">
                                        <Icon PrimaryIconUrl="../../../Images/Icons/sign-add-16.png"/>
                                    </telerik:RadButton>
                                </div>
                                <div class="col-sm-2 col-md-1">
                                    <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                                        <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png"/>
                                    </telerik:RadButton>
                                </div>
                                <div class="col-sm-1 col-md-1">
                                </div>
                            </div>
                            

                            <div class="row">
                                <div class="col-md-12">
                                        <asp:Label ID="Label3" runat="server"  CssClass="subTitulo"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
            
                                    <%--OnItemCommand="gvwparcial_ItemCommand" OnItemDataBound="gvwparcial_ItemDataBound">--%>
                                    <div class="col-sm-12">
                                        <telerik:RadGrid ID="gvwparcial" runat="server" Height="250px" Width="100%" AutoGenerateColumns="false" OnItemCommand="gvwparcial_ItemCommand">
                    
                                        <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView Width="900px">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Elim.">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btneliminaritem" runat="server" CommandName="EliminarItem" CommandArgument='<%# Eval("Id_Agenda") + "," + Eval("ItemCodigo") + "," + Eval("Kardex") %>'
                                                            ImageUrl="~/Images/Icons/delete-16.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                </telerik:GridTemplateColumn>


                                                <telerik:GridBoundColumn HeaderText="Id_Agenda" DataField="Id_Agenda" UniqueName="Id_Agenda" Visible="false">
                                                    <HeaderStyle Width="120px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Cliente" DataField="AgendaNombre" UniqueName="AgendaNombre">
                                                    <HeaderStyle Width="120px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="ItemCodigo" DataField="ItemCodigo" UniqueName="ItemCodigo">
                                                    <HeaderStyle Width="85px" />
                                                </telerik:GridBoundColumn>
                                                                   
                                                <telerik:GridBoundColumn HeaderText="Kardex" DataField="Kardex" UniqueName="Kardex">
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Nombre" DataField="NombreKardex" UniqueName="NombreKardex">
                                                    <HeaderStyle Width="300px" />
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn HeaderText="Unidad" DataField="Id_UnidadInv" UniqueName="Id_UnidadInv">
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Precio" DataField="Precio" UniqueName="Precio" DataFormatString="{0:F4}">
                                                    <HeaderStyle Width="70px" />
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn HeaderText="IdMoneda" DataField="IdMoneda" UniqueName="IdMoneda" Visible="false">
                                                    <HeaderStyle Width="70px" />
                                                </telerik:GridBoundColumn>
                                                                    
                                                <telerik:GridBoundColumn HeaderText="Moneda" DataField="Moneda" UniqueName="Moneda">
                                                    <HeaderStyle Width="85px" />
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn HeaderText="P. Especial" DataField="PrecioEspecial" UniqueName="PrecioEspecial" DataFormatString="{0:F4}">
                                                    <HeaderStyle Width="75px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridCheckBoxColumn DataField="SinTermino" HeaderText="Sin Termino" UniqueName="SinTermino">
                                                    <HeaderStyle Width="85px"/>
                                                </telerik:GridCheckBoxColumn>

                                                <telerik:GridBoundColumn UniqueName="FechaVigInicio" DataField="FechaVigInicio" HeaderText="Fecha Vig.Inicio" AllowFiltering="false"
                                                    HeaderStyle-Width="85px" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="FechaVigFin" DataField="FechaVigFin" HeaderText="Fecha Vig.Fin" AllowFiltering="false"
                                                    HeaderStyle-Width="85px" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>


                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Scrolling UseStaticHeaders="True" AllowScroll="true"></Scrolling>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Resizing AllowRowResize="True" EnableRealTimeResize="True"></Resizing>
                                        </ClientSettings>
                                    </telerik:RadGrid>

                                    </div>
                            </div>
                        </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>


    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
