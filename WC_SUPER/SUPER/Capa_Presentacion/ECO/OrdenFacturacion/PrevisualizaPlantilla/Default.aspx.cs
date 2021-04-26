using System;
using System.Data;
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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page
{
    public string sErrores;
    public int nIdPlantilla = 0;
    public string strHTMLFactura = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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

                nIdPlantilla = int.Parse(Request.QueryString["nIdPlantilla"].ToString());

                string[] aFactura = Regex.Split(Previsualizar(nIdPlantilla), "@#@");
                if (aFactura[0] == "OK") strHTMLFactura = aFactura[1];
                else sErrores = aFactura[1];
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al cargar la pantalla de visualización de plantillas", ex);
            }

        }
    }
    private string Previsualizar(int t629_idplantillaof)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            decimal dSubtotal = 0;
            decimal dTotal = 0;
            string sMoneda = "", sFechaFactura = "";

            // Leer Orden

            SqlDataReader dr = PLANTILLAORDENFAC.Previsualizar(t629_idplantillaof);
            if (dr.Read())
            {
                // Completar datos cabecera
                sMoneda = dr["t629_moneda"].ToString();
                sb.Append("<table id='tblCatalogo' align='center' style='width:920px;'>");
                sb.Append("<tr>");
                sb.Append("<td>");

                sb.Append("<table id='tblCabecera' align='left' style='width:900px;'>");
                sb.Append("<colgroup>");
                sb.Append("    <col style='width:500px;' />");
                sb.Append("    <col style='width:400px;' />");
                sb.Append("</colgroup>");

                sb.Append("<tr><td style='vertical-align:middle; padding-left:3px;'><label class='negri W70'>Su pedido: </label>" + HttpUtility.HtmlEncode(dr["t629_refcliente"].ToString()) + "</td>");
                sb.Append("<td style='text-align:right;'>");

                sb.Append("<table border='0' width='100%' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr>");
                sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/7.gif'></td>");
                sb.Append("<td height='6' background='../../../../Images/Tabla/8.gif'></td>");
                sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/9.gif'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width='6' background='../../../../Images/Tabla/4.gif'>&nbsp;</td>");
                sb.Append("<td background='../../../../Images/Tabla/5.gif' style='padding:5px'>");
                sb.Append("<!-- Inicio del contenido propio de la página -->");
                sb.Append("<center>");

                sb.Append("<table style='width:99%;text-align:left' cellpadding='2'>");
                sb.Append("<tr><td><nobr class='NBR W360' onmouseover='TTip(event)'>" + dr["t302_denominacion_destfact"].ToString() + "</nobr></td></tr>");
                sb.Append("<tr><td>" + dr["Direccion"].ToString() + "</td></tr>");
                sb.Append("<tr><td>" + dr["CodPostal"].ToString() + " " + dr["Poblacion"].ToString() + "</td></tr>");
                if (dr["t629_comentario"].ToString() != "")
                    sb.Append("<tr><td>A la AA: " + dr["t629_comentario"].ToString() + "</td></tr>");
                else
                    sb.Append("<tr><td>&nbsp;</td></tr>");
                sb.Append("</table>");

                sb.Append("</center>");
                sb.Append("<!--  Fin del contenido propio de la página -->");
                sb.Append("</td>");
                sb.Append("<td width='6' background='../../../../Images/Tabla/6.gif'>&nbsp;</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/1.gif'></td>");
                sb.Append("<td height='6' background='../../../../Images/Tabla/2.gif'></td>");
                sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/3.gif'></td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("</td>");
                sb.Append("</tr>");

                if (dr["t629_fprevemifact"] != DBNull.Value){
                    DateTime dtFecha = DateTime.Parse(dr["t629_fprevemifact"].ToString());
                    string mes = dtFecha.Month.ToString();
                    string dia = dtFecha.Day.ToString();
                    if (dia.Length == 1) dia = "0" + dia;
                    if (mes.Length == 1) mes = "0" + mes;
                    sFechaFactura = dia + "." + mes + "." + dtFecha.Year.ToString();
                }
                sb.Append("<tr><td colspan='2'><br>");
                sb.Append(" <table cellpadding='5px' width='100%' align='center'>");
                sb.Append("     <tr>");
                sb.Append("     <td width='15%' class='bordeltb' style='vertical-align:top;'><label class='negri'>N.I.F. cliente</label></br>" + dr["NifDestFra"].ToString() + "</td>");
                sb.Append("     <td width='15%' class='bordeltb' style='vertical-align:top;'><label class='negri'>Fecha factura</label></br>" + sFechaFactura + "</td>");
                sb.Append("     <td width='15%' class='bordeltb' style='vertical-align:top;'><label class='negri'>Código cliente</label></br>" + dr["t302_codigoexterno"].ToString() + "</td>");
                sb.Append("     <td width='55%' class='bordes' style='vertical-align:top;'><label class='negri'>Forma de pago</label></br>" + dr["denominacion_condicionpago"].ToString() + "</td>");
                sb.Append("     </tr>");
                sb.Append(" </table>");
                sb.Append(" </td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                
                sb.Append(" </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><br>");

                sb.Append("<table class='texto' style='width:900px;' cellpadding='5px'>");
                sb.Append("<colgroup>");
                sb.Append("    <col style='width:570px;' />");
                sb.Append("    <col style='width:70px;' />");
                sb.Append("    <col style='width:130px;' />");
                sb.Append("    <col style='width:130px;' />");
                sb.Append("</colgroup>");

                sb.Append("<tr>");
                sb.Append("<td class='bordeltb2' style='padding-left:3px;'><label class='negri'>Concepto</label></td>");
                sb.Append("<td class='bordeltb2' align='right'><label class='negri'>Cantidad</label></td>");
                sb.Append("<td class='bordeltb2' align='right'><label class='negri'>Precio</label></td>");
                sb.Append("<td class='bordes2'   align='right' style='padding-right:28px;'><label class='negri'>Importe</label></br></td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                float ft629_dto_porcen = float.Parse(dr["t629_dto_porcen"].ToString());
                decimal dt629_dto_importe = decimal.Parse(dr["t629_dto_importe"].ToString());


                decimal dImporte = 0;
                string strTexto = "";
                string sIdProyecto = int.Parse(dr["t301_idproyecto"].ToString()).ToString("###,###");
                string sTextoCabecera = dr["t629_textocabecera"].ToString();
                sTextoCabecera = sTextoCabecera.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

                sb.Append("<div id='divCatalogo' style='overflow: auto; overflow-x: hidden; width: 917px; height: 350px;'>");
                sb.Append("<table style='width:900px;' cellpadding='5px'>");
                sb.Append("<colgroup>");
                sb.Append("    <col style='width:570px;' />");
                sb.Append("    <col style='width:70px;' />");
                sb.Append("    <col style='width:130px;' />");
                sb.Append("    <col style='width:130px;' />");
                sb.Append("</colgroup>");

                sb.Append("<tr><td style='padding-left:3px;border-left: #5894ae 1px solid;border-right: #5894ae 1px solid;'>Nuestra referencia: " + sIdProyecto + "</td><td style='text-align:right;vertical-align: bottom;border-right: #5894ae 1px solid;'></td><td style='text-align:right;vertical-align: bottom;border-right: #5894ae 1px solid;'></td><td style='text-align:right;vertical-align:bottom;padding-right:28px;border-right: #5894ae 1px solid;'></td></tr>");
                if (sTextoCabecera != "") sb.Append("<tr style='word-wrap: break-word;'><td style='padding-left:3px;border-left: #5894ae 1px solid;border-right: #5894ae 1px solid;'>" + sTextoCabecera + "</td><td style='text-align:right;vertical-align: bottom;border-right: #5894ae 1px solid;'></td><td style='border-right: #5894ae 1px solid;'></td><td style='border-right: #5894ae 1px solid;text-align:right;vertical-align:bottom;padding-right:28px;'></td></tr>");

                // Completar datos de detalle

                dr = PLANTILLAPOSICIONFAC.CatalogoByPlantilla(null, t629_idplantillaof);

                int i = 0;
                while (dr.Read())
                {
                    sb.Append("<tr>");
                    strTexto = (string)dr["t630_descripcion"]; // SUSTITUIMOS RETORNO DE CARRO Y COMILLA DOBLE X COMILLA SIMPLE
                    strTexto = strTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);
                    //strTexto = strTexto.Replace(((char)10).ToString(), "<br>").Replace((char)34, (char)39);

                    sb.Append("<td style='padding-left:3px;border-left: #5894ae 1px solid;border-right: #5894ae 1px solid;'>" + strTexto + "</td>");
                    sb.Append("<td style='text-align:right;vertical-align:bottom;border-right: #5894ae 1px solid;'>" + float.Parse(dr["t630_unidades"].ToString()).ToString("N") + "</td>");

                    sb.Append("<td style='text-align:right;vertical-align:bottom;border-right: #5894ae 1px solid;");
                    if (decimal.Parse(dr["t630_preciounitario"].ToString()) < 0) sb.Append(" color:red;");
                    sb.Append("'>" + decimal.Parse(dr["t630_preciounitario"].ToString()).ToString("N"));

                    sb.Append("</td>");

                    sb.Append("<td style='text-align:right;vertical-align:bottom;padding-right:28px;border-right: #5894ae 1px solid;'");
                    if (decimal.Parse(dr["importe_sin_dto"].ToString()) < 0) sb.Append(" color:red;");
                    sb.Append("'>" + decimal.Parse(dr["importe_sin_dto"].ToString()).ToString("N"));

                    sb.Append("</td>");
                    sb.Append("</tr>");

                    dSubtotal += decimal.Parse(dr["importe_sin_dto"].ToString());
                    dImporte = 0;

                    if ((float)dr["t630_dto_porcen"] > 0)
                    {
                        sb.Append("<tr style='height:26px'>");
                        sb.Append("<td style='text-align:right; vertical-align:top; border-left: #5894ae 1px solid;border-right: #5894ae 1px solid;'><span style='position:relative;top:-10px'>Descuento (" + float.Parse(dr["t630_dto_porcen"].ToString()).ToString("N") + " %)</span>");
                        sb.Append("</td>");
                        sb.Append("<td style='border-right: #5894ae 1px solid;'>");
                        sb.Append("</td>");

                        sb.Append("<td style='vertical-align:top; text-align:right; border-right: #5894ae 1px solid;");
                        if (decimal.Parse(dr["importe_porcen"].ToString()) < 0) sb.Append(" color:red;");
                        sb.Append("'><span style='position:relative;top:-10px'>" + decimal.Parse(dr["importe_porcen"].ToString()).ToString("N"));

                        sb.Append("</span></td>");
                        dImporte = decimal.Parse(dr["importe_porcen"].ToString()) * (-1);

                        sb.Append("<td style='vertical-align:top; text-align:right; padding-right:28px;border-right: #5894ae 1px solid;");
                        if (dImporte < 0) sb.Append(" color:red;");
                        sb.Append("'><span style='position:relative;top:-10px'>" + dImporte.ToString("N"));

                        sb.Append("</span></td>");
                        sb.Append("</tr>");
                    }

                    if ((decimal)dr["t630_dto_importe"] > 0)
                    {
                        sb.Append("<tr style='height:25px'>");
                        sb.Append("<td style='vertical-align:top; text-align:right; border-left: #5894ae 1px solid;border-right: #5894ae 1px solid;'><span style='position:relative;top:-10px'>Descuento");
                        sb.Append("</span></td>");
                        sb.Append("<td style='vertical-align:top; border-right: #5894ae 1px solid;'>");
                        sb.Append("</td>");

                        sb.Append("<td style='vertical-align:top; text-align:right; border-right: #5894ae 1px solid;");
                        if (decimal.Parse(dr["t630_dto_importe"].ToString()) < 0) sb.Append(" color:red");
                        sb.Append("'><span style='position:relative;top:-10px'>" + decimal.Parse(dr["t630_dto_importe"].ToString()).ToString("N"));

                        sb.Append("</span></td>");
                        dImporte = decimal.Parse(dr["t630_dto_importe"].ToString()) * (-1);

                        sb.Append("<td style='vertical-align:top; text-align:right; padding-right:28px;border-right: #5894ae 1px solid;");
                        if (dImporte < 0) sb.Append(" color:red;");
                        sb.Append("'><span style='position:relative;top:-10px'>" + dImporte.ToString("N"));

                        sb.Append("</span></td>");
                        sb.Append("</tr>");
                        i++;
                    }
                    dSubtotal += dImporte;
                }
                while (i < 16)
                {
                    sb.Append("<tr><td style='border-left: #5894ae 1px solid;border-right: #5894ae 1px solid;'>&nbsp;</td><td style='border-right: #5894ae 1px solid;'></td><td style='border-right: #5894ae 1px solid;'></td><td style='border-right: #5894ae 1px solid;'></td></tr>");
                    i++;
                }

                sb.Append("</table>");
                sb.Append("</DIV>");

                dTotal = dSubtotal;

                sb.Append("<table style='width:900px;' cellpadding='0px'>");
                sb.Append("<colgroup>");
                sb.Append("    <col style='width:570px;' />");
                sb.Append("    <col style='width:70px;' />");
                sb.Append("    <col style='width:130px;' />");
                sb.Append("    <col style='width:130px;' />");
                sb.Append("</colgroup>");

                // Cerrar cuerpo

                sb.Append("<tr>");
                sb.Append("<td style='padding-left:3px;' class='bordet'>");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;' class='bordet'>");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;' class='bordet'>");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;' class='bordet'>");
                sb.Append("</td>");
                sb.Append("</tr>");

                // Subtotal

                sb.Append("<tr>");
                sb.Append("<td style='padding-left:3px;padding-top:5px;' >");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;padding-top:5px;'>");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;padding-top:5px;' class='bordelr' ><label class='negri'>Subtotal</label>");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;padding-right:28px;padding-top:5px;' class='bordelr'");
                if (dSubtotal < 0) sb.Append("color:red");
                sb.Append("'>" + dSubtotal.ToString("N"));
                sb.Append("</td>");
                sb.Append("</tr>");

                // Descuento en porcentaje

                if (ft629_dto_porcen > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-left:3px;padding-top:5px;'>");
                    sb.Append("</td>");
                    sb.Append("<td style='text-align:right;padding-top:5px;'>");
                    sb.Append("</td>");
                    sb.Append("<td style='text-align:right;padding-top:5px;' class='bordelr' ><label class='negri'>Descuento (" + ft629_dto_porcen.ToString("N") + " %)</label>");
                    sb.Append("</td>");

                    decimal dDto = (dSubtotal * decimal.Parse(ft629_dto_porcen.ToString()) / 100) * -1;
                    sb.Append("<td class='bordelr' style='text-align:right;padding-right:28px;padding-top:5px;");
                    if (dDto < 0) sb.Append("color:red");
                    sb.Append("'>" + dDto.ToString("N"));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    dTotal = dTotal + dDto;
                }

                // Descuento en importe

                if (dt629_dto_importe > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-left:3px;padding-top:5px;'>");
                    sb.Append("</td>");
                    sb.Append("<td style='text-align:right;padding-top:5px;'>");
                    sb.Append("</td>");
                    sb.Append("<td style='text-align:right;padding-top:5px;' class='bordelr' ><label class='negri'>Descuento</label>");
                    sb.Append("</td>");
                    decimal dDto = (dt629_dto_importe * -1);
                    sb.Append("<td class='bordelr' style='text-align:right;padding-right:28px;padding-top:5px;");
                    if (dDto < 0) sb.Append("color:red");
                    sb.Append("'>" + dDto.ToString("N"));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    dTotal = dTotal + dDto;
                }

                // Total
                sb.Append("<tr>");
                sb.Append("<td style='padding-left:3px;padding-top:5px;padding-bottom:5px;'>");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;padding-top:5px;padding-bottom:5px;'>");
                sb.Append("</td>");
                sb.Append("<td style='text-align:right;padding-top:5px;padding-bottom:5px;' class='bordelrb'><label class='negri'>Total</label>");
                sb.Append("</td>");
                sb.Append("<td class='bordelrb' style='text-align:right;padding-right:5px;padding-top:5px;padding-bottom:5px;'><label ");
                if (dTotal < 0) sb.Append(" style='color:red'");
                sb.Append(">" + dTotal.ToString("N") + "</label> " + sMoneda);
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append(" </td>");
                sb.Append("</tr>");
                sb.Append("</table>");

            }
            else
            {
                sErrores = "No se han obtenido los datos de la plantilla de orden de facturación nº: " + t629_idplantillaof.ToString("#,###") + ".";
                return "Error@#@" + sErrores;
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las posiciones de una plantilla de orden de facturación.", ex);
        }
    }


}
