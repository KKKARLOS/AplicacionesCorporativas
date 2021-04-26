using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IB.Progress.Models;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using IB.Progress.Shared;
using System.Configuration;

public partial class Capa_Presentacion_Evaluacion_Formularios_Estandar_Default : System.Web.UI.Page
{
    public string estado;
    public string idvaloracion;
    public string puntuacion;
    public string origen;
    public string acceso;
    public string menu;
    public string IdEvaluacion;
  
    protected void Page_Load(object sender, EventArgs e)
    {

        int idficepiConectado = int.Parse(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi.ToString());

        if (Request.QueryString["pt"] == "consultas")
        {
            //btnRegresar.Style.Add("display", "block");
            origen = "var origen ='" + Request.QueryString["pt"].ToString() + "';";
            menu = "var menu ='" + Request.QueryString["menu"].ToString() + "';";            
        }
        else {
            origen = "var origen = 'noConsultas'";
            if (Request.QueryString["acceso"] != null) acceso = "var acceso ='" + Request.QueryString["acceso"].ToString() + "';";
            
        }

        VALORACIONESPROGRESS.formulario_id1 evaluacion = null;
        IB.Progress.BLL.VALORACIONESPROGRESS evaluacionBLL = null;
        bool bCont = true;
        try
        {
            IdEvaluacion = "var IdEvaluacion = '" + Utils.decodpar(Request.QueryString["idval"].ToString()) + "';";
            evaluacion = new VALORACIONESPROGRESS.formulario_id1();
            evaluacionBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            evaluacion = evaluacionBLL.Select(idficepiConectado, int.Parse(Utils.decodpar(Request.QueryString["idval"].ToString())));

            //Si no devuelve resultados la select, no autorizamos el acceso.
            if (evaluacion == null) {
                Response.Redirect("~/NoAutorizado.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            } 

            if (evaluacion.SexoEvaluador.ToString() == "V")
            {
                lgEvaluador.InnerText = "Evaluador";
                spanEvaluador.InnerText = "El evaluador";

            }
            else {
                spanEvaluador.InnerText = "La evaluadora";
                lgEvaluador.InnerText = "Evaluadora";
            } 

            lgEvaluador.Style.Add("margin-top", "3px");

            if (evaluacion.SexoEvaluado.ToString() == "V")
            {
                lgEvaluado.InnerText = "Evaluado";
                spanEvaluado.InnerText = "El evaluado";

            }
            else {
                lgEvaluado.InnerText = "Evaluada";
                spanEvaluado.InnerText = "La evaluada";                
            } 

            lgEvaluado.Style.Add("margin-top", "3px");

            evaluacionBLL.Dispose();
        }
        catch (IB.Progress.Shared.IBException ibex)
        {
            if (evaluacionBLL != null) evaluacionBLL.Dispose();
            bCont = false;

            string msgerr = "";
            switch (ibex.ErrorCode)
            {
                case 109:
                    msgerr = ibex.Message;
                    break;
            }
            PieMenu.sErrores = "msgerr = '" + msgerr + "';";
            //Avisar a EDA por smtp
            //SMTP.send(asunto, ibex, .......);
        }
        catch (Exception ex)
        {
            if (evaluacionBLL != null) evaluacionBLL.Dispose();
            bCont = false;
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            //SMTP.send(asunto, ex, .......);
        }
        if (bCont)
        {
            List<string> listaRoles = (List<string>)(Session["ROLES"]);
            //int idficepiConectado = int.Parse(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi.ToString());
            bool autorizado = false;
            try
            {


                //Si "ACCESSO" ES NULL, LA WEB ESTÁ EN MANTENIMIENTO.
                if (Session["ACCESO"] == null) return;

                //SECURIZAMOS LOS FORMULARIOS YA QUE ES UNA INFORMACIÓN CONFIDENCIAL.
                //Si viene por consultas ADM, y no tiene el Rol pertinente, no le damos acceso.
                if (Request.QueryString["pt"] == "consultas" && Request.QueryString["menu"] == "ADM" || Request.QueryString["acceso"] == "formacion")
                {
                    if (listaRoles.Contains("SADM") || listaRoles.Contains("AADM")) autorizado = true;
                    try
                    {
                        if (!autorizado)
                        {
                            Response.Redirect("~/NoAutorizado.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                
                //Si viene por consultas evaluaciones (EVA) , miramos si tiene el rol "PEVA"
                else if (Request.QueryString["pt"] == "consultas" && Request.QueryString["menu"] == "EVA")
                {                   
                    if (listaRoles.Contains("PEVA")) autorizado = true;
                    try
                    {
                        if (!autorizado)
                        {
                            Response.Redirect("~/NoAutorizado.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }

                //Si viene por mis evaluaciones y la persona conectada no es el idficepi_evaluado, no le autorizamos.
                else if (Request.QueryString["acceso"] == "misevaluaciones")
                {
                    if (evaluacion.t001_idficepi_evaluado != idficepiConectado)
                    {
                        Response.Redirect("~/NoAutorizado.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                }

                //Si viene por evaluaciones de mi equipo ó completar abiertas ó acceso es null (esto pasa si borramos los parámetros del querystring)
                //else if (Request.QueryString["acceso"] == "demiequipo" || Request.QueryString["acceso"] == "completarabiertas")
                else if (Request.QueryString["acceso"] == "demiequipo")
                {
                    //if (evaluacion.t001_idficepi_evaluador != idficepiConectado)
                    //{
                    //    Response.Redirect("~/NoAutorizado.aspx", false);
                    //    Context.ApplicationInstance.CompleteRequest();
                    //    return;
                    //}
                }


                else if (Request.QueryString["acceso"] == "completarabiertas")
                {
                    if (evaluacion.t001_idficepi_evaluador != idficepiConectado)
                    {
                        Response.Redirect("~/NoAutorizado.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                }
                
                //No autorizamos el acceso si no viene "consultas, de "formación demandada",  de "mis evaluaciones", "de mi equipo", "completar abiertas", "parámetro acceso nulo", "parámetro acceso '' " 
                else if (Request.QueryString["acceso"] != "misevaluaciones" && Request.QueryString["acceso"] != "demiequipo" && Request.QueryString["acceso"] != "completarabiertas" && Request.QueryString["acceso"] != "formacion")
                {
                    Response.Redirect("~/NoAutorizado.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                //else {
                //    Response.Redirect("~/NoAutorizado.aspx", false);
                //    Context.ApplicationInstance.CompleteRequest();
                //    return;
                //}
             
                //FIN SECURIZACIÓN DE FORMULARIOS
                
                 Cargar_Evaluacion(evaluacion);
                
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "msgerr = 'Ocurrió un error obteniendo los roles de base de datos';", true);
                PieMenu.sErrores = "msgerr = 'Ocurrió un error cargando los datos de la evaluaciones.';";
            }
        }
    }

    protected void Cargar_Evaluacion(VALORACIONESPROGRESS.formulario_id1 oVal) {
        sEvaluador.InnerText = oVal.evaluador;
        dApertura.Value = oVal.t930_fechaapertura.ToShortDateString();
        if (oVal.t930_fechacierre != null)
            dCierre.Value = ((DateTime)oVal.t930_fechacierre).ToShortDateString();
        sEvaluado.InnerText = oVal.evaluado;
        sRol.Value = oVal.t930_denominacionROL;
        sCR.Value = oVal.t930_denominacionCR;
        if(oVal.t930_actividad!=null){
            switch (oVal.t930_actividad) { 
                case 1: rdbTecnica.Checked = true; break;
                case 2: rdbGestion.Checked = true; break;
                case 3: rdbComercial.Checked = true; break;
                case 4: rdbDirectiva.Checked = true; break;
            }
        }
        if (oVal.t930_objetoevaluacion == 1){
            rdbEvalProyecto.Checked = true;
            sProyecto.Value = oVal.t930_denominacionPROYECTO;
        }
        else if (oVal.t930_objetoevaluacion == 2)
        {
            rdbEvalDesempeno.Checked = true;
            divProyecto.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        }
        else {
            divProyecto.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        }
        rellenarCombo(selAnoIni, false);
        rellenarCombo(selAnoFin, false);
        rellenarCombo(selMesIni, true);
        rellenarCombo(selMesFin, true);

        if (oVal.t930_anomes_ini != null) {
            if (selAnoIni.Items.FindByValue(oVal.t930_anomes_ini.ToString().Substring(0, 4)) != null)
                selAnoIni.Value = oVal.t930_anomes_ini.ToString().Substring(0, 4);
            else {
                selAnoIni.Items.Add(new ListItem(oVal.t930_anomes_ini.ToString().Substring(0, 4), oVal.t930_anomes_ini.ToString().Substring(0, 4)));
                selAnoIni.Value = oVal.t930_anomes_ini.ToString().Substring(0, 4);
            }
            selMesIni.Value = oVal.t930_anomes_ini.ToString().Substring(4);
        }
        if (oVal.t930_anomes_fin != null)
        {
            if (selAnoFin.Items.FindByValue(oVal.t930_anomes_fin.ToString().Substring(0, 4)) != null)
                selAnoFin.Value = oVal.t930_anomes_fin.ToString().Substring(0, 4);
            else
            {
                selAnoFin.Items.Add(new ListItem(oVal.t930_anomes_fin.ToString().Substring(0, 4), oVal.t930_anomes_fin.ToString().Substring(0, 4)));
                selAnoFin.Value = oVal.t930_anomes_fin.ToString().Substring(0, 4);
            }
            selMesFin.Value = oVal.t930_anomes_fin.ToString().Substring(4);
        }

        sAreconocer.InnerText = oVal.t930_areconocer;
        sAmejorar.InnerText = oVal.t930_amejorar;
        if (oVal.t930_gescli != null)
        {
            switch (oVal.t930_gescli)
            {
                case 1: rdbGestion1.Checked = true; break;
                case 2: rdbGestion2.Checked = true; break;
                case 3: rdbGestion3.Checked = true; break;
                case 4: rdbGestion4.Checked = true; break;
            }
        }
        if (oVal.t930_liderazgo != null)
        {
            switch (oVal.t930_liderazgo)
            {
                case 1: rdbLiderazgo1.Checked = true; break;
                case 2: rdbLiderazgo2.Checked = true; break;
                case 3: rdbLiderazgo3.Checked = true; break;
                case 4: rdbLiderazgo4.Checked = true; break;
            }
        }
        if (oVal.t930_planorga != null)
        {
            switch (oVal.t930_planorga)
            {
                case 1: rdbOrganizacion1.Checked = true; break;
                case 2: rdbOrganizacion2.Checked = true; break;
                case 3: rdbOrganizacion3.Checked = true; break;
                case 4: rdbOrganizacion4.Checked = true; break;
            }
        }
        if (oVal.t930_exptecnico != null)
        {
            switch (oVal.t930_exptecnico)
            {
                case 1: rdbTecnico1.Checked = true; break;
                case 2: rdbTecnico2.Checked = true; break;
                case 3: rdbTecnico3.Checked = true; break;
                case 4: rdbTecnico4.Checked = true; break;
            }
        }
        if (oVal.t930_cooperacion != null)
        {
            switch (oVal.t930_cooperacion)
            {
                case 1: rdbCooperacion1.Checked = true; break;
                case 2: rdbCooperacion2.Checked = true; break;
                case 3: rdbCooperacion3.Checked = true; break;
                case 4: rdbCooperacion4.Checked = true; break;
            }
        }
        if (oVal.t930_iniciativa != null)
        {
            switch (oVal.t930_iniciativa)
            {
                case 1: rdbIniciativa1.Checked = true; break;
                case 2: rdbIniciativa2.Checked = true; break;
                case 3: rdbIniciativa3.Checked = true; break;
                case 4: rdbIniciativa4.Checked = true; break;
            }
        }
        if (oVal.t930_perseverancia != null)
        {
            switch (oVal.t930_perseverancia)
            {
                case 1: rdbPerseverancia1.Checked = true; break;
                case 2: rdbPerseverancia2.Checked = true; break;
                case 3: rdbPerseverancia3.Checked = true; break;
                case 4: rdbPerseverancia4.Checked = true; break;
            }
        }
        if (oVal.t930_interesescar != null)
        {
            switch (oVal.t930_interesescar)
            {
                case 1: rdbEvolucion1.Checked = true; break;
                case 2: rdbEvolucion2.Checked = true; break;
                case 3: rdbEvolucion3.Checked = true; break;
                case 4: rdbEvolucion4.Checked = true; break;
                case 5: rdbEvolucion5.Checked = true; break;
                case 6: rdbEvolucion6.Checked = true; break;
                case 7: rdbEvolucion7.Checked = true; break;
            }
        }
        sFormacion.InnerText = oVal.t930_formacion;
        sAutoevaluacion.InnerText = oVal.t930_autoevaluacion;


        if (Request.QueryString["menu"] != null)
        {
            if (Request.QueryString["menu"] == "ADM")
            {
                switch (oVal.t930_puntuacion)
                {
                    case true:
                        imgCualificacion.Src = "../../../../imagenes/imgValida.png";
                        break;
                    case false:
                        imgCualificacion.Src = "../../../../imagenes/imgNOvalida.png";
                        break;
                    default:
                        imgCualificacion.Src = "";
                        break;
                }
            }
        }
        

        switch (oVal.estado)
        {
            case "ABI":
                imgEstados.Src = "../../../../imagenes/estadoAbierta.png";
                break;
            case "CUR":
                imgEstados.Src = "../../../../imagenes/estadoEnCurso.png";
                break;
            case "CCF":
                imgEstados.Src = "../../../../imagenes/estadoCerradaFirmada.png";
                break;
            case "CSF":
                imgEstados.Src = "../../../../imagenes/estadoCerradaSinFirma.png";
                break;
        }

        if (oVal.t930_fecfirmaevaluador != null){
            selloEvaluador.Attributes.Add("data-after", ((DateTime)oVal.t930_fecfirmaevaluador).ToShortDateString());
            //txtEvaluador.InnerText = oVal.firmaevaluador;
        }
        else {
            //txtEvaluador.Attributes.Add("class", "hide");
            selloEvaluador.Attributes.Add("class", "hide");
        }
        if (oVal.t930_fecfirmaevaluado != null)
        {
            selloEvaluado.Attributes.Add("data-after", ((DateTime)oVal.t930_fecfirmaevaluado).ToShortDateString());
            //txtEvaluado.InnerText = oVal.firmaevaluado;
        }
        else {
            //txtEvaluado.Attributes.Add("class", "hide");
            selloEvaluado.Attributes.Add("class", "hide");
        }
        
        //Para habilitar o no campos
        habilitar_deshabilitar(oVal, (Profesional)Session["PROFESIONAL"]);
        idvaloracion = "var idValoracion=" + oVal.t930_idvaloracion.ToString() + ";";

        string jsval = "";

        switch (oVal.t930_puntuacion)
        {
            case null: jsval = "null"; break;
            case true: jsval = "1"; break;
            case false: jsval = "0"; break;            
        }
        puntuacion = "var puntuacion=" + jsval + ";";

    }

    List<string> meses = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    protected void rellenarCombo(HtmlSelect combo, bool mes) {
        if (mes)
        {
            foreach (string ms in meses)
            {
                ListItem opt=new ListItem();
                opt.Value = (meses.IndexOf(ms) + 1).ToString("00");
                opt.Text = ms;
                combo.Items.Add(opt);
            }
        }
        else {
            for (int k = DateTime.Now.Year - 2; k <= DateTime.Now.Year; k++) {
                ListItem opt = new ListItem();
                opt.Value = k.ToString();
                opt.Text = k.ToString();
                combo.Items.Add(opt);
            }
        }
    }

    protected void habilitar_deshabilitar(VALORACIONESPROGRESS.formulario_id1 oVal, Profesional prof) {

        //Cuando no viene por Consultas
        if (Request.QueryString["pt"] == null) {
            if (oVal.estado == "ABI" && oVal.t001_idficepi_evaluador == prof.t001_idficepi)
            {
                estado = "var estadoEvaluacion=1;";//Modificable por el evaluador
                sinfirma.Attributes["style"] = "display:inline-block";
                firmado.Attributes["style"] = "display:inline-block";
                cancelar.Attributes["style"] = "display:inline-block";
                
            }
            else if (oVal.estado == "CUR" && oVal.t001_idficepi_evaluado == prof.t001_idficepi)
            {
                estado = "var estadoEvaluacion=2;";//Modificable por el evaluado                                                
                sinfirma.Attributes["style"] = "display:inline-block";
                firmado.Attributes["style"] = "display:inline-block";
                cancelar.Attributes["style"] = "display:inline-block";            
            }
            else
            {
                estado = "var estadoEvaluacion=0;";//Sólo lectura                
                regresar.Attributes["style"] = "display:inline-block";            
            }
            
        }

        //Consultas
        else if (Request.QueryString["pt"] == "consultas") {
            if (oVal.t001_idficepi_evaluador == prof.t001_idficepi)
            {
                if (oVal.estado == "ABI")
                {
                    estado = "var estadoEvaluacion=1;";//Modificable por el evaluador
                    sinfirma.Attributes["style"] = "display:inline-block";
                    firmado.Attributes["style"] = "display:inline-block";
                    cancelar.Attributes["style"] = "display:inline-block";             
                }
                else
                {
                    estado = "var estadoEvaluacion=0;";//Sólo lectura
                    regresar.Attributes["style"] = "display:inline-block";
                }
            }

            else
            {
                estado = "var estadoEvaluacion=0;";//Sólo lectura
                regresar.Attributes["style"] = "display:inline-block";
            }


            if (Request.QueryString["menu"].ToString().ToUpper() == "ADM")
            {
                if (oVal.estado == "ABI")
                {
                    estado = "var estadoEvaluacion=1;";//Modificable por el evaluador
                    sinfirma.Attributes["style"] = "display:inline-block";
                    firmado.Attributes["style"] = "display:inline-block";
                    cancelar.Attributes["style"] = "display:inline-block";
                }

                else if (oVal.estado == "CUR")
                {
                    estado = "var estadoEvaluacion=0;";//Sólo lectura
                    regresar.Attributes["style"] = "display:inline-block";
                }

                else
                {
                    //CUALIFICANDO
                    estado = "var estadoEvaluacion=0;";//Sólo lectura
                    switch (oVal.t930_puntuacion)
                    {
                        case true:
                            CnoValida.Attributes["style"] = "display:inline-block";
                            Descualificar.Attributes["style"] = "display:inline-block";
                            regresar.Attributes["style"] = "display:inline-block";
                            break;

                        case false:
                            Cvalida.Attributes["style"] = "display:inline-block";
                            Descualificar.Attributes["style"] = "display:inline-block";
                            regresar.Attributes["style"] = "display:inline-block";
                            break;

                        default:
                            Cvalida.Attributes["style"] = "display:inline-block";
                            CnoValida.Attributes["style"] = "display:inline-block";
                            regresar.Attributes["style"] = "display:inline-block";
                            break;

                    }

                    regresar.Attributes.Add("class", "btn btn-default");
                }
            }

            
        }

        //También le paso a javascript el idficepi del evaluado
        estado += "var idficepiEvaluado=" + oVal.t001_idficepi_evaluado.ToString() + ";";
       
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void firmarFinalizarEvaluador(VALORACIONESPROGRESS.formulario_id1 form1)
    {
        VALORACIONESPROGRESS.formulario_id1 evaluacion = null;
        IB.Progress.BLL.VALORACIONESPROGRESS evaluacionBLL = null;
        try
        {
            string responsable = String.Empty;
            StringBuilder sb = new StringBuilder();
            IB.Progress.BLL.VALORACIONESPROGRESS valpro = new IB.Progress.BLL.VALORACIONESPROGRESS();
            valpro.UpdateEvaluador(form1);
            valpro.Dispose();

            int idficepiConectado = int.Parse(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi.ToString());
            
            evaluacionBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            evaluacion = evaluacionBLL.Select(idficepiConectado, form1.t930_idvaloracion);
            evaluacionBLL.Dispose();

            //Envío de correos
            if (form1.t930_fecfirmaevaluador != null)
            {                
                if (((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Sexo.ToString() == "V") responsable = "evaluador";
                else responsable = "evaluadora";

                sb.Append(evaluacion.Nombreevaluado + ", tu " + responsable + " " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrecorto.ToString() + ", ha cumplimentado y firmado tu evaluación.");
                sb.Append(" A partir de este momento, dispones de 15 días para hacer las anotaciones oportunas y firmarla. Transcurrido este plazo, si no la has firmado, la evaluación se cerrará automáticamente.</br></br>");
                sb.Append("</br></br>Si te encuentras en las oficinas de IBERMÁTICA, puedes acceder directamente pulsando <a href=" + ConfigurationManager.AppSettings["urlInterna"] + "/Default.aspx?FEVADO=true>aquí</a>");
                sb.Append("</br>Si estás fuera, puedes acceder pulsando <a href=" + ConfigurationManager.AppSettings["UrlExterna"] + "/Default.aspx?FEVADO=true>aquí</a>");

                Correo.Enviar("PROGRESS: Firma de tu " + responsable + "", sb.ToString(), evaluacion.Correoevaluado);
            }
            
    }
        catch (Exception ex)
        {
            if (evaluacionBLL != null) evaluacionBLL.Dispose();
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
            
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void firmarFinalizarEvaluado(VALORACIONESPROGRESS.formulario_id1 form1)
    {
        VALORACIONESPROGRESS.formulario_id1 evaluacion = null;
        IB.Progress.BLL.VALORACIONESPROGRESS evaluacionBLL = null;
        try
        {
            string supervisado = String.Empty;
            StringBuilder sb = new StringBuilder();
            IB.Progress.BLL.VALORACIONESPROGRESS valpro = new IB.Progress.BLL.VALORACIONESPROGRESS();
            valpro.UpdateEvaluado(form1);
            valpro.Dispose();

            int idficepiConectado = int.Parse(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi.ToString());
            
            evaluacionBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            evaluacion = evaluacionBLL.Select(idficepiConectado, form1.t930_idvaloracion);
            evaluacionBLL.Dispose();

            //Envío de correos
            if (form1.t930_fecfirmaevaluado != null)
            {
                if (((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Sexo.ToString() == "V") supervisado = "evaluado";
                else supervisado = "evaluada";

                sb.Append(evaluacion.Nombreevaluador+ ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha realizado el cierre de la valoración.");
                
                Correo.Enviar("PROGRESS: Firma de tu " + supervisado + "", sb.ToString(), evaluacion.Correoevaluador);
            }


        }
        catch (Exception ex)
        {

            if (evaluacionBLL != null) evaluacionBLL.Dispose();
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());            
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> getProyectos(int idficepi,int anomesdesde, int anomeshasta)
    {
        try
        {
            List<string> proyectos = null;
            IB.Progress.BLL.VALORACIONESPROGRESS valpro = new IB.Progress.BLL.VALORACIONESPROGRESS();
            proyectos = valpro.getProyectos(idficepi, anomesdesde, anomeshasta);
            valpro.Dispose();
            return proyectos;
        }
        catch (Exception)
        {
            
            throw;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void Cualificar(int t930_idvaloracion, Nullable<bool> t930_puntuacion)
    {
        IB.Progress.BLL.VALORACIONESPROGRESS valpro = null;
        try
        {
            valpro = new IB.Progress.BLL.VALORACIONESPROGRESS();
            valpro.CualificarEvaluacion(t930_idvaloracion, t930_puntuacion);
            valpro.Dispose();           
        }
        catch (Exception)
        {
            if (valpro != null) valpro.Dispose();
            throw;
        }
    }



    

}