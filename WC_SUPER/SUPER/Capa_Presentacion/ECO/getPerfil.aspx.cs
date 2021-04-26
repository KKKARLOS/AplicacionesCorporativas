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


public partial class getPerfil : System.Web.UI.Page
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
            string sListaPerfilesABorrar= Request.QueryString["lstB"].ToString();
            ObtenerPerfiles(int.Parse(Request.QueryString["nPE"].ToString()), sListaPerfilesABorrar);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerPerfiles(int nPE, string sListaPerfilesABorrar)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            string[] aPerfiles=null;
            if (sListaPerfilesABorrar != "")
            {
                aPerfiles = Regex.Split(sListaPerfilesABorrar, ",");
            }
            SqlDataReader dr = PERFILPROY.SelectByT301_idproyecto(null, nPE);

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='width:347px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                if (sListaPerfilesABorrar != "")
                {
                    if (!Utilidades.EstaEnLista(dr["t333_idperfilproy"].ToString(), aPerfiles))
                    {
                        sb.Append("<tr id='" + dr["t333_idperfilproy"].ToString() + "' style='height:16px;'");
                        sb.Append(" onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td style='padding-left:3px;'>" + dr["t333_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
                else
                {
                    sb.Append("<tr id='" + dr["t333_idperfilproy"].ToString() + "' style='height:16px;'");
                    sb.Append(" onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                    sb.Append("<td style='padding-left:3px;'>" + dr["t333_denominacion"].ToString() + "</td>");
                    sb.Append("</tr>");
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
            sErrores = Errores.mostrarError("Error al obtener la relación de perfiles.", ex);
        }
    }

}
