<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmPresupuestoProducto.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmPresupuestoProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte Proyección de Cobranzas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>

        function ShowCreate(objPerfil) {
            window.radopen("frmCargaMasivaPresupuesto.aspx?objPerfil=" + objPerfil, "rwCarga");
            return false;
        }   

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

       function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramProyectado.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=  ramProyectado.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
       }

        function ShowCreateViewGestion(cliente, sectorista, proyectado, fecha) {
            window.radopen("frmHistoricoGestion.aspx?strCliente=" + cliente + "&strSectorista=" + sectorista + "&strProyectado=" + proyectado + "&strfecha=" + fecha, "rwVidaLey");
            return false;
        }

        function ShowCreateViewDeuda(cliente, sectorista) {
            window.radopen("frmDetalleVencido.aspx?strCliente=" + cliente + "&strSectorista=" + sectorista, "rwVidaLey2");
            return false;
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExpResumen") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpDetalle") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpPDFDetalle") >= 0)
                args.set_enableAjax(false);
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramProyectado" runat="server" >
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpProyectado" ControlID="pnlProyectado"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdDocVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocVenta" LoadingPanelID="ralpProyectado"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdDocVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlProyectado" LoadingPanelID="ralpProyectado"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpProyectado" runat="server">
    </telerik:RadAjaxLoadingPanel>


    <telerik:RadWindowManager ID="rwmProyectado" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="600px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
             <telerik:RadWindow ID="rwVidaLey2" runat="server" Width="400px" Height="450px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

           <telerik:RadWindow ID="rwCarga" runat="server" Width="700px" Height="230px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlProyectado" runat="server" Width="100%" Height="700px" ClientEvents-OnRequestStart="requestStart" >
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Presupuesto de Ventas" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">

            </div>
        </div>

        <div class="row"> 
             <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo:" CssClass="etiqueta"></asp:Label>
                </div>

                <div class="col-md-1">
                    <telerik:RadMonthYearPicker ID="rmyReporte0" runat="server" Width="100px" DateInput-ReadOnly="true" Enabled="true" >
                        <DateInput ID="DateInput1" runat="server" DateFormat="MM-yyyy"></DateInput>
                    </telerik:RadMonthYearPicker>
                </div>
                 <div class="col-md-1">
                    <telerik:RadMonthYearPicker ID="rmyReporte" runat="server" Width="100px" DateInput-ReadOnly="true" Enabled="true" >
                        <DateInput ID="DateInput3" runat="server" DateFormat="MM-yyyy"></DateInput>
                    </telerik:RadMonthYearPicker>
                </div>
                 <div class="col-md-1" >
                     <asp:Label ID="Label2" runat="server"  Width="90px" CssClass="etiqueta" Text="Zona:">
                     </asp:Label>
                 </div>

                 <div class="col-md-2">
                      <telerik:RadComboBox ID="cboZona" runat="server" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cboZona_SelectedIndexChanged" Enabled="true" >
                     </telerik:RadComboBox>
                 </div>

                 <div class="col-md-1">
                     <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Vendedor:" Width="100px"></asp:Label>
                 </div>

                 <div class="col-md-3">
                      <telerik:RadComboBox ID="cboVendedor" runat="server" AutoPostBack="true" Width="330px"  OnSelectedIndexChanged="cboVendedor_SelectedIndexChanged" >
                     </telerik:RadComboBox>
                 </div>
  
                 <div class="col-md-1">
                     <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="80px" >
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                     </telerik:RadButton>
                     <asp:HiddenField ID="lblGrilla" runat="server" />
                 </div>

                  <div class="col-md-1">
                    <telerik:RadButton ID="btnCargaMasiva" runat="server" Text="Carga Masiva" OnClick="btnCargaMasiva_Click">
                        <Icon PrimaryIconUrl="../../Images/Icons/btnCarMas.png"/>
                    </telerik:RadButton>
                  </div>
        
                  <div class="col-md-1">
                          <asp:ImageButton ID="btnExpDetalle" runat="server"  ImageUrl="~/Images/Icons/24_excel.png" OnClick="btnExpDetalle_Click"  AlternateText="ExcelML" ToolTip="Descargar Excel" />
                  </div>

                 <div class="col-md-1">
                     <asp:Label ID="lblSemillas" runat="server" CssClass="etiqueta" Text="Mostrar solo Semillas:" Width="200px"></asp:Label>
                 </div>

                 <div class="col-md-1">
                      <asp:CheckBox runat="server" ID="chkSemilla"/>
                 </div>
             </div>
        </div>

        <div class="row"  >
                            <div class="col-md-12" >

                                <telerik:RadGrid  Height="600px"
                                    ID="grdDocVenta" 
                                    runat="server"  
                                    AutoGenerateColumns="False"
                                    OnNeedDataSource="grdDocVenta_NeedDataSource" 
                                    OnItemCommand="grdDocVenta_ItemCommand"
                                    OnItemDataBound="grdDocVenta_ItemDataBound"
                                    ShowFooter="true"   >
    
                                    <MasterTableView
                                        DataKeyNames="Kardex,_80_20,Clase1"
                                        HorizontalAlign="NotSet" AutoGenerateColumns="False" >
                                        <Columns>

                                            <telerik:GridBoundColumn HeaderText="N°" DataField="Correlativo"  ReadOnly="true">
                                                <ItemStyle Width="30px" />
                                                <HeaderStyle Width="30px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Categoría" DataField="Clase1"  ReadOnly="true">
                                                <ItemStyle Width="90px" />
                                                <HeaderStyle Width="90px"/>
                                                   <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </telerik:GridBoundColumn>


                                           <telerik:GridBoundColumn HeaderText="Kardex" DataField="Kardex"  ReadOnly="true">
                                                <ItemStyle Width="50px" />
                                                <HeaderStyle Width="50px"/>
                                                   <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="SKU" DataField="SKU"  ReadOnly="true">
                                                <ItemStyle Width="90px" />
                                                <HeaderStyle Width="90px"/>
                                                   <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Nombre" DataField="SKU_Nombre"  ReadOnly="true">
                                                <ItemStyle Width="320px" />
                                                <HeaderStyle Width="370px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Q.Venta2015" DataField="CantidadReal1" DataFormatString="{0:#,##0.00}" Aggregate="Sum" HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Width="100px" Font-Bold="true"  BackColor="Cyan"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right"  />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Q.Venta2016" DataField="CantidadReal2" DataFormatString="{0:#,##0.00}" Aggregate="Sum" HeaderStyle-ForeColor="Red">
                                                <HeaderStyle Width="100px" Font-Bold="true" BackColor="GreenYellow" />
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Q.Presup2016" DataField="CantidadPronostico2" DataFormatString="{0:#,##0.00}" Aggregate="Sum" HeaderStyle-ForeColor="Red">
                                                <HeaderStyle Width="100px" Font-Bold="true" BackColor="GreenYellow"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right"  />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Avance(%)" DataField="cumplimiento2" DataFormatString="{0:F0} %" HeaderStyle-ForeColor="Red">
                                                <HeaderStyle Width="100px" Font-Bold="true" BackColor="GreenYellow" />
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>


                                            <telerik:GridBoundColumn HeaderText="Enero" DataField="Mes1" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right"  />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Febrero" DataField="Mes2" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                               <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Marzo" DataField="Mes3" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Abril" DataField="Mes4" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Mayo" DataField="Mes5" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Junio" DataField="Mes6" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Julio" DataField="Mes7" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                  <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Agosto" DataField="Mes8" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                  <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Septiembre" DataField="Mes9" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Octubre" DataField="Mes10" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Noviembre" DataField="Mes11" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Diciembre" DataField="Mes12" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                 <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                  <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Pronostico" DataField="TotalProyectado" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                        </Columns>
                                        
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="2" />
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
