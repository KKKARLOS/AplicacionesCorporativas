using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IB.Progress.Shared;
using Newtonsoft.Json;
using System.Text;
using IB.Progress.Models;
using System.Configuration;

public partial class Capa_Presentacion_MiEquipo_GestionarSolicitudCambioRol_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

   
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.TramitacionCambioRol> solicitudCambioRolCat()
    {
        IB.Progress.BLL.TramitacionCambioRol pro = null;
        try
        {
            List<IB.Progress.Models.TramitacionCambioRol> profesionales = null;
            pro = new IB.Progress.BLL.TramitacionCambioRol();

            profesionales = pro.catalogoSolicitudes(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
                      
            pro.Dispose();

            return profesionales;
        }
        catch (Exception)
        {
            if (pro != null) pro.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Aceptar la propuesta de cambio de rol
    /// </summary>
    /// <param name="t940_idtramitacambiorol"></param>
    /// <param name="idficepi_interesado"></param>
    /// <param name="t004_idrol_propuesto"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void AceptarPropuesta(int t940_idtramitacambiorol, int idficepi_interesado, int t004_idrol_propuesto, string nombre_promotor, string nombreapellidosinteresado, string rolantiguo, string rolnuevo, string correoresporigen, string nombre_interesado, string nombreapellidos_promotor, string correointeresado, string motivo)
    {
        IB.Progress.BLL.TramitacionCambioRol solicitud = new IB.Progress.BLL.TramitacionCambioRol();        
        try
        {            
            //Aceptación de propuesta (La dejamos en estado P ... en manos de RRHH)
            solicitud.CambioEstadoSolicitudCambioRol(t940_idtramitacambiorol, Convert.ToChar("P"), ((Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
            solicitud.Dispose();

            //Se envía correo a OT
            StringBuilder sb = new StringBuilder();

            sb.Append(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha aprobado la solicitud de cambio de rol, que " + nombreapellidos_promotor + " realizó para  " + nombreapellidosinteresado + ". </br></br>");
            sb.Append("Debes acceder a PROGRESS para aprobar, o no, dicho cambio, explicando el motivo de tu decisión.</br></br>");
            sb.Append("Rol actual: " + rolantiguo + "</br>");
            sb.Append("Rol propuesto: " + rolnuevo + "</br></br>");
            sb.Append("Motivo de la propuesta: </br> " + motivo + "</br></br>");

            sb.Append("</br></br></br></br>Si te encuentras en las oficinas de IBERMÁTICA, puedes acceder directamente pulsando <a href=" + ConfigurationManager.AppSettings["UrlInterna"] + "/Default.aspx?CR=true>aquí</a>");
            sb.Append("</br>Si estás fuera, puedes acceder pulsando <a href=" + ConfigurationManager.AppSettings["UrlExterna"] + "/Default.aspx?CR=true>aquí</a>");

            Correo.Enviar("PROGRESS: Aprobación de cambio de rol", sb.ToString(), ConfigurationManager.AppSettings["SMTP_to_OTRRHH"]);

        }
        catch (Exception)
        {
            if (solicitud != null) solicitud.Dispose();
            throw;
        }
                
    }

    /// <summary>
    /// Rechaza la propuesta de cambio de rol
    /// </summary>
    /// <param name="t940_idtramitacambiorol"></param>
    /// <param name="motivorechazo"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void RechazarPropuesta(int t940_idtramitacambiorol, string motivorechazo, string nombre_promotor, string nombreapellidosinteresado, string rolantiguo, string rolnuevo, string correoresporigen, string motivo)
    {
        IB.Progress.BLL.TramitacionCambioRol solicitud = new IB.Progress.BLL.TramitacionCambioRol();

        try
        {                        
            solicitud.NoAceptacion(t940_idtramitacambiorol, motivorechazo, ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);            
            solicitud.Dispose();

            //Correo a la OT
            StringBuilder sb = new StringBuilder();

            sb.Append(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " no ha aprobado la solicitud de cambio de rol que " + nombre_promotor + " propuso para " + nombreapellidosinteresado + ". </br></br>");

            sb.Append("Rol actual: " + rolantiguo + "</br>");
            sb.Append("Rol propuesto: " + rolnuevo + "</br></br>");
            sb.Append("Motivo de la propuesta: </br> " + motivo + "</br></br>");
            sb.Append("Motivo de la decisión: </br> " + motivorechazo + "</br></br>");

            sb.Append("</br></br></br></br>Si te encuentras en las oficinas de IBERMÁTICA, puedes acceder directamente pulsando <a href=" + ConfigurationManager.AppSettings["UrlInterna"] + "/Default.aspx?CR=true>aquí</a>");
            sb.Append("</br>Si estás fuera, puedes acceder pulsando <a href=" + ConfigurationManager.AppSettings["UrlExterna"] + "/Default.aspx?CR=true>aquí</a>");

            Correo.Enviar("PROGRESS: Cambio de rol", sb.ToString(), ConfigurationManager.AppSettings["SMTP_to_OTRRHH"]);

        }
        catch (Exception ex)
        {
            if (solicitud != null) solicitud.Dispose();
            throw ex;
        }

    }

}