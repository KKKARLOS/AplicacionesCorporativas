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
    public string sLectura = "false", sLecturaInsMes = "false", sHayConsumos = "false";
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

                if (Session["OCULTAR_AUDITORIA"].ToString() == "1")
                {
                    this.cldAuditoria.Visible = false;
                    this.btnAuditoria.Visible = false;
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

                string strTabla = getDatosNivel(Request.QueryString["nSegMesProy"], Request.QueryString["sEstadoMes"], Request.QueryString["sModeloCoste"], Request.QueryString["nIdNodo"], Request.QueryString["sEstadoProy"], sMonedaProyecto, sMonedaImportes);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
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

    public void RaiseCallbackEvent(string eventArg)
    {
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        string sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getDatosNivel"):
                sResultado += getDatosNivel(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
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

    public string getDatosNivel(string sSegMesProy, string sEstadoMes, string sModeloCoste, string sIdNodo, string sEstadoProy, string sMonedaProyecto2, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            sLectura = "false";

            sb.Append("<table id=tblDatos style='width: 960px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:10px;' />");
            sb.Append("<col style='width:600px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            if (sMonedaProyecto2 != sMonedaImportes2)
            {
                sLectura = "true";
            }
            else
            {
                if (sEstadoProy == "H" || sEstadoProy == "C" || (bool)Session["MODOLECTURA_PROYECTOSUBNODO"] || sEstadoMes == "C")
                {
                    sLecturaInsMes = "true";
                    sLectura = "true";
                }
                if ((sEstadoProy == "A" || sEstadoProy == "P") && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                {
                    sLectura = "false";
                }
            }

            //Comprobar si hay conspermes. Si hay, poner en lectura y mostrar mmoff
            dr = CONSPERMES.Catalogo(int.Parse(sSegMesProy), null, null, null, null, null, null, 1, 0);
            if (dr.Read())
            {
                sHayConsumos = "true";
            }
            dr.Close();

            dr = CONSNIVELMES.Catalogo(int.Parse(sSegMesProy), sEstadoMes, sMonedaImportes2);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t442_idnivel"].ToString() + "' bd='' unidades='" + dr["t379_unidades"].ToString().Replace(",", ".") + "' style='height:20px;'");

                if (sLectura == "false" && sHayConsumos == "false") sb.Append(" onclick='mm(event)' ");

                sb.Append("><td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t442_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["coste"].ToString()).ToString("N") + "</td>");

                if (sLectura == "false" && sHayConsumos == "false")
                {
                    sb.Append("<td style='text-align:right;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='" + double.Parse(dr["t379_unidades"].ToString()).ToString("N") + "' onkeyup='fm(event);setUnidades(this);' onfocus='fn(this);' onchange='calcularTotal()' title='" + dr["t379_unidades"].ToString().Replace(",", ".") + "' /></td>");
                    sb.Append("<td style='text-align:right;padding-right:2px;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='" + double.Parse(dr["importe"].ToString()).ToString("N") + "' onkeyup='fm(event);setImporte(this);' onfocus='fn(this);' onchange='calcularTotal()' /></td>");
                }
                else
                {
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["t379_unidades"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right;padding-right:2px;'>" + double.Parse(dr["importe"].ToString()).ToString("N") + "</td>");
                }
                sb.Append("</tr>");

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sLectura + "@#@" + sHayConsumos + "@#@" + MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los consumos de niveles", ex);
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

    protected string Grabar(string sSegMesProy, string strDatos)
    {
        string sResul = "";
        bool bErrorControlado = false;
        double dUnidades = 0;
        double? dUnidadesBD = null;
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
            //CONSNIVELMES.DeleteByT325_idsegmesproy(tr, int.Parse(sSegMesProy));
            string[] aConsumo = Regex.Split(strDatos, "///");
            foreach (string oConsumo in aConsumo)
            {
                if (oConsumo == "") continue;
                string[] aValores = Regex.Split(oConsumo, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID NIvel 
                //2. Coste
                //3. Unidades
                dUnidades = double.Parse(aValores[3]);
                if (dUnidades == 0)
                    CONSNIVELMES.Delete(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]));
                else
                {//Si existe en BBDD, updateo, sino, inserto
                    dUnidadesBD=CONSNIVELMES.GetUnidades(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]));
                    if (dUnidadesBD == null)//No existe registro -> lo insertamos
                        CONSNIVELMES.Insert(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), decimal.Parse(aValores[2]), dUnidades);
                    else
                    {//El registro ya existe, solo updateamos si el valor es diferente
                        if (dUnidades != dUnidadesBD)
                            CONSNIVELMES.UpdateUnidades(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), dUnidades);
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los consumos por nivel.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
