using System;
using System.Collections;
using System.Collections.Generic;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using SUPER.Capa_Negocio;
/// <summary>
/// Summary description for Asunto
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Asunto : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("0c4689bb-7b37-48bb-b8fe-6bda1eb4db8a");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Asunto()
			: base()
        {
			//OpenDbConn();
        }
		
		public Asunto(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public Models.Asunto Select(Int32 t382_idasunto)
        {
            OpenDbConn();

            DAL.Asunto cAsuntoPE = new DAL.Asunto(cDblib);
            return cAsuntoPE.Select(t382_idasunto);
        }

        public int grabar(Models.Asunto DatosGenerales, List<Models.AsuntoRecursos> Integrantes)
        {
            int idReferencia;
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("5590F1B4-7073-4B5F-A4DB-9F301648D151");

            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);
            try
            {
                DAL.Asunto oAsunto = new DAL.Asunto(cDblib);
                DAL.AsuntoEstado oAsuntoEstadoDAL = new DAL.AsuntoEstado(cDblib);
                DAL.AsuntoRecursos oRecursoDAL = new DAL.AsuntoRecursos(cDblib);

                Models.AsuntoEstado oAsuntoEstado = new Models.AsuntoEstado();

                oAsuntoEstado.T385_codestado = byte.Parse(DatosGenerales.T382_estado);
                oAsuntoEstado.T385_idautor = (int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"];

                if (DatosGenerales.T382_idasunto==-1){
                    idReferencia = oAsunto.Insert(DatosGenerales);

                    oAsuntoEstado.T382_idasunto = idReferencia;
                    oAsuntoEstadoDAL.Insert(oAsuntoEstado);
                }
                else
                {
                    oAsunto.Update(DatosGenerales);
                    idReferencia = DatosGenerales.T382_idasunto;

                    if (DatosGenerales.T382_estado_anterior != DatosGenerales.T382_estado)
                    {
                        oAsuntoEstado.T382_idasunto = idReferencia;
                        oAsuntoEstadoDAL.Insert(oAsuntoEstado);
                    }
                }

                foreach (Models.AsuntoRecursos oRecurso in Integrantes)
                {
                    switch (oRecurso.accionBD)
                    {
                        case "I":
                            //Inserción
                            oRecurso.T382_idasunto = idReferencia;
                            oRecursoDAL.Insert(oRecurso);
                            break;
                        case "D":
                            //delete
                            oRecursoDAL.Delete(oRecurso);
                            break;
                        case "U":
                            //update
                            oRecursoDAL.Update(oRecurso);
                            break;
                    }
                }
                
                if (bConTransaccion) cDblib.commitTransaction(methodOwnerID);

                return idReferencia;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid()))
                    cDblib.rollbackTransaction(methodOwnerID);
                throw new Exception(ex.Message);
            }
            finally
            {
                //nota.Dispose();
            }
        }
        public void EnviarCorreo(Models.Asunto DatosGenerales, List<Models.AsuntoRecursos> Integrantes, bool bAlta)
        {
            string sTexto = "", sTO = "", sToAux = "", sAux, sIdResponsable, slMails;
            string sAsunto = "";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sb = new StringBuilder();

            sIdResponsable = DatosGenerales.T382_responsable.ToString();
            slMails = DatosGenerales.T382_alerta.ToString();

            if (slMails == "" && sIdResponsable == "") return;
            sAsunto = "Alerta de asunto en Bitácora de proyecto económico.";
            if (bAlta) sb.Append("<BR>SUPER le informa de la generación del siguiente asunto:<BR><BR>");
            else sb.Append("<BR>SUPER le informa de la modificación del siguiente asunto:<BR><BR>");

            sb.Append("<label style='width:120px'>Proyecto económico: </label>" + DatosGenerales.t301_idproyecto.ToString() + @" - " + DatosGenerales.DesPE + "<br>");
            sb.Append("<label style='width:120px'>Asunto: </label><b>" + DatosGenerales.T382_idasunto.ToString() + @" - " + DatosGenerales.T382_desasunto + "</b><br><br>");
            sb.Append("<b>Información del asunto:</b><br>");

            sb.Append("<label style='width:120px'>Responsable: </label>" + DatosGenerales.Responsable + "<br>");
            if (DatosGenerales.T382_flimite == null)
                sAux = "";
            else
                sAux = DatosGenerales.T382_flimite.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Límite: </label>" + sAux + "<br>");

            if (DatosGenerales.T382_ffin == null)
                sAux = "";
            else
                sAux = DatosGenerales.T382_ffin.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Fin: </label>" + sAux + "<br>");
            sb.Append("<label style='width:120px'>Ref. Externa: </label>" + DatosGenerales.T382_refexterna.ToString() + "<br>");
            sb.Append("<label style='width:120px'>Esfuerzo planificado: </label>" + double.Parse(DatosGenerales.T382_etp.ToString()).ToString("N") + "<br>");
            sb.Append("<label style='width:120px'>Esfuerzo real: </label>" + double.Parse(DatosGenerales.T382_etr.ToString()).ToString("N") + "<br>");
            sb.Append("<label style='width:120px'>Severidad: </label>" + DatosGenerales.DesSeveridad+ "<br>");
            sb.Append("<label style='width:120px'>Prioridad: </label>" + DatosGenerales.DesPrioridad + "<br>");
            sb.Append("<label style='width:120px'>Tipo: </label>" + DatosGenerales.T384_destipo + "<br>");
            sb.Append("<label style='width:120px'>Estado: </label>" + DatosGenerales.DesEstado + "<br>");
            sb.Append("<label style='width:120px'>Sistema afectado: </label>" + DatosGenerales.T382_sistema + "<br><br>");
            //descripcion larga
            sb.Append("<b><label style='width:120px'>Descripción: </label></b>" + DatosGenerales.T382_desasuntolong + "<br><br>");
            //observaciones
            sb.Append("<b><label style='width:120px'>Observaciones: </label></b>" + DatosGenerales.T382_obs + "<br><br>");
            //Departamento
            sb.Append("<b><label style='width:120px'>Departamento: </label></b>" + DatosGenerales.T382_dpto + "<br><br>");

            //Obtengo la lista de e-mail a los que alertar
            if (!slMails.Contains(";")) slMails += ";";
            string[] aMails = Regex.Split(slMails, ";");
            //Genero una tabla con la lista de e-mails a notificar
            sb.Append("<b><label style='width:400px'>Relación de e-mails a notificar: </label></b> <br>");
            sb.Append("<table width='400px' style='padding:10px;'>");
            sb.Append("<colgroup><col style='width:400px;' /></colgroup>");
            sb.Append("<tbody>");
            for (int i = 0; i < aMails.Length; i++)
            {
                sToAux = aMails[i].Trim();
                if (sToAux != "")
                {
                    sb.Append("<tr><td style='padding-left:5px;font-size:11px;'>");
                    sTO = sToAux;
                    sAux = sTO.Substring(0, 2);
                    if (sAux == "\r\n") sTO = sTO.Substring(2);
                    sb.Append(sTO);
                    sb.Append("</td></tr>");
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table><br>");
            //Genero una tabla con la lista de profesionales a notificar
            sb.Append("<b><label style='width:400px'>Relación de profesionales asignados: </label> </b><br>");
            sb.Append("<table width='400px' style='padding:10px;'>");
            sb.Append("<colgroup><col style='width:400px;' /></colgroup>");
            sb.Append("<tbody>");

            foreach (Models.AsuntoRecursos oRecurso in Integrantes)
            {
                if (oRecurso.accionBD!="D")
                {
                    sb.Append("<tr><td style='padding-left:5px;font-size:11px;'>");
                    sb.Append(oRecurso.nomRecurso);
                    sb.Append("</td></tr>");
                }
            }

            sb.Append("</tbody>");
            sb.Append("</table><br>");

            sTexto = sb.ToString();

            //Envío e-mail al responsable del asunto 
            if (sIdResponsable != "")
            {
                BLL.Recursos oRecursos = new BLL.Recursos();
                Models.Recursos oRecursoModel = new Models.Recursos();
                try
                {
                    oRecursoModel = oRecursos.establecerUsuarioIAP("", int.Parse(sIdResponsable));
                }
                catch (Exception ex)
                {
                    throw new Exception(System.Uri.EscapeDataString("Error al obtener el código de red" + ex.Message));
                }
                finally
                {
                    oRecursos.Dispose();
                }
                sTO = oRecursoModel.t001_codred;                  
                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);
            }
            //Obtengo la lista de e-mail a los que alertar y envío un correo a cada uno
            for (int i = 0; i < aMails.Length; i++)
            {
                if (aMails[i] != "")
                {
                    sTO = aMails[i];
                    //sTO.Replace((char)10, (char)160);
                    //sTO.Replace((char)13, (char)160);
                    sAux = sTO.Substring(0, 2);
                    if (sAux == "\r\n") sTO = sTO.Substring(2);
                    sTO.Trim();
                    string[] aMail = { sAsunto, sTexto, sTO };
                    aListCorreo.Add(aMail);
                }
            }
            //Obtengo la lista de profesionales a los que notificar y envío un correo a cada uno

            foreach (Models.AsuntoRecursos oRecurso in Integrantes)
            {
                if (oRecurso.T388_notificar)
                {
                    string[] aMail = { sAsunto, sTexto, oRecurso.mail };
                    aListCorreo.Add(aMail);
                }
            }

            Correo.EnviarCorreos(aListCorreo);
        }
        #endregion          
		
		#region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);
        }
        private void AttachDbConn(sqldblib.SqlServerSP extcDblib)
        {
            cDblib = extcDblib;
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) if (cDblib != null && cDblib.OwnerID.Equals(classOwnerID)) cDblib.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Asunto()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
