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
            Master.sbotonesOpcionOn = "7,75,90";            
            Master.TituloPagina = "Mantenimiento de datos de contratos";
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
            case ("NumContrato"):
                sResultado += ObtenerContrato(int.Parse(aArgs[1]));
                break;
            case ("contrato"):
                sResultado += ObtenerContratos(aArgs[1], Utilidades.unescape(aArgs[2]));
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
        //Se envía el resultado al     cliente.
        return _callbackResultado;
    }
    private string Eliminar(string args)
    {
        string sResul = "";
        string[] aArgs = Regex.Split(args, "///");
        SqlDataReader dr;

        try
        {
            
            //comprobaciones previas

            for (int i = 0; i < aArgs.Length; i++)
            {
                string[] aDatos = Regex.Split(aArgs[i].ToString(), "##");
                dr = CONTRATO.ComprobarContratoProyecto(int.Parse(aDatos[0].ToString()));
                dr.Read();
                if (int.Parse(dr["numero"].ToString()) != 0)
                {
                    //sResul += "El contrato " + int.Parse(aArgs[i].ToString()).ToString("#,###,###") + " no se puede borrar pues tiene proyectos vinculados.\n";
                    sResul += "- " + Utilidades.unescape(aDatos[1]).ToString() + ".\n";
                }
                dr.Close();
                dr.Dispose();
            }

            if (sResul != "")
            {
                sResul = "Los siguientes contratos no se pueden borrar pues tienen proyectos vinculados:\n\n" + sResul;
                return "N@#@" + sResul;
            }
        }
        catch (Exception ex)
        {
            sResul = "N@#@" + Errores.mostrarError("Error al validar el contrato", ex);
            return sResul;
        }
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
            //string[] aArgs = Regex.Split(args, "///");
            for (int i = 0; i < aArgs.Length; i++)
            {
                CONTRATO.Delete(tr, int.Parse(aArgs[i].ToString()));
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
            sResul = Errores.mostrarError("Error al borrar en la tabla de divisiones horizontales.", objError);
            Conexion.CerrarTransaccion(tr);
            return "N@#@" + sResul;
        }

        oConn = null;
        tr = null;

        return sResul;
    }
    private string ObtenerContrato(int idContrato)
    {
        try
        {
            StringBuilder sb = new StringBuilder();           

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 550px;'>");
            sb.Append("<colgroup><col style='width:550px;'></colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = CONTRATO.ObtenerExtensionPadre(idContrato);

            if (dr.Read())
            {
                sb.Append("<tr id='" + dr["t306_idcontrato"].ToString() + "' onclick=\"mm(event)\" ondblclick=\"Detalle(this)\" style='height:16px;' >");
                sb.Append("<td style='padding-left:5px;'>" + dr["t377_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el Contrato.", ex);
        }
    }
    private string ObtenerContratos(string sTipoBusqueda, string strCadena)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 550px;'>");
            sb.Append("<tbody>");
            SqlDataReader dr = CONTRATO.CatalogoDenominacion(strCadena, sTipoBusqueda, null);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' onclick=\"mm(event)\" ondblclick=\"Detalle(this)\" style='height:16px;' >");
                sb.Append("<td style='padding-left:5px;'>" + dr["DENOMINACION"].ToString() + "</td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los contratos", ex);
        }
    }
}
