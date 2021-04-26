using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Mantenimientos_ModeloFormulario_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static IB.Progress.Models.ColectivoFormulario catalogo()
    {
        IB.Progress.BLL.ColectivoFormulario dColectivoFormulario = null;

        try
        {
            dColectivoFormulario = new IB.Progress.BLL.ColectivoFormulario();

            IB.Progress.Models.ColectivoFormulario valores = dColectivoFormulario.catalogo();

            dColectivoFormulario.Dispose();

            return valores;
        }
        catch (Exception ex)
        {
            if (dColectivoFormulario != null) dColectivoFormulario.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al cargar el catálogo de colectivo/formulario.", ex.Message);
            throw ex;
        }
    }


    [WebMethod]
    public static void update(int t941_idcolectivo, int t934_idmodeloformulario)
    {
        try
        {
            IB.Progress.BLL.ColectivoFormulario rlb = new IB.Progress.BLL.ColectivoFormulario();
            rlb.Update(t941_idcolectivo, t934_idmodeloformulario);
            rlb.Dispose();
        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar categoría/colectivo", ex.Message);
        }
    }

}