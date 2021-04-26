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
    public int nIdT, nIdAccion;//nIdPE, nIdPT, 
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
            ASUNTO_T oAsto = ASUNTO_T.Select(null, int.Parse(txtIdAsunto.Text));
            txtDesAsunto.Text = oAsto.t600_desasunto;
            this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["p"].ToString());//Permiso

            nIdT = int.Parse(quitaPuntos(Request.QueryString["t"].ToString()));//nT

            //this.txtIdResponsable.Text = Request.QueryString["sIdResp"].ToString();
            this.txtIdResponsable.Text = oAsto.t600_responsable.ToString();
            //if (Request.QueryString["r"] != null && Request.QueryString["r"] != "undefined")//sIdResp
            //{
            //    if (Request.QueryString["r"] != "")
            //        this.txtIdResponsable.Text = Utilidades.decodpar(Request.QueryString["r"].ToString());
            //}

            this.hdnNodo.Value = TAREAPSP.GetNodo(tr, nIdT).ToString();
            try
            {
                Utilidades.SetEventosFecha(this.txtValLim);
                Utilidades.SetEventosFecha(this.txtValFin);

                ObtenerDatosAccion();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la acción", ex);
            }
            try
            {
                //Datos de las personas asignadas a la acción
                string strTabla = ObtenerRecursos(nIdAccion.ToString(), this.hdnNodo.Value);
                string[] aRecursos = Regex.Split(strTabla, "@#@");
                if (aRecursos[0] == "OK") divR.InnerHtml = aRecursos[1];

                //Datos de los documentos asociados a la acción
                //strTabla = ObtenerDocumentos(nIdAccion.ToString());
                //string[] aTabla = Regex.Split(strTabla, "@#@");
                //if (aTabla[0] == "OK") divDoc.InnerHtml = aTabla[1];
                string sEstado = TAREAPSP.getEstado(null, nIdT);
                divDoc.InnerHtml = Utilidades.ObtenerDocumentos("AC_T", nIdAccion, this.hdnAcceso.Text, sEstado);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
            }

            this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["p"].ToString());//Permiso
            if (this.hdnAcceso.Text == "R")
            {
                ModoLectura.Poner(this.Controls);
                sLectura = "true";
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
        string sResultado = "", sCad;
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
            case ("documentos"):
                //sResultado += ObtenerDocumentos(aArgs[1]);
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos("AC_T", int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
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
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private void ObtenerDatosAccion()
    {
        if (nIdAccion == -1)
        {
        }
        else
        {
            ACCION_T o = ACCION_T.Select(tr, nIdAccion);
            txtIdAccion.Text = o.t601_idaccion.ToString();
            txtDesAccion.Text = o.t601_desaccion;
            txtDescripcion.Text = o.t601_desaccionlong;
            if (o.t601_flimite.Year > 1900) txtValLim.Text = o.t601_flimite.ToShortDateString();
            //Obtengo la mayor de las fechas de vigencia de sus tareas
            if (o.t601_ffin.Year > 1900) txtValFin.Text = o.t601_ffin.ToShortDateString();
            this.cboAvance.SelectedValue = o.t601_avance.ToString();
            this.cboAvance.Text = o.t601_avance.ToString();

            this.txtDpto.Text = o.t601_dpto;
            this.txtAlerta.Text = o.t601_alerta;
            this.txtObs.Text = o.t601_obs;
        }
    }
    protected string Grabar(string strDatosTarea, string slIntegrantes)
    {
        string sResul = "", sAccionBD, sIdRecurso, sCad, oRec, sIdResponsable;
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
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
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
            // 11 -> enviar alerta (S/N)
            // 12 -> Id responsable del asunto
            // 13 -> num Tarea
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
                iCodAccion=ACCION_T.Insert(tr, dtNow, iCodAsunto, Utilidades.unescape(aDatosTarea[7]),
                            iAvance, Utilidades.unescape(aDatosTarea[2]),
                            Utilidades.unescape(aDatosTarea[3]),
                            Utilidades.unescape(aDatosTarea[4]),
                            dFfp, dFLi, Utilidades.unescape(aDatosTarea[8]));
            }
            else
            {
                ACCION_T.Update(tr, Utilidades.unescape(aDatosTarea[7]),
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
            {//Tenemos lista vacía. No hacemos nada
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

                        switch (sAccionBD)
                        {
                            case "I":
                                ACCIONRECURSOS_T.Insert(tr, int.Parse(sIdRecurso), iCodAccion, bNotificable);
                                break;
                            case "D":
                                ACCIONRECURSOS_T.Delete(tr, iCodAccion, int.Parse(sIdRecurso));
                                break;
                            case "U":
                                ACCIONRECURSOS_T.Update(tr, int.Parse(sIdRecurso), iCodAccion, bNotificable);
                                break;
                        }
                    }
                }//for
            }
            #endregion
            #region Enviar correos
            if (bEnviarAlerta)
            {
                oRec = iCodAccion.ToString() + "##";
                oRec += Utilidades.unescape(aDatosTarea[2]) + "##";//descripcion corta
                oRec += aDatosTarea[9] + "##" + Utilidades.unescape(aDatosTarea[10]) + "##";//cod y des Asunto
                oRec += dFLi.ToString() + "##" + dFfp.ToString() + "##";
                oRec += aDatosTarea[1] + "##";//avance
                oRec += Utilidades.unescape(aDatosTarea[3]) + "##";//descripcion larga accion
                oRec += Utilidades.unescape(aDatosTarea[8]) + "##";//observaciones
                oRec += Utilidades.unescape(aDatosTarea[4]) + "##";//Dpto
                oRec += aDatosTarea[13] + "##" ;//cod Tarea
                if (bAlta) sCad = "I";
                else sCad = "U";
                sIdResponsable = aDatosTarea[12];
                EnviarCorreoAlerta(sCad, oRec, Utilidades.unescape(aDatosTarea[7]), slIntegrantes, sIdResponsable, bAlta);
            }
            #endregion
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + iCodAccion.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la acción", ex);
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

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
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
            #region eliminar documentos

            string[] aDocs = Regex.Split(strIdsDocs, "##");

            foreach (string oDoc in aDocs)
            {
                DOCACC_T.Delete(tr, int.Parse(oDoc));
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
                                                                          Utilidades.unescape(sNom), sCR, "", "", "",true);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='width: 390px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:370px;' /></colgroup>");
            sb.Append("<tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' mail='" + dr["mail"].ToString() + "' style='height:20px;noWrap:true;' ");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ondblclick='convocarAux(this);' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[Profesional:&nbsp;");
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
            sb.Append("<table id='tblOpciones2' class='texto MM' style='width: 435px; mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:365px;' /><col style='width:40px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            SqlDataReader dr = ACCIONRECURSOS_T.SelectByt601_idaccion(tr, int.Parse(sIdAccion));
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
                sb.Append("<nobr class='NBR W360'>" + sDes + "</nobr></label></td>");

                sb.Append("<td><input type='checkbox' style='width:20px; margin-left:3px;' id='chkNot");
                sb.Append(i.ToString());
                sb.Append("' class='checkTabla' onclick='actualizarDatos(this);' ");
                if ((bool)dr["t605_notificar"]) sb.Append("checked=true");
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
        string sResul = "", sTexto = "", sTO = "", sAux, sToAux = "", sIdAccion = "";
        string sAsunto = "", sAccionBD="";
        bool bNotificable;
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sb = new StringBuilder();

        try
        {
            if (slMails == "" && slIntegrantes == "" && sIdResponsable=="") return "OK@#@";
            sAsunto = "Alerta de acción en Bitácora de tarea.";
            if (bAlta) sb.Append("<BR>SUPER le informa de la generación de la siguiente acción:<BR><BR>");
            else sb.Append("<BR>SUPER le informa de la modificación de la siguiente acción:<BR><BR>");
            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            //aDatosTarea[0] = hdnIdAccion
            //aDatosTarea[1] = txtDesAccion
            //aDatosTarea[2] = cod asunto
            //aDatosTarea[3] = des asunto
            //aDatosTarea[4] = f/limite
            //aDatosTarea[5] = f/fin
            //aDatosTarea[6] = avance
            //aDatosTarea[7] = descripción larga acción
            //aDatosTarea[8] = observaciones
            //aDatosTarea[9] = Dpto
            //aDatosTarea[10] = idTarea

            TAREAPSP oTar = TAREAPSP.Obtener(null, int.Parse(aDatosTarea[10]));
            sIdAccion = aDatosTarea[0];
            sb.Append("<label style='width:120px'>Proyecto económico: </label>" + oTar.num_proyecto.ToString("#,###") + @" - " + oTar.nom_proyecto + "<br>");
            sb.Append("<label style='width:120px'>Proyecto técnico: </label>" + oTar.t331_despt + "<br>");
            sb.Append("<label style='width:120px'>Fase: </label>" + oTar.t334_desfase + "<br>");
            sb.Append("<label style='width:120px'>Actividad: </label>" + oTar.t335_desactividad + "<br>");
            sb.Append("<label style='width:120px'>Tarea: </label>" + int.Parse(aDatosTarea[10]).ToString("#,###") + @" - " + oTar.t332_destarea + "<br>");
            //sb.Append("<label style='width:120px'>Asunto: </label>" + aDatosTarea[2] + @" - " + Utilidades.unescape(aDatosTarea[3]) + "<br>");
            //sb.Append("<label style='width:120px'>Acción: </label><b>" + aDatosTarea[0] + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
            sb.Append("<label style='width:120px'>Asunto: </label>" + aDatosTarea[2] + @" - " + aDatosTarea[3] + "<br>");
            sb.Append("<label style='width:120px'>Acción: </label><b>" + aDatosTarea[0] + @" - " + aDatosTarea[1] + "</b><br><br>");
            sb.Append("<b>Información de la acción:</b><br>");

            sAux = aDatosTarea[4];
            if (sAux == "")
                sb.Append("<label style='width:120px'>F/Límite: </label>&nbsp;<br>");
            else
                sb.Append("<label style='width:120px'>F/Límite: </label>" + sAux.Substring(0, 10) + "<br>");
            sAux = aDatosTarea[5];
            if (sAux == "")
                sb.Append("<label style='width:120px'>F/Fin: </label>&nbsp;");
            else
                sb.Append("<label style='width:120px'>F/Fin: </label>" + sAux.Substring(0, 10));
            //avance
            sAux = aDatosTarea[6];
            sb.Append("<br><label style='width:120px'>Avance: </label>" + sAux + "<br><br>");
            //descripcion larga
            //sAux = Utilidades.unescape(aDatosTarea[7]);
            sb.Append("<b><label style='width:120px'>Descripción: </label></b>" + aDatosTarea[7] + "<br><br>");
            //observaciones
            //sAux = Utilidades.unescape(aDatosTarea[8]);
            sb.Append("<b><label style='width:120px'>Observaciones: </label></b><br>" + aDatosTarea[8]);
            //Departamento/Grupo
            //sAux = Utilidades.unescape(aDatosTarea[9]);
            sb.Append("<br><br><b><label style='width:120px'>Departamento/Grupo: </label></b>" + aDatosTarea[9]);

            sb.Append("<br><br>");
            //Obtengo la lista de e-mail a los que alertar
            if (!slMails.Contains(";")) slMails += ";";
            string[] aMails = Regex.Split(slMails, ";");
            //Genero una tabla con la lista de e-mails a notificar
            sb.Append("<b><label style='width:400px'>Relación de e-mails a notificar: </label></b>&nbsp;<br>");
            sb.Append("<table width='400px' style='padding:10px;'>");
            sb.Append("<colgroup><col style='width:400px;' /></colgroup>");
            sb.Append("<tbody>");
            for (int i = 0; i < aMails.Length; i++)
            {
                sToAux = aMails[i].Trim();
                if (sToAux != "")
                {
                    sb.Append("<tr><td style='padding-left:5px;font-size:11px;'>");
                    //sTO = Utilidades.unescape(aMails[i]);
                    sTO = sToAux;
                    sAux = sTO.Substring(0, 2);
                    if (sAux == "\r\n") sTO = sTO.Substring(2);
                    sb.Append(sTO);
                    sb.Append("</td></tr>");
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table><br>");
            //Genero una tabla con la lista de profesionales a notificar
            sb.Append("<b><label style='width:400px'>Relación de profesionales asignados: </label>&nbsp;</b><br>");
            sb.Append("<table width='400px' style='padding:10px;'>");
            sb.Append("<colgroup><col style='width:400px;' /></colgroup>");
            sb.Append("<tbody>");
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
                                sb.Append("<tr><td style='padding-left:5px;font-size:11px;'>");
                                sb.Append(Utilidades.unescape(aIntegrante[3]));
                                sb.Append("</td></tr>");
                                break;
                        }
                    }
                }//for
            }
            sb.Append("</tbody>");
            sb.Append("</table><br>");
            
            sTexto = sb.ToString();

            //Envío e-mail al responsable del asunto 
            if (sIdResponsable != "")
            {
                //sTO = Recurso.CodigoRed(int.Parse(sIdResponsable));
                sTO = SUPER.Capa_Negocio.Recurso.GetDireccionMail(int.Parse(sIdResponsable));
                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);
            }
            //Obtengo la lista de e-mail a los que alertar y envío un correo a cada uno
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
            //Obtengo la lista de profesionales a los que notificar y envío un correo a cada uno
            if (slIntegrantes == "")
            {//Tenemos lista vacía. No hacemos nada
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
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de acción de Bitácora de Tarea (sIdResponsable=" + sIdResponsable + " sIdAccion=" + sIdAccion + ").", ex);
        }
        return sResul;
    }

}
