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
    public class CRM : System.Web.Services.WebService
    {

        public CRM()
        {
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IB.SUPER.SIC.Models.ItemCRM ObtenerDatosCRM(int ta206_iditemorigen, string ta206_itemorigen)
        {

            BLL.SolicitudPreventa c = new BLL.SolicitudPreventa();

            try
            {
                return c.SelectOrigen(ta206_iditemorigen, ta206_itemorigen);
            }
            catch (Exception ex)
            {
                LogError.LogearError("Error al obtener la información del CRM", ex);
                throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener la información de cabecera del CRM."));
            }
            finally
            {
                c.Dispose();
            }

        }
    }
}   