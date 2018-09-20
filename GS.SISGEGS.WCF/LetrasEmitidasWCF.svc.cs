using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GS.SISGEGS.DM;
using GS.SISGEGS.BL;
using System.Data;
 

namespace GS.SISGEGS.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "LetrasEmitidasWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione LetrasEmitidasWCF.svc o LetrasEmitidasWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class LetrasEmitidasWCF : ILetrasEmitidasWCF
    {
        public List<gsLetrasEmitidas_ListarResult> LetrasEmitidas_Listar(int idEmpresa, int codigoUsuario, string codAgenda, string opFinan, DateTime fechaInicial, DateTime fechaFinal)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<gsLetrasEmitidas_ListarResult> list = new List<gsLetrasEmitidas_ListarResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.LetrasEmitidas_Listar(idEmpresa, codigoUsuario, codAgenda, opFinan, fechaInicial, fechaFinal);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsLetrasEmitidas_CabeceraResult> LetrasEmitidas_Cabecera(int idEmpresa, int codigoUsuario, string opFinan)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<gsLetrasEmitidas_CabeceraResult> list = new List<gsLetrasEmitidas_CabeceraResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.LetrasEmitidas_Cabecera(idEmpresa, codigoUsuario, opFinan);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsLetrasEmitidas_DocumentosResult> LetrasEmitidas_Documentos(int idEmpresa, int codigoUsuario, string opFinan)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<gsLetrasEmitidas_DocumentosResult> list = new List<gsLetrasEmitidas_DocumentosResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.LetrasEmitidas_Documentos(idEmpresa, codigoUsuario, opFinan);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<gsLetrasEmitidas_LetrasResult> LetrasEmitidas_Letras(int idEmpresa, int codigoUsuario, string opFinan)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<gsLetrasEmitidas_LetrasResult> list = new List<gsLetrasEmitidas_LetrasResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.LetrasEmitidas_Letras(idEmpresa, codigoUsuario, opFinan);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsProcesoLetras_ListarResult> ProcesoLetras_NumerosUnicos(int idEmpresa, int codigoUsuario, int anho, int mes, string descripcion)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<gsProcesoLetras_ListarResult> list = new List<gsProcesoLetras_ListarResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.ProcesoLetras_NumerosUnicos(idEmpresa, codigoUsuario, anho,mes,descripcion);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void NumerosUnicos_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;
            try
            {
                objLetrasEmitidasBL = new LetrasEmitidasBL();
                objLetrasEmitidasBL.NumerosUnicos_RegistrarBulkCopy(idEmpresa, codigoUsuario,  tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int  ProcesoLetras_NumerosUnicos_Insertar(int idEmpresa, int codigoUsuario, int anho, int mes, int dia, string descripcion, int ID)
        {
            int ID_PROCESO = 0; 
            LetrasEmitidasBL objLetrasEmitidasBL;
            try
            {
                objLetrasEmitidasBL = new LetrasEmitidasBL();
                ID_PROCESO = objLetrasEmitidasBL.ProcesoLetras_NumerosUnicos_Insertar(idEmpresa, codigoUsuario, anho, mes,dia , descripcion, ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ID_PROCESO; 
        }

        public void NumerosUnicos_Registrar_Proceso(int idEmpresa, int codigoUsuario)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;
            try
            {
                objLetrasEmitidasBL = new LetrasEmitidasBL();
                objLetrasEmitidasBL.NumerosUnicos_Registrar_Proceso(idEmpresa, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<gsNumerosUnicos_ListarExportarResult> ProcesoLetras_NumerosUnicos_Listar(int idEmpresa, int codigoUsuario, int id_proceso)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<gsNumerosUnicos_ListarExportarResult> list = new List<gsNumerosUnicos_ListarExportarResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.ProcesoLetras_NumerosUnicos_Listar(idEmpresa, codigoUsuario, id_proceso);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VBG01425Result> LetrasElectronicas_Individual(int idEmpresa, int codigoUsuario, int id_Letra)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<VBG01425Result> list = new List<VBG01425Result>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.LetrasElectronicas_Individual(idEmpresa, codigoUsuario, id_Letra);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<gsLetrasElectronicas_ListarResult> LetrasElectronicas_Listar(int idEmpresa, int codigoUsuario, int id_OP)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<gsLetrasElectronicas_ListarResult> list = new List<gsLetrasElectronicas_ListarResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.LetrasElectronicas_Listar(idEmpresa, codigoUsuario, id_OP);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Registrar_LogLetrasDescargadas(int idEmpresa, int codigoUsuario, Int32 idletra, string usuariointranet, Int32 Op_DocVenta)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;
            try
            {
                

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                objLetrasEmitidasBL.Registrar_LogLetrasDescargadas(idEmpresa, codigoUsuario, idletra, usuariointranet, Op_DocVenta);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<USP_SEL_Canje_Automatico_LetrasResult> CanjeAutomaticoLetras_Listar(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, string Estado)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<USP_SEL_Canje_Automatico_LetrasResult> list = new List<USP_SEL_Canje_Automatico_LetrasResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.CanjeAutomaticoLetras_Listar(idEmpresa, fechaInicio, fechaFin, Estado);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CanjeAutomaticoLetras_Registrar(string Empresa, int ID_Letra, string Usuario)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;
            try
            {
                objLetrasEmitidasBL = new LetrasEmitidasBL();
                return objLetrasEmitidasBL.CanjeAutomaticoLetras_Registrar(Empresa, ID_Letra, Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Financiamiento_CA_Letras_General(string Empresa)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;
            try
            {
                objLetrasEmitidasBL = new LetrasEmitidasBL();
                return objLetrasEmitidasBL.Financiamiento_CA_Letras_General(Empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<USP_SEL_Estado_LetrasResult> Estado_Letras_Listar(string idEmpresa)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<USP_SEL_Estado_LetrasResult> list = new List<USP_SEL_Estado_LetrasResult>();

                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.Estado_Letras_Listar(idEmpresa);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<USP_SEL_Porcentaje_Avance_LetrasResult> Porcentaje_Avance_Letras_Lista(int idEmpresa, int codigoUsuario)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<USP_SEL_Porcentaje_Avance_LetrasResult> list = new List<USP_SEL_Porcentaje_Avance_LetrasResult>();
                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.Porcentaje_Avance_Letras_Lista(idEmpresa, codigoUsuario);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult> Porcentaje_Avance_Letras_Lista_x_Zonas(int idEmpresa, int codigoUsuario)
        {
            LetrasEmitidasBL objLetrasEmitidasBL;

            try
            {
                List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult> list = new List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult>();
                objLetrasEmitidasBL = new LetrasEmitidasBL();
                list = objLetrasEmitidasBL.Porcentaje_Avance_Letras_Lista_x_Zonas(idEmpresa, codigoUsuario);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
