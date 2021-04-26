using IB.SUPER.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

public partial class Capa_Presentacion_SIC_Tarea_CatalogoDeAccion_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        BLL.AccionPreventa cAccion = null;

        try
        {
            
            int ta204_idaccionpreventa = 0;

            //Historial de navegacion            
            IB.SUPER.Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);

            //Parámetros querystring (idacción)
            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());

            if (ht["origenPantalla"] != null && ht["origenPantalla"].ToString().ToUpper().Trim() == "CRM") {
                getCabeceraPreventa(body);
            }

            if (ht["id"] != null)
                ta204_idaccionpreventa = int.Parse(ht["id"].ToString());
            
            //Obtenemos los datos de la acción
            cAccion = new BLL.AccionPreventa();
            Models.AccionPreventa oAccion = cAccion.accionPreventa_catTareas(ta204_idaccionpreventa);

            string lbl = "";
            switch (oAccion.ta206_itemorigen.ToString())
            {
                case "O":
                    lbl = "Oportunidad";
                    div_container_cab_P.Visible = false;
                    break;
                case "E":
                    lbl = "Extensión";
                    div_container_cab_P.Visible = false;
                    break;
                case "P":
                    lbl = "Objetivo";
                    
                    div_container_cab_OE.Visible = false;
                    break;
                case "S":
                    lbl = "Solicitud";
                    divCliente.Visible = false;
                    div_container_cab_OE.Visible = false;
                    div_container_cab_P.Visible = false;
                    linkInformacionAdicional.Visible = false;
                    break;
            }

            lblOportunidadSolic.InnerText = lbl;

            //Deshabilitamos el botón añadir tarea en caso de acciones o solicitudes finalizadas o anuladas, o figuras sin acceso
            if (!oAccion.btnAddTarea)
                btnAddTarea.Style.Add("display", "none");

            txtTipoAccion.Value = oAccion.tipoAccion;
            txtLider.Value = oAccion.lider;
            txtFinRequerida.Value = oAccion.ta204_fechafinestipulada.ToShortDateString();

            //Publicamos estas variables para que sean accesibles desde el Javascript
            string script1 = "IB.vars.ta204_idaccionpreventa = " + oAccion.ta204_idaccionpreventa + ";";
            script1 += "IB.vars.ta206_iditemorigen = '" + oAccion.ta206_iditemorigen.ToString() + "';";
            script1 += "IB.vars.ta206_itemorigen = '" + oAccion.ta206_itemorigen.ToString() + "';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar el catálogo de tareas", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "IB.vars.error = 'Se ha producido un error al obtener los datos de la tareas.';", true);            
        }

        finally
        {            
            if (cAccion != null) cAccion.Dispose();            
        }  
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string obtenerTareasbyAccion(int ta204_idaccionpreventa)
    {

        BLL.TareaPreventa cTP = new BLL.TareaPreventa();

        try
        {            
            return JsonConvert.SerializeObject(cTP.CatalogoPorAccion(ta204_idaccionpreventa));            
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar las tareas de la acción", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error cargando los datos."));
        }
        finally
        {
            cTP.Dispose();
        }

    }

    public void getCabeceraPreventa(Control idBody)
    {
        body.Controls.Remove(Menu);
        body.Controls.AddAt(0, (this.LoadControl("~/Capa_Presentacion/bsUserControls/cabeceraPreventa.ascx")));
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void quitarNegritasTareasPendientes(int idtarea)
    {

        IB.SUPER.SIC.BLL.TareaPendientePreventa cTP = new IB.SUPER.SIC.BLL.TareaPendientePreventa();
        List<int> tablaconceptos = new List<int>();
        tablaconceptos.Add(4);

        try
        {
            cTP.quitarNegritaTareaPendiente((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], tablaconceptos, null, idtarea);
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al quitar las negritas de las tareas pendientes.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al quitar las negritas de las tareas pendientes."));
        }
        finally
        {
            cTP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.SolicitudPreventa ObtenerDatosSUPER(int ta206_iditemorigen)
    {

        BLL.SolicitudPreventa c = new BLL.SolicitudPreventa();

        try
        {
            return c.SelectById(ta206_iditemorigen);
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error al cargar la información de la solicitud", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener la información de cabecera de la solicitud."));
        }
        finally
        {
            c.Dispose();
        }

    }
}