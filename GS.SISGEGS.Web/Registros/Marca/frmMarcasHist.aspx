<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmMarcasHist.aspx.cs" Inherits="GS.SISGEGS.Web.Contratos.Reportes.frmMarcasHist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                //rWindow.BrowserWindow.refreshGrid(args);
                rWindow.close();
            });
        }

        <%--function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramGastosMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramGastosMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }--%>


        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <telerik:RadAjaxPanel ID="rapMarcasMng" runat="server" Width="100%">
        <div class="fila">
                <asp:label runat="server" ID="lblTitulo" CssClass="titulo"></asp:label>
            <br /><br />
            </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha Actualización" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadDatePicker ID="rdpFechaIni" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
            </div>
            <div class="colum3">
                <telerik:RadDatePicker ID="rdpFechaFin" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
            </div>
          
        </div>
        
        <div class="fila">
            &nbsp;
        </div>
        <div class="fila">
            <div class="colum3">
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" >
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
            
        </div>
       
        <div  class="fila">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdGeneralMarcas" runat="server" AutoGenerateColumns="false" Height="300px" Width="100%"  AllowSorting="true"  >
                    <%--OnNeedDataSource="grdGeneralMarcas_NeedDataSource" OnItemCommand="grdGeneralMarcas_ItemCommand"--%>
                                    <MasterTableView ShowFooter="true" >
                                            <Columns>
                                                <%--<telerik:GridBoundColumn DataField="idRegistroMarca" UniqueName="idRegistroMarca" HeaderText="Código" AllowSorting="true">
                                                    <HeaderStyle Width="50" />
                                                </telerik:GridBoundColumn> --%>
                                                <telerik:GridBoundColumn DataField="fechaHistorico" UniqueName="fechaHistorico" HeaderText="Fecha Actualización" AllowSorting="true" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>  
                                                <telerik:GridBoundColumn DataField="NombreComercial" UniqueName="NombreComercial" HeaderText="Empresa" AllowSorting="true">
                                                    <HeaderStyle Width="80" />
                                                </telerik:GridBoundColumn>                                               
                                                <telerik:GridBoundColumn DataField="Marca" UniqueName="Marca" HeaderText="Marca" AllowSorting="true">
                                                    <HeaderStyle Width="150" />
                                                </telerik:GridBoundColumn>                                                
                                                <telerik:GridBoundColumn DataField="Tipo" UniqueName="Tipo" HeaderText="Tipo" AllowSorting="true">
                                                    <HeaderStyle Width="150" />
                                                </telerik:GridBoundColumn>              
                                                <telerik:GridBoundColumn DataField="Clase" UniqueName="Clase" HeaderText="Clase" AllowSorting="true">
                                                    <HeaderStyle Width="50" />
                                                </telerik:GridBoundColumn>               
                                                <telerik:GridBoundColumn DataField="nombrePais" UniqueName="nombrePais" HeaderText="País" AllowSorting="true">
                                                    <HeaderStyle Width="150" />
                                                </telerik:GridBoundColumn>              
                                                <telerik:GridBoundColumn DataField="Certificado" UniqueName="Certificado" HeaderText="Certificado" AllowSorting="true">
                                                    <HeaderStyle Width="100" />
                                                </telerik:GridBoundColumn>                     
                                                <telerik:GridBoundColumn DataField="FechaVencimiento" UniqueName="FechaVencimiento" HeaderText="Fecha de Vencimiento" DataFormatString="{0:dd/MM/yyyy}" AllowSorting="true">
                                                    <HeaderStyle Width="100" />
                                                </telerik:GridBoundColumn>                                                                                                                                              
                                                <telerik:GridBoundColumn DataField="nombreTitular" UniqueName="nombreTitular" HeaderText="Titular" AllowSorting="true">
                                                    <HeaderStyle Width="300" />
                                                </telerik:GridBoundColumn>    
                                                <telerik:GridBoundColumn DataField="nombreEstado" UniqueName="nombreEstado" HeaderText="Estado" AllowSorting="true">
                                                    <HeaderStyle Width="100" />
                                                </telerik:GridBoundColumn>      
                                                <telerik:GridBoundColumn DataField="UsuarioHist" UniqueName="UsuarioHist" HeaderText="Usuario" AllowSorting="true">
                                                    <HeaderStyle Width="100" />
                                                </telerik:GridBoundColumn>   
                                            </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                    <Selecting AllowRowSelect="true"/>
                                </ClientSettings>
                            </telerik:RadGrid>
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
