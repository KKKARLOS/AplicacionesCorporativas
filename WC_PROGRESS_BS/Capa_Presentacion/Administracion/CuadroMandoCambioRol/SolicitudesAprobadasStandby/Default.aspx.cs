using IB.Progress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_CuadroMandoCambioRol_SolicitudesAprobadasStandby_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.TramitacionCambioRol> solicitudesCat()
    {
        IB.Progress.BLL.TramitacionCambioRol pro = null;
        try
        {
            List<IB.Progress.Models.TramitacionCambioRol> profesionales = null;
            pro = new IB.Progress.BLL.TramitacionCambioRol();

            profesionales = pro.getSolicitudesSegunEstado(Convert.ToChar("Y"), ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);

            pro.Dispose();

            return profesionales;

        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            throw ex;
        }
    }

    /// <summary>
    /// RRHH no aprueba el cambio de Rol que aceptó el Director (aprobador)
    /// </summary>
    /// <param name="oProfesional"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void AprobacionNoAceptada(List<IB.Progress.Models.TramitacionCambioRol> oProfesional)
    {
        try
        {
            IB.Progress.BLL.TramitacionCambioRol solicitud = new IB.Progress.BLL.TramitacionCambioRol();

            StringBuilder sbPromotor = null;
            StringBuilder sbAprobador = null;
            StringBuilder sbOtrosEvaluadores = null;

            solicitud.CambioEstadoSolicitudROL(oProfesional);
            solicitud.Dispose();

            //Envío de correos
            foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
            {
                sbPromotor = new StringBuilder();
                sbAprobador = new StringBuilder();

                //CORREO AL APROBADOR
                sbAprobador.Append(item.nomCortoAprobador + ", el cambio de rol solicitado por " + item.nombre_promotor + " para " + item.nombre_interesado + ", no ha sido aprobado por la OT después de analizar la situación.</br></br>");
                sbAprobador.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                sbAprobador.Append("Rol desestimado: " + item.t940_desrolPropuesto + "</br>");

                Correo.Enviar("PROGRESS: Cambio de rol", sbAprobador.ToString(), item.CorreoAprobador);

                //CORREO AL PROMOTOR
                sbPromotor.Append(item.nomCortoPromotor + ", el cambio de rol que solicitaste para  " + item.nombre_interesado + " no ha sido aprobado después de analizar la situación.</br></br>");
                sbPromotor.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                sbPromotor.Append("Rol desestimado: " + item.t940_desrolPropuesto + "</br>");
                Correo.Enviar("PROGRESS: Cambio de rol", sbPromotor.ToString(), item.CorreoPromotor);

                //CORREO A LOS EVALUADORES INTERMEDIOS            
                IB.Progress.BLL.Profesional bllProfesional = new IB.Progress.BLL.Profesional();
                List<Profesional> lstEvaluadores = bllProfesional.getAscendientesHastaAprobador(item.t001_idficepi_promotor);

                for (int i = 0; i < lstEvaluadores.Count; i++)
                {
                    sbOtrosEvaluadores = new StringBuilder();
                    sbOtrosEvaluadores.Append(lstEvaluadores[i].nombre + ", el cambio de rol solicitado por " + item.nombre_promotor + " para  " + item.nombre_interesado + ", no ha sido aprobado después de analizar la situación.</br></br>");
                    sbOtrosEvaluadores.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                    sbOtrosEvaluadores.Append("Rol desestimado: " + item.t940_desrolPropuesto + "</br>");
                    if (lstEvaluadores[i].Correo != "") Correo.Enviar("PROGRESS: Información sobre cambio de rol", sbOtrosEvaluadores.ToString(), lstEvaluadores[i].Correo);
                }

                bllProfesional.Dispose();
            }
        }
        catch (Exception ex)
        {            
            throw ex;
        }

    }

    /// <summary>
    /// RRHH aprueba el cambio de Rol que aceptó del Director (aprobador)
    /// </summary>
    /// <param name="oProfesional"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void AceptarPropuesta(List<IB.Progress.Models.TramitacionCambioRol> oProfesional)
    {
        IB.Progress.BLL.TramitacionCambioRol solicitud = new IB.Progress.BLL.TramitacionCambioRol();
        StringBuilder sbPromotor = null;
        StringBuilder sbOtrosEvaluadores = null;
        StringBuilder sbInteresado = null;
        StringBuilder sbAprobador = null;
        try
        {
            //Update de cambio de Rol
            solicitud.AceptarCambioRol(oProfesional);
            solicitud.Dispose();

            //Envío de Correos
            foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
            {
                sbPromotor = new StringBuilder();

                //ENVIAR CORREO AL PROMOTOR
                sbPromotor.Append(item.nomCortoPromotor + ", el cambio de rol que solicitaste para " + item.nombre_interesado + ", se ha producido y comunicado al profesional.</br></br>");
                sbPromotor.Append("Rol anterior: " + item.t940_desrolActual + "</br>");
                sbPromotor.Append("Rol nuevo: " + item.t940_desrolPropuesto + "</br>");

                if (item.CorreoPromotor != "") Correo.Enviar("PROGRESS: Cambio de rol confirmado", sbPromotor.ToString(), item.CorreoPromotor);


                //ENVIAR CORREO AL INTERESADO
                sbInteresado = new StringBuilder();

                sbInteresado.Append(item.nomCortoInteresado + ", el cambio de rol solicitado por " + item.nombre_promotor + " para ti, ya es efectivo.</br></br>");

                sbInteresado.Append("Rol anterior: " + item.t940_desrolActual + "</br>");
                sbInteresado.Append("Rol nuevo: " + item.t940_desrolPropuesto + "</br>");

                if (item.correointeresado != "") Correo.Enviar("PROGRESS: Cambio de rol confirmado", sbInteresado.ToString(), item.correointeresado);


                //ENVIAR CORREO AL APROBADOR
                sbAprobador = new StringBuilder();
                sbAprobador.Append(item.nomCortoAprobador + ", el cambio de rol solicitado por " + item.nombre_promotor + " para " + item.nombre_interesado + ", se ha producido y comunicado al profesional.</br></br>");
                sbAprobador.Append("Rol anterior: " + item.t940_desrolActual + "</br>");
                sbAprobador.Append("Rol nuevo: " + item.t940_desrolPropuesto + "</br>");

                if (item.CorreoAprobador != "") Correo.Enviar("PROGRESS: Cambio de rol confirmado", sbAprobador.ToString(), item.CorreoAprobador);

                //ENVIAR CORREO A LOS EVALUADORES INTERMEDIOS
                IB.Progress.BLL.Profesional bllProfesional = new IB.Progress.BLL.Profesional();
                List<Profesional> lstEvaluadores = bllProfesional.getAscendientesHastaAprobador(item.t001_idficepi_promotor);

                for (int i = 0; i < lstEvaluadores.Count; i++)
                {                    
                    sbOtrosEvaluadores = new StringBuilder();
                    sbOtrosEvaluadores.Append(lstEvaluadores[i].nombre + ", el cambio de rol solicitado por " + item.nombre_promotor + " para " + item.nombre_interesado + ", se ha producido y comunicado al profesional.</br></br>");
                    sbOtrosEvaluadores.Append("Rol anterior: " + item.t940_desrolActual + "</br>");
                    sbOtrosEvaluadores.Append("Rol nuevo: " + item.t940_desrolPropuesto + "</br>");
                    if (lstEvaluadores[i].Correo != "") Correo.Enviar("PROGRESS: Cambio de rol confirmado", sbOtrosEvaluadores.ToString(), lstEvaluadores[i].Correo);
                }

                bllProfesional.Dispose();
            }


        }
        catch (Exception ex)
        {
            if (solicitud != null) solicitud.Dispose();
            throw ex;
        }

    }
}