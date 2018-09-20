<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpL.Master" AutoEventWireup="true" CodeBehind="frmCierreContableMng.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Planificacion.frmCierreContableMng" %>
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
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <%--<telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />--%>

    <telerik:RadAjaxManager ID="ramCierreContableMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdCierreMng">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCierreMng" LoadingPanelID="ralpCierreContablefulMng" />
                    <telerik:AjaxUpdatedControl ControlID="RadTextBox1" UpdatePanelRenderMode="Inline"  />
                    <telerik:AjaxUpdatedControl ControlID="RadTextBox2"  UpdatePanelRenderMode="Inline"/>
                    <telerik:AjaxUpdatedControl ControlID="dpPeriodo" UpdatePanelRenderMode="Inline" LoadingPanelID="ralpCierreContablefulMng" />
                    
                   <%-- <telerik:AjaxUpdatedControl ControlID="SavedChangesList" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapCierreContableMng" LoadingPanelID="ralpCierreContablefulMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="dpPeriodo" LoadingPanelID="ralpCierreContablefulMng" />
                 </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="rwmCierre" runat="server" RenderMode="Lightweight" EnableShadow="true"></telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="ralpCierreContablefulMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator2" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />

    <telerik:RadAjaxPanel ID="rapCierreContableMng" runat="server" Width="100%">
         <div id="demo" class="demo-container no-bg">
        <div class="row">
            <div class="col-sm-3">
                Periodo:
                <telerik:RadMonthYearPicker ID="dpPeriodo" runat="server" DateInput-DateFormat="MM-yyyy" Width="100px"></telerik:RadMonthYearPicker>
                
            </div>
            <div class="col-sm-6">
                Fecha Inicio:
                <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="100px" AutoPostBack="True" Enabled="False" ></telerik:RadTextBox>
                Fecha Fin:
                <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="100px" AutoPostBack="True" Enabled="False" ></telerik:RadTextBox>
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
                </div>
        </div>
         
       <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdCierreMng" runat="server" Width="100%" AutoGenerateColumns="false" Height="400px"
                    GridLines="None"  PageSize="15"  OnUpdateCommand="grdCierreMng_UpdateCommand"
                    OnNeedDataSource="grdCierreMng_NeedDataSource" OnItemCreated="grdCierreMng_ItemCreated"
                    OnInsertCommand="grdCierreMng_InsertCommand" OnItemDataBound="grdCierreMng_ItemDataBound" >
<%--                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>--%>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="id_Modulo"
                        HorizontalAlign="NotSet" AutoGenerateColumns="False">
                        <Columns>
                            <telerik:GridEditCommandColumn>
                                <HeaderStyle Width="100px"/>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridCheckBoxColumn HeaderText="Activo" DataField="flag" UniqueName="flag" >
                                <HeaderStyle Width="30px"/>
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="id_Modulo" HeaderText="id_Modulo" UniqueName="id_Modulo" Visible="False">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Modulo" HeaderText="Modulo" UniqueName="Modulo">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NombreEstado" HeaderText="Estado" UniqueName="NombreEstado">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="FechaCierre" HeaderText="FechaCierre" UniqueName="FechaCierre" PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="100px"/>

                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Detalle" HeaderText="Detalle" UniqueName="Detalle">
                                <HeaderStyle Width="100px"/>
                                
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Observacion" HeaderText="Observacion" UniqueName="Observacion">
                                <HeaderStyle Width="100px"/>
                                
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Responsable" HeaderText="Responsable" UniqueName="Responsable">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            
                        </Columns>
                       
                    </MasterTableView>
                    
                    <ClientSettings >
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                        
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>
             </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
