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

public partial class Capa_Presentacion_Administracion_CuadroMandoCambioRol_SolicitudesNoAprobadas_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.TramitacionCambioRol> solicitudesNoAprobadasCat()
    {
        IB.Progress.BLL.TramitacionCambioRol pro = null;
        try
        {
            List<IB.Progress.Models.TramitacionCambioRol> profesionales = null;
            pro = new IB.Progress.BLL.TramitacionCambioRol();

            profesionales = pro.getSolicitudesSegunEstado(Convert.ToChar("X"), ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);

            pro.Dispose();

            return profesionales;

        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            throw ex;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void aceptarNoAprobacion(List<IB.Progress.Models.TramitacionCambioRol> oProfesional)
    {
        IB.Progress.BLL.TramitacionCambioRol solicitud = new IB.Progress.BLL.TramitacionCambioRol();
        StringBuilder sbPromotor = null;
        StringBuilder sbOtrosEvaluadores = null;        
        StringBuilder sbAprobador = null;
        try
        {
            //Update
            solicitud.CambioEstadoSolicitudROL(oProfesional);
            solicitud.Dispose();

            //Envío de Correos
            foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
            {
                sbPromotor = new StringBuilder();

                //ENVIAR CORREO AL PROMOTOR
                sbPromotor.Append(item.nomCortoPromotor + ", el cambio de rol que solicitaste para " + item.nombre_interesado + ", no ha sido aprobado por "+ item.aprobador +". </br></br>");

                sbPromotor.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                sbPromotor.Append("Rol no aprobado: " + item.t940_desrolPropuesto + "</br></br>");
                sbPromotor.Append("Motivo de la no aprobación: </br> " + item.t940_motivorechazo + "</br>");

                if (item.CorreoPromotor != "") Correo.Enviar("PROGRESS: Cambio de rol", sbPromotor.ToString(), item.CorreoPromotor);

                //ENVIAR CORREO AL APROBADOR
                sbAprobador = new StringBuilder();

                sbAprobador.Append(item.nomCortoAprobador + ", has rechazado el cambio de rol solicitado por " + item.nombre_promotor + " para " + item.nombre_interesado + ".</br></br>");

                sbAprobador.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                sbAprobador.Append("Rol rechazado: " + item.t940_desrolPropuesto + "</br></br>");
                sbAprobador.Append("Motivo del rechazo: </br> " + item.t940_motivorechazo + "</br>");


                if (item.CorreoAprobador != "") Correo.Enviar("PROGRESS: Cambio de rol", sbAprobador.ToString(), item.CorreoAprobador);


                //ENVIAR CORREO A LOS EVALUADORES INTERMEDIOS
                IB.Progress.BLL.Profesional bllProfesional = new IB.Progress.BLL.Profesional();
                List<Profesional> lstEvaluadores = bllProfesional.getAscendientesHastaAprobador(item.t001_idficepi_promotor);

                for (int i = 0; i < lstEvaluadores.Count; i++)
                {                    
                    sbOtrosEvaluadores = new StringBuilder();                    
                    sbOtrosEvaluadores.Append(lstEvaluadores[i].nombre + ", el cambio de rol solicitado por "+ item.nombre_promotor+" para " + item.nombre_interesado + ", no ha sido aprobado por "+item.aprobador+ ". </br></br>");
                    sbOtrosEvaluadores.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                    sbOtrosEvaluadores.Append("Rol no aprobado: " + item.t940_desrolPropuesto + "</br></br>");
                    sbOtrosEvaluadores.Append("Motivo de la no aprobación: </br> " + item.t940_motivorechazo + "</br>");
                    if (lstEvaluadores[i].Correo != "") Correo.Enviar("PROGRESS: Información sobre cambio de rol", sbOtrosEvaluadores.ToString(), lstEvaluadores[i].Correo);
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

    /// <summary>
    /// Dejamos las solicitud en Stand by
    /// </summary>
    /// <param name="oProfesional"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void standby(List<IB.Progress.Models.TramitacionCambioRol> oProfesional)
    {
        try
        {
            IB.Progress.BLL.TramitacionCambioRol solicitud = new IB.Progress.BLL.TramitacionCambioRol();
            
            StringBuilder sbAprobador = null;
            solicitud.CambioEstadoSolicitudROL(oProfesional);
            solicitud.Dispose();

            //CORREO AL APROBADOR
            foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
            {                
                sbAprobador = new StringBuilder();
                sbAprobador.Append(item.nomCortoAprobador + ", la Oficina Técnica de PROGRESS se va a poner en contacto contigo para recabar más información acerca de la solicitud de cambio de rol que, "+item.nombre_promotor+" propuso para "+ item.nombre_interesado+".</br></br>");                
                sbAprobador.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                sbAprobador.Append("Rol propuesto: " + item.t940_desrolPropuesto + "</br></br>");

                sbAprobador.Append("Motivo de la propuesta: </br> " + item.t940_motivopropuesto + "</br></br>");
                sbAprobador.Append("Motivo de la no aprobación: </br> " + item.t940_motivorechazo + "</br>");


                Correo.Enviar("PROGRESS: Cambio de rol, pendiente de confirmar", sbAprobador.ToString(), item.CorreoAprobador);                
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }


}