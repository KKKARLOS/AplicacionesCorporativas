using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_ECO_DialogoAlertas_CatalogoPendientes_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strHTMLTablaUsuario = "", strHTMLTablaGestor = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 9;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Alertas de proyectos bajo mi gestión";
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);

                CargarGrupos();
                CargarAlertas();
                CargarGestores();

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getAlertas"):
                sResultado += ObtenerAlertasGestion(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("goCarrusel"):
                sResultado += goCarrusel(aArgs[1]);
                break;
            case ("alertasGrupo"):
                sResultado += AlertasGrupo(aArgs[1]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }


    private string ObtenerAlertasGestion(
        string sIdProyectosubnodo,
        string sEstadoProy,
        string sIdNodo,
        string sIdCliente,
        string sIdInterlocutor,
        string sHabilitada,
        string sAsunto,
        string sGestor,
        string sStandby,
        string sSeguimiento,
        string sIdResponsable,
        string sGrupo
        )
    {
        try
        {
            byte? idGrupo = null;
            if (sGrupo != "")
                idGrupo = (byte?)byte.Parse(sGrupo);
            return "OK@#@"+ PSNALERTAS.ObtenerAlertasMiGestion(
                                (sIdProyectosubnodo=="0")? null: (int?)int.Parse(sIdProyectosubnodo),
                                (sEstadoProy == "") ? null : sEstadoProy,    
                                (sIdNodo=="0")? null: (int?)int.Parse(sIdNodo),
                                (sIdCliente == "0") ? null : (int?)int.Parse(sIdCliente),
                                (sIdInterlocutor == "0") ? null : (int?)int.Parse(sIdInterlocutor),
                                (sHabilitada == "") ? null : (bool?)((sHabilitada == "1")?true:false),
                                (sAsunto == "-1" || sAsunto=="") ? null : (byte?)byte.Parse(sAsunto), 
                                (sGestor=="-1")? null: (int?)int.Parse(sGestor),
                                (sStandby=="1")? true:false,
                                (sSeguimiento=="1")? true:false,
                                (sIdResponsable == "0") ? null : (int?)int.Parse(sIdResponsable),
                                idGrupo 
                               );
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los diálogos bajo mi gestión.", ex);
        }
    }

    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, "C", false);
            while (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["t301_idproyecto"].ToString() + "##");
                sb.Append(dr["t301_denominacion"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        return sResul;
    }

    protected void CargarAlertas()
    {
        SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerCatalogoAlertas(null, null);
        while (dr.Read())
        {
            cboAsunto.Items.Add(new ListItem(dr["t820_denominacion"].ToString(), dr["t820_idalerta"].ToString()));
        }
        dr.Close();
        dr.Dispose();
    }
    protected void CargarGestores()
    {
        SqlDataReader dr = SUPER.DAL.GESTALERTAS.ObtenerCatalogoGestores(null);
        while (dr.Read())
        {
            cboGestor.Items.Add(new ListItem(dr["Profesional"].ToString(), dr["t314_idusuario"].ToString()));
        }
        dr.Close();
        dr.Dispose();

        if (!SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
        {
            cboGestor.SelectedValue = Session["UsuarioActual"].ToString();
            cboGestor.Enabled = false;
        }
    }
    protected void CargarGrupos()
    {
        SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerCatalogoGrupos(null);
        while (dr.Read())
        {
            cboGrupo.Items.Add(new ListItem(dr["t821_denominacion"].ToString(), dr["t821_idgrupoalerta"].ToString()));
        }
        dr.Close();
        dr.Dispose();
    }

    private string AlertasGrupo(string sIdGrupo)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr;

            if (sIdGrupo == "")
                dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerCatalogoAlertas(null, null);
            else
                dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerCatalogoAlertas(null, byte.Parse(sIdGrupo));
            while (dr.Read())
            {
                sb.Append(dr["t820_idalerta"].ToString() + "##" + dr["t820_denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener las alertas de un grupo", ex);
        }
    }

    private string Grabar(string sDatosAlertas)
    {
        try
        {
            PSNALERTAS.EstablecerAlertaDetalleProyecto(sDatosAlertas);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer las alertas a nivel de proyecto.", ex);
        }
    }
    private string goCarrusel(string sNumProyecto)
    {
        string sResul = "", sAccesoCarrusel = "0"; ;
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, "C", false);
            if (dr.Read())
            {
                Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                Session["RTPT_PROYECTOSUBNODO"] = (dr["rtpt"].ToString() == "1") ? true : false;
                Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                sAccesoCarrusel = "1";
            }
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@" + sAccesoCarrusel;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        return sResul;
    }

}
