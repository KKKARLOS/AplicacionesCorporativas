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
    public string strTablaHTMLGrafico="", strTablaHTMLPropias = "", strTablaHTMLAjenas = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Registro de actividad";

                try
                {
                    string sAnoMes = Fechas.FechaAAnnomes(DateTime.Now).ToString();
                    obtenerGrafico(int.Parse(sAnoMes));

                    DateTime dtPrimer = Fechas.crearDateTime("01/"+ sAnoMes.Substring(4, 2)+ "/" +sAnoMes.Substring(0, 4));
                    DateTime dtUltimo = Fechas.getSigDiaUltMesCerrado(int.Parse(sAnoMes));
                    obtenerDatosPropios(dtPrimer, dtUltimo);
                    obtenerDatosAjenos(dtPrimer, dtUltimo);
                }
                catch (Exception ex)
                {
                    Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
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
            case ("buscar"):
                string sAnoMes = aArgs[1];
                DateTime dtPrimer = Fechas.crearDateTime("01/"+ sAnoMes.Substring(4, 2)+ "/" + sAnoMes.Substring(0, 4));
                DateTime dtUltimo = Fechas.getSigDiaUltMesCerrado(int.Parse(sAnoMes));
                sResultado += obtenerDatosPropios(dtPrimer, dtUltimo) + obtenerDatosAjenos(dtPrimer, dtUltimo);
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
    private string obtenerDatosPropios(DateTime dtPrimer, DateTime dtUltimo)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "", sFecAux;
        try
        {
            sb.Append("<table id='tblPropios' class='texto' style='WIDTH: 160px; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:100px;' />");//Fecha conexión
            sb.Append("<col style='width:60px;' />");//Usuario
            sb.Append("</colgroup>");
            //SqlDataReader dr = CONEXIONES.SelectPropias(null, int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
            SqlDataReader dr = CONEXIONES.SelectPropias(null, int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), dtPrimer, dtUltimo);

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;'>");
                sFecAux = dr["t459_fecconect"].ToString().Substring(0, 16);
                if (sFecAux.Substring(15, 1) == ":")
                    sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);
                sb.Append("<td style='padding-left:2px;'>" + sFecAux + "</td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t314_idusuario_conect"].ToString()).ToString("#,###") + "</td></tr>");
                //sb.Append("<td><nobr class='NBR W260'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            strTablaHTMLPropias= sb.ToString();
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener el registro de actividad propio.", ex);
        }
        return sResul;
    }
    private string obtenerDatosAjenos(DateTime dtPrimer, DateTime dtUltimo)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "", sFecAux;
        try
        {
            sb.Append("<table id='tblAjenos' class='texto' style='WIDTH: 695px; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:100px;' />");//Fecha de conexión
            sb.Append("<col style='width:595px;' />");//Profesional
            sb.Append("</colgroup>");
            SqlDataReader dr = CONEXIONES.SelectAjenas(null, (int)Session["UsuarioActual"], dtPrimer, dtUltimo);

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;'>");
                sFecAux = dr["t459_fecconect"].ToString().Substring(0, 16);
                if (sFecAux.Substring(15, 1) == ":")
                    sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);

                sb.Append("<td style='padding-left:2px;'>" + sFecAux + "</td>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            strTablaHTMLAjenas = sb.ToString();
            sResul = "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener el registro de actividad ajeno.", ex);
        }
        return sResul;
    }
    private void obtenerGrafico(int iAnoMes)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            string sAux, sAnoMesAux, sAnoMesIni, sAnoMesFin;
            int iAnoMesIni = Fechas.AddAnnomes(iAnoMes, -12), iPropias = 0, iAjenas = 0, nOcupacion = 0, nOcupacionMax = 0, iLargura = 0, iLarguraAjena = 0, iAnoMesAux, iAnoMesFin;

            sAnoMesIni = iAnoMesIni.ToString();
            DateTime dtPrimer = Fechas.crearDateTime("01/" + sAnoMesIni.Substring(4, 2) + "/" + sAnoMesIni.Substring(0, 4));
            iAnoMesFin = Fechas.AddAnnomes(iAnoMes, 1);
            sAnoMesFin = iAnoMesFin.ToString();
            DateTime dtUltimo = Fechas.crearDateTime("01/" + sAnoMesFin.Substring(4, 2) + "/" + sAnoMesFin.Substring(0, 4));

            sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 960px; table-layout:fixed;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:90px;'/>");
            sb.Append(" <col style=\"width:870px;;\" />");
            sb.Append("</colgroup>");
            sb.Append("<tr style=\"height:20px;cursor:default;\">");
            sb.Append("<td style='padding-left:3px; border-right-style:solid; border-right-width:2px; border-right-color: Black;'>&nbsp;</td>");
            sb.Append("<td>&nbsp;</td></tr>");

            #region Cargo array de 13 meses con el nº de conexiones propias por mes
            ArrayList slLista = new ArrayList();
            iAnoMesAux = iAnoMesIni;
            for (int iFila = 0; iFila < 13; iFila++)
            {
                sAux = Fechas.AnnomesAFechaDescLarga(iAnoMesAux);
                string[] aDatosAux = new string[] { sAux, "0", "0", iAnoMesAux.ToString() };
                slLista.Add(aDatosAux);
                iAnoMesAux = Fechas.AddAnnomes(iAnoMesAux, 1);
            }
            SqlDataReader dr = CONEXIONES.SelectPropiasMes(null, int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), dtPrimer, dtUltimo);
            while (dr.Read())
            {
                nOcupacion = int.Parse(dr["npropias"].ToString());
                if (nOcupacion > nOcupacionMax)
                    nOcupacionMax = nOcupacion;
                for (int iFila = 0; iFila < 13; iFila++)
                {
                    if (((string[])slLista[iFila])[3] == dr["anomes"].ToString())
                    {
                        ((string[])slLista[iFila])[1] = dr["npropias"].ToString();
                        break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion
            #region Cargo array de 13 meses con el nº de conexiones ajenas por mes
            SqlDataReader dr2 = CONEXIONES.SelectAjenasMes(null, (int)Session["UsuarioActual"], dtPrimer, dtUltimo);
            while (dr2.Read())
            {
                nOcupacion = int.Parse(dr2["najenas"].ToString());
                if (nOcupacion > nOcupacionMax)
                    nOcupacionMax = nOcupacion;
                for (int iFila = 0; iFila < 13; iFila++)
                {
                    if (((string[])slLista[iFila])[3] == dr2["anomes"].ToString())
                    {
                        ((string[])slLista[iFila])[2] = dr2["najenas"].ToString();
                        break;
                    }
                }
            }
            dr2.Close();
            dr2.Dispose();
            #endregion
            for (int iFila = 12; iFila >= 0; iFila--)
            {
                sAux = ((string[])slLista[iFila])[0];//Añomes en formato corto
                iPropias = int.Parse(((string[])slLista[iFila])[1]);
                iAjenas = int.Parse(((string[])slLista[iFila])[2]);
                sAnoMesAux = ((string[])slLista[iFila])[3];//Añomes en formato numerico

                nOcupacion=iPropias;
                //if (iAjenas > iPropias)
                //    nOcupacion = iAjenas;
                iLargura = flGetWidth(nOcupacion, nOcupacionMax);
                iLarguraAjena = flGetWidth(iAjenas, nOcupacionMax);
                sb.Append("<tr id=" + sAnoMesAux + " iPropias=" + iPropias.ToString() + " iAjenas=" + iAjenas.ToString() + " style=\"height:20px; background-color:White;\" onclick='buscar(this.id)'>");
                sb.Append("<td style='padding-left:3px; border-right-style:solid; border-right-width:2px; border-right-color: Black;'>" + sAux + "</td>");
                sb.Append("<td style=\"background-image:url('../../../Images/imgGanttBG175.gif')\">");
                if (iLargura != 0 || iLarguraAjena != 0)
                {
                    sb.Append("<span style='height:18px;noWrap:true;'>");
                    sb.Append("<img src='../../../Images/imgGanttR.gif' style='vertical-align:middle;height:12px;width:" + iLargura.ToString() + "px;margin:0px;border:0px;display:block;' />");
                    sb.Append("<img src='../../../Images/imgAR.gif' style='vertical-align:middle;height:5px;width:" + iLarguraAjena.ToString() + "px;margin:0px;border:0px;display:block;' />");
                    sb.Append("</span>");
                    sb.Append("<label style='margin-left:10px;color:red;vertical-align:top;margin-top:8px;font-weight:bold;'>" + iPropias.ToString() + "</label>");
                    sb.Append("<label style='margin-left:20px;vertical-align:top;margin-top:8px;font-weight:bold;'>" + iAjenas.ToString() + "</label>");
                }
                else
                    sb.Append("&nbsp;");
                sb.Append("</td></tr>");
            }
            sb.Append("<tr style=\"height:20px;\">");
            sb.Append("<td style='padding-left:3px; border-right-style:solid; border-right-width:2px; border-right-color: Black; border-top-style:solid; border-top-width:2px; border-top-color: Black;'>&nbsp;</td>");
            sb.Append("<td style=\" border-top-style:solid; border-top-width:2px; border-top-color: Black;\">&nbsp;</td></tr>");
            sb.Append("</table>");

            strTablaHTMLGrafico = sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener el registro de actividad propio.", ex);
        }
    }
    private int flGetWidth(double nOcupacion, double nOcupMax)
    {
        return System.Convert.ToInt32((nOcupMax == 0) ? 0 : 800 * nOcupacion / nOcupMax);
    }
    
}
