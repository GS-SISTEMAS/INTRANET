<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCanjeAtomaticoLetras.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Informacion.frmCanjeAtomaticoLetras" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Mantenimiento de usuarios
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>
     
    <script type="text/javascript">

        function Confirm() {
            var Result = confirm("Esta Seguro?");
            var confirm_value = document.querySelector('[name="confirm_value"]');
            if (Result) {
                return true;
            } else {
                return false;
            }
        }


            function requestStart(sender, args) {
                if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                    args.set_enableAjax(false);
            }


            function mostrarMensaje(mensaje) {
                console.log(mensaje);
                showError(mensaje,'Intranet');
            }

            

            function soloNumeros(e)
            {
                //showError('HOLA', 'Intranet');
                var textoLetras = $('#<%=txtLetras.ClientID %>').val();
                textoLetras = textoLetras.replace(/[^0-9]/g, '');
                $('#<%=txtLetras.ClientID %>').val(textoLetras);
                var key = window.Event ? e.which : e.keyCode

                if (key==13)
                {
                    $.ajax({
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        url: "frmCanjeAtomaticoLetras.aspx/Registrar",
                        dataType: "json",
                        data: JSON.stringify({ "letra": textoLetras }),
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            if (response.d != "") {
                                showError(response.d, 'Intranet')
                            }
                            else
                            {
                                $('#<%=btnBuscar.ClientID %>').click();
                            }
                            $('#<%=txtLetras.ClientID %>').focus();
                        },
                        error: function (response) {
                            showError(response);

                        }
                    });

                    //showError('HOLA ENTRE', 'Intranet');
                    //$('#<%=btnRegistrar.ClientID %>').click();
                }
                 

           }

            function tamañoPantalla()
            {
                return screen.width
            }
          


            function ShowCreate(objUsuario) {
                window.radopen("frmInactivacionUsuarioMng.aspx?objUsuario=" + objUsuario, "rwUsuario");
            return false;
            }

            function ShowCreateEdit(objUsuario) {
                window.radopen("frmUsuarioMngEdit.aspx?objUsuario=" + objUsuario, "rwUsuario");
                return false;
            }
        


            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= ramUsuario.ClientID %>").ajaxRequest("Rebind");
                }
                else {
                    $find("<%= ramUsuario.ClientID %>").ajaxRequest("Registro," + arg);
                }
            }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramUsuario" runat="server"  >
       <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="ibDesactivar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLetras" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


           <telerik:AjaxSetting AjaxControlID="ramUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLetras" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="grdLetras">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuario" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
       

       </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuario" runat="server">
    </telerik:RadAjaxLoadingPanel>
    
  

    <telerik:RadWindowManager ID="rwmUsuario" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwUsuario" runat="server" Width="410px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <telerik:RadAjaxPanel ID="rapUsuario" runat="server" Width="100%" ClientEvents-OnRequestStart="requestStart">
        

        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label2" runat="server" Text="Canje Automatico de Letras" CssClass="titulo"></asp:Label>
            </div>
        </div>
     

         <div class="row">
                <div class="col-md-2">
                    <label class="rcbLabel" style="font-size:12px">Nro.Letra:</label>  
                    <asp:TextBox ID="txtLetras" runat="server" Width="100px" onkeypress="soloNumeros(event)" MaxLength="12">
                    </asp:TextBox>
                </div>

                <div class="col-md-2">
                    <telerik:RadComboBox ID="cboEstado" runat="server" Width="100%" Label=" Estado:">
                    </telerik:RadComboBox>
                </div>
                
                <div class="col-md-2">
                    <telerik:RadDatePicker ID="dpInicio" runat="server" DateInput-ReadOnly="true" Width="200px" DateInput-Label="Desde:">
                            <DateInput runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="col-md-2">
                    <telerik:RadDatePicker ID="dpFinal" runat="server" DateInput-ReadOnly="true" Width="200px" DateInput-Label="Hasta:">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="col-md-1">
                    <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" Width="100%">
                        <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"  />
                    </telerik:RadButton>  
                </div>
                <div class="col-md-1">
                    <telerik:RadButton ID="btnFinanciamiento" runat="server"  OnClick="btnFinanciamiento_Click" Text="  Canjear" Width="100%">
                                <Icon PrimaryIconUrl="../../Images/Icons/money-16.png"  />
                    </telerik:RadButton>
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-1">
                    <telerik:RadButton ID="btnExcel" runat="server" Text="  Excel" OnClick="btnExcel_Click" Width="100%">
                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                    </telerik:RadButton> 
                </div>
        </div>
         <div class="row">
            <div class="col-md-12">
                    <telerik:RadButton ID="btnRegistrar"  runat="server" Width="24px"      
                        OnClick="btnRegistrar_Click" Style="display:none" > </telerik:RadButton>

            </div>
            
        </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" ></asp:Label> 
            <telerik:RadWindowManager ID="rwmReporte" runat="server"></telerik:RadWindowManager>
        </div>
    </div>
         <div class="row">
             <div class="col-sm-12">
                <telerik:RadGrid ID="grdLetras" runat="server" AutoGenerateColumns="false" Height="400px" Width="100%"
                        ShowFooter="false" AllowMultiRowSelection="false"
                    OnItemDataBound="grdLetras_ItemDataBound">
                    <ClientSettings>
                        <Scrolling 
                            AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"
                            FrozenColumnsCount="10" EnableNextPrevFrozenColumns="true" EnableColumnClientFreeze="true"></Scrolling>
                    </ClientSettings>
                    <ExportSettings Excel-Format="Html" OpenInNewWindow="true"></ExportSettings>
                    <MasterTableView TableLayout="Fixed" EditFormSettings-PopUpSettings-ScrollBars="Both">
                        <Columns>
                            <telerik:GridTemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibDesactivar" HeaderText="Estado"  
                                                runat="server"     
                                                ImageUrl="~/Images/Icons/circle-red-16.png"/>
                                        </ItemTemplate>
                                    <HeaderStyle Width="30px"/>
                                    <ItemStyle Width="30px" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn HeaderText="Cod. Letra" DataField="CodigoLetra">
                                <HeaderStyle Width="50px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Estado" DataField="Estado">
                                <HeaderStyle Width="100px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Cod. Finan." DataField="CodigoFinanciamiento" >
                                <HeaderStyle Width="60px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Fecha<br> de Giro" DataField="FechaGiro"  DataFormatString="{0:dd/MM/yyyy}" >
                                <HeaderStyle Width="75px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Fecha de<br>Aceptación" DataField="FechaAceptacion"   DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="75px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Tipo Moneda" DataField="TipoMoneda" >
                                <HeaderStyle Width="120px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" >
                                <HeaderStyle Width="80px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="N° LETRA" DataField="NLETRA" >
                                <HeaderStyle Width="90px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Fecha<br>Entrega" DataField="FechaEntrega"  DataFormatString="{0:dd/MM/yyyy}" >
                                <HeaderStyle Width="75px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="ID<br>Agenda" DataField="ID_Agenda" >
                                <HeaderStyle Width="90px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Cliente" DataField="Cliente" >
                                <HeaderStyle Width="250px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Nombre Direccion" DataField="NombreDireccion" >
                                <HeaderStyle Width="800px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>   
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
             </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">

</asp:Content>--%>
