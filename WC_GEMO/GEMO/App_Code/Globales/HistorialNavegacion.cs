using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace GEMO.BLL
{
    /// <summary>
    /// La clase HistorialNavegacion se utiliza para tener una relación de
    /// las páginas por las que ha navegado el usuario, de forma que pueda
    /// navegar hacia "atrás" con el botón de "Regresar".
    /// 
    /// Para ello se utiliza el objeto Stack, que es una especie de array en
    /// el que se van insertando urls y leyendo en orden inverso al insertado.
    /// El último dato que se inserta es el primero que se obtiene.
    /// </summary>
    /// 
    public class HistorialNavegacion
    {
        public static void Insertar(string strUrl)
        {
            if (HttpContext.Current.Session["Historial"] == null)
            {
                Stack stkHistorial = new Stack();
                stkHistorial.Push(strUrl);

                HttpContext.Current.Session["Historial"] = stkHistorial;
            }
            else
            {
                if (((Stack)HttpContext.Current.Session["Historial"]).Count == 0)
                {
                    ((Stack)HttpContext.Current.Session["Historial"]).Push(strUrl);
                }
                else
                {
                    if (strUrl != ((Stack)HttpContext.Current.Session["Historial"]).Peek().ToString())
                    {
                        ((Stack)HttpContext.Current.Session["Historial"]).Push(strUrl);
                    }
                }
            }
        }
        public static string Leer()
        {
            if (HttpContext.Current.Session["Historial"] == null)
            {
                return "~/Default.aspx";
            }
            else
            {
                //Se elimina la última opción, ya que esta es la página en la que
                //se encuentra el usuario, que ha registrado en el Page_Load.
                if (((Stack)HttpContext.Current.Session["Historial"]).Count > 1)
                {
                    ((Stack)HttpContext.Current.Session["Historial"]).Pop();
                }

                return ((Stack)HttpContext.Current.Session["Historial"]).Pop().ToString();
            }
        }

        public static int Contador()
        {
            if (HttpContext.Current.Session["Historial"] == null)
            {
                return 0;
            }
            else
            {
                return ((Stack)HttpContext.Current.Session["Historial"]).Count;
            }
        }
    }
}
