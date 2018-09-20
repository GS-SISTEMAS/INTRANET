<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmProyectadoComparativoMarca.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmProyectadoComparativoMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
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
    <telerik:RadAjaxPanel ID="pnlGeneralContratos" runat="server" Width="100%" Height="100%"  ClientEvents-OnRequestStart="requestStart">
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Seguimiento del pronóstico vs presupuesto año anterior" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">

            </div>
        </div>

        

        <div class="row">

            <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label ID="lblZona" runat="server" Text="Zona" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                        <telerik:RadComboBox ID="cboZona" runat="server" Width="100%" Enabled="true" >
                        </telerik:RadComboBox>
                </div>
            </div>
            <div class="col-md-12">
                 <div class="col-md-1">
                    <asp:Label runat="server" ID="lblPeriodo" Text="Periodo Inicio" CssClass="etiqueta"></asp:Label>
                </div>
                

                <div class="col-md-1">
                    <telerik:RadMonthYearPicker ID="dpPeriodoInicio" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%">
                                                    </telerik:RadMonthYearPicker>
                </div>

                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblPeriodoFinal" Text="Periodo Final" CssClass="etiqueta"></asp:Label>
                </div>
                

                <div class="col-md-1">
                    <telerik:RadMonthYearPicker ID="dpPeriodoFinal" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%">
                                                    </telerik:RadMonthYearPicker>
                </div>
                
                <div class ="col-md-1">
                    <telerik:LayoutColumn Span="6">
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" >
                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                        </telerik:RadButton>
                    </telerik:LayoutColumn>
                </div>
                <div class ="col-md-1">
                    <telerik:LayoutColumn Span="6">
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" AlternateText="ExcelML" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                    </telerik:LayoutColumn>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdComparativo" runat="server" AutoGenerateColumns="false" Height="480px" 
                    Width="100%" OnNeedDataSource="grdComparativo_NeedDataSource"
                    ShowFooter="true"
                    >
                    <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                    
                    <MasterTableView TableLayout="Fixed" DataKeyNames="Kardex"
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >

                                            <Columns>
                                                <telerik:GridBoundColumn DataField="FamKardexNiv04" UniqueName="FamKardexNiv04" HeaderText="Fam. Kardex Niv.04">
                                                    <HeaderStyle Width="100" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Marca" UniqueName="Marca" HeaderText="Marca">
                                                    <HeaderStyle Width="150" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Categoria" UniqueName="Categoria" HeaderText="Categoria">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Vigencia" UniqueName="Vigencia" HeaderText="Vigencia">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Kardex" UniqueName="Kardex" HeaderText="Kardex">
                                                    <HeaderStyle Width="40" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SKU_Nombre" UniqueName="SKU_Nombre" HeaderText="Descripción">
                                                    <HeaderStyle Width="250" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="CantidadPresupuestada" UniqueName="CantidadPresupuestada"  Aggregate="Sum"
                                                    HeaderText="Cantidad Presupuestada K/L" DataFormatString="{0:##,###0.##}">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CantidadPronosticada" UniqueName="CantidadPronosticada"  Aggregate="Sum"
                                                    HeaderText="Cantidad Pronosticada K/L" DataFormatString="{0:##,###0.##}">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CantidadAnterior" UniqueName="CantidadAnterior"  Aggregate="Sum"
                                                    HeaderText="Cantidad Año Anterior K/L" DataFormatString="{0:##,###0.##}">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Cumplimiento1" UniqueName="Cumplimiento1"  Aggregate="Avg"
                                                    HeaderText="% Cumplimiento (Pronostico / Año Anterior)"  DataFormatString="{0:F0} %" >
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cumplimiento2" UniqueName="Cumplimiento2"   Aggregate="Avg"
                                                    HeaderText="% Cumplimiento  (Pronostico / Presupuestada)"  DataFormatString="{0:F0} %">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>
                                                
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                            <Selecting AllowRowSelect="true"/>
                                        </ClientSettings>
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
