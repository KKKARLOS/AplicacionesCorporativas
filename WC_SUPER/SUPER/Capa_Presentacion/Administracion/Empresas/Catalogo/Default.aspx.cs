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
        Master.TituloPagina = "Mantenimiento de datos de la empresa";
        Master.bFuncionesLocales = true;

        try
        {
            if (!Page.IsCallback)
            {
                string strTabla0 = ObtenerEmpresas("1");
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
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "getDatos":
                sResultado += ObtenerEmpresas(aArgs[1]);
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
                EMPRESA.Delete(tr, int.Parse(aArgs[i].ToString()));
            }

            try
            {
                Conexion.CommitTransaccion(tr);

                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "N@#@" + Errores.mostrarError("Error al borrar los datos ( commit )", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }
        catch (System.Exception objError)
        {
            sResul = Errores.mostrarError("Error al borrar en la tabla de empresas.", objError);
            Conexion.CerrarTransaccion(tr);
            return "N@#@" + sResul;
        }

        oConn = null;
        tr = null;

        return sResul;
    }

    private string ObtenerEmpresas(string sActiva)
    {
        try
        {
            bool? bActiva;
            if (sActiva == "0") bActiva = null;
            else bActiva = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 640px;'  mantenimiento='0'>");
            sb.Append("<colgroup><col style='width:530px'/><col style='width:110px;'/></colgroup>");
            sb.Append("<tbody>");
            //SqlDataReader dr = EMPRESA.Catalogo(null, "", "", null, null, null, "", "", null, 2, 0);
            SqlDataReader dr = EMPRESA.Catalogo(bActiva);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T313_IDEMPRESA"].ToString() + "' onclick=\"mm(event)\" ondblclick=\"Detalle(this)\" style='height:16px;' >");
                
                if (bool.Parse(dr["t313_estado"].ToString())){
                    sb.Append("<td style='padding-left:5px;'>" + dr["T313_DENOMINACION"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t302_codigoexterno"].ToString() + "</td>");
                }
                else
                {
                    sb.Append("<td style='padding-left:5px;color:gray'>" + dr["T313_DENOMINACION"].ToString() + "</td>");
                    sb.Append("<td style='color:gray'>" + dr["t302_codigoexterno"].ToString() + "</td>");
                }

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
            return "Error@#@" + Errores.mostrarError("Error al obtener las empresas", ex);
        }
    }
}