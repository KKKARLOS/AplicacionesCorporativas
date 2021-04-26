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


public partial class Capa_Presentacion_eco_informes_ProfProy_Default : System.Web.UI.Page, ICallbackEventHandler
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
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "";
    public string strMagnitudes = "";
    public short nPantallaPreferencia = 15;
 	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Profesionales asignados a proyectos y proyectos asignados a profesionales";
        Master.bFuncionesLocales = true;

        if (!Page.IsCallback)
        {
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

            hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
            txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();
            hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
            txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

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
                //               cboConceptoEje.SelectedValue = aDatosPref[2];
                cboCategoria.SelectedValue = aDatosPref[3];
                cboCualidad.SelectedValue = aDatosPref[4];
                if (aDatosPref[7] == "1") rdbOperador.Items[0].Selected = true;
                else rdbOperador.Items[1].Selected = true;

                nUtilidadPeriodo = int.Parse(aDatosPref[8]);
                hdnDesde.Text = aDatosPref[9];
                txtDesde.Text = aDatosPref[10];
                hdnHasta.Text = aDatosPref[11];
                txtHasta.Text = aDatosPref[12];
                sSubnodos = aDatosPref[14];

                rdbOrdenacion.SelectedValue = aDatosPref[46];
                rdbConsumos.SelectedValue = aDatosPref[47];

                chkPropios.Checked = (aDatosPref[48] == "1") ? true : false;
                chkOtros.Checked = (aDatosPref[49] == "1") ? true : false;
                chkExternos.Checked = (aDatosPref[50] == "1") ? true : false;

                rdbFormato.SelectedValue = aDatosPref[51];
            }
            else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
            #endregion
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

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
                sResultado += cargarCriterios(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13]);
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
                    aArgs[1],  //sDesde
                    aArgs[2],  //sHasta
                    aArgs[3],  //sNivelEstructura 
                    aArgs[4],  //sCategoria
                    aArgs[5],  //sCualidad
                    aArgs[6],  //sProyectos
                    aArgs[7],  //sClientes
                    aArgs[8],  //sResponsables
                    aArgs[9],  //sNaturalezas
                    aArgs[10], //sHorizontal
                    aArgs[11], //sModeloContrato
                    aArgs[12], //sContrato
                    aArgs[13], //sIDEstructura
                    aArgs[14], //sSectores
                    aArgs[15], //sSegmentos
                    aArgs[16], //sComparacionLogica
                    aArgs[17], //sOrdenacion
                    aArgs[18], //sConsumos
                    aArgs[19], //sTipoRecurso
                    aArgs[20], //sCNP
                    aArgs[21], //sCSN1P
                    aArgs[22], //sCSN2P
                    aArgs[23], //sCSN3P
                    aArgs[24], //sCSN4P
                    aArgs[25]  //sURL)       
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

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }
    private string generarExcel(string sDesde, string sHasta, string sNivelEstructura,
                                string sCategoria, string sCualidad, string sProyectos, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica, string sOrdenacion, string sConsumos, string sTipoRecurso,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P, string sURL)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            DateTime dtFechaDesde = DateTime.Parse("01/" + sDesde.Substring(4, 2) + "/" + sDesde.Substring(0, 4));

            DateTime dtFechaHasta = DateTime.Parse("01/" + sHasta.Substring(4, 2) + "/" + sHasta.Substring(0, 4));
            dtFechaHasta = dtFechaHasta.AddMonths(1);
            dtFechaHasta = dtFechaHasta.AddDays(-1);

            SqlDataReader dr = PROYECTO.ObtenerProyectosProfesionales
                (
                (int)Session["UsuarioActual"],
                dtFechaDesde,
                dtFechaHasta,
                (sNivelEstructura == "0") ? null : (int?)int.Parse(sNivelEstructura),
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
                (sOrdenacion == "1") ? true : false,
                sTipoRecurso,
                (sConsumos == "1") ? true : false,
                sCNP,
                sCSN1P,
                sCSN2P,
                sCSN3P,
                sCSN4P,
                Session["MONEDA_VDC"].ToString()
                );

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<colgroup><col/><col/><col/><col style='width:450px;' /><col style='width:300px;' /><col style='width:300px;' /><col/><col /><col /></colgroup>");
            sb.Append("<tr style='height:16px;noWrap:true;'>");

            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>NºProyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Denominación</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Tipo de recurso</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Profesional</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proveedor</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>HPH</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>JPJ</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
            sb.Append("</tr>" + (char)10);

            //int i = 0;
            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;noWrap:true;'>");

                sb.Append("<td>" + dr["t301_idproyecto"].ToString() + "</td>");
                sb.Append("<td >" + dr["denominacion_proy"].ToString() + "</td>");
                sb.Append("<td>");

                if (dr["tipo_recurso"].ToString() == "E")
                    sb.Append("Externo");
                else if (dr["tipo_recurso"].ToString() == "P")
                    sb.Append("Propio");
                else if (dr["tipo_recurso"].ToString() == "O")
                    sb.Append("De otro " + Estructura.getDefCorta(Estructura.sTipoElem.NODO));

                sb.Append("</td>");

                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");

                if (dr["tipo_recurso"].ToString() != "E")
                    sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td><td></td>"); 
                else
                    sb.Append("<td></td><td>" + dr["t303_denominacion"].ToString() + "</td>"); 

                sb.Append("<td>" + dr["Horas"].ToString() + "</td>");
                sb.Append("<td>" + dr["Jornadas"].ToString() + "</td>");
                sb.Append("<td>" + dr["Importe"].ToString() + "</td>");

                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();

            //sb.Append("<tr><td colspan='9' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td></tr>");
            sb.Append("<tr style='vertical-align:top;'>");
            sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td>");

            for (var j = 2; j <= 9; j++)
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
    private string setPreferencia(string sCategoria, string sCualidad, string sCerrarAuto, string sActuAuto,
                                    string sOpcionPeriodo, string sOperadorLogico, string sOrdenacion, string sConsumos, string sPropios, string sOtros, string sExternos,
                                    string sFormato, string sValoresMultiples)
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
                                        15,
                                        null,
                //(sConceptoEje == "") ? null : sConceptoEje,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        (sOrdenacion == "") ? null : sOrdenacion,
                                        (sConsumos == "") ? null : sConsumos,
                                        (sPropios == "") ? null : sPropios,
                                        (sOtros == "") ? null : sOtros,
                                        (sExternos == "") ? null : sExternos,
                                        (sFormato == "") ? null : sFormato,
                                         null, null, null, null, null, null, null, null);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 15);
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
        string sOrdenacion = "", sConsumos = "", sPropios = "", sOtros = "", sExternos = "", sFormato = "";
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 15);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                //sb.Append(dr["ConceptoEje"].ToString() + "@#@"); //2
                sb.Append("0@#@"); //2
                sb.Append(dr["categoria"].ToString() + "@#@"); //3
                sb.Append(dr["cualidad"].ToString() + "@#@"); //4
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //5
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //6
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //7
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //8

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());

                sOrdenacion = dr["Ordenacion"].ToString(); //46
                sConsumos = dr["Consumos"].ToString(); //47
                sPropios = dr["Propios"].ToString(); //48
                sOtros = dr["Otros"].ToString(); //49
                sExternos = dr["Externos"].ToString(); //50
                sFormato = dr["Formato"].ToString(); //51

            }
            dr.Close();
            //dr.Dispose();

            #region Fechas
            switch (nUtilidadPeriodo)
            {
                case 1:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + 12).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 12) + "@#@");//12
                    break;
                case 2:
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//12
                    break;
                case 3:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + 1) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//12
                    break;
                case 4:
                    sb.Append("199001" + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(DateTime.Now.Year * 100 + DateTime.Now.Month) + "@#@");//12
                    break;
                case 5:
                    sb.Append("199001" + "@#@");//9
                    sb.Append(Fechas.AnnomesAFechaDescLarga(199001) + "@#@");//10
                    sb.Append("207812" + "@#@");//11
                    sb.Append(Fechas.AnnomesAFechaDescLarga(207812) + "@#@");//12
                    break;
                default:
                    sb.Append((DateTime.Now.Year * 100 + 1).ToString() + "@#@");//9
                    sb.Append(mes[0] + " " + DateTime.Now.Year.ToString() + "@#@");//10
                    sb.Append((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString() + "@#@");//11
                    sb.Append(mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString() + "@#@");//12
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
            sb.Append(strMagnitudes + "@#@"); //43
            sb.Append(strHTMLProyecto + "@#@"); //44
            sb.Append(strIDsProyecto + "@#@"); //45

            sb.Append(sOrdenacion + "@#@"); //46
            sb.Append(sConsumos + "@#@"); //47
            sb.Append(sPropios + "@#@"); //48
            sb.Append(sOtros + "@#@"); //49
            sb.Append(sExternos + "@#@"); //50
            sb.Append(sFormato + "@#@"); //51
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
}