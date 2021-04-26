using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_ECO_Escenarios_ProfesionalEscenario_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nEscenario = -1;
    public string sErrores = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (Request.QueryString["ie"] != null)
                    nEscenario = int.Parse(Utilidades.decodpar(Request.QueryString["ie"].ToString()));

                //rdbAmbito.Items[1].Text = Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;&nbsp;&nbsp;";
                //this.lblCR.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                //this.lblCR.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                //hdnNodoActual.Value = Request.QueryString["idNodo"].ToString();
                //hdnDesNodoActual.Value = Request.QueryString["desNodo"].ToString();
                //this.hdnCualidad.Value = Request.QueryString["Cual"].ToString();

                //PROYECTO oProy = PROYECTO.Obtener(null, int.Parse(Request.QueryString["nProyecto"].ToString()));
                //hdnEsReplicable.Value = (oProy.t301_esreplicable) ? "1" : "0";
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("tecnicos"):
                sResultado += ObtenerProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
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

    protected string ObtenerProfesionales(string strOpcion, string strValor1, string strValor2, string strValor3, string sModeloCoste, string sCualidad, string sNodoPSN, string sPSN)
    {
        string sResul = "", sV1, sV2, sV3;
        int nNodo = 0;
        StringBuilder sb = new StringBuilder();
        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);
            if (strOpcion == "C") nNodo = int.Parse(sV1);
            //En proyectos replicados con gestión deben aparecer solo los profesionales externos y los del nodo del proyecto replicado
            if (sCualidad == "P") nNodo = int.Parse(sNodoPSN);

            SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesCoste(strOpcion, sV1, sV2, sV3, nNodo, sModeloCoste, sCualidad, int.Parse(sPSN), int.Parse(sNodoPSN));

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:327px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("idCal='" + dr["t066_idcal"].ToString() + "' ");
                sb.Append("nodo='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("costecon='" + dr["costecon"].ToString() + "' ");
                sb.Append("costerep='" + dr["costerep"].ToString() + "' ");
                sb.Append("sexo='" + dr["sexo"].ToString() + "' ");

                if (dr["tipo"].ToString() == "F") sb.Append("tipo='F' ");
                else
                {
                    if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    else if (dr["t303_idnodo"].ToString() == sNodoPSN) sb.Append("tipo='P' ");
                    else sb.Append("tipo='N' ");
                }

                sb.Append("descal=\"" + Utilidades.escape(dr["t066_descal"].ToString()) + "\" ");
                sb.Append("desnodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("desempresa=\"" + Utilidades.escape(dr["empresa"].ToString()) + "\" ");
                sb.Append("alta='" + dr["t314_falta"].ToString().Substring(0,10) + "' ");

                sb.Append("onclick='mm(event)' ondblclick='insertarRecurso(this, 1)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W320' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }

}
