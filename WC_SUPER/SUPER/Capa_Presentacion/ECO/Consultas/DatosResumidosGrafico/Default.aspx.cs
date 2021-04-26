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
//Para los gráficos
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";
    ArrayList aSubnodos = new ArrayList();
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLHorizontal = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLQn = "", strHTMLQ1 = "", strHTMLQ2 = "", strHTMLQ3 = "", strHTMLQ4 = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsHorizontal = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsQn = "", strIDsQ1 = "", strIDsQ2 = "", strIDsQ3 = "", strIDsQ4 = "", strIDsProyecto = "";
    public short nPantallaPreferencia = 31;

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        InitializeComponent();
        base.OnInit(e);
    }
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Chart1.Customize += new EventHandler(this.Chart1_Customize);
        this.Chart2.Customize += new EventHandler(this.Chart2_Customize);
        this.Chart3.Customize += new EventHandler(this.Chart3_Customize);
        this.Chart4.Customize += new EventHandler(this.Chart4_Customize);
    }
    protected void Chart3_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
    {
        // Loop through all default legend items
        try
        {
            if (e.LegendName == "Default")
            {
                //e.LegendItems[7].ToolTip = "Bonos de transporte";
                //e.LegendItems[8].ToolTip = "Pagos concertados";
            }
        }
        catch { }
    }
    protected void Chart4_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
    {
        // Loop through all default legend items
        try
        {
            if (e.LegendName == "Default")
            {
                //e.LegendItems[7].ToolTip = "Bonos de transporte";
                //e.LegendItems[8].ToolTip = "Pagos concertados";
            }
        }
        catch { }
    }
    private void Chart1_Customize(Object sender, EventArgs args)
    {
        // Get X and Y axis labels collections
        CustomLabelsCollection xAxisLabels = Chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels;

        // Change Y axis labels
        for (int labelIndex = 0; labelIndex < xAxisLabels.Count; labelIndex++)
        {
            xAxisLabels[labelIndex].Text = AnnomesAFechaDescCorta(xAxisLabels[labelIndex].Text);
        }

    }
    private void Chart2_Customize(Object sender, EventArgs args)
    {
        // Get X and Y axis labels collections
        CustomLabelsCollection xAxisLabels = Chart2.ChartAreas["ChartArea1"].AxisX.CustomLabels;

        // Change Y axis labels
        for (int labelIndex = 0; labelIndex < xAxisLabels.Count; labelIndex++)
        {
            xAxisLabels[labelIndex].Text = AnnomesAFechaDescCorta(xAxisLabels[labelIndex].Text);
        }

    }
    private void Chart3_Customize(Object sender, EventArgs args)
    {
        // Get X and Y axis labels collections
        CustomLabelsCollection xAxisLabels = Chart3.ChartAreas["ChartArea1"].AxisX.CustomLabels;

        // Change Y axis labels
        for (int labelIndex = 0; labelIndex < xAxisLabels.Count; labelIndex++)
        {
            xAxisLabels[labelIndex].Text = AnnomesAFechaDescCorta(xAxisLabels[labelIndex].Text);
        }

    }
    private void Chart4_Customize(Object sender, EventArgs args)
    {
        // Get X and Y axis labels collections
        CustomLabelsCollection xAxisLabels = Chart4.ChartAreas["ChartArea1"].AxisX.CustomLabels;

        // Change Y axis labels
        for (int labelIndex = 0; labelIndex < xAxisLabels.Count; labelIndex++)
        {
            xAxisLabels[labelIndex].Text = AnnomesAFechaDescCorta(xAxisLabels[labelIndex].Text);
        }

    }
    #endregion

	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Resumen (Gráfico)";
                //Master.FuncionesJavaScript.Add("Javascript/FusionCharts.js");
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
                if (!Page.IsPostBack)
                {
                    this.txtAnno.Text = DateTime.Now.Year.ToString();

                    string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                    #region Lectura de preferencia
                    if (bHayPreferencia && aDatosPref[0] == "OK")
                    {
                        sHayPreferencia = "true";
                        cboEstado.SelectedValue = aDatosPref[2];
                        cboCategoria.SelectedValue = aDatosPref[3];
                        cboCualidad.SelectedValue = aDatosPref[4];
                        chkCerrarAuto.Checked = (aDatosPref[5] == "1") ? true : false;
                        chkActuAuto.Checked = (aDatosPref[6] == "1") ? true : false;
                        if (aDatosPref[7] == "1") rdbOperador.Items[0].Selected = true;
                        else rdbOperador.Items[1].Selected = true;
                        sSubnodos = aDatosPref[8];
                    }
                    else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                    #endregion

                    string[] aCriterios = Regex.Split(cargarCriterios(DateTime.Now.Year * 100 + 1, DateTime.Now.Year * 100 + 12), "@#@");
                    if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
                    else Master.sErrores = aCriterios[1];
                }
                if (this.hdnCriterios.Value != "")
                {
                    string[] aArgs = Regex.Split(this.hdnCriterios.Value, @"@#@");
                    //this.txtAnno.Text = aArgs[1];
                    //this.cboEstado.SelectedValue = aArgs[2];
                    //this.cboCategoria.SelectedValue = aArgs[3];
                    //this.cboCualidad.SelectedValue = aArgs[4];

                    string[] aCriterios = Regex.Split(cargarCriterios(int.Parse(aArgs[1]) * 100 + 1, int.Parse(aArgs[1]) * 100 + 12), "@#@");
                    if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
                    else Master.sErrores = aCriterios[1];

                    obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20]);
                }

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
            //case ("buscar"):
            //    sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20]);
            //    break;
            case ("getTablaCriterios"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]) * 100 + 1, int.Parse(aArgs[1]) * 100 + 12);
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

    private string obtenerDatos(string sAnno, string sEstado,
                                string sCategoria, string sCualidad, string sClientes, string sResponsables,
                                string sNaturalezas, string sHorizontal, string sModeloContrato,
                                string sContrato, string sIDEstructura, string sSectores, string sSegmentos,
                                string sComparacionLogica, string sCNP, string sCSN1P, string sCSN2P,
                                string sCSN3P, string sCSN4P, string sPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = ConsultasPGE.ObtenerDatosResumidosGrafico((int)Session["UsuarioActual"],
                    int.Parse(sAnno),
                    sEstado,
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
                    (sComparacionLogica == "1") ? true : false,
                    sCNP,
                    sCSN1P,
                    sCSN2P,
                    sCSN3P,
                    sCSN4P,
                    sPSN);
            //while (dr.Read())
            //{
            //    sb.Append(dr["t325_anomes"].ToString() + "##");
            //    sb.Append(dr["Ingresos_Netos"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Margen"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Obra_en_curso"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Saldo_de_Clientes"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Total_Cobros"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Total_Ingresos"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Volumen_de_Negocio"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Total_consumos"].ToString().Replace(",", ".") + "##");
            //    sb.Append(dr["Ratio"].ToString().Replace(",", ".") + "///");
            //}

            DataTable table = new DataTable();
            table.Load(dr);
            DataView dv = table.DefaultView;
            dr.Close();
            dr.Dispose();

            #region grafico 1
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;
            //Creo las series para el gráfico con cada uno de los conceptos
            //Creo una serie para el volumen de negocio
            Series serieVolNeg = new Series("Vol. negocio");
            //serieVolNeg.Points.DataBind(dv, "t325_anomes", "Volumen_de_Negocio", "");
            serieVolNeg.Points.DataBind(dv, "t325_anomes", "Volumen_de_Negocio", "");
            Chart1.Series.Add(serieVolNeg);

            //Creo una serie para el total de consumos
            Series serieConsumo = new Series("Total consumos");
            serieConsumo.Points.DataBind(dv, "t325_anomes", "Total_consumos", "");
            Chart1.Series.Add(serieConsumo);


            //Creo una serie para el total de ingresos
            Series serieIngreso = new Series("Ing. netos");
            serieIngreso.Points.DataBind(dv, "t325_anomes", "Ingresos_Netos", "");
            Chart1.Series.Add(serieIngreso);

            //Creo una serie para el margen
            Series serieMargen = new Series("Margen");
            serieMargen.Points.DataBind(dv, "t325_anomes", "Margen", "");
            Chart1.Series.Add(serieMargen);

            for (int i = 0; i < Chart1.Series.Count; i++)
            {
                if (dv.Count == 1)
                {
                    Chart1.Series[i].ChartType = SeriesChartType.Column;
                    Chart1.Series[i]["PointWidth"] = "0.2";
                    Chart1.Series[i]["DrawingStyle"] = "Default";
                }
                else
                {
                    Chart1.Series[i].ChartType = SeriesChartType.Spline;
                    Chart1.Series[i].BorderWidth = 2;
                    Chart1.Series[i].ShadowOffset = 1;
                }
                Chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
                Chart1.Series[i].MarkerColor = Color.Navy;
                Chart1.Series[i].MarkerSize = 6;
                Chart1.Series[i].ToolTip = "#VALY{C0}";
            }

            Chart1.Visible = true;

            #endregion
            #region gráfico 2
            Chart2.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            Chart2.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
            Chart2.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
            Chart2.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;
            //Creo las series para el gráfico con cada uno de los conceptos
            //Creo una serie para el total de ingresos
            Series serieTotIng = new Series("Total Ingresos");
            serieTotIng.Points.DataBind(dv, "t325_anomes", "Total_Ingresos", "");
            Chart2.Series.Add(serieTotIng);
            //Creo una serie para el total de cobros
            Series serieCobro = new Series("Total cobros");
            serieCobro.Points.DataBind(dv, "t325_anomes", "Total_Cobros", "");
            Chart2.Series.Add(serieCobro);

            for (int i = 0; i < Chart2.Series.Count; i++)
            {
                if (dv.Count == 1)
                {
                    Chart2.Series[i].ChartType = SeriesChartType.Column;
                    Chart2.Series[i]["PointWidth"] = "0.2";
                    Chart2.Series[i]["DrawingStyle"] = "Default";
                }
                else
                {
                    Chart2.Series[i].ChartType = SeriesChartType.Spline;
                    Chart2.Series[i].BorderWidth = 2;
                    Chart2.Series[i].ShadowOffset = 1;
                }
                Chart2.Series[i].MarkerStyle = MarkerStyle.Circle;
                Chart2.Series[i].MarkerColor = Color.Navy;
                Chart2.Series[i].MarkerSize = 6;
                Chart2.Series[i].ToolTip = "#VALY{C0}";
            }

            Chart2.Visible = true;

            #endregion
            #region gráfico 3
            Chart3.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            Chart3.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
            Chart3.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
            Chart3.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;
            //Creo la serie para el Ratio
            Series serieRatio = new Series("Ratio");
            serieRatio.Points.DataBind(dv, "t325_anomes", "Ratio", "");
            Chart3.Series.Add(serieRatio);

            for (int i = 0; i < Chart3.Series.Count; i++)
            {
                if (dv.Count == 1)
                {
                    Chart3.Series[i].ChartType = SeriesChartType.Column;
                    Chart3.Series[i]["PointWidth"] = "0.2";
                    Chart3.Series[i]["DrawingStyle"] = "Default";
                }
                else
                {
                    //Chart3.Series[i].ChartType = SeriesChartType.Spline;
                    Chart3.Series[i].ChartType = SeriesChartType.Area;
                    Chart3.Series[i].BorderWidth = 2;
                    Chart3.Series[i].ShadowOffset = 1;
                }
                //Chart3.Series[i].MarkerStyle = MarkerStyle.None;
                //Chart3.Series[i].MarkerColor = Color.Navy;
                //Chart3.Series[i].MarkerSize = 6;
                Chart3.Series[i].ToolTip = "#VALY{C0}";
            }

            Chart3.Visible = true;
            #endregion
            #region gráfico 4
            Chart4.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            Chart4.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
            Chart4.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
            Chart4.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;
            //Chart4.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "D";


            //Creo la serie para la obra en curso
            Series serieObra = new Series("Variación obra en curso");
            //serieObra.Points.DataBind(dv, "Mes", "Obra_en_curso", "");
            serieObra.Points.DataBind(dv, "t325_anomes", "Obra_en_curso", "");
            Chart4.Series.Add(serieObra);

            for (int i = 0; i < Chart4.Series.Count; i++)
            {
                if (dv.Count == 1)
                {
                    Chart4.Series[i].ChartType = SeriesChartType.Column;
                    Chart4.Series[i]["PointWidth"] = "0.2";
                    Chart4.Series[i]["DrawingStyle"] = "Default";
                }
                else
                {
                    //Chart4.Series[i].ChartType = SeriesChartType.Spline;
                    Chart4.Series[i].ChartType = SeriesChartType.Area;
                    Chart4.Series[i].BorderWidth = 2;
                    Chart4.Series[i].ShadowOffset = 1;
                }
                //Chart4.Series[i].MarkerStyle = MarkerStyle.Circle;
                //Chart4.Series[i].MarkerStyle = MarkerStyle.None;
                //Chart4.Series[i].MarkerColor = Color.Navy;
                //Chart4.Series[i].MarkerSize = 6;
                Chart4.Series[i].ToolTip = "#VALY{C0}";
            }

            Chart4.Visible = true;
            #endregion

            table.Dispose();
            dv.Dispose();
            
            return "OK@#@";// +sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos económicos.", ex);
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
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 31,
                                        (sEstado == "") ? null : sEstado,
                                        (sCategoria == "") ? null : sCategoria,
                                        (sCualidad == "") ? null : sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 31);
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
        //int nOpcion = 1;
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                        (int)Session["IDFICEPI_PC_ACTUAL"], 31);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["estado"].ToString() + "@#@"); //2
                sb.Append(dr["categoria"].ToString() + "@#@"); //3
                sb.Append(dr["cualidad"].ToString() + "@#@"); //4
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //5
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //6
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //7

                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
            }
            dr.Close();
            //dr.Dispose();

            #region HTML, IDs
            string[] aID = null;
            dr = PREFUSUMULTIVALOR.Obtener(null, idPrefUsuario);
            while (dr.Read())
            {
                switch (int.Parse(dr["t441_concepto"].ToString()))
                {
                    case 1:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsAmbito != "") strIDsAmbito += ",";
                        strIDsAmbito += aID[1];

                        aSubnodos = PREFUSUMULTIVALOR.SelectSubnodosAmbito(null, aSubnodos, int.Parse(aID[0]), int.Parse(aID[1]));
                        //strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:16px;'><td>";
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
                    //case 15:
                    //    if (strMagnitudes != "") strMagnitudes += "///";
                    //    strMagnitudes += dr["t441_valor"].ToString() +"##"+ dr["t441_denominacion"].ToString();
                    //    break;
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

            sb.Append(sSubnodos + "@#@"); //8
            sb.Append(strHTMLAmbito + "@#@"); //9
            sb.Append(strIDsAmbito + "@#@"); //10
            sb.Append(strHTMLResponsable + "@#@"); //11
            sb.Append(strIDsResponsable + "@#@"); //12
            sb.Append(strHTMLNaturaleza + "@#@"); //13
            sb.Append(strIDsNaturaleza + "@#@"); //14
            sb.Append(strHTMLModeloCon + "@#@"); //15
            sb.Append(strIDsModeloCon + "@#@"); //16
            sb.Append(strHTMLHorizontal + "@#@"); //17
            sb.Append(strIDsHorizontal + "@#@"); //18
            sb.Append(strHTMLSector + "@#@"); //19
            sb.Append(strIDsSector + "@#@"); //20
            sb.Append(strHTMLSegmento + "@#@"); //21
            sb.Append(strIDsSegmento + "@#@"); //22
            sb.Append(strHTMLCliente + "@#@"); //23
            sb.Append(strIDsCliente + "@#@"); //24
            sb.Append(strHTMLContrato + "@#@"); //25
            sb.Append(strIDsContrato + "@#@"); //26
            sb.Append(strHTMLQn + "@#@"); //27
            sb.Append(strIDsQn + "@#@"); //28
            sb.Append(strHTMLQ1 + "@#@"); //29
            sb.Append(strIDsQ1 + "@#@"); //30
            sb.Append(strHTMLQ2 + "@#@"); //31
            sb.Append(strIDsQ2 + "@#@"); //32
            sb.Append(strHTMLQ3 + "@#@"); //33
            sb.Append(strIDsQ3 + "@#@"); //34
            sb.Append(strHTMLQ4 + "@#@"); //35
            sb.Append(strIDsQ4 + "@#@"); //36
            sb.Append(strHTMLProyecto + "@#@"); //37
            sb.Append(strIDsProyecto + "@#@"); //38

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
    
    private string AnnomesAFechaDescCorta(string nAnnoMes)
    {
        string[] aMeses = new string[12] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
        return aMeses[int.Parse(nAnnoMes.ToString().Substring(4, 2)) - 1];
    }


}
