<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOcImportacion.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.OC.frmOcImportacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Creacion de Parciales OC
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramOC" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOC" LoadingPanelID="ralpOC"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOC">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOC" LoadingPanelID="ralpOC" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpOC" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rwmOC" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwOC" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <%--<telerik:RadWindow ID="rwDocumento" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>--%>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlOC" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="8">
                            <asp:Label ID="lblTitulo" runat="server" Text="Modulo de Gestion de Parciales" CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>

                <telerik:LayoutRow Height="95%">
                    <Content>
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                        EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaInicio" runat="server" Text="Fec.Inicial" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%" DateInput-ReadOnly="true">
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fec.Final" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%" DateInput-ReadOnly="true">
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
                                                ID="grdOC" 
                                                runat="server" 
                                                AllowFilteringByColumn="False" ShowFooter="False" 
                                                AllowSorting="True" Width="100%" 
                                                AutoGenerateColumns="False" Height="100%" 
                                                OnNeedDataSource="grdOC_NeedDataSource"
                                                OnItemCommand="grdOC_ItemCommand" 
                                                    >

                                                <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                <MasterTableView ShowFooter="False" Width="2060px">


                                                    <Columns>
                                                       
                                                        <telerik:GridTemplateColumn HeaderText="Parcial" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibEditar" runat="server" CommandArgument='<%# Eval("Op") %>' CommandName="CrearParcial"
                                                                    ImageUrl="~/Images/Icons/sign-add-16.png" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Eliminar" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibEliminar" runat="server" CommandArgument='<%# Eval("Op") %>' CommandName="EliminarParcial"
                                                                    ImageUrl="~/Images/Icons/delete-16.png" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn UniqueName="Op" DataField="Op" HeaderText="Op" AllowFiltering="false">
                                                            <HeaderStyle Width="30px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="NoRegistro" DataField="NoRegistro" HeaderText="NoRegistro" AllowFiltering="false">
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="ID_Agenda" DataField="ID_Agenda" HeaderText="ID_Agenda" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="AgendaNombre" DataField="AgendaNombre" HeaderText="AgendaNombre" AllowFiltering="false">
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridBoundColumn>

                                                        <%--<telerik:GridBoundColumn UniqueName="TieneParciales" DataField="TieneParcial" HeaderText="Tiene Parciales" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>--%>

                                                        <telerik:GridCheckBoxColumn DataField="TieneParcial" HeaderText="TieneParcial"   ToolTip="OC Tiene Parcial" UniqueName="TieneParcial" AllowSorting="true"   AllowFiltering="false">
                                                            <HeaderStyle Width="50px"/>
                                                        </telerik:GridCheckBoxColumn>

                                                    

                                                        <telerik:GridBoundColumn UniqueName="FechaOrden" DataField="FechaOrden" HeaderText="FechaOrden" AllowFiltering="false"
                                                            HeaderStyle-Width="70px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                       <telerik:GridBoundColumn UniqueName="FechaEntrega" DataField="FechaEntrega" HeaderText="FechaEntrega" AllowFiltering="false"
                                                            HeaderStyle-Width="70px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        

                                                        <telerik:GridBoundColumn UniqueName="Neto" DataField="Neto" HeaderText="Neto" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                       
                                                        <telerik:GridBoundColumn UniqueName="SubTotal" DataField="SubTotal" HeaderText="SubTotal" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Impuestos" DataField="Impuestos" HeaderText="Impuestos" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Total" DataField="Total" HeaderText="Total" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Observaciones" DataField="Observaciones" HeaderText="Observaciones" AllowFiltering="false">
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridBoundColumn>
                   






                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Scrolling UseStaticHeaders="True" AllowScroll="true"></Scrolling>
                                                    <Selecting AllowRowSelect="True"></Selecting>
                                                    <Resizing AllowRowResize="True" EnableRealTimeResize="True"></Resizing>
                                                </ClientSettings>
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

