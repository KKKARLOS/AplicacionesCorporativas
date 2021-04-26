using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using IB.SUPER.Shared;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;

namespace IB.SUPER.Services.IAP30
{
    [WebService(Namespace = "http://SUPER.ibermatica.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class BuscadorProyectos : System.Web.Services.WebService
    {
        public BuscadorProyectos()
        {
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.ProyectoNota> SeleccionarUnProyecto_IAP_GASVI()
        {
            BLL.ProyectoNota ProyectoNota = new BLL.ProyectoNota();
            try
            {

                List<IB.SUPER.IAP30.Models.ProyectoNota> lst = ProyectoNota.Catalogo((int)HttpContext.Current.Session["UsuarioActual"]);

                ProyectoNota.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                ProyectoNota.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de proyectos", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de proyectos"));
            }
            finally
            {
                ProyectoNota.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.ProyectoNota> Bitacora_IAP()
        {
            BLL.Bitacora Proyecto = new BLL.Bitacora();
            try
            {

                List<IB.SUPER.IAP30.Models.ProyectoNota> lst = Proyecto.Proyectos((int)HttpContext.Current.Session["UsuarioActual"]);

                Proyecto.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                Proyecto.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de proyectos (Bitacora_IAP)", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de proyectos (Bitacora_IAP)"));
            }
            finally
            {
                Proyecto.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.ProyectoNota> Bitacora_IAP_PT()
        {
            BLL.Bitacora Proyecto = new BLL.Bitacora();
            try
            {

                List<IB.SUPER.IAP30.Models.ProyectoNota> lst = Proyecto.ProyectosPT((int)HttpContext.Current.Session["UsuarioActual"]);

                Proyecto.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                Proyecto.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de proyectos (Bitacora_IAP_PT)", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de proyectos (Bitacora_IAP_PT)"));
            }
            finally
            {
                Proyecto.Dispose();
            }
        }

    }
}