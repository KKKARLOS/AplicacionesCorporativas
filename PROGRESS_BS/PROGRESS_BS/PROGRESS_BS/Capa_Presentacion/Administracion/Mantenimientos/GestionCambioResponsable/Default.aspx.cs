using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;

public partial class Capa_Presentacion_Administracion_Mantenimientos_GestionarMediacion_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.GestionCambioResponsable> CatalogoCambioResponsable(Nullable<int> estado, string apellido1, string apellido2, string nombre )
    {
        IB.Progress.BLL.GestionCambioResponsable pro = null;
        try
        {
            List<IB.Progress.Models.GestionCambioResponsable> profesionales = null;
            pro = new IB.Progress.BLL.GestionCambioResponsable();

            profesionales = pro.CatalogoCambioResponsable(estado, apellido1, apellido2, nombre);
            pro.Dispose();

            return profesionales;
        }
        catch (Exception)
        {
            if (pro != null) pro.Dispose();
            throw;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool validaEvalProgress(int idficepi_interesado, int idrespdestino)
    {
        IB.Progress.BLL.Profesional pro = null;
        if (idrespdestino != 0)
        {
            try
            {
                IB.Progress.Models.Profesional profesionales = null;
                pro = new IB.Progress.BLL.Profesional();

                profesionales = pro.validaProgress(idficepi_interesado, idrespdestino);
                pro.Dispose();

                return profesionales.validoEvalProgress;
            }


            catch (Exception)
            {
                if (pro != null) pro.Dispose();
                throw;
            }
        }

        else {
            return false;
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GestionAnulacion(int idpeticion, string estado,  string nombre_resporigen, string nombreapellidos_interesado,  string correo_resporigen, string nombre_respdestino, string correo_respdestino)
    {
        IB.Progress.BLL.GestionCambioResponsable valpro = null;
        try
        {
            valpro = new IB.Progress.BLL.GestionCambioResponsable();
            valpro.GestionAnulacion(idpeticion,((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
            valpro.Dispose();

            //CORREO AL ANTIGUO EVALUADOR
            StringBuilder sbAntEvaluador = new StringBuilder();

            sbAntEvaluador.Append(nombre_resporigen + ", la tramitación de salida de tu equipo de " + nombreapellidos_interesado + ", ha sido anulada. </br></br></br>");
            sbAntEvaluador.Append("Motivo: </br></br> Anulación realizada por RRHH.");

            if (correo_resporigen != "") Correo.Enviar("PROGRESS: Cancelación de propuesta de salida", sbAntEvaluador.ToString(), correo_resporigen);

            //Sólo mandamos correo al evaluador destino si es una anulación de una petición tramitada
            if (estado == "1" || estado == "6")
            {
                //CORREO AL EVALUADOR DESTINO
                StringBuilder sbEvaluadorDestino = new StringBuilder();

                sbEvaluadorDestino.Append(nombre_respdestino + ", la propuesta de incorporación de " + nombreapellidos_interesado + " a tu equipo, ha sido anulada. </br></br></br>");
                sbEvaluadorDestino.Append("Motivo: </br></br> Anulación realizada por RRHH.");

                if (correo_respdestino != "") Correo.Enviar("PROGRESS: Cancelación de propuesta de incorporación", sbEvaluadorDestino.ToString(), correo_respdestino);
            }
           
        }
        catch (Exception)
        {
            if (valpro != null) valpro.Dispose();
            throw;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GestionAsignacion(int idpeticion, string estado, string nombreapellidos_interesado, string nombre_resporigen, string correo_resporigen, string nombre_respdestino, string correo_respdestino, string sexo_respdestino, string nombreprofesional, string nombreapellidos_respdestino, string correointeresado, int idrespdestino, int idinteresado)
    {
        
        IB.Progress.BLL.GestionCambioResponsable valpro = null;
        try
        {
            valpro = new IB.Progress.BLL.GestionCambioResponsable();
            valpro.GestionAsignacion(idpeticion, ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi, idrespdestino, idinteresado);
            valpro.Dispose();
            
            //PETICIÓN TRAMITADA
            if (estado == "1" || estado == "3" || estado == "6") {
                
                //CORREO AL ANTIGUO EVALUADOR                
                StringBuilder sbAntEvaluador = new StringBuilder();

                sbAntEvaluador.Append(nombre_resporigen + ", a partir de este momento, " + nombreapellidos_interesado + ", ha dejado de formar parte de tu equipo. </br></br></br>");
                sbAntEvaluador.Append("Motivo: </br></br> Cambio realizado por RRHH.");

                if (correo_resporigen != "") Correo.Enviar("PROGRESS: Salida de un profesional de tu equipo", sbAntEvaluador.ToString(), correo_resporigen);

                //CORREO AL NUEVO EVALUADOR                
                StringBuilder sbNuevoEvaluador = new StringBuilder();

                sbNuevoEvaluador.Append(nombre_respdestino + ", a partir de este momento, " + nombreapellidos_interesado + ", ha pasado a formar parte de tu equipo. </br></br></br>");
                sbNuevoEvaluador.Append("Motivo: </br></br> Cambio realizado por RRHH.");

                if (correo_respdestino != "") Correo.Enviar("PROGRESS: Incorporación de un profesional a tu equipo", sbNuevoEvaluador.ToString(), correo_respdestino);


                //CORREO AL INTERESADO                
                string genero = String .Empty;
                if (sexo_respdestino == "V") genero = "nuevo evaluador";
                else genero = "nueva evaluador";

                StringBuilder sbInteresado = new StringBuilder();

                sbInteresado.Append(nombreprofesional + ", a partir de este momento, tu " + genero + " es  " + nombreapellidos_respdestino + " </br></br></br>");

                if (correointeresado != "") Correo.Enviar("PROGRESS: Cambio de responsable", sbInteresado.ToString(), correointeresado);

            }
            
           
        }

        
        catch (Exception)
        {
            if (valpro != null) valpro.Dispose();
            throw;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string otroEvaluador(int idpeticion, string estado, int idficepi_interesado, int idficepi_destino, string nombreapellidosprofesional, string nombreinteresado, string sexo, string correointeresado, string nombre_resporigen, string correo_resporigen, string nombreapellidos_interesado, string nombreprofesional, string correo_profesional, string nombre_respdestino, string correo_respdestino)
    {
        string devolucion = "";
        IB.Progress.BLL.GestionCambioResponsable valpro = null;

        bool esvalido = validaEvalProgress(idficepi_interesado, idficepi_destino);

        if (esvalido == true)
        {
            try
            {
                string genero = String.Empty;
                valpro = new IB.Progress.BLL.GestionCambioResponsable();
                valpro.GestionCambioResponsableUPD(idpeticion, idficepi_interesado, idficepi_destino, ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
                valpro.Dispose();


                if (sexo == "V") genero = "evaluador"; else genero = "evaluadora";

                //SIN PETICIÓN DE TRAMITACIÓN
                if (estado == "null")
                {

                    //Enviar correo al interesado
                    StringBuilder sb = new StringBuilder();

                    sb.Append(nombreinteresado + ", a partir de este momento tu " + genero + "  es  " + nombreapellidosprofesional + ". </br></br>");

                    if (correointeresado != "") Correo.Enviar("PROGRESS: Cambio de responsable", sb.ToString(), correointeresado);

                    //Enviar correo al antiguo evaluador
                    StringBuilder sbAntEvaluador = new StringBuilder();

                    sbAntEvaluador.Append(nombre_resporigen + ", a partir de este momento, " + nombreapellidos_interesado + " ha dejado de formar parte de tu equipo. </br></br></br>");
                    sbAntEvaluador.Append("Motivo: </br></br> Cambio realizado por RRHH.");

                    if (correo_resporigen != "") Correo.Enviar("PROGRESS: Salida de un profesional de tu equipo", sbAntEvaluador.ToString(), correo_resporigen);


                    //Enviar correo al nuevo evaluador
                    StringBuilder sbNuevoEvaluador = new StringBuilder();

                    sbNuevoEvaluador.Append(nombreprofesional + ", a partir de este momento, " + nombreapellidos_interesado + " ha pasado a formar parte de tu equipo. </br></br></br>");
                    sbNuevoEvaluador.Append("Motivo: </br></br> Cambio realizado por RRHH.");

                    if (correo_profesional != "") Correo.Enviar("PROGRESS: Incorporación de un profesional a tu equipo", sbNuevoEvaluador.ToString(), correo_profesional);
                }

                //PETICIÓN TRAMITADA
                else if (estado == "1" || estado == "3" || estado == "6")
                {

                    //CORREO AL ANTIGUO EVALUADOR
                    StringBuilder sbAntEvaluadorTramitada = new StringBuilder();

                    sbAntEvaluadorTramitada.Append(nombre_resporigen + ", a partir de este momento, " + nombreapellidos_interesado + " ha dejado de formar parte de tu equipo. </br></br></br>");
                    sbAntEvaluadorTramitada.Append("Motivo: </br></br> Cambio realizado por RRHH.");

                    if (correo_resporigen != "") Correo.Enviar("PROGRESS: Salida de un profesional de tu equipo", sbAntEvaluadorTramitada.ToString(), correo_resporigen);

                    //CORREO AL EVALUADOR destino (CANCELACIÓN)
                    StringBuilder sbNuevoEvaluadorDestinoTramitada = new StringBuilder();

                    sbNuevoEvaluadorDestinoTramitada.Append(nombre_respdestino + ", la propuesta de incorporación de " + nombreapellidos_interesado + " a tu equipo, ha sido cancelada. </br></br></br>");
                    sbNuevoEvaluadorDestinoTramitada.Append("Motivo: </br></br> Cambio realizado por RRHH.");

                    if (correo_respdestino != "") Correo.Enviar("PROGRESS: Cancelación de propuesta de incorporación", sbNuevoEvaluadorDestinoTramitada.ToString(), correo_respdestino);


                    //CORREO AL NUEVO EVALUADOR ASIGNADO POR RRHH
                    //Enviar correo al nuevo evaluador
                    StringBuilder sbNuevoEvaluadorTramitada = new StringBuilder();

                    sbNuevoEvaluadorTramitada.Append(nombreprofesional + ", a partir de este momento, " + nombreapellidos_interesado + " ha pasado a formar parte de tu equipo. </br></br></br>");
                    sbNuevoEvaluadorTramitada.Append("Motivo: </br></br> Cambio realizado por RRHH.");

                    if (correo_profesional != "") Correo.Enviar("PROGRESS: Incorporación de un profesional a tu equipo", sbNuevoEvaluadorTramitada.ToString(), correo_profesional);


                    //CORREO AL INTERESADO
                    StringBuilder sbInteresadoTramitada = new StringBuilder();

                    sbInteresadoTramitada.Append(nombreinteresado + ", a partir de este momento, tu " + genero + "  es  " + nombreapellidosprofesional + ". </br></br>");

                    if (correointeresado != "") Correo.Enviar("PROGRESS: Cambio de responsable", sbInteresadoTramitada.ToString(), correointeresado);

                }

               
            }
            catch (Exception ex)
            {
                if (valpro != null) valpro.Dispose();
                //throw ex;
                IB.Progress.Shared.Smtp.SendSMTP("Error al cambiar de evaluador. (Gestión cambio de responsable)", ex.Message);
            }
        }

        else {
             devolucion = "no";
        }

        return devolucion;
    }


    /// <summary>
    /// Obtiene el catálogo de los evaluadores
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getSeleccionarEvaluador(int t001_evaluado_actual, int t001_evaluador_actual, string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        try
        {
            List<IB.Progress.Models.Profesional> evaluadores = null;
            IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
            evaluadores = pro.getSeleccionarEvaluador(t001_evaluado_actual, t001_evaluador_actual, t001_apellido1, t001_apellido2, t001_nombre);
            pro.Dispose();
            return evaluadores;
        }
        catch (Exception ex)
        {            
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener el catálogo de evaluadores. (Gestión cambio de responsable)", ex.Message);
            throw ex;
        }
    }

}