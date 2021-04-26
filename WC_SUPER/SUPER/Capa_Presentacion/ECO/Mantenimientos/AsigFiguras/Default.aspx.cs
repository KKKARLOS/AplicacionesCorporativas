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
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Administracion_EstructuraOrg_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nNivel = 0;
    public string strTablaHTML = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.sbotonesOpcionOn = "71";
        Master.bFuncionesLocales = true;
        Master.TituloPagina = "Asignación de figuras";
        Master.FuncionesJavaScript.Add("Javascript/boxover.js");

        try
        {
            if (!Page.IsCallback)
            {
                nNivel = int.Parse(Request.QueryString["nNivel"].ToString());

                switch (nNivel)
                {
                    case 1: //SUPERNODO4
                        this.lblNivel.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4);
                        break;
                    case 2: //SUPERNODO3
                        this.lblNivel.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3);
                        break;
                    case 3: //SUPERNODO2
                        this.lblNivel.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2);
                        break;
                    case 4: //SUPERNODO1
                        this.lblNivel.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1);
                        break;
                    case 5: //NODO
                        this.lblNivel.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                        break;
                    case 6: //SUBNODO
                        this.lblNivel.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO);
                        break;
                }

                string strTabla = CargarCatalogo(nNivel);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                else Master.sErrores = aTabla[1];
            }
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

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        //switch (aArgs[0])
        //{
        //    case "getEstructura":
        //        sResultado += GenerarArbol((aArgs[1] == "1") ? true : false, int.Parse(aArgs[2]));
        //        break;
        //    case "eliminar":
        //        sResultado += Eliminar(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
        //        break;
                
        //}

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string CargarCatalogo(int nNivel)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = FIGURA.getCatalogoAsigFiguras(null, (int)Session["UsuarioActual"], nNivel);
            string sTootTip = "";

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 500px;'>");
            //sb.Append("<colgroup><col style='padding-left:5px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                switch(nNivel){
                    case 1:
                        sTootTip = "";
                        break;
                    case 2:
                        sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString();
                        break;
                    case 3:
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString();
                        break;
                    case 4:
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString();
                        break;
                    case 5:
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString();
                        break;
                    case 6:
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["DES_NODO"].ToString();



                        break;
                }
 
                sb.Append("<tr id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "' ");
                sb.Append("onclick='ms(this)' ondblclick='mdn(this)' nivel=" + dr["INDENTACION"].ToString() +" ");
                if (nNivel > 1) sb.Append("style='height:20px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");
                else sb.Append("style='height:20px;'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tarifas", ex);
        }
    }

}
