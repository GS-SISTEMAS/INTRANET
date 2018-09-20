<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmProyectado2.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.frmProyectado2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Mantenimiento de usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <script>


            function refreshGrid(arg) {
            if (!arg) {
                    $find("<%= ramUsuario.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=  ramUsuario.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
             }
            }


            function ShowCreateViewEstadoCuenta(id_cliente, fechaInicial, ID_Sectorista, ID_zona) {
                window.radopen("frmEstadoCuenta.aspx?id_cliente=" + id_cliente + "&fechaInicial=" + fechaInicial + "&ID_Sectorista=" + ID_Sectorista + "&ID_zona=" + ID_zona, "rwEstadoCuenta");
                return false;
            }

            function ShowCreateViewGestion(cliente, sectorista, zona, fecha) {
                window.radopen("frmHistoricoGestion.aspx?strCliente=" + cliente + "&strSectorista=" + sectorista + "&strZona=" + zona + "&strfecha=" + fecha, "rwVidaLey");
                return false;
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
    <telerik:RadAjaxManager ID="ramUsuario" runat="server" OnAjaxRequest="ramProyectado_AjaxRequest">
       <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="ramUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdClientes" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="grdClientes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


       </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuario" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmUsuario" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="720px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwUsuario" runat="server" Width="410px" Height="450px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>

               <telerik:RadWindow ID="rwEstadoCuenta" runat="server" Width="1050px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>


    <telerik:RadAjaxPanel ID="rapUsuario" runat="server" Width="100%">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Pronostico de Cobranza" CssClass="titulo"></asp:Label>
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

          <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid 
                    ID="grdClientes" 
                    runat="server"   
                    AutoGenerateColumns="false" Height="500px" 
                    OnItemCommand="grdClientes_ItemCommand"
                    OnNeedDataSource="grdClientes_NeedDataSource"
                    ShowFooter="true"
                    >
                    <MasterTableView DataKeyNames="id_Cliente,Id_Zona"
                                         HorizontalAlign="NotSet" AutoGenerateColumns="False" >
                               <Columns>

                                     <telerik:GridBoundColumn HeaderText="Nombre Zona" DataField="Nom_Zona"  Aggregate="Count" >
                                                <ItemStyle Width="220px" />
                                                <HeaderStyle Width="220px"/>
                                            </telerik:GridBoundColumn>


                                            <telerik:GridBoundColumn HeaderText="Nombre Cliente" DataField="ClienteNombre"    >
                                                <ItemStyle Width="260px" />
                                                <HeaderStyle Width="260px"/>
                                            </telerik:GridBoundColumn>

                                           <telerik:GridTemplateColumn  HeaderText="EstadoCuenta" AllowFiltering="false" HeaderStyle-Width="90px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibDeudaEstado" runat="server" ImageUrl="~/Images/Icons/search-16.png" CommandArgument='<%# Eval("id_Cliente") %>' CommandName="EstadoCuenta" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px"/>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>


                                              <telerik:GridBoundColumn HeaderText="NoVencida" DataField="ImportePendienteNoVencido"  DataFormatString="{0:#,##0.00}" Aggregate="Sum"  >
                                                <HeaderStyle Width="90px" />
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                              <telerik:GridBoundColumn HeaderText="Vencida" DataField="ImportePendienteVencido" DataFormatString="{0:#,##0.00}"  Aggregate="Sum" >
                                                <HeaderStyle Width="90px" />
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>


                                             <telerik:GridBoundColumn HeaderText="Deuda Total" DataField="ImportePendiente" DataFormatString="{0:#,##0.00}" Aggregate="Sum"  >
                                                <HeaderStyle Width="90px" />
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Presupuesto" DataField="Presupuesto" DataFormatString="{0:#,##0.00}" Aggregate="Sum"  >
                                                <HeaderStyle Width="90px" />
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                          <telerik:GridBoundColumn HeaderText="Pronostico" DataField="Proyectado" DataFormatString="{0:#,##0.00}"  Aggregate="Sum" >
                                                <HeaderStyle Width="85px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>


                                           <telerik:GridBoundColumn HeaderText="CuotaS1" DataField="MontoS1" DataFormatString="{0:#,##0.00}" Aggregate="Sum"  >
                                                <HeaderStyle Width="82px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn HeaderText="CuotaS2" DataField="MontoS2" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="82px"/>
                                               <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="CuotaS3" DataField="MontoS3" DataFormatString="{0:#,##0.00}"  Aggregate="Sum" >
                                                <HeaderStyle Width="82px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="CuotaS4" DataField="MontoS4" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="82px"/>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                 <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                   
                                          <telerik:GridBoundColumn HeaderText="Pron_NoVencido" DataField="ProyectadoNoVencido" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="100px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn HeaderText="Pron_01a30" DataField="Proyectado01a30" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="90px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                                  <telerik:GridBoundColumn HeaderText="Pron_31a60" DataField="Proyectado31a60" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="90px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                                 <telerik:GridBoundColumn HeaderText="Pron_61a120" DataField="Proyectado61a120" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="90px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn HeaderText="Pron_121a360" DataField="Proyectado121a360" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="90px"/>
                                              <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn HeaderText="Pron_360aMas" DataField="Proyectado360aMas" DataFormatString="{0:#,##0.00}" Aggregate="Sum" >
                                                <HeaderStyle Width="90px"/>
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
                                        
                                        <Scrolling AllowScroll="true"  UseStaticHeaders="true" FrozenColumnsCount="2"   />
                                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>

        <div>

        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" ></asp:Label>
        </div>
    </div>
</asp:Content>
