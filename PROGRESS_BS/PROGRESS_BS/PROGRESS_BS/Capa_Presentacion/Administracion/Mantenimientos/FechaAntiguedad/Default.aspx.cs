using IB.Progress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Mantenimientos_FechaAntiguedad_Default : System.Web.UI.Page
{
    public string defectoAntiguedad;
    protected void Page_Load(object sender, EventArgs e)
    {
        IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = null;
        try
        {
            valoracionesBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            List<VALORACIONESPROGRESS.TemporadaProgress> misvaloraciones = valoracionesBLL.TemporadaProgress();
            
            DateTime dte = misvaloraciones[0].T936_referenciaantiguedad;

            int prevMonth = dte.AddMonths(-1).Month;

            defectoAntiguedad = "var defectoAntiguedad = new Date(" + dte.Year + ", " + prevMonth + " , " + dte.Day + ");";

            valoracionesBLL.Dispose();
        }
        catch (Exception ex)
        {
            if (valoracionesBLL != null) valoracionesBLL.Dispose();
            throw ex;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void updateFecha(DateTime fecha)
    {
        try
        {
            IB.Progress.BLL.VALORACIONESPROGRESS rlb = new IB.Progress.BLL.VALORACIONESPROGRESS();

            rlb.UpdateFecha(fecha);
            rlb.Dispose();


        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar categoría/colectivo", ex.Message);
        }

    }
}