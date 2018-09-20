<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSMS.aspx.cs" Inherits="GS.SISGEGS.Web.Comunicaciones.SMS.frmSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
      
    
<%--     <script type="text/javascript"> 
        function textCounter(field, countfield, maxlimit) { 
            if (field.value.length > maxlimit) 
                field.value = field.value.substring(0, maxlimit); 
            else 
                countfield.value = maxlimit - field.value.length; 
        } 
    
    </script> --%>

    <script type="text/javascript">

        function ContarCaracter(s, e)
        {

            var c = e.get_keyCode();
            document.getElementById('<%= txtMensaje.ClientID %>').value = document.getElementById('<%= txtMensaje.ClientID %>').value.replace("\n", " ");
            if (c == 13) {
                eventArgs.set_cancel(true);
                return false;
            }
            else {
                var longitud = document.getElementById('<%= txtMensaje.ClientID %>').value.length;
                if (longitud >= 160) {
                    return false;
                }
                document.getElementById('<%= txtContador.ClientID %>').value = 159 - longitud;
            }
             
        }

    </script>





</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">



         <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>

     <telerik:RadWindowManager ID="rwmRegistroSMS" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwRegistroSMS" runat="server" Width="847px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlGeneralMarcas" runat="server" Width="100%" Height="700px" ClientEvents-OnRequestStart="requestStart" >
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Servicio de Mensajeria Multiple " CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">

            </div>
        </div>

        <div class="row">

             <div class="col-md-11">
                <div class="col-md-11">
                    <asp:Label runat="server" ID="lblMensajes" Text="Mensaje" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadTextBox ID="txtMensaje" runat="server"   Width="250px"  Height="100"  Enabled="true"  TextMode="MultiLine">
                        <ClientEvents OnKeyPress="ContarCaracter" />
       
                    </telerik:RadTextBox>     

                </div>
             </div>

            <div class="col-md-12">

                <div class="col-md-12">
                    <asp:Label runat="server" ID="lblPerfil" Text="Perfil" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadComboBox ID="cboPerfil" runat="server" Width="250px"  Enabled="true" >
                    </telerik:RadComboBox>   
                </div>
            </div>
           
            </div>
 
        <div class="col-md-3">
                <div class="col-md-11">
                   <asp:Label runat="server" ID="lblContador" Text="Cantidad de caracteres restantes" CssClass="etiqueta"></asp:Label>
            
                 <telerik:RadTextBox ID="txtContador"    type="text" runat="server" Width="50px"  Enabled="true" >

                </telerik:RadTextBox>  
                </div>
            
                    <telerik:LayoutColumn Span="6">
                     <telerik:RadButton ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" >
                        <Icon PrimaryIconUrl="../../Images/Icons/sign-add-20.png"/>
                        </telerik:RadButton>
                     </telerik:LayoutColumn>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
     


        
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">

</asp:Content>