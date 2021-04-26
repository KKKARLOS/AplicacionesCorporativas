using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using SUPER.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "", strArrayMeses = "", strTablaHTMLReconocimiento="";
    protected int nLineaBase = 0, nMesReferencia = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int idPSN = -1;
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 58;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.TituloPagina = "Gestión del Valor Ganado";
                Master.FuncionesJavaScript.Add("Javascript/fechas.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/ValorGanado/Functions/ValorGanado.js");

                if (Session["MONEDA_VDP"] != null)
                {
                    lblMonedaImportes.InnerText = Session["DENOMINACION_VDP"].ToString();
                }
                else
                {
                    if (Session["ID_PROYECTOSUBNODO"].ToString() != "")
                        lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(Session["MONEDA_PROYECTOSUBNODO"].ToString());
                }
                if ((Session["MONEDA_VDP"] != null || Session["ID_PROYECTOSUBNODO"] != null))// && (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    divMonedaImportes.Style.Add("visibility", "visible");

                if (Request.QueryString["lp"] != null)
                {
                    Session["VALORGANADO_LISTA_PE"] = Request.QueryString["lp"].ToString();
                }
                //else
                //    Session["VALORGANADO_LISTA_PE"] = null;

                if (Request.QueryString["sp"] != null)
                {
                    string[] aPSN = Regex.Split(Utilidades.decodpar(Request.QueryString["sp"].ToString()), "///");
                    idPSN = int.Parse(aPSN[0]);
                    Session["ID_PROYECTOSUBNODO"] = aPSN[0];
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (aPSN[1] == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = (aPSN[2] == "1") ? true : false;
                    Session["MONEDA_PROYECTOSUBNODO"] = aPSN[3];
                }
                else
                {
                    if (Request.QueryString["pe"] != null && !Page.IsPostBack)
                    {//Vengo del gráfico de la consulta de proyectos con línea base
                        int idPE = int.Parse(Utilidades.decodpar(Request.QueryString["pe"].ToString()));
                        
                        SqlDataReader dr = SUPER.Capa_Negocio.PROYECTO.GetInstanciaContratante(null, idPE);
                        if (dr.Read())
                        {
                            idPSN = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                            Session["ID_PROYECTOSUBNODO"] = idPSN.ToString();
                            Session["RTPT_PROYECTOSUBNODO"] = false;
                        }
                        dr.Close();
                        dr.Dispose();

                        SqlDataReader dr2 = PROYECTO.ObtenerProyectosByNumPE("pge", idPE, (int)Session["UsuarioActual"], false, false, false);
                        bool sw = false;
                        while (dr2.Read())
                        {
                            if (dr2["t305_cualidad"].ToString() == "J") continue;
                            if (!sw)
                            {
                                Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr2["modo_lectura"].ToString() == "1") ? true : false;
                                Session["MONEDA_PROYECTOSUBNODO"] = dr2["t422_idmoneda_proyecto"].ToString();
                                sw = true;
                            }
                        }
                        dr2.Close();
                        dr2.Dispose();
                    }
                }

                if (!Page.IsPostBack && Session["ID_PROYECTOSUBNODO"].ToString() != "")
                {
                    idPSN = int.Parse(Session["ID_PROYECTOSUBNODO"].ToString());
                    //Si no es solo RTPT, es que es algo más en el proyecto.
                    if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "" ||
                        PROYECTO.flEsSoloRtpt(null, idPSN, (int)Session["UsuarioActual"]) == "N")
                    {
                        PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, idPSN);
                        txtNumPE.Text = oPSN.t301_idproyecto.ToString("#,###");
                        txtDesPE.Text = PROYECTO.Obtener(null, oPSN.t301_idproyecto).t301_denominacion;


                        //1º a partir del PSN, obtener su última línea base, para a continuación obtener los datos
                        nLineaBase = LINEABASE.ObtenerUltimaLineaBase(idPSN);
                        if (nLineaBase > 0)
                        {
                            hdnIdLineaBase.Text = nLineaBase.ToString();
                            hdnIdProyectoSubNodo.Text = idPSN.ToString();

                            LINEABASE oLB = LINEABASE.Obtener(nLineaBase);
                            txtLineaBase.Text = oLB.t685_denominacion;
                            hdnFechaLineaBase.Text = oLB.t685_fecha.ToShortDateString();
                            hdnAutorLineaBase.Text = oLB.Promotor;
                            lblNumeroLineaBase.Text = "(" + oLB.numero_lb.ToString() + "ª de " + oLB.count_lb.ToString() + ")";

                            nMesReferencia = LINEABASE.ObtenerMesDefecto(nLineaBase);
                            //if (nMesReferencia == 190001) nMesReferencia = SUPER.Capa_Negocio.Fechas.FechaAAnnomes(DateTime.Now);
                            hdnMesReferencia.Text = nMesReferencia.ToString();

                            ObtenerDatosProyecto();
                        }
                    }
                }
                if (Page.IsPostBack)
                {
                    nLineaBase = int.Parse(hdnIdLineaBase.Text);
                    if (hdnSWCambioLineaBase.Text == "1")
                    {
                        LINEABASE oLB = LINEABASE.Obtener(nLineaBase);
                        txtLineaBase.Text = oLB.t685_denominacion;
                        hdnFechaLineaBase.Text = oLB.t685_fecha.ToShortDateString();
                        hdnAutorLineaBase.Text = oLB.Promotor;
                        lblNumeroLineaBase.Text = "(" + oLB.numero_lb.ToString() + "ª de " + oLB.count_lb.ToString() + ")";
                        nMesReferencia = LINEABASE.ObtenerMesDefecto(nLineaBase);
                        //if (nMesReferencia == 190001) nMesReferencia = SUPER.Capa_Negocio.Fechas.FechaAAnnomes(DateTime.Now);

                        hdnMesReferencia.Text = nMesReferencia.ToString();
                    }
                    else
                        nMesReferencia = int.Parse(hdnMesReferencia.Text);
                    ObtenerDatosProyecto();
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
        string sResultado = "";//, sCad = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("ActualizarMes"):
                sResultado += ActualizarMes(aArgs[1], aArgs[2]);
                break;
            case ("getReconocimiento"):
                sResultado += getReconocimiento(aArgs[1], aArgs[2]);
                break;
            case ("eliminar"):
                sResultado += eliminar(aArgs[1]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 6: //OBSERVACIONES
                        try
                        {
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + OBSERVACIONES_LB.ObtenerCatalogo(int.Parse(aArgs[2]));
                        }
                        catch (Exception ex)
                        {
                            sResultado += "Error@#@" + aArgs[1] + "@#@" + Errores.mostrarError("Error al obtener las observaciones.", ex);
                        }
                        break;
                }
                break;
            case ("addObservacion"):
                try{
                    OBSERVACIONES_LB.Insertar(int.Parse(aArgs[1]), (int)Session["IDFICEPI_ENTRADA"], false, Utilidades.unescape(aArgs[2]));
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + aArgs[1] + "@#@" + Errores.mostrarError("Error al obtener las observaciones.", ex);
                }
                break;
            case ("delObservacion"):
                try
                {
                    OBSERVACIONES_LB.Eliminar(int.Parse(aArgs[1]));
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + aArgs[1] + "@#@" + Errores.mostrarError("Error al eliminar la observación.", ex);
                }
                break;
            case ("updObservacion"):
                try
                {
                    OBSERVACIONES_LB.Modificar(int.Parse(aArgs[1]), Utilidades.unescape(aArgs[2]));
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + aArgs[1] + "@#@" + Errores.mostrarError("Error al eliminar la observación.", ex);
                }
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

    private void ObtenerDatosProyecto()
    {
        if (nLineaBase > 0)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = LINEABASE.ObtenerDatosValorGanado(nLineaBase, nMesReferencia, (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());
            Session["DS_DATOSLB"] = ds;
            #region Datos
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("insertarVGEnArray(");
                sb.Append(oFila["anomes"].ToString() + ",\"");
                sb.Append(oFila["estado"].ToString() + "\",");
                sb.Append(oFila["t305_idproyectosubnodo"].ToString() + ",");
                sb.Append(oFila["consumo_lb_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["consumo_iap_lb_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["consumo_ext_lb_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["consumo_oco_lb_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["consumo_ext_reconocido_lb_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["produccion_lb_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["consumo_real_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["produccion_real_acum_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["avance_tecnico_teorico_a_mes"].ToString().Replace(",", ".") + ",");
                sb.Append(((oFila["AC"] == DBNull.Value) ? "0" : oFila["AC"].ToString().Replace(",", ".")) + ",");
                sb.Append(((oFila["AC_IAP"] == DBNull.Value) ? "0" : oFila["AC_IAP"].ToString().Replace(",", ".")) + ",");
                sb.Append(((oFila["AC_EXT"] == DBNull.Value) ? "0" : oFila["AC_EXT"].ToString().Replace(",", ".")) + ",");
                sb.Append(((oFila["AC_OCO"] == DBNull.Value) ? "0" : oFila["AC_OCO"].ToString().Replace(",", ".")) + ",");
                sb.Append(oFila["BAC"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["BAC_IAP"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["BAC_EXT"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["BAC_OCO"].ToString().Replace(",", ".") + ",");
                //sb.Append(oFila["CPI"].ToString().Replace(",", ".") + ",");
                sb.Append(((oFila["CPI"] == DBNull.Value) ? "0" : oFila["CPI"].ToString().Replace(",", ".")) + ",");
                sb.Append(oFila["CV"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["EAC"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["EAC1"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["EAC2"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["EAC3"].ToString().Replace(",", ".") + ",");
                sb.Append("\"" + ((oFila["EACt"] == DBNull.Value) ? "" : ((DateTime)oFila["EACt"]).ToShortDateString()) + "\",");
                sb.Append(oFila["ETC"].ToString().Replace(",", ".") + ",");
                sb.Append(((oFila["EV"] == DBNull.Value) ? "0" : oFila["EV"].ToString().Replace(",", ".")) + ",");
                sb.Append(((oFila["EV_IAP"] == DBNull.Value) ? "0" : oFila["EV_IAP"].ToString().Replace(",", ".")) + ",");
                sb.Append(((oFila["EV_EXT"] == DBNull.Value) ? "0" : oFila["EV_EXT"].ToString().Replace(",", ".")) + ",");
                sb.Append(((oFila["EV_OCO"] == DBNull.Value) ? "0" : oFila["EV_OCO"].ToString().Replace(",", ".")) + ",");
                sb.Append(oFila["PV"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["PV_IAP"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["PV_EXT"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["PV_OCO"].ToString().Replace(",", ".") + ",");
                //sb.Append(oFila["SPI"].ToString().Replace(",", ".") + ",");
                sb.Append(((oFila["SPI"] == DBNull.Value) ? "0" : oFila["SPI"].ToString().Replace(",", ".")) + ",");
                sb.Append(oFila["SV"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["VAC"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["produccion_lb_total"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["PAC"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["PAC1"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["PAC2"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["PAC3"].ToString().Replace(",", ".") + ",");
                sb.Append(((oFila["TCPI"] == DBNull.Value) ? "0" : oFila["TCPI"].ToString().Replace(",", ".")) + ",");
                sb.Append(((oFila["TSPI"] == DBNull.Value) ? "0" : oFila["TSPI"].ToString().Replace(",", ".")) + ",");
                sb.Append(oFila["codigo_1"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_2"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_3"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_4"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_5"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_6"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_7"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_8"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_9"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_10"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_11"].ToString().Replace(",", ".") + ",");
                //sb.Append("\"" + ((oFila["codigo_12"] == DBNull.Value) ? "" : ((DateTime)oFila["codigo_12"]).ToShortDateString()) + "\",");
                sb.Append("\"" + ((oFila["codigo_12"] == DBNull.Value) ? "" : ((DateTime)oFila["codigo_12"]).ToLongDateString()) + "\",");
                sb.Append(oFila["codigo_13"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_14"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_15"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_16"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_17"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_18"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_19"].ToString().Replace(",", ".") + ",");
                sb.Append(oFila["codigo_20"].ToString().Replace(",", ".") + ");\n");
            }

            strArrayMeses = sb.ToString();
            #endregion

            #region Graficos

            #region Origen datos
            DataView dv;
            int nMesesMax = 24;
            int nDivisor = 1;
            double nSPI_MR = 0; //SPI Mes de referencia
            double nCPI_MR = 0; //CPI Mes de referencia

            if (ds.Tables[0].Rows.Count > nMesesMax)
            {
                DataTable dt = ds.Tables[0].Clone();
                //int nFilas = dt.Rows.Count;
                //for (int nFila = nFilas-1; nFila >= 0; nFila--)
                //    dt.Rows.RemoveAt(nFila);

                int nFilas = ds.Tables[0].Rows.Count;
                int nIndice = 0;

                while (nFilas / nDivisor > nMesesMax)
                {
                    nDivisor++;
                }
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    if ((int)oFila["anomes"] == nMesReferencia)
                    {
                        nSPI_MR = (oFila["SPI"] != DBNull.Value) ? double.Parse(oFila["SPI"].ToString()) : 0; //SPI Mes de referencia
                        nCPI_MR = (oFila["CPI"] != DBNull.Value) ? double.Parse(oFila["CPI"].ToString()) : 0; ; //CPI Mes de referencia
                    }
                    if (nIndice == 0 || nIndice == nFilas - 1)
                    {
                        dt.ImportRow(oFila);
                    }
                    else
                    {
                        if (nIndice % nDivisor == 0)
                            dt.ImportRow(oFila);
                    }
                    nIndice++;

                }

                dv = dt.DefaultView;
            }
            else
            {
                dv = ds.Tables[0].DefaultView;
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    if ((int)oFila["anomes"] == nMesReferencia)
                    {
                        nSPI_MR = (oFila["SPI"] != DBNull.Value) ? double.Parse(oFila["SPI"].ToString()) : 0; //SPI Mes de referencia
                        nCPI_MR = (oFila["CPI"] != DBNull.Value) ? double.Parse(oFila["CPI"].ToString()) : 0; //CPI Mes de referencia
                        break;
                    }
                }
            }

            #endregion

            #region Gráfico análisis del Valor Ganado
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;
            Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;

            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.TruncatedLabels = false;
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;


            // Creo una serie y sus atributos visuales para el grafico de TOTAL
            Series serieBAC = new Series("Total del proyecto - BAC");
            //Añado desde BBDD los puntos que conforman la serie
//            serieBAC.YValuesPerPoint = 3;
            //serieBAC.Points.DataBind(dv, "mes", "BAC_EXT,BAC_IAP,BAC_OCO", "");
            serieBAC.Points.DataBind(dv, "mes", "BAC", "");
            //foreach (DataRow oFila in ds.Tables[0].Rows)
            //{
            //    serieBAC.XValueMember = "mes";
            //    serieBAC.Points.AddY(double.Parse(oFila["BAC_IAP"].ToString()) + double.Parse(oFila["BAC_EXT"].ToString()) + double.Parse(oFila["BAC_OCO"].ToString()));
            //}
            serieBAC.Color = Color.Brown;
            //Añado la serie al gráfico
            Chart1.Series.Add(serieBAC);

            Series seriePV = new Series("Acumulado planificado - PV");
            seriePV.Points.DataBind(dv, "mes", "PV", "");
            seriePV.Color = Color.Cyan;
            Chart1.Series.Add(seriePV);

            Series serieAC = new Series("Acumulado real - AC");
            serieAC.Points.DataBind(dv, "mes", "AC", "");
            serieAC.Color = Color.Red;
            Chart1.Series.Add(serieAC);

            Series serieEV = new Series("Acum. avance técnico - EV");
            serieEV.Points.DataBind(dv, "mes", "EV", "");
            serieEV.Color = Color.Black;
            Chart1.Series.Add(serieEV);

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
                    Chart1.Series[i].ChartType = SeriesChartType.Line;
                    Chart1.Series[i].BorderWidth = 2;
                    Chart1.Series[i].ShadowOffset = 1;
                }
                Chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
                Chart1.Series[i].MarkerColor = Color.Navy;
                Chart1.Series[i].MarkerSize = 4;
                Chart1.Series[i].ToolTip = "#VALY{N0}";
            }
            //Chart1.RenderType = RenderType.ImageTag;
            //string sa = Chart1.GetHtmlImageMap("ctl00_CPHC_Chart1ImageMap");
            #region Labels Personalizados
            Chart1.Legends["Default"].Title = "COSTES";
            Chart1.Legends["Default"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
            Chart1.Legends["Default"].TitleSeparatorColor = Color.Black;

            // Disable legend item for the first series
            //Chart1.Series[0].IsVisibleInLegend = false;

            //// Add custom legend item with line style
            //LegendItem legendItem = new LegendItem();
            //legendItem.Name = "BACBAC";
            //legendItem.ToolTip = "ToolTip de BACBAC";
            //legendItem.ImageStyle = LegendImageStyle.Line;
            //legendItem.ShadowOffset = 2;
            //legendItem.Color = Color.LightBlue;
            //legendItem.MarkerStyle = MarkerStyle.Circle;
            //Chart1.Legends["Default"].CustomItems.Count; .CustomItems.Add(legendItem);
            #endregion
            #endregion

            #region Gráfico Evolución de SPI/CPI
            ChartSPI1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = false;
            ChartSPI1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Interval = 1;
            //ChartSPI1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            ChartSPI1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDot;
            //ChartSPI1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.Red;
            ChartSPI1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;
            ChartSPI1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
            ChartSPI1.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;


            ChartSPI1.ChartAreas["ChartArea1"].AxisX.LabelStyle.TruncatedLabels = false;
            ChartSPI1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;

            Series serieSPI = new Series("SPI");
            serieSPI.Points.DataBind(dv, "mes", "SPI", "");
            serieSPI.Color = Color.Brown;
            ChartSPI1.Series.Add(serieSPI);

            Series serieCPI = new Series("CPI");
            serieCPI.Points.DataBind(dv, "mes", "CPI", "");
            serieCPI.Color = Color.Cyan;
            ChartSPI1.Series.Add(serieCPI);

            for (int i = 0; i < ChartSPI1.Series.Count; i++)
            {
                if (dv.Count == 1)
                {
                    ChartSPI1.Series[i].ChartType = SeriesChartType.Column;
                    ChartSPI1.Series[i]["PointWidth"] = "0.2";
                    ChartSPI1.Series[i]["DrawingStyle"] = "Default";
                }
                else
                {
                    ChartSPI1.Series[i].ChartType = SeriesChartType.Line;
                    ChartSPI1.Series[i].BorderWidth = 2;
                    ChartSPI1.Series[i].ShadowOffset = 1;
                }
                ChartSPI1.Series[i].MarkerStyle = MarkerStyle.Circle;
                ChartSPI1.Series[i].MarkerColor = Color.Navy;
                ChartSPI1.Series[i].MarkerSize = 4;
                ChartSPI1.Series[i].ToolTip = "#VALY{N}";
            }

            StripLine sl = new StripLine();
            sl.BorderColor = Color.Black;
            sl.IntervalOffset = 1;
            sl.BorderWidth = 1;
            ChartSPI1.ChartAreas["ChartArea1"].AxisY.StripLines.Add(sl);

            #endregion

            #region Gráfico Situación de proyecto a mes de referencia

            ChartSPI2.Legends.Remove(ChartSPI2.Legends[0]);
            ChartSPI2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            ChartSPI2.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = false;
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;

            ChartSPI2.ChartAreas["ChartArea1"].AxisX.Title = "Sobrecoste <-- CPI --> Infracoste";
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.Title = "Retraso <-- SPI --> Adelanto";

            ChartSPI2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            ChartSPI2.ChartAreas["ChartArea1"].AxisX.Maximum = 2;
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.Maximum = 2;

            ChartSPI2.ChartAreas["ChartArea1"].AxisX.Interval = 10;
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.Interval = 10;

            LabelStyle ls = new LabelStyle();
            ls.Enabled = false;
            ChartSPI2.ChartAreas["ChartArea1"].AxisX.LabelStyle = ls;
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.LabelStyle = ls;

            StripLine sl1 = new StripLine();
            sl1.BorderColor = Color.Black;
            sl1.IntervalOffset = 1;
            sl1.BorderWidth = 1;
            ChartSPI2.ChartAreas["ChartArea1"].AxisX.StripLines.Add(sl1);

            StripLine sl2 = new StripLine();
            sl2.BorderColor = Color.Black;
            sl2.IntervalOffset = 1;
            sl2.BorderWidth = 1;
            ChartSPI2.ChartAreas["ChartArea1"].AxisY.StripLines.Add(sl2);

            if (nCPI_MR > 1.98) nCPI_MR = 1.98;
            else if (nCPI_MR < 0.02) nCPI_MR = 0.02;
            if (nSPI_MR > 1.98) nSPI_MR = 1.98;
            else if (nSPI_MR < 0.02) nSPI_MR = 0.02;

            //ChartSPI2.Series["Proyecto"].MarkerSize = 3;
            ChartSPI2.Series["Proyecto"].IsValueShownAsLabel = false;
            ChartSPI2.Series["Proyecto"].Font = new Font("Arial", 8.25F);
            ChartSPI2.Series["Proyecto"].Points.InsertXY(0, nCPI_MR, nSPI_MR);
            ChartSPI2.Series["Proyecto"].Points[0].MarkerStyle = MarkerStyle.Circle;
            ChartSPI2.Series["Proyecto"].Points[0].MarkerSize = 8;

            double dCSI = Math.Round(nCPI_MR * nSPI_MR, 2);

            if (dCSI < 0.9) ChartSPI2.Series["Proyecto"].Points[0].Color = Color.Red;
            else if (dCSI < 0.95) ChartSPI2.Series["Proyecto"].Points[0].Color = Color.Yellow;
            else ChartSPI2.Series["Proyecto"].Points[0].Color = Color.Green;    

            #endregion

            #region Gráfico de produccion
            ChartProduccion.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            ChartProduccion.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
            ChartProduccion.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
            ChartProduccion.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;
            ChartProduccion.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;

            ChartProduccion.ChartAreas["ChartArea1"].AxisX.LabelStyle.TruncatedLabels = false;
            ChartProduccion.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;


            // Creo una serie y sus atributos visuales para el grafico de TOTAL
            Series serieProdPlan = new Series("Acum. de producción planificada");
            serieProdPlan.Points.DataBind(dv, "mes", "produccion_lb_acum_a_mes", "");
            serieProdPlan.Color = Color.Cyan; //Color.Brown;
            ChartProduccion.Series.Add(serieProdPlan);

            Series serieProdRec = new Series("Acum. de producción reconocida");
            serieProdRec.Points.DataBind(dv, "mes", "produccion_real_acum_a_mes", "");
            serieProdRec.Color = Color.Yellow; //Color.Cyan;
            ChartProduccion.Series.Add(serieProdRec);

            Series serieProdSug = new Series("Acum. de producción sugerida");
            //serieProdSug.Points.DataBind(dv, "mes", "EV", "");
            foreach (DataRow oFila in dv.ToTable().Rows)
            {
                if (oFila["EV"] != DBNull.Value)
                    serieProdSug.Points.AddXY(oFila["mes"].ToString(), double.Parse(oFila["produccion_lb_total"].ToString()) * double.Parse(oFila["EV"].ToString()) / double.Parse(oFila["BAC"].ToString()));
            }
            serieProdSug.Color = Color.Black; //Color.Red;
            ChartProduccion.Series.Add(serieProdSug);

            Series serieProdAC = new Series("Acum. de coste total");
            serieProdAC.Points.DataBind(dv, "mes", "AC", "");
            serieProdAC.Color = Color.Red; //Color.Black;
            ChartProduccion.Series.Add(serieProdAC);

            for (int i = 0; i < ChartProduccion.Series.Count; i++)
            {
                if (dv.Count == 1)
                {
                    ChartProduccion.Series[i].ChartType = SeriesChartType.Column;
                    ChartProduccion.Series[i]["PointWidth"] = "0.2";
                    ChartProduccion.Series[i]["DrawingStyle"] = "Default";
                }
                else
                {
                    ChartProduccion.Series[i].ChartType = SeriesChartType.Line;
                    ChartProduccion.Series[i].BorderWidth = 2;
                    ChartProduccion.Series[i].ShadowOffset = 1;
                }
                ChartProduccion.Series[i].MarkerStyle = MarkerStyle.Circle;
                ChartProduccion.Series[i].MarkerColor = Color.Navy;
                ChartProduccion.Series[i].MarkerSize = 4;
                ChartProduccion.Series[i].ToolTip = "#VALY{N0}";
            }

            #region Labels Personalizados
            //ChartProduccion.Legends["Default"].Title = "COSTES";
            //ChartProduccion.Legends["Default"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
            //ChartProduccion.Legends["Default"].TitleSeparatorColor = Color.Black;

            // Disable legend item for the first series
            //ChartProduccion.Series[0].IsVisibleInLegend = false;

            //// Add custom legend item with line style
            //LegendItem legendItem = new LegendItem();
            //legendItem.Name = "BACBAC";
            //legendItem.ToolTip = "ToolTip de BACBAC";
            //legendItem.ImageStyle = LegendImageStyle.Line;
            //legendItem.ShadowOffset = 2;
            //legendItem.Color = Color.LightBlue;
            //legendItem.MarkerStyle = MarkerStyle.Circle;
            //ChartProduccion.Legends["Default"].CustomItems.Count; .CustomItems.Add(legendItem);
            #endregion
            #endregion

            #endregion

            ds.Dispose();

            strTablaHTMLReconocimiento = RECONOCIMIENTOLB.ObtenerCatalogo(nLineaBase, true, (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());

            string[] aTablas = Regex.Split(LINEABASE.ObtenerDesgloseLB(nLineaBase, (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString()), "{{septabla}}");
            divTituloMovil.InnerHtml = aTablas[0];
            divBodyFijo.InnerHtml = aTablas[1];
            divBodyMovil.InnerHtml = aTablas[2];

            tsPestanas.Items[0].Selected = true;
        }
    }
    private string getReconocimiento(string sLineaBase, string sSoloPendiente)
    {
        try
        {
            return "OK@#@" + RECONOCIMIENTOLB.ObtenerCatalogo(int.Parse(sLineaBase), (sSoloPendiente=="1") ? true : false, (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de reconocimiento.", ex);
        }
    }

    protected void Chart1_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
    {
        // Loop through all default legend items
        if (e.LegendName == "Default")
        {
            //e.LegendItems[0].ToolTip = "Volumen de negocio";
        }
    }

    public void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }

    private string recuperarPSN(string sT305IdProy)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProyConLineaBase(null, int.Parse(sT305IdProy));
            if (dr.Read())
            {
                sb.Append(sT305IdProy + "@#@");
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "@#@");
                sb.Append(Utilidades.escape(dr["t301_denominacion"].ToString()) + "@#@");
                sb.Append(dr["t685_idlineabase"].ToString() + "@#@");
                sb.Append(Utilidades.escape(dr["t685_denominacion"].ToString()) + "@#@");
                sb.Append(dr["numero_lb"].ToString() + "@#@");
                sb.Append(dr["count_lb"].ToString() + "@#@");
                sb.Append(((dr["t685_fecha"]==DBNull.Value)? "":((DateTime)dr["t685_fecha"]).ToShortDateString() ) + "@#@");
                sb.Append(dr["Promotor"].ToString() + "@#@");
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");
                sb.Append(dr["t301_estado"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }
    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            //22/05/2013 A esta pantalla se accede también desde PST            
            //SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pst", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (dr["t305_cualidad"].ToString() == "J") continue;
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
            sResul = "Error@#@" + Errores.mostrarError("Error al buscar el proyecto", ex);
        }
        return sResul;
    }
    private string ActualizarMes(string sID, string sMes)
    {
        try
        {
            RECONOCIMIENTOLB.ActualizarMesReconocimiento(int.Parse(sID), ((sMes=="")?null:(int?)int.Parse(sMes)));
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return  "Error@#@" + Errores.mostrarError("Error al actualizar el mes de reconocimiento.", ex);
        }
    }
    private string eliminar(string sLineaBase)
    {
        try
        {
            return "OK@#@" + LINEABASE.EliminarLineaBase(int.Parse(sLineaBase), (int)Session["IDFICEPI_ENTRADA"], Session["MONEDA_PROYECTOSUBNODO"].ToString());
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar la línea base.", ex);
        }
    }


    /// <summary>
    /// Create a chart image and return image file name
    /// </summary>
    /// 
    ///<param name="iType"> 
    ///Type of the chart to draw: 1=Sin(x) 2=Cos(x) 3=Tan(x)
    ///</param>
    [WebMethod]
    public static String GenerarGraficoAnalisis(int nLineaBase, int nMesReferencia, int nIAP, int nEXT, int nOCO)
    {
        DataSet ds = null;
        if (HttpContext.Current.Session["DS_DATOSLB"] != null)
            ds = (DataSet)HttpContext.Current.Session["DS_DATOSLB"];
        else
            ds = LINEABASE.ObtenerDatosValorGanado(nLineaBase, nMesReferencia, (HttpContext.Current.Session["MONEDA_VDP"] == null) ? HttpContext.Current.Session["MONEDA_PROYECTOSUBNODO"].ToString() : HttpContext.Current.Session["MONEDA_VDP"].ToString());

        #region Origen datos
        DataView dv;
        int nMesesMax = 24;
        int nDivisor = 1;
        double nSPI_MR = 0; //SPI Mes de referencia
        double nCPI_MR = 0; //CPI Mes de referencia

        if (ds.Tables[0].Rows.Count > nMesesMax)
        {
            DataTable dt = ds.Tables[0].Clone();
            //int nFilas = dt.Rows.Count;
            //for (int nFila = nFilas-1; nFila >= 0; nFila--)
            //    dt.Rows.RemoveAt(nFila);

            int nFilas = ds.Tables[0].Rows.Count;
            int nIndice = 0;

            while (nFilas / nDivisor > nMesesMax)
            {
                nDivisor++;
            }
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if ((int)oFila["anomes"] == nMesReferencia)
                {
                    nSPI_MR = (oFila["SPI"] != DBNull.Value) ? double.Parse(oFila["SPI"].ToString()) : 0; //SPI Mes de referencia
                    nCPI_MR = (oFila["CPI"] != DBNull.Value) ? double.Parse(oFila["CPI"].ToString()) : 0; ; //CPI Mes de referencia
                }
                if (nIndice == 0 || nIndice == nFilas - 1)
                {
                    dt.ImportRow(oFila);
                }
                else
                {
                    if (nIndice % nDivisor == 0)
                        dt.ImportRow(oFila);
                }
                nIndice++;

            }

            dv = dt.DefaultView;
        }
        else
        {
            dv = ds.Tables[0].DefaultView;
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if ((int)oFila["anomes"] == nMesReferencia)
                {
                    nSPI_MR = (oFila["SPI"] != DBNull.Value) ? double.Parse(oFila["SPI"].ToString()) : 0; //SPI Mes de referencia
                    nCPI_MR = (oFila["CPI"] != DBNull.Value) ? double.Parse(oFila["CPI"].ToString()) : 0; //CPI Mes de referencia
                    break;
                }
            }
        }

        #endregion

        #region Gráfico análisis del Valor Ganado

        Chart Chart1 = Grafico.GenerarGraficoAnalisis();

        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Interval = 1;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 1;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;
        Chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightGray;

        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.TruncatedLabels = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;


        Series serieBAC = new Series("Total del proyecto - BAC");
        Series seriePV = new Series("Acumulado planificado - PV");
        Series serieAC = new Series("Acumulado real - AC");
        Series serieEV = new Series("Acum. avance técnico - EV");

        //Añado desde BBDD los puntos que conforman la serie
        //serieBAC.Points.DataBind(dv, "mes", "BAC", "");
        //seriePV.Points.DataBind(dv, "mes", "PV_IAP", "");
        //serieAC.Points.DataBind(dv, "mes", "AC_IAP", "");
        //serieEV.Points.DataBind(dv, "mes", "EV_IAP", "");

        serieBAC.Color = Color.Brown;
        seriePV.Color = Color.Cyan;
        serieAC.Color = Color.Red;
        serieEV.Color = Color.Black;

        //Chart1.IsMapEnabled = true;

        decimal? nBAC = 0, nPV = 0, nAC = 0, nEV = 0;
        foreach (DataRow oFila in dv.ToTable().Rows)
        {
            //CustomLabel oCL = new CustomLabel(
            //Chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(   //AxisY.CustomLabels.Add(y, y + 1, labelY);
            //serieBAC.XValueMember = oFila["mes"].ToString();
            nBAC = ((nIAP == 1 && oFila["BAC_IAP"] != DBNull.Value) ? decimal.Parse(oFila["BAC_IAP"].ToString()) : 0)
                    + ((nEXT == 1 && oFila["BAC_EXT"] != DBNull.Value) ? decimal.Parse(oFila["BAC_EXT"].ToString()) : 0)
                    + ((nOCO == 1 && oFila["BAC_OCO"] != DBNull.Value) ? decimal.Parse(oFila["BAC_OCO"].ToString()) : 0);
            serieBAC.Points.AddXY(oFila["mes"].ToString(), (nBAC==0)?null:nBAC);

            //seriePV.XValueMember = oFila["mes"].ToString();
            nPV = ((nIAP == 1 && oFila["PV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["PV_IAP"].ToString()) : 0)
                    + ((nEXT == 1 && oFila["PV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["PV_EXT"].ToString()) : 0)
                    + ((nOCO == 1 && oFila["PV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["PV_OCO"].ToString()) : 0);
            seriePV.Points.AddXY(oFila["mes"].ToString(), (nPV == 0) ? null : nPV);

            //serieAC.XValueMember = oFila["mes"].ToString();
            nAC = ((nIAP == 1 && oFila["AC_IAP"] != DBNull.Value) ? decimal.Parse(oFila["AC_IAP"].ToString()) : 0)
                    + ((nEXT == 1 && oFila["AC_EXT"] != DBNull.Value) ? decimal.Parse(oFila["AC_EXT"].ToString()) : 0)
                    + ((nOCO == 1 && oFila["AC_OCO"] != DBNull.Value) ? decimal.Parse(oFila["AC_OCO"].ToString()) : 0);
            serieAC.Points.AddXY(oFila["mes"].ToString(), (nAC == 0) ? null :nAC);

            //serieEV.XValueMember = oFila["mes"].ToString();
            nEV = ((nIAP == 1 && oFila["EV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["EV_IAP"].ToString()) : 0)
                    + ((nEXT == 1 && oFila["EV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["EV_EXT"].ToString()) : 0)
                    + ((nOCO == 1 && oFila["EV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["EV_OCO"].ToString()) : 0);
            serieEV.Points.AddXY(oFila["mes"].ToString(), (nEV == 0) ? null : nEV);
        }
        //Añado la serie al gráfico
        Chart1.Series.Add(serieBAC);
        Chart1.Series.Add(seriePV);
        Chart1.Series.Add(serieAC);
        Chart1.Series.Add(serieEV);

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
                Chart1.Series[i].ChartType = SeriesChartType.Line;
                Chart1.Series[i].BorderWidth = 2;
                Chart1.Series[i].ShadowOffset = 1;
            }
            Chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
            Chart1.Series[i].MarkerColor = Color.Navy;
            Chart1.Series[i].MarkerSize = 4;
            Chart1.Series[i].ToolTip = "#VALY{N0}";
        }

        //Chart1.RenderType = RenderType.ImageMap;
        //string sa = Chart1.GetHtmlImageMap("TestMap");
        #region Labels Personalizados
        Chart1.Legends["Default"].Title = "COSTES";
        Chart1.Legends["Default"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
        Chart1.Legends["Default"].TitleSeparatorColor = Color.Black;

        #endregion
        #endregion

        ds.Dispose();

        String sFileGraficoAnalisis = String.Format("TempImagesGraficos/Chart_{0}.png", System.Guid.NewGuid().ToString());
        String sImageSrcGraficoAnalisis = @"../../../" + sFileGraficoAnalisis;
        //tempFileName = HttpContext.Current.Server.MapPath(tempFileName);
        sFileGraficoAnalisis = HttpContext.Current.Server.MapPath(sImageSrcGraficoAnalisis);
        Chart1.SaveImage(sFileGraficoAnalisis);

        string sMapHTML = Chart1.GetHtmlImageMap("ctl00_CPHC_Chart1ImageMap");  //GetHtmlImageMap: hay que hacerlo después de grabar la imagen. En caso contrario, devuelve el mapa vacío.

        return sImageSrcGraficoAnalisis +"{sep}"+ sMapHTML;
    }

    [WebMethod]
    public static String GenerarGraficoCPISPI(int nLineaBase, int nMesReferencia, int nIAP, int nEXT, int nOCO)
    {
        DataSet ds = null;
        if (HttpContext.Current.Session["DS_DATOSLB"] != null)
            ds = (DataSet)HttpContext.Current.Session["DS_DATOSLB"];
        else
            ds = LINEABASE.ObtenerDatosValorGanado(nLineaBase, nMesReferencia, (HttpContext.Current.Session["MONEDA_VDP"] == null) ? HttpContext.Current.Session["MONEDA_PROYECTOSUBNODO"].ToString() : HttpContext.Current.Session["MONEDA_VDP"].ToString());

        #region Origen datos
        DataView dv;
        int nMesesMax = 24;
        int nDivisor = 1;
        double nSPI_MR = 0; //SPI Mes de referencia
        double nCPI_MR = 0; //CPI Mes de referencia
        decimal? nSPI = 0, nCPI = 0, nEV = 0, nAC = 0, nPV = 0;

        if (ds.Tables[0].Rows.Count > nMesesMax)
        {
            DataTable dt = ds.Tables[0].Clone();
            //int nFilas = dt.Rows.Count;
            //for (int nFila = nFilas-1; nFila >= 0; nFila--)
            //    dt.Rows.RemoveAt(nFila);

            int nFilas = ds.Tables[0].Rows.Count;
            int nIndice = 0;

            while (nFilas / nDivisor > nMesesMax)
            {
                nDivisor++;
            }
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if ((int)oFila["anomes"] == nMesReferencia)
                {
                    //nSPI_MR = (oFila["SPI"] != DBNull.Value) ? double.Parse(oFila["SPI"].ToString()) : 0; //SPI Mes de referencia
                    //nCPI_MR = (oFila["CPI"] != DBNull.Value) ? double.Parse(oFila["CPI"].ToString()) : 0; ; //CPI Mes de referencia

                    nEV = ((nIAP == 1 && oFila["EV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["EV_IAP"].ToString()) : 0)
                            + ((nEXT == 1 && oFila["EV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["EV_EXT"].ToString()) : 0)
                            + ((nOCO == 1 && oFila["EV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["EV_OCO"].ToString()) : 0);
                    nAC = ((nIAP == 1 && oFila["AC_IAP"] != DBNull.Value) ? decimal.Parse(oFila["AC_IAP"].ToString()) : 0)
                            + ((nEXT == 1 && oFila["AC_EXT"] != DBNull.Value) ? decimal.Parse(oFila["AC_EXT"].ToString()) : 0)
                            + ((nOCO == 1 && oFila["AC_OCO"] != DBNull.Value) ? decimal.Parse(oFila["AC_OCO"].ToString()) : 0);
                    nPV = ((nIAP == 1 && oFila["PV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["PV_IAP"].ToString()) : 0)
                            + ((nEXT == 1 && oFila["PV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["PV_EXT"].ToString()) : 0)
                            + ((nOCO == 1 && oFila["PV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["PV_OCO"].ToString()) : 0);

                    nSPI_MR = (double)((nPV == 0) ? 0 : nEV / nPV);
                    nCPI_MR = (double)((nAC == 0) ? 0 : nEV / nAC);
                }
                if (nIndice == 0 || nIndice == nFilas - 1)
                {
                    dt.ImportRow(oFila);
                }
                else
                {
                    if (nIndice % nDivisor == 0)
                        dt.ImportRow(oFila);
                }
                nIndice++;

            }

            dv = dt.DefaultView;
        }
        else
        {
            dv = ds.Tables[0].DefaultView;
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if ((int)oFila["anomes"] == nMesReferencia)
                {
                    //nSPI_MR = (oFila["SPI"] != DBNull.Value) ? double.Parse(oFila["SPI"].ToString()) : 0; //SPI Mes de referencia
                    //nCPI_MR = (oFila["CPI"] != DBNull.Value) ? double.Parse(oFila["CPI"].ToString()) : 0; //CPI Mes de referencia

                    nEV = ((nIAP == 1 && oFila["EV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["EV_IAP"].ToString()) : 0)
                            + ((nEXT == 1 && oFila["EV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["EV_EXT"].ToString()) : 0)
                            + ((nOCO == 1 && oFila["EV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["EV_OCO"].ToString()) : 0);
                    nAC = ((nIAP == 1 && oFila["AC_IAP"] != DBNull.Value) ? decimal.Parse(oFila["AC_IAP"].ToString()) : 0)
                            + ((nEXT == 1 && oFila["AC_EXT"] != DBNull.Value) ? decimal.Parse(oFila["AC_EXT"].ToString()) : 0)
                            + ((nOCO == 1 && oFila["AC_OCO"] != DBNull.Value) ? decimal.Parse(oFila["AC_OCO"].ToString()) : 0);
                    nPV = ((nIAP == 1 && oFila["PV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["PV_IAP"].ToString()) : 0)
                            + ((nEXT == 1 && oFila["PV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["PV_EXT"].ToString()) : 0)
                            + ((nOCO == 1 && oFila["PV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["PV_OCO"].ToString()) : 0);

                    nSPI_MR = (double)((nPV == 0) ? 0 : nEV / nPV);
                    nCPI_MR = (double)((nAC == 0) ? 0 : nEV / nAC);
                    break;
                }
            }
        }

        #endregion

        #region Gráfico Evolución de SPI/CPI

        Chart ChartSPI1 = Grafico.GenerarGraficoCPISPI();

        ChartSPI1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = false;
        ChartSPI1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Interval = 1;
        //ChartSPI1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
        ChartSPI1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDot;
        //ChartSPI1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.Red;
        ChartSPI1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;
        ChartSPI1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
        ChartSPI1.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;

        ChartSPI1.ChartAreas["ChartArea1"].AxisX.LabelStyle.TruncatedLabels = false;
        ChartSPI1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;

        Series serieCPI = new Series("CPI");
        Series serieSPI = new Series("SPI");

        //Añado desde BBDD los puntos que conforman la serie
        //        serieSPI.Points.DataBind(dv, "mes", "SPI", "");
        //        serieCPI.Points.DataBind(dv, "mes", "CPI", "");

        foreach (DataRow oFila in dv.ToTable().Rows)
        {
            nCPI = 0;
            nEV = ((nIAP == 1 && oFila["EV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["EV_IAP"].ToString()) : 0)
                    + ((nEXT == 1 && oFila["EV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["EV_EXT"].ToString()) : 0)
                    + ((nOCO == 1 && oFila["EV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["EV_OCO"].ToString()) : 0);
            nAC = ((nIAP == 1 && oFila["AC_IAP"] != DBNull.Value) ? decimal.Parse(oFila["AC_IAP"].ToString()) : 0)
                    + ((nEXT == 1 && oFila["AC_EXT"] != DBNull.Value) ? decimal.Parse(oFila["AC_EXT"].ToString()) : 0)
                    + ((nOCO == 1 && oFila["AC_OCO"] != DBNull.Value) ? decimal.Parse(oFila["AC_OCO"].ToString()) : 0);
            nPV = ((nIAP == 1 && oFila["PV_IAP"] != DBNull.Value) ? decimal.Parse(oFila["PV_IAP"].ToString()) : 0)
                    + ((nEXT == 1 && oFila["PV_EXT"] != DBNull.Value) ? decimal.Parse(oFila["PV_EXT"].ToString()) : 0)
                    + ((nOCO == 1 && oFila["PV_OCO"] != DBNull.Value) ? decimal.Parse(oFila["PV_OCO"].ToString()) : 0);

            nSPI = (nPV == 0) ? 0 : nEV / nPV;
            nCPI = (nAC == 0) ? 0 : nEV / nAC;

            serieSPI.Points.AddXY(oFila["mes"].ToString(), (nSPI == 0) ? null : nSPI);
            serieCPI.Points.AddXY(oFila["mes"].ToString(), (nCPI == 0) ? null : nCPI);
        }
        serieSPI.Color = Color.Brown;
        serieCPI.Color = Color.Cyan;

        //Añado la serie al gráfico
        ChartSPI1.Series.Add(serieSPI);
        ChartSPI1.Series.Add(serieCPI);

        for (int i = 0; i < ChartSPI1.Series.Count; i++)
        {
            if (dv.Count == 1)
            {
                ChartSPI1.Series[i].ChartType = SeriesChartType.Column;
                ChartSPI1.Series[i]["PointWidth"] = "0.2";
                ChartSPI1.Series[i]["DrawingStyle"] = "Default";
            }
            else
            {
                ChartSPI1.Series[i].ChartType = SeriesChartType.Line;
                ChartSPI1.Series[i].BorderWidth = 2;
                ChartSPI1.Series[i].ShadowOffset = 1;
            }
            ChartSPI1.Series[i].MarkerStyle = MarkerStyle.Circle;
            ChartSPI1.Series[i].MarkerColor = Color.Navy;
            ChartSPI1.Series[i].MarkerSize = 4;
            ChartSPI1.Series[i].ToolTip = "#VALY{N}";
        }

        StripLine sl = new StripLine();
        sl.BorderColor = Color.Black;
        sl.IntervalOffset = 1;
        sl.BorderWidth = 1;
        ChartSPI1.ChartAreas["ChartArea1"].AxisY.StripLines.Add(sl);

        #endregion

        #region Gráfico de Situación de Proyecto a mes de referencia

        Chart ChartSPI2 = Grafico.GenerarGraficoSituacion();

        ChartSPI2.Legends.Remove(ChartSPI2.Legends[0]);
        ChartSPI2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        ChartSPI2.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = false;
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;

        ChartSPI2.ChartAreas["ChartArea1"].AxisX.Title = "Sobrecoste <-- CPI --> Infracoste";
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.Title = "Retraso <-- SPI --> Adelanto";

        ChartSPI2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
        ChartSPI2.ChartAreas["ChartArea1"].AxisX.Maximum = 2;
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.Maximum = 2;

        ChartSPI2.ChartAreas["ChartArea1"].AxisX.Interval = 10;
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.Interval = 10;

        LabelStyle ls = new LabelStyle();
        ls.Enabled = false;
        ChartSPI2.ChartAreas["ChartArea1"].AxisX.LabelStyle = ls;
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.LabelStyle = ls;

        StripLine sl1 = new StripLine();
        sl1.BorderColor = Color.Black;
        sl1.IntervalOffset = 1;
        sl1.BorderWidth = 1;
        ChartSPI2.ChartAreas["ChartArea1"].AxisX.StripLines.Add(sl1);

        StripLine sl2 = new StripLine();
        sl2.BorderColor = Color.Black;
        sl2.IntervalOffset = 1;
        sl2.BorderWidth = 1;
        ChartSPI2.ChartAreas["ChartArea1"].AxisY.StripLines.Add(sl2);

        if (nCPI_MR > 1.98) nCPI_MR = 1.98;
        else if (nCPI_MR < 0.02) nCPI_MR = 0.02;
        if (nSPI_MR > 1.98) nSPI_MR = 1.98;
        else if (nSPI_MR < 0.02) nSPI_MR = 0.02;

        //ChartSPI2.Series["Proyecto"].MarkerSize = 3;
        ChartSPI2.Series["Proyecto"].IsValueShownAsLabel = false;
        ChartSPI2.Series["Proyecto"].Font = new Font("Arial", 8.25F);
        ChartSPI2.Series["Proyecto"].Points.InsertXY(0, nCPI_MR, nSPI_MR);
        ChartSPI2.Series["Proyecto"].Points[0].MarkerStyle = MarkerStyle.Circle;
        ChartSPI2.Series["Proyecto"].Points[0].MarkerSize = 1;

        #endregion

        ds.Dispose();

        String sFileGraficoCPISPI = String.Format("TempImagesGraficos/Chart_{0}.png", System.Guid.NewGuid().ToString());
        String sImageSrcGraficoCPISPI = @"../../../" + sFileGraficoCPISPI;
        //tempFileName = HttpContext.Current.Server.MapPath(tempFileName);
        sFileGraficoCPISPI = HttpContext.Current.Server.MapPath(sImageSrcGraficoCPISPI);
        ChartSPI1.SaveImage(sFileGraficoCPISPI);

        String sFileGraficoSituacion = String.Format("TempImagesGraficos/Chart_{0}.png", System.Guid.NewGuid().ToString());
        String sImageSrcGraficoSituacion = @"../../../" + sFileGraficoSituacion;
        //tempFileName = HttpContext.Current.Server.MapPath(tempFileName);
        sFileGraficoSituacion = HttpContext.Current.Server.MapPath(sImageSrcGraficoSituacion);
        ChartSPI2.SaveImage(sFileGraficoSituacion);

        string sMapHTML = ChartSPI1.GetHtmlImageMap("ctl00_CPHC_ChartSPI1ImageMap");  //GetHtmlImageMap: hay que hacerlo después de grabar la imagen. En caso contrario, devuelve el mapa vacío.

        return sImageSrcGraficoCPISPI + "{sep}" + sImageSrcGraficoSituacion + "{sep}" + sMapHTML;
    }

}
