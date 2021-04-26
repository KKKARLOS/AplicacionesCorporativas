using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using IB.SUPER.Shared;
//using System.Text;
using SUPER.Capa_Negocio;
//Para el StreamReader
using System.IO;
//para el stringbuilder
using System.Text;
//Para el RegEx
using System.Text.RegularExpressions;
using System.Globalization;

public partial class Capa_Presentacion_Reporte_Fichero_Default : System.Web.UI.Page
{
    public Hashtable htTarea, htProfesional;
    //public int iCont = 0, iNumOk = 0;
    //public TAREA oTarea = null;
    //public PROFESIONAL oProfesional = null;

    //public StringBuilder sb = new StringBuilder();
    //public StringBuilder sbE = new StringBuilder();
    //public StringBuilder sbFlsCab1 = new StringBuilder();
    //public StringBuilder sbFlsCab2 = new StringBuilder();
    //public StringBuilder sbFlsPie1 = new StringBuilder();
    //public StringBuilder sbFlsPie2 = new StringBuilder();

    public string sErrores = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        if (HttpContext.Current.Request.Files.Count > 0)
        {
            var httpPostedFile = HttpContext.Current.Request.Files[0];
            //var httpPostedFile = HttpContext.Current.Request.Files["FicheroSubido"];
            if (httpPostedFile != null)
            {
                try
                {
                    //Validar(httpPostedFile, Request.Form[Constantes.sPrefijo + "optradio"].ToString());
                    //Validar(httpPostedFile, "D");
                    //Session["FicheroIAP" + Session["IDFICEPI_ENTRADA"].ToString()] = httpPostedFile;
                    HttpContext.Current.Cache["FicheroIAP_" + Session["IDFICEPI_ENTRADA"].ToString()] = httpPostedFile;
                }
                catch (Exception ex)
                {
                    LogError.LogearError("Error al analizar el fichero", ex);
                    throw new Exception(System.Uri.EscapeDataString("Error al analizar el fichero " + ex.Message));

                }
            }
        }
    }

    /*
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void getErroresValidacion2(string tipoFichero)
    {
        try
        {
            var httpPostedFile = HttpContext.Current.Request.Files["FicheroSubido"];
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al analizar el fichero", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al analizar el fichero " + ex.Message));

        }
        finally
        {
        }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void getErroresValidacion(HttpContext context)
    {
        try
        {
            //var httpPostedFile = HttpContext.Current.Request.Files["FicheroSubido"];
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            if (context.Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFile file = context.Request.Files[0];

                    byte[] content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);

                    int size = content.Length / 1024;
                    string name = file.FileName;

                    context.Response.Write("{\"name\": \"" + name + "\"" +  ",\"size\": " + size + "}");
                }
                catch (Exception ex)
                {
                    context.Response.Write("{\"jquery-upload-file-error\":\"Ocurrió un error subiendo el fichero: " + ex.Message + "\"}");
                }
            }
            else
                context.Response.Write("{\"jquery-upload-file-error\":\"No se ha detectado ningún fichero\"}");
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al analizar el fichero", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al analizar el fichero " + ex.Message));

        }
        finally
        {
        }

    }

    [WebMethod]
    public string UploadFiles()
    {
        // Checking no of files injected in Request object  
        if (Request.Files.Count > 0)
        {
            try
            {
                //  Get all files from Request object  
                HttpFileCollection files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFile file = files[i];
                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                    //file.SaveAs(fname);
                }
                // Returns message that successfully uploaded  
                //return Json("File Uploaded Successfully!");
                return "File Uploaded Successfully!";
            }
            catch (Exception ex)
            {
                //return Json("Error occurred. Error details: " + ex.Message);
                return "Error occurred. Error details: " + ex.Message;
            }
        }
        else
        {
            //return Json("No files selected.");
            return "No files selected.";
        }
    } 
     * */


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.FicheroIAP_Errores getErroresValidacion(string tipoFichero)
    {
        try
        {
            //var httpPostedFile = Session["FicheroIAP" + Session["IDFICEPI_ENTRADA"].ToString()];
            string sNombre ="FicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString();
            HttpPostedFile httpPostedFile = (HttpPostedFile)HttpContext.Current.Cache[sNombre];

            Models.FicheroIAP_Errores oRes= Validar(httpPostedFile, tipoFichero, int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()));

            //HttpContext.Current.Cache[sNombre] = "";
            
            return oRes;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al validar el fichero", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al validar el fichero. " + ex.Message));
        }
        finally
        {

        }
    }


    private static Models.FicheroIAP_Errores Validar(HttpPostedFile selectedFile, string sEstructu, int idFicepi)
    {
        //StringBuilder sbE = new StringBuilder();
        Models.FicheroIAP_Errores oRes = new Models.FicheroIAP_Errores();
        List<Models.FicheroIAP_Errores_Linea> oListaE = new List<Models.FicheroIAP_Errores_Linea>();
        bool bErrorControlado = false;
        BLL.FicheroIAP bFicheroIAP = new BLL.FicheroIAP();
        //int idFicepi = int.Parse(Session["IDFICEPI_ENTRADA"].ToString());
        int iCont = 0;
        int iNumOk = 0;
        string sErrores;
        Hashtable htT;
        Hashtable htP;
        try
        {
            //Vacío las caches
            //HttpContext.Current.Cache.Remove("TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
            //HttpContext.Current.Cache.Remove("ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());

            try
            {
                htT = CargarArrayTareas();
                htP = CargarArrayProfesionales();
            }
            catch (Exception ex)
            {
                bErrorControlado = true;
                throw (new Exception(ex.Message));
            }


            if (selectedFile.ContentLength != 0)
            {
                string sFichero = selectedFile.FileName;
                //Grabo el archivo en base de datos
                byte[] ArchivoEnBinario = new Byte[0];
                ArchivoEnBinario = new Byte[selectedFile.ContentLength]; //Crear el array de bytes con la longitud del archivo
                selectedFile.InputStream.Read(ArchivoEnBinario, 0, selectedFile.ContentLength); //Forzar al control del archivo a cargar los datos en el array

                int iRows = bFicheroIAP.Update(Constantes.FicheroIAP, idFicepi, ArchivoEnBinario);
                if (iRows == 0)
                {
                    bErrorControlado = true;
                    throw (new Exception("No existe entrada asociada a este proceso en el fichero de Maniobra"));
                }

                selectedFile.InputStream.Position = 0;
                StreamReader r = new StreamReader(selectedFile.InputStream, System.Text.Encoding.UTF7);
                DesdeFicheroIAP oDesdeFicheroIAP = null;

                String strLinea = null;
                while ((strLinea = r.ReadLine()) != "")
                {
                    if (strLinea == null) break;
                    iCont++;
                    try
                    {
                        oDesdeFicheroIAP = getLinea(DesdeFicheroIAP.getFila(strLinea, sEstructu), sEstructu, htT, htP);
                    }
                    catch (Exception ex)
                    {
                        bErrorControlado = true;
                        //oDesdeFicheroIAP = new DesdeFicheroIAP();
                        //sbE.Append(ponerFilaError(oDesdeFicheroIAP, "Error al procesar el fichero de entrada en la línea (" + iCont + ") " + ex.Message, sEstructu, iCont));
                        Models.FicheroIAP_Errores_Linea oLinE = new Models.FicheroIAP_Errores_Linea();
                        oLinE.Error = "Error al procesar el fichero de entrada en la línea (" + iCont + ") " + ex.Message;
                        oListaE.Add(oLinE);
                        continue;
                    }
                    Models.FicheroIAP_Errores_Linea oLin = validarCampos(oDesdeFicheroIAP, true, sEstructu, iCont, htT, htP);
                    if (oLin.Error==null)
                        iNumOk++;
                    else
                    {
                        oListaE.Add(oLin);
                    }
                    
                }
            }
            //if (sEstructu == "D") sFLS.Value = sCab1.Value + sbE.ToString() + sPie1.Value;
            //else sFLS.Value = sCab2.Value + sbE.ToString() + sPie2.Value;

            //nFilas.InnerText = iCont.ToString("#,##0");
            //nFilasC.InnerText = iNumOk.ToString("#,##0");
            //nFilasE.InnerText = (iCont - iNumOk).ToString("#,##0");
            //this.hdnIniciado.Value = "T";
            oRes.nFilas = iCont;
            oRes.nFilasC = iNumOk;
            oRes.nFilasE = iCont - iNumOk;
            if (oListaE.Count > 0)
                oRes.Errores = oListaE;
            //return iCont.ToString("#,##0") + "@#@" + iNumOk.ToString("#,##0") + "@#@" + (iCont - iNumOk).ToString("#,##0") + 
            //        "@#@" + sbE.ToString();

            return oRes;
        }
        catch (Exception ex)
        {
            if (bErrorControlado)
            {
                if (iCont != 0) sErrores = "Error al procesar el fichero de entrada en la línea (" + iCont + ") " + ex.Message;
                else sErrores = ex.Message;
            }
            else sErrores = "El fichero no tiene el formato requerido para el proceso";
            throw (new Exception(sErrores));
        }
        finally
        {
            bFicheroIAP.Dispose();
            
        }
    }
    private static Hashtable CargarArrayTareas()
    {
        Hashtable htTarea = new Hashtable();
        BLL.FicheroIAP bFicheroIAP = null;
        try
        {
            bFicheroIAP = new BLL.FicheroIAP();

            if (HttpContext.Current.Cache["TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] == null)
            {
                List<TAREA> oListaTareas = bFicheroIAP.GetTareas();
                foreach (TAREA OT in oListaTareas) //Recorro tabla de TAREA
                {
                    htTarea.Add(OT.t332_idtarea.ToString(), new TAREA(OT.t332_idtarea, OT.t332_destarea, OT.t331_idpt,
                                                                        OT.t332_estado, OT.t332_cle, OT.t332_tipocle, OT.t332_impiap,
                                                                        OT.t305_idproyectosubnodo, OT.t332_fiv, OT.t332_ffv,
                                                                        OT.t323_regjornocompleta, OT.t331_obligaest,
                                                                        OT.t331_estado, OT.t323_regfes, OT.t301_estado));
                }
                HttpContext.Current.Cache["TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] = htTarea;
            }
            else
                htTarea = (Hashtable)HttpContext.Current.Cache["TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()];
            return htTarea;
        }
        catch
        {
            throw (new Exception("Error al obtener las consultas para la carga de datos."));
        }
        finally
        {
            bFicheroIAP.Dispose();
        }
    }
    private static Hashtable CargarArrayProfesionales()
    {
        Hashtable htProfesional = new Hashtable();
        BLL.FicheroIAP bFicheroIAP = null;
        try
        {
            bFicheroIAP = new BLL.FicheroIAP();

            if (HttpContext.Current.Cache["ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] == null)
            {
                List<PROFESIONAL> oListaProf = bFicheroIAP.GetProfesionales();
                foreach (PROFESIONAL OP in oListaProf) //Recorro tabla de Profesionales
                {
                    htProfesional.Add(OP.t314_idusuario.ToString(),
                                                new PROFESIONAL(OP.t001_idficepi, OP.t314_idusuario, OP.Profesional,
                                                                OP.t303_ultcierreIAP, OP.t314_jornadareducida, OP.t303_idnodo,
                                                                OP.t314_horasjor_red, OP.t314_fdesde_red, OP.t314_fhasta_red,
                                                                OP.t314_controlhuecos, OP.fUltImputacion, OP.t066_idcal,
                                                                OP.t066_descal, OP.SemanaLaboral, OP.t001_codred, OP.fAlta, OP.fBaja));
                }
                HttpContext.Current.Cache["ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] = htProfesional;
            }
            else
                htProfesional = (Hashtable)HttpContext.Current.Cache["ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()];

            return htProfesional;
        }
        catch
        {
            throw (new Exception("Error al obtener las consultas para la carga de datos."));
        }
        finally
        {
            bFicheroIAP.Dispose();
        }
    }

    private static DesdeFicheroIAP getLinea(DesdeFicheroIAP oDesdeFicheroIAP, string sEstructu,
                                                Hashtable htTarea, Hashtable htProfesional)
    {
        TAREA oTarea = null;
        PROFESIONAL oProfesional = null;

        char[] chrs = { '+', '-', ',', '.' };
        char[] chrs2 = { '+', '-', '.' };

        int idx = 0;

        oTarea = (TAREA)htTarea[oDesdeFicheroIAP.idtarea];
        if (oTarea != null)
        {
            oDesdeFicheroIAP.t332_destarea = oTarea.t332_destarea;
        }

        oProfesional = (PROFESIONAL)htProfesional[oDesdeFicheroIAP.idusuario];
        if (oProfesional != null)
        {
            oDesdeFicheroIAP.Profesional = oProfesional.Profesional;
        }

        if (Utilidades.isNumeric(oDesdeFicheroIAP.idusuario))
        {
            idx = oDesdeFicheroIAP.idusuario.IndexOfAny(chrs);
            if (idx == -1) oDesdeFicheroIAP.t314_idusuario = System.Convert.ToInt32(oDesdeFicheroIAP.idusuario);
            else oDesdeFicheroIAP.t314_idusuario = -99999;
        }
        else oDesdeFicheroIAP.t314_idusuario = -1;

        if (Utilidades.isNumeric(oDesdeFicheroIAP.idtarea))
        {
            idx = oDesdeFicheroIAP.idtarea.IndexOfAny(chrs);
            if (idx == -1) oDesdeFicheroIAP.t332_idtarea = System.Convert.ToInt32(oDesdeFicheroIAP.idtarea);
            else oDesdeFicheroIAP.t332_idtarea = -99999;
        }
        else oDesdeFicheroIAP.t332_idtarea = -1;

        if (oDesdeFicheroIAP.fechaDesde.Length == 10)
        {
            if (Utilidades.isDate(oDesdeFicheroIAP.fechaDesde))
            {
                oDesdeFicheroIAP.t337_fechaDesde = DateTime.Parse(oDesdeFicheroIAP.fechaDesde);
                if (
                        (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaDesde.ToString().Substring(0, 2)))
                    ||
                        (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaDesde.ToString().Substring(3, 2)))
                    ||
                        (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaDesde.ToString().Substring(6, 4)))
                    )
                    oDesdeFicheroIAP.t337_fechaDesde = null;
            }
            else
            {
                oDesdeFicheroIAP.t337_fechaDesde = null;
            }
        }
        else
        {
            oDesdeFicheroIAP.t337_fechaDesde = null;
        }
        if (Utilidades.isNumeric(oDesdeFicheroIAP.esfuerzo))
        {
            idx = oDesdeFicheroIAP.esfuerzo.IndexOfAny(chrs2);
            if (idx == -1) oDesdeFicheroIAP.t337_esfuerzo = Double.Parse(oDesdeFicheroIAP.esfuerzo);
            else oDesdeFicheroIAP.t337_esfuerzo = -99999;
        }
        else
        {
            oDesdeFicheroIAP.t337_esfuerzo = -1;
        }

        //if (Request.Form[Constantes.sPrefijo + "rdbImputacion"].ToString() != "D")
        if (sEstructu != "D")
        {
            if (oDesdeFicheroIAP.fechaHasta.Length == 10)
            {
                if (Utilidades.isDate(oDesdeFicheroIAP.fechaHasta))
                {
                    oDesdeFicheroIAP.t337_fechaHasta = DateTime.Parse(oDesdeFicheroIAP.fechaHasta);
                    if (
                            (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaHasta.ToString().Substring(0, 2)))
                        ||
                            (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaHasta.ToString().Substring(3, 2)))
                        ||
                            (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaHasta.ToString().Substring(6, 4)))
                        )
                    {
                        oDesdeFicheroIAP.t337_fechaHasta = null;
                    }
                    else if (oDesdeFicheroIAP.t337_fechaDesde > oDesdeFicheroIAP.t337_fechaHasta)
                    { oDesdeFicheroIAP.t337_fechaHasta = null; }
                }
                else
                {
                    oDesdeFicheroIAP.t337_fechaHasta = null;
                }
            }
            else
            {
                oDesdeFicheroIAP.t337_fechaHasta = null;
            }

            if (Utilidades.isNumeric(oDesdeFicheroIAP.festivos))
            {
                if (oDesdeFicheroIAP.festivos == "1") oDesdeFicheroIAP.bfestivos = true;
                else if (oDesdeFicheroIAP.festivos == "0") oDesdeFicheroIAP.bfestivos = false;
                else oDesdeFicheroIAP.bfestivos = null;
            }
            else oDesdeFicheroIAP.bfestivos = null;
        }

        return oDesdeFicheroIAP;
    }
    private static string ponerFilaError_OLD(DesdeFicheroIAP oDesdeFicheroIAP, string sMens, string sEstructu, int iCont)
    {

        StringBuilder sb = new StringBuilder();
        //if (Request.Form[Constantes.sPrefijo + "optradio"].ToString() == "D")
        if (sEstructu == "D")
        {
            sb.Append("<tr style='cursor:default;height:16px;' id=" + iCont.ToString() + ">");
            sb.Append("<td><div style='cursor:default;width:235px;' title='" + oDesdeFicheroIAP.Profesional + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idusuario + "-" + oDesdeFicheroIAP.Profesional);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.fechaDesde + "</td>");
            sb.Append("<td><div style='cursor:default;width:235px;' title='" + oDesdeFicheroIAP.t332_destarea + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idtarea + "-" + oDesdeFicheroIAP.t332_destarea);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.esfuerzo);
            sb.Append("</td><td><div style='cursor:default;width:280px;' title='" + sMens + "' class='NBR'>");
            sb.Append(sMens);
            sb.Append("</div></td></tr>");
        }
        else
        {
            sb.Append("<tr style='cursor:default;height:16px;' id=" + iCont.ToString() + ">");
            sb.Append("<td><div style='cursor:default;width:200px;' title='" + oDesdeFicheroIAP.Profesional + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idusuario + "-" + oDesdeFicheroIAP.Profesional);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.fechaDesde);
            sb.Append("</td><td>");
            sb.Append(oDesdeFicheroIAP.fechaHasta + "</td>");
            sb.Append("<td><div style='cursor:default;width:200px;' title='" + oDesdeFicheroIAP.t332_destarea + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idtarea + "-" + oDesdeFicheroIAP.t332_destarea);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.esfuerzo);
            sb.Append("</td><td>");
            sb.Append(oDesdeFicheroIAP.festivos);
            sb.Append("</td><td><div style='cursor:default;width:220px;' title='" + sMens + "' class='NBR'>");
            sb.Append(sMens);
            sb.Append("</div></td></tr>");
        }
        return sb.ToString();
    }
    private static Models.FicheroIAP_Errores_Linea ponerFilaError(DesdeFicheroIAP oDesdeFicheroIAP, string sMens, string sEstructu, int iCont)
    {
        Models.FicheroIAP_Errores_Linea oLin = new Models.FicheroIAP_Errores_Linea();
        DateTime fechaSalida;

        oLin.Fila = iCont;
        oLin.Usuario = oDesdeFicheroIAP.idusuario + "-" + oDesdeFicheroIAP.Profesional;
        if (DateTime.TryParse(oDesdeFicheroIAP.fechaDesde, out fechaSalida)) oLin.Fecha = fechaSalida;        
        oLin.Tarea = oDesdeFicheroIAP.idtarea + "-" + oDesdeFicheroIAP.t332_destarea;
        oLin.Esfuerzo = Double.Parse(oDesdeFicheroIAP.esfuerzo.Replace(",", "."), CultureInfo.InvariantCulture);
        oLin.Error = sMens;

        if (sEstructu != "D")
        {
            if (DateTime.TryParse(oDesdeFicheroIAP.fechaHasta, out fechaSalida)) oLin.FechaH = fechaSalida;
            oLin.Festivos = oDesdeFicheroIAP.festivos;
        }
        return oLin;
    }

    private static Models.FicheroIAP_Errores_Linea validarCampos(DesdeFicheroIAP oDesdeFicheroIAP, bool bEscribir, string sEstructu, int iCont,
                                        Hashtable htTarea, Hashtable htProfesional)
    {
        TAREA oTarea;
        Models.FicheroIAP_Errores_Linea oRes = new Models.FicheroIAP_Errores_Linea();
        
        if (oDesdeFicheroIAP.t332_idtarea == -99999)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "Formato incorrecto. El número de tarea no acepta puntos, signos(+,-) ni comas decimales, (" + oDesdeFicheroIAP.idtarea + ")", sEstructu, iCont);
        }
        oTarea = (TAREA)htTarea[oDesdeFicheroIAP.idtarea];
        if (oTarea == null)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") no existe.", sEstructu, iCont);
        }
        else
        {
            if ((oTarea.t332_estado == 3 || oTarea.t332_estado == 4) && oTarea.t332_impiap == false)
            {
                if (bEscribir)
                    oRes = ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") tiene el estado (cerrado o finalizado) y no permite imputar IAP.", sEstructu, iCont);
            }
            else if (!(oTarea.t332_estado == 1 || ((oTarea.t332_estado == 3 || oTarea.t332_estado == 4) && oTarea.t332_impiap == true)))
            {
                if (bEscribir)
                    oRes = ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") tiene un estado no permitido para imputaciones (" + oTarea.t332_estado.ToString() + ")", sEstructu, iCont);
            }
            else if (oTarea.t331_estado != 1)
            {
                if (bEscribir)
                    oRes = ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") tiene el estado de su proyecto técnico no activo.", sEstructu, iCont);
            }
        }

        if (oDesdeFicheroIAP.t314_idusuario == -99999)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "Formato incorrecto. El número de usuario no acepta puntos, signos(+,-) ni comas decimales, (" + oDesdeFicheroIAP.idusuario + ")", sEstructu, iCont);
        }

        if ((PROFESIONAL)htProfesional[oDesdeFicheroIAP.idusuario] == null)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "El usuario (" + oDesdeFicheroIAP.idusuario + ") no existe.", sEstructu, iCont);
        }

        if (oDesdeFicheroIAP.t332_idtarea == -1)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "Número de tarea no numérico (" + oDesdeFicheroIAP.idtarea + ")", sEstructu, iCont);
        }

        if (oDesdeFicheroIAP.t314_idusuario == -1)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "Número de usuario no numérico (" + oDesdeFicheroIAP.idusuario + ")", sEstructu, iCont);
        }

        if (oDesdeFicheroIAP.t337_esfuerzo == -1)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "El valor del campo esfuerzo es no numérico (" + oDesdeFicheroIAP.esfuerzo + ")", sEstructu, iCont);
        }

        if (oDesdeFicheroIAP.t337_esfuerzo == -99999)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "El valor del campo esfuerzo no acepta puntos ni signos (+,-), (" + oDesdeFicheroIAP.esfuerzo + ")", sEstructu, iCont);
        }

        if (oDesdeFicheroIAP.t337_fechaDesde == null)
        {
            if (bEscribir)
                oRes = ponerFilaError(oDesdeFicheroIAP, "El valor del campo fecha desde tiene el formato incorrecto o su valor no es válido (" + oDesdeFicheroIAP.fechaDesde + ")", sEstructu, iCont);
        }

        //if (Request.Form[Constantes.sPrefijo + "rdbImputacion"].ToString() != "D")
        if (sEstructu != "D")
        {
            if (oDesdeFicheroIAP.t337_fechaHasta == null)
            {
                if (bEscribir)
                    oRes = ponerFilaError(oDesdeFicheroIAP, "El valor del campo fecha hasta tiene el formato incorrecto o su valor no es válido (" + oDesdeFicheroIAP.fechaHasta + ")", sEstructu, iCont);
            }

            if (oDesdeFicheroIAP.bfestivos == null)
            {
                if (bEscribir)
                    oRes = ponerFilaError(oDesdeFicheroIAP, "El valor del campo permitir imputar a festivos o no laborables, o no es numérico o su valor no es válido (" + oDesdeFicheroIAP.festivos + ")", sEstructu, iCont);
            }
        }

        return oRes;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.FicheroIAP_Errores Procesar(string tipoFichero)
    {
        Models.FicheroIAP_Errores oRes=null;
        BLL.FicheroIAP bFichero = new BLL.FicheroIAP();
        string sNombre = "FicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString();
        try
        {
            HttpPostedFile selectedFile = (HttpPostedFile)HttpContext.Current.Cache[sNombre];

            if (selectedFile != null)
            {
                oRes = bFichero.Grabar(tipoFichero, selectedFile);
            }
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message));
        }
        finally
        {
            bFichero.Dispose();
            HttpContext.Current.Cache.Remove(sNombre);
        }
        return oRes;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void limpiarCache()
    {
        try
        {
            HttpContext.Current.Cache.Remove("TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
            HttpContext.Current.Cache.Remove("ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
        }
        catch (Exception ex)
        {
            //throw (new Exception(ex.Message));
        }
    }

}