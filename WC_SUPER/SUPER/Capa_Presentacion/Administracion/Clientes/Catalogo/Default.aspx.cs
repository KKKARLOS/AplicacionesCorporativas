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

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    //public string strTablaHTML;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Mantenimiento de datos de clientes";
            Master.bFuncionesLocales = true;

            try
            {
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
            case ("cliente"):
                sResultado += ObtenerClientes(aArgs[1], aArgs[2]);
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
    private string ObtenerClientes(string sTipoBusqueda, string strCli)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style=\"WIDTH: 550px; CURSOR:url('../../../../images/imgManoAzul2.cur'),pointer;\">");
            sb.Append("<colgroup><col style='width:465px;' /><col style='width:85px;' /></colgroup>");
            SqlDataReader dr = CLIENTE.SelectByNombre(strCli, sTipoBusqueda, false, false, null);

            while (dr.Read())
            {
                //sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' onclick='ms(this)' ondblclick='Detalle(this)' style='height:16px;' >");
                ////sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' onmousedown='eventos(this);' style='height:16px;' >");
                ////sb.Append("<td style='padding-left:15px'>" + dr["t302_denominacion"].ToString() + "</td>");
                //sb.Append("<td style='padding-left:15px'><nobr class='NBR' onmouseover='TTip(event)' style='width:540px;'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                //sb.Append("</tr>");

                sb.Append("<tr id='" + dr["tipo"].ToString() + "_" + dr["t302_idcliente"].ToString() + "' idcliente='" + dr["t302_idcliente"].ToString() + "' ");
                sb.Append(" onclick=\"ms(this)\" ondblclick=\"Detalle(this)\" style='height:16px;");
                if ((bool)dr["t302_estado"])
                    sb.Append("'");
                else
                    sb.Append("color:gray;'");

                sb.Append("><td style='padding-left:5px;'><img src='../../../../images/img" + dr["tipo"].ToString() + ".gif' ");
                if (dr["tipo"].ToString() == "M") sb.Append("style='margin-right:5px;'");
                else sb.Append("style='margin-left:15px;margin-right:5px;'");
                sb.Append("><nobr class='NBR W400'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t302_codigoexterno"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los clientes", ex);
        }
    }
}
