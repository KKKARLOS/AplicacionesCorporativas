using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

namespace SUPER.BLL
{
    public class ContentServer
    {
        #region Propiedades y Atributos
        private string _Error;
        public string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }

        private byte[] _Archivo;
        public byte[] Archivo
        {
            get { return _Archivo; }
            set { _Archivo = value; }
        }
        #endregion
        public ContentServer()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public ContentServer(Nullable<long> idDoc)
        {
            if (idDoc != null)
            {
                try
                {
                    IB.Conserva.svcConserva.CSVDocument oDoc = IB.Conserva.ConservaHelper.ObtenerDocumento((long)idDoc);
                    Archivo = oDoc.content;
                    Error = "";
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                }
            }
            else
                Error = "La clave para identificar el documento en el Content-Server es vacía";
        }
        /// <summary>
        /// Dado un id de documento en Atenea devuelve el array de bytes que conforman el archivo
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public static byte[] ObtenerDocumento(Nullable<long> idDoc)
        {
            byte[] Archivo = null;
            if (idDoc != null)
            {
                IB.Conserva.svcConserva.CSVDocument oDoc = IB.Conserva.ConservaHelper.ObtenerDocumento((long)idDoc);
                Archivo = oDoc.content;
            }
            return Archivo;
        }
        public static long InsertarDocumento(string sNombreArchivo, byte[] ContenidoArchivo)
        {
            return IB.Conserva.ConservaHelper.SubirDocumento(sNombreArchivo, ContenidoArchivo);
        }
        /// <summary>
        /// Modifica el contenido de un documento
        /// </summary>
        /// <param name="idDoc"></param>
        /// <param name="ContenidoArchivo"></param>
        public static void ModificarDocumento(long idDoc, byte[] ContenidoArchivo)
        {
            IB.Conserva.ConservaHelper.ActualizarContenidoDocumento(idDoc, ContenidoArchivo);
        }
        /// <summary>
        /// Sustitute un documento por otro diferente
        /// </summary>
        /// <param name="idDoc"></param>
        /// <param name="sNombreArchivo"></param>
        /// <param name="ContenidoArchivo"></param>
        public static void CambiarDocumento(long idDoc, string sNombreArchivo, byte[] ContenidoArchivo)
        {
            IB.Conserva.ConservaHelper.ActualizarDocumento(idDoc, ContenidoArchivo, sNombreArchivo);
        }

        public static string ComponerErrorConserva(ConservaException cex)
        {
            string msg = "";

            switch (cex.ErrorCode)
            {
                case 100: 
                case 101:
                case 102:
                case 103:
                    msg = "Ocurrió un error de validación: " + cex.Message;
                    break;
                case 110:
                case 111:
                case 112:
                    msg = "Ocurrió un error accediendo al repositorio de documentos: " + cex.Message;
                    break;
                case 140:
                case 141:
                case 142:
                case 143:
                    msg = "Existe un error en la configuración de acceso al repositorio: " + cex.Message;
                    break;
                case 120:
                    msg = "Ocurrió un error en la operación del repositorio.";
                    if (cex.InnerException != null)
                        if (cex.InnerException.GetType().Name == "ConservaException")
                        {
                            ConservaException icex = (ConservaException)cex.InnerException;
                            msg += " Código:" + icex.ErrorCode + ". Descripción:" + icex.Message;
                        }
                        else
                            msg += " Descripción=" + cex.InnerException.Message;
                    break;
            }

            return msg;
        }
    }
}
