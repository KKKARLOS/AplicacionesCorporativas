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
using EO.Web;
//using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;




public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial = "", sErrores;
    public string gsAcceso, sNodo = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Copia de profesionales asignados";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    //if (!(bool)Session["FORANEOS"])
                    //{
                    //    this.imgForaneo.Visible = false;
                    //    this.lblForaneo.Visible = false;
                    //}
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.txtIdTarea.Focus();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("recursos"):
                sResultado += ObtenerRecursos(aArgs[1], aArgs[2]);
                break;
            case ("buscar"):
                sResultado += ObtenerTareas(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("buscar2"):
                sResultado += ObtenerTareas2(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("recuperarPSN"):
            case ("recuperarPSN2"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("getTarea1"):
                sResultado += getTarea1(aArgs[1]);
                break;
            case ("getTarea2"):
                sResultado += getTarea2(aArgs[1]);
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

    private string ObtenerRecursos(string sIdTarea, string sCodUne)
    {// Devuelve el código HTML del catalogo de recursos asociados a la tarea
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            sb.Append("<div style='background-image:url(../../../../Images/imgFT20.gif); width:460px'>");
            sb.Append("<table id='tblOpciones3' class='texto' style='width: 460px;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width:20px'/><col style='width:425px'/><col style='width:15px'/></colgroup>");//style='padding-left:5px' 
            sb.Append("<tbody>");
            if (sIdTarea != "")
            {
                dr = TareaRecurso.Catalogo(int.Parse(sIdTarea), false);
                while (dr.Read())
                {
                    if (dr["t336_estado"].ToString() == "1")
                    {//Solo muestro los activos en la tarea
                        sb.Append("<tr style='height:20px' bd='' id='" + dr["t314_idusuario"].ToString() + "' ");
                        sb.Append("estado='" + dr["t336_estado"].ToString() + "' ");
                        sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                        sb.Append("baja='" + dr["baja"].ToString() + "' ");
                        sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                        //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                        //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                        //else sb.Append("tipo='N' ");
                        sb.Append("><td></td><td><nobr class='NBR W410'>" + dr["empleado"].ToString() + "</nobr></td>");
                        sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' checked='true'></td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</tbody>");
                sb.Append("</table></div>");
                dr.Close(); dr.Dispose();
            }
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las personas ", ex);
        }
    }
    private string ObtenerTareas(string sPE, string sPT, string sF, string sA)
    {// Devuelve el código HTML del catalogo de tareas 
        StringBuilder sb = new StringBuilder();
        string sId;
        SqlDataReader dr;
        try
        {
            if (sPT == "")
                dr = TAREAPSP.CatalogoPE(int.Parse(sPE), (int)Session["UsuarioActual"]);
            else
            {
                if (sF == "")
                {
                    if (sA == "")
                        dr = TAREAPSP.CatalogoPT(int.Parse(sPT));
                    else
                        dr = TAREAPSP.CatalogoA(int.Parse(sA));
                }
                else
                {
                    if (sA == "")
                        dr = TAREAPSP.CatalogoF(int.Parse(sF));
                    else
                        dr = TAREAPSP.CatalogoA(int.Parse(sA));
                }
            }
            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width:460px'>");
            sb.Append("<table id='tblOpciones' class='texto MANO' style='width: 460px; table-layout:fixed;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width: 460px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sId = dr["t332_idtarea"].ToString();
                sb.Append("<tr id='" + sId + "' onClick='ms(this);getRecursos(this.id)' style='height:16px'");
                sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                sb.Append("<label style='width:60px'>P.T.&nbsp;:</label>" + dr["t331_despt"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Fase&nbsp;:</label>" + dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Actividad&nbsp;:</label>" + dr["t335_desactividad"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Tarea&nbsp;:</label>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###"));
                sb.Append(" " + dr["t332_destarea"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W445'>" + dr["t332_destarea"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table></div>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tareas ", ex);
        }
    }
    private string ObtenerTareas2(string sPE, string sPT, string sF, string sA)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            if (sPT == "")
                dr = TAREAPSP.CatalogoPE(int.Parse(sPE), (int)Session["UsuarioActual"]);
            else
            {
                if (sF == "")
                {
                    if (sA == "")
                        dr = TAREAPSP.CatalogoPT(int.Parse(sPT));
                    else
                        dr = TAREAPSP.CatalogoA(int.Parse(sA));
                }
                else
                {
                    if (sA == "")
                        dr = TAREAPSP.CatalogoF(int.Parse(sF));
                    else
                        dr = TAREAPSP.CatalogoA(int.Parse(sA));
                }
            }
            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width: 460px;'>");
            sb.Append("<table id='tblOpciones2' class='texto MANO' style='width: 460px;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width:445px;' /><col style='width:15px'/></colgroup>");//style='padding-left:5px' 
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t332_idtarea"].ToString() + "' ");
                if ((bool)dr["t332_notif_prof"])
                    sb.Append(" notif='S' ");
                else
                    sb.Append(" notif='N' ");
                sb.Append("onClick='mm(event)' style='height:16px' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                sb.Append("<label style='width:60px'>P.T.&nbsp;:</label>" + dr["t331_despt"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Fase&nbsp;:</label>" + dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Actividad&nbsp;:</label>" + dr["t335_desactividad"].ToString().Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Tarea&nbsp;:</label>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " " + dr["t332_destarea"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W430'>" + dr["t332_destarea"].ToString() + "</nobr></td>");
                sb.Append("<td><input type='checkbox' style='width:15' class='checkTabla' checked='true'></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("<tbody>");
            sb.Append("</table></div>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tareas ", ex);
        }
    }
    private string recuperarPSN(string nPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nPSN), (int)Session["UsuarioActual"], "PST");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");// + "@#@");  //4
                //sb.Append(dr["estado"].ToString());  //5
                if ((bool)dr["t305_admiterecursospst"]) sb.Append("S");//5
                else sb.Append("N");//5
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }
    private string Grabar(string sIdTareaOrigen, string sRecursos, string sTareas)
    {
        string sResul = "", sIdRecurso, sItem;
        bool bNotificar = false;
        try
        {
            if (sIdTareaOrigen == "" || sRecursos == "" || sTareas == "")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aRecursos = Regex.Split(sRecursos, @"##");
                for (int i = 0; i < aRecursos.Length; i++)
                {
                    sIdRecurso = aRecursos[i];
                    string[] aItems = Regex.Split(sTareas, @"##");
                    for (int j = 0; j < aItems.Length; j++)
                    {
                        sItem = aItems[j];
                        string[] aIt = Regex.Split(sItem, @",");
                        //ID item, notificar
                        if (aIt[1] == "S") bNotificar = true;
                        else bNotificar = false;
                        sResul += PonerRecurso("T", int.Parse(sIdRecurso), int.Parse(aIt[0]), bNotificar);
                    }
                }//for
            }
            if (sResul != "")
                sResul = "Error@#@" + sResul;
            else
                sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
        }
        return sResul;
    }
    private string PonerRecurso(string sTipoItem, int IdRecurso, int nIdTarea, bool bNotifProf)
    {
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        string sResul = "";
        //bool bAdmiteRecursoPST = true, bRecursoAsignado = false;
        try
        {
            //Abro transaccion
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);

            //bRecursoAsignado = TareaRecurso.InsertarTEC(tr, nIdTarea, IdRecurso, null, null, null, null, null, null, 1, null, "", false,
            //                                            bAdmiteRecursoPST, IdPsn, IdNodo, iUltCierreEco);
            int iRes = TareaRecurso.InsertarSNE(tr, nIdTarea, IdRecurso, null, null, null, null, null, null, 1, "", "", false);

            if (bNotifProf && iRes != 0)//se notifica a profesionales y no estaba ya asignado a la tarea
            {
                TAREAPSP oTar = TAREAPSP.Obtener(null, nIdTarea);
                TAREAPSP oTar2 = TAREAPSP.ObtenerOTC(null, nIdTarea);
                string oRec = "##" + nIdTarea.ToString() + "##" + IdRecurso.ToString() + "################";
                oRec += Utilidades.escape(oTar.t332_destarea) + "##";
                oRec += oTar.num_proyecto.ToString() + "##" + Utilidades.escape(oTar.nom_proyecto) + "##";
                oRec += Utilidades.escape(oTar.t331_despt) + "##" + Utilidades.escape(oTar.t334_desfase) + "##" + Utilidades.escape(oTar.t335_desactividad) + "##";
                oRec += Utilidades.escape(oTar2.t346_codpst) + "##" + Utilidades.escape(oTar2.t346_despst) + "##";
                oRec += Utilidades.escape(oTar.t332_otl) + "##" + Utilidades.escape(oTar.t332_incidencia) + "##";

                TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, "", "", "", "", Utilidades.escape(oTar.t332_mensaje));
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = Errores.mostrarError("Error al grabar", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string getTarea1(string sTarea)
    {
        StringBuilder sb = new StringBuilder();
        string sId;
        try
        {
            TAREAPSP oTar = TAREAPSP.Obtener(null, int.Parse(sTarea.Replace(".", "")));
            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width: 460px;'>");
            sb.Append("<table id='tblOpciones' class='texto MANO' style='width: 460px;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width: 460px;' /></colgroup>");
            sb.Append("<tbody>");
            if (oTar.t332_idtarea != 0)
            {
                sId = oTar.t332_idtarea.ToString();
                sb.Append("<tr id='" + sId + "' onclick='ms(this);getRecursos(this.id)' style='height:16px'");
                sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                sb.Append("<label style='width:60px'>P.T.&nbsp;:</label>" + oTar.t331_despt.Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Fase&nbsp;:</label>" + oTar.t334_desfase.Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Actividad&nbsp;:</label>" + oTar.t335_desactividad.Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Tarea&nbsp;:</label>" + int.Parse(sId).ToString("#,###"));
                sb.Append(" " + oTar.t332_destarea.Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W445'>" + oTar.t332_destarea + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table></div>");
            return "OK@#@" + oTar.t332_destarea + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la tarea ", ex);
        }
    }
    private string getTarea2(string sTarea)
    {
        StringBuilder sb = new StringBuilder();
        string sId;
        try
        {
            TAREAPSP oTar = TAREAPSP.Obtener(null, int.Parse(sTarea.Replace(".", "")));
            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width: 460px;'>");
            sb.Append("<table id='tblOpciones2' class='texto MANO' style='width: 460px;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width:445px' /><col style='width:15px'/></colgroup>");
            sb.Append("<tbody>");
            if (oTar.t332_idtarea != 0)
            {
                sId = oTar.t332_idtarea.ToString();
                sb.Append("<tr id='" + sId + "' onclick='ms(this);' style='height:16px'");
                if (oTar.t332_notif_prof)
                    sb.Append(" notif='S' ");
                else
                    sb.Append(" notif='N' ");

                sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                sb.Append("<label style='width:60px'>P.T.&nbsp;:</label>" + oTar.t331_despt.Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Fase&nbsp;:</label>" + oTar.t334_desfase.Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Actividad&nbsp;:</label>" + oTar.t335_desactividad.Replace((char)34, (char)39));
                sb.Append("<br><label style='width:60px'>Tarea&nbsp;:</label>" + int.Parse(sId).ToString("#,###"));
                sb.Append(" " + oTar.t332_destarea.Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td><nobr class='NBR W430'>" + oTar.t332_destarea + "</nobr></td>");
                sb.Append("<td><input type='checkbox' style='width:15' class='checkTabla' checked='true'></td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table></div>");
            return "OK@#@" + oTar.t332_destarea + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la tarea ", ex);
        }
    }

}
