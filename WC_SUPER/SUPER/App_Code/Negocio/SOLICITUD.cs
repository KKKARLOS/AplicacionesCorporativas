using System;
using System.Collections.Generic;
//using System.Linq;
using System.Data.SqlClient;
using System.Web;
//using SUPER.Capa_Datos;
using System.Text;
using System.Collections;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de SOLICITUD
    /// </summary>
    public class SOLICITUD
    {
        public SOLICITUD()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Registra una solicitud de certificado y envía un correo a la cuenta indicada en el web.config por CorreoCertificaciones
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t696_tipo"></param>
        /// <param name="t696_nombre"></param>
        /// <param name="t696_observaciones"></param>
        /// <param name="t001_idficepi_solic"></param>
        /// <param name="t582_idcertificado"></param>
        /// <param name="t697_usuticks"></param>
        /// <returns></returns>
        public static int Insertar(SqlTransaction tr, string t696_tipo, string t696_nombre, string t696_observaciones, int t001_idficepi_solic,
                                   Nullable<int> t582_idcertificado, string t697_usuticks, string t696_tipopeticion)
        {
            int iRes = -1;

            iRes= SUPER.DAL.SOLICITUD.Insertar(tr, t696_tipo, t696_nombre, t696_observaciones, t001_idficepi_solic, t582_idcertificado, t697_usuticks, t696_tipopeticion);
            EnviarCorreo(iRes, t696_tipo, t696_nombre, t696_observaciones, t001_idficepi_solic, t582_idcertificado, t696_tipopeticion);

            return iRes;
        }
        public static void Resolver(SqlTransaction tr, int t696_id, string t696_motivo, int t001_idficepi_res, string t696_tipores)
        {
            SUPER.DAL.SOLICITUD.Resolver(tr, t696_id, t696_motivo, t001_idficepi_res, t696_tipores);
        }

        private static string EnviarCorreo(int idSolicitud, string sTipo, string sNombre, string sObs, int t001_idficepi, Nullable<int> idCertificado, string sTipoPeticion)
        {
            string sResul = "", sTamanoLabel="120";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sbuilder = new StringBuilder();
            string sAsunto = "", sTexto = "", sTO = "";
            //bool bPrimerArchivo = true;
            //try
            //{
                if (sTipo == "C")
                {
                   
                    if (sTipoPeticion == "R")
                    {
                        sAsunto = "Solicitud de renovación de certificado.";
                        sbuilder.Append(@"<br />SUPER le informa de la solicitud de renovación de un certificado con los siguientes datos:<br /><br />");
                    }
                    else
                    {
                        sAsunto = "Solicitud de creación de certificado.";
                        sbuilder.Append(@"<br />SUPER le informa de la solicitud de creación de un certificado con los siguientes datos:<br /><br />");

                    }
                }
                else
                {
                    if (sTipoPeticion == "R")
                    {
                        sAsunto = "Solicitud de renovación de examen.";
                        sbuilder.Append(@"<br />SUPER le informa de la solicitud de renovación de un examen con los siguientes datos:<br /><br />");
                    }
                    else
                    {
                        sAsunto = "Solicitud de creación de examen.";
                        sbuilder.Append(@"<br />SUPER le informa de la solicitud de creación de un examen con los siguientes datos:<br /><br />");
                    }                   
                }
                //Si no hay datos de certificado, los label pueden ser más cortos
                if (idCertificado != null)
                    sTamanoLabel = "140";

                sbuilder.Append("<label style='width:" + sTamanoLabel + "px'><b>Solicitante: </b></label>" + SUPER.DAL.Profesional.GetNombre(t001_idficepi) + "<br /><br />");
                if (idCertificado != null)
                {
                    SqlDataReader dr1 = SUPER.DAL.Certificado.GetCertificado(null, int.Parse(idCertificado.ToString()));
                    if (dr1.Read())
                    {
                        sbuilder.Append("<label style='width:140px'><b>Certificado: </b></label>" + dr1["T582_NOMBRE"].ToString() + "<br />");
                        sbuilder.Append("<label style='width:140px'><b>Entidad Certificadora: </b></label>" + dr1["Entidad"].ToString() + "<br />");
                        sbuilder.Append("<label style='width:140px'><b>Entorno Tecnológico: </b></label>" + dr1["Entorno"].ToString() + "<br /><br />");
                    }
                }
                //sbuilder.Append("<label style='width:" + sTamanoLabel + "px'><b>Denominación: </b></label>" + SUPER.Capa_Negocio.Utilidades.unescape(sNombre) + "<br />");
                //sbuilder.Append("<label style='width:" + sTamanoLabel + "px'><b>Observaciones: </b></label>" + SUPER.Capa_Negocio.Utilidades.unescape(sObs) + "<br />");
                sbuilder.Append("<label style='width:" + sTamanoLabel + "px'><b>Denominación: </b></label>" + sNombre + "<br />");
                sbuilder.Append("<label style='width:" + sTamanoLabel + "px'><b>Observaciones: </b></label>" + sObs + "<br />");
                
                //sbuilder.Append("<label style='width:120px'><b>Archivos: </b></label>");
                //SqlDataReader dr = SUPER.BLL.DOCSOLICITUD.Catalogo(idSolicitud);
                //while (dr.Read())
                //{
                //    if (bPrimerArchivo)
                //    {
                //        sbuilder.Append(dr["t697_nombrearchivo"].ToString() + "<br />");
                //        bPrimerArchivo = false;
                //    }
                //    else
                //        sbuilder.Append("<label style='width:120px'></label>" + dr["t697_nombrearchivo"].ToString() + "<br />");
                //}
                //dr.Close();
                //dr.Dispose();
                sTO = SUPER.DAL.Profesional.GetCuentaCorreo(t001_idficepi) + ";";
                sTO += System.Configuration.ConfigurationManager.AppSettings["CorreoCertificaciones"].ToString();
                sTexto = sbuilder.ToString();

                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);

                SUPER.Capa_Negocio.Correo.EnviarCorreosCert(aListCorreo);

                sResul = "OK@#@";
            //}
            //catch (Exception ex)
            //{
            //    sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al enviar correo de solicitud CVT.", ex);
            //}
            return sResul;
        }
    }
}