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
    public string sLectura = "false", sLecturaInsMes = "false", sAgrupado = "false";
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

                string strTabla = getDatosProduccion(Request.QueryString["nSegMesProy"], Request.QueryString["sEstadoMes"], Request.QueryString["sEstadoProy"], sMonedaProyecto, sMonedaImportes);
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getDatosProduccion"):
                sResultado += getDatosProduccion(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
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

    public string getDatosProduccion(string sSegMesProy, string sEstadoMes, string sEstadoProy, string sMonedaProyecto2, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds;
        string sModoCoste = "";

        try
        {
            sLectura = "false";

            sb.Append("<TABLE id=tblDatos style='width: 700px;' mantenimiento=1>");
            sb.Append("<COLGROUP>");
            sb.Append("<COL style='width:15px;' />");
            sb.Append("<COL style='width:325px;' />");
            sb.Append("<COL style='width:120px;' />");
            sb.Append("<COL style='width:120px;' />");
            sb.Append("<COL style='width:120px;' />");
            sb.Append("</COLGROUP>");
            sb.Append("<tbody>");
            ds = PRODUCFACTPROF.ObtenerProduccionPerfilAgrupado(int.Parse(sSegMesProy), sMonedaImportes2);

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

            if (ds.Tables[0].Rows.Count > 0)
            {
                sAgrupado = "true";
                sLectura = "true";
            }
            else
            {
                if (sEstadoProy == "H" || sEstadoProy == "C") ds = PRODUCFACTPERF.CatalogoMesCerrado(int.Parse(sSegMesProy), sMonedaImportes2);
                else if (sEstadoMes == "A") ds = PRODUCFACTPERF.CatalogoMesAbierto(int.Parse(sSegMesProy), sMonedaImportes2);
                //else if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA") ds = PRODUCFACTPERF.CatalogoMesCerradoSA(int.Parse(sSegMesProy));
                else ds = PRODUCFACTPERF.CatalogoMesCerrado(int.Parse(sSegMesProy), sMonedaImportes2);
            }

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                //sModoCoste = oFila["t301_modelocoste"].ToString();
                //sAvanceProd = double.Parse(oFila["t325_avanceprod"].ToString()).ToString("N");

                sb.Append("<tr id='" + oFila["t333_idperfilproy"].ToString() + "' bd='' unidades='" + oFila["unidades"].ToString().Replace(",", ".") + "' ");

                if (sLectura != "true") sb.Append(" onclick='ms(this)' ");

                sb.Append("style='height:20px;'>");

                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td>" + oFila["t333_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:right;'>" + double.Parse(oFila["tarifa"].ToString()).ToString("N") + "</td>");

                if (sLectura == "true" || sAgrupado == "true")
                {
                    sb.Append("<td style='text-align:right;'>" + double.Parse(oFila["unidades"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(oFila["importe"].ToString()).ToString("N") + "</td>");
                }
                else
                {
                    sb.Append("<td style='text-align:right;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='" + double.Parse(oFila["unidades"].ToString()).ToString("N") + "' onkeyup='fm(event);setUnidades(this);' onfocus='fn(this);' onchange='calcularTotal()' title='" + oFila["unidades"].ToString().Replace(",", ".") + "' /></td>");
                    sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='" + double.Parse(oFila["importe"].ToString()).ToString("N") + "' onkeyup='fm(event);setImporte(this);' onfocus='fn(this);' onchange='calcularTotal()' /></td>");
                }
                sb.Append("</tr>");
            }

            ds.Dispose();

            SEGMESPROYECTOSUBNODO oSegMes = SEGMESPROYECTOSUBNODO.Obtener(null, int.Parse(sSegMesProy), sMonedaImportes2);
            sAvanceProd = oSegMes.t325_avanceprod.ToString("N");
            sModoCoste = oSegMes.t301_modelocoste;

            this.txtAvanProd.Value = sAvanceProd;
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sLectura + "@#@" + sModoCoste + "@#@" + sAvanceProd + "@#@" + sAgrupado + "@#@" + MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la producción por perfil.", ex);
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
    protected string Grabar(string sSegMesProy, string sAvanceProd, string strDatos)
    {
        string sResul = "";
        bool bErrorControlado = false;
        double dUnidades = 0;
        double? dUnidadesBD=null;
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
            //PRODUCFACTPERF.DeleteByT325_idsegmesproy(tr, int.Parse(sSegMesProy));
            string[] aProd = Regex.Split(strDatos, "///");
            foreach (string oProd in aProd)
            {
                if (oProd == "") continue;
                string[] aValores = Regex.Split(oProd, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID Tarifa 
                //2. Unidades
                //3. Importe tarifa
                dUnidades = double.Parse(aValores[2]);
                if (dUnidades == 0)
                    PRODUCFACTPERF.Delete(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]));
                else
                {//Si existe en BBDD, updateo, sino, inserto
                    dUnidadesBD=PRODUCFACTPERF.GetUnidades(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]));
                    if (dUnidadesBD == null)//No existe registro -> lo insertamos
                        PRODUCFACTPERF.Insert(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), decimal.Parse(aValores[3]), dUnidades);
                    else
                    {//El registro ya existe, solo updateamos si el valor es diferente
                        if (dUnidades != dUnidadesBD)
                            PRODUCFACTPERF.UpdateUnidades(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), dUnidades);
                    }
                }
            }

            SEGMESPROYECTOSUBNODO.UpdateAvanceProduccion(tr, int.Parse(sSegMesProy), decimal.Parse(sAvanceProd));
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar la producción por perfil.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
}
