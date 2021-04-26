using System;
using System.Web.UI;
using GASVI.BLL;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Capa_Presentacion_BonoTransporte_BonoNuevo_Default : System.Web.UI.Page
{
    public string strTablaHtml;
    public string sErrores = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["GVT_IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                if (Request.QueryString.Get("iB") != null)
                {
                    hdnIdBono.Text = Utilidades.decodpar(Request.QueryString.Get("iB").ToString());
                    this.Title = " ::: GASVI 2.0 ::: - Detalle de bono transporte&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    if (Request.QueryString.Get("dB") != null)
                    {
                        txtDenominacion.Text = Utilidades.decodpar(Request.QueryString.Get("dB").ToString());
                    }
                    if (Request.QueryString.Get("eB") != null)
                    {
                        if (Utilidades.decodpar(Request.QueryString.Get("eB").ToString()) == "Activo") rdbEstado.Text = "A";
                        else rdbEstado.Text = "B";
                    }                    
                    obtenerMonedas();
                    if (Request.QueryString.Get("iM") != null)
                    {
                        cboMoneda.SelectedValue = Utilidades.decodpar(Request.QueryString.Get("iM").ToString());
                    }
                    //if (Request.QueryString.Get("dsB") != null)
                    //{
                    //    txtDescripcion.Text = Request.QueryString.Get("dsB").ToString();
                    //}
                }
                else this.Title = " ::: GASVI 2.0 ::: - Nuevo bono transporte&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del bono de transporte", ex);
            }

        }
    }

    protected void obtenerMonedas()
    {
        List<ElementoLista> oLista = MONEDA.ObtenerMonedas(true);
        ListItem oLI = null;
        foreach (ElementoLista oMoneda in oLista)
        {
            oLI = new ListItem(oMoneda.sDenominacion, oMoneda.sValor);
            //oLI.Attributes.Add("defecto", oMoneda.sDatoAux1);
            if (oMoneda.sValor == "EUR") oLI.Selected = true;
            cboMoneda.Items.Add(oLI);
        }
    }

}
