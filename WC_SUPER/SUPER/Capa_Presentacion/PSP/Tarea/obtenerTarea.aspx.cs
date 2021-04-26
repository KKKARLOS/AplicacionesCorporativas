using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
/// <summary>
/// Pantalla de selección de proyectos técnicos
/// </summary>
public partial class obtenerTarea : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strErrores, sDesPE,sDesPT,sDesFase, sTarea, sDesActividad,sAux;
    public int nPE, nPT, nFase, nActividad;

	private void Page_Load(object sender, System.EventArgs e)
	{
        try
        {
            if (!Page.IsCallback){
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                //Recojo el codigo y descripción de proyecto económico
                sAux = Request.QueryString["nIdPE"].ToString();
                if (sAux != "")
                {
                    this.hdnT305IdProy.Value = sAux;
                }
                sAux = Request.QueryString["nPE"].ToString();
                if (sAux != "")
                {
                    this.txtNumPE.Text = sAux;
                    sAux = sAux.Replace(".", "");
                    nPE = int.Parse(sAux);
                }
                else
                {
                    nPE = -1;
                }
                sDesPE = Request.QueryString["sPE"].ToString();
                this.txtPE.Text = sDesPE;
                //Recojo el codigo y descripción de proyecto técnico
                sAux = Request.QueryString["nPT"].ToString();
                this.txtNumPT.Text = sAux;
                if (sAux != "")
                {
                    sAux = sAux.Replace(".", "");
                    nPT = int.Parse(sAux);
                }
                else
                {
                    nPT = -1;
                }
                sDesPT = Request.QueryString["sPT"].ToString();
                this.txtPT.Text = sDesPT;
                //Recojo el codigo y descripción de fase
                sAux = Request.QueryString["nFase"].ToString();
                if (sAux != "")
                {
                    sAux = sAux.Replace(".", "");
                    nFase = int.Parse(sAux);
                }
                else
                {
                    nFase = -1;
                }
                this.txtNumFase.Text = nFase.ToString();
                sDesFase = Request.QueryString["sFase"].ToString();
                this.txtFase.Text = sDesFase;
                //Recojo el codigo y descripción de actividad
                sAux = Request.QueryString["nAct"].ToString();
                if (sAux != "")
                {
                    sAux = sAux.Replace(".", "");
                    nActividad = int.Parse(sAux);
                }
                else
                {
                    nActividad = -1;
                }
                this.txtNumActividad.Text = nActividad.ToString();
                sDesActividad = Request.QueryString["sAct"].ToString();
                this.txtActividad.Text = sDesActividad;

                //Recojo la descripción de la tarea
                sTarea = Request.QueryString["sTarea"].ToString();
                this.hdnDesTarea.Text = sTarea;

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            strErrores = Errores.mostrarError("Error al obtener las tareas", ex);
        }

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("T"):
                sResultado += ObtenerTareas(aArgs[4], aArgs[1], aArgs[2], aArgs[3], Utilidades.unescape(aArgs[5]), aArgs[6]);
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

    private string ObtenerTareas(string sTipoBusqueda, string num_proy_tec, string num_fase,string num_act,
                                 string strNomTarea, string sT305IdProy)
    {
        string sResul = "",sPT;
        int nPT, nPE;
        SqlDataReader dr;
        try
        {

            StringBuilder strBuilder = new StringBuilder();
            // int i = 0;
            strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 440px;'>");
            strBuilder.Append("<colgroup><col style='width:440px'></colgroup>");
            strBuilder.Append("<tbody>");
            if ((sT305IdProy == "") || (sT305IdProy == "-1"))
            {
                nPE = -1;
                int iUser = (int)Session["UsuarioActual"];
                dr = TAREAPSP.Catalogo4(strNomTarea, 2, 0, sTipoBusqueda, iUser);
            }
            else
            {
                if (num_proy_tec == "")
                {
                    nPT = -1;
                }
                else
                {
                    sPT = num_proy_tec.Replace(".", "");
                    nPT = int.Parse(sPT);
                }
                if (nPT == -1)
                {
                    //sPE = sPE.Replace(".", "");
                    nPE = int.Parse(sT305IdProy);
                    int iUser = (int)Session["UsuarioActual"];
                    dr = TAREAPSP.Catalogo3(strNomTarea, nPE, 2, 0, sTipoBusqueda, iUser);
                }
                else
                {
                    if (num_fase == "" || num_fase == "-1" || num_fase == "0")
                    {
                        if (num_act == "" || num_act == "-1" || num_act == "0")
                        {
                            dr = TAREAPSP.Catalogo2(null, strNomTarea, nPT, null, null, 2, 0, sTipoBusqueda);
                        }
                        else
                        {
                            dr = TAREAPSP.Catalogo2(null, strNomTarea, nPT, null, int.Parse(num_act), 2, 0, sTipoBusqueda);
                        }
                    }
                    else
                    {
                        if (num_act == "" || num_act == "-1" || num_act == "0")
                        {
                            dr = TAREAPSP.Catalogo2(null, strNomTarea, nPT, int.Parse(num_fase), null, 2, 0, sTipoBusqueda);
                        }
                        else
                        {
                            dr = TAREAPSP.Catalogo2(null, strNomTarea, nPT, int.Parse(num_fase), int.Parse(num_act), 2, 0, sTipoBusqueda);
                        }
                    }
                }
            }
            while (dr.Read())
            {
                StringBuilder sbTitle = new StringBuilder();
                sbTitle.Append("<b>Proy. Eco.</b>: ");
                sbTitle.Append(dr["nom_proyecto"].ToString().Replace((char)34, (char)39));
                sbTitle.Append("<br><b>Proy. Téc.</b>: ");
                sbTitle.Append(dr["t331_despt"].ToString().Replace((char)34, (char)39));
                if (dr["t334_desfase"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Fase</b>:          ");
                    sbTitle.Append(dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                }
                if (dr["t335_desactividad"].ToString() != "")
                {
                    sbTitle.Append("<br><b>Actividad</b>:  ");
                    sbTitle.Append(dr["t335_desactividad"].ToString().Replace((char)34, (char)39));
                }
                sbTitle.Append("<br><b>Tarea</b>:  ");
                sbTitle.Append(dr["t332_destarea"].ToString().Replace((char)34, (char)39));

                strBuilder.Append("<tr id='" + dr["t332_idtarea"].ToString());
                strBuilder.Append("' nPE=\"");
                strBuilder.Append(dr["num_proyecto"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" sPE=\"");
                strBuilder.Append(dr["nom_proyecto"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" nPT=\"");
                strBuilder.Append(dr["t331_idpt"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" sPT=\"");
                strBuilder.Append(dr["t331_despt"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" nF=\"");
                strBuilder.Append(dr["t334_idfase"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" sF=\"");
                strBuilder.Append(dr["t334_desfase"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" nA=\"");
                strBuilder.Append(dr["t335_idactividad"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" sA=\"");
                strBuilder.Append(dr["t335_desactividad"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" sT305IdPr=\"");
                strBuilder.Append(dr["t305_idproyectosubnodo"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" sEst=\"");
                strBuilder.Append(dr["t301_estado"].ToString().Replace((char)34, (char)39));
                strBuilder.Append("\" onclick='ms(this);estructura(this);' onmouseover='TTip(event)' ondblclick='aceptarClick(this.rowIndex)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[");
                strBuilder.Append(sbTitle);
                //strBuilder.Append("]\"><td><span class='NBR' style='width:400px;'>" + dr["t332_destarea"].ToString() + "</span></td></tr>");
                strBuilder.Append("]\"><td><nobr style='width:400px;'>" + dr["t332_destarea"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            strBuilder.Append("</tbody>");
            strBuilder.Append("</table>");

            sResul = "OK@#@" + strBuilder.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al Obtener las tareas", ex);
        }
        return sResul;
    }
}

