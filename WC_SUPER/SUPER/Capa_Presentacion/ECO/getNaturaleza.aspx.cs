using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class getNaturaleza : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sErrores = "";

    protected void Page_Load(object sender, EventArgs e)
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

            string strTabla = getGrupoNaturalezas(Request.QueryString["nTipologia"].ToString());
            string[] aTabla = Regex.Split(strTabla, "@#@");
            if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
            else sErrores = aTabla[1];
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }

        //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
        //   y la funci�n que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
        //2� Se "registra" la funci�n que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getSubgrupoNat"):
                sResultado += getSubGrupoNaturalezas(aArgs[1]);
                break;
            case ("getNaturaleza"):
                sResultado += getNaturalezasProd(aArgs[1]);
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

    private string getGrupoNaturalezas(string sTipolog�a)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos1' class='texto MANO' style='WIDTH: 500px;'>");
        sb.Append("<colgroup><col style='width:495px;' /></colgroup>");
        sb.Append("<tbody>");
        try
        {
            SqlDataReader dr = GRUPONAT.CatalogoPorTipologia(int.Parse(sTipolog�a));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t321_idgruponat"].ToString() + "' style='height:16px;' onclick='ms(this);getSubgrupoNat(this.id)' >");
                sb.Append("<td style='padding-left:5px;'>" + dr["t321_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los grupos de naturaleza", ex);
        }
        return sResul;
    }
    private string getSubGrupoNaturalezas(string sIDGrupo)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos2' class='texto MANO' style='WIDTH: 500px;'>");
        sb.Append("<colgroup><col style='width:495px;' /></colgroup>");
        sb.Append("<tbody>");
        try
        {
            SqlDataReader dr = SUBGRUPONAT.Catalogo(null, "", int.Parse(sIDGrupo), null, null, 2, 0);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t322_idsubgruponat"].ToString() + "' style='height:16px;' onclick='ms(this);getNaturalezaProd(this.id)' >");
                sb.Append("<td style='padding-left:5px;'>" + dr["t322_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los subgrupos de naturaleza.", ex);
        }
        return sResul;
    }
    private string getNaturalezasProd(string sIDSubGrupo)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos3' class='texto MA' style='WIDTH: 500px;'>");
        sb.Append("<colgroup><col style='width:495px;' /></colgroup>");
        sb.Append("<tbody>");
        try
        {
            SqlDataReader dr = NATURALEZA.CatalogoConPlantilla(int.Parse(sIDSubGrupo));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t323_idnaturaleza"].ToString() + "' idPlant='" + dr["t338_idplantilla"].ToString() + "' desPlant='" + Utilidades.escape(dr["t338_denominacion"].ToString()) + "' style='height:16px;' ondblclick='aceptarClick(this.rowIndex)' >");
                sb.Append("<td style='padding-left:5px;'>" + dr["t323_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las naturalezas de producci�n.", ex);
        }
        return sResul;
    }

}
