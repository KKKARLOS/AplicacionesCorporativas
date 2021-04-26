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
    public class BuscadorCatalogoBasico : System.Web.Services.WebService
    {
        public BuscadorCatalogoBasico()
        {
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.Horizontal> obtenerHorizontales()
        {
            BLL.Horizontal HorizontalBLL = new BLL.Horizontal();

            try
            {

                List<IB.SUPER.IAP30.Models.Horizontal> lHorizontal = null;

                lHorizontal = HorizontalBLL.Catalogo();
                return lHorizontal;

            }
            catch (Exception ex)
            {

                LogError.LogearError("Error al obtener los horizontales.", ex);
                throw new Exception(System.Uri.EscapeDataString("Error al obtener los horizontales."));

            }
            finally
            {

                HorizontalBLL.Dispose();

            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.Cualificador> obtenerCualificadores(string sTipo, Int32 t303_idnodo)
        {
            BLL.Cualificador CualificadorBLL = new BLL.Cualificador();

            try
            {

                List<IB.SUPER.IAP30.Models.Cualificador> lCualificador = null;

                lCualificador = CualificadorBLL.Catalogo(sTipo, t303_idnodo);
                return lCualificador;

            }
            catch (Exception ex)
            {

                LogError.LogearError("Error al obtener los cualificadores.", ex);
                throw new Exception(System.Uri.EscapeDataString("Error al obtener los cualificadores."));

            }
            finally
            {

                CualificadorBLL.Dispose();

            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.NodoBasico> obtenerCRsAdmin()
        {
            IB.SUPER.APP.BLL.Nodo NodolBLL = new IB.SUPER.APP.BLL.Nodo();
            try
            {
                List<IB.SUPER.APP.Models.NodoBasico> lNodo = null;

                lNodo = NodolBLL.Catalogo();
                return lNodo;
            }
            catch (Exception ex)
            {
                LogError.LogearError("Error al obtener los CRs para administrador.", ex);
                throw new Exception(System.Uri.EscapeDataString("Error al obtener los CRs para administrador."));
            }
            finally
            {
                NodolBLL.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.NodoBasico> obtenerCRsNoHermes()
        {
            IB.SUPER.APP.BLL.Nodo NodolBLL = new IB.SUPER.APP.BLL.Nodo();
            try
            {
                List<IB.SUPER.APP.Models.NodoBasico> lNodo = null;

                lNodo = NodolBLL.CatalogoNoHermes();
                return lNodo;
            }
            catch (Exception ex)
            {
                LogError.LogearError("Error al obtener los CRs No-Hermes.", ex);
                throw new Exception(System.Uri.EscapeDataString("Error al obtener los CRs No-Hermes."));
            }
            finally
            {
                NodolBLL.Dispose();
            }
        }

        /// <summary>
        /// Obtiene los Proyectos económicos y técnicos accesibles desde la consulta de bitácora de IAP
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.NodoBasico> PtBitacora(int t305_idproyectosubnodo)
        {

            BLL.Bitacora cProyecto = new BLL.Bitacora();
            try
            {
                return cProyecto.ProyectosTecnicos((int)HttpContext.Current.Session["UsuarioActual"], t305_idproyectosubnodo);
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo la lista de proyectos técnicos", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de proyectos técnicos"));
            }
            finally
            {
                cProyecto.Dispose();
            }
        }

    }
}