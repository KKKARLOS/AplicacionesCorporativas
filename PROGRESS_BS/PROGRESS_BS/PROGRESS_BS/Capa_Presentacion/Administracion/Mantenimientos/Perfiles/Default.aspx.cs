using IB.Progress.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Perfiles_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<IB.Progress.Models.PersonasPerfiles> lisProfesionales1 = null;
        List<IB.Progress.Models.PersonasPerfiles> lisProfesionales2 = null;
        List<IB.Progress.Models.PersonasPerfiles> lisProfesionales3 = null;
        List<IB.Progress.Models.PersonasPerfiles> lisProfesionales4 = null;
        List<IB.Progress.Models.PersonasPerfiles> lisProfesionales5 = null;
        
        IB.Progress.Models.Perfiles lPerfiles = null;

        IB.Progress.BLL.Perfiles bllPerfiles = null;

        bool bCont = true;
        try
        {

            bllPerfiles = new IB.Progress.BLL.Perfiles();
            lPerfiles = bllPerfiles.catalogo();
            bllPerfiles.Dispose();



            //Cargar las tablas
            //LinQ de profesionales
            lisProfesionales1 = (from comProgress in lPerfiles.SelectPersonas
                                 select comProgress).ToList<IB.Progress.Models.PersonasPerfiles>();

            lisProfesionales2 = (from comProgress in lPerfiles.SelectPersonas2
                                 where comProgress.T939_figura == "A"
                                 select comProgress).ToList<IB.Progress.Models.PersonasPerfiles>();

            lisProfesionales3 = (from comProgress in lPerfiles.SelectPersonas2
                                 where comProgress.T939_figura == "S"
                                 select comProgress).ToList<IB.Progress.Models.PersonasPerfiles>();


            lisProfesionales4 = (from comProgress in lPerfiles.SelectPersonas3
                                 select comProgress).ToList<IB.Progress.Models.PersonasPerfiles>();

            lisProfesionales5 = (from comProgress in lPerfiles.SelectPersonas4
                                 select comProgress).ToList<IB.Progress.Models.PersonasPerfiles>();

            

        }
        catch (IB.Progress.Shared.IBException ibex)
        {
            if (bllPerfiles != null) bllPerfiles.Dispose();
            bCont = false;

            string msgerr = "";
            switch (ibex.ErrorCode)
            {
                case 102:
                    msgerr = ibex.Message;
                    break;
            }
            PieMenu.sErrores = "msgerr = '" + msgerr + "';";
            //Avisar a EDA por smtp            
            Smtp.SendSMTP("Error al cargar el catálogo de perfiles", " Perfiles");
        }
        catch (Exception ex)
        {
            if (bllPerfiles != null) bllPerfiles.Dispose();
            bCont = false;
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script1", "msgerr = 'Ocurrió un error general en la aplicación.';", true);
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            Smtp.SendSMTP("Ha ocurrido un error general", "Error general en Roles/aprobadores");
        }

        if (bCont)
        {
            try
            {
                //Lista de profesionales izquierda
                foreach (IB.Progress.Models.PersonasPerfiles r in lisProfesionales1)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());
                    ulProfesionales1.Controls.Add(listItem);
                }

                foreach (IB.Progress.Models.PersonasPerfiles r in lisProfesionales2)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());
                    ulProfesionales2.Controls.Add(listItem);
                }

                foreach (IB.Progress.Models.PersonasPerfiles r in lisProfesionales3)
                {                    
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());

                    //Si el conectado es superadministrador, habilitamos los botones pertinentes
                    if (r.T001_idficepi.ToString() == ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString())
                    {                        
                        btnSuperAdministradores.Style.Add("display", "block");
                        btnContenedor3.Style.Add("visibility", "visible");
                    }
                   

                    ulProfesionales3.Controls.Add(listItem);
                }

                foreach (IB.Progress.Models.PersonasPerfiles r in lisProfesionales4)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());
                    ulProfesionales4.Controls.Add(listItem);
                }

                foreach (IB.Progress.Models.PersonasPerfiles r in lisProfesionales5)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());

                    //Si el conectado es superadministrador, habilitamos los botones pertinentes
                    if (r.T001_idficepi.ToString() == ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString())
                    {
                        btnSuperAdministradores.Style.Add("display", "block");
                        btnContenedor3.Style.Add("display", "block");
                    }
                  

                    ulProfesionales5.Controls.Add(listItem);
                }

              
            }
            catch (Exception)
            {
                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "msgerr = 'Ocurrió un error obteniendo los roles de base de datos';", true);
                PieMenu.sErrores = "msgerr = 'Ocurrió un error cargando las listas de los roles';";
            }
        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> listaRoles()
    {
        List<string> listaRoles = new List<string>();
        try
        {
            return (List<string>)(HttpContext.Current.Session["ROLES"]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void update(int contenedor, List<short> lista)
    {
        try
        {

            IB.Progress.BLL.ComunidadProgress cComunidad = new IB.Progress.BLL.ComunidadProgress();


            IB.Progress.BLL.Perfiles cPerfiles = new IB.Progress.BLL.Perfiles();

            cPerfiles.Update(contenedor, lista);
            cPerfiles.Dispose();

        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar los perfiles", ex.Message);
        }
    }
}