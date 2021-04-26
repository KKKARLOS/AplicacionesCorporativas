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
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 0;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto="";
    public short nPantallaPreferencia = 14;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (!(bool)Session["FICHAECO1024"])
                {
                    Master.nResolucion = 1280;
                }
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Ficha económica";
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");

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

                hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();
                hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

                lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
                lblMonedaImportes2.InnerText = Session["DENOMINACION_VDC"].ToString();

                //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                //{
                    divMonedaImportes.Style.Add("visibility", "visible");
                    divMonedaImportes2.Style.Add("visibility", "visible");
                //}

                if ((bool)Session["CALCULOONLINE"])
                {
                    rdbResultadoCalculo.Items[0].Selected = true;
                    rdbResultadoCalculo2.Items[0].Selected = true;
                }
                else
                {
                    rdbResultadoCalculo.Items[1].Selected = true;
                    rdbResultadoCalculo2.Items[1].Selected = true;
                }

                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                #region Lectura de preferencia
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    sHayPreferencia = "true";
                    cboCategoria.SelectedValue = aDatosPref[2];
                    cboCualidad.SelectedValue = aDatosPref[3];
                    chkCerrarAuto.Checked = (aDatosPref[4] == "1") ? true : false;
                    chkActuAuto.Checked = (aDatosPref[5] == "1") ? true : false;
                    //if (chkActuAuto.Checked) btnObtener.Disabled = true;
                    if (aDatosPref[6] == "1") rdbOperador.Items[0].Selected = true;
                    else rdbOperador.Items[1].Selected = true;

                    nUtilidadPeriodo = int.Parse(aDatosPref[7]);
                    hdnDesde.Text = aDatosPref[8];
                    txtDesde.Text = aDatosPref[9];
                    hdnHasta.Text = aDatosPref[10];
                    txtHasta.Text = aDatosPref[11];
                    sSubnodos = aDatosPref[12];

                    if (chkActuAuto.Checked)
                    {
                        string strTabla = obtenerDatos(hdnDesde.Text,
                                            hdnHasta.Text,
                                            "7",
                                            cboCategoria.SelectedValue,
                                            cboCualidad.SelectedValue,
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
                                            strIDsQ4,
                                            strIDsProyecto);
                            
                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] == "OK")
                        {
                            divTablaTitulo.InnerHtml = aTabla[1];
                            strTablaHTML = aTabla[2];
                            divResultadoTotales.InnerHtml = aTabla[3];
                        }
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                }
                else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                #endregion

                string[] aCriterios = Regex.Split(cargarCriterios(int.Parse(hdnDesde.Text), int.Parse(hdnHasta.Text)), "@#@");
                if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
                else Master.sErrores = aCriterios[1];

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
            case ("buscar"):
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21]);
                break;
            case ("getTablaCriterios"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
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
            case ("setResultadoOnline"):
                Session["CALCULOONLINE"] = (aArgs[1] == "1") ? true : false;
                sResultado += "OK";
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
            SqlDataReader dr = ConsultasPGE.ObtenerCombosDatosResumidosCriterios((int)Session["UsuarioActual"], nDesde, nHasta, Constantes.nNumElementosMaxCriterios);
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

            return "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }

    private string obtenerDatos(string sDesde, string sHasta, string sNivelEstructura,
                                string sCategoria, string sCualidad, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P, string sPSN)
    {

        int nWidthTabla = 470; //370+100
        int nColumnasACrear = 0;
        StringBuilder sb = new StringBuilder();
        StringBuilder sbColgroupTitulo = new StringBuilder();
        StringBuilder sbTitulo = new StringBuilder();
        StringBuilder sbResultado = new StringBuilder();
        string sTablaTitulo = "";
        string sTablaContenido = "";
        string sTablaResultado = "";
        decimal nRatio = 0;

        string sColor = "";
        try
        {
            sbColgroupTitulo.Append("<colgroup>");
            sbColgroupTitulo.Append("<col style='width:350px;' />");
            sbColgroupTitulo.Append("<col style='width:120px;' />");

            DataSet ds = ConsultasPGE.ObtenerDatosFichaEconomica((int)Session["UsuarioActual"],
                int.Parse(sDesde), int.Parse(sHasta),
                (sNivelEstructura=="0")? null:(int?)int.Parse(sNivelEstructura),
                sCategoria,
                sCualidad,
                sClientes,
                sResponsables,
                sNaturalezas,
                sHorizontal,
                sModeloContrato,
                sContrato,
                sIDEstructura,
                sSectores,
                sSegmentos,
                (sComparacionLogica=="1")? true:false,
                sCNP,
                sCSN1P,
                sCSN2P,
                sCSN3P,
                sCSN4P,
                sPSN,
                Session["MONEDA_VDC"].ToString()
                );

            bool bTitulos = false;
            int i = 0;
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if (!bTitulos)
                {
                    sbTitulo.Append("<tr class='TBLINI'>");
                    sbTitulo.Append("<td></td>");
                    for (int x = 4; x < ds.Tables[0].Columns.Count; x++)
                    {
                        if (x == 4)
                        {
                            string[] aNomMeses = Regex.Split(ds.Tables[0].Columns[x].ColumnName, "-");
                            sbTitulo.Append("<td>");
                            sbTitulo.Append(Fechas.AnnomesAFechaDescCorta(int.Parse(aNomMeses[0].ToString())));
                            sbTitulo.Append(" - ");
                            sbTitulo.Append(Fechas.AnnomesAFechaDescCorta(int.Parse(aNomMeses[1].ToString())));
                            sbTitulo.Append("</td>");
                        }
                        else
                        {
                            sbColgroupTitulo.Append("<col style='width:100px;' />");
                            sbTitulo.Append("<td>" + Fechas.AnnomesAFechaDescLarga(int.Parse(ds.Tables[0].Columns[x].ColumnName)) + "</td>");
                            nColumnasACrear++;
                        }
                    }
                    sbTitulo.Append("</tr>");
                    bTitulos = true;
                }

                if (oFila["t454_idformula"].ToString() != "3"
                    && oFila["t454_idformula"].ToString() != "1"
                    && oFila["t454_idformula"].ToString() != "2"
                    && oFila["t454_idformula"].ToString() != "8"
                    )
                {
                    switch (oFila["nivel"].ToString())
                    {
                        case "1":
                            sb.Append("<tr id='" + oFila["t454_idformula"].ToString() + "' ");
                            //sb.Append(" style='display:block; height: 20px' nivel='1' desplegado='1'>");
                            sb.Append(" style='display:table-row; height: 20px' nivel='1' desplegado='1'>");
                            if (oFila["t454_idformula"].ToString() != "5") sb.Append("<td style='text-align:left;'><IMG class=NSEG1 onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                            else sb.Append("<td style='text-align:left;'><IMG class=NSEG1 src='../../../../images/imgSeparador.gif' style='width:9px;'>");

                            sb.Append("<nobr class='NBR' style='width:320px;'>" + oFila["t454_literal"].ToString() + "</nobr></td>");
                            break;
                        case "2":
                            sb.Append("<tr id='" + oFila["t454_idformula"].ToString() + "' ");
                            sb.Append(" style='display:none; height: 20px' nivel='2' desplegado='1'>");
                            if (oFila["t454_idformula"].ToString() == "33" || oFila["t454_idformula"].ToString() == "34") sb.Append("<td style='text-align:left;'><IMG class=NSEG2 onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                            else sb.Append("<td style='text-align:left;'><IMG class=NSEG2 src='../../../../images/imgSeparador.gif' style='width:9px;'>");

                            sb.Append("<nobr class='NBR' style='width:300px;'>" + oFila["t454_literal"].ToString() + "</nobr></td>");
                            break;
                        case "3":
                            sb.Append("<tr id='" + oFila["t454_idformula"].ToString() + "' ");
                            sb.Append(" style='display:none; height:20px;' nivel='3'>");
                            sb.Append("<td style='text-align:left;'><IMG class=NSEG3 src='../../../../images/imgSeparador.gif'>");
                            sb.Append("<nobr class='NBR' style='width:280px;'>" + oFila["t454_literal"].ToString() + "</nobr></td>");
                            break;
                    }

                    for (int x = 4; x < ds.Tables[0].Columns.Count; x++)
                    {
                        if (decimal.Parse(oFila.ItemArray[x].ToString()) < 0) sColor = "red";
                        else sColor = "black";
                        sb.Append("<td style='color:" + sColor + "'>" + decimal.Parse(oFila.ItemArray[x].ToString()).ToString("N") + "</td>");
                    }
                    sb.Append("</tr>");
                }
                else
                {
                    if (oFila["t454_idformula"].ToString() == "8")
                    {
                        sbResultado.Append("<tr id='" + oFila["t454_idformula"].ToString() + "' style='height:17px' class='TBLFIN'>");
                        sbResultado.Append("<td style='text-align:left;'><nobr class='NBR W320 NSEG1'>Ratio</nobr></td>");
                        for (int x = 4; x < ds.Tables[0].Columns.Count; x++)
                        {
                            if (decimal.Parse(oFila.ItemArray[x].ToString()) == 0) nRatio = 0;
                            else
                            {
                                nRatio = decimal.Parse(ds.Tables[0].Rows[i-1].ItemArray[x].ToString()) * 100 / decimal.Parse(oFila.ItemArray[x].ToString());
                            }
                            if (nRatio < 0) sColor = "red";
                            else sColor = "black";
                            sbResultado.Append("<td style='color:" + sColor + "'>" + nRatio.ToString("N") + " %</td>");
                        }
                        sbResultado.Append("</tr>");
                    }
                    else
                    {
                        sbResultado.Append("<tr id='" + oFila["t454_idformula"].ToString() + "' style='HEIGHT: 17px' class='TBLFIN'>");
                        if (oFila["t454_idformula"].ToString() == "3") sbResultado.Append("<td style='text-align:left;'><nobr class='NSEG1'>" + oFila["t454_literal"].ToString() + " / </nobr><span style='color:red'>Facturación anticipada</span></td>");
                        else sbResultado.Append("<td style='text-align:left;'><nobr class='NBR W320 NSEG1'>" + oFila["t454_literal"].ToString() + "</nobr></td>");
                        for (int x = 4; x < ds.Tables[0].Columns.Count; x++)
                        {

                            if (decimal.Parse(oFila.ItemArray[x].ToString()) < 0) sColor = "red";
                            else sColor = "black";
                            sbResultado.Append("<td style='color:" + sColor + "'>" + decimal.Parse(oFila.ItemArray[x].ToString()).ToString("N") + "</td>");
                        }
                        sbResultado.Append("</tr>");
                    }
                }

                i++;
            }
            ds.Dispose();

            nWidthTabla = nWidthTabla + nColumnasACrear * 100;
            sTablaContenido = "<table id='tblDatos' class='texto' style='width:" + nWidthTabla.ToString() + "px; ' cellpadding='0'>";
            sTablaContenido += sbColgroupTitulo.ToString();
            sTablaContenido += "</colgroup>";
            sTablaContenido += sb.ToString();
            //sTablaContenido += "</tbody>";
            sTablaContenido += "</table>";

            sTablaTitulo = "<table id='tblTitulo' class='texto' style='width:" + nWidthTabla.ToString() + "px;' cellpadding='0'>";
            sTablaTitulo += sbColgroupTitulo.ToString();
            sTablaTitulo += "</colgroup>";
            sTablaTitulo += sbTitulo.ToString();
            sTablaTitulo += "</table>";

            sTablaResultado = "<table id='tblResultado' class='texto' style='width:" + nWidthTabla.ToString() + "px;' cellpadding='0'>";
            sTablaResultado += sbColgroupTitulo.ToString();
            sTablaResultado += "</colgroup>";
            sTablaResultado += sbResultado.ToString();
            sTablaResultado += "</table>";

            return "OK@#@" + sTablaTitulo + "@#@" + sTablaContenido + "@#@" + sTablaResultado;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos económicos.", ex);
        }
    }

    private string setResolucion()
    {
        try
        {
            Session["FICHAECO1024"] = !(bool)Session["FICHAECO1024"];

            USUARIO.UpdateResolucion(5, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["FICHAECO1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }

    private string setPreferencia(string sCategoria, string sCualidad, string sCerrarAuto, string sActuAuto, string sOpcionPeriodo, string sOperadorLogico, string sValoresMultiples)
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 14,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 14);
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

        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "")? null:(int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 14);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["categoria"].ToString() + "@#@"); //2
                sb.Append(dr["cualidad"].ToString() + "@#@"); //3
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //4
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //5
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //6
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //7
                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());
            }
            dr.Close();
            //dr.Dispose();

            #region Fechas
            switch (nUtilidadPeriodo)
            {
                case 1:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + 12).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 12) + "@#@");//11
                    break;
                case 2:
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//11
                    break;
                case 3:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//11
                    break;
                case 4:
                    sb.Append("199001" + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//11
                    break;
                case 5:
                    sb.Append("199001" + "@#@");//8
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//9
                    sb.Append("207812" + "@#@");//10
                    sb.Append(Fechas.AnnomesAFechaDescLarga(207812) + "@#@");//11
                    break;
                default:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//8
                    sb.Append(mes[0] + " " + DateTime.Now.Year.ToString() + "@#@");//9
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//10
                    sb.Append(mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString() + "@#@");//11
                    break;
            }
            #endregion

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
                        strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:18px;' idAux='";
                        strHTMLAmbito += SUBNODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLAmbito += "'><td>";

                        switch (int.Parse(aID[0]))
                        {
                            case 1: strHTMLAmbito += "<img src='../../../../images/imgSN4.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 2: strHTMLAmbito += "<img src='../../../../images/imgSN3.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 3: strHTMLAmbito += "<img src='../../../../images/imgSN2.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 4: strHTMLAmbito += "<img src='../../../../images/imgSN1.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 5: strHTMLAmbito += "<img src='../../../../images/imgNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 6: strHTMLAmbito += "<img src='../../../../images/imgSubNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLAmbito += "<nobr class='NBR W230'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 2:
                        if (strIDsResponsable != "") strIDsResponsable += ",";
                        strIDsResponsable += dr["t441_valor"].ToString();
                        strHTMLResponsable += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 3:
                        if (strIDsNaturaleza != "") strIDsNaturaleza += ",";
                        strIDsNaturaleza += dr["t441_valor"].ToString();
                        strHTMLNaturaleza += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 4:
                        if (strIDsModeloCon != "") strIDsModeloCon += ",";
                        strIDsModeloCon += dr["t441_valor"].ToString();
                        strHTMLModeloCon += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 5:
                        if (strIDsHorizontal != "") strIDsHorizontal += ",";
                        strIDsHorizontal += dr["t441_valor"].ToString();
                        strHTMLHorizontal += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 6: if (strIDsSector != "") strIDsSector += ",";
                        strIDsSector += dr["t441_valor"].ToString();
                        strHTMLSector += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 7: if (strIDsSegmento != "") strIDsSegmento += ",";
                        strIDsSegmento += dr["t441_valor"].ToString();
                        strHTMLSegmento += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 8:
                        if (strIDsCliente != "") strIDsCliente += ",";
                        strIDsCliente += dr["t441_valor"].ToString();
                        strHTMLCliente += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 9:
                        if (strIDsContrato != "") strIDsContrato += ",";
                        strIDsContrato += dr["t441_valor"].ToString();
                        strHTMLContrato += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 10:
                        if (strIDsQn != "") strIDsQn += ",";
                        strIDsQn += dr["t441_valor"].ToString();
                        strHTMLQn += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 11:
                        if (strIDsQ1 != "") strIDsQ1 += ",";
                        strIDsQ1 += dr["t441_valor"].ToString();
                        strHTMLQ1 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 12:
                        if (strIDsQ2 != "") strIDsQ2 += ",";
                        strIDsQ2 += dr["t441_valor"].ToString();
                        strHTMLQ2 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 13:
                        if (strIDsQ3 != "") strIDsQ3 += ",";
                        strIDsQ3 += dr["t441_valor"].ToString();
                        strHTMLQ3 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 14:
                        if (strIDsQ4 != "") strIDsQ4 += ",";
                        strIDsQ4 += dr["t441_valor"].ToString();
                        strHTMLQ4 += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    //case 15: //strMagnitudes  break;
                    case 16:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsProyecto != "") strIDsProyecto += ",";
                        strIDsProyecto += aID[0];

                        strHTMLProyecto += "<tr id='" + aID[0] + "' style='height:18px;' ";
                        strHTMLProyecto += "categoria='" + aID[1] + "' ";
                        strHTMLProyecto += "cualidad='" + aID[2] + "' ";
                        strHTMLProyecto += "estado='" + aID[3] + "'><td>";

                        if (aID[1] == "P") strHTMLProyecto += "<img src='../../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";
                        else strHTMLProyecto += "<img src='../../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";

                        switch (aID[2])
                        {
                            case "C": strHTMLProyecto += "<img src='../../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "J": strHTMLProyecto += "<img src='../../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        switch (aID[3])
                        {
                            case "A": strHTMLProyecto += "<img src='../../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "C": strHTMLProyecto += "<img src='../../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "H": strHTMLProyecto += "<img src='../../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLProyecto += "<nobr class='NBR W190' style='margin-left:10px;' onmouseover='TTip(event)'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
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
