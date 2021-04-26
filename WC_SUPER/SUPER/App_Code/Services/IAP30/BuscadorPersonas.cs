using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using IB.SUPER.Shared;
using IB.SUPER.IAP30.Models;

namespace IB.SUPER.Services.IAP30
{
    [WebService(Namespace = "http://SUPER.ibermatica.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class BuscadorPersonas
    {
        public BuscadorPersonas()
        {
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.ProfesionalIap> ObtenerProfesionales(string tipoBusqueda, String nombre, String apellido1, String apellido2, Boolean bajas, String nodo)
        {
            /*switch (tipo.ToLower())
            {
                case "buscador":
                    return oProfesionalesIAP.Catalogo;
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
            }*/


            IB.SUPER.IAP30.BLL.ProfesionalIap oProfesionalesIAP = null;
            try
            {
                oProfesionalesIAP = new IB.SUPER.IAP30.BLL.ProfesionalIap();

                List<IB.SUPER.IAP30.Models.ProfesionalIap> lst = oProfesionalesIAP.Catalogo(tipoBusqueda.ToUpper(), nombre, apellido1, apellido2, bajas, nodo);

                return lst;
            }
            catch (Exception ex)
            {

                LogError.LogearError("Ocurrió un error en la búsqueda de  [" + tipoBusqueda + "]", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error en la búsqueda de [" + tipoBusqueda + "]"));
            }
            finally
            {
                oProfesionalesIAP.Dispose();
            }


        }
    }
}