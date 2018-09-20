using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using GS.SISGEGS.BE;

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "DashboardWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione DashboardWCF.svc o DashboardWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class DashboardWCF : IDashboardWCF
    {
        public List<gsDocVenta_ReporteVenta_MesResult> DocVenta_ReporteVenta_Mes(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            try
            {
                return objDocVentaBL.DocVenta_ReporteVenta_Mes(idEmpresa, codigoUsuario, fechaInicio, fechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empresa_ListarDashboardResult> Empresa_ListarDashboard()
        {
            EmpresaBL objEmpresaBL = new EmpresaBL();
            try
            {
                return objEmpresaBL.Empresa_ListarDashboard();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DocVenta_Top5Vendedor(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<VendedorTop5BE> lstTop5Venta, ref List<VendedorTop5BE> lstTop5Renta)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            List<gsDocVenta_ReporteVenta_VendedorResult> lstVendedores, lstVendActual, lstRentActual;
            List<VendedorTop5BE> lstVenta = new List<VendedorTop5BE>();
            List<VendedorTop5BE> lstRenta = new List<VendedorTop5BE>();
            int cont = 0;
            try
            {
                lstVendedores = objDocVentaBL.DocVenta_ReporteVenta_Vendedor(idEmpresa, codigoUsuario, null, fechaInicio, fechaFinal);
                lstVendedores.RemoveAll(x => x.ID_Vendedor == "11111111111" || x.ID_Vendedor == "22222222222" || x.ID_Vendedor == "33333333");
                lstVendActual = lstVendedores.FindAll(x => x.Mes == fechaFinal.Month).OrderByDescending(x => x.ValorVenta).Take(5).ToList();
                lstRentActual = lstVendedores.FindAll(x => x.Mes == fechaFinal.Month).OrderByDescending(x => x.Rentabilidad).Take(5).ToList();
                foreach (gsDocVenta_ReporteVenta_VendedorResult vendedor in lstVendActual)
                {
                    VendedorTop5BE top5 = new VendedorTop5BE();
                    if (vendedor.Vendedor.Length >= 30)
                        top5.Vendedor = vendedor.Vendedor.Substring(0, 30);
                    else
                        top5.Vendedor = vendedor.Vendedor;
                    top5.Valorventa = Convert.ToDecimal(Math.Round(Convert.ToDouble(vendedor.ValorVenta) / 1000.0, 0));
                    top5.Ranking = cont + 1;
                    int rankAnt = lstVendedores.FindAll(x => x.Mes == fechaInicio.Month).FindIndex(x => x.ID_Vendedor == vendedor.ID_Vendedor) + 1;
                    if (rankAnt > top5.Ranking)
                        top5.Updown = 1;
                    else
                    {
                        if (rankAnt == top5.Ranking)
                            top5.Updown = 0;
                        else
                            top5.Updown = -1;
                    }
                    cont++;
                    lstVenta.Add(top5);
                }
                lstTop5Venta = lstVenta;

                lstVendedores = lstVendedores.OrderByDescending(x => (x.Año * 100 + x.Mes) * 1000 + x.Rentabilidad).ToList();
                cont = 0;
                foreach (gsDocVenta_ReporteVenta_VendedorResult vendedor in lstRentActual)
                {
                    VendedorTop5BE top5 = new VendedorTop5BE();
                    if (vendedor.Vendedor.Length >= 30)
                        top5.Vendedor = vendedor.Vendedor.Substring(0, 30);
                    else
                        top5.Vendedor = vendedor.Vendedor;
                    top5.Rentabilidad = (decimal)vendedor.Rentabilidad;
                    top5.Ranking = cont + 1;
                    int rankAnt = lstVendedores.FindAll(x => x.Mes == fechaInicio.Month).FindIndex(x => x.ID_Vendedor == vendedor.ID_Vendedor) + 1;
                    if (rankAnt > top5.Ranking)
                        top5.Updown = 1;
                    else
                    {
                        if (rankAnt == top5.Ranking)
                            top5.Updown = 0;
                        else
                            top5.Updown = -1;
                    }
                    cont++;
                    lstRenta.Add(top5);
                }
                lstTop5Renta = lstRenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DocVenta_Top5Cliente(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<VendedorTop5BE> lstTop5Venta, ref List<VendedorTop5BE> lstTop5Renta)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            List<gsDocVenta_ReporteVenta_ClienteTotalResult> lstClientes, lstVentActual, lstRentActual;
            List<VendedorTop5BE> lstVenta = new List<VendedorTop5BE>();
            List<VendedorTop5BE> lstRenta = new List<VendedorTop5BE>();
            int cont = 0;
            try
            {
                lstClientes = objDocVentaBL.DocVenta_ReporteVenta_ClienteTotal(idEmpresa, codigoUsuario, null, fechaInicio, fechaFinal);
                lstClientes.RemoveAll(x => x.ID_Cliente == "20191503482" || x.ID_Cliente == "20509089923");
                lstClientes.RemoveAll(x => string.IsNullOrEmpty(x.Cliente));
                lstVentActual = lstClientes.FindAll(x => x.Mes == fechaFinal.Month).OrderByDescending(x => x.ValorVenta).Take(5).ToList();
                lstRentActual = lstClientes.FindAll(x => x.Mes == fechaFinal.Month).OrderByDescending(x => x.Rentabilidad).Take(5).ToList();
                foreach (gsDocVenta_ReporteVenta_ClienteTotalResult cliente in lstVentActual)
                {
                    VendedorTop5BE top5 = new VendedorTop5BE();
                    if (cliente.Cliente.Length >= 30)
                        top5.Vendedor = cliente.Cliente.Substring(0, 30);
                    else
                        top5.Vendedor = cliente.Cliente;
                    top5.Valorventa = Math.Round((decimal)cliente.ValorVenta / 1000, 0);
                    top5.Ranking = cont + 1;
                    int rankAnt = lstClientes.FindAll(x => x.Mes == fechaInicio.Month).FindIndex(x => x.ID_Cliente == cliente.ID_Cliente) + 1;
                    if (rankAnt > top5.Ranking)
                        top5.Updown = 1;
                    else
                    {
                        if (rankAnt == top5.Ranking)
                            top5.Updown = 0;
                        else
                            top5.Updown = -1;
                    }
                    cont++;
                    lstVenta.Add(top5);
                }
                lstTop5Venta = lstVenta;

                lstClientes = lstClientes.OrderByDescending(x => (x.Año * 100 + x.Mes) * 1000 + x.Rentabilidad).ToList();
                cont = 0;
                foreach (gsDocVenta_ReporteVenta_ClienteTotalResult cliente in lstRentActual)
                {
                    VendedorTop5BE top5 = new VendedorTop5BE();
                    if (cliente.Cliente.Length >= 30)
                        top5.Vendedor = cliente.Cliente.Substring(0, 30);
                    else
                        top5.Vendedor = cliente.Cliente;
                    top5.Rentabilidad = (decimal)cliente.Rentabilidad;
                    top5.Ranking = cont + 1;
                    int rankAnt = lstClientes.FindAll(x => x.Mes == fechaInicio.Month).FindIndex(x => x.ID_Cliente == cliente.ID_Cliente) + 1;
                    if (rankAnt > top5.Ranking)
                        top5.Updown = 1;
                    else
                    {
                        if (rankAnt == top5.Ranking)
                            top5.Updown = 0;
                        else
                            top5.Updown = -1;
                    }
                    cont++;
                    lstRenta.Add(top5);
                }
                lstTop5Renta = lstRenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DocVenta_Top5Producto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<VendedorTop5BE> lstTop5Venta, ref List<VendedorTop5BE> lstTop5Renta)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            List<gsDocVenta_ReporteVenta_ProductoTotalResult> lstProductos, lstVentActual, lstRentActual;
            List<VendedorTop5BE> lstVenta = new List<VendedorTop5BE>();
            List<VendedorTop5BE> lstRenta = new List<VendedorTop5BE>();
            int cont = 0;
            try
            {
                lstProductos = objDocVentaBL.DocVenta_ReporteVenta_ProductoTotal(idEmpresa, codigoUsuario, null, fechaInicio, fechaFinal);
                if (idEmpresa == 1)
                    lstProductos.RemoveAll(x => string.IsNullOrEmpty(x.Descripcion) || x.Kardex == 4104 || x.Kardex == 4105 || x.Kardex == 4106 || x.Kardex == 4107);
                if (idEmpresa == 2)
                    lstProductos.RemoveAll(x => string.IsNullOrEmpty(x.Descripcion) || x.Kardex == 1511 || x.Kardex == 1512 || x.Kardex == 1513 || x.Kardex == 1514);
                lstVentActual = lstProductos.FindAll(x => x.Mes == fechaFinal.Month).OrderByDescending(x => x.ValorVenta).Take(5).ToList();
                lstRentActual = lstProductos.FindAll(x => x.Mes == fechaFinal.Month).OrderByDescending(x => x.Rentabiliad).Take(5).ToList();
                foreach (gsDocVenta_ReporteVenta_ProductoTotalResult producto in lstVentActual)
                {
                    VendedorTop5BE top5 = new VendedorTop5BE();
                    if (producto.Descripcion.Length >= 30)
                        top5.Vendedor = producto.Descripcion.Substring(0, 30);
                    else
                        top5.Vendedor = producto.Descripcion;
                    top5.Valorventa = Math.Round((decimal)producto.ValorVenta / 1000, 0);
                    top5.Ranking = cont + 1;
                    int rankAnt = lstProductos.FindAll(x => x.Mes == fechaInicio.Month).FindIndex(x => x.Kardex == producto.Kardex) + 1;
                    if (rankAnt > top5.Ranking)
                        top5.Updown = 1;
                    else
                    {
                        if (rankAnt == top5.Ranking)
                            top5.Updown = 0;
                        else
                            top5.Updown = -1;
                    }
                    cont++;
                    lstVenta.Add(top5);
                }
                lstTop5Venta = lstVenta;

                lstProductos = lstProductos.OrderByDescending(x => (x.Año * 100 + x.Mes) * 1000 + x.Rentabiliad).ToList();
                cont = 0;
                foreach (gsDocVenta_ReporteVenta_ProductoTotalResult producto in lstRentActual)
                {
                    VendedorTop5BE top5 = new VendedorTop5BE();
                    if (producto.Descripcion.Length >= 30)
                        top5.Vendedor = producto.Descripcion.Substring(0, 30);
                    else
                        top5.Vendedor = producto.Descripcion;
                    top5.Rentabilidad = (decimal)producto.Rentabiliad;
                    top5.Ranking = cont + 1;
                    int rankAnt = lstProductos.FindAll(x => x.Mes == fechaInicio.Month).FindIndex(x => x.Kardex == producto.Kardex) + 1;
                    if (rankAnt > top5.Ranking)
                        top5.Updown = 1;
                    else
                    {
                        if (rankAnt == top5.Ranking)
                            top5.Updown = 0;
                        else
                            top5.Updown = -1;
                    }
                    cont++;
                    lstRenta.Add(top5);
                }
                lstTop5Renta = lstRenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DocVentaDev_ResumenMensual(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<NotaCreditoKPIBE> lstKPI)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            List<gsDocVentaDev_ReporteDevMesResult> lstNotasCredito;
            List<NotaCreditoKPIBE> lstKPIPrevio = new List<NotaCreditoKPIBE>();
            int mes, anho;
            try
            {
                lstNotasCredito = objDocVentaBL.DocVentaDev_ReporteDevMes(idEmpresa, codigoUsuario, null, fechaInicio, fechaFinal);
                mes = fechaInicio.Month;
                anho = fechaInicio.Year;
                for (int i = anho * 100 + mes; i <= fechaFinal.Year * 100 + fechaFinal.Month; i = fechaInicio.Year * 100 + fechaInicio.Month)
                {
                    NotaCreditoKPIBE objNotaCreditoKPIBE = new NotaCreditoKPIBE();
                    objNotaCreditoKPIBE.Periodo = fechaInicio.Year.ToString() + "-" + fechaInicio.Month.ToString();
                    objNotaCreditoKPIBE.Total = (decimal)lstNotasCredito.FindAll(x => x.Mes == fechaInicio.Month && x.Anho == fechaInicio.Year).Sum(x => x.Neto);
                    objNotaCreditoKPIBE.TotalMes = (decimal)lstNotasCredito.FindAll(x => x.Mes == fechaInicio.Month && x.Anho == fechaInicio.Year && x.MesOrigen == fechaInicio.Month && x.AnhoOrigen == fechaInicio.Year).Sum(x => x.Neto);
                    fechaInicio = fechaInicio.AddMonths(1);
                    if (objNotaCreditoKPIBE.Total != 0)
                        lstKPIPrevio.Add(objNotaCreditoKPIBE);
                }
                lstKPI = lstKPIPrevio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DocVentaDev_KPIMotivo(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal, ref List<DocVentaDev_KPIMotivoBE> lstMotivo)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            List<gsDocVentaDev_ReporteDevMotivoResult> lstMotivoDet;
            List<DocVentaDev_KPIMotivoBE> lst = new List<DocVentaDev_KPIMotivoBE>();
            decimal total = 0;
            int conta = 0;
            try
            {
                lstMotivoDet = objDocVentaBL.DocVentaDev_ReporteDevMotivo(idEmpresa, codigoUsuario, null, fechaInicio, fechaFinal);
                if (lstMotivoDet.FindAll(x => x.Mes == fechaFinal.Month && x.Anho == fechaFinal.Year).Sum(x => x.Neto) == 0)
                    lstMotivoDet = lstMotivoDet.FindAll(x => x.Mes == fechaInicio.Month && x.Anho == fechaInicio.Year);
                else
                    lstMotivoDet = lstMotivoDet.FindAll(x => x.Mes == fechaFinal.Month && x.Anho == fechaFinal.Year);
                lstMotivoDet = lstMotivoDet.OrderByDescending(x => x.Neto).ToList();
                total = (decimal)lstMotivoDet.Sum(x => x.Neto);
                foreach (gsDocVentaDev_ReporteDevMotivoResult motivo in lstMotivoDet)
                {
                    DocVentaDev_KPIMotivoBE objMotivoBE = new DocVentaDev_KPIMotivoBE();
                    objMotivoBE.Motivo = motivo.Motivo.ToUpper();
                    objMotivoBE.Porcentaje = Math.Round((decimal)(motivo.Neto / total) * 100, 0);
                    objMotivoBE.Monto = Math.Round((decimal)motivo.Neto, 2);
                    lst.Add(objMotivoBE);
                    conta++;
                    if (conta == 5)
                        break;
                }
                if (conta == 5)
                {
                    DocVentaDev_KPIMotivoBE objMotivoBE = new DocVentaDev_KPIMotivoBE();
                    objMotivoBE.Motivo = "OTROS";
                    objMotivoBE.Porcentaje = 100 - lst.Sum(x => x.Porcentaje);
                    objMotivoBE.Monto = total - lst.Sum(x => x.Monto);
                    lst.Add(objMotivoBE);
                }
                lstMotivo = lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Noticia_PublicarDashboard(DateTime fecha, ref List<Noticia_PublicarDashboardResult> lstNoticia, ref List<NoticiaFoto_PublicarDashboardResult> lstNoticiaFoto)
        {
            NoticiaBL objNoticiaBL = new NoticiaBL();
            try
            {
                lstNoticia = objNoticiaBL.Noticia_PublicarDashboard(fecha, ref lstNoticiaFoto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Personal_ListarCumpleanhosResult> Personal_ListarCumpleanhos(DateTime fecha)
        {
            RRHHBL objRRHHBL = new RRHHBL();
            try
            {
                return objRRHHBL.Personal_ListarCumpleanhos(fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Aviso_PublicarDashboardResult> Aviso_PublicarDashboard(DateTime fecha)
        {
            AvisoBL objAvisoBL = new AvisoBL();
            try
            {
                return objAvisoBL.Aviso_PublicarDashboard(fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rentabilidad_Producto(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal,
            ref List<DocVenta_RentaProdBE> lstRentaProd, ref gsConfigIndicadores_RentabilidadResult objConfigIndRenta)
        {
            DocVentaBL objDocVentaBL = new DocVentaBL();
            List<DocVenta_RentaProdBE> lstRenta = new List<DocVenta_RentaProdBE>();
            try
            {
                ConfigIndicadoresBL objConfigIndicadoresBL = new ConfigIndicadoresBL();
                objConfigIndRenta = objConfigIndicadoresBL.ConfigIndicadores_Rentabilidad(idEmpresa, codigoUsuario, fechaFinal);

                gsDocVenta_ReporteVenta_MesResult mes = objDocVentaBL.DocVenta_ReporteVenta_Mes(idEmpresa, codigoUsuario, fechaInicio, fechaFinal).Single();
                List<gsDocVenta_ReporteVenta_MarcaProductoResult> lstProductos = objDocVentaBL.DocVenta_ReporteVenta_MarcaProducto(idEmpresa, codigoUsuario, null, null, null, fechaInicio, fechaFinal).FindAll(x => !string.IsNullOrEmpty(x.Descripcion));

                foreach (gsDocVenta_ReporteVenta_MarcaProductoResult producto in lstProductos)
                {
                    DocVenta_RentaProdBE objProductoBE = new DocVenta_RentaProdBE();
                    objProductoBE.Kardex = Convert.ToInt32(producto.Kardex);
                    objProductoBE.Descripcion = producto.Descripcion.Replace('\n', ' ');
                    objProductoBE.Rentabilidad = (decimal)producto.Rentabilidad;
                    if ((decimal)mes.ValorVenta > 0)
                        objProductoBE.PorcVenta = (decimal)producto.ValorVenta / (decimal)mes.ValorVenta * 100;
                    else
                        objProductoBE.PorcVenta = 0;
                    if (objProductoBE.Rentabilidad >= objConfigIndRenta.rentabilidadAlta)
                    {
                        if (objProductoBE.PorcVenta >= objConfigIndRenta.ventaAlta)
                            objProductoBE.Categoria = "VARA";
                        else
                            objProductoBE.Categoria = "VBRA";
                    }
                    else
                    {
                        if (objProductoBE.PorcVenta >= objConfigIndRenta.ventaAlta)
                            objProductoBE.Categoria = "VARB";
                        else
                            objProductoBE.Categoria = "VBRB";
                    }
                    objProductoBE.ValorVenta = (decimal)producto.ValorVenta;
                    lstRenta.Add(objProductoBE);
                }
                lstRentaProd = lstRenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
