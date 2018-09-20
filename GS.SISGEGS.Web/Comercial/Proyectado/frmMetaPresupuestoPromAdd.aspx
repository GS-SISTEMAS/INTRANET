<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpM.Master" AutoEventWireup="true" CodeBehind="frmMetaPresupuestoPromAdd.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmMetaPresupuestoPromAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>

    <%--<script>
        function ClearText() {
            var txtmonto = $find("<%= txtmonto.ClientID %>");
            txtmonto.set_value("0");
        }

    </script>--%>

    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(args);
                rWindow.close();
            });
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpPre" runat="server">
    </telerik:RadAjaxLoadingPanel>
    

    <telerik:RadWindowManager ID="rwmPre" runat="server" EnableShadow="true">
        <Windows>
             <telerik:RadWindow ID="rwPre" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registro de Presupuesto por Promotor"></asp:Label>
                        </telerik:LayoutColumn>
                  
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
                            <div class="col-xs-6">
                                <telerik:RadComboBox ID="cbcliente" runat="server" Height="200" Width="200px"
                                    EmptyMessage="Seleccione un Cliente"
                                    Label="Cliente :" Skin="Office2010Silver">
                                    <WebServiceSettings Path="frmMetaPresupuestoPromAdd.aspx" Method="CargarClientes" />
                                </telerik:RadComboBox>
                            </div>

                            <div class="col-xs-6">
                                <telerik:RadComboBox ID="cbpromotor" runat="server" Height="200" Width="200"
                                    EmptyMessage="Seleccione un Promotor"
                                    Label="Promotor :" Skin="Office2010Silver">
                                    <WebServiceSettings Path="Territories.asmx" Method="GetTerritories" />
                                </telerik:RadComboBox>
                            </div>
                            
                            
                        </div>
                        <div class="row">
                            
                           <div class="col-xs-6">
                                <telerik:RadNumericTextBox ID="txtmonto" runat="server" Type="Number" NumberFormat-DecimalDigits="2" MinValue="0" Label="Total :"
                                    NumberFormat-GroupSeparator="" MaxLength="6" Width="150px" 6>
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-xs-2">
                                <telerik:RadButton ID="btnagregar" runat="server" Text="Agregar" OnClick="btnagregar_Click">
                                    <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                                </telerik:RadButton>
                            </div>
                            <div class="col-xs-2">
                                <telerik:RadButton ID="btncerrar" runat="server" Text="Cerrar" OnClick="btncerrar_Click">
                                    <Icon PrimaryIconUrl="../../Images/Icons/delete-16.png" />
                                </telerik:RadButton>
                            </div>
                            <div class="col-xs-2"></div>
                            
                            
                        </div>
                        <%--<div class="row">
                            <div class="col-md-4">
                                <telerik:RadTextBox ID="txtzona" runat="server" Label="Zona:" Width="100%" LabelWidth="20%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadTextBox ID="txtjefezona" runat="server" Label="Jefe de Zona:" Width="100%" LabelWidth="20%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-4"></div>
                        </div>--%>
                    </Content>
                </telerik:LayoutRow>

                <telerik:LayoutRow CssClass="containerSubTitulo">
                    <Content>
                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" CssClass="subTitulo"></asp:Label>
                        </div>
                    </Content>
                </telerik:LayoutRow>

                <telerik:LayoutRow>
                    <Content>
                        <div class="row">
                            <div class="col-md-12">
                                        <telerik:RadGrid ID="gvwProductos" runat="server" Width="100%" Height="350px" 
                                            AutoGenerateColumns="false" Skin="Office2010Silver" ShowFooter="true" OnItemCommand="gvwProductos_ItemCommand"
                                             >
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" ></Scrolling>
                                                <Selecting AllowRowSelect="True"></Selecting>
                                            </ClientSettings>
                                            <MasterTableView>
                                                <Columns>
                                                    
                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" AllowFiltering="false" UniqueName="ibEliminar">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ibEliminar" runat="server" CommandArgument='<%# Eval("Id") + "," + Eval("Aprobado") %>' CommandName="EliminarProm"
                                                                                            ImageUrl="~/Images/Icons/delete-16.png" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="40px" />
                                                                                </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn HeaderText="Id" DataField="Id" UniqueName="Id" Visible="false">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Anno" DataField="Anno" UniqueName="Anno" Visible="false">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Mes" DataField="Mes" UniqueName="Mes" Visible="false">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Id_Vendedor" DataField="Id_Vendedor" UniqueName="Id_Vendedor" Visible="false">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Id_Cliente" DataField="Id_Cliente" UniqueName="Id_Cliente" Visible="false">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="NombreCliente" DataField="NombreCliente" UniqueName="NombreCliente" Visible="true">
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="Id_Promotor" DataField="Id_Promotor" UniqueName="Id_Promotor" Visible="false">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn HeaderText="NombrePromotor" DataField="NombrePromotor" UniqueName="NombrePromotor" Visible="true">
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridCheckBoxColumn DataField="Aprobado" HeaderText="Apro." UniqueName="Aprobado" AllowSorting="true"   AllowFiltering="false">
                                                                                    <HeaderStyle Width="50px"/>
                                                                                </telerik:GridCheckBoxColumn>

                                                    <%--<telerik:GridBoundColumn HeaderText="Aprobado" DataField="Aprobado" UniqueName="Aprobado" Visible="true">
                                                        <HeaderStyle Width="50px" />
                                                    </telerik:GridBoundColumn>--%>

                                                    <telerik:GridBoundColumn HeaderText="Total" DataField="Total" UniqueName="Total" Visible="true" Aggregate="Sum" DataFormatString="{0:N}">
                                                        <HeaderStyle Width="60px" />
                                                    </telerik:GridBoundColumn>



                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label Text="Monto Restante:" runat="server"></asp:Label>
                                <asp:Label ID="lblDiferencia" Text="0.00" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
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
