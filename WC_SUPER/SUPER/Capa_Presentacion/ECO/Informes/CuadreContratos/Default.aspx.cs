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


public partial class Capa_Presentacion_eco_informes_CuadreContratos_Default : System.Web.UI.Page, ICallbackEventHandler
{
    public SqlConnection oConn;
    public SqlTransaction tr;
    private string _callbackResultado = null;
    protected ArrayList aProyecto;
    public string strTablaHtml;
    public int nEstructuraMinima = 0, nUtilidadPeriodo = 0;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "", strHTMLProveedores = "", strHTMLSoporteAdm = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "" , strIDsSoporteAdm = "";
    public string strIDsProveedores = "";
    public short nPantallaPreferencia = 21;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Cuadre de contratos";
        Master.bFuncionesLocales = true;

        if (!Page.IsCallback)
        {
            cboConceptoEje.Items.Add(new ListItem("", "0"));
            cboConceptoEje.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.NODO), "1"));
            cboConceptoEje.Items.Add(new ListItem("Cliente", "2"));
            cboConceptoEje.Items.Add(new ListItem("Responsable del contrato", "3"));

            lblCDP.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
            lblCSN1P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
            lblCSN2P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
            lblCSN3P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
            lblCSN4P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));

            if (!Utilidades.EstructuraActiva("SN4")) fstCSN4P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN3")) fstCSN3P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN2")) fstCSN2P.Style.Add("visibility", "hidden");
            if (!Utilidades.EstructuraActiva("SN1")) fstCSN1P.Style.Add("visibility", "hidden");

            lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
            //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                divMonedaImportes.Style.Add("visibility", "visible");

            //hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
            //txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();
            //hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
            //txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

            string[] aCriterios = Regex.Split(cargarCriterios(), "@#@");
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
                cboConceptoEje.SelectedValue = aDatosPref[2];

                rdbContrato.SelectedValue = aDatosPref[3];
                //if (rdbContrato.SelectedValue == "0") txtTolerancia.Attributes.Add("readonly", "readonly");
                //if (aDatosPref[4].ToString() != "")
                //    txtTolerancia.Text = int.Parse(aDatosPref[4].ToString()).ToString("#,###");
                //else
                    txtTolerancia.Text = aDatosPref[4] == "" ? "0" : aDatosPref[4];

                if (aDatosPref[7] == "1") rdbOperador.Items[0].Selected = true;
                else rdbOperador.Items[1].Selected = true;

                //nUtilidadPeriodo = int.Parse(aDatosPref[8]);
                //hdnDesde.Text = aDatosPref[9];
                //txtDesde.Text = aDatosPref[10];
                //hdnHasta.Text = aDatosPref[11];
                //txtHasta.Text = aDatosPref[12];

                sSubnodos = aDatosPref[14];

                rdbFormato.SelectedValue = aDatosPref[45];
            }
            else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
            //else txtTolerancia.Attributes.Add("readonly", "readonly");

            #endregion

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor

            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
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
                sResultado += cargarCriterios();
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
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
                    aArgs[1],  //sConceptoEje
                    aArgs[2],  //sNivelEstructura 
                    aArgs[3],  //sProyectos
                    aArgs[4],  //sClientes
                    aArgs[5],  //sResponsables
                    aArgs[6],  //sNaturalezas
                    aArgs[7], //sHorizontal
                    aArgs[8], //sModeloContrato
                    aArgs[9], //sContrato
                    aArgs[10], //sIDEstructura
                    aArgs[11], //sSectores
                    aArgs[12], //sSegmentos
                    aArgs[13], //sComparacionLogica
                    aArgs[14], //sCNP
                    aArgs[15], //sCSN1P
                    aArgs[16], //sCSN2P
                    aArgs[17], //sCSN3P
                    aArgs[18], //sCSN4P
                    aArgs[19], //Cuadre
                    aArgs[20], //Tolerancia
                    aArgs[21],  //sURL,
                    aArgs[22]  //sSoporteAdm)    
                    );
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
    private string cargarCriterios()
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
            SqlDataReader dr = ConsultasPGE.ObtenerDatosCuadreContratos((int)Session["UsuarioActual"], Constantes.nNumElementosMaxCriterios);
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
    private string generarExcel(string sConceptoEje, string sNivelEstructura,
                                string sProyectos, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P,
                                string sCuadre, string sTolerancia,
                                string sURL, string sSoporteAdm)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = CONTRATO.Cuadre
                (
                int.Parse(sConceptoEje),
                (int)Session["UsuarioActual"],
                (sNivelEstructura == "0") ? null : (int?)int.Parse(sNivelEstructura),
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
                sSoporteAdm,
                (sCuadre == "1") ? true : false,
                double.Parse(sTolerancia),
                Session["MONEDA_VDC"].ToString()
                );

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<colgroup><col/><col/><col/><col/><col style='width:450px;' /><col/><col/><col/><col/>");

            if (int.Parse(sConceptoEje) == 3) sb.Append("<col/><col/>");
            else sb.Append("<col/>");

            sb.Append("</colgroup>");

            //double diferencia = 0;
            string sConcepto = "";
            //int i = 0;
            sb.Append("<tr style='height:16px;noWrap:true;'>");

            if (int.Parse(sConceptoEje) == 1)
                sConcepto = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
            else if (int.Parse(sConceptoEje) == 2)
                sConcepto = "Cliente";
            else if (int.Parse(sConceptoEje) == 3)
                sConcepto = "Responsable del contrato";

            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + sConcepto + "</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Contrato</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>NºProyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Denominación</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Responsable del proyecto</td>");
            if (int.Parse(sConceptoEje) == 3)
            {
                sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cliente</td>");
                sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</td>");
            }
            else if (int.Parse(sConceptoEje) == 2) sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</td>");
            else if (int.Parse(sConceptoEje) == 1) sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cliente</td>");

            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Contratado</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Producido en el proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Producido en el contrato</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Descuadre</td>");

            sb.Append("</tr>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;noWrap:true;'>");

                sb.Append("<td>" + dr["denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t306_idcontrato"].ToString() + "-" + dr["t377_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t301_idproyecto"].ToString() + "</td>");
                sb.Append("<td >" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["responsable_proyecto"].ToString() + "</td>");
                if (int.Parse(sConceptoEje) == 3)
                {
                    sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                }
                else if (int.Parse(sConceptoEje) == 2) sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                else if (int.Parse(sConceptoEje) == 1) sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");

                sb.Append("<td>" + dr["TotalContrato"].ToString() + "</td>");
                sb.Append("<td>" + dr["TotalProduccionProyecto"].ToString() + "</td>");
                sb.Append("<td>" + dr["TotalProduccionContrato"].ToString() + "</td>");
                sb.Append("<td>" + dr["Diferencia"].ToString() + "</td>");

                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();

            string strCont = "9";
            if (int.Parse(sConceptoEje) == 1) strCont = "10";

            //sb.Append("<tr><td colspan='" + strCont + "' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td></tr>");

            sb.Append("<tr style='vertical-align:top;'>");
            sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td>");

            for (var j = 2; j <= int.Parse(strCont); j++)
            {
                sb.Append("<td></td>");
            }
            sb.Append("</tr>" + (char)13);

            sb.Append("</table>");

            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al generar el Excel.", ex);
        }
    }
    private string setPreferencia(
        string sConceptoEje,
        string sOperadorLogico,
        string sFormato, string sCuadre, string sTolerancia, string sValoresMultiples
        )
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"],
                                        21,
                                        (sConceptoEje == "") ? null : sConceptoEje,
                                        (sFormato == "") ? null : sFormato,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sCuadre == "") ? null : sCuadre,
                                        (sTolerancia == "") ? null : sTolerancia.Replace(".",""),
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null, null, null, null, null, null, null);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 21);
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
        string sFormato = "";
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 21);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["ConceptoEje"].ToString() + "@#@"); //2
                //sb.Append("0@#@"); //2
                //sb.Append(dr["categoria"].ToString() + "@#@"); //3
                //sb.Append(dr["cualidad"].ToString() + "@#@"); //4

                sb.Append(dr["Cuadre"].ToString() + "@#@"); //3
                if (dr["Tolerancia"].ToString() != "")
                    sb.Append(int.Parse(dr["Tolerancia"].ToString()).ToString("#,###") + "@#@"); //4
                else
                    sb.Append("@#@"); //4

                //sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //5
                //sb.Append(dr["ActuAuto"].ToString() + "@#@"); //6

                sb.Append("0@#@"); //5
                sb.Append("0@#@"); //6

                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //7
                //sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //8
                sb.Append("0@#@"); //8

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = 5;

                sFormato = dr["Formato"].ToString(); //51                
            }
            dr.Close();
            //dr.Dispose();

            sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//9
            sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//10
            sb.Append((DateTime.Now.Year * 100 + 12).ToString() + "@#@");//11
            sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 12) + "@#@");//12

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
                    case 17:
                        if (strIDsSoporteAdm != "") strIDsSoporteAdm += ",";
                        strIDsSoporteAdm += dr["t441_valor"].ToString();
                        strHTMLSoporteAdm += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                        //case 19:
                        //    if (strIDsProveedores != "") strIDsProveedores += ",";
                        //    strIDsProveedores += dr["t441_valor"].ToString();
                        //    strHTMLProveedores += "<tr id='" + dr["t441_valor"].ToString() + "'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        //    break;

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

            sb.Append(nOpcion + "@#@"); //13
            sb.Append(sSubnodos + "@#@"); //14
            sb.Append(strHTMLAmbito + "@#@"); //15
            sb.Append(strIDsAmbito + "@#@"); //16
            sb.Append(strHTMLResponsable + "@#@"); //17
            sb.Append(strIDsResponsable + "@#@"); //18
            sb.Append(strHTMLNaturaleza + "@#@"); //19
            sb.Append(strIDsNaturaleza + "@#@"); //20
            sb.Append(strHTMLModeloCon + "@#@"); //21
            sb.Append(strIDsModeloCon + "@#@"); //22
            sb.Append(strHTMLHorizontal + "@#@"); //23
            sb.Append(strIDsHorizontal + "@#@"); //24
            sb.Append(strHTMLSector + "@#@"); //25
            sb.Append(strIDsSector + "@#@"); //26
            sb.Append(strHTMLSegmento + "@#@"); //27
            sb.Append(strIDsSegmento + "@#@"); //28
            sb.Append(strHTMLCliente + "@#@"); //29
            sb.Append(strIDsCliente + "@#@"); //30
            sb.Append(strHTMLContrato + "@#@"); //31
            sb.Append(strIDsContrato + "@#@"); //32
            sb.Append(strHTMLQn + "@#@"); //33
            sb.Append(strIDsQn + "@#@"); //34
            sb.Append(strHTMLQ1 + "@#@"); //35
            sb.Append(strIDsQ1 + "@#@"); //36
            sb.Append(strHTMLQ2 + "@#@"); //37
            sb.Append(strIDsQ2 + "@#@"); //38
            sb.Append(strHTMLQ3 + "@#@"); //39
            sb.Append(strIDsQ3 + "@#@"); //40
            sb.Append(strHTMLQ4 + "@#@"); //41
            sb.Append(strIDsQ4 + "@#@"); //42
            //sb.Append(strHTMLProveedores + "@#@"); //43
            //sb.Append(strIDsProveedores + "@#@"); //44
            sb.Append(strHTMLProyecto + "@#@"); //45
            sb.Append(strIDsProyecto + "@#@"); //46

            sb.Append(sFormato + "@#@"); //47
            sb.Append(strHTMLSoporteAdm + "@#@"); //48
            sb.Append(strIDsSoporteAdm + "@#@"); //49
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
}