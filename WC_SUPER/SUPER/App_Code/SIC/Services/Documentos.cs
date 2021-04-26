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
    public class Documentos : System.Web.Services.WebService
    {

        public Documentos()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="origenEdicion">tareapreventa || accionpreventa</param>
        /// <returns></returns>
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.DocumentacionPreventa> Catalogo(string origenEdicion, int idorigenEdicion)
        {

            BLL.DocumentacionPreventa.enumOrigenEdicion enumProp = 0;

            switch (origenEdicion.ToLower())
            {
                case "tareapreventa":
                    enumProp = BLL.DocumentacionPreventa.enumOrigenEdicion.tareapreventa;
                    break;
                case "accionpreventa":
                    enumProp = BLL.DocumentacionPreventa.enumOrigenEdicion.accionpreventa;
                    break;
                case "tareasaccionpreventa":
                    enumProp = BLL.DocumentacionPreventa.enumOrigenEdicion.tareasaccionpreventa;
                    break;
                case "acciontareapreventa":
                    enumProp = BLL.DocumentacionPreventa.enumOrigenEdicion.acciontareapreventa;
                    break;
                default:
                    throw new Exception(System.Uri.EscapeDataString("Valor del parámetro [origenEdicion] no válido"));
            }

            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                List<IB.SUPER.SIC.Models.DocumentacionPreventa> lst = cDP.Catalogo(enumProp, idorigenEdicion);

                return lst;
            }

            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo el catálogo de documentos", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo el catálogo de documentos"));
            }

            finally
            {
                cDP.Dispose();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="origenEdicion">tareapreventa || accionpreventa</param>
        /// <returns></returns>
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.DocumentacionPreventa> CatalogoGUID(string GUID)
        {


            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                List<IB.SUPER.SIC.Models.DocumentacionPreventa> lst = cDP.CatalogoGUID(new Guid(GUID));

                return lst;
            }

            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo el catálogo de documentos", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo el catálogo de documentos"));
            }

            finally
            {
                cDP.Dispose();
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IB.SUPER.SIC.Models.DocumentacionPreventa Select(int ta210_iddocupreventa)
        {

            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                return cDP.Select(ta210_iddocupreventa);
            }

            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo el documento", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo el documento"));
            }

            finally
            {
                cDP.Dispose();
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Delete(int ta210_iddocupreventa)
        {

            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                cDP.Delete(ta210_iddocupreventa);
            }
            catch (ValidationException vex)
            {
                throw new ValidationException(System.Uri.EscapeDataString(vex.Message));
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error eliminando el documento", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error eliminando el documento"));
            }

            finally
            {
                cDP.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IB.SUPER.SIC.Models.DocumentacionPreventa Update(IB.SUPER.SIC.Models.DocumentacionPreventa oDoc)
        {

            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                cDP.Update(oDoc);

                return cDP.Select(oDoc.ta210_iddocupreventa);
            }
            catch (ValidationException vex)
            {
                throw new ValidationException(System.Uri.EscapeDataString(vex.Message));
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error actualizando las propiedades del documento", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error actualizando las propiedades del documento"));
            }

            finally
            {
                cDP.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IB.SUPER.SIC.Models.DocumentacionPreventa Insert(IB.SUPER.SIC.Models.DocumentacionPreventa oDoc)
        {

            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                int ta210_iddocupreventa = cDP.Insert(oDoc);

                return cDP.Select(ta210_iddocupreventa);
            }
            catch (ValidationException vex)
            {
                throw new ValidationException(System.Uri.EscapeDataString(vex.Message));
            }
            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error adjuntando el nuevo documento", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error adjuntando el nuevo documento"));
            }

            finally
            {
                cDP.Dispose();
            }
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string obtenerOrigenEdicionEstado(string origenEdicion, int idorigenEdicion)
        {

            BLL.DocumentacionPreventa.enumOrigenEdicion enumProp = 0;

            switch (origenEdicion.ToLower())
            {
                case "tareapreventa":
                    enumProp = BLL.DocumentacionPreventa.enumOrigenEdicion.tareapreventa;
                    break;
                case "accionpreventa":
                    enumProp = BLL.DocumentacionPreventa.enumOrigenEdicion.accionpreventa;
                    break;
                default:
                    throw new Exception(System.Uri.EscapeDataString("Valor del parámetro [origenEdicion] no válido"));
            }

            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                return cDP.ObtenerEstadoOrigenEdicion(enumProp, idorigenEdicion);

            }

            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error inicializando el módulo de documentación", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error inicializando el módulo de documentación"));
            }

            finally
            {
                cDP.Dispose();
            }
        }
    }
}