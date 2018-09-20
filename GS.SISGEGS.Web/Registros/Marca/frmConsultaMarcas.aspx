<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmConsultaMarcas.aspx.cs" Inherits="GS.SISGEGS.Web.Contratos.Reportes.frmConsultaMarcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

        function ShowInsertForm(id) {
            window.radopen("frmMarcasMng.aspx?idMarca=" + id, "rwRegistroMarca");
            return false;
        }

        function ShowHistoryForm(id) {
            window.radopen("frmMarcasHist.aspx?idMarca=" + id, "rwRegistroMarca");
            return false;
        }

        function ShowDocumentsForm(id) {
            window.radopen("frmMarcasDocs.aspx?idMarca=" + id, "rwRegistroMarca");
            return false;
        }

        function showLogo(id, imagen) {  
                window.radopen("frmMarcaLogo.aspx?idMarca=" + id + "&imagen=" + imagen, "rwRegistroMarca");
                return false;
        }

        function ShowClaseForm() {
            window.radopen("frmMarcasClase.aspx", "rwRegistroMarca");
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl LoadingPanelID="ralpReporte" ControlID="pnlGeneralMarcas"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

         <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>

     <telerik:RadWindowManager ID="rwmRegistroMarca" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwRegistroMarca" runat="server" Width="847px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlGeneralMarcas" runat="server" Width="100%" Height="700px" ClientEvents-OnRequestStart="requestStart" >
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte General de Registro de Marcas" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">

            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblEmpresa" Text="Empresa" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadComboBox ID="cboEmpresa" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblMarca" Text="Marca" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadTextBox ID="txtMarca" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadTextBox>   
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblTipo" Text="Tipo" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadComboBox ID="cboTipo" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblPais" Text="País" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadComboBox ID="cboPais" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
            </div>
            <div class="col-md-12">
                 <div class="col-md-1">
                    <asp:Label runat="server" ID="lblTitular" Text="Títular del Registro" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadComboBox ID="cboTitular" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblFecha" Text="Fecha Vencimiento" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadDatePicker ID="dpFechaDesde" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
                <div class="col-md-2">
                    <telerik:RadDatePicker ID="dpFechaHasta" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                </div>
                
            </div>
            <div class="col-md-12">
                 <div class="col-md-1">
                    <asp:Label runat="server" ID="lblClase" Text="Clase" CssClass="etiqueta"></asp:Label>
                     <asp:ImageButton runat="server" ID ="btnAbrirClase" ImageUrl ="~/Images/Icons/info-16.png" OnClientClick="ShowClaseForm();" />
                </div>
                <div class="col-md-3">
                    <telerik:RadComboBox ID="cboClase" runat="server" Width="250px"  Enabled="true" >
                            <Items>
                                <telerik:RadComboBoxItem Text ="Todos" Value=""/>
                                <telerik:RadComboBoxItem Text ="1" Value="1"/>
                                <telerik:RadComboBoxItem Text ="2" Value="2"/>
                                <telerik:RadComboBoxItem Text ="3" Value="3"/>
                                <telerik:RadComboBoxItem Text ="4" Value="4"/>
                                <telerik:RadComboBoxItem Text ="5" Value="5"/>
                                <telerik:RadComboBoxItem Text ="6" Value="6"/>
                                <telerik:RadComboBoxItem Text ="7" Value="7"/>
                                <telerik:RadComboBoxItem Text ="8" Value="8"/>
                                <telerik:RadComboBoxItem Text ="9" Value="9"/>
                                <telerik:RadComboBoxItem Text ="10" Value="10"/>
                                <telerik:RadComboBoxItem Text ="11" Value="11"/>
                                <telerik:RadComboBoxItem Text ="12" Value="12"/>
                                <telerik:RadComboBoxItem Text ="13" Value="13"/>
                                <telerik:RadComboBoxItem Text ="14" Value="14"/>
                                <telerik:RadComboBoxItem Text ="15" Value="15"/>
                                <telerik:RadComboBoxItem Text ="16" Value="16"/>
                                <telerik:RadComboBoxItem Text ="17" Value="17"/>
                                <telerik:RadComboBoxItem Text ="18" Value="18"/>
                                <telerik:RadComboBoxItem Text ="19" Value="19"/>
                                <telerik:RadComboBoxItem Text ="20" Value="20"/>
                                <telerik:RadComboBoxItem Text ="21" Value="21"/>
                                <telerik:RadComboBoxItem Text ="22" Value="22"/>
                                <telerik:RadComboBoxItem Text ="23" Value="23"/>
                                <telerik:RadComboBoxItem Text ="24" Value="24"/>
                                <telerik:RadComboBoxItem Text ="25" Value="25"/>
                                <telerik:RadComboBoxItem Text ="26" Value="26"/>
                                <telerik:RadComboBoxItem Text ="27" Value="27"/>
                                <telerik:RadComboBoxItem Text ="28" Value="28"/>
                                <telerik:RadComboBoxItem Text ="29" Value="29"/>
                                <telerik:RadComboBoxItem Text ="30" Value="30"/>
                                <telerik:RadComboBoxItem Text ="31" Value="31"/>
                                <telerik:RadComboBoxItem Text ="32" Value="32"/>
                                <telerik:RadComboBoxItem Text ="33" Value="33"/>
                                <telerik:RadComboBoxItem Text ="34" Value="34"/>
                                <telerik:RadComboBoxItem Text ="35" Value="35"/>
                                <telerik:RadComboBoxItem Text ="36" Value="36"/>
                                <telerik:RadComboBoxItem Text ="37" Value="37"/>
                                <telerik:RadComboBoxItem Text ="38" Value="38"/>
                                <telerik:RadComboBoxItem Text ="39" Value="39"/>
                                <telerik:RadComboBoxItem Text ="40" Value="40"/>
                                <telerik:RadComboBoxItem Text ="41" Value="41"/>
                                <telerik:RadComboBoxItem Text ="42" Value="42"/>
                                <telerik:RadComboBoxItem Text ="43" Value="43"/>
                                <telerik:RadComboBoxItem Text ="44" Value="44"/>
                                <telerik:RadComboBoxItem Text ="45" Value="45"/>
                            </Items>
                    </telerik:RadComboBox>    
                </div>

                <div class ="col-md-1">
                    <telerik:LayoutColumn Span="6">
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" >
                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                        </telerik:RadButton>
                    </telerik:LayoutColumn>
                </div>

                <div class="col-md-2">
                    <telerik:LayoutColumn Span="6">
                     <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                                    </telerik:RadButton>
                               </telerik:LayoutColumn>
                    
                </div>
                <div class="col-md-3">
                    <telerik:LayoutColumn Span="6">
                     <telerik:RadButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png"/>
                                    </telerik:RadButton>
                               </telerik:LayoutColumn>
                    
                </div>
            </div>
        </div>
        <div>
           &nbsp; 
        </div>
        

        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdGeneralMarcas" runat="server" AutoGenerateColumns="false" Height="600px" Width="100%"  AllowSorting="true"  
                        OnNeedDataSource="grdGeneralMarcas_NeedDataSource" OnItemCommand="grdGeneralMarcas_ItemCommand" OnItemDataBound="grdGeneralMarcas_ItemDataBound">
                    <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                                    <MasterTableView ShowFooter="true" >
                                            <Columns>
                                               <%-- <telerik:GridTemplateColumn HeaderText="Edit." AllowFiltering="false" UniqueName="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("idRegistroMarca") %>' CommandName="Editar" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                </telerik:GridTemplateColumn>--%>
                                                <telerik:GridBoundColumn DataField="idRegistroMarca" UniqueName="idRegistroMarca" HeaderText="Código" AllowSorting="true">
                                                    <HeaderStyle Width="50" />
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
                                                <telerik:GridBoundColumn UniqueName="Logo" DataField="Logo" Display="false"> 
                                                    </telerik:GridBoundColumn>  
                                                <%--<telerik:GridTemplateColumn HeaderText="Doc." AllowFiltering="false" UniqueName="Doc">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibDocumentos" runat="server" ImageUrl="~/Images/Icons/folder-document-16.png" CommandArgument='<%# Eval("idRegistroMarca") %>' CommandName="AdjuntarDocumento" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                </telerik:GridTemplateColumn>  
                                                <telerik:GridTemplateColumn HeaderText="Hist." AllowFiltering="false" UniqueName="Hist">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibHistorial" runat="server" ImageUrl="~/Images/Icons/notepad-16.png" CommandArgument='<%# Eval("idRegistroMarca") %>' CommandName="Historial" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                </telerik:GridTemplateColumn>   
                                                <telerik:GridTemplateColumn HeaderText="Logo" AllowFiltering="false" UniqueName="Logo">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibLogo" runat="server" ImageUrl="~/Images/Icons/search-16.png" CommandArgument='<%# Eval("Logo") %>' CommandName="Logo" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                </telerik:GridTemplateColumn>   --%>  
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
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
