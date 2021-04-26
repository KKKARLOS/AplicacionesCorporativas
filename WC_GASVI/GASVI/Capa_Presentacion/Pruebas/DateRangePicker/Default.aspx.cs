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

public partial class Capa_Presentacion_Pruebas_DateRangePicker_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/DateRangePicker/js/jquery-1.3.1.min.js");
        Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/DateRangePicker/js/jquery-ui-1.7.1.custom.min.js");
        Master.FuncionesJavaScript.Add("Capa_Presentacion/Pruebas/DateRangePicker/js/daterangepicker.jQuery.js");
        Master.FicherosCSS.Add("Capa_Presentacion/Pruebas/DateRangePicker/css/ui.daterangepicker.cs");
        Master.FicherosCSS.Add("Capa_Presentacion/Pruebas/DateRangePicker/css/redmond/jquery-ui-1.7.1.custom.css");

    }
}
