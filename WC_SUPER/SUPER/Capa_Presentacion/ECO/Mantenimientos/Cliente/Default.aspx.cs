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
    public string strTablaHtml;
 	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Mantenimiento de datos de clientes";
        Master.bFuncionesLocales = true;

        try
        {
            string strTabla0 = ObtenerClientes();
            string[] aTabla0 = Regex.Split(strTabla0, "@#@");
            if (aTabla0[0] == "OK") strTablaHtml = aTabla0[1];
            else hdnErrores.Text = aTabla0[1];
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    private string ObtenerClientes()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width:550px;'>");
            sb.Append("<tbody>");
            SqlDataReader dr = CLIENTE.DeUnResponsable((int)Session["UsuarioActual"]);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["cod_cliente"].ToString() + "' onclick='ms(this)' ondblclick='Detalle(this)' style='height:16px;' >");
                sb.Append("<td style='padding-left:5px;'>" + dr["nom_cliente"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los clientes", ex);
        }
    }
}
