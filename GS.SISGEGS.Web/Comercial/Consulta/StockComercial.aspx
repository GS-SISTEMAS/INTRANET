<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="StockComercial.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Consulta.StockComercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Stocks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function AlertaSeleccion(strmsn) {
            var mensaje = strmsn; 
            alert(mensaje);
        }

        function ShowRegistrarGestion(Kardex, ID_Almacen) {
            window.radopen("frmRegistrarGestion.aspx?Kardex=" + Kardex + "&ID_Almacen=" + ID_Almacen, "rwVoucher");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramStock.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=  ramStock.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function buscarHistorico() {
            document.getElementById('ctl00_body_btnBuscar').click();
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramStock" runat="server" OnAjaxRequest="ramProyectado_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpStock" ControlID="rapStock"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpStock" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
       <Windows> 

        <telerik:RadWindow ID="rwVoucher" runat="server" Width="650px" Height="350px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
        </telerik:RadWindow>

       </Windows> 
    </telerik:RadWindowManager>


    <telerik:RadAjaxPanel ID="rapStock" runat="server" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar Stocks" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblAlmacen" runat="server" Text="Almacén" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-10">
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cboAlmacen" runat="server" Width="100%"
                            autopostback="True" AllowCustomText="true"  EmptyMessage="Search for warehouse..."   
                            onselectedindexchanged="cboAlmacen_SelectedIndexChanged"
                            >

                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                  <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label1" runat="server" Text="Tipo Material" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                       

                           <telerik:RadComboBox RenderMode="Lightweight" ID="cboTipoMaterial" AllowCustomText="true" runat="server" Width="100%"
                                   EmptyMessage="Search for type..."
                                onselectedindexchanged="cboTipoMaterial_SelectedIndexChanged" AutoPostBack="true"
                               >
                            </telerik:RadComboBox>

                    </div>
                </div>
            </div>
               <div class="col-md-4">
                  <div class="row">
             
                </div>
            </div>
       
        </div>
        <div class="row">
             <div class="col-md-4">
                  <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="Marca" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-10">
                        
                           <telerik:RadComboBox RenderMode="Lightweight" ID="cboMarca" AllowCustomText="true" runat="server" Width="100%"
                                   EmptyMessage="Search for brand..."
                               onselectedindexchanged="cboMarca_SelectedIndexChanged" AutoPostBack="true"
                               >
                            </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Producto" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="col-md-8">
                         <telerik:RadAutoCompleteBox ID="acbProducto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" DropDownWidth="50%"
                                                            DropDownHeight="150px" EmptyMessage="Buscar producto" AllowCustomEntry="true"  >
                                                            <WebServiceSettings Method="Item_BuscarProducto" Path="StockComercial.aspx" />
                         </telerik:RadAutoCompleteBox>
                    </div>
                </div>
            </div>
                 <div class="col-md-1">
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                </telerik:RadButton>
            </div>
                 <div class="col-md-3">
             
            </div>

        </div>
        <div class="row">
            <div class="col-md-12" >
                <telerik:RadGrid ID="grdStock" 
                    runat="server" Width="100%" Height="470px" 
                    AllowSorting="false" AllowMultiRowSelection="false"  
                    AutoGenerateColumns="False" ShowFooter="true"
                    ClientSettings-Scrolling-AllowScroll="true"
                     OnItemDataBound="grdStock_ItemDataBound"
                    >
 
                    <MasterTableView>
                      
                        <Columns>
                            
                            <telerik:GridBoundColumn HeaderText="ID_Almacen" DataField="ID_Almacen" Display="false"  HeaderStyle-Width="60px">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="No_Almacen" DataField="No_Almacen" HeaderStyle-Width="280px"
                                                     Aggregate="Count" FooterText="Total: " >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Kardex" DataField="Kardex" HeaderStyle-Width="60px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Codigo" DataField="Codigo" HeaderStyle-Width="100px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripcion" DataField="Descripcion" Display="true" HeaderStyle-Width="250px">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="PesoNeto" DataField="PesoNeto_Kg" Display="true" HeaderStyle-Width="60px" 
                                ItemStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
 
                            <telerik:GridBoundColumn HeaderText="Stock(Disp)" DataFormatString="{0:F0}" DataField="StockDisponible" Aggregate="Sum" HeaderStyle-Width="80px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="DiasInv." DataFormatString="{0:F0}" DataField="DiasInventario" Aggregate="Avg" HeaderStyle-Width="60px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
 
                            <telerik:GridBoundColumn HeaderText="KgLt(Disp)" DataFormatString="{0:F2}" DataField="StockDisponible_KgLt" Aggregate="Sum" HeaderStyle-Width="70px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
 

                            <telerik:GridBoundColumn HeaderText="MesStock(Disp)" DataFormatString="{0:F2}" DataField="MesStock_Disponible" Aggregate="Sum" HeaderStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>



                            <telerik:GridBoundColumn UniqueName="TransitoOC" HeaderText="TransXOC" DataFormatString="{0:F2}" DataField="TransitoOC" Aggregate="Sum" HeaderStyle-Width="70px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="TransitoGestion" HeaderText="TransXGestion" DataFormatString="{0:#,##0.00}"  DataField="TransitoGestion" Aggregate="Sum" HeaderStyle-Width="85px"
                                ItemStyle-HorizontalAlign="Right" 
                                ItemStyle-ForeColor="Blue"
                                >
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="StockTotal_KgLt" HeaderText="TotalxKgLt" DataFormatString="{0:F2}" DataField="StockTotal_KgLt" Aggregate="Sum" HeaderStyle-Width="80px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
         
                            <telerik:GridBoundColumn UniqueName="MesesStock_Disponible" HeaderText="MesesStock(Disp)" DataFormatString="{0:F2}" DataField="MesesStock_Disponible" Aggregate="Sum" HeaderStyle-Width="110px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="PromedioVenta_KgLt" HeaderText="AVGVentas" DataFormatString="{0:F2}" DataField="PromedioVenta_KgLt" Aggregate="Avg" HeaderStyle-Width="70px"
                                ItemStyle-HorizontalAlign="Right">
                                 <FooterStyle HorizontalAlign="Right" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
