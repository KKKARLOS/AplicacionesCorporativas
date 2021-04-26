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


public partial class getParametros : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";

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

            ObtenerParametros(int.Parse(Request.QueryString["nIdConsulta"].ToString()));
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los parámetros", ex);
        }

    }

    protected void ObtenerParametros(int nIdConsulta)
    {
        StringBuilder sb = new StringBuilder();
        string sChecked = "", sValor = "";
        try
        {
            SqlDataReader dr = PARAMETROCONSULTAPERSONAL.SelectByIdconsulta(null, nIdConsulta);

            sb.Append("<table id='tblDatos' class='texto' style='width: 700px;'>");
            sb.Append("<colgroup><col style='width:150px;' /><col style='width:150px;' /><col style='width:400px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sChecked = "";
                sValor = "";
                sb.Append("<tr style=\"height: 20px; ");
                if (dr["t474_visible"].ToString() == "N") sb.Append("display:none; ");
                else sb.Append("display:table-row; ");
                sb.Append("\" ");
                if ((bool)dr["t474_opcional"]) sb.Append("opcional=1 ");
                else sb.Append("opcional=0 ");
                sb.Append("tipoparam='" + dr["t474_tipoparametro"].ToString() + "' >");
                //sb.Append("<td>");
                switch (dr["t474_tipoparametro"].ToString())
                {
                    case "I":
                        sb.Append("<td title='Entero' style='padding-left:3px;'>" + dr["t474_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t474_valordefecto"].ToString() != "") sValor = int.Parse(dr["t474_valordefecto"].ToString()).ToString("#,###");
                        if (dr["t474_visible"].ToString() == "M") sb.Append("<input type='text' class='txtNumM' style='width:60px;' value='" + sValor + "' onfocus='fn(this,9,0)' />");
                        else sb.Append("<input type='text' class='txtNumL' style='width:60px;' value='" + dr["t474_valordefecto"].ToString() + "' readonly />");
                        break;
                    case "V":
                        sb.Append("<td title='Carácter' style='padding-left:3px;'>" + dr["t474_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td title='" + dr["t474_valordefecto"].ToString() + "'>");
                        if (dr["t474_visible"].ToString() == "M") sb.Append("<input type='text' class='txtM' style='width:100px;' maxlength='7500' value='" + dr["t474_valordefecto"].ToString() + "' />");
                        else sb.Append("<input type='text' class='txtL' style='width:140px;' value='" + dr["t474_valordefecto"].ToString() + "' readonly />");
                        break;
                    case "M":
                        sb.Append("<td title='Importe' style='padding-left:3px;'>" + dr["t474_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t474_valordefecto"].ToString() != "") sValor = double.Parse(dr["t474_valordefecto"].ToString()).ToString("N");
                        if (dr["t474_visible"].ToString() == "M") sb.Append("<input type='text' class='txtNumM' style='width:60px;' value='" + sValor + "' onfocus='fn(this,9,2)' />");
                        else sb.Append("<input type='text' class='txtNumL' style='width:60px;' value='" + sValor + "' readonly />");
                        break;
                    case "D":
                        sb.Append("<td title='Fecha' style='padding-left:3px;'>" + dr["t474_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t474_visible"].ToString() == "M")
                        {
                            if (Session["BTN_FECHA"].ToString()=="I")
                                sb.Append("<input type='text' id='" + dr["t472_idconsulta"].ToString() + dr["t474_textoparametro"].ToString().Trim() + "' class='txtM MANO' style='width:60px;' value='" + dr["t474_valordefecto"].ToString() + "' Calendar='oCal' onclick='mc(event);' readonly='true' />");
                            else
                                sb.Append("<input type='text' id='" + dr["t472_idconsulta"].ToString() + dr["t474_textoparametro"].ToString().Trim() + "' class='txtM MANO' style='width:60px;' value='" + dr["t474_valordefecto"].ToString() + "' Calendar='oCal' onfocus='focoFecha(event);' onmousedown='mc1(event)'/>");
                        }
                        else sb.Append("<input type='text' id='" + dr["t472_idconsulta"].ToString() + dr["t474_textoparametro"].ToString().Trim() + "' class='txtM MANO' style='width:60px;' value='" + dr["t474_valordefecto"].ToString() + "' readonly />");
                        break;
                    case "B":
                        sb.Append("<td title='Booleano' style='padding-left:3px;'>" + dr["t474_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t474_valordefecto"].ToString() == "1") sChecked = "checked";
                        else sChecked = "";
                        if (dr["t474_visible"].ToString() == "M") sb.Append("<input type='checkbox' class='check' "+ sChecked +" />");
                        else sb.Append("<input type='checkbox' class='check' " + sChecked + " disabled />");
                        break;
                    case "A":
                        sb.Append("<td title='Mes y año' style='padding-left:3px;'>" + dr["t474_nombreparametro"].ToString() + "</td>");
                        sb.Append("<td>");
                        if (dr["t474_visible"].ToString() == "M") sb.Append("<input type='text' class='txtM MANO' style='width:90px;text-align:center;' value='" + dr["t474_valordefecto"].ToString() + " 'readonly onclick='getMesValor(this)' />");
                        else sb.Append("<input type='text' class='txtM MANO' style='width:90px;text-align:center;' value='" + dr["t474_valordefecto"].ToString() + "' readonly />");
                        break;
                }
                sb.Append("</td>");

                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W300'>" + dr["t474_comentarioparametro"].ToString() + "</nobr></td>");
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
            sErrores = Errores.mostrarError("Error al obtener la relación de parámetros.", ex);
        }
    }

}
