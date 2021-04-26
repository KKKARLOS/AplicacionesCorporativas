using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using SUPER.Capa_Negocio;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public partial class Parametrizacion : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtmlTablas;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Parametrización";
            Master.bFuncionesLocales = true;
            Master.nBotonera = 9;
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

            try
            {
                PARAMETRIZACIONSUPER oPar = PARAMETRIZACIONSUPER.Select(null);
                txtTolerancia.Value = oPar.t725_toleranciapasohistorico.ToString("N");
                txtHistorico.Value = oPar.t725_mesespasohistorico.ToString();
                txtProduccionCVT.Value = oPar.t725_produccionCVT.ToString("N");

                chkAccesoAu.Checked = oPar.t725_accesoauditoria;
                chkAlertas.Checked = oPar.t725_alertasproy_activas;
                chkMailCIA.Checked = oPar.t725_correoCIAactivo;
                this.chkForaneo.Checked = oPar.t725_foraneos;
                txtDiasAvisoVto.Value = oPar.t725_diasavisovencim.ToString("#,###");

                //if (oPar.t725_auditgeneral)
                //    chkGeneral.Checked = true;
                //else
                //    chkGeneral.Checked = false;

                if (oPar.t725_auditgeneral) rdbEstado.SelectedValue = "1";
                else rdbEstado.SelectedValue = "0";

                string strTabla0 = obtenerTablas();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] == "OK") strTablaHtmlTablas = aTabla0[1];
                else Master.sErrores = aTabla0[1];

                //if (!User.IsInRole("DIS"))
                //{
                //    divGeneral.Style.Add("display", "none");
                //}
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos de la parametrización.", ex);
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
            case ("grabar"):
                try
                {
                    sResultado += "OK@#@" + grabar(aArgs[1], aArgs[2]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar", ex);
                }
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

    public string obtenerTablas()
    {

        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = TABLASAUDITSUPER.CatalogoTablas();
            sb.Append("<table id='tblTablas' style='width:390px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:15px;' />");
            sb.Append(" <col style='width:320px;' />");
            sb.Append(" <col style='width:30px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t300_tabla"].ToString() + "' bd='' style='height:20px;' onclick='mm(event)'>");
                sb.Append(" <td style='padding-left:5px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append(" <td><nobr class='NBR' style='width:300px'>" + dr["t300_tabladenusuario"].ToString() + "</nobr></td>");
                sb.Append(" <td style='padding-left:5px;'><input type='checkbox' style='width:15px;' class='check' onclick='activarGrabar();fm(event);' ");
                if (bool.Parse(dr["t300_auditar"].ToString())) sb.Append("checked=true");
                sb.Append("></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el catálogo de tablas", ex);
        }
    }

    public string grabar(string sParametrizacion, string sTabla)
    {
        string sResul = "";
        SqlConnection oConn = null;
        SqlTransaction tr = null;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            throw (new Exception("Error al abrir la conexión."));
        }
        #endregion

        try
        {
            if (sParametrizacion != "")
            {

                //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aDatos = Regex.Split(sParametrizacion, "#sCad#");
                Session["OCULTAR_AUDITORIA"] = aDatos[2];
                Session["ALERTASPROY_ACTIVAS"] = (aDatos[5] == "1") ? true : false;
                Session["FORANEOS"] = (aDatos[6] == "1") ? true : false;
                PARAMETRIZACIONSUPER.Update(tr,
                                            (aDatos[0] == "") ? 0 : decimal.Parse(aDatos[0]),
                                            byte.Parse(aDatos[1]),
                                            (aDatos[2] == "1") ? true : false,
                                            (aDatos[3] == "1") ? true : false,
                                            (aDatos[4] == "") ? 0 : decimal.Parse(aDatos[4]),
                                            (aDatos[5] == "1") ? true : false,
                                            (aDatos[6] == "1") ? true : false,//Mail CIA
                                            (aDatos[7] == "1") ? true : false,
                                            (aDatos[8] == "") ? 0 : int.Parse(aDatos[7])
                                            );

            }
            if (sTabla != "")
            {
                //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aDatos2 = Regex.Split(sTabla, "#sFin#");
                for (int i = 0; i <= aDatos2.Length - 1; i++)
                {
                    string[] aElem = Regex.Split(aDatos2[i], "#sCad#");
                    TABLASAUDITSUPER.Update(tr,
                                            aElem[0],
                                            (aElem[1] == "1") ? true : false);

                }
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar la parametrización.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
            if (sResul != "")
                throw (new Exception(sResul));
        }
        return sResul;
    }

}
