using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_MiEquipo_ArbolDependencias_Default : System.Web.UI.Page
{
    public string idficepi;
    public string nombre;    
    public string origen;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                      
            idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
            nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";
            origen = "var origen ='" + Request.QueryString["origen"].ToString() + "';";
        }
        catch (Exception)
        {
            //throw new Exception("No se ha podido cargar el árbol de dependencias");
            PieMenu.sErrores = "msgerr = 'No se ha podido cargar el árbol de dependencias.';";
        }
    }

    /// <summary>
    /// Muestra el catálogo de dependencias
    /// </summary>
    /// <param name="idficepi"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.ArbolDependencias> catArbolDependencias(int idficepi)
    {
        IB.Progress.BLL.ArbolDependencias aDependencias = null;
        try
        {
            List<IB.Progress.Models.ArbolDependencias> arbolDependencias = null;
            aDependencias = new IB.Progress.BLL.ArbolDependencias();

            arbolDependencias = aDependencias.catalogoArbolDependencias(idficepi);
            aDependencias.Dispose();

            return arbolDependencias;
        }
        catch (Exception ex)
        {
            if (aDependencias != null) aDependencias.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener el catálogo de dependencias.", ex.Message);
            throw ex;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.ArbolDependencias> SelectPotencial(int idficepi)
    {
        IB.Progress.BLL.ArbolDependencias aDependencias = null;
        try
        {
            List<IB.Progress.Models.ArbolDependencias> arbolDependencias = null;
            aDependencias = new IB.Progress.BLL.ArbolDependencias();

            arbolDependencias = aDependencias.SelectPotencialidad(idficepi);
            aDependencias.Dispose();

            return arbolDependencias;
        }
        catch (Exception ex)
        {
            if (aDependencias != null) aDependencias.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al seleccionar al profesional potencial.", ex.Message);
            throw ex;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.ArbolDependencias> SelectYO(int idficepi)
    {
        IB.Progress.BLL.ArbolDependencias aDependencias = null;
        try
        {
            List<IB.Progress.Models.ArbolDependencias> arbolDependencias = null;
            aDependencias = new IB.Progress.BLL.ArbolDependencias();

            arbolDependencias = aDependencias.SelectYO(idficepi);
            aDependencias.Dispose();

            return arbolDependencias;
        }
        catch (Exception ex)
        {
            if (aDependencias != null) aDependencias.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al seleccionar al profesional yo@enibermatica.", ex.Message);
            throw ex;
        }
    }


    /// <summary>
    /// Obtiene los profesionales evaluadores o que figuren como evaluador en alguna valoración
    /// </summary>
    /// <param name="t001_apellido1"></param>
    /// <param name="t001_apellido2"></param>
    /// <param name="t001_nombre"></param>
    /// <param name="administrador"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadores(string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> evaluadores = null;
            pro = new IB.Progress.BLL.Profesional();
            evaluadores = pro.getFic(t001_apellido1, t001_apellido2, t001_nombre);
            pro.Dispose();
            return evaluadores;
        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener los profesionales que figuran como evaluador en alguna valoración. (Árbol de dependencias)", ex.Message);
            throw ex;
        }
    }

    /// <summary>
    /// Busca entre los profesionales que dependen de un idficepi
    /// </summary>
    /// <param name="idficepi"></param>
    /// <param name="t001_apellido1"></param>
    /// <param name="t001_apellido2"></param>
    /// <param name="t001_nombre"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadoresArbol(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> evaluadores = null;
            pro = new IB.Progress.BLL.Profesional();
            evaluadores = pro.EvaluadoresArbol(idficepi, t001_apellido1, t001_apellido2, t001_nombre);
            pro.Dispose();
            return evaluadores;
        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener los profesionales que dependen de un ficepi. (Árbol de dependencias)", ex.Message);
            throw ex;
        }
    }



}