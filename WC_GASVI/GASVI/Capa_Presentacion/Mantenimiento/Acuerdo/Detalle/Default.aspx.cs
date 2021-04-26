using System;
using System.Web.UI;
using GASVI.BLL;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Acuerdo_AcuerdoNuevo_Default : System.Web.UI.Page
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
                obtenerMonedas();
                if (Request.QueryString.Get("iA") != null)
                {
                    hdnIdAcuerdo.Text = Utilidades.decodpar(Request.QueryString.Get("iA").ToString());
                    this.Title = " ::: GASVI 2.0 ::: - Detalle del acuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                    //if (Request.QueryString.Get("dA") != null) txtDenominacion.Text = Utilidades.unescape(Request.QueryString.Get("dA").ToString().Replace((char)34, (char)39));
                    if (Request.QueryString.Get("dA") != null) txtDenominacion.Text = Utilidades.decodpar(Request.QueryString.Get("dA").ToString());
                    if (Request.QueryString.Get("fi") != null) txtInicio.Text = Utilidades.decodpar(Request.QueryString.Get("fi").ToString());
                    if (Request.QueryString.Get("ff") != null) txtFin.Text = Utilidades.decodpar(Request.QueryString.Get("ff").ToString());
                    //if (Request.QueryString.Get("nR") != null) txtResponsable.Text = Utilidades.unescape(Request.QueryString["nR"].ToString().Replace((char)34, (char)39));
                    if (Request.QueryString.Get("nR") != null) txtResponsable.Text = Utilidades.decodpar(Request.QueryString["nR"].ToString());
                    if (Request.QueryString.Get("iR") != null) hdnIdResp.Text = Utilidades.decodpar(Request.QueryString.Get("iR").ToString());
                    if (Request.QueryString.Get("iM") != null)
                    {
                        cboMoneda.SelectedValue = Utilidades.decodpar(Request.QueryString.Get("iM").ToString());
                    }

                }
                else this.Title = " ::: GASVI 2.0 ::: - Nuevo acuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                hdnIdMod.Text = Session["GVT_IDFICEPI"].ToString();
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
            //if (oMoneda.sDatoAux1 == "1") oLI.Selected = true;
            if (oMoneda.sValor == "EUR") oLI.Selected = true;
            cboMoneda.Items.Add(oLI);
        }
    }


}
