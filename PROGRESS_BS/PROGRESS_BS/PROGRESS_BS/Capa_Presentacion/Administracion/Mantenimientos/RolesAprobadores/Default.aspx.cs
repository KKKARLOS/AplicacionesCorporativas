using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using IB.Progress.Shared;

public partial class Capa_Presentacion_Administracion_Mantenimientos_RolesAprobadores_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        List<IB.Progress.Models.ROLIB> lisRolNoAprob = null;
        List<IB.Progress.Models.ROLIB> lisRolAprob = null;
        IB.Progress.BLL.ROLIB rlb = null;

        bool bCont = true;
        try
        {
            rlb = new IB.Progress.BLL.ROLIB();
            List<IB.Progress.Models.ROLIB> lRolib = rlb.Catalogo();
            rlb.Dispose();

            //Cargar las tablas
            //LinQ para separar los aprobadores de los NO aprobadores
            lisRolNoAprob = (from rolib in lRolib 
                             where rolib.t004_aprobador == false 
                             select rolib).ToList<IB.Progress.Models.ROLIB>();

            lisRolAprob = (from rolib in lRolib 
                           where rolib.t004_aprobador == true 
                           select rolib).ToList<IB.Progress.Models.ROLIB>();
        }
        catch (IB.Progress.Shared.IBException ibex)
        {
            if (rlb != null) rlb.Dispose();
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
            Smtp.SendSMTP("Error al cargar el catálogo de roles", " Roles/aprobadores");
        }
        catch (Exception ex)
        {
            if (rlb != null) rlb.Dispose();
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
                //Cargamos la lista de no aprobadores
                foreach (IB.Progress.Models.ROLIB r in lisRolNoAprob)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.t004_desrol;
                    listItem.Attributes.Add("value", r.t004_idrol.ToString());
                    lisNoAprob.Controls.Add(listItem);
                }

                //Cargamos la lista de aprobadores
                foreach (IB.Progress.Models.ROLIB r in lisRolAprob)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "list-group-item");
                    listItem.InnerText = r.t004_desrol;
                    listItem.Attributes.Add("value", r.t004_idrol.ToString());
                    lisAprob.Controls.Add(listItem);
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
    public static void updatearRoles(List<short> listaRoles)
    {
        try
        {
            IB.Progress.BLL.ROLIB rlb = new IB.Progress.BLL.ROLIB();
            rlb.Update(listaRoles);
            rlb.Dispose();
            
            //Correo.Enviar("Correo Progress", ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombre.ToString() + " ha actualizado los roles correctamente", ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Correo.ToString());
                        
        }
        catch (Exception ex)
        {            
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar los roles", ex.Message);
        }
    }
}