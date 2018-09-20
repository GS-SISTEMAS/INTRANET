<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmMetaPresupuestoIng.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmMetaPresupuestoIng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Ingreso de Presupuestos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>
    <script type="text/javascript">
        function StringText() {
                setTimeout(function ()
                  {
                    var txtcliente = $find("<%= acbCliente.ClientID%>");
                    var txtproducto = $find("<%= abcProducto.ClientID%>");
                    <%--var txtprecio = $find("<%= txtprecio2.ClientID%>");
                    var txtcantidad= $find("<%= txtCantidad2.ClientID%>");--%>
                    txtproducto.get_entries().clear();
                    //txtprecio.text = "";
                    //txtcantidad.text = "";
                    txtproducto.focus();

                    //txtcliente.get_entries().clear();
                    //txtcliente.focus();
                  }, 0);
        }

        
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }
    
        <%--function AsignarTabStrip()
        {
            var tabStrip= $find("<%= RadTabStrip1.ClientID %>");
        }--%>


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpPre" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmPre" runat="server" EnableShadow="true">
        <Windows>
             <telerik:RadWindow ID="rwPre" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlPre" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart" >
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow >
                    <content>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registro de Presupuesto por Jefe de Zona"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" Style="top: 1px; left: 3px" Width="100px">
                                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                    </telerik:RadButton>
                                </div>

                            </div>
                        </div>
                    </content>
                </telerik:LayoutRow>
                
                <telerik:LayoutRow Height="100%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadTabStrip runat="server" ID="stripPre" MultiPageID="radmultipage" SelectedIndex="1"  CssClass="col-md-12" Skin="Office2010Silver">
                                <Tabs>
                                    <telerik:RadTab Text="Consulta de Presupuestos" Selected="True"></telerik:RadTab>
                                    <telerik:RadTab Text="Registro"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="radmultipage" SelectedIndex="0" Height="93%" Width="100%"  CssClass="col-md-12">
                                <telerik:RadPageView runat="server" ID="consultaPre" Height="100%">
                                    <telerik:LayoutRow Height="95%">
                                        <Content>

                                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%"  Skin="Office2010Silver">
                                                <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None"  Skin="Office2010Silver">
                                                    <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px"  Skin="Office2010Silver">
                                                        <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="400px" Title="Filtros de Busqueda" 
                                                            EnableDock="true" MinWidth="225" MinHeight="225" Scrolling="None"  Skin="Office2010Silver">
                                                            <div class="fila">
                                                                <div class="colum3">
                                                                    <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicial" CssClass="etiqueta"></asp:Label>
                                                                </div>
                                                                <div class="colum7">
                                                                    <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" DateInput-ReadOnly="true">
                                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                                    </telerik:RadDatePicker>
                                                                </div>
                                                            </div>
                                                            <div class="fila">
                                                                <div class="colum3">
                                                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final" CssClass="etiqueta"></asp:Label>
                                                                </div>
                                                                <div class="colum7">
                                                                    <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%" DateInput-ReadOnly="true">
                                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                                    </telerik:RadDatePicker>
                                                                </div>
                                                            </div>


                                                            <div class="fila">
                                                                
                                                                <div class="colum3">
                                                                    <asp:Label ID="Label1" runat="server" Text="Jefe Zona" CssClass="etiqueta"></asp:Label>
                                                                </div>
                                                                <div class="colum7">
                                                                    <telerik:RadAutoCompleteBox ID="abcJefeZona" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                                                        DropDownHeight="150px" EmptyMessage="Buscar Jefe de Zona" AllowCustomEntry="true" DropDownWidth="200px" >
                                                                        <WebServiceSettings Method="Agenda_BuscarVendedor" Path="frmMetaPresupuestoIng.aspx" />
                                                                    </telerik:RadAutoCompleteBox>
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
                                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="700px">
                                                    <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%">

                                                        <telerik:LayoutRow Height="100%">
                                                            <Content>

                                                                    <telerik:RadGrid 
                                                                    ID="gvwSeguimiento" 
                                                                    runat="server" 
                                                                    ShowFooter="False" 
                                                                    AllowSorting="True" Width="100%" 
                                                                    AutoGenerateColumns="False" Height="100%" 
                                                                    Skin="Office2010Silver" 
                                                                    ShowGroupPanel="false" OnItemCommand="gvwSeguimiento_ItemCommand" 
                                                                     OnNeedDataSource="gvwSeguimiento_NeedDataSource"
                                                                    AllowPaging="False" CssClass="containerSubTitulo"
                                                                    >
                                                                    <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                                
                                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                                    <MasterTableView ShowFooter="true" Width="100%" ShowGroupFooter="true">
                                                                        <GroupByExpressions>
                                                                            <telerik:GridGroupByExpression>
                                                                                <SelectFields>
                                                                                    <telerik:GridGroupByField FieldAlias="Vendedor" FieldName="Vendedor"></telerik:GridGroupByField>
                                                                                </SelectFields>
                                                                                <GroupByFields>
                                                                                    <telerik:GridGroupByField FieldName="Vendedor" SortOrder="Ascending"></telerik:GridGroupByField>
                                                                                </GroupByFields>
                                                                            </telerik:GridGroupByExpression>

                                                                            <telerik:GridGroupByExpression>
                                                                                <SelectFields>
                                                                                    <telerik:GridGroupByField FieldAlias="Periodo" FieldName="Periodo"></telerik:GridGroupByField>
                                                                                </SelectFields>
                                                                                <GroupByFields>
                                                                                    <telerik:GridGroupByField FieldName="Periodo"></telerik:GridGroupByField>
                                                                                </GroupByFields>
                                                                            </telerik:GridGroupByExpression>

                                                                            <%--<telerik:GridGroupByExpression>
                                                                                <SelectFields>
                                                                                    <telerik:GridGroupByField FieldAlias="Cliente" FieldName="Cliente"></telerik:GridGroupByField>
                                                                                </SelectFields>
                                                                                <GroupByFields>
                                                                                    <telerik:GridGroupByField FieldName="Cliente"></telerik:GridGroupByField>
                                                                                </GroupByFields>
                                                                            </telerik:GridGroupByExpression>--%>
                                                                        </GroupByExpressions>
                                                                        <CommandItemSettings ShowExportToExcelButton="true" />

                                                                            <Columns>
                                                       
                                                                                <telerik:GridTemplateColumn HeaderText="Editar" AllowFiltering="false" UniqueName="ibEditar">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("Anno") + "," + Eval("Mes") + "," + Eval("Id_Vendedor") %>' CommandName="EditarSeg"
                                                                                            ImageUrl="~/Images/Icons/pencil-16.png" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridTemplateColumn>

                                                                                <telerik:GridTemplateColumn HeaderText="Promotor" AllowFiltering="false" UniqueName="ibProm">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ibProm" runat="server" CommandArgument='<%# Eval("Anno") + "," + Eval("Mes") + "," + Eval("Id_Vendedor") + "," + Eval("Aprobado") %>' CommandName="AgregarPromotor"
                                                                                            ImageUrl="~/Images/Icons/money-16.png" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridTemplateColumn>

                                                        
                                                                                <telerik:GridBoundColumn UniqueName="Periodo" DataField="Periodo" HeaderText="Periodo" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Anno" DataField="Anno" HeaderText="Anno" AllowFiltering="false">
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Mes" DataField="Mes" HeaderText="Mes" AllowFiltering="false">
                                                                                    <HeaderStyle Width="60px" />
                                                                                </telerik:GridBoundColumn>


                                                                                <telerik:GridBoundColumn UniqueName="Id_Vendedor" DataField="Id_Vendedor" HeaderText="Id_Vendedor" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="120px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Vendedor" DataField="Vendedor" HeaderText="Vendedor" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="170px" />
                                                                                </telerik:GridBoundColumn>
                                                                         
                                                                                


                                                                                <telerik:GridBoundColumn UniqueName="ID_Zona" DataField="ID_Zona" HeaderText="ID_Zona" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="100px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="ZonaNombre" DataField="ZonaNombre" HeaderText="Zona" Visible="true">
                                                                                    <HeaderStyle Width="100px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Fecha" DataField="Fecha" HeaderText="Fecha" AllowFiltering="false" Visible="false"
                                                                                    HeaderStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}">
                                                                                    <HeaderStyle Width="80px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridCheckBoxColumn DataField="Aprobado" HeaderText="Aprob." UniqueName="Aprobado" AllowSorting="true"   AllowFiltering="false">
                                                                                    <HeaderStyle Width="70px"/>
                                                                                </telerik:GridCheckBoxColumn>

                                                                                <telerik:GridBoundColumn UniqueName="NomUsuario" DataField="NomUsuario" HeaderText="Usuario" Visible="false">
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="FechaRegistro" DataField="FechaRegistro" HeaderText="FechaRegistro" AllowFiltering="false" Visible="false"
                                                                                    HeaderStyle-Width="90px" DataFormatString="{0:dd/MM/yyyy}">
                                                                                    <HeaderStyle Width="90px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Cliente" DataField="Cliente" HeaderText="Cliente" AllowFiltering="false" Visible="true">
                                                                                    <HeaderStyle Width="300px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Item_ID" DataField="Item_ID" HeaderText="Kardex" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="70px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="nombreKardex" DataField="nombreKardex" HeaderText="Nombre Kardex" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="250px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="ItemCodigo" DataField="ItemCodigo" HeaderText="ItemCodigo" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="100px" />
                                                                                </telerik:GridBoundColumn>


                                                                                <telerik:GridBoundColumn UniqueName="Unidad" DataField="Unidad" HeaderText="Unidad" AllowFiltering="false" Visible="false">
                                                                                    <HeaderStyle Width="60px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Id_G5" DataField="Id_G5" HeaderText="Id_G5" AllowFiltering="false" Visible="true">
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridBoundColumn>

                                                                                 <telerik:GridBoundColumn UniqueName="NombreG5" DataField="NombreG5" HeaderText="NombreG5" AllowFiltering="false" Visible="true">
                                                                                    <HeaderStyle Width="250px" />
                                                                                </telerik:GridBoundColumn>


                                                                                <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad" HeaderText="Cantidad" AllowFiltering="false" Aggregate="Sum" FooterText="Total:">
                                                                                    <HeaderStyle Width="70px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Precio" DataField="Precio" HeaderText="Precio" AllowFiltering="false">
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridBoundColumn>

                                                                                <telerik:GridBoundColumn UniqueName="Total" DataField="Total" HeaderText="Total" AllowFiltering="false" Aggregate="Sum" FooterText="Total: ">
                                                                                    <HeaderStyle Width="50px" />
                                                                                </telerik:GridBoundColumn>

                                                                            </Columns>
                                                                    </MasterTableView>
                                                                    <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                                                        <Scrolling UseStaticHeaders="True" AllowScroll="true"></Scrolling>
                                                                        <Selecting AllowRowSelect="True"></Selecting>
                                                                        <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                                                    ResizeGridOnColumnResize ="False"></Resizing>
                                                                    </ClientSettings>
                                                                        <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                                                                    </telerik:RadGrid>
                                                                </Content>
                                                            </telerik:LayoutRow>

                                                        </telerik:RadPageLayout>
                                                    </telerik:RadPane>
                                                </telerik:RadSplitter>


                                        </Content>
                                    </telerik:LayoutRow>

                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="registroPre" Height="100%">

                                    <telerik:RadPageLayout ID="RadPageLayout4" runat="server" Width="100%" Height="100%">
                                        <Rows>
                                            
                                            <telerik:LayoutRow >
                                                <Content>
                                                    <div class="row" style="padding-bottom:15px;"> 
                                                        <div class="col-md-3">
                                                        <telerik:RadTextBox ID="txtzona" runat="server" ReadOnly="true" Label="Zona" Width="100%" LabelWidth="20%"></telerik:RadTextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <telerik:RadMonthYearPicker ID="rmyPre" runat="server" DateInput-Label="Periodo">
                                                                <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                                            </telerik:RadMonthYearPicker>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:HiddenField runat="server" ID="txt_idvendedor" Value=""/>
                                                            <asp:HiddenField runat="server" ID="txt_idzona" Value="" />
                                                            <telerik:RadTextBox ID="txtvendedor" runat="server" ReadOnly="true" Width="100%" Label="Jefe de Zona"  LabelWidth="30%" ></telerik:RadTextBox>
                                                        </div>
                                                    
                                                        <div class="col-md-2">
                                                            <asp:Label ID="lblestado" runat="server" Label="Estado" LabelWidth="100%" ForeColor="Red" Font-Size="Medium" Font-Bold="true" BackColor="#ffff99"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                            <telerik:RadButton ID="btnnuevo" runat="server" Text="Nuevo" OnClick="btnnuevo_Click">
                                                                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png" />
                                                            </telerik:RadButton>
                                                            <telerik:RadButton ID="btnaprobar" runat="server" Text="Aprobar" OnClick="btnaprobar_Click">
                                                                <Icon PrimaryIconUrl="../../Images/Icons/sign-check-16.png" />
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
                                                    <div class="row" style="padding:3px 0px 3px 0px;">
                                                        <div class="col-md-3">
                                                        <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                                            DropDownHeight="150px" EmptyMessage="Buscar cliente" AllowCustomEntry="true" Label="Cliente" LabelWidth="20%" DropDownWidth="200px">
                                                            <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmMetaPresupuestoIng.aspx" />
                                                        </telerik:RadAutoCompleteBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <telerik:RadAutoCompleteBox ID="abcProducto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                                                DropDownHeight="150px" EmptyMessage="Buscar Producto" AllowCustomEntry="true" Label="Producto" DropDownWidth="200px" >
                                                                <WebServiceSettings Method="Item_BuscarProducto" Path="frmMetaPresupuestoIng.aspx" />
                                                            </telerik:RadAutoCompleteBox>
                                                        </div>

                                                        <div class="col-md-2">
                                                            
                                                            <telerik:RadNumericTextBox ID="txtCantidad2" runat="server" Type="Number" NumberFormat-DecimalDigits="2" MinValue="0" Label="Cantidad Kg/Lt" LabelWidth="50%"
                                                                NumberFormat-GroupSeparator="" MaxLength="6" Width="100%">
                                                            </telerik:RadNumericTextBox>
                                                        </div>


                                                        <div class="col-md-2">
                                                            <telerik:RadNumericTextBox ID="txtprecio2" runat="server" Type="Number" NumberFormat-DecimalDigits="2" MinValue="0" Label="Precio" LabelWidth="40%"
                                                                NumberFormat-GroupSeparator="" MaxLength="6" Width="100%">
                                                            </telerik:RadNumericTextBox>
                                                        </div>
                                                    
                                                        
                                                        <div class="col-md-2">
                                                            <telerik:RadButton ID="btnagregar" runat="server" Text="Agregar" OnClick="btnagregar_Click">
                                                                            <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                                                                        </telerik:RadButton>
                                                            <telerik:RadButton ID="btnguardar" runat="server" Text="Guardar" OnClick="btnguardar_Click">
                                                                            <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                                                                        </telerik:RadButton>
                                                        </div>

                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow >
                                                <Content>
                                                    <div class="row" style="padding:3px 0px 3px 0px;text-align:center;">
                                                        <div class="col-md-12">
                                                            <asp:Label Text=".::Detalle::." runat="server" CssClass="etiqueta"></asp:Label>
                                                        </div>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            
                                            <telerik:LayoutRow >
                                                    <Columns>
                                                        <telerik:LayoutColumn Height="100%">
                                                            <telerik:RadGrid
                                                            ID="gvwItems"
                                                            runat="server"
                                                            ShowFooter="true"
                                                            AllowSorting="True" Width="100%"
                                                            AutoGenerateColumns="False" Height="400px"
                                                            Skin="Office2010Silver" OnItemCommand="gvwItems_ItemCommand" OnItemDataBound="gvwItems_ItemDataBound"
                                                            
                                                            
                                                            AllowPaging="False" CssClass="containerSubTitulo">
                                                        
                                                            <MasterTableView ShowFooter="true" Width="100%">
                                                            
                                                                <Columns>

                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" AllowFiltering="false" UniqueName="ibEditar">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("Id_Cliente") + "," + Eval("CodigoProducto") %>' CommandName="EliminarItem"
                                                                                ImageUrl="~/Images/Icons/delete-16.png" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridTemplateColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Id" DataField="Id" HeaderText="Id" AllowFiltering="false" Visible="false">
                                                                        <HeaderStyle Width="40px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Anno" DataField="Anno" HeaderText="Año" AllowFiltering="false" Visible="false">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Mes" DataField="Mes" HeaderText="Mes" AllowFiltering="false" Visible="false">
                                                                        <HeaderStyle Width="60px" />
                                                                    </telerik:GridBoundColumn>


                                                                    <telerik:GridBoundColumn UniqueName="Id_Vendedor" DataField="Id_Vendedor" HeaderText="Id_Vendedor" AllowFiltering="false" Visible="false">
                                                                        <HeaderStyle Width="120px" />
                                                                    </telerik:GridBoundColumn>

                                                                    

                                                                    <telerik:GridBoundColumn UniqueName="Id_Cliente" DataField="Id_Cliente" HeaderText="Ruc" AllowFiltering="false">
                                                                        <HeaderStyle Width="100px" />
                                                                    </telerik:GridBoundColumn>


                                                                    <telerik:GridBoundColumn UniqueName="TipoCliente" DataField="TipoCliente" HeaderText="TipoCliente" AllowFiltering="false">
                                                                        <HeaderStyle Width="100px" />
                                                                    </telerik:GridBoundColumn>


                                                                    <telerik:GridBoundColumn UniqueName="Cliente" DataField="Cliente" HeaderText="Cliente" AllowFiltering="false">
                                                                        <HeaderStyle Width="300px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="CodigoProducto" DataField="CodigoProducto" HeaderText="CodigoProducto" AllowFiltering="false" Visible="false">
                                                                        <HeaderStyle Width="120px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Kardex" DataField="Kardex" HeaderText="Kardex" AllowFiltering="false" Visible="false">
                                                                        <HeaderStyle Width="80px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="NombreKardex" DataField="NombreKardex" HeaderText="NombreKardex" AllowFiltering="false" Visible="false">
                                                                        <HeaderStyle Width="250px" />
                                                                    </telerik:GridBoundColumn>

                                                                     <telerik:GridBoundColumn UniqueName="Id_G5" DataField="Id_G5" HeaderText="Id_G5" AllowFiltering="false" Visible="true">
                                                                        <HeaderStyle Width="100px" />
                                                                    </telerik:GridBoundColumn>

                                                                     <telerik:GridBoundColumn UniqueName="NombreG5" DataField="NombreG5" HeaderText="NombreG5" AllowFiltering="false" Visible="true">
                                                                        <HeaderStyle Width="250px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad" HeaderText="Cantidad" AllowFiltering="false" Aggregate="Sum" FooterAggregateFormatString="{0:n}">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn UniqueName="Precio" DataField="Precio" HeaderText="Precio" AllowFiltering="false">
                                                                        <HeaderStyle Width="100px" />
                                                                    </telerik:GridBoundColumn>


                                                                    <telerik:GridBoundColumn UniqueName="Total" DataField="Total" HeaderText="Total" AllowFiltering="false" Aggregate="Sum" FooterAggregateFormatString="{0:n}">
                                                                        <HeaderStyle Width="100px" />
                                                                    </telerik:GridBoundColumn>





                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                                                <Scrolling UseStaticHeaders="True" AllowScroll="true"></Scrolling>
                                                                <Selecting AllowRowSelect="True"></Selecting>
                                                                <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                                    ResizeGridOnColumnResize="False"></Resizing>
                                                            </ClientSettings>
                                                        
                                                            </telerik:RadGrid>
                                                     </telerik:LayoutColumn>
                                                        
                                                        
                                                    </Columns>
                                                    
                                                 
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


