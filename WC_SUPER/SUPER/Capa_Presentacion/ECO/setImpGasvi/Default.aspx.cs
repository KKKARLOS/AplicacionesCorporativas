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
    public string strTablaHTML = "", strTablaHTML2 = "";
    public string sErrores = "";
    public string sLectura = "false", sLecturaInsMes = "false", sModeloImputacionGasvi = "";
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

                PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, int.Parse(Request.QueryString["nPSN"].ToString()));
                sModeloImputacionGasvi = oPSN.t305_importaciongasvi.ToString();

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

                string strTabla = getDatos(Request.QueryString["nSegMesProy"], Request.QueryString["sEstadoMes"], Request.QueryString["sEstadoProy"], sModeloImputacionGasvi, sMonedaImportes);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error")
                {
                    this.strTablaHTML = aTabla[1];
                    this.strTablaHTML2 = aTabla[2];
                }
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
            case ("getDatos"):
                sResultado += getDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
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

    public string getDatos(string sSegMesProy, string sEstadoMes, string sEstadoProy, string sModeloImputacionGasvi, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        
        try
        {
            sLectura = "false";

            if (sModeloImputacionGasvi == "1") sLectura = "true";
            else
            {
                if (sEstadoProy == "H" || sEstadoProy == "C" || (bool)Session["MODOLECTURA_PROYECTOSUBNODO"] || sEstadoMes == "C")
                {
                    sLectura = "true";
                }
                if ((sEstadoProy == "A" || sEstadoProy == "P") && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                {
                    sLectura = "false";
                }
            }
            if (sEstadoProy == "H" || sEstadoProy == "C" || (bool)Session["MODOLECTURA_PROYECTOSUBNODO"])
            {
                sLecturaInsMes = "true";
            }

            string sMano = " MAM";
            if (sLectura == "true") sMano = " MA";

            sb.Append("<table class='texto" + sMano + "' id='tblDatos' style='width: 450px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:195px;' />");
            sb.Append("<col style='width:195px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            sb2.Append("<table class='texto" + sMano + "' id='tblDatos2' style='width: 450px;'>");
            sb2.Append("<colgroup>");
            sb2.Append("<col style='width:10px;' />");
            sb2.Append("<col style='width:185px;' />");
            sb2.Append("<col style='width:195px;' />");
            sb2.Append("<col style='width:60px;' />");
            sb2.Append("</colgroup>");
            sb2.Append("<tbody>");

            DataSet ds = DATOECO.ObtenerConsumosGASVI(int.Parse(sSegMesProy), sMonedaImportes2);
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("<tr id='" + oFila["t420_idreferencia"].ToString() + "' bd='' style='height:20px;' ");
                sb.Append("profesional=\"" + Utilidades.escape(oFila["Interesado"].ToString()) + "\" ");
                sb.Append("ondblclick=\"mdng(" + oFila["t420_idreferencia"].ToString() + ",'" + oFila["TipoNota"].ToString() + "')\" ");
//                if (sLectura == "false") 
                sb.Append(" onclick=mm(event) onmousedown=DD(event) ");
                //sb.Append("><td><nobr class='NBR W180' ondblclick=\"mdng(" + oFila["t420_idreferencia"].ToString() + ",'" + oFila["TipoNota"].ToString() + "')\" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["Interesado"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style='width:70px;'>Empresa:</label>" + oFila["EMPRESA"].ToString() + "<br><label style='width:70px;'>Responsable:</label>" + oFila["Responsable_proyecto"].ToString() + "<br><label style='width:70px;'>Aprobación:</label>" + ((DateTime)oFila["t420_fAprobada"]).ToShortDateString() + "] hideselects=[off]\">(Ref: " + int.Parse(oFila["t420_idreferencia"].ToString()).ToString("#,###") + ")&nbsp;&nbsp;" + oFila["Interesado"].ToString() + "</nobr></td>");
                sb.Append("><td style='padding-left:5px;'><nobr class='NBR W180' ondblclick=\"mdng(" + oFila["t420_idreferencia"].ToString() + ",'" + oFila["TipoNota"].ToString() + "')\" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["Interesado"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style='width:70px;'>Responsable:</label>" + oFila["Responsable_proyecto"].ToString() + "<br><label style='width:70px;'>Aprobación:</label>" + ((DateTime)oFila["t420_fAprobada"]).ToShortDateString() + "] hideselects=[off]\">(Ref: " + int.Parse(oFila["t420_idreferencia"].ToString()).ToString("#,###") + ")&nbsp;&nbsp;" + oFila["Interesado"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W180'>" + oFila["t420_concepto"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(oFila["IMPORTE"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                sb2.Append("<tr id='" + oFila["t420_idreferencia"].ToString() + "' idDatoEco='" + oFila["t376_iddatoeco"].ToString() + "' bd='' style='height:20px;' ");
                sb2.Append("profesional=\"" + Utilidades.escape(oFila["Interesado"].ToString()) + "\" ");
                sb2.Append("ondblclick=\"mdng(" + oFila["t420_idreferencia"].ToString() + ",'" + oFila["TipoNota"].ToString() + "')\" ");
//                if (sLectura == "false") 
                sb2.Append(" onclick='mm(event)' onmousedown='DD(event);' ");
                sb2.Append("><td><img src='../../../images/imgFN.gif'></td>");
                //sb2.Append("<td><nobr class='NBR W170' ondblclick=\"mdng(" + oFila["t420_idreferencia"].ToString() + ",'" + oFila["TipoNota"].ToString() + "')\" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["Interesado"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style='width:70px;'>Empresa:</label>" + oFila["EMPRESA"].ToString() + "<br><label style='width:70px;'>Responsable:</label>" + oFila["Responsable_proyecto"].ToString() + "<br><label style='width:70px;'>Aprobación:</label>" + ((DateTime)oFila["t420_fAprobada"]).ToShortDateString() + "] hideselects=[off]\">(Ref: " + int.Parse(oFila["t420_idreferencia"].ToString()).ToString("#,###") + ")&nbsp;&nbsp;" + oFila["Interesado"].ToString() + "</nobr></td>");
                sb2.Append("<td col><nobr class='NBR W170' ondblclick=\"mdng(" + oFila["t420_idreferencia"].ToString() + ",'" + oFila["TipoNota"].ToString() + "')\" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + oFila["Interesado"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style='width:70px;'>Responsable:</label>" + oFila["Responsable_proyecto"].ToString() + "<br><label style='width:70px;'>Aprobación:</label>" + ((DateTime)oFila["t420_fAprobada"]).ToShortDateString() + "] hideselects=[off]\">(Ref: " + int.Parse(oFila["t420_idreferencia"].ToString()).ToString("#,###") + ")&nbsp;&nbsp;" + oFila["Interesado"].ToString() + "</nobr></td>");
                sb2.Append("<td><nobr class='NBR W180'>" + oFila["t420_concepto"].ToString() + "</nobr></td>");
                sb2.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(oFila["IMPORTE"].ToString()).ToString("N") + "</td>");
                sb2.Append("</tr>");
            }
            ds.Dispose();
            sb2.Append("</tbody>");
            sb2.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sb2.ToString() + "@#@" + sLectura + "@#@" + MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los consumos de GASVI", ex);
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
        string sResul = "", sElementosInsertados = "";
        int nAux = 0;

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
            string[] aConsumo = Regex.Split(strDatos, "///");
            foreach (string oConsumo in aConsumo)
            {
                if (oConsumo == "") continue;
                string[] aValores = Regex.Split(oConsumo, "##");
                //0. Opcion BD. "I", "D"
                //1. ID nota gasvi
                //2. Profesional (para el motivo)
                //3. idDatoEco

                switch(aValores[0]){
                    case "I":
                        nAux = DATOECO.InsertConsumoGasvi(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), Utilidades.unescape(aValores[2]));
                        if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                        else sElementosInsertados += "//" + nAux.ToString();
                        break;
                    case "D":
                        DATOECO.Delete(tr, int.Parse(aValores[3]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al importar las imputaciones de GASVI", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
