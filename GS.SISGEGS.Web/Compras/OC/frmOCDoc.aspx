<%@ Page Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmOCDoc.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.OC.frmOCDoc" %>

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

    <telerik:RadAjaxPanel ID="rapOCDocsMng" runat="server" Width="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-12">
                            <asp:label runat="server" ID="lblTitulo" CssClass="titulo"></asp:label>
                        </div>
                    </Content>                 
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-sm-1 col-md-1">
                            <asp:Label ID="lblDocumento" runat="server" Text="Documento" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-2">
                            <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" 
                                ID="RadAsyncUpload1" 
                                MultipleFileSelection="Disabled" EnableInlineProgress="false"
                                AutoAddFileInputs="false"  Localization-Select="Seleccionar Archivo"
                                AllowedFileExtensions="xls,xlsx,pdf,doc,docx,png,jpg"
                                OnFileUploaded="RadAsyncUpload1_FileUploaded"
                                />
                        </div>
                        <div class="col-sm-2 col-md-2">
                             <telerik:RadButton ID="btnCargar" runat="server" Text="Cargar">
                                <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                            </telerik:RadButton>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <telerik:RadButton ID="btnbuscar" runat="server" Text="Buscar" OnClick="btnbuscar_Click" >
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </div>
                        <div class="col-sm-3 col-md-5">

                        </div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-12">
                             <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </div>  
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-12">

                        
                            <asp:GridView runat="server" ID="gvwOCDocs" 
                                Width="100%" AutoGenerateColumns="False" 
                                OnRowDataBound="gvwOCDocs_RowDataBound" 
                                CssClass="table table-striped" OnRowCommand="gvwOCDocs_RowCommand"
                                
                                >
                            
                            <Columns>


                                <asp:BoundField DataField="Documento" HeaderText="Documento" />
                                <asp:BoundField DataField="RutaDocumento" HeaderText="RutaDocumento" Visible="false" />
                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />

                                <asp:TemplateField HeaderText="Descarga">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="hlDescarga" Text="Descargar" Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btneliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("Documento") %>' ImageUrl="~/Images/Icons/delete-16.png"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />--%>

                        </asp:GridView>  
                        </div>
                    </Content>
                </telerik:LayoutRow>
                    
            </Rows>
        </telerik:RadPageLayout>   
    </telerik:RadAjaxPanel>

</asp:Content>

