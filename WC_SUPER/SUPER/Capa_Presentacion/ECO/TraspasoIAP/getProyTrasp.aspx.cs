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



public partial class Default : System.Web.UI.Page
{
    public string strTablaHTML="";
    public string sErrores = "";

    protected void Page_Load(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
            }
        }
    }

    //private string ObtenerProyectos(string sUser, string sNodo, string sEstado, string sCategoria, string sCliente)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    int? idNodo = null;
    //    int? idCliente = null;

    //    try
    //    {
    //        if (sNodo != "") idNodo = int.Parse(sNodo);
    //        if (sCliente != "") idCliente = int.Parse(sCliente);

    //        sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 920px;' cellSpacing='0' border='0'>");
    //        sb.Append("<colgroup>");
    //        sb.Append("<col style='width:20px' />");
    //        sb.Append("<col style='width:20px' />");
    //        sb.Append("<col style='width:20px' />");
    //        sb.Append("<col style='width:65px; text-align:right; padding-right:10px;' />");
    //        sb.Append("<col style='width:355px' />");
    //        sb.Append("<col style='width:200px' />");
    //        sb.Append("<col style='width:240px' />");
    //        sb.Append("</colgroup>");
    //        sb.Append("<tbody>");
    //        SqlDataReader dr = PROYECTO.ObtenerProyectosModuloEco(int.Parse(sUser), idNodo, sEstado, sCategoria, idCliente,"");

    //        while (dr.Read())
    //        {
    //            sb.Append("<tr onDblClick='aceptarClick(this.rowIndex)' style='height:20px' id=\"");
    //            sb.Append(dr["t305_idproyectosubnodo"].ToString());
    //            sb.Append("///");
    //            sb.Append(dr["modo_lectura"].ToString());
    //            sb.Append("\" onmouseover='TTip()'>");

    //            if (dr["t301_categoria"].ToString() == "P") sb.Append("<td><img src='../../../images/imgProducto.gif' style='width:16px;height:16px;' /></td>");
    //            else sb.Append("<td><img src='../../../images/imgServicio.gif' width='16px' height='16px' /></td>");

    //            //sb.Append("<td><img src='../../../images/imgIconoContratante.gif' /></td>");

    //            switch (dr["t305_cualidad"].ToString())
    //            {
    //                case "C": sb.Append("<td><img src='../../../images/imgIconoContratante.gif' style='width:16px;height:16px;' /></td>"); break;
    //                case "J": sb.Append("<td><img src='../../../images/imgIconoRepJor.gif' style='width:16px;height:16px;' /></td>"); break;
    //                case "P": sb.Append("<td><img src='../../../images/imgIconoRepPrecio.gif' style='width:16px;height:16px;' /></td>"); break;
    //            }

    //            switch (dr["t301_estado"].ToString())
    //            {
    //                case "A": sb.Append("<td><img src='../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='width:16px;height:16px;' /></td>"); break;
    //                case "C": sb.Append("<td><img src='../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='width:16px;height:16px;' /></td>"); break;
    //                case "H": sb.Append("<td><img src='../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='width:16px;height:16px;' /></td>"); break;
    //                case "P": sb.Append("<td><img src='../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='width:16px;height:16px;' /></td>"); break;
    //            }
                
    //            sb.Append("<td");
    //            if(ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
    //                sb.Append(" title=\"" + dr["motivo"].ToString() + "\"");
    //            sb.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
    //            sb.Append("<td><nobr class='NBR W350'>" + dr["t301_denominacion"].ToString() + "</nobr></td>");
    //            sb.Append("<td><nobr class='NBR W190'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
    //            sb.Append("<td><nobr class='NBR W230'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
    //            sb.Append("</tr>");
    //        }
    //        dr.Close(); 
    //        dr.Dispose();
    //        sb.Append("</tbody>");
    //        sb.Append("</table>");

    //        return "OK@#@"+sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        sErrores += Errores.mostrarError("Error al obtener los proyectos económicos", ex);
    //        return "Error@#@Error al obtener los proyectos económicos";
    //    }
    //}
}
