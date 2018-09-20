<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmProyectado.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.frmProyectado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte Proyección de Cobranzas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">

        function ShowCreate(objPerfil) {
            window.radopen("frmCargaMasiva.aspx?objPerfil=" + objPerfil, "rwCarga");
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

        function refreshGridBuscar(arg) {
            if (!arg) {
                $find("<%=ramProyectado.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=ramProyectado.ClientID %>").ajaxRequest("RebindAndNavigateBuscar(" + arg + ")");
            }
         }

         function refreshGridProyectado(arg) {
             $find("<%=ramProyectado.ClientID %>").ajaxRequest("RebindAndNavigateBuscar(" + arg + ")");
           }



        function ShowCreateViewGestion(cliente, sectorista, zona, fecha) {
            window.radopen("frmHistoricoGestion.aspx?strCliente=" + cliente + "&strSectorista=" + sectorista + "&strZona=" + zona + "&strfecha=" + fecha, "rwVidaLey");
            return false;
        }

        function ShowCreateViewEstadoCuenta(id_cliente,fechaInicial, ID_Vendedor, ID_Sectorista,ID_zona ) {
            window.radopen("frmEstadoCuenta.aspx?id_cliente=" + id_cliente + "&fechaInicial=" + fechaInicial + "&ID_Vendedor=" + ID_Vendedor + "&ID_Sectorista=" + ID_Sectorista + "&ID_zona=" + ID_zona, "rwEstadoCuenta");
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
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

        function buscarError() {
            alert('No se realizó la proyección del cliente para el periodo seleccionado.!!');
        }

        function buscar() {
            document.getElementById('ctl00_body_btnBuscar').click();
        }

        function buscarHistorico() {
            document.getElementById('ctl00_body_btnBuscar').click();
        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramProyectado" runat="server" OnAjaxRequest="ramProyectado_AjaxRequest" >
        <AjaxSettings>

              <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlProyectado" LoadingPanelID="ralpProyectado" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
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

    <telerik:RadAjaxLoadingPanel ID="ralpProyectado" runat="server" >
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmProyectado" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="720px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
             <telerik:RadWindow ID="rwVidaLey2" runat="server" Width="400px" Height="450px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

           <telerik:RadWindow ID="rwCarga" runat="server" Width="700px" Height="220px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

             <telerik:RadWindow ID="rwEstadoCuenta" runat="server" Width="1050px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlProyectado" runat="server" Width="100%" Height="600px" ClientEvents-OnRequestStart="requestStart"  >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Proyección de Cobranza" CssClass="titulo"></asp:Label>
            </div>
           
        </div>
        <div class="row">
             <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-1">
                    <telerik:RadMonthYearPicker ID="rmyReporte" runat="server" Width="130%"  >
                        <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                    </telerik:RadMonthYearPicker>
                </div>
                 <div class="col-md-3">
                     <telerik:RadComboBox ID="cboSectorista" runat="server" AutoPostBack="true" Width="100%"  Label="Sectorista:"
                          OnSelectedIndexChanged="cboSectorista_SelectedIndexChanged" >
                     </telerik:RadComboBox>
                      <asp:HiddenField ID="lblCodigoSectorista" runat="server" />
                 </div>
                 <div class="col-md-5">
                      <telerik:RadComboBox ID="cboZona" runat="server" AutoPostBack="true" Width="95%"  Label="Zona:"
                          OnSelectedIndexChanged="cboZonas_SelectedIndexChanged" >
                     </telerik:RadComboBox>
                 </div>
                 <div class="col-md-1">
                     <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar"  Width="130%">
                                <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                     </telerik:RadButton>
                     <asp:HiddenField ID="lblGrilla" runat="server" />
                 </div>

                <div class="col-md-1">
                </div>

             </div>
        </div>

 
        <div class="row"  >
                            <div class="col-md-12" >

                                <telerik:RadGrid  Height="400px"
                                    ID="grdDocVenta" 
                                    runat="server"  
                                    AutoGenerateColumns="False"
                                     OnNeedDataSource="grdDocVenta_NeedDataSource"  
                                    OnItemCommand="grdDocVenta_ItemCommand"
                                  
                                     >    

                                    <MasterTableView 
                                        DataKeyNames="id_Cliente,Id_Zona"
                                         HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                     >
                                      
                                        <Columns>

       

                                            <telerik:GridBoundColumn HeaderText="Nombre Cliente" DataField="ClienteNombre"  ReadOnly="true" 
                                                >
                                                <ItemStyle Width="350px" />
                                                <HeaderStyle Width="350px"/>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridTemplateColumn  HeaderText="EstadoCuenta" AllowFiltering="false" HeaderStyle-Width="90px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibDeudaEstado" runat="server" ImageUrl="~/Images/Icons/search-16.png" CommandArgument='<%# Eval("id_Cliente") %>' CommandName="EstadoCuenta" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px"/>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>


                                              <telerik:GridBoundColumn HeaderText="NoVencida" DataField="ImportePendienteNoVencido" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="90px" />
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                              <telerik:GridBoundColumn HeaderText="Vencida" DataField="ImportePendienteVencido" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="90px" />
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>


                                             <telerik:GridBoundColumn HeaderText="Deuda Total" DataField="ImportePendiente" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="90px" />
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="CuotaS1" DataField="MontoS1" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="75px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="CuotaS2" DataField="MontoS2" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="75px"/>
                                               <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="CuotaS3" DataField="MontoS3" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="75px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="CuotaS4" DataField="MontoS4" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="75px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Proyectado" DataField="Proyectado" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="75px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn HeaderText="Proyectado01a30" DataField="Proyectado01a30" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="120px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                                  <telerik:GridBoundColumn HeaderText="Proyectado31a60" DataField="Proyectado31a60" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="120px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                                 <telerik:GridBoundColumn HeaderText="Proyectado61a120" DataField="Proyectado61a120" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="120px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                                   <telerik:GridBoundColumn HeaderText="Proyectado121a360" DataField="Proyectado121a360" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="120px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                                     <telerik:GridBoundColumn HeaderText="Proyectado361aMas" DataField="Proyectado361aMas" DataFormatString="${0:#,##0.00}" Aggregate="Sum">
                                                <HeaderStyle Width="120px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>



                                            <telerik:GridBoundColumn HeaderText="Estado" DataField="estado" >
                                                <HeaderStyle Width="100px"/>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="Observación" DataField="obsercacion">
                                                <HeaderStyle Width="180px"/>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Gestión" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibGestion" runat="server" ImageUrl="~/Images/Icons/notepad-16.png" CommandArgument='<%# Eval("id_Cliente") %>' CommandName="Gestion" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="70px"/>
                                            </telerik:GridTemplateColumn>

                                        </Columns>
                                        
                                    </MasterTableView>
                                   <ClientSettings >
                                        <Scrolling AllowScroll="true"  UseStaticHeaders="true"/>
                                        <Selecting AllowRowSelect="true" />
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
