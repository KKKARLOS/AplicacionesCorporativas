using SUPER.Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_bsUserControls_Menu_Menu : System.Web.UI.UserControl
{
    //public string imgFotoBase64;
    private bool bEsMiembro = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DIAMANTE"] != null)
        {
            //Imagen logo (estática o fija según pantalla de configuración
            if ((bool)Session["DIAMANTE"])
            {
                imgDiamante.Src = Session["strServer"] + "Capa_Presentacion/bsImages/imgDiamante.gif";
            }
            else
                imgDiamante.Src = Session["strServer"] + "Capa_Presentacion/bsImages/imgDiamanteFijo.gif";
        }

        //Texto usuario y reconexión
        if (Session["DES_EMPLEADO_ENTRADA"] != null)
        {
            this.textoUsuario.InnerText = Session["DES_EMPLEADO_ENTRADA"].ToString();
            if (Session["DES_EMPLEADO"].ToString() != "")
            {
                if (Session["DES_EMPLEADO"].ToString() != Session["DES_EMPLEADO_ENTRADA"].ToString())
                {
                    if (Session["SEXO_ENTRADA"].ToString() == "V")
                        this.textoUsuarioReconectado.InnerText =  Session["DES_EMPLEADO"].ToString();
                    else
                        this.textoUsuarioReconectado.InnerText =  Session["DES_EMPLEADO"].ToString();
                }
            }
        }
        else
            this.textoUsuario.InnerText= "";


        crearMenu(SiteMap.RootNode, -1, ulPrincipal);

        if (SiteMap.CurrentNode != null)
        {

        }

    }

    private void crearMenu(SiteMapNode stMp, int level, HtmlGenericControl parentControl)
    {
        if (level == -1)
        {
            foreach (SiteMapNode smN in stMp.ChildNodes)
            {
                if (accesso(smN))
                    crearMenu(smN, 0, parentControl);
                    
            }
        }
        else
        {

            if (stMp["divider"] == null)
            {
                HtmlGenericControl lisItem = new HtmlGenericControl("li");
                lisItem.Attributes.Add("accesoHome", stMp["accesoHome"]);
                lisItem.Attributes.Add("data-parent", stMp["data-parent"]);
                lisItem.Attributes.Add("id", stMp["id"]);
                HtmlAnchor enlace = new HtmlAnchor();
                HtmlGenericControl spanText = new HtmlGenericControl("span");
                enlace.HRef = stMp.Url;
                spanText.InnerText = stMp.Title;

                if (stMp.ResourceKey != null)//Se usa este campo "ResourceKey" para pintar icono 
                {
                    HtmlGenericControl span = new HtmlGenericControl("span");
                }
                enlace.Controls.Add(spanText);


                if (stMp.HasChildNodes)
                {
                    HtmlGenericControl lisSubItem = new HtmlGenericControl("ul");
                    foreach (SiteMapNode stMpChild in stMp.ChildNodes)
                    {
                        if (accesso(stMpChild))
                            crearMenu(stMpChild, level + 1, lisSubItem);
                    }
                    lisItem.Controls.Add(lisSubItem);
                }
               
                
                lisItem.Controls.AddAt(0, enlace);
                parentControl.Controls.Add(lisItem);
                
                 
            }
            else
            {
                HtmlGenericControl divider = new HtmlGenericControl("div");
                divider.Attributes.Add("class", "divider");
                parentControl.Controls.Add(divider);
            }
        }
    }

    private bool accesso(SiteMapNode smn)
    {       
        foreach (string rol in Roles.GetRolesForUser())
        {
            //Comprobamos que no ha sido deshabilitada ninguna opción desde la administración (IAP, PGE, PST, ADP)
            if (
                  (smn.Title == "PGE" && !Utilidades.EsModuloAccesible("PGE"))
                  || (smn.Title == "PST" && !Utilidades.EsModuloAccesible("PST"))
                  || (smn.Title == "IAP" && !Utilidades.EsModuloAccesible("IAP"))
                  || (smn.Title == "ADP" && !Utilidades.EsModuloAccesible("ADP"))
                  )//Si el acceso a los módulos está cortado, no se muestran...
            {
                //if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "A")
                if (!SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                {// a menos que el usuario sea administrador.
                    bEsMiembro = false;
                    break;                    
                }
            }

            if (smn.Roles.Contains(rol) || smn.Roles.Contains("*") || bEsMiembro )
                return true;
        }
        return false;
    }
}