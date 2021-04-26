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
    protected int nIndice;
    public int i = 0, iNumNodos = 0;
    public SqlConnection oConn;
    public SqlTransaction tr;
    private bool bHayPreferencia = false;
    public short nPantallaPreferencia = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (!(bool)Session["CONST1024"])
            {
                Master.nResolucion = 1280;
            }
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.TituloPagina = "Consulta de consumos por tarea";

            try
            {
                bool bEsAdminProduccion = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();

                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                DateTime dHoy = DateTime.Now, dtAux;
                int nDias = dHoy.Day;
                dtAux = dHoy.AddDays(-nDias + 1);
                txtFechaInicio.Text = dtAux.ToShortDateString();
                dtAux = dtAux.AddMonths(1).AddDays(-1);
                txtFechaFin.Text = dtAux.ToShortDateString();

                this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                //this.hdnNodo.Value = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                //Cargo la denominacion del label Nodo
                string sAux = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                if (sAux.Trim() != "")
                {
                    this.lblNodo.InnerText = sAux;
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    this.gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                }
                //obtenerAtributosEstadisticos();
                divAE.InnerHtml = "<table id='tblVAE'></table>";
                if (bEsAdminProduccion)
                {
                    cboCR.Visible = false;
                    //hdnIdNodo.Visible = true;
                    txtDesNodo.Visible = true;
                    gomaNodo.Visible = true;
                }
                else
                {
                    cboCR.Visible = true;
                    //hdnIdNodo.Visible = false;
                    txtDesNodo.Visible = false;
                    gomaNodo.Visible = false;
                    cargarNodos();
                }
                //if (!string.IsNullOrEmpty(sOP))
                //{
                //    string strTabla = obtenerTareas("", "", "", "", "", "", "", txtFechaInicio.Text, txtFechaFin.Text, "", "", "", "", "");
                //    string[] aTabla = Regex.Split(strTabla, "@#@");
                //    if (aTabla[0] == "OK") divDatos.InnerHtml = aTabla[1];
                //}
                //else
                //{
                //PREFERENCIAS
                string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                if (bHayPreferencia && aDatosPref[0] == "OK")
                {
                    if (bEsAdminProduccion)
                    {
                        hdnIdNodo.Value = aDatosPref[1];
                        txtDesNodo.Text = aDatosPref[2];

                        string sAux1 = obtenerAtributosEstadisticos(aDatosPref[1]);
                        if (sAux1.Substring(0, 2) == "OK")
                        {
                            sAux1 = sAux1.Substring(5, sAux1.Length - 5);
                            divAE.InnerHtml = sAux1;
                        }
                    }
                    else
                    {
                        cboCR.SelectedValue = aDatosPref[1];
                        if (iNumNodos > 1)
                        {
                            string sAux2 = obtenerAtributosEstadisticos(aDatosPref[1]);
                            if (sAux2.Substring(0, 2) == "OK")
                            {
                                sAux2 = sAux2.Substring(5, sAux2.Length - 5);
                                divAE.InnerHtml = sAux2;
                            }
                        }
                    }
                    this.hdnT305IdProy.Value = aDatosPref[3];
                    if (aDatosPref[4] != "") txtCodProy.Text = int.Parse(aDatosPref[4]).ToString("#,###");
                    this.txtNomProy.Text = aDatosPref[5];
                    this.hdnIdPT.Text = aDatosPref[6];
                    this.txtDesPT.Text = aDatosPref[7];
                    this.hdnIdFase.Text = aDatosPref[8];
                    this.txtFase.Text = aDatosPref[9];
                    this.hdnIdActividad.Text = aDatosPref[10];
                    this.txtActividad.Text = aDatosPref[11];
                    if (aDatosPref[12] != "") txtIdTarea.Text = int.Parse(aDatosPref[12]).ToString("#,###");
                    this.txtDesTarea.Text = aDatosPref[13];
                    this.hdnCliente.Value = aDatosPref[14];
                    this.txtIdCliente.Text = aDatosPref[15];
                    this.txtDesCliente.Text = aDatosPref[16];
                    this.txtIdPST.Text = aDatosPref[17];
                    this.txtCodPST.Text = aDatosPref[18];
                    this.txtDesPST.Text = aDatosPref[19];

                    if (aDatosPref[20] == "S")
                        rdbCodigo.Items[0].Selected = true;
                    else
                    {
                        if (aDatosPref[20] == "E") rdbCodigo.Items[1].Selected = true;
                        else rdbCodigo.Items[2].Selected = true;
                    }
                    //this.chkEstado.Items[0].Selected = (aDatosPref[21] == "1") ? true : false;
                    //this.chkEstado.Items[1].Selected = (aDatosPref[22] == "1") ? true : false;
                    //this.chkEstado.Items[2].Selected = (aDatosPref[23] == "1") ? true : false;
                    //this.chkEstado.Items[3].Selected = (aDatosPref[24] == "1") ? true : false;
                    //this.chkEstado.Items[4].Selected = (aDatosPref[25] == "1") ? true : false;
                    string sEstado = "";
                    string[] aAux = Regex.Split(aDatosPref[21], "#");
                    //int iNumElem=0;
                    for (int i = 0; i < 6; i++)
                    {
                        this.chkEstado.Items[i].Selected = (aAux[i] == "1") ? true : false;
                        if (sEstado == "") sEstado = i.ToString();
                        else sEstado += "," + i.ToString();
                    }
                    //foreach (ListItem li in chkEstado.Items)
                    //{
                    //    if (li.Selected == true)
                    //    {
                    //        //msg += "<BR>" + li.Text + " is selected.";
                    //        if (sEstado == "") sEstado = iNumElem.ToString();
                    //        else sEstado += "," + iNumElem.ToString();
                    //    }
                    //    iNumElem++;
                    //}

                    chkActuAuto.Checked = (aDatosPref[22] == "1") ? true : false;
                    chkCamposLibres.Checked = (aDatosPref[23] == "1") ? true : false;
                    if (chkActuAuto.Checked)
                    {
                        //btnObtener.Disabled = true;
                        string strTabla = obtenerTareas(aDatosPref[1], aDatosPref[3], aDatosPref[6], aDatosPref[8], aDatosPref[10], aDatosPref[12],
                                                        aDatosPref[13], aDatosPref[14], txtFechaInicio.Text, txtFechaFin.Text,
                                                        "", aDatosPref[5], sEstado, "", aDatosPref[17], (aDatosPref[23] =="1") ? true : false);
                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] == "OK") divDatos.InnerHtml = aTabla[1];
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                    else
                        divDatos.InnerHtml = "<table id='tblDatos'></table>";
                }
                else
                {
                    rdbCodigo.Items[0].Selected = true;
                    if (aDatosPref[0] == "Error")
                        Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                    else
                        divDatos.InnerHtml = "<table id='tblDatos'></table>";
                }
                //}
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
            case ("getTareaM"):
                sResultado += obtenerTareasM(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9],
                                            aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18]=="1" ? true : false);
                break;
            case ("getTarea"):
                //sResultado += obtenerTareas(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8],
                //                            aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15]);
                sResultado += obtenerTareasTot(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9],
                                               aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18] == "1" ? true : false);
                break;
            case ("getProf"):
                sResultado += obtenerProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4], int.Parse(aArgs[5]));
                break;
            case ("getConsumos"):
                sResultado += obtenerConsumos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], int.Parse(aArgs[5]));
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("getAE"):
                sResultado += obtenerAtributosEstadisticos(aArgs[1]);
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8],
                                             aArgs[9], aArgs[10], aArgs[11], aArgs[12]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
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

    private string obtenerAtributosEstadisticos(string sNodo)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbMarcar = new StringBuilder();
        StringBuilder sbDesmarcar = new StringBuilder();
        bool b1024 = (bool)Session["CONST1024"];
        string sRes = "";

        sbMarcar.Append("<IMG src='../../../../images/botones/imgmarcar.gif' onclick='mVAEs(this.parentNode.parentNode)' align='absMiddle' border='0' title='Marca todos los valores del atributo estadístico'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        sbDesmarcar.Append("<IMG src='../../../../images/botones/imgdesmarcar.gif' onclick='dVAEs(this.parentNode.parentNode)' align='absMiddle' border='0' title='Desmarca todos los valores del atributo estadístico'>");

        if (b1024)
        {
            sb.Append("<table id='tblVAE' style='width: 370px;'>");
            sb.Append("<colgroup><col style='width:10px' /><col style='width:220px;' /><col style='width:140px' /></colgroup>");
        }
        else
        {
            sb.Append("<table id='tblVAE' style='width: 520px;'>");
            sb.Append("<colgroup><col style='width:10px' /><col style='width:350px;' /><col style='width:160px' /></colgroup>");
        }
        try
        {
            if (sNodo == "")
                sRes = "error@#@";
            else
            {
                sRes = "OK@#@";
                SqlDataReader dr = AE.Catalogo(null, "", true, null, null, short.Parse(sNodo), null, "T", 4, 0);
                ArrayList aAE = new ArrayList();
                while (dr.Read())
                {
                    string[] aDatos = new string[] { dr["t341_idae"].ToString(), dr["t341_nombre"].ToString() };
                    aAE.Add(aDatos);
                }
                dr.Close();
                dr.Dispose();
                for (int i = 0; i < aAE.Count; i++)
                {
                    if (i % 2 == 0) sb.Append("<tr style='vertical-align:top;' id='" + ((string[])aAE[i])[0] + "' des='" + ((string[])aAE[i])[1] + "' class='FA' ti=1>" + (char)13);
                    else sb.Append("<tr style='vertical-align:top;' id='" + ((string[])aAE[i])[0] + "' des='" + ((string[])aAE[i])[1] + "' class='FB' ti=1>" + (char)13);

                    sb.Append("<td><input type='checkbox' class='checkTabla' onclick='borrarCatalogo();buscar1();'></td>");
                    sb.Append("<td style='padding-left:8px;'>" + ((string[])aAE[i])[1] + "<br />" + sbMarcar + sbDesmarcar + "</td>" + (char)13);
                    if (b1024)
                    {
                        sb.Append("<td><div style='overflow: auto; width: 140px; height:45px; position: relative; z-index: 7000'>" + (char)13);
                        sb.Append("<table width='120px'>" + (char)13);
                    }
                    else
                    {
                        sb.Append("<td><div style='overflow: auto; width: 160px; height:45px; position: relative; z-index: 7000'>" + (char)13);
                        sb.Append("<table width='140px'>" + (char)13);
                    }

                    sb.Append("<tr ti=2><td>" + (char)13);

                    SqlDataReader dr1 = VAE.Catalogo(null, "", true, int.Parse(((string[])aAE[i])[0]), null, 5, 0);
                    int x = 0;
                    while (dr1.Read())
                    {
                        if (x > 0) sb.Append("<br />");
                        sb.Append("<input id='" + dr1["t340_idvae"].ToString() + "' idAE='" + ((string[])aAE[i])[0] + "'");
                        sb.Append(" type='checkbox' class='checkTabla' value='" + dr1["t340_idvae"].ToString() + "'");
                        sb.Append(" onclick='borrarCatalogo();buscar1();' />&nbsp;" + dr1["t340_valor"].ToString() + (char)13);
                        x++;
                    }
                    dr1.Close(); dr1.Dispose();

                    sb.Append("</td></tr>" + (char)13);
                    sb.Append("</table>" + (char)13);
                    sb.Append("</div>" + (char)13);
                    sb.Append("</td>" + (char)13);

                    sb.Append("</tr>" + (char)13);
                }
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los atributos estadísticos", ex);
        }

        sb.Append("</table>" + (char)13);
        //divAE.InnerHtml = sb.ToString();
        sRes += sb.ToString();
        sb.Length = 0;
        return sRes;
    }
    private string obtenerTareas(string sNodo, string sPE, string sPT, string sFase, string sActividad, string sTarea, string sDesTarea, string sCliente,
                                 string sDesde, string sHasta, string sVAE, string sDesPE, string sEstado, string sAE, string sIdPST, bool bCamposLibres)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        int? nNodo = null, numPE = null, numPT = null, numFase = null, numActividad = null, numTarea = null, numCliente = null, numPST = null;
        int nWidthTabla = 1160;
        bool b1024 = false;
        bool bError = false;
        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                if ((bool)Session["CONST1024"])
                {
                    b1024 = true;
                    nWidthTabla = 980;
                }
                if (sNodo != "") nNodo = int.Parse(sNodo);
                if (sPE != "") numPE = int.Parse(sPE);
                if (sPT != "") numPT = int.Parse(sPT);
                if (sFase != "") numFase = int.Parse(sFase);
                if (sActividad != "") numActividad = int.Parse(sActividad);
                if (sTarea != "") numTarea = int.Parse(sTarea);
                if (sCliente != "") numCliente = int.Parse(sCliente);
                if (sIdPST != "") numPST = int.Parse(sIdPST);

                SqlDataReader dr =
                    TAREAPSP.ConsumosTareaMensual_T((int)Session["UsuarioActual"], nNodo,
                                                    numPE, numPT, numFase, numActividad, numTarea, Utilidades.unescape(sDesTarea),
                                                    numCliente, DateTime.Parse(sDesde), DateTime.Parse(sHasta), sVAE, sEstado, sAE, numPST, bCamposLibres );
                int i = 0;
                while (dr.Read())
                {
                    if (i == 0)
                    {
                        nWidthTabla = nWidthTabla + (120 * (dr.FieldCount - 12));
                        sb.Append("<table id='tblDatos' style='width: " + nWidthTabla.ToString() + "px;'>");
                        sb.Append("<colgroup>");
                        sb.Append("<col style='width:20px;' />");
                        sb.Append("<col style='width:45px;' />");
                        if (b1024)
                        {
                            sb.Append("<col style='width:305px;' />");
                            sb.Append("<col style='width:85px;' />");
                            sb.Append("<col style='width:85px;' />");
                            sb.Append("<col style='width:65px;' />");
                        }
                        else
                        {
                            sb.Append("<col style='width:445px;' />");
                            sb.Append("<col style='width:100px;' />");
                            sb.Append("<col style='width:100px;' />");
                            sb.Append("<col style='width:75px;' />");
                        }

                        sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");
                        if (dr.FieldCount > 12)
                        {
                            for (int x = 13; x <= dr.FieldCount; x++)
                                sb.Append("<col style='width:120px;' />");
                        }
                        sb.Append("</colgroup>");
                    }
                    if (i % 2 == 0) sb.Append("<tr class=FA ");
                    else sb.Append("<tr class=FB ");

                    sb.Append("nivel=1 N='S' sw=1 style='height:18px' onmouseover='TTip(event);' T='" + dr["t332_idtarea"].ToString() + "' desplegado=0>");
                    sb.Append("<td><img src='../../../../images/plus.gif' onclick='mostrar(this)' style='width:9px; height:9px;' class='ICO'></td>");
                    sb.Append("<td>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + "</td>");
                    if (b1024)
                    {
                        sb.Append("<td style='margin-left:5px;'><span class='NBR W295'>" + dr["t332_destarea"].ToString() + "</span></td>");
                    }
                    else
                    {
                        sb.Append("<td style='margin-left:5px;'><span class='NBR W440'>" + dr["t332_destarea"].ToString() + "</span></td>");
                    }
                    if (dr["t346_codpst"].ToString() == "") sb.Append("<td>");
                    else sb.Append("<td title=\"" + dr["t346_despst"].ToString() + "\">");
                    sb.Append(dr["t346_codpst"].ToString() + "</td>");

                    if (dr["t332_otl"].ToString() == "") sb.Append("<td>");
                    else sb.Append("<td title=\"" + dr["t332_otl"].ToString() + "\">");
                    sb.Append(dr["t332_otl"].ToString() + "</td>");

                    sb.Append("<td>" + dr["Estado"].ToString() + "</td>");

                    sb.Append("<td style='text-align:right;'>");//etpl
                    double nETPL = 0;
                    if (dr["etpl"] != DBNull.Value) nETPL = double.Parse(dr["etpl"].ToString());
                    if (nETPL > 0) sb.Append(nETPL.ToString("N"));// TotalHorasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;'>");//etpr
                    double nETPR = 0;
                    if (dr["etpr"] != DBNull.Value) nETPR = double.Parse(dr["etpr"].ToString());
                    if (nETPR > 0) sb.Append(nETPR.ToString("N"));// TotalJornadasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;'>");//FFPR
                    string sFecha = dr["ffpr"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["ffpr"].ToString()).ToShortDateString();
                    sb.Append(sFecha);
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;'>");//Horas
                    double nHR = 0;
                    if (dr["TotalHorasReportadas"] != DBNull.Value) nHR = double.Parse(dr["TotalHorasReportadas"].ToString());
                    sb.Append(nHR.ToString("N"));// TotalHorasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;padding-right:5px;'>");//Jornadas
                    double nJR = 0;
                    if (dr["TotalJornadasReportadas"] != DBNull.Value) nJR = double.Parse(dr["TotalJornadasReportadas"].ToString());
                    sb.Append(nJR.ToString("N"));// TotalJornadasReportadas
                    sb.Append("</td>");

                    if (dr.FieldCount > 12)
                    {
                        for (int x = 12; x < dr.FieldCount; x++)
                            sb.Append("<td>" + dr[x].ToString() + "</td>");
                    }
                    sb.Append("</tr>");

                    i++;
                }
                dr.Close();
                dr.Dispose();

                if (i == 0)
                {
                    sb.Append("<table id='tblDatos' style='width: " + nWidthTabla.ToString() + "px;'>");
                    sb.Append("<colgroup>");
                    sb.Append("<col style='width:20px;' />");
                    sb.Append("<col style='width:35px;' />");
                    sb.Append("<col style='width:455px;' />");
                    sb.Append("<col style='width:100px;' />");
                    sb.Append("<col style='width:100px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("</colgroup>");
                }

                sb.Append("</table>");
                sb.Append("@#@");

                if (sPE != "" && sDesPE == "")
                {
                    //obtener la descripcion del proyecto económico
                    try
                    {
                        SqlDataReader dr2 = PROYECTO.fgGetDatosProy(int.Parse(sPE));
                        sb.Append(dr2["t301_denominacion"].ToString());
                        dr2.Close();
                        dr2.Dispose();
                    }
                    catch (Exception ex)
                    {
                        sResul = "Error@#@" + ex.Message;
                        return sResul;
                    }

                }
                //        //sResul += "@#@" + sPE;
                sResul = "OK@#@" + sb.ToString();
                sb.Length = 0; //Para liberar memoria       
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta", ex);
        }

        return sResul;
    }
    private string obtenerTareasM(string sNodo, string sPE, string sPT, string sFase, string sActividad, string sTarea, string sDesTarea,
                                 string sCliente, string sDesde, string sHasta, string sCodigo, string sVAE, string sDesPE,
                                 string sEstado, string sAE, string sIdPST, string sColumnasAE, bool bCamposLibres)
    {
        string sResul = "", sCodRecurso = "";
        StringBuilder sb = new StringBuilder();
        int? nNodo = null, numPE = null, numPT = null, numFase = null, numActividad = null, numTarea = null, numCliente = null, numPST = null;
        bool bError = false;

        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                if (sNodo != "") nNodo = int.Parse(sNodo);
                if (sPE != "") numPE = int.Parse(sPE);
                if (sPT != "") numPT = int.Parse(sPT);
                if (sFase != "") numFase = int.Parse(sFase);
                if (sActividad != "") numActividad = int.Parse(sActividad);
                if (sTarea != "") numTarea = int.Parse(sTarea);
                if (sCliente != "") numCliente = int.Parse(sCliente);
                if (sIdPST != "") numPST = int.Parse(sIdPST);

                SqlDataReader dr =
                    TAREAPSP.ConsumosTareaMensual_T2((int)Session["UsuarioActual"], nNodo,
                                                    numPE, numPT, numFase, numActividad, numTarea, Utilidades.unescape(sDesTarea),
                                                    numCliente, DateTime.Parse(sDesde), DateTime.Parse(sHasta), sVAE, sEstado, sAE, numPST, bCamposLibres);

                int i = 0;
                while (dr.Read())
                {
                    if (i % 2 == 0) sb.Append("<tr class=FA style='vertical-align:top;'>");
                    else sb.Append("<tr class=FB style='vertical-align:top;'>");

                    sb.Append("<td>" + dr["t301_idproyecto"].ToString() + " - " + dr["t301_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t331_despt"].ToString() + "</td>");
                    sb.Append("<td style='margin--right:5px;'>" + dr["t332_idtarea"].ToString() + " - " + dr["t332_destarea"].ToString() + "</td>");
                    sCodRecurso = "";
                    if (sCodigo != "N")
                    {
                        if (sCodigo == "S") sCodRecurso = " (" + dr["t314_idusuario"].ToString() + ")";
                        else sCodRecurso = " (" + dr["t360_codigoexterno"].ToString() + ")";
                    }
                    sb.Append("<td>" + dr["profesional"].ToString() + sCodRecurso + "</td>");

                    //sb.Append("<td>" + dr["t026_fecha"].ToString() + "</td>");
                    sb.Append("<td>");
                    string sFechaCons = dr["t337_fecha"].ToString();
                    if (sFechaCons != "")
                    {
                        sFechaCons = DateTime.Parse(dr["t337_fecha"].ToString()).ToShortDateString();
                    }
                    sb.Append(sFechaCons);
                    sb.Append("</td>");

                    sb.Append("<td>" + dr["Comentario"].ToString() + "</td>");//comentario

                    sb.Append("<td>" + dr["t346_codpst"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t346_despst"].ToString() + "</td>");
                    //OTL
                    sb.Append("<td>" + dr["t332_otl"].ToString() + "</td>");
                    sb.Append("<td>" + dr["Estado"].ToString() + "</td>");

                    sb.Append("<td style='text-align:right;'>");//etpl
                    double nETPL = 0;
                    if (dr["etpl"] != DBNull.Value) nETPL = double.Parse(dr["etpl"].ToString());
                    if (nETPL > 0) sb.Append(nETPL.ToString("N"));// TotalHorasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;'>");//etpr
                    double nETPR = 0;
                    if (dr["etpr"] != DBNull.Value) nETPR = double.Parse(dr["etpr"].ToString());
                    if (nETPR > 0) sb.Append(nETPR.ToString("N"));// TotalJornadasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;'>");//FFPR
                    string sFecha = dr["ffpr"].ToString();
                    if (sFecha != "")
                    {
                        sFecha = DateTime.Parse(dr["ffpr"].ToString()).ToShortDateString();
                    }
                    sb.Append(sFecha);
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;'>");//Horas
                    double nHR = 0;
                    if (dr["TotalHorasReportadas"] != DBNull.Value) nHR = double.Parse(dr["TotalHorasReportadas"].ToString());
                    sb.Append(nHR.ToString("N"));// TotalHorasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;padding-right:5px;'>");//Jornadas
                    double nJR = 0;
                    if (dr["TotalJornadasReportadas"] != DBNull.Value) nJR = double.Parse(dr["TotalJornadasReportadas"].ToString());
                    sb.Append(nJR.ToString("N"));// TotalJornadasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='padding-right:5px;'>");//
                    sb.Append(dr["Facturable"]);// Facturable
                    sb.Append("</td>");

                    sColumnasAE = "";
                    if (dr.FieldCount > 21)
                    {
                        for (int x = 21; x < dr.FieldCount; x++)
                        {
                            sColumnasAE += "<td>" + dr.GetName(x) + "</td>";
                            if ((dr[x].GetType()).Name == "Decimal") sb.Append("<td>" + double.Parse(dr[x].ToString()).ToString("N") + "</td>"); 
                            else sb.Append("<td>" + dr[x].ToString() + "</td>");
                        }
                    }

                    sb.Append("</tr>");

                    i++;
                }
                dr.Close();
                dr.Dispose();

                string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
                StringBuilder sb2 = new StringBuilder();
                sb2.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		        sb2.Append("	<TR align=center style='background-color: #BCD4DF;'>");
                sb2.Append("<td style='width:300px;'>Proyecto Económico</TD>");
                sb2.Append("<td style='width:300px;'>Proyecto Técnico</TD>");
                sb2.Append("<td style='width:300px;'>Tarea</TD>");
                sb2.Append("<td style='width:300px;'>Profesional</TD>");
                sb2.Append("<td style='width:70px;'>F. consumo</TD>");
                sb2.Append("<td style='width:300px;'>Comentario</TD>");
                sb2.Append("<td style='width:200px;'>Código OTC</TD>");
                sb2.Append("<td style='width:400px;'>Denominación OTC</TD>");
                sb2.Append("<td style='width:200px;'>OTL</TD>");
                sb2.Append("<td style='width:60px;'>Estado</td>");
                sb2.Append("<td style='width:75px;'>ETPL</td>");
                sb2.Append("<td style='width:75px;'>ETPR</td>");
                sb2.Append("<td style='width:60px;'>FFPR</td>");
                sb2.Append("<td style='width:75px;'>Horas</td>");
                sb2.Append("<td style='width:75px;'>Jornadas</td>");
                sb2.Append("<td style='width:60px;'>Factur.</td>");
                sb2.Append(sColumnasAE);
                Session[sIdCache] = sb2.ToString() + sb.ToString() + "</table>";

                sb.Append("@#@");
                if (sPE != "" && sDesPE == "")
                {
                    //obtener la descripcion del proyecto económico
                    try
                    {
                        SqlDataReader dr2 = PROYECTO.fgGetDatosProy(int.Parse(sPE));
                        sb.Append(dr2["t301_denominacion"].ToString());
                        dr2.Close();
                        dr2.Dispose();
                    }
                    catch (Exception ex)
                    {
                        sResul = "Error@#@" + ex.Message;
                        return sResul;
                    }

                }
                //sResul = "OK@#@" + sb.ToString();
                sResul = "OK@#@cacheado@#@" + sIdCache + "@#@" + sb2.ToString() + sb.ToString(); ;
                sb.Length = 0; //Para liberar memoria   
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@@" + Errores.mostrarError("Error al obtener los datos de consulta", ex);
        }

        return sResul;
    }
    private string obtenerProfesionales(string sTarea, string sDesde, string sHasta, string sCodigo, int numAE)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool bError = false;

        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                SqlDataReader dr = TAREAPSP.ConsumosTareaMensual_P((int)Session["UsuarioActual"], null,
                                                                   int.Parse(sTarea), DateTime.Parse(sDesde), DateTime.Parse(sHasta), sCodigo);

                while (dr.Read())
                {
                    sb.Append("<tr nivel=2 N='S' style='height:18px' T='" + dr["t332_idtarea"].ToString() + "'");
                    sb.Append(" R='" + dr["t314_idusuario"].ToString() + "' desplegado=0");
                    sb.Append(" tipo ='" + dr["interno"].ToString() + "'");
                    sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                    sb.Append(" baja ='" + dr["baja"].ToString() + "' sw=1>");
                    string sUsuario = "";
                    if (sCodigo != "N")
                    {
                        if (sCodigo == "S") sUsuario = "(" + dr["t314_idusuario"].ToString() + ")";
                        else sUsuario = "(" + dr["t360_codigoexterno"].ToString() + ")";
                    }
                    sb.Append("<td style='text-indent:5px;'><img src='../../../../images/plus.gif' onclick='mostrar(this)' style='width:9px; height:9px;' class='ICO'></td>");
                    //sb.Append("<td style='padding-left:5px;'></td>");//para el icono del menos
                    //sb.Append("<td></td>");//para el icono del profesional
                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        if (dr["interno"].ToString() == "I")
                            sb.Append("<td><img src='../../../../images/imgUsuIV.gif' style='width:16px; height:16px;' class='ICO'></td>");
                        else
                            sb.Append("<td><img src='../../../../images/imgUsuEV.gif' style='width:16px; height:16px;' class='ICO'></td>");
                    }
                    else
                    {
                        if (dr["interno"].ToString() == "I")
                            sb.Append("<td><img src='../../../../images/imgUsuIM.gif' style='width:16px; height:16px;' class='ICO'></td>");
                        else
                            sb.Append("<td><img src='../../../../images/imgUsuEM.gif' style='width:16px; height:16px;' class='ICO'></td>");
                    }
                    //sb.Append("<td><nobr class='NBR W430' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "&nbsp;&nbsp;&nbsp;" + sUsuario + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W430' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "&nbsp;&nbsp;&nbsp;" + sUsuario + "</nobr></td>");

                    sb.Append("<td></td>"); //OTC
                    sb.Append("<td></td>"); //OTL
                    sb.Append("<td></td>"); //Estado
                    sb.Append("<td style='text-align:right;'></td>");//etpl
                    sb.Append("<td style='text-align:right;'></td>");//etpr
                    sb.Append("<td style='text-align:right;'></td>");//FFPR

                    sb.Append("<td style='text-align:right;'>");//Horas
                    double nHR = 0;
                    if (dr["TotalHorasReportadas"] != DBNull.Value) nHR = double.Parse(dr["TotalHorasReportadas"].ToString());
                    sb.Append(nHR.ToString("N"));// TotalHorasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;padding-right:5px;'>");//Jornadas
                    double nJR = 0;
                    if (dr["TotalJornadasReportadas"] != DBNull.Value) nJR = double.Parse(dr["TotalJornadasReportadas"].ToString());
                    sb.Append(nJR.ToString("N"));// TotalJornadasReportadas
                    sb.Append("</td>");
                    if (numAE > 0)
                    {
                        for (int x = 0; x < numAE; x++) sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sResul = "OK@#@" + sb.ToString();
                sb.Length = 0; //Para liberar memoria   
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta", ex);
        }

        return sResul;
    }
    private string obtenerConsumos(string sTarea, string nProfesional, string sDesde, string sHasta, int numAE)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool bError = false;

        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                SqlDataReader dr = TAREAPSP.ConsumosTareaMensual_C(int.Parse(sTarea), int.Parse(nProfesional), DateTime.Parse(sDesde), DateTime.Parse(sHasta));

                while (dr.Read())
                {
                    sb.Append("<tr nivel=3 sw=1 style='height:18px' onmouseover='TTip(event);'>");
                    //sb.Append("<td class='N3'></td><td></td><td>");
                    sb.Append("<td></td><td></td><td>");
                    string sFecha = dr["t337_fecha"].ToString();
                    if (sFecha != "") sFecha = DateTime.Parse(dr["t337_fecha"].ToString()).ToShortDateString();
                    sb.Append(sFecha);
                    if (dr["Comentarios"].ToString() != "")
                    {
                        if (!(bool)Session["CONST1024"])
                            sb.Append("&nbsp;&nbsp;&nbsp;<span class='NBR W375'>" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</span>");  //Fechas de Consumo y Comentarios
                        else
                            sb.Append("&nbsp;&nbsp;&nbsp;<span class='NBR W240'>" + dr["Comentarios"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</span>");  //Fechas de Consumo y Comentarios
                    }

                    sb.Append("</td>");
                    sb.Append("<td></td>"); //OTC
                    sb.Append("<td></td>"); //OTL
                    sb.Append("<td></td>"); //Estado
                    sb.Append("<td style='text-align:right;'></td>");//etpl
                    sb.Append("<td style='text-align:right;'></td>");//etpr
                    sb.Append("<td style='text-align:right;'></td>");//FFPR

                    sb.Append("<td style='text-align:right;'>");//Horas
                    double nHR = 0;
                    if (dr["TotalHorasReportadas"] != DBNull.Value) nHR = double.Parse(dr["TotalHorasReportadas"].ToString());
                    sb.Append(nHR.ToString("N"));// TotalHorasReportadas
                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;padding-right:5px;'>");//Jornadas
                    double nJR = 0;
                    if (dr["TotalJornadasReportadas"] != DBNull.Value) nJR = double.Parse(dr["TotalJornadasReportadas"].ToString());
                    sb.Append(nJR.ToString("N"));// TotalJornadasReportadas
                    sb.Append("</td>");

                    if (numAE > 0)
                    {
                        for (int x = 0; x < numAE; x++) sb.Append("<td></td>");
                    }

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sResul = "OK@#@" + sb.ToString();
                sb.Length = 0; //Para liberar memoria     
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta", ex);
        }

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
            while (dr.Read())
            {
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
    private void cargarNodos()
    {
        try
        {
            //Cargar el combo de nodos accesibles
            //int iNumNodos = 0;
            string sNodo = "";
            ListItem oLI = null;
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, false);
            while (dr.Read())
            {
                sNodo = dr["identificador"].ToString();
                oLI = new ListItem(dr["denominacion"].ToString(), sNodo);
                cboCR.Items.Add(oLI);
                iNumNodos++;
            }
            dr.Close();
            dr.Dispose();
            if (iNumNodos == 1)
            {
                string sAux = obtenerAtributosEstadisticos(sNodo);
                if (sAux.Substring(0, 2) == "OK")
                {
                    this.hdnNumNodos.Value = "1";
                    sAux = sAux.Substring(5, sAux.Length - 5);
                    divAE.InnerHtml = sAux;
                }
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    #region Preferencias
    private string setPreferencia(string sNodo, string sPSN, string sPT, string sF, string sA, string sT, string sCli, string sPST, string sCod,
                                  string sEstado, string sActuAuto, string sCamposLibres)
    {
        string sResul = "";
        string strNodo = null, strActuAuto = null, strPSN = null, strPT = null, strF = null, strA = null, strT = null, strCli = null;
        string strPST = null, strCod = null, strEstado = null, strCamposLibres = null;
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
            if (sNodo != "") strNodo = sNodo;
            if (sPSN != "") strPSN = sPSN;
            if (sPT != "") strPT = sPT;
            if (sF != "") strF = sF;
            if (sA != "") strA = sA;
            if (sT != "") strT = sT;
            if (sCli != "") strCli = sCli;
            if (sPST != "") strPST = sPST;
            if (sCod != "") strCod = sCod;
            if (sEstado != "") strEstado = sEstado;
            if (sActuAuto != "") strActuAuto = sActuAuto;
            if (sCamposLibres != "") strCamposLibres = sCamposLibres;

            int nPref = PREFERENCIAUSUARIO.Insertar(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 10, strNodo, strPSN, strPT, strF,
                                                    strA, strT, strCli, strPST, strCod, strEstado, strActuAuto, "", "", "", "", 
                                                    null, null, null, null, null, sCamposLibres);

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
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 10);
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
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 10);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["idNodo"].ToString() + "@#@"); //0
                sb.Append(dr["t303_denominacion"].ToString() + "@#@"); //1
                sb.Append(dr["nPSN"].ToString() + "@#@"); //2 
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@"); //3
                sb.Append(dr["t301_denominacion"].ToString() + "@#@"); //4
                sb.Append(dr["nPT"].ToString() + "@#@"); //5
                sb.Append(dr["t331_despt"].ToString() + "@#@"); //6
                sb.Append(dr["nF"].ToString() + "@#@"); //7
                sb.Append(dr["t334_desfase"].ToString() + "@#@"); //8
                sb.Append(dr["nA"].ToString() + "@#@"); //9
                sb.Append(dr["t335_desactividad"].ToString() + "@#@"); //10
                sb.Append(dr["nT"].ToString() + "@#@"); //11
                sb.Append(dr["t332_destarea"].ToString() + "@#@"); //12
                sb.Append(dr["nCli"].ToString() + "@#@"); //13
                sb.Append(dr["t302_codigoexterno"].ToString() + "@#@"); //14
                sb.Append(dr["t302_denominacion"].ToString() + "@#@"); //15
                sb.Append(dr["nPST"].ToString() + "@#@"); //16
                sb.Append(dr["t346_codpst"].ToString() + "@#@"); //17
                sb.Append(dr["t346_despst"].ToString() + "@#@"); //18
                sb.Append(dr["TipoCodigo"].ToString() + "@#@"); //19
                sb.Append(dr["Estado"].ToString() + "@#@"); //20
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //21
                sb.Append(dr["CamposLibres"].ToString() + "@#@"); //22
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
    #endregion
    private string setResolucion()
    {
        try
        {
            Session["CONST1024"] = !(bool)Session["CONST1024"];

            USUARIO.UpdateResolucion(11, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["CONST1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }


    private string obtenerTareasTot(string sNivel, string sNodo, string sPE, string sPT, string sFase, string sActividad, string sTarea,
                                    string sDesTarea, string sCliente, string sDesde, string sHasta, string sVAE, string sDesPE,
                                    string sEstado, string sAE, string sIdPST, string sCodigo, bool bCamposLibres)
    {
        string sResul = ""; string sColumnasAE = "";
        StringBuilder sb = new StringBuilder();
        int? nNodo = null, numPE = null, numPT = null, numFase = null, numActividad = null, numTarea = null, numCliente = null, numPST = null;
        int nWidthTabla = 1160;
        bool b1024 = false;
        bool bError = false;
        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError)
            {
                if ((bool)Session["CONST1024"])
                {
                    b1024 = true;
                    nWidthTabla = 980;
                }
                if (sNodo != "") nNodo = int.Parse(sNodo);
                if (sPE != "") numPE = int.Parse(sPE);
                if (sPT != "") numPT = int.Parse(sPT);
                if (sFase != "") numFase = int.Parse(sFase);
                if (sActividad != "") numActividad = int.Parse(sActividad);
                if (sTarea != "")
                {
                    numTarea = int.Parse(sTarea);
                    sDesTarea = "";
                }
                if (sCliente != "") numCliente = int.Parse(sCliente);
                if (sIdPST != "") numPST = int.Parse(sIdPST);

                SqlDataReader dr =
                    TAREAPSP.ConsumosTareaMensual_Tot(sNivel, (int)Session["UsuarioActual"], nNodo,
                                                    numPE, numPT, numFase, numActividad, numTarea, Utilidades.unescape(sDesTarea),
                                                    numCliente, DateTime.Parse(sDesde), DateTime.Parse(sHasta), sVAE, sEstado, sAE, numPST, bCamposLibres);
                int i = 0;
                while (dr.Read())
                {
                    sColumnasAE = "";
                    if (i == 0)
                    {
                        nWidthTabla = nWidthTabla + (120 * (dr.FieldCount - 20));
                        sb.Append("<table id='tblDatos' style='width: " + nWidthTabla.ToString() + "px;'>");
                        sb.Append("<colgroup>");
                        sb.Append("<col style='width:20px;' />");
                        sb.Append("<col style='width:45px;' />");
                        if (b1024)
                        {
                            sb.Append("<col style='width:305px;' />");
                            sb.Append("<col style='width:85px;' />");
                            sb.Append("<col style='width:85px;' />");
                            sb.Append("<col style='width:65px;' />");
                        }
                        else
                        {
                            sb.Append("<col style='width:445px;' />");
                            sb.Append("<col style='width:100px;' />");
                            sb.Append("<col style='width:100px;' />");
                            sb.Append("<col style='width:75px;' />");
                        }

                        sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");
                        
                        //if (!b1024) sb.Append("<col style='width:75px;' />");
                        sb.Append("<col style='width:75px;' />");

                        if (dr.FieldCount > 20)
                        {
                            for (int x = 20; x < dr.FieldCount; x++)
                                sb.Append("<col style='width:120px;' />");
                        }
                        sb.Append("</colgroup>");
                    }
                    sb.Append("<tr");
                    //Si es fila de tarea pongo fila azul y fila blanca
                    if (dr["TipoLinea"].ToString() == "1")
                    {
                        if (i % 2 == 0) sb.Append(" class=FA");
                        else sb.Append(" class=FB");
                        i++;
                    }
                    sb.Append(" nivel=" + dr["TipoLinea"].ToString() + " N='S' style='height:18px' onmouseover='TTip(event);'");
                    sb.Append(" T='" + dr["t332_idtarea"].ToString() + "' R='" + dr["t314_idusuario"].ToString() + "'");

                    sb.Append(" tipo ='" + dr["interno"].ToString() + "'");
                    sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                    sb.Append(" baja ='" + dr["baja"].ToString() + "' sw=0");
                    //En funcion del tipo de linea y del nivel que estamos desplegando ponemos si la fila está desplegada o no
                    if (int.Parse(dr["TipoLinea"].ToString()) < int.Parse(sNivel))
                        sb.Append(" desplegado=1>");
                    else
                        sb.Append(" desplegado=0>");
                    //Si el nivel pedido es 1 se mostrarán las tareas con el icono +
                    //Si el nivel pedido es 2 se mostrarán las tareas con el icono - y los profesionales con el icono +
                    //Si el nivel pedido es 2 se mostrarán las tareas con el icono - los profesionales con el icono - y las fechas sin icono
                    sb.Append("<td></td>");
                    //switch (sNivel)
                    //{
                    //    case "1":
                    //        sb.Append("<td><img src='../../../../images/plus.gif' onclick='mostrar(this)' style='width:9px; height:9px;' class='ICO'></td>");
                    //        break;
                    //    case "2":
                    //        if (dr["TipoLinea"].ToString() == "1")
                    //            sb.Append("<td><img src='../../../../images/minus.gif' onclick='mostrar(this)' style='width:9px; height:9px;' class='ICO'></td>");
                    //        else
                    //            sb.Append("<td style='text-indent:5px;'><img src='../../../../images/plus.gif' onclick='mostrar(this)' style='width:9px; height:9px;' class='ICO'></td>");
                    //        break;
                    //    case "3":
                    //        if (dr["TipoLinea"].ToString() == "1")
                    //            sb.Append("<td><img src='../../../../images/minus.gif' onclick='mostrar(this)' style='width:9px; height:9px;' class='ICO'></td>");
                    //        else
                    //        {
                    //            if (dr["TipoLinea"].ToString() == "2")
                    //                sb.Append("<td style='text-indent:5px;'><img src='../../../../images/minus.gif' onclick='mostrar(this)' style='width:9px; height:9px;' class='ICO'></td>");
                    //            else
                    //                sb.Append("<td></td>");
                    //        }
                    //        break;
                    //}

                    if (dr["TipoLinea"].ToString() == "3")
                    {
                        string sFecha = dr["ffpr"].ToString();
                        if (sFecha != "") sFecha = DateTime.Parse(dr["ffpr"].ToString()).ToShortDateString();
                        string sCadConsumo = "<td></td><td style='margin-left:5px;'>" + sFecha;
                        if (dr["denominacion"].ToString() != "")
                        {
                            if (b1024)
                                sCadConsumo += "&nbsp;&nbsp;&nbsp;<span class='NBR W240'>" + dr["denominacion"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</span>";  //Fechas de Consumo y Comentarios
                            else
                                sCadConsumo += "&nbsp;&nbsp;&nbsp;<span class='NBR W375'>" + dr["denominacion"].ToString().Replace("'", "&#39;").Replace("\"", "&#39;") + "</span>";  //Fechas de Consumo y Comentarios
                        }
                        sCadConsumo += "</td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        sb.Append(sCadConsumo);
                    }
                    else
                    {
                        if (dr["TipoLinea"].ToString() == "1")
                        {
                            sb.Append("<td>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + "</td>");
                            
                            if (b1024)
                                sb.Append("<td style='margin-left:5px;'><span class='NBR W300'>" + dr["denominacion"].ToString() + "</span></td>");
                            else
                                sb.Append("<td style='margin-left:5px;'><span class='NBR W445'>" + dr["denominacion"].ToString() + "</span></td>");
                            if (dr["t346_codpst"].ToString() == "") sb.Append("<td>");
                            else sb.Append("<td title=\"" + dr["t346_despst"].ToString() + "\">");
                            sb.Append(dr["t346_codpst"].ToString() + "</td>");

                            if (dr["t332_otl"].ToString() == "") sb.Append("<td>");
                            else sb.Append("<td title=\"" + dr["t332_otl"].ToString() + "\">");
                            sb.Append(dr["t332_otl"].ToString() + "</td>");

                            sb.Append("<td>" + dr["Estado"].ToString() + "</td>");
                        }
                        else//TipoLinea==2 Profesional
                        {
                            string sUsuario = "";
                            if (sCodigo != "N")
                            {
                                if (sCodigo == "S")
                                    sUsuario = " (" + dr["t314_idusuario"].ToString() + ")";
                                else
                                {
                                    if (dr["t360_codigoexterno"].ToString() != "")
                                        sUsuario = " (" + dr["t360_codigoexterno"].ToString() + ")";
                                }
                            }
                            sb.Append("<td></td>");//para el icono del profesional
                            if (b1024)
                                sb.Append("<td style='margin-left:5px;'><span class='NBR W300' style='noWrap:true;'");
                            else
                                sb.Append("<td><span class='NBR W445' style='noWrap:true;'");
                            //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["denominacion"].ToString() + sUsuario + "</nobr></td>");
                            sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["denominacion"].ToString() + sUsuario + "</span></td>");
                            //sb.Append(" >" + dr["denominacion"].ToString() + sUsuario + "</nobr></td>");
                            sb.Append("<td></td><td></td><td></td>");
                        }

                        sb.Append("<td style='text-align:right;'>");//etpl
                        double nETPL = 0;
                        if (dr["etpl"] != DBNull.Value) nETPL = double.Parse(dr["etpl"].ToString());
                        if (nETPL > 0) sb.Append(nETPL.ToString("N"));// TotalHorasReportadas
                        sb.Append("</td>");

                        sb.Append("<td style='text-align:right;'>");//etpr
                        double nETPR = 0;
                        if (dr["etpr"] != DBNull.Value) nETPR = double.Parse(dr["etpr"].ToString());
                        if (nETPR > 0) sb.Append(nETPR.ToString("N"));// TotalJornadasReportadas
                        sb.Append("</td>");

                        sb.Append("<td style='text-align:right;'>");//FFPR
                        string sFecha = dr["ffpr"].ToString();
                        if (sFecha != "") sFecha = DateTime.Parse(dr["ffpr"].ToString()).ToShortDateString();
                        sb.Append(sFecha);
                        sb.Append("</td>");

                    }
                    sb.Append("<td style='text-align:right;'>");//Horas
                    double nHR = 0;
                    if (dr["TotalHorasReportadas"] != DBNull.Value) nHR = double.Parse(dr["TotalHorasReportadas"].ToString());
                    sb.Append(nHR.ToString("N"));// TotalHorasReportadas
                    sb.Append("</td>");

                    //if (!b1024)
                    //{
                        sb.Append("<td style='text-align:right;padding-right:5px;'>");//Jornadas
                        double nJR = 0;
                        if (dr["TotalJornadasReportadas"] != DBNull.Value) nJR = double.Parse(dr["TotalJornadasReportadas"].ToString());
                        sb.Append(nJR.ToString("N"));// TotalJornadasReportadas
                        sb.Append("</td>");
                    //}
                    
                    if (dr.FieldCount > 20)
                    {
                        for (int x = 20; x < dr.FieldCount; x++)
                        {
                            /*sb.Append("<td style='text-align:right;padding-right:5px;'>" + dr[x].ToString() + "</td>");
                            sColumnasAE += dr.GetName(x) + ",";*/
                            sColumnasAE += dr.GetName(x) + ",";
                            if ((dr[x].GetType()).Name == "Decimal") sb.Append("<td style='text-align:right;padding-right:5px;'>" + double.Parse(dr[x].ToString()).ToString("N") + "</td>");
                            else sb.Append("<td style='text-align:right;padding-right:5px;'>" + dr[x].ToString() + " </td>");
                        }
                            
                    }
                    sb.Append("</tr>");

                }
                dr.Close();
                dr.Dispose();

                if (i == 0)
                {
                    sb.Append("<table id='tblDatos' class='texto' style='WIDTH: " + nWidthTabla.ToString() + "px; BORDER-COLLAPSE: collapse; table-layout:fixed; ' cellSpacing='0' border='0'>");
                    sb.Append("<colgroup>");

                    sb.Append("<col style='width:20px;' />");
                    sb.Append("<col style='width:45px;' />");
                    if (b1024)
                    {
                        sb.Append("<col style='width:315px;' />");
                        sb.Append("<col style='width:85px;' />");
                        sb.Append("<col style='width:85px;' />");
                        sb.Append("<col style='width:65px;' />");
                    }
                    else
                    {
                        sb.Append("<col style='width:445px;' />");
                        sb.Append("<col style='width:100px;' />");
                        sb.Append("<col style='width:100px;' />");
                        sb.Append("<col style='width:75px;' />");
                    }

                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");
                    sb.Append("<col style='width:75px;' />");                   

                    sb.Append("</colgroup>");
                }

                sb.Append("</table>");
                sb.Append("@#@");

                if (sPE != "" && sDesPE == "")
                {
                    //obtener la descripcion del proyecto económico
                    try
                    {
                        SqlDataReader dr2 = PROYECTO.fgGetDatosProy(int.Parse(sPE));
                        sb.Append(dr2["t301_denominacion"].ToString());
                        dr2.Close();
                        dr2.Dispose();
                    }
                    catch (Exception ex)
                    {
                        sResul = "Error@#@" + ex.Message;
                        return sResul;
                    }

                }
                //        //sResul += "@#@" + sPE;
                sResul = "OK@#@" + sb.ToString() + "@#@" + sColumnasAE;
                sb.Length = 0; //Para liberar memoria     
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta", ex);
        }

        return sResul;
    }

}
