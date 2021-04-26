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

using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Contratos_Proyectos : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Clientes/Contratos/Proyectos";
        Master.bFuncionesLocales = true;

        if (!Page.IsCallback)
        {
            try
            {
                lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
                //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    divMonedaImportes.Style.Add("visibility", "visible");

                cargarNodos();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    private void cargarNodos()
    {
        try
        {
            //Obtener los datos necesarios
            //Cargo la denominacion del label Nodo
            this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"], false);
            while (dr.Read())
            {
                oLI = new ListItem(dr["DENOMINACION"].ToString(), dr["IDENTIFICADOR"].ToString());
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
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
             case ("generarExcel"):
                 sResultado += generarExcel
                     (
                     aArgs[1],  //NumPE
                     aArgs[2],  //IdContrato
                     aArgs[3],  //Nodo
                     aArgs[4],  //IdCliente 
                     aArgs[5],  //IdResponPE
                     aArgs[6],  //IdResponCO
                     aArgs[7],  //Estado
                     aArgs[8],  //Categoria
                     aArgs[9],  //PedidoCliente
                     aArgs[10]  //PedidoIbermatica
                     );
                 break;
             case ("contrato"):
                 sResultado += ObtenerContrato(int.Parse(aArgs[1]));
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
    private string ObtenerContrato(int idContrato)
    {
        try
        {
            string sValor = "";
            SqlDataReader dr = CONTRATO.ObtenerExtensionPadre(idContrato);
            if (dr.Read()) sValor = dr["t377_denominacion"].ToString() ;
         
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sValor;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el Contrato.", ex);
        }
    }
    private string generarExcel(string sNumProy, string sIdContrato, string sIdNodo, string sIdCliente,
                                string sIdResponPE, string sIdResponCO, string sEstado, 
                                string sCategoria, string sPedidoCliente, string sPedidoIbermatica)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = CONTRATO.ProyectosCliente
                (
                (int)Session["UsuarioActual"],
                sEstado,
                sCategoria,
                (sNumProy=="0")? null:(int?)int.Parse(sNumProy),
                (sIdContrato=="")? null:(int?)int.Parse(sIdContrato),
                (sIdNodo=="")? null:(int?)int.Parse(sIdNodo),
                (sIdCliente=="")? null:(int?)int.Parse(sIdCliente),
                (sIdResponPE=="")? null:(int?)int.Parse(sIdResponPE),
                (sIdResponCO == "")? null : (int?)int.Parse(sIdResponCO),
                sPedidoCliente,
                sPedidoIbermatica,
                Session["MONEDA_VDC"].ToString()
                );

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col/>");
            sb.Append("    <col style='width:auto;'/>");
            sb.Append("    <col style='width:auto;'/>");
            sb.Append("    <col/>");
            sb.Append("    <col style='width:auto;'/>");
            sb.Append("    <col/>");
            sb.Append("    <col style='width:auto;'/>");
            sb.Append("    <col style='width:auto;'/>");
            sb.Append("    <col /><col /><col /><col /><col /><col /><col /><col />");
            sb.Append("</colgroup>");

            //int i = 0;
            sb.Append("<tr style='height:16px;noWrap:true;text-align:center;'>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>NºOportunidad</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Denominación oportunidad</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Responsable del contrato</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Código externo</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Nombre del cliente</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>NºProyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Denominación proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Responsable del proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Estado</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Categoría</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe contratado</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe producido</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe facturado a ventas (clientes)</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe facturado (proveedores)</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cartera de pedidos</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Pendiente a facturar</td>");
            
            sb.Append("</tr>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;noWrap:true;'>");
                sb.Append("<td>" + int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###,###") + "</td>");
                sb.Append("<td>" + dr["t377_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["Comercial"].ToString() + "</td>");
                sb.Append("<td>" + dr["t302_codigoexterno"].ToString() + "</td>");
                sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###,###") + "</td>");
                sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["Responsable"].ToString() + "</td>");                
                sb.Append("<td>" + dr["t301_estado"].ToString() + "</td>");
                sb.Append("<td>" + dr["t301_categoria"].ToString() + "</td>");

                sb.Append("<td>" + decimal.Parse(dr["IMPORTE_CONTRATADO"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["TOTAL_PRODUCIDO"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["IMPORTE_FACTURA"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["IMPORTE_PROVEEDORES"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["CARTERA"].ToString()).ToString("N") + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["PENDIENTE_FACTURAR"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();

            sb.Append("<tr style='height:16px;noWrap:true;'>");
            sb.Append("<td colspan='16' style='background-color: #BCD4DF;font-weight:bold;'></br></br></br>Nota: No se recogen en estos datos los importes facturados a anticipos.</td>");
            sb.Append("</tr>" + (char)10);

            //sb.Append("<tr><td colspan='16' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td></tr>");

            sb.Append("<tr style='vertical-align:top;'>");
            sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + Session["DENOMINACION_VDC"].ToString() + "</td>");

            for (var j = 2; j <= 16; j++)
            {
                sb.Append("<td></td>");
            }
            sb.Append("</tr>" + (char)13);
            sb.Append("</table>");
            //return "OK@#@" + sb.ToString();
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al generar el Excel.", ex);
        }
    }

}