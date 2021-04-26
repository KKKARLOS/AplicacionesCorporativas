using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_CabeceraMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //spnUsuario.InnerText = ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre;
        //spnUsuario.Style.Add(HtmlTextWriterStyle.MarginRight, "10px");
 
        crearMenu(SiteMap.RootNode, -1, ulPrincipal);

        if (SiteMap.CurrentNode != null) {
            if (!accesso(SiteMap.CurrentNode))
            {
                Response.Redirect("~/NoAutorizado.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        this.lblSession.InnerText = "La sesión caducará en " + Session.Timeout.ToString() + " min.";

        IB.Progress.Models.Profesional oProfEntrada = (IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL_ENTRADA"];
        IB.Progress.Models.Profesional oProf = (IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"];


        if (oProfEntrada.t001_idficepi != oProf.t001_idficepi)
        {
            lblReconectado.Visible = true;
            lblReconectado.InnerText = "Reconectado como: " + oProf.nombrelargo;
        }

    }

    private void crearMenu(SiteMapNode stMp, int level, HtmlGenericControl parentControl)
    {
        if (level == -1)
        {
            foreach (SiteMapNode smN in stMp.ChildNodes) {
                if (accesso(smN))
                    crearMenu(smN, 0, parentControl);                                  
            }
        }
        else if (stMp.Title != "Home")//Cuando se cumplen ciertas circunstancias y no queremos pintar alguna opción
        {
            HtmlGenericControl lisItem = new HtmlGenericControl("li");
            lisItem.Attributes.Add("data-name", stMp.Title);

            HtmlAnchor enlace = new HtmlAnchor();
            HtmlGenericControl spanText = new HtmlGenericControl("span");
            enlace.HRef = stMp.Url;
            spanText.InnerText = stMp.Title;
            string lisItemClass = "dropdown-submenu";
            if (stMp.ResourceKey != null)//Se usa este campo "ResourceKey" para pintar icono 
            {
                enlace.Attributes.Add("class", "dropdown-toggle");
                enlace.Attributes.Add("data-toggle", "dropdown");
                
                //enlace.Attributes.Add("data-name", stMp.Title);

                //todo revisar
                enlace.HRef = "#";
                HtmlGenericControl span = new HtmlGenericControl("span");
                span.Attributes.Add("class", stMp.ResourceKey);
                enlace.Controls.Add(span);
                lisItemClass = "dropdown";
            }
            enlace.Controls.Add(spanText);

            if (stMp.HasChildNodes)
            {
                lisItem.Attributes.Add("class", lisItemClass);
                if (level == 0)
                {
                    HtmlGenericControl b = new HtmlGenericControl("b");
                    b.Attributes.Add("class", "caret");
                    enlace.Controls.Add(b);
                }
                HtmlGenericControl lisSubItem = new HtmlGenericControl("ul");
                lisSubItem.Attributes.Add("class", "dropdown-menu");
                foreach (SiteMapNode stMpChild in stMp.ChildNodes)
                {
                    if (accesso(stMpChild))
                        crearMenu(stMpChild, level + 1, lisSubItem);
                }
                lisItem.Controls.Add(lisSubItem);
            }
            lisItem.Controls.AddAt(0, enlace);
            parentControl.Controls.Add(lisItem);
            if ((stMp.Title == "Evaluaciones de mi equipo" || stMp.Title == "Confirmar mi equipo" || stMp.Title == "Gestionar entradas a mi equipo" || stMp.Title == "Gestionar solicitudes de cambio de rol" || stMp.Title == "Completar evaluaciones abiertas")  && Roles.GetRolesForUser().Contains("PEVA")) //Si es evaluador, en estas opciones se pinta la raya inferior(divider)
            {
                HtmlGenericControl divider = new HtmlGenericControl("div");
                divider.Attributes.Add("class", "divider");
                parentControl.Controls.Add(divider);
            }

            if ((stMp.Title == "Gestionar cambio de responsable" || stMp.Title == "Mantenimiento de maestros"))
            {
                HtmlGenericControl divider = new HtmlGenericControl("div");
                divider.Attributes.Add("class", "divider");
                parentControl.Controls.Add(divider);
            }

            if ((stMp.Title == "Competencias y roles"))
            {
                HtmlGenericControl divider = new HtmlGenericControl("div");
                divider.Attributes.Add("class", "divider");
                parentControl.Controls.Add(divider);
            }

            if ((stMp.Title == "Reconexión"))
            {
                HtmlGenericControl divider = new HtmlGenericControl("div");
                divider.Attributes.Add("class", "divider");
                parentControl.Controls.Add(divider);
            }

        }
    }

    private bool accesso(SiteMapNode smn) {
        if (Session["ACCESO"] == null) return false;
        
        foreach (string rol in Roles.GetRolesForUser()) {
            if (smn.Roles.Contains(rol) || smn.Roles.Contains("*") )
                return true;
        }
        return false;
    }
}