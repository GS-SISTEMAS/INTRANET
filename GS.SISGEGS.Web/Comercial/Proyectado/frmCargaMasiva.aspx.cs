using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.ReporteVentaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Data;
using System.Data.OleDb;

using System.Data.Odbc;
using System.Data.Sql;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.IO;

namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmCargaMasiva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    //Empresa_Cargar();
                    //cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                    if (Request.QueryString["objPerfil"] == "") {
                        Title = "Registrar pronóstico";
                        ///*cboEmpresa*/.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para carga masiva de pronóstico: ";
                        lblMensaje.CssClass = "mensajeExito";
                    }

                    //ScriptManager sm = ScriptManager.GetCurrent(Page);
                    //if (sm != null) sm.RegisterPostBackControl(btnCargaMasivaArchivo);
                    //AddPostBackTrigger(btnCargaMasivaArchivo.UniqueID);


                }
            }
            catch (Exception ex) {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void RadAsyncUpload1_FileUploaded1(object sender, FileUploadedEventArgs e)
        {
            string fullPath;
            string Destino = "";
            string savePath = "c:\\temp\\uploads\\";

            List<DataTable> tablas = new List<DataTable>();
            DataTable tabla = new DataTable();

            try
            {
                if (Panel1.CssClass == "background1")
                {
                    Panel1.CssClass = "background2";
                }
                else
                {
                    Panel1.CssClass = "background1";
                }

                //simulate longer page load
                //System.Threading.Thread.Sleep(2000);
                string strFecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" ;
               

                fullPath = Path.Combine(savePath, strFecha +  e.File.GetName());


                e.File.SaveAs(fullPath);

                Destino = fullPath;

                tabla = LEER_EXCEL(Destino, "Pronostico");
                tablas = REGISTRAR_EXCEL(tabla);
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
         }

        DataTable LEER_EXCEL(string Directorio, string nombreAchivo)
        {
            string Extencion = "";
            string connectionString = ""; 

            Extencion = Path.GetExtension(Directorio);

            if(Extencion == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directorio + ";Extended Properties=Excel 8.0;";
            }
            else if (Extencion == ".xlsx")
            {
                connectionString = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Directorio + "';Extended Properties=Excel 12.0;";
            }


            OleDbConnection _connection = new OleDbConnection();
            OleDbDataAdapter _dataAdatpter = new OleDbDataAdapter();
            DataSet _ds = new DataSet();
            SqlCommand _command = new SqlCommand() ;
            DataTable dt = new DataTable();

            

            try
            {
                _connection = new OleDbConnection(connectionString);
                _dataAdatpter = new OleDbDataAdapter("SELECT * FROM [" + nombreAchivo + "$A4:AA550]", _connection);
                _dataAdatpter.Fill(_ds);
                dt =  _ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error de carga. " + ex.Message.ToString();
                lblMensaje.CssClass = "Error";
            }
            finally
            {
                if(_dataAdatpter != null)
                { _dataAdatpter.Dispose(); }
                
                if( _connection.State == ConnectionState.Open)
                { _connection.Close(); }

                if (_connection != null)
                { _connection.Dispose();
                }
                _connection = null;
                _command = null;
            }
            return dt; 
        }

        DataTable FormatoTabla()
        {
            DataTable dtTabla = new DataTable() ;

            DataColumn columnFecha1 = new  DataColumn();
            columnFecha1.ColumnName = "Fecha";
            columnFecha1.DataType = System.Type.GetType("System.DateTime");

            DataColumn columnFecha2 = new DataColumn();
            columnFecha2.ColumnName = "Aprobacion1Fecha";
            columnFecha2.DataType = System.Type.GetType("System.DateTime");

            DataColumn columnFecha3 = new DataColumn();
            columnFecha3.ColumnName = "Aprobacion2Fecha";
            columnFecha3.DataType = System.Type.GetType("System.DateTime");

            DataColumn columnFecha4 = new DataColumn();
            columnFecha4.ColumnName = "UsuarioAccesoFecha";
            columnFecha4.DataType = System.Type.GetType("System.DateTime");

            dtTabla.Columns.Add("ID_Pronostico");
            dtTabla.Columns.Add(columnFecha1);
            dtTabla.Columns.Add("Item_ID");
            dtTabla.Columns.Add("Precio");
            dtTabla.Columns.Add("Costo");
            dtTabla.Columns.Add("Cantidad");
            dtTabla.Columns.Add("Importe");
            dtTabla.Columns.Add("Aprobacion1");
            dtTabla.Columns.Add("Aprobacion2");
            dtTabla.Columns.Add(columnFecha2);
            dtTabla.Columns.Add(columnFecha3);
            dtTabla.Columns.Add("Aprobacion1CodUsuario");
            dtTabla.Columns.Add("Aprobacion2CodUsuario");
            dtTabla.Columns.Add("UsuarioAcceso");
            dtTabla.Columns.Add(columnFecha4);
            dtTabla.Columns.Add("Año");
            dtTabla.Columns.Add("Trimestre");
            dtTabla.Columns.Add("Mes");
            dtTabla.Columns.Add("Semana");
            dtTabla.Columns.Add("Dia");
            dtTabla.Columns.Add("ID_Zona");
            dtTabla.Columns.Add("ID_Vendedor");
            dtTabla.Columns.Add("ID_Cliente");
            dtTabla.Columns.Add("ID_Moneda");
            return dtTabla;

        }

        List<DataTable>REGISTRAR_EXCEL(DataTable tabla)
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient(); 

            List<DataTable> lista = new List<DataTable>();
            DataTable dtGral;
            DataTable dtError;
            DataTable dtEnvio = new DataTable(); 

            //string codVendedor;
            int codZona;
            string Periodo;
            int empresa, usuario;
            int anho;
            int mes;
            decimal MesCantidad; 
            string strMes;
            int id_zonaf = 0 ;
            string id_vendedorf = "";
            decimal precio = 0;
            decimal defPrecio = 0;
            string cortar = "";
            DateTime Fecha;
            int Kardex;
            DateTime FechaCarga = new DateTime();
            int contar = 0;
            string stringPeriodo;
            int yearActual;
            int mesInicial, mesFinal, mesDefoult;
            int yearAnterior, yearDefoult;

            try
            {
                dtEnvio = FormatoTabla();
                empresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                dtGral = tabla;
                dtError = dtGral.Clone();
                dtError.Rows.Clear();
                dtError.Columns.Add("Comentario");
                int lent = 0;



                for (int n = 0; n < dtGral.Rows.Count; n++)
                {
                    var strID_Zona = dtGral.Rows[n]["Id_Zona"];
                    if (string.IsNullOrEmpty(strID_Zona.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        codZona = Convert.ToInt32(dtGral.Rows[n]["Id_Zona"].ToString());

                        contar++;

                        if (contar == 247)
                        {
                            contar = contar;
                        }

                        if (codZona > 0)
                        {

                            codZona = int.Parse(dtGral.Rows[n]["Id_Zona"].ToString());
                            id_zonaf = codZona;

                            Kardex = int.Parse(dtGral.Rows[n]["Kardex"].ToString());

                            if(Kardex == 896)
                            {
                                contar++;
                                contar--;
                            }


                            try
                            {
                                precio = decimal.Parse(dtGral.Rows[n]["Precio"].ToString());
                            }
                            catch
                            {
                                precio = 0;
                            }

                            Periodo = dtGral.Rows[n]["Periodo"].ToString();
                            anho = int.Parse(Periodo.Substring(0, 4));
                            mes = 1;

                            FechaCarga = Convert.ToDateTime(anho.ToString() + "/01/01");



                            yearActual = anho;
                            mesFinal = 12;
                            yearAnterior = yearActual - 1;
                            mesInicial = 1;
                            mesDefoult = mesInicial;
                            yearDefoult = yearActual;

                            for (int y = 1; y < 13; y++)
                            {

                                if (mesDefoult > 12)
                                {
                                    mesDefoult = 1;
                                    yearDefoult = yearActual + 1;
                                    stringPeriodo = yearDefoult + "_" + mesDefoult;
                                }
                                else
                                {
                                    stringPeriodo = yearDefoult + "_" + mesDefoult;
                                }

                                strMes = mesDefoult.ToString();
                                if (strMes.Length == 1)
                                {
                                    strMes = "0" + strMes;
                                }

                                Fecha = Convert.ToDateTime(yearDefoult + "/" + strMes + "/" + "01");
                                string strFecha = yearDefoult + "/" + strMes + "/" + "01";
                                string strCantidad;
                                strCantidad = dtGral.Rows[n][stringPeriodo].ToString();

                                if (strCantidad.Length == 0)
                                {
                                    MesCantidad = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        MesCantidad = decimal.Parse(strCantidad);
                                    }
                                    catch
                                    {
                                        MesCantidad = 0;
                                    }
                                }


                                DataRow newRow = dtEnvio.NewRow();


                                newRow["Fecha"] = strFecha;
                                newRow["Item_ID"] = Kardex;
                                newRow["Precio"] = precio;
                                newRow["Costo"] = 0;

                                newRow["Cantidad"] = MesCantidad;
                                newRow["Importe"] = (precio * MesCantidad);
                                newRow["Aprobacion1"] = true;
                                newRow["Aprobacion2"] = true;
                                newRow["Aprobacion1Fecha"] = DateTime.Now;
                                newRow["Aprobacion2Fecha"] = DateTime.Now;
                                newRow["Aprobacion1CodUsuario"] = usuario;

                                newRow["Aprobacion2CodUsuario"] = usuario;
                                newRow["UsuarioAcceso"] = usuario;

                                newRow["UsuarioAccesoFecha"] = DateTime.Now;

                                newRow["Año"] = yearDefoult;
                                newRow["Trimestre"] = null;
                                newRow["Mes"] = mesDefoult;
                                newRow["Semana"] = null;
                                newRow["Dia"] = null;
                                newRow["ID_Zona"] = codZona;
                                newRow["ID_Vendedor"] = null;
                                newRow["ID_Cliente"] = null;
                                newRow["ID_Moneda"] = 0;

                                //objReporteVentaWCF.ProyectadoVentas_Registrar(empresa, usuario, 0, Fecha, Kardex, 0, 0, MesCantidad, 0, true , year3, mesDefoult, codZona, codVendedor, null, 0);

                                dtEnvio.Rows.Add(newRow);

                                mesDefoult = mesDefoult + 1;
                            }
                        }
                    }
                      
                    
                }


                if(dtEnvio.Rows.Count > 0 )
                {
                    dtEnvio.TableName = "dtEnvio"; 
                    objReporteVentaWCF.ProyectadoVentas_RegistrarBulkCopy(empresa, usuario, FechaCarga, id_zonaf, id_vendedorf, null, 0, dtEnvio);
                    lblPositivo.Text = "Se cargó con éxito el archivo !!!";
                }
                else
                {
                    lblPositivo.Text = "No existen datos en el archivo !!!";
                }
                
            }
            catch (Exception ex)
            {

                lblMensaje.Text =  contar.ToString() +  "_Error de carga. " + ex.Message.ToString();
                lblMensaje.CssClass = "Error";
            }
            return lista;
        }

       

      }
  }