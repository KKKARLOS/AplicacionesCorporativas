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

public partial class Capa_Presentacion_Administracion_EstructuraNat_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nNE = 0;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.nBotonera = 35;
        Master.bFuncionesLocales = true;
        Master.TituloPagina = "Estructura tipología-naturaleza";

        try
        {
            if (!Page.IsCallback)
            {
                nNE = int.Parse(Request.QueryString["nNE"].ToString());
                string strTabla = GenerarArbol(false, nNE);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") divCatalogo.InnerHtml = aTabla[1];
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
        switch (aArgs[0])
        {
            case "getEstructura":
                sResultado += GenerarArbol((aArgs[1] == "1") ? true : false, int.Parse(aArgs[2]));
                break;
            case "eliminar":
                sResultado += Eliminar(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
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

    private string GenerarArbol(bool bMostrarInactivos, int nNivelExp)
    {
        try
        {
            SqlDataReader dr = NATURALEZA.GetEstructura(bMostrarInactivos);
            StringBuilder sb = new StringBuilder();
            string sColor = "black";
            sb.Append("<TABLE class='texto' id=tblDatos style='WIDTH: 500px; margin-top:3px;cursor:url(../../../images/imgManoAzul2.cur),pointer;'>");

            //sb.Append("<TABLE class='texto MA' id=tblDatos style='WIDTH: 500px; margin-top:3px;'>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sColor = "black";
                if ((int)dr["ESTADO"]==0) sColor = "gray";
                if ((int)dr["INDENTACION"] <= nNivelExp)
                {
                    sb.Append("<tr id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "' ");
                    sb.Append(" style='display:table-row; height:20px; vertical-align:middle;' nivel=" + dr["INDENTACION"].ToString() + " ondblclick='mdn(this)' >");

                    if ((int)dr["INDENTACION"] < 4)
                    {
                        if ((int)dr["INDENTACION"] < nNivelExp) 
                            sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../images/minus.gif' style='cursor:pointer;'>");
                        else 
                            sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                    }
                    else 
                        sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' src='../../../images/imgSeparador.gif'>");
                }
                else
                {
                    sb.Append("<tr id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "' ");
                    sb.Append(" style='display:none; height:20px; vertical-align:middle;' nivel=" + dr["INDENTACION"].ToString() + " ondblclick='mdn(this)' >");
                    sb.Append("<td>");
                    if ((int)dr["INDENTACION"] < 4) 
                        sb.Append("<IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                    else 
                        sb.Append("<IMG class='N" + dr["INDENTACION"].ToString() + "' src='../../../images/imgSeparador.gif'>");
                }
                switch ((int)dr["INDENTACION"])
                {
                    case 1:
                        sb.Append("<IMG src='../../../images/imgTipologia.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case 2:
                        sb.Append("<IMG src='../../../images/imgGrupo.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case 3:
                        sb.Append("<IMG src='../../../images/imgSubgrupo.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case 4:
                        sb.Append("<IMG src='../../../images/imgNaturaleza.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                }
                sb.Append("<label class='texto' onclick='marcarLabel(this)' style='cursor:url(../../../images/imgManoAzul2.cur),pointer;color:" + sColor + "'>" + dr["DENOMINACION"].ToString() + "</label></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener la estructura tipología-naturaleza", ex);
        }
    }
    private string Eliminar(int nNivel, int nIDItem)
    {
        try
        {
            switch (nNivel)
            {
                case 1:
                    TIPOLOGIAPROY.Delete(null, (byte)nIDItem); 
                    break;
                case 2: 
                    GRUPONAT.Delete(null, nIDItem); 
                    break;
                case 3: 
                    SUBGRUPONAT.Delete(null, nIDItem); 
                    break;
                case 4: 
                    NATURALEZA.Delete(null, nIDItem); 
                    break;
            }

            return "OK@#@";
        }
        catch (Exception ex)
        {
            //return "Error@#@" + Errores.mostrarError("Error al obtener la estructura organizativa", ex);
            if (Errores.EsErrorIntegridad(ex)) return "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al obtener la estructura tipología-naturaleza", ex, false) +"@#@"+ Errores.CampoResponsableIntegridad(ex); //ex.Message;
            else return "Error@#@" + Errores.mostrarError("Error al obtener la estructura tipología-naturaleza", ex);
        }
    }

}
