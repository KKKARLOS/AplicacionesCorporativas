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
//
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;


public partial class Capa_Presentacion_Consultas_Seguimiento_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string sMSC; //Mes siguiente al último mes cerrado
    protected ArrayList aSeguimiento;
    public SqlConnection oConn;
    public SqlTransaction tr;

    public double TotalPLPE, MesPE, AcumulPE, PendEstPE, TotalEstPE, TotalPRPE, TotalAVPE, TotalPrePE, TotalAvancePE, TotalProPE, TotalIndiCon, TotalIndiDes;
    public DateTime InicioPE, FinPLPE, FinEstPE, FinPRPE;
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nPantallaAcceso = 4;
            Master.nBotonera = 40;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Seguimiento de proyecto";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/NumberFormat154.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FicherosCSS.Add("Capa_Presentacion/PSP/Consultas/SeguimientoHist/Seguimiento.css");
            if (!(bool)Session["FOTOPST1024"])
            {
                Master.nResolucion = 1280;
            }
            lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
            //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                divMonedaImportes.Style.Add("visibility", "visible");

            try
            {
                this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

                if (Session["ID_PROYECTOSUBNODO"].ToString() != "")
                {
                    string sT305IdProy = Session["ID_PROYECTOSUBNODO"].ToString();
                    SqlDataReader dr = PROYECTO.fgGetDatosProy2(int.Parse(sT305IdProy));
                    if (dr.Read())
                    {
                        this.txtEstado.Text = dr["t301_estado"].ToString();
                        this.txtNomProy.Text = dr["t301_denominacion"].ToString();
                        this.txtCodProy.Text = dr["t301_idproyecto"].ToString();
                        this.t305IdProyectoSubnodo.Text = sT305IdProy;
                        this.txtUne.Text = dr["t303_idnodo"].ToString();
                        this.txtDesCR.Text = dr["t303_denominacion"].ToString();
                        this.txtNomResp.Text = dr["responsable"].ToString();
                        this.txtNomCliente.Text = dr["t302_denominacion"].ToString();
                        this.MonedaPSN.Text = dr["t422_idmoneda_proyecto"].ToString();
                        this.txtNivelPresupuesto.Text = dr["t305_nivelpresupuesto"].ToString();
                    }
                    dr.Close();
                    dr.Dispose();
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
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
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("cargar"):
                sResultado += obtenerProyectos();
                break;
            case ("getDatosProy"):
                sResultado += obtenerDatosProyecto(aArgs[1]);
                break;
            case ("getPT"):
                DateTime dAux2 = DateTime.Parse("01/" + aArgs[3]);
                dAux2 = dAux2.AddMonths(1);
                dAux2 = dAux2.AddDays(-1);
                sResultado += obtenerPT(aArgs[1], aArgs[2], dAux2.ToShortDateString(), aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("getTarea"):
                DateTime dAux3 = DateTime.Parse("01/" + aArgs[4]);
                dAux3 = dAux3.AddMonths(1);
                dAux3 = dAux3.AddDays(-1);
                sResultado += obtenerTarea(aArgs[1], aArgs[2], aArgs[3], dAux3.ToShortDateString(), aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("getFotoPT"):
                sResultado += obtenerFotoPT(aArgs[1]);
                break;
            case ("getFotoTarea"):
                sResultado += obtenerFotoTarea(aArgs[1]);
                break;
            case ("ListaFotos"):
                sResultado += obtenerListaFotos(aArgs[1]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
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

    private string obtenerProyectos()
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            //SqlDataReader dr = PROYECTO.ObtenerProyectosModuloTec((int)Session["UsuarioActual"],
            //                                                      int.Parse(Session["NodoActivo"].ToString()), "A","", null);
            SqlDataReader dr = PROYECTO.ObtenerProyectosModuloTecnico((int)Session["UsuarioActual"],
                                                                      null, "A", "", null, false, "");
            while (dr.Read())
            {
                sb.Append(dr["t301_estado"].ToString() + "///" + dr["t303_idnodo"].ToString() + "///" +
                                  dr["t301_idproyecto"].ToString() + "///" + dr["t301_denominacion"].ToString() + "///" +
                                  dr["t305_idproyectosubnodo"].ToString() + "///" + dr["t305_cualidad"].ToString() + "##");
            }
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos los proyectos", ex);
        }

        return sResul;
    }
    private string obtenerDatosProyecto(string sPSN)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy2(int.Parse(sPSN));
            if (dr.Read())
            {
                sb.Append(dr["t301_estado"].ToString());
                sb.Append("@#@");
                sb.Append(dr["t302_denominacion"].ToString());
                sb.Append("@#@");
                sb.Append(dr["responsable"].ToString());
                sb.Append("@#@");
                sb.Append(dr["t305_nivelpresupuesto"].ToString());
            }
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos del proyecto " + sPSN, ex);
        }

        return sResul;
    }
    private string obtenerListaFotos(string sPE)
    {
        string sResul = "";
        StringBuilder strBuilder = new StringBuilder();

        try
        {   //ordenado por fecha descendente
            SqlDataReader dr = FOTOSEGPE.Catalogo(null, null, null, int.Parse(sPE), null, 2, 1);
            while (dr.Read())
            {
                strBuilder.Append(dr["t373_idFotoPE"].ToString() + "///" + dr["t373_fecha"].ToString() + "///" + dr["fecIAP"].ToString() + "##");
            }
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@" + strBuilder.ToString();
            strBuilder.Length = 0; //Para liberar memoria
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la lista de instantáneas", ex);
        }

        return sResul;
    }

    private string obtenerPT(string nNumEmpleado, string sNumProyecto, string sdMSC, string sEstadoPE, string sMonedaProyecto, string sNivelPresupuesto)
    {
        string TotalPLPE = "0,00", MesPE = "0,00", AcumulPE = "0,00", PendEstPE = "0,00", TotalEstPE = "0,00", 
               TotalPRPE = "0,00", TotalAVPE = "0", TotalPrePE = "0,00", TotalAvancePE = "0", TotalProPE = "0,00", 
               TotalIndiCon = "0", TotalIndiDes = "0", TotalPdtePrevisto = "0,00";
        string InicioPE = "", FinPLPE = "", FinEstPE = "", FinPRPE = "";
        double dTotalIndiDesPlazo = 0;

        Session["ID_PROYECTOSUBNODO"] = sNumProyecto;
        Session["MONEDA_PROYECTOSUBNODO"] = sMonedaProyecto;
        string sResul = "";
        string sFecha, sFFPR, sFIPL, sFFPL;
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 1300px; text-align:right;'>");
            sb.Append("<colgroup><col style='width:185px;' />");

            sb.Append("<col style='width:70px;' />");//Planificado.Total
            sb.Append("<col style='width:60px;' />");//Planificado.Inicio
            sb.Append("<col style='width:60px;' />");//Planificado.Fin
            sb.Append("<col style='width:100px;' />");//Planificado.presupuesto

            sb.Append("<col style='width:60px;' />");//IAP.Mes
            sb.Append("<col style='width:70px;' />");//IAP.Acumulado
            sb.Append("<col style='width:65px;' />");//IAP.Pendiente Estimado
            sb.Append("<col style='width:70px;' />");//IAP.Total estimado
            sb.Append("<col style='width:60px;' />");//IAP.Fin estimado

            sb.Append("<col style='width:70px;' />");//Previsto.Total
            sb.Append("<col style='width:70px;' />");//Previsto.Pdte
            sb.Append("<col style='width:60px;' />");//Previsto.Fin
            sb.Append("<col style='width:40px;' />");//Previsto %

            sb.Append("<col style='width:40px;' />");//Avance %
            sb.Append("<col style='width:100px;' />");//Avance. Producido

            sb.Append("<col style='width:40px;' />");//Indicadores. % consumido
            sb.Append("<col style='width:40px;' />");//Indicadores.% desviación esfuerzo
            sb.Append("<col style='width:40px;' /></colgroup>");//Indicadores % desviacion plazo
            sb.Append("<tbody>");

            string sEsRtpt = PROYECTO.flEsSoloRtpt(null, int.Parse(sNumProyecto), (int)Session["UsuarioActual"]);
            SqlDataReader dr = SUPER.Capa_Negocio.SEGMESPROYECTOSUBNODO.DatosProyectoTecnico(int.Parse(sNumProyecto), DateTime.Parse(sdMSC),
                                                                          int.Parse(nNumEmpleado), sEsRtpt, 
                                                                          (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString(), sNivelPresupuesto);
            while (dr.Read())
            {
                sb.Append("<tr PE='" + sNumProyecto + "' PT='" + dr["t331_idpt"].ToString() + "' estado='" + dr["estado"].ToString() + "' cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append(" style='height:20px;' nivel='" + dr["indice"].ToString() + "' desplegado=0 exp=0 T=0>");

                sb.Append("<td style='text-align:left;' onmouseover='TTip(event);'>");  //PT
                sb.Append("<img class=NSEG1 src='../../../../images/plus.gif' onclick='mostrar(this);' style='margin-left:5px; cursor:pointer;'>");
                sb.Append("<img src='../../../../images/imgProyTecOff.gif' class='ICO'>");
                sb.Append("<nobr class='NBR W140'>" + dr["descripcion"].ToString() + "</nobr></td>");
                sb.Append("<td>");
                if (double.Parse(dr["TotalPlanificado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalPlanificado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["FechaInicioPlanificado"].ToString();
                sFIPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaInicioPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sFecha = dr["FechaFinPlanificado"].ToString();
                sFFPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaFinPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["TotalPresupuesto"].ToString()) > 0) sb.Append(double.Parse(dr["TotalPresupuesto"].ToString()).ToString("N"));
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["EsfuerzoMes"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoMes"].ToString()).ToString("N"));
                sb.Append("</td>");

                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>");
                else
                    sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["PendienteEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["PendienteEstimado"].ToString()).ToString("N"));
                sb.Append("</td>");

                //if (!bTotPR) sb.Append("<td>");
                //else sb.Append("<td style='background-color:#F58D8D;'>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                //if (!bFinPR) sb.Append("<td>");
                //else sb.Append("<td style='background-color:#F58D8D;'>");
                sb.Append("<td>" + sFecha + "</td>");

                //if (!bTotPR) sb.Append("<td>");
                //else sb.Append("<td style='background-color:#F58D8D;'>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0) sb.Append(double.Parse(dr["TotalPrevisto"].ToString()).ToString("N"));
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["PendientePrevisto"].ToString()) > 0) sb.Append(double.Parse(dr["PendientePrevisto"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["FinPrevisto"].ToString();
                sFFPR = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinPrevisto"].ToString()).ToShortDateString();
                //if (!bFinPR) sb.Append("<td>");
                //else sb.Append("<td style='background-color:#F58D8D;'>");
                sb.Append("<td>" + sFecha + "</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["PorcPrevisto"].ToString()) > 0) sb.Append(double.Parse(dr["PorcPrevisto"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["PorcAvance"].ToString()) > 0) sb.Append(double.Parse(dr["PorcAvance"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["Producido"].ToString()) > 0) sb.Append(double.Parse(dr["Producido"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["PorcConsumido"].ToString()) > 0) sb.Append(double.Parse(dr["PorcConsumido"].ToString()).ToString("N"));
                sb.Append("</td>");

                bool bHayDatos = false;
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPlanificado"].ToString()) > 0) //Tot. previsto y Tot. planificado != 0
                {
                    bHayDatos = true;
                }
                sb.Append("<td style='text-align:right;' ");
                if (bHayDatos)
                {
                    double dDesviacion = double.Parse(dr["PorcDesviacion"].ToString());
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + double.Parse(dr["PorcDesviacion"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("></td>");//% Desviación esfuerzo
                //Desviación de plazo
                if (sFIPL != "" && sFFPL != "" && sFFPR != "")
                {
                    int iDiasPlanificados = 1;
                    if (sFIPL != sFFPL)
                        iDiasPlanificados = Fechas.DateDiff("day", DateTime.Parse(sFIPL), DateTime.Parse(sFFPL));
                    iDiasPlanificados++;
                    double dDesviacion = (Fechas.DateDiff("day", DateTime.Parse(sFFPL), DateTime.Parse(sFFPR)) * 100) / iDiasPlanificados;

                    sb.Append("<td style='text-align:right;' ");
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + dDesviacion.ToString("N") + "</td>");//% Desviación plazo
                }
                else
                    sb.Append("<td></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            
            sb.Append("</tbody>");
            sb.Append("</table>");

            #region Totales a nivel de PE
            SqlDataReader drPE =SUPER.Capa_Negocio.SEGMESPROYECTOSUBNODO.DatosProyectoEconomico(int.Parse(nNumEmpleado), DateTime.Parse(sdMSC), int.Parse(sNumProyecto), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString(), sNivelPresupuesto);
            if (drPE.Read())
            {
                TotalPLPE = double.Parse(drPE["TotalPlanificado"].ToString()).ToString("N");
                if (drPE["FechaInicioPlanificado"] != DBNull.Value)
                    InicioPE = DateTime.Parse(drPE["FechaInicioPlanificado"].ToString()).ToShortDateString();
                if (drPE["FechaFinPlanificado"] != DBNull.Value)
                    FinPLPE = DateTime.Parse(drPE["FechaFinPlanificado"].ToString()).ToShortDateString();
                TotalPrePE = double.Parse(drPE["TotalPresupuesto"].ToString()).ToString("N");
                MesPE = double.Parse(drPE["EsfuerzoMes"].ToString()).ToString("N");
                AcumulPE = double.Parse(drPE["EsfuerzoTotalAcumulado"].ToString()).ToString("N");
                PendEstPE = double.Parse(drPE["PendienteEstimado"].ToString()).ToString("N");
                TotalEstPE = double.Parse(drPE["TotalEstimado"].ToString()).ToString("N");
                if (drPE["FinEstimado"] != DBNull.Value)
                    FinEstPE = DateTime.Parse(drPE["FinEstimado"].ToString()).ToShortDateString();
                TotalPRPE = double.Parse(drPE["TotalPrevisto"].ToString()).ToString("N");
                TotalPdtePrevisto = double.Parse(drPE["PendientePrevisto"].ToString()).ToString("N");
                if (drPE["FinPrevisto"] != DBNull.Value)
                    FinPRPE = DateTime.Parse(drPE["FinPrevisto"].ToString()).ToShortDateString();
                if (double.Parse(drPE["PorcPrevisto"].ToString()) > 0)
                    TotalAVPE = double.Parse(drPE["PorcPrevisto"].ToString()).ToString("N");
                if (double.Parse(drPE["PorcAvance"].ToString()) > 0)
                    TotalAvancePE = double.Parse(drPE["PorcAvance"].ToString()).ToString("N");
                TotalProPE = double.Parse(drPE["Producido"].ToString()).ToString("N");
                if (double.Parse(drPE["PorcConsumido"].ToString()) > 0)
                    TotalIndiCon = double.Parse(drPE["PorcConsumido"].ToString()).ToString("N");
                if (double.Parse(drPE["PorcDesviacion"].ToString()) > 0)
                    TotalIndiDes = double.Parse(drPE["PorcDesviacion"].ToString()).ToString("N");
                //% Desviación plazos
                if (InicioPE != "" && FinPLPE != "" && FinPRPE != "")
                {
                    int iDiasPlanificados = 1;
                    if (InicioPE != FinPLPE)
                        iDiasPlanificados = Fechas.DateDiff("day", DateTime.Parse(InicioPE), DateTime.Parse(FinPLPE));
                    iDiasPlanificados++;
                    dTotalIndiDesPlazo = (Fechas.DateDiff("day", DateTime.Parse(FinPLPE), DateTime.Parse(FinPRPE)) * 100) / iDiasPlanificados;
                }
            }
            drPE.Close();
            drPE.Dispose();

            sb.Append("@#@" + TotalPLPE);
            sb.Append("@#@" + InicioPE);
            sb.Append("@#@" + FinPLPE);
            sb.Append("@#@" + TotalPrePE);
            sb.Append("@#@" + MesPE);
            sb.Append("@#@" + AcumulPE);
            sb.Append("@#@" + PendEstPE);
            sb.Append("@#@" + TotalEstPE);
            sb.Append("@#@" + FinEstPE);
            sb.Append("@#@" + TotalPRPE);
            sb.Append("@#@" + TotalPdtePrevisto);
            sb.Append("@#@" + FinPRPE);
            sb.Append("@#@" + TotalAVPE);
            sb.Append("@#@" + TotalAvancePE);
            sb.Append("@#@" + TotalProPE);
            sb.Append("@#@" + TotalIndiCon);
            sb.Append("@#@" + TotalIndiDes);
            sb.Append("@#@" + dTotalIndiDesPlazo.ToString("N"));

            #endregion

            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()));

            sResul = "OK@#@" + sb.ToString() +"@#@" + oPSN.t422_idmoneda + "@#@" + oPSN.t422_denominacionimportes;
            sb.Length = 0; //Para liberar memoria
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos", ex);
        }

        return sResul;
    }
    private string obtenerTarea(string nNumEmpleado, string sNumProyecto, string sPT, string sdMSC, string sEstado, string sCualidad, string sNivelPresupuesto)
    {
        string sResul = "", sFecha, sFFPR, sFIPL, sFFPL;
        StringBuilder sb = new StringBuilder();
        string sDisplay = "", sTipo="";
        int iNivel = 1;
        try
        {
            SqlDataReader dr = SUPER.Capa_Negocio.SEGMESPROYECTOSUBNODO.DatosFaseActivTareas(int.Parse(sPT), DateTime.Parse(sdMSC), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString(), sNivelPresupuesto);
            //sDisplay = "block";
            sDisplay = "table-row";
            while (dr.Read())
            {
                sTipo = dr["tipo"].ToString();
                iNivel = int.Parse(dr["nivel"].ToString());

                #region Control de Estimación y Previsión
                bool bTotPR = false;
                bool bFinPR = false;
                //if (!bLectura) // A definir bLectura en función del estado del proyecto económico
                //{
                if (double.Parse(dr["TotalPrevisto"].ToString()) < double.Parse(dr["TotalEstimado"].ToString())) bTotPR = true;
                if (dr["FinEstimado"] != DBNull.Value)
                {
                    DateTime dAux = DateTime.Parse("01/01/1900");
                    if (dr["FinPrevisto"] != DBNull.Value) dAux = DateTime.Parse(dr["FinPrevisto"].ToString());
                    if (dAux < DateTime.Parse(dr["FinEstimado"].ToString())) bFinPR = true;
                }
                //}
                #endregion

                sb.Append("<tr PE='" + sNumProyecto + "' ");
                sb.Append("PT='" + dr["t331_idpt"].ToString() + "' ");
                sb.Append("F='" + dr["t334_idfase"].ToString() + "' ");
                sb.Append("A='" + dr["t335_idactividad"].ToString() + "' ");
                sb.Append("T='" + dr["t332_idtarea"].ToString() + "' ");

                switch (sTipo){
                    case "T":
                        //sDisplay = (iNivel == 2) ? "block" : "none";
                        sDisplay = (iNivel == 2) ? "table-row" : "none";
                        sb.Append("style='display: " + sDisplay + "; height:20px;' ");
                        sb.Append(" desplegado=0 nivel=" + iNivel.ToString() + " exp=2>");

                        if ((int)dr["t332_idtarea"] > 0)
                        {
                            sb.Append("<td><img class=N" + iNivel.ToString());
                            sb.Append("  src='../../../../images/imgSeparador.gif' style='width:9px;'>");
                            sb.Append("<img src='../../../../images/imgTareaOff.gif' class='ICO'>");
                        }
                        else
                        {
                            sb.Append("<td style='color:gray'>");
                            sb.Append("<img class=N" + iNivel.ToString() + " src='../../../../images/imgSeparador.gif' class='ICO'>");
                        }
                        switch (iNivel)
                        {
                            case 2: sb.Append("<nobr class='NBR W130 "); break;
                            case 3: sb.Append("<nobr class='NBR W110 "); break;
                            case 4: sb.Append("<nobr class='NBR W100 "); break;
                        }
                        sb.Append("' onmouseover='TTip(event)' style='text-align:left;'>" + dr["descripcion"].ToString() + "</nobr></td>");
                        break;
                    case "A":
                        if ((int)dr["t334_idfase"] > 0)
                            sb.Append(" style='display: none; height:20px;' desplegado=1 nivel=" + iNivel.ToString() + " exp=1>");
                        else
                        {
                            sb.Append(" style='display:table-row; height:20px;' desplegado=1 nivel=" + iNivel.ToString() + " exp=1>");
                        }
                        sb.Append("<td><img class=N" + iNivel.ToString() + " onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                        sb.Append("<img src='../../../../images/imgActividadOff.gif' class='ICO'>");
                        switch (iNivel)
                        {
                            case 2: sb.Append("<nobr class='NBR W130' style='text-align:left;' onmouseover='TTip(event)'>"); break;
                            case 3: sb.Append("<nobr class='NBR W110' style='text-align:left;' onmouseover='TTip(event)'>"); break;
                        }
                        sb.Append(dr["descripcion"].ToString() + "</nobr></td>");
                        break;

                    case "F":
                        sb.Append(" style='display:table-row; height:20px;' desplegado=1 nivel=" + iNivel.ToString() + " exp=1>");
                        sb.Append("<td><img class=N" + iNivel.ToString() + " onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                        sb.Append("<img src='../../../../images/imgFaseOff.gif' class='ICO'>");
                        sb.Append("<nobr class='NBR W130' style='text-align:left;' onmouseover='TTip(event)'>" + dr["descripcion"].ToString() + "</nobr></td>");
                        break;
                }

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["TotalPlanificado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalPlanificado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["FechaInicioPlanificado"].ToString();
                sFIPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaInicioPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sFecha = dr["FechaFinPlanificado"].ToString();
                sFFPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaFinPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["TotalPresupuesto"].ToString()) > 0) sb.Append(double.Parse(dr["TotalPresupuesto"].ToString()).ToString("N"));
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["EsfuerzoMes"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoMes"].ToString()).ToString("N"));
                sb.Append("</td>");

                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>");
                else
                    sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["PendienteEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["PendienteEstimado"].ToString()).ToString("N"));
                sb.Append("</td>");

                string sAux = "";
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sAux = double.Parse(dr["TotalEstimado"].ToString()).ToString("N");
                if (!bTotPR)
                    sb.Append("<td style='text-align:right;'>" + sAux + "</td>");//Total. e.
                else
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>" + sAux + "</td>");//Total. e.

                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                if (!bFinPR)
                    sb.Append("<td>" + sFecha + "</td>");//Fin est.
                else
                    sb.Append("<td style='background-color:#F58D8D;'>" + sFecha + "</td>");//Fin est.

                sAux = "";
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0) sAux = double.Parse(dr["TotalPrevisto"].ToString()).ToString("N");
                if (!bTotPR)
                    sb.Append("<td style='text-align:right;'>" + sAux + "</td>"); //TotalPR.
                else
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>" + sAux + "</td>"); //TotalPR.

                sAux = "";
                if (double.Parse(dr["PendientePrevisto"].ToString()) > 0) 
                    sAux = double.Parse(dr["PendientePrevisto"].ToString()).ToString("N");
                if (!bTotPR)
                    sb.Append("<td style='text-align:right;'>" + sAux + "</td>"); //Pendiente previsto
                else
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>" + sAux + "</td>"); //Pendiente previsto

                sFecha = dr["FinPrevisto"].ToString();
                sFFPR = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinPrevisto"].ToString()).ToShortDateString();

                if (!bFinPR)
                    sb.Append("<td>" + sFecha + "</td>"); //FinPR
                else
                    sb.Append("<td style='background-color:#F58D8D;'>" + sFecha + "</td>"); //FinPR

                sAux = "";
                if (double.Parse(dr["PorcPrevisto"].ToString()) > 0) sAux = double.Parse(dr["PorcPrevisto"].ToString()).ToString("N");

                sb.Append("<td style='text-align:right;' title='" + dr["PorcPrevisto"].ToString() + "'>" + sAux + "</td>"); //% Previsto

                sAux = "";
                if (double.Parse(dr["PorcAvance"].ToString()) > 0) sAux = double.Parse(dr["PorcAvance"].ToString()).ToString("N");

                sb.Append("<td style='text-align:right;' title='" + dr["PorcAvance"].ToString() + "'>" + sAux + "</td>"); //% Producido

                sAux = "";
                if (double.Parse(dr["Producido"].ToString()) > 0) sAux = double.Parse(dr["Producido"].ToString()).ToString("N");

                sb.Append("<td style='text-align:right;'>" + sAux + "</td>");//Producido

                if (double.Parse(dr["PorcConsumido"].ToString()) > 0)
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["PorcConsumido"].ToString()).ToString("N") + "</td>");//% Consumido
                else
                    sb.Append("<td></td>");//% Consumido

                bool bHayDatos = false;
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPlanificado"].ToString()) > 0) //Tot. previsto y Tot. planificado != 0
                {
                    bHayDatos = true;
                }
                sb.Append("<td style='text-align:right;' ");
                if (bHayDatos)
                {
                    double dDesviacion = double.Parse(dr["PorcDesviacion"].ToString());
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + double.Parse(dr["PorcDesviacion"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("></td>");//% Desviación

                //% Desviación plazos
                if (sFIPL != "" && sFFPL != "" && sFFPR != "")
                {
                    int iDiasPlanificados = 1;
                    if (sFIPL != sFFPL)
                        iDiasPlanificados = Fechas.DateDiff("day", DateTime.Parse(sFIPL), DateTime.Parse(sFFPL));
                    iDiasPlanificados++;
                    if (iDiasPlanificados == 0) iDiasPlanificados = 1;
                    double dDesviacion = (Fechas.DateDiff("day", DateTime.Parse(sFFPL), DateTime.Parse(sFFPR)) * 100) / iDiasPlanificados;

                    sb.Append("<td style='text-align:right;' ");
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + dDesviacion.ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("<td></td>");
                
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las tareas", ex);
        }

        return sResul;
    }

    private string obtenerFotoPT(string sFotoPE)
    {
        string TotalPLPE = "0,00", MesPE = "0,00", AcumulPE = "0,00", PendEstPE = "0,00", TotalEstPE = "0,00", 
               TotalPRPE = "0,00", TotalPrevistoPdte = "0,00",
               TotalAVPE = "0", TotalPrePE = "0,00", TotalAvancePE = "0", TotalProPE = "0,00", TotalIndiCon = "0", TotalIndiDes = "0";
        string InicioPE = "", FinPLPE = "", FinEstPE = "", FinPRPE = "";

        string sResul = "";
        string sFecha, sFFPR, sFIPL, sFFPL;
        double dTotalIndiDesPlazo = 0;

        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos2' class='texto' style='width: 1300px; text-align:right;'>");
            sb.Append("<colgroup><col style='width:185px;' />");
            sb.Append("<col style='width:70px;' />");//Planificado.Total
            sb.Append("<col style='width:60px;' />");//Planificado.Inicio
            sb.Append("<col style='width:60px;' />");//Planificado.Fin
            sb.Append("<col style='width:100px;' />");//Planificado.presupuesto

            sb.Append("<col style='width:60px;' />");//IAP.Mes
            sb.Append("<col style='width:70px;' />");//IAP.Acumulado
            sb.Append("<col style='width:65px;' />");//IAP.Pendiente Estimado
            sb.Append("<col style='width:70px;' />");//IAP.Total estimado
            sb.Append("<col style='width:60px;' />");//IAP.Fin estimado

            sb.Append("<col style='width:70px;' />");//Previsto.Total
            sb.Append("<col style='width:70px;' />");//Previsto.Pdte
            sb.Append("<col style='width:60px;' />");//Previsto.Fin
            sb.Append("<col style='width:40px;' />");//Previsto %

            sb.Append("<col style='width:40px;' />");//Avance %
            sb.Append("<col style='width:100px;' />");//Avance. Producido

            sb.Append("<col style='width:40px;' />");//Indicadores. % consumido
            sb.Append("<col style='width:40px;' />");//Indicadores.% desviación esfuerzo
            sb.Append("<col style='width:40px;' /></colgroup>");//Indicadores % desviacion plazo
            sb.Append("<tbody>");

            SqlDataReader dr = FOTOSEGPT.CatalogoPT(int.Parse(sFotoPE), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());

            while (dr.Read())
            {
                sb.Append("<tr idFotoPE='" + dr["t373_idFotoPE"].ToString() + "' ");
                sb.Append(" idFotoPT='" + dr["t374_idFotoPT"].ToString() + "' ");
                sb.Append("style='height:20px;' nivel='1' desplegado=0 exp=0 T=0>");

                sb.Append("<td style='text-align:left;' onmouseover='TTip(event);'>");  //PT
                sb.Append("<img class=NSEG1 src='../../../../images/plus.gif' onclick='mostrar(this);' style='margin-left:5px;cursor:pointer;'>");  //PT
                sb.Append("<IMG src='../../../../images/imgProyTecOff.gif' class='ICO'>");
                sb.Append("<nobr class='NBR W140'>" + dr["t374_denominacion"].ToString() + "</nobr></td>");  //PT
                sb.Append("<td>");
                if (double.Parse(dr["t375_TotPL"].ToString()) > 0) sb.Append(double.Parse(dr["t375_TotPL"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["t375_IniPL"].ToString();
                sFIPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["t375_IniPL"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sFecha = dr["t375_FinPL"].ToString();
                sFFPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["t375_FinPL"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sb.Append("<td style='text-align:right;'>");
                if (decimal.Parse(dr["t375_Presupuesto"].ToString()) > 0) sb.Append(decimal.Parse(dr["t375_Presupuesto"].ToString()).ToString("N"));
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_Mes"].ToString()) > 0) sb.Append(double.Parse(dr["t375_Mes"].ToString()).ToString("N"));
                sb.Append("</td>");

                if (double.Parse(dr["t375_TotalPR"].ToString()) > 0 && double.Parse(dr["t375_Acumulado"].ToString()) > double.Parse(dr["t375_TotalPR"].ToString()))
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>");
                else
                    sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_Acumulado"].ToString()) > 0) sb.Append(double.Parse(dr["t375_Acumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_PendEst"].ToString()) > 0) sb.Append(double.Parse(dr["t375_PendEst"].ToString()).ToString("N"));
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_TotalEst"].ToString()) > 0) sb.Append(double.Parse(dr["t375_TotalEst"].ToString()).ToString("N"));
                sb.Append("</td>");
                sFecha = dr["t375_FinEst"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["t375_FinEst"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_TotalPR"].ToString()) > 0) sb.Append(double.Parse(dr["t375_TotalPR"].ToString()).ToString("N"));
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_pendpr"].ToString()) > 0) 
                    sb.Append(double.Parse(dr["t375_pendpr"].ToString()).ToString("N"));
                sb.Append("</td>");
                sFecha = dr["t375_FinPR"].ToString();
                sFFPR = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["t375_FinPR"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_PorcPR"].ToString()) > 0) sb.Append(double.Parse(dr["t375_PorcPR"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_PorcAV"].ToString()) > 0) sb.Append(double.Parse(dr["t375_PorcAV"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_Producido"].ToString()) > 0) sb.Append(double.Parse(dr["t375_Producido"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["t375_PorcCon"].ToString()) > 0) sb.Append(double.Parse(dr["t375_PorcCon"].ToString()).ToString("N"));
                sb.Append("</td>");

                bool bHayDatos = false;
                if (double.Parse(dr["t375_TotalPR"].ToString()) > 0 && double.Parse(dr["t375_TotPL"].ToString()) > 0) //Tot. previsto y Tot. planificado != 0
                {
                    bHayDatos = true;
                }
                sb.Append("<td style='text-align:right;' ");
                if (bHayDatos)
                {
                    double dDesviacion = double.Parse(dr["t375_PorcDes"].ToString());
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + double.Parse(dr["t375_PorcDes"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("></td>");//% Desviación

                //% Desviación plazos
                if (sFIPL != "" && sFFPL != "" && sFFPR != "")
                {
                    int iDiasPlanificados = 1;
                    if (sFIPL != sFFPL)
                        iDiasPlanificados = Fechas.DateDiff("day", DateTime.Parse(sFIPL), DateTime.Parse(sFFPL));
                    iDiasPlanificados++;
                    double dDesviacion = (Fechas.DateDiff("day", DateTime.Parse(sFFPL), DateTime.Parse(sFFPR)) * 100) / iDiasPlanificados;

                    sb.Append("<td style='text-align:right;' ");
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + dDesviacion.ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            #region Totales a nivel de PE
            SqlDataReader drPE = FOTOSEGPE.CatalogoPE(int.Parse(sFotoPE), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());

            if (drPE.Read())
            {
                TotalPLPE = double.Parse(drPE["t375_TotPL"].ToString()).ToString("N");
                if (drPE["t375_IniPL"] != DBNull.Value)
                    InicioPE = DateTime.Parse(drPE["t375_IniPL"].ToString()).ToShortDateString();
                if (drPE["t375_FinPL"] != DBNull.Value)
                    FinPLPE = DateTime.Parse(drPE["t375_FinPL"].ToString()).ToShortDateString();
                TotalPrePE = decimal.Parse(drPE["t375_Presupuesto"].ToString()).ToString("N");
                MesPE = double.Parse(drPE["t375_Mes"].ToString()).ToString("N");
                AcumulPE = double.Parse(drPE["t375_Acumulado"].ToString()).ToString("N");
                PendEstPE = double.Parse(drPE["t375_PendEst"].ToString()).ToString("N");
                TotalEstPE = double.Parse(drPE["t375_TotalEst"].ToString()).ToString("N");
                if (drPE["t375_FinEst"] != DBNull.Value)
                    FinEstPE = DateTime.Parse(drPE["t375_FinEst"].ToString()).ToShortDateString();
                TotalPRPE = double.Parse(drPE["t375_TotalPR"].ToString()).ToString("N");
                TotalPrevistoPdte = double.Parse(drPE["t375_pendPR"].ToString()).ToString("N");
                if (drPE["t375_FinPR"] != DBNull.Value)
                    FinPRPE = DateTime.Parse(drPE["t375_FinPR"].ToString()).ToShortDateString();
                if (double.Parse(drPE["t375_PorcPR"].ToString()) > 0)
                    TotalAVPE = double.Parse(drPE["t375_PorcPR"].ToString()).ToString("N");
                if (double.Parse(drPE["t375_PorcAV"].ToString()) > 0)
                    TotalAvancePE = double.Parse(drPE["t375_PorcAV"].ToString()).ToString("N");
                TotalProPE = double.Parse(drPE["t375_Producido"].ToString()).ToString("N");
                if (double.Parse(drPE["t375_PorcCon"].ToString()) > 0)
                    TotalIndiCon = double.Parse(drPE["t375_PorcCon"].ToString()).ToString("N");
                //if (double.Parse(drPE["t375_PorcDes"].ToString()) > 0)
                if (double.Parse(drPE["t375_PorcDes"].ToString()) != 0)
                    TotalIndiDes = double.Parse(drPE["t375_PorcDes"].ToString()).ToString("N");
                //% Desviación plazos
                if (InicioPE != "" && FinPLPE != "" && FinPRPE != "")
                {
                    int iDiasPlanificados = 1;
                    if (InicioPE != FinPLPE)
                        iDiasPlanificados = Fechas.DateDiff("day", DateTime.Parse(InicioPE), DateTime.Parse(FinPLPE));
                    iDiasPlanificados++;
                    dTotalIndiDesPlazo = (Fechas.DateDiff("day", DateTime.Parse(FinPLPE), DateTime.Parse(FinPRPE)) * 100) / iDiasPlanificados;
                }
            }
            drPE.Close();
            drPE.Dispose();

            sb.Append("@#@" + TotalPLPE);
            sb.Append("@#@" + InicioPE);
            sb.Append("@#@" + FinPLPE);
            sb.Append("@#@" + TotalPrePE);
            sb.Append("@#@" + MesPE);
            sb.Append("@#@" + AcumulPE);
            sb.Append("@#@" + PendEstPE);
            sb.Append("@#@" + TotalEstPE);
            sb.Append("@#@" + FinEstPE);
            sb.Append("@#@" + TotalPRPE);
            sb.Append("@#@" + TotalPrevistoPdte);
            sb.Append("@#@" + FinPRPE);
            sb.Append("@#@" + TotalAVPE);
            sb.Append("@#@" + TotalAvancePE);
            sb.Append("@#@" + TotalProPE);
            sb.Append("@#@" + TotalIndiCon);
            sb.Append("@#@" + TotalIndiDes);
            sb.Append("@#@" + dTotalIndiDesPlazo.ToString("N"));

            #endregion

            sResul = "OK@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria

        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos de la foto", ex);
        }

        return sResul;
    }
    private string obtenerFotoTarea(string sFotoPT)
    {
        string sResul = "", sFecha, sFFPR, sFIPL, sFFPL;
        StringBuilder sb = new StringBuilder();
        string sDisplay = "", sTipo = "";
        int iNivel = 1;
        try
        {
            SqlDataReader dr = FOTOSEGPT.DatosFaseActivTareas(int.Parse(sFotoPT), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());
            //sDisplay = "block";
            sDisplay = "table-row";
            while (dr.Read())
            {
                sTipo = dr["tipo"].ToString();
                iNivel = int.Parse(dr["nivel"].ToString());

                #region Control de Estimación y Previsión
                bool bTotPR = false;
                bool bFinPR = false;
                //if (!bLectura) // A definir bLectura en función del estado del proyecto económico
                //{
                if (double.Parse(dr["TotalPrevisto"].ToString()) < double.Parse(dr["TotalEstimado"].ToString())) bTotPR = true;
                if (dr["FinEstimado"] != DBNull.Value)
                {
                    DateTime dAux = DateTime.Parse("01/01/1900");
                    if (dr["FinPrevisto"] != DBNull.Value) dAux = DateTime.Parse(dr["FinPrevisto"].ToString());
                    if (dAux < DateTime.Parse(dr["FinEstimado"].ToString())) bFinPR = true;
                }
                //}
                #endregion

                sb.Append("<tr idFotoPT='" + dr["t374_idFotoPT"].ToString() + "' ");
                sb.Append("F='" + dr["t460_idfotosegf"].ToString() + "' ");
                sb.Append("A='" + dr["t461_idfotosega"].ToString() + "' ");
                sb.Append("T='" + dr["t375_idfotot"].ToString() + "' ");

                switch (sTipo)
                {
                    case "T":
                        sDisplay = (iNivel == 2) ? "table-row" : "none";
                        sb.Append("style='display: " + sDisplay + "; height:20px;' ");
                        sb.Append(" desplegado=0 nivel=" + iNivel.ToString() + " exp=2>");

                        if ((int)dr["t375_idfotot"] > 0)
                        {
                            sb.Append("<td><img class=N" + iNivel.ToString());
                            sb.Append("  src='../../../../images/imgSeparador.gif' style='width:9px;'>");
                            sb.Append("<img src='../../../../images/imgTareaOff.gif' class='ICO'>");
                        }
                        else
                        {
                            sb.Append("<td style='color:gray'>");
                            sb.Append("<img class=N" + iNivel.ToString() + " src='../../../../images/imgSeparador.gif' class='ICO'>");
                        }
                        switch (iNivel)
                        {
                            case 2: sb.Append("<nobr class='NBR W130 "); break;
                            case 3: sb.Append("<nobr class='NBR W110 "); break;
                            case 4: sb.Append("<nobr class='NBR W100 "); break;
                        }
                        sb.Append("' style='text-align:left;' onmouseover='TTip(event)'>" + dr["descripcion"].ToString() + "</nobr></td>");
                        break;
                    case "A":
                        if ((int)dr["t460_idfotosegf"] > 0)
                            sb.Append(" style='display: none; height:20px;' desplegado=1 nivel=" + iNivel.ToString() + " exp=1>");
                        else
                        {
                            sb.Append(" style='display: table-row; height:20px;' desplegado=1 nivel=" + iNivel.ToString() + " exp=1>");
                        }
                        sb.Append("<td><img class=N" + iNivel.ToString() + " onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                        sb.Append("<img src='../../../../images/imgActividadOff.gif' class='ICO'>");
                        switch (iNivel)
                        {
                            case 2: sb.Append("<nobr class='NBR W130' style='text-align:left;' onmouseover='TTip(event)'>"); break;
                            case 3: sb.Append("<nobr class='NBR W110' style='text-align:left;' onmouseover='TTip(event)'>"); break;
                        }
                        sb.Append(dr["descripcion"].ToString() + "</nobr></td>");
                        break;

                    case "F":
                        sb.Append(" style='display: table-row; height:20px;' desplegado=1 nivel=" + iNivel.ToString() + " exp=1>");
                        sb.Append("<td><img class=N" + iNivel.ToString() + " onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                        sb.Append("<img src='../../../../images/imgFaseOff.gif' class='ICO'>");
                        sb.Append("<nobr class='NBR W130' style='text-align:left;' onmouseover='TTip(event)'>" + dr["descripcion"].ToString() + "</nobr></td>");
                        break;
                }

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["TotalPlanificado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalPlanificado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["FechaInicioPlanificado"].ToString();
                sFIPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaInicioPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sFecha = dr["FechaFinPlanificado"].ToString();
                sFFPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaFinPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["TotalPresupuesto"].ToString()) > 0) sb.Append(double.Parse(dr["TotalPresupuesto"].ToString()).ToString("N"));
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["EsfuerzoMes"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoMes"].ToString()).ToString("N"));
                sb.Append("</td>");

                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>");
                else
                    sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");
                if (double.Parse(dr["PendienteEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["PendienteEstimado"].ToString()).ToString("N"));
                sb.Append("</td>");

                string sAux = "";
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sAux = double.Parse(dr["TotalEstimado"].ToString()).ToString("N");
                if (!bTotPR)
                    sb.Append("<td style='text-align:right;'>" + sAux + "</td>");//Total. e.
                else
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>" + sAux + "</td>");//Total. e.

                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                if (!bFinPR)
                    sb.Append("<td>" + sFecha + "</td>");//Fin est.
                else
                    sb.Append("<td style='background-color:#F58D8D;'>" + sFecha + "</td>");//Fin est.

                sAux = "";
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0) sAux = double.Parse(dr["TotalPrevisto"].ToString()).ToString("N");
                if (!bTotPR)
                    sb.Append("<td style='text-align:right;'>" + sAux + "</td>"); //TotalPR.
                else
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>" + sAux + "</td>"); //TotalPR.

                sAux = "";
                if (double.Parse(dr["PendientePrevisto"].ToString()) > 0)
                    sAux = double.Parse(dr["PendientePrevisto"].ToString()).ToString("N");
                if (!bTotPR)
                    sb.Append("<td style='text-align:right;'>" + sAux + "</td>"); //Pendiente previsto
                else
                    sb.Append("<td style='background-color:#F58D8D; text-align:right;'>" + sAux + "</td>"); //Pendiente previsto

                sFecha = dr["FinPrevisto"].ToString();
                sFFPR = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinPrevisto"].ToString()).ToShortDateString();

                if (!bFinPR)
                    sb.Append("<td>" + sFecha + "</td>"); //FinPR
                else
                    sb.Append("<td style='background-color:#F58D8D;'>" + sFecha + "</td>"); //FinPR

                sAux = "";
                if (double.Parse(dr["PorcPrevisto"].ToString()) > 0) sAux = double.Parse(dr["PorcPrevisto"].ToString()).ToString("N");

                sb.Append("<td style='text-align:right;' title='" + dr["PorcPrevisto"].ToString() + "'>" + sAux + "</td>"); //% Previsto

                sAux = "";
                if (double.Parse(dr["PorcAvance"].ToString()) > 0) sAux = double.Parse(dr["PorcAvance"].ToString()).ToString("N");

                sb.Append("<td style='text-align:right;' title='" + dr["PorcAvance"].ToString() + "'>" + sAux + "</td>"); //% Producido

                sAux = "";
                if (double.Parse(dr["Producido"].ToString()) > 0) sAux = double.Parse(dr["Producido"].ToString()).ToString("N");

                sb.Append("<td style='text-align:right;'>" + sAux + "</td>");//Producido

                if (double.Parse(dr["PorcConsumido"].ToString()) > 0)
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["PorcConsumido"].ToString()).ToString("N") + "</td>");//% Consumido
                else
                    sb.Append("<td></td>");//% Consumido

                bool bHayDatos = false;
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPlanificado"].ToString()) > 0) //Tot. previsto y Tot. planificado != 0
                {
                    bHayDatos = true;
                }
                sb.Append("<td style='text-align:right;' ");
                if (bHayDatos)
                {
                    double dDesviacion = double.Parse(dr["PorcDesviacion"].ToString());
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + double.Parse(dr["PorcDesviacion"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("></td>");//% Desviación

                //% Desviación plazos
                if (sFIPL != "" && sFFPL != "" && sFFPR != "")
                {
                    int iDiasPlanificados = 1;
                    if (sFIPL != sFFPL)
                        iDiasPlanificados = Fechas.DateDiff("day", DateTime.Parse(sFIPL), DateTime.Parse(sFFPL));
                    iDiasPlanificados++;
                    double dDesviacion = (Fechas.DateDiff("day", DateTime.Parse(sFFPL), DateTime.Parse(sFFPR)) * 100) / iDiasPlanificados;

                    sb.Append("<td style='text-align:right;' ");
                    if (dDesviacion <= 5) sb.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" class='SA'");
                    else if (dDesviacion > 20) sb.Append(" class='SR'");
                    sb.Append(">" + dDesviacion.ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("<td></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@" + sb.ToString();
            sb.Length = 0; //Para liberar memoria
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las tareas de la foto", ex);
        }

        return sResul;
    }

    private string recuperarPSN(string nIdProySubNodo)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nIdProySubNodo), (int)Session["UsuarioActual"], "PST");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //4
                sb.Append(dr["t422_idmoneda_proyecto"].ToString());  //5
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
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pst", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = (dr["rtpt"].ToString() == "1") ? true : false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "##");
                sb.Append(dr["t422_idmoneda_proyecto"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al buscar el proyecto", ex);
        }
        return sResul;
    }

    private string setResolucion()
    {
        try
        {
            Session["FOTOPST1024"] = !(bool)Session["FOTOPST1024"];

            SUPER.Capa_Negocio.USUARIO.UpdateResolucion(9, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["FOTOPST1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }

}