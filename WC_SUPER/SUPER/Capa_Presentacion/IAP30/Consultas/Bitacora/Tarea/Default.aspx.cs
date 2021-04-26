using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using IB.SUPER.Shared;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using System.Web.UI.HtmlControls;

public partial class Capa_Presentacion_IAP30_Consultas_Bitacora_Tarea_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;
        BLL.TareaBitacora oTarea_BLL = new BLL.TareaBitacora(cDblib);

        try
        {
            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
            //Variables de sesión
            string script1 = "IB.vars.superEditor = '" + Utilidades.EsAdminProduccion() + "';";
            //script1 += "IB.vars.codUsu = '" + Session["NUM_EMPLEADO_IAP"].ToString() + "';";

            if (ht["idAsunto"] != null && ht["idAsunto"].ToString() != "")
            {
                script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
            }
            if (ht["idAccion"] != null && ht["idAccion"].ToString() != "")
            {
                script1 += "IB.vars.idAccion = '" + ht["idAccion"].ToString() + "';";
            }

            if (ht["idTarea"] != null && ht["idTarea"].ToString() != "")
            {
                script1 += "IB.vars.idTarea = '" + ht["idTarea"].ToString() + "';";
                Models.TareaBitacora oTarea = new Models.TareaBitacora();
                oTarea = oTarea_BLL.Select(int.Parse(ht["idTarea"].ToString()));
                this.hdnIdProyectoSubNodo.Value = oTarea.t305_idproyectosubnodo.ToString();
                script1 += "IB.vars.idNodo = '" + oTarea.cod_une.ToString() + "';";
                script1 += "IB.vars.nPE = '" + oTarea.cod_pe.ToString() + "';";
                script1 += "IB.vars.estadoProyecto = '" + oTarea.t301_estado + "';";
                script1 += "IB.vars.desPE = '" + oTarea.nom_pe + "';";
                script1 += "IB.vars.nPSN = '" + oTarea.t305_idproyectosubnodo.ToString() + "';";
                script1 += "IB.vars.nPT = '" + oTarea.cod_pt.ToString() + "';";
                script1 += "IB.vars.desPT = '" + oTarea.nom_pt + "';";
                script1 += "IB.vars.desTarea = '" + oTarea.nom_tarea + "';";
                script1 += "IB.vars.fase = '" + oTarea.nom_fase + "';";
                script1 += "IB.vars.actividad = '" + oTarea.nom_actividad + "';";
            }
            else
            {
                if (ht["nPSN"] != null && ht["nPSN"].ToString() != "")
                {
                    script1 += "IB.vars.nPSN = '" + ht["nPSN"].ToString() + "';";
                    script1 += "IB.vars.txtProy = '" + ht["txtProy"].ToString() + "';";
                    this.hdnIdProyectoSubNodo.Value = ht["nPSN"].ToString();
                }
                else
                {
                    script1 += "IB.vars.nPSN = '';";
                    script1 += "IB.vars.txtProy = '';";
                }
            }
            if (ht["conACC"] != null && ht["conACC"].ToString() != "" && ht["conACC"].ToString() == "S")
            {
                this.chkConAcciones.Checked = true;
            }
            if (ht["auto"] != null && ht["auto"].ToString() != "" && ht["auto"].ToString() == "S")
            {
                this.chkAutomatica.Checked = true;
            }
            if (ht["tipo"] != null && ht["tipo"].ToString() != "")
            {
                //this.cboTipo.Value = ht["tipo"].ToString();
                script1 += "IB.vars.tipo = '" + ht["tipo"].ToString() + "';";
            }
            else
                script1 += "IB.vars.tipo = '';";
            if (ht["estado"] != null && ht["estado"].ToString() != "")
            {
                this.cboEstado.Value = ht["estado"].ToString();
            }
            if (ht["severidad"] != null && ht["severidad"].ToString() != "")
            {
                this.cboSeveridad.Value = ht["severidad"].ToString();
            }
            if (ht["prio"] != null && ht["prio"].ToString() != "")
            {
                this.cboPrioridad.Value = ht["prio"].ToString();
            }
            if (ht["denom"] != null && ht["denom"].ToString() != "")
            {
                this.txtDenominacion.Value = ht["denom"].ToString();
            }
            if (ht["notifD"] != null && ht["notifD"].ToString() != "") script1 += "IB.vars.notifD = '" + ht["notifD"].ToString() + "';";
            else script1 += "IB.vars.notifD = '';";
            if (ht["notifH"] != null && ht["notifH"].ToString() != "") script1 += "IB.vars.notifH = '" + ht["notifH"].ToString() + "';";
            else script1 += "IB.vars.notifH = '';";

            if (ht["limiteD"] != null && ht["limiteD"].ToString() != "") script1 += "IB.vars.limiteD = '" + ht["limiteD"].ToString() + "';";
            else script1 += "IB.vars.limiteD = '';";
            if (ht["limiteH"] != null && ht["limiteH"].ToString() != "") script1 += "IB.vars.limiteH = '" + ht["limiteH"].ToString() + "';";
            else script1 += "IB.vars.limiteH = '';";

            if (ht["finD"] != null && ht["finD"].ToString() != "") script1 += "IB.vars.finD = '" + ht["finD"].ToString() + "';";
            else script1 += "IB.vars.finD = '';";
            if (ht["finH"] != null && ht["finH"].ToString() != "") script1 += "IB.vars.finH = '" + ht["finH"].ToString() + "';";
            else script1 += "IB.vars.finH = '';";

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
        finally
        {
            oTarea_BLL.Dispose();
            DBConn.Dispose();
        }
    }

    /// <summary>
    /// Obtiene los partes de actividad en base a los filtros de la pantalla
    /// </summary>
    /// <returns>List<Models.ParteActividad></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.Bitacora> getElementos(int idTarea, bool acciones, string Denominacion,
                                                           Nullable<int> TipoAsunto, Nullable<int> Estado, Nullable<int> Severidad, Nullable<int> Prioridad,
                                                                    Nullable<DateTime> dNotif, Nullable<DateTime> hNotif,
                                                                    Nullable<DateTime> dLimite, Nullable<DateTime> hLimite,
                                                                    Nullable<DateTime> dFin, Nullable<DateTime> hFin
                                                                    )
    {
        BLL.Bitacora BitacoraBLL = new BLL.Bitacora();

        try
        {
            List<Models.Bitacora> lBitacora = null;

            lBitacora = BitacoraBLL.CatalogoTareas(idTarea, acciones, Denominacion, TipoAsunto, Estado, Severidad, Prioridad,
                                            dNotif, hNotif, dLimite, hLimite, dFin, hFin);
            return lBitacora;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener la bitácora", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener la bitácora"));
        }
        finally
        {
            BitacoraBLL.Dispose();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.TipoAsunto> obtenerTipoAsunto()
    {
        BLL.TipoAsunto TipoAsuntoBLL = new BLL.TipoAsunto();

        try
        {
            List<Models.TipoAsunto> lTipoAsunto = null;

            lTipoAsunto = TipoAsuntoBLL.Catalogo();
            return lTipoAsunto;
        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los tipos de asunto", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los tipos de asunto"));

        }
        finally
        {

            TipoAsuntoBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los datos de la tarea por el identificador seleccionado en el buscador
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.TareaIAP obtenerDetalleTarea(string idTarea)
    {
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.TareaIAP tareaIAPBLL = new BLL.TareaIAP(cDblib);

        try
        {
            return tareaIAPBLL.Select(Int32.Parse(idTarea));//, (int)HttpContext.Current.Session["UsuarioActual"]
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la tarea (" + idTarea + ")."));
        }
        finally
        {
            tareaIAPBLL.Dispose();
            DBConn.Dispose();
        }
    }


    /// <summary>
    /// Obtiene los datos de la tarea por el identificador introducido por teclado
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.TareaIAP getDetalleTarea(string idTarea)
    {
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.TareaIAP tareaIAPBLL = new BLL.TareaIAP(cDblib);

        try
        {
            return tareaIAPBLL.SelectBitacora(Int32.Parse(idTarea), (int)HttpContext.Current.Session["UsuarioActual"]);
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la tarea (" + idTarea + ")."));
        }
        finally
        {
            tareaIAPBLL.Dispose();
            DBConn.Dispose();
        }
    }

}