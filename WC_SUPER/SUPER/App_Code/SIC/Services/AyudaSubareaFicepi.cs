using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using IB.SUPER.Shared;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;

namespace IB.SUPER.Services.SIC
{

    [WebService(Namespace = "http://SUPER.ibermatica.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class AyudaSubareaFicepi : System.Web.Services.WebService
    {

        public AyudaSubareaFicepi()
        {
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.KeyValue> ObtenerAreasDeUnidad(string tipo, int ta199_idunidadpreventa, bool admin)
        {
            BLL.Ayudas.enumAyuda enumlst;

            switch (tipo.ToUpper())
            {
                case "SIC_AYUDA1AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1AREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA2AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2AREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA3AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3AREASPREVENTA_CAT;
                    break;

                case "SIC_AYUDA4AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA4AREASPREVENTA_CAT;
                    break;
                default:
                    throw new Exception("Tipo de ayuda no soportado.");

            }

            BLL.SubareaPreventa cSA = new BLL.SubareaPreventa();

            try
            {
                return cSA.ObtenerAreasDeUnidad(enumlst, ta199_idunidadpreventa, admin);
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo la lista de áreas", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de áreas"));
            }
            finally
            {
                cSA.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.KeyValue> ObtenerSubareasDeArea(string tipo, int ta200_idareapreventa, bool admin)
        {
            BLL.Ayudas.enumAyuda enumlst;

            switch (tipo.ToUpper())
            {
                case "SIC_AYUDA1SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1SUBAREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA2SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2SUBAREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA3SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3SUBAREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA4SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA4SUBAREASPREVENTA_CAT;
                    break;

                default:
                    throw new Exception("Tipo de ayuda no soportado.");

            }

            BLL.SubareaPreventa cSA = new BLL.SubareaPreventa();

            try
            {
                return cSA.ObtenerSubareasDeArea(enumlst, ta200_idareapreventa, admin);
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo la lista de subáreas", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de subáreas"));
            }
            finally
            {
                cSA.Dispose();
            }
        }

    }
}