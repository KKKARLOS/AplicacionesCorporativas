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

//using System.Collections.Generic;
using EO.Web;
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

            Master.TituloPagina = "Copia de profesionales por proyecto";
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
                    SqlDataReader dr= USUARIO.ObtenerDatosProfUsuario((int)Session["UsuarioActual"]);
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getItems"):
                sResultado += ObtenerItems(int.Parse(aArgs[1]), (aArgs[2] == "1") ? true : false);
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
            sb.Append("<table id='tblOpciones' class='texto MANO' style='width:440px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;noWrap:true;' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional&nbsp;:</label>");
                sb.Append(dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");

                sb.Append("<label style='width:60px'>Usuario&nbsp;:</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));

                sb.Append("<br><label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("onclick='ms(this); getProys();'>");
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
    private string ObtenerItems(int idRecurso, bool bSoloActivos)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr=null;
        string sDeriva, sNotif;
        try
        {
            sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            sb.Append("<table id='tblTareas' class='texto' style='width:440px;'>");
            sb.Append("<colgroup><col style='width:420px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            dr = Recurso.CatalogoTareas(null, idRecurso, "E2", (int)Session["UsuarioActual"], bSoloActivos);
            if (dr != null)
            {
                while (dr.Read())
                {
                    //if ((bool)dr["t305_admiterecursospst"])
                    //    sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' a='S' ");
                    //else
                    //    sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' a='N' ");

                    sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString());
                    sb.Append("' nodo='" + dr["t303_idnodo"].ToString());
                    sb.Append("' cierre='" + dr["t303_ultcierreeco"].ToString());
                    sb.Append("' psn='" + dr["t305_idproyectosubnodo"].ToString());
                    //sb.Append("' baja='" + dr["baja"].ToString());

                    if ((bool)dr["t330_deriva"])
                        sDeriva = "T";
                    else
                        sDeriva = "F";
                    sb.Append("' deriva='" + sDeriva);

                    if ((bool)dr["t305_avisorecursopst"])
                        sNotif = "T";
                    else
                        sNotif = "F";
                    sb.Append("' notif='" + sNotif);

                    sb.Append("' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                    sb.Append("header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[");
                    sb.Append("<label style='width:40px'>" + sNodo + "&nbsp;:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39));
                    sb.Append("<br><label style='width:40px'>P.E.&nbsp;:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                    sb.Append("] hideselects=[off]\">");

                    sb.Append("<td><nobr class='NBR' style='");
                    if (dr["baja"].ToString() == "1") sb.Append("color:red;");

                    sb.Append("width:410px; padding-left:5px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' checked='true'></td>");
                    sb.Append("</tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
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

            SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad,"",true);
            sb.Append("<table id='tblOpciones2' class='texto MAM' style='WIDTH: 440px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional:&nbsp;</label>");
                sb.Append(dr["profesional"].ToString().Replace((char)34, (char)39));

                sb.Append("<br><label style='width:60px'>Usuario:&nbsp;</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append("<br><label style='width:60px'>");

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

    private string Grabar(string sIdRecursoOrigen, string sRecursos, string sItems)
    {
        string sResul = "", sItem;
        int idPSN, idUser;
        bool bDeriva, bNotif;
        decimal costecon = 0, costerep = 0;
        try
        {
            if (sIdRecursoOrigen == "" || sRecursos == "" || sItems=="")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aRecursos = Regex.Split(sRecursos, @"##");
                for (int i = 0; i < aRecursos.Length; i++)
                {
                    idUser = int.Parse(aRecursos[i]);

                    string[] aItems = Regex.Split(sItems, @"##");
                    for (int j = 0; j < aItems.Length; j++)
                    {
                        sItem = aItems[j];
                        string[] aIt = Regex.Split(sItem, @",");
                        idPSN = int.Parse(aIt[3]);
                        if (aIt[4] == "T")
                            bDeriva = true;
                        else
                            bDeriva = false;
                        if (aIt[5] == "T")
                            bNotif = true;
                        else
                            bNotif = false;
                        costecon = 0;
                        costerep = 0;
                        SqlDataReader dr = USUARIOPROYECTOSUBNODO.GetCoste(idPSN, idUser);
                        if (dr.Read())
                        {
                            costecon = decimal.Parse(dr["coste_con"].ToString());
                            costerep = decimal.Parse(dr["coste_rep"].ToString());
                        }
                        dr.Close();
                        dr.Dispose();
                        //ID item, id nodo, anomes del ultimo cierre economico, id proyectosubnodo
                        sResul += PonerRecurso(idUser, int.Parse(aIt[1]), int.Parse(aIt[2]), idPSN, bDeriva, costecon, costerep, bNotif);
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
    private string PonerRecurso(int IdRecurso, int IdNodo, int iUltCierreEco, int IdPsn,
                                bool bDeriva, decimal costecon, decimal costerep, bool bNotif)
    {
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        string sResul = "";
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
            //lA FECHA DE alta en el proyecto será la siguiente al último mes cerrado del nodo
            DateTime dtFechaAlta = Fechas.AnnomesAFecha(Fechas.AddAnnomes(iUltCierreEco, 1));
            if (!TareaRecurso.AsociadoAProyecto(tr, IdPsn, IdRecurso))
            {
                //TareaRecurso.AsociarAProyecto(tr, IdNodo, IdRecurso, IdPsn, null, dtFechaAlta, null);
                //if (costecon == null) costecon = 0;
                USUARIOPROYECTOSUBNODO.Insert(tr, IdPsn, IdRecurso, costecon, costerep, bDeriva, dtFechaAlta, null, null);
            }
            else
            {
                //TareaRecurso.ReAsociarAProyecto(tr, IdRecurso, IdPsn);
                if (!USUARIOPROYECTOSUBNODO.AsociadoDeAltaProyecto(tr, IdPsn, IdRecurso))
                    USUARIOPROYECTOSUBNODO.Update(tr, IdPsn, IdRecurso, costecon, costerep, bDeriva, dtFechaAlta, null, null);
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
}
