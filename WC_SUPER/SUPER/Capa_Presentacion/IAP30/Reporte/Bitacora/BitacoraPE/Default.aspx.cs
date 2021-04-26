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


public partial class Capa_Presentacion_Reporte_BitacoraPE_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //int nPSN = 0;
        string script1 = "";
        string sOrigen = "BITACORA";
        //string script1 = "IB.vars.idficepi = '" + Session["IDFICEPI_PC_ACTUAL"] + "';";
        //script1 += "IB.vars.codUsu = '" + Session["NUM_EMPLEADO_IAP"].ToString() + "';";
        //script1 += "IB.vars.bLectura = false;";

        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";

        Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
        try
        {
            script1 += "IB.vars.origen = '" + ht["ori"].ToString() + "';";
            if (ht["ori"].ToString() == "reporteIAP")
                sOrigen = "IAP";
        }
        catch { script1 += "IB.vars.origen = '';"; }
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


        string sIdPSN = ht["nPSN"].ToString();
        if (sIdPSN != "")
        {
            //nPSN = int.Parse(ht["nPSN"].ToString());
            script1 += "IB.vars.nPSN = '" + sIdPSN + "';";
            string hdnAcceso = ponerDatosProyecto(sOrigen, sIdPSN);
            script1 += "IB.vars.hdnAcceso = '" + hdnAcceso + "';";

        }
        else
        {
            script1 += "IB.vars.nPSN = '';";
            script1 += "IB.vars.hdnAcceso = 'X';";
        }
        //script1 += "IB.vars.hdnAcceso = 'E';";

        //script1 += "IB.vars.qs = '" + Request.QueryString.ToString() + "';";
        script1 += "IB.vars.qs = '" + Utils.decodpar(Request.QueryString.ToString()) + "';";
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
    }

    private string ponerDatosProyecto(string sOrigen, string sIdPSN)
    {
        BLL.ProyectoSubNodo Proy = new BLL.ProyectoSubNodo();
        Models.ProyectoSubNodo oProy = new Models.ProyectoSubNodo();
        string hdnAcceso = "X";
        try
        {
            int idPSN = int.Parse(sIdPSN);

            oProy = Proy.Select(idPSN);

            this.idProyecto.Value = oProy.t301_idproyecto.ToString("#,###");
            this.desProyecto.Value = oProy.t301_denominacion;

            if (sOrigen == "IAP")
            {
                if (oProy.t305_accesobitacora_iap != "X")
                {
                    if (oProy.t301_estado == "C" || oProy.t301_estado == "H")
                        hdnAcceso = "L";
                    else
                        hdnAcceso = oProy.t305_accesobitacora_iap;
                }
                else
                {
                    //throw new Exception("El proyecto no permite el acceso a bitácora desde IAP.");
                    string script2 = "IB.vars['error'] = 'El proyecto no permite el acceso a bitácora desde IAP.';";
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
                }
            }
            else
            {
                if (oProy.t305_accesobitacora_pst != "X")
                {
                    if (oProy.t301_estado == "C" || oProy.t301_estado == "H")
                        hdnAcceso = "L";
                    else
                        hdnAcceso = oProy.t305_accesobitacora_pst;
                }
                else
                {
                    //throw new Exception("El proyecto no permite el acceso a bitácora desde IAP.");
                    string script2 = "IB.vars['error'] = 'El proyecto no permite el acceso a bitácora desde IAP.';";
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
        finally
        {
            Proy.Dispose();
        }

        return hdnAcceso;
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
    public static List<Models.AsuntoCat> getAsuntos(string nPSN, string tipoAsunto, string estado)
    {
        BLL.AsuntoCat bAsunto = new BLL.AsuntoCat();
        int? idAsunto=null;
        byte? idEstado=null;
        try
        {
            List<Models.AsuntoCat> lAsuntos = null;
            if (tipoAsunto != "-1") idAsunto = int.Parse(tipoAsunto);
            if (estado != "-1") idEstado = byte.Parse(tipoAsunto);

            lAsuntos = bAsunto.Catalogo(int.Parse(nPSN), idAsunto, idEstado);

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
    public static List<Models.Accion> getAcciones(string nAsunto)
    {
        BLL.Accion bAccion = new BLL.Accion();
        try
        {
            List<Models.Accion> lAcciones = null;

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.ProyectoTecnico> getBitacoraPT(string nPSN)
    {
        BLL.ProyectoTecnicoBitacora cPT = new BLL.ProyectoTecnicoBitacora();
        try
        {
            List<Models.ProyectoTecnico> lPTs = null;
            string nUser = HttpContext.Current.Session["NUM_EMPLEADO_IAP"].ToString();
            lPTs = cPT.Catalogo(int.Parse(nPSN), int.Parse(nUser));

            cPT.Dispose();
            return lPTs;
        }
        catch (Exception ex)
        {
            if (cPT != null) cPT.Dispose();
            throw ex;
        }
        finally
        {
            cPT.Dispose();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrarAsuntos(List<Models.AsuntoCat> lineas)
    {
        BLL.AsuntoCat asunto = new BLL.AsuntoCat();
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
    public static void borrarAcciones(List<Models.Accion> lineas)
    {
        BLL.Accion accion = new BLL.Accion();
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