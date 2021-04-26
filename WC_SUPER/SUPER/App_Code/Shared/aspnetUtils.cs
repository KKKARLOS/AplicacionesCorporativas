using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace IB.SUPER.Shared
{
    public class aspnetUtils
    {
        public aspnetUtils()
        {

        }

        public static void visualizarGuia(UserControl Menu)
        {
            try
            {
                HtmlGenericControl lbl = new HtmlGenericControl("label");
                lbl = (HtmlGenericControl)Menu.FindControl("guia");
                lbl.Visible = true;

                HtmlGenericControl lbl2 = new HtmlGenericControl("label");
                lbl2 = (HtmlGenericControl)Menu.FindControl("guiaxs");
                lbl2.Visible = true;
            }
            catch (Exception ex)
            {

                LogError.LogearError("Error al visualizar la guía", ex);
                throw new Exception(System.Uri.EscapeDataString("Error al visualizar la guía"));
            }
        }
    }
}