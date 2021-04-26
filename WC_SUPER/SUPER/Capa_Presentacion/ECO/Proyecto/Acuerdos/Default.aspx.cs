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
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLAcuerdos = "", strAc="";
    public string sErrores="";
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

                    string sPE = Request.QueryString["idPE"];
                    if (sPE != null)
                        getAcuerdos(sPE);
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener el catálogo de espacios de acuerdo", ex);
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
            case ("datosEspacio"):
                //sResultado += getEspacio(int.Parse(aArgs[1]));
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
    private void getAcuerdos(string sPE)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        int nPE = int.Parse(sPE), i = 0;
        SqlDataReader dr = ESPACIOACUERDO.Catalogo2(null, nPE);

        sb2.Append("<table id='tblDatos' class='texto MANO' style='width:900px'>");
        sb2.Append("<colgroup><col style='width:75px;' /><col style='width:335px;' /><col style='width:80px;' /><col style='width:80px;' /><col style='width:330px;' /></colgroup>");
        sb2.Append("</tbody>");
        while (dr.Read())
        {
            sb2.Append("<tr id='" + dr["t638_idAcuerdo"].ToString() + "' style='height:20px;' onclick='ms(this);mostrarAcuerdo(this.id);'>");
            if (dr["t638_ffin"].ToString() == "")
                sb2.Append("<td style='padding-left:3px'></td>");
            else
                sb2.Append("<td style='padding-left:3px'>" + dr["t638_ffin"].ToString().Substring(0, 10) + "</td>");
            sb2.Append("<td title='" + dr["denUsuarioFD"].ToString() + "'><nobr class='NBR W330'>" + dr["denUsuarioFD"].ToString() + "</nobr></td>");
            if (dr["t638_facept"].ToString() == "")
            {
                sb2.Append("<td style='padding-left:3px'></td>");
                if (dr["t638_fdeneg"].ToString() == "")
                    sb2.Append("<td style='padding-left:3px'></td>");
                else
                    sb2.Append("<td style='padding-left:3px' title='" + dr["t638_desdeneg"].ToString() + "'>" + dr["t638_fdeneg"].ToString().Substring(0, 10) + "</td>");
            }
            else
            {
                sb2.Append("<td style='padding-left:3px'>" + dr["t638_facept"].ToString().Substring(0, 10) + "</td>");
                sb2.Append("<td style='padding-left:3px'></td>");
                
            }
            sb2.Append("<td title='" + dr["denUsuarioA"].ToString() + "'><nobr class='NBR W320'>" + dr["denUsuarioA"].ToString() + "</nobr></td></tr>");

            //Cargo el array con todos los espacios de acuerdo para manejarlo desde cliente 
            sb.Append("aAc[" + i.ToString() + "] = new Array(" + dr["t638_idAcuerdo"].ToString() +",");//0
            if ((bool)dr["t638_tipoIAP"]) sb.Append("1,");//1
            else sb.Append("0,");
            if ((bool)dr["t638_tiporesproy"]) sb.Append("1,");//2
            else sb.Append("0,");
            if ((bool)dr["t638_tipocliente"]) sb.Append("1,");//3
            else sb.Append("0,");
            if ((bool)dr["t638_tipoimpfijo"]) sb.Append("1,");//4
            else sb.Append("0,");
            if ((bool)dr["t638_tipootras"]) sb.Append("1,");//5
            else sb.Append("0,");

            if (dr["t638_textootras"].ToString() == "") sb.Append("'',");
            else sb.Append("\"" + Utilidades.escape(dr["t638_textootras"].ToString()) + "\",");//6

            //if (dr["t066_descal"].ToString() == "") sb.Append("'',");
            //else sb.Append("\"" + dr["t066_descal"].ToString() + "\",");//7

            if (dr["t638_periodicidad"].ToString() == "") sb.Append("'',");
            else sb.Append("\"" + Utilidades.escape(dr["t638_periodicidad"].ToString()) + "\",");//8

            if (dr["t638_aconsiderar"].ToString() == "") sb.Append("'',");
            else sb.Append("\"" + Utilidades.escape(dr["t638_aconsiderar"].ToString()) + "\",");//9

            if ((bool)dr["t638_conciliacion"]) sb.Append("1,'");//10
            else sb.Append("0,'");

            sb.Append(dr["t638_tipoconciliacion"].ToString() + "',");//11

            if (dr["t638_contacto"].ToString() == "") sb.Append("'',");
            else sb.Append("\"" + Utilidades.escape(dr["t638_contacto"].ToString()) + "\",");//12

            if (dr["t638_facept"].ToString()=="")//13
                sb.Append("'',");
            else
                sb.Append("'" + dr["t638_facept"].ToString().Substring(0, 10) + "',");

            if ((bool)dr["t638_facturaSA"]) sb.Append("1");//14
            else sb.Append("0");

            //if (dr["denUsuarioA"].ToString() == "") sb.Append("''");
            //else sb.Append("\"" + dr["denUsuarioA"].ToString() + "\"");//14

            sb.Append(");\n");
            i++;
        }
        dr.Close(); 
        dr.Dispose();
        sb2.Append("</tbody>");
        sb2.Append("</table>");

        strAc = sb.ToString();
        this.strTablaHTMLAcuerdos = sb2.ToString();
        if (i == 0)
            sErrores = "No existen espacios de acuerdo para los que se haya pedido aceptación.";
    }
}
