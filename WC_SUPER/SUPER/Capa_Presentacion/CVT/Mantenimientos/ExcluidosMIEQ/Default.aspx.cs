using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;

public partial class Default : System.Web.UI.Page
{
    public string strTablaExcluidos;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de profesionales excluidos del correo de tareas vencidas de mi equipo en relación con CVT";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.Modulo = "CVT";
            try
            {
                string strTabla0 = SUPER.BLL.EXCLUIDOS_MIEQ.Obtener();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                strTablaExcluidos = aTabla0[0];
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los profesionalesexcluidos del correo de tareas vencidas de mi equipo en relación con CVT", ex);
            }
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string profesionales(string sAp1, string sAp2, string sNombre)
    {
        try
        {
            return "OK@#@" + SUPER.BLL.EXCLUIDOS_MIEQ.obtenerProfesionales(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre));
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al cargar los profesionales de mi equipo excluidos de notificaciones de correo de tareas vencidas.", ex);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Grabar(string sIdsProfesionales)
    {
        try
        {
            return "OK@#@" + SUPER.BLL.EXCLUIDOS_MIEQ.Grabar(sIdsProfesionales);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al grabar los profesionales de mi equipo excluidos de notificaciones de correo de tareas vencidas.", ex);
        }
    }
}
