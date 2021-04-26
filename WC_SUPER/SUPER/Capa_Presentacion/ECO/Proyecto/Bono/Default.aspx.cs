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

using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using System.Xml;
using System.IO;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, sErrores, sNodo = "";
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session["IDRED"] == null)
                    {
                        try
                        {
                            Response.Redirect("~/SesionCaducadaModal.aspx", true);
                        }
                        catch (System.Threading.ThreadAbortException) { return; }
                    }

                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    if (Request.QueryString["psn"] != null)
                    {
                        this.hdnPSN.Value = Request.QueryString["psn"].ToString();
                        bool bLectura = false;

                        this.hdnEstadoPSN.Value = PROYECTOSUBNODO.getEstado(null, int.Parse(this.hdnPSN.Value));
                        if (this.hdnEstadoPSN.Value == "C" || this.hdnEstadoPSN.Value == "H")
                        {
                            //ModoLectura.Poner(this.Controls);
                            bLectura = true;
                        }
                        GetProfesionales(int.Parse(this.hdnPSN.Value), bLectura);
                    }
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), aArgs[2]);
                break;
            //case ("profesionales"):
            //    sResultado += GetProfesionales(aArgs[1], int.Parse(aArgs[2]), aArgs[3], aArgs[4]);
            //    break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }
    private string GetProfesionales(int idPSN, bool bModoLectura)
    {
        StringBuilder sb = new StringBuilder();
        //string sAux = "", sTexto = "";

        sb.Append("<table id='tblDatos'");
        if (bModoLectura)
            sb.Append(" class='texto' style='width:900px;'>");
        else
            sb.Append(" class='texto MANO' style='width:900px;'>");
        sb.Append("<colgroup>");
        sb.Append(" <col style='width:15px;' />");
        sb.Append(" <col style='width:20px;' />");
        sb.Append(" <col style='width:300px;' />");
        sb.Append(" <col style='width:65px;' />");
        sb.Append(" <col style='width:65px;' />");
        sb.Append(" <col style='width:255px;' />");
        sb.Append(" <col style='width:90px;' />");
        sb.Append(" <col style='width:90px;' />");
        sb.Append("</colgroup>");

        SqlDataReader dr = USUARIOPSN_BONOTRANS.ObtenerProfesionales(null, idPSN);
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' ");
            sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
            sb.Append("sexo='" + dr["t001_sexo"].ToString() + "'");
            sb.Append("bono='" + dr["t655_idBono"].ToString() + "' ");
            sb.Append("bonoNew='" + dr["t655_idBono"].ToString() + "' ");
            sb.Append("fiab='" + dr["t665_anomesdesde"].ToString() + "' ");
            sb.Append("ffab='" + dr["t665_anomeshasta"].ToString() + "' ");
            sb.Append("desnodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
            sb.Append("desempresa=\"" + Utilidades.escape(dr["empresa"].ToString()) + "\" ");
            sb.Append("desofi=\"" + Utilidades.escape(dr["T010_DESOFICINA"].ToString()) + "\" ");
            if (bModoLectura)
                sb.Append("style='height:20px' >");
            else
                sb.Append("style='height:20px' >");
                //sb.Append("style='height:20px' onclick='ms(this);'>");

            sb.Append("<td></td><td></td>");

            sb.Append("<td><nobr class='NBR W290'>" + dr["profesional"].ToString() + "</nobr></td>");
            sb.Append("<td>" + ((DateTime)dr["t330_falta"]).ToShortDateString() + "</td>");
            sb.Append("<td>" + ((dr["t330_fbaja"] == DBNull.Value) ? "" : ((DateTime)dr["t330_fbaja"]).ToShortDateString()) + "</td>");
            if (bModoLectura)
                sb.Append("<td><nobr class='NBR W200' >" + dr["t655_denominacion"].ToString() + "</nobr></td>");
            else
                sb.Append("<td style='vertical-align:super'><nobr class='NBR W200 MA' ondblclick='getBono(this)'>" + dr["t655_denominacion"].ToString() + "</nobr></td>");
            
            if (dr["t665_anomesdesde"].ToString() != "")
                sb.Append("<td>" + Fechas.AnnomesAFechaDescLarga(int.Parse(dr["t665_anomesdesde"].ToString())) + "</td>");
            else
                sb.Append("<td></td>");

            if (dr["t665_anomeshasta"].ToString() != "")
                sb.Append("<td>" + Fechas.AnnomesAFechaDescLarga(int.Parse(dr["t665_anomeshasta"].ToString())) + "</td>");
            else
                sb.Append("<td></td>");
            //if (dr["t655_idBono"].ToString() != "")
            //{
            //    sTexto = dr["t665_comentario"].ToString();
            //    sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "</br>").Replace((char)34, (char)39);
            //    sTexto = Utilidades.unescape(sTexto);
            //    //sb.Append("<td onmouseover='TTip()'><nobr class='NBR' style='width:103px'>" + dr["t665_comentario"].ToString() + "</nobr></td>");
            //    sb.Append("<td><image style='width:16px;' class='MA' ondblclick='cargarObserva(this.parentNode.parentNode);' src='../../../../images/imgComentario.gif'></image></td>");
            //    sb.Append("<td><nobr class='NBR' style='noWrap:true; width:83px; height:16px' margin-left:5px; ");
            //    if (dr["t665_comentario"].ToString() != "")
            //        sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[</br>" + sTexto + "] hideselects=[off]\" ");
            //    sb.Append(">" + Utilidades.escape(dr["t665_comentario"].ToString()) + "</nobr></td>");
            //}
            //else
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</table>");
        strTablaHtml = sb.ToString();

        return "OK@#@" + sb.ToString();
    }
    protected string Grabar(int idPSN, string strDatos)
    {
        string sResul = "";

        #region apertura de conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            string[] aClase = Regex.Split(strDatos, "///");
            foreach (string oClase in aClase)
            {
                if (oClase == "") continue;
                string[] aValores = Regex.Split(oClase, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. id Usuario
                //2. id Bono
                //3. id Bono nuevo
                //4. anomes desde
                //5. anomes hasta
                switch (aValores[0])
                {
                    case "U":
                        USUARIOPSN_BONOTRANS.Update(tr, idPSN, int.Parse(aValores[1]), int.Parse(aValores[2]),
                                                    int.Parse(aValores[3]), int.Parse(aValores[4]), int.Parse(aValores[5]));
                        break;
                    case "I":
                        USUARIOPSN_BONOTRANS.Insert(tr, idPSN, int.Parse(aValores[1]), int.Parse(aValores[3]),
                                                    int.Parse(aValores[4]), int.Parse(aValores[5]));
                        break;
                    case "D":
                        if (aValores[2] != "")
                            USUARIOPSN_BONOTRANS.Delete(tr, idPSN, int.Parse(aValores[1]), int.Parse(aValores[2]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar.", ex, false);// +"@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}