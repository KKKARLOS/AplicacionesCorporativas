using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IB.SUPER.Shared;
using Newtonsoft.Json;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Shared = IB.SUPER.Shared;

public partial class Capa_Presentacion_SIC_Accion_Detalle_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int ta204_idaccionpreventa = 0;
        int ta201_idsubareapreventa = -1;
        bool soyLider = false;

        BLL.AccionPreventa cAP = null;
        BLL.Usuario cUsuario = null;
        Models.PerfilesEdicion oPE = null;
        BLL.SolicitudPreventa cSP = null;
        Shared.DBConn cDBConn = null;
        BLL.AreaPreventa cArea = null;
        Models.SolicitudPreventa oSP = null;



        try
        {
            //Historial de navegacion
            Shared.HistorialNavegacion.Insertar(Request.Url.ToString(), true);

            //params modo=A&itemorigen=O&iditemorigen=6&origenpantalla=CRM
            //qs --> bW9kbz1BJml0ZW1vcmlnZW49TyZpZGl0ZW1vcmlnZW49NiZvcmlnZW5wYW50YWxsYT1DUk0=

            //params modo=A&itemorigen=S&iditemorigen=19&origenpantalla=SUPER
            //qs --> bW9kbz1BJml0ZW1vcmlnZW49UyZpZGl0ZW1vcmlnZW49MTkmb3JpZ2VucGFudGFsbGE9U1VQRVI=

            //params modo=E&id=6&itemorigen=O&iditemorigen=6&origenpantalla=CRM
            //qs --> bW9kbz1FJmlkPTYmaXRlbW9yaWdlbj1PJmlkaXRlbW9yaWdlbj02Jm9yaWdlbnBhbnRhbGxhPUNSTQ==

            //params modo=E&id=12&itemorigen=O&iditemorigen=1&origenpantalla=SUPER
            //qs --> bW9kbz1FJmlkPTEyJml0ZW1vcmlnZW49TyZpZGl0ZW1vcmlnZW49MSZvcmlnZW5wYW50YWxsYT1TVVBFUg==

            //params modo=E&id=6&itemorigen=O&iditemorigen=6&origenpantalla=SUPER
            //qs --> bW9kbz1FJmlkPTYmaXRlbW9yaWdlbj1PJmlkaXRlbW9yaWdlbj02Jm9yaWdlbnBhbnRhbGxhPVNVUEVS

            //params modo=E&id=50&itemorigen=S&iditemorigen=19&origenpantalla=SUPER
            //qs --> bW9kbz1FJmlkPTUwJml0ZW1vcmlnZW49UyZpZGl0ZW1vcmlnZW49MTkmb3JpZ2VucGFudGFsbGE9U1VQRVI=

            //params modo=E&id=1&origenpantalla=CRM
            //qs --> bW9kbz1FJmlkPTEmb3JpZ2VucGFudGFsbGE9Q1JN

            //params modo=E&id=50&origenpantalla=SUPER
            //qs --> bW9kbz1FJmlkPTUwJm9yaWdlbnBhbnRhbGxhPVNVUEVS

            //params modo=C&id=6&itemorigen=O&iditemorigen=6&origenpantalla=SUPER
            //qs --> bW9kbz1DJmlkPTYmaXRlbW9yaWdlbj1PJmlkaXRlbW9yaWdlbj02Jm9yaWdlbnBhbnRhbGxhPVNVUEVS

            //parametros:
            // - modo            A=Alta; E=Edicióm; C=Consulta
            // - id              id de la acción para los modos E y C
            // - itemorigen      Tipo de solicitud para el modo A --> O->Oportunidad; P->Partida/Objetivo; E->Extensión; S->SUPER
            // - iditemorigen    id del tipo de solicitud (ta206_idsolicitudpreventa si viene desde super)
            // - origenpantalla  Origen de la llamada a la pantalla --> CRM; SUPER

            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());

            string modo = ht["modo"].ToString().Trim().ToUpper();
            string origenpantalla = ht["origenpantalla"].ToString().Trim().ToUpper();
            string itemorigen = "";
            string iditemorigen = "";
            string caller = ""; //pantalla que llama (autoasignacion, etc)

            //cabecera
            if (origenpantalla == "CRM")
            {
                getCabeceraPreventa(body);
            }


            cDBConn = new Shared.DBConn();

            if (modo == "E" || modo == "C")  //Edicion y consulta
            {
                ta204_idaccionpreventa = int.Parse(ht["id"].ToString());

                if (ht["itemorigen"] != null && ht["iditemorigen"] != null)
                {
                    itemorigen = ht["itemorigen"].ToString().Trim().ToUpper();
                    iditemorigen = ht["iditemorigen"].ToString().Trim().ToUpper();
                }
                else
                {
                    cSP = new BLL.SolicitudPreventa(cDBConn.dblibclass);
                    oSP = cSP.getSolicitudbyAccion(ta204_idaccionpreventa);

                    itemorigen = oSP.ta206_itemorigen;
                    iditemorigen = oSP.ta206_iditemorigen.ToString();
                }

                if (ht["caller"] != null && ht["caller"].ToString().Trim().Length > 0)
                    caller = ht["caller"].ToString();

                cAP = new BLL.AccionPreventa(cDBConn.dblibclass);
                Models.AccionPreventa oAP = cAP.Select(ta204_idaccionpreventa);
                ta201_idsubareapreventa = oAP.ta201_idsubareapreventa;

                //Protecciones:
                // - Si la acción no está abierta --> pantalla en modo consulta
                if (modo == "E" && oAP.ta204_estado != "A") modo = "C";

                string jsonAccion = Newtonsoft.Json.JsonConvert.SerializeObject(oAP);
                string script0 = "IB.vars.oAccion = " + jsonAccion + ";";
                script0 += "IB.vars.ta204_idaccionpreventa = " + oAP.ta204_idaccionpreventa + ";";
                script0 += "IB.vars.ta206_estado = '" + oAP.ta206_estado + "';";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script0", script0, true);

                soyLider = (int)Session["IDFICEPI_PC_ACTUAL"] == oAP.t001_idficepi_lider;

            }
            else if (modo == "A") //alta
            {
                itemorigen = ht["itemorigen"].ToString().Trim().ToUpper();
                iditemorigen = ht["iditemorigen"].ToString().Trim().ToUpper();

                //Alta de acciones desde SUPER: sólo se permite crear acciones en el area de la solicitud
                if (itemorigen == "S")
                {
                    cSP = new BLL.SolicitudPreventa(cDBConn.dblibclass);
                    cArea = new BLL.AreaPreventa(cDBConn.dblibclass);
                    oSP = cSP.Select(int.Parse(iditemorigen), itemorigen);
                    Models.AreaPreventa oAP = cArea.Select((int)oSP.ta200_idareapreventa);

                    string script3 = "IB.vars.ta199_idunidadpreventa = " + oAP.ta199_idunidadpreventa + ";";
                    script3 += "IB.vars.ta200_idareapreventa = " + oSP.ta200_idareapreventa + ";";
                    script3 += "IB.vars.ta206_estado = '" + oSP.ta206_estado + "';";
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script3", script3, true);
                }

                string script0 = "IB.vars.ta204_idaccionpreventa = '" + Guid.NewGuid().ToString() + "';";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script0", script0, true);
            }

            string script1 = "IB.vars.modo = '" + modo + "';";
            script1 += "IB.vars.origenpantalla = '" + origenpantalla + "';";
            script1 += "IB.vars.caller = '" + caller + "';";
            script1 += "IB.vars.iditemorigen = " + iditemorigen + ";";
            script1 += "IB.vars.itemorigen = '" + itemorigen + "';";
            script1 += "IB.vars.idficepi = " + Session["IDFICEPI_PC_ACTUAL"].ToString() + ";";
            script1 += "IB.vars.profesional = '" + Session["APELLIDO1"].ToString().ToUpper() + " " + Session["APELLIDO2"].ToString().ToUpper() + ", " + Utils.Capitalize(Session["NOMBRE"].ToString()) + "';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

            cUsuario = new BLL.Usuario(cDBConn.dblibclass);
            oPE = cUsuario.obtenerPerfilesEdicionUsuario(User, soyLider, ta201_idsubareapreventa);
            if (origenpantalla == "SUPER") oPE.soyComercial = false; //desde super el rol comercial no debe afectar

            //oPE.soyAdministrador = false;
            //oPE.soyFiguraSubareaActual = true;
            //oPE.soyLider = false;
            //oPE.soyPosibleLider = true;
            //oPE.soyFiguraAreaActual = false;

            string script2 = "IB.vars.perfilesEdicion = " + JsonConvert.SerializeObject(oPE) + ";";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);

            //Información de cabecera. Solo el ID, el resto por ajax en el init del js.
            string lbl = "";
            switch (itemorigen)
            {
                case "O":
                    lbl = "Oportunidad";
                    div_container_cab_P.Visible = false;
                    break;
                case "E":
                    lbl = "Extensión";
                    div_container_cab_P.Visible = false;
                    divOportExt.Attributes.Add("class", "form-group show");
                    break;
                case "P":
                    lbl = "Objetivo";
                    //div_txtCuenta_cab.Visible = false;
                    div_container_cab_OE.Visible = false;

                    break;
                case "S":
                    lbl = "Solicitud";
                    div_txtCuenta_cab.Visible = false;
                    div_container_cab_OE.Visible = false;
                    div_container_cab_P.Visible = false;
                    linkInformacionAdicional.Visible = false;
                    break;
            }
            this.lblItemorigen_cab.InnerText = lbl;
            this.txtIditemorigen_cab.Value = iditemorigen;
        }

        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar el detalle de la acción preventa", ex);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scripterr", "IB.vars.error = 'Se ha producido un error durante la carga de la acción preventa.';", true);
        }

        finally
        {
            if (cArea != null) cArea.Dispose();
            if (cAP != null) cAP.Dispose();
            if (cUsuario != null) cUsuario.Dispose();
            if (cSP != null) cSP.Dispose();
            if (cDBConn != null) cDBConn.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.AccionPreventa getAccion(int ta204_idaccionpreventa)
    {

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();

        try
        {
            //int ta204_idaccionpreventa = int.Parse(IB.SUPER.Shared.Crypt.Decrypt(System.Uri.UnescapeDataString(ta204_idaccionpreventa_encrypt)));

            return cAP.Select(ta204_idaccionpreventa);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar la acción preventa", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los datos de la acción preventa."));
        }
        finally
        {
            cAP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int GrabarAccionAlta(Models.AccionPreventa oAccion, SolicitudAux oSolAux, string guidprovisional)
    {

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();

        try
        {
            Models.SolicitudPreventa oSP = new Models.SolicitudPreventa();

            oSP.ta206_itemorigen = oSolAux.itemorigen;
            
            //Si item origen == SUPER --> el iditemorigen es el ta206_idsolicitudpreventa
            if (oSolAux.itemorigen == "S")
                oSP.ta206_idsolicitudpreventa = oSolAux.iditemorigen;
            else
                oSP.ta206_iditemorigen = oSolAux.iditemorigen;

            if (guidprovisional.Trim().Length == 0) guidprovisional = Guid.NewGuid().ToString();
                
            int ta204_idaccionpreventa =  cAP.Insert(oAccion, oSP, new Guid(guidprovisional));



            //Shared.HistorialNavegacion.Reemplazar(newUrl);

            return ta204_idaccionpreventa;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar la acción preventa", ex);

            if (ex.GetType() == typeof(IB.SUPER.Shared.ValidationException))
                throw new IB.SUPER.Shared.ValidationException(System.Uri.EscapeDataString(ex.Message));
            else
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al grabar la acción preventa."));
        }
        finally
        {
            cAP.Dispose();
        }

    }

     [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.PerfilesEdicion obtenerPerfilesEdicionUsuario(int? t001_idficepi_lider, int ta201_idsubareapreventa)
    {
        

        BLL.Usuario cUsuario = new BLL.Usuario();

        try
        {
            bool soyLider = false;
            if (t001_idficepi_lider != null && t001_idficepi_lider == int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()))
                soyLider = true;

            return cUsuario.obtenerPerfilesEdicionUsuario(HttpContext.Current.User, soyLider, ta201_idsubareapreventa);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener los perfiles de edición del usuario", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al obtener los perfiles de edición del usuario."));
        }
        finally
        {
            cUsuario.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GrabarAccionEdicion(Models.AccionPreventa oAccion)
    {

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();

        try
        {
            cAP.Update(oAccion);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar la acción preventa", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al grabar la acción preventa."));
        }
        finally
        {
            cAP.Dispose();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<int> ObtenerLideresSubarea(int ta201_idsubareapreventa)
    {
        BLL.FiguraSubareaPreventa cFS = new BLL.FiguraSubareaPreventa();

        try
        {
            return cFS.ObtenerLideresSubarea(ta201_idsubareapreventa);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener los posibles líderes del subarea", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al obtener los posibles líderes del subarea."));
        }
        finally
        {
            cFS.Dispose();
        }

    
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int AutoasignarLider(int ta204_idaccionpreventa)
    {

        BLL.AccionPreventa cAP = new BLL.AccionPreventa();

        try
        {
            return cAP.AutoasignarLider(ta204_idaccionpreventa);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al autoasignar lider", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al autoasignar lider."));
        }
        finally
        {
            cAP.Dispose();
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
            Shared.LogError.LogearError("Error al cargar la información de la solicitud", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener la información de cabecera de la solicitud."));
        }
        finally
        {
            c.Dispose();
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.AccionLider> GetLideresSolicitud(int ta206_iditemorigen, string ta206_itemorigen, int? ta204_idaccionpreventa, int ta201_idsubareapreventa)
    { 


        BLL.SolicitudPreventa c = new BLL.SolicitudPreventa();

        try
        {
            return c.GetLideresSolicitud(ta206_iditemorigen, ta206_itemorigen, ta204_idaccionpreventa, ta201_idsubareapreventa);
        }
        catch (Exception ex)
        {
            Shared.LogError.LogearError("Error al obtener la lista de lideres de las otras acciones de la solicitud", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener la lista de líderes de las otras acciones de la solicitud."));
        }
        finally
        {
            c.Dispose();
        }
    }
    
    
    
    private void getCabeceraPreventa(Control idBody)
    {
        body.Controls.Remove(Page.FindControl("Menu"));
        body.Controls.AddAt(0, (this.LoadControl("~/Capa_Presentacion/bsUserControls/cabeceraPreventa.ascx")));
    }

}

    public class SolicitudAux {

        public string itemorigen { get; set; }
        public int iditemorigen { get; set; }
    }


