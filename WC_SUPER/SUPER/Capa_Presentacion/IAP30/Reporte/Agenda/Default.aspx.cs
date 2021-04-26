using IB.SUPER.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;


public partial class Capa_Presentacion_Reporte_Agenda_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //Carga de guia de ayuda
            aspnetUtils.visualizarGuia(Menu);

            //Capturar fecha del calendario enviado por parámetro
            /*Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
            string fechaCalendario = ht["date"].ToString();*/

            this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
            string script1 = "IB.vars.desProfesional = '" + Session["DES_EMPLEADO_IAP"].ToString() + "';";
            script1 += "IB.vars.desPromotor = '" + Session["DES_EMPLEADO_ENTRADA"].ToString() + "';";
            script1 += "IB.vars.aSemLab = '" + Session["aSemLab"] + "';";

            //script1 += "IB.vars.fechaCal = '" + fechaCalendario + "';";

            //registramos en un form runat='server'
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }

        
    }

    /// <summary>
    /// Obtiene los eventos programados de un usuario en un periodo de tiempo
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.PlanifAgendaCat> getCatalogoEventos(DateTime fechaIni, DateTime fechaFin)
    {
        BLL.PlanifAgendaCat planifAgendaCat = new BLL.PlanifAgendaCat();

        try
        {
            List<Models.PlanifAgendaCat> lCatalogoEventos = null;

            lCatalogoEventos = planifAgendaCat.CatalogoEventos(int.Parse(HttpContext.Current.Session["IDFICEPI_IAP"].ToString()), fechaIni, fechaFin);
            return lCatalogoEventos;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar los eventos programados", ex);
            throw new Exception("Error al cargar los eventos programados");
        }
        finally
        {
            planifAgendaCat.Dispose();
        }
    }

    /// <summary>
    /// Grabar un evento
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static string grabarEvento(Models.PlanifAgendaCat evento, string[] otrosProfesionales, bool confirmarBorrado)
    {
        BLL.PlanifAgendaCat planifAgendaCat = new BLL.PlanifAgendaCat();
        string result = ""; ;
        int resultGrabarEvento;
        try
        {
            resultGrabarEvento = planifAgendaCat.grabarEvento(evento, confirmarBorrado);
            if (resultGrabarEvento == -1) return resultGrabarEvento + "";
            else result = resultGrabarEvento + "";
            if (otrosProfesionales != null && otrosProfesionales.Count() > 0) result = planifAgendaCat.grabarEventoProfAsignados(evento, otrosProfesionales, confirmarBorrado);
            return result;
        }

        catch (ValidationException ex)
        {            
            throw new ValidationException(System.Uri.EscapeDataString(ex.Message));

        }

        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString(ex.Message));                 
        }

        finally
        {
            planifAgendaCat.Dispose();
        }     
   
    }
    

    /// <summary>
    /// Obtiene el detalle de un evento
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static Models.PlanifAgendaCat getDetalleEvento(int idEvento)
    {
        BLL.PlanifAgendaCat planifAgendaCat = new BLL.PlanifAgendaCat();

        try
        {
            Models.PlanifAgendaCat oEvento = null;

            oEvento = planifAgendaCat.getDetalleEvento(idEvento);
            return oEvento;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar el detalle de un evento", ex);
            throw new Exception("Error al cargar el detalle de un evento");
        }
        finally
        {
            planifAgendaCat.Dispose();
        }
    }



    /// <summary>
    /// Elimina un evento
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static void eliminarEvento(Models.PlanifAgendaCat evento, bool enviarMail, bool solapamiento)
    {
        BLL.PlanifAgendaCat planifAgendaCat = new BLL.PlanifAgendaCat();
        ArrayList aListCorreo = new ArrayList();   
        try
        {
            int result = planifAgendaCat.eliminarEvento(evento, enviarMail, solapamiento);
            
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al elmininar el evento", ex);
            throw new Exception("Error al eliminar el evento");
        }
       
        finally
        {
            planifAgendaCat.Dispose();
        }
    }

    /// <summary>
    /// Valida la tarea introducido por teclado
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.ValidarTareaAgenda validarTarea(int idFicepiProf, string idTarea)
    {

        BLL.ValidarTareaAgenda validarTareaAgendaBLL = new BLL.ValidarTareaAgenda();
        Models.ValidarTareaAgenda oTarea;
        try
        {
            if (idFicepiProf == 0) oTarea = validarTareaAgendaBLL.Select((int)HttpContext.Current.Session["IDFICEPI_IAP"], Int32.Parse(idTarea));
            else oTarea = validarTareaAgendaBLL.Select(idFicepiProf, Int32.Parse(idTarea));
            if (oTarea == null) return null;  
            return oTarea;
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la tarea (" + idTarea + ")."));
        }
        finally
        {
            validarTareaAgendaBLL.Dispose();
        }
    }

}