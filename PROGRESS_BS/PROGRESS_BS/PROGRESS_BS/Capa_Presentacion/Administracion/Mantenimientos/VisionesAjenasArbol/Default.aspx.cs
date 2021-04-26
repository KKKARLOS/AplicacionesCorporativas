using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Mantenimientos_VisionesAjenasArbol_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.VisionesAjenasArbol> catalogoVisionesAjenasArbol()
    {
        IB.Progress.BLL.VisionesAjenasArbol dVisionesAjenasArbol = null;
        try
        {
            List<IB.Progress.Models.VisionesAjenasArbol> visionesAjenasArbol = null;
            dVisionesAjenasArbol = new IB.Progress.BLL.VisionesAjenasArbol();

            visionesAjenasArbol = dVisionesAjenasArbol.catalogoVisionesAjenasArbol("A", null);
            dVisionesAjenasArbol.Dispose();

            return visionesAjenasArbol;
        }
        catch (Exception EX)
        {
            if (dVisionesAjenasArbol != null) dVisionesAjenasArbol.Dispose();
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void deleteVisualizador(int idficepiVisualizador)
    {
        try
        {            
            IB.Progress.BLL.VisionesAjenasArbol cVision = new IB.Progress.BLL.VisionesAjenasArbol();


            cVision.DeleteVisualizador(idficepiVisualizador);
            cVision.Dispose();

        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar los perfiles", ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadores(string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> evaluadores = null;
            pro = new IB.Progress.BLL.Profesional();
            evaluadores = pro.getFicProfesionales(t001_apellido1, t001_apellido2, t001_nombre);
            pro.Dispose();
            return evaluadores;
        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener evaluadores", ex.Message);
            throw;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getProfesionales(int t001_idficepi_visualizador)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> profesionales = null;
            pro = new IB.Progress.BLL.Profesional();
            profesionales = pro.getProfesionalesVisualizadores(t001_idficepi_visualizador);
            pro.Dispose();

            return profesionales;
        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener profesionales", ex.Message);
            throw;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GrabarDatos(List<IB.Progress.Models.VisionesAjenasArbol> oVisualizadores)
    {
        IB.Progress.BLL.VisionesAjenasArbol bllvision = null;
        try
        {
            bllvision = new IB.Progress.BLL.VisionesAjenasArbol();
            bllvision.MantenerCatalogo("A",oVisualizadores);
            bllvision.Dispose();
        }
        catch (Exception ex)
        {
            if (bllvision != null) bllvision.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al insertar visualizadores", ex.Message);
            throw;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.VisionesAjenasArbol> selVisualizados(int t001_idficepi_visualizador)
    {
        IB.Progress.BLL.VisionesAjenasArbol dVisionesAjenasArbol = null;
        try
        {
            List<IB.Progress.Models.VisionesAjenasArbol> visionesAjenasArbol = null;
            dVisionesAjenasArbol = new IB.Progress.BLL.VisionesAjenasArbol();

            visionesAjenasArbol = dVisionesAjenasArbol.catalogoVisionesAjenasArbol("A", t001_idficepi_visualizador);
            dVisionesAjenasArbol.Dispose();

            return visionesAjenasArbol;
        }
        catch (Exception EX)
        {
            if (dVisionesAjenasArbol != null) dVisionesAjenasArbol.Dispose();
            throw;
        }
    }


}