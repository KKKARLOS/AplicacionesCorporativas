using IB.Progress.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public string strMsg = "";    
    //private bool bError = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Control de acceso a la aplicación
        try
        {
            
            Session["IP"] = Request.ServerVariables["REMOTE_ADDR"];

            string[] sUrlAux = Regex.Split(Request.ServerVariables["URL"], "/");
            if (sUrlAux[1].ToUpper() != "PROGRESS20") Session["strServer"] = "/";
            else Session["strServer"] = "/PROGRESS20/";
            Session["strHost"] = Request.ServerVariables["HTTP_HOST"];

            //Captura del usuario de red.
            if (Request.QueryString["scr"] != null)
            {
                Session["IDRED"] = Utils.decodpar(Request.QueryString["scr"].ToString());
            }
            else
            {
                string[] aIdRed = Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\");
                Array.Reverse(aIdRed);
                Session["IDRED"] = aIdRed[0];
                Session["IDRED"] = "DOIZALVI";
                //Session["IDRED"] = "DOBAGOMO";

                //todo Comentar estas líneas al pasar a producción
                if (Session["IDRED"].ToString().ToUpper() == "DOVEMODA" ||
                    Session["IDRED"].ToString().ToUpper() == "DOIZALVI" ||
                    Session["IDRED"].ToString().ToUpper() == "DOASANJA")
                {
                    if (Request.QueryString["codred"] != null)
                        Session["IDRED"] = Request.QueryString["codred"].ToString();
                }
                
            }
        }
        catch (Exception ex)
        {
            //bError = true;
            PieMenu.sErrores = "msgerr='" + ex.Message + "';";
        }
        #endregion




        //#region Control de identificación del usuario
        //if (!bError)
        //{

        //    try
        //    {
                
        //        //DbConn dbconn = null;
        //        try
        //        {
        //            //Abro conexión
        //            //dbconn = new DbConn();
        //            //IB.Progress.Models.Profesional oProfesional = new IB.Progress.Models.Profesional();
        //            //IB.Progress.BLL.Profesional cProfesional = new IB.Progress.BLL.Profesional(dbconn.dblibclass);

        //            //IB.Progress.Models.Aplicacion oAplicacion = new IB.Progress.Models.Aplicacion();

        //            //oAplicacion = cProfesional.Acceso();


        //            //if (!oAplicacion.Estado)
        //            //{
        //            //    divMantenimiento.Attributes.Add("class", "bsAlert col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-info cajaTexto pad10");
        //            //    txtMantenimiento.InnerText = oAplicacion.Motivo.ToString();
        //            //    return;
        //            //}

        //            //Capturamos el navegador
        //            //HttpBrowserCapabilities brObject = Request.Browser;

        //            //if (brObject.Type == "IE6" || brObject.Type == "IE7" || brObject.Type == "IE8")
        //            //{
        //            //    divNavegador.Attributes.Add("class", "show");
        //            //    return;
        //            //}

        //            //Esta variable de sesión la usamos para no dejar acceder a ninguna página cuando la web está en mantenimiento.
        //            //Session["ACCESO"] = 1;

        //            //oProfesional = cProfesional.Obtener(Session["IDRED"].ToString());
        //            //if (oProfesional.bIdentificado)
        //            //{
        //            //    //Dejo todo el objeto el la variable de sesión PROFESIONAL
        //            //    Session["PROFESIONAL"] = oProfesional;
        //            //    cProfesional.CargarRoles(oProfesional);
        //            //}
        //            //else
        //            //{
        //            //    //Muestro el contenedor de usuario no autorizado.
        //            //    divUsuNoAut.Attributes.Add("class", "bsAlert col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-info cajaTexto pad10");
        //            //}
        //            //Destruyo la conexión
        //            //dbconn.Dispose();
        //            ////Destruyo las clases
        //            //cProfesional.Dispose();
        //            //if (oProfesional.bIdentificado)
        //            //{
        //            //    if (Request.QueryString["ME"] != null)
        //            //        Response.Redirect("Capa_Presentacion/MiEquipo/Confirmarlo/Default.aspx");
        //            //    else if (Request.QueryString["IT"] != null)
        //            //        Response.Redirect("Capa_Presentacion/MiEquipo/GestIncorporaciones/Default.aspx");

        //            //    else if (Request.QueryString["FEVADO"] != null)
        //            //        Response.Redirect("Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx?FEVADO=1");

        //            //    else if (Request.QueryString["APROL"] != null)
        //            //        Response.Redirect("Capa_Presentacion/MiEquipo/GestionarSolicitudCambioRol/Default.aspx");

        //            //    else if (Request.QueryString["GESENT"] != null)
        //            //        Response.Redirect("Capa_Presentacion/MiEquipo/GestIncorporaciones/Default.aspx");

        //            //    else if (Request.QueryString["GESMED"] != null)
        //            //        Response.Redirect("Capa_Presentacion/Administracion/Mantenimientos/GestionCambioResponsable/Default.aspx");

        //            //    else if (Request.QueryString["GESSAL"] != null)
        //            //        Response.Redirect("Capa_Presentacion/MiEquipo/TramitarSalidas/Default.aspx");

        //            //    else
        //            //        Response.Redirect("capa_presentacion/home/home.aspx");


        //            //    //string navegador = "var navegador ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";

        //            //}
        //        }
        //        catch (Exception)
        //        {
        //            //Destruyo la conexión
        //            //dbconn.Dispose();
        //            throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strMsg += Errores.mostrarError("Error al obtener los datos del profesional.", ex);
        //    }
        //}
        //#endregion
    }


    /// <summary>
    /// Logea al usuario y devuelve array de dos posiciones:
    /// [0] --> 0:login ok; 1:Aplicación no accesible; 2:login error;
    /// [1] --> Mensaje de error a mostrar en pantalla
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string[] loginUser()
    {
        string[] result = new string[2];

        DbConn dbconn = null;
        IB.Progress.BLL.Profesional cProfesional = null;
        try
        {
            //Abro conexión
            dbconn = new DbConn();
            IB.Progress.Models.Profesional oProfesional = new IB.Progress.Models.Profesional();
            cProfesional = new IB.Progress.BLL.Profesional(dbconn.dblibclass);

            IB.Progress.Models.Aplicacion oAplicacion = new IB.Progress.Models.Aplicacion();

            oAplicacion = cProfesional.Acceso();

            //Comprobamos el estado de la aplicación
            if (!oAplicacion.Estado)
            {                
                result[0] = "1";
                result[1] = oAplicacion.Motivo.ToString();
                return result;
            }


            //Esta variable de sesión la usamos para no dejar acceder a ninguna página cuando la web está en mantenimiento.
            HttpContext.Current.Session["ACCESO"] = 1;

            oProfesional = cProfesional.Obtener(HttpContext.Current.Session["IDRED"].ToString());
            if (oProfesional.bIdentificado)
            {

                cProfesional.CargarRoles(oProfesional);

                //Dejo todo el objeto el la variable de sesión PROFESIONAL
                HttpContext.Current.Session["PROFESIONAL"] = oProfesional;
                HttpContext.Current.Session["PROFESIONAL_ENTRADA"] = oProfesional;

            }
            else
            {
                //Muestro el contenedor de usuario no autorizado.                
                result[0] = "2";
                result[1] = "";
                return result;
            }

            //Destruyo la conexión
            //Destruyo las clases
            if(cProfesional != null) cProfesional.Dispose();
            dbconn.Dispose();

            result[0] = "0";
            result[1] = "";
            return result;


        }
        catch (Exception)
        {
            //Destruyo la conexión
            if (cProfesional != null) cProfesional.Dispose();
            dbconn.Dispose();
            throw;
        }
    }

}