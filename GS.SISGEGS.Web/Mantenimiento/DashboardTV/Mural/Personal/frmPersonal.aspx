<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmPersonal.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.DashboardTV.Mural.Personal.frmPersonal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre || Mantenimiento de personal
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function RefreshImage(urlImage) {
            $("#imgPersonal").attr("src", urlImage);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramPersonal" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPersonal" LoadingPanelID="ralpPersonal"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPersonal" LoadingPanelID="ralpPersonal"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPersonal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPersonal" LoadingPanelID="ralpPersonal"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPersonal" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapPersonal" runat="server" Height="100%" Width="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblTitulo" runat="server" Text="Mantenimiento de datos personales" CssClass="titulo"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <telerik:RadComboBox ID="cboEmpresa" runat="server" Width="100%" DataTextField="nombreComercial" DataValueField="codigoRHPlus" Label="Empresa"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-2">
                                <telerik:RadTextBox ID="txtBuscar" runat="server" EmptyMessage="Escribir nombres o apellidos" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-1">
                                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"></telerik:RadButton>
                            </div>
                        </div>
                          <div class="row">
                            <div class="col-md-2">
                                <telerik:RadComboBox ID="cboMes" runat="server" Width="100%" DataTextField="Mes" DataValueField="valor" Label="Mes">

                                    <Items>   
                                          <telerik:RadComboBoxItem runat="server" Text="Todos" Value="0" />   
                                        <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" />   
                                        <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2"  />   
                                        <telerik:RadComboBoxItem runat="server" Text="Marzo"  Value="3" />
                                                  <telerik:RadComboBoxItem runat="server" Text="Abril"  Value="4" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Mayo"  Value="5" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Junio"  Value="6" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Julio"  Value="7" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Agosto"  Value="8" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Septiembre"  Value="9" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Octubre"  Value="10" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Nomviembre"  Value="11" /> 
                                                  <telerik:RadComboBoxItem runat="server" Text="Diciembre"  Value="12" /> 

                                    </Items>

                                </telerik:RadComboBox>
                            </div>
                            <div class="col-md-2">
                                
                            </div>
                            <div class="col-md-1">
                               
                            </div>
                        </div>

                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="90%">
                    <Columns>
                        <telerik:LayoutColumn Span="9" Height="100%">
                            <telerik:RadGrid ID="grdPersonal" runat="server" Width="100%" Height="100%" AutoGenerateColumns="false" OnItemCommand="grdPersonal_ItemCommand">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Edit.">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" ToolTip="Editar" CommandArgument='<%# Eval("NroDocumento") %>' CommandName="Editar"/>
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px"/>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Empresa" HeaderText="Empresa" UniqueName="Empresa">
                                            <HeaderStyle Width="100px"/>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NroDocumento" HeaderText="NroDocumento" UniqueName="NroDocumento">
                                            <HeaderStyle Width="90px"/>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ApPaterno" HeaderText="ApPaterno" UniqueName="ApPaterno">
                                            <HeaderStyle Width="140px"/>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ApMaterno" HeaderText="ApMaterno" UniqueName="ApMaterno">
                                            <HeaderStyle Width="140px"/>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" UniqueName="Nombres">
                                            <HeaderStyle Width="200px"/>
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha Nacimiento" UniqueName="Fecha">
                                            <HeaderStyle Width="200px"/>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Cargo" HeaderText="Cargo" UniqueName="Cargo">
                                            <HeaderStyle Width="140px"/>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ImagenURL" HeaderText="Imagen" UniqueName="ImagenURL">
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true"/>
                                    <Selecting AllowRowSelect="true"/>
                                </ClientSettings>
                            </telerik:RadGrid>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="3">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblNroDocumento" runat="server" CssClass="etiqueta"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblNombres" runat="server" CssClass="etiqueta"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblEmmprePersonal" runat="server" CssClass="etiqueta"></asp:Label>
                                    <asp:Label ID="imageURL" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <asp:Image ID="imgPersonal" runat="server" Height="300px" Width="225px"/>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblImagen" runat="server" Text="Imagen" CssClass="etiqueta" ></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <telerik:RadAsyncUpload ID="rauImagen" runat="server" MaxFileInputsCount="1" TargetFolder="~/Images/Temp" Enabled="false"
                                        AllowedFileExtensions="jpg,jpeg,png,gif" OnFileUploaded="rauImagen_FileUploaded">
                                    </telerik:RadAsyncUpload>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" Enabled="false">
                                        <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png"/>
                                    </telerik:RadButton>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Enabled="false">
                                        <Icon PrimaryIconUrl="../../../Images/Icons/sign-error-16.png"/>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
