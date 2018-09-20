<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="GS.SISGEGS.Web.Commercial.Consulta.Stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Stocks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramStock" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpStock" ControlID="rapStock"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpStock" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapStock" runat="server" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar Stocks" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblAlmacen" runat="server" Text="Almacen" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-10">
                        <telerik:RadComboBox ID="cboAlmacen" runat="server" Width="100%"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="txtDescripcion" runat="server" MaxLength="100" Width="100%"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdStock" runat="server" Width="100%" Height="500px"
                    AllowSorting="false" AllowMultiRowSelection="false" ShowGroupPanel="false"
                    AutoGenerateColumns="False">
                    <MasterTableView>
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="Descripcion" FieldName="Descripcion"></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Descripcion"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="No_Almacen" DataField="No_Almacen">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripcion" DataField="Descripcion" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Stock (Físico)" DataFormatString="{0:F0}" DataField="Stock">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Stock (Disponible)" DataFormatString="{0:F0}" DataField="StockDisponible">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="U.M." DataField="UnidadControl">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Categoría" DataField="FamiliaKardexNiv02_Nombre">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                        <Selecting AllowRowSelect="True"></Selecting>
                        <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                            ResizeGridOnColumnResize="False"></Resizing>
                    </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                </telerik:RadGrid>
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
