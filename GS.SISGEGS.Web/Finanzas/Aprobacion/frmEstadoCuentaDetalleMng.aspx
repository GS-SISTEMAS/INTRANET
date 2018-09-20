<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpL.Master" AutoEventWireup="true" CodeBehind="frmEstadoCuentaDetalleMng.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Aprobacion.frmEstadoCuentaDetalleMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        function CancelEdit() {
            GetRadWindow().close();
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcelMng") >= 0)
                args.set_enableAjax(false);

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="col-md-12">
              <div class="row">
                  <div class="col-md-1">
                        <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Empresa: " Width="73px"></asp:Label>
                  </div>
                   <div class="col-md-3">
                        <telerik:RadComboBox ID="cboEmpresa" runat="server" OnSelectedIndexChanged="cboEmpresa_OnSelectedIndexChanged" AutoPostBack="True"></telerik:RadComboBox>
                        
                  </div>
                   <div class="col-md-8">
                  </div>
                </div>
                 <div class="row">
                  <div class="col-md-1">
                      <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                  </div>
                  <div class="col-md-3">
                              <telerik:RadTextBox  ID="acbCliente" runat="server" Width="300px"> 
                            </telerik:RadTextBox>
                  </div>
                  <div class="col-md-8">
                  </div>
              </div>
                 <div class="row">
                  <div class="col-md-12">
                       <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Resumen: " Width="116px"></asp:Label>
                  </div>
              </div>

          
               <div class="row">
                <div class="col-md-12">

                    <div id="loading" style="border: solid 1px Red; width: 100px; height: 50px; display: none;
      text-align: center; margin: auto;">
      Custom<br />
      loading....
  </div>
                    <telerik:RadGrid ID="grdResumenClienteMng" runat="server" AllowMultiRowSelection="false" AutoGenerateColumns="False" 
                Height="140px"  Width="100%" 
                 AllowSorting="True" ShowFooter="true" >

                 <MasterTableView TableLayout="Fixed" DataKeyNames="id_agenda"  >
                        <Columns>
                            <telerik:GridBoundColumn DataField="id_agenda" HeaderText="Código"  HeaderStyle-Width="100px" AllowSorting="false"   >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="AgendaNombre" HeaderText="Cliente" HeaderStyle-Width="250px" AllowSorting="false"
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
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" ></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                </telerik:RadGrid>
               </div>
               </div>
                 <div style="height: 5px">
                 </div>

               <div class="row">
                  <div class="col-md-12">
                       <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Detalle: " Width="116px"></asp:Label>
                  </div>
               </div>

             <div class="row">
               <div class="col-md-12">
                  <telerik:RadGrid ID="grdEstadoCuentaMng" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="300px"  Width="100%"
                    AllowSorting="True"  ShowFooter="true" OnNeedDataSource="grdEstadoCuentaMng_OnNeedDataSource"
                    
                     >
  
                    <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                    <MasterTableView TableLayout="Fixed" DataKeyNames="TipoDocumento"
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >
                         <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                   <telerik:GridGroupByField FieldAlias="CODIGO" FieldName="id_agenda" />
                                    <telerik:GridGroupByField FieldAlias="CLIENTE" FieldName="ClienteNombre" />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="id_agenda"  />
                                    <telerik:GridGroupByField FieldName="ClienteNombre"/>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            
                            <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="TipoDocumento" UniqueName="TipoDocumento" HeaderStyle-Width="110px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroDocumento" Display="true" HeaderText="NumDocumento" HeaderStyle-Width="140px" AllowSorting="false"
                                Aggregate="Count" FooterText="Total Documentos: ">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="numeroLetra" Display="true" HeaderText="NumLetra" HeaderStyle-Width="140px" AllowSorting="false">
                                
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Referencia"  HeaderText="Referencia" HeaderStyle-Width="150px" AllowSorting="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha Emisión" HeaderStyle-Width="110px" DataFormatString="{0:dd/MM/yyyy}"  >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" HeaderStyle-Width="145px" DataFormatString="{0:dd/MM/yyyy}"   >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="DiasMora" HeaderText="DiasMora" HeaderStyle-Width="60px" >
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
   
                            <telerik:GridBoundColumn DataField="monedasigno" HeaderText="Mon" HeaderStyle-Width="45px" AllowSorting="false"
                               >
                            </telerik:GridBoundColumn>
                  
                            <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" HeaderStyle-Width="65px" 
                                DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false"  FooterText="Total:"  FooterStyle-HorizontalAlign="Right">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeudaSoles" HeaderText="Pendiente(S/)" HeaderStyle-Width="80px" 
                                DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false"
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeudaDolares" HeaderText="Pendiente($)" HeaderStyle-Width="80px" 
                             DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EstadoDoc" HeaderText="Estado Doc." HeaderStyle-Width="130px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Banco" HeaderText="Banco" HeaderStyle-Width="220px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NumeroUnico" HeaderText="NumeroUnico" HeaderStyle-Width="100px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_Financiamiento" HeaderText="ID_Financiamiento" HeaderStyle-Width="100px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>

                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>
              </div>
             </div>

              </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
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
