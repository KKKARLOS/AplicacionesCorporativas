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
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page
{
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHtml="";
 	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Catálogo de fórmulas de datos económicos";
        Master.bFuncionesLocales = true;

        try
        {
            string strTabla0 = ObtenerFormulas();
            string[] aTabla0 = Regex.Split(strTabla0, "@#@");
            if (aTabla0[0] == "OK") strTablaHtml = aTabla0[1];
            else Master.sErrores =  aTabla0[1];
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
    private string ObtenerFormulas()
    {
        string sClasesExcluidas = "";
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto' style='width: 950px;cursor:url(../../../images/imgManoAzul2.cur),pointer;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:40px;'>");
            sb.Append("<col style='width:350px;'>");
            sb.Append("<col style='width:350px;'>");
            sb.Append("<col style='width:210px;'>");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = FORMULA.CatalogoGeneral();

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t454_idformula"].ToString() + "' onclick=\"ms(this)\" ondblclick=\"Detalle(this)\" style='height:16px;' >");
                sb.Append("<td style='padding-right:5px;text-align:right;'>" + ((int)dr["t454_idformula"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["t454_nombre"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["t454_literal"].ToString() + "</nobr></td>");
                sClasesExcluidas = dr["t454_clasesexcl"].ToString();
                if (sClasesExcluidas != "")
                    sClasesExcluidas = sClasesExcluidas.Substring(0, sClasesExcluidas.Length - 2);
                sb.Append("<td title='"+sClasesExcluidas+"'><nobr class='NBR W200'>" + sClasesExcluidas + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las fórmulas", ex);
        }
    }
}