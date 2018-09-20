<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGuiaVentaEmail.aspx.cs" Inherits="GS.SISGEGS.Web.Almacen.Operacion.frmGuiaVentaEmail" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
table { border-collapse: collapse; width: 791px; margin-left:auto; margin-right:auto;} 
table, td, th { border: 1px solid black;}
th { text-align:center}
</style> 

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table  >
        <tr style="text-align: center; height: 23px; background-color: #4CAF50; color: white;">
            <th colspan="2" >GUIA DE VENTA </th>
        </tr>
        <tr style="text-align: center; height: 23px; background-color: #adad85; color: white;">
            <th colspan="2" > <asp:Label ID="lblEMpresa" runat="server"   Font-Size="Small"></asp:Label> </th>
             
        </tr>

        <tr>
            <td  style="  width: 691px; ">

                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
                <br />
                    <p>
                      <b>N&deg; Guia:  </b> <asp:Label ID="lblGuia" runat="server" Text="Label"></asp:Label> 
                      <b>&nbsp;&nbsp; Pedido: </b>  <asp:Label ID="lblPedido" runat="server" Text="Label"></asp:Label>
                    </p>
                    <p><b>Cliente: </b><asp:Label ID="lblCliente" runat="server" Text="Label" Font-Size="Small"></asp:Label>
                    </p>
               
                <p><b>Fecha Salida Almacen: </b> <asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label>                   
                </p>
                <p>
                    <b>Hora Salida Almacen:</b> <asp:Label ID="lblHora" runat="server" Text="Label"></asp:Label>
                    
                </p>
            </td>
            <td>
                <p>								 
                    &nbsp;<asp:Image ID="Image1" runat="server" Height="427px" Width="426px" />
                </p>
            </td>
        </tr>							  
			
        <%--<tr>--%>
<%--            <td colspan="2" >Silvestre</td> 
        </tr>--%>
    </table>
    </form>
</body>
</html>
