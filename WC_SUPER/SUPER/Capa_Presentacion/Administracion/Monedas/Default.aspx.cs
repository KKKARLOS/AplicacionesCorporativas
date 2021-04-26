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
using EO.Web;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public SqlConnection oConn;
    public SqlTransaction tr;

    private void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Monedas";
                Master.sbotonesOpcionOn = "4,19";
                Master.sbotonesOpcionOff = "4";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                hdnFechaAnoMes.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();

                PARAMETRIZACIONSUPER oPS = PARAMETRIZACIONSUPER.Select(null);
                cboActualizacionTCA.SelectedValue = oPS.t725_modotipocambioBCE.ToString();

                string[] aTabla = Regex.Split(ObtenerMonedas("1"), "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("monedas"):
                sResultado += ObtenerMonedas(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("setTCA"):
                sResultado += setTCA(aArgs[1]);
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
            string[] aMoneda = Regex.Split(strDatos, "///");
            foreach (string oMoneda in aMoneda)
            {
                if (oMoneda == "") continue;
                string[] aValores = Regex.Split(oMoneda, "##");

                //0. Opcion BD. "D"
                //1. ID Moneda
                //2. Denominación importes
                //3. Cambio actual
                //4. Cambio siguiente
                //5. A partir de
                //6. Gestión - estado
                //7. Visibilidad - estado

                switch (aValores[0])
                {

                    case "U":
                        MONEDA.Update(tr, aValores[1], "", (aValores[6] == "1") ? true : false, (aValores[3] == "") ? null : (decimal?)decimal.Parse(aValores[3]), (aValores[4] == "") ? null : (decimal?)decimal.Parse(aValores[4]), (aValores[5] == "") ? null : (int?)int.Parse(aValores[5]), Utilidades.unescape(aValores[2]), (aValores[7] == "1") ? true : false);
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
            if (HttpContext.Current.Cache["Lista_Monedas"] != null){
                HttpContext.Current.Cache.Remove("Lista_Monedas");
            }
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las monedas.", ex) + "@#@";
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    //private string ObtenerMonedas(Nullable<bool> t422_estadovisibilidad)
    private string ObtenerMonedas(string t422_estadovisibilidad)
    {
        string sFecha, sAux = "";

        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append(@"<table id='tblDatos' style='width:970px; text-align:left;' mantenimiento='1'>
                    <colgroup>
                        <col style='width:200px;' />
                        <col style='width:230px;' />
                        <col style='width:70px;' />
                        <col style='width:70px;' />
                        <col style='width:120px;' />
                        <col style='width:70px;' />
                        <col style='width:70px;' />
                        <col style='width:70px;' />
                        <col style='width:70px;' />
                    </colgroup>
                <tbody>");

            //DataTable dt = MONEDA.ObtenerTipoCambioBCE();
            SqlDataReader dr = MONEDA.CatalogoMan((t422_estadovisibilidad == "1") ? true : false);

            while (dr.Read())
            {
                //string sCodEstado = dr["estado"].ToString();

                sb.Append("<tr id='" + dr["t422_idmoneda"].ToString() + "' ");
                if (dr["t422_idmoneda"].ToString() != "EUR")
                    sb.Append(" onclick='ms(this)' ");
                sFecha = "";
                if (dr["t422_anomessiguiente"] == System.DBNull.Value) sFecha = "";
                else sFecha = dr["t422_anomessiguiente"].ToString();

                sb.Append("fecha='" + sFecha + "' ");
                sb.Append("bd=''>");

                #region Creación tabla HTML

                sb.Append("<td>");// Denominación
                sb.Append("<nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["t422_denominacion"].ToString() + "</nobr>");
                sb.Append("</td>");

                sb.Append("<td>");//Ver importes en
                if (dr["t422_idmoneda"].ToString() == "EUR")
                    sb.Append("<nobr class='NBR W220' onmouseover='TTip(event)' style='margin-left:2px;'>" + dr["t422_denominacionimportes"].ToString() + "</nobr>");
                else
                    sb.Append("<input type='text' maxlength='50' class='txtL' style='width:220px;' value='" + dr["t422_denominacionimportes"].ToString() + " ' onKeyUp='mod(this);' />");
                sb.Append("</td>");

                //sb.Append("<td>");//Actual
                sAux = "";
                if (dr["t422_tipocambio"] == System.DBNull.Value) sAux = "";
                else sAux = double.Parse(dr["t422_tipocambio"].ToString()).ToString("##,##0.0000");

                if (dr["t422_idmoneda"].ToString() == "EUR")
                {
                    sb.Append("<td style='text-align:right;'>");//Actual
                    sb.Append("<nobr class='NBR W60' style='margin-right:3px;'>" + sAux + "</nobr>");
                }
                else
                {
                    sb.Append("<td style='text-align:right;'>");//Actual
                    sb.Append("<input type='text' maxlength='9' class='txtNumL' onfocus='fn(this,5, 4)' style='width:60px' value=\"" + sAux + "\" onKeyUp='mod(this);'>");
                }
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");//Siguiente
                sAux = "";
                if (dr["t422_tipocambiosiguiente"] == System.DBNull.Value) sAux = "";
                else sAux = double.Parse(dr["t422_tipocambiosiguiente"].ToString()).ToString("##,##0.0000");

                if (dr["t422_idmoneda"].ToString() == "EUR")
                    sb.Append("<nobr class='NBR W60'>" + sAux + "</nobr>");
                else
                    sb.Append("<input type='text' maxlength='9' class='txtNumL' onfocus='fn(this,5, 4)' style='width:60px' value=\"" + sAux + "\" onKeyUp='mod(this);'>");
                sb.Append("</td>");

                sb.Append("<td title='Mes y año'>"); //A partir de

                sFecha = "";
                if (dr["t422_anomessiguiente"] == System.DBNull.Value) sFecha = "";
                else sFecha = Fechas.AnnomesAFechaDescLarga(int.Parse(dr["t422_anomessiguiente"].ToString()));

                if (dr["t422_idmoneda"].ToString() == "EUR")
                    sb.Append("<nobr class='NBR W90'>" + sFecha + "</nobr>");
                else
                    sb.Append("<input type='text' class='txtFecL' style='width:90px;text-align:center;' value='" + sFecha + "' readonly onclick='getMesValor(this)' />");

                sb.Append("<image style='cursor:pointer;width:16px;visibility:");
                if (sFecha != "") sb.Append("visible;");
                else sb.Append("hidden;");
                sb.Append("vertical-align:middle;' src='../../../images/imgBorrar.gif' onclick='borrarFecha(this)' style='cursor:pointer'></image>");
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");//TCOD
                sb.Append((dr["tipocambio_bce_diario"] != DBNull.Value) ? double.Parse(dr["tipocambio_bce_diario"].ToString()).ToString("##,##0.0000") : "");
                sb.Append("</td>");

                sb.Append("<td style='text-align:right;'>");//TCOM
                sb.Append((dr["tipocambio_bce_medio_mensual"] != DBNull.Value) ? double.Parse(dr["tipocambio_bce_medio_mensual"].ToString()).ToString("##,##0.0000") : "");
                sb.Append("</td>");

                sb.Append("<td style='text-align:center;'>"); //Gestión
                if (dr["t422_idmoneda"].ToString() != "EUR")
                {
                    sb.Append("<input type='checkbox' style='width:15px; cursor:pointer'");
                    sb.Append(" id='chkGestion' class='checkTabla'");
                    if ((bool)dr["t422_estado"]) sb.Append(" checked");
                    sb.Append(" onclick=\"cont(this);\">");
                }
                sb.Append("</td>");

                sb.Append("<td style='text-align:center;'>"); //Visibilidad
                if (dr["t422_idmoneda"].ToString() != "EUR")
                {
                    sb.Append("<input type='checkbox' style='width:15px; cursor:pointer'");
                    sb.Append(" id='chkVisibilidad' class='checkTabla'");
                    if ((bool)dr["t422_estadovisibilidad"]) sb.Append(" checked");
                    sb.Append(" onclick=\"cont(this);\">");
                }
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
            return "Error@#@" + Errores.mostrarError("Error al obtener las monedas", ex);
        }
    }
    protected string setTCA(string sModoActualizacion)
    {
        try
        {
            MONEDA.ActualizarModoTipoCambioBCE(null, byte.Parse(sModoActualizacion));
            return "OK";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer el sistema de actualización.", ex) + "@#@";
        }
    }



}

