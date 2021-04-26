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


public partial class getClasesClonar : System.Web.UI.Page
{
    public string strTablaHTML = "";
    public string sErrores = "";
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

            getArbolClases();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }

    }

    protected void getArbolClases()
    {
        sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblDatos' style='width: 500px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width: 500px;' />");
            //sb.Append("    <col style='width:250px;' />");
            //sb.Append("    <col style='width:300px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            byte nGrupo = 0, nSubgrupo = 0, nConcepto = 0;
            int nClase = 0;
            
            SqlDataReader dr = CLASEECO.ObtenerClasesClonables(null, SUPER.Capa_Negocio.Utilidades.EsAdminProduccion());
            while (dr.Read())
            {
                if (nGrupo != (byte)dr["t326_idgrupoeco"])
                {
                    nGrupo = (byte)dr["t326_idgrupoeco"];
                    nSubgrupo = (byte)dr["t327_idsubgrupoeco"];
                    nConcepto = (byte)dr["t328_idconceptoeco"];
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearGrupo(dr);
                }
                else if (nSubgrupo != (byte)dr["t327_idsubgrupoeco"])
                {
                    nSubgrupo = (byte)dr["t327_idsubgrupoeco"];
                    nConcepto = (byte)dr["t328_idconceptoeco"];
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearSubgrupo(dr);
                }
                else if (nConcepto != (byte)dr["t328_idconceptoeco"])
                {
                    nConcepto = (byte)dr["t328_idconceptoeco"];
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearConcepto(dr);
                }
                else
                {
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearClase(dr);
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
            sErrores = Errores.mostrarError("Error al obtener las clases clonables.", ex);
        }
    }
    private void CrearGrupo(IDataReader dr)
    {
        sb.Append("<tr tipo='G' style='height:20px;'>");
        sb.Append("<td style='padding-left:5px;'>" + dr["t326_denominacion"].ToString() + "</td>");
        sb.Append("</tr>");
        CrearSubgrupo(dr);
    }
    private void CrearSubgrupo(IDataReader dr)
    {
        sb.Append("<tr tipo='S' style='height:20px;'>");
        sb.Append("<td style='padding-left:50px;'>" + dr["t327_denominacion"].ToString() + "</td>");
        sb.Append("</tr>");
        CrearConcepto(dr);
    }
    private void CrearConcepto(IDataReader dr)
    {
        sb.Append("<tr tipo='C' style='height:20px;' >");
        sb.Append("<td style='padding-left:100px;'>" + dr["t328_denominacion"].ToString() + "</td>");
        sb.Append("</tr>");
        CrearClase(dr);
    }
    private void CrearClase(IDataReader dr)
    {
        sb.Append("<tr tipo='CL' id='" + dr["t329_idclaseeco"].ToString() + "' style='height:20px;cursor:pointer;' >");
        sb.Append("<td style='padding-left:150px;'><input type='checkbox' class='checkTabla' style='vertical-align:middle;' checked /><label style='cursor:pointer; margin-left:5px;'  onclick='this.previousSibling.click()'>" + dr["t329_denominacion"].ToString() + "</label></td>");
        sb.Append("</tr>");
    }

}
