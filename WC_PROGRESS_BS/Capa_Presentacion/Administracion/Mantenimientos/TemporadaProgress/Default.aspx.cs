using IB.Progress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Mantenimientos_TemporadaProgress_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.VALORACIONESPROGRESS.TemporadaProgress> getTemporada()
    {
        IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = null;
        try
        {
            valoracionesBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            List<VALORACIONESPROGRESS.TemporadaProgress> listTemporada = valoracionesBLL.TemporadaProgress();

            valoracionesBLL.Dispose();

            return listTemporada;

        }
        catch (Exception ex)
        {
            if (valoracionesBLL != null) valoracionesBLL.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener la temporada Progress", ex.Message);
            throw ex;
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void updateTemporada(IB.Progress.Models.VALORACIONESPROGRESS.TemporadaProgress oDatos)
    {
        try
        {
            IB.Progress.BLL.VALORACIONESPROGRESS rlb = new IB.Progress.BLL.VALORACIONESPROGRESS();

            rlb.updateTemporada(oDatos);
            rlb.Dispose();


        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar la temporada Progress", ex.Message);
        }

    }


}