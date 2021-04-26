using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using IB.SUPER.Shared;

public partial class Capa_Presentacion_Reporte_BitacoraT_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //int nPSN = 0;
        string script1 = "";
        //string script1 = "IB.vars.idficepi = '" + Session["IDFICEPI_PC_ACTUAL"] + "';";
        //script1 += "IB.vars.codUsu = '" + Session["NUM_EMPLEADO_IAP"].ToString() + "';";
        //script1 += "IB.vars.bLectura = false;";

        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";

        Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
        string sIdT = ht["nT"].ToString();
        if (sIdT != "")
        {
            script1 += "IB.vars.nT = '" + sIdT + "';";
            Models.TareaBitacora oTarea = ponerDatosTarea(sIdT);
            script1 += "IB.vars.nPSN = '" + oTarea.t305_idproyectosubnodo.ToString() + "';";
            script1 += "IB.vars.nPT = '" + oTarea.cod_pt.ToString() + "';";
            script1 += "IB.vars.hdnAcceso = '" + oTarea.sAccesoBitacora + "';";
        }
        else
        {
            script1 += "IB.vars.nPSN = '';IB.vars.nPT = '';IB.vars.nT = '';";
        }
        try { script1 += "IB.vars.origen = '" + ht["ori"].ToString() + "';"; }
        catch { script1 += "IB.vars.origen = '';"; }
        try
        {
            script1 += "IB.vars.origen2 = '" + ht["ori2"].ToString() + "';";
        }
        catch
        {
            script1 += "IB.vars.origen2 = '';";
        }
        try
        {
            script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
        }
        catch { script1 += "IB.vars.idAsunto = '';"; }
        try
        {
            script1 += "IB.vars.idAccion = '" + ht["idAccion"].ToString() + "';";
        }
        catch { script1 += "IB.vars.idAccion = '';"; }

        //script1 += "IB.vars.qs = '" + Request.QueryString.ToString() + "';";
        script1 += "IB.vars.qs = '" + Utils.decodpar(Request.QueryString.ToString()) + "';";
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
    }

    private Models.TareaBitacora ponerDatosTarea(string sIdT)
    {
        BLL.TareaBitacora Tarea = new BLL.TareaBitacora();
        Models.TareaBitacora oTarea = new Models.TareaBitacora();
        oTarea.sAccesoBitacora = "X";
        try
        {
            int idT = int.Parse(sIdT);

            oTarea = Tarea.Select(idT);
            this.idProyecto.Value = oTarea.cod_pe.ToString("#,###");
            this.desProyecto.Value = oTarea.nom_pe;
            this.idPT.Value = oTarea.cod_pt.ToString("#,###");
            this.desProyectoT.Value = oTarea.nom_pt;
            this.fase.Value = oTarea.nom_fase;
            this.actividad.Value = oTarea.nom_actividad;
            this.idTarea.Value = idT.ToString("#,###");
            this.tareaDes.Value = oTarea.nom_tarea;
            #region Comprobación de permiso de acceso. Lo comento para solo mirar el permiso a nivel de tarea
            //if (oTarea.t305_accesobitacora_pst != "X")
            //{
            //    if (oTarea.t331_acceso_iap != "X")
            //    {
            //        if (oTarea.t332_acceso_iap != "X")
            //        {
            //            if (oTarea.t301_estado == "C" || oTarea.t301_estado == "H")
            //                this.hdnAcceso.Value = "L";
            //            else
            //            {
            //                if (oTarea.t305_accesobitacora_pst == "L")
            //                    this.hdnAcceso.Value = "L";
            //                else
            //                {
            //                    if (oTarea.t331_acceso_iap == "L")
            //                        this.hdnAcceso.Value = "L";
            //                    else
            //                        this.hdnAcceso.Value = oTarea.t332_acceso_iap;
            //                }
            //            }
            //        }
            //        else
            //            throw new Exception("La tarea no permite el acceso a bitácora desde IAP.");
            //    }
            //    else
            //        throw new Exception("El proyecto técnico no permite el acceso a bitácora desde IAP.");
            //}
            //else
            //    throw new Exception("El proyecto económico no permite el acceso a bitácora desde IAP.");
            #endregion
            if (oTarea.t332_acceso_iap != "X")
            {
                if (oTarea.t301_estado == "C" || oTarea.t301_estado == "H")
                    oTarea.sAccesoBitacora = "L";
                else
                    oTarea.sAccesoBitacora = oTarea.t332_acceso_iap;
            }
            else
                throw new Exception("La tarea no permite el acceso a bitácora desde IAP.");
        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
        finally
        {
            Tarea.Dispose();
        }
        return oTarea;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.TipoAsuntoCat> getTiposAsunto()
    {
        IB.SUPER.IAP30.BLL.TipoAsuntoCat cCatalogo = new IB.SUPER.IAP30.BLL.TipoAsuntoCat();

        try
        {
            Models.TipoAsuntoCat oFiltro = new Models.TipoAsuntoCat();
            oFiltro.t384_destipo = "";
            oFiltro.nOrden = 1;
            oFiltro.nAscDesc = 0;

            List<Models.TipoAsuntoCat> Lista = cCatalogo.Catalogo(oFiltro);
            return Lista;
        }
        catch (Exception ex)
        {
            if (cCatalogo != null) cCatalogo.Dispose();
            throw ex;
        }
        finally
        {
            cCatalogo.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AsuntoCat> getAsuntos(string nTarea, string tipoAsunto, string estado)
    {
        BLL.AsuntoT bAsunto = new BLL.AsuntoT();
        int? idAsunto = null;
        byte? idEstado = null;
        try
        {
            List<Models.AsuntoCat> lAsuntos = null;
            if (tipoAsunto != "-1") idAsunto = int.Parse(tipoAsunto);
            if (estado != "-1") idEstado = byte.Parse(tipoAsunto);

            lAsuntos = bAsunto.Catalogo(int.Parse(nTarea), idAsunto, idEstado);

            bAsunto.Dispose();
            return lAsuntos;
        }
        catch (Exception ex)
        {
            if (bAsunto != null) bAsunto.Dispose();
            throw ex;
        }
        finally
        {
            bAsunto.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AccionT> getAcciones(string nAsunto)
    {
        BLL.AccionT bAccion = new BLL.AccionT();
        try
        {
            List<Models.AccionT> lAcciones = null;

            if (nAsunto != "")
                lAcciones = bAccion.Catalogo(int.Parse(nAsunto));

            bAccion.Dispose();
            return lAcciones;
        }
        catch (Exception ex)
        {
            if (bAccion != null) bAccion.Dispose();
            throw ex;
        }
        finally
        {
            bAccion.Dispose();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrarAsuntos(List<Models.AsuntoCat> lineas)
    {
        BLL.AsuntoT asunto = new BLL.AsuntoT();
        try
        {
            asunto.BorrarAsuntos(lineas);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al eliminar asunto", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al eliminar asunto " + ex.Message));

        }
        finally
        {
            asunto.Dispose();
        }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrarAcciones(List<Models.AccionT> lineas)
    {
        BLL.AccionT accion = new BLL.AccionT();
        try
        {
            accion.BorrarAcciones(lineas);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al eliminar acción", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al eliminar acción " + ex.Message));

        }
        finally
        {
            accion.Dispose();
        }

    }

}