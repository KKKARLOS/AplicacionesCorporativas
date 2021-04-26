using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;


public partial class getCliente : System.Web.UI.Page
{
    public string sErrores = "";
    public int nIdAgrup = 0;

	private void Page_Load(object sender, System.EventArgs e)
	{
        if (!Page.IsCallback)
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

                int nIdProy=int.Parse(Utilidades.decodpar(Request.QueryString["p"].ToString()));
                txtProyectoInicio.Text = nIdProy.ToString("#,###");
                nIdAgrup = int.Parse(Utilidades.decodpar(Request.QueryString["a"].ToString()));
                hdnIdAgrupacion.Text = nIdAgrup.ToString();

                if (nIdAgrup > 0)
                {
                    txtDenominacion.Text = Utilidades.decodpar(Request.QueryString["d"].ToString());
                    SqlDataReader dr = AGRUPACIONPROYECTO.ObtenerCatalogo(nIdAgrup);
                    string sProyectos = "";
                    while (dr.Read())
                    {
                        if (dr["t301_idproyecto"].ToString() != nIdProy.ToString())
                           sProyectos += dr["t301_idproyecto"].ToString() + ";";
                    }
                    dr.Close();
                    dr.Dispose();
                    txtProyectos.Text = sProyectos;
                }
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los clientes", ex);
            }
        }
    }

    //private string ObtenerClientes(string sTipoBusqueda, string strCli, string sInterno, string sSoloActivos, string sTipoCliente)
    //{
    //    try
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 550px; table-layout:fixed;' cellpadding='0' cellSpacing='0' border='0'>");
    //        sb.Append("<colgroup><col style='padding-left:5px;'></colgroup>");
    //        sb.Append("<tbody>");
    //        SqlDataReader dr = CLIENTE.SelectByNombreSAP(strCli, sTipoBusqueda, (sSoloActivos == "1") ? true : false, (sInterno == "1") ? true : false, sTipoCliente);

    //        int i = 0;
    //        bool bExcede = false;
    //        while (dr.Read())
    //        {
    //            sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' ");
    //            sb.Append(" cif='" + dr["CIF"].ToString() + "' ");
    //            if ((bool)dr["t302_estado"]) sb.Append(" onclick=\"msse(this)\" ondblclick=\"aceptarClick(this.rowIndex)\" style='height:16px;'");
    //            else sb.Append("style='height:16px;cursor:default;color:gray;'");

    //            sb.Append("><td><img src='../../../../images/img" + dr["tipo"].ToString() + ".gif' ");
    //            if (dr["tipo"].ToString() == "M") sb.Append("style='margin-right:5px;'");
    //            else sb.Append("style='margin-left:15px;margin-right:5px;'");
    //            sb.Append("><nobr class='NBR W475'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
    //            sb.Append("</tr>");
    //            i++;
    //            if (i > Constantes.nNumMaxTablaCatalogo)
    //            {
    //                bExcede = true;
    //                break;
    //            }
    //        }
    //        dr.Close();
    //        dr.Dispose();
    //        if (!bExcede)
    //        {
    //            sb.Append("</tbody>");
    //            sb.Append("</table>");
    //        }else
    //        {
    //            sb.Length = 0;
    //            sb.Append("EXCEDE");
    //        }

    //        return "OK@#@" + sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al obtener los clientes", ex);
    //    }
    //}
}
