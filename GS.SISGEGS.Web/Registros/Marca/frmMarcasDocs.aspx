<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmMarcasDocs.aspx.cs" Inherits="GS.SISGEGS.Web.Contratos.Reportes.frmMarcasDocs" %>
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
            <div class="colum1">
                <asp:Label ID="lblTipo" runat="server" Text="Tipo" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadComboBox ID="cboTipoDocumento" runat="server" Width="100%">
                </telerik:RadComboBox>
            </div>
            <div class="colum1">
                <asp:Label ID="lblDocumento" runat="server" Text="Documento" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum4" style="vertical-align:top">
               <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" Width="100%" 
                                ID="RadAsyncUpload1" 
                                MultipleFileSelection="Disabled" EnableInlineProgress="false"
                                AutoAddFileInputs="false"  Localization-Select="Seleccionar Archivo"
                                AllowedFileExtensions="xls,xlsx,pdf,doc,docx,png,jpg"
                                OnFileUploaded="RadAsyncUpload1_FileUploaded1"
                                />
            </div>
          <div class="colum2">
                <telerik:RadButton ID="btnCargar" runat="server" Text="Cargar" >
                    <%--<Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />--%>
                </telerik:RadButton>
              <%--OnClick="btnBuscar_Click"--%> 
            </div>
        </div>
               
        <div class="fila">
            <div class="colum3">
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" >
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum6">
                 <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>            
        </div>
       
        <div  class="fila">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="grdGeneralMarcas" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdGeneralMarcas_RowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    
                    <Columns>
                        <asp:BoundField DataField="idDocumento" HeaderText="Código" />
                        <asp:BoundField DataField="nombreTipoDocumento" HeaderText="Tipo Documento" />
                        <asp:BoundField DataField="documento" HeaderText="Documento" />
                        <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha Registro" />

                         <asp:TemplateField HeaderText="Descarga">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hlDescarga" Text="Descargar" Target="_blank"></asp:HyperLink>
                             </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066"/>
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                    
                </asp:GridView>                
            </div>
        </div>
       
    </telerik:RadAjaxPanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">


</asp:Content>
