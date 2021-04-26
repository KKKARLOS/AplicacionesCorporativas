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


public partial class Capa_Presentacion_Reporte_BitacoraPT_Default : System.Web.UI.Page
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
        string sIdPT = ht["nPT"].ToString();
        if (sIdPT != "")
        {
            script1 += "IB.vars.nPT = '" + sIdPT + "';";
            Models.ProyectoTecnico oProy = ponerDatosPT(sIdPT);
            script1 += "IB.vars.nPSN = '" + oProy.t305_idproyectosubnodo.ToString() + "';";
            script1 += "IB.vars.hdnAcceso = '" + oProy.t305_accesobitacora_pst + "';";
        }
        else
        {
            script1 += "IB.vars.nPT = '';IB.vars.hdnAcceso = 'X';";
        }
        try
        {
            script1 += "IB.vars.origen = '" + ht["ori"].ToString() + "';";
        }
        catch
        {
            script1 += "IB.vars.origen = '';";
        }
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

    private Models.ProyectoTecnico ponerDatosPT(string sIdPT)
    {
        BLL.ProyectoTecnico Proy = new BLL.ProyectoTecnico();
        Models.ProyectoTecnico oProy = new Models.ProyectoTecnico();
        oProy.t305_accesobitacora_pst = "X";
        try
        {
            int idPT = int.Parse(sIdPT);

            oProy = Proy.Select(idPT);
            this.idProyecto.Value = oProy.num_proyecto.ToString("#,###");
            this.desProyecto.Value = oProy.nom_proyecto;
            this.idPT.Value = idPT.ToString("#,###");
            this.desProyectoT.Value = oProy.t331_despt;
            #region Comprobación de permiso de acceso. Lo comento para solo mirar el permiso a nivel de PT
            //if (oProy.t305_accesobitacora_pst != "X")
            //{
            //    if (oProy.t331_acceso_iap != "X")
            //    {
            //        if (oProy.t301_estado == "C" || oProy.t301_estado == "H")
            //            this.hdnAcceso.Value = "L";
            //        else
            //        {
            //            if (oProy.t305_accesobitacora_pst == "L")
            //                this.hdnAcceso.Value = "L";
            //            else
            //                this.hdnAcceso.Value = oProy.t331_acceso_iap;
            //        }
            //    }
            //    else
            //        throw new Exception("El proyecto técnico no permite el acceso a bitácora desde IAP.");
            //}
            //else
            //    throw new Exception("El proyecto económico no permite el acceso a bitácora desde IAP.");
            #endregion
            if (oProy.t331_acceso_iap != "X")
            {
                if (oProy.t301_estado == "C" || oProy.t301_estado == "H")
                    oProy.t305_accesobitacora_pst = "L";
                else
                    oProy.t305_accesobitacora_pst = oProy.t331_acceso_iap;
            }
            else
                throw new Exception("El proyecto técnico no permite el acceso a bitácora desde IAP.");
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
        return oProy;
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
    public static List<Models.AsuntoCat> getAsuntos(string nPT, string tipoAsunto, string estado)
    {
        BLL.AsuntoPT bAsunto = new BLL.AsuntoPT();
        int? idAsunto = null;
        byte? idEstado = null;
        try
        {
            List<Models.AsuntoCat> lAsuntos = null;
            if (tipoAsunto != "-1") idAsunto = int.Parse(tipoAsunto);
            if (estado != "-1") idEstado = byte.Parse(tipoAsunto);

            lAsuntos = bAsunto.Catalogo(int.Parse(nPT), idAsunto, idEstado);

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
    public static List<Models.AccionPT> getAcciones(string nAsunto)
    {
        BLL.AccionPT bAccion = new BLL.AccionPT();
        try
        {
            List<Models.AccionPT> lAcciones = null;

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
    public static List<Models.TareaBitacora> getBitacoraTarea(string nPT)
    {
        BLL.TareaBitacora cTarea = new BLL.TareaBitacora();
        try
        {
            List<Models.TareaBitacora> lPTs = null;
            lPTs = cTarea.Catalogo(int.Parse(nPT));

            cTarea.Dispose();
            return lPTs;
        }
        catch (Exception ex)
        {
            if (cTarea != null) cTarea.Dispose();
            throw ex;
        }
        finally
        {
            cTarea.Dispose();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrarAsuntos(List<Models.AsuntoCat> lineas)
    {
        BLL.AsuntoPT asunto = new BLL.AsuntoPT();
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
    public static void borrarAcciones(List<Models.AccionPT> lineas)
    {
        BLL.AccionPT accion = new BLL.AccionPT();
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