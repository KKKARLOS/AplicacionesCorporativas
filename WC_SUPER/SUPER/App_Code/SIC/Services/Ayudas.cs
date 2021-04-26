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
    public class Ayudas : System.Web.Services.WebService
    {

        public Ayudas()
        {
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.APP.Models.KeyValue2> Buscar(string tipo, string filtro, bool admin)
        {


            BLL.Ayudas.enumAyuda enumlst;


            switch (tipo.ToUpper())
            {
                case "SIC_AYUDA1UNIDADESPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1UNIDADESPREVENTA_CAT;
                    break;
                case "SIC_AYUDA1AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1AREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA1SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1SUBAREASPREVENTA_CAT;
                    break;


                case "SIC_AYUDA4UNIDADESPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA4UNIDADESPREVENTA_CAT;
                    break;
                case "SIC_AYUDA4AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA4AREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA4SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA4SUBAREASPREVENTA_CAT;
                    break;


                    

                case "SIC_AYUDA1TEMORIGEN_O_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_O_CAT;
                    break;
                case "SIC_AYUDA1TEMORIGEN_E_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_E_CAT;
                    break;
                case "SIC_AYUDA1TEMORIGEN_P_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_P_CAT;
                    break;
                case "SIC_AYUDA1TEMORIGEN_S_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_S_CAT;
                    break;

                case "SIC_AYUDA2UNIDADESPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2UNIDADESPREVENTA_CAT;
                    break;
                case "SIC_AYUDA2AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2AREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA2SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2SUBAREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA2TEMORIGEN_O_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_O_CAT;
                    break;
                case "SIC_AYUDA2TEMORIGEN_E_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_E_CAT;
                    break;
                case "SIC_AYUDA2TEMORIGEN_P_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_P_CAT;
                    break;
                case "SIC_AYUDA2TEMORIGEN_S_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_S_CAT;
                    break;

                case "SIC_AYUDA3UNIDADESPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3UNIDADESPREVENTA_CAT;
                    break;
                case "SIC_AYUDA3AREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3AREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA3SUBAREASPREVENTA_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3SUBAREASPREVENTA_CAT;
                    break;
                case "SIC_AYUDA3ITEMORIGEN_O_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3ITEMORIGEN_O_CAT;
                    break;
                case "SIC_AYUDA3ITEMORIGEN_E_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3ITEMORIGEN_E_CAT;
                    break;
                case "SIC_AYUDA3ITEMORIGEN_P_CAT":
                    enumlst = BLL.Ayudas.enumAyuda.SIC_AYUDA3ITEMORIGEN_P_CAT;
                    break;

                case "CUENTASCRM":
                    enumlst = BLL.Ayudas.enumAyuda.cuentasCRM;
                    break;
                case "ACCIONESPREVENTA":
                    enumlst = BLL.Ayudas.enumAyuda.accionesPreventa;
                    break;

                default:
                    throw new Exception("Tipo de ayuda no soportado.");

            }

            BLL.Ayudas cAyudas = null;
            try
            {
                cAyudas = new BLL.Ayudas();

                List<IB.SUPER.APP.Models.KeyValue2> lst = cAyudas.Buscar(enumlst, filtro, admin);

                cAyudas.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAyudas.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la ayuda [" + tipo + "]", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la ayuda [" + tipo + "]"));
            }


        }

       
    }
}