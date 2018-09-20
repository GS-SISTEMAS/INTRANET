using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.CobranzasWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.PerfilWCF;
using System.Data;
using System.Data.OleDb;

using System.Data.Odbc;
using System.Data.Sql;
using System.Data.SqlClient;

namespace GS.SISGEGS.Web.Finanzas.Cobranzas
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
                        Title = "Registrar proyección";
                        ///*cboEmpresa*/.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para cargar masivamente la proyección";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                
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

        protected void btnCargaMasivaArchivo_Click(object sender, EventArgs e)
        {
            string resumen = "", positivo = "";//, negativo = "", total="";
           // int indice;

            resumen = SubirArchivoClientePrincipal();
            //indice = resumen.IndexOf("/");
            //total = resumen.Substring(0, indice);
            //negativo = resumen.Substring(indice + 1, resumen.Length - 1 - total.Length);
            //positivo = (Convert.ToInt32(total) - Convert.ToInt32(negativo)).ToString();
            lblCliCliPriResCarMasPos.Text = "Agregados con exito: " +  positivo;

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + 100 + ");", true);
            //if( ! negativo.Contains("0"))
            //{
            //    lblCliCliPriResCarMasNeg.Text = "NO agregados:" + negativo;
            //}
            //lblCliCliPriResCarMasTot.Text = "Total de registros: " + total;

        }

        private string SubirArchivoClientePrincipal()
        {
            List<DataTable> tablas = new List<DataTable>();
            DataTable tabla = new DataTable(); 
            string Fecha;
            Fecha = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            Fecha = Fecha + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            string Destino = "";

            try
            {
                if (filCargaMasiva.HasFile)
                {
                    Destino = SaveFile(filCargaMasiva.PostedFile);
                    tabla = LEER_EXCEL(Destino, "Proyectado");
                    tablas = REGISTRAR_EXCEL(tabla);

                    lblMensaje.Text = "El archivo se cargo con éxito.";
                    lblMensaje.CssClass = "mensajeExito";
                }
                else
                {
                    lblMensaje.Text = "Error: You did not specify a file to upload.";
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error de carga. " + ex.Message.ToString();
                lblMensaje.CssClass = "mensajeError";
            }
            return Destino;
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
                _dataAdatpter = new OleDbDataAdapter("SELECT * FROM [" + nombreAchivo + "$A1:K400]", _connection);
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

        List<DataTable>REGISTRAR_EXCEL(DataTable tabla)
        {
            CobranzasWCFClient objProyectadoWCF = new CobranzasWCFClient();
            List<DataTable> lista = new List<DataTable>();
            DataTable dtGral;
            DataTable dtError;

            string codSectorista;
            string codCliente;
            int codZona;
            string Periodo;
            int empresa, usuario; 
            decimal Semana1;
            decimal Semana2;
            decimal Semana3;
            decimal Semana4;
            decimal Proyectado;
            string cortar = ""; 


            try
            {
                dtGral = tabla;
                dtError = dtGral.Clone();
                dtError.Rows.Clear();
                dtError.Columns.Add("Comentario");
                int lent = 0; 

                for( int n = 0; n < dtGral.Rows.Count; n ++)
                {
                    codSectorista = "";
                    codCliente = "";
                    codZona = 0;
                    Periodo = "";
                    Semana1 = 0;
                    Semana2 = 0;
                    Semana3 = 0;
                    Semana4 = 0;
                    Proyectado = 0;

                    codSectorista = dtGral.Rows[n]["codSectorista"].ToString();
                    lent = codSectorista.Length;

                    if (lent > 0) {
                        cortar = codSectorista.Substring(0, 1);

                        if (cortar == "'")
                        {
                            codSectorista = codSectorista.Substring(1, lent - 1);
                        }
                        codSectorista = codSectorista.ToString();

                        codCliente = dtGral.Rows[n]["codCliente"].ToString();
                        lent = codCliente.Length;
                        cortar = codCliente.Substring(0, 1);

                        if (cortar == "'")
                        {
                            codCliente = codCliente.Substring(1, lent - 1);
                        }

                        codZona = int.Parse(dtGral.Rows[n]["codZona"].ToString());
                        Periodo = dtGral.Rows[n]["Periodo"].ToString();

                        Semana1 = decimal.Parse(dtGral.Rows[n]["Semana1"].ToString());
                        Semana2 = decimal.Parse(dtGral.Rows[n]["Semana2"].ToString());
                        Semana3 = decimal.Parse(dtGral.Rows[n]["Semana3"].ToString());
                        Semana4 = decimal.Parse(dtGral.Rows[n]["Semana4"].ToString());
                        Proyectado = Semana1 + Semana2 + Semana3 + Semana4;

                        empresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                        usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                        objProyectadoWCF.ProyectadoCobranza_Registrar(empresa, usuario, codCliente, codSectorista, Periodo, codZona, Semana1, Semana2, Semana3, Semana4, Proyectado);
                    }

                }

                lblMensaje.Text = "El archivo se cargo con éxito. ";
                lblMensaje.CssClass = "mensajeExito";

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error de carga. " + ex.Message.ToString();
                lblMensaje.CssClass = "mensajeError";
            }
            return lista;
        }

        string SaveFile(HttpPostedFile file)
        {
            // Specify the path to save the uploaded file to.
            string savePath = "c:\\temp\\uploads\\";

            // Get the name of the file to upload.
            string fileName = filCargaMasiva.FileName;

            // Create the path and file name to check for duplicates.
            string pathToCheck = savePath + fileName;

            // Create a temporary file name to use for checking duplicates.
            string tempfileName = "";

            try
            {
                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;
                }

                savePath += fileName;

                filCargaMasiva.SaveAs(savePath);

                // Notify the user that the file name was changed.
                lblMensaje.Text = "El archivo se puede cargar.";
                lblMensaje.CssClass = "mensajeExito";

            }
            catch(Exception ex)
            {
                // Notify the user that the file name was changed.
                //lblMensaje.Text = "A file with the same name already exists." +
                //    "<br />Se cargó con éxito el archivo.";
                lblMensaje.Text = "Error: Al Copiar el archivo. " + ex.Message;
                lblMensaje.CssClass = "mensajeExito";
            }

            return savePath;
         

        }

      }
    }