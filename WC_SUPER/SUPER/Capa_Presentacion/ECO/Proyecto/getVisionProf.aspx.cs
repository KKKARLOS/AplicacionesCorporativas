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


public partial class getVisionProf : System.Web.UI.Page
{
    public string sErrores="", strTablaHTML="";

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

            ObtenerVision(Request.QueryString["sPSN"].ToString());
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerVision(string sPSN)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerUsuariosConVision(sPSN, "");

            sb.Append("<table id='tblDatos' style='width:700px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:430px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");

                sb.Append("bit='"+ dr["BitPE"].ToString() +"' ");
                if (dr["ml_me"].ToString() != "2"
                    || dr["am_me"].ToString() != "2")
                    sb.Append("pge='1' ");
                else
                    sb.Append("pge='0' ");
                if (dr["ml_mt"].ToString() != "2"
                    || dr["am_mt"].ToString() != "2")
                    sb.Append("pst='1' ");
                else
                    sb.Append("pst='0' ");

                sb.Append(">");

                //if (dr["t001_sexo"].ToString() == "V")
                //{
                //    switch (dr["tipo"].ToString())
                //    {
                //        case "E": sb.Append("<td><img src='../../../Images/imgUsuEV.gif' style='width:16px;height:16px;' /></td>"); break;
                //        case "I": sb.Append("<td><img src='../../../Images/imgUsuIV.gif' style='width:16px;height:16px;' /></td>"); break;
                //    }
                //}
                //else
                //{
                //    switch (dr["tipo"].ToString())
                //    {
                //        case "E": sb.Append("<td><img src='../../../Images/imgUsuEM.gif' style='width:16px;height:16px;' /></td>"); break;
                //        case "I": sb.Append("<td><img src='../../../Images/imgUsuIM.gif' style='width:16px;height:16px;' /></td>"); break;
                //    }
                //}
                sb.Append("<td><img src='../../../Images/imgUsu" + dr["tipo"].ToString() + dr["t001_sexo"].ToString() + ".gif' style='width:16px;height:16px;' /></td>");

                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W420' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Motivo:</label>" + dr["Desmotivo"].ToString() + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                switch (dr["BitPE"].ToString())
                {
                    case "0": sb.Append("<td></td>"); break;
                    case "1": 
                        sb.Append("<td><img src='../../../Images/botones/imgBitacora.gif' style='width:16px;height:16px;' /></td>"); 
                        break;
                }
                switch (dr["ml_me"].ToString())
                {
                    case "0": sb.Append("<td><img src='../../../Images/imgAccesoW.gif' style='width:16px;height:16px;' /></td>"); break;
                    case "1": sb.Append("<td><img src='../../../Images/imgAccesoR.gif' style='width:16px;height:16px;' /></td>"); break;
                    case "2": sb.Append("<td><img src='../../../Images/imgAccesoN.gif' style='width:16px;height:16px;' /></td>"); break;
                }
                //sb.Append("<td>" + dr["ml_me"].ToString() + "</td>");
                switch (dr["am_me"].ToString())
                {
                    case "0": sb.Append("<td><img src='../../../Images/imgVisionCompleta.gif' style='width:16px;height:16px;' title='Completo' /></td>"); break;
                    case "1": sb.Append("<td><img src='../../../Images/imgVisionRestringida.gif' style='width:16px;height:16px;' title='Restringido' /></td>"); break;
                    case "2": sb.Append("<td><img src='../../../Images/imgVisionNula.gif' style='width:16px;height:16px;' title='Ninguno' /></td>"); break;
                }
//                sb.Append("<td>" + dr["am_me"].ToString() + "</td>");
                switch (dr["ml_mt"].ToString())
                {
                    case "0": sb.Append("<td><img src='../../../Images/imgAccesoW.gif' style='width:16px;height:16px;' /></td>"); break;
                    case "1": sb.Append("<td><img src='../../../Images/imgAccesoR.gif' style='width:16px;height:16px;' /></td>"); break;
                    case "2": sb.Append("<td><img src='../../../Images/imgAccesoN.gif' style='width:16px;height:16px;' /></td>"); break;
                }
//                sb.Append("<td>" + dr["ml_mt"].ToString() + "</td>");
                switch (dr["am_mt"].ToString())
                {
                    case "0": sb.Append("<td><img src='../../../Images/imgVisionCompleta.gif' style='width:16px;height:16px;' title='Completo' /></td>"); break;
                    case "1": sb.Append("<td><img src='../../../Images/imgVisionRestringida.gif' style='width:16px;height:16px;' title='Restringido' /></td>"); break;
                    case "2": sb.Append("<td><img src='../../../Images/imgVisionNula.gif' style='width:16px;height:16px;' title='Ninguno' /></td>"); break;
                }
//                sb.Append("<td>" + dr["am_mt"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }

}
