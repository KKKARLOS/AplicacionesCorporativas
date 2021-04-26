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
    protected int nIndice;
    public int i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.sbotonesOpcionOn = "71";
                Master.TituloPagina = "Consulta de imputaciones";
                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                Utilidades.SetEventosFecha(this.txtDesde);
                Utilidades.SetEventosFecha(this.txtHasta);

                DateTime dHoy = DateTime.Now, dtAux;
                int nDias = dHoy.Day;
                dtAux = dHoy.AddDays(-nDias + 1);
                txtDesde.Text = dtAux.ToShortDateString();
                dtAux = dtAux.AddMonths(1).AddDays(-1);
                txtHasta.Text = dtAux.ToShortDateString();

                this.cboProyecto.AppendDataBoundItems = true;
                this.cboProyecto.DataValueField = "t305_idproyectosubnodo";
                this.cboProyecto.DataTextField = "t305_seudonimo";
                this.cboProyecto.DataSource = Consumo.ObtenerProyectosImputacionesIAP((int)Session["UsuarioActual"],
                                                                                        DateTime.Parse(txtDesde.Text), 
                                                                                        DateTime.Parse(txtHasta.Text));
                this.cboProyecto.DataBind();

                string strTabla = obtenerDatos("",
                                           txtDesde.Text,
                                           txtHasta.Text);

                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK")
                {
                    this.strTablaHTML = aTabla[1];
                }
                else Master.sErrores += Errores.mostrarError(aTabla[1]);


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
            case ("getProyectos"):
                sResultado += getProyectos(aArgs[1], aArgs[2]);
                break;
            case ("buscar"):
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3]);
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

    private string obtenerDatos(string sPSN, string sFechaDesde, string sFechaHasta)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = Consumo.ObtenerImputacionesIAP((int)Session["UsuarioActual"], (sPSN=="")?null:(int?)int.Parse(sPSN), DateTime.Parse(sFechaDesde), DateTime.Parse(sFechaHasta)); 

            nIndice = 0;
            int nPE = 0, nPT = 0, nF=0, nA=0, nT = 0, nNivel=0;
            int nFactual = 0, nAactual = 0;
            string sHTML = "";

            sb.Append("<table id='tblDatos' style='width:950px;text-align:right;'>");
            sb.Append("<colgroup><col style='width:700px;'/><col style='width:125px;' /><col style='width:125px;'/></colgroup>");

            while (dr.Read())
            {
                sHTML = "";
                nFactual = int.Parse(dr["t334_idfase"].ToString());
                nAactual = int.Parse(dr["t335_idactividad"].ToString());
                if (nPE != int.Parse(dr["t301_idproyecto"].ToString()))
                {
                    //Crear PE, PT, F, A, T y consumo
                    nT = int.Parse(dr["t332_idtarea"].ToString());
                    nA = int.Parse(dr["t335_idactividad"].ToString());
                    nF = int.Parse(dr["t334_idfase"].ToString());
                    nPT = int.Parse(dr["t331_idpt"].ToString());
                    nPE = int.Parse(dr["t301_idproyecto"].ToString());
                    sHTML = CrearProyEco(dr);
                }
                else if (nPT != int.Parse(dr["t331_idpt"].ToString()))
                {
                    //Crear PT, F, A, T y consumo
                    nT = int.Parse(dr["t332_idtarea"].ToString());
                    nA = int.Parse(dr["t335_idactividad"].ToString());
                    nF = int.Parse(dr["t334_idfase"].ToString());
                    nPT = int.Parse(dr["t331_idpt"].ToString());
                    sHTML = CrearProyTec(dr);
                }
                else if ((nF != nFactual) && (nFactual != 0))
                {
                    //Crear F, A, T y consumo
                    nT = int.Parse(dr["t332_idtarea"].ToString());
                    nA = int.Parse(dr["t335_idactividad"].ToString());
                    nF = int.Parse(dr["t334_idfase"].ToString());
                    nPT = int.Parse(dr["t331_idpt"].ToString());
                    sHTML = CrearFase(dr);
                }
                else if ((nA != nAactual) && (nAactual != 0))
                {
                    //Crear A, T y consumo
                    nT = int.Parse(dr["t332_idtarea"].ToString());
                    nA = int.Parse(dr["t335_idactividad"].ToString());
                    nF = int.Parse(dr["t334_idfase"].ToString());
                    nPT = int.Parse(dr["t331_idpt"].ToString());
                    if (nFactual == 0)
                        nNivel = 3;
                    else nNivel = 4;
                    sHTML = CrearActividad(dr, nNivel);
                }
                else if (nT != int.Parse(dr["t332_idtarea"].ToString()))
                {
                    //Crear T y consumo
                    if (nFactual == 0)
                    {
                        if (nAactual == 0) nNivel = 3;
                        else nNivel = 4;
                    }
                    else
                    {
                        nNivel = 5;
                    }
                    nT = int.Parse(dr["t332_idtarea"].ToString());
                    sHTML = CrearTarea(dr, nNivel);
                }
                else
                {
                    //Crear consumo
                    if (nFactual == 0)
                    {
                        if (nAactual == 0) nNivel = 4;
                        else nNivel = 5;
                    }
                    else
                    {
                        nNivel = 6;
                    }
                    sHTML = CrearConsumo(dr, nNivel);
                }

                sb.Append(sHTML);
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
            //sResul += "@#@" + txtHorasReportadas.Text + "@#@" + txtJornadasReportadas.Text + "@#@" + txtJornadasEconomicas.Text + "@#@" + hdnFechaDesde.Text + "@#@" + hdnFechaHasta.Text;
            //sResul += "@#@" + hdnFechaDesde.Text + "@#@" + hdnFechaHasta.Text;
            sb.Length = 0; //Para liberar memoria        
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de proyectos", ex);
        }

        return sResul;
    }
    private string getProyectos(string sFechaDesde, string sFechaHasta)
    {
        StringBuilder sb = new StringBuilder();
        bool bError = false;
        string sResul = "";

        try
        {
            if (!Utilidades.isDate(sFechaDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sFechaHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                SqlDataReader dr = Consumo.ObtenerProyectosImputacionesIAP((int)Session["UsuarioActual"], DateTime.Parse(sFechaDesde), DateTime.Parse(sFechaHasta));

                while (dr.Read())
                {
                    sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##" + dr["t305_seudonimo"].ToString() + "///");
                }
                dr.Close();
                dr.Dispose();

                sResul = "OK@#@" + sb.ToString();
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta de proyectos", ex);
        }
        return sResul;
    }

    private string CrearProyEco(IDataReader dr)
    {
        string sResul = CrearFila(dr, 1 ,"PE");
        sResul += CrearFila(dr, 2, "PT");
        if (dr["t334_idfase"].ToString() != "0")
        {
            sResul += CrearFila(dr, 3, "F");
            if (dr["t335_idactividad"].ToString() != "0")
            {
                sResul += CrearFila(dr, 4, "A");
                sResul += CrearFila(dr, 5, "T");
                sResul += CrearFila(dr, 6, "C");
            }
        }
        else
        {
            if (dr["t335_idactividad"].ToString() != "0")
            {
                sResul += CrearFila(dr, 3, "A");
                sResul += CrearFila(dr, 4, "T");
                sResul += CrearFila(dr, 5, "C");
            }
            else
            {
                sResul += CrearFila(dr, 3, "T");
                sResul += CrearFila(dr, 4, "C");
            }
        }
        return sResul;
    }
    private string CrearProyTec(IDataReader dr)
    {
        string sResul = CrearFila(dr, 2, "PT");
        if (dr["t334_idfase"].ToString() != "0")
        {
            sResul += CrearFila(dr, 3, "F");
            if (dr["t335_idactividad"].ToString() != "0")
            {
                sResul += CrearFila(dr, 4, "A");
                sResul += CrearFila(dr, 5, "T");
                sResul += CrearFila(dr, 6, "C");
            }
        }
        else
        {
            if (dr["t335_idactividad"].ToString() != "0")
            {
                sResul += CrearFila(dr, 3, "A");
                sResul += CrearFila(dr, 4, "T");
                sResul += CrearFila(dr, 5, "C");
            }
            else
            {
                sResul += CrearFila(dr, 3, "T");
                sResul += CrearFila(dr, 4, "C");
            }
        }
        return sResul;
    }
    private string CrearFase(IDataReader dr)
    {
        string sResul = CrearFila(dr, 3, "F");
        sResul += CrearFila(dr, 4, "A");
        sResul += CrearFila(dr, 5, "T");
        sResul += CrearFila(dr, 6, "C");
        return sResul;
    }
    private string CrearActividad(IDataReader dr, int nNivel)
    {
        string sResul = CrearFila(dr, nNivel, "A");
        sResul += CrearFila(dr, nNivel + 1, "T");
        sResul += CrearFila(dr, nNivel + 2, "C");
        return sResul;
    }
    private string CrearTarea(IDataReader dr, int nNivel)
    {
        string sResul = CrearFila(dr, nNivel, "T");
        sResul += CrearFila(dr, nNivel + 1, "C");
        return sResul;
    }
    private string CrearConsumo(IDataReader dr, int nNivel)
    {
        string sResul = CrearFila(dr, nNivel, "C");
        return sResul;
    }
    private string CrearFila(IDataReader dr, int nNivel, string sTipo)
    {
        StringBuilder sb = new StringBuilder();

        if (nIndice % 2 == 0) sb.Append("<tr class=FA ");
        else sb.Append("<tr class=FB ");

        if (nNivel == 1) nIndice++;

        if (sTipo == "PE" || sTipo == "PT" || sTipo == "F" || sTipo == "A" || sTipo=="T")
        {// NIVELES CON NODO(EXPANDIR/CONTRAER) = PE / PT / FASE / ACTIVIDAD / Tareas
            sb.Append(" N='S'");
        }
        if (sTipo != "PE") // OCULTAMOS niveles que no sean de PE
            sb.Append(" style='display:none;cursor:default;height:17px;' ");
        else
            sb.Append(" style='cursor:default;height:17px;' ");

        sb.Append(" nivel='" + nNivel.ToString() + "' tipo='" + sTipo + "' ");
        if (sTipo == "C")
        {
            if (dr["TotalHorasReportadas"] != DBNull.Value)
                sb.Append("nH=" + dr["TotalHorasReportadas"].ToString() + " ");
            else
                sb.Append("nH=0 ");
            if (dr["TotalJornadasReportadas"] != DBNull.Value)
                sb.Append("nJ=" + dr["TotalJornadasReportadas"].ToString() + " ");
            else
                sb.Append("nJ=0 ");
            sb.Append("onmouseover='TTip(event);' title='" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "' >");
        }
        else
            sb.Append(">");

        //sb.Append("<td width='665px' class='N");
        sb.Append("<td class='N");
        if (nNivel == 1)
            sb.Append("1'>");
        else if (nNivel == 2)
            sb.Append("2'>");
        else if (nNivel == 3)
            sb.Append("3'>");
        else if (nNivel == 4)
            sb.Append("4'>");
        else if (nNivel == 5)
            sb.Append("5'>");
        else if (nNivel == 6)
            sb.Append("6'>");

        if (sTipo == "PE" || sTipo == "PT" || sTipo == "F" || sTipo == "A" || sTipo == "T")
            sb.Append("<img src='../../../images/plus.gif' style='cursor:pointer'  onclick=" + (char)34 + "mostrar(this)" + (char)34 + " style='width:9px; height:9px;' class='ICO'>");

        if (sTipo == "PE")
        {
            string sEstado = dr["t301_estado"].ToString();
            switch (sEstado)
            {
                case "A":
                    sb.Append("<IMG class='ICO' src='../../../images/imgIconoProyAbierto.gif' style='width:16px; height:16px;' title='Proyecto abierto' class='ICO'>");
                    break;
                case "H":
                    sb.Append("<IMG class='ICO' src='../../../images/imgIconoProyHistorico.gif' style='width:16px; height:16px;' title='Proyecto histórico' class='ICO'>");
                    break;
                case "C":
                    sb.Append("<IMG class='ICO' src='../../../images/imgIconoProyCerrado.gif' style='width:16px; height:16px;' title='Proyecto cerrado' class='ICO'>");
                    break;
                case "P":
                    sb.Append("<IMG class='ICO' src='../../../images/imgIconoProyPresup.gif' style='width:16px; height:16px;' title='Proyecto presupuestado' class='ICO'>");
                    break;
            }
            //sb.Append(dr["t301_idproyecto"].ToString() + " - " + dr["t301_denominacion"].ToString() + "</td>");  //Proy. Economico
            sb.Append("<div class='NBR W500' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cualidad:</label>" + dr["Cualidad"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_idnodo"].ToString() + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

        }
        else if (sTipo == "PT")
        {
            sb.Append("<img class='ICO' src='../../../images/imgProyTecOff.gif' style='width:14px; height:14px;' >");
            sb.Append(dr["t331_despt"].ToString() + "</td>");  //Proy.Tecnico
        }
        else if (sTipo == "F")
        {
            sb.Append("<img class='ICO' src='../../../images/imgfaseOff.gif' style='width:14px; height:14px;'>");
            sb.Append(dr["t334_desfase"].ToString() + "</td>");  //Fase
        }
        else if (sTipo == "A")
        {
            sb.Append("<img class='ICO' src='../../../images/imgActividadOff.gif' style='width:14px; height:14px;' >");
            sb.Append(dr["t335_desactividad"].ToString() + "</td>");  //Actividad
        }
        else if (sTipo == "T")
        {
            sb.Append("<img class='ICO' src='../../../images/imgTareaOff.gif' style='width:14px; height:14px;' >");
            sb.Append(dr["t332_idtarea"].ToString() + " - " + dr["t332_destarea"].ToString() + "</td>");  //Tarea 
        }
        else if (sTipo == "C")
        {
            string sFecha = dr["t337_fecha"].ToString();
            if (sFecha != "") sFecha = DateTime.Parse(dr["t337_fecha"].ToString()).ToShortDateString();
            sb.Append(sFecha + "&nbsp;&nbsp;&nbsp;<div class='NBR' style='width:520px'>" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</nobr></td>");  //Fechas de Consumo y Comentarios
        }

        if (sTipo == "C")
        {
            double nHR = 0;
            if (dr["TotalHorasReportadas"] != DBNull.Value)
            {
                nHR = double.Parse(dr["TotalHorasReportadas"].ToString());
            }
            sb.Append("<td style='text-align:right;padding-right:5px;'>");
            sb.Append(nHR.ToString("N"));// TotalHorasReportadas
            sb.Append("</td>");

            double nJR = 0;
            if (dr["TotalJornadasReportadas"] != DBNull.Value)
            {
                nJR = double.Parse(dr["TotalJornadasReportadas"].ToString());
                sb.Append("<td title='" + dr["TotalJornadasReportadas"].ToString() + "' style='text-align:right;padding-right:5px;'>");
            }
            else
                sb.Append("<td title='0' style='text-align:right;padding-right:5px;'>");
            sb.Append(nJR.ToString("N"));// TotalJornadasReportadas
        }
        else
            sb.Append("<td style='text-align:right;padding-right:5px;'></td><td style='text-align:right;padding-right:5px;'>");

        sb.Append("</td></tr>");

        string sResul = sb.ToString();
        sb.Length = 0;
        return sResul;
    }
}
