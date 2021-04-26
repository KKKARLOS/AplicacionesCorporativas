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
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string sErrores, sLectura = "false";
    public string strTablaHTMLIntegrantes, sNodo = "";
    protected string strInicial;

    protected void Page_Load(object sender, EventArgs e)
    {
        strInicial = "";
        sLectura = "false";
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
                    this.hdnDenNodo.Value = sNodo;
                    //Obtener los datos necesarios
                    int nIdPE = int.Parse(Request.QueryString["sCodPE"].ToString());
                    this.hdnPSN.Value = nIdPE.ToString();
                    GetProyecto(nIdPE);
                    this.hdnNodo.Value=Request.QueryString["nCR"].ToString();
                    //Cargo las vacaciones del año actual de los profesionales internos del proyecto
                    ObtenerIntegrantes(nIdPE, this.hdnNodo.Value, "P");
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener los datos", ex);
                }
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
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerIntegrantes(int.Parse(aArgs[1]), aArgs[2], aArgs[3]);
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
    private string ObtenerIntegrantes(int nIdPE, string sCodCR, string sVista)
    {
        StringBuilder sb = new StringBuilder();
        DateTime dt;
        string sDias = "";//, sFecha="";
        int idProfAnt = -1, idProfAct = -1;
        int iMesAnt = -1, iMesAct = -1;
        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.Vacaciones(nIdPE, sVista);
            if (sVista == "P")
            {
                #region ordenación por profesional
                sb.Append("<table id='tblOpciones2' class='texto MANO' style='width: 855px;'>");
                sb.Append("<colgroup><col style='width:20px;' /><col style='width:60px;' /><col style='width:100px;' /><col style='width:675px;' /></colgroup>");
                while (dr.Read())
                {
                    idProfAct = int.Parse(dr["t314_idusuario"].ToString());
                    //dt = getFecha(dr["t187_fechavacacion"].ToString());
                    dt = DateTime.Parse(dr["t187_fechavacacion"].ToString());
                    iMesAct = (dt.Year * 100) + dt.Month;

                    if (idProfAnt != idProfAct)
                    {
                        if (sDias != "")//Pongo el último mes del profesional anterior
                            sb.Append(PonerMes(iMesAnt, sDias));
                        sDias = dt.Day.ToString() + ", ";
                        sb.Append(PonerProfesional(dr, sCodCR));
                        iMesAnt = iMesAct;
                    }
                    else
                    {
                        if (iMesAnt != iMesAct)
                        {
                            sb.Append(PonerMes(iMesAnt, sDias));
                            sDias = dt.Day.ToString() + ", ";
                        }
                        else
                        {
                            sDias += dt.Day.ToString() + ", ";
                        }
                    }
                    iMesAnt = iMesAct;
                    idProfAnt = idProfAct;
                }
                if (sDias != "")
                    sb.Append(PonerMes(iMesAct, sDias));
                #endregion
            }
            else
            {
                #region ordenación por mes
                string sUserAnt = "", sSexoAnt = "", sNodoAnt = "", sProfAnt = "";
                string sUserAct = "", sSexoAct = "", sNodoAct = "", sProfAct = "";
                sb.Append("<table id='tblOpciones2' class='texto MANO' style='width: 855px;'>");
                sb.Append("<colgroup><col style='width:60px' /><col style='width:20px;' /><col style='width:300px;' /><col style='width:475px;' /></colgroup>");
                while (dr.Read())
                {
                    idProfAct = int.Parse(dr["t314_idusuario"].ToString());
                    //dt = getFecha(dr["t187_fechavacacion"].ToString());
                    dt = DateTime.Parse(dr["t187_fechavacacion"].ToString());
                    iMesAct = (dt.Year * 100) + dt.Month;
                    sUserAct = dr["t314_idusuario"].ToString();
                    sSexoAct = dr["t001_sexo"].ToString();
                    sNodoAct = dr["t303_idnodo"].ToString();
                    sProfAct = dr["Profesional"].ToString();

                    if (iMesAnt != iMesAct)
                    {
                        if (sDias != "")//Pongo el último mes del profesional anterior
                            sb.Append(PonerProfesional2(sUserAnt, sSexoAnt, sNodoAnt, sProfAnt, sCodCR, sDias));
                        sDias = "";
                        sb.Append(PonerMes2(iMesAct));
                    }
                    else
                    {
                        if (idProfAnt != idProfAct)
                        {
                            sb.Append(PonerProfesional2(sUserAnt, sSexoAnt, sNodoAnt, sProfAnt, sCodCR, sDias));
                            sDias = "";
                        }
                    }
                    sDias += dt.Day.ToString() + ", ";

                    sUserAnt = sUserAct;
                    sSexoAnt = sSexoAct;
                    sNodoAnt = sNodoAct;
                    sProfAnt = sProfAct;
                    iMesAnt = iMesAct;
                    idProfAnt = idProfAct;
                }
                sb.Append(PonerProfesional2(sUserAct, sSexoAct, sNodoAct, sProfAct, sCodCR, sDias));
                #endregion
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            strTablaHTMLIntegrantes = sb.ToString();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception e)
        {
            //Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@" + e.Message;
        }
    }

    private string PonerProfesional(SqlDataReader dr, string sCodCR)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<tr t='PP' onclick='mm(event)' style='height:20px'");
        sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");

        if (dr["t303_idnodo"].ToString() == sCodCR) sb.Append("tipo='P' ");
        else sb.Append("tipo='N' ");
        sb.Append("><td style='padding-left:3px;'></td><td colspan=3>");
        //sb.Append("<NOBR id='" + dr["t314_idusuario"].ToString() + "' class='NBR W410' title='" + dr["Profesional"].ToString() + "'>" + dr["Profesional"].ToString() + "</NOBR></td>");
        sb.Append(dr["Profesional"].ToString() + "</td></tr>");

        return sb.ToString();
    }
    private string PonerMes(int iMes, string sDias)
    {
        StringBuilder sb = new StringBuilder();
        if (sDias != "")
        {
            sDias = sDias.Substring(0, sDias.Length - 2);
            string sMes = Fechas.AnnomesAFechaDescLarga(iMes);
            sb.Append("<tr t='PM' onclick='mm(event)' style='height:20px'><td colspan=2></td>");
            sb.Append("<td>" + Fechas.AnnomesAFechaDescLarga(iMes) + "</td>");
            //sb.Append("<td><NOBR class='NBR W410'>" + sDias + "</NOBR></td></tr>");
            sb.Append("<td>" + sDias + "</td></tr>");
        }
        return sb.ToString();
    }
    private DateTime getFecha(string sFecha)
    {
        string[] aF = Regex.Split(sFecha, "/");
        DateTime dt = new DateTime(int.Parse(aF[0]), int.Parse(aF[1]), int.Parse(aF[2]));
        return dt;
    }
    private string PonerProfesional2(string sUser, string sSexo, string sNodo, string sProf, string sCodCR, string sDias)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<tr t='MP' onclick='mm(event)' style='height:20px' sexo='" + sSexo + "' ");

        if (sNodo == sCodCR) sb.Append("tipo='P' ");
        else sb.Append("tipo='N' ");

        sb.Append("><td></td><td></td><td>");
        sb.Append("<nobr class='NBR W290' title='" + sProf + "'>" + sProf + "</nobr></td>");
        if (sDias != "")
        {
            sDias = sDias.Substring(0, sDias.Length - 2);
            sb.Append("<td>" + sDias + "</td></tr>");
        }

        return sb.ToString();
    }
    private string PonerMes2(int iMes)
    {
        StringBuilder sb = new StringBuilder();
        string sMes = Fechas.AnnomesAFechaDescLarga(iMes);
        sb.Append("<tr t='MM' onclick='mm(event)' style='height:20px'>");
        sb.Append("<td colspan=4>" + Fechas.AnnomesAFechaDescLarga(iMes) + "</td></tr>");
        return sb.ToString();
    }
    private void GetProyecto(int idPSN)
    {
        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy(idPSN);
            if (dr.Read())
            {
                this.hdnPE.Value = int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###");
                this.hdnDenPE.Value = dr["t301_denominacion"].ToString();
            }
            dr.Close();
            dr.Dispose();

        }
        catch(Exception)
        {
        }
    }
}
