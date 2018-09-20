<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmFamDocumentos.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Generales.frmFamDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Asignación de familias de documentos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnComercial">
                <UpdatedControls>
                    <%--<telerik:AjaxUpdatedControl ControlID="lstDocumentos" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
        <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="rPanel1" runat="server" LoadingPanelID="ralpReporte">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Asignación de Familias a Documentos"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblmensaje" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>                
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <telerik:RadListBox ID="lstDocumentos" runat="server" CheckBoxes="true" ShowCheckAll="true"
                                RenderMode="Lightweight" DataTextField="Nombre" DataValueField="id_documento" Height="500px"></telerik:RadListBox>
                            <div style="margin-top:5px;">
                                <asp:Button ID="btnComercial" runat="server" Text="Comercial" OnClick="btnComercial_Click" />
                                <span style="margin-left:30px;"><asp:Button ID="btnNoComercial" runat="server" Text="No comercial" OnClick="btnNoComercial_Click" /></span>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="titulo" style="font-size:18px;">
                                        Comercial
                                    </div>
                                    <div style="min-height:150px;">
                                        <telerik:RadGrid ID="rgComercial" runat="server" AutoGenerateColumns="false" Width="100%" OnItemCommand="rgComercial_ItemCommand">                                            
                                            <MasterTableView TableLayout="Fixed">
                                                <Columns>                                                    
                                                    <telerik:GridBoundColumn DataField="id_documento" HeaderText="ID">                                                        
                                                        <ItemStyle Width="40px" />
                                                        <HeaderStyle Width="40px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Nombre" HeaderText="Documento"></telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="">                                                        
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEliminaComercial" runat="server" ImageUrl="~/Images/Icons/delete-16.png" Width="16" Height="16" 
                                                                CommandArgument='<%# Eval("id_documento") %>' CommandName="Eliminar"/>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40px" />
                                                        <HeaderStyle Width="40px" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="titulo" style="font-size:18px;">
                                        No Comercial
                                    </div>
                                    <div style="min-height:150px;">
                                        <telerik:RadGrid ID="rgNoComercial" runat="server" AutoGenerateColumns="false" Width="100%" OnItemCommand="rgNoComercial_ItemCommand">                                            
                                            <MasterTableView TableLayout="Fixed">
                                                <Columns>                                                    
                                                    <telerik:GridBoundColumn DataField="id_documento" HeaderText="ID">                                                        
                                                        <ItemStyle Width="40px" />
                                                        <HeaderStyle Width="40px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Nombre" HeaderText="Documento"></telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="">                                                        
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEliminaComercial" runat="server" ImageUrl="~/Images/Icons/delete-16.png" Width="16" Height="16" 
                                                                CommandArgument='<%# Eval("id_documento") %>' CommandName="Eliminar"/>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40px" />
                                                        <HeaderStyle Width="40px" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">                    
                    <asp:Label ID="lblresultado" runat="server"></asp:Label>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
