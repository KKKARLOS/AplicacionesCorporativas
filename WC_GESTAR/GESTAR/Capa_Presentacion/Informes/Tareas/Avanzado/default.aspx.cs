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

    public partial class TareasAvan : System.Web.UI.Page
    {
        //SqlDataReader dr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.TituloPagina = "Informe de tareas por criterios avanzados";
            Master.bFuncionesLocales = true;
            Master.nBotonera = 12;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

            Utilidades.SetEventosFecha(this.txtFechaInicio);
            Utilidades.SetEventosFecha(this.txtFechaFin);

            try
            {
                if (!Page.IsPostBack)
                {
                    Session["PANT_AVANZADO"] = null; // BORRARIA LOS FILTROS DE OTRAS PANTALLAS ( POR SI NO DA AL BOTÓN REGRESAR )
                    Session["PANT_VENCIMIENTO"] = null; 
                    Session["PANT_PROPIAS_TAR"] = null;

                    txtFechaInicio.Attributes.Add("readonly", "readonly");
                    txtFechaFin.Attributes.Add("readonly", "readonly");
                    txtArea.Attributes.Add("readonly", "readonly");

                    string strDia = DateTime.Today.Day.ToString();
                    if (strDia.Length == 1) strDia = "0" + strDia;
                    string strMes = DateTime.Today.Month.ToString();
                    if (strMes.Length == 1) strMes = "0" + strMes;
                    //txtFechaInicio.Text = "01/01/" + DateTime.Today.Year.ToString();
                    //txtFechaFin.Text = strDia + "/" + strMes + "/" + DateTime.Today.Year.ToString();
                    this.hdnFDesde.Value = "01/01/" + DateTime.Today.Year.ToString();
                    this.hdnFHAsta.Value = strDia + "/" + strMes + "/" + DateTime.Today.Year.ToString();

                    cboSituacion.Items.Insert(0, new ListItem("", "3"));
                    //cboSituacion.Items[0].Selected = true;

                    cboRtado.Items.Insert(0, new ListItem("", "0"));
                    cboRtado.Items[0].Selected = true;

                    if (Session["PANT_AVANZADO_TAR"] != null)
                    {
                        System.Web.UI.Control Area = Master.FindControl("CPHC");
                        PonerValoresFiltros.Actualizar(Area.Controls, (Hashtable)Session["PANT_AVANZADO_TAR"]);
                        //Master.Botonera.Items[0].Enabled = true;
                        //Master.nBotonera = 12;
                    }

                    //txtFechaFin.Text = dtmFechaFin.AddMonths(1).AddDays(-1).ToShortDateString();
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


                    aFiltros.Add("hdnIDArea", hdnIDArea.Text);
                    aFiltros.Add("txtArea", txtArea.Text);

                    aFiltros.Add("hdnDeficiencia", hdnDeficiencia.Text);
                    aFiltros.Add("txtDeficiencia", txtDeficiencia.Text);

                    aFiltros.Add("hdnCoordinador", hdnCoordinador.Text);
                    aFiltros.Add("txtCoordinador", txtCoordinador.Text);

                    aFiltros.Add("hdnEspecialista", hdnEspecialista.Text);
                    aFiltros.Add("txtEspecialista", txtEspecialista.Text);

                    aFiltros.Add("cboSituacion", cboSituacion.SelectedValue);
                    aFiltros.Add("cboRtado", cboRtado.SelectedValue);

                    aFiltros.Add("rdlOpciones", rdlOpciones.SelectedValue);

                    aFiltros.Add("txtFechaInicio", txtFechaInicio.Text);
                    aFiltros.Add("txtFechaFin", txtFechaFin.Text);

                    aFiltros.Add("chkTramitadas", chkTramitadas.Checked);
                    aFiltros.Add("chkPdteAclara", chkPdteAclara.Checked);
                    aFiltros.Add("chkAclaRta", chkAclaRta.Checked);
                    aFiltros.Add("chkAceptadas", chkAceptadas.Checked);
                    aFiltros.Add("chkRechazadas", chkRechazadas.Checked);
                    aFiltros.Add("chkEnEstudio", chkEnEstudio.Checked);
                    aFiltros.Add("chkPdtesDeResolucion", chkPdtesDeResolucion.Checked);
                    aFiltros.Add("chkPdtesDeAcepPpta", chkPdtesDeAcepPpta.Checked);
                    aFiltros.Add("chkEnResolucion", chkEnResolucion.Checked);
                    aFiltros.Add("chkResueltas", chkResueltas.Checked);
                    aFiltros.Add("chkNoAprobadas", chkNoAprobadas.Checked);

                    Session["PANT_AVANZADO_TAR"] = aFiltros;
                    Session["ORIGEN"] = "PANT_AVANZADO_TAR";

                    Response.Redirect("../Catalogo/default.aspx");
                    break;     			
                case "regresar":
                    Session["PANT_AVANZADO_TAR"] = null;
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                    break;
            }
        }
    }
}
