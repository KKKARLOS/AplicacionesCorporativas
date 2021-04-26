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

using System.Threading;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sOrigen = "";
    public string sOpcion = "";
    public string strTablaHTML = "";
    public string strArrayNodos = "";
    public string sAnoMesPropuesto = "";
    public ArrayList aProyectos = new ArrayList();
    public string[] aMeses = null;
    public string sPSNUsuario = "";
    //public string sValidaciones = "false";
    private bool bHayPreferencia = false;
    public string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLProyecto = "";
    public string strIDsAmbito = "", strIDsResponsable = "", strIDsProyecto = "";
    ArrayList aSubnodos = new ArrayList();
    public string sCriterios = "", sSubnodos = "", sHayPreferencia = "false";//, sCargarCriterios = "true";
    public short nPantallaPreferencia = 34;
    public string sIdSegMesProyGenerarDialogos = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 33;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Cierre mensual";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");

                try
                {
                    if (Request.QueryString["origen"].ToString() == "QURN")//ADM
                    {
                        sOrigen = Utilidades.decodpar(Request.QueryString["origen"].ToString());
                        hdnAnoMesPropuesto.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                        DateTime dFechaLimite = DateTime.Parse("05/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                        if (DateTime.Today <= dFechaLimite) hdnAnoMesPropuesto.Text = (DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month).ToString();

                        string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");

                        if (bHayPreferencia && aDatosPref[0] == "OK")
                        {
                            sHayPreferencia = "true";
                            chkCerrarAuto.Checked = (aDatosPref[2] == "1") ? true : false;
                            chkActuAuto.Checked = (aDatosPref[3] == "1") ? true : false;
                            //if (chkActuAuto.Checked) btnObtener.Disabled = true;
                            if (aDatosPref[4] == "1") rdbOperador.Items[0].Selected = true;
                            else rdbOperador.Items[1].Selected = true;

                            sSubnodos = aDatosPref[5];

                            if (chkActuAuto.Checked)
                            {
                                string strTabla = getProyectosCierre(true, "", this.hdnAnoMesPropuesto.Text, strIDsResponsable, 
                                                                     sSubnodos, strIDsProyecto, sOrigen, aDatosPref[4],"N");
                                string[] aTabla = Regex.Split(strTabla, "@#@");
                                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                                else Master.sErrores += Errores.mostrarError(aTabla[1]);
                            }
                        }
                        else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);
                    }
                    else
                    {
                        sOrigen = Request.QueryString["origen"];
                        sOpcion = Request.QueryString["opcion"];

                        //sPSNUsuario = (sOrigen == "carrusel" || (sOrigen == "proynocerrados" && Request.QueryString["opcion"] == "cerrarproy")) ? Request.QueryString["nPSN"] : "";
                        if (sOrigen == "carrusel" || (sOrigen == "proynocerrados" && sOpcion == "cerrarproy"))
                        {
                            sPSNUsuario = Request.QueryString["nPSN"];
                        }
                        else
                        {
                            if (sOrigen == "proynocerrados" && sOpcion == "cerrarlista")
                                sPSNUsuario = Request.QueryString["lp"];
                            else
                                sPSNUsuario = "";
                        }
                        //string strTabla = getProyectosCierre(false, (sOrigen == "carrusel" || (sOrigen == "proynocerrados" && Request.QueryString["opcion"] == "cerrarproy")) ? Request.QueryString["nPSN"] : "", "", "", "", "", sOrigen);
                        //string[] aTabla = Regex.Split(strTabla, "@#@");
                        //if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                        //else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
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
            case ("setCierre"):
                sResultado += procesarCierre(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("refrescar"):
                sResultado += getProyectosCierre(false, "", "", "", "", "", "", "","N");
                break;
            case ("buscar"):
                sResultado += getProyectosCierre(true, "", aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6],"N");
                break;
            case ("buscarUsuario"):
                sResultado += getProyectosCierre(false, aArgs[1], "", "", "", "", aArgs[2], "","N");
                break;
            case ("setProyCierre"):
                sResultado += getProyectosCierre(false, aArgs[1], "", "", "", "", aArgs[2], "","S");
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
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

    private string getProyectosCierre(bool bADM, string nPSN, string sAnomes, string sResponsables, string sSubnodos,
                                      string sPSN, string sOrigen, string sComparacionLogica, string bListaProy)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            Hashtable htAjustes = new Hashtable();
            SqlDataReader dr = null;
            bool bMesesOK = true, bProcesable = true;
            DateTime? oDT1 = null, oDT2 = null, oDT3 = null, oDT4 = null;
            int nTiempoBD = 0;
            int nTiempoHTML = 0;
            int nTiempoDialogos = 0;

            sb.Append("<TABLE class='texto' id='tblDatos' style='width: 974px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:140px;' />");
            sb.Append("<col style='width:35px;' />");
            sb.Append("<col style='width:55px;' />");
            sb.Append("<col style='width:54px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:55px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:25px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            bool bCarrusel = false;
            if (sOrigen == "carrusel")
            {
                bCarrusel = (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) ? true : false;
            }

            oDT1 = DateTime.Now;
            if (bListaProy == "S")
            {
                dr = PROYECTOSUBNODO.ObtenerProyectosACerrar(nPSN);
            }
            else
            {
                if (!bADM)
                {
                    if (sOrigen == "menucierresat" || sOrigen == "menucierresatsaa")
                        dr = PROYECTOSUBNODO.ObtenerProyectosACerrarUSA((int)Session["UsuarioActual"], (sOrigen == "menucierresatsaa") ? true : false);
                    else
                        dr = PROYECTOSUBNODO.ObtenerProyectosACerrar((int)Session["UsuarioActual"], (nPSN == "") ? null : (int?)int.Parse(nPSN), bCarrusel);
                }
                else
                    dr = PROYECTOSUBNODO.ObtenerProyectosACerrarADM(int.Parse(sAnomes), sResponsables, sSubnodos, sPSN, (sComparacionLogica == "1") ? true : false);
            }
            oDT2 = DateTime.Now;
            string s_idsegmesproy = "";
            while (dr.Read())
            {
                bMesesOK = true;
                bProcesable = true;

                aProyectos.Add(dr["t301_idproyecto"].ToString());
                sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("anomes='" + dr["t325_anomes"].ToString() + "' ");
                //sb.Append("ajuste='" + dr["ajuste"].ToString() + "' ");
                //sb.Append("importe_ajuste='" + dr["Importe_Ajuste"].ToString() + "' ");
                sb.Append("nSegMes='" + dr["t325_idsegmesproy"].ToString() + "' ");

                //if ( (byte)dr["monedas_proyectos_contrato"] > 1
                //    || (dr["t306_idcontrato"] != DBNull.Value && dr["t422_idmoneda_proyecto"].ToString() != dr["t422_idmoneda_contrato"].ToString()))
                //{//Por ahora no permitimoes cerrar este caso ni a los administradores.
                //    sb.Append("a_procesar='0' ");
                //    bProcesable = false;
                //}
                //else 
                if (bADM)
                {
                    sb.Append("a_procesar='1' ");
                    s_idsegmesproy += dr["t325_idsegmesproy"].ToString()+",";
                }
                else if (dr["faltan_CEC_obligatorios"].ToString() == "1"
                    || dr["faltan_CED_obligatorios"].ToString() == "1"
                    || dr["faltan_Cualificadores_obligatorios"].ToString() == "1"
                    || (Fechas.AddAnnomes(int.Parse(dr["T303_ultcierreeco"].ToString()), 1) != int.Parse(dr["t325_anomes"].ToString()) && sOrigen != "carrusel")
                    //|| ( (byte)dr["monedas_proyectos_contrato"] > 1 )
                    //|| (dr["t306_idcontrato"] != DBNull.Value && dr["t422_idmoneda_proyecto"].ToString() != dr["t422_idmoneda_contrato"].ToString())
                    )
                {
                    sb.Append("a_procesar='0' ");
                    bProcesable = false;
                }
                else
                {
                    sb.Append("a_procesar='1' ");
                    s_idsegmesproy += dr["t325_idsegmesproy"].ToString() + ",";
                }

                sb.Append("style='height:20px' procesado=''>");

                if (dr["ajuste"].ToString() == "1")
                    this.hdnExcepcion.Text = "1";

                if (dr["t305_cualidad"].ToString() == "C") sb.Append("<td style=\"border-right:none\"><img src='../../../images/imgIconoContratante.gif' width='16px' height='16px' /></td>");
                else sb.Append("<td style=\"border-right:none\"><img src='../../../images/imgIconoRepPrecio.gif' width='16px' height='16px' /></td>");

                sb.Append("<td style=\"border-right:none; padding-right:5px;text-align:right;\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W135' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:75px'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>Categoría:</label>" + ((dr["t301_categoria"].ToString() == "P") ? "Producto" : "Servicio") + "<br><label style='width:75px'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>Moneda:</label>" + dr["t422_denominacion_proyecto"].ToString() + "] hideselects=[off]\">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                sb.Append("<td style='padding-left:2px;'>" + dr["t422_idmoneda_proyecto"].ToString() + "</td>");
                sb.Append("<td style='padding-left:1px;'>" + Fechas.AnnomesAFechaDescCorta(int.Parse(dr["T303_ultcierreeco"].ToString())) + "</td>");

                if (Fechas.AddAnnomes(int.Parse(dr["T303_ultcierreeco"].ToString()), 1) != int.Parse(dr["t325_anomes"].ToString()))
                {
                    sb.Append("<td style='color:red;padding-left:1px;'>");
                    bMesesOK = false;
                }
                else sb.Append("<td style='padding-left:1px;'>");
                sb.Append(Fechas.AnnomesAFechaDescCorta(int.Parse(dr["t325_anomes"].ToString())) + "</td>");

                sb.Append("<td style='text-align:right;padding-right:2px;'>" + decimal.Parse(dr["consumoIAP"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:2px;'>" + decimal.Parse(dr["consumoPGE"].ToString()).ToString("N") + "</td>");

                if (bMesesOK) sb.Append("<td style=\"border-right:none;text-align:center;padding-right:2px;\"></td>");
                else
                {
                    if (sOrigen == "carrusel") sb.Append("<td style=\"border-right:none;text-align:center;padding-right:2px;\"><img src='../../../Images/imgCalAma.gif' /></td>");
                    else sb.Append("<td style=\"border-right:none;text-align:center;padding-right:2px;\"><img src='../../../Images/imgCalRojo.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }

                if (dr["faltan_CEC_obligatorios"].ToString() == "1" || dr["faltan_CED_obligatorios"].ToString() == "1")
                {
                    sb.Append("<td style=\"border-right:none;text-align:center;\"><img src='../../../Images/imgIconoObl16.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=\"border-right:none;text-align:center;\"></td>");

                if (dr["faltan_Cualificadores_obligatorios"].ToString() == "1")
                {
                    sb.Append("<td style=\"border-right:none;text-align:center;\"><img src='../../../Images/imgIconoObl16Azul.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=\"border-right:none;text-align:center;\"></td>");

                if (decimal.Parse(dr["consumonivel"].ToString()) > 0)
                {
                    sb.Append("<td style=\"border-right:none;text-align:center;\"><img src='../../../Images/imgConsNivel.gif' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=\"border-right:none;text-align:center;\"></td>");

                if ( (byte)dr["monedas_proyectos_contrato"] > 1 
                    || (dr["t306_idcontrato"] != DBNull.Value && dr["t422_idmoneda_proyecto"].ToString() != dr["t422_idmoneda_contrato"].ToString())
                    ){
                    string sTitle = "";
                    if ((byte)dr["monedas_proyectos_contrato"] > 1) sTitle += "Los proyectos asociados al contrato tienen monedas diferentes.<br>";
                    if (dr["t306_idcontrato"] != DBNull.Value && dr["t422_idmoneda_proyecto"].ToString() != dr["t422_idmoneda_contrato"].ToString()) sTitle += "Moneda del contrato: " + dr["t422_denominacion_contrato"].ToString() + "\nMoneda del proyecto: " + dr["t422_denominacion_proyecto"].ToString() + "\n";

                    if (dr["ajuste"].ToString() == "1")
                    {
                        if ((string)htAjustes[dr["t301_categoria"].ToString() + dr["t306_idcontrato"].ToString()] == null)
                        {
                            htAjustes.Add(dr["t301_categoria"].ToString() + dr["t306_idcontrato"].ToString(),
                                            dr["t301_idproyecto"].ToString()
                                             );
                        }
                        sTitle += "Importe de ajuste: " + decimal.Parse(dr["Importe_Ajuste"].ToString()).ToString("N") + " " + dr["t422_denominacionimportes_proyecto"].ToString();
                        this.hdnExcepcion.Text = "1";
                    }
                    sb.Append("<td style=\"border-right:none;text-align:center;\"><img src='../../../Images/imgExclamacion.png' title='" + sTitle + "' /></td>");
                }
                else if (dr["ajuste"].ToString() == "1" && bProcesable)   //si hay ajuste y no hay excepción que impida cerrar
                {
                    if ((string)htAjustes[dr["t301_categoria"].ToString() + dr["t306_idcontrato"].ToString()] == null)
                    {
                        htAjustes.Add(dr["t301_categoria"].ToString() + dr["t306_idcontrato"].ToString(),
                                        dr["t301_idproyecto"].ToString()
                                         );
                        sb.Append("<td style=\"border-right:none;text-align:center;\"><img src='../../../Images/imgAjuste2.gif' title='Importe de ajuste: " + decimal.Parse(dr["Importe_Ajuste"].ToString()).ToString("N") + "' /></td>");
                        this.hdnExcepcion.Text = "1";
                    }
                    else sb.Append("<td style=\"border-right:none;text-align:center;\"></td>");
                }
                else sb.Append("<td style=\"border-right:none;text-align:center;\"></td>");

                if (decimal.Parse(dr["t325_consperiod"].ToString()) != 0)
                {
                    sb.Append("<td style=\"border-right:none;text-align:center;\"><img src='../../../Images/imgConsumoPeriod.gif' title='Consumos periodificados: " + decimal.Parse(dr["t325_consperiod"].ToString()).ToString("N") + "' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td style=\"border-right:none;text-align:center;\"></td>");
                if (decimal.Parse(dr["t325_prodperiod"].ToString()) != 0)
                {
                    sb.Append("<td style='text-align:center;'><img src='../../../Images/imgProduccionPeriod.gif' title='Producción periodificada: " + decimal.Parse(dr["t325_prodperiod"].ToString()).ToString("N") + "' /></td>");
                    this.hdnExcepcion.Text = "1";
                }
                else sb.Append("<td></td>");

                if (dr["t306_idcontrato"].ToString() == "" || dr["t305_cualidad"].ToString() != "C") sb.Append("<td style='text-align:right;'></td>");
                else sb.Append("<td style='text-align:right;'>" + int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###") + "</td>");

                if (dr["t305_cualidad"].ToString() != "C") sb.Append("<td style='text-align:right;'></td>");
                else sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["TotalContrato"].ToString()).ToString("N") + "</td>");
                
                sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["TotalProducido"].ToString()).ToString("N") + "</td>");

                if (dr["t305_cualidad"].ToString() != "C") sb.Append("<td style='text-align:right;'></td>");
                else sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["TPPAC"].ToString()).ToString("N") + "</td>");

                if (bProcesable) sb.Append("<td style=\"border-right:none\"><img src='../../../Images/imgMesAbierto.gif' /></td>");
                else sb.Append("<td style=\"border-right:none;padding-left:3px;text-align:center;\"><img src='../../../Images/imgMesNoProceso.gif' /></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            oDT3 = DateTime.Now;
            nTiempoBD = Fechas.DateDiff("mm", (DateTime)oDT1, (DateTime)oDT2);
            nTiempoHTML = Fechas.DateDiff("mm", (DateTime)oDT2, (DateTime)oDT3);
            //Obtengo las alertas producidas por los proyectos a cerrar.
            //s_idsegmesproy
            int nAlertas = 0;
            if (s_idsegmesproy != "")
            {
                DataSet ds = SUPER.Capa_Datos.SEGMESPROYECTOSUBNODO.ObtenerDialogosDeAlertasDS(null, s_idsegmesproy, true);
                Session["DS_ALERTASCIERRE"] = ds;
                nAlertas = ds.Tables[0].Rows.Count;
            }
            oDT4 = DateTime.Now;
            nTiempoDialogos = Fechas.DateDiff("mm", (DateTime)oDT3, (DateTime)oDT4);

            return "OK@#@" + sb.ToString() + "@#@" + this.hdnExcepcion.Text + "@#@" + nAlertas.ToString() + "@#@"
                            + nTiempoBD.ToString() + "@#@"
                            + nTiempoHTML.ToString() + "@#@"
                            + nTiempoDialogos.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos.", ex);
        }
    }
    private string procesarCierre(string sOrigen, string sAnomesADM, string sPSN, string sCualidad, string sSegMesProy, string sIDProyecto, string sAnomes, string sIdSegMesProy)
    {
        string sResul = "";
        string sEstadoMes = "";
        bool bReintentar = true, bHayQueAjustar = false;
        decimal nImporteAjuste = 0;

        #region apertura de conexión y transacción
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
            //comprobar que no se hayan abierto meses anteriores.
            if (sOrigen == "ADM" || int.Parse(sAnomes) == PROYECTOSUBNODO.ObtenerPrimerMesAbierto(tr, int.Parse(sPSN)))
            {
                if (sCualidad == "C")
                {
                    SEGMESPROYECTOSUBNODO.GenerarMesEnTransaccion(tr, int.Parse(sIDProyecto));

                    SqlDataReader dr = SEGMESPROYECTOSUBNODO.ObtenerAjuste(tr, int.Parse(sSegMesProy));
                    if (dr.Read())
                    {
                        bHayQueAjustar = ((int)dr["ajuste"] == 1) ? true : false;
                        nImporteAjuste = decimal.Parse(dr["Importe_Ajuste"].ToString());
                    }
                    dr.Close();
                    dr.Dispose();

                    if (bHayQueAjustar)
                    {
                        //buscar el mes máximo para ese PSN y crear uno posterior para el ajuste
                        int nUltAnomes = PROYECTOSUBNODO.ObtenerUltimoMes(tr, int.Parse(sPSN));
                        sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, int.Parse(sPSN), Fechas.AddAnnomes(nUltAnomes, 1));
                        int nNuevoSegMes = SEGMESPROYECTOSUBNODO.Insert(tr, int.Parse(sPSN), Fechas.AddAnnomes(nUltAnomes, 1), sEstadoMes, 0, 0, false, 0, 0);

                        DATOECO.Insert(tr, nNuevoSegMes, Constantes.AjusteProdCont, "Ajuste de producción y contratación", nImporteAjuste, null, null, null);
                    }
                }

                if (sOrigen == "ADM") SEGMESPROYECTOSUBNODO.CerrarMesADM(tr, int.Parse(sPSN), int.Parse(sAnomesADM));
                else SEGMESPROYECTOSUBNODO.Cerrar(tr, int.Parse(sSegMesProy));

                sResul = "OK@#@";
            }
            else
            {
                sResul = "OK@#@NO";
            }

            Conexion.CommitTransaccion(tr);

            if ((bool)Session["ALERTASPROY_ACTIVAS"] && sIdSegMesProy != "")
            {
                try
                {
                    sIdSegMesProyGenerarDialogos = sIdSegMesProy;
                    ThreadStart ts = new ThreadStart(GenerarDialogos);
                    Thread workerThread = new Thread(ts);
                    workerThread.Start();
                    //SEGMESPROYECTOSUBNODO.GenerarDialogosDeAlertas(sIdSegMesProy);
                }
                catch (Exception) { }
            }
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar el cierre.", ex, bReintentar);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

    private void GenerarDialogos()
    {
        SEGMESPROYECTOSUBNODO.GenerarDialogosDeAlertas(sIdSegMesProyGenerarDialogos, false);
    }

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

    private string setPreferencia(string sCerrarAuto, string sActuAuto, string sOperadorLogico, string sValoresMultiples)
    {
        string sResul = "";

        #region abrir conexión y transacción
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
            int nPref = PREFERENCIAUSUARIO.Insertar(tr,
                                        (int)Session["IDFICEPI_PC_ACTUAL"], 34,
                                        null,// sCategoria,
                                        null,// sCualidad,
                                        (sCerrarAuto == "") ? null : sCerrarAuto,
                                        (sActuAuto == "") ? null : sActuAuto,
                                        (sOperadorLogico == "") ? null : sOperadorLogico,
                                        null,// sOpcionPeriodo,
                                        null,// sEstado,
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            #region Valores Múltiples
            if (sValoresMultiples != "")
            {
                string[] aValores = Regex.Split(sValoresMultiples, "///");
                foreach (string oValor in aValores)
                {
                    if (oValor == "") continue;
                    string[] aDatos = Regex.Split(oValor, "##");
                    ///aDatos[0] = concepto
                    ///aDatos[1] = idValor
                    ///aDatos[2] = denominacion

                    PREFUSUMULTIVALOR.Insertar(tr, nPref, byte.Parse(aDatos[0]), aDatos[1], Utilidades.unescape(aDatos[2]));
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + nPref.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string delPreferencia()
    {
        try
        {
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 34);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar las preferencias", ex);
        }
    }
    private string getPreferencia(string sIdPrefUsuario)
    {
        StringBuilder sb = new StringBuilder();
        int idPrefUsuario = 0;
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                        (int)Session["IDFICEPI_PC_ACTUAL"], 34);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //2
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //3
                sb.Append(dr["OperadorLogico"].ToString() + "@#@"); //4
                idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
            }
            dr.Close();
            dr.Dispose();

            #region HTML, IDs
            int nNivelMinimo = 0;
            bool bAmbito = false;
            string[] aID = null;
            dr = PREFUSUMULTIVALOR.Obtener(null, idPrefUsuario);
            while (dr.Read())
            {
                switch (int.Parse(dr["t441_concepto"].ToString()))
                {
                    case 1:
                        if (!bAmbito)
                        {
                            bAmbito = true;
                            nNivelMinimo = 6;
                        }
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (int.Parse(aID[0]) < nNivelMinimo) nNivelMinimo = int.Parse(aID[0]);

                        if (strIDsAmbito != "") strIDsAmbito += ",";
                        strIDsAmbito += aID[1];

                        aSubnodos = PREFUSUMULTIVALOR.SelectSubnodosAmbito(null, aSubnodos, int.Parse(aID[0]), int.Parse(aID[1]));
                        strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:16px;' idAux='";
                        strHTMLAmbito += SUBNODO.fgGetCadenaID(aID[0], aID[1]);
                        strHTMLAmbito += "'><td>";

                        switch (int.Parse(aID[0]))
                        {
                            case 1: strHTMLAmbito += "<img src='../../../images/imgSN4.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 2: strHTMLAmbito += "<img src='../../../images/imgSN3.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 3: strHTMLAmbito += "<img src='../../../images/imgSN2.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 4: strHTMLAmbito += "<img src='../../../images/imgSN1.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 5: strHTMLAmbito += "<img src='../../../images/imgNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            case 6: strHTMLAmbito += "<img src='../../../images/imgSubNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLAmbito += "<nobr class='NBR W230'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    case 2:
                        if (strIDsResponsable != "") strIDsResponsable += ",";
                        strIDsResponsable += dr["t441_valor"].ToString();
                        strHTMLResponsable += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                    //case 16:
                    case 3:
                        aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                        if (strIDsProyecto != "") strIDsProyecto += ",";
                        strIDsProyecto += aID[0];

                        strHTMLProyecto += "<tr id='" + aID[0] + "' style='height:16px;' ";
                        strHTMLProyecto += "categoria='" + aID[1] + "' ";
                        strHTMLProyecto += "cualidad='" + aID[2] + "' ";
                        strHTMLProyecto += "estado='" + aID[3] + "'><td>";

                        if (aID[1] == "P") strHTMLProyecto += "<img src='../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";
                        else strHTMLProyecto += "<img src='../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";

                        switch (aID[2])
                        {
                            case "C": strHTMLProyecto += "<img src='../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "J": strHTMLProyecto += "<img src='../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        switch (aID[3])
                        {
                            case "A": strHTMLProyecto += "<img src='../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "C": strHTMLProyecto += "<img src='../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "H": strHTMLProyecto += "<img src='../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            case "P": strHTMLProyecto += "<img src='../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                        }

                        strHTMLProyecto += "<nobr class='NBR W190' style='margin-left:10px;' onmouseover='TTip(event)'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion

            for (int i = 0; i < aSubnodos.Count; i++)
            {
                if (i > 0) sSubnodos += ",";
                sSubnodos += aSubnodos[i];
            }

            sb.Append(sSubnodos + "@#@"); //5
            sb.Append(strHTMLAmbito + "@#@"); //6
            sb.Append(strIDsAmbito + "@#@"); //7
            sb.Append(strHTMLResponsable + "@#@"); //8
            sb.Append(strIDsResponsable + "@#@"); //9
            sb.Append(strHTMLProyecto + "@#@"); //10
            sb.Append(strIDsProyecto ); //11

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
}
