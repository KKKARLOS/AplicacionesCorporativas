using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using IB.SUPER.Shared;

namespace IB.SUPER.Services.APP
{
    /// <summary>
    /// Descripción breve de Historial
    /// </summary>
    [WebService(Namespace = "http://SUPER.ibermatica.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class Historial : System.Web.Services.WebService
    {

        public Historial()
        {

        }

        /// <summary>
        /// Obtiene la última url de navegación almacenada en el historial
        /// </summary>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Leer(string urlActual)
        {
            try
            {
                return HistorialNavegacion.Leer(urlActual);
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo la última url del historial de navegación", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la última url del historial de navegación"));
            }
        }

        /// <summary>
        /// Reemplaza la última url almacenada en el historial por la pasada como parámetro.
        /// </summary>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Reemplazar(string newUrl)
        {
            try
            {
                HistorialNavegacion.Reemplazar(newUrl);
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error reemplazando la última url del historial de navegación", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error reemplazando la última url del historial de navegación"));
            }
        }
    }

}