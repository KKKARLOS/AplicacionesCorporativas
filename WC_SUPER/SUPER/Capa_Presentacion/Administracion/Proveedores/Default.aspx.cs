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
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";
            Master.TituloPagina = "Mantenimiento de datos de proveedores";

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
            case ("Proveedor"):
                sResultado += ObtenerProveedores(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case "grabar":
                sResultado += Grabar(aArgs[1]);
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
    private string ObtenerProveedores(string sTipoBusqueda, string strDenominacion, string sActivo, string sCodigoExterno)
    {
        try
        {
            bool bActivo;

            if (sActivo == "1") bActivo = true;
            else bActivo = false;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' style='WIDTH: 546px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:80px;'>");
            sb.Append("    <col style='width:400px;'>");
            sb.Append("    <col style='width:66px;'>");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = PROVEEDOR.SelectByNombreHuecos(strDenominacion, (sCodigoExterno == "") ? null : sCodigoExterno, sTipoBusqueda, bActivo);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t315_idproveedor"].ToString()  + "' bd='' style='height:16px;' >");
                sb.Append("<td  style='width:80px;padding-left:3px;'>" + dr["t315_codigoexterno"].ToString() + "</td>");
                sb.Append("<td>" + dr["t315_denominacion"].ToString() + "</td>");
                sb.Append("<td align='center'>&nbsp;<INPUT  hideFocus class='check' onclick='javascript:ActualizaFila(this.parentNode.parentNode);' runat='server' type=checkbox ");

                if ((bool)dr["t315_controlhuecos"]) sb.Append("checked");

                sb.Append(" /></td>"); 
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los proveedores", ex);
        }
    }

    private string Grabar(string strDatos)
    {
        string sResul = "", sValoresInsertados = "";
        SqlConnection oConn = null;
        SqlTransaction tr;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
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
            string[] aDatos = Regex.Split(strDatos, "///");

            foreach (string oEstructura in aDatos)
            {
                if (oEstructura == "") continue;
                string[] aEstructura = Regex.Split(oEstructura, "##");

                ///aEstructura[0] = Opcion BD. "I", "U", "D"
                ///aEstructura[1] = ID Proveedor
                ///aEstructura[2] = Control de huecos

                switch (aEstructura[0])
                {
                    case "U":
                        PROVEEDOR.Upd_Control_Huecos(tr, int.Parse(aEstructura[1]), (aEstructura[2]=="1")?true:false);
                        break;
                    case "D":
                    case "I":
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sValoresInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (Errores.EsErrorIntegridad(ex)) sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al grabar los valores", ex, false); //ex.Message;
            else sResul = "Error@#@" + Errores.mostrarError("Error al grabar los valores", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
