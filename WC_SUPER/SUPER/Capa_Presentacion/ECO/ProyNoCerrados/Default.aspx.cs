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
using EO.Web;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sOrigen = "";

    public string strTablaHTML = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.nBotonera = 50;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Proyectos no cerrados en un mes";
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                
            if (!Page.IsPostBack)
            {
                if (!Page.IsCallback)
                {
                    try
                    {
                        hdnAnoMesPropuesto.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                        DateTime dFechaLimite = DateTime.Parse("05/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                        if (DateTime.Today <= dFechaLimite) hdnAnoMesPropuesto.Text = (DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month).ToString();

                        //string strTabla = getProyectosNoCerrados(int.Parse(hdnAnoMesPropuesto.Text));
                        //string[] aTabla = Regex.Split(strTabla, "@#@");
                        //if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                        //else Master.sErrores += Errores.mostrarError(aTabla[1]);
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
            case ("refrescar"):
                sResultado += getProyectosNoCerrados(int.Parse(aArgs[1]));
                break;
            case ("setPSN"):
                Session["ID_PROYECTOSUBNODO"] = aArgs[1];
                Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                Session["RTPT_PROYECTOSUBNODO"] = false;
                Session["MONEDA_PROYECTOSUBNODO"] = aArgs[2];

                sResultado += "OK";
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

    private string getProyectosNoCerrados(int iAnoMes)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            bool bMesesOK = true, bProcesable = true;

            sb.Append("<table class='texto' id=tblDatos style='width: 970px;' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:25px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:55px;' />");
            sb.Append("<col style='width:220px;' />");
            sb.Append("<col style='width:55px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:60px;' />");//Contrato
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:25px;' />");
            sb.Append("</colgroup>");
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosNoCerrados((int)Session["UsuarioActual"], iAnoMes);

            while (dr.Read())
            {
                bMesesOK = true;
                bProcesable = true;

                sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("monedaPSN='" + dr["t422_idmoneda_proyecto"].ToString() + "' ");

                sb.Append(" ondblclick='irCarrusel(this)' ");

                if (dr["faltan_CEC_obligatorios"].ToString() == "1"
                    || dr["faltan_CED_obligatorios"].ToString() == "1"
                    || (Fechas.AddAnnomes(int.Parse(dr["T303_ultcierreeco"].ToString()), 1) != int.Parse(dr["t325_anomes"].ToString()) && sOrigen != "carrusel")
                    )
                {
                    bProcesable = false;
                }
                if (bProcesable) sb.Append(" p='1'");
                else sb.Append(" p='0'");
                //sb.Append(" ondblclick='CierreProyecto(this)'  procesado='' ");
                //
                

                sb.Append(" procesado='' ");
                //if (iAnoMes == int.Parse(dr["t325_anomes"].ToString())) sb.Append(" class='MANO' ");
                sb.Append(" class='MA' ");
                sb.Append("style='height:20px;'>");

                if (dr["ajuste"].ToString() == "1") this.hdnExcepcion.Text = "1";
                //Celda 1
                sb.Append("<td style='cursor:pointer;'>");
                if (bProcesable) sb.Append("<input type='checkbox' class='check' style='margin-left:5px;'/>");
                sb.Append("</td>");
                //Celda 2
                if (dr["t305_cualidad"].ToString() == "C") 
                    sb.Append("<td><img src='../../../images/imgIconoContratante.gif' width='16px' height='16px' /></td>");
                else sb.Append("<td><img src='../../../images/imgIconoRepPrecio.gif' width='16px' height='16px' /></td>");
                //Celda 3
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                //Celda 4
                if (iAnoMes != int.Parse(dr["t325_anomes"].ToString())) 
                    sb.Append("<td style='padding-left:3px;' style='color:red'>");
                else sb.Append("<td style='padding-left:3px;'>");

                sb.Append("<nobr class='NBR W210' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:75px'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='irCarrusel(this.parentNode.parentNode)' >" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                //Celda 5
                sb.Append("<td>" + Fechas.AnnomesAFechaDescCorta(int.Parse(dr["T303_ultcierreeco"].ToString())) + "</td>");
                //Celda 6
                if (Fechas.AddAnnomes(int.Parse(dr["T303_ultcierreeco"].ToString()), 1) != int.Parse(dr["t325_anomes"].ToString()))
                {
                    sb.Append("<td style='color:red'>");
                    bMesesOK = false;
                }
                else sb.Append("<td>");
                sb.Append(Fechas.AnnomesAFechaDescCorta(int.Parse(dr["t325_anomes"].ToString())) + "</td>");
                //Celda 7
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(dr["consumoIAP"].ToString()).ToString("N") + "</td>");
                //Celda 8
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + double.Parse(dr["consumoPGE"].ToString()).ToString("N") + "</td>");
                //Celda 9
                if (bMesesOK) sb.Append("<td style=text-align:center;'></td>");
                else
                {
                    if (sOrigen == "carrusel") sb.Append("<td style=text-align:center;'><img src='../../../Images/imgCalAma.gif' /></td>");
                    else sb.Append("<td  style=text-align:center;'><img src='../../../Images/imgCalRojo.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                //Celda 10
                if (dr["faltan_CEC_obligatorios"].ToString() == "1" || dr["faltan_CED_obligatorios"].ToString() == "1")
                {
                    sb.Append("<td style=text-align:center;'><img src='../../../Images/imgIconoObl16.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=text-align:center;'></td>");
                //Celda 11
                if (dr["faltan_Cualificadores_obligatorios"].ToString() == "1")
                {
                    sb.Append("<td style=text-align:center;'><img src='../../../Images/imgIconoObl16Azul.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=text-align:center;'></td>");
                //Celda 12
                if (decimal.Parse(dr["consumonivel"].ToString()) > 0)
                {
                    sb.Append("<td style=text-align:center;'><img src='../../../Images/imgConsNivel.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=text-align:center;'></td>");
                //Celda 13
                if (dr["ajuste"].ToString() == "1")
                {
                    sb.Append("<td style=text-align:center;'><img src='../../../Images/imgAjuste2.gif' title='Importe de ajuste: " + double.Parse(dr["Importe_Ajuste"].ToString()).ToString("N") + " &euro;' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=text-align:center;'></td>");
                //Celda 14
                if (dr["t306_idcontrato"].ToString() == "" || dr["t305_cualidad"].ToString() != "C") 
                    sb.Append("<td style='text-align:right;'></td>");
                else sb.Append("<td style='text-align:right;'>" + int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###") + "</td>");
                //Celda 15
                if (dr["t305_cualidad"].ToString() != "C") sb.Append("<td style='text-align:right;'></td>");
                else sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["TotalContrato"].ToString()).ToString("N") + "</td>");
                //Celda 16
                sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["TotalProducido"].ToString()).ToString("N") + "</td>");
                //Celda 17
                if (dr["t305_cualidad"].ToString() != "C") sb.Append("<td style='text-align:right;'></td>");
                else sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["TPPAC"].ToString()).ToString("N") + "</td>");
                //Celda 18
                if (bProcesable) sb.Append("<td style='text-align:right;padding-right:3px;'><img src='../../../Images/imgMesAbierto.gif' /></td>");
                else sb.Append("<td style='text-align:right;padding-right:3px;'><img src='../../../Images/imgMesNoProceso.gif' /></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + this.hdnExcepcion.Text;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos.", ex);
        }
    }
    //private string procesarCierre(string sPSN, string sCualidad, string sSegMesProy, string sHayAjuste, string sImporteAjuste, string sIDProyecto, string sAnomes)
    //{
    //    string sResul = "";
    //    bool bReintentar = true;

    //    #region Apertura de conexión y transacción
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccionSerializable(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        //comprobar que no se hayan abierto meses anteriores.
    //        if (int.Parse(sAnomes) == PROYECTOSUBNODO.ObtenerPrimerMesAbierto(tr, int.Parse(sPSN)))
    //        {
    //            if (sCualidad == "C")
    //            {
    //                SEGMESPROYECTOSUBNODO.GenerarMes(tr, int.Parse(sIDProyecto));

    //                if (sHayAjuste == "1")
    //                {
    //                    DATOECO.Insert(tr, int.Parse(sSegMesProy), Constantes.AjusteProdCont, "Ajuste de producción y contratación", decimal.Parse(sImporteAjuste), null, null, null);
    //                }
    //            }

    //            SEGMESPROYECTOSUBNODO.Cerrar(tr, int.Parse(sSegMesProy));

    //            sResul = "OK@#@";
    //        }
    //        else
    //        {
    //            sResul = "OK@#@NO";
    //        }


    //        Conexion.CommitTransaccion(tr);
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al procesar el cierre.", ex, bReintentar);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }

    //    return sResul;
    //}

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
}
