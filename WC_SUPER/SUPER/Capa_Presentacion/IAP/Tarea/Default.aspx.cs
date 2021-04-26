using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using EO.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_IAP_ImpDiaria_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores="";
    public int nIdTarea, nObligaest, nPT;
    public string sDesTarea, sNotas="0";
    private ArrayList aListCorreo;
    public SqlConnection oConn;
    public SqlTransaction tr;
    protected string sEstado, sImputacion;
    public bool bEstadoLectura = false;

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
            
            //txtDesTarea.Attributes["maxlength"] = "100";
            nIdTarea    = int.Parse(Utilidades.decodpar(Request.QueryString["t"].ToString()));
            nObligaest  = int.Parse(Utilidades.decodpar(Request.QueryString["nObligaest"].ToString()));
            nPT         = int.Parse(Utilidades.decodpar(Request.QueryString["pt"].ToString()));
            sDesTarea   = Utilidades.decodpar(Request.QueryString["sDesTarea"].ToString());
            
            try
            {
                Utilidades.SetEventosFecha(this.txtFinEst);

                ObtenerDatosTarea();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la tarea", ex);
            }

            try
            {
                //string strTabla = ObtenerDocumentos(nIdTarea.ToString());
                //string[] aTabla = Regex.Split(strTabla, "@#@");
                //if (aTabla[0] == "OK") divCatalogoDoc.InnerHtml = aTabla[1];

                string sEstadoProy = TAREAPSP.getEstado(null, nIdTarea);
                string sPermiso="E";
                if (bEstadoLectura) sPermiso = "R";
                div1.InnerHtml = Utilidades.ObtenerDocumentos("IAP_T", nIdTarea, sPermiso, sEstadoProy);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la tarea", ex);
            }

            sEstado = Utilidades.decodpar(Request.QueryString["estado"]);
            sImputacion = Utilidades.decodpar(Request.QueryString["imputacion"]);
            switch (sEstado)//Estado
            {
                case "0"://Paralizada
                    bEstadoLectura = true;
                    break;
                case "1"://Activo
                    break;
                case "2"://Pendiente
                    bEstadoLectura = true;
                    break;
                case "3"://Finalizada
                    if (sImputacion == "0") bEstadoLectura = true;
                    break;
                case "4"://Cerrada
                    if (sImputacion == "0") bEstadoLectura = true;
                    break;
            }
            if (bEstadoLectura) ModoLectura.Poner(this.Controls);
            
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
        
        ////2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("documentos"):
                //sResultado += "OK@#@" + ObtenerDocumentos(aArgs[1]);
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos("IAP_T", int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
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

    private void ObtenerDatosTarea()
    {
        TAREAPSP o = TAREAPSP.ObtenerDatosRecurso(null, nIdTarea, int.Parse(Session["NUM_EMPLEADO_IAP"].ToString()));

        txtNumPE.Text = o.num_proyecto.ToString("#,###");
        txtPE.Text = o.nom_proyecto;
        txtPT.Text = o.t331_despt;
        txtFase.Text = o.t334_desfase;
        txtActividad.Text = o.t335_desactividad;

        txtIdTarea.Text = o.t332_idtarea.ToString("#,###");
        txtDesTarea.Text = o.t332_destarea;
        txtDescripcion.Text = o.t332_destarealong;

        txtPriCon.Text = (o.dPrimerConsumo.HasValue)? ((DateTime)o.dPrimerConsumo).ToShortDateString():"";
        txtUltCon.Text = (o.dPrimerConsumo.HasValue) ? ((DateTime)o.dUltimoConsumo).ToShortDateString() : "";
        txtConHor.Text = o.nConsumidoHoras.ToString("N");
        txtConJor.Text = o.nConsumidoJornadas.ToString("N");
        txtPteEst.Text = o.nPendienteEstimado.ToString("N");
        if (o.nAvanceTeorico > -1)
            txtAvanEst.Text = o.nAvanceTeorico.ToString("N");
        if (o.t336_etp > 0) txtTotPre.Text = o.t336_etp.ToString("N");
        txtFinPre.Text = (o.t336_ffp.HasValue)? ((DateTime)o.t336_ffp).ToShortDateString() : "";
        txtIndicaciones.Text = o.t336_indicaciones;
        txtColectivas.Text = o.t332_mensaje;

        if (o.t336_ete > 0) txtTotEst.Text = o.t336_ete.ToString("N");
        txtFinEst.Text = (o.t336_ffe.HasValue) ? ((DateTime)o.t336_ffe).ToShortDateString() : "";
        txtComentario.Text = o.t336_comentario;

        if (o.nCompletado == 1) chkFinalizada.Checked = true;

        txtNotas1.Text = o.t332_notas1;
        txtNotas2.Text = o.t332_notas2;
        txtNotas3.Text = o.t332_notas3;
        txtNotas4.Text = o.t332_notas4;

        if (!o.t332_notasiap) tsPestanas.Items[2].Disabled = true;
        else sNotas = "1";
    }
    //private string ObtenerDocumentos(string sIdTarea)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    bool bModificable;
    //    SqlDataReader dr = DOCUT.Catalogo(null, int.Parse(sIdTarea), "", "", "", null, null, null, true, null, 3, 0);

    //    sb.Append("<div style='background-image:url(../../../Images/imgFT20.gif); width:0%; height:0%'>");
    //    sb.Append("<table id='tblDocumentos' class='texto' style='WIDTH: 800px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0'>");
    //    sb.Append("<colgroup><col style='width:265px;padding-left:5px;' /><col width='215px' /><col width='225px' /><col width='100px' /></colgroup>");
    //    sb.Append("<tbody>");
    //    while (dr.Read())
    //    {   //Si el archivo NO es sólo lectura, o si el usuario es el autor del archivo, o es administrador, se permite modificar.
    //        if ((dr["t314_idusuario_autor"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString() || Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A")
    //            || (!(bool)dr["t363_privado"] && !(bool)dr["t363_modolectura"]))
    //            bModificable = true;
    //        else
    //            bModificable = false;

    //        sb.Append("<tr id='" + dr["t363_iddocut"].ToString() + "' onclick='mmse(this);' sTipo='T' sAutor='" + dr["t314_idusuario_autor"].ToString() + "' onmouseover='TTip()' style='height:20px;'>");

    //        //Si el archivo NO es sólo lectura, o si el usuario es el autor del archivo, o es administrador, se permite modificar.
    //        if (bModificable)
    //            sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id, 'IAP')\"><nobr class='NBR' style='width:255px'>" + dr["t363_descripcion"].ToString() + "</nobr></td>");
    //        else
    //            sb.Append("<td class='MANO'><nobr class='NBR' style='width:255px'>" + dr["t363_descripcion"].ToString() + "</nobr></td>");

    //        if (dr["t363_nombrearchivo"].ToString() == "")
    //        {
    //            if (bModificable)
    //                sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
    //            else
    //                sb.Append("<td class='MANO'></td>");
    //        }
    //        else
    //        {
    //            string sNomArchivo = dr["t363_nombrearchivo"].ToString() + Utilidades.TamanoArchivo((int)dr["bytes"]);
    //            //Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
    //            if ((!(bool)dr["t363_privado"]) || ((bool)dr["t363_privado"] && dr["t314_idusuario_autor"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString()) || Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A")
    //                sb.Append("<td><img src=\"../../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
    //            else
    //                sb.Append("<td><img src=\"../../../images/imgSeparador.gif\" width='16px' height='16px' style='vertical-align:bottom;'>");
    //            if (bModificable)
    //                sb.Append("&nbsp;<nobr class='NBR MA' style='width:205px;' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
    //            else
    //                sb.Append("&nbsp;<nobr class='NBR MANO' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
    //        }

    //        if (dr["t363_weblink"].ToString() == "")
    //        {
    //            if (bModificable)
    //                sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
    //            else
    //                sb.Append("<td class='MANO'></td>");
    //        }
    //        else
    //        {
    //            string sHTTP = "";
    //            if (dr["t363_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
    //            sb.Append("<td><a href='" + sHTTP + dr["t363_weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr["t363_weblink"].ToString() + "</nobr></a></td>");
    //        }
    //        if (bModificable)
    //            sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR' style='width:140px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
    //        else
    //            sb.Append("<td class='MANO'><nobr class='NBR' style='width:140px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
    //    }
    //    dr.Close();
    //    dr.Dispose();
    //    sb.Append("</tbody>");
    //    sb.Append("</table>");
    //    sb.Append("</div>");

    //    return "OK@#@" + sb.ToString();
    //}

    protected string Grabar(string strDatos)
    {
        string sResul = "";
        aListCorreo = new ArrayList();

        #region Abrir conexión y transacción
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
            string[] aValores = Regex.Split(strDatos, "##");
            ///aValores[0] = IdTarea
            ///aValores[1] = nETE
            ///aValores[2] = sFFE
            ///aValores[3] = sObservaciones
            ///aValores[4] = nFinalizado;
            ///aValores[5] = nFinalizadoAntes;
            ///aValores[6] = nPT;
            ///aValores[7] = Desc Tarea;
            ///aValores[8] = sNotas;
            ///aValores[9] = txtNotas1;
            ///aValores[10] = txtNotas2;
            ///aValores[11] = txtNotas3;
            ///aValores[12] = txtNotas4;
            ///aValores[13] = sComentarioOriginal;
            ///aValores[14] = nETEOriginal;
            ///aValores[15] = sFFEOriginal;

            double nETE = 0;
            DateTime? sFFE = null;

            if (aValores[1] != "") nETE = double.Parse(aValores[1]);
            if (aValores[2] != "") sFFE = DateTime.Parse(aValores[2]);

            TareaRecurso.ActualizarEstimacion(tr, int.Parse(Session["NUM_EMPLEADO_IAP"].ToString()), int.Parse(aValores[0]), sFFE, nETE, Utilidades.unescape(aValores[3]), (aValores[4]=="1")?true:false);

            if (aValores[5] == "0" && aValores[4] == "1")
            {
                //Si antes la tarea no estaba finalizada y ahora sí,
                //Mail al (a los) RTPT indicando que se ha finalizado
                //y continuar con la grabación
//                TareaFinalizada(tr, int.Parse(aValores[0]), int.Parse(aValores[6]));
            }

            if (aValores[3] != aValores[13] || aValores[1] != aValores[14] || aValores[2] != aValores[15])
            {
                //Si se han modificado los comentarios del técnico al gerente
                //Mail al (a los) RTPT 
//                EstimacionModificada(tr, int.Parse(aValores[0]), int.Parse(aValores[6]));
            }

            if (aValores[8] == "1")
            {
                TAREAPSP.ActualizarNotas(tr, int.Parse(aValores[0]), Utilidades.unescape(aValores[9]), Utilidades.unescape(aValores[10]), Utilidades.unescape(aValores[11]), Utilidades.unescape(aValores[12]));
            }

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la tarea", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        try
        {
            if (aListCorreo.Count > 0)
                Correo.EnviarCorreos(aListCorreo);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar el mail a los responsables técnicos de proyectos técnicos", ex);
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
                DOCUT.Delete(tr, int.Parse(oDoc));
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

}
