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
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_PSP_CONCEP_ESTRUC_Default : System.Web.UI.Page
{
    public string sErrores="";

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

                if (Utilidades.EstructuraActiva("SN4")) cboNivelEstru.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4), "19"));
                if (Utilidades.EstructuraActiva("SN3")) cboNivelEstru.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3), "18"));
                if (Utilidades.EstructuraActiva("SN2")) cboNivelEstru.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2), "17"));
                if (Utilidades.EstructuraActiva("SN1")) cboNivelEstru.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1), "16"));
                cboNivelEstru.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.NODO), "15"));
                cboNivelEstru.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO), "1"));

            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
            }
        }
    }

}
