using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 0;
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "";
    public short nPantallaPreferencia = 27;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Consulta masiva de consumos por proyecto"; 
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                lblCDP.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                lblCSN1P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
                lblCSN2P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
                lblCSN3P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
                lblCSN4P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));

                if (!Utilidades.EstructuraActiva("SN4")) fstCSN4P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN3")) fstCSN3P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN2")) fstCSN2P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN1")) fstCSN1P.Style.Add("visibility", "hidden");

                if (Utilidades.EstructuraActiva("SN4")) nEstructuraMinima = 1;
                else if (Utilidades.EstructuraActiva("SN3")) nEstructuraMinima = 2;
                else if (Utilidades.EstructuraActiva("SN2")) nEstructuraMinima = 3;
                else if (Utilidades.EstructuraActiva("SN1")) nEstructuraMinima = 4;

                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                DateTime dHoy = DateTime.Now, dtAux;
                int nDias = dHoy.Day;
                dtAux = dHoy.AddDays(-nDias + 1);
                txtFechaInicio.Text = dtAux.ToShortDateString();
                dtAux = dtAux.AddMonths(1).AddDays(-1);
                txtFechaFin.Text = dtAux.ToShortDateString();

                hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();

                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                #region Lectura de preferencia o llegada desde el carrusel
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    sHayPreferencia = "true";
                    cboEstado.SelectedValue = aDatosPref[7];
                    cboCategoria.SelectedValue = aDatosPref[2];
                    cboCualidad.SelectedValue = aDatosPref[3];
                    chkCerrarAuto.Checked = (aDatosPref[4] == "1") ? true : false;
                    chkActuAuto.Checked = (aDatosPref[5] == "1") ? true : false;
                    //if (chkActuAuto.Checked) btnObtener.Disabled = true;
                    if (aDatosPref[6] == "1") rdbOperador.Items[0].Selected = true;
                    else rdbOperador.Items[1].Selected = true;
                    sSubnodos = aDatosPref[13];

                    if (chkActuAuto.Checked)
                    {
                        string strTabla = ObtenerProyectos(
                                            hdnDesde.Text,
                                            hdnHasta.Text,
                                            "7",
                                            cboEstado.SelectedValue,
                                            cboCategoria.SelectedValue,
                                            cboCualidad.SelectedValue,
                                            strIDsProyecto,
                                            strIDsCliente,
                                            strIDsResponsable,
                                            strIDsNaturaleza,
                                            strIDsHorizontal,
                                            strIDsModeloCon,
                                            strIDsContrato,
                                            sSubnodos,
                                            strIDsSector,
                                            strIDsSegmento,
                                            aDatosPref[6],
                                            strIDsQn,
                                            strIDsQ1,
                                            strIDsQ2,
                                            strIDsQ3,
                                            strIDsQ4
                                            );

                        string[] aTabla = Regex.Split(strTabla, "@#@");

                        if (aTabla[0] == "OK")
                        {
                            this.strTablaHTML = aTabla[1];
                        }
 
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                }
                else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                #endregion

                string[] aCriterios = Regex.Split(cargarCriterios(int.Parse(hdnDesde.Text), int.Parse(hdnHasta.Text)), "@#@");
                if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
                else Master.sErrores = aCriterios[1];

            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
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
            case ("buscar"):
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21], aArgs[22]);
                break;
            case ("getExcel"):
                sResultado += ObtenerDatosConsumos(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
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
    //private string ObtenerProyectos(string sModulo, string sNodo, string sEstado, string sCategoria, string sIdCliente, string sIdResponsable, string sNumPE, string sDesPE, string sTipoBusqueda, string sCualidad, string sIDContrato, string sIdHorizontal)

    private string ObtenerProyectos(string sDesde, string sHasta, string sNivelEstructura,
                                string sEstado, string sCategoria, string sCualidad, string sProyectos, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P)

    {
        string sResul = "";
        bool bError = false;
        try
        {
            if (!Utilidades.isNumeric(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isNumeric(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                SqlDataReader dr = PROYECTO.ObtenerMasivoProyectos(
                                            (int)Session["UsuarioActual"],
                                            int.Parse(sDesde),
                                            int.Parse(sHasta),
                                            (sNivelEstructura == "0") ? null : (int?)int.Parse(sNivelEstructura),
                                            sEstado,
                                            sCategoria,
                                            sCualidad,
                                            sProyectos,
                                            sClientes,
                                            sResponsables,
                                            sNaturalezas,
                                            sHorizontal,
                                            sModeloContrato,
                                            sContrato,
                                            sIDEstructura,
                                            sSectores,
                                            sSegmentos,
                                            (sComparacionLogica == "1") ? true : false,
                                            sCNP,
                                            sCSN1P,
                                            sCSN2P,
                                            sCSN3P,
                                            sCSN4P,
                                            false
                                            );
                sb.Append("<table id='tblDatos' class='texto MAM' style='width: 450px;'>" + (char)10);
                sb.Append("<colgroup>" + (char)10);
                sb.Append("<col style='width:20px' />");
                sb.Append("<col style='width:20px' />");
                sb.Append("<col style='width:20px' />");
                sb.Append("<col style='width:50px;' />");
                sb.Append("<col style='width:340px;' />");
                sb.Append("</colgroup>" + (char)10);

                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "'");
                    sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                    sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                    sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");

                    //sb.Append("ondblclick='insertarItem(this)' onclick='mmse(this)' onmousedown='DD(this)' ");
                    sb.Append("style='height:20px'>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td style='text-align:right; padding-right:10px;'");
                    if (ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
                        sb.Append(" title=\"" + dr["desmotivo"].ToString() + "\"");
                    sb.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td><span class='NBR W300' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</span></td>");
                    sb.Append("</tr>" + (char)10);
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</table>");

                sResul = "OK@#@" + sb.ToString();
            }
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer obtenerProyectos ", objError);
        }
        return sResul;
    }
    private string ObtenerDatosConsumos(string sDesde, string sHasta, string sProyectos)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;
        DateTime dtDesde, dtHasta;
        bool bError = false;

        try
        {
            //SqlDataReader dr = Proyecto.ExportarProyectos(sDesde, sHasta, int.Parse(Session["UsuarioActual"].ToString()), Session["PerfilActual"].ToString(), int.Parse(Session["CRActual"].ToString()), sProyectos);

            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                dtDesde = System.Convert.ToDateTime(sDesde);
                dtHasta = System.Convert.ToDateTime(sHasta);

                SqlDataReader dr = PROYECTO.GetConsumosProy(dtDesde, dtHasta, sProyectos);
                sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
                sb.Append("<tr align=center style='background-color: #BCD4DF;'>");
                sb.Append("<td>F. consumo</td>");
                sb.Append("<td>Nº profesional</td>");
                sb.Append("<td>Profesional</td>");
                //Petición Yolanda 16/05/2016
                sb.Append("<td>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " del profesional</td>");
                sb.Append("<td>Proveedor</td>");

                sb.Append("<td>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " del proyecto</td>");
                sb.Append("<td>Nº proyecto</td>");
                sb.Append("<td>Denominación proyecto</td>");
                sb.Append("<td>Denominación proyecto técnico</td>");
                sb.Append("<td>Nº tarea</td>");
                sb.Append("<td>Denominación tarea</td>");
                sb.Append("<td>Facturable</td>");
                sb.Append("<td>Horas</td>");
                sb.Append("<td>Jornadas</td>");
                sb.Append("<td>Comentario</td>");
                sb.Append("<td>Cliente</td>");
                sb.Append("<td>Naturaleza</td>");
                sb.Append("<td>Modelo de contratación</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' border=1>");
                sb.Append("<COLGROUP>");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");//facturable
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("</COLGROUP>");
                while (dr.Read())
                {
                    sb.Append("<tr style='vertical-align:top;'>");
                    sb.Append("<td>" + ((DateTime)dr["t337_fecha"]).ToShortDateString() + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["profesional"].ToString() + "</td>");

                    //CR del profesional
                    sb.Append("<td>" + dr["NodoUsuario"].ToString() + "</td>");
                    //proveedor del profesional
                    sb.Append("<td>" + dr["Proveedor"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</td>");
                    //CR del proyecto
                    sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");

                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["t301_denominacion"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</td>");
                    sb.Append("<td>" + dr["T331_despt"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t332_idtarea"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["t332_destarea"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</td>");
                    if ((bool)dr["t332_facturable"])
                        sb.Append("<td style='text-align:center;'>X</td>");
                    else
                        sb.Append("<td>&nbsp;</td>");

                    sb.Append("<td style='text-align:rigth;'>" + decimal.Parse(dr["TotalHorasReportadas"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + decimal.Parse(dr["TotalJornadasReportadas"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td>" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</td>");
                    sb.Append("<td>" + dr["t302_denominacion"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</td>");
                    sb.Append("<td>" + dr["t323_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t316_denominacion"].ToString() + "</td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");

                //sResul = "OK@#@" + sb.ToString();
                string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
                Session[sIdCache] = sb.ToString(); ;

                sResul = "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); ;
                sb.Length = 0; //Para liberar memoria

                return sResul;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta masiva de proyectos", ex);
        }

        return sResul;

    }
    private string ObtenerDatosConsumos_New(string sDesde, string sHasta, string sProyectos)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;
        DateTime dtDesde, dtHasta;
        bool bError = false;

        try
        {
            //SqlDataReader dr = Proyecto.ExportarProyectos(sDesde, sHasta, int.Parse(Session["UsuarioActual"].ToString()), Session["PerfilActual"].ToString(), int.Parse(Session["CRActual"].ToString()), sProyectos);

            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError && sProyectos=="")
            {
                sResul = "Error@#@Debe seleccionar algún proyecto";
                bError = true;
            }
            if (!bError)
            {
                dtDesde = System.Convert.ToDateTime(sDesde);
                dtHasta = System.Convert.ToDateTime(sHasta);

                //HttpContext.Current.Cache.Remove("CONSPROYMASIVO_" + Session["IDFICEPI_ENTRADA"].ToString());

                DataSet ds = PROYECTO.GetConsumosProyDS(dtDesde, dtHasta, sProyectos);
                
                HttpContext.Current.Cache.Insert("CONSPROYMASIVO_"+Session["IDFICEPI_ENTRADA"].ToString(), ds, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
                //Ver si se puede destruir el dataset aquí
                //ds.Dispose();

                sResul = "OK@#@" + sb.ToString();

                return sResul;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta masiva de proyectos", ex);
        }

        return sResul;

    }

    private string cargarCriterios(int nDesde, int nHasta)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        try
        {
            /*
             * t -> tipo
             * c -> codigo
             * d -> denominacion
             * ///datos auxiliares para el catálogo de proyecto (16)
             * a -> categoria
             * u -> cualidad
             * e -> estado
             * l -> cliente
             * n -> nodo
             * r -> responsable
             * */
            SqlDataReader dr = ConsultasPSP.ObtenerProyectos((int)Session["UsuarioActual"], nDesde, nHasta, Constantes.nNumElementosMaxCriterios);
            while (dr.Read())
            {
                if ((int)dr["codigo"] == -1)
                    sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"excede\":1};\n");
                else
                {
                    if ((int)dr["tipo"] == 16)
                        sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\",\"p\":\"" + dr["t301_idproyecto"].ToString() + "\",\"a\":\"" + dr["t301_categoria"].ToString() + "\",\"u\":\"" + dr["t305_cualidad"].ToString() + "\",\"e\":\"" + dr["t301_estado"].ToString() + "\",\"l\":\"" + dr["t302_denominacion"].ToString() + "\",\"n\":\"" + dr["t303_denominacion"].ToString() + "\",\"r\":\"" + dr["Responsable"].ToString() + "\"};\n");
                    else
                        sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\"};\n");
                }
                i++;
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }

    private string setPreferencia(string sEstado, string sCategoria, string sCualidad, string sCerrarAuto, string sActuAuto, string sOperadorLogico, string sValoresMultiples)
    {
        string sResul = "";

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
            int nPref = PREFERENCIAUSUARIO.Insertar(tr,
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 27,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        //(sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        (sEstado == "") ? null : sEstado,
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            #region Valores Múltiples
            if (sValoresMultiples != "")
            {
                string[] aValores = Regex.Split(sValoresMultiples, "///");
                foreach (string oValor in aValores)
                {
                    if (oValor == "") continue;
                    string[] aDatos = Regex.Split(oValor, "##");
                    ///aDatos[0] = concepto
                    ///aDatos[1] = idValor
                    ///aDatos[2] = denominacion

                    PREFUSUMULTIVALOR.Insertar(tr, nPref, byte.Parse(aDatos[0]), aDatos[1], Utilidades.unescape(aDatos[2]));
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + nPref.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string delPreferencia()
    {
        try
        {
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 27);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar la preferencia", ex);
        }
    }
    private string getPreferencia(string sIdPrefUsuario)
    {
        StringBuilder sb = new StringBuilder();
        int idPrefUsuario = 0;
        string sEstado = "";
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 27);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["categoria"].ToString() + "@#@"); //2
                sb.Append(dr["cualidad"].ToString() + "@#@"); //3
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //4
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //5
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //6
                sb.Append(dr["estado"].ToString() + "@#@"); //7
                sEstado = dr["estado"].ToString();
                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
            }
            dr.Close();
            //dr.Dispose();

            sb.Append("0@#@");//8
            //sb.Append("0@#@");//9
            //sb.Append("0@#@");//10
            //sb.Append("0@#@");//11
            //sb.Append("0@#@");//12
            DateTime dHoy = DateTime.Now, dtAux;
            int nDias = dHoy.Day;
            dtAux = dHoy.AddDays(-nDias + 1);
            txtFechaInicio.Text = dtAux.ToShortDateString();
            dtAux = dtAux.AddMonths(1).AddDays(-1);
            txtFechaFin.Text = dtAux.ToShortDateString();

            hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
            hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();

            sb.Append(hdnDesde.Text + "@#@");//9
            sb.Append(txtFechaInicio.Text + "@#@");//10
            sb.Append(hdnHasta.Text + "@#@");//11
            sb.Append(txtFechaFin.Text + "@#@");//12


            #region HTML, IDs
            int nNivelMinimo = 0;
            bool bAmbito = false;
            string[] aID = null;
            dr = PREFUSUMULTIVALOR.Obtener(null, idPrefUsuario);
            while (dr.Read())
            {
                switch (int.Parse(dr["t441_concepto"].ToString()))
                {
                    case 1:
                        if (!bAmbito)
                        {
                            bAmbito = true;
                            nNivelMinimo = 6;
                        }
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (int.Parse(aID[0]) < nNivelMinimo) nNivelMinimo = int.Parse(aID[0]);

                        if (strIDsAmbito != "") strIDsAmbito += ",";
                        strIDsAmbito += aID[1];

                        aSubnodos = PREFUSUMULTIVALOR.SelectSubnodosAmbito(null, aSubnodos, int.Parse(aID[0]), int.Parse(aID[1]));
                        strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:16px;' idAux='";
                        strHTMLAmbito += SUBNODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLAmbito += "'><td>";

                        switch (int.Parse(aID[0]))
                        {
                            case 1: strHTMLAmbito += "<img src='../../../../../images/imgSN4.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case 2: strHTMLAmbito += "<img src='../../../../../images/imgSN3.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case 3: strHTMLAmbito += "<img src='../../../../../images/imgSN2.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case 4: strHTMLAmbito += "<img src='../../../../../images/imgSN1.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case 5: strHTMLAmbito += "<img src='../../../../../images/imgNodo.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case 6: strHTMLAmbito += "<img src='../../../../../images/imgSubNodo.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLAmbito += "<nobr class='NBR W230'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 2:
                        if (strIDsResponsable != "") strIDsResponsable += ",";
                        strIDsResponsable += dr["t441_valor"].ToString();
                        strHTMLResponsable += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 3:
                        if (strIDsNaturaleza != "") strIDsNaturaleza += ",";
                        strIDsNaturaleza += dr["t441_valor"].ToString();
                        strHTMLNaturaleza += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 4:
                        if (strIDsModeloCon != "") strIDsModeloCon += ",";
                        strIDsModeloCon += dr["t441_valor"].ToString();
                        strHTMLModeloCon += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 5:
                        if (strIDsHorizontal != "") strIDsHorizontal += ",";
                        strIDsHorizontal += dr["t441_valor"].ToString();
                        strHTMLHorizontal += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 6: if (strIDsSector != "") strIDsSector += ",";
                        strIDsSector += dr["t441_valor"].ToString();
                        strHTMLSector += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 7: if (strIDsSegmento != "") strIDsSegmento += ",";
                        strIDsSegmento += dr["t441_valor"].ToString();
                        strHTMLSegmento += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 8:
                        if (strIDsCliente != "") strIDsCliente += ",";
                        strIDsCliente += dr["t441_valor"].ToString();
                        strHTMLCliente += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 9:
                        if (strIDsContrato != "") strIDsContrato += ",";
                        strIDsContrato += dr["t441_valor"].ToString();
                        strHTMLContrato += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 10:
                        if (strIDsQn != "") strIDsQn += ",";
                        strIDsQn += dr["t441_valor"].ToString();
                        strHTMLQn += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 11:
                        if (strIDsQ1 != "") strIDsQ1 += ",";
                        strIDsQ1 += dr["t441_valor"].ToString();
                        strHTMLQ1 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 12:
                        if (strIDsQ2 != "") strIDsQ2 += ",";
                        strIDsQ2 += dr["t441_valor"].ToString();
                        strHTMLQ2 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 13:
                        if (strIDsQ3 != "") strIDsQ3 += ",";
                        strIDsQ3 += dr["t441_valor"].ToString();
                        strHTMLQ3 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    case 14:
                        if (strIDsQ4 != "") strIDsQ4 += ",";
                        strIDsQ4 += dr["t441_valor"].ToString();
                        strHTMLQ4 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><span class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                    //case 15: //strMagnitudes  break;
                    case 16:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsProyecto != "") strIDsProyecto += ",";
                        strIDsProyecto += aID[0];

                        strHTMLProyecto += "<tr id='" + aID[0] + "' style='height:16px;' ";
                        strHTMLProyecto += "categoria='" + aID[1] + "' ";
                        strHTMLProyecto += "cualidad='" + aID[2] + "' ";
                        strHTMLProyecto += "estado='" + aID[3] + "'><td>";

                        if (aID[1] == "P") strHTMLProyecto += "<img src='../../../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";
                        else strHTMLProyecto += "<img src='../../../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";

                        switch (aID[2])
                        {
                            case "C": strHTMLProyecto += "<img src='../../../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "J": strHTMLProyecto += "<img src='../../../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        switch (aID[3])
                        {
                            case "A": strHTMLProyecto += "<img src='../../../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "C": strHTMLProyecto += "<img src='../../../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "H": strHTMLProyecto += "<img src='../../../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLProyecto += "<span class='NBR W185' style='margin-left:10px;' onmouseover='TTip(event)'>" + dr["t441_denominacion"].ToString() + "</span></td></tr>";
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion

            for (int i = 0; i < aSubnodos.Count; i++)
            {
                if (i > 0) sSubnodos += ",";
                sSubnodos += aSubnodos[i];
            }

            sb.Append(sSubnodos + "@#@"); //12
            sb.Append(strHTMLAmbito + "@#@"); //13
            sb.Append(strIDsAmbito + "@#@"); //14
            sb.Append(strHTMLResponsable + "@#@"); //15
            sb.Append(strIDsResponsable + "@#@"); //16
            sb.Append(strHTMLNaturaleza + "@#@"); //17
            sb.Append(strIDsNaturaleza + "@#@"); //18
            sb.Append(strHTMLModeloCon + "@#@"); //19
            sb.Append(strIDsModeloCon + "@#@"); //20
            sb.Append(strHTMLHorizontal + "@#@"); //21
            sb.Append(strIDsHorizontal + "@#@"); //22
            sb.Append(strHTMLSector + "@#@"); //23
            sb.Append(strIDsSector + "@#@"); //24
            sb.Append(strHTMLSegmento + "@#@"); //25
            sb.Append(strIDsSegmento + "@#@"); //26
            sb.Append(strHTMLCliente + "@#@"); //27
            sb.Append(strIDsCliente + "@#@"); //28
            sb.Append(strHTMLContrato + "@#@"); //29
            sb.Append(strIDsContrato + "@#@"); //30
            sb.Append(strHTMLQn + "@#@"); //31
            sb.Append(strIDsQn + "@#@"); //32
            sb.Append(strHTMLQ1 + "@#@"); //33
            sb.Append(strIDsQ1 + "@#@"); //34
            sb.Append(strHTMLQ2 + "@#@"); //35
            sb.Append(strIDsQ2 + "@#@"); //36
            sb.Append(strHTMLQ3 + "@#@"); //37
            sb.Append(strIDsQ3 + "@#@"); //38
            sb.Append(strHTMLQ4 + "@#@"); //39
            sb.Append(strIDsQ4 + "@#@"); //40
            sb.Append(strHTMLProyecto + "@#@"); //41
            sb.Append(strIDsProyecto + "@#@"); //42
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
}
