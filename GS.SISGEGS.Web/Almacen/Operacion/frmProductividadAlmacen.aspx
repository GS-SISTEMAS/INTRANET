<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmProductividadAlmacen.aspx.cs" Inherits="GS.SISGEGS.Web.Almacen.Operacion.frmProductividadAlmacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Productividad Almacen
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>
     
    <script type="text/javascript">

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
                var textoLetras = $('#<%=txtProductividad.ClientID %>').val();
                textoLetras = textoLetras.replace(/[^0-9]/g, '');
                $('#<%=txtProductividad.ClientID %>').val(textoLetras);
                var key = window.Event ? e.which : e.keyCode

                if (key==13)
                {
                    $.ajax({
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        url: "frmProductividadAlmacen.aspx/Registrar",
                        dataType: "json",
                        data: JSON.stringify({ "Op": textoLetras }),
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response)
                        {
                            if (response.d != "")
                            {
                                showError(response.d, 'Intranet')
                            }
                            else
                            {
                                $('#<%=btnBuscar.ClientID %>').click();
                            }
                            $('#<%=txtProductividad.ClientID %>').focus();
                        },
                        error: function (response) {
                            showError(response);

                        }
                    });

                    //showError('HOLA ENTRE', 'Intranet');
                    //$('#<%=btnRegistrar.ClientID %>').click();
                }
                 

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
                    <telerik:AjaxUpdatedControl ControlID="grdProductividad" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           
           <telerik:AjaxSetting AjaxControlID="ramUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProductividad" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="grdProductividad">
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
            <telerik:RadWindow ID="rwUsuario" runat="server"  ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <telerik:RadAjaxPanel ID="rapUsuario" runat="server" Width="100%"   ClientEvents-OnRequestStart="requestStart">
        

        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label2" runat="server" Text="Productividad Almacen" CssClass="titulo"></asp:Label>
            </div>
        </div>
     

         <div class="row">
                <div class="col-md-2">
                    <label class="rcbLabel" style="font-size:12px">Nro.OP:</label>  
                    <asp:TextBox ID="txtProductividad" runat="server" Width="100px"  onkeypress="soloNumeros(event)" MaxLength="12">
                     
                    </asp:TextBox>
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
                    <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click"  Text="Buscar" Width="100%">
                        <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"  />
                    </telerik:RadButton>  
                </div> 
                <div class="col-md-1">
                </div>
                <div class="col-md-1">
                    <telerik:RadButton ID="btnExcel" runat="server" Text="  Excel"  Width="100%"  OnClick="btnExcel_Click">
                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                    </telerik:RadButton> 
                </div>
        </div>
         <div class="row">
            <div class="col-md-12">
                    <telerik:RadButton ID="btnRegistrar"  runat="server" Width="24px" Style="display:none" > </telerik:RadButton>
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
                <telerik:RadGrid ID="grdProductividad" runat="server" AutoGenerateColumns="false" Height="500px" Width="100%"
                        ShowFooter="false" AllowMultiRowSelection="false" OnItemDataBound="grdLetras_ItemDataBound" >
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
                                    <asp:ImageButton ID="ibDesactivar" HeaderText="Estado"  runat="server"  ImageUrl="~/Images/Icons/circle-red-16.png"/>
                                </ItemTemplate>
                                <HeaderStyle Width="30px"/>
                                <ItemStyle Width="30px" />
                            </telerik:GridTemplateColumn>
 
                            <telerik:GridBoundColumn HeaderText="OP" DataField="OP">
                                <HeaderStyle Width="80px"/>
                            </telerik:GridBoundColumn> 
                             

                            <telerik:GridBoundColumn HeaderText="Fecha Inicio" DataField="FechaInicio"  DataFormatString="{0:dd/MM/yyyy}" >
                                <HeaderStyle Width="100px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Hora Inicio" DataField="HoraInicio"     >
                                <HeaderStyle Width="100px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn HeaderText="Fecha Fin" DataField="FechaFin"  DataFormatString="{0:dd/MM/yyyy}" >
                                <HeaderStyle Width="100px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Hora Fin" DataField="HoraFin"    >
                                <HeaderStyle Width="100px"/>
                                <FooterStyle Font-Bold="true"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="Tiempo<br>Productividad" DataField="TProductividad"   >
                                <HeaderStyle Width="120px"/>
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

