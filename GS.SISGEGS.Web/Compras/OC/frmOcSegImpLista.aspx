<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOcSegImpLista.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.OC.frmOcSegImpLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Creacion de Seguimiento de Importacion
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramSeg" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gvwSeguimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSeg" LoadingPanelID="ralpSeg" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpSeg" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rwmSeg" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwSeg" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <%--<telerik:RadWindow ID="rwDocumento" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>--%>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlSeg" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="8">
                            <asp:Label ID="lblTitulo" runat="server" Text="Modulo de Gestion de Seguimientos" CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png" />
                            </telerik:RadButton>
                            
                        </telerik:LayoutColumn>

                        <telerik:LayoutColumn Span="1">
                        <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click"  ToolTip="Descargar Excel">
                                            <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                        </telerik:RadButton>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>

                <telerik:LayoutRow Height="95%">
                    <Content>
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="400px" Title="Filtros de Busqueda"
                                        EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaInicio" runat="server" Text="ETDAprox Ini." CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" DateInput-ReadOnly="true">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaFinal" runat="server" Text="ETDAprox Fin." CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%" DateInput-ReadOnly="true">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>



                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="Label3" runat="server" Text="F.Ingreso Ini." CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaIngresoIni" runat="server" Width="100%" DateInput-DisplayDateFormat="dd/MM/yyyy">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="Label4" runat="server" Text="F.Ingreso Fin." CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaIngresoFin" runat="server" Width="100%"  DateInput-DisplayDateFormat="dd/MM/yyyy">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>


                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="Label1" runat="server" Text="Proveedor" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <asp:textbox ID="txtproveedor" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>

                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="Label2" runat="server" Text="Estado" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadComboBox  ID="cbestado" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="200" RenderMode="Lightweight" />
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
                            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%">
                                <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%">
                                    <telerik:LayoutRow Height="100%">
                                        <Content>

                                                <telerik:RadGrid 
                                                ID="gvwSeguimiento" 
                                                runat="server" 
                                                AllowFilteringByColumn="true" ShowFooter="False" 
                                                AllowSorting="True" Width="100%" 
                                                AutoGenerateColumns="False" Height="100%" OnNeedDataSource="gvwSeguimiento_NeedDataSource" OnItemCommand="gvwSeguimiento_ItemCommand" 
                                                OnItemDataBound="gvwSeguimiento_ItemDataBound" OnPreRender="gvwSeguimiento_PreRender"  Skin="Office2010Silver" ShowGroupPanel="true"
                                                AllowPaging="False"
                                                    >
                                                <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                                
                                                <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                <MasterTableView ShowFooter="False" Width="2060px" >
                                                    <CommandItemSettings ShowExportToExcelButton="true" />

                                                    <Columns>
                                                       
                                                        <telerik:GridTemplateColumn HeaderText="Parcial" AllowFiltering="false" UniqueName="ibEditar">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("Id_SegImp") %>' CommandName="EditarSeg"
                                                                    ImageUrl="~/Images/Icons/pencil-16.png" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>

                                                        

                                                        <telerik:GridBoundColumn UniqueName="Id_SegImp" DataField="Id_SegImp" HeaderText="#Seg." AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Anno" DataField="Anno" HeaderText="Año" AllowFiltering="false">
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridBoundColumn UniqueName="No_RegistroParcial" DataField="No_RegistroParcial" HeaderText="Purchase Order" AllowFiltering="false">
                                                            <HeaderStyle Width="120px" />
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridCheckBoxColumn DataField="Liquidacion" HeaderText="Liq."   ToolTip="Proceso Liquidacion" UniqueName="Liquidacion" AllowSorting="true"   AllowFiltering="false">
                                                            <HeaderStyle Width="50px"/>
                                                        </telerik:GridCheckBoxColumn>

                                                        <telerik:GridBoundColumn UniqueName="FechaLiquidacion" DataField="FechaLiquidacion" HeaderText="F. Liq." AllowFiltering="false"
                                                            HeaderStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridBoundColumn UniqueName="UsuarioLiquidacion" DataField="UsuarioLiquidacion" HeaderText="User Liq." AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="NombreLiquidacion" DataField="NombreLiquidacion" HeaderText="NombreLiquidacion" Visible="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Id_Estado" DataField="Id_Estado" HeaderText="Id_Estado" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="NombreEstado" DataField="NombreEstado" HeaderText="Estado" AllowFiltering="false">
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridBoundColumn>

                                                        
                                                        <telerik:GridBoundColumn UniqueName="ID_Agenda" DataField="ID_Agenda" HeaderText="ID_Agenda" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridBoundColumn UniqueName="NombreProveedor" DataField="NombreProveedor" HeaderText="Proveedor" AllowFiltering="false">
                                                            <HeaderStyle Width="300px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="ItemCodigo" DataField="ItemCodigo" HeaderText="ItemCodigo" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridBoundColumn>

                                                        <%--<telerik:GridBoundColumn UniqueName="Kardex" DataField="Kardex" HeaderText="Kardex" AllowFiltering="false">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>--%>

                                                        <telerik:GridBoundColumn FilterControlWidth="50px" DataField="Kardex" HeaderText="Kardex" FilterDelay="2000"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                       <%-- <telerik:GridBoundColumn UniqueName="NombreKardex" DataField="NombreKardex" HeaderText="Ingrediente Activo" AllowFiltering="false">
                                                            <HeaderStyle Width="380px" />
                                                        </telerik:GridBoundColumn>--%>

                                                        <telerik:GridBoundColumn FilterControlWidth="250px" DataField="NombreKardex" HeaderText="Ingrediente Activo" FilterDelay="2000"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="380px" />
                                                        </telerik:GridBoundColumn>

                                                       <%-- <telerik:GridBoundColumn UniqueName="Marca" DataField="Marca" HeaderText="Marca" AllowFiltering="false">
                                                            <HeaderStyle Width="120px" />
                                                        </telerik:GridBoundColumn>--%>

                                                        <telerik:GridBoundColumn FilterControlWidth="100px" DataField="Marca" HeaderText="Marca" FilterDelay="2000"
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                            <HeaderStyle Width="120px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Id_UnidadInv" DataField="Id_UnidadInv" HeaderText="Unidad" AllowFiltering="false">
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Cantidad" DataField="Cantidad" HeaderText="Cantidad" AllowFiltering="false">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Precio" DataField="Precio" HeaderText="Precio" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridBoundColumn UniqueName="Importe" DataField="Importe" HeaderText="Importe USD" AllowFiltering="false">
                                                            <HeaderStyle Width="95px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="CantidadContenedor" DataField="CantidadContenedor" HeaderText="# de Contenedores" AllowFiltering="false">
                                                            <HeaderStyle Width="120px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Id_Agente" DataField="Id_Agente" HeaderText="Id_Agente" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="NombreAgente" DataField="NombreAgente" HeaderText="Agente de Aduanas" AllowFiltering="false">
                                                            <HeaderStyle Width="250px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="FechaETDAprox" DataField="FechaETDAprox" HeaderText="ETD Aprox" AllowFiltering="false"
                                                            HeaderStyle-Width="85px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="85px" />
                                                        </telerik:GridBoundColumn>

                                                       <telerik:GridBoundColumn UniqueName="FechaETD" DataField="FechaETD" HeaderText="ETD" AllowFiltering="false"
                                                            HeaderStyle-Width="85px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="85px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="FechaETA" DataField="FechaETA" HeaderText="ETA" AllowFiltering="false"
                                                            HeaderStyle-Width="85px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="85px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false" UniqueName="imgSE">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgSE" runat="server" Width="16px" Height="16px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="25px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn UniqueName="DiasLibresSE" DataField="DiasLibresSE" HeaderText="Días libres SE" AllowFiltering="false">
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridBoundColumn UniqueName="FechaSobreEstadia" DataField="FechaSobreEstadia" HeaderText="F. límite SE" AllowFiltering="false"
                                                            HeaderStyle-Width="90px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="90px" />
                                                        </telerik:GridBoundColumn>


                                                        <telerik:GridBoundColumn UniqueName="FechaIngresoAlm" DataField="FechaIngresoAlm" HeaderText="F. Ingreso a Planta" AllowFiltering="false"
                                                            HeaderStyle-Width="170px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="90px" />
                                                        </telerik:GridBoundColumn>

                                                        

                                                        <telerik:GridBoundColumn UniqueName="DiasSobreEstadia" DataField="DiasSobreEstadia" HeaderText="Días de SE" AllowFiltering="false">
                                                            <HeaderStyle Width="75px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="EstadoSobreEstadia" DataField="EstadoSobreEstadia" HeaderText="SE" AllowFiltering="false">
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridBoundColumn>



                                                        <telerik:GridBoundColumn UniqueName="Id_TipoVia" DataField="Id_TipoVia" HeaderText="Id_TipoVia" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                       
                                                        <telerik:GridBoundColumn UniqueName="NombreVia" DataField="NombreVia" HeaderText="Vía" AllowFiltering="false">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false" UniqueName="imgALM">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgALM" runat="server" Width="16px" Height="16px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="25px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn UniqueName="DiasAlmacenaje" DataField="DiasAlmacenaje" HeaderText="Días Alm." AllowFiltering="false">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="NumeroDua" DataField="NumeroDua" HeaderText="DAM" AllowFiltering="false">
                                                            <HeaderStyle Width="90px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="NumeroBL" DataField="NumeroBL" HeaderText="# de BL/AWB" AllowFiltering="false">
                                                            <HeaderStyle Width="90px" />
                                                        </telerik:GridBoundColumn>
                   

                                                        <telerik:GridBoundColumn UniqueName="LinkDua" DataField="LinkDua" HeaderText="Link/Obs." AllowFiltering="false">
                                                            <HeaderStyle Width="150px" />
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
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
</asp:Content>
