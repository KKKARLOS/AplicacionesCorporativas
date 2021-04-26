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
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos' class='texto MANO' style='WIDTH: 400px; table-layout:fixed; border-collapse: collapse;' cellSpacing='0' cellPadding='0' border='0'></table>";
    public string sErrores = "";
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
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
                // Leer Historico de monedas
                hdnID.Text = Utilidades.decodpar(Request.QueryString["id"].ToString());
                lblMoneda.InnerText = Utilidades.decodpar(Request.QueryString["Moneda"].ToString());

                if (DateTime.Now.Month == 1) hdnDesde.Text = ((DateTime.Now.Year - 1) * 100 + 1).ToString();
                else hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();

                if (DateTime.Now.Month == 1) txtDesde.Text = mes[0] + " " + (DateTime.Now.Year - 1).ToString();
                else txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();

                if (DateTime.Now.Month == 1) hdnHasta.Text = ((DateTime.Now.Year - 1) * 100 + 12).ToString();
                else hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month-1).ToString();

                //hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                //txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

                if (DateTime.Now.Month == 1) txtHasta.Text = mes[11] + " " + (DateTime.Now.Year - 1).ToString();
                else txtHasta.Text = mes[DateTime.Now.Month - 2] + " " + DateTime.Now.Year.ToString();

                string[] aTabla = Regex.Split(ObtenerHistoricoMoneda(), "@#@");
                if (aTabla[0] == "OK")
                {
                    strTablaHTML = aTabla[1];
                }
                else sErrores += Errores.mostrarError(aTabla[1]);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la moneda", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    private string ObtenerHistoricoMoneda()
    {
        string sAux = "";

        StringBuilder sb = new StringBuilder();

        try
        {

            sb.Append("<table id='tblDatos' style='WIDTH: 300px;' mantenimiento='1'>");
            sb.Append("<colgroup>");

            sb.Append("<col style='width:180px;' />");//Denominación
            sb.Append("<col style='width:120px;' />");//Tipo de cambio
            sb.Append("</colgroup>");
            sb.Append("<tbody>");


            SqlDataReader dr = TIPOCAMBIOMENSUAL.Catalogo(hdnID.Text);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t699_anomes"].ToString() + "' style='height:20px;cursor:pointer;' onclick='ms(this)' bd=''>");

                #region Creación tabla HTML

                sb.Append("<td style='text-align:center;'>" + Fechas.AnnomesAFechaDescLarga(int.Parse(dr["t699_anomes"].ToString())) + "</td>");

                sb.Append("<td style='text-align:left;'>");//CELL   2
                sAux = double.Parse(dr["t699_tipocambio"].ToString()).ToString("##,##0.0000");

                sb.Append("<input type='text' maxlength='9' style='width:80px' ");
                if ( int.Parse(dr["t699_anomes"].ToString()) >= int.Parse((DateTime.Now.Year * 100 + DateTime.Now.Month).ToString()))
                     sb.Append(" class='txtNumV' value=\"" + sAux + "\">");
                else 
                    sb.Append(" class='txtNumL' onfocus='fn(this,5, 4)' value=\"" + sAux + "\" onKeyUp='mod(this);'>");

                sb.Append("</td>");

                sb.Append("</tr>");

                #endregion
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el histórico de monedas", ex);
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
            case ("grabar"):
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
    protected string Grabar(string strDatos)
    {
        string sResul = "";

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

        try
        {
            string[] aTipoCambio = Regex.Split(strDatos, "///");
            foreach (string oTipoCambio in aTipoCambio)
            {
                if (oTipoCambio == "") continue;
                string[] aValores = Regex.Split(oTipoCambio, "##");

                //0. Opcion BD. "D"
                //1. ID anomes
                //2. Tipo de cambio

                switch (aValores[0])
                {
                    case "U":
                        TIPOCAMBIOMENSUAL.Update(tr, hdnID.Text, int.Parse(aValores[1]), decimal.Parse(aValores[2]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los tipos de cambio mensuales de las monedas.", ex) + "@#@";
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
}
