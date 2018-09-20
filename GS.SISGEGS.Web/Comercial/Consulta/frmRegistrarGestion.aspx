<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS_Stock.Master" AutoEventWireup="true" CodeBehind="frmRegistrarGestion.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Consulta.frmRegistrarGestion" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script >

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return ((key >= 48 && key <= 57) || (key == 8))
        }
 
        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

          function TextChanged(sender, e) {
            dateVar = new Date();

            if (sender.value != "")
                dateVar.setDate(dateVar.getDate() + parseInt(sender.value));
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramUsuarioMngEdit.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramUsuarioMngEdit.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
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

     <telerik:RadAjaxManager ID="ramUsuarioMngEdit" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuarioMngEdit" LoadingPanelID="ralpUsuarioMngEdit"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdGestionStock">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuarioMngEdit" LoadingPanelID="ralpUsuarioMngEdit"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ramUsuarioMngEdit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVencidos" LoadingPanelID="ralpUsuarioMngEdit"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>


     <telerik:RadAjaxLoadingPanel ID="ralpUsuarioMngEdit" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

     <telerik:RadAjaxPanel ID="rapUsuarioMngEdit" runat="server" Width="100%">
              <div class="fila">
          <div class="colum10">
         </div>
     </div>
              <div class="fila">

            <div class="colum2"    >
                <asp:Label ID="lblMonedaImporte" runat="server" Text="Cantidad" CssClass="etiqueta" ></asp:Label>
            </div>

            <div class="colum2">
                    <asp:TextBox ID="txtImporte" runat="server"  Width="100%" onKeyPress="return soloNumeros(event)" ></asp:TextBox>     
            </div>

              <div class="colum2">
                  <telerik:RadButton ID="btnAgregar" runat="server" Text="Guardar" OnClick="btnAgregar_Click">
                                <Icon PrimaryIconUrl="~/Images/Icons/floppy-16.png" />
                            </telerik:RadButton>
         </div>
           <div class="colum2">
                 &nbsp;&nbsp;
         </div>
           <div class="colum2">
                       <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" Visible="true" ToolTip="CERRAR"> 
                            <Icon PrimaryIconUrl="~/Images/Icons/delete-16.png" />
                        </telerik:RadButton>
          </div>

     </div>
 
              <div class="fila">
                    <div class="colum2">
                       <asp:Label ID="Label1" runat="server" Text="Observación: " CssClass="etiqueta" ></asp:Label>
                  </div>

                  <div class="colum8">
                        <telerik:RadTextBox ID="txtObservacion" runat="server"   Width="100%" TextMode="MultiLine" Height="40px" MaxLength="90000"></telerik:RadTextBox>               
                    </div>
            </div>
              <div class="fila">
         <div class="colum10">
                <telerik:RadGrid ID="grdGestionStock" runat="server" Width="100%" 
                    AutoGenerateColumns="false" Height="155px"
                     OnItemCommand="grdGestionStock_ItemCommand"
                    ShowFooter="true"
                    >
                    <MasterTableView EditMode="PopUp">
                        <Columns>

                            <telerik:GridTemplateColumn HeaderText="Elim." Aggregate="Count">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>'
                                                ImageUrl="~/Images/Icons/trashcan-16.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                  </telerik:GridTemplateColumn>
                            
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID"  >
                                <HeaderStyle Width="20px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="Cantidad" Aggregate="Sum" >
                                <HeaderStyle Width="80px"/>
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                                  <telerik:GridBoundColumn DataField="FechaRegistro" HeaderText="FechaRegistro" UniqueName="FechaRegistro" >
                                <HeaderStyle Width="130px"/>
                            </telerik:GridBoundColumn>

                               <telerik:GridBoundColumn DataField="Observacion" HeaderText="Observación" UniqueName="Observacion" >
                                <HeaderStyle Width="350px"/>
                            </telerik:GridBoundColumn>
 
                        </Columns>
                    </MasterTableView>
                    
                     <ClientSettings Scrolling-UseStaticHeaders="true">
                        <Scrolling UseStaticHeaders="True" AllowScroll="true"></Scrolling>
                        <Selecting AllowRowSelect="True"></Selecting>
                        <Resizing AllowRowResize="True" EnableRealTimeResize="True"></Resizing>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
     </div>
     </telerik:RadAjaxPanel>


        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
</asp:Content>
