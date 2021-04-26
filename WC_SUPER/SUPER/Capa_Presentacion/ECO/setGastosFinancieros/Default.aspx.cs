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
    public string strTablaHTML;
    public string sErrores = "";
    public string sLectura = "false";
    public string sLecturaInsMes = "false";
    public string sAvanceProd = "";
    public string sMonedaProyecto = "", sMonedaImportes = "";

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

                if ((bool)Session["MODOLECTURA_PROYECTOSUBNODO"]) sLecturaInsMes = "true";

                #region Monedas y denominaciones
                sMonedaProyecto = Session["MONEDA_PROYECTOSUBNODO"].ToString();
                lblMonedaProyecto.InnerText = MONEDA.getDenominacion(Session["MONEDA_PROYECTOSUBNODO"].ToString());

                if (Session["MONEDA_VDP"] == null)
                {
                    sMonedaImportes = sMonedaProyecto;
                    lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(sMonedaImportes);
                }
                else
                {
                    sMonedaImportes = Session["MONEDA_VDP"].ToString();
                    lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(Session["MONEDA_VDP"].ToString());
                }
                #endregion

                //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    divMonedaImportes.Style.Add("visibility", "visible");

                string strTabla = getDatosGF(Request.QueryString["nSegMesProy"], Request.QueryString["sEstadoMes"], Request.QueryString["sEstadoProy"], sMonedaProyecto, sMonedaImportes);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.txtGastosFinancieros.Value = aTabla[1];
                else sErrores = aTabla[1];
            }
            catch (Exception ex)
            {
                this.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)//
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
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getDatosGF"):
                sResultado += getDatosGF(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
            case ("getMesesProy"):
                sResultado += getMesesProy(aArgs[1]);
                break;
            case ("addMesesProy"):
                sResultado += addMesesProy(aArgs[1], aArgs[2], aArgs[3]);
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

    public string getDatosGF(string sSegMesProy, string sEstadoMes, string sEstadoProy, string sMonedaProyecto2, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        //string sModoCoste = "";

        try
        {
            sLectura = "true";

            if (sMonedaProyecto2 != sMonedaImportes2)
            {
                sLectura = "true";
            }
            else
            {
                if (sEstadoProy == "A" || sEstadoProy == "P")// && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                {
                    if (sEstadoMes == "A" && SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        sLectura = "false";
                    }
                }
                else sLecturaInsMes = "true";
            }

            SEGMESPROYECTOSUBNODO oSegMes = SEGMESPROYECTOSUBNODO.Obtener(null, int.Parse(sSegMesProy), sMonedaImportes2);

            sb.Append(oSegMes.t325_gastosfinancieros.ToString("N"));

            return "OK@#@" + sb.ToString() + "@#@" + sLectura + "@#@" + MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los gastos financieros.", ex);
        }
    }
    private string getMesesProy(string sIDProySubnodo)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, int.Parse(sIDProySubnodo));

            while (dr.Read())
            {
                sb.Append(dr["t325_idsegmesproy"].ToString() + "##");
                sb.Append(dr["t325_anomes"].ToString() + "##");
                sb.Append(dr["t325_estado"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses del proyectosubnodo", ex);
        }
    }
    private string addMesesProy(string nIdProySubNodo, string sDesde, string sHasta)
    {
        return SEGMESPROYECTOSUBNODO.InsertarSegMesProy(nIdProySubNodo, sDesde, sHasta);
    }
    protected string Grabar(string sSegMesProy, string sGF)
    {
        string sResul = "";
        bool bErrorControlado = false;

        #region apertura de conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "SA")
            {
                SEGMESPROYECTOSUBNODO oSMPSN = SEGMESPROYECTOSUBNODO.Obtener(tr, int.Parse(sSegMesProy), null);
                if (oSMPSN.t325_estado == "C")
                {
                    bErrorControlado = true;
                    throw (new Exception("Durante su intervención en la pantalla, otro usuario ha cerrado el mes en curso."));
                }
            }
            SEGMESPROYECTOSUBNODO.UpdateGastosFinancieros(tr, int.Parse(sSegMesProy), decimal.Parse(sGF));
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los gastos financieros.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
}
