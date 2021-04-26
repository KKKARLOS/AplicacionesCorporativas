using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;


public partial class Capa_Presentacion_eco_informes_Facturas_Default : System.Web.UI.Page, ICallbackEventHandler
{
    public SqlConnection oConn;
    public SqlTransaction tr;
    private string _callbackResultado = null;
    protected ArrayList aProyecto;
    public string strTablaHtml;
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 2;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public string sCriterios = "", sSubnodos = "", sNodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    ArrayList aNodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLClienteFact = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "", strHTMLCR = "", strHTMLProveedores = "", strHTMLSociedad = "", strHTMLPais = "", strHTMLProvincia = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsClienteFact = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "", strIDsCRs = "", strIDsProveedores = "", strIDsSociedades = "", strIDsPaises = "", strIDsProvincias = "";//, strIDsClaseEco = "";
    public string strMagnitudes = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Informe de datos económicos de proyectos";
        Master.bFuncionesLocales = true;
        //Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
        //Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

        if (!Page.IsCallback)
        {            

            lblCR.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
            lblCSN1P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
            lblCSN2P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
            //lblCSN3P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
            //lblCSN4P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));

            //if (!Utilidades.EstructuraActiva("SN4")) fstCSN4P.Style.Add("visibility", "hidden");
            //if (!Utilidades.EstructuraActiva("SN3")) fstCSN3P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN2")) fstCSN2P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN1")) fstCSN1P.Style.Add("visibility", "hidden");

            lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
            //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                divMonedaImportes.Style.Add("visibility", "visible");

            int iAnio = DateTime.Now.Year, iMes = DateTime.Now.Month;
            hdnDesde.Text = (iAnio * 100 + iMes).ToString();
            DateTime dFechaLimite = DateTime.Parse("05/" + iMes.ToString() + "/" + iAnio.ToString());
            if (DateTime.Today <= dFechaLimite)
            {
                dFechaLimite = DateTime.Today.AddMonths(-1);
                hdnDesde.Text = (dFechaLimite.Year * 100 + dFechaLimite.Month).ToString();
            }
            txtDesde.Text = mes[dFechaLimite.Month - 1] + " " + dFechaLimite.Year.ToString();
            hdnHasta.Text = hdnDesde.Text;
            txtHasta.Text = txtDesde.Text;           

            string[] aCriterios = Regex.Split(cargarCriterios(int.Parse(hdnDesde.Text), int.Parse(hdnHasta.Text)), "@#@");
            if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
            else Master.sErrores = aCriterios[1];

            if (Utilidades.EstructuraActiva("SN4")) nEstructuraMinima = 1;
            else if (Utilidades.EstructuraActiva("SN3")) nEstructuraMinima = 2;
            else if (Utilidades.EstructuraActiva("SN2")) nEstructuraMinima = 3;
            else if (Utilidades.EstructuraActiva("SN1")) nEstructuraMinima = 4;

            strTablaHtml = "<tr id='*' class='FA'><td>&lt; Todos &gt;</td></tr>";

            string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
            #region Lectura de preferencia

            if (bHayPreferencia && aDatosPref[0] == "OK")
            {
                sHayPreferencia = "true";
                hdnGrupoEco.Text = aDatosPref[2];
                //lblGrupoEco.InnerText = Utilidades.unescape(aDatosPref[3]);
                cboCategoria.SelectedValue = aDatosPref[4];
                cboCualidad.SelectedValue = aDatosPref[5];
                if (aDatosPref[8] == "1")
                    rdbOperador.Items[0].Selected = true;
                else
                    rdbOperador.Items[1].Selected = true;

                nUtilidadPeriodo = int.Parse(aDatosPref[9]);
                //nUtilidadPeriodo = 0;
                //hdnDesde.Text = aDatosPref[9];
                //txtDesde.Text = aDatosPref[10];
                //hdnHasta.Text = aDatosPref[11];
                //txtHasta.Text = aDatosPref[12];
                sSubnodos = aDatosPref[15];
                sNodos = aDatosPref[51];

                //rdbOrdenacion.SelectedValue=aDatosPref[46];
                //rdbConsumos.SelectedValue=aDatosPref[47];
                //chkPropios.Checked = (aDatosPref[48] == "1") ? true : false;
                //chkOtros.Checked = (aDatosPref[49] == "1") ? true : false;
                //chkExternos.Checked = (aDatosPref[50] == "1") ? true : false;

                //rdbFormato.SelectedValue = aDatosPref[49];
            }
            else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
            #endregion

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getTablaCriterios"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]), int.Parse(aArgs[1]));
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
                break;           
            case ("generarExcel"):
                sResultado += generarExcel
                    (
                    aArgs[1], //sDesde
                    aArgs[2], //sHasta
                    aArgs[3], //hueco libre
                    aArgs[4], //sCategoria
                    aArgs[5], //sCualidad
                    aArgs[6], //sProyectos
                    aArgs[7], //sClientes gestion
                    aArgs[8], //sResponsables
                    aArgs[9], //sNaturalezas
                    aArgs[10], //sHorizontal
                    aArgs[11], //sModeloContrato
                    aArgs[12], //sContrato
                    aArgs[13], //sIDEstructura
                    aArgs[14], //sSectores
                    aArgs[15], //sSegmentos
                    aArgs[16], //sComparacionLogica
                    aArgs[17], //sCNP
                    aArgs[18], //sCSN1P
                    aArgs[19], //sCSN2P
                    aArgs[20], //sCSN3P
                    aArgs[21], //sCSN4P
                    aArgs[22], //sCR
                    aArgs[23], //sPaises
                    aArgs[24] //sProvincias
                    );
                break;
        }
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
        SqlDataReader dr;
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
            //SqlDataReader dr = DATOECO.ObtenerCombosInf((int)Session["UsuarioActual"], nDesde, nHasta, Constantes.nNumElementosMaxCriterios);
            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
                dr = DATOECO.ObtenerCombosInfAdm(nDesde, nHasta, Constantes.nNumElementosMaxCriterios);
            else
                dr = DATOECO.ObtenerCombosInf((int)Session["UsuarioActual"], nDesde, nHasta, Constantes.nNumElementosMaxCriterios);

            while (dr.Read())
            {
                //if ((int)dr["codigo"] == -1)
                if (dr["codigo"].ToString() == "-1")
                    sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"excede\":1};\n");
                else
                {
                    //if ((int)dr["tipo"] == 16)
                    if (dr["tipo"].ToString() == "16")
                        sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":\"" + dr["codigo"].ToString() + "\",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\",\"p\":\"" + dr["t301_idproyecto"].ToString() + "\",\"a\":\"" + dr["t301_categoria"].ToString() + "\",\"u\":\"" + dr["t305_cualidad"].ToString() + "\",\"e\":\"" + dr["t301_estado"].ToString() + "\",\"l\":\"" + dr["t302_denominacion"].ToString() + "\",\"n\":\"" + dr["t303_denominacion"].ToString() + "\",\"r\":\"" + dr["Responsable"].ToString() + "\"};\n");
                    else
                        sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":\"" + dr["codigo"].ToString() + "\",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\"};\n");
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
    private string setPreferencia(string sCategoria, string sCualidad, string sCerrarAuto,
                                  string sActuAuto, string sOperadorLogico,
                                  string sOpcionPeriodo, string sFormato, string sValoresMultiples)
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
            short nPantalla = 43;
            int nPref = PREFERENCIAUSUARIO.Insertar(tr,
                                        (int)Session["IDFICEPI_PC_ACTUAL"],
                                        nPantalla,null, null, 
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        (sFormato == "") ? null : sFormato,
                                        null, null, null, null, null, null, null, null, null, null, null, null);

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
            short nPantalla = 43;
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], nPantalla);
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
        int idPrefUsuario = 0; //, nConceptoEje = 0;
        int nOpcion = 1;
        short nPantalla = 43;
        //string sFormato = "";
        try
        {
            SqlDataReader dr =
                PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                            (int)Session["IDFICEPI_PC_ACTUAL"], nPantalla);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //2
                sb.Append(dr["GrupoEco"].ToString() + "@#@"); //3
                sb.Append(dr["TipoFactura"].ToString() + "@#@"); //4
                sb.Append(dr["categoria"].ToString() + "@#@"); //5
                sb.Append(dr["cualidad"].ToString() + "@#@"); //6
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //7
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //8
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //9
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //10

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());

                //sOrdenacion = dr["Ordenacion"].ToString(); //46
                //sConsumos = dr["Consumos"].ToString(); //47
                //sPropios = dr["Propios"].ToString(); //48
                //sOtros = dr["Otros"].ToString(); //49
                //sExternos = dr["Externos"].ToString(); //50
                //sFormato = dr["Formato"].ToString(); //49

            }
            dr.Close();
            //dr.Dispose();

            #region Fechas
            switch (nUtilidadPeriodo)
            {
                case 1:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//12
                    sb.Append((DateTime.Now.Year * 100 + 12).ToString() + "@#@");//13
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 12) + "@#@");//14
                    break;
                case 2:
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//
                    break;
                case 3:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//
                    break;
                case 4:
                    sb.Append("199001" + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//
                    break;
                case 5:
                    sb.Append("199001" + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//
                    sb.Append("207812" + "@#@");//
                    sb.Append(Fechas.AnnomesAFechaDescLarga(207812) + "@#@");//
                    break;
                default:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//
                    sb.Append(mes[0] + " " + DateTime.Now.Year.ToString() + "@#@");//
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//
                    sb.Append(mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString() + "@#@");//
                    break;
            }
            #endregion

            #region HTML, IDs
            int nNivelMinimo = 0;
            bool bAmbito = false;
            bool bCRs = false;
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
                    case 15:
                        if (strMagnitudes != "") strMagnitudes += "///";
                        strMagnitudes += dr["t441_valor"].ToString() + "##" + dr["t441_denominacion"].ToString();
                        break;
                    case 16:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsProyecto != "") strIDsProyecto += ",";
                        strIDsProyecto += aID[0];

                        strHTMLProyecto += "<tr id='" + aID[0] + "' style='height:16px;' ";
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
//aqui
                    //case 18:
                    //    if (strIDsCRs != "") strIDsCRs += ",";
                    //    strIDsCRs += dr["t441_valor"].ToString();
                    //    strHTMLCR += "<tr id='" + dr["t441_valor"].ToString() + "'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                    //    break;
                    case 18:
                        if (!bCRs)
                        {
                            bCRs = true;
                            nNivelMinimo = 5;
                        }
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (int.Parse(aID[0]) < nNivelMinimo) nNivelMinimo = int.Parse(aID[0]);

                        if (strIDsCRs != "") strIDsCRs += ",";
                        strIDsCRs += aID[1];

                        aNodos = PREFUSUMULTIVALOR.SelectNodosAmbito(null, aNodos, int.Parse(aID[0]), int.Parse(aID[1]));
                        strHTMLCR += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:16px;' idAux='";
                        strHTMLCR += NODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLCR += "'><td>";

                        switch (int.Parse(aID[0]))
                        {
                            case 1: strHTMLCR += "<img src='../../../../images/imgSN4.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 2: strHTMLCR += "<img src='../../../../images/imgSN3.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 3: strHTMLCR += "<img src='../../../../images/imgSN2.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 4: strHTMLCR += "<img src='../../../../images/imgSN1.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 5: strHTMLCR += "<img src='../../../../images/imgNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLCR += "<nobr class='NBR W230'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;                    
                    case 20:
                        if (strIDsProveedores != "") strIDsProveedores += ",";
                        strIDsProveedores += dr["t441_valor"].ToString();
                        strHTMLProveedores += "<tr id='" + dr["t441_valor"].ToString() + "'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;                   
                    case 34: if (strIDsPaises != "") strIDsPaises += ",";
                        strIDsPaises += dr["t441_valor"].ToString();
                        strHTMLPais += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 35: if (strIDsProvincias != "") strIDsProvincias += ",";
                        strIDsProvincias += dr["t441_valor"].ToString();
                        strHTMLProvincia += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
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

            for (int i = 0; i < aNodos.Count; i++)
            {
                if (i > 0) sNodos += ",";
                sNodos += aNodos[i];
            }

            sb.Append(nOpcion + "@#@"); //15
            sb.Append(sSubnodos + "@#@"); //16
            sb.Append(strHTMLAmbito + "@#@"); //17
            sb.Append(strIDsAmbito + "@#@"); //18
            sb.Append(strHTMLResponsable + "@#@"); //19
            sb.Append(strIDsResponsable + "@#@"); //20
            sb.Append(strHTMLNaturaleza + "@#@"); //21
            sb.Append(strIDsNaturaleza + "@#@"); //22
            sb.Append(strHTMLModeloCon + "@#@"); //23
            sb.Append(strIDsModeloCon + "@#@"); //24
            sb.Append(strHTMLHorizontal + "@#@"); //25
            sb.Append(strIDsHorizontal + "@#@"); //26
            sb.Append(strHTMLSector + "@#@"); //27
            sb.Append(strIDsSector + "@#@"); //28
            sb.Append(strHTMLSegmento + "@#@"); //29
            sb.Append(strIDsSegmento + "@#@"); //30
            sb.Append(strHTMLCliente + "@#@"); //31
            sb.Append(strIDsCliente + "@#@"); //32
            sb.Append(strHTMLContrato + "@#@"); //33
            sb.Append(strIDsContrato + "@#@"); //34
            sb.Append(strHTMLQn + "@#@"); //35
            sb.Append(strIDsQn + "@#@"); //36
            sb.Append(strHTMLQ1 + "@#@"); //37
            sb.Append(strIDsQ1 + "@#@"); //38
            sb.Append(strHTMLQ2 + "@#@"); //39
            sb.Append(strIDsQ2 + "@#@"); //40
            sb.Append(strHTMLQ3 + "@#@"); //41
            sb.Append(strIDsQ3 + "@#@"); //42
            sb.Append(strHTMLQ4 + "@#@"); //43
            sb.Append(strIDsQ4 + "@#@"); //44
            sb.Append(strMagnitudes + "@#@"); //45
            sb.Append(strHTMLProyecto + "@#@"); //46
            sb.Append(strIDsProyecto + "@#@"); //47

            //sb.Append(sOrdenacion + "@#@"); //46
            //sb.Append(sConsumos + "@#@"); //47
            //sb.Append(sPropios + "@#@"); //48
            //sb.Append(sOtros + "@#@"); //49
            //sb.Append(sExternos + "@#@"); //50
            sb.Append(strHTMLClienteFact + "@#@"); //48
            sb.Append(strIDsClienteFact + "@#@"); //49
            sb.Append(sNodos + "@#@"); //52
            sb.Append(strHTMLProveedores + "@#@"); //53

            sb.Append(strHTMLSociedad + "@#@"); //54
            sb.Append(strIDsSociedades + "@#@"); //55
            sb.Append(strHTMLPais + "@#@"); //56
            sb.Append(strIDsPaises + "@#@"); //57
            sb.Append(strHTMLProvincia + "@#@"); //58
            sb.Append(strIDsProvincias + "@#@"); //59
            
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }

    private string generarExcel(string sAnoMesD, string sAnoMesH, string sHuecoLibre,
                                string sCategoria, string sCualidad, string sProyectos, string sClientes, string sResponsables,
                                string sNaturalezas, string sHorizontal, string sModeloContrato, string sContrato, string sIDEstructura,
                                string sSectores, string sSegmentos, string sComparacionLogica, string sCNP, string sCSN1P, string sCSN2P,
                                string sCSN3P, string sCSN4P, string sCR, string sPaises, string sProvincias)
    {
        string sRes = "";
        try
        {
            
                    sRes = generarExcelProduccion(sAnoMesD, sAnoMesH, sHuecoLibre, sCategoria, sCualidad, sProyectos, sClientes,
                                         sResponsables, sNaturalezas, sHorizontal, sModeloContrato, sContrato, sIDEstructura, sSectores,
                                         sSegmentos, sComparacionLogica, sCNP, sCSN1P, sCSN2P, sCSN3P, sCSN4P,
                                         sCR, sPaises, sProvincias);
                    
            return sRes;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al generar el Excel de las facturas.", ex);
        }
    }
    
    private string generarExcelProduccion(string sAnoMesD, string sAnoMesH, string sHuecoLibre,
                                string sCategoria, string sCualidad, string sProyectos, string sClientes, string sResponsables,
                                string sNaturalezas, string sHorizontal, string sModeloContrato, string sContrato, string sIDEstructura,
                                string sSectores, string sSegmentos, string sComparacionLogica, string sCNP, string sCSN1P, string sCSN2P,
                                string sCSN3P, string sCSN4P, string sCR, string sPaises, string sProvincias)
    {
        StringBuilder sb = new StringBuilder();
        int iNumCols = 17, iNumColsAux;
        string sAux = "", sPrimer = "";

        //string sTitulo = "INFORME DE PRODUCCION";
        try
        {
            SqlDataReader dr = DATOECO.ObtenerProduccionPorProfesional(
                (int)Session["UsuarioActual"],
                int.Parse(sAnoMesD),//Fechas.AnnomesAFecha(int.Parse(sAnoMesD))
                int.Parse(sAnoMesH),
                7,//(sNivelEstructura == "0") ? null : (int?)int.Parse(sNivelEstructura) + 1,
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
                sCR,     
                sPaises,
                sProvincias,
                Session["MONEDA_VDC"].ToString()
                );

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<colgroup>");
            if (Utilidades.EstructuraActiva("SN1"))
            {
                sb.Append("<col style='width:auto;'/>");
                iNumCols++;
            }
            if (Utilidades.EstructuraActiva("SN2"))
            {
                sb.Append("<col style='width:auto;'/>");
                iNumCols++;
            }
            if (Utilidades.EstructuraActiva("SN3"))
            {
                sb.Append("<col style='width:auto;'/>");
                iNumCols++;
            }
            if (Utilidades.EstructuraActiva("SN4"))
            {
                sb.Append("<col style='width:auto;'/>");
                iNumCols++;
            }
            sb.Append("<col style='width:auto;'/><col style='width:auto;' /><col style='width:auto;' /><col style='width:auto;' /><col style='width:auto;' /><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/><col style='width:auto;'/></colgroup>");
            iNumColsAux = iNumCols - 1;
            //sb.Append("<tr style='height:16px;noWrap:true;'><td></td><td colspan=" + iNumColsAux.ToString() + ">" + sTitulo + "</td></tr>");
            //sb.Append("<tr style='height:16px;noWrap:true;'><td colspan=" + iNumCols.ToString() + "><img src='" + sURL + "/../../../../images/imgIconoContratante.gif' class='ICO'/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Contratante</td></tr>");
            //sb.Append("<tr style='height:16px;noWrap:true;'><td colspan=" + iNumCols.ToString() + "><img src='" + sURL + "/../../../../images/imgIconoRepJor.gif' class='ICO'/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Replicado</td></tr>");
            //sb.Append("<tr style='height:16px;noWrap:true;'><td colspan=" + iNumCols.ToString() + "><img src='" + sURL + "/../../../../images/imgIconoRepPrecio.gif' class='ICO'/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Replicado con gestión propia</td></tr>");

            sb.Append("<tr style='height:16px;noWrap:true;' align='center'>");
            if (Utilidades.EstructuraActiva("SN4"))
                sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) + "</td>");
            if (Utilidades.EstructuraActiva("SN3"))
                sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) + "</td>");
            if (Utilidades.EstructuraActiva("SN2"))
                sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) + "</td>");
            if (Utilidades.EstructuraActiva("SN1"))
                sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) + "</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cód.Cliente</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cliente</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Sector</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Segmento</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Provincia</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>País</td>");

            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Responsable del proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cualidad</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cod.Proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proyecto técnico</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Fase</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Actividad</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cod.Tarea</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Tarea</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Profesional</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Perfil</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Tarifa unitaria</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>TUC</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>TUCNF</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>TUCF</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Imp. fact.</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Unid. a fact.</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Imp. a fact.</td>");

            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold; text-align:right;'>Mes</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold; text-align:right;'>Año</td>");

            sb.Append("</tr>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr>");
                if (Utilidades.EstructuraActiva("SN4"))
                    sb.Append("<td>" + dr["t394_denominacion"].ToString() + "</td>");//supernodo4
                if (Utilidades.EstructuraActiva("SN3"))
                    sb.Append("<td>" + dr["t393_denominacion"].ToString() + "</td>");//supernodo3
                if (Utilidades.EstructuraActiva("SN2"))
                    sb.Append("<td>" + dr["t392_denominacion"].ToString() + "</td>");//supernodo2
                if (Utilidades.EstructuraActiva("SN1"))
                    sb.Append("<td>" + dr["t391_denominacion"].ToString() + "</td>");//supernodo1
                sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");//nodo

                sb.Append("<td align='right'>" + dr["t302_codigoexterno"].ToString() + "</td>");//Id.Cliente
                sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");//Cliente
                sb.Append("<td>" + dr["t483_denominacion"].ToString() + "</td>"); //Sector
                sb.Append("<td>" + dr["t484_denominacion"].ToString() + "</td>"); //Segmento
                sb.Append("<td>" + dr["t173_denominacion"].ToString() + "</td>"); //Provincia
                sb.Append("<td>" + dr["t172_denominacion"].ToString() + "</td>"); //Pais
                sb.Append("<td>" + dr["responsable_proyecto"].ToString() + "</td>");
                sb.Append("<td>");               
                sb.Append(dr["cualidad"].ToString());
                sb.Append("</td><td >" + dr["t301_idproyecto"].ToString());
                sb.Append("</td><td >" + dr["t301_denominacion"].ToString());
                sb.Append("</td><td >" + dr["t331_despt"].ToString());
                sb.Append("</td><td >" + dr["fase"].ToString());
                sb.Append("</td><td >" + dr["actividad"].ToString());
                sb.Append("</td><td>" + dr["t332_idtarea"].ToString());
                sb.Append("</td><td>" + dr["t332_destarea"].ToString());
                sb.Append("</td><td >" + dr["profesional"].ToString());
                sb.Append("</td><td >" + dr["t333_denominacion"].ToString());//perfil

                sb.Append("</td><td>" + dr["CosteUnitario"].ToString());
                sb.Append("</td><td >" + dr["thc"].ToString());
                sb.Append("</td><td >" + dr["thcnf"].ToString());
                sb.Append("</td><td >" + dr["thcf"].ToString());
                sb.Append("</td><td>" + dr["impIap"].ToString());//Importe facturar
                sb.Append("</td><td >" + dr["haf"].ToString());//Horas a facturar
                sb.Append("</td><td >" + dr["impaFact"].ToString());//Importe a facturar 
               
                sb.Append("</td><td style='text-align:right;'>" + dr["mes"].ToString());
                sb.Append("</td><td style='text-align:right;'>" + dr["anno"].ToString());

                //sb.Append("</td><td style='text-align:right;'>" + decimal.Parse(dr["Importe"].ToString()).ToString("N"));

                sb.Append("</td></tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            //sb.Append("<tr><td colspan='" + iNumCols.ToString() + "' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td></tr>");
            sb.Append("<tr style='vertical-align:top;'>");
            sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td>");

            for (var j = 2; j <= iNumCols; j++)
            {
                sb.Append("<td></td>");
            }
            sb.Append("</tr>" + (char)13);  
            sb.Append("</table>");

            //17/11/2015. Prueba Mikel. En vez de devolver al js la tabla, la guardamos en vble de sesión
            //return "OK@#@" + sb.ToString();
            //HttpContext.Current.Cache.Remove("EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString());

            //KARLOS
            //string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + Guid.NewGuid().ToString();
            //HttpContext.Current.Cache.Insert(sIdCache, sb.ToString(), null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString();

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();

        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al generar el Excel de la producción.", ex);
        }
    }    
}