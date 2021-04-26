using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using SUPER.Capa_Negocio;
/// <summary>
/// Summary description for AccionPT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AccionPT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("8af354c8-877b-4445-b79c-ce2cc0226af1");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AccionPT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AccionPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        		        
        public Models.AccionPT Select(Int32 t410_idaccion)
        {
            OpenDbConn();

            DAL.AccionPT cAccionPT = new DAL.AccionPT(cDblib);
            return cAccionPT.Select(t410_idaccion);
        }

        public List<Models.AccionPT> Catalogo(int t382_idasunto)
        {
            OpenDbConn();

            DAL.AccionPT cConsulta = new DAL.AccionPT(cDblib);
            return cConsulta.Catalogo(t382_idasunto);
        }

        public void BorrarAcciones(List<Models.AccionPT> lista)
        {
            Guid methodOwnerID = new Guid("74122BF0-F33B-48BE-B665-9C1A70AF00BC");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AccionPT cAccion = new DAL.AccionPT(cDblib);
                foreach (Models.AccionPT oAccion in lista)
                {
                    cAccion.Delete(oAccion.T410_idaccion);
                }
                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

            }
            catch (Exception ex)
            {//rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public int grabar(Models.AccionPT DatosGenerales, List<Models.AccionRecursosPT> Integrantes, List<Models.AccionTareasPT> Tareas)
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
                DAL.AccionPT oAccion = new DAL.AccionPT(cDblib);
                DAL.AccionRecursosPT oRecursoDAL = new DAL.AccionRecursosPT(cDblib);
                DAL.AccionTareasPT oTareaDAL = new DAL.AccionTareasPT(cDblib);

                if (DatosGenerales.T410_idaccion == -1)
                {
                    DatosGenerales.T410_fcreacion = System.DateTime.Now;
                    idReferencia = oAccion.Insert(DatosGenerales);
                }
                else
                {
                    oAccion.Update(DatosGenerales);
                    idReferencia = DatosGenerales.T410_idaccion;
                }

                foreach (Models.AccionRecursosPT oRecurso in Integrantes)
                {
                    switch (oRecurso.accionBD)
                    {
                        case "I":
                            //Inserción
                            oRecurso.t410_idaccion = idReferencia;
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

                foreach (Models.AccionTareasPT oTarea in Tareas)
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
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);
                throw new Exception(ex.Message);
            }
            finally
            {
                //nota.Dispose();
            }
        }

        public void EnviarCorreo(Models.AccionPT DatosGenerales, List<Models.AccionRecursosPT> Integrantes, bool bAlta)
        {
            string sTexto = "", sTO = "", sToAux = "", sAux, sIdResponsable, slMails;
            string sAsunto = "";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sb = new StringBuilder();

            sIdResponsable = DatosGenerales.t314_idusuario_responsable.ToString();
            slMails = DatosGenerales.T410_alerta.ToString();

            if (slMails == "" && sIdResponsable == "") return;
            sAsunto = "Alerta de acción en Bitácora de proyecto técnico.";
            if (bAlta) sb.Append("<BR>SUPER le informa de la generación de la siguiente acción:<BR><BR>");
            else sb.Append("<BR>SUPER le informa de la modificación de la siguiente acción:<BR><BR>");

            sb.Append("<label style='width:120px'>Proyecto económico: </label>" + DatosGenerales.t301_idproyecto.ToString() + @" - " + DatosGenerales.t301_denominacion + "<br>");
            sb.Append("<label style='width:120px'>Proyecto técnico: </label>" + DatosGenerales.t331_idpt.ToString() + @" - " + DatosGenerales.t331_despt + "<br>");
            sb.Append("<label style='width:120px'>Asunto: </label><b>" + DatosGenerales.T409_idasunto.ToString() + @" - " + DatosGenerales.T409_desasunto + "</b><br><br>");
            sb.Append("<label style='width:120px'>Acción: </label><b>" + DatosGenerales.T410_idaccion + @" - " + DatosGenerales.T410_desaccion + "</b><br><br>");
            sb.Append("<b>Información de la acción:</b><br>");

            //sb.Append("<label style='width:120px'>Responsable: </label>" + DatosGenerales.Responsable + "<br>");
            if (DatosGenerales.T410_flimite == null)
                sAux = "";
            else
                sAux = DatosGenerales.T410_flimite.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Límite: </label>" + sAux + "<br>");

            if (DatosGenerales.T410_ffin == null)
                sAux = "";
            else
                sAux = DatosGenerales.T410_ffin.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Fin: </label>" + sAux + "<br>");
            sb.Append("<label style='width:120px'>Avance: </label>" + DatosGenerales.T410_avance + "<br><br>");
            //descripcion larga
            sb.Append("<b><label style='width:120px'>Descripción: </label></b>" + DatosGenerales.T410_desaccionlong + "<br><br>");
            //observaciones
            sb.Append("<b><label style='width:120px'>Observaciones: </label></b>" + DatosGenerales.T410_obs + "<br><br>");
            //Departamento
            sb.Append("<b><label style='width:120px'>Departamento: </label></b>" + DatosGenerales.T410_dpto + "<br><br>");

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

            foreach (Models.AccionRecursosPT oRecurso in Integrantes)
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

            foreach (Models.AccionRecursosPT oRecurso in Integrantes)
            {
                if (oRecurso.t414_notificar)
                {
                    string[] aMail = { sAsunto, sTexto, oRecurso.MAIL };
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
        ~AccionPT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
