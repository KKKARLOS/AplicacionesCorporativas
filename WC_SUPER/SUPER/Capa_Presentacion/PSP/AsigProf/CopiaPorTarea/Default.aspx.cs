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
    protected string strInicial = "", sErrores;//, sLectura = "false";
    public string sNodo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Copia de tareas asignadas";
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
                    SqlDataReader dr = USUARIO.ObtenerDatosProfUsuario((int)Session["UsuarioActual"]);
                    if (dr.Read())
                        this.hdnCRActual.Value = dr["t303_idnodo"].ToString();
                    dr.Close();
                    dr.Dispose();
                    txtApellido1.Focus();
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
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3], (aArgs[4] == "1") ? true : false);
                break;
            case ("grabar"):
                sResultado += Grabar((aArgs[1] == "1") ? true : false,
                                    (aArgs[2] == "1") ? true : false,
                                    aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("getItems"):
                sResultado += ObtenerItems(int.Parse(aArgs[1]), aArgs[2], (aArgs[3] == "1") ? true : false);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
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

    private string ObtenerPersonas(string sAP1, string sAP2, string sNom, bool bSoloActivos)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            //SqlDataReader dr = USUARIO.ObtenerProfesionalesPST(Utilidades.unescape(sAP1), Utilidades.unescape(sAP2), Utilidades.unescape(sNom));
            SqlDataReader dr =
                Recurso.ObtenerRelacionProfesionalesTarifa("N", Utilidades.unescape(sAP1), Utilidades.unescape(sAP2),
                                                           Utilidades.unescape(sNom), "", "", "C", "", bSoloActivos);
            sb.Append("<table id='tblOpciones' class='texto MANO' style='WIDTH: 440px; table-layout:fixed;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional&nbsp;:</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>Empresa&nbsp;:</label>");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");

                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "'>");
                sb.Append("<td></td><td><nobr class='NBR W420'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los usuarios", ex);
            return "error@#@";
        }
    }
    private string ObtenerItems(int idRecurso, string sTipoItem, bool bSoloActivos)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        try
        {
            sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            sb.Append("<table id='tblTareas' class='texto' style='width: 440px; border-collapse: collapse; table-layout:fixed;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:420px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            dr = Recurso.CatalogoTareas(null, idRecurso, sTipoItem, (int)Session["UsuarioActual"], bSoloActivos);
            if (dr != null)
            {
                while (dr.Read())
                {
                    switch (sTipoItem)
                    {
                        case "T":
                            if ((bool)dr["t305_admiterecursospst"])
                                sb.Append("<tr id='" + dr["t332_idtarea"].ToString() + "' a='S' ");
                            else
                                sb.Append("<tr id='" + dr["t332_idtarea"].ToString() + "' a='N' ");
                            sb.Append(" nodo='" + dr["t303_idnodo"].ToString());
                            sb.Append("' cierre='" + dr["t303_ultcierreeco"].ToString());
                            sb.Append("' psn='" + dr["t305_idproyectosubnodo"].ToString());
                            if ((bool)dr["t332_notif_prof"])
                                sb.Append("' notif='S'");
                            else
                                sb.Append("' notif='N'");
                            sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                            sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                            sb.Append("<label style='width:40px'>" + sNodo + "&nbsp;:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.E.&nbsp;:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.T.&nbsp;:</label>" + dr["t331_despt"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>Fase&nbsp;:</label>" + dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>Activ.&nbsp;:</label>" + dr["t335_desactividad"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>Tarea&nbsp;:</label>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " " + dr["t332_destarea"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");

                            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W410'");
                            if (dr["baja"].ToString() == "1") sb.Append(" style='color:red;'");
                            sb.Append(">" + dr["t332_destarea"].ToString() + "</nobr></td>");
                            break;
                        case "A":
                            if ((bool)dr["t305_admiterecursospst"])
                                sb.Append("<tr id='" + dr["t335_idactividad"].ToString() + "' a='S' ");
                            else
                                sb.Append("<tr id='" + dr["t335_idactividad"].ToString() + "' a='N' ");
                            sb.Append(" nodo='" + dr["t303_idnodo"].ToString());
                            sb.Append("' cierre='" + dr["t303_ultcierreeco"].ToString());
                            sb.Append("' psn='" + dr["t305_idproyectosubnodo"].ToString());
                            sb.Append("' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                            sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                            sb.Append("<label style='width:40px'>" + sNodo + "&nbsp;:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.E.&nbsp;:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.T.&nbsp;:</label>" + dr["t331_despt"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>Fase&nbsp;:</label>" + dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>Activ.&nbsp;:</label>" + dr["t335_desactividad"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");

                            sb.Append("<td><nobr class='NBR W410'");
                            if (dr["baja"].ToString() == "1") sb.Append(" style='color:red;'");
                            sb.Append(">" + dr["t335_desactividad"].ToString() + "</nobr></td>");
                            break;
                        case "F":
                            if ((bool)dr["t305_admiterecursospst"])
                                sb.Append("<tr id='" + dr["t334_idfase"].ToString() + "' a='S' ");
                            else
                                sb.Append("<tr id='" + dr["t334_idfase"].ToString() + "' a='N' ");
                            sb.Append(" nodo='" + dr["t303_idnodo"].ToString());
                            sb.Append("' cierre='" + dr["t303_ultcierreeco"].ToString());
                            sb.Append("' psn='" + dr["t305_idproyectosubnodo"].ToString());
                            sb.Append("' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                            sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                            sb.Append("<label style='width:40px'>" + sNodo + "&nbsp;:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.E.&nbsp;:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.T.&nbsp;:" + dr["t331_despt"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>Fase&nbsp;:</label>" + dr["t334_desfase"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");

                            sb.Append("<td><nobr class='NBR W410'");
                            if (dr["baja"].ToString() == "1") sb.Append(" style='color:red;'");
                            sb.Append(">" + dr["t334_desfase"].ToString() + "</nobr></td>");
                            break;
                        case "P":
                            if ((bool)dr["t305_admiterecursospst"])
                                sb.Append("<tr id='" + dr["t331_idpt"].ToString() + "' a='S' ");
                            else
                                sb.Append("<tr id='" + dr["t331_idpt"].ToString() + "' a='N' ");
                            sb.Append(" nodo='" + dr["t303_idnodo"].ToString());
                            sb.Append("' cierre='" + dr["t303_ultcierreeco"].ToString());
                            sb.Append("' psn='" + dr["t305_idproyectosubnodo"].ToString());
                            sb.Append("' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                            sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                            sb.Append("<label style='width:40px'>" + sNodo + "&nbsp;:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.E.&nbsp;:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.T.&nbsp;:</label>" + dr["t331_despt"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");

                            sb.Append("<td><nobr class='NBR W410'");
                            if (dr["baja"].ToString() == "1") sb.Append(" style='color:red;'");
                            sb.Append(">" + dr["t331_despt"].ToString() + "</nobr></td>");
                            break;
                        case "E":
                            if ((bool)dr["t305_admiterecursospst"])
                                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' a='S' ");
                            else
                                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' a='N' ");
                            sb.Append(" nodo='" + dr["t303_idnodo"].ToString());
                            sb.Append("' cierre='" + dr["t303_ultcierreeco"].ToString());
                            sb.Append("' psn='" + dr["t305_idproyectosubnodo"].ToString());
                            sb.Append("' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                            sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                            sb.Append("<label style='width:40px'>" + sNodo + "&nbsp;:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("<br><label style='width:40px'>P.E.&nbsp;:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                            sb.Append("] hideselects=[off]\">");

                            sb.Append("<td><nobr class='NBR W410'");
                            if (dr["baja"].ToString() == "1") sb.Append(" style='color:red;'");
                            sb.Append(">" + dr["t301_denominacion"].ToString() + "</nobr></td>");
                            break;
                    }
                    sb.Append("<td><input type='checkbox' style='width:15' class='checkTabla' checked='true'></td>");
                    sb.Append("</tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los items del usuario", ex);
            return "error@#@";
        }
    }

    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3, string sCodUne,
                                    string t305_idProyectoSubnodo, string sCualidad)
    {
        //Relacion de técnicos candidatos a ser asignados a la actividad
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();
        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);

            SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "", true);
            sb.Append("<table id='tblOpciones2' class='texto MAM' style='WIDTH: 440px; table-layout:fixed;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional:&nbsp;</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>Empresa&nbsp;:</label>");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");

                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' onclick='mm(event);' ondblclick='insertarRecurso(this);' onmousedown='DD(event)'>");
                sb.Append("<td></td><td><nobr class='NBR W420'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }
    /// <summary>
    /// bSoloAsignadas implica que solo se van a asignar profesionales a las tareas en las que el usuario original estuviera asignado
    /// </summary>
    /// <param name="bSoloAsignadas"></param>
    /// <param name="sIdRecursoOrigen"></param>
    /// <param name="sTipoItem"></param>
    /// <param name="sRecursos"></param>
    /// <param name="sItems"></param>
    /// <returns></returns>
    private string Grabar(bool bSoloAsignadas, bool bSoloActivas, string sIdRecursoOrigen, string sTipoItem, string sRecursos, string sItems)
    {
        string sResul = "", sIdRecurso, sItem;
        bool bNotificar = false;
        try
        {
            if (sIdRecursoOrigen == "" || sTipoItem == "" || sRecursos == "" || sItems == "")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aRecursos = Regex.Split(sRecursos, @"##");
                for (int i = 0; i < aRecursos.Length; i++)
                {
                    sIdRecurso = aRecursos[i];
                    string[] aItems = Regex.Split(sItems, @"##");
                    for (int j = 0; j < aItems.Length; j++)
                    {
                        sItem = aItems[j];
                        string[] aIt = Regex.Split(sItem, @",");
                        //ID item, asociar a proyecto, id nodo, anomes del ultimo cierre economico, id proyectosubnodo, notificar (solo en tareas)
                        if (sTipoItem == "T")
                        {
                            if (aIt[5] == "S") bNotificar = true;
                            else bNotificar = false;
                        }
                        sResul += PonerRecurso(bSoloAsignadas, bSoloActivas, int.Parse(sIdRecursoOrigen), sTipoItem, int.Parse(sIdRecurso), 
                                               int.Parse(aIt[0]), aIt[1],  int.Parse(aIt[2]), int.Parse(aIt[3]), int.Parse(aIt[4]), 
                                               bNotificar);
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
    private string PonerRecurso(bool bSoloAsignadas, bool bSoloActivas, int iRecursoOrigen, string sTipoItem, int IdRecurso, int IdItem, 
                                string sHaciaProy, int IdNodo, int iUltCierreEco, int IdPsn, bool bNotifProf)
    {
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        string sResul = "";
        bool bAdmiteRecursoPST, bRecursoAsignado=false;
        try
        {
            if (sHaciaProy == "S")
                bAdmiteRecursoPST = true;
            else
                bAdmiteRecursoPST = false;
            //Abro transaccion
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);

            switch (sTipoItem)
            {
                case "E":
                    PROYECTOSUBNODO.AsignarTareasProfesional(tr, bSoloAsignadas, bSoloActivas, iRecursoOrigen, IdItem, IdRecurso, null, null, 
                                                    -1, "", false, bAdmiteRecursoPST, IdNodo, iUltCierreEco);
                    break;
                case "P":
                    ProyTec.AsignarTareasProfesional(tr, bSoloAsignadas, bSoloActivas, iRecursoOrigen, IdItem, IdRecurso, null, null, -1, "", false, bAdmiteRecursoPST, IdPsn, IdNodo, iUltCierreEco);
                    break;
                case "F":
                    FASEPSP.AsignarTareasProfesional(tr, bSoloAsignadas, bSoloActivas, iRecursoOrigen, IdItem, IdRecurso, null, null, -1, "", false, bAdmiteRecursoPST, IdPsn, IdNodo, iUltCierreEco);
                    break;
                case "A":
                    ACTIVIDADPSP.AsignarTareasProfesional(tr, bSoloAsignadas, bSoloActivas, iRecursoOrigen, IdItem, IdRecurso, null, null, -1, "", false, bAdmiteRecursoPST, IdPsn, IdNodo, iUltCierreEco);
                    break;
                case "T":
                    //iNumAsig = TareaRecurso.InsertarSNE(tr, IdItem, IdRecurso, null, null, null, null, null, null, 1, null, "", false);
                    bRecursoAsignado = TareaRecurso.InsertarTEC(tr, IdItem, IdRecurso, null, null, null, null, null, null, 1, null, "", false,
                                                                bAdmiteRecursoPST, IdPsn, IdNodo, iUltCierreEco);
                    if (bNotifProf && bRecursoAsignado)//se notifica a profesionales y no estaba ya asignado a la tarea
                    {
                        TAREAPSP oTar = TAREAPSP.Obtener(null, IdItem);
                        TAREAPSP oTar2 = TAREAPSP.ObtenerOTC(null, IdItem);
                        string oRec = "##" + IdItem.ToString() + "##" + IdRecurso.ToString() + "################";
                        oRec += Utilidades.escape(oTar.t332_destarea) + "##";
                        oRec += oTar.num_proyecto.ToString() + "##" + Utilidades.escape(oTar.nom_proyecto) + "##";
                        oRec += Utilidades.escape(oTar.t331_despt) + "##" + Utilidades.escape(oTar.t334_desfase) + "##" + Utilidades.escape(oTar.t335_desactividad) + "##";
                        oRec += Utilidades.escape(oTar2.t346_codpst) + "##" + Utilidades.escape(oTar2.t346_despst) + "##";
                        oRec += Utilidades.escape(oTar.t332_otl) + "##" + Utilidades.escape(oTar.t332_incidencia) + "##";

                        TareaRecurso.EnviarCorreoRecurso(tr, "I", oRec, "", "", "", "", Utilidades.escape(oTar.t332_mensaje));
                    }
                    break;
            }
            //if (sHaciaProy == "S") 
            //{
            //    if (!TareaRecurso.AsociadoAProyecto(tr, IdPsn, IdRecurso))
            //    {//lA FECHA DE alta en el proyecto será la siguiente al último mes cerrado del nodo
            //        DateTime dtFechaAlta = Fechas.AnnomesAFecha(Fechas.AddAnnomes(iUltCierreEco, 1));
            //        TareaRecurso.AsociarAProyecto(tr, IdNodo, IdRecurso, IdPsn, null, dtFechaAlta, null);
            //    }
            //    else
            //        TareaRecurso.ReAsociarAProyecto(tr, IdRecurso, IdPsn);
            //}
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
}
