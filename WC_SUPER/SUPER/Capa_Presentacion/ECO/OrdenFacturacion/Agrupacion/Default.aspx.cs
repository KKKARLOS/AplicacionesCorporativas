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


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public string strTablaHTML = "";
    public int nProy = 0;
    public SqlConnection oConn;
    public SqlTransaction tr;

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

                nProy = int.Parse(Request.QueryString["nProy"].ToString());
                hdnT301IdProy.Text = nProy.ToString();

                string[] aTabla = Regex.Split(getAgrupaciones(nProy.ToString()), "@#@");
                if (aTabla[0] == "OK")
                {
                    this.strTablaHTML = aTabla[1];
                }
                else sErrores += Errores.mostrarError(aTabla[1]);

            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos.", ex);
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
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getAgrupaciones"):
                sResultado += getAgrupaciones(aArgs[1]);
                break;
            case ("getProyectos"):
                sResultado += getProyectos(aArgs[1]);
                break;
            case ("setAgrupacion"):
                sResultado += setAgrupacion(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("delAgrupacion"):
                sResultado += delAgrupacion(aArgs[1]);
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

    private string getAgrupaciones(string sProyecto)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAgrupaciones' class='texto MA' style='width: 420px;'>");
            sb.Append("    <colgroup>");
            sb.Append("        <col style='width:50px;' />");
            sb.Append("        <col style='width:150px;' />");
            sb.Append("        <col style='width:220px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = AGRUPACIONORDEN.ObtenerCatalogo(null, int.Parse(sProyecto));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t622_idagrupacion"].ToString() + "' ");
                sb.Append("autor='" + dr["t314_idusuario_autor"].ToString() + "' ");
                sb.Append("onclick='ms(this);getProyectos(this)' onDblClick='aceptarClick(this);' style='height:20px'>");
                sb.Append("<td style='text-align:right; padding-right: 10px;'>" + ((int)dr["t622_idagrupacion"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W140'>" + dr["t622_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W210'>" + dr["Autor"].ToString() + "</nobr></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener las agrupaciones.", ex);
        }
    }
    private string getProyectos(string sAgrupacion)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblProyectos' class='texto' style='width: 420px;'>");
            sb.Append("    <colgroup>");
            sb.Append("        <col style='width:70px;' />");
            sb.Append("        <col style='width:350px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = AGRUPACIONPROYECTO.ObtenerCatalogo(int.Parse(sAgrupacion));

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px'>");
                sb.Append("<td style='text-align:right; padding-right: 10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["Responsable"].ToString() + "</nobr></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos que componen la agrupación.", ex);
        }
    }

    private string setAgrupacion(string sIdAgrupacion, string sDenominacion, string sProyectos)
    {
        string sResul = "";
        bool bErrorControlado = false;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
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
            string sAgrupacion = AGRUPACIONPROYECTO.ExisteMismosProyectos(tr, sProyectos.Replace(";", ","));
            if (sAgrupacion != "")
            {
                bErrorControlado = true;
                throw (new Exception("Ya existe una agrupación para los proyectos indicados (" + sAgrupacion + ")."));
            }

            int nIDAgrupacion = int.Parse(sIdAgrupacion);

            if (nIDAgrupacion == 0)
            {
                nIDAgrupacion = AGRUPACIONORDEN.Insert(tr, Utilidades.unescape(sDenominacion), (int)Session["UsuarioActual"]);
            }else
            {
                AGRUPACIONORDEN.Update(tr, nIDAgrupacion, Utilidades.unescape(sDenominacion));
                AGRUPACIONPROYECTO.DeleteByAgrupacion(tr, nIDAgrupacion);
            }

            AGRUPACIONPROYECTO.Insert(tr, nIDAgrupacion, sProyectos.Replace(";",","));

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) sResul = sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la agrupación", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string delAgrupacion(string sIdAgrupacion)
    {
        try
        {
            AGRUPACIONORDEN.Delete(null, int.Parse(sIdAgrupacion));

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al grabar los datos de la agrupación", ex);
        }
    }

}
