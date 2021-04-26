using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using EO.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;
//using Microsoft.JScript;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sNodo = "", sEstructuraNodo = "", sModeloCosteNodo = "", sEsGestor = "false", sTipologias = "";
    public string sMSUMCNodo = ""; //Mes siguiente al último mes cerrado del Nodo.
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback && Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }

        try
        {
            if (!Page.IsCallback)
            {
                Master.TituloPagina = "Detalle de proyecto";
                Master.nBotonera = 31;
                Master.bFuncionesLocales = true;

                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/Proyecto/Functions/ddfiguras.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/SegECO/Functions/funcionesUSA.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FicherosCSS.Add("App_Themes/Corporativo/ddfiguras.css");
                Master.bContienePestanas = true;

                try
                {
                    Utilidades.SetEventosFecha(this.txtFIP);
                    Utilidades.SetEventosFecha(this.txtFFP);

                    if (Session["OCULTAR_AUDITORIA"].ToString() == "1")
                        this.btnAuditoria.Visible = false;

                    if (!(bool)Session["ALERTASPROY_ACTIVAS"])
                    {
                        this.fstDialogosAlertas.Style.Add("visibility", "hidden");
                        this.fstDialogosAlertasDEF.Style.Add("visibility", "hidden");
                        this.imgDialogos.Visible = false;
                        this.imgAlertas.Visible = false;
                    }
                    //if (!(bool)Session["FORANEOS"])
                    //{
                    //    this.imgForaneo.Visible = false;
                    //    this.lblForaneo.Visible = false;
                    //    this.imgForaneo2.Visible = false;
                    //    this.lblForaneo2.Visible = false;
                    //}
                    //string sKK = Session["DES_EMPLEADO_ENTRADA"].ToString();
                    sEstructuraNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    lblNodo.Text = sEstructuraNodo;
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    lblSubnodo.Text = Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO);
                    lblSubnodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO));
                    sNodo = lblNodo.Text;

                    this.hdnFigurasForaneos.Value = Session["FIGURASFORANEOS"].ToString();

                    if (Request.QueryString["sOp"].ToString() == "nuevo")
                    {
                        if (Session["APELLIDO2"].ToString() == "")
                            txtResponsable.Text = Session["APELLIDO1"].ToString() + ", " + Session["NOMBRE"].ToString();
                        else
                            txtResponsable.Text = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();

                        hdnRespPSN.Text = Session["UsuarioActual"].ToString();
                        hdnSupervisor.Text = Session["IDFICEPI_PC_ACTUAL"].ToString();
                        txtSupervisor.Text = txtResponsable.Text;
                    }
                    else
                    {

                    }
                    if (Request.QueryString["sIdProySub"] != null)
                    {
                        Session["ID_PROYECTOSUBNODO"] = Request.QueryString["sIdProySub"].ToString();
                    }

                    if (User.IsInRole("A")
                        || User.IsInRole("GSN4")
                        || User.IsInRole("GSN3")
                        || User.IsInRole("GSN2")
                        || User.IsInRole("GSN1")
                        || User.IsInRole("GN")
                        || User.IsInRole("GSB")) sEsGestor = "true";

                    #region Ocultar cualificadores de estructura que no está en uso
                    if (!Utilidades.EstructuraActiva("SN4"))
                    {
                        imgQ4.Style.Add("visibility", "hidden");
                        lblCSN4P.Style.Add("visibility", "hidden");
                        imgCSN4P.Style.Add("visibility", "hidden");
                        txtCSN4P.Style.Add("visibility", "hidden");
                        imgGomaCSN4P.Style.Add("visibility", "hidden");
                        imgGomaCSN4P.Attributes.Add("utilizado", "0");
                    }
                    else imgGomaCSN4P.Attributes.Add("utilizado", "1");

                    if (!Utilidades.EstructuraActiva("SN3"))
                    {
                        imgQ3.Style.Add("visibility", "hidden");
                        lblCSN3P.Style.Add("visibility", "hidden");
                        imgCSN3P.Style.Add("visibility", "hidden");
                        txtCSN3P.Style.Add("visibility", "hidden");
                        imgGomaCSN3P.Style.Add("visibility", "hidden");
                        imgGomaCSN3P.Attributes.Add("utilizado", "0");
                    }
                    else imgGomaCSN3P.Attributes.Add("utilizado", "1");

                    if (!Utilidades.EstructuraActiva("SN2"))
                    {
                        imgQ2.Style.Add("visibility", "hidden");
                        lblCSN2P.Style.Add("visibility", "hidden");
                        imgCSN2P.Style.Add("visibility", "hidden");
                        txtCSN2P.Style.Add("visibility", "hidden");
                        imgGomaCSN2P.Style.Add("visibility", "hidden");
                        imgGomaCSN2P.Attributes.Add("utilizado", "0");
                    }
                    else imgGomaCSN2P.Attributes.Add("utilizado", "1");

                    if (!Utilidades.EstructuraActiva("SN1"))
                    {
                        imgQ1.Style.Add("visibility", "hidden");
                        lblCSN1P.Style.Add("visibility", "hidden");
                        imgCSN1P.Style.Add("visibility", "hidden");
                        txtCSN1P.Style.Add("visibility", "hidden");
                        imgGomaCSN1P.Style.Add("visibility", "hidden");
                        imgGomaCSN1P.Attributes.Add("utilizado", "0");
                    }
                    else imgGomaCSN1P.Attributes.Add("utilizado", "1");
                    #endregion
                }
                catch (Exception ex)
                {
                    Master.sErrores += Errores.mostrarError("Error al obtener las descripciones de la estructura", ex);
                }
                try
                {
                    obtenerTipologias();
                    obtenerModalidadContrato();

                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        txtModificaciones.Rows = 8;
                        txtObservaciones.Rows = 8;
                        TROBSADM.Style.Add("display", "block");
                    }
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al cargar los datos ", ex);
                }

                //12/15 (Lacalle): Se elimina el resp. CVT de esta pantalla (en el caso de resucitar, valdría con eliminar las dos líneas siguientes)
                //lblValidador.Style.Add("visibility", "hidden");
                //txtValidador.Style.Add("visibility", "hidden");


                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
            else
            {

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
            case ("getTarifas"):
                sResultado += obtenerMaestroTarifasPerfil(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14]);
                break;
            case ("grabarAcuerdo"):
                sResultado += GrabarAcuerdo(aArgs[1], aArgs[2]);
                break;
            case ("grabarConfirmacion"):
                sResultado += GrabarConfirmacion(aArgs[1], aArgs[2], true);
                break;
            case ("denegarConfirmacion"):
                sResultado += GrabarConfirmacion(aArgs[1], aArgs[2], false);
                break;
            case ("enviarCorreoCAUDEF"):
                sResultado += enviarCorreoCAUDEF(aArgs[1]);
                break;
            case ("getDatosPestana"):
                //sResultado += "OK@#@" + aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0://General
                        sResultado += obtenerDatosPE(aArgs[1], aArgs[2]);
                        break;
                    //case 1://Perfiles/Tarifas
                    //    sResultado += obtenerTarifasPerfil(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                    //    break;
                    case 2://Profesionales
                        //subpestañas
                        break;
                    case 3://Atributos estadísticos
                        //subpestañas
                        break;
                    case 4://Documentación
                        sResultado += obtenerDocumentos(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                        break;
                    case 5://Control
                        //sResultado += obtenerControl(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                        //subpestañas
                        break;
                    case 6://Anotaciones
                        sResultado += obtenerAnotaciones(aArgs[1], aArgs[2]);
                        break;
                    case 7://Periodificación
                        sResultado += obtenerPeriodificacion(aArgs[1], aArgs[2], aArgs[3]);
                        break;
                }
                break;
            case ("getDatosPestanaProf"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://Asignacion
                        sResultado += obtenerRecursosAsociados(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                        break;
                    case 1://Pool
                        sResultado += obtenerPoolGF(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                        break;
                    case 2://Figuras
                        sResultado += obtenerFigurasPSN(aArgs[1], aArgs[2], aArgs[3]);
                        break;
                }
                break;
            case ("getDatosPestanaCEE"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://Departamental
                        sResultado += obtenerCECDepartamentales(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                        break;
                    case 1://Corporativo
                        sResultado += obtenerCECCorporativos(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                        break;
                }
                break;
            case ("getDatosPestanaControl"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://Genérica
                        sResultado += obtenerControl(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                        break;
                    case 1://Soporte administrativo
                        sResultado += obtenerSoporteAdministrativo(aArgs[1], aArgs[2], aArgs[3]);
                        break;
                }
                break;
            case ("getDatosPestanaPN"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://Perfiles
                        sResultado += obtenerTarifasPerfil(aArgs[1], aArgs[2], aArgs[3]);
                        break;
                    case 1://Niveles
                        sResultado += obtenerNivelesCoste(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                        break;
                }
                break;
            case ("tecnicos"):
                sResultado += obtenerProfesionalesFigura(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), aArgs[4]);
                break;
            case ("documentos"):
                sResultado += obtenerDocumentos("4", aArgs[1], aArgs[3], aArgs[4]);
                break;
            case ("elimdocs"):
                sResultado += eliminarDocumentos(aArgs[1]);
                break;
            case ("setTipologia"):
                sResultado += setTipologia(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("getNodoDefecto"):
                sResultado += obtenerNodoDefecto();
                break;
            case ("getSubnodoDefecto"):
                sResultado += obtenerSubnodoDefecto(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("getProducido"):
                sResultado += obtenerProducido(aArgs[1]);
                break;
            case ("setPerfilesATareas"):
                sResultado += setPerfilesATareas(aArgs[1]);
                break;
            case ("addMesesProy"):
                sResultado += addMesesProy(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("eliminarMesProy"):
                sResultado += eliminarMesProy(aArgs[1]);
                break;
            case ("refrescarInterlocutor"):
                sResultado += refrescarInterlocutor(aArgs[1]);
                break;
            case ("verificarPerfilesBorrar"):
                sResultado += verificarPerfilesBorrar(aArgs[1]);
                break;
            case ("verificarNivelesBorrar"):
                sResultado += verificarNivelesBorrar(aArgs[1]);
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

    private void obtenerTipologias()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("var js_tip = new Array();\n");

        SqlDataReader dr = TIPOLOGIAPROY.Catalogo(null, "", null, null, null, null, null, 7, 0);
        ListItem oItem;
        int i = 0;
        while (dr.Read())
        {
            oItem = new ListItem(dr["t320_denominacion"].ToString(), dr["t320_idtipologiaproy"].ToString());
            oItem.Attributes.Add("interno", ((bool)dr["t320_interno"]) ? "1" : "0");
            oItem.Attributes.Add("requierecontrato", ((bool)dr["t320_requierecontrato"]) ? "1" : "0");
            cboTipologia.Items.Add(oItem);
            sb.Append("\tjs_tip[" + i + "] = {\"id\":" + dr["t320_idtipologiaproy"].ToString() + ",\"denominacion\":\"" + dr["t320_denominacion"].ToString() + "\",\"interno\":\"");
            if ((bool)dr["t320_interno"]) sb.Append("1");
            else sb.Append("0");
            sb.Append("\",\"requierecontrato\":\"");
            if ((bool)dr["t320_requierecontrato"]) sb.Append("1");
            else sb.Append("0");
            sb.Append("\"};\n");
            i++;
        }
        dr.Close();
        dr.Dispose();
        sTipologias = sb.ToString();
    }
    private void obtenerModalidadContrato()
    {
        //cboModContratacion.DataTextField = "t316_denominacion";
        //cboModContratacion.DataValueField = "t316_idmodalidad";
        //cboModContratacion.DataSource = MODALIDADCONTRATO.Catalogo(null, "", false, 2, 0);
        //cboModContratacion.DataBind();
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = MODALIDADCONTRATO.Catalogo(null, "", false, 2, 0);
        while (dr.Read())
        {
            sb.Append(dr["t316_idmodalidad"].ToString());
            sb.Append("@#@");
            sb.Append(dr["t316_denominacion"].ToString());
            sb.Append("///");
        }
        dr.Close();
        dr.Dispose();
        this.hdnModContrato.Value = sb.ToString();
    }
    private string obtenerNodoDefecto()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            int i = 0;
            SqlDataReader dr = NODO.ObtenerNodosGestor((int)Session["UsuarioActual"]);

            while (dr.Read())
            {
                i++;
                if (i > 1)
                    break;
                sb.Append(dr["idnodo"].ToString() + "##");
                sb.Append(dr["denominacion"].ToString() + "##");
                sb.Append(dr["GSB"].ToString() + "##");
                sb.Append(dr["t303_ultcierreeco"].ToString() + "##");
                sb.Append(dr["t303_modelocostes"].ToString() + "##");
                sb.Append(dr["t303_modelotarifas"].ToString() + "##");
                sb.Append(dr["t303_denominacion_CNP"].ToString() + "##");
                sb.Append(dr["t303_obligatorio_CNP"].ToString() + "##");
                sb.Append(dr["t391_denominacion_CSN1P"].ToString() + "##");
                sb.Append(dr["t391_obligatorio_CSN1P"].ToString() + "##");
                sb.Append(dr["t392_denominacion_CSN2P"].ToString() + "##");
                sb.Append(dr["t392_obligatorio_CSN2P"].ToString() + "##");
                sb.Append(dr["t393_denominacion_CSN3P"].ToString() + "##");
                sb.Append(dr["t393_obligatorio_CSN3P"].ToString() + "##");
                sb.Append(dr["t394_denominacion_CSN4P"].ToString() + "##");
                sb.Append(dr["t394_obligatorio_CSN4P"].ToString() + "##");
                sb.Append(dr["t303_tipolinterna"].ToString() + "##");
                sb.Append(dr["t303_tipolespecial"].ToString() + "##");
                sb.Append(dr["t303_tipolproductivaSC"].ToString() + "##");
                sb.Append(dr["t422_idmoneda"].ToString() + "##");
                sb.Append(dr["t422_denominacion"].ToString() + "##");
                sb.Append(dr["t303_pgrcg"].ToString());
            }
            dr.Close();
            dr.Dispose();

            if (i != 1) sb.Length = 0;

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " por defecto.", ex);
        }

    }
    private string obtenerSubnodoDefecto(int nNodo, int GSN)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = SUBNODO.CatalogoFigura(null, nNodo, (int)Session["UsuarioActual"], GSN);

            int i = 0;
            while (dr.Read())
            {
                sResul = dr["t304_idsubnodo"].ToString() + "##" + dr["t304_denominacion"].ToString();
                i++;
            }

            if (i != 1) sResul = "";

            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el " + Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) + " por defecto.", ex);
        }
    }

    private string Grabar(string strDatosBasicos, string strGeneral, string strPerfilTarifas, string strNivelesCoste, string strProfesionales,
                          string strPoolGF, string strFiguras, string strCEEDep, string strCEECor, string strControl, string strPedidos, 
                          string strSoporteAdm, string strAnotaciones, string strPeriodificacion)
    {
        string sResul = "", sTarifasInsertadas = "", sNivelesInsertados = "", sPedidosInsertadosI = "", sPedidosInsertadosC = "", sCualidad="";
        string sEstadoMes = "", sDenPE = "", sExternalizable = "0", sVisadorCV = "";
        bool bErrorControlado = false, bPermitirPST = false;
        int nPE = -1, nPSN = -1, nAux, nIdAcuerdo = -1, nVisadorCV=-1;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] aDatosBasicos = Regex.Split(strDatosBasicos, "##");
            ///aDatosBasicos[0] = Num. PE
            ///aDatosBasicos[1] = Des. PE
            ///aDatosBasicos[2] = Estado PE
            ///aDatosBasicos[3] = hdnIdProyectoSubNodo
            ///aDatosBasicos[4] = Responsable ProyectoSubNodo
            ///aDatosBasicos[5] = Horizontal
            ///aDatosBasicos[6] = idNodo
            ///aDatosBasicos[7] = idPlantilla
            ///aDatosBasicos[8] = hdnCualidad
            if (aDatosBasicos[0] != "") nPE = int.Parse(aDatosBasicos[0]);
            if (aDatosBasicos[3] != "") nPSN = int.Parse(aDatosBasicos[3]);
            sDenPE = Utilidades.unescape(aDatosBasicos[1]);
            sCualidad = aDatosBasicos[8];

            #region Datos Profesionales DELETES
            if (strProfesionales != "")//No se ha modificado nada de la pestaña de Profesionales
            {
                string[] aProfesionales = Regex.Split(strProfesionales, "///");
                foreach (string oProf in aProfesionales)
                {
                    if (oProf == "") continue;
                    string[] aProf = Regex.Split(oProf, "##");
                    ///aProf[0] = bd
                    ///aProf[1] = idUsuario
                    ///aProf[2] = Coste para la contratante
                    ///aProf[3] = Tipo
                    ///aProf[4] = Deriva
                    ///aProf[5] = FAlta
                    ///aProf[6] = FBaja
                    ///aProf[7] = Tarifa
                    ///aProf[8] = Nombre Empleado
                    ///aProf[9] = Coste para la replicada                    

                    switch (aProf[0])
                    {
                        case "D":
                            //Comprobar antes de eliminar que el profesional no tenga imputaciones en meses abiertos.
                            if (USUARIO.bTieneConsumosEnMesesAbiertos(tr, int.Parse(aProf[1]), nPSN))
                            {
                                bErrorControlado = true;
                                throw (new Exception("No se puede desasignar a " + aProf[8] + " del proyecto, ya que tiene consumos imputados en meses abiertos."));
                            }
                            nAux = USUARIOPROYECTOSUBNODO.Delete(tr, nPSN, int.Parse(aProf[1]));
                            nAux = TareaRecurso.DeleteDeTareasSinConsumos(tr, int.Parse(aProf[1]), nPSN);
                            break;
                    }
                }
            }
            #endregion

            #region Datos Generales
            if (strGeneral != "")//No se ha modificado nada de la pestaña general
            {
                string[] aGeneral = Regex.Split(strGeneral, "##");
                ///aGeneral[0] = cboNodo
                ///aGeneral[1] = cboSubnodo
                ///aGeneral[2] = txtIDContrato
                ///aGeneral[3] = txtImpContrato (se pasa en blanco ya que no se utiliza)
                ///aGeneral[4] = txtIDCliente
                ///aGeneral[5] = chkPAP
                ///aGeneral[6] = cboTipologia
                ///aGeneral[7] = cboModContratacion
                ///aGeneral[8] = hdnIdNaturaleza
                ///aGeneral[9] = cboTarificacion
                ///aGeneral[10] = cboCategoria
                ///aGeneral[11] = txtFIP
                ///aGeneral[12] = txtFFP
                ///aGeneral[13] = txtDescripcion
                ///aGeneral[14] = modelo coste
                ///aGeneral[15] = chkFinalizado
                ///aGeneral[16] = hdnCualidad
                ///aGeneral[17] = chkHeredaNodo
                ///aGeneral[18] = txtSeudonimo
                ///aGeneral[19] = cboBitacoraIAP
                ///aGeneral[20] = cboBitacoraPST
                ///aGeneral[21] = rdbGasvi
                ///aGeneral[22] = chkPermitirPST
                ///aGeneral[23] = chkAvisoRespPST
                ///aGeneral[24] = chkAvisoProfPST
                ///aGeneral[25] = chkAvisoFigura
                ///aGeneral[26] = hdnannoPIG
                ///aGeneral[27] = hdnCNP
                ///aGeneral[28] = hdnCSN1P
                ///aGeneral[29] = 1 / 0. Cambio modelo de tarificación.
                ///aGeneral[30] = 1 / 0. Cambio modelo de coste.
                ///aGeneral[31] = P(0) / S(1) Circuito de aprobación GASVI
                ///aGeneral[32] = IDFICEPI visador
                ///aGeneral[33] = hdnCSN1P
                ///aGeneral[34] = hdnCSN1P
                ///aGeneral[35] = hdnCSN1P
                ///aGeneral[36] = chkPGRCG
                ///aGeneral[37] = cboImportarGasvi
                ///aGeneral[38] = chkEsReplicable
                ///aGeneral[39] = hdnModContraIni
                ///aGeneral[40] = hdnIdMoneda
                ///aGeneral[41] = chkOPD
                ///aGeneral[42] = hdnVisadorCV
                ///aGeneral[43] = hdnInterlocutor
                ///aGeneral[44] = txtPreviMeses
                ///aGeneral[45] = chkGaranActi
                ///aGeneral[46] = txtFIGar
                ///aGeneral[47] = txtFFGar
                ///aGeneral[48] = hdnInterlocutorDEF
                ///aGeneral[49] = hdnIdNLO

                if (nPE == 0) //insert
                {
                    #region INSERT
                    nPE = PROYECTO.Insert(tr,
                                    aDatosBasicos[2],//estado
                                    sDenPE,//denominacion
                                    Utilidades.unescape(aGeneral[13]), //descripcion
                                    int.Parse(aGeneral[4]), //id cliente proyecto
                                    (aGeneral[2] != "") ? (int?)int.Parse(aGeneral[2]) : null,//idcontrato
                                    (aDatosBasicos[5] != "") ? (int?)int.Parse(aDatosBasicos[5]) : null, // id horizontal
                                    int.Parse(aGeneral[8]), //id naturaleza
                                    (aGeneral[7] != "") ? (byte?)byte.Parse(aGeneral[7]) : null,//idmodalidad
                                    DateTime.Parse(aGeneral[11]), //fiprev
                                    DateTime.Parse(aGeneral[12]),//ffprev
                                    aGeneral[10],//categoria
                                    aGeneral[14], //modelo coste
                                    aGeneral[9],//modelo tarifa
                                    null,
                                    (aGeneral[5] == "1") ? true : false,
                                    (aGeneral[36] == "1") ? true : false,
                                    (aGeneral[38] == "1") ? true : false,

                                    (aGeneral[44] != "") ? (short?) short.Parse(aGeneral[44]) : null,//txtPreviMeses
                                    (aGeneral[45] == "1") ? true : false, //chkGaranActi
                                    (aGeneral[46] != "") ? (DateTime?) DateTime.Parse(aGeneral[46]) : null, //txtFIGar
                                    (aGeneral[47] != "") ? (DateTime?) DateTime.Parse(aGeneral[47]) : null, //txtFFGar
                                    (aGeneral[49] != "") ? (int?)int.Parse(aGeneral[49]) : null//id Nueva Línea de Oferta
                                    );

                    nPSN = PROYECTOSUBNODO.InsertGeneral(tr,
                        nPE,
                        int.Parse(aGeneral[1]),
                        (aGeneral[15] == "1") ? true : false,
                        aGeneral[16],
                        (aGeneral[17] == "1") ? true : false,
                        int.Parse(aDatosBasicos[4]),
                        Utilidades.unescape(aGeneral[18]),
                        aGeneral[19], aGeneral[20],
                        (aGeneral[21] == "1") ? true : false,
                        (aGeneral[22] == "1") ? true : false,
                        (aGeneral[23] == "1") ? true : false,
                        (aGeneral[24] == "1") ? true : false,
                        (aGeneral[25] == "1") ? true : false,
                        (aGeneral[32] != "") ? (int?)int.Parse(aGeneral[32]) : null,
                        (aGeneral[31] == "S") ? true : false,
                        (aGeneral[27] != "") ? (int?)int.Parse(aGeneral[27]) : null, //hdnCNP
                        (aGeneral[28] != "") ? (int?)int.Parse(aGeneral[28]) : null, //hdnCSN1P
                        (aGeneral[33] != "") ? (int?)int.Parse(aGeneral[33]) : null, //hdnCSN2P
                        (aGeneral[34] != "") ? (int?)int.Parse(aGeneral[34]) : null, //hdnCSN3P
                        (aGeneral[35] != "") ? (int?)int.Parse(aGeneral[35]) : null, //hdnCSN4P
                        byte.Parse(aGeneral[37]),
                        aGeneral[40],
                        (aGeneral[41] == "1") ? true : false,
                        (aGeneral[42] != "") ? (int?)int.Parse(aGeneral[42]) : null, //visador de CV
                        (aGeneral[43] != "") ? (int?)int.Parse(aGeneral[43]) : null  //Interlocutor
                        );

                    Session["ID_PROYECTOSUBNODO"] = nPSN;
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
                    Session["MONEDA_PROYECTOSUBNODO"] = aGeneral[40];

                    if (aDatosBasicos[4] == Session["UsuarioActual"].ToString())
                    {//Si el usuario ha designado otro responsable al proyecto NO se asigna el rol
                        if (!User.IsInRole("RP"))
                            Roles.AddUserToRole(User.Identity.Name.ToString(), "RP");
                    }

                    PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(tr, nPSN);
                    bPermitirPST = oPSN.t305_admiterecursospst;
                    #endregion
                }
                else //update
                {
                    #region Update
                    bPermitirPST = (aGeneral[22] == "1") ? true : false;

                    //Antes de grabar si el estado es cerrado compruebo que no tiene meses abiertos en instancias contratantes o replicadas con gestión 
                    if (aDatosBasicos[2] == "C")
                    {
                        if (PROYECTO.HayMesesAbiertos(tr, nPE))
                        {
                            bErrorControlado = true;
                            throw (new Exception("Intento de cerrar un proyecto que tiene meses abiertos en instancias contratantes o replicadas con gestión."));
                        }
                        //Si el estado que se va a grabar es CERRADO y antes no lo estaba se actualiza el ETPR y FFPR de todas las tareas que no estén cerradas, finalizadas o anuladas
                        string sEstadoAnterior = PROYECTO.getEstado(tr, nPE);
                        if (sEstadoAnterior != "C")
                        {
                            PROYECTO.CierreTecnico(tr, nPE);
                            //Si hay consumos IAP y no existe mes -> crear mes, traspasar IAP y cerrar mes
                            PROYECTO.TraspasarIAP(tr, nPE, false);
                        }
                    }
                    if (aGeneral[29] == "1" || aGeneral[30] == "1")//se ha modificado el modelo de tarificación o se ha modificado el modelo de coste
                    {
                        PROYECTOSUBNODO oPSNModelo = PROYECTOSUBNODO.Obtener(tr, nPSN);
                        if (oPSNModelo.mesesCerrados > 0)
                        {
                            bErrorControlado = true;
                            if (aGeneral[29] == "1")
                                throw (new Exception("No se permite modificar el modelo de tarificación debido a que existen meses cerrados para el proyecto."));
                            else
                                throw (new Exception("No se permite modificar el modelo de coste debido a que existen meses cerrados para el proyecto."));
                        }
                    }

                    nAux = PROYECTO.Update(tr, nPE, aDatosBasicos[2],
                        sDenPE,
                        Utilidades.unescape(aGeneral[13]), int.Parse(aGeneral[4]),
                        (aGeneral[2] != "") ? (int?)int.Parse(aGeneral[2]) : null,
                        (aDatosBasicos[5] != "") ? (int?)int.Parse(aDatosBasicos[5]) : null, // id horizontal
                        int.Parse(aGeneral[8]),
                        (aGeneral[7] != "") ? (byte?)byte.Parse(aGeneral[7]) : null,
                        DateTime.Parse(aGeneral[11]),
                        DateTime.Parse(aGeneral[12]),
                        aGeneral[10],
                        aGeneral[14],
                        aGeneral[9],
                        (aGeneral[26] != "") ? (short?)short.Parse(aGeneral[26]) : null,
                        (aGeneral[5] == "1") ? true : false,
                        (aGeneral[36] == "1") ? true : false,
                        (aGeneral[38] == "1") ? true : false,

                        (aGeneral[44] != "") ? (short?)short.Parse(aGeneral[44]) : null,//txtPreviMeses
                        (aGeneral[45] == "1") ? true : false, //chkGaranActi
                        (aGeneral[46] != "") ? (DateTime?)DateTime.Parse(aGeneral[46]) : null, //txtFIGar
                        (aGeneral[47] != "") ? (DateTime?)DateTime.Parse(aGeneral[47]) : null, //txtFFGar
                        (aGeneral[49] != "") ? (int?)int.Parse(aGeneral[49]) : null//id Nueva Línea de Oferta
                        );

                    nAux = PROYECTOSUBNODO.UpdateGeneral(tr, nPSN, nPE,
                                                    int.Parse(aGeneral[1]),
                                                    (aGeneral[15] == "1") ? true : false,
                                                    aGeneral[16],
                                                    (aGeneral[17] == "1") ? true : false,
                                                    int.Parse(aDatosBasicos[4]),
                                                    Utilidades.unescape(aGeneral[18]),
                                                    aGeneral[19], aGeneral[20],
                                                    (aGeneral[21] == "1") ? true : false,
                                                    (aGeneral[22] == "1") ? true : false,
                                                    (aGeneral[23] == "1") ? true : false,
                                                    (aGeneral[24] == "1") ? true : false,
                                                    (aGeneral[25] == "1") ? true : false,
                                                    (aGeneral[32] != "") ? (int?)int.Parse(aGeneral[32]) : null,
                                                    (aGeneral[31] == "S") ? true : false,
                                                    (aGeneral[27] != "") ? (int?)int.Parse(aGeneral[27]) : null, //hdnCNP
                                                    (aGeneral[28] != "") ? (int?)int.Parse(aGeneral[28]) : null, //hdnCSN1P
                                                    (aGeneral[33] != "") ? (int?)int.Parse(aGeneral[33]) : null, //hdnCSN2P
                                                    (aGeneral[34] != "") ? (int?)int.Parse(aGeneral[34]) : null, //hdnCSN3P
                                                    (aGeneral[35] != "") ? (int?)int.Parse(aGeneral[35]) : null, //hdnCSN4P
                                                    byte.Parse(aGeneral[37]),
                                                    aGeneral[40],
                                                    (aGeneral[41] == "1") ? true : false,
                                                    (aGeneral[42] != "") ? (int?)int.Parse(aGeneral[42]) : null, //visador de CV
                                                    (aGeneral[43] != "") ? (int?)int.Parse(aGeneral[43]) : null,  //Interlocutor
                                                    (aGeneral[48] != "") ? (int?)int.Parse(aGeneral[48]) : null  //Interlocutor OC y FA DEF
                                                    );
                    #endregion
                }
            }

            #endregion

            #region Datos Profesionales INSERT y UPDATES
            if (strProfesionales != "")//No se ha modificado nada de la pestaña de Profesionales
            {
                string[] aProfesionales = Regex.Split(strProfesionales, "///");
                foreach (string oProf in aProfesionales)
                {
                    if (oProf == "") continue;
                    string[] aProf = Regex.Split(oProf, "##");
                    ///aProf[0] = bd
                    ///aProf[1] = idUsuario
                    ///aProf[2] = Coste para la contratante
                    ///aProf[3] = Tipo
                    ///aProf[4] = Deriva
                    ///aProf[5] = FAlta
                    ///aProf[6] = FBaja
                    ///aProf[7] = Tarifa
                    ///aProf[8] = Nombre Empleado
                    ///aProf[9] = Coste para la replicada                    
                    ///aProf[10] = FBaja inicial (para controlar si hay cambios en la FBaja)                    
                    ///aProf[11] = nodo del usuario

                    DateTime? fBaja = null;
                    DateTime? fBajaInicial = null;
                    //                    int? nTarifa = null;
                    int nTarifa = -1;
                    bool bDeriva = false;
                    decimal? nCosteCon = null;
                    if (aProf[6] != "") fBaja = DateTime.Parse(aProf[6]);
                    if (aProf[10] != "") fBajaInicial = DateTime.Parse(aProf[10]);
                    if (aProf[7] != "") nTarifa = int.Parse(aProf[7]);
                    if (aProf[4] == "1") bDeriva = true;
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) nCosteCon = (aProf[2] == "") ? 0 : decimal.Parse(aProf[2]);

                    switch (aProf[0])
                    {
                        case "I":
                            if (fBaja != null)
                            {
                                string sFechaAux = USUARIO.ObtenerFecUltImputacProy(tr, int.Parse(aProf[1]), int.Parse(aDatosBasicos[3]));
                                DateTime dtAux = DateTime.Parse(sFechaAux);
                                if (fBaja < dtAux)
                                {
                                    bErrorControlado = true;
                                    string sMsg = "Grabación denegada.\n\nNo es correcta la fecha de baja ";
                                    sMsg += "para el profesional '" + aProf[1] + " " + aProf[8] + "'.\n\n";
                                    sMsg += "La fecha de baja debe ser posterior a la última imputación del profesional.";
                                    sMsg += "Y en el caso de ser un empleado interno también ha de ser superior a la última fecha del ";
                                    sMsg += "cierre de IAP del " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " al que pertenece.\n\n";
                                    sMsg += "En el caso de este profesional la fecha de baja debería ser posterior al " + sFechaAux + "\n\n";
                                    throw (new Exception(sMsg));
                                }
                            }
                            if (sCualidad == "C")
                            {
                                if (aDatosBasicos[6] != aProf[11] && aProf[11] != "")
                                if (USUARIO.ExistenReplicaConGestionEnCR(tr, nPE, int.Parse(aProf[11])))
                                {
                                    bErrorControlado = true;
                                    string sMsg = "No se puede asignar el profesional '" + aProf[1] + " " + aProf[8] + "'.\n\n";
                                    sMsg += "Ya existe una réplica con gestión para el C.R. del usuario\n\n";
                                    throw (new Exception(sMsg));
                                }
                            }
                            USUARIOPROYECTOSUBNODO.Insert(tr, nPSN, int.Parse(aProf[1]), (aProf[2] == "") ? 0 : decimal.Parse(aProf[2]),
                                                          (aProf[9] == "") ? 0 : decimal.Parse(aProf[9]),
                                                          bDeriva, DateTime.Parse(aProf[5]), fBaja, nTarifa);

                            #region OLD
                            //try
                            //{
                            //    USUARIOPROYECTOSUBNODO.Insert(tr, nPSN, int.Parse(aProf[1]), (aProf[2] == "") ? 0 : decimal.Parse(aProf[2]),
                            //                                  (aProf[9] == "") ? 0 : decimal.Parse(aProf[9]),
                            //                                  bDeriva, DateTime.Parse(aProf[5]), fBaja, nTarifa);
                            //}
                            //catch(Exception e1)
                            //{
                            //    if (e1.Message.IndexOf("No se puede asociar el usuario")>=0)
                            //    {
                            //        bErrorControlado = true;
                            //        string sMsg = "Grabación denegada.\n\nNo se puede asignar el profesional '" + aProf[1] + " " + aProf[8] + "'.\n\n";
                            //        sMsg += "Revise si existe réplica con gestión para el C.R. del usuario o el proyecto es replicable\n\n";
                            //        throw (new Exception(sMsg));
                            //    }
                            //    else
                            //    {
                            //        throw (new Exception(e1.Message));
                            //    }
                            //}
                            #endregion
                            break;
                        case "U":
                            //Si se le ha puesto fecha de baja comprobar que no hay imputaciones en días posteriores
                            //y que la fecha de baja sea mayor que la última fecha de cierre de nodo
                            //Siempre que haya cambio de fecha de baja
                            if (fBaja != null && fBajaInicial != fBaja)
                            {
                                string sFechaAux = USUARIO.ObtenerFecUltImputacProy(tr, int.Parse(aProf[1]), int.Parse(aDatosBasicos[3]));
                                DateTime dtAux = DateTime.Parse(sFechaAux);
                                if (fBaja < dtAux)
                                {
                                    bErrorControlado = true;
                                    string sMsg = "Grabación denegada.\n\nNo es correcta la fecha de baja ";
                                    sMsg += "para el profesional '" + aProf[1] + " " + aProf[8] + "'.\n\n";
                                    sMsg += "La fecha de baja debe ser posterior a la última imputación del profesional.";
                                    sMsg += "Y en el caso de ser un empleado interno también ha de ser superior a la última fecha del ";
                                    sMsg += "cierre de IAP del " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " al que pertenece.\n\n";
                                    sMsg += "En el caso de este profesional la fecha de baja debería ser posterior al " + sFechaAux + "\n\n";
                                    throw (new Exception(sMsg));
                                }
                            }
                            nAux = USUARIOPROYECTOSUBNODO.Update(tr, nPSN, int.Parse(aProf[1]), nCosteCon, null,
                                                                 bDeriva, DateTime.Parse(aProf[5]), fBaja, nTarifa);
                            break;
                        //case "D":
                        //    //Comprobar antes de eliminar que el profesional no tenga imputaciones en meses abiertos.
                        //    if (USUARIO.bTieneConsumosEnMesesAbiertos(tr, int.Parse(aProf[1]), nPSN))
                        //    {
                        //        bErrorControlado = true;
                        //        throw (new Exception("No se puede desasignar a " + aProf[8] +" del proyecto, ya que tiene consumos imputados en meses abiertos."));
                        //    }
                        //    nAux = USUARIOPROYECTOSUBNODO.Delete(tr, nPSN, int.Parse(aProf[1]));
                        //    nAux = TareaRecurso.DeleteDeTareasSinConsumos(tr, int.Parse(aProf[1]), nPSN);
                        //    break;
                    }
                }
            }
            #endregion

            #region Datos PoolGF
            if (strPoolGF != "")//No se ha modificado nada de la pestaña de Pools
            {
                string[] aPool = Regex.Split(strPoolGF, "///");
                foreach (string oPool in aPool)
                {
                    if (oPool == "") continue;
                    string[] aDatosPoolGF = Regex.Split(oPool, "##");
                    ///aDatosPoolGF[0] = bd
                    ///aDatosPoolGF[1] = idGF
                    if (aDatosPoolGF[0] == "I") POOL_GF_PSN.InsertarSiNoExiste(tr, nPSN, int.Parse(aDatosPoolGF[1]));
                    else POOL_GF_PSN.Delete(tr, nPSN, int.Parse(aDatosPoolGF[1]));
                }
            }

            #endregion

            #region Datos Figuras
            if (strFiguras != "")//No se ha modificado nada de la pestaña de Figuras
            {
                string[] aUsuarios = Regex.Split(strFiguras, "///");
                foreach (string oUsuario in aUsuarios)
                {
                    if (oUsuario == "") continue;
                    string[] aFig = Regex.Split(oUsuario, "##");
                    ///aFig[0] = bd
                    ///aFig[1] = idUsuario
                    ///aFig[2] = Figuras

                    //FIGURAPROYECTOSUBNODO.Delete(tr, nPSN, int.Parse(aFig[1]));

                    //if (aFig[0] != "D")
                    //{
                    //    string[] aFiguras = Regex.Split(aFig[2], ",");
                    //    foreach (string oFigura in aFiguras)
                    //    {
                    //        if (oFigura == "") continue;
                    //        FIGURAPROYECTOSUBNODO.Insert(tr, nPSN, int.Parse(aFig[1]), oFigura);
                    //    }
                    //}
                    if (aFig[0] == "D")
                        FIGURAPROYECTOSUBNODO.DeleteUsuario(tr, nPSN, int.Parse(aFig[1]));
                    else
                    {
                        string[] aFiguras = Regex.Split(aFig[2], ",");
                        foreach (string oFigura in aFiguras)
                        {
                            if (oFigura == "") continue;
                            string[] aFig2 = Regex.Split(oFigura, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            if (aFig2[0] == "D")
                                FIGURAPROYECTOSUBNODO.Delete(tr, nPSN, int.Parse(aFig[1]), aFig2[1]);
                            else
                                FIGURAPROYECTOSUBNODO.Insert(tr, nPSN, int.Parse(aFig[1]), aFig2[1]);
                        }
                    }
                }
            }

            #endregion

            #region Perfil/Tarifas
            if (strPerfilTarifas != "")//No se ha modificado nada de la pestaña de Perfil/Tarifas
            {
                string[] aPerfilTarifas = Regex.Split(strPerfilTarifas, "///");
                foreach (string oPerfil in aPerfilTarifas)
                {
                    if (oPerfil == "") continue;
                    string[] aPerfil = Regex.Split(oPerfil, "##");
                    ///aPerfil[0] = bd
                    ///aPerfil[1] = idPerfil
                    ///aPerfil[2] = Descripcion
                    ///aPerfil[3] = Tarifa
                    ///aPerfil[4] = Orden
                    ///aPerfil[5] = Activo

                    switch (aPerfil[0])
                    {
                        case "I":
                            nAux = PERFILPROY.Insert(tr, Utilidades.unescape(aPerfil[2]), decimal.Parse(aPerfil[3]), nPE, short.Parse(aPerfil[4]), (aPerfil[5] == "1") ? true : false);
                            if (sTarifasInsertadas == "") sTarifasInsertadas = nAux.ToString();
                            else sTarifasInsertadas += "//" + nAux.ToString();
                            break;
                        case "U":
                            PERFILPROY.Update(tr, int.Parse(aPerfil[1]), Utilidades.unescape(aPerfil[2]), decimal.Parse(aPerfil[3]), nPE, short.Parse(aPerfil[4]), (aPerfil[5] == "1") ? true : false);
                            break;
                        case "D":
                            PERFILPROY.Delete(tr, int.Parse(aPerfil[1]));
                            break;
                    }
                }
            }

            #endregion

            #region Niveles/Coste
            if (strNivelesCoste != "")//No se ha modificado nada de la pestaña de Niveles/Coste
            {
                string[] aNivelesCoste = Regex.Split(strNivelesCoste, "///");
                foreach (string oNivel in aNivelesCoste)
                {
                    if (oNivel == "") continue;
                    string[] aNivel = Regex.Split(oNivel, "##");
                    ///aNivel[0] = bd
                    ///aNivel[1] = idPerfil
                    ///aNivel[2] = Descripcion
                    ///aNivel[3] = Coste
                    ///aNivel[4] = Orden
                    ///aNivel[5] = Activo

                    switch (aNivel[0])
                    {
                        case "I":
                            nAux = NIVELPSN.Insert(tr, Utilidades.unescape(aNivel[2]), nPSN, decimal.Parse(aNivel[3]), (aNivel[5] == "1") ? true : false, byte.Parse(aNivel[4]));
                            if (sNivelesInsertados == "") sNivelesInsertados = nAux.ToString();
                            else sNivelesInsertados += "//" + nAux.ToString();
                            break;
                        case "U":
                            NIVELPSN.Update(tr, int.Parse(aNivel[1]), Utilidades.unescape(aNivel[2]), nPSN, decimal.Parse(aNivel[3]), (aNivel[5] == "1") ? true : false, byte.Parse(aNivel[4]));
                            break;
                        case "D":
                            NIVELPSN.Delete(tr, int.Parse(aNivel[1]));
                            break;
                    }
                }
            }

            #endregion

            #region Datos CEE Departamentales
            if (strCEEDep != "")
            {
                string[] aCEEDep = Regex.Split(strCEEDep, "///");

                foreach (string oCEEDep in aCEEDep)
                {
                    if (oCEEDep == "") continue;
                    string[] aValores = Regex.Split(oCEEDep, "##");
                    ///aValores[0] = opcionBD;
                    ///aValores[1] = idAE;
                    ///aValores[2] = idVAE;

                    switch (aValores[0])
                    {
                        case "I":
                            AEPROYECTOSUBNODO.Insert(tr, nPSN, int.Parse(aValores[2]));
                            break;
                        case "U":
                            AEPROYECTOSUBNODO.Update(tr, nPSN, int.Parse(aValores[1]), int.Parse(aValores[2]));
                            break;
                        case "D":
                            AEPROYECTOSUBNODO.Delete(tr, nPSN, int.Parse(aValores[2]));
                            break;
                    }
                }
            }
            #endregion

            #region Datos CEE Corporativos
            if (strCEECor != "")
            {
                string[] aCEECor = Regex.Split(strCEECor, "///");

                foreach (string oCEECor in aCEECor)
                {
                    if (oCEECor == "") continue;
                    string[] aValores = Regex.Split(oCEECor, "##");
                    ///aValores[0] = opcionBD;
                    ///aValores[1] = idCEE;
                    ///aValores[2] = idVCEE;

                    switch (aValores[0])
                    {
                        case "I":
                            CECPROYECTO.Insert(tr, nPE, int.Parse(aValores[2]));
                            break;
                        case "U":
                            CECPROYECTO.Update(tr, nPE, int.Parse(aValores[1]), int.Parse(aValores[2]));
                            break;
                        case "D":
                            CECPROYECTO.Delete(tr, nPE, int.Parse(aValores[2]));
                            break;
                    }
                }
            }
            #endregion

            #region Control
            #region Cualificador
            string[] aControl = Regex.Split(strControl, "##");
            if (aControl[0] != "")
            {
                PROYECTO.UpdateCualificador(tr, nPE, int.Parse(aControl[0]));
            }
            #endregion
            #region Pedidos
            if (strPedidos != "")//No se ha modificado nada de la pestaña de Control
            {
                string[] aPedidos = Regex.Split(strPedidos, "///");
                foreach (string oPedido in aPedidos)
                {
                    if (oPedido == "") continue;
                    string[] aPedido = Regex.Split(oPedido, "##");
                    ///aPedido[0] = bd
                    ///aPedido[1] = tipo
                    ///aPedido[2] = nPE
                    ///aPedido[3] = id
                    ///aPedido[4] = Codigo
                    ///aPedido[5] = Fecha
                    ///aPedido[6] = Comentario

                    switch (aPedido[0])
                    {
                        case "I":
                            nAux = PEDIDOPROYECTO.Insert(tr, int.Parse(aPedido[2]), aPedido[1], Utilidades.unescape(aPedido[4]), (aPedido[5] == "") ? null : ((DateTime?)DateTime.Parse(aPedido[5])), Utilidades.unescape(aPedido[6]));
                            if (aPedido[1] == "I")
                            {
                                if (sPedidosInsertadosI == "") sPedidosInsertadosI = nAux.ToString();
                                else sPedidosInsertadosI += "//" + nAux.ToString();
                            }
                            else
                            {
                                if (sPedidosInsertadosC == "") sPedidosInsertadosC = nAux.ToString();
                                else sPedidosInsertadosC += "//" + nAux.ToString();
                            }
                            break;
                        case "U":
                            PEDIDOPROYECTO.Update(tr, int.Parse(aPedido[3]), int.Parse(aPedido[2]), aPedido[1], Utilidades.unescape(aPedido[4]), (aPedido[5] == "") ? null : ((DateTime?)DateTime.Parse(aPedido[5])), Utilidades.unescape(aPedido[6]));
                            break;
                        case "D":
                            PEDIDOPROYECTO.Delete(tr, int.Parse(aPedido[3]));
                            break;
                    }
                }
            }
            #endregion
            #region Soporte Administrativo
            if (strSoporteAdm != "")
            {
                string[] aSoporte = Regex.Split(strSoporteAdm, "##");
                if (byte.Parse(aSoporte[0]) == 0)
                {
                    //Si no es externalizable -> Borrar los espacios de acuerdo de ese proyecto
                    //ESPACIOACUERDO.Delete2(tr, nPE);
                    PROYECTO.UpdateSoporte(tr, nPE, 0, null, null, null);
                    //DOC_ACUERDO_PROY.DeleteByPE(tr, nPE);
                    if (aSoporte[1] != "")
                    {//Era externalizable y deja de serlo
                        //string sNombre = "";
                        //SqlDataReader dr = USUARIO.ObtenerDatosProfUsuario(int.Parse(Session["UsuarioActual"].ToString()));
                        //if (dr.Read())
                        //    sNombre = dr["PROFESIONAL"].ToString();
                        //dr.Close();
                        //dr.Dispose();
                        EnviarCorreosResponsablesSoporte(5, nPSN, nPE, sDenPE, Session["DES_EMPLEADO"].ToString(), "");
                    }
                }
                else
                {
                    #region externalizable
                    bool bEnviadoResp = false;
                    int? nSAT = null;
                    int? nSAA = null;
                    int? nIdCal = null;
                    if (aSoporte[1] != "") nSAT = int.Parse(aSoporte[1]);
                    if (aSoporte[2] != "") nSAA = int.Parse(aSoporte[2]);
                    if (aSoporte[14] != "") nIdCal = int.Parse(aSoporte[14]);
                    PROYECTO.UpdateSoporte(tr, nPE, byte.Parse(aSoporte[0]), nSAT, nSAA, nIdCal);
                    //SAT (titular)
                    if (aSoporte[3] != aSoporte[1])
                    {
                        //Si se pasa de no tener SAT a tenerlo, hay que enviar un correo a Responsables, Delegados y Colaboradores del proyecto
                        //indicando que se ha nombrado SAT y que es necesario cumplimentar el espacio de acuerdo
                        //En otro caso no se envía correo al responsable
                        if (aSoporte[3] == "")
                        {
                            EnviarCorreoUsuarioSoporte(true, "T", nPE, sDenPE, int.Parse(aSoporte[1]));
                            EnviarCorreosResponsablesSoporte(1, nPSN, nPE, sDenPE, Utilidades.unescape(aSoporte[25]), "");
                            bEnviadoResp = true;
                        }
                        else
                        {
                            if (aSoporte[1] == "")
                            {//Era SAT y ha dejado de serlo
                                EnviarCorreoUsuarioSoporte(false, "T", nPE, sDenPE, int.Parse(aSoporte[3]));
                            }
                            else
                            {//Se ha cambiado de SAT -> notificación de alta al nuevo y de baja al viejo. 
                                EnviarCorreoUsuarioSoporte(true, "T", nPE, sDenPE, int.Parse(aSoporte[1]));
                                EnviarCorreoUsuarioSoporte(false, "T", nPE, sDenPE, int.Parse(aSoporte[3]));
                            }
                        }
                    }
                    //SAA (alternativo)
                    if (aSoporte[4] != aSoporte[2])
                    {
                        if (aSoporte[4] == "")
                        {//No había SAA y ahora si
                            EnviarCorreoUsuarioSoporte(true, "A", nPE, sDenPE, int.Parse(aSoporte[2]));
                            if (!bEnviadoResp)
                                EnviarCorreosResponsablesSoporte(3, nPSN, nPE, sDenPE, Utilidades.unescape(aSoporte[26]), "");
                        }
                        else
                        {
                            if (aSoporte[2] == "")
                            {//Era SAA y ha dejado de serlo
                                EnviarCorreoUsuarioSoporte(false, "A", nPE, sDenPE, int.Parse(aSoporte[4]));
                            }
                            else
                            {//Se ha cambiado de SAA -> notificación de alta al nuevo y de baja al viejo.
                                EnviarCorreoUsuarioSoporte(true, "A", nPE, sDenPE, int.Parse(aSoporte[2]));
                                EnviarCorreoUsuarioSoporte(false, "A", nPE, sDenPE, int.Parse(aSoporte[4]));
                            }
                        }
                    }
                    #endregion
                    #region Espacio de acuerdo
                    DateTime? dFFin = null;
                    //DateTime? dFAcept = null;
                    if (aSoporte[20] != "") dFFin = DateTime.Parse(aSoporte[20]);
                    //if (aSoporte[21] != "") dFAcept = DateTime.Parse(aSoporte[21]);

                    //Si se había pedido aceptación del espacio de acuerdo -> INSERT
                    //Sino -> UPDATE
                    if (aSoporte[24] == "")
                    {
                        //Si no había espacio de acuerdo lo inserto, sino lo updateo
                        if (aSoporte[5] == "")
                            nIdAcuerdo = ESPACIOACUERDO.Insert(tr, nPE, (aSoporte[6] == "1") ? true : false, (aSoporte[7] == "1") ? true : false,
                                                      (aSoporte[8] == "1") ? true : false, (aSoporte[9] == "1") ? true : false,
                                                      (aSoporte[10] == "1") ? true : false, Utilidades.unescape(aSoporte[11]),
                                                      Utilidades.unescape(aSoporte[12]), Utilidades.unescape(aSoporte[13]),
                                                      (aSoporte[15] == "1") ? true : false, (aSoporte[16] == "") ? null : aSoporte[16], Utilidades.unescape(aSoporte[17]),
                                                      null, (aSoporte[19] != "") ? (int?)int.Parse(aSoporte[19]) : null, dFFin, null,
                                                      (aSoporte[27] == "1") ? true : false, null, null);
                        else
                            ESPACIOACUERDO.Update2(tr, int.Parse(aSoporte[5]), (aSoporte[6] == "1") ? true : false, (aSoporte[7] == "1") ? true : false,
                                                  (aSoporte[8] == "1") ? true : false, (aSoporte[9] == "1") ? true : false,
                                                  (aSoporte[10] == "1") ? true : false, Utilidades.unescape(aSoporte[11]),
                                                  Utilidades.unescape(aSoporte[12]), Utilidades.unescape(aSoporte[13]),
                                                  (aSoporte[15] == "1") ? true : false, (aSoporte[16] == "") ? null : aSoporte[16], Utilidades.unescape(aSoporte[17]),
                                                  (aSoporte[18] != "") ? (int?)int.Parse(aSoporte[18]) : null, dFFin,
                                                  (aSoporte[27] == "1") ? true : false);
                    }
                    else
                    {//Por si acaso vuelvo a comprobar si existe
                        //if (ESPACIOACUERDO.Existe(tr, nPE))
                        //    ESPACIOACUERDO.Update2(tr, nPE, (aSoporte[6] == "1") ? true : false, (aSoporte[7] == "1") ? true : false,
                        //                          (aSoporte[8] == "1") ? true : false, (aSoporte[9] == "1") ? true : false,
                        //                          (aSoporte[10] == "1") ? true : false, Utilidades.unescape(aSoporte[11]),
                        //                          Utilidades.unescape(aSoporte[12]), Utilidades.unescape(aSoporte[13]),
                        //                          (aSoporte[15] == "1") ? true : false, aSoporte[16], Utilidades.unescape(aSoporte[17]),
                        //                          (aSoporte[18] != "") ? (int?)int.Parse(aSoporte[18]) : null,
                        //                          (aSoporte[19] != "") ? (int?)int.Parse(aSoporte[19]) : null, dFFin, dFAcept);
                        //else
                        nIdAcuerdo = ESPACIOACUERDO.Insert(tr, nPE, (aSoporte[6] == "1") ? true : false, (aSoporte[7] == "1") ? true : false,
                                                  (aSoporte[8] == "1") ? true : false, (aSoporte[9] == "1") ? true : false,
                                                  (aSoporte[10] == "1") ? true : false, Utilidades.unescape(aSoporte[11]),
                                                  Utilidades.unescape(aSoporte[12]), Utilidades.unescape(aSoporte[13]),
                                                  (aSoporte[15] == "1") ? true : false, (aSoporte[16] == "") ? null : aSoporte[16], Utilidades.unescape(aSoporte[17]),
                                                  (aSoporte[18] != "") ? (int?)int.Parse(aSoporte[18]) : null,
                                                  null, dFFin, null, (aSoporte[27] == "1") ? true : false, null, null);
                    }
                    //Miro si hay que enviar correos de confirmación
                    //Correo para pedir aceptación a los responsables del soporte administrativo
                    if (aSoporte[22] == "S")
                    {
                        EnviarCorreoUsuarioSoporte(true, "X", nPE, sDenPE, int.Parse(aSoporte[1]));
                        if (aSoporte[2] != "")
                            EnviarCorreoUsuarioSoporte(true, "X", nPE, sDenPE, int.Parse(aSoporte[2]));
                    }
                    //Correo para Aceptar al reponsable, Delegado y Colaboradores del proyecto
                    //Se envía al pulsar el botón Aceptar
                    //if (aSoporte[23] == "S")
                    //    EnviarCorreosResponsablesSoporte(2, nPSN, nPE, sDenPE, Session["DES_EMPLEADO"].ToString(), "");
                }
                    #endregion
            }
            #endregion

            #endregion

            #region Anotaciones
            if (strAnotaciones != "")//No se ha modificado nada de la pestaña de Perfil/Tarifas
            {
                string[] aAnotaciones = Regex.Split(strAnotaciones, "##");
                PROYECTOSUBNODO.UpdateAnotaciones(tr, nPSN, Utilidades.unescape(aAnotaciones[0]), Utilidades.unescape(aAnotaciones[1]), Utilidades.unescape(aAnotaciones[2]));
            }

            #endregion

            #region Datos Periodificacion
            if (strPeriodificacion != "")//No se ha modificado nada de la pestaña de Periodificacion
            {
                string[] aPeriodificacion = Regex.Split(strPeriodificacion, "///");
                foreach (string oPeriodificacion in aPeriodificacion)
                {
                    if (oPeriodificacion == "") continue;
                    string[] aPeriod = Regex.Split(oPeriodificacion, "##");
                    ///aPeriod[0] = bd
                    ///aPeriod[1] = idSegMesProy
                    ///aPeriod[2] = Produccion
                    ///aPeriod[3] = Consumo
                    ///aPeriod[4] = anomes

                    nAux = SEGMESPROYECTOSUBNODO.UpdateProduccionConsumoPeriodificacion(tr, int.Parse(aPeriod[1]), (aPeriod[2] == "") ? 0 : decimal.Parse(aPeriod[2]), (aPeriod[3] == "") ? 0 : decimal.Parse(aPeriod[3]));
                    if (nAux == 0)
                    {
                        sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, nPSN, int.Parse(aPeriod[4]));
                        nAux = SEGMESPROYECTOSUBNODO.Insert(tr, nPSN, int.Parse(aPeriod[4]), sEstadoMes, 0, 0, false, (aPeriod[2] == "") ? 0 : decimal.Parse(aPeriod[2]), (aPeriod[3] == "") ? 0 : decimal.Parse(aPeriod[3]));
                    }
                }
            }
            #endregion

            #region Grabación de plantilla
            if (aDatosBasicos[7] != "")
            {
                //Hay que grabar la plantilla de PE.
                int iPos, iMargen, iPT = -1, iFase = -1, iActiv = -1, iTarea = -1, iHito = -1, iAux = -1, iOrden = 0, idItemHitoPl;
                double fDuracion;
                decimal fPresupuesto, fAvance;
                string sTipo, sDesc, sFiniPL, sFfinPL, sFiniV, sFfinV, sAux, sAvisos, sIdTareaPL, sCad;
                bool bFacturable, bObligaEst, bAvanceAutomatico, bEstadoTarea;
                ArrayList alTareas = new ArrayList();

                PROYECTOSUBNODO.BorrarPTByPSN(tr, nPSN);

                #region 1º Se insertan las filas de la estructura
                SqlDataReader dr = PlantTarea.Catalogo(int.Parse(aDatosBasicos[7]));
                while (dr.Read())
                {
                    sTipo = dr["Tipo"].ToString();
                    if (sTipo == "H")
                    {
                        sTipo = "HT";
                    }
                    sDesc = Utilidades.escape(dr["Nombre"].ToString());
                    iMargen = int.Parse(dr["margen"].ToString());

                    //Si la linea es de hito compruebo si el hito es de tarea o no para actualizar la variable iTarea
                    if (sTipo == "HT" || sTipo == "HM" || sTipo == "HF")
                    {
                        switch (iMargen)
                        {
                            case 80://es un hito de tarea por lo que mantengo el código de tarea
                                break;
                            case 60://es un hito de fase y actividad o de tarea con actividad sin fase
                                if (iFase != -1) iTarea = -1;
                                break;
                            case 40://es un hito de fase o de tarea sin actividad ni fase o de actividad sin fase
                                if (iFase != -1)
                                {
                                    iTarea = -1;
                                    iActiv = -1;
                                }
                                else
                                {
                                    if (iActiv != -1)
                                    {
                                        iTarea = -1;
                                    }
                                }
                                break;
                            case 20://es un hito proyecto técnico
                            case 0://es un hito de proyecto económico
                                iTarea = -1;
                                iActiv = -1;
                                iFase = -1;
                                break;
                        }

                    }

                    fDuracion = 0;
                    sFiniPL = ""; //¿alguno es obligatorio?
                    sFfinPL = "";
                    sFiniV = Fechas.primerDiaMes(DateTime.Today).ToShortDateString();
                    sFfinV = "";
                    fPresupuesto = 0;
                    fAvance = 0;
                    sIdTareaPL = dr["t339_iditems"].ToString();

                    bFacturable = (bool)dr["t339_facturable"];

                    //if (sEstado != "D") 
                    iOrden++;
                    //iOrden = int.Parse(aElem[8]);
                    //Si no ha cambiado la linea pero el orden actual es distinto del original hay que updatear la linea para actualizar el orden
                    switch (sTipo)
                    {
                        case "P":
                            iPT = -1;
                            iFase = -1;
                            iActiv = -1;
                            break;
                        case "F":
                            iFase = -1;
                            iActiv = -1;
                            break;
                        case "A":
                            iActiv = -1;
                            if (iMargen != 40)
                                iFase = -1;
                            break;
                        case "T":
                            iTarea = -1;
                            if (iMargen == 40)
                            {
                                iFase = -1;
                            }
                            else
                            {
                                if (iMargen != 60)
                                {
                                    iFase = -1;
                                    iActiv = -1;
                                }
                            }
                            break;
                        case "HT":
                        case "HF":
                        case "HM":
                            iHito = -1;//int.Parse(aElem[7]);
                            break;
                    }

                    bObligaEst = (bool)dr["obliga"];
                    bAvanceAutomatico = (bool)dr["avance"];
                    sAux = EstrProy.Insertar(tr, int.Parse(aDatosBasicos[6]), nPE, nPSN, sTipo, sDesc, iPT, iFase, iActiv, iMargen, iOrden,
                                             sFiniPL, sFfinPL, fDuracion, sFiniV, sFfinV, fPresupuesto,
                                             bFacturable, bObligaEst, bAvanceAutomatico, "1", "", fAvance);

                    iPos = sAux.IndexOf("##");
                    iAux = int.Parse(sAux.Substring(0, iPos));
                    sAvisos = sAux.Substring(iPos + 2);

                    switch (sTipo)
                    {
                        case "P": iPT = iAux; break;
                        case "F": iFase = iAux; break;
                        case "A": iActiv = iAux; break;
                        case "T":
                            iTarea = iAux;
                            if (sIdTareaPL != "" && sIdTareaPL != "-1")
                            {
                                string[] aDatosAux = new string[] { sIdTareaPL, iAux.ToString() };
                                alTareas.Add(aDatosAux);
                                //Grabo los atributos estadísticos provenientes de la plantilla. iAux=código de tarea
                                TAREAPSP.InsertarAE(tr, int.Parse(sIdTareaPL), iAux);
                            }

                            //Hay que guardar las tareas que quedan pendientes, ya que luego hay que actualizar el estado en pantalla
                            bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, short.Parse(aDatosBasicos[7]), iAux);
                            if (bEstadoTarea)
                            {
                                //actualizo el estado de la tarea
                                TAREAPSP.Modificar(tr, iTarea, sDesc, iPT, iActiv, iOrden, sFiniPL, sFfinPL, fDuracion, sFiniV,
                                                   sFfinV, (int)Session["UsuarioActual"], fPresupuesto, 2, bFacturable);
                                //sAvisos = "Se han insertado tareas que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que la tarea no tiene valores asignados";
                                //if (sTareasPendientes == "") sTareasPendientes = iAux.ToString();
                                //else sTareasPendientes += "//"+ iAux.ToString();
                            }
                            break;
                        case "HT":
                            iHito = iAux;
                            break;
                    }
                    if (sTipo.Substring(0, 1) == "H")
                    {
                        AsociarTareasHitos(tr, nPSN, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                    }
                }
                dr.Close();
                dr.Dispose();
                #endregion

                #region 2º Se insertan las filas de los hitos de cumplimiento discontinuo
                dr = PlantTarea.CatalogoHitos(int.Parse(aDatosBasicos[7]));
                while (dr.Read())
                {
                    sTipo = "HM";
                    sDesc = dr["t369_deshito"].ToString();
                    idItemHitoPl = (int)dr["t369_idhito"];
                    iOrden = int.Parse(dr["t369_orden"].ToString());

                    sAux = EstrProy.Insertar(tr, int.Parse(aDatosBasicos[6]), nPE, nPSN, sTipo, sDesc, 0, 0, 0, 0, iOrden, "", "", 0, "", "", 0, false, false, false, "1", "", 0);
                    iPos = sAux.IndexOf("##");
                    iAux = int.Parse(sAux.Substring(0, iPos));
                    sAvisos = sAux.Substring(iPos + 2);
                    //Si es hito de cumplimiento discontinuo y se ha cargado desde plantilla hay que grabar sus tareas
                    if (sTipo == "HM")
                    {
                        if (idItemHitoPl > 0)
                        {
                            //Recojo las tareas de plantilla del código de hito en plantilla
                            sCad = HITOE_PLANT.fgListaTareasPlantilla(tr, idItemHitoPl);
                            string[] aElems2 = Regex.Split(sCad, @"##");
                            for (int j = 0; j < aElems2.Length; j++)
                            {
                                sIdTareaPL = aElems2[j];
                                if (sIdTareaPL != "" && sIdTareaPL != "-1")
                                {
                                    //Identifico el código de tarea real asociado al codigo de tarea de plantilla
                                    for (int n = 0; n < alTareas.Count; n++)
                                    {
                                        if (((string[])alTareas[n])[0] == sIdTareaPL)
                                        {//Inserto la tarea del hito
                                            sCad = ((string[])alTareas[n])[1];
                                            iTarea = int.Parse(sCad);
                                            EstrProy.InsertarTareaHito(tr, iAux, iTarea);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }//while
                dr.Close();
                dr.Dispose();
                #endregion
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            if (strGeneral != "")
            {
                string[] aGeneral = Regex.Split(strGeneral, "##");
                if (aGeneral[7] != aGeneral[39])
                {
                    PROYECTO oProy = PROYECTO.Obtener(null, nPE);
                    sExternalizable = ((bool)oProy.t301_externalizable) ? "1" : "0";
                }
            }
            if (aDatosBasicos[3] == "") //Se trata de un proyecto nuevo.
            {
                PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, nPSN);
                nVisadorCV = (int)oPSN.t001_idficepi_visadorcv;
                sVisadorCV = oPSN.des_visadorcv;
            }
            string sIdExpProf = "";
            if (strProfesionales != "")
            {//Si el proyecto está asociado a alguna experiencia en la que alguno de los profesionales tiene definida plantilla
                //la aplicación preguntará si se quiere cualificar el proyecto para CVT
                sIdExpProf = PROYECTO.GetExpConPlantilla(null, nPE);
            }
            Session["NUM_PROYECTO"] = nPE;
            string sPermitirPST = (bPermitirPST) ? "1" : "0";
            sResul = "OK@#@" + nPE.ToString() + "@#@" + nPSN.ToString() + "@#@" + sTarifasInsertadas + "@#@" + sPedidosInsertadosI +
                     "@#@" + sPedidosInsertadosC + "@#@" + sNivelesInsertados + "@#@" + sPermitirPST + "@#@" + nIdAcuerdo.ToString() +
                     "@#@" + sExternalizable + "@#@" + nVisadorCV.ToString() + "@#@" + sVisadorCV + "@#@" + sIdExpProf;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            //if (bErrorControlado) sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
            if (bErrorControlado) sResul = "DENEGADO@#@Operación rechazada.\n\n" + ex.Message;
            else if (Errores.EsErrorIntegridad(ex)) sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al grabar los datos del proyecto", ex, false); //ex.Message;
            else sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del proyecto", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private void AsociarTareasHitos(SqlTransaction tr, int nT305IdProy, int nPT, int nFase, int nActiv, int nTarea, int nHito, int nMargen)
    {
        string sTipo = "";
        int nCodigo = -1;

        try
        {
            #region 1º Identificar el tipo de Hito
            if (nMargen == 0)
            {
                sTipo = "PE"; //Hito de proyecto económico
                nCodigo = nT305IdProy;
            }
            else
            {
                if (nTarea != -1)
                {
                    sTipo = "T"; //Hito de tarea
                    nCodigo = nTarea;
                }
                else
                {
                    if (nPT != -1 && nFase == -1 && nActiv == -1)// && nTarea == -1
                    {
                        sTipo = "PT"; //Hito de proyecto técnico
                        nCodigo = nPT;
                    }
                    else if (nFase != -1 && nActiv == -1)// && nTarea == -1
                    {
                        sTipo = "F"; //Hito de Fase
                        nCodigo = nFase;
                    }
                    else if (nActiv != -1)// && nTarea == -1
                    {
                        sTipo = "A"; //Hito de Actividad
                        nCodigo = nActiv;
                    }
                }
            }
            #endregion

            ////2º Se borran las tareas que pudiera haber ligadas al hito
            //EstrProy.BorrarTareasHito(tr, nHito);

            //3º Se asocian al hito las tareas que correspondan al "tipo" (nivel) del hito.
            //EstrProy.AsociarTareasHito(tr, sTipo, nHito, nCodigo, short.Parse(Session["NodoActivo"].ToString()));
            EstrProy.AsociarTareasHito(tr, sTipo, nHito, nCodigo);
        }
        catch (Exception ex)
        {
            //Conexion.CerrarTransaccion(tr);
            Errores.mostrarError("Error al grabar las tareas asociadas a los hitos", ex);
        }
    }

    //private void AddModalidadSiNoExiste(string t306_idmodalidad)
    //{
    //    var bExiste = false;
    //    for (int i = 0; i < cboModContratacion.Items.Count - 1; i++)
    //    {
    //        if (cboModContratacion.Items[i].Value == t306_idmodalidad)
    //        {
    //            bExiste = true;
    //            break;
    //        }
    //    }
    //    if (!bExiste)
    //    {
    //        MODALIDADCONTRATO oElem = SUPER.Capa_Negocio.MODALIDADCONTRATO.Select(null, byte.Parse(t306_idmodalidad));
    //        ListItem l = new ListItem(oElem.t316_denominacion, oElem.t316_idmodalidad.ToString(), true); 
    //        //l.Selected = true;
    //        cboModContratacion.Items.Add(l);
    //    }
    //}
    private string obtenerDatosPE(string sPestana, string sIDProySubNodo)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.ObtenerConProduccion(null, int.Parse(sIDProySubNodo));
            PROYECTO oProy = PROYECTO.Obtener(null, oPSN.t301_idproyecto);
            //if (oProy.t316_idmodalidad.HasValue)
            //{
            //    AddModalidadSiNoExiste(oProy.t316_idmodalidad.ToString());
            //}
            NODO oNodo = NODO.Select(null, oPSN.t303_idnodo);
            oNodo.ObtenerCualificadoresEstructura();

            sb.Append(oPSN.t305_idproyectosubnodo.ToString() + "///"); //0
            sb.Append(oPSN.t301_idproyecto.ToString() + "///"); //1
            sb.Append(oPSN.t304_idsubnodo.ToString() + "///"); //2
            sb.Append(oPSN.t305_finalizado + "///"); //3
            sb.Append(oPSN.t305_cualidad + "///"); //4
            sb.Append(((bool)oPSN.t305_heredanodo) ? "1///" : "0///"); //5
            sb.Append(oPSN.t303_idnodo.ToString() + "///"); //6
            sb.Append(oPSN.t303_denominacion.ToString() + "///"); //7
            sb.Append(oPSN.t304_denominacion.ToString() + "///"); //8
            sb.Append(oPSN.t303_ultcierreeco.ToString() + "///"); //9
            sb.Append(oPSN.t314_idusuario_responsable.ToString() + "///"); //10
            sb.Append(oPSN.des_responsable.ToString() + "///"); //11
            sb.Append(((bool)oPSN.t305_finalizado) ? "1///" : "0///"); //12
            sb.Append(Utilidades.escape(oPSN.t305_seudonimo) + "///"); //13
            sb.Append(oPSN.t305_accesobitacora_iap + "///"); //14
            sb.Append(oPSN.t305_accesobitacora_pst + "///"); //15
            sb.Append(((bool)oPSN.t305_imputablegasvi) ? "1///" : "0///"); //16
            sb.Append(((bool)oPSN.t305_admiterecursospst) ? "1///" : "0///"); //17
            sb.Append(((bool)oPSN.t305_avisoresponsablepst) ? "1///" : "0///"); //18
            sb.Append(((bool)oPSN.t305_avisorecursopst) ? "1///" : "0///"); //19
            sb.Append(((bool)oPSN.t305_avisofigura) ? "1///" : "0///"); //20

            if (oPSN.t314_idusuario_responsable.ToString() == Session["UsuarioActual"].ToString() ||
                SUPER.Capa_Negocio.Utilidades.EsAdminProduccion() ||
                    (oProy.t301_externalizable && oProy.t301_estado != "C" && oProy.t301_estado != "H" && oPSN.t305_cualidad == "C"
                        && (Session["UsuarioActual"].ToString() == oProy.t314_idusuario_SAT.ToString() ||
                            Session["UsuarioActual"].ToString() == oProy.t314_idusuario_SAA.ToString())
                    )
                )
            {
                sb.Append("R///"); //21
            }
            else
            {
                SqlDataReader dr = FIGURA.getFigurasPSN(null, oPSN.t305_idproyectosubnodo, (int)Session["UsuarioActual"]);
                while (dr.Read())
                {
                    if (sResul == "") sResul = dr["t310_figura"].ToString();
                    else sResul += "##" + dr["t310_figura"].ToString();
                }
                dr.Close();
                dr.Dispose();
                sb.Append(sResul + "///"); //21
            }
            sb.Append(oPSN.fecInicioReal.ToString() + "///"); //22
            sb.Append(oPSN.fecFinReal.ToString() + "///"); //23
            sb.Append(oPSN.nProducidoReal.ToString("N") + "///"); //24
            sb.Append(oPSN.mesesCerrados.ToString() + "///"); //25
            sb.Append(oPSN.t001_ficepi_visador.ToString() + "///"); //26
            sb.Append(Utilidades.escape(oPSN.des_visador) + "///"); //27
            sb.Append(((bool)oPSN.t305_supervisor_visador) ? "1///" : "0///"); //28

            sb.Append(oPSN.t476_idcnp.ToString() + "///"); //29
            if (oPSN.t476_idcnp.HasValue)
            {
                CDP oCNP = CDP.Select(null, (int)oPSN.t476_idcnp);
                sb.Append(Utilidades.escape(oCNP.t476_denominacion) + "///"); //30
            }
            else sb.Append("///"); //30

            sb.Append(oPSN.t485_idcsn1p.ToString() + "///"); //31
            if (oPSN.t485_idcsn1p.HasValue)
            {
                CSN1P oCSN1P = CSN1P.Select(null, (int)oPSN.t485_idcsn1p);
                sb.Append(Utilidades.escape(oCSN1P.t485_denominacion) + "///"); //32
            }
            else sb.Append("///"); //32

            sb.Append(oPSN.t487_idcsn2p.ToString() + "///"); //33
            if (oPSN.t487_idcsn2p.HasValue)
            {
                CSN2P oCSN2P = CSN2P.Select(null, (int)oPSN.t487_idcsn2p);
                sb.Append(Utilidades.escape(oCSN2P.t487_denominacion) + "///"); //34
            }
            else sb.Append("///"); //34

            sb.Append(oPSN.t489_idcsn3p.ToString() + "///"); //35
            if (oPSN.t489_idcsn3p.HasValue)
            {
                CSN3P oCSN3P = CSN3P.Select(null, (int)oPSN.t489_idcsn3p);
                sb.Append(Utilidades.escape(oCSN3P.t489_denominacion) + "///"); //36
            }
            else sb.Append("///"); //36

            sb.Append(oPSN.t491_idcsn4p.ToString() + "///"); //37
            if (oPSN.t491_idcsn4p.HasValue)
            {
                CSN4P oCSN4P = CSN4P.Select(null, (int)oPSN.t491_idcsn4p);
                sb.Append(Utilidades.escape(oCSN4P.t491_denominacion) + "///"); //38
            }
            else sb.Append("///"); //38

            if (oPSN.t305_cualidad != "C")
            {
                PROYECTOSUBNODO oPSNCON = PROYECTOSUBNODO.ObtenerContratante(null, oPSN.t301_idproyecto);
                sb.Append(oPSNCON.t303_denominacion + "///"); //39
                sb.Append(oPSNCON.des_responsable + "///"); //40
                sb.Append(oPSNCON.ext_responsable + "///"); //41
            }
            else
            {
                sb.Append("///"); //39
                sb.Append("///"); //40
                sb.Append("///"); //41
            }

            sb.Append(oPSN.t305_importaciongasvi.ToString() + "///"); //42

            SqlDataReader drGSB = SUBNODO.CatalogoPorUsuarioNodo(null, oPSN.t314_idusuario_responsable, oPSN.t303_idnodo);
            if (drGSB.Read())
                sb.Append(drGSB["GSB"].ToString() + "///"); //43
            else
                sb.Append("1" + "///"); //43
            drGSB.Close();
            drGSB.Dispose();

            sb.Append(oPSN.t422_idmoneda + "///"); //44
            sb.Append(Utilidades.escape(oPSN.t422_denominacion) + "///"); //45
            sb.Append(((bool)oPSN.t305_opd) ? "1///" : "0///"); //46
            sb.Append(((oPSN.t001_idficepi_visadorcv.HasValue)? oPSN.t001_idficepi_visadorcv.ToString():"") + "///"); //47
            sb.Append(Utilidades.escape(oPSN.des_visadorcv) + "///"); //48
            sb.Append(((oPSN.t001_idficepi_interlocutor.HasValue) ? oPSN.t001_idficepi_interlocutor.ToString() : "") + "///"); //49
            sb.Append(Utilidades.escape(oPSN.des_interlocutor) + "///"); //50
            sb.Append(Utilidades.escape(oPSN.profesional_diceno_cvt) + "///"); //51
            sb.Append(((oPSN.t301_fechano_cvt.HasValue)? ((DateTime)oPSN.t301_fechano_cvt).ToString().Substring(0, ((DateTime)oPSN.t301_fechano_cvt).ToString().Length-3) :"") + "///"); //52
            sb.Append(Utilidades.escape(oPSN.t301_motivono_cvt) + "///"); //53

            sb.Append(PROYECTOSUBNODO.getAcceso(null, int.Parse(sIDProySubNodo), int.Parse(Session["UsuarioActual"].ToString())) + "///");//54
            sb.Append(oPSN.t001_idficepi_responsable.ToString() + "///"); //55

            sb.Append(((oPSN.t001_idficepi_interlocalertasocfa.HasValue) ? oPSN.t001_idficepi_interlocalertasocfa.ToString() : "") + "///"); //56
            sb.Append(Utilidades.escape(oPSN.des_interlocalertasocfa) + "///"); //57

            sb.Append("{sep}");

            sb.Append(oProy.t301_idproyecto.ToString() + "///"); //0
            sb.Append(oProy.t301_estado + "///"); //1
            sb.Append(Utilidades.escape(oProy.t301_denominacion) + "///"); //2
            sb.Append(Utilidades.escape(oProy.t301_descripcion) + "///"); //3
            sb.Append(oProy.t302_idcliente_proyecto.ToString() + "///"); //4
            sb.Append((oProy.t306_idcontrato.HasValue) ? oProy.t306_idcontrato.Value.ToString() + "///" : "///"); //5 NULL
            sb.Append((oProy.t307_idhorizontal.HasValue) ? oProy.t307_idhorizontal.Value.ToString() + "///" : "///"); //6 NULL
            sb.Append(oProy.t320_idtipologiaproy.ToString() + "///"); //7
            sb.Append(oProy.t323_idnaturaleza.ToString() + "///"); //8
            sb.Append((oProy.t316_idmodalidad.HasValue) ? oProy.t316_idmodalidad.Value.ToString() + "///" : "///"); //9 NULL
            sb.Append(oProy.t301_fiprev.ToShortDateString() + "///"); //10
            sb.Append(oProy.t301_ffprev.ToShortDateString() + "///"); //11
            sb.Append(oProy.t301_fcreacion.ToShortDateString() + "///"); //12
            sb.Append(((bool)oProy.t301_pap) ? "1///" : "0///"); //13
            sb.Append(oProy.t301_categoria + "///"); //14
            sb.Append(oProy.t301_modelotarif + "///"); //15
            sb.Append(Utilidades.escape(oProy.t302_denominacion) + "///"); //16
            sb.Append(Utilidades.escape(oProy.t377_denominacion) + "///"); //17
            sb.Append(oProy.t301_modelocoste + "///"); //18
            sb.Append(Utilidades.escape(oProy.t323_denominacion) + "///"); //19
            sb.Append((oProy.t307_idhorizontal.HasValue) ? Utilidades.escape(oProy.t307_denominacion) + "///" : "///"); //20
            sb.Append(oProy.t301_annoPIG + "///"); //21
            SqlDataReader dr1 = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(sIDProySubNodo), (int)Session["UsuarioActual"], "PGE");
            if (dr1.Read())
            {
                sb.Append(((bool)dr1["t301_pap"]) ? "1///" : "0///"); //22
                sb.Append(dr1["t302_codigoexterno_cli"].ToString() + "///");  //23
                sb.Append(dr1["t302_codigoexterno_emp"].ToString() + "///");  //24
                sb.Append("1///");  //25
            }
            else
            {
                sb.Append("///"); //22
                sb.Append("///");  //23
                sb.Append("///");  //24
                sb.Append("0///");  //25
            }
            dr1.Close();
            dr1.Dispose();
            sb.Append(((bool)oProy.t301_pgrcg) ? "1///" : "0///"); //26
            sb.Append(((bool)oProy.t301_esreplicable) ? "1///" : "0///"); //27
            //Soporte administrativo
            //if (oProy.t301_externalizable && oProy.t301_estado != "C" && oProy.t301_estado != "H" && oPSN.t305_cualidad == "C")
            if (oProy.t301_externalizable && oPSN.t305_cualidad == "C")
                {
                    sb.Append(((bool)oProy.t301_externalizable) ? "1///" : "0///"); //28
                sb.Append(oProy.t314_idusuario_SAT.ToString() + "///"); //29
                sb.Append(oProy.t314_idusuario_SAA.ToString() + "///"); //30
                sb.Append(oProy.soporte_titular.ToString() + "///"); //31
                sb.Append(oProy.soporte_alternativo.ToString() + "///"); //32
            }
            else
            {
                sb.Append("0///");  //28
                sb.Append("///"); //29
                sb.Append("///");  //30
                sb.Append("///");  //31
                sb.Append("///");  //32
            }
            sb.Append((oProy.t301_mesesprevgar.HasValue) ? oProy.t301_mesesprevgar.Value.ToString() + "///" : "///"); //33 NULL       
            sb.Append(((bool)oProy.t301_activagar) ? "1///" : "0///"); //34
            sb.Append((oProy.t301_iniciogar.HasValue) ? oProy.t301_iniciogar.Value.ToShortDateString() + "///" : "///"); //35 NULL
            sb.Append((oProy.t301_fingar.HasValue) ? oProy.t301_fingar.Value.ToShortDateString() + "///" : "///"); //36 NULL
            
            sb.Append(oProy.t055_idcalifOCFA.ToString() + "///"); //37
            sb.Append(oProy.t055_denominacion.ToString() + "///"); //38
            sb.Append(Utilidades.escape(oProy.t316_denominacion) + "///"); //39 MODALIDAD DE CONTRATO

            //if (oProy.t301_externalizable && oProy.t301_estado != "C" && oProy.t301_estado != "H" && oPSN.t305_cualidad == "C")
            if (oProy.t301_externalizable && oPSN.t305_cualidad == "C")
            {
                sb.Append(oProy.t001_idficepi_SAT.ToString() + "///"); //40
            }
            else
            {
                sb.Append("///");  //40
            }
            sb.Append(oProy.t195_idlineaoferta.ToString() + "///"); //41
            sb.Append(oProy.t195_denominacion + "///"); //42

            sb.Append("{sep}");
            sb.Append(oNodo.t303_modelocostes + "///"); // 0
            sb.Append(oNodo.t303_modelotarifas + "///"); // 1
            sb.Append(oNodo.t303_denominacion_CNP + "///"); // 2
            sb.Append((oNodo.t303_obligatorio_CNP) ? "1///" : "0///"); // 3
            sb.Append(oNodo.t391_denominacion_CSN1P + "///"); // 4
            sb.Append((oNodo.t391_obligatorio_CSN1P) ? "1///" : "0///"); // 5
            sb.Append(oNodo.t392_denominacion_CSN2P + "///"); // 6
            sb.Append((oNodo.t392_obligatorio_CSN2P) ? "1///" : "0///"); // 7
            sb.Append(oNodo.t393_denominacion_CSN3P + "///"); // 8
            sb.Append((oNodo.t393_obligatorio_CSN3P) ? "1///" : "0///"); // 9
            sb.Append(oNodo.t394_denominacion_CSN4P + "///"); // 10
            sb.Append((oNodo.t394_obligatorio_CSN4P) ? "1///" : "0///"); // 11
            sb.Append(oNodo.t303_ultcierreeco + "///"); // 12

          

            sResul = "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos del proyecto", ex);
        }

        return sResul;
    }
    private string obtenerRecursosAsociados(string sPestana, string sIDProySubNodo, string sLectura, string sCualidad, string sMostrarBajas, string sPE)
    {
        string sResul = "", sFecha = "", sDerivaImg = "";
        int nPSN = -1;
        //bool bLectura = false;
        StringBuilder sb = new StringBuilder();

        try
        {
            //if (sLectura == "true") bLectura = true;
            if (sIDProySubNodo != "") nPSN = int.Parse(sIDProySubNodo);
            SqlDataReader dr = null;

            if (sCualidad == "J")
            {
                dr = USUARIOPROYECTOSUBNODO.CatalogoUsuariosRepJ(nPSN, (sMostrarBajas == "1") ? true : false);
            }
            else
            {
                dr = USUARIOPROYECTOSUBNODO.CatalogoUsuarios(nPSN, sCualidad, (sMostrarBajas == "1") ? true : false);
            }

            sb.Append("<TABLE id='tblProfAsig' style='width: 920px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width: 14px'/>");
            sb.Append("    <col style='width: 18px;'/>");
            sb.Append("    <col style='width: 18px;'/>");
            sb.Append("    <col style='width: 60px;'/>");
            sb.Append("    <col style='width: 280px;'/>");
            sb.Append("    <col style='width: 170px;'/>");
            sb.Append("    <col style='width: 60px;'/>");
            sb.Append("    <col style='width: 75px'/>");
            sb.Append("    <col style='width: 75px'/>");
            sb.Append("    <col style='width: 150px;'/>");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sDerivaImg = ((bool)dr["t330_deriva"]) ? "1" : "0";
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' ");
                sb.Append("tarifa='" + dr["t333_idperfilproy"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("alta='" + dr["t314_falta"].ToString().Substring(0, 10) + "' ");
                if (dr["t330_fbaja"] != DBNull.Value)
                    sb.Append("fbaja='" + dr["t330_fbaja"].ToString().Substring(0, 10) + "' ");
                else
                    sb.Append("fbaja=''");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("costecon='" + dr["coste"].ToString() + "' ");
                //sb.Append("costecon='" + float.Parse(dr["coste"].ToString()).ToString("N") + "' ");
                sb.Append("costerep='0' ");//porque en las updates no se toca el coste para la replicada.
                sb.Append("deriva='" + sDerivaImg + "' ");
                sb.Append("desnodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                if (dr["tipo"].ToString() == "E")
                    sb.Append("desempresa=\"" + Utilidades.escape(dr["empresa"].ToString()) + "\" ");
                sb.Append("idCal='" + dr["t066_idcal"].ToString() + "' ");
                sb.Append(" style='height:20px;'>");

                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'></td>");
                sb.Append("<td style='text-align:center'></td>");

                sb.Append("<td style='text-align:right; padding-right: 5px;'>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");
                sb.Append("<td style='padding-leftt: 5px;'><div class='NBR W170' title='" + dr["t066_descal"].ToString() + "'>" + dr["t066_descal"].ToString() + "</div></td>");

                sb.Append("<td style='text-align:right; padding-right:5px;'>" + decimal.Parse(dr["coste"].ToString()).ToString("N") + "</td>");

                if (dr["t330_falta"] != DBNull.Value) sFecha = DateTime.Parse(dr["t330_falta"].ToString()).ToShortDateString();
                else sFecha = "";
                sb.Append("<td style='text-align:center'>" + sFecha + "</td>");

                if (dr["t330_fbaja"] != DBNull.Value) sFecha = DateTime.Parse(dr["t330_fbaja"].ToString()).ToShortDateString();
                else sFecha = "";
                sb.Append("<td style='text-align:center'>" + sFecha + "</td>");

                if (dr["t333_denominacion"].ToString() != "")
                    sb.Append("<td><span class='NBR W120' style='vertical-align:top;' title='" + dr["t333_denominacion"].ToString() + "'>" + dr["t333_denominacion"].ToString() + "</span></td>");//tarifa 
                else
                    sb.Append("<td><span class='NBR W120' style='vertical-align:top;'></span></td>");//tarifa 
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sPestana + "@#@" + sb.ToString() + "@#@";
            if (sPestana == "0")
                sResul += getDatosGenSA(sPE);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }
    private string obtenerTarifasPerfil(string sPestana, string sIDProySubNodo, string sLectura)
    {
        bool bLectura = false;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (sLectura == "true") bLectura = true;
            SqlDataReader dr = PERFILSUPER.CatalogoPerfilesNodo_By_PSN(null, int.Parse(sIDProySubNodo));

            sb.Append("<table id='tblTarifas1' class='texto MAM' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:195px;' /><col style='width:75px;' /><col style='width:75px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t347_idperfilsuper"].ToString() + "'");
                if (!bLectura) sb.Append("onclick='mm(event)' ondblclick='insertarTarifa(this, 1)' onmousedown='DD(event);'");
                sb.Append(" style='height:20px'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t347_denominacion"].ToString() + "</td>");
                sb.Append("<td  style='text-align:right;padding-right:3px;'>" + decimal.Parse(dr["t347_imptarifahor"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:3px;'>" + decimal.Parse(dr["t347_imptarifajor"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("|||");

            dr = PERFILPROY.CatalogoPerfilesProyecto_By_PSN(null, int.Parse(sIDProySubNodo));

            sb.Append("<table id='tblTarifas2' class='texto MM' style='width: 320px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:170px;' /><col style='width:90px;' /><col style='width:30px;' /></colgroup>");

            sb.Append("<tbody id='tBodyTarifas2'>");

            string sEventos = " onclick='mm(event)' onmousedown='DD(event);'";
            string sEventoCheck = " onclick='aGPN(0);fm(event)'";
            if (bLectura)
            {
                sEventos = "";
                sEventoCheck = "";
            }
            string sChecked = "", sDisabled = "";

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t333_idperfilproy"].ToString() + "' bd='' orden='" + dr["t333_orden"].ToString() + "' " + sEventos + " style='height:20px'>");
                if (bLectura) sb.Append("<td></td>");
                else sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                if (bLectura) sb.Append("<td></td>");
                else sb.Append("<td><img src='../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");

                if (bLectura) sb.Append("<td><nobr class='NBR W160'>" + dr["t333_denominacion"].ToString() + "</nobr></td>");
                else sb.Append("<td><input type='text' class='txtL' style='width:170px' value='" + dr["t333_denominacion"].ToString() + "' maxlength='30' onKeyUp='aGPN(0);fm(event)'></td>");

                if (bLectura) sb.Append("<td>" + decimal.Parse(dr["t333_imptarifa"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right;'><input type='text' class='txtNumL' style='width:70px' value='" + decimal.Parse(dr["t333_imptarifa"].ToString()).ToString("N") + "' onKeyUp='aGPN(0);fm(event);bActualizarProducido=true;' onfocus='fn(this,5,2)'></td>");

                sChecked = "";
                if (bLectura) sDisabled = "disabled";
                if ((bool)dr["t333_estado"]) sChecked = "checked";
                sb.Append("<td  align='center'><input type='checkbox' class='check' " + sChecked + " " + sEventoCheck + " " + sDisabled + "></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");


            return "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de tarifas por perfil.", ex);
        }
    }
    private string obtenerNivelesCoste(string sPestana, string sPSN, string sIdNodo, string sLectura)
    {
        //int nPE = 0;
        bool bLectura = false;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (sLectura == "true") bLectura = true;
            SqlDataReader dr = COSTENIVEL.CatalogoNivelesNodo_By_PSN(null, int.Parse(sPSN));

            sb.Append("<table id='tblNiveles1' class='texto MAM' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:195px;' /><col style='width:75px;' /><col style='width:75px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t380_idNivelConsumo"].ToString() + "'");
                if (!bLectura) sb.Append("onclick='mm(event)' ondblclick='insertarNivel(this, 1)' onmousedown='DD(event);'");
                sb.Append(" style='height:20px'>");
                sb.Append("<td  style='padding-left:5px;'>" + dr["t380_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:right;padding-right:3px;'>" + decimal.Parse(dr["t380_costenivelH"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:3px;'>" + decimal.Parse(dr["t380_costenivelJ"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("|||");

            dr = NIVELPSN.SelectByT305_idproyectosubnodo(null, int.Parse(sPSN));

            sb.Append("<table id='tblNiveles2' class='texto MM' style='width: 320px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:170px;' /><col style='width:90px;' /><col style='width:30px;'/></colgroup>");
            sb.Append("<tbody id='tBodyNiveles2'>");

            string sEventos = " onclick='mm(event)' onmousedown='DD(event);'";
            string sEventoCheck = " onclick='aGPN(1);fm(event)'";
            if (bLectura)
            {
                sEventos = "";
                sEventoCheck = "";
            }
            string sChecked = "", sDisabled = "";

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t442_idnivel"].ToString() + "' bd='' orden='" + dr["t442_orden"].ToString() + "' " + sEventos + " style='height:20px'>");
                if (bLectura) sb.Append("<td></td>");
                else sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                if (bLectura) sb.Append("<td></td>");
                else sb.Append("<td><img src='../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                if (bLectura) sb.Append("<td><nobr class='NBR W160'>" + dr["t442_denominacion"].ToString() + "</nobr></td>");
                else sb.Append("<td><input type='text' class='txtL' style='width:170px' value='" + dr["t442_denominacion"].ToString() + "' maxlength='30' onKeyUp='aGPN(1);fm(event)'></td>");

                if (bLectura) sb.Append("<td>" + decimal.Parse(dr["t442_impnivel"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right;'><input type='text' class='txtNumL' style='width:70px' value='" + decimal.Parse(dr["t442_impnivel"].ToString()).ToString("N") + "' onKeyUp='aGPN(1);fm(event);' onfocus='fn(this,5,2)'></td>");

                sChecked = "";
                if (bLectura) sDisabled = "disabled";
                if ((bool)dr["t442_estado"]) sChecked = "checked";
                sb.Append("<td align='center'><input type='checkbox' class='check' " + sChecked + " " + sEventoCheck + " " + sDisabled + "></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");


            return "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de Costes por Nivel.", ex);
        }
    }
    private string obtenerMaestroTarifasPerfil(string sTipo, string sIDProySubNodo, string sEstado)
    {
        string sResul = "";
        //int nPE = 0;
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr;
            bool? bEstado;

            //Si sEstado=T no restrinjo por estado de la PST, sino saco solo las activas
            if (sEstado == "T") bEstado = null;
            else bEstado = true;

            if (sTipo == "N") dr = PERFILSUPER.CatalogoPerfilesNodo_By_PSN(null, int.Parse(sIDProySubNodo));
            else dr = PERFILSUPER.CatalogoPerfilesCliente_By_PSN(null, int.Parse(sIDProySubNodo), bEstado);

            sb.Append("<table id='tblTarifas1' class='texto MANO' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:195px;' /><col style='width:75px;' /><col style='width:75px;' /></colgroup>");
            sb.Append("<tbody>");
            string sColor = "";
            while (dr.Read())
            {
                if ((bool)dr["t347_estado"]) sColor = ""; else sColor = ";color:Red;"; 
                sb.Append("<tr id='" + dr["t347_idperfilsuper"].ToString() + "' ondblclick='insertarTarifa(this, 1)' onmousedown='mm(event);DD(event);' style='height:20px" + sColor + "'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t347_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:right;padding-right:3px;'>" + decimal.Parse(dr["t347_imptarifahor"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:3px;'>" + decimal.Parse(dr["t347_imptarifajor"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de tarifas por perfil.", ex);
        }

        return sResul;
    }
    private string obtenerFigurasPSN(string sPestana, string sIDProySubNodo, string sNodoPSN)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = FIGURAPROYECTOSUBNODO.CatalogoFiguras(int.Parse(sIDProySubNodo));

            sb.Append("<TABLE id='tblFiguras2' class='texto MM' style='width: 420px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width: 10px' />");
            sb.Append("    <col style='width: 20px' />");
            sb.Append("    <col style='width: 290px;' />");
            sb.Append("    <col style='width: 100px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            int nUsuario = 0;
            bool bHayFilas = false;
            string sColor = "black";
            while (dr.Read())
            {
                bHayFilas = true;
                sbuilder.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "sFig:\"" + dr["t310_figura"].ToString() + "\"};");
                i++;
                sColor = "black";
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    if (dr["baja"].ToString() == "1") sColor = "red";
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:22px;color:" + sColor + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_denominacion"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sNodoPSN) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");
                    sb.Append(" onclick='mm(event)' onmousedown='DD(event);'>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'

                    //sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");
                    //Figuras
                    sb.Append("<td><div style='height:20px;'><ul id='box-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["t310_figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='J' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='M' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='B' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='S' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["t310_figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='J' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='M' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='B' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='S' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Secretaria' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            if (bHayFilas)
            {
                sb.Append("</ul></div></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            StringBuilder sb2 = new StringBuilder();

            i = 0;

            dr = FIGURAPROYECTOSUBNODO.CatalogoFigurasVirtuales(int.Parse(sIDProySubNodo));

            sb2.Append("<TABLE id='tblFiguras3' class='texto' style='width: 420px;'>");
            sb2.Append("<colgroup>");
            sb2.Append("    <col style='width: 10px' />");
            sb2.Append("    <col style='width: 20px' />");
            sb2.Append("    <col style='width: 290px;' />");
            sb2.Append("    <col style='width: 100px;' />");
            sb2.Append("</colgroup>");

            nUsuario = 0;
            bHayFilas = false;
            sColor = "black";

            while (dr.Read())
            {
                bHayFilas = true;
                i++;
                sColor = "black";
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb2.Append("</td>");
                        sb2.Append("</tr>");
                    }
                    if (dr["baja"].ToString() == "1") sColor = "red";
                    sb2.Append("<tr style='height:22px;color:" + sColor + "' ");
                    sb2.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");

                    if (dr["t303_denominacion"].ToString() == "") sb2.Append("tipo='E'");
                    else if (dr["t303_idnodo"].ToString() == sNodoPSN) sb2.Append("tipo='P'");
                    else sb2.Append("tipo='N'");

                    sb2.Append(" ondrop='return false;'>");

                    sb2.Append("<td></td>");
                    sb2.Append("<td></td>");
                    //sb2.Append("<td><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'
                    sb2.Append("<td style='padding-left:3px;'><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'

                    //Figuras
                    sb2.Append("<td>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb2.Append("<img src='../../../Images/imgDelegado.gif' title='Delegado' />"); break;
                        case "C": sb2.Append("<img src='../../../Images/imgColaborador.gif' title='Colaborador' />"); break;
                        case "J": sb2.Append("<img src='../../../Images/imgJefeProyecto.gif' title='Jefe' />"); break;
                        case "M": sb2.Append("<img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' />"); break;
                        case "B": sb2.Append("<img src='../../../Images/imgBitacorico.gif' title='Bitacórico' />"); break;
                        case "S": sb2.Append("<img src='../../../Images/imgSecretaria.gif' title='Asistente' />"); break;
                        case "I": sb2.Append("<img src='../../../Images/imgInvitado.gif' title='Invitado' />"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb2.Append("<img src='../../../Images/imgDelegado.gif' title='Delegado' />"); break;
                        case "C": sb2.Append("<img src='../../../Images/imgColaborador.gif' title='Colaborador' />"); break;
                        case "J": sb2.Append("<img src='../../../Images/imgJefeProyecto.gif' title='Jefe' />"); break;
                        case "M": sb2.Append("<img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' />"); break;
                        case "B": sb2.Append("<img src='../../../Images/imgBitacorico.gif' title='Bitacórico' />"); break;
                        case "S": sb2.Append("<img src='../../../Images/imgSecretaria.gif' title='Secretaria' />"); break;
                        case "I": sb2.Append("<img src='../../../Images/imgInvitado.gif' title='Invitado' />"); break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            if (bHayFilas)
            {
                sb2.Append("</td>");
                sb2.Append("</tr>");
            }
            sb2.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "///" + sbuilder.ToString() + "///" + sb2.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras.", ex);
        }
    }

    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre, string sNodoPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            //SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(sAp1, sAp2, sNombre, (bool)Session["FORANEOS"], int.Parse(sNodoPSN));
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(sAp1, sAp2, sNombre, true, int.Parse(sNodoPSN));

            sb.Append("<TABLE id='tblFiguras1' class='texto MAM' style='width: 400px;'>");
            sb.Append("<colgroup><col style='width: 20px;' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                //if (dr["t303_denominacion"].ToString() == "") sb.Append("tipo='E' ");
                //else if (dr["t303_idnodo"].ToString() == sNodoPSN) sb.Append("tipo='P' ");
                //else sb.Append("tipo='N' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append(" onclick='mm(event)' ondblclick='insertarFigura(this)' onmousedown='DD(event);'>");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W375' ondblclick='insertarFigura(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "]  hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W375' ondblclick='insertarFigura(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "]  hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string obtenerAnotaciones(string sPestana, string sIDProySubNodo)
    {
        try
        {
            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, int.Parse(sIDProySubNodo));
            return "OK@#@" + sPestana
                    + "@#@" + Utilidades.escape(oPSN.t305_modificaciones) + "///"
                    + Utilidades.escape(oPSN.t305_observaciones) + "///"
                    + Utilidades.escape(oPSN.t305_observacionesadm);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las anotaciones del proyectosubnodo", ex);
        }
    }
    private string obtenerPoolGF(string sPestana, string nPSN, string idNodo, string sLectura)
    {
        bool bLectura = false;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (sLectura == "true") bLectura = true;
            SqlDataReader dr = GrupoFun.CatalogoGrupos(int.Parse(idNodo));

            sb.Append("<table id='tblPool1' class='texto MAM' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width: 350px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IdGrupro"].ToString() + "'");
                if (!bLectura) sb.Append("onclick='mm(event)' ondblclick='insertarPoolGF(this)' onmousedown='DD(event);'");
                sb.Append(" style='height:16px'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["Nombre"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("|||");

            dr = POOL_GF_PSN.CatalogoGFdePSN(tr, int.Parse(nPSN));

            sb.Append("<table id='tblPool2' class='texto MM' style='width: 320px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:305px;' /><col /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t342_idgrupro"].ToString() + "' bd=''");
                if (!bLectura) sb.Append(" onclick='mm(event)' onmousedown='DD(event);'");
                sb.Append(" style='height:16px'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td>" + dr["t342_desgrupro"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de tarifas por perfil.", ex);
        }
    }
    private string obtenerCECDepartamentales(string sPestana, string nPSN, string idNodo, string sLectura)
    {
        bool bLectura = false;
        SqlDataReader dr = null;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (sLectura == "true") bLectura = true;

            //1º se obtienen los AE del Nodo
            dr = AE.CatalogoByUne(short.Parse(idNodo), "E", null);

            sb.Append("<table id='tblAECR' class='texto MAM' style='width: 210px;'>");
            sb.Append("<colgroup><col style='width:15px' /><col style='width:195px' /></colgroup>");
            sb.Append("<tbody>");
            string sObl = "";
            while (dr.Read())
            {
                sObl = ((bool)dr["t341_obligatorio"]) ? "1" : "0";
                sb.Append("<tr id='" + dr["t341_idae"].ToString() + "' cliente='" + dr["cod_cliente"].ToString() + "' obl='" + sObl + "' ");
                if (!bLectura) sb.Append(" onclick='mm(event);' ondblclick='asociarAE(this, true)' onmousedown='DD(event);' ");
                sb.Append(" style='height:16px' >");

                if ((bool)dr["t341_obligatorio"])
                    sb.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                else
                    sb.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                sb.Append("<td><nobr class='NBR W190'>" + dr["t341_nombre"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append("|||");

            //2º se obtienen los valores de los AE del Nodo
            dr = VAE.CatalogoByUne(int.Parse(idNodo), "E");

            int i = 0;
            while (dr.Read())
            {
                sb.Append("\taVAE_js[" + i.ToString() + "] = new Array(\"" + dr["t341_idae"].ToString() + "\",\"" + dr["t340_idvae"].ToString() + "\",\"" + dr["t340_valor"].ToString() + "\");\n");
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("|||");

            //3º se obtienen los AE asociados al PSN
            sb.Append("<table id='tblAET' class='texto MM' style='width: 380px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:15px' /><col style='width:247px' /><col style='width:108px' /></colgroup>");
            sb.Append("<tbody>");
            dr = AE.CatalogoByPSN(int.Parse(nPSN));
            while (dr.Read())
            {
                sObl = ((bool)dr["t341_obligatorio"]) ? "1" : "0";
                sb.Append("<tr id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl + "' bd=''");
                if (!bLectura) sb.Append(" onclick='mm(event);mostrarValoresAE(this);' onKeyUp=\"mfa(this,'U')\" onmousedown='DD(event);' ");
                sb.Append(" style='height:16px' >");

                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                if (bLectura)
                {
                    if ((bool)dr["t341_obligatorio"])
                        sb.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        sb.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    sb.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t340_valor"].ToString() + "</td></tr>");
                }
                else
                {
                    if ((bool)dr["t341_obligatorio"])
                        sb.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        sb.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    sb.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t340_valor"].ToString() + "</td></tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de criterios estadísticos departamentales.", ex);
        }
    }
    private string obtenerCECCorporativos(string sPestana, string nPSN, string idNodo, string sLectura)
    {
        bool bLectura = false;
        SqlDataReader dr = null;
        StringBuilder sb = new StringBuilder();
        try
        {
            if (sLectura == "true") bLectura = true;

            //1º se obtienen los AE del Nodo que estén activos
            dr = CEC.CatalogoCorporativosByNodo(int.Parse(idNodo), true);

            sb.Append("<table id='tblCEECR' class='texto MAM' style='width: 210px;'>");
            sb.Append("<colgroup><col style='width:15px' /><col style='width:195px' /></colgroup>");
            sb.Append("<tbody>");
            string sObl = "";
            while (dr.Read())
            {
                sObl = ((bool)dr["t381_obligatorio"]) ? "1" : "0";
                sb.Append("<tr id='" + dr["t345_idcec"].ToString() + "' obl='" + sObl + "' ");
                if (!bLectura) sb.Append(" onclick='mm(event);' ondblclick='asociarCEE(this, true)' onmousedown='DD(event);' ");
                sb.Append(" style='height:16px' >");

                if ((bool)dr["t381_obligatorio"])
                    sb.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                else
                    sb.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                sb.Append("<td><nobr class='NBR W190'>" + dr["t345_denominacion"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append("|||");

            //2º se obtienen los valores de los AE del Nodo
            dr = VCEC.CatalogoCorporativosByNodo(int.Parse(idNodo), true);

            int i = 0;
            while (dr.Read())
            {
                sb.Append("\taVCEE_js[" + i.ToString() + "] = new Array(\"" + dr["t345_idcec"].ToString() + "\",\"" + dr["t435_idvcec"].ToString() + "\",\"" + dr["t435_valor"].ToString() + "\");\n");
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("|||");

            //3º se obtienen los AE asociados al PSN
            sb.Append("<table id='tblCEET' class='texto MM' style='width: 380px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:15px' /><col style='width:247px' /><col style='width:108px' /></colgroup>");
            sb.Append("<tbody>");
            dr = CEC.CatalogoCorporativosByPSN(int.Parse(nPSN));
            while (dr.Read())
            {
                sObl = ((bool)dr["t381_obligatorio"]) ? "1" : "0";
                sb.Append("<tr id='" + dr["t345_idcec"].ToString() + "' vae='" + dr["t435_idvcec"].ToString() + "' obl='" + sObl + "' bd=''");
                if (!bLectura) sb.Append(" onclick='mm(event);mostrarValoresCEE(this);' onKeyUp=\"mfa(this,'U')\" onmousedown='DD(event);' ");
                sb.Append(" style='height:16px' >");

                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                if (bLectura)
                {
                    if ((bool)dr["t381_obligatorio"])
                        sb.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        sb.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    sb.Append("<td>" + dr["t345_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t435_valor"].ToString() + "</td></tr>");
                }
                else
                {
                    if ((bool)dr["t381_obligatorio"])
                        sb.Append("<td><img src='../../../images/imgIconoObl.gif' title='Obligatorio'></td>");
                    else
                        sb.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                    sb.Append("<td>" + dr["t345_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t435_valor"].ToString() + "</td></tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de criterios estadísticos corporativos.", ex);
        }
    }
    private string obtenerDocumentos(string sPestana, string nPSN, string sModoAcceso, string sEstadoProyecto)
    {
        string sCad;
        try
        {
            sCad = Utilidades.ObtenerDocumentos("PE", int.Parse(nPSN), sModoAcceso, sEstadoProyecto);
            return "OK@#@" + sPestana + "@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de documentos.", ex);
        }
    }
    private string obtenerPeriodificacion(string sPestana, string nPSN, string sLectura)
    {
        StringBuilder sb = new StringBuilder();
        string sColor = "";
        bool bLectura = false;
        bool bMesAbierto = false;
        double nRentabilidadContrato = 0;
        decimal nTotalContrato = 0, nPendienteContrato = 0;
        try
        {
            if (sLectura == "true") bLectura = true;
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerCatalogoPeriodificacion(int.Parse(nPSN));

            sb.Append("<table id='tblPeriodificacion' class='texto' style='width: 600px;text-align:right;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                if (dr["t325_estado"].ToString() == "A")
                {
                    bMesAbierto = true;
                    sColor = "green";
                }
                else
                {
                    bMesAbierto = false;
                    sColor = "red";
                }
                if (bLectura) bMesAbierto = false; //afecta al modo lectura pero no a los colores de los meses.

                sb.Append("<tr id='" + dr["t325_idsegmesproy"].ToString() + "' ");
                sb.Append("anomes='" + dr["t325_anomes"].ToString() + "' ");
                sb.Append("estado='" + dr["t325_estado"].ToString() + "' ");
                sb.Append("bd='' onclick='ms(this)' style='height:20px;' >");

                sb.Append("<td style='padding-left:2px;text-align:left;color:" + sColor + ";'>" + Fechas.AnnomesAFechaDescLarga((int)dr["t325_anomes"]) + "</td>");
                sb.Append("<td title='" + dr["dedicacion"].ToString() + "'>" + double.Parse(dr["dedicacion"].ToString()).ToString("N") + "</td>");
                //if (bMesAbierto) sb.Append("<td><input type='text' class='txtNumL' style='width:50px;' value='" + double.Parse(dr["dedicacion"].ToString()).ToString("N") + "' title='" + dr["dedicacion"].ToString() + "' onfocus='fn(this)' onChange='setPorcProd(this);' > %</td>");
                //else sb.Append("<td title='" + dr["dedicacion"].ToString() + "'>" + double.Parse(dr["dedicacion"].ToString()).ToString("N") + " %</td>");

                if (bMesAbierto) sb.Append("<td style='padding-right:5px;'><input type='text' class='txtNumL' style='width:85px;' value='" + decimal.Parse(dr["t325_prodperiod"].ToString()).ToString("N") + "' onfocus='fn(this)' onChange='setProduccion(this);' ></td>");
                else sb.Append("<td>" + decimal.Parse(dr["t325_prodperiod"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["resto_produccion_mes"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["total_produccion_mes"].ToString()).ToString("N") + "</td>");

                if (bMesAbierto) sb.Append("<td style='padding-right:5px;'><input type='text' class='txtNumL' style='width:85px;' value='" + decimal.Parse(dr["t325_consperiod"].ToString()).ToString("N") + "' onfocus='fn(this)' onChange='setConsumo(this);' ></td>");
                else sb.Append("<td>" + decimal.Parse(dr["t325_consperiod"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["resto_consumo_mes"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["total_consumo_mes"].ToString()).ToString("N") + "</td>");

                sb.Append("<td>" + decimal.Parse(dr["margen_mes"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='border-right:\"\"'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            //dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            dr = CONTRATO.ObtenerDatosEconomicos(int.Parse(nPSN));
            if (dr.Read())
            {
                nTotalContrato = decimal.Parse(dr["total_contrato"].ToString());
                nPendienteContrato = decimal.Parse(dr["pendiente_contrato"].ToString());
                nRentabilidadContrato = double.Parse(dr["rentabilidad_contrato"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "|||" + nTotalContrato.ToString("N") + "|||" + nPendienteContrato.ToString("N") + "|||" + nRentabilidadContrato.ToString("N");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la periodificación.", ex);
        }
    }
    protected string eliminarDocumentos(string strIdsDocs)
    {
        string sResul = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region eliminar documentos

            string[] aDocs = Regex.Split(strIdsDocs, "##");

            foreach (string oDoc in aDocs)
            {
                DOCUPE.Delete(tr, int.Parse(oDoc));
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string setTipologia(string sIDTipologia)
    {
        string sResul = "";

        try
        {
            int nFilas = 0;
            SqlDataReader dr = NATURALEZA.NaturalezasPorTipologia(int.Parse(sIDTipologia));
            while (dr.Read())
            {
                sResul = dr["t323_idnaturaleza"].ToString() + "@#@" + dr["denominacion"].ToString() + "@#@" + dr["t338_idplantilla"].ToString() + "@#@" + dr["t338_denominacion"].ToString();
                nFilas++;
                if (nFilas > 1)
                {
                    sResul = "";
                    break;
                }
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la naturaleza", ex);
        }
    }
    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        try
        {
            dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        finally
        {
            dr.Close();
            dr.Dispose();
        }
        return sResul;
    }
    private string obtenerProducido(string sIDProySubNodo)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.ObtenerConProduccion(null, int.Parse(sIDProySubNodo));
            sb.Append(oPSN.nProducidoReal.ToString("N"));

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener lo producido", ex);
        }

        return sResul;
    }

    private string setPerfilesATareas(string sIDProySubNodo)
    {
        try
        {
            PROYECTOSUBNODO.setPerfilesDefectoATareas(null, int.Parse(sIDProySubNodo));
            return "OK";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al asignar los perfiles por defecto a las tareas.", ex);
        }
    }
    private string addMesesProy(string nIdProySubNodo, string sDesde, string sHasta)
    {
        return SEGMESPROYECTOSUBNODO.InsertarSegMesProy(nIdProySubNodo, sDesde, sHasta);
    }

    private string eliminarMesProy(string nIdSegMesProy)
    {

        try
        {
            SEGMESPROYECTOSUBNODO.Delete(null, int.Parse(nIdSegMesProy));
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar un mes del proyecto.", ex);
        }
        return "OK";
    }

    private string refrescarInterlocutor(string nPSN)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, int.Parse(nPSN));
            sb.Append(((oPSN.t001_idficepi_interlocutor.HasValue) ? oPSN.t001_idficepi_interlocutor.ToString() : "") + "@#@"); 
            sb.Append(Utilidades.escape(oPSN.des_interlocutor));

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el interlocutor.", ex);
        }
    }

    private string verificarPerfilesBorrar(string sListaPerfiles)
    {
        int nCount = 0;
        bool bHayNoBorrables = false;
        StringBuilder sb = new StringBuilder();
        string sRes = "OK@#@";
        try
        {
            string[] aPerfiles = Regex.Split(sListaPerfiles, "///");
            foreach (string oPerfil in aPerfiles)
            {
                if (oPerfil != "")
                {
                    string[] aPerf = Regex.Split(oPerfil, "##");
                    if (aPerf[0] != "")
                    {
                        nCount = SUPER.Capa_Negocio.PERFILPROY.ComprobarBorrado(null, int.Parse(aPerf[0]));
                        if (nCount > 0)
                        {
                            bHayNoBorrables = true;
                            sb.Append(aPerf[0] + "##" + aPerf[1] + "///");
                        }
                    }
                }
            }
            if (bHayNoBorrables)
            {
                sRes += "AVISO@#@" + sb.ToString();
            }
            return sRes;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el interlocutor.", ex);
        }
    }
    private string verificarNivelesBorrar(string sListaNiveles)
    {
        int nCount = 0;
        bool bHayNoBorrables = false;
        StringBuilder sb = new StringBuilder();
        string sRes = "OK@#@";
        try
        {
            string[] aNiveles = Regex.Split(sListaNiveles, "///");
            foreach (string oNivel in aNiveles)
            {
                if (oNivel != "")
                {
                    string[] aNiv = Regex.Split(oNivel, "##");
                    if (aNiv[0] != "")
                    {
                        nCount = SUPER.Capa_Negocio.NIVELPSN.ComprobarBorrado(null, int.Parse(aNiv[0]));
                        if (nCount > 0)
                        {
                            bHayNoBorrables = true;
                            sb.Append(aNiv[0] + "##" + aNiv[1] + "///");
                        }
                    }
                }
            }
            if (bHayNoBorrables)
            {
                sRes += "AVISO@#@" + sb.ToString();
            }
            return sRes;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el interlocutor.", ex);
        }
    }

    #region pestaña Control
    private string obtenerControl(string sPestana, string nPSN, string nPE, string sLectura)
    {
        StringBuilder sbI = new StringBuilder();
        StringBuilder sbC = new StringBuilder();
        string sFecha = "";
        try
        {
            SqlDataReader dr = PEDIDOPROYECTO.SelectByT301_idproyecto(null, int.Parse(nPE));

            sbI.Append("<table id='tblPedidosIbermatica' class='texto' style='width: 460px;' mantenimiento='1'>");
            sbI.Append("<colgroup><col style='width:20px;' /><col style='width:120px;' /><col style='width:70px' /><col style='width:250px' /></colgroup>");
            sbI.Append("<tbody>");

            sbC.Append("<table id='tblPedidosCliente' class='texto' style='width: 460px;' mantenimiento='1'>");
            sbC.Append("<colgroup><col style='width:20px;' /><col style='width:120px;' /><col style='width:70px' /><col style='width:250px' /></colgroup>");
            sbC.Append("<tbody>");

            while (dr.Read())
            {
                if (sLectura == "true")
                {
                    if (dr["t480_tipopedido"].ToString() == "I")
                    {
                        sbI.Append("<tr id='" + dr["t480_idpedido"].ToString() + "' ");
                        sbI.Append("bd='' ");
                        sbI.Append("style='height:20px;'>");
                        sbI.Append("<td></td>");
                        sbI.Append("<td>" + dr["t480_pedido"].ToString() + "</td>");
                        if (dr["t480_fechapedido"].ToString() == "") sbI.Append("<td></td>");
                        else sbI.Append("<td>" + ((DateTime)dr["t480_fechapedido"]).ToShortDateString() + "</td>");
                        sbI.Append("<td>" + dr["t480_comentario"].ToString() + "</td>");
                        sbI.Append("</tr>");
                    }
                    else
                    {
                        sbC.Append("<tr id='" + dr["t480_idpedido"].ToString() + "' ");
                        sbC.Append("bd='' ");
                        sbC.Append("style='height:20px;'>");
                        sbC.Append("<td></td>");
                        sbC.Append("<td>" + dr["t480_pedido"].ToString() + "</td>");
                        if (dr["t480_fechapedido"].ToString() == "") sbC.Append("<td></td>");
                        else sbC.Append("<td>" + ((DateTime)dr["t480_fechapedido"]).ToShortDateString() + "</td>");
                        sbC.Append("<td>" + dr["t480_comentario"].ToString() + "</td>");
                        sbC.Append("</tr>");
                    }
                }
                else
                {
                    if (dr["t480_tipopedido"].ToString() == "I")
                    {
                        sbI.Append("<tr id='" + dr["t480_idpedido"].ToString() + "' ");
                        sbI.Append("bd='' ");
                        sbI.Append("onclick='ms(this);' style='height:20px;'>");
                        sbI.Append("<td><img src='../../../images/imgFN.gif'></td>");
                        sbI.Append("<td><input type='text' class='txtL' style='width:110px;' value='" + dr["t480_pedido"].ToString() + "' onkeypress='aG(5);fm(event);' maxlength='15' /></td>");

                        sFecha = dr["t480_fechapedido"].ToString();
                        if (sFecha != "") sFecha = ((DateTime)dr["t480_fechapedido"]).ToShortDateString();
                        if (Session["BTN_FECHA"].ToString() == "I")
                            sbI.Append("<td><input type='text' id='txtFecPedido" + dr["t480_idpedido"].ToString() + "' class='txtL' style='width:60px; cursor:pointer' value='" + sFecha + "' Calendar='oCal' onclick='mc(event);' onchange='aG(5);fm(event);' goma='1' readonly /></td>");
                        else
                            sbI.Append("<td><input type='text' id='txtFecPedido" + dr["t480_idpedido"].ToString() + "' class='txtL' style='width:60px; cursor:pointer' value='" + sFecha + "' Calendar='oCal' onchange='aG(5);fm(event);' goma='1' onfocus='focoFecha(event);' onmousedown='mc1(this)'/></td>");
                        sbI.Append("<td><input type='text' class='txtL' style='width:240px;' value='" + dr["t480_comentario"].ToString() + "' onkeypress='aG(5);fm(event);' maxlength='50' /></td>");
                        sbI.Append("</tr>");
                    }
                    else
                    {
                        sbC.Append("<tr id='" + dr["t480_idpedido"].ToString() + "' ");
                        sbC.Append("bd='' ");
                        sbC.Append("onclick='ms(this);' style='height:20px;'>");
                        sbC.Append("<td><img src='../../../images/imgFN.gif'></td>");
                        sbC.Append("<td><input type='text' class='txtL' style='width:110px;' value='" + dr["t480_pedido"].ToString() + "' onkeypress='aG(5);fm(event);' maxlength='15' /></td>");

                        sFecha = dr["t480_fechapedido"].ToString();
                        if (sFecha != "") sFecha = ((DateTime)dr["t480_fechapedido"]).ToShortDateString();
                        if (Session["BTN_FECHA"].ToString() == "I")
                            sbC.Append("<td><input type='text' id='txtFecPedido" + dr["t480_idpedido"].ToString() + "' class='txtL' style='width:60px; cursor:pointer' value='" + sFecha + "' Calendar='oCal' onclick='mc(event);' onchange='aG(5);fm(event);' goma='1' readonly /></td>");
                        else
                            sbC.Append("<td><input type='text' id='txtFecPedido" + dr["t480_idpedido"].ToString() + "' class='txtL' style='width:60px; cursor:pointer' value='" + sFecha + "' Calendar='oCal' onchange='aG(5);fm(event);' goma='1' onfocus='focoFecha(event);' onmousedown='mc1(this)'/></td>");

                        sbC.Append("<td><input type='text' class='txtL' style='width:240px;' value='" + dr["t480_comentario"].ToString() + "' onkeypress='aG(5);fm(event);' maxlength='50' /></td>");
                        sbC.Append("</tr>");
                    }
                }
            }
            dr.Close();
            dr.Dispose();

            sbI.Append("</tbody>");
            sbI.Append("</table>");
            sbC.Append("</tbody>");
            sbC.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sbI.ToString() + "|||" + sbC.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de pedidos.", ex);
        }
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Obtiene los datos generales de un proyecto ligados al soporte administrativo
    /// </summary>
    /// -----------------------------------------------------------------------------
    private string getDatosGenSA(string sPE)
    {
        StringBuilder sb = new StringBuilder();
        bool bExternalizable = false;
        try
        {
            //Datos de T301_PROYECTO
            SqlDataReader dr = PROYECTO.getSoporteAdministrativo(null, int.Parse(sPE));
            if (dr.Read())
            {
                bExternalizable = (bool)dr["t301_externalizable"];
                sb.Append((bExternalizable) ? "1///" : "0///");
                sb.Append(dr["t314_idusuario_SAT"].ToString() + "///");//1
                sb.Append(dr["denUsuario_SAT"].ToString() + "///");
                sb.Append(dr["t314_idusuario_SAA"].ToString() + "///");
                sb.Append(dr["denUsuario_SAA"].ToString() + "///");
                //Calendario
                sb.Append(dr["t066_idcal"].ToString() + "///");
                sb.Append(dr["t066_descal"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener datos del soporte administrativo.", ex);
        }
    }
    private string obtenerSoporteAdministrativo(string sPestana, string nPE, string nPSN)
    {
        StringBuilder sb = new StringBuilder();
        bool bExternalizable = false;
        string sDatosGen = "";
        try
        {
            sDatosGen = getDatosGenSA(nPE);
            string[] aDatos = Regex.Split(sDatosGen, @"///");
            if (aDatos[0] == "1")
                bExternalizable = true;

            sb.Append(sDatosGen);

            if (bExternalizable)
            {
                //Datos de T638_ESPACIOACUERDO
                SqlDataReader dr2 = ESPACIOACUERDO.Select1(null, int.Parse(nPE));
                if (dr2.Read())
                {
                    //Hay espacio de acuerdo
                    sb.Append(dr2["t638_idAcuerdo"].ToString() + "///");
                    //Tipo de facturación
                    sb.Append(((bool)dr2["t638_tipoIAP"]) ? "1///" : "0///");
                    sb.Append(((bool)dr2["t638_tiporesproy"]) ? "1///" : "0///");
                    sb.Append(((bool)dr2["t638_tipocliente"]) ? "1///" : "0///");
                    sb.Append(((bool)dr2["t638_tipoimpfijo"]) ? "1///" : "0///");
                    sb.Append(((bool)dr2["t638_tipootras"]) ? "1///" : "0///");
                    sb.Append(Utilidades.escape(dr2["t638_textootras"].ToString()) + "///");
                    //Factura
                    sb.Append(Utilidades.escape(dr2["t638_periodicidad"].ToString()) + "///");
                    sb.Append(Utilidades.escape(dr2["t638_aconsiderar"].ToString()) + "///");
                    if (ESPACIOACUERDO.TieneDocumentacion(null, int.Parse(nPE)))
                        sb.Append("S///");
                    else
                        sb.Append("N///");
                    //Conciliacion
                    sb.Append(((bool)dr2["t638_conciliacion"]) ? "1///" : "0///");
                    sb.Append(dr2["t638_tipoconciliacion"].ToString() + "///");
                    sb.Append(Utilidades.escape(dr2["t638_contacto"].ToString()) + "///");
                    //Confirmacion
                    sb.Append(dr2["t314_idusuario_findatos"].ToString() + "///");
                    if (dr2["t638_ffin"].ToString() != "")
                        sb.Append(((DateTime)dr2["t638_ffin"]).ToShortDateString() + "///");
                    else
                        sb.Append("///");
                    sb.Append(Utilidades.escape(dr2["denUsuarioFD"].ToString()) + "///");

                    sb.Append(dr2["t314_idusuario_aceptacion"].ToString() + "///");
                    if (dr2["t638_facept"].ToString() != "")
                        sb.Append(((DateTime)dr2["t638_facept"]).ToShortDateString() + "///");
                    else
                        sb.Append("///");
                    sb.Append(Utilidades.escape(dr2["denUsuarioA"].ToString()) + "///");

                    SqlDataReader dr3 = ESPACIOACUERDO.Catalogo2(null, int.Parse(nPE));
                    if (dr3.Read())
                        sb.Append("S///");
                    else
                        sb.Append("N///");
                    dr3.Close();
                    dr3.Dispose();
                    sb.Append(((bool)dr2["t638_facturaSA"]) ? "1///" : "0///");
                    //Denegación
                    sb.Append(dr2["t314_idusuario_denegacion"].ToString() + "///");
                    if (dr2["t638_fdeneg"].ToString() != "")
                        sb.Append(((DateTime)dr2["t638_fdeneg"]).ToShortDateString() + "///");
                    else
                        sb.Append("///");
                    sb.Append(Utilidades.escape(dr2["denUsuarioD"].ToString()));
                }
                else//No hay espacio de acuerdo 
                    sb.Append("///");
                dr2.Close();
                dr2.Dispose();
            }

            return "OK@#@" + sPestana + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener datos del soporte administrativo.", ex);
        }
    }
    public static string EnviarCorreoUsuarioSoporte(bool bAlta, string sTipo, int nPE, string denPE, int nUser)
    {
        string sResul = "";
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sb = new StringBuilder();
        string sAsunto = "", sTexto = "", sTipoUsuario = "Soporte Titular", sTO = "";

        try
        {
            sAsunto = "Soporte de administración de proyecto.";
            if (sTipo == "X")
            {
                sb.Append(@"<BR>SUPER le informa que tiene pendiente de aceptar una información para facturación para el soporte de administración del siguiente proyecto:<BR><BR>");
                sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(denPE) + "</b><br><br>");
                sb.Append(@"Si está de acuerdo con la información correspondiente, deberá pulsar el botón Aceptar de la información para facturación del detalle del proyecto económico (pestaña Control, subpestaña S. administrativo)<br><br>");
            }
            else
            {
                if (sTipo == "A")
                    sTipoUsuario = "Soporte Alternativo";
                if (bAlta)
                    sb.Append(@"<BR>SUPER le informa de su asignación como " + sTipoUsuario + " para el soporte administrativo del siguiente proyecto:<BR><BR>");
                else
                    sb.Append(@"<BR>SUPER le informa de su desasignación como " + sTipoUsuario + " para el soporte administrativo del siguiente proyecto:<BR><BR>");
                sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(denPE) + "</b><br><br>");
            }

            sTO = Recurso.CodigoRed(nUser);
            sTexto = sb.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            if (sTO != "") aListCorreo.Add(aMail);

            Correo.EnviarCorreos(aListCorreo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo a usuarios de soporte administrativo.", ex);
        }
        return sResul;
    }
    /// <summary>
    /// Envia un correo a Responsables, Delegados y Colaboradores del proyecto
    //indicando que se ha nombrado SAT y que es necesario cumplimentar el espacio de acuerdo
    /// </summary>
    public static string EnviarCorreosResponsablesSoporte(int iTipo, int nPSN, int nPE, string denPE, string sDenUSA, string sMensDenegacion)
    {
        string sResul = "";
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sb = new StringBuilder();
        string sAsunto = "", sTexto = "", sTO = "";

        try
        {
            switch (iTipo)
            {
                case 1:
                    sAsunto = "Soporte de administración de proyecto.";
                    sb.Append(@"<BR>SUPER le informa que " + sDenUSA + " ha sido nombrado como Soporte Titular para el soporte de administración del siguiente proyecto:<BR><BR>");
                    sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(denPE) + "</b><br><br>");
                    sb.Append(@"Y por tanto es necesario, si no lo ha hecho ya, rellenar la información para facturación correspondiente del detalle del proyecto económico (pestaña Control, subpestaña Info.Admva.)<br><br>");
                    break;
                case 3:
                    sAsunto = "Soporte de administración de proyecto.";
                    sb.Append(@"<BR>SUPER le informa que " + sDenUSA + " ha sido nombrado como Soporte Alternativo para el soporte de administración del siguiente proyecto:<BR><BR>");
                    sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(denPE) + "</b><br><br>");
                    break;
                case 2:
                    sAsunto = "Soporte de administración de proyecto.";
                    sb.Append(@"<BR>SUPER le informa que " + sDenUSA + " ha aceptado la información para facturación para el soporte de administración del siguiente proyecto:<BR><BR>");
                    sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(denPE) + "</b><br><br>");
                    break;
                case 4:
                    sAsunto = "Soporte de administración de proyecto.";
                    sb.Append(@"<BR>SUPER le informa que " + sDenUSA + "  ha denegado el soporte de administración del siguiente proyecto:<BR><BR>");
                    //sb.Append(@"<BR>SUPER le informa que no se ha aceptado el soporte de administración del siguiente proyecto:<BR><BR>");
                    sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(denPE) + "</b><br><br>");
                    if (sMensDenegacion != "")
                        sb.Append(@"Motivo de la denegación: " + sMensDenegacion + "<br><br>");
                    break;
                case 5:
                    sAsunto = "Soporte de administración de proyecto.";
                    sb.Append(@"<BR>SUPER le informa que " + sDenUSA + " ha eliminado el soporte de administración del siguiente proyecto:<BR><BR>");
                    //sb.Append(@"<BR>SUPER le informa que no se ha aceptado el soporte de administración del siguiente proyecto:<BR><BR>");
                    sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(denPE) + "</b><br><br>");
                    break;
            }
            sTO = flListaResponsable(nPSN);
            sTexto = sb.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            if (sTO != "") aListCorreo.Add(aMail);

            Correo.EnviarCorreos(aListCorreo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo a usuarios de soporte administrativo.", ex);
        }
        return sResul;
    }
    private static string flListaResponsable(int nIdPSN)
    {
        string sResul = "";
        try
        {
            SqlDataReader dr = PROYECTO.getMailResponsables(null, nIdPSN);
            while (dr.Read())
            {
                if (dr["MAIL"].ToString() != "")
                {
                    if (sResul != "") sResul += ";" + dr["MAIL"].ToString();
                    else sResul = dr["MAIL"].ToString();
                }
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al generar la lista de Responsables del proyectosubnodo. " + nIdPSN.ToString(), ex);
        }
        return sResul;
    }
    private string GrabarAcuerdo(string strDatosBasicos, string strSoporteAdm)
    {
        int nPE = -1, nPSN = -1, nIdAcuerdo = -1;
        string sDenPE, sResul = "OK";
        bool bEnviadoResp = false;
        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] aDatosBasicos = Regex.Split(strDatosBasicos, "##");
            ///aDatosBasicos[0] = Num. PE
            ///aDatosBasicos[1] = Denominación PE
            ///aDatosBasicos[2] = Id PSN
            ///aDatosBasicos[3] = Estado PE
            if (aDatosBasicos[0] != "") nPE = int.Parse(aDatosBasicos[0]);
            sDenPE = aDatosBasicos[1];
            if (aDatosBasicos[2] != "") nPSN = int.Parse(aDatosBasicos[2]);
            if (strSoporteAdm != "")
            {
                string[] aSoporte = Regex.Split(strSoporteAdm, "##");
                if (byte.Parse(aSoporte[0]) == 0)
                {
                    //Si no es externalizable -> Borrar el espacio de acuerdo
                    //ESPACIOACUERDO.Delete2(tr, nPE);
                    PROYECTO.UpdateSoporte(tr, nPE, 0, null, null, null);
                    //DOC_ACUERDO_PROY.DeleteByPE(tr, nPE);
                    if (aSoporte[1] != "")
                    {//Era externalizable y deja de serlo
                        //string sNombre = "";
                        //SqlDataReader dr = USUARIO.ObtenerDatosProfUsuario(int.Parse(Session["UsuarioActual"].ToString()));
                        //if (dr.Read())
                        //    sNombre = dr["PROFESIONAL"].ToString();
                        //dr.Close();
                        //dr.Dispose();
                        EnviarCorreosResponsablesSoporte(5, nPSN, nPE, sDenPE, Session["DES_EMPLEADO"].ToString(), "");
                    }
                }
                else
                {
                    #region externalizable
                    int? nSAT = null;
                    int? nSAA = null;
                    int? nIdCal = null;
                    if (aSoporte[1] != "") nSAT = int.Parse(aSoporte[1]);
                    if (aSoporte[2] != "") nSAA = int.Parse(aSoporte[2]);
                    if (aSoporte[14] != "") nIdCal = int.Parse(aSoporte[14]);
                    PROYECTO.UpdateSoporte(tr, nPE, byte.Parse(aSoporte[0]), nSAT, nSAA, nIdCal);
                    //Si ha habido algún cambio en los usuario de soporte se comunica por correo (tanto la asignación como la desasignación)
                    //SAT (titular)
                    if (aSoporte[3] != aSoporte[1])
                    {
                        //Si se pasa de no tener SAT a tenerlo, hay que enviar un correo a Responsables, Delegados y Colaboradores del proyecto
                        //indicando que se ha nombrado SAT y que es necesario cumplimentar el espacio de acuerdo
                        if (aSoporte[3] == "")
                        {
                            EnviarCorreoUsuarioSoporte(true, "T", nPE, sDenPE, int.Parse(aSoporte[1]));
                            EnviarCorreosResponsablesSoporte(1, nPSN, nPE, sDenPE, Utilidades.unescape(aSoporte[25]), "");
                            bEnviadoResp = true;
                        }
                        else
                        {
                            if (aSoporte[1] == "")
                            {//Era SAT y ha dejado de serlo
                                EnviarCorreoUsuarioSoporte(false, "T", nPE, sDenPE, int.Parse(aSoporte[3]));
                            }
                            else
                            {//Se ha cambiado de SAT -> notificación de alta al nuevo y de baja al viejo
                                EnviarCorreoUsuarioSoporte(true, "T", nPE, sDenPE, int.Parse(aSoporte[1]));
                                EnviarCorreoUsuarioSoporte(false, "T", nPE, sDenPE, int.Parse(aSoporte[3]));
                            }
                        }
                    }
                    //SAA (alternativo)
                    if (aSoporte[4] != aSoporte[2])
                    {
                        if (aSoporte[4] == "")
                        {//No había SAA y ahora si
                            EnviarCorreoUsuarioSoporte(true, "A", nPE, sDenPE, int.Parse(aSoporte[2]));
                            if (!bEnviadoResp)
                                EnviarCorreosResponsablesSoporte(3, nPSN, nPE, sDenPE, Utilidades.unescape(aSoporte[26]), "");
                        }
                        else
                        {
                            if (aSoporte[2] == "")
                            {//Era SAA y ha dejado de serlo
                                EnviarCorreoUsuarioSoporte(false, "A", nPE, sDenPE, int.Parse(aSoporte[4]));
                            }
                            else
                            {//Se ha cambiado de SAA -> notificación de alta al nuevo y de baja al viejo
                                EnviarCorreoUsuarioSoporte(true, "A", nPE, sDenPE, int.Parse(aSoporte[2]));
                                EnviarCorreoUsuarioSoporte(false, "A", nPE, sDenPE, int.Parse(aSoporte[4]));
                            }
                        }
                    }
                    #endregion
                    #region Espacio de acuerdo
                    //DateTime? dFAcept = null;
                    //if (aSoporte[21] != "") dFAcept = DateTime.Parse(aSoporte[21]);
                    DateTime? dFFin = null;
                    if (aSoporte[20] != "") dFFin = DateTime.Parse(aSoporte[20]);

                    //Si se había pedido aceptación del espacio de acuerdo -> INSERT
                    //Sino -> UPDATE
                    if (aSoporte[5] != "" && aSoporte[24] == "")
                    {
                        ESPACIOACUERDO.Update2(tr, int.Parse(aSoporte[5]), (aSoporte[6] == "1") ? true : false,
                                              (aSoporte[7] == "1") ? true : false,
                                              (aSoporte[8] == "1") ? true : false, (aSoporte[9] == "1") ? true : false,
                                              (aSoporte[10] == "1") ? true : false, Utilidades.unescape(aSoporte[11]),
                                              Utilidades.unescape(aSoporte[12]), Utilidades.unescape(aSoporte[13]),
                                              (aSoporte[15] == "1") ? true : false, (aSoporte[16] == "") ? null : aSoporte[16], Utilidades.unescape(aSoporte[17]),
                                              (aSoporte[18] != "") ? (int?)int.Parse(aSoporte[18]) : null, dFFin,
                                              (aSoporte[27] == "1") ? true : false);
                    }
                    else
                    {
                        nIdAcuerdo = ESPACIOACUERDO.Insert(tr, nPE, (aSoporte[6] == "1") ? true : false,
                                              (aSoporte[7] == "1") ? true : false,
                                              (aSoporte[8] == "1") ? true : false, (aSoporte[9] == "1") ? true : false,
                                              (aSoporte[10] == "1") ? true : false, Utilidades.unescape(aSoporte[11]),
                                              Utilidades.unescape(aSoporte[12]), Utilidades.unescape(aSoporte[13]),
                                              (aSoporte[15] == "1") ? true : false, (aSoporte[16] == "") ? null : aSoporte[16], Utilidades.unescape(aSoporte[17]),
                                             (aSoporte[18] != "") ? (int?)int.Parse(aSoporte[18]) : null, null, dFFin, null,
                                              (aSoporte[27] == "1") ? true : false, null, null);
                    }
                    //Miro si hay que enviar correos de confirmación
                    //Correo para pedir aceptación a los responsables del soporte administrativo
                    if (aSoporte[22] == "S")
                    {
                        EnviarCorreoUsuarioSoporte(true, "X", nPE, sDenPE, int.Parse(aSoporte[1]));
                        if (aSoporte[2] != "")
                            EnviarCorreoUsuarioSoporte(true, "X", nPE, sDenPE, int.Parse(aSoporte[2]));
                    }
                    //Correo para Aceptar al reponsable, Delegado y Colaboradores del proyecto
                    //Se envía al pulsar el botón Aceptar
                    //if (aSoporte[23] == "S")
                    //    EnviarCorreosResponsablesSoporte(2, nPSN, nPE, sDenPE, Session["DES_EMPLEADO"].ToString(), "");
                    #endregion
                }
                Conexion.CommitTransaccion(tr);
            }
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (Errores.EsErrorIntegridad(ex))
                sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al grabar la información para facturación.", ex, false);
            else
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar la información para facturación.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul + "@#@" + nIdAcuerdo.ToString();
    }
    private string GrabarConfirmacion(string strDatosBasicos, string strSoporteAdm, bool bAceptada)
    {
        int nPE = -1, nPSN = -1;
        string sDenPE, sResul = "OK@#@", sMensDenegacion = "", sKK = "1";
        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] aDatosBasicos = Regex.Split(strDatosBasicos, "##");
            ///aDatosBasicos[0] = Num. PE
            ///aDatosBasicos[1] = Denominación PE
            ///aDatosBasicos[2] = Id PSN
            ///aDatosBasicos[3] = Estado PE
            ///aDatosBasicos[4] = mensaje en caso de denegacion
            if (aDatosBasicos[0] != "") nPE = int.Parse(aDatosBasicos[0]);
            sDenPE = aDatosBasicos[1];
            if (aDatosBasicos[2] != "") nPSN = int.Parse(aDatosBasicos[2]);
            sKK = "2";
            if (strSoporteAdm != "")
            {
                string[] aSoporte = Regex.Split(strSoporteAdm, "##");
                if (byte.Parse(aSoporte[0]) != 0)
                {
                    DateTime? dFAux = null;
                    if (aSoporte[3] != "") dFAux = DateTime.Parse(aSoporte[3]);
                    //Miro si hay que enviar correos al reponsable, Delegado y Colaboradores del proyecto
                    //if (aSoporte[4] == "S")
                    if (bAceptada)
                    {
                        sKK = "3";
                        ESPACIOACUERDO.Aceptar(tr, int.Parse(aSoporte[1]), (aSoporte[2] != "") ? (int?)int.Parse(aSoporte[2]) : null, dFAux);
                        sKK = "4";
                        EnviarCorreosResponsablesSoporte(2, nPSN, nPE, sDenPE, Session["DES_EMPLEADO"].ToString(), "");
                        sKK = "5";
                    }
                    else
                    {//Se ha denegado el soporte
                        sKK = "6";
                        if (aDatosBasicos[4] != "") sMensDenegacion = Utilidades.unescape(aDatosBasicos[4]);
                        sKK = "7";
                        ESPACIOACUERDO.Denegar(tr, int.Parse(aSoporte[1]), (aSoporte[2] != "") ? (int?)int.Parse(aSoporte[2]) : null, dFAux, sMensDenegacion);
                        sKK = "8";
                        EnviarCorreosResponsablesSoporte(4, nPSN, nPE, sDenPE, Session["DES_EMPLEADO"].ToString(), sMensDenegacion);
                        sKK = "9";
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (Errores.EsErrorIntegridad(ex))
            {
                sResul = "Error@#@Operación rechazada.\n\nsKK=" + sKK + "\n\n" + Errores.mostrarError("Error al grabar la aceptación de la información para facturación.", ex, false);
            }
            else
            {
                sResul = "Error@#@\n\nsKK=" + sKK + "\n\n" + Errores.mostrarError("Error al grabar la aceptación de la información para facturación.", ex);
            }
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    /// <summary>
    /// Envia un correo a un USA con el texto que el usuario haya tecleado
    /// </summary>
    private string enviarCorreoCAUDEF(string strDatos)
    {
        string sResul = "";
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sb = new StringBuilder();
        string sAsunto = "", sTexto = "", sTO = "";

        try
        {
            string[] aDatos = Regex.Split(strDatos, "##");
            int nPE = int.Parse(aDatos[0]);

            sAsunto = "Petición de modificación de calendario de profesionales.";
            sb.Append(@"<BR>SUPER le informa que ");
            if (Session["APELLIDO2"].ToString() == "")
                sb.Append(Session["APELLIDO1"].ToString() + ", " + Session["NOMBRE"].ToString());
            else
                sb.Append(Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString());

            sb.Append(@" ha realizado una petición para el siguiente proyecto:<BR><BR>");
            sb.Append("<b>" + nPE.ToString("#,###") + @" - " + Utilidades.unescape(aDatos[1]) + "</b><br><br>");
            sb.Append("<b>Texto de la petición:</b><br><br>" + Utilidades.unescape(aDatos[2]));
            sb.Append("<br><br>");
            sTexto = sb.ToString();

            sTO = ConfigurationManager.AppSettings["CorreoDEF"].ToString();
            if (sTO != "")
            {
                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);
            }

            Correo.EnviarCorreos(aListCorreo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo a usuarios de soporte administrativo.", ex);
        }
        return sResul;
    }

    #endregion
}
