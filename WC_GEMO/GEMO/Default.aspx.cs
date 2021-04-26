using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using GEMO.BLL;

public partial class Default : System.Web.UI.Page
{
    public string strEnlace = "";
    public string strMsg = "";
    private bool bError = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Control del estado de acceso a la aplicación
        try
        {
            AccesoAplicaciones objAccApli = new AccesoAplicaciones();
            objAccApli.Leer();
            if (objAccApli.Estado == false)
            {
                Session["MOTIVO"] = objAccApli.Motivo;
                Response.Redirect("Mantenimiento.aspx", true);
            }
            else
            {
                //Se establece si hay novedades o no que mostrar a los usuarios.
                Session["HAYNOVEDADES"] = "0";
                if (objAccApli.Novedades) Session["HAYNOVEDADES"] = "1";
            }
        }
        catch (Exception ex)
        {
            bError = true;
            strMsg += Errores.mostrarError("Error al comprobar el acceso a la aplicación:", ex);
        }
        #endregion

        #region Control de identificación del usuario

        if (!bError)
        {
            if (Request.QueryString["sCodRed"] != null && Request.QueryString["SessionID"].ToString() == Session.SessionID)
            {
                //El control del Session.SessionID nos evita el agujero de que alguien ponga en la url el parámetro "sCodRed" y puede entrar.
                //Session["IDRED"] = Request.QueryString["sCodRed"].ToString();
            }
            else
            {

                string[] sUrlAux = Regex.Split(Request.ServerVariables["URL"], "/");

                if (sUrlAux[1].ToUpper() != "GEMO") Session["strServer"] = "/";
                else Session["strServer"] = "/GEMO/";

/*                
              string strServer = Request.ServerVariables["URL"].ToUpper();

              string strProyecto = "GEMONET";

                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E" && System.Configuration.ConfigurationManager.ConnectionStrings["VSNET"].ToString().ToUpper() == "N")
                    strProyecto = "GEMO";

                int intWeb = strServer.IndexOf(strProyecto);

                if (intWeb != -1)
                {
                    Session["strServer"] = "/" + strProyecto + "/";
                }
                else
                {
                    Session["strServer"] = "/";
                }
*/ 

                if (Request.QueryString["scr"] != null)
                {
                    Session["IDRED"] = Utilidades.decodpar(Request.QueryString["scr"].ToString());
                }
                else
                {
                    //Captura del usuario de red.
                    string[] aIdRed = Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\");
                    Array.Reverse(aIdRed);
                    Session["IDRED"] = aIdRed[0];
                }
                //Session["IDRED"] = "DOPEOTCA";  // INVITADO Y BENEFICIARIO
                //Session["IDRED"] = "DOARHUMI";  // CONTROLADOR
                //Session["IDRED"] = "DOLAGAJM";  // INVITADO
                //Session["IDRED"] = "DOGULESA";  // NADA
                //Session["IDRED"] = "DOLORIIZ";
                //Session["IDRED"] = "DOBAGOMO";//Mónica Bajo
                PROFESIONALES oProf = PROFESIONALES.Obtener(Session["IDRED"].ToString());
                if (oProf.bIdentificado)
                {
                    Session["IDFICEPI"] = oProf.IdFicepi;
                    Session["PROFESIONAL"] = oProf.Profesional;
                    Session["ADMIN"] = oProf.Administrador;
                    Session["NOMBRE"] = oProf.Nombre;
                    Session["APELLIDO1"] = oProf.Apellido1;
                    Session["APELLIDO2"] = oProf.Apellido2;

                    #region Valores que se cargan una única vez al entrar
                    if (Session["IDFICEPI_ENTRADA"] == null)
                    {
                        Session["IDFICEPI_ENTRADA"] = oProf.IdFicepi;
                        Session["IDRED_ENTRADA"] = Session["IDRED"];
                        Session["PROFESIONAL_ENTRADA"] = oProf.Nombre;
                        Session["BTN_FECHA"] = oProf.t001_botonfecha;
                        //Session["ADMIN_ENTRADA"] = oProf.Administrador;
                    }
                    #endregion

                    //if (!PROFESIONALES.bPerteneceDIS(int.Parse(Session["IDFICEPI_ENTRADA"].ToString())))
                    //    strMsg += "Acceso no permitido";

                    strEnlace = Session["strServer"] + "Capa_Presentacion/Inicio/Default.aspx";
                    PROFESIONALES.CargarRoles(oProf.IdFicepi, Session["ADMIN"].ToString());
                 }
                else
                {
                    strMsg = "No se han podido obtener los datos del usuario '" + Session["IDRED"].ToString() + "'";
                }
            }
        }
        #endregion
    }
}
