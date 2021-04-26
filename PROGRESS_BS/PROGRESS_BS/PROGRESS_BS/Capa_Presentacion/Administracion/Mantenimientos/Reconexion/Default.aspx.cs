using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL = IB.Progress.BLL;
using Models = IB.Progress.Models;

public partial class Capa_Presentacion_Administracion_Reconexion_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getProfesionales(string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> evaluadores = null;
            pro = new IB.Progress.BLL.Profesional();

            IB.Progress.Models.Profesional oProf = (IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL_ENTRADA"];
            IB.Progress.Models.Profesional oProfEnCurso = (IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"];

            evaluadores = pro.getFicProfesionales_Reconexion(oProf.t001_idficepi, oProfEnCurso.t001_idficepi, t001_apellido1, t001_apellido2, t001_nombre);
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
    public static void establecerVariableSesion(string[] datos)
    {        

        //datos[0] = nombre
        //datos[1] = codred
        BLL.Profesional cProfesional = new BLL.Profesional();

        try
        {
            Models.Profesional oProfesional = cProfesional.Obtener(datos[1]);
            if (oProfesional.bIdentificado)
            {
                
                cProfesional.CargarRoles(oProfesional);

                //Dejo todo el objeto el la variable de sesión PROFESIONAL
                HttpContext.Current.Session["PROFESIONAL"] = oProfesional;

                
            }
            else
                throw new Exception("No se ha podido autenticar al usuario.");
            
        }
        catch (Exception ex)
        {            
            throw new Exception("Ocurrió un error autenticando al usuario");
        }
        finally
        {
            cProfesional.Dispose();
        }
    }
}