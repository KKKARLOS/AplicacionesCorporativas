﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
    public string strTablaHTMLEnt = "", strTablaHTMLFamEnt = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        sLectura = "false";
        if (!Page.IsCallback)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session["IDRED"] == null)
                    {
                        try { Response.Redirect("~/SesionCaducadaModal.aspx", true); }
                        catch (System.Threading.ThreadAbortException) { return; }
                    }
                    this.hdnIdFamilia.Value = Request.QueryString["idF"].ToString();
                    this.txtDenFamilia.Text = Request.QueryString["denF"].ToString();
                    //if (this.txtDenFamilia.Text != "")
                    //    this.txtDenFamilia.Enabled = false;
                    //strTablaHTMLEnt = obtenerEntornos();
                    strTablaHTMLFamEnt = obtenerEntornosFamilia(int.Parse(this.hdnIdFamilia.Value));
                    //txtApellido1.Focus();
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
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), Utilidades.unescape(aArgs[2]), aArgs[3]);
                break;
            case ("entornos"):
                sResultado += obtenerEntornos(aArgs[1], Utilidades.unescape(aArgs[2]));
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
    /*
    private string obtenerEntornos()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblEntornos' style='width:250px;' mantenimiento='0'>");
        sb.Append("<tbody id='tbodyEnt'>");

        SqlDataReader dr = SUPER.BLL.EntornoTecno.getEntornos(0);//Solo los activos
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t036_idcodentorno"].ToString() + "' style='height:16px;' class='MAM'");
            sb.Append(" onclick='mm(event)' ondblclick='addEntorno(this);' onmousedown='DD(event)'>");
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W240'>" + dr["T036_DESCRIPCION"].ToString() + "</nobr></td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();

        sb.Append("</tbody>");
        sb.Append("</table>");

        return sb.ToString();
    }
     * */
    public static string obtenerEntornos(string sTipoBusqueda, string sCadena)
    {
        string sResul = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        SqlDataReader dr = null;

        dr = SUPER.DAL.Curriculum.ObtenerCriterio(5, sCadena, sTipoBusqueda.ToCharArray()[0]);

        sb.Append("<table id='tblEntornos' class='texto MAM' style='width:250px;'>" + (char)10);
        sb.Append("<colgroup><col style='width:350px;' /></colgroup>" + (char)10);
        sb.Append("<tbodyEnt>");

        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["codigo"].ToString() + "' title='" + dr["denominacion"].ToString() + "' ");
            sb.Append("onclick='mm(event)' ondblclick='addEntorno(this)' onmousedown='DD(event)' style='height:16px;'>");
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W240'>" + dr["denominacion"].ToString() + "</nobr></td>");
            sb.Append("</tr>" + (char)10);
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");

        sResul = "OK@#@" + sb.ToString();

        return sResul;
    }

    private string obtenerEntornosFamilia(int idFamilia)
    {
        StringBuilder sb = new StringBuilder();//Familias de Entornos privadas
        sb.Append("<table id='tblEntFam' style='width:270px;' mantenimiento='1'>");
        sb.Append("<tbody id='tbodydestino'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:250px;' /></colgroup>");
        if (idFamilia != -1)
        {
            List<SUPER.BLL.ElementoLista> Lista = SUPER.BLL.FamiliaEntorno.CatalogoEntorno(idFamilia);
            foreach (SUPER.BLL.ElementoLista oElem in Lista)
            {
                sb.Append("<tr id='" + oElem.sValor + "' style='height:16px;' class='MM' bd=''");
                sb.Append(" onclick='mm(event)' onmousedown='DD(event)'>");
                sb.Append("<td><img src='../../../../../images/imgFN.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;' /></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W240'>" + oElem.sDenominacion + "</nobr></td></tr>");
            }
        }
        sb.Append("</tbody></table>");

        return sb.ToString();
    }
    private string Grabar(int idFamilia, string sDenFamilia, string sEntornosFam)
    {
        string sRes = "";
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);

            if (idFamilia == -1)
                idFamilia = SUPER.BLL.FamiliaEntorno.Insertar(tr, sDenFamilia, int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), false);
            else
                SUPER.BLL.FamiliaEntorno.CambiarDenominacion(tr, idFamilia, sDenFamilia, int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));

            if (sEntornosFam != "")
            {
                string[] aEntornos = Regex.Split(sEntornosFam, @"#");
                for (int i = 0; i < aEntornos.Length - 1; i++)
                {
                    if (aEntornos[i] != "")
                    {
                        string[] aEnt = Regex.Split(aEntornos[i], @",");
                        switch (aEnt[0])
                        {
                            case "I":
                                SUPER.BLL.FamiliaEntorno.InsertarEntorno(tr, idFamilia, int.Parse(aEnt[1]));
                                break;
                            case "D":
                                SUPER.BLL.FamiliaEntorno.BorrarEntorno(tr, idFamilia, int.Parse(aEnt[1]));
                                break;
                        }
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            sRes = "OK@#@" + idFamilia.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sRes = "Error@#@" + Errores.mostrarError("Error al grabar familia", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sRes;
    }
}
