<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="VouchersDetraccioMng.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Reportes.VouchersDetraccioMng" %>
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
    <div class="col-md-10">
                <telerik:RadGrid ID="grdVouchers" runat="server" Width="520px" AutoGenerateColumns="false" Height="250px">
                    <MasterTableView EditMode="PopUp">
                        <Columns>
                            
                            <telerik:GridBoundColumn DataField="id_voucher" HeaderText="id_voucher" UniqueName="id_voucher" Visible="False">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="voucher" HeaderText="voucher" UniqueName="voucher" >
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="origen" HeaderText="origen" UniqueName="origen">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="aniomesdia" HeaderText="aniomesdia" UniqueName="aniomesdia">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="glosa" HeaderText="glosa" UniqueName="glosa">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="monto" HeaderText="monto" UniqueName="monto">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            
                        </Columns>
                    </MasterTableView>
                    
                    <ClientSettings>
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
