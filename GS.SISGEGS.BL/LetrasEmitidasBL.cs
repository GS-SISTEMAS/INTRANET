using System;
using System.Collections.Generic;
using System.Linq;
using GS.SISGEGS.DM;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.BL
{
    public interface ILetrasEmitidasBL
    {
        List<gsLetrasEmitidas_ListarResult> LetrasEmitidas_Listar(int idEmpresa, int codigoUsuario, string codAgenda, string opFinan, DateTime fechaInicio, DateTime fechaFin);
        List<gsLetrasEmitidas_CabeceraResult> LetrasEmitidas_Cabecera(int idEmpresa, int codigoUsuario, string opFinan);
        List<gsLetrasEmitidas_DocumentosResult> LetrasEmitidas_Documentos(int idEmpresa, int codigoUsuario, string opFinan);
        List<gsLetrasEmitidas_LetrasResult> LetrasEmitidas_Letras(int idEmpresa, int codigoUsuario, string opFinan);
        List<gsProcesoLetras_ListarResult> ProcesoLetras_NumerosUnicos(int idEmpresa, int codigoUsuario, int anho, int mes, string descripcion);

        void NumerosUnicos_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla);

        int ProcesoLetras_NumerosUnicos_Insertar(int idEmpresa, int codigoUsuario, int anho, int mes, int dia, string descripcion, int ID);

        void NumerosUnicos_Registrar_Proceso(int idEmpresa, int codigoUsuario);

        List<gsNumerosUnicos_ListarExportarResult> ProcesoLetras_NumerosUnicos_Listar(int idEmpresa, int codigoUsuario, int id_proceso);

        List<gsLetrasElectronicas_ListarResult> LetrasElectronicas_Listar(int idEmpresa, int codigoUsuario, int id_OP);

        List<VBG01425Result> LetrasElectronicas_Individual(int idEmpresa, int codigoUsuario, int id_Letra);

        void Registrar_LogLetrasDescargadas(int idEmpresa, int codigoUsuario, Int32 idletra, string usuariointranet, Int32 Op_DocVenta);

        string CanjeAutomaticoLetras_Registrar(string Empresa, int ID_Letra, string Usuario);
    }

    public class LetrasEmitidasBL
    {
        public List<gsLetrasEmitidas_ListarResult> LetrasEmitidas_Listar(int idEmpresa, int codigoUsuario, string codAgenda, string opFinan, DateTime fechaInicio, DateTime fechaFin)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsLetrasEmitidas_ListarResult> list = dcg.gsLetrasEmitidas_Listar(opFinan, fechaInicio, fechaFin, codAgenda).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        public List<gsLetrasEmitidas_CabeceraResult> LetrasEmitidas_Cabecera(int idEmpresa, int codigoUsuario, string opFinan )
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsLetrasEmitidas_CabeceraResult> list = dcg.gsLetrasEmitidas_Cabecera(opFinan).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        public List<gsLetrasEmitidas_DocumentosResult> LetrasEmitidas_Documentos(int idEmpresa, int codigoUsuario, string opFinan)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsLetrasEmitidas_DocumentosResult> list = dcg.gsLetrasEmitidas_Documentos(opFinan).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        public List<gsLetrasEmitidas_LetrasResult> LetrasEmitidas_Letras(int idEmpresa, int codigoUsuario, string opFinan)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsLetrasEmitidas_LetrasResult> list = dcg.gsLetrasEmitidas_Letras(opFinan).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar las letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        // Numeros unicos
        public List<gsProcesoLetras_ListarResult> ProcesoLetras_NumerosUnicos(int idEmpresa, int codigoUsuario, int anho, int mes , string descripcion)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<gsProcesoLetras_ListarResult > list = dcg.gsProcesoLetras_Listar(anho, mes, descripcion).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Procesos de letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        public void NumerosUnicos_RegistrarBulkCopy(int idEmpresa, int codigoUsuario, DataTable tabla)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                SqlConnection conn;
                try
                {
     
                    dci.Connection.Open();
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.Connection.Open();
                    dcg.CommandTimeout = 60;

                    conn = new SqlConnection(dcg.Connection.ConnectionString);

                    using (SqlConnection connection = conn)
                    {
                        connection.Open();
                        string commandText = "delete gstblLetras_NumeroUnicos_TEMP ";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        //command.Parameters.Add("@ID_zona", SqlDbType.Int);
                        //command.Parameters["@ID_zona"].Value = Id_Zona;

                        command.ExecuteNonQuery();


                        // Create a table with some rows. 
                        DataTable newReporte = tabla;

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "dbo.gstblLetras_NumeroUnicos_TEMP";

                            try
                            {
                                bulkCopy.WriteToServer(newReporte);
                            }
                            catch (Exception ex)
                            {
                                string mensaje = ex.Message + " - " + ex.TargetSite.Name;
                                throw new ArgumentException("Error al momento de registrar el pronostico masivo en la base de datos.");
                            }
                        }


                        // Cargar Tabla Pronostico
                        //string commandText2 = "delete Doc_VentaProyectado where Año = @Anho and ID_Zona = @ID_Zona; ";
                        //SqlCommand command2 = new SqlCommand(commandText2, connection);
                        //command2.Parameters.Add("@ID_zona", SqlDbType.Int);
                        //command2.Parameters["@ID_zona"].Value = Id_Zona;
                        //command2.Parameters.Add("@Anho", SqlDbType.Int);
                        //command2.Parameters["@Anho"].Value = Anho;
                        //command2.ExecuteNonQuery();


                        //// Cargar Tabla Pronostico
                        //string commandText3 = "insert Doc_VentaProyectado "
                        //                      + " select Fecha, Item_ID, Precio, Costo, Cantidad, Importe, Aprobacion1, Aprobacion2, Aprobacion1Fecha, Aprobacion2Fecha,  "
                        //                       + "Aprobacion1CodUsuario, Aprobacion2CodUsuario, UsuarioAcceso, UsuarioAccesoFecha, Año, Trimestre, Mes, Semana, Dia, ID_Zona, ID_Vendedor, "
                        //                       + "ID_Cliente, ID_Moneda  from Doc_VentaProyectadoTEMP "
                        //                       + "where Año = @Anho and ID_Zona = @ID_Zona "
                        //                       + "; ";

                        //SqlCommand command3 = new SqlCommand(commandText3, connection);
                        //command3.Parameters.Add("@ID_zona", SqlDbType.Int);
                        //command3.Parameters["@ID_zona"].Value = Id_Zona;
                        //command3.Parameters.Add("@Anho", SqlDbType.Int);
                        //command3.Parameters["@Anho"].Value = Anho;
                        //command3.ExecuteNonQuery();

 

                        connection.Close();

                    }

                    dcg.Connection.Close();
                    dci.Connection.Close();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de registrar las cobranzas en la base de datos.");
                }

            }
        }

        public int ProcesoLetras_NumerosUnicos_Insertar(int idEmpresa, int codigoUsuario, int anho, int mes, int dia, string descripcion, int ID)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    int ID_PROCESO = 0;
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    ID_PROCESO = dcg.gsProcesoLetras_Insertar(ID, anho, mes, dia, codigoUsuario, descripcion);

                    return ID_PROCESO; 
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Procesos de letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        public void NumerosUnicos_Registrar_Proceso(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.CommandTimeout = 120;

                    dcg.gsNumerosUnicos_Registrar();

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Procesos de letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        public List<gsNumerosUnicos_ListarExportarResult> ProcesoLetras_NumerosUnicos_Listar(int idEmpresa, int codigoUsuario, int id_proceso)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<gsNumerosUnicos_ListarExportarResult> list = dcg.gsNumerosUnicos_ListarExportar(id_proceso).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Procesos de letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        // ----- Letra Electronica

        public List<gsLetrasElectronicas_ListarResult> LetrasElectronicas_Listar(int idEmpresa, int codigoUsuario, int id_OP)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    List<gsLetrasElectronicas_ListarResult> list = dcg.gsLetrasElectronicas_Listar(id_OP).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Procesos de letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }


        public List<VBG01425Result> LetrasElectronicas_Individual(int idEmpresa, int codigoUsuario, int id_Letra)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));

                    string Moneda = null;
                    decimal? Total = 0;
                    bool? OK = null;

                    List<VBG01425Result> list = dcg.VBG01425(id_Letra, ref Moneda, ref Total, ref OK, 1).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Procesos de letras emitidas en la base de datos.");
                }
                finally
                {

                }
            }
        }

        public void Registrar_LogLetrasDescargadas(int idEmpresa, int codigoUsuario,Int32 idletra,string usuariointranet,Int32 Op_DocVenta)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dcg.USP_INS_LogDescargaLetra_V2(idletra, DateTime.Now, codigoUsuario, usuariointranet, Op_DocVenta);

                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al registrar el usuario de descarga");
                }
                finally
                {

                }
            }
        }





        public List<USP_SEL_Canje_Automatico_LetrasResult> CanjeAutomaticoLetras_Listar(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, string Estado)
        {
            try
            {
                ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {

                    List<USP_SEL_Canje_Automatico_LetrasResult> listLetras = new List<USP_SEL_Canje_Automatico_LetrasResult>();
                    try
                    {
                        listLetras = dci.USP_SEL_Canje_Automatico_Letras(idEmpresa, fechaInicio, fechaFin, Estado).ToList();
                        return listLetras;
                    }
                    catch (Exception ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        dci.SubmitChanges();
                        throw new ArgumentException("Error al consultar Letras");
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string CanjeAutomaticoLetras_Registrar(string Empresa, int ID_Letra, string Usuario)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.USP_INS_Canje_Automatico_Letras(Empresa, ID_Letra, Usuario).ToList()[0].RESULT.ToString();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }


        public int Financiamiento_CA_Letras_General(string Empresa)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.USP_INS_Financiamiento_CA_Letras_General(Empresa);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    throw ex;
                }
                finally
                {
                    dci.SubmitChanges();
                }
            }
        }


        public List<USP_SEL_Estado_LetrasResult> Estado_Letras_Listar(string idEmpresa)
        {
            try
            {
                ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
                using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
                {

                    List<USP_SEL_Estado_LetrasResult> listLetras = new List<USP_SEL_Estado_LetrasResult>();
                    try
                    {
                        listLetras = dci.USP_SEL_Estado_Letras(idEmpresa).ToList();
                        return listLetras;
                    }
                    catch (Exception ex)
                    {
                        dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                        dci.SubmitChanges();
                        throw new ArgumentException("Error al consultar Letras");
                    }
                    finally
                    {
                        dci.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USP_SEL_Porcentaje_Avance_LetrasResult> Porcentaje_Avance_Letras_Lista(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<USP_SEL_Porcentaje_Avance_LetrasResult> list = dcg.USP_SEL_Porcentaje_Avance_Letras().ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Porcentajes de Avance de Letras .");
                }
                finally
                {

                }
            }
        }

        public List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult> Porcentaje_Avance_Letras_Lista_x_Zonas(int idEmpresa, int codigoUsuario)
        {
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    //dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult> list = dcg.USP_SEL_Porcentaje_Avance_Letras_Zonas().ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al momento de consultar los Porcentajes de Avance de Letras .");
                }
                finally
                {

                }
            }
        }



    }
}
