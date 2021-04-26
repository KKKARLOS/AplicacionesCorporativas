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

public partial class Capa_Presentacion_Administracion_CuadroMandoCambioRol_SolicitudesAprobadas_Default : System.Web.UI.Page
{
    public string idficepi;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Se obtienen de las variables de sesión el idficepi y nombre del usuario conectado
        idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.TramitacionCambioRol> solicitudesAprobadasCat()
    {
        IB.Progress.BLL.TramitacionCambioRol pro = null;
        try
        {
            List<IB.Progress.Models.TramitacionCambioRol> profesionales = null;
            pro = new IB.Progress.BLL.TramitacionCambioRol();

            profesionales = pro.getSolicitudesSegunEstado(Convert.ToChar("P"),((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);

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
    public static void AceptarPropuesta(List<IB.Progress.Models.TramitacionCambioRol> oProfesional)
    {
        IB.Progress.BLL.TramitacionCambioRol solicitud = new IB.Progress.BLL.TramitacionCambioRol();

        List<IB.Progress.Models.TramitacionCambioRol> oProfbyID = null;

        StringBuilder sbPromotor = null;
        StringBuilder sbOtrosEvaluadores = null;
        StringBuilder sbInteresado = null;
        StringBuilder sbAprobador = null;
        try
        {
            //Update de cambio de Rol
            solicitud.AceptarCambioRol(oProfesional);
            solicitud.Dispose();

            oProfbyID = new List<TramitacionCambioRol>();

            //Linq.. Devuelve los id promotor distintos
            //var oProfbyID2 = (from e in oProfesional
            //                  select e.t001_idficepi_promotor
            //              ).Distinct().ToList();

            //Agrupa por id promotor y select profesionales
            //var result = oProfesional.GroupBy(objeto => objeto.t001_idficepi_promotor)
            //       .Select(grp => grp.First())
            //       .ToList();

            //var newList = oProfesional.GroupBy(x => x.t001_idficepi_promotor)
            //                                      .Select(group => new { GroupID = group.Key, Profesionales = group.ToList() })
            //                                      .ToList();

            //Envío de Correos
            foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
            {                                
                //ENVIAR CORREO AL PROMOTOR
                sbPromotor = new StringBuilder();
                sbPromotor.Append(item.nomCortoPromotor + ", el cambio de rol que solicitaste para " + item.nombre_interesado + ", se ha producido y comunicado al profesional. </br></br>");

                sbPromotor.Append("Rol anterior: " + item.t940_desrolActual + "</br>");
                sbPromotor.Append("Rol nuevo: " + item.t940_desrolPropuesto + "</br>");

                if (item.CorreoPromotor != "") Correo.Enviar("PROGRESS: Cambio de rol confirmado", sbPromotor.ToString(), item.CorreoPromotor);


                //ENVIAR CORREO AL INTERESADO
                sbInteresado = new StringBuilder();

                sbInteresado.Append(item.nomCortoInteresado + ", el cambio de rol solicitado por " + item.nombre_promotor + " para tí, ya es efectivo.</br></br>");

                sbInteresado.Append("Rol anterior: " + item.t940_desrolActual + "</br>");
                sbInteresado.Append("Rol nuevo: " + item.t940_desrolPropuesto + "</br>");

                if (item.correointeresado != "") Correo.Enviar("PROGRESS: Cambio de rol confirmado", sbInteresado.ToString(), item.correointeresado);


                //ENVIAR CORREO AL APROBADOR
                sbAprobador = new StringBuilder();

                sbAprobador.Append(item.nomCortoAprobador + ", el cambio de rol solicitado por " + item.nombre_promotor + " para "+ item.nombre_interesado +", se ha producido y comunicado al profesional.</br></br>");

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
                    if (lstEvaluadores[i].Correo != "") Correo.Enviar("PROGRESS: Información sobre cambio de rol confirmado", sbOtrosEvaluadores.ToString(), lstEvaluadores[i].Correo);
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

            StringBuilder sbPromotor = null;
            StringBuilder sbAprobador = null;
            solicitud.CambioEstadoSolicitudROL(oProfesional);
            solicitud.Dispose();

            foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
            {
                sbPromotor = new StringBuilder();
                sbAprobador = new StringBuilder();

                //Correo al aprobador
                sbAprobador.Append(item.nomCortoAprobador + ", la Oficina Técnica de PROGRESS se va a poner en contacto con " + item.nombre_promotor + ", evaluador de "+ item.nombre_interesado +", para recabar más información acerca de la solicitud de cambio de rol que propuso para él y que tú inicialmente has validado.</br></br>");
                sbAprobador.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                sbAprobador.Append("Rol propuesto: " + item.t940_desrolPropuesto + "</br></br>");
                sbAprobador.Append("Motivo de la propuesta:</br>");
                sbAprobador.Append(item.t940_motivopropuesto);

                Correo.Enviar("PROGRESS: Cambio de rol, pendiente de confirmar", sbAprobador.ToString(), item.CorreoAprobador);

                //Correo al promotor
                sbPromotor.Append(item.nomCortoPromotor + ", la Oficina Técnica de PROGRESS se va a poner en contacto contigo, para recabar más información acerca de la solicitud de cambio de rol de " + item.nombre_interesado + ".</br></br>");
                sbPromotor.Append("Rol actual: " + item.t940_desrolActual + "</br>");
                sbPromotor.Append("Rol propuesto: " + item.t940_desrolPropuesto + "</br></br>");
                sbPromotor.Append("Motivo de la propuesta:</br>");
                sbPromotor.Append(item.t940_motivopropuesto);

                Correo.Enviar("PROGRESS: Cambio de rol, pendiente de confirmar", sbPromotor.ToString(), item.CorreoPromotor);
            }
        }
        catch (Exception ex) 
        {
            
            throw ex;
        }
       
    }
}