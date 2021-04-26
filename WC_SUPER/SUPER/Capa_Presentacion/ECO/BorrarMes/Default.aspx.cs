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
    public string strTablaHTML = "";
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
                string strTabla = getDatos();
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error")
                    this.strTablaHTML = aTabla[1];
                else sErrores = aTabla[1];
            }
            catch (Exception ex)
            {
                this.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        //1� Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        string sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("procesar"):
                sResultado += Procesar(aArgs[1]);
                break;
            case ("getDatos"):
                sResultado += getDatos();
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
    public string getDatos()
    {
        string sMoneda = (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString();
        if (sMoneda == "") sMoneda = "EUR";
        int idPSN = int.Parse(Session["ID_PROYECTOSUBNODO"].ToString());

        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table class='texto' id='tblDatos' style='width: 600px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:40px;' />");
            sb.Append("<col style='width:160px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");


            SqlDataReader dr = SEGMESPROYECTOSUBNODO.ObtenerMesesAbiertosParaBorrado(null, idPSN, sMoneda);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t325_idsegmesproy"].ToString() + "' bd='' style='height:20px;'>");
                sb.Append("<td style='padding-left:5px; text-align:center;'><input type='checkbox' class='checkTabla' ></td>");//onclick='bCambios=true;'
                sb.Append("<td style='padding-left:5px;' >" + Fechas.AnnomesAFechaDescLarga((int)dr["t325_anomes"]) + "</td>");
                if (decimal.Parse(dr["Consumos"].ToString()) != 0) sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["Consumos"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right;'></td>");
                if (decimal.Parse(dr["Produccion"].ToString()) != 0) sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["Produccion"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right;'></td>");
                if (decimal.Parse(dr["Ingresos"].ToString()) != 0) sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["Ingresos"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right;'></td>");
                if (decimal.Parse(dr["Cobros"].ToString()) != 0) sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["Cobros"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right; padding-right:2px;'></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses abiertos", ex);
        }
    }

    protected string Procesar(string strDatos)
    {
        string sResul = "";

        #region apertura de conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion

        try
        {
            SEGMESPROYECTOSUBNODO.BorrarMesesAbiertos(tr, strDatos);

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar los meses abiertos indicados.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
