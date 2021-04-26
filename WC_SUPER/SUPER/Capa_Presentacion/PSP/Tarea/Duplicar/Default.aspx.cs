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

public partial class Capa_Presentacion_Tarea_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, gsAcceso;
    public int nPE, nCR;
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
            sErrores = "";
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
            case ("buscar"):
                sResultado += ObtenerTareas(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
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
    private string ObtenerTareas(string sPE, string sPT, string sF, string sA)
    {// Devuelve el código HTML del catalogo de tareas 
        StringBuilder sb = new StringBuilder();
        string sDes;
        SqlDataReader rdr;
        try
        {
            if (sPT =="")
                rdr = TAREAPSP.CatalogoPE(int.Parse(sPE), (int)Session["UsuarioActual"]);
            else
            {
                if (sF == "")
                {
                    if (sA == "")
                        rdr = TAREAPSP.CatalogoPT(int.Parse(sPT));
                    else
                        rdr = TAREAPSP.CatalogoA(int.Parse(sA));
                }
                else
                {
                    if (sA == "")
                        rdr = TAREAPSP.CatalogoF(int.Parse(sF));
                    else
                        rdr = TAREAPSP.CatalogoA(int.Parse(sA));
                }
            }
            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width:420px;'>");
            sb.Append("<table id='tblOpciones' class='texto MAM' style='width: 420px;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width:420px'  /></colgroup>");
            sb.Append("<tbody id='tBodyTareas'>");
            while (rdr.Read())
            {
                sDes = int.Parse(rdr["t332_idtarea"].ToString()).ToString("#,###") + "-" + rdr["t332_destarea"].ToString();
                sb.Append("<tr id='" + rdr["t332_idtarea"].ToString() + "' onclick='mm(event)' onmousedown='DD(event);' ");
                sb.Append("ondblclick='convocar(this.id,children[0].innerText,true);' style='height:16px'><td style='padding-left:5px'><label id='lbl' style='width:415px;text-overflow:ellipsis;overflow:hidden'");

                if (sDes.Length > 80)
                {
                    sb.Append(" title='" + sDes + "'");
                }

                sb.Append("><NOBR>" + sDes + "</NOBR></label></td></tr>");
            }
            sb.Append("</tbody></table></div>");
            rdr.Close(); rdr.Dispose();
            //this.strTablaHTMLPersonas = sb.ToString();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las personas ", ex);
        }
    }
    protected string Grabar(string sCopias, string slTareas)
    {
        string sIdTarea, sResul = "", sDesTarea="";
        int nIdTarea, iUsuario, nIdTareaNew;
        short nOrden=0, nCopias=0;
        byte nEstado=1;
        DateTime? dValIni = null;
        DateTime? dValFin = null;
        DateTime? dPLIni = null;
        DateTime? dPLFin = null;
        DateTime? dPRFin = null;

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
            iUsuario = int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString());
            nCopias = short.Parse(sCopias);
            string[] aTarea = Regex.Split(slTareas, "##");
            for (int i = 0; i < aTarea.Length; i++)
            {
                sIdTarea = aTarea[i];
                if ((sIdTarea != "") && (sIdTarea != "0"))
                {
                    nIdTarea = int.Parse(sIdTarea);
                    TAREAPSP oTarea = TAREAPSP.Obtener(tr, nIdTarea);
                    nOrden = TAREAPSP.flCalcularOrden2(null, oTarea.t331_idpt);
                    //Por defecto la nueva tarea estará activa salvo que la original esté pendiente
                    if (oTarea.t332_estado == 2) 
                        nEstado = 2;
                    else
                        nEstado = 1;
                    if (oTarea.t332_fiv.ToString() != "") dValIni = oTarea.t332_fiv;
                    if (oTarea.t332_ffv.ToString() != "") dValFin = oTarea.t332_ffv;
                    if (oTarea.t332_fipl.ToString() != "") dPLIni = oTarea.t332_fipl;
                    if (oTarea.t332_ffpl.ToString() != "") dPLFin = oTarea.t332_ffpl;
                    if (oTarea.t332_ffpr.ToString() != "") dPRFin = oTarea.t332_ffpr;
                    for (short j = 1; j <= nCopias; j++)
                    {
                        sDesTarea = "Copia " + j.ToString() + " de " + oTarea.t332_destarea;
                        if (sDesTarea.Length>100) sDesTarea = sDesTarea.Substring(1, 100);
                        nIdTareaNew = TAREAPSP.Insert(tr
                                                , sDesTarea //Des. tarea;
                                                , oTarea.t332_destarealong //Des. larga de tarea;
                                                , oTarea.t331_idpt//Id PT
                                                , oTarea.t335_idactividad//IdActividad
                                                , iUsuario//promotor
                                                , iUsuario//último modificador
                                                , DateTime.Now//F.Alta
                                                , DateTime.Now//F.Ultima modificacion
                                                , dValIni//F.Inicio Vigencia
                                                , dValFin//F.Fin vigencia
                                                , nEstado//estado 
                                                , dPLIni//F.Inicio Planificada
                                                , dPLFin//F.Fin Planificada
                                                , oTarea.t332_etpl//Esfuerzo total estimado
                                                , dPRFin//F.Fin Prevista
                                                , oTarea.t332_etpr//Esfuerzo total previsto
                                                , null//PST
                                                , oTarea.t332_cle//Control límite de esfuerzos
                                                , oTarea.t332_tipocle//tipo de control de límite de esfuerzo
                                                , nOrden//orden
                                                , oTarea.t332_facturable//facturable
                                                , decimal.Parse(oTarea.t332_presupuesto.ToString())//presupuesto
                                                , null, //24
                                                "",//OTL
                                                "",//INCIDENCIA
                                                "",//OBSERVACIONES
                                                oTarea.t332_notificable,//notificabled
                                                "",//NOTAS1
                                                "",//NOTAS2
                                                "",//NOTAS3
                                                "",//NOTAS4
                                                oTarea.t332_avance,//avance real
                                                oTarea.t332_avanceauto,//avance automatico (true/false)
                                                oTarea.t332_impiap, //imputable en IAP (true/false)
                                                false, //notas IAP
                                                oTarea.t332_heredanodo, //recursos heredados del nodo
                                                oTarea.t332_heredaproyeco,//recursos heredados del PE
                                                "", //mesaje generico para todos los profesionales
                                                oTarea.t332_notif_prof//Notificar a profesionales asociados
                                                ,oTarea.t332_acceso_bitacora_iap
                                                , null//pendiente ultimo null modo facturación
                                                , oTarea.t332_horascomplementarias);
                        POOL_GF_TAREA.DuplicarPoolGF(tr, nIdTarea, nIdTareaNew);
                        //Asigno lo atributos estadístico que tuviera la tarea original
                        AETAREAPSP.DuplicarAEs(tr, nIdTarea, nIdTareaNew);
                    }//For copias
                }//IF
            }//FOR
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
        return sResul;
    }
    private string Permiso(string sT305IdProy, string sCodUne, string sNumProyEco, string sIdTarea)
    {
        string sResul = "N", sEstProy;
        try
        {
            //1º miramos si hay acceso sobre la tarea
            string sUserAct = Session["UsuarioActual"].ToString();
            int iUserAct = int.Parse(sUserAct);
            sIdTarea = sIdTarea.Replace(".", "");
            sResul = TAREAPSP.getAcceso(null, int.Parse(sIdTarea), iUserAct);
            //N-> no hay acceso R-> acceso en lectura W-> acceso en escritura
            if (sResul != "N")
            {
                //Miramos el estado del proyecto economico. Por que si está cerrado aunque tenga permiso solo se podrá leer
                sEstProy = EstrProy.estadoProyecto(sT305IdProy);
                if (sEstProy == "C" || sEstProy=="H")
                {
                    if (sResul == "W") sResul = "R";
                }
                if (sResul == "R")
                {
                    ModoLectura.Poner(this.Controls);
                }
            }
            gsAcceso = sResul;
            this.hdnAcceso.Text = sResul;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener permisos sobre la tarea " + sIdTarea, ex);
        }
        //return "OK@#@" + sResul;
        return sResul;
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
                sb.Append(dr["t301_denominacion"].ToString());// + "@#@");  //2
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }
}
