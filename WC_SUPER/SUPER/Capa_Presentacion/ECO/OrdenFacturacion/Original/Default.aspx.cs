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
    private string _callbackResultado = null;
    public string sErrores = "";
    public int nIdOrden = 0;
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

                nIdOrden = int.Parse(Request.QueryString["nIdOrden"].ToString());
                hdnIdOrden.Text = nIdOrden.ToString();
                txtIdOrden.Text = nIdOrden.ToString();

                // Leer Orden Original
                ORDENFACORIGEN oOrdenOrigen = ORDENFACORIGEN.Select(null, nIdOrden);

                txtNumPE.Text = oOrdenOrigen.t301_idproyecto.ToString("#,###");
                txtDesPE.Text = oOrdenOrigen.t301_denominacion;
                //txtNIFCliSolicitante.Text = oOrdenOrigen.NifSolicitante;
                //txtDesCliSolicitante.Text = oOrdenOrigen.t302_denominacion_solici;

                txtNIFCliPago.Text = oOrdenOrigen.NifRespPago;
                txtDesCliRespPago.Text = oOrdenOrigen.t302_denominacion_respago;

                txtNIFCliDestFac.Text = oOrdenOrigen.NifDestFra;
                txtDesClienteDestFac.Text = oOrdenOrigen.t302_denominacion_destfact;
                cldDireccion.InnerHtml = oOrdenOrigen.direccion;

                txtRefCli.Text = oOrdenOrigen.t619_refcliente;

                //txtRespCom.Text = oOrdenOrigen.RespComercial;
                txtCondPago.Text = oOrdenOrigen.des_condicionpago;
                txtViaPago.Text = oOrdenOrigen.des_viapago;
                txtMoneda.Text = oOrdenOrigen.des_moneda;
                txtOV.Text = oOrdenOrigen.des_ovsap;

                txtFecPrevEmFac.Text = oOrdenOrigen.t619_fprevemifact.ToShortDateString();
                txtFecDiferida.Text = (oOrdenOrigen.t619_fdiferida.HasValue) ? ((DateTime)oOrdenOrigen.t619_fdiferida).ToShortDateString() : "";

                txtClaveAgru.Text = oOrdenOrigen.t619_idagrupacion.ToString();
                txtClaveAgru.Attributes.Add("title", oOrdenOrigen.t622_denominacion);
                txtComentarios.Text = oOrdenOrigen.t619_comentario;
                txtObsPool.Text = oOrdenOrigen.t619_observacionespool;
                txtDtoPorc.Text = (oOrdenOrigen.t619_dto_porcen == 0) ? "" : oOrdenOrigen.t619_dto_porcen.ToString("N");
                txtDtoImporte.Text = (oOrdenOrigen.t619_dto_importe == 0) ? "" : oOrdenOrigen.t619_dto_importe.ToString("N");
                chkIVA.Checked = oOrdenOrigen.t619_ivaincluido;
                txtCabecera.Text = oOrdenOrigen.t610_textocabecera;

                if (oOrdenOrigen.t619_infotramit != "")
                {
                    this.lblTramitadaPor1.InnerHtml = "Tramitada por:";
                    this.lblTramitadaPor2.InnerHtml = oOrdenOrigen.t619_infotramit;
                }
                else
                {
                    this.lblTramitadaPor1.InnerHtml = "";
                    this.lblTramitadaPor2.InnerHtml = "";
                }
            }
            catch (NullReferenceException ex)
            {
                sErrores = ex.Message;
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la cabecera de la orden", ex);
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
        string sResultado = "", sCad = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("documentos"):
                string sModoAcceso = "W", sEstadoProyecto = "A";
                //sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), "W", "A");
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = getDocumentos(aArgs[1], aArgs[3], aArgs[4]);
                //sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("getDatosPestana"):
                //sResultado += aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0: //Cabecera
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1: //Posiciones
                        //sResultado += getPosiciones(aArgs[2]);
                        sCad = getPosiciones(aArgs[2], aArgs[3]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;

                        break;
                    case 2://DOCUMENTACION
                        sCad = getDocumentos(aArgs[2], aArgs[3], aArgs[4]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
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

    private string getPosiciones(string st619_idordenfac, string sModoAcceso)
    {
        StringBuilder sb = new StringBuilder();
        string sClase = "";
        try
        {
            if (sModoAcceso == "W") sClase = "MANO";
            sb.Append("<table id='tblPosiciones' class='" + sClase + "' style='width: 880px;' cellpadding='2'>");
		    sb.Append("    <colgroup>");
		    sb.Append("        <col style='width:15px;' />");
		    sb.Append("        <col style='width:20px;' />");
	        sb.Append("        <col style='width:425px;' />");
	        sb.Append("        <col style='width:70px;' />");
	        sb.Append("        <col style='width:70px;' />");
	        sb.Append("        <col style='width:70px;' />");
	        sb.Append("        <col style='width:70px;' />");
		    sb.Append("        <col style='width:100px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = POSICIONFACORIGEN.CatalogoByOrdenFac(null, int.Parse(st619_idordenfac));
            string strDatos = "", sToolTip = "";

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t620_posicion"].ToString() + "' bd='' onclick='mm(event)' style='height:38px; vertical-align:top;'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");

                sToolTip = " title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[";
                sToolTip += "<label style='width:150px;'>&nbsp;</label><br>";
                sToolTip += "<label style='width:100px;'><b><u>Posición:</u></b></label><br>";
                sToolTip += "<label style='width:100px;'>Nº SUPER:</label>" + dr["t620_posicion"].ToString() + "<br>";

                sToolTip += "] hideselects=[off]\"";
                sb.Append("<td><img src='../../../../images/imgPosicionD.gif' ");
                sb.Append(sToolTip);
                sb.Append("></td>");
                sb.Append("<td>");
                //sb.Append("<input type='text' class='txtM' style='width:420px;' value=\"" + dr["t620_denominacion"].ToString() + "\" readonly />");
                sb.Append("<textarea class='txtMultiM' style='width:420px;' rows='2' readonly>" + dr["t620_descripcion"].ToString() + "</textarea>");
                sb.Append("</td>");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + float.Parse(dr["t620_unidades"].ToString()).ToString("N") + "\" readonly /></td>");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + decimal.Parse(dr["t620_preciounitario"].ToString()).ToString("N") + "\" readonly /></td>");
                strDatos = "";
                if ((float)dr["t620_dto_porcen"] > 0) strDatos = float.Parse(dr["t620_dto_porcen"].ToString()).ToString("N");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + strDatos + "\" readonly /></td>");
                strDatos = "";
                if ((decimal)dr["t620_dto_importe"] > 0) strDatos = decimal.Parse(dr["t620_dto_importe"].ToString()).ToString("N");
                sb.Append("<td><input type='text' class='txtNumM' style='width:60px;' value=\"" + strDatos + "\" readonly /></td>");
                sb.Append("<td><input type='text' class='txtNumM' style='width:90px;' value=\"" + decimal.Parse(dr["importe"].ToString()).ToString("N") + "\" readonly /></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las posiciones de una órden de facturación.", ex);
        }
    }
    private string getDocumentos(string st610_idordenfac, string sModoAcceso, string sEstProy)
    {
        StringBuilder sb = new StringBuilder();
        bool bModificable = false;

        try
        {
            SqlDataReader dr = DOCUOF.Catalogo(int.Parse(st610_idordenfac));
            if (sModoAcceso == "R")
                sb.Append("<table id='tblDocumentos' class='texto' style='width: 850px;'>");
            else
                sb.Append("<table id='tblDocumentos' class='texto MANO' style='width: 850px;'>");
            
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("    <col style='width:250px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                ////Si el usuario es el autor del archivo, o es administrador, se permite modificar.
                //if ((dr["t314_idusuario_autor"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString() || SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()))
                //{
                //    if (sModoAcceso == "R")
                //        bModificable = false;
                //    else
                //        bModificable = true;
                //}
                //else
                //    bModificable = false;

                sb.Append("<tr style='height:20px;' id='" + dr["t624_iddocuof"].ToString() + "' onclick='mm(event);' sTipo='OF' sAutor='" + dr["t314_idusuario_autor"].ToString() + "' onmouseover='TTip(event)'>");

                if (bModificable)
                    sb.Append("<td style='padding-left:3px;' class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR W290'>" + dr["t624_descripcion"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W280'>" + dr["t624_descripcion"].ToString() + "</nobr></td>");

                if (dr["t624_nombrearchivo"].ToString() == "")
                {
                    if (bModificable)
                        sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                    else
                        sb.Append("<td></td>");
                }
                else
                {
                    string sNomArchivo = dr["t624_nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                    ////Si la persona que entra es el autor, o es administrador, se permite descargar.
                    //if (dr["t314_idusuario_autor"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString() || SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    sb.Append("<td><img src=\"../../../../images/imgDescarga.gif\" width='16px' height='16px' class='MANO' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                    //else
                    //    sb.Append("<td><img src=\"../../../../images/imgSeparador.gif\" width='16px' height='16px' style='vertical-align:bottom;'>");
                    if (bModificable)
                        sb.Append("&nbsp;<nobr class='NBR MA' style='width:260px;' ondblclick=\"modificarDoc(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                    else
                        sb.Append("&nbsp;<nobr class='NBR' style='width:260px;'>" + sNomArchivo + "</nobr></td>");
                }

                //sb.Append("<td><nobr class='NBR' style='width:90px;'>" + dr["autor"].ToString() + "</nobr></td>");
                if (bModificable)
                    sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR W240'>" + dr["autor"].ToString() + "</nobr></td></tr>");
                else
                    sb.Append("<td><nobr class='NBR W240'>" + dr["autor"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documentos de la orden de facturación", ex);
        }
    }

}
