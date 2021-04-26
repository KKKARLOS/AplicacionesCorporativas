using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using IB.SUPER.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.APP.Models;


namespace IB.SUPER.Services.SIC
{
    /// <summary>
    /// Descripción breve de Listas
    /// </summary>
    [WebService(Namespace = "http://SUPER.ibermatica.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class Listas : System.Web.Services.WebService
    {

        public Listas()
        {
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.KeyValue> ObtenerLista(string tipo, int? filtrarPor)
        {


            BLL.Listas.enumLista enumlst = 0;

            switch (tipo.ToLower())
            {
                case "unidad_preventa":
                    enumlst = BLL.Listas.enumLista.unidad_preventa;
                    break;
                case "area_preventa":
                    enumlst = BLL.Listas.enumLista.area_preventa;
                    break;
                case "subarea_preventa":
                    enumlst = BLL.Listas.enumLista.subarea_preventa;
                    break;
                case "tipoaccion_preventa":
                    enumlst = BLL.Listas.enumLista.tipoaccion_preventa;
                    break;
                case "tipodocumento_preventa":
                    enumlst = BLL.Listas.enumLista.tipodocumento_preventa;
                    break;

            }

            BLL.Listas cListas = null;
            try
            {
                cListas = new BLL.Listas();

                List<IB.SUPER.APP.Models.KeyValue> lst = cListas.GetList(enumlst, filtrarPor);

                cListas.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cListas.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista [" + tipo + "]", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista [" + tipo + "]"));
            }


        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.KeyValue> ObtenerListaEstructura(string tipo, int? filtrarPor, string origenMenu)
        {

            BLL.Listas.enumLista enumlst = 0;

            switch (tipo.ToLower())
            {
                case "unidad_preventa":
                    enumlst = BLL.Listas.enumLista.unidad_preventa;
                    break;
                case "area_preventa":
                    enumlst = BLL.Listas.enumLista.area_preventa;
                    break;
                case "subarea_preventa":
                    enumlst = BLL.Listas.enumLista.subarea_preventa;
                    break;
            }

            BLL.Listas cListas = null;
            try
            {
                cListas = new BLL.Listas();

                List<IB.SUPER.APP.Models.KeyValue> lst = cListas.GetListEstructura(enumlst, filtrarPor, origenMenu);

                cListas.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cListas.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de estructura [" + tipo + "]", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista  de estructura [" + tipo + "]"));
            }


        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.TipoAccionPreventa> ObtenerListaTipoAccion()
        {


            BLL.Listas cListas = null;
            try
            {
                cListas = new BLL.Listas();

                List<IB.SUPER.SIC.Models.TipoAccionPreventa> lst = cListas.GetListTipoAccionPreventa();

                cListas.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cListas.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista ObtenerListaTipoAccion", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista [ObtenerListaTipoAccion]"));
            }


        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.TipoAccionPreventa> ObtenerListaTipoAccionFiltrada(string itemorigen, int ta206_iditemorigen)
        {

            BLL.Listas.enumLista enumlst = 0;


            BLL.Listas cListas = null;
            try
            {
                cListas = new BLL.Listas();

                List<IB.SUPER.SIC.Models.TipoAccionPreventa> lst = cListas.GetListTipoAccionFiltrada(itemorigen, ta206_iditemorigen);

                cListas.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cListas.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista [TipoAccion_filtrada]", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista [TipoAccion_filtrada]"));
            }


        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.SubareaPreventa> ObtenerSubareas()
        {


            BLL.Listas cListas = null;
            try
            {
                cListas = new BLL.Listas();

                List<IB.SUPER.SIC.Models.SubareaPreventa> lst = cListas.GetListSubareas();

                cListas.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cListas.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista [ObtenerSubareas]", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista [ObtenerSubareas]"));
            }


        }

    }
}