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
    public string  sNodo = "", sErrores, sLectura, strTablaHTMLIntegrantes;//, sCualidad = ""
    //public int nPSN;
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
            sLectura = "false";
            try
            {
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}
                sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                //nPSN = int.Parse(Request.QueryString["idPSN"].ToString());
                this.hdnIdCalIni.Value = Request.QueryString["idCal"].ToString();
                //sCualidad = Request.QueryString["Cuali"].ToString();
                this.hdnPSN.Value = Request.QueryString["idPSN"].ToString();
                this.hdnPSN.Value = Request.QueryString["idPSN"].ToString();
                this.hdnIdNodo.Value = Request.QueryString["nodo"].ToString();
                txtCalDestino.Text = Request.QueryString["denCal"].ToString();

                strTablaHTMLIntegrantes = getUsuarios(int.Parse(Request.QueryString["idPSN"].ToString()), 
                                                      Request.QueryString["Cuali"].ToString(), "0");
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener datos.", ex);
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
            case ("getUsuarios"):
                sResultado += "OK@#@" + getUsuarios(int.Parse(aArgs[1]), aArgs[2], aArgs[3]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string getUsuarios(int idPSN, string sCualidad, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = null;

            if (sCualidad == "J")
                dr = USUARIOPROYECTOSUBNODO.CatalogoUsuariosRepJ(idPSN, (sMostrarBajas == "1") ? true : false);
            else
                dr = USUARIOPROYECTOSUBNODO.CatalogoUsuarios(idPSN, sCualidad, (sMostrarBajas == "1") ? true : false);

            sb.Append("<TABLE id='tblDatos' class='texto MAM' style='width: 430px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width: 20px;' />");
            sb.Append("    <col style='width: 210px;' />");
            sb.Append("    <col style='width: 200px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("alta='" + dr["t314_falta"].ToString().Substring(0, 10) + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("desnodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                //sb.Append("desempresa=\"" + Utilidades.escape(dr["empresa"].ToString()) + "\" ");
                sb.Append("idCal='" + dr["t066_idcal"].ToString() + "' ");
                sb.Append("onclick='mm(event)' ondblclick='insertarRecurso(this)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                //sb.Append("<td>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "</td>");
                //sb.Append("<td></td><td><nobr class='NBR W210' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td align='center'></td><td style='padding-left:5px;'><nobr class='NBR W205' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");

                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W198' style='noWrap:true;' title=\"" + dr["t066_descal"].ToString() + "\" >" + dr["t066_descal"].ToString() + "</nobr></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }
    protected string Grabar(string strDatosBasicos)
    {
        string sResul = "";
        int nUser, nCal;
        #region abrir conexión y transacción
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
            string[] aDatosBasicos = Regex.Split(strDatosBasicos, "///");
            foreach (string oRec in aDatosBasicos)
            {
                if (oRec != "")
                {
                    string[] aDatos = Regex.Split(oRec, "##");
                    nUser = int.Parse(aDatos[0]);
                    nCal = int.Parse(aDatos[1]);
                    Calendario.setCalendarioProfesional(tr, nUser, nCal);
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
