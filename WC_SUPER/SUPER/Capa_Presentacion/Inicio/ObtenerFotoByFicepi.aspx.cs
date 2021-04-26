using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Data.SqlClient;

public partial class ObtenerFotoByFicepi : System.Web.UI.Page
{
    protected byte[] binaryImage = null;

	protected void Page_Load(object sender, System.EventArgs e)
	{
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        if (Request.QueryString.Get("nF") != null && Request.QueryString.Get("nF") != "")
        {
            SqlDataReader dr = SUPER.DAL.Profesional.ObtenerFotoConversacion(int.Parse(Request.QueryString.Get("nF").ToString()));
            while (dr.Read())
            {
                if (dr["foto"] != DBNull.Value)
                    binaryImage = (byte[])(dr["foto"]);
            }
            //Cargar la foto de BD del idficepi que obtenemos por parametro
            dr.Close();
            dr.Dispose();
            Response.BinaryWrite(binaryImage);
        }
        
	}

}
