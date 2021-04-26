using IB.Progress.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IB.Progress.Models;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using System.Configuration;

public partial class Capa_Presentacion_MiEquipo_SolicitarCambioRol_Default : System.Web.UI.Page
{

    //private static string nombreaprobador = String.Empty;
    //private static string correoaprobador = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        //miequipo.Last().correoaprobador
        string correoaprobador = String.Empty;
        string nombreaprobador = String.Empty;
        string nombreapellidosaprobador = String.Empty;

        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
        List<MIEQUIPO.profesional_CRol> miequipo = null;
        bool bCont = true;
        try
        {
            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            miequipo = miequipoBLL.CatCambioRol(((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi);
            nombreaprobador = miequipo.Last().nombreaprobador;
            correoaprobador = miequipo.Last().correoaprobador;
            nombreapellidosaprobador = miequipo.Last().nombreapellidosaprobador;

            miequipoBLL.Dispose();
        }
        catch (IB.Progress.Shared.IBException ibex)
        {
            if (miequipoBLL != null) miequipoBLL.Dispose();
            bCont = false;

            string msgerr = "";
            switch (ibex.ErrorCode)
            {
                case 100:
                case 101:
                    msgerr = ibex.Message;
                    break;
            }
            PieMenu.sErrores = "msgerr = '" + msgerr + "';";
            //Avisar a EDA por smtp
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ibex.ToString());
        }
        catch (Exception ex)
        {
            if (miequipoBLL != null) miequipoBLL.Dispose();
            bCont = false;
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
        }
        if (bCont)
        {
            try
            {
                foreach (IB.Progress.Models.MIEQUIPO.profesional_CRol prof in miequipo)
                {
                    if (prof.idficepi != 0)
                    {
                        HtmlTableCell htblcel1 = new HtmlTableCell();
                        if (prof.estado != null)
                        {
                            HtmlGenericControl aux = new HtmlGenericControl();
                            if (prof.estado == 1)//Salida en tramite
                            {
                                aux.TagName = "span";
                                aux.Attributes.Add("class", "glyphicon glyphicon-new-window");
                            }

                            else if (prof.estado == 3)//Salida rechazada
                            {
                                aux.TagName = "i";
                                aux.Attributes.Add("class", "fa fa-compress");
                            }
                            else if (prof.estado == 6)//Salida en tramite(mediación de RRHH solicitada)
                            {
                                aux.TagName = "i";
                                aux.Attributes.Add("class", "glyphicon glyphicon-user");
                            }

                            htblcel1.Controls.Add(aux);
                        }
                        HtmlGenericControl nombre = new HtmlGenericControl("span");
                        nombre.InnerText = prof.prof;
                        htblcel1.Controls.Add(nombre);
                        HtmlTableCell htblcel2 = new HtmlTableCell();
                        htblcel2.InnerText = prof.rol;
                        HtmlTableCell htblcel3 = new HtmlTableCell();
                        HtmlTableRow htblrow = new HtmlTableRow();
                        if (prof.t940_idtramitacambiorol != -1)
                        {
                            htblcel3.InnerText = prof.rol_prop;
                            htblrow.Attributes.Add("id", prof.t940_idtramitacambiorol.ToString());
                            //htblrow.Attributes.Add("data-correoaprobador", prof.correoaprobador.ToString());
                            //htblrow.Attributes.Add("data-nombreaprobador", prof.nombreaprobador.ToString());
                        }

                        HtmlTableCell htblcel4 = new HtmlTableCell();

                        if (prof.t940_motivopropuesto != "") htblcel4.InnerHtml = "<a data-placement='top' data-toggle='popover' title='' href='#' data-content='" + prof.t940_motivopropuesto + "'><i class='glyphicon glyphicon-comment text-primary'></i></a>";

                        htblrow.Attributes.Add("idficepi", prof.idficepi.ToString());
                        htblrow.Attributes.Add("data-correoaprobador", correoaprobador);
                        htblrow.Attributes.Add("data-nombreaprobador", nombreaprobador);
                        htblrow.Attributes.Add("data-nombreapellidosaprobador", nombreapellidosaprobador);
                        
                        if (prof.t940_resolucion != null) htblrow.Attributes.Add("data-t940_resolucion", prof.t940_resolucion.ToString());
                        if (prof.nombreapellidosprofesional != null) htblrow.Attributes.Add("data-nombreapellidosprofesional", prof.nombreapellidosprofesional.ToString());
                        htblrow.Cells.Add(htblcel1);
                        htblrow.Cells.Add(htblcel2);
                        htblrow.Cells.Add(htblcel3);
                        htblrow.Cells.Add(htblcel4);
                        idtbody.Controls.Add(htblrow); 
                    }
                }
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "msgerr = 'Ocurrió un error obteniendo los roles de base de datos';", true);
                PieMenu.sErrores = "msgerr = 'Ocurrió un error cargando los datos de mi equipo';";
                Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<ROLIB> obtenerRoles()
    {
        IB.Progress.BLL.ROLIB bllRoles = new IB.Progress.BLL.ROLIB();
        List<ROLIB> roles = bllRoles.Catalogo();
        bllRoles.Dispose();
        return roles;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string insert(TRAMITACIONCAMBIOROL_INS tcr_i, string nombreapellidosinteresado, string rolactual, string rolpropuesto, string motivo, string nombreaprobador, string correoaprobador, string nombreapellidosaprobador)
    {        
        try
        {
            //Añado al objto el idpromotor
            tcr_i.t001_idficepi_promotor = ((Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi;
            IB.Progress.BLL.TramitacionCambioRol blltcr_i = new IB.Progress.BLL.TramitacionCambioRol();
            string result = blltcr_i.Insert(tcr_i);
            blltcr_i.Dispose();

            
            //Correo para el aprobador           
            StringBuilder sb = new StringBuilder();

            sb.Append(nombreaprobador + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha solicitado un cambio de rol para " + nombreapellidosinteresado + ". </br> Debes acceder a PROGRESS para aprobar, o no,  dicho cambio, explicando el motivo de tu decisión.</br></br>");

            sb.Append("Rol actual: " + rolactual + "</br>");
            sb.Append("Rol propuesto: " + rolpropuesto + "</br></br>");
            sb.Append("Motivo de la propuesta: </br></br>" + motivo);
            sb.Append("</br></br></br></br>Si te encuentras en las oficinas de IBERMÁTICA, puedes acceder directamente pulsando <a href=" + ConfigurationManager.AppSettings["UrlInterna"] + "/Default.aspx?APROL=true>aquí</a>");
            sb.Append("</br>Si estás fuera, puedes acceder pulsando <a href=" + ConfigurationManager.AppSettings["UrlExterna"] + "/Default.aspx?APROL=true>aquí</a>");

            if (correoaprobador != "") Correo.Enviar("PROGRESS: Solicitud de cambio de rol", sb.ToString(), correoaprobador);

            //Correo a los evaluadores intermedios            
            IB.Progress.BLL.Profesional bllProfesional = new IB.Progress.BLL.Profesional();
            List<Profesional> lstEvaluadores = bllProfesional.getAscendientesHastaAprobador(((Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);

            StringBuilder sbEvaluadores = new StringBuilder();

            for (int i = 0; i < lstEvaluadores.Count; i++)
            {
                sbEvaluadores.Append(lstEvaluadores[i].nombre + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha solicitado un cambio de rol para " + nombreapellidosinteresado + ", que gestionará " + nombreapellidosaprobador + ".</br></br>");
                sbEvaluadores.Append("Rol actual: " + rolactual + "</br>");
                sbEvaluadores.Append("Rol propuesto: " + rolpropuesto + "</br></br>");
                sbEvaluadores.Append("Motivo del cambio: </br></br>" + motivo);

                if (lstEvaluadores[i].Correo != "") Correo.Enviar("PROGRESS: Información sobre solicitud de cambio de rol", sbEvaluadores.ToString(), lstEvaluadores[i].Correo);
            }

            bllProfesional.Dispose();
            
            return result;
        }
        catch (Exception ex)
        {            
            Smtp.SendSMTP("Error al solicitar cambio de rol", ex.ToString());
            throw ex;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void delete(int t940_idtramitacambiorol, string nombreapellidosinteresado, string rolactual, string rolpropuesto, string motivo, string nombreaprobador, string correoaprobador)
    {
        try
        {
            IB.Progress.BLL.TramitacionCambioRol blltcr_i = new IB.Progress.BLL.TramitacionCambioRol();

            blltcr_i.Delete(t940_idtramitacambiorol);
            blltcr_i.Dispose();

            StringBuilder sb = new StringBuilder();

            sb.Append(nombreaprobador + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha anulado la solicitud de cambio de Rol que había realizado para " + nombreapellidosinteresado + ".</br></br>");

            sb.Append("Rol actual: " + rolactual + "</br>");
            sb.Append("Rol propuesto: " + rolpropuesto + "</br></br>");
            sb.Append("Motivo: </br></br>" + motivo);

            if (correoaprobador != "") Correo.Enviar("PROGRESS: Anulación de solicitud de cambio de Rol", sb.ToString(), correoaprobador);

        }
        catch (Exception ex)
        {
            Smtp.SendSMTP("Error al rechazar un cambio de rol", ex.ToString());
            throw ex;
        }

    }
}