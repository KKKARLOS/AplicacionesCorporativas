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

    public partial class Avanzado : System.Web.UI.Page //, ICallbackEventHandler
    {
        //SqlDataReader dr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.TituloPagina = "Informe de órdenes por criterios avanzados";
            Master.bFuncionesLocales = true;
            Master.nBotonera = 9;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Utilidades.SetEventosFecha(this.txtFechaInicio);
            Utilidades.SetEventosFecha(this.txtFechaFin);
    
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["PANT_VENCIMIENTO"] = null;
                    Session["PANT_PROPIAS_TAR"] = null;
                    Session["PANT_AVANZADO_TAR"] = null;

                    txtFechaInicio.Attributes.Add("readonly", "readonly");
                    txtFechaFin.Attributes.Add("readonly", "readonly");
                    txtArea.Attributes.Add("readonly", "readonly");
                    txtEntrada.Attributes.Add("readonly", "readonly");
                    txtAlcance.Attributes.Add("readonly", "readonly");
                    txtTipo.Attributes.Add("readonly", "readonly");
                    txtProducto.Attributes.Add("readonly", "readonly");
                    txtRequisito.Attributes.Add("readonly", "readonly");
                    txtCausa.Attributes.Add("readonly", "readonly");
                    txtProveedor.Attributes.Add("readonly", "readonly");
                    txtCliente.Attributes.Add("readonly", "readonly");
                    txtCR.Attributes.Add("readonly", "readonly");
                    hdnFechaInicio.Text = "01/01/" + DateTime.Today.Year.ToString();
                    string strDia = DateTime.Today.Day.ToString();
                    if (strDia.Length == 1) strDia = "0" + strDia;
                    string strMes = DateTime.Today.Month.ToString();
                    if (strMes.Length == 1) strMes = "0" + strMes;
                    hdnFechaFin.Text = strDia + "/" + strMes + "/" + DateTime.Today.Year.ToString();
                    cboRtado.Items.Insert(0, new ListItem("", "0"));
                    cboRtado.Items[0].Selected = true;

                    cboImportancia.Items.Insert(0, new ListItem("", "0"));
                    cboImportancia.Items[0].Selected = true;

                    cboPrioridad.Items.Insert(0, new ListItem("", "0"));
                    cboPrioridad.Items[0].Selected = true;

                    if (Session["PANT_AVANZADO"] != null)
                    {
                        System.Web.UI.Control Area = Master.FindControl("CPHC");
                        PonerValoresFiltros.Actualizar(Area.Controls, (Hashtable)Session["PANT_AVANZADO"]);
                        //Master.Botonera.Items[0].Enabled = true;
                        Master.nBotonera = 12;
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


                    aFiltros.Add("cboImportancia", cboImportancia.SelectedValue);
                    aFiltros.Add("cboPrioridad", cboPrioridad.SelectedValue);
                    aFiltros.Add("cboRtado", cboRtado.SelectedValue);

                    aFiltros.Add("txtFechaInicio", txtFechaInicio.Text);
                    aFiltros.Add("txtFechaFin", txtFechaFin.Text);
                    aFiltros.Add("txtArea", txtArea.Text);
                    aFiltros.Add("txtEntrada", txtEntrada.Text);
                    aFiltros.Add("txtAlcance", txtAlcance.Text);
                    aFiltros.Add("txtTipo", txtTipo.Text);
                    aFiltros.Add("txtProducto", txtProducto.Text);
                    aFiltros.Add("txtProceso", txtProceso.Text);
                    aFiltros.Add("txtRequisito", txtRequisito.Text);
                    aFiltros.Add("txtCausa", txtCausa.Text);
                    aFiltros.Add("txtProveedor", txtProveedor.Text);
                    aFiltros.Add("txtCliente", txtCliente.Text);
                    aFiltros.Add("txtCR", txtCR.Text);
                    aFiltros.Add("txtCoordinador", txtCoordinador.Text);
                    aFiltros.Add("txtSolicitante", txtSolicitante.Text);

                    aFiltros.Add("hdnIDArea", hdnIDArea.Text);
                    aFiltros.Add("hdnEntrada", hdnEntrada.Text);
                    aFiltros.Add("hdnAlcance", hdnAlcance.Text);
                    aFiltros.Add("hdnTipo", hdnTipo.Text);
                    aFiltros.Add("hdnProducto", hdnProducto.Text);
                    aFiltros.Add("hdnProceso", hdnProceso.Text);
                    aFiltros.Add("hdnRequisito", hdnRequisito.Text);
                    aFiltros.Add("hdnCausa", hdnCausa.Text);
                    aFiltros.Add("hdnCoordinador", hdnCoordinador.Text);
                    aFiltros.Add("hdnSolicitante", hdnSolicitante.Text);
                    aFiltros.Add("hdnFechaInicio", hdnFechaInicio.Text);
                    aFiltros.Add("hdnFechaFin", hdnFechaFin.Text);
                    aFiltros.Add("hdnCaso", hdnCaso.Text);

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
                    aFiltros.Add("chkAprobadas", chkAprobadas.Checked);
                    aFiltros.Add("chkAnuladas", chkAnuladas.Checked);

                    aFiltros.Add("chkTramitadas2", chkTramitadas2.Checked);
                    aFiltros.Add("chkPdteAclara2", chkPdteAclara2.Checked);
                    aFiltros.Add("chkAclaRta2", chkAclaRta2.Checked);
                    aFiltros.Add("chkAceptadas2", chkAceptadas2.Checked);
                    aFiltros.Add("chkRechazadas2", chkRechazadas2.Checked);
                    aFiltros.Add("chkEnEstudio2", chkEnEstudio2.Checked);
                    aFiltros.Add("chkPdtesDeResolucion2", chkPdtesDeResolucion2.Checked);
                    aFiltros.Add("chkPdtesDeAcepPpta2", chkPdtesDeAcepPpta2.Checked);
                    aFiltros.Add("chkEnResolucion2", chkEnResolucion2.Checked);
                    aFiltros.Add("chkResueltas2", chkResueltas2.Checked);
                    aFiltros.Add("chkNoAprobadas2", chkNoAprobadas2.Checked);
                    aFiltros.Add("chkAprobadas2", chkAprobadas2.Checked);
                    aFiltros.Add("chkAnuladas2", chkAnuladas2.Checked);

                    aFiltros.Add("rdlCasoCronologia", rdlCasoCronologia.SelectedValue);
                    aFiltros.Add("rdlCasoActual", rdlCasoActual.SelectedValue);

                    Session["PANT_AVANZADO"] = aFiltros;
                    Session["ORIGEN"] = "PANT_AVANZADO";

                    Response.Redirect("../Catalogo/default.aspx");
                    break;     			
                case "regresar":
                    Session["PANT_AVANZADO"] = null;
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                    break;
            }
        }
    }
}
