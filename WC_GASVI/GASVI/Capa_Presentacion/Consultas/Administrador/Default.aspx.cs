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
using System.Text.RegularExpressions;
using EO.Web;
using GASVI.BLL;

public partial class Capa_Presentacion_Consultas_Administrador_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.nBotonera = 8;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js?14112017");
            Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            Master.TituloPagina = "Consulta de mis notas";
            Master.bFuncionesLocales = true;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            if (!Page.IsCallback)
            {
                if (Request.QueryString["so"] != null)
                {
                    if (Session["GVT_FILTROSSOLICITUDESADM"] != null)
                        Session["GVT_FILTROSSOLICITUDESADM"] = null;

                    hdnDesde.Text = (DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month).ToString();
                    txtDesde.Text = mes[DateTime.Today.AddMonths(-1).Month - 1] + " " + DateTime.Today.AddMonths(-1).Year.ToString();

                    hdnHasta.Text = (DateTime.Today.Year * 100 + DateTime.Today.Month).ToString();
                    txtHasta.Text = mes[DateTime.Today.Month - 1] + " " + DateTime.Today.Year.ToString();
                }
                else
                {
                    string[] aFiltros = (string[])Session["GVT_FILTROSSOLICITUDESADM"];
                    chkEstadoT.Checked = (aFiltros[0].IndexOf("T") > -1) ? true : false;
                    chkEstadoS.Checked = (aFiltros[0].IndexOf("S") > -1) ? true : false;
                    chkEstadoN.Checked = (aFiltros[0].IndexOf("N") > -1) ? true : false;
                    chkEstadoR.Checked = (aFiltros[0].IndexOf("R") > -1) ? true : false;
                    chkEstadoA.Checked = (aFiltros[0].IndexOf("A") > -1) ? true : false;
                    chkEstadoB.Checked = (aFiltros[0].IndexOf("B") > -1) ? true : false;
                    chkEstadoL.Checked = (aFiltros[0].IndexOf("L") > -1) ? true : false;
                    chkEstadoO.Checked = (aFiltros[0].IndexOf("O") > -1) ? true : false;
                    chkEstadoC.Checked = (aFiltros[0].IndexOf("C") > -1) ? true : false;
                    chkEstadoX.Checked = (aFiltros[0].IndexOf("X") > -1) ? true : false;

                    chkMotivo1.Checked = (aFiltros[1].IndexOf("1") > -1) ? true : false;
                    chkMotivo4.Checked = (aFiltros[1].IndexOf("4") > -1) ? true : false;
                    chkMotivo2.Checked = (aFiltros[1].IndexOf("2") > -1) ? true : false;
                    chkMotivo5.Checked = (aFiltros[1].IndexOf("5") > -1) ? true : false;
                    chkMotivo3.Checked = (aFiltros[1].IndexOf("3") > -1) ? true : false;
                    chkMotivo6.Checked = (aFiltros[1].IndexOf("6") > -1) ? true : false;

                    hdnDesde.Text = aFiltros[2].ToString();
                    txtDesde.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(aFiltros[2]));

                    hdnHasta.Text = aFiltros[3].ToString();
                    txtHasta.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(aFiltros[3]));

                    txtConcepto.Text = aFiltros[4];
                    hdnIdProyectoSubNodo.Text = aFiltros[5];
                    txtReferencia.Text = aFiltros[6];
                    txtProyecto.Text = aFiltros[7];

                    hdnIdAprobador.Text = aFiltros[8];
                    txtAprobador.Text = aFiltros[9];
                    hdnIdBeneficiario.Text = aFiltros[10];
                    txtBeneficiario.Text = aFiltros[11];

                    hdnCR.Text = aFiltros[12];
                    txtCR.Text = aFiltros[13];
                    hdnCli.Text = aFiltros[14];
                    txtCli.Text = aFiltros[15];

                }

                //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
                //   y la funci�n que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2� Se "registra" la funci�n que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar la p�gina.", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getSolicitudesADM"):
                try
                {
                    Session["GVT_FILTROSSOLICITUDESADM"] = new string[] { aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16] };
                    /*string sEstados,
                    string sMotivos,
                    string nDesde,
                    string nHasta,
                    string t420_concepto,
                    string t305_idproyectosubnodo,
                    string t420_idreferencia)
                    txtProyecto,
                    hdnIdAprobador,
                    txtAprobador,
                    hdnIdBeneficiario,
                    txtBeneficiario */

                    sResultado += "OK@#@" + CABECERAGV.ObtenerSolicitudesADM(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[9], aArgs[11], aArgs[13], aArgs[15]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener las solicitudes.", ex);
                }
                break;

        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
}
