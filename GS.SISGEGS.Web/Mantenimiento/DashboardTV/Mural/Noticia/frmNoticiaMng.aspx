<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpM.Master" AutoEventWireup="true" CodeBehind="frmNoticiaMng.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.DashboardTV.Mural.Noticia.frmNoticiaMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
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
    <telerik:RadAjaxManager ID="ramNoticiasMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapNoticiasMng" LoadingPanelID="ralpPerfulMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdImagenes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapNoticiasMng" LoadingPanelID="ralpPerfulMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindow ID="RadWindow1" runat="server"></telerik:RadWindow>

    <telerik:RadAjaxLoadingPanel ID="ralpPerfulMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapNoticiasMng" runat="server" Width="100%">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                <telerik:RadComboBox ID="cboEmpresa" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa">
                </telerik:RadComboBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblTitulo" runat="server" Text="Titulo " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                <telerik:RadTextBox ID="txtTitulo" runat="server" MaxLength="50" Width="100%"></telerik:RadTextBox>
            </div>
        </div>

         <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblTexto" runat="server" Text="Texto " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
                <telerik:RadTextBox ID="txtTexto" runat="server" Width="100%" TextMode="MultiLine" MaxLength="200" Height="40px"></telerik:RadTextBox>
            </div>
        </div>
       <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblFechaPublicacion" runat="server" Text="Publicar" CssClass="etiqueta" Width="150px"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadDatePicker ID="dpFechaPublicacion" runat="server" Width="100%" DateInput-DateFormat="dd/MM/yyyy">
                </telerik:RadDatePicker>
            </div>
           <div class="colum1">
                <asp:Label ID="lblFechaVencimiento" runat="server" Text="Vence" CssClass="etiqueta" Width="150px"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadDatePicker ID="dpFechaVencimiento" runat="server" Width="100%" DateInput-DateFormat="dd/MM/yyyy">
                </telerik:RadDatePicker>
            </div>
           <div class="colum1">
                <telerik:RadButton ID="ckbActivo" runat="server" Text="Activo" ToggleType="CheckBox" ButtonType="ToggleButton" Checked="true">
                </telerik:RadButton>
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <telerik:RadGrid ID="grdImagenes" runat="server" Width="100%" Height="120px" Visible="false" AutoGenerateColumns="false"
                    OnDeleteCommand="grdImagenes_DeleteCommand">
                    <MasterTableView DataKeyNames="idNoticiaFoto">
                        <Columns>
                            <telerik:GridBoundColumn DataField="idNoticiaFoto" HeaderText="ID" UniqueName="idNoticiaFoto" Display="false">
                                <HeaderStyle />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="urlImagen" HeaderText="Nombre imagen" UniqueName="urlImagen">
                                <HeaderStyle />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Sel.">
                                <ItemTemplate>
                                    <telerik:RadButton ID="btnSeleccionar" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox" Checked='<%# Eval("activo") %>'>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn ConfirmText="¿Desea eliminar la imagen?" ConfirmDialogType="RadWindow"
                                HeaderStyle-Width="40px" HeaderText="Elim." ConfirmTitle="Eliminar" ButtonType="ImageButton"
                                CommandName="Delete" ImageUrl="~/Images/Icons/delete-16.png" UniqueName="Elim" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblArchivo" runat="server" Text="Imagenes " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadAsyncUpload ID="rauArchivo" runat="server" MaxFileInputsCount="5" AllowedFileExtensions="jpg,jpeg,png,gif" 
                    Width="100%" OnFileUploaded="rauArchivo_FileUploaded" TargetFolder="~/Images/Temp"></telerik:RadAsyncUpload>
            </div>
        </div>
        <div class="fila">
            <div class="colum8">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum8">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
