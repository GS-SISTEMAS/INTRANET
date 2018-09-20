<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOcImpParcial.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.OC.frmOcImpParcial" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Creacion de Parciales OC
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>

    <script type="text/javascript">
        

        function AvisoOk(args) {
            //showError("hola");
            showSuccess(args);
        }
        function AvisoError(args) {
            //showError("hola");
            showError(args);
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramOC" runat="server"> <%-- OnAjaxRequest="ramPedidoMng_AjaxRequest"--%>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOC" LoadingPanelID="ralpOC" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="gvwparcial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOC" LoadingPanelID="ralpOC"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            


            <telerik:AjaxSetting AjaxControlID="gvwoc">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gvwoc" LoadingPanelID="ralpOC"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            

            <telerik:AjaxSetting AjaxControlID="ramOc">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gvwoc" LoadingPanelID="ralpOC" />
                    <telerik:AjaxUpdatedControl ControlID="gvwparcial"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="btnagregarparcial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlOC" LoadingPanelID="ralpOC" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
 

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpOC" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmOC" runat="server" EnableShadow="true">
        <Windows>
             <telerik:RadWindow ID="rwOC" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlOC" runat="server" Width="100%" Height="100%" >
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow >
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registrar Parciales de OC"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png"/>
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                
                <telerik:LayoutRow Height="85%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadTabStrip runat="server" ID="stripOC" MultiPageID="radmultipage" SelectedIndex="1" CssClass="col-md-12">
                                <Tabs>
                                    <telerik:RadTab Text="OC Principal" Selected="True"></telerik:RadTab>
                                    <telerik:RadTab Text="Oc Parcial"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="radmultipage" SelectedIndex="0" Height="93%" Width="100%"  CssClass="col-md-12">
                                <telerik:RadPageView runat="server" ID="pageoc" Height="100%">
                                    <telerik:RadPageLayout ID="RadPageLayout4" runat="server" Width="100%" Height="100%">
                                        <Rows>
                                            
                                            <telerik:LayoutRow >
                                                <Content>
                                                    <div class="col-md-3">
                                                        <telerik:RadTextBox ID="txtop" runat="server" ReadOnly="true" Width="100%" EmptyMessage="op de OC" Label="Op" LabelWidth="33%"   ></telerik:RadTextBox>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <telerik:RadTextBox ID="txtnrooc" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Numero de OC" Label="Nro Importación"  LabelWidth="44%" ></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadDatePicker ID="dtpfechaoc" runat="server" Width="100%"  DateInput-ReadOnly="true" DatePopupButton-Visible="false"
                                                            DateInput-Label="Fecha OC" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="30%">
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <%--<telerik:RadButton ID="btncrearparcial" runat="server" Text="Crear Parcial" OnClick="btncrearparcial_Click" >
                                                            <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png"/>
                                                        </telerik:RadButton>--%>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-3">
                                                        <telerik:RadTextBox ID="txtidagenda" runat="server" ReadOnly="true" Width="100%"  Label="Codigo Agenda" LabelWidth="33%"   ></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <telerik:RadTextBox ID="txtagendanombre" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre Proveedor" Label="Nombre Proveedor"  LabelWidth="18%" ></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label2" runat="server" Text="Observaciones" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12">
                                                        <telerik:RadTextBox ID="txtobservaciones" runat="server" Width="100%" TextMode="MultiLine" Height="50px" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label3" runat="server" Text="Detalle de la OC" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                           
                                                
                                            <telerik:LayoutRow >
                                                
                                                    <Columns>
                                                        <telerik:LayoutColumn Span="12" Height="100%">
                                                            <telerik:RadGrid ID="gvwoc" runat="server" Width="100%" Height="350px" AutoGenerateColumns="false"
                                                                OnItemCommand="gvwoc_ItemCommand" OnItemDataBound="gvwoc_ItemDataBound">
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                                </ClientSettings>
                                                                <MasterTableView DataKeyNames="Kardex" ClientDataKeyNames="Kardex" Width="900px">
                                                                    <Columns>
                                                                        <%--<telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="idAmarre" runat="server" Text='<%# Eval("ID_Amarre") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>--%>
                                                                        <telerik:GridBoundColumn HeaderText="Kardex" DataField="Kardex" UniqueName="Kardex">
                                                                            <HeaderStyle Width="50px" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="ItemCodigo" DataField="ItemCodigo" UniqueName="ItemCodigo">
                                                                            <HeaderStyle Width="100px" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="Nombre Kardex" DataField="Nombre" UniqueName="Nombre">
                                                                            <HeaderStyle Width="380px" />
                                                                        </telerik:GridBoundColumn>
                                                                        
                                                                        <telerik:GridBoundColumn HeaderText="Marca" DataField="Marca" UniqueName="Marca">
                                                                            <HeaderStyle Width="200px" />
                                                                        </telerik:GridBoundColumn>

                                                                        <telerik:GridBoundColumn HeaderText="Unidad" DataField="ID_UnidadInv" UniqueName="ID_UnidadInv">
                                                                            <HeaderStyle Width="60px" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="Cantidad" DataField="Cantidad" UniqueName="Cantidad" DataFormatString="{0:F2}">
                                                                            <HeaderStyle Width="70px" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn HeaderText="Cant. Disponible" DataField="CantidadDisponible" UniqueName="CantidadDisponible" DataFormatString="{0:F2}">
                                                                            <HeaderStyle Width="100px" />
                                                                        </telerik:GridBoundColumn>
                                                                    
                                                                        <telerik:GridBoundColumn HeaderText="Precio" DataField="Precio" UniqueName="Precio" DataFormatString="{0:F4}">
                                                                            <HeaderStyle Width="80px" />
                                                                        </telerik:GridBoundColumn>

                                                                        <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" UniqueName="Importe" DataFormatString="{0:F4}">
                                                                            <HeaderStyle Width="80px" />
                                                                        </telerik:GridBoundColumn>

                                                                        

                                                                        <%--<telerik:GridTemplateColumn HeaderText="Elim.">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Item_ID") %>'
                                                                                    ImageUrl="~/Images/Icons/trashcan-16.png" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="40px" />
                                                                        </telerik:GridTemplateColumn>--%>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <%--<ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                                                    <ClientEvents OnRowDblClick="RowDblClick" />
                                                                </ClientSettings>--%>
                                                            </telerik:RadGrid>
                                                        </telerik:LayoutColumn>
                                                    </Columns>
                                                
                                            </telerik:LayoutRow>


                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="pageparcial" CssClass="col-md-12" Height="100%" >
                                    <telerik:RadPageLayout ID="RadPageLayout5" runat="server" Height="100%" Width="100%">
                                        <Rows>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12 containerSubTitulo">
                                                        <asp:Label ID="Label4" runat="server" Text="Detalle de Parciales" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-4">
                                                        <telerik:RadTextBox ID="txtnroimpparcial" runat="server" ReadOnly="true" Width="60%" EmptyMessage="Nro de Importación" Label="Nro de Importación" LabelWidth="60%"></telerik:RadTextBox>
                                                        
                                                    </div>
                                                    <div class="col-md-1">
                                                        <%--<telerik:RadButton ID="btnagregarparcial" runat="server" Text="Nuevo" OnClick="btnagregarparcial_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                                                        </telerik:RadButton>--%>
                                                    </div>
                                                    <div class="col-md-7"></div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12 containerSubTitulo">
                                                        <asp:Label ID="Label1" runat="server" Text="Adición de Items" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-2">
                                                        <asp:label Text="Nro Parcial:" runat="server" CssClass="etiqueta"></asp:label>
                                                        <span></span>
                                                        <telerik:RadComboBox ID="cbonroparcial" runat="server" Width="50%" AutoPostBack="true" OnSelectedIndexChanged="cbonroparcial_SelectedIndexChanged"></telerik:RadComboBox>
                                                        
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:label Text="Kardex" runat="server" CssClass="etiqueta"></asp:label>
                                                        <span></span>
                                                        <telerik:RadComboBox ID="cbokardex" runat="server" Width="70%" OnSelectedIndexChanged="cbokardex_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-5">
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-4">
                                                        <asp:label Text="Cantidad Dis. " runat="server" CssClass="etiqueta"></asp:label>
                                                        <span></span>
                                                        <telerik:RadTextBox ID="txtcantidaddisponible" runat="server" ReadOnly="true" Width="30%"></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:label Text="Cantidad " runat="server" CssClass="etiqueta"></asp:label>
                                                        <span></span>
                                                        <%--<telerik:RadTextBox ID="txtcantidad" runat="server" ReadOnly="false" Width="50%" InputType="Number" ></telerik:RadTextBox>--%>
                                                        <telerik:RadNumericTextBox ID="txtcantidad2" runat="server" ReadOnly="false" Width="50%" Type="Number" NumberFormat-DecimalDigits="4" MaxLength="12" MinValue="0"></telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:label Text="Precio " runat="server" CssClass="etiqueta"></asp:label>
                                                        <span></span>
                                                        <%--<telerik:RadTextBox ID="txtprecio" runat="server" ReadOnly="false" Width="60%" InputType="Number"></telerik:RadTextBox>--%>
                                                        <telerik:RadNumericTextBox ID="txtprecio2" runat="server" ReadOnly="false" Width="50%" Type="Number" NumberFormat-DecimalDigits="4" MaxLength="12" MinValue="0"></telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadButton ID="btnagregaritem" runat="server" Text="Agregar" OnClick="btnagregaritem_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                                                        </telerik:RadButton>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadButton ID="btnguardaritem" runat="server" Text="Guardar" OnClick="btnguardaritem_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                                                        </telerik:RadButton>

                                                        <asp:HiddenField ID="hvkardex" runat="server" />
                                                        <asp:HiddenField ID="hvnroparcial" runat="server" />
                                                        <asp:HiddenField ID="hvid" runat="server" />

                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow Height="60%">
                                                <Columns>
                                                    <telerik:LayoutColumn Span="12" Height="100%">
                                                        <telerik:RadGrid ID="gvwparcial" runat="server" Width="100%" Height="270px" AutoGenerateColumns="false"
                                                            OnItemCommand="gvwparcial_ItemCommand" OnItemDataBound="gvwparcial_ItemDataBound">
                                                            <ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2"></Scrolling>
                                                            </ClientSettings>
                                                            <MasterTableView DataKeyNames="No_RegistroParcial" ClientDataKeyNames="No_RegistroParcial" Width="900px" ShowFooter="true">
                                                                <Columns>
                                                                    <%--<telerik:GridTemplateColumn>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="idAmarre" runat="server" Text='<%# Eval("ID_Amarre") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>--%>
                                                                    
                                                                    
                                                                    <%--<telerik:GridTemplateColumn HeaderText="Edit.">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btneditaritem" runat="server" OnClick="btneditaritem_Click" CommandName="EditarItem" CommandArgument='<%# Eval("Id") %>'
                                                                                ImageUrl="~/Images/Icons/pencil-16.png" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="40px" />
                                                                    </telerik:GridTemplateColumn>--%>

                                                                    <telerik:GridTemplateColumn HeaderText="Elim.">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btneliminaritem" runat="server" CommandName="EliminarItem" CommandArgument='<%# Eval("Id") + "," + Eval("NroParcial") + "," + Eval("Kardex") %>'
                                                                                ImageUrl="~/Images/Icons/delete-16.png" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="40px" />
                                                                    </telerik:GridTemplateColumn>


                                                                    <telerik:GridBoundColumn HeaderText="Op_OC" DataField="Op_OC" UniqueName="Op_OC">
                                                                        <HeaderStyle Width="120px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="No_RegistroParcial" DataField="No_RegistroParcial" UniqueName="No_RegistroParcial">
                                                                        <HeaderStyle Width="120px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderText="NroParcial" DataField="NroParcial" UniqueName="NroParcial">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>
                                                                    

                                                                    <telerik:GridBoundColumn HeaderText="Id" DataField="Id" UniqueName="Id">
                                                                        <HeaderStyle Width="30px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderText="ItemCodigo" DataField="ItemCodigo" UniqueName="ItemCodigo">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>
                                                                   
                                                                    <telerik:GridBoundColumn HeaderText="Kardex" DataField="Kardex" UniqueName="Kardex">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="NombreKardex" DataField="NombreKardex" UniqueName="NombreKardex">
                                                                        <HeaderStyle Width="300px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderText="Id_UnidadInv" DataField="Id_UnidadInv" UniqueName="Id_UnidadInv">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderText="Cantidad" DataField="Cantidad" UniqueName="Cantidad" DataFormatString="{0:F2}">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>
                                                                    
                                                                    <telerik:GridBoundColumn HeaderText="Precio" DataField="Precio" UniqueName="Precio" DataFormatString="{0:F4}">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" UniqueName="Importe" DataFormatString="{0:F4}">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <%--<telerik:GridTemplateColumn HeaderText="Elim.">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Item_ID") %>'
                                                                                ImageUrl="~/Images/Icons/trashcan-16.png" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="40px" />
                                                                    </telerik:GridTemplateColumn>--%>
                                                                </Columns>
                                                            </MasterTableView>
                                                           <ClientSettings>
                                                                <Scrolling UseStaticHeaders="True" AllowScroll="true"></Scrolling>
                                                                <Selecting AllowRowSelect="True"></Selecting>
                                                                <Resizing AllowRowResize="True" EnableRealTimeResize="True"></Resizing>
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </telerik:LayoutColumn>
                                                </Columns>
                                            </telerik:LayoutRow>
                                            
                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>

                            </telerik:RadMultiPage>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>                

            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
