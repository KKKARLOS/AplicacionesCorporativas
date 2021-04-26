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
            sb.Append("<div style='background-image:url(../../../../../Images/imgFT16.gif); width:440px;'>");
            sb.Append("<table id='tblOpciones' class='texto MAM' style='width: 440px;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width:440px' /></colgroup>");
            sb.Append("<tbody id='tBodyTareas'>");
            while (rdr.Read())
            {
                sDes = int.Parse(rdr["t332_idtarea"].ToString()).ToString("#,###") + "-" + rdr["t332_destarea"].ToString();
                sb.Append("<tr id='" + rdr["t332_idtarea"].ToString() + "' est='" + rdr["t332_estado"].ToString());
                sb.Append("' onclick='mm(event)' onmousedown='DD(event);' ");
                sb.Append("ondblclick=\"convocar(this.id,children[0].innerText,this.getAttribute('est'));\" style='height:16px'>");
                sb.Append("<td style='padding-left:5px'><label class=texto id='lbl' style='width:435px;text-overflow:ellipsis;overflow:hidden'");

                if (sDes.Length > 80)
                    sb.Append(" title='" + sDes + "'");

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
    protected string Grabar(string strDatosTarea, string slTareas)
    {
        string sIdTarea, sEstado, sResul = "";
        int nIdTarea, nIdPT, iUsuario;
        bool bFaltanValoresAE = false, bPrimeraTarea=true;
        short nOrden=0, iCodUne;

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
            string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
            ///aDatosTarea[0] = IDPT
            ///aDatosTarea[1] = ID Actividad
            ///aDatosTarea[2] = NodoActivo
            int? nAct = null;
            if ((aDatosTarea[1] != "") && (aDatosTarea[1] != "0")) nAct = int.Parse(aDatosTarea[1]);
            nIdPT = int.Parse(aDatosTarea[0]);
            iUsuario = int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString());
            iCodUne = short.Parse(aDatosTarea[2]);

            string[] aTarea = Regex.Split(slTareas, "##");
            for (int i = 0; i < aTarea.Length; i++)
            {
                if (aTarea[i] != "")
                {
                    string[] aTar = Regex.Split(aTarea[i], ";");
                    sIdTarea = aTar[0];
                    sEstado = aTar[1];
                    if ((sIdTarea != "") && (sIdTarea != "0"))
                    {
                        nIdTarea = int.Parse(sIdTarea);
                        if (bPrimeraTarea)
                        {
                            nOrden = TAREAPSP.flCalcularOrden2(null, nIdPT);
                            bPrimeraTarea = false;
                        }
                        else
                            nOrden++;
                        TAREAPSP.ModificarPadre(tr, nIdTarea, nIdPT, nAct, nOrden, iUsuario);
                        //Como he podido cambiar de PT hay que ver si tiene los AE del nuevo PT para calcular su estado
                        //Siempre que la tarea no esté FINALIZADA, CERRADA o ANULADA (Victor 14/02/2012)
                        if (sEstado != "3" && sEstado != "4" && sEstado!="5")
                        {
                            bFaltanValoresAE = ProyTec.bFaltanValoresAE(tr, iCodUne, nIdPT);
                            if (bFaltanValoresAE)
                            {
                                TAREAPSP.ModificarEstado(tr, nIdTarea, 2, iUsuario);//Paso a estado PENDIENTE
                            }
                        }
                    }
                }
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
                if (sEstProy == "C" || sEstProy == "H")
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
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString());// + "@#@");  //4
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
