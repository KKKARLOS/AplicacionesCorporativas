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
/// Summary description for Accion
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Accion : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("bd58fafa-d291-4b61-a7d5-43a927ce3bea");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Accion()
			: base()
        {
			//OpenDbConn();
        }
		
		public Accion(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public Models.Accion Select(Int32 t383_idaccion)
        {
            OpenDbConn();

            DAL.Accion cAccionPE = new DAL.Accion(cDblib);
            return cAccionPE.Select(t383_idaccion);
        }

        public List<Models.Accion> Catalogo(int t382_idasunto)
        {
            OpenDbConn();

            DAL.Accion cConsulta = new DAL.Accion(cDblib);
            return cConsulta.Catalogo(t382_idasunto);
        }

        public void BorrarAcciones(List<Models.Accion> lista)
        {
            Guid methodOwnerID = new Guid("74122BF0-F33B-48BE-B665-9C1A70AF00BC");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.Accion cAccion = new DAL.Accion(cDblib);
                foreach (Models.Accion oAccion in lista)
                {
                    cAccion.Delete(oAccion.t383_idaccion);
                }
                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

            }
            catch (Exception ex)
            {//rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public int grabar(Models.Accion DatosGenerales, List<Models.AccionRecursos> Integrantes, List<Models.AccionTareas> Tareas)
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
                DAL.Accion oAccion = new DAL.Accion(cDblib);
                DAL.AccionRecursos oRecursoDAL = new DAL.AccionRecursos(cDblib);
                DAL.AccionTareas oTareaDAL = new DAL.AccionTareas(cDblib);

                if (DatosGenerales.t383_idaccion == -1)
                {
                    DatosGenerales.t383_fcreacion = System.DateTime.Now;
                    idReferencia = oAccion.Insert(DatosGenerales);
                }
                else
                {
                    oAccion.Update(DatosGenerales);
                    idReferencia = DatosGenerales.t383_idaccion;
                }

                foreach (Models.AccionRecursos oRecurso in Integrantes)
                {
                    switch (oRecurso.accionBD)
                    {
                        case "I":
                            //Inserción
                            oRecurso.T383_idaccion = idReferencia;
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

                foreach (Models.AccionTareas oTarea in Tareas)
                {
                    switch (oTarea.accionBD)
                    {
                        case "I":
                            //Inserción
                             oTareaDAL.Insert(oTarea);
                            break;
                        case "D":
                            //delete
                            oTareaDAL.Delete(oTarea);
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

        public void EnviarCorreo(Models.Accion DatosGenerales, List<Models.AccionRecursos> Integrantes, bool bAlta)
        {
            string sTexto = "", sTO = "", sToAux = "", sAux, sIdResponsable, slMails;
            string sAsunto = "";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sb = new StringBuilder();

            sIdResponsable = DatosGenerales.t382_responsable.ToString();
            slMails = DatosGenerales.t383_alerta.ToString();

            if (slMails == "" && sIdResponsable == "") return;
            sAsunto = "Alerta de acción en Bitácora de proyecto económico.";
            if (bAlta) sb.Append("<BR>SUPER le informa de la generación de la siguiente acción:<BR><BR>");
            else sb.Append("<BR>SUPER le informa de la modificación de la siguiente acción:<BR><BR>");

            sb.Append("<label style='width:120px'>Proyecto económico: </label>" + DatosGenerales.t301_idproyecto.ToString() + @" - " + DatosGenerales.DesPE + "<br>");
            sb.Append("<label style='width:120px'>Asunto: </label><b>" + DatosGenerales.t382_idasunto.ToString() + @" - " + DatosGenerales.t382_desasunto + "</b><br><br>");
            sb.Append("<label style='width:120px'>Acción: </label><b>" + DatosGenerales.t383_idaccion + @" - " + DatosGenerales.t383_desaccion + "</b><br><br>");
            sb.Append("<b>Información de la acción:</b><br>");

            //sb.Append("<label style='width:120px'>Responsable: </label>" + DatosGenerales.Responsable + "<br>");
            if (DatosGenerales.t383_flimite == null)
                sAux = "";
            else
                sAux = DatosGenerales.t383_flimite.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Límite: </label>" + sAux + "<br>");

            if (DatosGenerales.t383_ffin == null)
                sAux = "";
            else
                sAux = DatosGenerales.t383_ffin.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Fin: </label>" + sAux + "<br>");
            sb.Append("<label style='width:120px'>Avance: </label>" + DatosGenerales.t383_avance + "<br><br>");
            //descripcion larga
            sb.Append("<b><label style='width:120px'>Descripción: </label></b>" + DatosGenerales.t383_desaccionlong + "<br><br>");
            //observaciones
            sb.Append("<b><label style='width:120px'>Observaciones: </label></b>" + DatosGenerales.t383_obs + "<br><br>");
            //Departamento
            sb.Append("<b><label style='width:120px'>Departamento: </label></b>" + DatosGenerales.t383_dpto + "<br><br>");

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

            foreach (Models.AccionRecursos oRecurso in Integrantes)
            {
                if (oRecurso.accionBD != "D")
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

            foreach (Models.AccionRecursos oRecurso in Integrantes)
            {
                if (oRecurso.T389_notificar)
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
        ~Accion()
        {
            Dispose(false);
        }
		
        #endregion    
    }
}
