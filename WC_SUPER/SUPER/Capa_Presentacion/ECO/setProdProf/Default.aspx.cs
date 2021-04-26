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
    public string sModoTarifa = "";
    public string sNodo = "";
    protected int nIndice = 0;
    public string sAvanceProd = "0,00";
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

                sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
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

                string strTabla = getDatosProfesionales(Request.QueryString["nSegMesProy"], Request.QueryString["sEstadoMes"], Request.QueryString["sEstadoProy"], "1", sMonedaProyecto, sMonedaImportes);
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
            case ("getDatosProf"):
                sResultado += getDatosProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
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

    public string getDatosProfesionales(string sSegMesProy, string sEstadoMes, string sEstadoProy, string sConConsumos, string sMonedaProyecto2, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        int i = 0;
        try
        {
            sLectura = "false";

            if (sEstadoMes == "A") 
                dr = PRODUCFACTPROF.CatalogoMesAbierto(int.Parse(sSegMesProy), (sConConsumos == "1") ? true : false, sMonedaImportes2);
            else 
                dr = PRODUCFACTPROF.CatalogoMesCerrado(int.Parse(sSegMesProy), (sConConsumos == "1") ? true : false, sMonedaImportes2);

            sb.Append("<table id=tblDatos style='width: 980px;' mantenimiento=1>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:15px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:250px;' />");
            sb.Append("<col style='width:130px;' />");
            sb.Append("<col style='width:65px;' />");
            sb.Append("<col style='width:65px;' />");
            sb.Append("<col style='width:65px;' />");
            sb.Append("<col style='width:65px;' />");
            sb.Append("<col style='width:65px;' />");
            sb.Append("<col style='width:65px;' />");
            sb.Append("<col style='width:95px;' />");
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

           
            while (dr.Read())
            {
                //sModoTarifa = dr["t301_modelotarif"].ToString();
                //sAvanceProd = double.Parse(dr["t325_avanceprod"].ToString()).ToString("N");
                sb.Append("<tr id='" + i.ToString() + "' bd='' ");
                sb.Append("tipo=" + dr["tipo"].ToString() + " ");
                sb.Append("nPT=" + dr["t331_idpt"].ToString() + " ");
                sb.Append("nF='" + dr["t334_idfase"].ToString() + "' ");
                sb.Append("nA='" + dr["t335_idactividad"].ToString() + "' ");
                sb.Append("nT='" + dr["t332_idtarea"].ToString() + "' ");
                sb.Append("nProf='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("nPerfil='" + dr["t333_idperfilproy"].ToString() + "' ");
                sb.Append("unidades='" + dr["consumosfacturar"].ToString().Replace(",", ".") + "' ");
                sb.Append("unidfact='" + dr["consumosF"].ToString().Replace(",", ".") + "'");
                sb.Append("sexo='" + dr["sexo"].ToString() + "' ");
                sb.Append("nivel=" + dr["nivel"].ToString() + " ");
                sb.Append("importe='" + dr["totalfacturar"].ToString().Replace(",", ".") + "' ");

                //if (sLectura != "true" && dr["tipo"].ToString() == "U") sb.Append(" onclick='msse(this)' ");
                switch (dr["tipo"].ToString())
                {
                    case "PT": sb.Append("bgcolor='#A6C7D7'"); break;
                    case "F": sb.Append("bgcolor='#BCD5E1'"); break;
                    case "A": sb.Append("bgcolor='#D1E2EA'"); break;
                    case "T": sb.Append("bgcolor='#E6EEF2'"); break;
                }
                sb.Append(" style='height:20px' nivel='" + ((int)dr["nivel"]).ToString() + "'>");

                sb.Append("<td></td>");//icono bd

                //sb.Append("<td class='N" + ((int)dr["nivel"]).ToString() + "'>");
                switch ((int)dr["nivel"])
                {
                    case 1:
                        sb.Append("<td align=right></td><td colspan='5' ");
                        if (dr["tipo"].ToString() != "U") sb.Append("onmouseover='TTip(event)'");
                        sb.Append("><nobr class='NBR W310'");
                        break;
                    case 2:
                        sb.Append("<td align=right colspan='2'></td><td colspan='4' ");
                        if (dr["tipo"].ToString() != "U") sb.Append("onmouseover='TTip(event)'");
                        sb.Append("><nobr class='NBR W290'");
                        break;
                    case 3:
                        sb.Append("<td align=right colspan='3'></td><td colspan='3' ");
                        if (dr["tipo"].ToString() != "U") sb.Append("onmouseover='TTip(event)'");
                        sb.Append("><nobr class='NBR W270'");
                        break;
                    case 4:
                        sb.Append("<td align=right colspan='4'></td><td colspan='2' ");
                        if (dr["tipo"].ToString() != "U") sb.Append("onmouseover='TTip(event)'");
                        sb.Append("><nobr class='NBR W250'");
                        break;
                    case 5:
                        sb.Append("<td align=right colspan='5'></td><td><nobr class='NBR W230'");
                        break;
                }

                if (dr["tipo"].ToString() == "U")
                {
                    //sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["desItem"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ");
                    sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["desItem"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ");
                }
                sb.Append(">");

                if (dr["tipo"].ToString() == "T") sb.Append(int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - ");
                sb.Append(dr["desItem"].ToString() + "</nobr></td>");


                if (dr["tipo"].ToString() == "U")
                {
                    sb.Append("<td><nobr class='NBR W130'>" + dr["t333_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["tarifa"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["consumos"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["consumosNF"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right;' title='" + dr["consumosF"].ToString().Replace(",", ".") + "'>" + double.Parse(dr["consumosF"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["importeF"].ToString()).ToString("N") + "</td>");

                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["consumosfacturar"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(dr["totalfacturar"].ToString()).ToString("N") + "</td>");
                }
                else
                {
                    sb.Append("<td></td>");
                    sb.Append("<td style='text-align:right;'></td>");
                    sb.Append("<td style='text-align:right;'></td>");
                    sb.Append("<td style='text-align:right;'></td>");
                    sb.Append("<td style='text-align:right;'></td>");
                    sb.Append("<td style='text-align:right;'></td>");
                    sb.Append("<td style='text-align:right;'></td>");
                    sb.Append("<td style='text-align:right; padding-right:2px;'></td>");
                }

                sb.Append("</tr>");
                i++;

            }
            dr.Close();
            dr.Dispose();

            SEGMESPROYECTOSUBNODO oSegMes = SEGMESPROYECTOSUBNODO.Obtener(null, int.Parse(sSegMesProy), sMonedaImportes2);
            sAvanceProd = oSegMes.t325_avanceprod.ToString("N");
            sModoTarifa = oSegMes.t301_modelotarif;

            this.txtAvanProd.Value = sAvanceProd;
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sLectura + "@#@" + sModoTarifa + "@#@" + sAvanceProd + "@#@" + MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la producción de los profesionales (linea=" + i.ToString() +")", ex);
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
            //PRODUCFACTPROF.DeleteByT325_idsegmesproy(tr, int.Parse(sSegMesProy));

            string[] aProd = Regex.Split(strDatos, "///");
            foreach (string oProd in aProd)
            {
                if (oProd == "") continue;
                string[] aValores = Regex.Split(oProd, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID Tarea
                //2. ID usuario 
                //3. ID Perfil
                //4. Unidades

                if (aValores[0] == "D")
                    PRODUCFACTPROF.Delete(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), int.Parse(aValores[2]));
                else
                {
                    if (aValores[3] != "")
                        PRODUCFACTPROF.UpdateInsertSiNoExiste(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), 
                                                              int.Parse(aValores[2]), int.Parse(aValores[3]), double.Parse(aValores[4]));
                }
            }

            SEGMESPROYECTOSUBNODO.UpdateAvanceProduccion(tr, int.Parse(sSegMesProy), decimal.Parse(sAvanceProd));

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado)
            {
                string sAux = "Error al grabar la producción de los profesionales.\nsSegMesProy=" + sSegMesProy + "\nsAvanceProd=" + sAvanceProd + "\nstrDatos=" + strDatos;
                sResul = "Error@#@" + Errores.mostrarError(sAux, ex);
            }
            else
                sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
}
