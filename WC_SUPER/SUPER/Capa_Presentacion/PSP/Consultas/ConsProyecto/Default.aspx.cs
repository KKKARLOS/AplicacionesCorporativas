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
                Master.sbotonesOpcionOn = "38,18";
                Master.sbotonesOpcionOff = "";

                Master.TituloPagina = "Consumos por proyecto";
                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                Utilidades.SetEventosFecha(this.txtValIni);
                Utilidades.SetEventosFecha(this.txtValFin);

                DateTime dHoy = DateTime.Now, dtAux;
                int nDias = dHoy.Day;
                dtAux = dHoy.AddDays(-nDias + 1);
                txtValIni.Text = dtAux.ToShortDateString();
                dtAux = dtAux.AddMonths(1).AddDays(-1);
                txtValFin.Text = dtAux.ToShortDateString();

                this.hdnNodo.Value = Estructura.getDefCorta(Estructura.sTipoElem.NODO);

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
            case ("proyecto"):          
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
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

    private string obtenerDatos(string sPSN, string sFechaDesde, string sFechaHasta, string sEsSoloRtpt)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool bError = false, bEsSoloRtpt=false;

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
            if (sEsSoloRtpt == "S")
                bEsSoloRtpt = true;

            if (!bError)
            {
                SqlDataReader dr = PROYECTO.ObtenerDatosConsProyecto((int)Session["UsuarioActual"], int.Parse(sPSN), sFechaDesde, sFechaHasta, bEsSoloRtpt);
                nIndice = 0;
                int nPT = 0, nF = 0, nA = 0, nT = 0, nNivel = 0, nProf = 0;
                int nFactual = 0, nAactual = 0;
                string sHTML = "";

                sb.Append("<table id='tblDatos' class='texto' style='width: 950px;'>");
                sb.Append("<colgroup><col style='width:750px'/>");
                //sb.Append("<col width='125px' class='tddr' /><col class='tddr' style='width:125px;padding-right:5px;' /></colgroup>");
                sb.Append("<col style='width:100px' /><col style='width:100px;' /></colgroup>");

                while (dr.Read())
                {
                    sHTML = "";
                    nFactual = int.Parse(dr["t334_idfase"].ToString());
                    nAactual = int.Parse(dr["t335_idactividad"].ToString());
                    if (nPT != int.Parse(dr["t331_idpt"].ToString()))
                    {
                        //Crear PT, F, A, T, profesional y consumo
                        nT = int.Parse(dr["t332_idtarea"].ToString());
                        nA = int.Parse(dr["t335_idactividad"].ToString());
                        nF = int.Parse(dr["t334_idfase"].ToString());
                        nPT = int.Parse(dr["t331_idpt"].ToString());
                        nProf = int.Parse(dr["t314_idusuario"].ToString());
                        sHTML = CrearProyTec(dr);
                    }
                    else if ((nF != nFactual) && (nFactual != 0))
                    {
                        //Crear F, A, T, profesional y consumo
                        nT = int.Parse(dr["t332_idtarea"].ToString());
                        nA = int.Parse(dr["t335_idactividad"].ToString());
                        nF = int.Parse(dr["t334_idfase"].ToString());
                        nPT = int.Parse(dr["t331_idpt"].ToString());
                        nProf = int.Parse(dr["t314_idusuario"].ToString());
                        sHTML = CrearFase(dr);
                    }
                    else if ((nA != nAactual) && (nAactual != 0))
                    {
                        //Crear A, T, profesional y consumo
                        nT = int.Parse(dr["t332_idtarea"].ToString());
                        nA = int.Parse(dr["t335_idactividad"].ToString());
                        nF = int.Parse(dr["t334_idfase"].ToString());
                        nPT = int.Parse(dr["t331_idpt"].ToString());
                        nProf = int.Parse(dr["t314_idusuario"].ToString());
                        if (nFactual == 0)
                            nNivel = 2;
                        else nNivel = 3;
                        sHTML = CrearActividad(dr, nNivel);
                    }
                    else if (nT != int.Parse(dr["t332_idtarea"].ToString()))
                    {
                        //Crear T, profesional y consumo
                        if (nFactual == 0)
                        {
                            if (nAactual == 0) nNivel = 2;
                            else nNivel = 3;
                        }
                        else
                        {
                            nNivel = 4;
                        }
                        nT = int.Parse(dr["t332_idtarea"].ToString());
                        nProf = int.Parse(dr["t314_idusuario"].ToString());
                        sHTML = CrearTarea(dr, nNivel);
                    }
                    else if (nProf != int.Parse(dr["t314_idusuario"].ToString()))
                    {
                        //Crear profesional y consumo
                        if (nFactual == 0)
                        {
                            if (nAactual == 0) nNivel = 3;
                            else nNivel = 4;
                        }
                        else
                        {
                            nNivel = 5;
                        }
                        nProf = int.Parse(dr["t314_idusuario"].ToString());
                        sHTML = CrearProfesional(dr, nNivel);
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
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta", ex);
        }

        return sResul;
    }

    private string CrearProyTec(IDataReader dr)
    {
        string sResul = CrearFila(dr, 1, "PT");
        if (dr["t334_idfase"].ToString() != "0")
        {
            sResul += CrearFila(dr, 2, "F");
            if (dr["t335_idactividad"].ToString() != "0")
            {
                sResul += CrearFila(dr, 3, "A");
                sResul += CrearFila(dr, 4, "T");
                sResul += CrearFila(dr, 5, "P");
                sResul += CrearFila(dr, 6, "C");
            }
        }
        else
        {
            if (dr["t335_idactividad"].ToString() != "0")
            {
                sResul += CrearFila(dr, 2, "A");
                sResul += CrearFila(dr, 3, "T");
                sResul += CrearFila(dr, 4, "P");
                sResul += CrearFila(dr, 5, "C");
            }
            else
            {
                sResul += CrearFila(dr, 2, "T");
                sResul += CrearFila(dr, 3, "P");
                sResul += CrearFila(dr, 4, "C");
            }
        }
        return sResul;
    }
    private string CrearFase(IDataReader dr)
    {
        string sResul = CrearFila(dr, 2, "F");
        sResul += CrearFila(dr, 3, "A");
        sResul += CrearFila(dr, 4, "T");
        sResul += CrearFila(dr, 5, "P");
        sResul += CrearFila(dr, 6, "C");
        return sResul;
    }
    private string CrearActividad(IDataReader dr, int nNivel)
    {
        string sResul = CrearFila(dr, nNivel, "A");
        sResul += CrearFila(dr, nNivel + 1, "T");
        sResul += CrearFila(dr, nNivel + 2, "P");
        sResul += CrearFila(dr, nNivel + 3, "C");
        return sResul;
    }
    private string CrearTarea(IDataReader dr, int nNivel)
    {
        string sResul = CrearFila(dr, nNivel, "T");
        sResul += CrearFila(dr, nNivel + 1, "P");
        sResul += CrearFila(dr, nNivel + 2, "C");
        return sResul;
    }
    private string CrearProfesional(IDataReader dr, int nNivel)
    {
        string sResul = CrearFila(dr, nNivel, "P");
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

        if (sTipo == "P" || sTipo == "PT" || sTipo == "F" || sTipo == "A" || sTipo == "T")
        {// NIVELES CON NODO(EXPANDIR/CONTRAER) = PT / FASE / ACTIVIDAD / Tareas / PROFESIONALES
            sb.Append(" N='S'");
            if (sTipo == "P")
                sb.Append(" baja=" + dr["baja"].ToString());
        }
        if (sTipo != "PT") // OCULTAMOS niveles que no sean de PT
            sb.Append(" style='display:none;cursor:default;height:17px;' ");
        else
            sb.Append(" style='cursor:default;height:17px;' ");

        sb.Append(" nivel='" + nNivel.ToString() + "' tipo='" + sTipo + "' ");
        //if (sTipo == "P")
        //    sb.Append("onmouseover='TTip(this);' ");
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
            sb.Append("title='" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "' ");
            
        }
        sb.Append(">");

        sb.Append("<td width='650px' class='N");
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
            sb.Append("6'>");// style='width:535px' 

        if (sTipo == "P" || sTipo == "PT" || sTipo == "F" || sTipo == "A" || sTipo == "T")
            sb.Append("<img src='../../../../images/plus.gif' style='cursor:pointer' onclick=" + (char)34 + "mostrar2(this);" + (char)34 + " style='width:9px; height:9px;' class='ICO'>");

        if (sTipo == "PT")
        {
            sb.Append("<img src='../../../../images/imgProyTecOff.gif' style='width:14px; height:14px;' class='ICO'>");
            sb.Append(dr["t331_despt"].ToString() + "</td>");  //Proy.Tecnico
        }
        else if (sTipo == "F")
        {
            sb.Append("<img src='../../../../images/imgfaseOff.gif' style='width:14px; height:14px;' class='ICO'>");
            sb.Append(dr["t334_desfase"].ToString() + "</td>");  //Fase
        }
        else if (sTipo == "A")
        {
            sb.Append("<img src='../../../../images/imgActividadOff.gif' style='width:14px; height:14px;' class='ICO'>");
            sb.Append(dr["t335_desactividad"].ToString() + "</td>");  //Actividad
        }
        else if (sTipo == "T")
        {
            sb.Append("<img src='../../../../images/imgTareaOff.gif' style='width:14px; height:14px;' class='ICO'>");
            sb.Append(dr["t332_idtarea"].ToString() + " - " + dr["t332_destarea"].ToString() + "</td>");  //Tarea 
        }
        else if (sTipo == "P")
        {
            if (dr["interno"].ToString() == "I")
            {
                if (dr["t001_sexo"].ToString() == "V")
                    sb.Append("<img src='../../../../images/imgUsuIV.gif' style='width:16px; height:16px;' class='ICO'>");
                else
                    sb.Append("<img src='../../../../images/imgUsuIM.gif' style='width:16px; height:16px;' class='ICO'>");
            }
            else
            {
                if (dr["t001_sexo"].ToString() == "V")
                    sb.Append("<img src='../../../../images/imgUsuEV.gif' style='width:16px; height:16px;' class='ICO'>");
                else
                    sb.Append("<img src='../../../../images/imgUsuEM.gif' style='width:16px; height:16px;' class='ICO'>");
            }
            //sb.Append(dr["Profesional"].ToString() + "</td>");  //Profesional 
            //sb.Append("<nobr class='NBR W520 N1' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "</nobr></td>");
            sb.Append("<nobr class='NBR W520 N1' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "</nobr></td>");
        }
        else if (sTipo == "C")
        {
            string sFecha = dr["t337_fecha"].ToString();
            if (sFecha != "") sFecha = DateTime.Parse(dr["t337_fecha"].ToString()).ToShortDateString();
            //sb.Append(sFecha + "&nbsp;<nobr class='NBR' style='width:520px' >" + dr["Comentarios"].ToString() + "</nobr></td>");  //Fechas de Consumo y Comentarios
            sb.Append(sFecha + "<nobr class='NBR W500 N2' >" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</nobr></td>");  //Fechas de Consumo y Comentarios
        }

        
        if (sTipo == "C")
        {
            double nHR = 0;
            if (dr["TotalHorasReportadas"] != DBNull.Value)
            {
                nHR = double.Parse(dr["TotalHorasReportadas"].ToString());
            }
            sb.Append("<td style='text-align:right;'>");
            sb.Append(nHR.ToString("N"));// TotalHorasReportadas
            sb.Append("</td>");

            double nJR = 0;
            if (dr["TotalJornadasReportadas"] != DBNull.Value)
            {
                nJR = double.Parse(dr["TotalJornadasReportadas"].ToString());
                sb.Append("<td title='" + dr["TotalJornadasReportadas"].ToString() + "' style='padding-right:5px;text-align:right;'>");
            }
            else
                sb.Append("<td title='0' style='padding-right:5px;text-align:right;'>");
            sb.Append(nJR.ToString("N"));// TotalJornadasReportadas
        }
        else
            sb.Append("<td style='text-align:right;'></td><td style='padding-right:5px;text-align:right;'>");

        sb.Append("</td></tr>");

        string sResul = sb.ToString();
        sb.Length = 0;
        return sResul;
    }

    private string recuperarPSN(string sT305IdProy)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy2(int.Parse(sT305IdProy));
            if (dr.Read())
            {
                sb.Append(dr["t301_estado"].ToString() + "@#@");  //2
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //3
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "@#@");  //4
                sb.Append(sT305IdProy + "@#@");  //5
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //6
                sb.Append(dr["responsable"].ToString() + "@#@");  //7
                if ((bool)dr["t320_facturable"]) sb.Append("1@#@");  //8
                else sb.Append("0@#@");  //8
                sb.Append(dr["t302_denominacion"].ToString() + "@#@");  //9
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //10
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

}
