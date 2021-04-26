using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_MiEquipo_DesgloseRol_Default : System.Web.UI.Page
{

    public string idficepi;
    public string nombre;
    public string sexo;
    //public string administrador;
    public string origen;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            //Usuario Administrador
            //if (Request.QueryString["origen"] == "ADM")
            //{
            //    administrador = "true";
            //}
            ////Usuario
            //else
            //{
            //    administrador = "false";
            //}

            //Registramos el idficepi para poder usarlo en Javascript. 
            //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptAdministrador", "var bAdmin = " + administrador + ";", true);

            //administrador = "var administrador = " + administrador + "";
            idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
            nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";
            sexo = "var sexo ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).Sexo.ToString() + "';";
            origen = "var origen ='" + Request.QueryString["origen"].ToString() + "';";
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.DesgloseRol> catalogoDesgloseRol(int idficepi, int parentesco)
    {
        IB.Progress.BLL.DesgloseRol dRol = null;
        try
        {
            List<IB.Progress.Models.DesgloseRol> desgloseRol = null;
            dRol = new IB.Progress.BLL.DesgloseRol();

            desgloseRol = dRol.catalogoDesgloseRol(idficepi, parentesco);            
            dRol.Dispose();

            return desgloseRol;
        }
        catch (Exception EX)
        {
            if (dRol != null) dRol.Dispose();
            throw;
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
        List<IB.Progress.Models.Profesional> evaluadores = null;
        IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
        evaluadores = pro.getFic(t001_apellido1, t001_apellido2, t001_nombre);
        pro.Dispose();
        return evaluadores;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idficepi"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadoresDescendientes(int idficepi)
    {
        List<IB.Progress.Models.Profesional> evaluadores = null;
        IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();

        evaluadores = pro.getEvaluadoresDescendientes(idficepi);
        pro.Dispose();
        return evaluadores;
    }
}