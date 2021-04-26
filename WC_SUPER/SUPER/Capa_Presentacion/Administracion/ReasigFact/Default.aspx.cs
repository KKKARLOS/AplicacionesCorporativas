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
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.TituloPagina = "Reasignación de facturas";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            try
            {
                if (Session["OCULTAR_AUDITORIA"].ToString() == "1")
                {
                    this.cldAuditoria.Visible = false;
                    this.btnAuditoria.Visible = false;
                }
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("getLineas"):
                sResultado += getLineas(aArgs[1], aArgs[2]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
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

    private string getLineas(string sSerie, string sNumero)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            decimal dTotFact = 0, dTotCobro = 0;
            string sHayCobros = "F", sAnoMes="", sClaseEco="";

            int iAnoMes = Factura.GetAnoMes(sSerie, int.Parse(sNumero));
            int i = 0;
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(" aTipo = new Array();");
            //SqlDataReader dr2 = TIPOCAMBIOMENSUAL.ListaMes(sSerie, sNumero);
            SqlDataReader dr2 = TIPOCAMBIOMENSUAL.ListaMes(iAnoMes);
            while (dr2.Read())
            {
                sb2.Append("aTipo[" + i.ToString() + "] = {idM:\"" + dr2["t422_idmoneda"].ToString() + "\"," +
                                "tc:\"" + dr2["t699_tipocambio"].ToString() + "\"};");//\n
                i++;
            }

            SqlDataReader dr = Factura.LineasYCobros(sSerie, int.Parse(sNumero));

            sb.Append("<table id='tblDatos' class='texto MANO' style='width: 950px;' mantenimiento='1'>");
            //sb.Append("<colgroup><col style='width:10px;' /><col style='width:290px;' /><col style='width:100px;text-align:right;' />");
            //sb.Append("<col style='width:100px;text-align:right;' /><col style='width:30px;text-align:right;' />");
            //sb.Append("<col style='width:20px;text-align:right;padding-right:3px;' /><col style='width:120px;' /><col style='width:120px;' />");
            //sb.Append("<col style='width:160px;' /></colgroup>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:275px;' /><col style='width:80px;' />");
            sb.Append("<col style='width:80px;' /><col style='width:80px;' />");
            sb.Append("<col style='width:80px;' /><col style='width:30px;' />");
            sb.Append("<col style='width:25px;' /><col style='width:25px;' />");
            sb.Append("<col style='width:95px;' /><col style='width:100px;' /><col style='width:70px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            while (dr.Read())
            {
                sHayCobros = "T";
                dTotFact += decimal.Parse(dr["importeEU"].ToString());
                dTotCobro += decimal.Parse(dr["cobroEU"].ToString());
                if (sAnoMes == "") sAnoMes = dr["t325_anomes"].ToString();
                if (sClaseEco == "") sClaseEco = dr["t329_idclaseeco"].ToString();
                sb.Append("<tr id='" + dr["t376_iddatoeco"].ToString() + "' iPSN='" + dr["t305_idproyectosubnodo"].ToString() + "'");
                sb.Append(" cat='" + dr["t301_categoria"].ToString() + "'");
                sb.Append(" cua='" + dr["t305_cualidad"].ToString() + "'");
                sb.Append(" est='" + dr["t301_estado"].ToString() + "'");
                sb.Append(" mp='" + dr["t422_denominacionimportes"].ToString() + "'");
                sb.Append(" bd='' onclick='mm(event)' style='height:20px'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td>");
                switch (dr["t301_categoria"].ToString())
                {
                    case "P":
                        sb.Append("<img src='../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                    default:
                        sb.Append("<img src='../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                }
                switch (dr["t305_cualidad"].ToString())
                {
                    case "C":
                        sb.Append("<img src='../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                    case "J":
                        sb.Append("<img src='../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                    case "P":
                        sb.Append("<img src='../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                }
                switch (dr["t301_estado"].ToString())
                {
                    case "A":
                        sb.Append("<img src='../../../images/imgIconoProyAbierto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                    case "C":
                        sb.Append("<img src='../../../images/imgIconoProyCerrado.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                    case "H":
                        sb.Append("<img src='../../../images/imgIconoProyHistorico.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                    case "P":
                        sb.Append("<img src='../../../images/imgIconoProyPresup.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
                        break;
                }

                sb.Append("<nobr class='NBR W190' style='margin-left:5px;' ");
                //sb.Append("title='" + dr["t301_denominacion"].ToString());
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString());
                sb.Append("</nobr></td>");

                sb.Append("<td style='text-align:right;'><input type='text' class='txtNumL' style='width:80px;' value=\"" + decimal.Parse(dr["importeEU"].ToString()).ToString("N") + "\" onKeyUp='vtn(event);fm(event);recTotal();setImpMoneda(this);' onfocus='fn(this)'></td>");
                sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["importeMP"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["cobroEU"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:3px;'>" + decimal.Parse(dr["cobroMP"].ToString()).ToString("N") + "</td>");
                sb.Append("<td title='" + dr["t422_denominacionimportes"].ToString() + "'>" + dr["t422_idmoneda"].ToString() + "</td>");

                sb.Append("<td style='text-align:right;'>" + dr["t325_anomes"].ToString().Substring(0, 4) + "</td>");
                sb.Append("<td style='text-align:right;padding-right:3px;'>" + dr["t325_anomes"].ToString().Substring(4, 2) + "</td>");

                //sb.Append("<td title='" + dr["denCliProy"].ToString() + "'>");
                sb.Append("<td title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Cód.Ext.:</label>" + dr["codCliProy"].ToString() + "<br><label style='width:60px;'>Cliente:</label>" + dr["denCliProy"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("><nobr class='NBR W100' style='margin-left:5px;'>" + dr["denCliProy"].ToString() + "</nobr></td>");
                //sb.Append("<td title='" + dr["denCliFact"].ToString() + "'><nobr class='NBR W100' style='margin-left:5px;'>" + dr["denCliFact"].ToString() + "</nobr></td>");
                sb.Append("<td title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Cód.Ext.:</label>" + dr["codCliFact"].ToString() + "<br><label style='width:60px;'>Cliente:</label>" + dr["denCliFact"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("><div class='NBR W100' style='margin-left:5px;'>" + dr["denCliFact"].ToString() + "</div></td>");
                
                sb.Append("<td title='"+dr["t376_motivo"].ToString()+"'><div class='NBR W70' style='margin-left:0px;'>" + dr["t376_motivo"].ToString() + "</div></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sHayCobros + "@#@" + dTotFact.ToString("N") + "@#@" + dTotCobro.ToString("N") + "@#@" + sAnoMes + "@#@" + sClaseEco+ "@#@" + sb2.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las líneas de la factura.", ex);
        }
    }
    protected string Grabar(string sSerie, string sNumero, string sImpTot, string sAnoMes, string sClaseEco, string strLineas)
    {
        string sResul = "", sDesc = "", t376_motivo="";//, sElementosInsertados = "";
        int idDatoEcoFact, idDatoEcoCob, nAnoMesFact, nAnoMesCobro, idT305, idT325, t329_idclaseeco = -1;
        int? t313_idempresa=null;
        int? t302_idcliente = null;
        DateTime? dtFecha = System.DateTime.Today;
        decimal dTotalFact = decimal.Parse(sImpTot), dImpLinea, dAux, dCobro;
        bool bProrratear = false;
        string sRefCliente = "";
        #region abrir conexión y transacción serializable
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            nAnoMesFact = int.Parse(sAnoMes);
            #region Obtener datos de la primera línea de la factura
            SqlDataReader drLineas = Factura.Lineas(tr, sSerie, int.Parse(sNumero));
            if (drLineas.Read())
            {
                t329_idclaseeco = int.Parse(drLineas["t329_idclaseeco"].ToString());
                t376_motivo = drLineas["t376_motivo"].ToString();
                if (drLineas["t376_fecha"].ToString() != "")
                    dtFecha = DateTime.Parse(drLineas["t376_fecha"].ToString());
                else
                    dtFecha = null;
                if (drLineas["t313_idempresa"].ToString() != "")
                    t313_idempresa = int.Parse(drLineas["t313_idempresa"].ToString());
                else
                    t313_idempresa = null;
                if (drLineas["t302_idcliente"].ToString() != "")
                    t302_idcliente = int.Parse(drLineas["t302_idcliente"].ToString());
                else
                    t302_idcliente = null;

                if (drLineas["t376_refcliente"].ToString() != "")
                    sRefCliente = drLineas["t376_refcliente"].ToString();
            }
            drLineas.Close();
            drLineas.Dispose();
            #endregion
            #region Cargar Cobros agrupados por mes en el array slCobros
            ArrayList slCobros = new ArrayList();
            SqlDataReader drCobros = Factura.CobrosMes(tr, sSerie, int.Parse(sNumero));
            while (drCobros.Read())
            {
                string[] aDatosAux = new string[] {
                    drCobros["t325_anomes"].ToString(),
                    drCobros["cobro"].ToString(),
                    drCobros["fecha"].ToString()
                    };
                slCobros.Add(aDatosAux);
            }
            drCobros.Close();
            drCobros.Dispose();
            #endregion
            #region Actualizar lineas de factura
            string[] aLinea = Regex.Split(strLineas, "///");
            //Para cada linea de factura
            foreach (string oLinea in aLinea)
            {
                if (oLinea == "") continue;

                string[] aValores = Regex.Split(oLinea, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID datoEco
                //2. idT305
                //3. Importe linea factura
                switch (aValores[0])
                {
                    case "I":
                        bProrratear = true;
                        //if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                        //else sElementosInsertados += "//" + nAux.ToString();
                        idT305 = int.Parse(aValores[2]);
                        dImpLinea = decimal.Parse(aValores[3]);
                        idT325 = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, idT305, nAnoMesFact);
                        if (idT325==0)
                            idT325=SEGMESPROYECTOSUBNODO.InsertSiNoExiste(tr, idT305, nAnoMesFact);
                        idDatoEcoFact = DATOECO.InsertFactura(tr, idT325, int.Parse(sClaseEco), t376_motivo, dImpLinea, null, null,
                                                              dtFecha, sSerie, int.Parse(sNumero), t313_idempresa, t302_idcliente, null, sRefCliente);
                        break;
                    case "U":
                        bProrratear = true;
                        idDatoEcoFact = int.Parse(aValores[1]);
                        dImpLinea = decimal.Parse(aValores[3]);
                        DATOECO.UpdateImporte(tr, idDatoEcoFact, dImpLinea);
                        break;
                    case "D":
                        bProrratear = true;
                        DATOECO.Delete(tr, int.Parse(aValores[1]));
                        break;
                }
            }
            #endregion
            #region cobros
            //Reparto lo cobrado entre las lineas de factura resultantes
            if (bProrratear)
            {
                #region Obtener datos de las líneas de la factura
                ArrayList slLineas = new ArrayList();
                SqlDataReader drLineas2 = Factura.Lineas(tr, sSerie, int.Parse(sNumero));
                while (drLineas2.Read())
                {
                    string[] aDatosAux = new string[] {
                    drLineas2["t305_idproyectosubnodo"].ToString(),
                    drLineas2["t376_iddatoeco"].ToString(),
                    drLineas2["t376_importe"].ToString()};
                    slLineas.Add(aDatosAux);
                }
                drLineas2.Close();
                drLineas2.Dispose();
                #endregion
                //Para cada mes con cobro
                for (int iFilaCob = 0; iFilaCob < slCobros.Count; iFilaCob++)
                {
                    nAnoMesCobro = int.Parse(((string[])slCobros[iFilaCob])[0]);
                    dCobro = decimal.Parse(((string[])slCobros[iFilaCob])[1]);
                    if (((string[])slCobros[iFilaCob])[2] != "")
                        dtFecha = DateTime.Parse(((string[])slCobros[iFilaCob])[2]);
                    else
                        dtFecha = null;
                    //Para cada linea de factura
                    for (int iFilaFac = 0; iFilaFac < slLineas.Count; iFilaFac++)
                    {
                        idT305 = int.Parse(((string[])slLineas[iFilaFac])[0]);
                        idDatoEcoFact = int.Parse(((string[])slLineas[iFilaFac])[1]);
                        dImpLinea = decimal.Parse(((string[])slLineas[iFilaFac])[2]);
                        dAux = (dImpLinea / dTotalFact) * dCobro;
                        //Miro si hay cobro para esa linea de factura
                        idDatoEcoCob = DATOECO.ExisteCobro(tr, nAnoMesCobro, idDatoEcoFact);
                        if (idDatoEcoCob == 0)
                        {
                            idT325 = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, idT305, nAnoMesCobro);
                            if (idT325 == 0)
                                idT325 = SEGMESPROYECTOSUBNODO.InsertSiNoExiste(tr, idT305, nAnoMesCobro);
                            DATOECO.InsertCobro(tr, idT325, dAux, dtFecha, idDatoEcoFact, sSerie, int.Parse(sNumero));
                        }
                        else
                            DATOECO.UpdateImporte(tr, idDatoEcoCob, dAux);

                    }
                }
            }
            #endregion
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";// +sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la factura.", ex, false) + "@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    private string recuperarPSN(string sT305IdProy)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy(int.Parse(sT305IdProy));
            if (dr.Read())
            {
                sb.Append(sT305IdProy + "@#@");
                sb.Append(dr["t301_estado"].ToString() + "@#@");
                sb.Append(dr["t301_categoria"].ToString() + "@#@");  
                sb.Append(dr["t305_cualidad"].ToString() + "@#@");  
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " ");
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");
                sb.Append(dr["t422_idmoneda"].ToString() + "@#@");
                sb.Append(dr["denMoneda"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }

}
