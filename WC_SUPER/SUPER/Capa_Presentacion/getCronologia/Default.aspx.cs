using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page
{
    public string strTablaHTML = "", sErrores="";
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

                #region recojo parámetros
                if (Request.QueryString["t"] != null)
                {//Nombre de la tabla
                    this.hdnTabla.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["t"].ToString());
                }
                if (Request.QueryString["k"] != null)
                {//Clave del registro 
                    if (SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["t"]) == "ASICRONO" ||
                        SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["t"]) == "T595_FICEPIEXAMENCRONO")
                    {
                        string[] key = Regex.Split(SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["k"].ToString()), "@@");
                        this.hdnKey.Value = key[0];//ASICRONO:idcurso
                        this.hdnKey2.Value = key[1];//idficepi
                    }
                    else
                        this.hdnKey.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["k"].ToString());
                }
                #endregion

                strTablaHTML = SUPER.BLL.Historial.ObtenerHistorial(this.hdnTabla.Value, int.Parse(this.hdnKey.Value),(this.hdnKey2.Value=="-1") ? null: (int?)int.Parse(this.hdnKey2.Value));
            }
            catch (Exception ex)
            {
                sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener los datos", ex);
            }
        }
    }
}
