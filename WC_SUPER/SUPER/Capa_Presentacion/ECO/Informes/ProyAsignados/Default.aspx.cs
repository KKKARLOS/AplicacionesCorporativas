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
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "", strHTMLOrgComercial = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto="", strIDsOrgComercial;
    public string strMagnitudes = "";
    public short nPantallaPreferencia = 13;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                //if (!(bool)Session["DATOSRES1024"])
                //{
                //    Master.nResolucion = 1280;
                //}
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Proyectos asignados";

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

                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                #region Lectura de preferencia
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    sHayPreferencia = "true";
                    //cboConceptoEje.SelectedValue = aDatosPref[2];
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

                    //ContentPlaceHolder oCPHC = (ContentPlaceHolder)Master.FindControl("CPHC");
                    //string[] aMagnitudes = Regex.Split(aDatosPref[43], "///");
                    //foreach (string oMagnitud in aMagnitudes)
                    //{
                    //    if (oMagnitud == "") continue;
                    //    string[] aDatos = Regex.Split(oMagnitud, "##");
                    //    ///aDatos[0] = id
                    //    ///aDatos[1] = checked
                    //    ((CheckBox)oCPHC.FindControl(aDatos[0])).Checked = (aDatos[1] == "1") ? true : false;
                    //}
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
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21], aArgs[22], aArgs[23], aArgs[24], aArgs[25]);
                break;
            case ("getTablaCriterios"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
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

    private string obtenerDatos(string sEstado, string sDesde, string sHasta, string sNivelEstructura,
                                string sCategoria, string sCualidad, string sIDProyecto, string sClientes,
                                string sResponsables, string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica, string sNivelIndentacion,
                                string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P, string sPSN, string sOrgComercial)
    {

        //int nNivelIndentacion = int.Parse(sNivelIndentacion) + 1;
        int nWidthTabla = 3020;// 2550;
        StringBuilder sb = new StringBuilder();
        try
        {
            //Añado espacio para las columnas de la superestructura que está activa
            if (Utilidades.EstructuraActiva("SN4")) nEstructuraMinima = 1;
            else if (Utilidades.EstructuraActiva("SN3")) nEstructuraMinima = 2;
            else if (Utilidades.EstructuraActiva("SN2")) nEstructuraMinima = 3;
            else if (Utilidades.EstructuraActiva("SN1")) nEstructuraMinima = 4;
            nWidthTabla += 300 * (5 - nEstructuraMinima);

            //if (sNivelIndentacion == "1")
            //{
            sb.Append("<table id='tblDatos' class='texto' style='font-size:10px;width:" + nWidthTabla.ToString() + "px;'>");
                sb.Append("<colgroup>");
                if (Utilidades.EstructuraActiva("SN4")) sb.Append("<col style='width:300px;' />");//SNN4
                if (Utilidades.EstructuraActiva("SN3")) sb.Append("<col style='width:300px;' />");//SNN3
                if (Utilidades.EstructuraActiva("SN2")) sb.Append("<col style='width:300px;' />");//negocio
                if (Utilidades.EstructuraActiva("SN1")) sb.Append("<col style='width:300px;' />");//linea
                sb.Append("<col style='width:300px;' />");//nodo
                //sb.Append("<col style='width:200px; text-align:right;' />");//area (subnodo)
                sb.Append("<col style='width:300px;' />");//area (subnodo)
                sb.Append("<col style='width:400px;' />");//cliente
                sb.Append("<col style='width:50px;' />");//numero contrato
                sb.Append("<col style='width:300px;' />");//contrato
                sb.Append("<col style='width:50px;' />");//numero proyecto
                sb.Append("<col style='width:500px;' />");//nombre proyecto
                sb.Append("<col style='width:100px;' />");//tipo (producto / servicio)
                sb.Append("<col style='width:200px;' />");//modelo de contratacion
                sb.Append("<col style='width:100px;' />");//estado
                sb.Append("<col style='width:200px;' />");//tipología
                sb.Append("<col style='width:300px;' />");//naturaleza de producción
                sb.Append("<col style='width:400px;' />");//responsable
                sb.Append("<col style='width:400px;' />");//comercial
                sb.Append("<col style='width:200px;' />");//cualificador
                sb.Append("<col style='width:400px;' />");//motivo visión
                sb.Append("<col style='width:400px;' />");//organización comercial
                sb.Append("<col style='width:70px;' />");//Código NLO
                sb.Append("<col style='width:400px;' />");//Denominación NLO
            sb.Append("</colgroup>");
            //}
                sb.Append("<TR style='background-color: #BCD4DF;'>");
                if (Utilidades.EstructuraActiva("SN4")) sb.Append("<td style='padding-left:2px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) + "</TD>");
                if (Utilidades.EstructuraActiva("SN3")) sb.Append("<td style='padding-left:2px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) + "</TD>");
                if (Utilidades.EstructuraActiva("SN2")) sb.Append("<td style='padding-left:2px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) + "</TD>");
                if (Utilidades.EstructuraActiva("SN1")) sb.Append("<td style='padding-left:2px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) + "</TD>");
                sb.Append("<td style='padding-left:2px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "</TD>");
                sb.Append("<td style='padding-left:2px;'>" + Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) + "</TD>");
                sb.Append("<td>Cliente</TD><td style='text-align:right;'>Nº</TD><td>Contrato</TD><td style='text-align:right;'>NºProy.</TD><td>Proyecto</TD><td>Tipo</TD><td>Modelo contratación</TD>");
            sb.Append("<td>Estado</TD><td>Tipología</TD><td>Naturaleza de producción</TD><td>Responsable</TD><td>Comercial</TD><td>Cualificador de ventas</td><td>Motivo visión del proyecto</TD><td>Organización comercial</TD>");
            sb.Append("<td>Código NLO</td><td>Nueva Línea de Oferta</td>");

            sb.Append("</TR>");

            SqlDataReader dr = ConsultasPGE.InformeProyectosAsignados(sEstado,
                (int)Session["UsuarioActual"],
                int.Parse(sDesde), int.Parse(sHasta),
                (sNivelEstructura=="0")? null:(int?)int.Parse(sNivelEstructura),
                sCategoria,
                sCualidad,
                (sIDProyecto=="")? null:(int?)int.Parse(sIDProyecto),
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
                sOrgComercial);

            while (dr.Read())
            {
                sb.Append("<tr style='height:18px;'><td style='padding-left:2px;'>");
                if (Utilidades.EstructuraActiva("SN4"))
                {
                    sb.Append(dr["t394_denominacion"].ToString());
                    sb.Append("</td><td style='padding-left:2px;'>");
                }
                if (Utilidades.EstructuraActiva("SN3"))
                {
                    sb.Append(dr["t393_denominacion"].ToString());
                    sb.Append("</td><td style='padding-left:2px;'>");
                }
                if (Utilidades.EstructuraActiva("SN2"))
                {
                    sb.Append(dr["t392_denominacion"].ToString());//negocio
                    sb.Append("</td><td style='padding-left:2px;'>");
                }
                if (Utilidades.EstructuraActiva("SN1"))
                {
                    sb.Append(dr["t391_denominacion"].ToString());//linea
                    sb.Append("</td><td style='padding-left:2px;'>");
                }
                sb.Append(dr["t303_denominacion"].ToString());
                sb.Append("</td><td style='padding-left:2px;'>");
                sb.Append(dr["t304_denominacion"].ToString());
                sb.Append("</td><td style='padding-left:2px;'>");
                sb.Append(dr["t302_denominacion"].ToString());
                sb.Append("</td><td style='text-align:right;'>");
                if (dr["t306_idcontrato"]!=DBNull.Value)
                    sb.Append(int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###"));
                sb.Append("</td><td>");
                sb.Append(dr["t377_denominacion"].ToString());//contrato
                sb.Append("</td><td style='text-align:right;'>");
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###"));
                sb.Append("</td><td>");
                sb.Append(dr["t301_denominacion"].ToString());
                sb.Append("</td><td>");
                sb.Append(dr["t301_categoria"].ToString());//Tipo: producto o servicio
                sb.Append("</td><td>");
                sb.Append(dr["t316_denominacion"].ToString());//modelo
                sb.Append("</td><td>");
                sb.Append(dr["t301_estado"].ToString());
                sb.Append("</td><td>");
                sb.Append(dr["t320_denominacion"].ToString());//Tipologia
                sb.Append("</td><td>");
                sb.Append(dr["t323_denominacion"].ToString());//naturaleza
                sb.Append("</td><td>");
                sb.Append(dr["responsable_proyecto"].ToString());
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["comercial_hermes"].ToString());//Organización comercial
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["t055_denominacion"].ToString());//Cualificador
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["motivo_vision"].ToString());
                sb.Append("</td><td>");
                sb.Append(dr["ta212_denominacion"].ToString());//Organización comercial
                //NLO
                sb.Append("</td><td style='text-align:right;'>");
                if (dr["t195_codigoexterno"] != DBNull.Value)
                    sb.Append(int.Parse(dr["t195_codigoexterno"].ToString()).ToString("#,###"));
                sb.Append("</td><td>");
                sb.Append(dr["t195_denominacion"].ToString());//nUEVA lÍNEA DE oFERTA
                sb.Append("<td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
        }
    }

    private string setPreferencia(string sEstado, string sCategoria, string sCualidad, string sCerrarAuto, string sActuAuto, string sOpcionPeriodo, string sOperadorLogico, string sValoresMultiples)
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 13,
                                        (sEstado == "") ? null : sEstado,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        (sOpcionPeriodo == "") ? null : sOpcionPeriodo,
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 13);
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
        int idPrefUsuario = 0, nConceptoEje=0;
        int nOpcion = 1;
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 13);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["Estado"].ToString() + "@#@"); //2
                sb.Append(dr["categoria"].ToString() + "@#@"); //3
                sb.Append(dr["cualidad"].ToString() + "@#@"); //4
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //5
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //6
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //7
                sb.Append(dr["OpcionPeriodo"].ToString() + "@#@"); //8
                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());
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
                    //case 15://Columnas a sacar
                    //    if (strMagnitudes != "") strMagnitudes += "///";
                    //    strMagnitudes += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
                    //    break;
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
                        if (strIDsOrgComercial != "") strIDsOrgComercial += ",";
                        strIDsOrgComercial += dr["t441_valor"].ToString();
                        strHTMLOrgComercial += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion

            if (nConceptoEje >= 7)
            {
                nOpcion = nConceptoEje;
            }
            else
            {
                if (nNivelMinimo != 0) nOpcion = nNivelMinimo;
                else nOpcion = nEstructuraMinima;
            }

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
            sb.Append(strHTMLOrgComercial + "@#@"); //46
            sb.Append(strIDsOrgComercial + "@#@"); //47

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }

}
