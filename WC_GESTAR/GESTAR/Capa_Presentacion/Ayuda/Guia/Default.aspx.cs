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
using EO.Web;
using GESTAR.Capa_Negocio;

public partial class Capa_Presentacion_Ayuda_Guia_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.bFuncionesLocales = true;
        Master.nBotonera = 8;
        Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                Response.Redirect(HistorialNavegacion.Leer(), true);
                break;
        }
    }

}
