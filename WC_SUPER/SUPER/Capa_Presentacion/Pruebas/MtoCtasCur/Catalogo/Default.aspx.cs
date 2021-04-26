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
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHtml;
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.nBotonera = 51;
        Master.TituloPagina = "Mantenimiento de datos de Cuentas";
        Master.bFuncionesLocales = true;

        try
        {
            if (!Page.IsCallback)
            {
                string strTabla0 = ObtenerCuentas();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] == "OK") strTablaHtml = aTabla0[1];
                else Master.sErrores = aTabla0[1];
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }

        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@";

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "getDatos":
                sResultado += ObtenerCuentas();
                break;
            case "eliminar":
                sResultado += Eliminar(aArgs[1]);
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

    private string Eliminar(string args)
    {
        string sResul = "";
        #region Conexion
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] aArgs = Regex.Split(args, "///");
            for (int i = 0; i < aArgs.Length; i++)
            {
                SUPER.DAL.CUENTASCUR.Delete(tr, int.Parse(aArgs[i]));
            }

            try
            {
                Conexion.CommitTransaccion(tr);

                sResul =  "OK@#@";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al borrar los datos ( commit )", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }
        catch (System.Exception objError)
        {
            sResul = Errores.mostrarError("Error al borrar en la tabla de divisiones horizontales.", objError);
            Conexion.CerrarTransaccion(tr);
            return "Error@#@" + sResul;
        }

        oConn = null;
        tr = null;

        return sResul;
    }

    private string ObtenerCuentas()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = SUPER.DAL.CUENTASCUR.Catalogo("");

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px;'>");
            sb.Append("<colgroup><col style='width:300px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["cu_id"].ToString() + "' bd='' onclick='mm(event)' ondblclick='Detalle(this);' style='height:20px'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W300' onmouseover='TTip(event)'>" + dr["cu_nombre"].ToString() + "</nobr></td>");
                sb.Append("<td align='right'>" + decimal.Parse(dr["cu_vn"].ToString()).ToString("N") + "</td>");
                sb.Append("<td align='center'>");
                if ((bool)dr["cu_escliente"])
                    sb.Append("<img src='../../../../images/imgOk.gif' />");
                else
                    sb.Append("<img src='../../../../images/imgSeparador.gif' />");

                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");


            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            //border:solid 1px #317082;
            return "Error@#@" + Errores.mostrarError("Error al obtener las divisiones horizontales", ex);
        }
    }
}