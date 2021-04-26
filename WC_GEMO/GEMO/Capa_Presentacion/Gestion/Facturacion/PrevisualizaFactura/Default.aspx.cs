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
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using GEMO.BLL;
using Microsoft.JScript;
public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores;
    //public int nIdOrden = 0;
    public string strHTMLFactura = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                Master.TituloPagina = "Visualización de la factura";
                Master.nBotonera = 0;
                Master.bFuncionesLocales = true;
                Master.FicherosCSS.Add("Capa_Presentacion/Gestion/Facturacion/PrevisualizaFactura/factura.css");
                //Master.FicherosCSS.Add("App_Themes/Corporativo/ddfiguras.css");
                //Master.bContienePestanas = true;

                //nIdOrden = int.Parse(Request.QueryString["nIdOrden"].ToString());

                //string[] aFactura = Regex.Split(Previsualizar(nIdOrden), "@#@");
                //if (aFactura[0] == "OK") strHTMLFactura = aFactura[1];
                //else sErrores = aFactura[1];

                cboFechaFra.DataValueField = "ID";
                cboFechaFra.DataTextField = "DENOMINACION";
                cboFechaFra.DataSource = GEMO.DAL.FACTURACION.Fechas();
                cboFechaFra.DataBind();

                //string[] aFactura = Regex.Split(Previsualizar(1), "@#@");
                //if (aFactura[0] == "OK") strHTMLFactura = aFactura[1];
                //else sErrores = aFactura[1];

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al cargar la pantalla de visualización de facturas", ex);
            }
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@";
        try
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "OK@#@" + Previsualizar(int.Parse(Utilidades.unescape(aArgs[1])), DateTime.Parse(Utilidades.unescape(aArgs[2]).ToString()), int.Parse(Utilidades.unescape(aArgs[3])));
                    break;
                //case ("generarExcel"):
                //    sResultado += "OK@#@" + GEMO.BLL.LINEA.generarExcel();
                //    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener mis líneas", ex);
                    break;
                //case ("generarExcel"):
                //    sResultado += "Error@#@" + Errores.mostrarError("Error al generar el fichero excel", ex);
                //    break;
            }
        }
        _callbackResultado = sResultado;
    }

    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string Previsualizar(int iLinea, DateTime dFecha, int iResponsable)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblCatalogo' align='center' style='WIDTH:960px; table-layout:fixed;' border='0' cellspacing='0' cellpadding='0'>");

            sb.Append("<tr>");
            sb.Append("<td>");

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

            sb.Append("<table border='0' align='right' width='99%' cellspacing='0' class='texto' cellpadding='2'>");
            sb.Append("<tr><td>MIS LINEAS</td></tr>");
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


            sb.Append(GEMO.BLL.FACTURACION.ConsumosMesPropias(iResponsable, dFecha));


            sb.Append("<tr>");
            sb.Append("<td>");

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

            sb.Append("<table border='0' align='right' width='99%' cellspacing='0' class='texto' cellpadding='2'>");
            sb.Append("<tr><td>MI EQUIPO</td></tr>");
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

            sb.Append("</br>");

            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>");

            sb.Append(@"<table id='tblTitulo' style='width:810px; margin-top:10px; height:17px' cellpadding='5px'>
                    <colgroup>
                        <col style='width:360px;' />
                        <col style='width:150px; text-align:right;' />
                        <col style='width:150px; text-align:right;' />
                        <col style='width:150px; text-align:right;' />
                    </colgroup>
                    <tr>
                        <td class='bordeltb'><label class='negri'>Beneficiario</label></td>
                        <td class='bordeltb'><label class='negri'>Nº de línea</label></td>
                        <td class='bordeltb'><label class='negri'>Nº de extensión</label></td>
                        <td class='bordes'><label class='negri'>Total importe</label></td>
                    </tr>
                    </table>");

            sb.Append(GEMO.BLL.FACTURACION.ConsumosLineaMesColaboradores(iResponsable, dFecha));

            sb.Append("</br>");

            sb.Append("</td>");
            sb.Append("</tr>");
/*
            sb.Append("<tr>");
            sb.Append("<td>");

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

            sb.Append("<table border='0' align='right' width='99%' cellspacing='0' class='texto' cellpadding='2'>");
            sb.Append("<tr><td>DETALLES FACTURADOS</td></tr>");
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

            sb.Append("</br>");

            sb.Append("</td>");
            sb.Append("</tr>");
*/

            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al previsualizar el resumen de consumos.", ex);
        }
    }
    //private string Previsualizar(int i610_idordenfac)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    try
    //    {
    //        decimal dSubtotal = 0;
    //        decimal dTotal = 0;
    //        string sMoneda = "", sFechaFactura = "";

    //        // Leer Orden

    //        SqlDataReader dr = ORDENFAC.Previsualizar(i610_idordenfac);
    //        if (dr.Read())
    //        {
    //            // Completar datos cabecera

    //            sMoneda = dr["t610_moneda"].ToString();
    //            sb.Append("<table id='tblCatalogo' align='center' style='WIDTH:920px; table-layout:fixed;' border='0' cellspacing='0' cellpadding='0'>");
    //            sb.Append("<tr>");
    //            sb.Append("<td>");

    //            sb.Append("<table id='tblCabecera' class='texto' align='left' style='WIDTH:900px; table-layout:fixed;' border='0' cellspacing='0' cellpadding='0'>");
    //            sb.Append("<colgroup>");
    //            sb.Append("    <col style='width:500px; padding-left:3px;' />");
    //            sb.Append("    <col style='width:400px;' />");
    //            sb.Append("</colgroup>");

    //            sb.Append("<tr><td valign='middle'><label class='negri'>Su pedido: </label>" + dr["t610_refcliente"].ToString() + "</td>");
    //            sb.Append("<td align='right'>");

    //            sb.Append("<table border='0' width='100%' cellspacing='0' cellpadding='0'>");
    //            sb.Append("<tr>");
    //            sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/7.gif'></td>");
    //            sb.Append("<td height='6' background='../../../../Images/Tabla/8.gif'></td>");
    //            sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/9.gif'></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td width='6' background='../../../../Images/Tabla/4.gif'>&nbsp;</td>");
    //            sb.Append("<td background='../../../../Images/Tabla/5.gif' style='padding:5px'>");
    //            sb.Append("<!-- Inicio del contenido propio de la página -->");
    //            sb.Append("<center>");

    //            sb.Append("<table border='0' align='right' width='99%' cellspacing='0' class='texto' cellpadding='2'>");
    //            sb.Append("<tr><td>" + dr["t302_denominacion_destfact"].ToString() + "</td></tr>");
    //            sb.Append("<tr><td>" + dr["Direccion"].ToString() + "</td></tr>");
    //            sb.Append("<tr><td>" + dr["CodPostal"].ToString() + " " + dr["Poblacion"].ToString() + "</td></tr>");
    //            if (dr["t610_comentario"].ToString() != "")
    //                sb.Append("<tr><td>A/A: " + dr["t610_comentario"].ToString() + "</td></tr>");
    //            else
    //                sb.Append("<tr><td>&nbsp;</td></tr>");
    //            sb.Append("</table>");

    //            sb.Append("</center>");
    //            sb.Append("<!--  Fin del contenido propio de la página -->");
    //            sb.Append("</td>");
    //            sb.Append("<td width='6' background='../../../../Images/Tabla/6.gif'>&nbsp;</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/1.gif'></td>");
    //            sb.Append("<td height='6' background='../../../../Images/Tabla/2.gif'></td>");
    //            sb.Append("<td width='6' height='6' background='../../../../Images/Tabla/3.gif'></td>");
    //            sb.Append("</tr>");
    //            sb.Append("</table>");

    //            sb.Append("</td>");
    //            sb.Append("</tr>");

    //            //DateTime dtFecha = DateTime.Parse(dr["t610_fprevemifact"].ToString());
    //            if (dr["t610_fprevemifact"] != DBNull.Value)
    //            {
    //                DateTime dtFecha = DateTime.Parse(dr["t610_fprevemifact"].ToString());
    //                string mes = dtFecha.Month.ToString();
    //                string dia = dtFecha.Day.ToString();
    //                if (dia.Length == 1) dia = "0" + dia;
    //                if (mes.Length == 1) mes = "0" + mes;
    //                sFechaFactura = dia + "." + mes + "." + dtFecha.Year.ToString();
    //            }

    //            sb.Append("<tr><td colspan='2'><br>");
    //            sb.Append(" <table class='texto' cellSpacing=0 cellPadding='5px' width='100%' align='center' border=0>");
    //            sb.Append("     <tr>");
    //            sb.Append("     <td width='15%' class='bordeltb' valign='top'><label class='negri'>N.I.F. cliente</label></br>" + dr["NifDestFra"].ToString() + "</td>");
    //            sb.Append("     <td width='15%' class='bordeltb' valign='top'><label class='negri'>Fecha factura</label></br>" + sFechaFactura + "</td>");
    //            sb.Append("     <td width='15%' class='bordeltb' valign='top'><label class='negri'>Código cliente</label></br>" + dr["t302_codigoexterno"].ToString() + "</td>");
    //            sb.Append("     <td width='55%' class='bordes' valign='top'><label class='negri'>Forma de pago</label></br>" + dr["denominacion_condicionpago"].ToString() + "</td>");
    //            sb.Append("     </tr>");
    //            sb.Append(" </table>");
    //            sb.Append(" </td>");
    //            sb.Append("</tr>");
    //            sb.Append("</table>");

    //            sb.Append(" </td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td><br>");

    //            sb.Append("<table class='texto' style='WIDTH:900px;table-layout:fixed;' border='0' cellspacing='0' cellpadding='5px'>");
    //            sb.Append("<colgroup>");
    //            sb.Append("    <col style='width:570px; padding-left:3px;' />");
    //            sb.Append("    <col style='width:70px;' />");
    //            sb.Append("    <col style='width:130px;' />");
    //            sb.Append("    <col style='width:130px;' />");
    //            sb.Append("</colgroup>");

    //            sb.Append("<tr>");
    //            sb.Append("<td class='bordeltb2'><label class='negri'>Concepto</label></td>");
    //            sb.Append("<td class='bordeltb2' align='right'><label class='negri'>Cantidad</label></td>");
    //            sb.Append("<td class='bordeltb2' align='right'><label class='negri'>Precio</label></td>");
    //            sb.Append("<td class='bordes2' align='right' style='padding-right:28px;'><label class='negri'>Importe</label></br></td>");
    //            sb.Append("</tr>");
    //            sb.Append("</table>");

    //            float ft610_dto_porcen = float.Parse(dr["t610_dto_porcen"].ToString());
    //            decimal dt610_dto_importe = decimal.Parse(dr["t610_dto_importe"].ToString());

    //            decimal dImporte = 0;
    //            string strTexto = "";
    //            string sIdProyecto = int.Parse(dr["t301_idproyecto"].ToString()).ToString("###,###");
    //            string sTextoCabecera = dr["t610_textocabecera"].ToString();
    //            sTextoCabecera = sTextoCabecera.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

    //            sb.Append("<DIV id='divCatalogo' style='OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 916px; HEIGHT: 350px;'>");
    //            sb.Append("<table class='texto' style='WIDTH:900px;table-layout:fixed;' border='0' cellspacing='0' cellpadding='5px'>");
    //            sb.Append("<colgroup>");
    //            sb.Append("    <col style='width:570px; padding-left:3px;' />");
    //            sb.Append("    <col style='width:70px;text-align:right;vertical-align: bottom;' />");
    //            sb.Append("    <col style='width:130px;text-align:right;vertical-align: bottom;' />");
    //            sb.Append("    <col style='width:130px;text-align:right;vertical-align: bottom;padding-right:28px;' />");
    //            sb.Append("</colgroup>");

    //            sb.Append("<tr><td>Nuestra referencia: " + sIdProyecto + "</td><td></td><td></td><td></td></tr>");
    //            if (sTextoCabecera != "") sb.Append("<tr style='word-wrap: break-word;'><td>" + sTextoCabecera + "</td><td></td><td></td><td></td></tr>");

    //            // Completar datos de detalle

    //            dr = POSICIONFAC.CatalogoByOrdenFac(null, i610_idordenfac);


    //            while (dr.Read())
    //            {
    //                sb.Append("<tr>");
    //                strTexto = (string)dr["t611_descripcion"]; // SUSTITUIMOS RETORNO DE CARRO Y COMILLA DOBLE X COMILLA SIMPLE
    //                strTexto = strTexto.Replace(((char)13).ToString()+((char)10).ToString(), "<br>").Replace((char)34, (char)39);

    //                sb.Append("<td style='word-wrap: break-word;'>" + strTexto);
    //                sb.Append("</td>");
    //                sb.Append("<td>" + float.Parse(dr["t611_unidades"].ToString()).ToString("N"));
    //                sb.Append("</td>");

    //                sb.Append("<td");
    //                if (decimal.Parse(dr["t611_preciounitario"].ToString()) < 0) sb.Append(" style='color:red'");
    //                sb.Append(">" + decimal.Parse(dr["t611_preciounitario"].ToString()).ToString("N"));

    //                sb.Append("</td>");

    //                sb.Append("<td");
    //                if (decimal.Parse(dr["importe_sin_dto"].ToString()) < 0) sb.Append(" style='color:red'");
    //                sb.Append(">" + decimal.Parse(dr["importe_sin_dto"].ToString()).ToString("N"));

    //                sb.Append("</td>");
    //                sb.Append("</tr>");

    //                dSubtotal += decimal.Parse(dr["importe_sin_dto"].ToString());
    //                dImporte = 0;

    //                if ((float)dr["t611_dto_porcen"] > 0)
    //                {
    //                    sb.Append("<tr style='height:26px'>");
    //                    sb.Append("<td align='right' valign='top'><span style='position:relative;top:-10px'>Descuento (" + float.Parse(dr["t611_dto_porcen"].ToString()).ToString("N") + " %)</span>");
    //                    sb.Append("</td>");
    //                    sb.Append("<td>");
    //                    sb.Append("</td>");

    //                    sb.Append("<td valign='top'");
    //                    if (decimal.Parse(dr["importe_porcen"].ToString()) < 0) sb.Append(" style='color:red'");
    //                    sb.Append("><span style='position:relative;top:-10px'>" + decimal.Parse(dr["importe_porcen"].ToString()).ToString("N"));

    //                    sb.Append("</span></td>");
    //                    dImporte = decimal.Parse(dr["importe_porcen"].ToString()) * (-1);

    //                    sb.Append("<td valign='top'");
    //                    if (dImporte < 0) sb.Append(" style='color:red'");
    //                    sb.Append("><span style='position:relative;top:-10px'>" + dImporte.ToString("N"));

    //                    sb.Append("</span></td>");
    //                    sb.Append("</tr>");
    //                }

    //                if ((decimal)dr["t611_dto_importe"] > 0)
    //                {
    //                    sb.Append("<tr style='height:25px'>");
    //                    sb.Append("<td align='right' valign='top'><span style='position:relative;top:-10px'>Descuento</span>");
    //                    sb.Append("</td>");
    //                    sb.Append("<td>");
    //                    sb.Append("</td>");

    //                    sb.Append("<td valign='top'");
    //                    if (decimal.Parse(dr["t611_dto_importe"].ToString())<0) sb.Append(" style='color:red'");
    //                    sb.Append("><span style='position:relative;top:-10px'>" + decimal.Parse(dr["t611_dto_importe"].ToString()).ToString("N"));

    //                    sb.Append("</span></td>");
    //                    dImporte = decimal.Parse(dr["t611_dto_importe"].ToString()) * (-1);

    //                    sb.Append("<td valign='top'");
    //                    if (dImporte < 0) sb.Append(" style='color:red'");
    //                    sb.Append("><span style='position:relative;top:-10px'>" + dImporte.ToString("N"));

    //                    sb.Append("</span></td>");
    //                    sb.Append("</tr>");
    //                }
    //                dSubtotal += dImporte;
    //            }


    //            sb.Append("</table>");
    //            sb.Append("</DIV>");

    //            dTotal = dSubtotal;

    //            sb.Append("<table class='texto' style='WIDTH:900px;table-layout:fixed;' border='0' cellspacing='0' cellpadding='5px'>");
    //            sb.Append("<colgroup>");
    //            sb.Append("    <col style='width:570px; padding-left:3px;' />");
    //            sb.Append("    <col style='width:70px;text-align:right;' />");
    //            sb.Append("    <col style='width:130px;text-align:right;' />");
    //            sb.Append("    <col style='width:130px;text-align:right;' />");
    //            sb.Append("</colgroup>");

    //            // Cerrar cuerpo

    //            sb.Append("<tr>");
    //            sb.Append("<td class='bordet'>");
    //            sb.Append("</td>");
    //            sb.Append("<td class='bordet'>");
    //            sb.Append("</td>");
    //            sb.Append("<td class='bordet'>");
    //            sb.Append("</td>");
    //            sb.Append("<td class='bordet'>");
    //            sb.Append("</td>");
    //            sb.Append("</tr>");

    //            // Subtotal

    //            sb.Append("<tr>");
    //            sb.Append("<td>");
    //            sb.Append("</td>");
    //            sb.Append("<td>");
    //            sb.Append("</td>");
    //            sb.Append("<td class='bordelr' ><label class='negri'>Subtotal</label>");
    //            sb.Append("</td>");
    //            sb.Append("<td class='bordelr' style='padding-right:28px;");
    //            if (dSubtotal < 0) sb.Append("color:red");
    //            sb.Append("'>" + dSubtotal.ToString("N"));
    //            sb.Append("</td>");
    //            sb.Append("</tr>");

    //            // Descuento en porcentaje

    //            if (ft610_dto_porcen > 0)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td>");
    //                sb.Append("</td>");
    //                sb.Append("<td>");
    //                sb.Append("</td>");
    //                sb.Append("<td class='bordelr' ><label class='negri'>Descuento (" + ft610_dto_porcen.ToString("N") + " %)</label>");
    //                sb.Append("</td>");
    //                decimal dDto = (dSubtotal * decimal.Parse(ft610_dto_porcen.ToString()) / 100) * -1;
    //                sb.Append("<td class='bordelr' style='padding-right:28px;");
    //                if (dDto < 0) sb.Append("color:red");
    //                sb.Append("'>" + dDto.ToString("N"));
    //                sb.Append("</td>");
    //                sb.Append("</tr>");
    //                dTotal = dTotal + dDto;
    //            }

    //            // Descuento en importe

    //            if (dt610_dto_importe > 0)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td>");
    //                sb.Append("</td>");
    //                sb.Append("<td>");
    //                sb.Append("</td>");
    //                sb.Append("<td class='bordelr' ><label class='negri'>Descuento</label>");
    //                sb.Append("</td>");
    //                decimal dDto = (dt610_dto_importe * -1);
    //                sb.Append("<td class='bordelr' style='padding-right:28px;");
    //                if (dDto < 0) sb.Append("color:red");
    //                sb.Append("'>" + dDto.ToString("N"));
    //                sb.Append("</td>");
    //                sb.Append("</tr>");
    //                dTotal = dTotal + dDto;
    //            }

    //            // Total

    //            sb.Append("<tr>");
    //            sb.Append("<td>");
    //            sb.Append("</td>");
    //            sb.Append("<td>");
    //            sb.Append("</td>");
    //            sb.Append("<td class='bordelrb'><label class='negri'>Total</label>");
    //            sb.Append("</td>");
    //            sb.Append("<td class='bordelrb'><label ");
    //            if (dTotal < 0) sb.Append(" style='color:red'");
    //            sb.Append(">" + dTotal.ToString("N") + "</label> " + sMoneda);
    //            sb.Append("</td>");
    //            sb.Append("</tr>");

    //            sb.Append(" </td>");
    //            sb.Append("</tr>");
    //            sb.Append("</table>");
    //        }
    //        else
    //        {
    //            sErrores = "La orden de facturación ha sido eliminada por otro usuario.";
    //        }
    //        dr.Close();
    //        dr.Dispose();

    //        return "OK@#@" + sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al obtener las posiciones de una órden de facturación.", ex);
    //    }
    //}


}
