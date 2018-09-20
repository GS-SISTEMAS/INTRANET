using System;
using System.Collections.Generic;

using System.Web.UI;

using GS.SISGEGS.Web.ReporteVentaWCF;

using GS.SISGEGS.DM;

using System.Data;
using System.Data.OleDb;


using System.Data.SqlClient;
using Telerik.Web.UI;
using System.IO;

namespace GS.SISGEGS.Web.Comercial.Proyectado
{
    public partial class frmCargaMasivaPresupuesto : System.Web.UI.Page
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
                        Title = "Registrar presupuesto";
                        ///*cboEmpresa*/.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para carga masiva de presupuesto: ";
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

        //private void AddPostBackTrigger(string controlId)
        //{
        //    PostBackTrigger existingTrigger = FindPostBackTrigger(controlId);

        //    if (existingTrigger != null)
        //    {
        //        var trigger = new PostBackTrigger { ControlID = controlId };
        //        //UpdatePanel1.Triggers.Add(trigger);
        //    }
        //}
        //private PostBackTrigger FindPostBackTrigger(string controlId)
        //{
        //    return
        //        UpdatePanel1
        //            .Triggers.OfType<PostBackTrigger>()
        //            .FirstOrDefault(pt => pt.ControlID == controlId);
        //}

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
            savePath = "C:\\Users\\cesar.coronel\\Desktop\\Pruebas_TXT\\Plantillas\\Destino\\"; 

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
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directorio + ";Extended Properties=Excel 8.0;";
                   //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + Directorio + "; Extended Properties =\"Excel 8.0;HDR=Yes;IMEX=2\"";

                   //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Directorio + ";Extended Properties =Excel 12.0 Xml; HDR = YES;IMEX=1";

            OleDbConnection _connection = new OleDbConnection();
            OleDbDataAdapter _dataAdatpter = new OleDbDataAdapter();
            DataSet _ds = new DataSet();
            SqlCommand _command = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                _connection = new OleDbConnection(connectionString);
                _connection.Open(); 
                _dataAdatpter = new OleDbDataAdapter("SELECT * FROM [" + nombreAchivo + "$A4:AA480]", _connection);
                _dataAdatpter.Fill(_ds);
                dt =  _ds.Tables[0];
                _connection.Close(); 

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
            dtTabla.Columns.Add("ID_Vendedor");
            dtTabla.Columns.Add("ID_Cliente");
            dtTabla.Columns.Add("ID_Moneda");
            dtTabla.Columns.Add("Precio");
            dtTabla.Columns.Add("ID_Zona");
            
            
            return dtTabla;

        }

        List<DataTable>REGISTRAR_EXCEL(DataTable tabla)
        {
            ReporteVentaWCFClient objReporteVentaWCF = new ReporteVentaWCFClient(); 

            List<DataTable> lista = new List<DataTable>();
            DataTable dtGral;
            DataTable dtError;
            DataTable dtEnvio = new DataTable(); 

            string codVendedor;
            int codZona;
            string Periodo;
            int empresa, usuario;
            int anho;
            int mes;
            int MesCantidad; 
            string strMes;
            int id_zonaf = 0 ;
            string id_vendedorf = "";
            decimal precio = 0;
            decimal defPrecio = 0;
            string cortar = "";
            DateTime Fecha;
            int Kardex;


            string stringPeriodo;
            int year1;
            int mes1, mes2, mesDefoult;
            int year2, year3;

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

                for( int n = 0; n < dtGral.Rows.Count; n ++)
                {
                    if( n == 205)
                    {
                        codVendedor = dtGral.Rows[n]["ID_Vendedor"].ToString();
                    }

                    codVendedor = dtGral.Rows[n]["ID_Vendedor"].ToString();


                    if(codVendedor.Length > 0 )
                    {
                        lent = codVendedor.Length;
                        cortar = codVendedor.Substring(0, 1);

                    if (cortar == "'")
                    {

                        codVendedor = codVendedor.Substring(1, lent - 1);
                    }
                    codVendedor = codVendedor.ToString();
                        id_vendedorf = codVendedor; 

                    codZona = int.Parse(dtGral.Rows[n]["Id_Zona"].ToString());
                        id_zonaf = codZona; 

                    Kardex = int.Parse(dtGral.Rows[n]["Kardex"].ToString());

                    try
                    {
                        precio = decimal.Parse(dtGral.Rows[n]["Precio Vend#"].ToString());
                    }
                    catch {
                        precio = 0;
                    }

                    Periodo = dtGral.Rows[n]["Periodo"].ToString();
                    anho = int.Parse( Periodo.Substring(0, 4));
                    mes = int.Parse(Periodo.Substring(4, 2)); 



                    year2 = anho;
                    mes2 = mes;
                    year1 = year2 - 1;
                    mes1 = 1;
                    mesDefoult = 13;
                    year3 = year2;

                        for (int y = 1; y < 13; y++)
                        {

                            if (mesDefoult > 12)
                            {
                                mesDefoult = 1;
                                year3 = year2 + 1;
                                stringPeriodo = year3 + "_" + mesDefoult;
                            }
                            else
                            {
                                stringPeriodo = year3 + "_" + mesDefoult;
                            }

                            strMes = mesDefoult.ToString();
                            if (strMes.Length == 1)
                            {
                                strMes = "0" + strMes;
                            }

                            Fecha = Convert.ToDateTime(year3 + "/" + strMes + "/" + "01");
                            string strFecha = year3 + "/" + strMes + "/" + "01"; 
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
                                    MesCantidad = int.Parse(strCantidad);
                                }
                                catch
                                {
                                    MesCantidad = 0;
                                }
                            }
                        

                        DataRow newRow = dtEnvio.NewRow();


                            newRow["Fecha"] = strFecha;
                            newRow["Item_ID"] = Kardex;
                            
                            newRow["Cantidad"] = MesCantidad;
                            newRow["Importe"] = 0;
                            newRow["Aprobacion1"] = true;
                            newRow["Aprobacion2"] = true;
                            newRow["Aprobacion1Fecha"] = DateTime.Now;
                            newRow["Aprobacion2Fecha"] = DateTime.Now;
                            newRow["Aprobacion1CodUsuario"] = usuario;

                            newRow["Aprobacion2CodUsuario"] = usuario;
                            newRow["UsuarioAcceso"] = usuario;

                            newRow["UsuarioAccesoFecha"] = DateTime.Now;

                            newRow["Año"] = year3;
                            newRow["Trimestre"] = 0;
                            newRow["Mes"] = mesDefoult;
                            newRow["Semana"] = 0;
                            newRow["Dia"] = 0;
                            
                            newRow["ID_Vendedor"] = codVendedor;
                            newRow["ID_Cliente"] = null;
                            newRow["ID_Moneda"] = 0;
                            newRow["Precio"] = precio;
                            newRow["ID_Zona"] = codZona;

                            //objReporteVentaWCF.ProyectadoVentas_Registrar(empresa, usuario, 0, Fecha, Kardex, 0, 0, MesCantidad, 0, true , year3, mesDefoult, codZona, codVendedor, null, 0);

                            dtEnvio.Rows.Add(newRow);

                            mesDefoult = mesDefoult + 1;
                     }
                   }
                }


                if(dtEnvio.Rows.Count > 0 )
                {
                    dtEnvio.TableName = "dtEnvio"; 
                    objReporteVentaWCF.PresupuestadoVentas_RegistrarBulkCopy(empresa, usuario, DateTime.Now, id_zonaf, id_vendedorf, null, 0, dtEnvio);
                    lblPositivo.Text = "Se cargó con éxito el archivo !!!";
                }
                else
                {
                    lblPositivo.Text = "No existen datos en el archivo !!!";
                }
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error de carga. " + ex.Message.ToString();
                lblMensaje.CssClass = "Error";
            }
            return lista;
        }

        //string SaveFile(HttpPostedFile file)
        //{
        //    // Specify the path to save the uploaded file to.
        //    string savePath = "c:\\temp\\uploads\\";

        //    // Get the name of the file to upload.
        //    string fileName = filCargaMasiva.FileName;

        //    // Create the path and file name to check for duplicates.
        //    string pathToCheck = savePath + fileName;

        //    // Create a temporary file name to use for checking duplicates.
        //    string tempfileName = "";

        //    // Check to see if a file already exists with the
        //    // same name as the file to upload.        
        //    if (System.IO.File.Exists(pathToCheck))
        //    {
        //        int counter = 2;
        //        while (System.IO.File.Exists(pathToCheck))
        //        {
        //            // if a file with this name already exists,
        //            // prefix the filename with a number.
        //            tempfileName = counter.ToString() + fileName;
        //            pathToCheck = savePath + tempfileName;
        //            counter++;
        //        }

        //        fileName = tempfileName;

        //        // Notify the user that the file name was changed.
        //        //lblMensaje.Text = "A file with the same name already exists." +
        //        //    "<br />Your file was saved as " + fileName;
        //        //lblMensaje.CssClass = "Exito";
        //    }
        //    else
        //    {
        //        // Notify the user that the file was saved successfully.
        //        lblMensaje.Text = "Your file was uploaded successfully.";
        //        lblMensaje.CssClass = "Error";
        //    }
        //    savePath += fileName;
        //    filCargaMasiva.SaveAs(savePath);

        //    return savePath;
        //}

      }
  }