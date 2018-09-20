<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmMarcasMng.aspx.cs" Inherits="GS.SISGEGS.Web.Contratos.Reportes.frmMarcasMng" %>
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
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadComboBox ID="cboEmpresa" runat="server" Width="100%">
                    </telerik:RadComboBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadTextBox runat="server" ID="txtMarca"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadComboBox ID="cboTipo" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
            
                <div class="colum2">
                    <asp:Label ID="lblCertificado" runat="server" Text="Certificado/N° Expediente" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadTextBox runat="server" ID="txtCertificado"></telerik:RadTextBox>
                </div>
        </div>
        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblClase" runat="server" Text="Clase" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadComboBox ID="cboClase" runat="server" Width="100%"  Enabled="true" >
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
            
                <div class="colum2">
                    <asp:Label ID="lblPais" runat="server" Text="País" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                     <telerik:RadComboBox ID="cboPais" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
        </div>
        <div class="fila">
            <div class="colum2">
                    <asp:Label ID="lblFechaSuscripcion" runat="server" Text="Vencimiento" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                     <telerik:RadDatePicker ID="dtpVencimiento" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="colum2">
                    <asp:Label ID="lblTitular" runat="server" Text="Títular" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadComboBox ID="cboTitular" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
            
                
        </div>


        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                     <telerik:RadComboBox ID="cboEstado" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
        </div>
        
        

        
        <div class="fila">
            &nbsp;
        </div>
        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" >
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
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
