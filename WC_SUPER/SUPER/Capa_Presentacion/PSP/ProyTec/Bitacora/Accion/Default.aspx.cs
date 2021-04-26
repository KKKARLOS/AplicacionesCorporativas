using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, sLectura, strTablaHTMLIntegrantes, sNodo = "";
    public int nIdPE, nIdPT, nIdAccion;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            sErrores = "";
            sLectura = "false";
            sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            nIdAccion = int.Parse(Utilidades.decodpar(Request.QueryString["a"].ToString()));//nIdAccion
            txtIdAsunto.Text = Utilidades.decodpar(Request.QueryString["as"].ToString());//idAsunto
            //txtDesAsunto.Text = Utilidades.unescape(Request.QueryString["desAsunto"].ToString());
            ASUNTO_PT oAsto = ASUNTO_PT.Select(null, int.Parse(txtIdAsunto.Text));
            txtDesAsunto.Text = oAsto.t409_desasunto;
            this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["p"].ToString());//Permiso
            nIdPE = int.Parse(quitaPuntos(Request.QueryString["nPE"].ToString()));
            this.hdnDesPE.Text = Request.QueryString["desPE"].ToString();
            nIdPT = int.Parse(quitaPuntos(Utilidades.decodpar(Request.QueryString["nPT"].ToString())));
            this.hdnDesPT.Text = Request.QueryString["desPT"].ToString();
            this.txtIdResponsable.Text = oAsto.t409_responsable.ToString();
            if (Request.QueryString["sIdResp"] != null && Request.QueryString["sIdResp"] != "undefined")
            {
                if (Request.QueryString["sIdResp"] != "")
                    this.txtIdResponsable.Text = Utilidades.decodpar(Request.QueryString["r"].ToString());//sIdResp
            }
            this.hdnNodo.Value = ProyTec.GetNodo(tr, nIdPT).ToString();
            try
            {
                Utilidades.SetEventosFecha(this.txtValLim);
                Utilidades.SetEventosFecha(this.txtValFin);

                ObtenerDatosAccion();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la acci�n", ex);
            }
            try
            {
                //Datos de las personas asignadas a la acci�n
                string strTabla = ObtenerRecursos(nIdAccion.ToString(), this.hdnNodo.Value);
                string[] aRecursos = Regex.Split(strTabla, "@#@");
                if (aRecursos[0] == "OK") divR.InnerHtml = aRecursos[1];

                //Datos de las tareas asignadas a la acci�n
                strTabla = ObtenerTareas(nIdAccion.ToString());
                string[] aTareas = Regex.Split(strTabla, "@#@");
                if (aTareas[0] == "OK") divH.InnerHtml = aTareas[1];

                //Datos de los documentos asociados a la acci�n
                //strTabla = ObtenerDocumentos(nIdAccion.ToString());
                //string[] aTabla = Regex.Split(strTabla, "@#@");
                //if (aTabla[0] == "OK") divDoc.InnerHtml = aTabla[1];
                string sEstado = PROYECTO.getEstado(null, nIdPE);
                divDoc.InnerHtml = Utilidades.ObtenerDocumentos("AC_PT", nIdAccion, this.hdnAcceso.Text, sEstado);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
            }

            this.hdnAcceso.Text = Request.QueryString["p"].ToString();//Permiso
            if (this.hdnAcceso.Text == "R")
            {
                ModoLectura.Poner(this.Controls);
                sLectura = "true";
            }
            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad;
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("documentos"):
                //sResultado += ObtenerDocumentos(aArgs[1]);
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos("AC_PT", int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
                break;
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
        }
        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
    private void ObtenerDatosAccion()
    {
        if (nIdAccion == -1)
        {
        }
        else
        {
            ACCION_PT o = ACCION_PT.Select(tr, nIdAccion);
            txtIdAccion.Text = o.t410_idaccion.ToString();
            txtDesAccion.Text = o.t410_desaccion;
            txtDescripcion.Text = o.t410_desaccionlong;
            if (o.t410_flimite.Year > 1900) txtValLim.Text = o.t410_flimite.ToShortDateString();
            //Obtengo la mayor de las fechas de vigencia de sus tareas
            if (o.t410_ffin.Year > 1900) txtValFin.Text = o.t410_ffin.ToShortDateString();
            this.cboAvance.SelectedValue = o.t410_avance.ToString();
            this.cboAvance.Text = o.t410_avance.ToString();

            this.txtDpto.Text = o.t410_dpto;
            this.txtAlerta.Text = o.t410_alerta;
            this.txtObs.Text = o.t410_obs;
        }
    }
    protected string Grabar(string strDatosTarea, string slIntegrantes, string sTareas)
    {
        string sResul = "", sAccionBD, sIdRecurso, sCad, sTipoLinea, sCodTarea, oRec, sIdResponsable;
        int iCodAccion, iCodAsunto;
        byte iAvance;
        DateTime? dFfp = null;
        DateTime? dFLi = null;
        bool bNotificable = false, bEnviarAlerta=false, bAlta=false;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        try
        {
            #region Datos accion
            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            // 0 -> id accion (si -1 es un alta)
            // 1 -> avance
            // 2 -> descripcion corta
            // 3 -> descripcion larga
            // 4 -> departamento
            // 5 -> f/fin
            // 6 -> f/limite
            // 7 -> alerta
            // 8 -> observaciones
            // 9 -> asunto
            // 10 -> des asunto
            // 11 -> num PE
            // 12 -> des PE
            // 13 -> enviar alerta (S/N)
            // 14 -> Id responsable del asunto
            // 15 -> num PT
            // 16 -> des PT
            //if (aDatosTarea[13] == "S") bEnviarAlerta = true;
            bEnviarAlerta = true;
            if (aDatosTarea[0] == "") iCodAccion = -1;
            else iCodAccion = int.Parse(aDatosTarea[0]);
            iCodAsunto = int.Parse(aDatosTarea[9]);
            if (aDatosTarea[1] == "") iAvance = 0;
            else iAvance = byte.Parse(aDatosTarea[1]);
            if (aDatosTarea[5] != "") dFfp = DateTime.Parse(aDatosTarea[5]);
            if (aDatosTarea[6] != "") dFLi = DateTime.Parse(aDatosTarea[6]);

            if (iCodAccion == -1) bAlta = true;
            if (bAlta)
            {
                DateTime dtNow = System.DateTime.Now;
                iCodAccion=ACCION_PT.Insert(tr, dtNow, iCodAsunto, Utilidades.unescape(aDatosTarea[7]),
                            iAvance, Utilidades.unescape(aDatosTarea[2]),
                            Utilidades.unescape(aDatosTarea[3]),
                            Utilidades.unescape(aDatosTarea[4]),
                            dFfp, dFLi, Utilidades.unescape(aDatosTarea[8]));
            }
            else
            {
                ACCION_PT.Update(tr, Utilidades.unescape(aDatosTarea[7]),
                            iAvance,
                            Utilidades.unescape(aDatosTarea[2]),
                            Utilidades.unescape(aDatosTarea[3]),
                            Utilidades.unescape(aDatosTarea[4]),
                            dFfp, dFLi, iCodAccion,
                            Utilidades.unescape(aDatosTarea[8]));
            }
            #endregion
            #region Datos integrantes
            //OfiTec.BorrarIntegrantes(iCodCR);
            if (slIntegrantes == "")
            {//Tenemos lista vac�a. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aPersonas = Regex.Split(slIntegrantes, @"///");

                for (int i = 0; i < aPersonas.Length - 1; i++)
                {
                    if (aPersonas[i] != "")
                    {
                        string[] aIntegrante = Regex.Split(aPersonas[i], @"##");
                        sAccionBD = aIntegrante[0];
                        sIdRecurso = aIntegrante[1];
                        if (aIntegrante[2] == "1") bNotificable = true;
                        else bNotificable = false;

                        oRec = iCodAccion.ToString() + "##" + sIdRecurso + "##";
                        oRec += Utilidades.unescape(aDatosTarea[2]) + "##";//descripcion corta
                        oRec += aDatosTarea[11] + "##" + Utilidades.unescape(aDatosTarea[12]) + "##";//cod y des PE
                        oRec += aDatosTarea[9] + "##" + Utilidades.unescape(aDatosTarea[10]) + "##";//cod y des Asunto
                        oRec += dFLi.ToString() + "##" + dFfp.ToString() + "##";

                        switch (sAccionBD)
                        {
                            case "I":
                                ACCIONRECURSOS_PT.Insert(tr, int.Parse(sIdRecurso), iCodAccion, bNotificable);
                                //if (bNotificable) EnviarCorreoRecurso("I", oRec);
                                break;
                            case "D":
                                //delete
                                ACCIONRECURSOS_PT.Delete(tr, iCodAccion, int.Parse(sIdRecurso));
                                //if (bNotificable) EnviarCorreoRecurso("D", oRec);
                                break;
                            case "U":
                                //update
                                ACCIONRECURSOS_PT.Update(tr, int.Parse(sIdRecurso), iCodAccion, bNotificable);
                                //if (bNotificable) EnviarCorreoRecurso("U", oRec);
                                break;
                        }
                    }
                }//for
            }
            #endregion
            #region tareas
            //Grabamos las tareas asociadas a la acci�n
            if (sTareas != "")
            {
                string[] aTareas = Regex.Split(sTareas, @"##");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    sTipoLinea = sCad.Substring(0, 1);
                    sCodTarea = quitaPuntos(sCad.Substring(1));
                    if (sTipoLinea == "D")
                    {//Borrar accion-tarea
                        ACCIONTAREAS_PT.Delete(tr, iCodAccion, int.Parse(sCodTarea));
                    }
                    else
                    {
                        if (sTipoLinea == "I")
                        {//Insertar accion-tarea
                            ACCIONTAREAS_PT.Insert(tr, int.Parse(sCodTarea), iCodAccion);
                        }
                    }
                }
            }
            #endregion
            #region Enviar correos
            if (bEnviarAlerta)
            {
                oRec = iCodAccion.ToString() + "##";
                oRec += Utilidades.unescape(aDatosTarea[2]) + "##";//descripcion corta
                oRec += aDatosTarea[11] + "##" + Utilidades.unescape(aDatosTarea[12]) + "##";//cod y des PE
                oRec += aDatosTarea[9] + "##" + Utilidades.unescape(aDatosTarea[10]) + "##";//cod y des Asunto
                oRec += dFLi.ToString() + "##" + dFfp.ToString() + "##";
                oRec += aDatosTarea[1] + "##";//avance
                oRec += Utilidades.unescape(aDatosTarea[3]) + "##";//descripcion larga accion
                oRec += Utilidades.unescape(aDatosTarea[8]) + "##";//observaciones
                oRec += Utilidades.unescape(aDatosTarea[4]) + "##";//Dpto
                oRec += aDatosTarea[15] + "##" + Utilidades.unescape(aDatosTarea[16]) + "##";//cod y des PT
                if (bAlta) sCad = "I";
                else sCad = "U";
                sIdResponsable = aDatosTarea[14];
                EnviarCorreoAlerta(sCad, oRec, Utilidades.unescape(aDatosTarea[7]), slIntegrantes, sIdResponsable, bAlta);
            }
            #endregion
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + iCodAccion.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la acci�n", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string EliminarDocumentos(string strIdsDocs)
    {
        string sResul = "";

        #region abrir conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region eliminar documentos

            string[] aDocs = Regex.Split(strIdsDocs, "##");

            foreach (string oDoc in aDocs)
            {
                DOCACC_PT.Delete(tr, int.Parse(oDoc));
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string ObtenerPersonas(string sAP1, string sAP2, string sNom, string sCR)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa("N", Utilidades.unescape(sAP1), Utilidades.unescape(sAP2),
                                                                          Utilidades.unescape(sNom), sCR, "", "", "", true);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='width: 390px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:370px;' /></colgroup>");
            sb.Append("<tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' mail='" + dr["mail"].ToString() + "' style='height:20px;noWrap:true;' ");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ondblclick='convocarAux(this);' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'>�");
                sb.Append("Informaci�n] body=[Profesional:&nbsp;");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else if (dr["t303_idnodo"].ToString() == sCR) sb.Append("tipo='P' ");
                //else sb.Append("tipo='N' ");

                sb.Append("><td></td><td><nobr class='NBR W360'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close(); dr.Dispose();
            //this.strTablaHTMLPersonas = strBuilder.ToString();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las personas", ex);
        }
    }
    private string ObtenerRecursos(string sIdAccion, string sNodo)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        string sDes;
        try
        {
            sb.Append("<table id='tblOpciones2' class='texto MM' style='width: 435px; ' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:385px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            SqlDataReader dr = ACCIONRECURSOS_PT.SelectByt410_idaccion(tr, int.Parse(sIdAccion));
            while (dr.Read())
            {
                sb.Append("<tr id='");
                sb.Append(dr["t314_idusuario"].ToString());
                sb.Append("' mail='" + dr["mail"].ToString());
                sb.Append("' bd='' onclick='mm(event)' onmousedown='DD(event)' style='height:20px' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else if (dr["t303_idnodo"].ToString() == sNodo) sb.Append("tipo='P' ");
                //else sb.Append("tipo='N' ");

                sb.Append("><td></td><td></td>");
                sDes = dr["nomRecurso"].ToString();
                if (sDes.Length > 80) sb.Append("<td title='" + sDes + "'>");
                else sb.Append("<td>");
                sb.Append("<nobr class='NBR W380'>" + sDes + "</NOBR></label></td>");

                sb.Append("<td><input type='checkbox' style='width:15;' id='chkNot");
                sb.Append(i.ToString());
                sb.Append("' class='checkTabla' onclick='actualizarDatos(this);' ");
                if ((bool)dr["t414_notificar"]) sb.Append("checked=true");
                sb.Append("></td></tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody></table>");
            this.strTablaHTMLIntegrantes = sb.ToString();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las personas", ex);
        }
    }
    protected string ObtenerTareas(string sIdAccion)
    {
        //Relacion de tareas asignadas a la acci�n
        string sResul = "", sCad, sFecha, sCodTarea;
        double fPrev = 0, fCons = 0, fAvance = 0;
        bool bAvanceAutomatico;

        StringBuilder sbuilder = new StringBuilder();
        try
        {
            sbuilder.Append("<table id='tblTareas' class='texto MANO' style='width: 920px; ' >");
            sbuilder.Append("<colgroup><col style='width:50px;' />");//idTarea
            sbuilder.Append("<col style='width:370px;' />");//Desc Tarea
            sbuilder.Append("<col style='width:75px;'/>");//ETPL
            sbuilder.Append("<col style='width:75px;'/>");//FIPL
            sbuilder.Append("<col style='width:75px;'/>");//FFPL
            sbuilder.Append("<col style='width:75px;'/>");//ETPR
            sbuilder.Append("<col style='width:75px;'/>");//FFPR
            sbuilder.Append("<col style='width:75px;'/>");//CONSUMO
            sbuilder.Append("<col style='width:50px;' /></colgroup>");//AVANCE
            sbuilder.Append("<tbody>");
            SqlDataReader dr = ACCIONTAREAS_PT.SelectByt410_idaccion(tr, int.Parse(sIdAccion));
            while (dr.Read())
            {
                StringBuilder sbTitle = new StringBuilder();
                sbTitle.Append("<b>Proy. Eco.</b>: ");
                sbTitle.Append(dr["nom_proyecto"].ToString().Replace((char)34, (char)39));
                sbTitle.Append("<br><b>Proy. T�c.</b>: ");
                sbTitle.Append(dr["t331_despt"].ToString().Replace((char)34, (char)39));
                if (dr["t334_desfase"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Fase</b>:����������");
                    sbTitle.Append(dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                }
                if (dr["t335_desactividad"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Actividad</b>:��");
                    sbTitle.Append(dr["t335_desactividad"].ToString().Replace((char)34, (char)39));
                }

                sCodTarea = dr["t332_idtarea"].ToString();
                sbuilder.Append("<tr bd='N' id='" + sCodTarea);
                sbuilder.Append("' onclick='mm(event)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'>��Estructura] body=[");
                sbuilder.Append(sbTitle + "]\" style='height:16px'>");
                sbuilder.Append("<td style='text-align:right;'>" + int.Parse(sCodTarea).ToString("#,###") + "</td>");
                sbuilder.Append("<td><nobr class='NBR W370' style='padding-left:5px;'>" + dr["t332_destarea"].ToString() + "</nobr></td>");

                if (dr["t332_etpl"] != DBNull.Value)
                {
                    sbuilder.Append("<td style='text-align:right;'>");
                    sbuilder.Append(double.Parse(dr["t332_etpl"].ToString()).ToString("N"));
                    sbuilder.Append("</td>");
                }
                else
                    sbuilder.Append("<td></td>");


                sFecha = dr["t332_fipl"].ToString();
                if (sFecha != "")
                    sFecha = DateTime.Parse(dr["t332_fipl"].ToString()).ToShortDateString();
                sbuilder.Append("<td style='padding-left:5px;'>" + sFecha + "</td>");

                sFecha = dr["t332_ffpl"].ToString();
                if (sFecha != "")
                    sFecha = DateTime.Parse(dr["t332_ffpl"].ToString()).ToShortDateString();
                sbuilder.Append("<td>" + sFecha + "</td>");

                if (dr["t332_etpr"] != DBNull.Value)
                {
                    sbuilder.Append("<td style='text-align:right;'>");
                    sbuilder.Append(double.Parse(dr["t332_etpr"].ToString()).ToString("N"));
                    sbuilder.Append("</td>");
                }
                else
                    sbuilder.Append("<td></td>");


                sFecha = dr["t332_ffpr"].ToString();
                if (sFecha != "")
                    sFecha = DateTime.Parse(dr["t332_ffpr"].ToString()).ToShortDateString();
                sbuilder.Append("<td style='padding-left:5px;'>" + sFecha + "</td>");

                sbuilder.Append("<td style='text-align:right;'>" + double.Parse(dr["consumo"].ToString()).ToString("N") + "</td>");
                //%Avance
                bAvanceAutomatico = (bool)dr["t332_avanceauto"];
                if (!bAvanceAutomatico)
                {
                    fAvance = double.Parse(dr["t332_AVANCE"].ToString());
                }
                else
                {
                    fPrev = double.Parse(dr["t332_etpr"].ToString());
                    fCons = double.Parse(dr["Consumo"].ToString());
                    if (fPrev == 0) fAvance = 0;
                    else fAvance = (fCons * 100) / fPrev;
                }
                sCad = fAvance.ToString("N");
                sbuilder.Append("<td style='text-align:right; padding-right:5px;'>" + sCad);
                sbuilder.Append("</td></tr>");
                //i++;
            }
            dr.Close();
            dr.Dispose();
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table>");
            sResul = "OK@#@" + sbuilder.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relaci�n de tareas.", ex);
        }

        return sResul;
    }
    private string quitaPuntos(string sCadena)
    {
        //Finalidad:Elimina los puntos de una cadena
        string sRes;

        sRes = sCadena;
        try
        {
            if (sCadena == "") return "";
            sRes = sRes.Replace(".", "");
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al quitar puntos de la cadena" + sCadena, ex);
        }
        return sRes;
    }
    private static string EnviarCorreoAlerta(string sTipo, string strDatosTarea, string slMails, string slIntegrantes, string sIdResponsable, bool bAlta)
    {
        string sResul = "", sTexto = "", sTO = "", sToAux = "", sAux, sIdAccion = "";
        string sAsunto = "", sAccionBD="";
        bool bNotificable;
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sbuilder = new StringBuilder();

        try
        {
            if (slMails == "" && slIntegrantes == "" && sIdResponsable=="") return "OK@#@";
            sAsunto = "Alerta de acci�n en Bit�cora de proyecto t�cnico.";
            if (bAlta) sbuilder.Append("<BR>SUPER le informa de la generaci�n de la siguiente acci�n:<BR><BR>");
            else sbuilder.Append("<BR>SUPER le informa de la modificaci�n de la siguiente acci�n:<BR><BR>");
            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            //aDatosTarea[0] = hdnIdAccion
            //aDatosTarea[1] = txtDesAccion
            //aDatosTarea[2] = txtNumPE
            //aDatosTarea[3] = txtPE
            //aDatosTarea[4] = cod asunto
            //aDatosTarea[5] = des asunto
            //aDatosTarea[6] = f/limite
            //aDatosTarea[7] = f/fin
            //aDatosTarea[8] = avance
            //aDatosTarea[9] = descripci�n larga acci�n
            //aDatosTarea[10] = observaciones
            //aDatosTarea[11] = Dpto
            //aDatosTarea[12] = txtNumPT
            //aDatosTarea[13] = txtPT
            sIdAccion = aDatosTarea[0];
            sbuilder.Append("<label style='width:120px'>Proyecto econ�mico: </label>" + aDatosTarea[2] + @" - " +aDatosTarea[3] + "<br>");
            sbuilder.Append("<label style='width:120px'>Proyecto t�cnico: </label>" + aDatosTarea[12] + @" - " + aDatosTarea[13] + "<br>");
            sbuilder.Append("<label style='width:120px'>Asunto: </label>" + aDatosTarea[4] + @" - " + aDatosTarea[5] + "<br>");
            sbuilder.Append("<label style='width:120px'>Acci�n: </label><b>" + aDatosTarea[0] + @" - " +aDatosTarea[1] + "</b><br><br>");
            sbuilder.Append("<b>Informaci�n de la acci�n:</b><br>");

            sAux = aDatosTarea[6];
            if (sAux == "")
                sbuilder.Append("<label style='width:120px'>F/L�mite: </label>&nbsp;<br>");
            else
                sbuilder.Append("<label style='width:120px'>F/L�mite: </label>" + sAux.Substring(0, 10) + "<br>");
            sAux = aDatosTarea[7];
            if (sAux == "")
                sbuilder.Append("<label style='width:120px'>F/Fin: </label>&nbsp;");
            else
                sbuilder.Append("<label style='width:120px'>F/Fin: </label>" + sAux.Substring(0, 10));
            //avance
            sAux = aDatosTarea[8];
            sbuilder.Append("<br><label style='width:120px'>Avance: </label>" + sAux + "<br><br>");
            //descripcion larga
            sbuilder.Append("<b><label style='width:120px'>Descripci�n: </label></b>" + aDatosTarea[9] + "<br><br>");
            //observaciones
            sbuilder.Append("<b><label style='width:120px'>Observaciones: </label></b><br>" + aDatosTarea[10]);
            //Departamento/Grupo
            sbuilder.Append("<br><br><b><label style='width:120px'>Departamento/Grupo: </label></b>" + aDatosTarea[11]);

            sbuilder.Append("<br><br>");
            //Obtengo la lista de e-mail a los que alertar
            if (!slMails.Contains(";")) slMails += ";";
            string[] aMails = Regex.Split(slMails, ";");
            //Genero una tabla con la lista de e-mails a notificar
            sbuilder.Append("<b><label style='width:400px'>Relaci�n de e-mails a notificar: </label></b>&nbsp;<br>");
            sbuilder.Append("<table width='400px' style='padding:10px;'>");
            sbuilder.Append("<colgroup><col style='width:400px;' /></colgroup>");
            sbuilder.Append("<tbody>");
            for (int i = 0; i < aMails.Length; i++)
            {
                sToAux = aMails[i].Trim();
                if (sToAux != "")
                {
                    sbuilder.Append("<tr><td style='padding-left:5px;font-size:11px;'>");
                    //sTO = Utilidades.unescape(aMails[i]);
                    sTO = sToAux;
                    sAux = sTO.Substring(0, 2);
                    if (sAux == "\r\n") sTO = sTO.Substring(2);
                    sbuilder.Append(sTO);
                    sbuilder.Append("</td></tr>");
                }
            }
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table><br>");
            //Genero una tabla con la lista de profesionales a notificar
            sbuilder.Append("<b><label style='width:400px'>Relaci�n de profesionales asignados: </label>&nbsp;</b><br>");
            sbuilder.Append("<table width='400px' style='padding:10px;'>");
            sbuilder.Append("<colgroup><col style='width:400px;' /></colgroup>");
            sbuilder.Append("<tbody>");
            if (slIntegrantes != "")
            {//Con la cadena generamos una lista y la recorremos 
                string[] aPersonas = Regex.Split(slIntegrantes, @"///");

                for (int i = 0; i < aPersonas.Length; i++)
                {
                    if (aPersonas[i] != "")
                    {
                        string[] aIntegrante = Regex.Split(aPersonas[i], @"##");
                        sAccionBD = aIntegrante[0];
                        if (aIntegrante[2] == "1") bNotificable = true;
                        else bNotificable = false;
                        switch (sAccionBD)
                        {
                            case "":
                            case "I":
                            case "U":
                            case "N":
                                sbuilder.Append("<tr><td style='padding-left:5px;font-size:11px;'>");
                                sbuilder.Append(Utilidades.unescape(aIntegrante[3]));
                                sbuilder.Append("</td></tr>");
                                break;
                        }
                    }
                }//for
            }
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table><br>");
            
            sTexto = sbuilder.ToString();

            //Env�o e-mail al responsable del asunto 
            if (sIdResponsable != "")
            {
                //sTO = Recurso.CodigoRed(int.Parse(sIdResponsable));
                sTO = SUPER.Capa_Negocio.Recurso.GetDireccionMail(int.Parse(sIdResponsable));
                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);
            }
            //Obtengo la lista de e-mail a los que alertar y env�o un correo a cada uno
            for (int i = 0; i < aMails.Length; i++)
            {
                if (aMails[i] != "")
                {
                    sTO = aMails[i];
                    //sTO = Utilidades.unescape(aMails[i]);
                    sAux = sTO.Substring(0, 2);
                    if (sAux == "\r\n") sTO = sTO.Substring(2);
                    sTO.Trim();
                    string[] aMail = { sAsunto, sTexto, sTO };
                    aListCorreo.Add(aMail);
                }
            }
            //Obtengo la lista de profesionales a los que notificar y env�o un correo a cada uno
            if (slIntegrantes == "")
            {//Tenemos lista vac�a. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos 
                string[] aPersonas = Regex.Split(slIntegrantes, @"///");
                
                for (int i = 0; i < aPersonas.Length; i++)
                {
                    if (aPersonas[i] != "")
                    {
                        string[] aIntegrante = Regex.Split(aPersonas[i], @"##");
                        sAccionBD = aIntegrante[0];
                        sTO = aIntegrante[4];
                        if (aIntegrante[2] == "1") bNotificable = true;
                        else bNotificable = false;
                        if (bNotificable)
                        {
                            switch (sAccionBD)
                            {
                                case "":
                                case "I":
                                case "U":
                                case "N":
                                    string[] aMail = { sAsunto, sTexto, sTO };
                                    aListCorreo.Add(aMail);
                                    break;
                            }
                        }
                    }
                }//for
            }

            Correo.EnviarCorreos(aListCorreo);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + 
                Errores.mostrarError("Error al enviar correo de acci�n de Bit�cora de PT (sIdResponsable=" + sIdResponsable + 
                " sIdAccion=" + sIdAccion +
                " strDatosTarea=" + strDatosTarea +
                " slMails=" + slMails +
                " slIntegrantes=" + slIntegrantes +
                ").", ex);
        }
        return sResul;
    }

}
