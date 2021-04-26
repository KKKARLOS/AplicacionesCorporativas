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
using System.Collections;
using System.Configuration;

public partial class Capa_Presentacion_MiEquipo_TramitarSalidas_Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]    
    public static string MiEquipo()
    {
        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
        IB.Progress.Models.MIEQUIPO miequipo = null;
        try
        {
            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            miequipo = miequipoBLL.Catalogo(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
            miequipoBLL.Dispose();

            string retval = JsonConvert.SerializeObject(miequipo);
            return retval;
        }
        catch (Exception ex)
        {
            if (miequipoBLL != null) miequipoBLL.Dispose();
            
            //PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
            return "";
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getFicepi(int idficepiinteresado, string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            
            List<IB.Progress.Models.Profesional> profesionales = null;
            pro = new IB.Progress.BLL.Profesional();
            profesionales = pro.getFicepi_Evaluadordestino(idficepiinteresado, ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi, t001_apellido1, t001_apellido2, t001_nombre);

            pro.Dispose();
            return profesionales;
        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            Smtp.SendSMTP("Error al cargar los profesionales. (Tramitar salidas)", ex.Message);
            throw ex;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<int> validaEvalProgress(List<int> listaid, int t001_idevalprogress)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            IB.Progress.Models.Profesional profesionales = null;
            pro = new IB.Progress.BLL.Profesional();

            List<int> valido = new List<int>();

            foreach (int idficepi in listaid)
            {
                profesionales = pro.validaProgress(idficepi, t001_idevalprogress);

                if (profesionales.validoEvalProgress == false)
                {
                    valido.Add(idficepi);
                }
            }

            pro.Dispose();
            return valido;
        }
        
        catch (Exception)
        {
            if (pro != null) pro.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Inserta uno o varios registros en la tabla t937_tramitacioncambioresponsable
    /// </summary>
    /// <param name="listaProfesionales"></param>
    /// <param name="t001_idficepi_respdestino"></param>
    /// <param name="t937_comentario_resporigen"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string insert(List<int> listaProfesionales, int t001_idficepi_respdestino, string t937_comentario_resporigen, List<string> listadocorreo)
    {
        IB.Progress.BLL.TramitarSalidas valpro = null;
        try
        {
            string supervisado = String.Empty;
            valpro = new IB.Progress.BLL.TramitarSalidas();
            List<string> datosEvaluador = valpro.Insert(listaProfesionales, t001_idficepi_respdestino, t937_comentario_resporigen);
            valpro.Dispose();

            //ENVIAR CORREO AL EVALUADOR DESTINO
            StringBuilder sb = new StringBuilder();

            sb.Append(datosEvaluador[1] + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " propone transferir a estos profesionales a tu equipo. Puedes aceptar o rechazar su propuesta en la aplicación Progress, accediendo a 'Equipo/Gestionar entradas a mi equipo'.</br></br>");
            sb.Append("<ul>");

            for (int i = 0; i < listadocorreo.Count; i++)
            {
                sb.Append("<li>" + listadocorreo[i] + "</li>");
            }

            sb.Append("</ul></br></br>");


            sb.Append("Motivo: </br></br>" + t937_comentario_resporigen);

            sb.Append("</br></br></br></br>Si te encuentras en las oficinas de IBERMÁTICA, puedes acceder directamente pulsando <a href=" + ConfigurationManager.AppSettings["UrlInterna"] + "/Default.aspx?GESENT=true>aquí</a>");
            sb.Append("</br>Si estás fuera, puedes acceder pulsando <a href=" + ConfigurationManager.AppSettings["UrlExterna"] + "/Default.aspx?GESENT=true>aquí</a>");

            if (datosEvaluador[0] != "") Correo.Enviar("PROGRESS: Petición de admisión de profesionales en tu equipo", sb.ToString(), datosEvaluador[0]);

            IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
            IB.Progress.Models.MIEQUIPO miequipo = null;

            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            miequipo = miequipoBLL.Catalogo(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
            miequipoBLL.Dispose();

            string retval = JsonConvert.SerializeObject(miequipo);
            return retval;

        }

        catch (Exception)
        {
            if (valpro != null) valpro.Dispose();
            throw;
        }

    }

    /// <summary>
    /// Anula la salida en trámite en estado 1(tramitada) o 3 (rechazada)
    /// </summary>
    /// <param name="idpeticion"></param>
    /// <returns></returns>
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static string anularSalida(List<string> idpeticiones, List<string> nombreevaluadordestino, List<string> nombreapellidosprofesional, string motivo, List<string> correovaluadordestino, List<string> estados)
    //{
    //    IB.Progress.BLL.TramitarSalidas valpro = null;
    //    try
    //    {
    //        valpro = new IB.Progress.BLL.TramitarSalidas();
    //        IB.Progress.Models.Profesional oProf = (IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL_ENTRADA"];

    //        valpro.Update(idpeticiones, oProf.t001_idficepi);                                   
    //        valpro.Dispose();
            
    //        //ENVIAR CORREO A RESPONSABLE DESTINO           
    //        for (int i = 0; i < idpeticiones.Count; i++)
    //        {                
    //            StringBuilder sb = new StringBuilder();
    //            sb.Append(nombreevaluadordestino[i] + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha anulado la petición de admisión de " + nombreapellidosprofesional[i] + " en tu equipo.</br></br>");

    //            sb.Append("Motivo: </br></br>" + motivo);
                
    //            if (correovaluadordestino[i] != "" && estados[i] == "1") Correo.Enviar("PROGRESS: Anulación de petición de admisión de profesionales en tu equipo", sb.ToString(), correovaluadordestino[i]);    
    //        }

    //        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
    //        IB.Progress.Models.MIEQUIPO miequipo = null;
    //        miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
    //        miequipo = miequipoBLL.Catalogo(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
    //        miequipoBLL.Dispose();

    //        string retval = JsonConvert.SerializeObject(miequipo);
    //        return retval;
    //    }
    //    catch (Exception)
    //    {
    //        if (valpro != null) valpro.Dispose();
    //        throw;
    //    }
    //}



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string anularSalidaMasiva(List<string> idpeticiones, List<IB.Progress.Models.MIEQUIPO.profesional> oProfesional)
    {
        IB.Progress.BLL.TramitarSalidas valpro = null;

        try
        {

            valpro = new IB.Progress.BLL.TramitarSalidas();
            IB.Progress.Models.Profesional oProf = (IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL_ENTRADA"];

            valpro.Update(idpeticiones, oProf.t001_idficepi);
            valpro.Dispose();

            List<miclase> lst = (from o in oProfesional
                                select new miclase{idficepievaluadordestino = o.idficepievaluadordestino,
                                                   correoevaluadordestino = o.correoevaluadordestino,
                                                   nombreevaluadordestino = o.nombreevaluadordestino,                                                   
                                                   motivo = o.Motivo,
                                                   estado = o.estado}).Distinct(new ProfComparer()).ToList<miclase>();

            
            foreach (miclase mc in lst)
            {

                mc.evaluados = (from o in oProfesional
                                where o.idficepievaluadordestino == mc.idficepievaluadordestino
                                select o).ToList<IB.Progress.Models.MIEQUIPO.profesional>();

            }


            foreach (miclase item in lst)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(item.nombreevaluadordestino + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha anulado la petición de admisión de los siguientes profesionales en tu equipo:");
                sb.Append("<br /><ul>");

                foreach (IB.Progress.Models.MIEQUIPO.profesional oEvaluado in item.evaluados)
                {
                    if(oEvaluado.estado == 1)
                        sb.Append("<li>" + oEvaluado.nombreapellidosprofesional + "</li>");                    
                }
                sb.Append("</ul>");
                sb.Append("</br></br>Motivo:</br>"+ item.motivo);

                if (item.correoevaluadordestino != "" && item.estado == 1) Correo.Enviar("PROGRESS: Anulación de petición de admisión de profesionales en tu equipo", sb.ToString(), item.correoevaluadordestino);
            }

            IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
            IB.Progress.Models.MIEQUIPO miequipo = null;
            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            miequipo = miequipoBLL.Catalogo(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
            miequipoBLL.Dispose();

            string retval = JsonConvert.SerializeObject(miequipo);
            return retval;
            
        }
        catch (Exception ex)
        {
            if (valpro != null) valpro.Dispose();
            throw ex;
        }
    }

    public class miclase
    {
        public miclase()
        {
        }

        public int idficepievaluadordestino { get; set; }
        public string correoevaluadordestino { get; set; }
        public string nombreevaluadordestino { get; set; }
        public string motivo { get; set; }
        public byte? estado { get; set; }
        public List<IB.Progress.Models.MIEQUIPO.profesional> evaluados { get; set; }
    }

    // this class is used to compare two objects of type Usuers to remove 
    // all objects that are duplicates only by field IdUser
    public class ProfComparer : IEqualityComparer<miclase>
    {
        public bool Equals(miclase x, miclase y)
        {
            if (x.idficepievaluadordestino == y.idficepievaluadordestino)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(miclase obj)
        {
            return obj.idficepievaluadordestino.GetHashCode();
        }
    }


    /// <summary>
    /// Solicita mediación a RRHH
    /// </summary>
    /// <param name="idpeticion"></param>
    /// <returns></returns>
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static string solicitarMediacion(int idpeticion, string nombreapellidosprofesional, string motivo , string estado, string correovaluadordestino)
    //{
    //    IB.Progress.BLL.TramitarSalidas valpro = null;
    //    try
    //    {
    //        valpro = new IB.Progress.BLL.TramitarSalidas();
    //        valpro.SolicitarMediacion(idpeticion);
    //        valpro.Dispose();

    //        //ENVIAR CORREO A RRHH
    //        StringBuilder sb = new StringBuilder();

    //        sb.Append(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + ", ha solicitado mediación para la tramitación de salida de su equipo de " + nombreapellidosprofesional + ".  </br></br>");

    //        sb.Append("Motivo: </br></br>" + motivo);
    //        sb.Append("</br></br></br></br>Si te encuentras en las oficinas de IBERMÁTICA, puedes acceder directamente pulsando <a href='http://" + HttpContext.Current.Session["strHost"] + "/Default.aspx?GESMED=true'>aquí</a>");

    //        if (System.Configuration.ConfigurationManager.AppSettings["SMTP_toRRHH"].ToString() != "") Correo.Enviar("PROGRESS: Solicitud de mediación", sb.ToString(), System.Configuration.ConfigurationManager.AppSettings["SMTP_toRRHH"].ToString());
            
    //        //Si está tramitada enviamos correo al evaluador destino
    //        StringBuilder sbEvaluador = new StringBuilder();
    //        sbEvaluador.Append(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + ", ha solicitado mediación a RRHH para la solicitud de incorporación en tu equipo de " + nombreapellidosprofesional + ". </br></br>");
    //        sbEvaluador.Append("Motivo: </br></br>" + motivo);

    //        if (estado == "1" && correovaluadordestino !=  "") Correo.Enviar("PROGRESS: Solicitud de mediación", sbEvaluador.ToString(), correovaluadordestino);

    //        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
    //        IB.Progress.Models.MIEQUIPO miequipo = null;

    //        miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
    //        miequipo = miequipoBLL.Catalogo(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
    //        miequipoBLL.Dispose();

    //        string retval = JsonConvert.SerializeObject(miequipo);
    //        return retval;
    //    }
    //    catch (Exception)
    //    {
    //        if (valpro != null) valpro.Dispose();
    //        throw;
    //    }
    //}

}

//public class miclase
//{
//    public miclase() { }

//    public string correoevaluadordestino { get; set; }
//    public string nombreevaluadordestino { get; set; }
//    public byte? estado { get; set; }
//    public List<IB.Progress.Models.MIEQUIPO.profesional> evaluados { get; set; }
//}