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
    public string strTablaHTML = "";
    public string sErrores = "";
    public string sLectura = "false";
    public string sLecturaInsMes = "false";
    public string sModoCoste = "";
    public string sNodo = "";
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
                //string sprueba = Request.QueryString["prueba"].ToString();
                sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                if ((bool)Session["MODOLECTURA_PROYECTOSUBNODO"]) sLecturaInsMes = "true";

                cboRecursos.Items.Add(new ListItem("Todos los asignados al proyecto", "0"));
                cboRecursos.Items.Add(new ListItem("Pertenecientes al " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) +" del proyecto", "1"));
                cboRecursos.Items.Add(new ListItem("Pertenecientes a otros " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) +" de la empresa", "2"));
                cboRecursos.Items.Add(new ListItem("Pertenecientes a otras empresas grupo", "3"));
                cboRecursos.Items.Add(new ListItem("Externos", "4"));
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}

                string sConcepto = Request.QueryString["C"];
                if (sConcepto != "1" && sConcepto != "2" && sConcepto != "3" && sConcepto != "4") sConcepto = "0";
                cboRecursos.SelectedValue = sConcepto;

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
                
               // if (Request.QueryString["nPSN"] != null) hdnIdProyectoSubNodo.Text = Request.QueryString["nPSN"];

                string strTabla = getDatosProfesionales(sConcepto, Request.QueryString["CL"], Request.QueryString["nSegMesProy"], Request.QueryString["sEstadoMes"], Request.QueryString["sEstadoProy"], Request.QueryString["sCualidad"], sMonedaProyecto, sMonedaImportes);
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
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getDatosProf"):
                sResultado += getDatosProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
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

    public string getDatosProfesionales(string sC, string sClase, string sSegMesProy, string sEstadoMes, string sEstadoProy, string sCualidad, string sMonedaProyecto2, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        try
        {
            sLectura = "false";

            if (sEstadoProy == "H" || sEstadoProy == "C")
            {
                sLecturaInsMes = "true";
                if (sCualidad != "J") dr = CONSPERMES.CatalogoMesCerrado(int.Parse(sSegMesProy), sMonedaImportes2);
                else dr = CONSPERMES.CatalogoMesCerradoReplicado(int.Parse(sSegMesProy), sMonedaImportes2);
            }
            else if (sEstadoMes == "A")
            {
                if (sCualidad != "J") dr = CONSPERMES.CatalogoMesAbierto(int.Parse(sSegMesProy), sMonedaImportes2);
                else dr = CONSPERMES.CatalogoMesAbiertoReplicado(int.Parse(sSegMesProy), sMonedaImportes2);
            }
            else if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
            {
                if (sCualidad != "J") dr = CONSPERMES.CatalogoMesCerradoSA(int.Parse(sSegMesProy), sMonedaImportes2);
                else dr = CONSPERMES.CatalogoMesCerradoSAReplicado(int.Parse(sSegMesProy), sMonedaImportes2);
            }
            else
            {
                if (sCualidad != "J") dr = CONSPERMES.CatalogoMesCerrado(int.Parse(sSegMesProy), sMonedaImportes2);
                else dr = CONSPERMES.CatalogoMesCerradoReplicado(int.Parse(sSegMesProy), sMonedaImportes2);
            }

            sb.Append("<TABLE class=texto id=tblDatos style='width: 960px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:10px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:570px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            if (sMonedaProyecto2 != sMonedaImportes2)
            {
                sLectura = "true";
            }
            else
            {
                if (sEstadoProy == "H" || sEstadoProy == "C" || (bool)Session["MODOLECTURA_PROYECTOSUBNODO"] || sEstadoMes == "C" || sCualidad == "J")
                {
                    sLectura = "true";
                }
                if ((sEstadoProy == "A" || sEstadoProy == "P") && (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA" && sCualidad != "J"))
                {
                    sLectura = "false";
                }
                if (sClase == "-15")
                    sLectura = "true";
            }

            while (dr.Read())
            {
                sModoCoste = dr["t301_modelocoste"].ToString();
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' ");
                sb.Append("idNodo='" + dr["t303_idnodo_usuariomes"].ToString() + "' ");
                sb.Append("caso='" + dr["caso"].ToString() + "' ");
                sb.Append("tipo='" + dr["TipoRecurso"].ToString() + "' ");
                sb.Append("unidades='" + dr["t378_unidades"].ToString().Replace(",", ".") + "' ");
                sb.Append("costecon='" + dr["coste"].ToString().Replace(",", ".") + "' ");
                sb.Append("costerep='" + dr["costerep"].ToString().Replace(",", ".") + "' ");
                sb.Append("idempresa='" + dr["t313_idempresa_nodomes"].ToString() + "' ");
                if (sClase == "-15") sb.Append("importe='" + dr["importe_cesion"].ToString().Replace(",", ".") + "' ");
                else sb.Append("importe='" + dr["importe"].ToString().Replace(",", ".") + "' ");

                if (sLectura != "true") sb.Append(" onclick='mm(event)' ");

                if (sC == "0" || sC == dr["caso"].ToString() || sClase == "-15") sb.Append("style='height:20px;'>");
                else sb.Append("style='height:20px;display:none;'>");

                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                switch (dr["TipoRecurso"].ToString())
                {
                    case "1":
                        sb.Append("<td><img border='0' src='../../../Images/imgUsuP" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' /></td>"); 
                        break;
                    case "2":
                    case "3":
                        sb.Append("<td><img border='0' src='../../../Images/imgUsuN" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' /></td>"); 
                        break;
                    case "4":
                        sb.Append("<td><img border='0' src='../../../Images/imgUsuE" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' /></td>");
                        break;
                    case "5":
                        sb.Append("<td><img border='0' src='../../../Images/imgUsuF" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' /></td>");
                        break;
                }
                //sb.Append("<td><nobr class='NBR W520' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["Profesional"].ToString() + "<nobr></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W520' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                if (dr["caso"].ToString() == "4")
                {//Si externo, ponemos su proveedor
                    sb.Append("<br><label style='width:70px;'>Proveedor:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39));
                }
                else//Sino, ponemos su nodo (SI NO ES FORÁNEO)
                {
                    if (dr["TipoRecurso"].ToString() != "5")
                        sb.Append("<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                }
                sb.Append("] hideselects=[off]\" >" + dr["Profesional"].ToString() + "<nobr></td>");

                if (sCualidad != "J" || sClase == "-15") sb.Append("<td style='text-align:right;' title='" + dr["coste"].ToString() + "'>" + decimal.Parse(dr["coste"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right;' title='" + dr["costerep"].ToString() + "'>" + decimal.Parse(dr["costerep"].ToString()).ToString("N") + "</td>");

                if (sLectura == "true")
                {
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["t378_unidades"].ToString()).ToString("N") + "</td>");
                    if (sClase == "-15") sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["importe_cesion"].ToString()).ToString("N") + "</td>");
                    else sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["importe"].ToString()).ToString("N") + "</td>");
                }
                else
                {
                    sb.Append("<td style='text-align:right;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='" + double.Parse(dr["t378_unidades"].ToString()).ToString("N") + "' onkeyup='fm(event);activarGrabar();if(event.keyCode!=9)setUnidades(this);' onfocus='fn(this);' onchange='calcularTotal()' title='" + dr["t378_unidades"].ToString().Replace(",", ".") + "' /></td>");
                    if ((bool)dr["t323_coste"])
                    {
                        if (sClase == "-15") sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='" + decimal.Parse(dr["importe_cesion"].ToString()).ToString("N") + "' title='" + dr["importe_cesion"].ToString() + "' onkeyup='fm(event);activarGrabar();if(event.keyCode!=9)setImporte(this);' onfocus='fn(this);' onchange='calcularTotal()' /></td>");
                        else sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='" + decimal.Parse(dr["importe"].ToString()).ToString("N") + "' title='" + dr["importe"].ToString() + "' onkeyup='fm(event);activarGrabar();if(event.keyCode!=9)setImporte(this);' onfocus='fn(this);' onchange='calcularTotal()' /></td>");
                    }
                    else sb.Append("<td style='text-align:right; padding-right:2px;'><input type='text' class='txtNumL' style='width:90px; cursor:pointer' value='0,00' readonly /></td>");
                }
                sb.Append("</tr>");

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sLectura + "@#@" + MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los consumos de los profesionales", ex);
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
        string sResul = "";
        bool bErrorControlado = false;
        double dUnidades = 0;
        double? dUnidadesBD = null;
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
            //CONSPERMES.DeleteByT325_idsegmesproy(tr, int.Parse(sSegMesProy));
            string[] aConsumo = Regex.Split(strDatos, "///");
            foreach (string oConsumo in aConsumo)
            {
                if (oConsumo == "") continue;
                string[] aValores = Regex.Split(oConsumo, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID usuario 
                //2. Coste
                //3. Unidades
                //4. Costerep
                //5. idempresa_nodomes
                //6. Nodo
                dUnidades = double.Parse(aValores[3]);
                if (dUnidades == 0)
                {
                    CONSPERMES.Delete(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]));
                }
                else
                {//Si existe en BBDD, updateo, sino, inserto
                    dUnidadesBD=CONSPERMES.GetUnidades(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]));
                    if (dUnidadesBD == null)//No existe registro -> lo insertamos
                    {
                        int? nEmpresa = null;
                        if (aValores[5] != "") nEmpresa = int.Parse(aValores[5]);
                        int? nNodo = null;
                        if (aValores[6] != "") nNodo = int.Parse(aValores[6]);
                        CONSPERMES.Insert(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), dUnidades, decimal.Parse(aValores[2]), decimal.Parse(aValores[4]), nNodo, nEmpresa);
                    }
                    else
                    {//El registro ya existe, solo updateamos si el valor es diferente
                        if (dUnidades != dUnidadesBD)
                            CONSPERMES.UpdateUnidades(tr, int.Parse(sSegMesProy), int.Parse(aValores[1]), dUnidades);
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los consumos de los profesionales.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
