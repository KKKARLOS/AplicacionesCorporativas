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
using SUPER.Capa_Negocio;
//Para el stringbuilder
using System.Text;

public partial class Capa_Presentacion_Reporte_Tarea_Default : System.Web.UI.Page
{
    public int idTarea, nObligaest, nPT;
    public string sDesTarea, sNotas = "0";
    protected string sEstado, sImputacion, sEstadoProy;
    public bool bEstadoLectura = false;
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        //Recogida de parámetros y volcado en IB.vars
        try
        {

            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
            idTarea = int.Parse(ht["t"].ToString());
            //Lo que anteriormente era el estado de la tarea, ahora será el estado del proyecto al que pertenece la tarea.
            //Asi se evita llamar al procedure SUP_TAREA_ESTADO
            sEstado = ht["estado"].ToString();
            sEstadoProy = ht["estadopsn"].ToString();
            sImputacion = ht["imputacion"].ToString();

            string nombreUsuario = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString(); ;
            
            //No se si se utilizan
            /*int nObligaest = int.Parse(ht["nObligaest"].ToString());
            int nPT = int.Parse(ht["pt"].ToString());
            string sDesTarea = ht["sDesTarea"].ToString();            
            int sEstado = int.Parse(ht["estado"].ToString());
            int sImputacion = int.Parse(ht["imputacion"].ToString());
             */

            //Modo en el se accederá al contenedor de documentos --> Edición o Consulta
            string sModoContainer = "E";
            //Si el proyecto está cerrado o es histórico, se accede en modo consulta
            if (sEstadoProy == "C" || sEstadoProy == "H") sModoContainer = "C";

            //Datos recibidos por parámetro
            string script1 = "IB.vars.idTarea = '" + idTarea + "';";
            script1 += "IB.vars.estadotarea = '" + sEstado + "';";
            script1 += "IB.vars.imputacion = '" + sImputacion + "';";
            script1 += "IB.vars.estadopsn = '" + sEstadoProy + "';";
            script1 += "IB.vars.idficepi = '" + HttpContext.Current.Session["NUM_EMPLEADO_IAP"] + "';";
            script1 += "IB.vars.superEditor = '" + Utilidades.EsAdminProduccion() + "';";
            script1 += "IB.vars.sModoContainer = '" + sModoContainer + "';";
            script1 += "IB.vars.nombreUsuario = '" + nombreUsuario + "';";            


            //Variables de sesión
            /*script1 += "IB.vars.jornadaReducida = '" + Session["JORNADA_REDUCIDA"] + "';";
            script1 += "IB.vars.nHorasRed = '" + Session["NHORASRED"] + "';";
            script1 += "IB.vars.fecDesRed = '" + Session["FECDESRED"] + "';";
            script1 += "IB.vars.fechasRed = '" + Session["FECHASRED"] + "';";
            script1 += "IB.vars.UMC_IAP = '" + Session["UMC_IAP"].ToString() + "';";
            script1 += "IB.vars.aSemLab = '" + Session["aSemLab"] + "'.split(',');";
            script1 += "IB.vars.controlHuecos = '" + Session["CONTROLHUECOS"] + "';";*/

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }

    }        

    /// <summary>
    /// Obtiene los datos de la tarea 
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.TareaIAP obtenerDetalleTarea(string idTarea)
    {

        BLL.TareaIAP tareaIAPBLL = new BLL.TareaIAP();
         Models.TareaIAP oTareaIAP;
        try
        {            

            oTareaIAP = tareaIAPBLL.Select(Int32.Parse(idTarea), (int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"]);

            return oTareaIAP;
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la tarea (" + idTarea + ")."));
        }
        finally
        {
            tareaIAPBLL.Dispose();
        }
    }

    /// <summary>
    ///Obteneción de documentos
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.DocutC3> obtenerDocumentos(string idTarea)
    {
        
        BLL.DocutC3 cDocutc3 = new BLL.DocutC3();

        try
        {

            int idUserAct = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            List<Models.DocutC3> catalogoDocumentos = cDocutc3.Catalogo(int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString()), Int32.Parse(idTarea));

            /*string sPermiso = "E";
            switch (sEstado)//Estado
            {
                case "0"://Paralizada
                    bEstadoLectura = true;
                    break;
                case "1"://Activo
                    break;
                case "2"://Pendiente
                    bEstadoLectura = true;
                    break;
                case "3"://Finalizada
                    if (sImputacion == "0") bEstadoLectura = true;
                    break;
                case "4"://Cerrada
                    if (sImputacion == "0") bEstadoLectura = true;
                    break;
            }
            if (bEstadoLectura) sPermiso = "R";       
            Utilidades.ObtenerDocumentos("IAP_T", idTarea, sPermiso, sEstadoProy);*/

            return catalogoDocumentos;       

        }

        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("Error en la obtención de documentos de la tarea (" + idTarea + ")."));
           
        }
        finally
        {
            cDocutc3.Dispose();
        }
    }

    /// <summary>
    /// Grabar los datos de la tarea
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string grabarTarea(string idTarea, Int32 finalizado, string totalEst, string fechaFinEst, string comentario, Int32 grabarNotas, string[] notas)
    {
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.EstimacionIAP cEstimocaionIAP = new BLL.EstimacionIAP(cDblib);
        BLL.NotasIAP cNotasIAP = new BLL.NotasIAP(cDblib);

        Models.EstimacionIAP oEstimacionIAP = new Models.EstimacionIAP();
        Models.NotasIAP oNotasIAP = new Models.NotasIAP();
        string sRes = "Grabación correcta.";
        try
        {

            int idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());

            #region Obtención de datos de la pantalla y carga de models

            int nTarea = int.Parse(idTarea); //Código de tarea
            bool bFinalizado = (finalizado == 1) ? true : false; //Tarea finalizada
            double nETE = double.Parse(totalEst); //Total estimado
            DateTime? dFFE = null;
            if (fechaFinEst != "") dFFE = DateTime.Parse(fechaFinEst); //Fecha fin estimación
            oEstimacionIAP.t314_idusuario = idUser;
            oEstimacionIAP.t332_idtarea = nTarea;
            oEstimacionIAP.t336_comentario = comentario;
            oEstimacionIAP.t336_completado = bFinalizado;
            oEstimacionIAP.t336_ete = nETE;
            oEstimacionIAP.t336_ffe = dFFE;

            bool bGrabarNotas = (grabarNotas == 1) ? true : false; //grabar notas

            if (bGrabarNotas) { 
                oNotasIAP.t332_idtarea = nTarea;
                oNotasIAP.t332_notas1 = notas[0];
                oNotasIAP.t332_notas2 = notas[1];
                oNotasIAP.t332_notas3 = notas[2];
                oNotasIAP.t332_notas4 = notas[3];
            }
            

            #endregion
            
            cEstimocaionIAP.Update(oEstimacionIAP);
            cNotasIAP.Update(oNotasIAP);
            return sRes;
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("Error al grabar los datos de la tarea (" + idTarea + ")."));
        }
        finally
        {
            cEstimocaionIAP.Dispose();
            cNotasIAP.Dispose();
            DBConn.Dispose();
        }
    }
}