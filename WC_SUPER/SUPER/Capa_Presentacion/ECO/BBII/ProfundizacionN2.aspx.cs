using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_ECO_BBII_ProfundizacionN2 : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public StringBuilder sbN2 = null;
    public string sErrores = "";
    protected void Page_Load(object sender, EventArgs e)
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
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("getProfundizacionN2"):
                sResultado += ObtenerProfundizacionN2(aArgs[1], aArgs[2]);
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

 
    private string ObtenerProfundizacionN2(string sVista,
                       string sParametros)
    {
        try
        {
            decimal total = 0;
            string[] aParam = Regex.Split(sParametros, "{sepparam}");
            #region Lista de parámetros
            ///sVista == 1 //Ambito Económico
            ///aParam[0] = idPSN
            ///aParam[1] = anomes
            ///aParam[2] = formula
            ///aParam[3] = sAgrupado
            #endregion

            SqlDataReader dr = null;
            #region Creación del SqlDataReader
            //oDT1 = DateTime.Now;
            switch (sVista)
            {
                case "1": //Ambito Económico
                    dr = SUPER.DAL.PROYECTOSUBNODO.AnalisisEconomicoProfundizacionN2(null,
                                int.Parse(aParam[0]),
                                int.Parse(aParam[1]),
                                int.Parse(aParam[2]),
                                Session["MONEDA_VDC"].ToString(),
                                (aParam[3] == "1") ? true : false);
                    break;
            }
            // oDT2 = DateTime.Now;
            #endregion

            //StringBuilder sb = new StringBuilder();
            sbN2 = new StringBuilder();

            sbN2.Append("<table id='tblDatosProfN2' style='width:950px;' cellspadding='0' cellspacing='0' border='0'>");
            sbN2.Append("   <col style='width:80px;' />");
            sbN2.Append("   <col style='width:130px;' />");
            sbN2.Append("   <col style='width:130px;' />");
            sbN2.Append("   <col style='width:230px;' />");
            sbN2.Append("   <col style='width:280px;' />");
            sbN2.Append("   <col style='width:100px;' />");
            sbN2.Append("</colgroup>");

            while (dr.Read())
            {
                sbN2.Append("<tr ");
                sbN2.Append("idG=" + dr["t326_idgrupoeco"].ToString() + " ");
                sbN2.Append("idS=" + dr["t327_idsubgrupoeco"].ToString() + " ");
                sbN2.Append("idC=" + dr["t326_idgrupoeco"].ToString() + " ");
                sbN2.Append("idCL=" + dr["idclase"].ToString() + " ");
                sbN2.Append(">");
                sbN2.Append("<td><nobr class='NBR W80'>" + dr["t326_denominacion"].ToString() + "</nobr></td>");
                sbN2.Append("<td><nobr class='NBR W120'>" + dr["t327_denominacion"].ToString() + "</nobr></td>");
                sbN2.Append("<td><nobr class='NBR W120'>" + dr["t328_denominacion"].ToString() + "</nobr></td>");
                sbN2.Append("<td><nobr class='NBR W220'>" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                sbN2.Append("<td><nobr class='NBR W270'>" + dr["motivo"].ToString() + "</nobr></td>");
                sbN2.Append("<td style='text-align:right;'>" + decimal.Parse(dr["IMPORTE"].ToString()).ToString("#,##0.00") + " " + dr["signo"].ToString() + "</td>");
                sbN2.Append("</tr>");
                if (dr["signo"].ToString() == "(+)" && dr["IMPORTE"].ToString() != "")
                    total += decimal.Parse(dr["IMPORTE"].ToString());
                if (dr["signo"].ToString() == "(-)" && dr["IMPORTE"].ToString() != "")
                    total = total - decimal.Parse(dr["IMPORTE"].ToString());

            }
            dr.Close();
            dr.Dispose();
            sbN2.Append("</table>");

            return "OK@#@" + sbN2.ToString() + "@#@" + total.ToString("N") ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de la profundización de segundo nivel.", ex);
        }
    }
}