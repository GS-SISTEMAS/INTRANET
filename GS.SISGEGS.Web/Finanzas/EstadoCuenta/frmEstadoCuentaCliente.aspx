<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmEstadoCuentaCliente.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.EstadoCuenta.frmEstadoCuentaCliente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Estado de cuenta</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function requestStart(sender, args) {
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

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEstadoCuenta" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdResumenCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdResumenCliente" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpEstadoCuenta" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwExportarPDF" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEstadoCuenta" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar estado de cuenta" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
               <div class="row">
                 <div class="col-md-12">
                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Fecha Emisión:" Width="98px"></asp:Label>
                        </td>

                        <td class="auto-style1">
                            <asp:Label ID="lblFinalEmision" runat="server" CssClass="etiqueta" Text="Hasta: " Width="45px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpFinalEmision" runat="server" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker></td>
                      <td class="auto-style1">
                            &nbsp;</td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td class="auto-style1">
                            <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 0px; left: 1px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                            <asp:ImageButton ID="btnExpDetalle" runat="server"  Height="30px" ImageUrl="~/Images/Icons/24_excel.png" OnClick="btnExpDetalle_Click" Width="30px" />
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                            <asp:ImageButton ID="btnExpPDFDetalle" runat="server"  Height="30px" ImageUrl="~/Images/Icons/24_pdf.png" OnClick="btnExpPDFDetalle_Click" Width="30px" />
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                        </td>
                        <td colspan="1">
                            &nbsp;</td>
                        <td colspan="1">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="3" >&nbsp; &nbsp;</td>
                        <td>
                            <asp:Label ID="lblPromedio" runat="server" CssClass="etiqueta" Text="Periodo promedio de cobranza:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtIdAgenda" Runat="server" Enabled="False" Width="90px">
                            </telerik:RadTextBox>
                        </td>
                        <td colspan="10">
                            <telerik:RadTextBox ID="txtAgendaNombre" Runat="server" Enabled="False" Width="420px">
                            </telerik:RadTextBox>
                        </td>
                        <td >
                            <telerik:RadTextBox ID="txtPromedio" Runat="server" Enabled="False" Width="100%" style="text-align: right">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                 <div style="height: 5px">
                 </div>
              </div>
             </div>

             <div class="row">
                 <div class="col-md-3">
               <table>
                   <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Resumen limite crediticio: " Width="150px"></asp:Label>
                           <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                        <td>
                            &nbsp;</td>
                         <td> 
                            
                             &nbsp;&nbsp;</td>
                        <td>
                         
                        </td>
                    </tr>
                </table>
               <telerik:RadGrid ID="grdResumenCliente" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="60px"  Width="90%"
                    OnNeedDataSource="grdResumenCliente_NeedDataSource" >

                   <MasterTableView TableLayout="Fixed" DataKeyNames="id_agenda"  >
                        <Columns>
                            <telerik:GridBoundColumn DataField="TotalCredito"  HeaderText="Deuda Total" HeaderStyle-Width="80px" AllowSorting="false" DataFormatString="{0:$ #,##0.00}">
                        
                                 <ItemStyle HorizontalAlign="Right" BackColor="#ffcc99"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="LineaCredito" HeaderText="LineaCredito" HeaderStyle-Width="80px" DataFormatString="{0:$ #,##0.00}"  
                               >
                                 <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="CreditoDisponible" HeaderText="CreditoDisponible" HeaderStyle-Width="100px"  DataFormatString="{0:$ #,##0.00}"
                                HeaderStyle-HorizontalAlign="Right" 
                           >
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                 <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                </telerik:RadGrid>
                </div>
                <div class="col-md-9">
                <table>
                   <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Resumen deuda x vencimiento: " Width="200px"></asp:Label>
                           </td>
                        <td>
                            &nbsp;</td>
                         <td> 
                            
                             &nbsp;&nbsp;</td>
                        <td>
                         
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="grdResumenVencimientos" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="60px"  Width="80%"
                    OnNeedDataSource="grdResumenVencimientos_NeedDataSource" >

                   <MasterTableView TableLayout="Fixed" DataKeyNames="id_agenda"  >
                        <Columns>
                            <telerik:GridBoundColumn DataField="DeudaTotal"  HeaderText="DeudaTotal" HeaderStyle-Width="100px"  DataFormatString="{0:$ #,##0.00}"  HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" BackColor="#ffcc99"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NoVencido"  HeaderText="No Vencido" HeaderStyle-Width="80px" AllowSorting="false" DataFormatString="{0:$ #,##0.00}">
                        
                                 <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencido01a30" HeaderText="Vencido 01a30" HeaderStyle-Width="80px" DataFormatString="{0:$ #,##0.00}"     > 
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencido31a60" HeaderText="Vencido 31a60" HeaderStyle-Width="100px"  DataFormatString="{0:$ #,##0.00}"  HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="Vencido61a120" HeaderText="Vencido 61a120" HeaderStyle-Width="100px"  DataFormatString="{0:$ #,##0.00}"  HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn DataField="Vencido121a360" HeaderText="Vencido 121a360" HeaderStyle-Width="100px"  DataFormatString="{0:$ #,##0.00}"  HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Vencido361amas" HeaderText="Vencido 361aMás" HeaderStyle-Width="100px"  DataFormatString="{0:$ #,##0.00}"  HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>


                        </Columns>
                    </MasterTableView>
                 <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                </telerik:RadGrid>
              </div>
             </div>

                 <div class="row">
                 <div class="col-md-12">
                 <div style="height: 5px">
                 </div>
               <table>
                    <tr>
                        <td>
                              <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Detalle deuda x documentos: " Width="180px"></asp:Label></td>
                        <td>
                               &nbsp;</td>
                         <td>
                             &nbsp;&nbsp;</td>
                        <td>
                         
                        </td>

                    </tr>
                </table>
                <telerik:RadGrid ID="grdEstadoCuenta" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="400px"  Width="100%"
                    OnNeedDataSource="grdEstadoCuenta_NeedDataSource" AllowSorting="True"  ShowFooter="true" >

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
                            <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="TipoDocumento" UniqueName="TipoDocumento" HeaderStyle-Width="130px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroDocumento" Display="true" HeaderText="NumDocumento" HeaderStyle-Width="140px" AllowSorting="false"
                                Aggregate="Count" FooterText="Total Documentos: ">
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
                            <telerik:GridBoundColumn DataField="DeudaSoles" HeaderText="DeudaSoles" HeaderStyle-Width="80px" 
                                DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false"
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeudaDolares" HeaderText="DeudaDolares" HeaderStyle-Width="80px" 
                             DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EstadoDoc" HeaderText="Estado Doc." HeaderStyle-Width="130px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Banco" HeaderText="Banco" HeaderStyle-Width="250px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NumeroUnico" HeaderText="NumeroUnico" HeaderStyle-Width="100px" AllowSorting="false">
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
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
