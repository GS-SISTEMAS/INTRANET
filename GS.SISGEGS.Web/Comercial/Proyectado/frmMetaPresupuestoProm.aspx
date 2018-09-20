<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmMetaPresupuestoProm.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmMetaPresupuestoProm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Ingreso de Presupuestos por Promotor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>

    <script type="text/javascript">

        function ShowInsertForm(strId_Vendedor, strAnno, strMes) {
            window.radopen("frmMetaPresupuestoPromAdd.aspx?Id_Vendedor=" + strId_Vendedor +
                "&Anno=" + strAnno + "&Mes=" + strMes, "rwPre");
            return false;
        }

        function refreshGrid(arg) {
            <%--if (!arg) {
                $find("<%= ramPresupuesto.ClientID %>").ajaxRequest("Rebind");
            }--%>
            
                $find("<%= ramPresupuesto.ClientID %>").ajaxRequest("Rebind");
            
           
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpPre" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramPresupuesto" runat="server" OnAjaxRequest="ramPresupuesto_AjaxRequest">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="RadPageLayout1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnagregar" LoadingPanelID="ralpPre"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rpgResumenZona" LoadingPanelID="ralpPre"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnAprobar" LoadingPanelID="ralpPre"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>  


            <%--<telerik:AjaxSetting AjaxControlID="rpgResumenZona">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPre" LoadingPanelID="ralpPre"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>

            <%--<telerik:AjaxSetting AjaxControlID="btnagregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rpgResumenZona" LoadingPanelID="ralpPre"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="rwmPre" runat="server" EnableShadow="true">
        <Windows>
             <telerik:RadWindow ID="rwPre" runat="server" Width="700px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlPre" runat="server" Width="100%" Height="100%" >
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow >
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registro de Presupuesto por Jefe de Zona"></asp:Label>
                        </telerik:LayoutColumn>
                        <%--<telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png"/>
                            </telerik:RadButton>
                        </telerik:LayoutColumn>--%>
                    </Columns>
                </telerik:LayoutRow>

                <telerik:LayoutRow CssClass="containerSubTitulo">
                    <Content>
                        <div class="col-md-12">
                            <asp:Label ID="Label3" runat="server" CssClass="subTitulo"></asp:Label>
                        </div>
                    </Content>
                </telerik:LayoutRow>

                <telerik:LayoutRow>
                    <Content>
                        <div class="row">
                            <div class="col-md-4">
                                <telerik:RadTextBox ID="txtperiodo" runat="server" Label="Periodo:" Width="100%" LabelWidth="20%" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblestado" runat="server" Label="Estado" LabelWidth="100%" ForeColor="Red" Font-Size="Medium" Font-Bold="true" BackColor="#ffff99"></asp:Label>
                            </div>
                            <div class="col-md-4">

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <telerik:RadTextBox ID="txtzona" runat="server" Label="Zona:" Width="100%" LabelWidth="20%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadTextBox ID="txtjefezona" runat="server" Label="Jefe de Zona:" Width="100%" LabelWidth="20%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label Text="Productos Disponibles del Periodo:" CssClass="etiqueta" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-12">
                                        <telerik:RadGrid ID="gvwProductos" runat="server" Width="100%" Height="450px" 
                                            AutoGenerateColumns="false" Skin="Office2010Silver" ShowFooter="true" >
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" ></Scrolling>
                                                <Selecting AllowRowSelect="True"></Selecting>
                                            </ClientSettings>
                                            <MasterTableView>
                                                <Columns>
                                              
                                                    <telerik:GridBoundColumn HeaderText="Periodo" DataField="Periodo" UniqueName="Periodo" Visible="false">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Id_G5" DataField="Id_G5" UniqueName="Id_G5" Visible="false">
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Nombre_G5" DataField="Nombre_G5" UniqueName="Nombre_G5">
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridBoundColumn>


                                                    <telerik:GridBoundColumn HeaderText="Cantidad" DataField="Cantidad" UniqueName="Cantidad" Visible="true" Aggregate="Sum" DataFormatString="{0:N}">
                                                        <HeaderStyle Width="60px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Precio" DataField="Precio" UniqueName="Precio" Visible="true" Aggregate="Avg" DataFormatString="{0:N}">
                                                        <HeaderStyle Width="60px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Total" DataField="Total" UniqueName="Total" Visible="true" Aggregate="Sum" DataFormatString="{0:N}">
                                                        <HeaderStyle Width="60px" />
                                                    </telerik:GridBoundColumn>


                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>

                                </div>
                            </div>

                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label Text="Resumen por Promotor" CssClass="etiqueta" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <telerik:RadButton ID="btnagregar" runat="server" Text="Agregar" OnClick="btnagregar_Click">
                                                <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                                        </telerik:RadButton>
                                    </div>
                                    <div class="col-md-3">
                                        <telerik:RadButton ID="btnregresar" runat="server" Text="Regresar" OnClick="btnregresar_Click">
                                                <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png" />
                                        </telerik:RadButton>
                                    </div>
                                    <div class="col-md-3">
                                        <telerik:RadButton ID="btnAprobar" runat="server" Text="Aprobar" OnClick="btnAprobar_Click">
                                                <Icon PrimaryIconUrl="../../Images/Icons/pencil-16.png" />
                                        </telerik:RadButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="100" Height="450px"
                                            ID="rpgResumenZona" runat="server" ColumnHeaderZoneText="ColumnHeaderZone" RowGroupsDefaultExpanded="false"
                                            Skin="Office2010Silver" OnNeedDataSource="rpgResumenZona_NeedDataSource">
                                            <ClientSettings EnableFieldsDragDrop="false">
                                                <Scrolling AllowVerticalScroll="true"></Scrolling>

                                            </ClientSettings>
                                            <Fields>
                                                <telerik:PivotGridRowField DataField="NombreCliente" ZoneIndex="0" CellStyle-Width="250px">
                                                    <CellStyle Width="250px" Font-Size="X-Small" />
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridColumnField DataField="NombrePromotor">
                                                    <CellStyle Width="100px" Font-Size="X-Small" />
                                                </telerik:PivotGridColumnField>



                                                <%--<telerik:PivotGridAggregateField DataField="Importe_USD" Aggregate="Sum" DataFormatString="${0:N}" Caption="Importe $" GrandTotalAggregateFormatString="${0:N}">
                                            <CellStyle  Width="100px"  Font-Size="X-Small"  />
                                            <HeaderCellTemplate>
                                                Total Importe
                                            </HeaderCellTemplate>
                                            <ColumnGrandTotalHeaderCellTemplate>
                                                Total Importe
                                            </ColumnGrandTotalHeaderCellTemplate>
                                        </telerik:PivotGridAggregateField>--%>

                                                <telerik:PivotGridAggregateField DataField="Total" Aggregate="Sum" DataFormatString="${0:N}" Caption="Saldo $" GrandTotalAggregateFormatString="${0:N}">
                                                    <CellStyle Width="100px" Font-Size="X-Small" />
                                                    <HeaderCellTemplate>
                                                        Total
                                                    </HeaderCellTemplate>
                                                    <ColumnGrandTotalHeaderCellTemplate>
                                                        Total
                                                    </ColumnGrandTotalHeaderCellTemplate>
                                                </telerik:PivotGridAggregateField>
                                            </Fields>
                                            <%--<SortExpressions>
                                        <telerik:PivotGridSortExpression FieldName="NombreZona" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                    </SortExpressions>--%>
                                        </telerik:RadPivotGrid>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </Content>
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
