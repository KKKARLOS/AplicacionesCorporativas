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


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, sLectura, strTablaHTMLIntegrantes, sNodo = "";
    public int nIdT, nIdAsunto;//nIdPE, nIdPT, 
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
            nIdAsunto = int.Parse(Utilidades.decodpar(Request.QueryString["as"].ToString()));//nIdAsunto
            this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["p"].ToString());//Permiso
            nIdT = int.Parse(quitaPuntos(Utilidades.decodpar(Request.QueryString["t"].ToString())));//nT
            this.hdnNodo.Value = TAREAPSP.GetNodo(tr, nIdT).ToString();
            try
            {
                Utilidades.SetEventosFecha(this.txtValNotif);
                Utilidades.SetEventosFecha(this.txtValLim);
                Utilidades.SetEventosFecha(this.txtValFin);

                ObtenerDatosAsunto();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del asunto", ex);
            }
            try
            {
                //Datos de las personas asignadas al asunto
                string strTabla = ObtenerRecursos(nIdAsunto.ToString(), this.hdnNodo.Value);
                string[] aRecursos = Regex.Split(strTabla, "@#@");
                if (aRecursos[0] == "OK") divR.InnerHtml = aRecursos[1];

                //Datos del historial del asunto
                strTabla = ObtenerHistorial(nIdAsunto.ToString());
                string[] aTareas = Regex.Split(strTabla, "@#@");
                if (aTareas[0] == "OK") divH.InnerHtml = aTareas[1];

                //Datos de los documentos asociados al asunto
                //strTabla = ObtenerDocumentos(nIdAsunto.ToString());
                //string[] aTabla = Regex.Split(strTabla, "@#@");
                //if (aTabla[0] == "OK") divDoc.InnerHtml = aTabla[1];
                string sEstado = TAREAPSP.getEstado(null, nIdT);
                divDoc.InnerHtml = Utilidades.ObtenerDocumentos("AS_T", nIdAsunto, this.hdnAcceso.Text, sEstado);
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
            case ("historial"):
                sResultado += ObtenerHistorial(aArgs[1]);
                break;
            case ("documentos"):
                //sResultado += ObtenerDocumentos(aArgs[1]);
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos("AS_T", int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
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
    private void ObtenerDatosAsunto()
    {
        int nTarea = 0;
        //Relleno el combo de tipo de asunto 
        this.cboTipo.DataValueField = "t384_idtipo";
        this.cboTipo.DataTextField = "t384_destipo";
        this.cboTipo.DataSource = TIPOASUNTO.Catalogo("", null, null, 3, 0);
        this.cboTipo.DataBind();

        if (nIdAsunto == -1)
        {
            nTarea = nIdT;
            txtValCre.Text = DateTime.Now.ToShortDateString();
            txtValNotif.Text = DateTime.Now.ToShortDateString();
            this.txtIdResponsable.Text = Session["NUM_EMPLEADO_ENTRADA"].ToString();
            this.txtResponsable.Text = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
            this.txtRegistrador.Text = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
        }
        else
        {
            ASUNTO_T o = ASUNTO_T.Select(tr, nIdAsunto);
            nTarea = o.t332_idtarea;
            txtIdAsunto.Text = o.t600_idasunto.ToString();
            txtDesAsunto.Text = o.t600_desasunto;
            txtDescripcion.Text = o.t600_desasuntolong;
            this.txtEtp.Text = o.t600_etp.ToString("N");
            this.txtEtr.Text = o.t600_etr.ToString("N");//#,##0.##
            if (o.t600_fcreacion.Year > 1900) txtValCre.Text = o.t600_fcreacion.ToShortDateString();
            if (o.t600_fnotificacion.Year > 1900) txtValNotif.Text = o.t600_fnotificacion.ToShortDateString();
            if (o.t600_flimite.Year > 1900) txtValLim.Text = o.t600_flimite.ToShortDateString();
            if (o.t600_ffin.Year > 1900) txtValFin.Text = o.t600_ffin.ToShortDateString();
            this.txtDpto.Text = o.t600_dpto;
            this.txtAlerta.Text = o.t600_alerta;
            this.txtObs.Text = o.t600_obs;
            this.txtRefExt.Text = o.t600_refexterna;
            this.txtSistema.Text = o.t600_sistema;
            this.cboEstado.SelectedValue = o.t600_estado.ToString();
            this.cboEstado.Text = o.Estado;
            this.txtEstadoAnt.Text = o.t600_estado.ToString();
            this.cboPrioridad.SelectedValue = o.t600_prioridad.ToString();
            this.cboPrioridad.Text = o.Prioridad;
            this.cboSeveridad.SelectedValue = o.t600_severidad.ToString();
            this.cboSeveridad.Text = o.Severidad;
            this.cboTipo.SelectedValue = o.t384_idtipo.ToString();
            this.cboTipo.Text = o.Tipo;
            this.txtNotificador.Text = o.t600_notificador;
            this.txtIdResponsable.Text = o.t600_responsable.ToString();
            this.txtResponsable.Text = o.Responsable;
            this.txtRegistrador.Text = o.Registrador;
        }

        TAREAPSP oTarea = TAREAPSP.Obtener(null, nTarea);
        PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, oTarea.t305_idproyectosubnodo);
        if (oPSN.t305_opd)
        {
            lblNumero.Style.Add("display", "none");
            txtIdAsunto.Style.Add("display", "none");
            lblCreacion.Style.Add("visibility", "hidden");
            txtValCre.Style.Add("visibility", "hidden");
            tsPestanas.Items[1].Disabled = true;
        }
    }
    protected string Grabar(string strDatosTarea, string slIntegrantes)
    {
        string sResul = "", sAccionBD, sIdRecurso;
        int iCodAsunto;
        byte iEstadoAnt, iEstadoAct;
        //DateTime? dFfp = null;
        //DateTime? dFLi = null;
        //DateTime? dFno = null;
        double dEtp , dEtr;

        bool bNotificable = false, bEnviarAlerta = true, bAlta = false;
        #region Abrir conexión y transacción
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
            #region Datos asunto
            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            // 0 -> id asunto (si -1 es un alta)
            // 1 -> descripcion corta
            // 2 -> descripcion larga
            // 3 -> referencia externa
            // 4 -> f/notificación
            // 5 -> f/fin
            // 6 -> f/limite
            // 7 -> alerta
            // 8 -> departamento
            // 9 -> tipo
            // 10-> estado
            // 11-> severidad
            // 12-> prioridad
            // 13-> sistema
            // 14-> esfuerzo planificado
            // 15-> esfuerzo real
            // 16 -> observaciones
            // 17 -> cod une (vacío)
            // 18 -> cod Tarea
            // 19 -> notificador
            // 20 -> responsable
            // 21 -> estado anterior
            // 22 -> desc Responsable
            // 23 -> desc Tipo
            // 24 -> desc Estado
            // 25 -> desc Severidad
            // 26 -> desc Prioridad
            // 27 -> cod T
            if (aDatosTarea[0] == "") iCodAsunto = -1;
            else iCodAsunto = int.Parse(aDatosTarea[0]);
            //if (aDatosTarea[4] != "") dFno = DateTime.Parse(aDatosTarea[4]);
            //if (aDatosTarea[5] != "") dFfp = DateTime.Parse(aDatosTarea[5]);
            //if (aDatosTarea[6] != "") dFLi = DateTime.Parse(aDatosTarea[6]);
            DateTime dFno = DateTime.Parse(aDatosTarea[4]);
            DateTime dFfp = DateTime.Parse((aDatosTarea[5] == "") ? "01/01/1900" : aDatosTarea[5]);
            DateTime dFLi = DateTime.Parse((aDatosTarea[6] == "") ? "01/01/1900" : aDatosTarea[6]);
            if (aDatosTarea[21] == "") iEstadoAnt = byte.Parse(aDatosTarea[10]);
            else iEstadoAnt = byte.Parse(aDatosTarea[21]);
            iEstadoAct = byte.Parse(aDatosTarea[10]);
            if (aDatosTarea[14] != "") dEtp=double.Parse(aDatosTarea[14]);
            else dEtp = 0;
            if (aDatosTarea[15] != "") dEtr = double.Parse(aDatosTarea[15]);
            else dEtr = 0;
            if (iCodAsunto == -1)
            {
                bAlta=true;
                iCodAsunto = ASUNTO_T.Insert(tr, int.Parse(aDatosTarea[18]),
                                Utilidades.unescape(aDatosTarea[7]), Utilidades.unescape(aDatosTarea[1]),
                                Utilidades.unescape(aDatosTarea[2]), Utilidades.unescape(aDatosTarea[8]),
                                iEstadoAct, dEtp, dEtr, dFfp, dFLi, dFno,
                                Utilidades.unescape(aDatosTarea[19]), Utilidades.unescape(aDatosTarea[16]),
                                byte.Parse(aDatosTarea[12]), Utilidades.unescape(aDatosTarea[3]),
                                int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()),
                                int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()),
                                byte.Parse(aDatosTarea[11]), Utilidades.unescape(aDatosTarea[13]),
                                int.Parse(aDatosTarea[9]));
                ASUNTOESTADO_T.Insert(tr, iCodAsunto, iEstadoAct, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
            }
            else
            {
                bAlta = false;
                ASUNTO_T.Update(tr, int.Parse(aDatosTarea[18]),Utilidades.unescape(aDatosTarea[7]),
                            Utilidades.unescape(aDatosTarea[1]),Utilidades.unescape(aDatosTarea[2]),
                            Utilidades.unescape(aDatosTarea[8]), iEstadoAct, dEtp, dEtr, dFfp, dFLi, dFno,
                            iCodAsunto,Utilidades.unescape(aDatosTarea[19]),Utilidades.unescape(aDatosTarea[16]),
                            byte.Parse(aDatosTarea[12]),Utilidades.unescape(aDatosTarea[3]),
                            int.Parse(aDatosTarea[20]),byte.Parse(aDatosTarea[11]),Utilidades.unescape(aDatosTarea[13]),
                            int.Parse(aDatosTarea[9]));
                if (iEstadoAnt != iEstadoAct)
                {
                    ASUNTOESTADO_T.Insert(tr, iCodAsunto, iEstadoAct, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                }
            }
            #endregion
            #region Datos integrantes
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
                                ASUNTORECURSOS_T.Insert(tr, int.Parse(sIdRecurso), iCodAsunto, bNotificable);
                                break;
                            case "D":
                                //delete
                                ASUNTORECURSOS_T.Delete(tr, iCodAsunto, int.Parse(sIdRecurso));
                                break;
                            case "U":
                                //update
                                ASUNTORECURSOS_T.Update(tr, int.Parse(sIdRecurso), iCodAsunto, bNotificable);
                                break;
                        }
                    }
                }//for
            }
            #endregion
            #region Enviar correos
            if (bEnviarAlerta)
            {
                //if (bAlta) sCad = "I";
                //else sCad = "U";
                EnviarCorreoAlerta(iCodAsunto.ToString(), strDatosTarea, slIntegrantes, bAlta);
            }
            #endregion
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + iCodAsunto.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del asunto", ex);
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
                DOCASU_T.Delete(tr, int.Parse(oDoc));
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

            sb.Append("<table id='tblOpciones' class='texto MAM' style='width:390px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:370px;' /></colgroup>");
            sb.Append("<tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString());
                //Los foraneos no tienen código de red luego para el envío de correo hay que cargar su e-mail
                sb.Append("' mail='" + dr["mail"].ToString());
                sb.Append("' style='height:20px; noWrap:true;' ");

                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ondblclick='convocarAux(this);' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[Profesional:&nbsp;");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
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
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las personas", ex);
        }
    }
    private string ObtenerRecursos(string sIdAsunto, string sNodo)
    {
        StringBuilder sb = new StringBuilder();
        string sDes;
        int i = 0;
        try
        {
            sb.Append("<table id='tblOpciones2' class='texto MM' style='width: 435px; mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:365px;' /><col style='width:40px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            SqlDataReader dr = ASUNTORECURSOS_T.SelectByt600_idasunto(tr, int.Parse(sIdAsunto));
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
                sb.Append("<nobr class='NBR W360'>" + sDes + "</NOBR></label></td>");

                sb.Append("<td><input type='checkbox' style='width:20px; margin-left:3px;' id='chkNot");
                sb.Append(i.ToString());
                sb.Append("' class='checkTabla' onclick='actualizarDatos(this);' ");
                if ((bool)dr["t604_notificar"]) sb.Append("checked=true");
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
    protected string ObtenerHistorial(string sIdAsunto)
    {
        //Cronología del asunto
        string sFecha="";

        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblTareas' style='width:700px; text-align:left;'>");
            //............................ estado........... Fecha................Profesional
            sb.Append("<colgroup><col style='width:135px;' /><col style='width:75px;' /><col style='width:490px;' /></colgroup>");
            SqlDataReader dr = ASUNTOESTADO_T.SelectByt600_idasunto(tr, int.Parse(sIdAsunto));

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px;'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["Estado"].ToString() + "</td>");
                sFecha = dr["t606_fecha"].ToString();
                if (sFecha != "")
                    sFecha = DateTime.Parse(dr["t606_fecha"].ToString()).ToShortDateString();
                sb.Append("<td>");
                sb.Append(sFecha);
                sb.Append("</td><td>");
                sb.Append(dr["nomRecurso"].ToString());
                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la cronología del asunto.", ex);
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
    private static string EnviarCorreoAlerta(string sIdAsunto, string strDatosTarea, string slIntegrantes, bool bAlta)
    {
        string sResul = "", sTexto = "", sTO = "", sAux, sIdResponsable, slMails, sToAux = "";
        string sAsunto = "", sAccionBD="";
        bool bNotificable = false;
        ArrayList aListCorreo = new ArrayList();
        StringBuilder sb = new StringBuilder();

        try
        {
            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            sIdResponsable = aDatosTarea[20];
            //slMails = Utilidades.unescape(aDatosTarea[7]);
            slMails = aDatosTarea[7];
            if (slMails == "" && sIdResponsable == "") return "OK@#@";
            sAsunto = "Alerta de asunto en Bitácora de tarea.";
            if (bAlta) sb.Append("<BR>SUPER le informa de la generación del siguiente asunto:<BR><BR>");
            else sb.Append("<BR>SUPER le informa de la modificación del siguiente asunto:<BR><BR>");
            // 0 -> id asunto (si -1 es un alta)
            // 1 -> descripcion corta
            // 2 -> descripcion larga
            // 3 -> referencia externa
            // 4 -> f/notificación
            // 5 -> f/fin
            // 6 -> f/limite
            // 7 -> alerta
            // 8 -> departamento
            // 9 -> tipo
            // 10-> estado
            // 11-> severidad
            // 12-> prioridad
            // 13-> sistema
            // 14-> esfuerzo planificado
            // 15-> esfuerzo real
            // 16 -> observaciones
            // 17 -> cod une
            // 18 -> cod Tarea
            // 19 -> notificador
            // 20 -> id responsable
            // 21 -> estado anterior
            // 22 -> nombre responsable
            // 23 -> desc tipo
            // 24-> desc estado
            // 25-> desc severidad
            // 26-> desc prioridad

            TAREAPSP oTar = TAREAPSP.Obtener(null, int.Parse(aDatosTarea[18]));

            sb.Append("<label style='width:120px'>Proyecto económico: </label>" + oTar.num_proyecto.ToString("#,###") + @" - " + oTar.nom_proyecto + "<br>");
            sb.Append("<label style='width:120px'>Proyecto técnico: </label>" + oTar.t331_despt + "<br>");
            sb.Append("<label style='width:120px'>Fase: </label>" + oTar.t334_desfase + "<br>");
            sb.Append("<label style='width:120px'>Actividad: </label>" + oTar.t335_desactividad + "<br>");
            sb.Append("<label style='width:120px'>Tarea: </label>" + int.Parse(aDatosTarea[18]).ToString("#,###") + @" - " + oTar.t332_destarea + "<br>");
            sb.Append("<label style='width:120px'>Asunto: </label><b>" + sIdAsunto + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
            sb.Append("<b>Información del asunto:</b><br>");

            sb.Append("<label style='width:120px'>Responsable: </label>" + Utilidades.unescape(aDatosTarea[22]) + "<br>");
            sAux = aDatosTarea[6];
            if (sAux == "")
                sb.Append("<label style='width:120px'>F/Límite: </label>&nbsp;<br>");
            else
                sb.Append("<label style='width:120px'>F/Límite: </label>" + sAux.Substring(0, 10) + "<br>");
            sAux = aDatosTarea[5];
            if (sAux == "")
                sb.Append("<label style='width:120px'>F/Fin: </label>&nbsp;<br>");
            else
                sb.Append("<label style='width:120px'>F/Fin: </label>" + sAux.Substring(0, 10) + "<br>");
            sb.Append("<label style='width:120px'>Ref. Externa: </label>" + Utilidades.unescape(aDatosTarea[3]) + "<br>");
            sb.Append("<label style='width:120px'>Esfuerzo planificado: </label>" + Utilidades.unescape(aDatosTarea[14]) + "<br>");
            sb.Append("<label style='width:120px'>Esfuerzo real: </label>" + Utilidades.unescape(aDatosTarea[15]) + "<br>");
            sb.Append("<label style='width:120px'>Severidad: </label>" + Utilidades.unescape(aDatosTarea[25]) + "<br>");
            sb.Append("<label style='width:120px'>Prioridad: </label>" + Utilidades.unescape(aDatosTarea[26]) + "<br>");
            sb.Append("<label style='width:120px'>Tipo: </label>" + Utilidades.unescape(aDatosTarea[23]) + "<br>");
            sb.Append("<label style='width:120px'>Estado: </label>" + Utilidades.unescape(aDatosTarea[24]) + "<br>");
            sb.Append("<label style='width:120px'>Sistema afectado: </label>" + Utilidades.unescape(aDatosTarea[13]) + "<br><br>");
            //descripcion larga
            sb.Append("<b><label style='width:120px'>Descripción: </label></b>" + Utilidades.unescape(aDatosTarea[2]) + "<br><br>");
            //observaciones
            sb.Append("<b><label style='width:120px'>Observaciones: </label></b>" + Utilidades.unescape(aDatosTarea[16]) + "<br><br>");
            //Departamento
            sb.Append("<b><label style='width:120px'>Departamento: </label></b>" + Utilidades.unescape(aDatosTarea[8]) + "<br><br>");

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
                    sTO = Utilidades.unescape(aMails[i]);
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
                    //sTO.Replace((char)10, (char)160);
                    //sTO.Replace((char)13, (char)160);
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
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de asunto de Bitácora de tarea. Asunto=" + sIdAsunto, ex);
        }
        return sResul;
    }
}
