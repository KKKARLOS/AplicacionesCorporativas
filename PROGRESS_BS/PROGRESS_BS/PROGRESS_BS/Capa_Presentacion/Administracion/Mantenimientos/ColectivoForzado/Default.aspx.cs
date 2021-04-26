using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Mantenimientos_ProfesionalColectivo_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static IB.Progress.Models.ColectivoFormulario ColectivoForzado()
    {
        IB.Progress.BLL.ColectivoFormulario dColectivoFormulario = null;

        try
        {
            dColectivoFormulario = new IB.Progress.BLL.ColectivoFormulario();

            IB.Progress.Models.ColectivoFormulario valores = dColectivoFormulario.ColectivoForzado();

            dColectivoFormulario.Dispose();

            return valores;
        }
        catch (Exception ex)
        {
            if (dColectivoFormulario != null) dColectivoFormulario.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al cargar el catálogo del colectivo forzado.", ex.Message);
            throw ex;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void update(List<IB.Progress.Models.Colectivo> lista)
    {
        IB.Progress.BLL.ColectivoFormulario cColectivo = null;
        try
        {
            cColectivo = new IB.Progress.BLL.ColectivoFormulario();
            cColectivo.UpdateColectivoForzado(lista);

            cColectivo.Dispose();
        }
        catch (Exception ex)
        {
            if (cColectivo != null) cColectivo.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar el colectivo forzado", ex.Message);
        }
    }
}