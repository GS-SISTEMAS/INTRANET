<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpXLL.Master" AutoEventWireup="true" CodeBehind="frmEstadoCuenta.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.Proyeccion.frmEstadoCuenta" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
                args.set_enableAjax(false);


            if (args.get_eventTarget().indexOf("btnPDFDetalle") >= 0)
                args.set_enableAjax(false);

            if (args.get_eventTarget().indexOf("btnExcelDetalle") >= 0)
                args.set_enableAjax(false);

            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpResumen") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpDetalle") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpPDFDetalle") >= 0)
                args.set_enableAjax(false);
        }

        function ShowInsertForm(variable) {
            window.radopen("frmExportarPDFPopup.aspx?strFileNombre=" + variable, "rwExportarPDF");
            return false;
        }

        function AbrirNuevoVentana(variable)
        {
            strCodigoSobre = codSobre;
            strCodigoPais = codPais;

            var surl = 'frmExportarPDFPopup.aspx?strFileNombre=' + variable
            window.open(surl, "", "left=0px,top=0px,height=730x,width=1160px,status=no,toolbar=no,menubar=no,location=no,resizable=yes");

        }

        function CancelEdit() {
            GetRadWindow().close();
        }


        function ShowCreateViewProyectar(objDocumento, strfecha) {
            window.radopen("frmSemanal.aspx?objDocumento=" + objDocumento + "&strfecha=" + strfecha, "rwProyectar");
            return false;
        }

        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }

        function refreshGridProyectado(arg) {
            $find("<%=ramEstadoCuenta.ClientID %>").ajaxRequest("RebindAndNavigateProyectado(" + arg + ")");

                }


    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEstadoCuenta" runat="server" OnAjaxRequest="ramEstadoCuenta_AjaxRequest" >
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="body">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="btnBuscarResumenCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdEstadoCuentaCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuentaCliente" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpEstadoCuenta" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="100%" Height="100%" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwExportarPDF" runat="server" Width="100%" Height="100%" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwProyectar" runat="server" Width="400px" Height="450px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>


        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEstadoCuenta" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
         <div class="col-md-12">
                <div class="col-md-9">
                    <asp:Label ID="lblTitulo" runat="server" Text="Deuda Cliente" CssClass="titulo"></asp:Label>
                </div>
                <div class="col-md-1">
                          
                </div> 
                <div class="col-md-1">
                          
                </div> 
                <div class="col-md-1">
                      <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" Visible="true" ToolTip="CERRAR" Width="140%"> 
                        <Icon PrimaryIconUrl="~/Images/Icons/delete-16.png" />
                    </telerik:RadButton>
                </div>
         </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                     
                    <div class="col-md-4">
                     <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Fecha:"></asp:Label>
                    </div>
                     <div class="col-md-8">
                      <telerik:RadDatePicker ID="dpFinalEmision" runat="server" DateInput-ReadOnly="true" Width="150%" Enabled="false">
                                <DateInput runat="server" DateFormat="MM-yyyy">
                                </DateInput>
                       </telerik:RadDatePicker>
                    </div>
                  
                </div>

                <div class="col-md-5">
                           <telerik:RadAutoCompleteBox Width="100%" ID="acbCliente" runat="server" AllowCustomEntry="true"   Label="Cliente:" Enabled="false"
                                  EmptyMessage="Selec. cliente" InputType="Text" 
                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" >
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmEstadoCuenta.aspx" />
                            </telerik:RadAutoCompleteBox>
                </div>

                <div class="col-md-5">
                         
                </div>
               
            </div>
        </div> 
       <div class="row">
            <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="rmpRepEstadoCuenta" SelectedIndex="0" CssClass="col-md-12">
                <Tabs>
                    <telerik:RadTab Text="Detalle" Selected="True"></telerik:RadTab>
                    <telerik:RadTab Text="Garantías "></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
           <telerik:RadMultiPage runat="server" ID="rmpRepEstadoCuenta" SelectedIndex="0" Height="100%" CssClass="col-md-12">
                <telerik:RadPageView runat="server" ID="pageDetalle" CssClass="col-md-12" Height="100%"> 
                          <div class="row">
                                       <div class="col-md-12">
                                           <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Resumen: " ></asp:Label>
                                       </div> 
                          </div> 
                          <div class="row">
                                       <div class="col-md-12">
                                           <telerik:RadGrid ID="grdResumenCliente" runat="server" 
                                               AllowMultiRowSelection="false" AutoGenerateColumns="False" 
                                            Height="80px"  Width="100%" 
                                          OnNeedDataSource="grdResumenCliente_NeedDataSource" AllowSorting="True" >

                                         <MasterTableView TableLayout="Fixed" DataKeyNames="id_agenda"  >
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="id_agenda" HeaderText="Código"  HeaderStyle-Width="100px" AllowSorting="false"  Display="false"  >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="AgendaNombre" HeaderText="Cliente" HeaderStyle-Width="280px" AllowSorting="false" Display="false"
                                                        Aggregate="Count" FooterText="Total Cliente: " >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="EstadoDes" HeaderText="Estado"  HeaderStyle-Width="80px" AllowSorting="false"   >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="DiasCredito" HeaderText="Díascrédito "  HeaderStyle-Width="70px" AllowSorting="false"   >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="BloqueoLineaCredito" HeaderText="DíasGracia"  HeaderStyle-Width="70px" AllowSorting="false" Visible="false"   >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="FechaVCMTLinea" HeaderText="VcmtLínea"  HeaderStyle-Width="70px" AllowSorting="false" DataFormatString="{0:dd/MM/yyyy}"   >
                                                    </telerik:GridBoundColumn>

                                                     <telerik:GridBoundColumn DataField="AprobadoDes" HeaderText="LíneaAprobada"  HeaderStyle-Width="90px" AllowSorting="false"   >
                                                           <ItemStyle HorizontalAlign="Center"/> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="LineaCredito" HeaderText="LineaCredito" HeaderStyle-Width="80px" 
                                                       Aggregate="Sum"   DataFormatString="{0:#,##0.00}"  FooterStyle-HorizontalAlign="Right"  >
                                                         <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="DeudaTotal"  HeaderText="DeudaTotal" HeaderStyle-Width="70px" AllowSorting="false" 
                                                      Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"   DataFormatString="{0:#,##0.00}"  >
                                                         <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="CreditoDisponible" HeaderText="CreditoDisponible" HeaderStyle-Width="110px"   HeaderStyle-HorizontalAlign="Right"   
                                                        Aggregate="Sum"   FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00}" >
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>


                                                   <telerik:GridBoundColumn DataField="DeudaVencida"  HeaderText="DeudaVencida" HeaderStyle-Width="90px" AllowSorting="false" 
                                                     Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  DataFormatString="{0:#,##0.00}"  >
                                                         <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="PorVencer30"  HeaderText="PorVencer30" HeaderStyle-Width="80px" AllowSorting="false" DataFormatString="{0:#,##0.00}"
                                                        Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                                         <ItemStyle HorizontalAlign="Right" /> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="NoVencido"  HeaderText="NoVencido" HeaderStyle-Width="80px" AllowSorting="false" DataFormatString="{0:#,##0.00}"
                                                        Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                                         <ItemStyle HorizontalAlign="Right" /> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="Vencido01a30" HeaderText="Vencido01a30" HeaderStyle-Width="90px"   DataFormatString="{0:#,##0.00}"  
                                                        Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  > 
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="Vencido31a60" HeaderText="Vencido31a60" HeaderStyle-Width="100px"   DataFormatString="{0:#,##0.00}"
                                                         Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="Vencido61a120" HeaderText="Vencido61a120" HeaderStyle-Width="100px"  DataFormatString="{0:#,##0.00}" 
                                                         Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                      <telerik:GridBoundColumn DataField="Vencido121a360" HeaderText="Vencido121a360" HeaderStyle-Width="100px" 
                                                      Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  DataFormatString="{0:#,##0.00}"  HeaderStyle-HorizontalAlign="Right">
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="Vencido361amas" HeaderText="Vencido361aMás" HeaderStyle-Width="100px"  
                                                        Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>

                                                </Columns>
                                            </MasterTableView>
                                         <ClientSettings>
                                            <Selecting AllowRowSelect="true"/>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" ></Scrolling>
                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                        </ClientSettings>
                                        </telerik:RadGrid>
                                       </div> 
                          </div> 
                          <div class="row">
                                       <div class="col-md-12">
                                             <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Detalle: " ></asp:Label>
                                       </div> 
                          </div> 
                          <div class="row">
                                    <div class="col-md-12">
                                        <telerik:RadGrid ID="grdEstadoCuenta" runat="server" AllowMultiRowSelection="false"
                                            AutoGenerateColumns="False" Height="210px"  Width="100%"
                                            OnNeedDataSource="grdEstadoCuenta_NeedDataSource" 
                                            OnItemCommand="grdEstadoCuenta_ItemCommand"
                                            AllowSorting="True"  ShowFooter="true"

                                             >
  
                                            <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                            <MasterTableView TableLayout="Fixed" DataKeyNames="TipoDocumento"
                                                AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >
                                              
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Origen" Display="false" HeaderText="Origen" HeaderStyle-Width="110px" AllowSorting="false"    >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="OrigenOp" Display="false" HeaderText="OrigenOp" HeaderStyle-Width="110px" AllowSorting="false"    >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridTemplateColumn HeaderText="Pronosticar" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnProyectar" runat="server" ImageUrl="~/Images/Icons/notepad-16.png" CommandArgument='<%# Eval("OrigenOp") %>' CommandName="Proyectar" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"/>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="TipoDocumento" UniqueName="TipoDocumento" HeaderStyle-Width="150px" 
                                                        AllowSorting="false"  Aggregate="Count" FooterText="Total Documentos: "  >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="ID_Agenda" Display="false" HeaderText="ID_Agenda" HeaderStyle-Width="110px" AllowSorting="false"    >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="NroDocumento" Display="true" HeaderText="NumDocumento" HeaderStyle-Width="110px" AllowSorting="false"
                                                       >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="Fecha" Display="false" HeaderText="FechaEmisión" HeaderStyle-Width="90px" DataFormatString="{0:dd/MM/yyyy}"  >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="FechaVencimiento" HeaderStyle-Width="110px" DataFormatString="{0:dd/MM/yyyy}"   >
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="DiasMora" HeaderText="DiasMora" HeaderStyle-Width="50px" >
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>
   
                                                    <telerik:GridBoundColumn DataField="monedasigno" HeaderText="Mon" HeaderStyle-Width="45px" AllowSorting="false"  >
                                                    </telerik:GridBoundColumn>
                  
                                                    <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" HeaderStyle-Width="65px" 
                                                        DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false"  FooterText="Total:"  FooterStyle-HorizontalAlign="Right">
                                                        <ItemStyle  HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DeudaSoles" HeaderText="Pendiente(S/)" HeaderStyle-Width="90px" 
                                                        DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false"
                                                        Aggregate="Sum"  FooterStyle-HorizontalAlign="Right">
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DeudaDolares" HeaderText="Pendiente($)" HeaderStyle-Width="90px" 
                                                     DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                                        Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>
                                                    
                                                       <telerik:GridBoundColumn DataField="Proyectado" HeaderText="Pronostico($)" HeaderStyle-Width="90px" 
                                                     DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                                        Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                                        <ItemStyle HorizontalAlign="Right"/> 
                                                    </telerik:GridBoundColumn>
                                                
                                                </Columns>
                                            </MasterTableView>

                                        <ClientSettings>
                                              <Selecting AllowRowSelect="true"/>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                        </ClientSettings>
                                          
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                </telerik:RadPageView>
                 <telerik:RadPageView runat="server" ID="pageGarantia" CssClass="col-md-12" Height="100%">
                               <div class="row">
                                       <div class="col-md-12">
                                             <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Garantías: " ></asp:Label>
                                       </div> 
                                 </div> 
                                 <div class="row">
                                  <div class="col-md-12">
                                        <telerik:RadGrid ID="grdGarantia" runat="server" Width="100%" Height="100px"
				                      	AllowSorting="false" AllowMultiRowSelection="false" ShowGroupPanel="false"
                    		            AutoGenerateColumns="False" visible="false">
                                        <MasterTableView TableLayout="Fixed" DataKeyNames="ID_Agenda"
                                            AllowMultiColumnSorting="true"  ShowGroupFooter="true">
                                            <Columns>

                                                <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Tipo Garantia" 
                                                    HeaderStyle-Width="170px" AllowSorting="false"  >
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="Fecha de Venc." 
                                                    HeaderStyle-Width="100px" AllowSorting="false"   DataFormatString="{0:dd/MM/yyyy}" >
                                                </telerik:GridBoundColumn>
                                                <telerik:GridHyperLinkColumn DataTextField="Valor" HeaderText="Valor" HeaderStyle-Width="100px"  
                                                    DataTextFormatString="{0:$ #,##0.00}"  HeaderStyle-HorizontalAlign="Left"  AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                                  <ItemStyle  HorizontalAlign="Left"/> 
                                                </telerik:GridHyperLinkColumn>

                                            </Columns>
                                           </MasterTableView>
                                           <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                                <Selecting AllowRowSelect="True"></Selecting>
                                                <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                    ResizeGridOnColumnResize="False"></Resizing>
                                           </ClientSettings>

                                        </telerik:RadGrid>
                                  </div>
                                </div>
                </telerik:RadPageView>
           </telerik:RadMultiPage>
       </div> 

      
       
    </telerik:RadAjaxPanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
             <asp:Label ID="lblMensajeResumenCliente" runat="server"></asp:Label>
             <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lblDate2" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
