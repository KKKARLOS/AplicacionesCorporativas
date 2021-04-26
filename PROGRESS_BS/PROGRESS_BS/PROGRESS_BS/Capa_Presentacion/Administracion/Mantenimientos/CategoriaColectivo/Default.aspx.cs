using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Mantenimientos_CategoriaColectivo_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static IB.Progress.Models.CategoriaColectivo catalogo()
    {
        IB.Progress.BLL.CategoriaColectivo dCategoriaColectivo = null;
        
        try
        {
            dCategoriaColectivo = new IB.Progress.BLL.CategoriaColectivo();

            IB.Progress.Models.CategoriaColectivo valores = dCategoriaColectivo.catalogo();

            dCategoriaColectivo.Dispose();

            return valores;
        }
        catch (Exception ex)
        {
            if (dCategoriaColectivo != null) dCategoriaColectivo.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al cargar el catálogo de categoría/colectivo.", ex.Message);
            throw ex;
        }
    }


    [WebMethod]
    
    public static void update(int t935_idcategoriaprofesional, int t941_idcolectivo)
    {
        try
        {            
            IB.Progress.BLL.CategoriaColectivo rlb = new IB.Progress.BLL.CategoriaColectivo();
            rlb.Update(t935_idcategoriaprofesional, t941_idcolectivo);                     
            rlb.Dispose();                            
        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar categoría/colectivo", ex.Message);
        }
    }
}