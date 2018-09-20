using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.LetrasEmitidasWCF;

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

namespace GS.SISGEGS.Web.Contabilidad.Informacion
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
                    //Empresa_Cargar();
                    //cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);


                    if (Request.QueryString["Periodo"] != "") {

                        ///*cboEmpresa*/.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();
                        Title = "Registrar Número Únicos";
                        lblMensaje.Text = "Listo para carga masiva de Números Unicos : ";
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
            string strPeriodo; 

            string fullPath;
            string Destino = "";
            string savePath = "c:\\temp\\uploads\\";
            int Anho, Mes, Dia; 


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

                strPeriodo = Request.QueryString["Periodo"].ToString();

                Anho = int.Parse(strPeriodo.Substring(0, 4));
                Mes = int.Parse(strPeriodo.Substring(4, 2));
                Dia = int.Parse(strPeriodo.Substring(6, 2));


                string strFecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" ; 

                fullPath = Path.Combine(savePath, strFecha +  e.File.GetName());

                e.File.SaveAs(fullPath);

                Destino = fullPath;

                tabla = LEER_EXCEL(Destino, "NumerosUnicos");
                tablas = REGISTRAR_EXCEL(tabla, Anho, Mes, Dia);
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
         }

        DataTable LEER_EXCEL(string Directorio, string nombreAchivo)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directorio + ";Extended Properties=Excel 8.0;";
            //string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Directorio + ";Extended Properties =Excel 12.0 Xml; HDR = YES;IMEX=1";

            OleDbConnection _connection = new OleDbConnection();
            OleDbDataAdapter _dataAdatpter = new OleDbDataAdapter();
            DataSet _ds = new DataSet();
            SqlCommand _command = new SqlCommand() ;
            DataTable dt = new DataTable();

            try
            {
                _connection = new OleDbConnection(connectionString);
                _dataAdatpter = new OleDbDataAdapter("SELECT * FROM [" + nombreAchivo + "$A1:H50000]", _connection);
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

            DataColumn FechaVencimiento = new  DataColumn();
            FechaVencimiento.ColumnName = "FechaVencimiento";
            FechaVencimiento.DataType = System.Type.GetType("System.DateTime");
            DataColumn FechaRegistro = new DataColumn();
            FechaRegistro.ColumnName = "FechaRegistro";
            FechaRegistro.DataType = System.Type.GetType("System.DateTime");


            dtTabla.Columns.Add("Id_Proceso");
            dtTabla.Columns.Add("Anho");
            dtTabla.Columns.Add("Mes");
            dtTabla.Columns.Add("Dia");
            dtTabla.Columns.Add("Correlativo");
            dtTabla.Columns.Add("Cliente");
            dtTabla.Columns.Add("Estado");
            dtTabla.Columns.Add("ID_LETRA");
            dtTabla.Columns.Add(FechaVencimiento);

            dtTabla.Columns.Add("Importe");
            dtTabla.Columns.Add("NumeroUnico");

            dtTabla.Columns.Add("IdUsuario");
            dtTabla.Columns.Add(FechaRegistro);
            dtTabla.Columns.Add("Banco");

            return dtTabla;

        }

        List<DataTable>REGISTRAR_EXCEL(DataTable tabla, int Anho, int Mes , int Dia)
        {
            LetrasEmitidasWCFClient objLetrasEmitidas = new LetrasEmitidasWCFClient(); 
 
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
            string Correlativo;
            string Cliente;
            string Estado;
            string Id_Letra;
            DateTime FechaVencimiento;
            float Importe;
            string NumeroUnico;
            string Banco;
            int Id_Proceso = 0;

            empresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
            usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;
            try
            {
                dtEnvio = FormatoTabla();


                dtGral = tabla;
                dtError = dtGral.Clone();
                dtError.Rows.Clear();
                dtError.Columns.Add("Comentario");
                int lent = 0;

               
                Id_Proceso = objLetrasEmitidas.ProcesoLetras_NumerosUnicos_Insertar(empresa, usuario, Anho, Mes, Dia, "",0);


                for (int n = 0; n < dtGral.Rows.Count; n++)
                {
                    var strNumeroUnico = dtGral.Rows[n]["NumeroUnico"];
                    if (string.IsNullOrEmpty(strNumeroUnico.ToString()))
                    {
                        break;
                    }
                    else
                    {
 
                        if( !string.IsNullOrEmpty(strNumeroUnico.ToString()))
                        {

                            Correlativo =  dtGral.Rows[n]["Correlativo"].ToString();
                            Cliente =  dtGral.Rows[n]["Cliente"].ToString();
                            Estado =  dtGral.Rows[n]["Estado"].ToString();
                            Id_Letra =  dtGral.Rows[n]["ID_LETRA"].ToString();
                            FechaVencimiento =  DateTime.Parse(dtGral.Rows[n]["FechaVencimiento"].ToString());
                            Importe = float.Parse(dtGral.Rows[n]["Importe"].ToString());
                            NumeroUnico = dtGral.Rows[n]["NumeroUnico"].ToString();
                            Banco = dtGral.Rows[n]["Banco"].ToString();

                            DataRow newRow = dtEnvio.NewRow();

                            newRow["Id_Proceso"] = Id_Proceso;
                            newRow["Anho"] = Anho;
                            newRow["Mes"] = Mes;
                            newRow["Dia"] = Dia;

                            newRow["Correlativo"] = Correlativo;
                            newRow["Cliente"] = Cliente;
                            newRow["Estado"] = Estado;
                            newRow["ID_LETRA"] = Id_Letra;

                            newRow["FechaVencimiento"] = FechaVencimiento;
                            newRow["Importe"] = Importe;
                            newRow["NumeroUnico"] = NumeroUnico;

                            newRow["IdUsuario"] = usuario;
                            newRow["FechaRegistro"] = DateTime.Now.ToShortDateString() ;
                            newRow["Banco"] = Banco;

                            dtEnvio.Rows.Add(newRow);
               
                        }
                    }
                      
                    
                }


                if(dtEnvio.Rows.Count > 0 )
                {
                    dtEnvio.TableName = "dtEnvio";
                

                    if(Id_Proceso > 0)
                    {
                        objLetrasEmitidas.NumerosUnicos_RegistrarBulkCopy(empresa, usuario, dtEnvio);

                        objLetrasEmitidas.NumerosUnicos_Registrar_Proceso(empresa, usuario );

                        objLetrasEmitidas.ProcesoLetras_NumerosUnicos_Insertar(empresa, usuario, Anho, Mes, Dia, "Se cargó con éxito el archivo", Id_Proceso);
                        lblPositivo.Text = "Se cargó con éxito el archivo !!!";
                    }
                    else
                    {
                        objLetrasEmitidas.ProcesoLetras_NumerosUnicos_Insertar(empresa, usuario, Anho, Mes, Dia, "No Se cargó el archivo ", Id_Proceso);
                        lblPositivo.Text = "No Se cargó el archivo !!!";
                    }
                  


                    
                }
                else
                {
                    lblPositivo.Text = "No existen datos en el archivo !!!";
                }
                
            }
            catch (Exception ex)
            {
                lblPositivo.Text = "Error de carga. " + ex.Message.ToString();
                objLetrasEmitidas.ProcesoLetras_NumerosUnicos_Insertar(empresa, usuario, Anho, Mes, Dia, "No Se cargó el archivo ", Id_Proceso);
                lblMensaje.Text =   "Error de carga. " + ex.Message.ToString();
                lblMensaje.CssClass = "Error";
            }
            return lista;
        }

       

      }
  }