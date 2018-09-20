using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS.SISGEGS.UT.DashboardWCF;
using GS.SISGEGS.DM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GS.SISGEGS.BE;

namespace GS.SISGEGS.UT
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void Empresa_Dashboard()
        {
            DashboardWCFClient objDashboardWCF = new DashboardWCFClient();
            List<Empresa_ListarDashboardResult> lstEmpresa;
            try {
                lstEmpresa = objDashboardWCF.Empresa_ListarDashboard().ToList();
                Assert.Equals(2, lstEmpresa.Count);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [TestMethod]
        public void Top5Vendedores()
        {
            DashboardWCFClient objDashboardWCF = new DashboardWCFClient();
            VendedorTop5BE[] lstVendedor = null;
            VendedorTop5BE[] lstRenta = null;
            DateTime fechaInicio, fechaFinal;
            try
            {
                fechaInicio = DateTime.Now.Date.AddDays(1 - DateTime.Now.Date.Day).AddMonths(-1);
                fechaFinal = fechaInicio.AddMonths(2).AddDays(-1);    
                objDashboardWCF.DocVenta_Top5Producto(2, 1, fechaInicio, fechaFinal, ref lstVendedor, ref lstRenta);
                Assert.Equals(5, lstVendedor.ToList().Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void Top5Clientes()
        {
            DashboardWCFClient objDashboardWCF = new DashboardWCFClient();
            VendedorTop5BE[] lstVendedor = null;
            VendedorTop5BE[] lstRenta = null;
            DateTime fechaInicio, fechaFinal;
            try
            {
                fechaInicio = new DateTime(2016, 4, 1);
                fechaFinal = fechaInicio.AddMonths(2).AddDays(-1);
                objDashboardWCF.DocVenta_Top5Cliente(1, 1, fechaInicio, fechaFinal, ref lstVendedor, ref lstRenta);
                Assert.Equals(5, lstVendedor.ToList().Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void KPINotasCredito()
        {
            DashboardWCFClient objDashboardWCF = new DashboardWCFClient();
            NotaCreditoKPIBE[] lstKPI = null;            
            DateTime fechaInicio, fechaFinal;
            try
            {
                fechaInicio = DateTime.Now.Date.AddDays(1 - DateTime.Now.Date.Day).AddMonths(-12);
                fechaFinal = fechaInicio.AddMonths(12).AddDays(-1);
                objDashboardWCF.DocVentaDev_ResumenMensual(2, 1, fechaInicio, fechaFinal, ref lstKPI);
                Assert.Equals(5, lstKPI.ToList().Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void PieNotaCreditoi()
        {
            DashboardWCFClient objDashboardWCF = new DashboardWCFClient();
            DocVentaDev_KPIMotivoBE[] lstKPI = null;
            DateTime fechaInicio, fechaFinal;
            try
            {
                if (DateTime.Now.Day > 5)
                    fechaInicio = DateTime.Now.Date.AddDays(1 - DateTime.Now.Date.Day).AddMonths(-1);
                else
                    fechaInicio = DateTime.Now.Date.AddDays(1 - DateTime.Now.Date.Day).AddMonths(-2);
                fechaFinal = fechaInicio.AddMonths(2).AddDays(-1);
                objDashboardWCF.DocVentaDev_KPIMotivo(2, 1, fechaInicio, fechaFinal, ref lstKPI);
                Assert.Equals(5, lstKPI.ToList().Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
