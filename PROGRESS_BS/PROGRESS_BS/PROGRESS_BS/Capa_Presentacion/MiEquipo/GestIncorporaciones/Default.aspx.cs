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

public partial class Capa_Presentacion_MiEquipo_GestIncorporaciones_Default : System.Web.UI.Page
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
            miequipo = miequipoBLL.IncorporacionesCAT(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
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
    public static string RechazarIncorporacion(List<string> listapeticiones, string motivoRechazo, List<IB.Progress.Models.MIEQUIPO.profEntradasTramite> oProfesional)
    {
        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
        IB.Progress.Models.MIEQUIPO miequipo = null;
        try
        {            
            IB.Progress.BLL.GestionarIncorporaciones oGestionarIncorporaciones = new IB.Progress.BLL.GestionarIncorporaciones();
            oGestionarIncorporaciones.RechazarIncorporacion(listapeticiones, motivoRechazo);
            oGestionarIncorporaciones.Dispose();



            List<miclase> lst = (from o in oProfesional
                                 select new miclase
                                 {
                                     idficepievaluadordestino = o.Idficepievaluadordestino,
                                     correoevaluadordestino = o.correoresporigen,
                                     nombreevaluadordestino = o.nombreresporigen,
                                     motivo = motivoRechazo,
                                 }).Distinct(new ProfComparer()).ToList<miclase>();


            foreach (miclase mc in lst)
            {

                mc.evaluados = (from o in oProfesional
                                where o.Idficepievaluadordestino == mc.idficepievaluadordestino
                                select o).ToList<IB.Progress.Models.MIEQUIPO.profEntradasTramite>();

            }




            foreach (miclase item in lst)
            {
                StringBuilder sb = new StringBuilder();                
                sb.Append(item.nombreevaluadordestino + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " no ha aceptado en su equipo a:");
                sb.Append("<br /><ul>");

                foreach (IB.Progress.Models.MIEQUIPO.profEntradasTramite oEvaluado in item.evaluados)
                {
                    sb.Append("<li>" + oEvaluado.nombreapellidosinteresado + "</li>");

                }
                sb.Append("</ul>");
                sb.Append("</br></br>Motivo:</br>" + motivoRechazo);

                if (item.correoevaluadordestino != "") Correo.Enviar("PROGRESS: Cambio de evaluador/a - no aceptación", sb.ToString(), item.correoevaluadordestino);
            }

            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            miequipo = miequipoBLL.IncorporacionesCAT(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
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
    public static string AceptarIncorporacion(List<string> listapeticiones, List<IB.Progress.Models.MIEQUIPO.profEntradasTramite> oProfesional)
    {

        //Validamos si el evalprogress es válido para el profesional
        IB.Progress.Models.Profesional profesionales = null;
        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
        IB.Progress.Models.MIEQUIPO miequipo = null;
        string retval = "";
        string genero = String.Empty;


        IB.Progress.Models.Profesional oProf = (IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL_ENTRADA"];

        for (int i = 0; i < oProfesional.Count; i++)
        {

            profesionales = validaEvalProgress(oProfesional[i].idficepi, oProf.t001_idficepi);

            if (profesionales.validoEvalProgress == true)
            {
                try
                {

                    if (((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Sexo.ToString() == "V") genero = "nuevo evaluador";
                    else genero = "nueva evaluadora";


                    IB.Progress.BLL.GestionarIncorporaciones oGestionarIncorporaciones = new IB.Progress.BLL.GestionarIncorporaciones();
                    oGestionarIncorporaciones.AceptarIncorporacion(oProf.t001_idficepi, listapeticiones);
                    oGestionarIncorporaciones.Dispose();

                    miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
                    miequipo = miequipoBLL.IncorporacionesCAT(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
                    miequipoBLL.Dispose();

                    retval = JsonConvert.SerializeObject(miequipo);

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

            else
            {
                return "KO";
            }

        }


        List<miclase> lst = (from o in oProfesional
                             select new miclase
                             {
                                 idficepievaluadordestino = o.Idficepievaluadordestino,
                                 correoevaluadordestino = o.correoresporigen,
                                 nombreevaluadordestino = o.nombreresporigen
                             }).Distinct(new ProfComparer()).ToList<miclase>();


        foreach (miclase mc in lst)
        {

            mc.evaluados = (from o in oProfesional
                            where o.Idficepievaluadordestino == mc.idficepievaluadordestino
                            select o).ToList<IB.Progress.Models.MIEQUIPO.profEntradasTramite>();

        }

        foreach (miclase item in lst)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.nombreevaluadordestino + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha aceptado en su equipo a:");
            sb.Append("<br /><ul>");

            foreach (IB.Progress.Models.MIEQUIPO.profEntradasTramite oEvaluado in item.evaluados)
            {
                sb.Append("<li>" + oEvaluado.nombreapellidosinteresado + "</li>");

            }
            sb.Append("</ul>");

            if (item.correoevaluadordestino != "") Correo.Enviar("PROGRESS: Cambio de evaluador/a - aceptación", sb.ToString(), item.correoevaluadordestino);
        }

       
        return retval;

    }

    
    
    public static IB.Progress.Models.Profesional validaEvalProgress(int idficepi, int t001_idevalprogress)
    {
        try
        {
            IB.Progress.Models.Profesional profesionales = null;
            IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();

            profesionales = pro.validaProgress(idficepi, t001_idevalprogress);
            
            pro.Dispose();
            return profesionales;
        }
        catch (Exception)
        {

            throw;
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
        public List<IB.Progress.Models.MIEQUIPO.profEntradasTramite> evaluados { get; set; }
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

}