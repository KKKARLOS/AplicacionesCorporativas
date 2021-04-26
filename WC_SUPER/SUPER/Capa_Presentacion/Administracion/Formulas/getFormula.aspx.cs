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


public partial class getFormula : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "", sTitulo = "";
    public int nFormula = 0;
    public StringBuilder sb = null;

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
            nFormula = int.Parse(Request.QueryString["id"].ToString());
            sTitulo = Utilidades.decodpar(Request.QueryString["nombre"].ToString());

            ObtenerFormula(nFormula);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerFormula(int t454_idformula)
    {
        sb = new StringBuilder();
        SqlDataReader dr = null;

        try
        {
            sb.Append("<table id='tblDatos' style='width: 700px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:150px;' />");
            sb.Append("    <col style='width:250px;' />");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            byte nGrupo = 0, nSubgrupo = 0, nConcepto = 0;

            dr = FORMULA.ObtenerDetalleFormula(t454_idformula);
            while (dr.Read())
            {
                //sb.Append("<tr style='height:16px;'>");
                //sb.Append("<td>" + dr["t326_denominacion"].ToString() + "</td>");
                //sb.Append("<td>" + dr["t327_denominacion"].ToString() + "</td>");
                //sb.Append("<td>" + dr["t328_denominacion"].ToString() + "</td>");
                //sb.Append("</tr>");

                if (nGrupo != (byte)dr["t326_idgrupoeco"])
                {
                    nGrupo = (byte)dr["t326_idgrupoeco"];
                    CrearGrupo(dr);
                }
                else if (nSubgrupo != (byte)dr["t327_idsubgrupoeco"])
                {
                    nSubgrupo = (byte)dr["t327_idsubgrupoeco"];
                    CrearSubgrupo(dr);
                }
                else
                {
                    nConcepto = (byte)dr["t328_idconceptoeco"];
                    CrearConcepto(dr);
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener los conceptos de la fórmula.", ex);
        }
    }

    private void CrearGrupo(IDataReader dr)
    {
        sb.Append("<tr tipo='G' style='height:16px;'>");
        sb.Append("<td style='padding-left:3px;'>" + dr["t326_denominacion"].ToString() + "</td>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("</tr>");
        if ((byte)dr["t327_idsubgrupoeco"] != 0)
            CrearSubgrupo(dr);
    }
    private void CrearSubgrupo(IDataReader dr)
    {
        sb.Append("<tr tipo='S' style='height:16px;'>");
        sb.Append("<td style='padding-left:3px;'></td>");
        sb.Append("<td>" + dr["t327_denominacion"].ToString() + "</td>");
        sb.Append("<td></td>");
        sb.Append("</tr>");
        if ((byte)dr["t328_idconceptoeco"] != 0)
            CrearConcepto(dr);
    }
    private void CrearConcepto(IDataReader dr)
    {
        sb.Append("<tr tipo='C' coef='" + dr["t455_coeficiente"].ToString() + "' style='height:16px;' >");
        sb.Append("<td style='padding-left:3px;'></td>");
        sb.Append("<td></td>");
        if ((short)dr["t455_coeficiente"] == 1) sb.Append("<td style='color:green; font-weight:bold;'>");
        else if ((short)dr["t455_coeficiente"] == -1) sb.Append("<td style='color:red; font-weight:bold;'>");
        else sb.Append("<td style='color:gray'>");
        
        sb.Append(dr["t328_denominacion"].ToString() + "</td>");
        sb.Append("</tr>");
    }

}
