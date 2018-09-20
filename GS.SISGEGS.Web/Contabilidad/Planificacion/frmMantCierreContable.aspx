<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="frmMantCierreContable.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Planificacion.frmMantCierreContable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    IntranetGS: Mantenimiento de Cierre Contable por Modulos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowCreate(objCierreContable) {
            window.radopen("frmCierreContableMng.aspx?objCierreContable=" + objCierreContable, "rwCierreContable");
            return false;
        }

        function ShowCreateEdit(objCierrePeriodo) {
            window.radopen("frmModificaCierreContable.aspx?objCierrePeriodo=" + objCierrePeriodo, "rwCierreContable02");
            return false;
        }

        function ShowHistorial(objHistorial) {
            window.radopen("frmHistorialCierreContable.aspx?objHistorial=" + objHistorial, "rwCierreContable02");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramCierreContable.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramCierreContable.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnPdf") >= 0)
                args.set_enableAjax(false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramCierreContable" runat="server" OnAjaxRequest="ramCierreContable_AjaxRequest" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapCierreContable" LoadingPanelID="ralpCierreContable"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <%--<telerik:AjaxUpdatedControl ControlID="dpBuscar" UpdatePanelRenderMode ="Block" LoadingPanelID="ralpCierreContable" />--%>
                    
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ramCierreContable">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCierrePorPeriodo" LoadingPanelID="ralpCierreContable"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdCierreContable" LoadingPanelID="ralpCierreContable"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCierrePorPeriodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapCierreContable" LoadingPanelID="ralpCierreContable"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCierreContable">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapCierreContable" LoadingPanelID="ralpCierreContable"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpCierreContable" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmCierreContable" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwCierreContable" runat="server" Width="1000px" Height="600px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwCierreContable02" runat="server" Width="800px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    
    <telerik:RadAjaxPanel ID="rapCierreContable" runat="server" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Mantenimiento de Cierre Contable por Mes"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
                <%--<telerik:RadTextBox ID="txtBuscar" runat="server" Width="40%" EmptyMessage="Buscar"></telerik:RadTextBox>--%>
                <telerik:RadMonthYearPicker ID="dpBuscar" runat="server" DateInput-DateFormat="MM-yyyy" Width="100px"></telerik:RadMonthYearPicker>
                <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar">
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnNuevo" runat="server" Text="Agregar" OnClick="btnNuevo_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/file-empty-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdCierrePorPeriodo" runat="server" Width="50%" AutoGenerateColumns="false" Height="150px" OnItemCommand="grdCierrePorPeriodo_ItemCommand"
                    OnSelectedIndexChanged="grdCierrePorPeriodo_OnSelectedIndexChanged" >
                    <MasterTableView DataKeyNames="idPlanificacion">
                        <Columns>
                            
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("idPlanificacion") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="periodo" HeaderText="periodo" UniqueName="periodo">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AgendaNombre" HeaderText="Usuario" UniqueName="AgendaNombre">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaInicio" HeaderText="FechaInicio" UniqueName="FechaInicio" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaFin" HeaderText="FechaFin" UniqueName="FechaFin" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="True">
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
                <telerik:RadButton ID="btnPdf" runat="server" Text="PDF" OnClick="btnPdf_OnClick">
                                <Icon PrimaryIconUrl="../../Images/Icons/pdf_22.png"/>
                            </telerik:RadButton>
            </div>

        </div>
        <div class="row">
            <div class="col-md-10">
                
                <telerik:RadGrid ID="grdCierreContable" runat="server" Width="90%" AutoGenerateColumns="false" Height="400px" OnItemCommand="grdCierreContable_ItemCommand"
                    OnNeedDataSource="grdCierreContable_OnNeedDataSource"
                    AllowPaging="True" GridLines="None" RegisterWithScriptManager="False">
                    
                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true"  Excel-Format="Html" >
                        <Pdf PageHeight="210mm" PageWidth="297mm" DefaultFontFamily="Arial Unicode MS" PageTopMargin="45mm"
                            BorderStyle="Medium">
                        </Pdf>
                    </ExportSettings>
                    
                    <MasterTableView EditMode="PopUp" DataKeyNames="id_Modulo" TableLayout="Fixed" >
                        <Columns>
                            
                            <telerik:GridBoundColumn DataField="id_Modulo" HeaderText="id_Modulo" UniqueName="id_Modulo" Visible="False">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Modulo" HeaderText="Modulo" UniqueName="Modulo">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Estado" HeaderText="IdEstado" UniqueName="Estado" Visible="False">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NombreEstado" HeaderText="Estado" UniqueName="NombreEstado">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaCierre" HeaderText="FechaCierre" UniqueName="FechaCierre" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Detalle" HeaderText="Detalle" UniqueName="Detalle">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Observacion" HeaderText="Observacion" UniqueName="Observacion">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Responsable" HeaderText="Responsable" UniqueName="Responsable">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("id_Detalle") %>'  CommandName="Editar"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibDetalle" runat="server" ImageUrl="~/Images/Icons/notepad-16.png" CommandArgument='<%# Eval("id_Detalle") %>'  CommandName="Detalle"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Font-Size="12px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Font-Names="Arial Unicode MS" Font-Size="12px"
                ></ItemStyle>
            <AlternatingItemStyle HorizontalAlign="Center" Font-Names="Arial Unicode MS" Font-Size="12px"></AlternatingItemStyle>
                    <ClientSettings>
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
            <div class="col-xs-1">
                <div class="row">
                    <telerik:RadCalendar RenderMode="Classic" ID="RadCalendar1" runat="server" TitleFormat="MMMM yyyy"  
                        AutoPostBack="true" OnDayRender="CustomizeDay" Width="120px" Height="90"></telerik:RadCalendar>
                </div>
                <div class="row">
                    <telerik:RadCalendar RenderMode="Classic" ID="RadCalendar2" runat="server" TitleFormat="MMMM yyyy"  
                        AutoPostBack="true" OnDayRender="CustomizeDay2" Width="120px" Height="90"></telerik:RadCalendar>
                </div>
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
