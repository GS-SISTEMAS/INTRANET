<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmNumerosUnicos.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Informacion.frmNumerosUnicosMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Números Únicos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>

        function ShowCreate(Periodo) {
            window.radopen("frmCargaMasiva.aspx?Periodo=" + Periodo, "rwCarga");
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
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("ibEditar") >= 0)
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
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Pronóstico de Ventas" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">

            </div>
        </div>

         <div class="row">
            <div class="col-md-12">
                    <div class="col-md-1">
                   <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-3">
                         <telerik:RadComboBox ID="cboEmpresa" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa" Width="85%" ></telerik:RadComboBox>
                    </div>
                    <div class="col-md-8">
                       
                    </div>
           </div>
         </div>


          <div class="row"> 
             <div class="col-md-12">

                <div class="col-md-1">
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo:" CssClass="etiqueta"></asp:Label>
                </div>

                <div class="col-md-3">
                    <telerik:RadMonthYearPicker ID="rmyReporte0" runat="server" Width="85%"  DateInput-ReadOnly="true"  Enabled="true" >
                        <DateInput ID="DateInput1" runat="server" DateFormat="MM-yyyy"></DateInput>
                    </telerik:RadMonthYearPicker>
                </div>

                  <div class="col-md-8">
                  </div>

                </div>
             </div>
         <div class="row"> 
             <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label ID="Label1" runat="server" Text="Usuario :" CssClass="etiqueta"></asp:Label>
                </div>
                 <div class="col-md-3">
                        <telerik:RadTextBox ID="txtBuscar" runat="server" Width="85%" EmptyMessage="Buscar DNI/Nombre/Login"></telerik:RadTextBox>
                 </div>

               <div class="col-md-1">
                     <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100%" >
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                     </telerik:RadButton>
                 
                 </div>

                    <div class="col-md-1">

                        <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" Width="100%">
                                                    <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                                                </telerik:RadButton>

                  </div>


                  <div class="col-md-1">
                    <telerik:RadButton ID="btnCargaMasiva" runat="server" Text="Cargar" OnClick="btnCargaMasiva_Click" Width="100%" >
                        <Icon PrimaryIconUrl="~/Images/Icons/btnCarMas.png"/>
                    </telerik:RadButton>
                  </div>
 
                  <div class="col-md-5">
                  </div>


             </div>
        </div>

        <div class="row"  >
                            <div class="col-md-12" >

                                <telerik:RadGrid  Height="450px"
                                    ID="grdDocVenta" 
                                    runat="server"  
                                    AutoGenerateColumns="False"
                            
                                    OnItemCommand="grdDocVenta_ItemCommand"
                                 
                                    ShowFooter="true"   >
    
                                    <MasterTableView
                                       
                                        HorizontalAlign="NotSet" AutoGenerateColumns="False" >
                                        <Columns>

                                            <telerik:GridBoundColumn HeaderText="N°" DataField="id"  ReadOnly="true">
                                                <ItemStyle Width="30px" />
                                                <HeaderStyle Width="30px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn HeaderText="FechaProceso" DataField="FechaProceso"  ReadOnly="true">
                                                <ItemStyle Width="70px" />
                                                <HeaderStyle Width="70px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                                            </telerik:GridBoundColumn>


                                           <telerik:GridBoundColumn HeaderText="FechaRegistro" DataField="FechaRegistro"  ReadOnly="true">
                                                <ItemStyle Width="70px" />
                                                <HeaderStyle Width="70px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                                            </telerik:GridBoundColumn>
 
                                            <telerik:GridBoundColumn HeaderText="UsuarioRegistro" DataField="UsuarioRegistro"  ReadOnly="true">
                                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                <HeaderStyle Width="100px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                                            </telerik:GridBoundColumn>

                                                 <telerik:GridBoundColumn HeaderText="FechaModifico" DataField="fechaModifico"  ReadOnly="true">
                                                <ItemStyle Width="70px" />
                                                <HeaderStyle Width="70px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                                            </telerik:GridBoundColumn>
 
                                            <telerik:GridBoundColumn HeaderText="UsuarioModifico" DataField="UsuarioModifico"  ReadOnly="true">
                                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                <HeaderStyle Width="100px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true" />
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn HeaderText="Observación" DataField="Observacion"  ReadOnly="true" >
                                                <ItemStyle Width="100px"  HorizontalAlign="Left"/>
                                                <HeaderStyle Width="100px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true"/>
                                            </telerik:GridBoundColumn>


                                            <telerik:GridBoundColumn HeaderText="Estado" DataField="activo"  ReadOnly="true" >
                                                <ItemStyle Width="60px"  HorizontalAlign="Left"/>
                                                <HeaderStyle Width="60px"/>
                                                   <ItemStyle HorizontalAlign="Left" Font-Bold="true"/>
                                            </telerik:GridBoundColumn>

                                              <telerik:GridTemplateColumn >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibEditar"  runat="server" ImageUrl="~/Images/Icons/excel-16.png" CommandArgument='<%# Eval("id") %>'  CommandName="Editar"/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"/><HeaderTemplate>Exportar</HeaderTemplate>
                                            </telerik:GridTemplateColumn>
     

                                        </Columns>
                                        
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="0" />
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
