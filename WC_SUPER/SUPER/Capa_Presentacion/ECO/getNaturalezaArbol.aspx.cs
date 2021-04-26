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


public partial class getNaturaleza : System.Web.UI.Page
{
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
            string sCoste = "";
            if (Request.QueryString["coste"] != null)
            {
                if (Request.QueryString["coste"].ToString() != "")
                    sCoste = Request.QueryString["coste"].ToString();
            }
            string strTabla = getArbolNaturalezas(Request.QueryString["nTipologia"].ToString(), sCoste);
            string[] aTabla = Regex.Split(strTabla, "@#@");
            if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
            else sErrores = aTabla[1];
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }

    }

    private string getArbolNaturalezas(string sTipología, string sCoste)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool? bCoste = null;

        sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 500px;'>");
        sb.Append("<colgroup><col style='width:495px;' /></colgroup>");
        sb.Append("<tbody>");
        try
        {
            if (sCoste == "1")
                bCoste = true;
            else
            {
                if (sCoste == "0")
                    bCoste = false;
            }

            SqlDataReader dr = GRUPONAT.CatalogoArbolPorTipologia(int.Parse(sTipología), bCoste);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t321_idgruponat"].ToString() + "-" + dr["t322_idsubgruponat"].ToString() + "-" + dr["t323_idnaturaleza"].ToString() + "' idPlant='" + dr["t338_idplantilla"].ToString() + "' desPlant='" + Utilidades.escape(dr["t338_denominacion"].ToString()) + "' ");
                sb.Append(" nivel='" + dr["INDENTACION"].ToString() + "' ");

                sb.Append("");
                switch ((int)dr["INDENTACION"])
                {
                    case 1: 
                        sb.Append(" style='DISPLAY: table-row; HEIGHT: 20px; vertical-align:middle;'>");
                        sb.Append("<td style='padding-left:5px;'><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../images/plus.gif' style='cursor:pointer;'><IMG src='../../images/imgGrupo.gif' style='margin-left:3px;margin-right:3px;'>");
                        sb.Append(dr["denominacion"].ToString() + "</td>");
                        break;
                    case 2:
                        sb.Append(" style='DISPLAY: none; HEIGHT: 20px; vertical-align:middle;'>");
                        sb.Append("<td style='padding-left:5px;'><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../images/plus.gif' style='cursor:pointer;'><IMG src='../../images/imgSubgrupo.gif' style='margin-left:3px;margin-right:3px;'>");
                        sb.Append(dr["denominacion"].ToString() + "</td>");
                        break;
                    case 3:
                        sb.Append(" class='MANO' style='DISPLAY: none; HEIGHT: 20px; vertical-align:middle;' onclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td style='padding-left:5px;'><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../images/imgSeparador.gif'><IMG src='../../images/imgNaturaleza.gif' style='margin-left:3px;margin-right:3px;'>");
                        sb.Append("<label class='texto' onmouseover=\"this.style.textDecoration='underline'\" onmouseout=\"this.style.textDecoration='none'\">" + dr["DENOMINACION"].ToString() + "</label></td>");
                        break;
                }
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
}
