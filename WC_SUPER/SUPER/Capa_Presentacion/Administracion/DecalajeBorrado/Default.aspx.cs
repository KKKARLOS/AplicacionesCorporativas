using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 43;
                Master.nResolucion = 1280;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Decalaje y borrado de órdenes de facturación";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                try
                {
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    imgMarcar.Attributes.Add("title", "Marcar todos los " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    imgDesmarcar.Attributes.Add("title", "Desmarcar todos los " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    cargarNodos();
                }
                catch (Exception ex)
                {
                    Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += obtenerOF(aArgs[1], aArgs[2]);
                break;
            case ("procesar"):
                sResultado += procesarOF(aArgs[1], aArgs[2]);
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

    private void cargarNodos()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = NODO.CatalogoConEstructura();
            string sTootTip = "";

            sb.Append("<table id='tblNodos' style='width: 500px;'>");
            sb.Append("<colgroup><col style='width:70px;' /><col style='width:410px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                if (Utilidades.EstructuraActiva("SN4")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString();

                sb.Append("<tr id='" + dr["NODO"].ToString() + "' ");
                sb.Append("style='height:20px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");
                sb.Append("<td style='text-align:right; padding-right:3px;'>" + dr["NODO"].ToString() + "</td>");
                sb.Append("<td style='padding-left:5px;'>" + dr["denominacion"].ToString() + "</td>");
                sb.Append("<td><input type='checkbox' class='check' style='cursor:pointer' onclick='setNodosCount();borrarCatalogo();'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private string obtenerOF(string sAnomes, string sNodos)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            string sChkBorrado = "", sChkDecalaje = "";
            sb.Append("<table id='tblDatos' style='width:1230px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:380px;' />");
            sb.Append("<col style='width:320px;' />");
            sb.Append("<col style='width:320px;' />");
            sb.Append("<col style='width:90px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = DATOECO.ObtenerOrdenesFacturacionBorradoDecalaje(int.Parse(sAnomes), sNodos);

            while (dr.Read())
            {
                sChkBorrado = "";
                sChkDecalaje = "";
                sb.Append("<tr idDE='" + dr["t376_iddatoeco"].ToString() + "' "); //id Dato Económico
                sb.Append("idSM='" + dr["t325_idsegmesproy"].ToString() + "' "); //id SegMes
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' "); //

                sb.Append("linea=\"" + Utilidades.escape(dr["linea"].ToString()) + "\" ");
                sb.Append("cr=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["responsable"].ToString()) + "\" ");
                sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString()) + "\" ");
                sb.Append("moneda=\"" +  Utilidades.escape(dr["t610_moneda"].ToString()) + "\" ");
                sb.Append("desfactura=\"" + Utilidades.escape(dr["DestFactura"].ToString()) + "\" ");
                sb.Append("tramitador=\"" + Utilidades.escape(dr["Tramitador"].ToString()) + "\" ");

                sb.Append("style='height:20px;'>");

                sb.Append("<td><nobr class='NBR W375' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString() + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString() + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "&nbsp;&nbsp;&nbsp;" + dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t329_denominacion"].ToString() + "</td>");
                sb.Append("<td>" + dr["t376_motivo"].ToString() + "</td>");
                sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["t376_importe"].ToString()).ToString("N") + "</td>");

                if ((int)dr["t376_iddatoeco_facturacion"] > 0)
                {
                    sChkBorrado = "checked";
                    sChkDecalaje = "";
                }else{
                    sChkBorrado = "";
                    sChkDecalaje = "checked";
                }
                sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' style='cursor:pointer' onclick='this.parentNode.nextSibling.children[0].checked=false;countBorDec();' " + sChkBorrado + "></td>");
                sb.Append("<td style=\"border-right:'';text-align:center;\"><input type='checkbox' class='check' style='cursor:pointer' onclick='this.parentNode.previousSibling.children[0].checked=false;countBorDec();' " + sChkDecalaje + "></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + DATOECO.ObtenerOrdenesFacturacionMes(int.Parse(sAnomes)).ToString("#,###");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las órdenes de facturación.", ex);
        }
    }
    private string procesarOF(string sAnomes, string strDatos)
    {
        string sResul = "";
        string sEstadoMes = "";
        int nSMP = 0;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
        }
        #endregion

        try
        {
            string[] aOrdenes = Regex.Split(strDatos, "///");

            foreach (string oOrden in aOrdenes)
            {
                if (oOrden == "") continue;

                //0. t376_iddatoeco
                //1. t325_idsegmesproy
                //2. t305_idproyectosubnodo
                //3. Opcion: B -> Borrado, D -> Decalaje

                string[] aValores = Regex.Split(oOrden, "##");
                if (aValores[3] == "B")
                {
                    DATOECO.Delete(tr, int.Parse(aValores[0]));
                }
                else
                {
                    //1º Obtener el siguiente idsegmesproy al actual.
                    nSMP = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, int.Parse(aValores[2]), Fechas.AddAnnomes(int.Parse(sAnomes), 1));
                    if (nSMP == 0)
                    {
                        sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, int.Parse(aValores[2]), Fechas.AddAnnomes(int.Parse(sAnomes), 1));
                        nSMP = SEGMESPROYECTOSUBNODO.Insert(tr, int.Parse(aValores[2]), Fechas.AddAnnomes(int.Parse(sAnomes), 1), sEstadoMes, 0, 0, false, 0, 0);
                    }
                    DATOECO.UpdateDecalaje(tr, int.Parse(aValores[0]), nSMP);
                }
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de avance", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
