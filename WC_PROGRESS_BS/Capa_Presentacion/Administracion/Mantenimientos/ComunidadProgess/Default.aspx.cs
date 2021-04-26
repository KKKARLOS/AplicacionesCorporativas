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

public partial class Capa_Presentacion_Administracion_Mantenimientos_ComunidadProgess_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<IB.Progress.Models.Personas> lisProfesionales1 = null;
        List<IB.Progress.Models.Personas> lisProfesionales2 = null;
        List<IB.Progress.Models.CR> lisCR1 = null;
        List<IB.Progress.Models.CR> lisCR2 = null;
        List<IB.Progress.Models.Empresas> lisEmpresas1 = null;
        List<IB.Progress.Models.Empresas> lisEmpresas2 = null;
        List<IB.Progress.Models.Personas> lisProfesionalesforzados1 = null;
        List<IB.Progress.Models.Personas> lisProfesionalesforzados2 = null;


        IB.Progress.Models.ComunidadProgress lComunidadProgress = null;

        IB.Progress.BLL.ComunidadProgress bllComunidadProgress = null;

        bool bCont = true;
        try
        {
            bllComunidadProgress = new IB.Progress.BLL.ComunidadProgress();
            lComunidadProgress = bllComunidadProgress.catalogo();
            bllComunidadProgress.Dispose();


            
            //Cargar las tablas
            //LinQ de profesionales
            lisProfesionales1 = (from comProgress in lComunidadProgress.SelectPersonas
                                 select comProgress).ToList<IB.Progress.Models.Personas>();

            lisProfesionales2 = (from comProgress in lComunidadProgress.SelectPersonas2
                                 select comProgress).ToList<IB.Progress.Models.Personas>();


            //LinQ de CR's
            lisCR1 = (from comProgress in lComunidadProgress.SelectCR
                      select comProgress).ToList<IB.Progress.Models.CR>();


            lisCR2 = (from comProgress in lComunidadProgress.SelectCR2
                      select comProgress).ToList<IB.Progress.Models.CR>();


            //LinQ de Empresas
            lisEmpresas1 = (from comProgress in lComunidadProgress.SelectEmpresas
                            select comProgress).ToList<IB.Progress.Models.Empresas>();

            lisEmpresas2 = (from comProgress in lComunidadProgress.SelectEmpresas2
                      select comProgress).ToList<IB.Progress.Models.Empresas>();


            //LinQ de profesionales forzados
            lisProfesionalesforzados1 = (from comProgress in lComunidadProgress.SelectPersonasForzadas
                                         select comProgress).ToList<IB.Progress.Models.Personas>();

            lisProfesionalesforzados2 = (from comProgress in lComunidadProgress.SelectPersonasForzadas2
                                         select comProgress).ToList<IB.Progress.Models.Personas>();

            
        }
        catch (IB.Progress.Shared.IBException ibex)
        {
            if (bllComunidadProgress != null) bllComunidadProgress.Dispose();
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
            Smtp.SendSMTP("Error al cargar el catálogo de comunidad progress", " Comunidad progress");
        }
        catch (Exception ex)
        {
            if (bllComunidadProgress != null) bllComunidadProgress.Dispose();
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
                foreach (IB.Progress.Models.Personas r in lisProfesionales1)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());
                    ulProfesionales1.Controls.Add(listItem);
                }

                //Lista de profesionales derecha
                foreach (IB.Progress.Models.Personas r in lisProfesionales2)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());
                    ulProfesionales2.Controls.Add(listItem);
                }

                //Lista de CR's izquierda
                foreach (IB.Progress.Models.CR r in lisCR1)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.T303_denominacion;
                    listItem.Attributes.Add("value", r.T303_idnodo.ToString());
                    ulCR1.Controls.Add(listItem);
                }


                //Lista de CR's derecha
                foreach (IB.Progress.Models.CR r in lisCR2)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.T303_denominacion;
                    listItem.Attributes.Add("value", r.T303_idnodo.ToString());
                    ulCR2.Controls.Add(listItem);
                }

                //Lista de empresas izquierda
                foreach (IB.Progress.Models.Empresas r in lisEmpresas1)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.T313_denominacion;
                    listItem.Attributes.Add("value", r.T313_idempresa.ToString());
                    UlEmpresas1.Controls.Add(listItem);
                }

                //Lista de empresas derecha
                foreach (IB.Progress.Models.Empresas r in lisEmpresas2)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.T313_denominacion;
                    listItem.Attributes.Add("value", r.T313_idempresa.ToString());
                    UlEmpresas2.Controls.Add(listItem);
                }

                //Lista de profesionales forzados izquierda
                foreach (IB.Progress.Models.Personas r in lisProfesionalesforzados1)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());
                    ulForzados1.Controls.Add(listItem);
                }

                //Lista de profesionales forzados izquierda
                foreach (IB.Progress.Models.Personas r in lisProfesionalesforzados2)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.Profesional;
                    listItem.Attributes.Add("value", r.T001_idficepi.ToString());
                    ulForzados2.Controls.Add(listItem);
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
    public static void update(int contenedor, List<short> lista)
    {
        IB.Progress.BLL.ComunidadProgress cComunidad = null;
        try
        {
            cComunidad = new IB.Progress.BLL.ComunidadProgress();

            cComunidad.Update(contenedor, lista);
            cComunidad.Dispose();
        }
        catch (Exception ex)
        {
            if (cComunidad != null) cComunidad.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar comunidad Progress", ex.Message);
        }
    }
}