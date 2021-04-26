using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IB.SUPER.Shared;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Newtonsoft.Json;

public partial class Capa_Presentacion_SIC_DetalleTarea_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        BLL.TareaPreventa cTP = null;
        BLL.AccionPreventa cAccion = null;
        BLL.Usuario cUsuario = null;
        Models.PerfilesEdicion oPE = null;
        IB.SUPER.Shared.DBConn cDBConn = null;

        try
        {
            Guid uidDocumento;
            int ta204_idaccionpreventa = 0;
            int ta207_idtareapreventa = 0;
            int ta201_idsubareapreventa = -1;
            string modoPantalla = "C";
            bool soyLider = false;


            //Historial de navegacion
            IB.SUPER.Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);

            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());

            cDBConn = new IB.SUPER.Shared.DBConn();

            //Cuando viene de una insert no hay idtarea
            if (ht["idTarea"] != null)
                ta207_idtareapreventa = int.Parse(ht["idTarea"].ToString());

            //Cuando el modo de edición sea "A", la acción vendrá por parámetro URL
            if (ht["idAccion"] != null)
                ta204_idaccionpreventa = int.Parse(ht["idAccion"].ToString());

            if (ht["modoPantalla"] != null)
                modoPantalla = ht["modoPantalla"].ToString();

            //Obtenemos los datos de la acción
            cAccion = new BLL.AccionPreventa();
            Models.AccionPreventa oAccion = cAccion.Select(ta204_idaccionpreventa);
            ta201_idsubareapreventa = oAccion.ta201_idsubareapreventa;

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
            this.txtFinRequerida.Value = oAccion.ta204_fechafinestipulada.ToShortDateString();
            this.ta205_denominacion.Value = oAccion.ta205_denominacion;
            this.lider.Value = oAccion.lider;
            soyLider = (int)Session["IDFICEPI_PC_ACTUAL"] == oAccion.t001_idficepi_lider;

            string estadoParticipacion = String.Empty;
            uidDocumento = Guid.NewGuid();
            string ta207_estado = String.Empty;

            //Publicamos estas variables para que sean accesibles desde el Javascript
            string script1 = "IB.vars.ta207_idtareapreventa = '" + ta207_idtareapreventa.ToString() + "';";
            script1 += "IB.vars.ta206_iditemorigen = '" + oAccion.ta206_iditemorigen.ToString() + "';";
            script1 += "IB.vars.ta206_itemorigen = '" + oAccion.ta206_itemorigen.ToString() + "';";
            script1 += "IB.vars.ta204_estado = '" + oAccion.ta204_estado.ToString() + "';";


            
            if (modoPantalla == "A") {
                //Obtener denominaciones de tarea
                cTP = new BLL.TareaPreventa();
                List<Models.TareaPreventa> lstDenominaciones = cTP.lstDenominacionesTarea();
                selectDenominacion.DataSource = lstDenominaciones;
                selectDenominacion.DataTextField = "ta219_denominacion";
                selectDenominacion.DataValueField = "ta219_idtipotareapreventa";

                selectDenominacion.DataBind();

                selectDenominacion.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                selectDenominacion.SelectedIndex = 0;
                selectDenominacion.Items.Add(new ListItem("Otras tareas", "-1"));
                cTP.Dispose();

            }



            //Obtener el detalle de la tarea en caso de que el modo pantalla sea edición
            if (modoPantalla == "E")
            {
                cTP = new BLL.TareaPreventa();
                Models.TareaPreventaDetalleParticipante oTPDE = cTP.DetalleTarea(ta207_idtareapreventa, int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()));

                ta201_idsubareapreventa = oTPDE.ta201_idsubareapreventa;
                if (oTPDE.ta219_idtipotareapreventa != null)
                {
                    //selectDenominacion.SelectedValue = oTPDE.ta219_idtipotareapreventa.ToString();                    
                    ta207_denominacion.Attributes.Remove("required");
                    selectDenominacion.Items.Insert(0, new ListItem(oTPDE.ta219_denominacion, oTPDE.ta219_idtipotareapreventa.ToString()));                                        
                }
                else {                    
                    selectDenominacion.Items.Add(new ListItem("Otras tareas", "-1"));
                    selectDenominacion.SelectedValue = "-1";
                    divinputDenominacion.Style.Add("display", "block");                    
                }
                    

                //Obtiene el estado de un participante en una tarea.
                Models.TareaPreventaDetalleParticipante oTPSP = cTP.estadoparticipacion(int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), ta207_idtareapreventa);

                if (oTPDE.ta207_idtareapreventa == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "IB.vars.aviso = 1;", true); //No se han obtenido datos de la tarea
                }

                //Estado participación
                estadoParticipacion = oTPSP.ta214_estado;

                //Líder 
                soyLider = (int)Session["IDFICEPI_PC_ACTUAL"] == oTPDE.t001_idficepi_lider;

                //Datos
                this.ta207_idtareapreventa.Value = oTPDE.ta207_idtareapreventa.ToString();                
                this.ta207_denominacion.Value = oTPDE.ta207_denominacion;
                this.ta207_observaciones.Value = oTPDE.ta207_observaciones;
                this.ta207_fechafinprevista.Value = oTPDE.ta207_fechafinprevista.ToString("dd/MM/yyyy");
                this.ta207_fechacreacion.Value = oTPDE.ta207_fechacreacion.ToString("dd/MM/yyyy");

                this.lblSello.Attributes.Add("data-after", BLL.TareaPreventa.GetLiteralEstadoTarea(oTPDE.ta207_estado.ToString()));

                
                if (oTPDE.ta207_fechafinreal != null) {
                    this.ta207_fechafinreal.Value = oTPDE.ta207_fechafinreal.ToString();
                    ta207_fechafinreal.Style.Add("visibility", "visible");
                    lblta207_fechafinreal.Style.Add("visibility", "visible");                                        
                }
                else {
                    ta207_fechafinreal.Style.Add("visibility", "hidden");
                    lblta207_fechafinreal.Style.Add("visibility", "hidden");
                }

                setSelloEstado(oTPDE.ta207_estado);


                this.ta207_descripcion.InnerText = oTPDE.ta207_descripcion;
                this.ta207_comentario.InnerText = oTPDE.ta207_comentarios;
                this.textareaMotivoAnulacion.InnerText = oTPDE.ta207_motivoanulacion;

                this.linkDocumentacion.InnerText = "Documentación";

                //Publicamos estas variables para que sean accesibles desde el Javascript
                script1 += "IB.vars.ta204_idaccionpreventa = " + oTPDE.ta204_idaccionpreventa + ";";
                script1 += "IB.vars.ta201_idsubareapreventa = " + oTPDE.ta201_idsubareapreventa + ";";
                script1 += "IB.vars.ta207_estado = '" + oTPDE.ta207_estado.ToString() + "';";
                script1 += "IB.vars.t001_idficepi_lider = '" + oTPDE.t001_idficepi_lider.ToString() + "';";

                script1 += "IB.vars.soyLider = " + soyLider.ToString().ToLower() + ";";
                script1 += "IB.vars.fechaCreacion = '" + oTPDE.ta207_fechacreacion.ToString("dd/MM/yyyy") + "';";
            }

           
            //Perfiles de usuario
            cUsuario = new BLL.Usuario(cDBConn.dblibclass);
            oPE = cUsuario.obtenerPerfilesEdicionUsuario(User, soyLider, ta201_idsubareapreventa);

            //Publicamos los perfiles del Usuario
            string script2 = "IB.vars.perfilesEdicion = " + JsonConvert.SerializeObject(oPE) + ";";

            //Publicamos estas variables para que sean accesibles desde el Javascript            
            script1 += "IB.vars.modoPantalla = '" + modoPantalla + "';";
            script1 += "IB.vars.estadoParticipacion = '" + estadoParticipacion + "';";
            script1 += "IB.vars.ta204_idaccionpreventa = '" + ta204_idaccionpreventa + "';";

            //if (modoPantalla == "A") {                
            //    ta207_denominacion.Attributes.Remove("required");                
            //}
                script1 += "IB.vars.uidDocumento = '" + uidDocumento + "';";
            //script1 += "IB.vars.ta207_estado = 'A';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);

        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar el detalle de la tarea", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "IB.vars.error = 'Se ha producido un error al obtener los datos de la tarea.';", true);
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
            if (cAccion != null) cAccion.Dispose();
            if (cUsuario != null) cUsuario.Dispose();
            if (cDBConn != null) cDBConn.Dispose();
        }
    }

    /// <summary>
    /// Actualiza la participación
    /// </summary>
    /// <param name="ta207_idtareapreventa"></param>
    /// <param name="estado"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void actualizarParticipacion(int ta207_idtareapreventa, string estado)
    {
        BLL.ParticipanteTareaPreventa cETP = null;

        try
        {
            Models.ParticipanteTareaPreventa o = new Models.ParticipanteTareaPreventa();
            o.ta207_idtareapreventa = ta207_idtareapreventa;
            o.t001_idficepi_participante = (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"];
            o.ta214_estado = Convert.ToString(estado);

            cETP = new BLL.ParticipanteTareaPreventa();
            cETP.Update(o);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al actualizar la participación del participante en la tarea", ex);
            throw new Exception("Error al actualizar la participación en la tarea");
        }
        finally
        {
            if (cETP != null) cETP.Dispose();
        }

    }

    /// <summary>
    /// Obtiene los participantes de una tarea (se carga en un Datatable)
    /// </summary>
    /// <param name="ta207_idtareapreventa"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string obtenerParticipantes(int ta207_idtareapreventa)
    {
        BLL.TareaPreventa cTP = new BLL.TareaPreventa();

        try
        {
            //Participantees de la tarea            
            return JsonConvert.SerializeObject(cTP.ObtenerParticipantes(ta207_idtareapreventa));
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener los participantes de la tarea", ex);
            throw new Exception("Error al obtener los participantes de la tarea");
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
        }

    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int grabarTarea(Models.TareaPreventa oTarea, List<Models.ParticipanteTareaPreventa> listaParticipantes, string modoPantalla, string estadoParticipacion, Models.PerfilesEdicion oPerfilesEdicion)
    {
        BLL.TareaPreventa cTP = new BLL.TareaPreventa();

        try
        {
            return cTP.grabarTarea(oTarea, listaParticipantes, modoPantalla, estadoParticipacion, oPerfilesEdicion);                        
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar la tarea", ex);
            throw ex;
        }
        finally
        {
            if (cTP != null) cTP.Dispose();
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


    private string setSelloEstado(string estado)
    {

        string clase = String.Empty;
        switch (estado)
        {
            case "A":
                clase = "verde";
                break;
            case "F":
                clase = "gris";
                lblta207_fechafinreal.InnerText = "Fecha de finalización";
                break;
            case "X":
                clase = "rojo";
                lblta207_fechafinreal.InnerText = "Fecha de anulación";
                break;
            case "FS":
            case "XS":
            case "XA":
            case "FA":
                clase = "rojo";
                lblta207_fechafinreal.InnerText = "Fecha de cierre";
                break;

        }
        return clase;
    }

    private string GetLiteralEstadoParticipacion(string estado)
    {
        switch (estado)
        {
            case "A": return "En curso";
            case "F": return "Finalizada";
            case "X": return "Anulada";
            default: return "";
        }
    }

}