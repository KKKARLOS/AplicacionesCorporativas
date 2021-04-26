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
    public class BuscadorClientes : System.Web.Services.WebService
    {
        public BuscadorClientes()
        {
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.Cliente> obtenerClientes(string t302_denominacion, string sTipoBusqueda, bool bSoloActivos, bool bInternos)
        {
            BLL.Cliente Cliente = new BLL.Cliente();
            try
            {

                List<IB.SUPER.IAP30.Models.Cliente> lst = Cliente.Catalogo(t302_denominacion, sTipoBusqueda, bSoloActivos, bInternos);

                Cliente.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                Cliente.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de clientes", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de clientes"));
            }

        }
    }
}