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
    public class Documentos : System.Web.Services.WebService
    {

        public Documentos()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="origenEdicion">detalletarea</param>
        /// <returns></returns>
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.IAP30.Models.Documento> Catalogo(string origenEdicion, int idUsuAutorizado, int idElemento)
        {

            BLL.Documento.enumOrigenEdicion enumProp = 0;

            switch (origenEdicion)
            {
                case "detalleTarea":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleTarea;
                    break;
                case "detalleAsuntoPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPE;
                    break;
                case "detalleAccionPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPE;
                    break;
                case "detalleAsuntoPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPT;
                    break;
                case "detalleAccionPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPT;
                    break;
                case "detalleAsuntoTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoTA;
                    break;
                case "detalleAccionTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionTA;
                    break;
                default:
                    throw new Exception(System.Uri.EscapeDataString("Valor del parámetro [origenEdicion] no válido"));
            }

            BLL.Documento cDoc = new BLL.Documento();

            try
            {
                List<IB.SUPER.IAP30.Models.Documento> lst = cDoc.Catalogo(enumProp, idUsuAutorizado, idElemento);

                return lst;
            }

            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo el catálogo de documentos", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo el catálogo de documentos"));
            }

            finally
            {
                cDoc.Dispose();
            }
        }

        /*/// <summary>
        /// </summary>
        /// <param name="origenEdicion">tareapreventa || accionpreventa</param>
        /// <returns></returns>
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<Models.DocumentacionPreventa> CatalogoGUID(string GUID)
        {


            BLL.DocumentacionPreventa cDP = new BLL.DocumentacionPreventa();

            try
            {
                List<Models.DocumentacionPreventa> lst = cDP.CatalogoGUID(new Guid(GUID));

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

        */
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IB.SUPER.IAP30.Models.Documento Select(string origenEdicion, int idDocumento)
        {

            BLL.Documento.enumOrigenEdicion enumProp = 0;

            switch (origenEdicion)
            {
                case "detalleTarea":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleTarea;
                    break;
                case "detalleAsuntoPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPE;
                    break;
                case "detalleAccionPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPE;
                    break;
                case "detalleAsuntoPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPT;
                    break;
                case "detalleAccionPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPT;
                    break;
                case "detalleAsuntoTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoTA;
                    break;
                case "detalleAccionTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionTA;
                    break;
                default:
                    throw new Exception(System.Uri.EscapeDataString("Valor del parámetro [origenEdicion] no válido"));
            }
            BLL.Documento cDoc = new BLL.Documento();

            try
            {
                return cDoc.Select(enumProp, idDocumento);
            }

            catch (Exception ex)
            {
                LogError.LogearError("Ocurrió un error obteniendo el documento", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo el documento"));
            }

            finally
            {
                cDoc.Dispose();
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Delete(string origenEdicion, int idDocumento)
        {

            BLL.Documento.enumOrigenEdicion enumProp = 0;

            switch (origenEdicion)
            {
                case "detalleTarea":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleTarea;
                    break;
                case "detalleAsuntoPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPE;
                    break;
                case "detalleAccionPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPE;
                    break;
                case "detalleAsuntoPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPT;
                    break;
                case "detalleAccionPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPT;
                    break;
                case "detalleAsuntoTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoTA;
                    break;
                case "detalleAccionTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionTA;
                    break;
                default:
                    throw new Exception(System.Uri.EscapeDataString("Valor del parámetro [origenEdicion] no válido"));
            }

            BLL.Documento cDoc = new BLL.Documento();

            try
            {
                cDoc.Delete(enumProp, idDocumento);
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
                cDoc.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IB.SUPER.IAP30.Models.Documento Update(string origenEdicion, IB.SUPER.IAP30.Models.Documento oDoc)
        {

            BLL.Documento.enumOrigenEdicion enumProp = 0;

            switch (origenEdicion)
            {
                case "detalleTarea":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleTarea;
                    break;
                case "detalleAsuntoPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPE;
                    break;
                case "detalleAccionPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPE;
                    break;
                case "detalleAsuntoPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPT;
                    break;
                case "detalleAccionPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPT;
                    break;
                case "detalleAsuntoTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoTA;
                    break;
                case "detalleAccionTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionTA;
                    break;
                default:
                    throw new Exception(System.Uri.EscapeDataString("Valor del parámetro [origenEdicion] no válido"));
            }

            BLL.Documento cDoc = new BLL.Documento();
            try
            {
                cDoc.Update(enumProp, oDoc);

                return cDoc.Select(enumProp, oDoc.idDocumento);
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
                cDoc.Dispose();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IB.SUPER.IAP30.Models.Documento Insert(string origenEdicion, IB.SUPER.IAP30.Models.Documento oDoc)
        {
            BLL.Documento.enumOrigenEdicion enumProp = 0;

            switch (origenEdicion)
            {
                case "detalleTarea":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleTarea;
                    break;
                case "detalleAsuntoPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPE;
                    break;
                case "detalleAccionPE":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPE;
                    break;
                case "detalleAsuntoPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoPT;
                    break;
                case "detalleAccionPT":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionPT;
                    break;
                case "detalleAsuntoTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAsuntoTA;
                    break;
                case "detalleAccionTA":
                    enumProp = BLL.Documento.enumOrigenEdicion.detalleAccionTA;
                    break;
                default:
                    throw new Exception(System.Uri.EscapeDataString("Valor del parámetro [origenEdicion] no válido"));
            }

            BLL.Documento cDoc = new BLL.Documento();

            try
            {
                int idDocumento = cDoc.Insert(enumProp, oDoc);

                return cDoc.Select(enumProp, idDocumento);

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
                cDoc.Dispose();
            }
        }

        /*[WebMethod(EnableSession = false)]
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
        }*/
    }
}
