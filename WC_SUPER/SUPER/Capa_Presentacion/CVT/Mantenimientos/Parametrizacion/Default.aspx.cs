using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using SUPER.Capa_Negocio;
using System.Text;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

public partial class Parametrizacion : System.Web.UI.Page
{
    public string strTablaHtmlTablas;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Parametrización";
            Master.bFuncionesLocales = true;
            Master.nBotonera = 9;
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.Modulo = "CVT";
            Utilidades.SetEventosFecha(this.txt_fproceso_act_masi);
            Utilidades.SetEventosFecha(this.txt_fultenvio_tar_ven_noven);
            Utilidades.SetEventosFecha(this.txt_fultenvio_tar_ven_mieq);
 
            try
            {
                PARAMETRIZACIONSUPER oPar = PARAMETRIZACIONSUPER.Select(null);
                txt_ndias_act_masi.Value = oPar.t725_ndias_act_masi.ToString("#,###");
                txt_ndias_envi_validar.Value = oPar.t725_ndias_envi_validar.ToString("#,###");
                txt_ndias_validar_reg.Value = oPar.t725_ndias_validar_reg.ToString("#,###");
                txt_ndias_cualifi_proy.Value = oPar.t725_ndias_cualifi_proy.ToString("#,###");
                txt_ndias_alta_exp.Value = oPar.t725_ndias_alta_exp.ToString("#,###");
                txt_ndias_cualifi_proy.Value = oPar.t725_ndias_cualifi_proy.ToString("#,###");
                txt_ndias_peticion_bor.Value = oPar.t725_ndias_peticion_bor.ToString("#,###");
                txt_ndias_tar_ven_noven.Value = oPar.t725_ndias_tar_ven_noven.ToString("#,###");
                txt_ndias_tar_ven_mieq.Value = oPar.t725_ndias_tar_ven_mieq.ToString("#,###");
                txt_fproceso_act_masi.Text = (oPar.t725_fproceso_act_masi.ToString() == "") ? "" : DateTime.Parse(oPar.t725_fproceso_act_masi.ToString()).ToShortDateString();
                txt_fultenvio_tar_ven_noven.Text = (oPar.t725_fultenvio_tar_ven_noven.ToString() == "") ? "" : DateTime.Parse(oPar.t725_fultenvio_tar_ven_noven.ToString()).ToShortDateString();
                txt_fultenvio_tar_ven_mieq.Text = (oPar.t725_fultenvio_tar_ven_mieq.ToString() == "") ? "" : DateTime.Parse(oPar.t725_fultenvio_tar_ven_mieq.ToString()).ToShortDateString();
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos de los parámetros.", ex);
            }
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Grabar(string sParametrizacion)
    {
        try
        {
            return "OK@#@" + SUPER.Capa_Negocio.PARAMETRIZACIONSUPER.Grabar(sParametrizacion);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al grabar los parámetros de notificaciones de correo.", ex);
        }
    }
}
