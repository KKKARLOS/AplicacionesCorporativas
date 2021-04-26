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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 0;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Mantenimiento de gestores de alertas";
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                tdNegocio.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2);
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
        
        //Session.Clear();
        //Session.Abandon();

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getGestores"):
                sResultado += GESTALERTAS.ObtenerGestoresAlertas();
                break;
            case "addGestor":
            case "delGestor":
                sResultado += GESTALERTAS.InsertarGestores(aArgs[1]);
                break;
            case ("getProfesionales"):
                sResultado += ObtenerProfesionales(Utilidades.unescape(aArgs[1].ToString()), Utilidades.unescape(aArgs[2].ToString()), Utilidades.unescape(aArgs[3].ToString()));
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

    private string ObtenerProfesionales(string strApellido1, string strApellido2, string strNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = Recurso.ObtenerProfesionales(strApellido1, strApellido2, strNombre);

            sb.Append("<table id='tblProfesionales' class='MANO' style='width:350px;'>");
            sb.Append("<colgroup><col style='width:20px;'/><col  style='width:330px;'/></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["num_empleado"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("onclick='mm(event);'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W310'>" + dr["TECNICO"].ToString() + "</nobr></td>" + (char)13);
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
            return "Error@#@" + Errores.mostrarError("Error al cargar los profesionales", ex);
        }
    }


}
