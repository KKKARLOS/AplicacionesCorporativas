using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Evaluacion_Abrir_Default : System.Web.UI.Page
{
    public string js_miequipo = "";
        
    protected void Page_Load(object sender, EventArgs e)
    {
        // Stop Caching
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        // Stop Caching
        IB.Progress.Models.MIEQUIPO miequipo = null;
        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
        bool bCont = true;

        try {
            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            miequipo = miequipoBLL.CatalogoAbrirEvaluacion(((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi);
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
            //SMTP.send(asunto, ibex, .......);
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener el catálogo de mi equipo. (Abrir nueva evaluación)", msgerr);
        }
        catch (Exception ex)
        {
            if (miequipoBLL != null) miequipoBLL.Dispose();
            bCont = false;
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            //SMTP.send(asunto, ex, .......);
            IB.Progress.Shared.Smtp.SendSMTP("Ocurrió un error general en la aplicación.", ex.Message);
        }

        if (bCont)
        {
            try
            {
                //Variable 'miequipo' de javascript
                StringBuilder jsmiequipo = new StringBuilder();
                jsmiequipo.Append("var miequipo=[];");

                foreach (IB.Progress.Models.MIEQUIPO.profesional prof in miequipo.profesionales)
                {
                    //Lleno la lista de profesionales de mi equipo
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.Attributes.Add("value", prof.idficepi.ToString());
                    if (prof.estado != null || prof.evaluacionEnCurso ||prof.evaluacionAbierta)
                    {
                        HtmlGenericControl aux = new HtmlGenericControl();
                        if (prof.estado == 1)
                        {
                            aux.TagName = "span";
                            aux.Attributes.Add("class", "glyphicon glyphicon-new-window");
                        }
                        else if (prof.estado == 3 || prof.estado == 6)
                        {
                            aux.TagName = "i";
                            aux.Attributes.Add("class", "fa fa-compress");
                        }

                        else if (prof.evaluacionAbierta)
                        {

                            aux.InnerHtml = "<a data-placement='right' data-toggle='  popover' data-container='body' href='#' title='' data-content='Evaluación pendiente de tu firma'><i class='fa fa-file-text-o verde'></i></a>";
                            //aux.TagName = "i";
                            //aux.Attributes.Add("class", "fa fa-file-text-o verde");
                            //aux.Attributes.Add("data-toggle", "tooltip");
                            //aux.Attributes.Add("data-original-title", "Evaluación pendiente de firma del evaluador");
                        }

                        else if (prof.evaluacionEnCurso)
                        {
                            aux.InnerHtml = "<a data-placement='right' data-toggle='popover' href='#' title='' data-container='body'  data-content='Evaluación pendiente de la firma de " + prof.Nombreevaluado + "'><i class='fa fa-file-text-o azul'></i></a>";
                            //aux.TagName = "i";
                            //aux.Attributes.Add("class", "fa fa-file-text-o azul");
                            //aux.Attributes.Add("data-toggle", "tooltip");
                            //aux.Attributes.Add("data-original-title", "Evaluación pendiente de firma del evaluado");

                        }
                        listItem.Attributes.Add("class", "list-group-item pend");
                        listItem.Controls.Add(aux);
                    }
                    HtmlTableCell nombre = new HtmlTableCell("span");
                    nombre.InnerText = prof.prof;
                    listItem.Controls.Add(nombre);
                    lisMiEquipo.Controls.Add(listItem);

                    jsmiequipo.Append("var oProf = new Object();");
                    //Cargo el objeto 'miequipo' para javascript
                    foreach (PropertyInfo prop in prof.GetType().GetProperties())
                    {
                        if (prop.PropertyType.Name == "String")
                            jsmiequipo.Append("oProf." + prop.Name + " = '" + ((prop.GetValue(prof, null) != null) ? prop.GetValue(prof, null).ToString() : "") + "';");
                        else
                            jsmiequipo.Append("oProf." + prop.Name + " = " + ((prop.GetValue(prof, null) != null) ? prop.GetValue(prof, null).ToString().ToLower() : "null") + ";");
                    }
                    jsmiequipo.Append("oProf.aevaluar=false;");
                    jsmiequipo.Append("miequipo.push(oProf);");
                }
                //Registro en la página la estructura del array de mi equipo
                js_miequipo = jsmiequipo.ToString();
            }
            catch (Exception)
            {
                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "msgerr = 'Ocurrió un error obteniendo los roles de base de datos';", true);
                PieMenu.sErrores = "msgerr = 'Ocurrió un error cargando los datos de mi equipo';";
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.VALORACIONESPROGRESS.ImprimirInsertados> insertarEvaluaciones(List<int> listaProfesionales)
    {
        try
        {
            IB.Progress.BLL.VALORACIONESPROGRESS valpro = new IB.Progress.BLL.VALORACIONESPROGRESS();
            List<IB.Progress.Models.VALORACIONESPROGRESS.ImprimirInsertados> impInsertados = valpro.Insert(listaProfesionales);
            valpro.Dispose();

           
            return impInsertados;
        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al insertar la evaluación", ex.Message);
            throw ex;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void EnvioCorreos(List<IB.Progress.Models.MIEQUIPO.profesional> listaProfesionalesCorreo)
    {
        List<IB.Progress.Models.MIEQUIPO.profesional> evaludosAperturaValoracion = (from items in listaProfesionalesCorreo
                                                                                    select items).ToList<IB.Progress.Models.MIEQUIPO.profesional>();

        List<IB.Progress.Models.MIEQUIPO.profesional> evaludosNotificados = (from items in listaProfesionalesCorreo
                                                                             where items.enviarcorreo == true
                                                                             select items).ToList<IB.Progress.Models.MIEQUIPO.profesional>();



        //Enviar correo. Un correo por cada evaluado. Un correo al evaluador con la lista de evaluados
        
        //Correo a enviar al evaluador
        StringBuilder plantillaParaEvaluador = new StringBuilder();
        plantillaParaEvaluador.Append("</br></br><span style='font-family:arial !important; font-size:10pt;'> " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).T001_nombre.ToString() + ", has abierto evaluaciones a los siguientes profesionales:</span></br></br>");
        plantillaParaEvaluador.Append("<table style='font-family:arial !important; font-size:10pt;' style='width:700px' id='tblfkCorreo' class='fkPijama'>");
        plantillaParaEvaluador.Append("<thead><tr><th style='font-family:arial !important; font-size:9pt; background-color:#50849F; color:#FFF;text-align:left;padding-left:5px'>Profesional</th></tr></thead>");
        try
        {

            plantillaParaEvaluador.Append("<tbody>");

            string clase = "";
            for (int i = 0; i < evaludosAperturaValoracion.Count; i++)
            {
                clase = i % 2 == 0 ? clase = "background-color:#F2F2F2" : "background-color:#fafafa";
                plantillaParaEvaluador.Append("<tr style='" + clase+ "'>");
                plantillaParaEvaluador.Append("<td style='font-size:10pt !important; font-family:arial !important; padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;'><span style='font-size:9pt;font-family:arial;'>" + evaludosAperturaValoracion[i].prof + "</span></td>");
                plantillaParaEvaluador.Append("</tr>");

                //Correo para cada evaluado con notificación activada.
                if (evaludosAperturaValoracion[i].enviarcorreo)
                    Correo.Enviar("PROGRESS: Apertura de valoración", evaludosAperturaValoracion[i].textocorreo, evaludosAperturaValoracion[i].Correo);                    
            }

            plantillaParaEvaluador.Append("</tbody></table>");

            plantillaParaEvaluador.Append("</br></br><span style='font-family:arial !important;font-size:10pt !important'>Las comunicaciones que han recibido automáticamente, son las siguientes:</span></br></br>");
            plantillaParaEvaluador.Append("<table id='tblfkCorreo' style='width:700px'>");
            plantillaParaEvaluador.Append("<thead><tr><th style='font-family:arial !important; font-size:9pt; background-color:#50849F; color:#FFF;text-align:left;padding-left:5px'>Profesional</th><th style='font-family:arial !important; font-size:9pt; background-color:#50849F; color:#FFF;text-align:left;padding-left:5px'>Comunicación</th></tr></thead>");

            plantillaParaEvaluador.Append("<tbody>");
            for (int i = 0; i < evaludosNotificados.Count; i++)
            {
                clase = i % 2 == 0 ? clase = "background-color:#F2F2F2" : "background-color:#fafafa";
                plantillaParaEvaluador.Append("<tr style='"+ clase +"'>");
                plantillaParaEvaluador.Append("<td style='font-size:10pt !important; font-family:arial !important; padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;vertical-align:top'><span style='font-size:9pt;font-family:arial'>" + evaludosNotificados[i].prof + "</span></td>");
                plantillaParaEvaluador.Append("<td style='font-size:10pt !important; font-family:arial !important; padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;vertical-align:top'><span style='font-size:9pt;font-family:arial'>" + evaludosNotificados[i].textocorreo + "</span></td>");
                plantillaParaEvaluador.Append("</tr>");

            }


            plantillaParaEvaluador.Append("</tbody></table>");

            //Correo.Enviar("Asunto", plantillaParaEvaluador.ToString(), ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Correo.ToString());
            //Correo.Enviar("Correo Progress", ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombre.ToString() + " ha actualizado los roles correctamente", ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Correo.ToString());
        }

        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al enviar el correo al evaluado", ex.Message);
            throw ex;
        }


        //Correo para el evaluador con una lista de los correos enviados a evaluados.                    
        try
        {
            Correo.Enviar("PROGRESS: Confirmación de apertura de valoración", plantillaParaEvaluador.ToString(), ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Correo.ToString());
        }
        catch (Exception ex)
        {

            IB.Progress.Shared.Smtp.SendSMTP("Error al enviar el correo al evaluador", ex.Message); ;
            throw ex;
        }
 
    }

   
}

