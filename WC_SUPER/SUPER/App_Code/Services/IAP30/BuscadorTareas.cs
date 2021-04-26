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
    public class BuscadorTareas : System.Web.Services.WebService
    {

        public BuscadorTareas()
        {
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.BuscadorTareasBloque> tareasEnBloque(Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {

            BLL.BuscadorTareasBloque buscadorTareasBloque = new BLL.BuscadorTareasBloque();
            try
            {

                List<IB.SUPER.IAP30.Models.BuscadorTareasBloque> lst = buscadorTareasBloque.Catalogo((int)HttpContext.Current.Session["UsuarioActual"], Fechas.AnnomesAFecha((int)HttpContext.Current.Session["UMC_IAP"]), fechaInicio, fechaFin);

                return lst;
            }
            catch (Exception ex)
            {

                LogError.LogearError("Ocurrió un error obteniendo la lista de tareas en bloque", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de tareas en bloque"));
            }
            finally
            {
                buscadorTareasBloque.Dispose();
            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.BuscadorTareasBloque> tareasAgendaEnBloque(Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {

            BLL.BuscadorTareasBloque buscadorTareasBloque = new BLL.BuscadorTareasBloque();
            try
            {

                List<IB.SUPER.IAP30.Models.BuscadorTareasBloque> lst = buscadorTareasBloque.CatalogoAgenda((int)HttpContext.Current.Session["IDFICEPI_IAP"]);

                return lst;
            }
            catch (Exception ex)
            {

                LogError.LogearError("Ocurrió un error obteniendo la lista de tareas en bloque", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de tareas en bloque"));
            }
            finally
            {
                buscadorTareasBloque.Dispose();
            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.ConsumoIAPMasivaPSN> proyectosEconomicos(Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {

            BLL.ConsumoIAPMasivaPSN consumoIAPMasivaPSN = new BLL.ConsumoIAPMasivaPSN();
            try
            {

                List<IB.SUPER.IAP30.Models.ConsumoIAPMasivaPSN> lst = consumoIAPMasivaPSN.Catalogo((int)HttpContext.Current.Session["UsuarioActual"], Fechas.AnnomesAFecha((int)HttpContext.Current.Session["UMC_IAP"]).AddMonths(1).AddDays(-1), fechaInicio, fechaFin);

                return lst;
            }
            catch (Exception ex)
            {

                LogError.LogearError("Ocurrió un error obteniendo la lista de proyectos económicos", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de proyectos económicos"));
            }

            finally
            {
                consumoIAPMasivaPSN.Dispose();
            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.ConsumoIAPMasivaPT> proyectosTecnicos(string sPSN, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {

            BLL.ConsumoIAPMasivaPT consumoIAPMasivaPT = new BLL.ConsumoIAPMasivaPT();
            try
            {

                List<IB.SUPER.IAP30.Models.ConsumoIAPMasivaPT> lst = consumoIAPMasivaPT.Catalogo((int)HttpContext.Current.Session["UsuarioActual"], int.Parse(sPSN), Fechas.AnnomesAFecha((int)HttpContext.Current.Session["UMC_IAP"]).AddMonths(1).AddDays(-1), fechaInicio, fechaFin);

                consumoIAPMasivaPT.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                consumoIAPMasivaPT.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de proyectos técnicos", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de proyectos técnicos"));
            }
            finally
            {
                consumoIAPMasivaPT.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.ConsumoIAPMasivaT> tareas(string sPT, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {

            BLL.ConsumoIAPMasivaT consumoIAPMasivaT = new BLL.ConsumoIAPMasivaT();
            try
            {

                List<IB.SUPER.IAP30.Models.ConsumoIAPMasivaT> lst = consumoIAPMasivaT.Catalogo((int)HttpContext.Current.Session["UsuarioActual"], int.Parse(sPT), Fechas.AnnomesAFecha((int)HttpContext.Current.Session["UMC_IAP"]).AddMonths(1).AddDays(-1), fechaInicio, fechaFin);

                consumoIAPMasivaT.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                consumoIAPMasivaT.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de tareas", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de tareas"));
            }
            finally
            {
                consumoIAPMasivaT.Dispose();
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.BuscadorTareasBloque> tareasBitacoraIAP()
        {

            BLL.BuscadorTareasBloque buscadorTareasBloque = new BLL.BuscadorTareasBloque();
            try
            {

                List<IB.SUPER.IAP30.Models.BuscadorTareasBloque> lst = buscadorTareasBloque.tareasBitacoraIAP((int)HttpContext.Current.Session["UsuarioActual"]);

                return lst;
            }
            catch (Exception ex)
            {

                LogError.LogearError("Ocurrió un error obteniendo la lista de tareas ", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de tareas "));
            }
            finally
            {
                buscadorTareasBloque.Dispose();
            }

        }

    }
}