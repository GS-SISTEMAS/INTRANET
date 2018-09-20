<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmConsultaDocMaximo.aspx.cs" Inherits="GS.SISGEGS.Web.Sistemas.Reportes.frmConsultaDocMaximo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de Documentos Maximo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramMaximo" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMaximo" LoadingPanelID="ralpMaximo"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdMaximo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMaximo" LoadingPanelID="ralpMaximo" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpMaximo" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rwmMaximo" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwMaximo" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <%--<telerik:RadWindow ID="rwDocumento" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>--%>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlMaximo" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="8">
                            <asp:Label ID="lblTitulo" runat="server" Text="Gestion de Errores Maximo - Facturas" CssClass="titulo"></asp:Label>
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
                                                <asp:Label ID="Label1" runat="server" Text="Procesado" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <asp:CheckBox ID="chkprocesados" runat="server" Checked="true" CssClass="form-check-input" />
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
                                                ID="grdMaximo" 
                                                runat="server" 
                                                AllowFilteringByColumn="True" ShowFooter="False" 
                                                AllowSorting="True" Width="100%" 
                                                AutoGenerateColumns="false" Height="100%" 
                                                    >

                                                <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                <MasterTableView ShowFooter="False" Width="2060px">


                                                    <Columns>
                                                       

                                                        <telerik:GridBoundColumn UniqueName="Empresa" DataField="Empresa" HeaderText="Empresa" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="NroFacturaMaximo" DataField="NroFacturaMaximo" HeaderText="Factura Maximo" AllowFiltering="false">
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="OCMaximo" DataField="OCMaximo" HeaderText="OCMaximo" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="OCGenesys" DataField="OCGenesys" HeaderText="OCGenesys" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Agenda" DataField="Agenda" HeaderText="Agenda" AllowFiltering="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="FechaProceso" DataField="FechaProceso" HeaderText="FechaProceso" AllowFiltering="false"
                                                            HeaderStyle-Width="70px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridCheckBoxColumn HeaderText="Estado" UniqueName="Estado" DataField="Estado" ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" AllowFiltering="false">
                                                            <HeaderStyle Width="40px"
                                                                />
                                                        </telerik:GridCheckBoxColumn>

                                                        <telerik:GridBoundColumn UniqueName="NroFacturaProveedor" DataField="NroFacturaProveedor" HeaderText="Fac. Proveedor" AllowFiltering="false">
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="FechaFactura" DataField="FechaFactura" HeaderText="FechaFactura" AllowFiltering="false"
                                                            HeaderStyle-Width="70px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="FechaVencimiento" DataField="FechaVencimiento" HeaderText="FechaVencimiento" AllowFiltering="false"
                                                            HeaderStyle-Width="70px" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="IGV" DataField="IGV" HeaderText="IGV" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="Total" DataField="Total" HeaderText="Total" AllowFiltering="false">
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="MensajeError" DataField="MensajeError" HeaderText="MensajeError" AllowFiltering="false">
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn UniqueName="FacturaGenesys" DataField="FacturaGenesys" HeaderText="FacturaGenesys" AllowFiltering="false">
                                                            <HeaderStyle Width="70px" />
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
