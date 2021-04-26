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


public partial class getDetalle : System.Web.UI.Page
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

            Int64 nID = Int64.Parse(Utilidades.decodpar(Request.QueryString["nd"].ToString()));
            //string nID = Utilidades.decodpar(Request.QueryString["nd"].ToString());
            SqlDataReader dr = AUDITSUPER.ObtenerDetalle(nID);
            if (dr.Read())
            {
                cldTabla.InnerText = dr["t300_tabladenusuario"].ToString();
                cldCampo.InnerText = dr["Campo"].ToString();
                cldAccion.InnerText = dr["Accion"].ToString();
                cldQue.InnerText = dr["Que"].ToString();
                cldQuien.InnerHtml = dr["Quien"].ToString();
                cldCuando.InnerText = dr["t499_cuando"].ToString();
                cldAntiguo.InnerText = dr["t499_valorantiguo"].ToString();
                cldNuevo.InnerText = dr["t499_valornuevo"].ToString();
                //usuario del sistema ? no mostrar USU_APLICACION
                if (dr["t499_usuario_sistema"].ToString().ToUpper() != "USU_APLICACION")
                    cldUsuSis.InnerText = dr["t499_usuario_sistema"].ToString();
                else
                    cldUsuSis.InnerText = "Aplicación SUPER.Net";
                cldEquipo.InnerText = dr["t499_hostname"].ToString();
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }

    }
}
