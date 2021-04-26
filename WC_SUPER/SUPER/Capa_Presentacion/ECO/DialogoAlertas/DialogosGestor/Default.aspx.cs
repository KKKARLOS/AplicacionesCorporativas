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
                //Master.nBotonera = 58;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Catálogo de diálogos bajo mi gestión";
                Master.FicherosCSS.Add("PopCalendar/css/Classic.css");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);

                CargarGrupos();
                CargarAlertas();
                CargarEstados();
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
            case ("getDialogosGestion"):
                sResultado += ObtenerDialogosGestion(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
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


    private string ObtenerDialogosGestion(
        string sSoloAbiertos,
        string sGrupo,
        string sAsunto,
        string sIdProyectosubnodo,
        string sIdInterlocutor,
        string sEstado,
        string sIdNodo,
        string sIdCliente,
        string sIdDialogo,
        string sFLR,
        string sGestor,
        string sIdResponsable,
        string sMesDesde,
        string sMesHasta,
        string sOrden,
        string sAscDesc
        )
    {
        try
        {
            return "OK@#@"+ DIALOGOALERTAS.ObtenerDialogosMiGestion((int)Session["UsuarioActual"],
                                (sSoloAbiertos=="1")? true:false, //bool bSoloAbiertos,
                                (sGrupo == "") ? null : (byte?)byte.Parse(sGrupo), //Nullable<byte> t821_idgrupoalerta,
                                (sAsunto == "-1" || sAsunto=="") ? null : (byte?)byte.Parse(sAsunto), //Nullable<byte> t820_idalerta,
                                (sIdProyectosubnodo == "0") ? null : (int?)int.Parse(sIdProyectosubnodo), //Nullable<int> t305_idproyectosubnodo,
                                (sIdInterlocutor=="0")? null: (int?)int.Parse(sIdInterlocutor), //Nullable<int> t001_idficepi_interlocutor,
                                (sEstado=="-1")? null: (byte?)byte.Parse(sEstado), //Nullable<byte> t827_idestadodialogoalerta,
                                (sIdNodo=="0")? null: (int?)int.Parse(sIdNodo), //Nullable<int> t303_idnodo,
                                (sIdCliente=="0")? null: (int?)int.Parse(sIdCliente), //Nullable<int> t302_idcliente,
                                (sIdDialogo=="0")? null: (int?)int.Parse(sIdDialogo), //Nullable<int> t831_iddialogoalerta,
                                (sFLR=="")? null: (DateTime?)DateTime.Parse(sFLR), //Nullable<DateTime> t831_flr,
                                (sGestor=="-1")? null: (int?)int.Parse(sGestor), //Nullable<int> t314_idusuario_gestor,
                                (sIdResponsable == "0") ? null : (int?)int.Parse(sIdResponsable), //Nullable<int> t301_idproyecto
                                (sMesDesde=="")? null: (int?)int.Parse(sMesDesde), //null, //Nullable<int> t831_anomesdecierre,
                                (sMesHasta=="")? null: (int?)int.Parse(sMesHasta), //null, //Nullable<int> t831_anomesdecierre,
                                byte.Parse(sOrden),
                                byte.Parse(sAscDesc)

                               );
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los diálogos bajo mi gestión.", ex);
        }
    }
    private string AlertasGrupo(string sIdGrupo)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr;

            if (sIdGrupo=="")
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
    protected void CargarEstados()
    {
        SqlDataReader dr = SUPER.Capa_Datos.ESTADODIALOGOALERTAS.ObtenerCatalogoEstados(null, false);
        while (dr.Read())
        {
            cboEstado.Items.Add(new ListItem(dr["t827_denominacion"].ToString(), dr["t827_idestadodialogoalerta"].ToString()));
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

    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            while (dr.Read())
            {
                if (dr["t305_cualidad"].ToString() != "C") continue;

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
