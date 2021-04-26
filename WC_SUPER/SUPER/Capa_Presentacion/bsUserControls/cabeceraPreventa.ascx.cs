using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_bsUserControls_cabeceraPreventa : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["DES_EMPLEADO_ENTRADA"] != null)
            {
                this.textoUsuario.InnerText = Session["DES_EMPLEADO_ENTRADA"].ToString();
            }
            else
                this.textoUsuario.InnerText = "";
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }
}