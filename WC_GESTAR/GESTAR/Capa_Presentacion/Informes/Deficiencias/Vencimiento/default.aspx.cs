using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GESTAR.Capa_Negocio;
using System.Text.RegularExpressions;
using EO.Web;

namespace GESTAR.Capa_Presentacion.ASPX
{
    /// <summary>
    /// Descripción breve de main2.
    /// </summary>
    /// 

    public partial class Vencimiento: System.Web.UI.Page
    {
        //SqlDataReader dr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.TituloPagina = "Informe de órdenes por fechas";
            Master.bFuncionesLocales = true;
            Master.nBotonera = 12;
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            
            Utilidades.SetEventosFecha(this.txtFechaInicio);
            Utilidades.SetEventosFecha(this.txtFechaFin);
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["PANT_AVANZADO"] = null; // BORRARIA LOS FILTROS DE OTRAS PANTALLAS ( POR SI NO DA AL BOTÓN REGRESAR )
                    Session["PANT_PROPIAS_TAR"] = null;
                    Session["PANT_AVANZADO_TAR"] = null;

                    this.txtFechaInicio.Attributes.Add("readonly", "readonly");
                    this.txtFechaFin.Attributes.Add("readonly", "readonly");
                    txtArea.Attributes.Add("readonly", "readonly");

                    string strDia = DateTime.Today.Day.ToString();
                    if (strDia.Length == 1) strDia = "0" + strDia;
                    string strMes = DateTime.Today.Month.ToString();
                    if (strMes.Length == 1) strMes = "0" + strMes;

                    txtFechaInicio.Text ="01/01/" + DateTime.Today.Year.ToString();
                    txtFechaFin.Text = strDia + "/" + strMes + "/" + DateTime.Today.Year.ToString();

                    if (Session["PANT_VENCIMIENTO"] != null)
                    {
                        System.Web.UI.Control Area = Master.FindControl("CPHC");
                        PonerValoresFiltros.Actualizar(Area.Controls, (Hashtable)Session["PANT_VENCIMIENTO"]);
                        //Master.Botonera.Items[0].Enabled = true;
                        //Master.nBotonera = 12;
                    }
               }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al cargar la página", ex);
            }
        }

        protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
        {
            switch (e.Item.CommandName.ToLower())
            {
                case "buscar":
                    Hashtable aFiltros = new Hashtable();

                    aFiltros.Add("rdlOpciones", rdlOpciones.SelectedValue);
                    aFiltros.Add("txtFechaInicio", txtFechaInicio.Text);
                    aFiltros.Add("txtFechaFin", txtFechaFin.Text);
                    aFiltros.Add("hdnIDArea", hdnIDArea.Text);
                    aFiltros.Add("txtArea", txtArea.Text);

                    Session["PANT_VENCIMIENTO"] = aFiltros;
                    Session["ORIGEN"] = "PANT_VENCIMIENTO";

                    Response.Redirect("../Catalogo/default.aspx");
                    break;     			
                case "regresar":
                    Session["PANT_VENCIMIENTO"] = null;
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                    break;
            }
        }
    }
}
