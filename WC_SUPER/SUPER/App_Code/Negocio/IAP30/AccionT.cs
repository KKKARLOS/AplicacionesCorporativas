using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using SUPER.Capa_Negocio;
/// <summary>
/// Summary description for AccionT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AccionT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6d44a565-460f-488c-9914-92eb6667d922");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AccionT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AccionT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public Models.AccionT Select(Int32 t601_idaccion)
        {
            OpenDbConn();

            DAL.AccionT cAccionTarea = new DAL.AccionT(cDblib);
            return cAccionTarea.Select(t601_idaccion);
        }

        public List<Models.AccionT> Catalogo(int t600_idasunto)
        {
            OpenDbConn();

            DAL.AccionT cConsulta = new DAL.AccionT(cDblib);
            return cConsulta.Catalogo(t600_idasunto);
        }

        public void BorrarAcciones(List<Models.AccionT> lista)
        {
            Guid methodOwnerID = new Guid("3BC30F92-578A-4551-807F-74019A4DE66F");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AccionT cAccion = new DAL.AccionT(cDblib);
                foreach (Models.AccionT oAccion in lista)
                {
                    cAccion.Delete(oAccion.T601_idaccion);
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

        public int grabar(Models.AccionT DatosGenerales, List<Models.AccionRecursosT> Integrantes)
        {
            int idReferencia;
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("9BA8A99E-6FE6-41EC-BDA6-1A123F0E7C9A");

            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);
            try
            {
                DAL.AccionT oAccion = new DAL.AccionT(cDblib);
                DAL.AccionRecursosT oRecursoDAL = new DAL.AccionRecursosT(cDblib);

                if (DatosGenerales.T601_idaccion == -1)
                {
                    DatosGenerales.T601_fcreacion = System.DateTime.Now;
                    idReferencia = oAccion.Insert(DatosGenerales);
                }
                else
                {
                    oAccion.Update(DatosGenerales);
                    idReferencia = DatosGenerales.T601_idaccion;
                }

                foreach (Models.AccionRecursosT oRecurso in Integrantes)
                {
                    switch (oRecurso.accionBD)
                    {
                        case "I":
                            //Inserción
                            oRecurso.t601_idaccion = idReferencia;
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
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);
                throw new Exception(ex.Message);
            }
            finally
            {
                //nota.Dispose();
            }
        }

        public void EnviarCorreo(Models.AccionT DatosGenerales, List<Models.AccionRecursosT> Integrantes, bool bAlta)
        {
            string sTexto = "", sTO = "", sToAux = "", sAux, sIdResponsable, slMails;
            string sAsunto = "";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sb = new StringBuilder();

            sIdResponsable = DatosGenerales.t314_idusuario_responsable.ToString();
            slMails = DatosGenerales.T601_alerta.ToString();

            if (slMails == "" && sIdResponsable == "") return;
            sAsunto = "Alerta de acción en Bitácora de tarea.";
            if (bAlta) sb.Append("<BR>SUPER le informa de la generación de la siguiente acción:<BR><BR>");
            else sb.Append("<BR>SUPER le informa de la modificación de la siguiente acción:<BR><BR>");

            sb.Append("<label style='width:120px'>Proyecto económico: </label>" + DatosGenerales.t301_idproyecto.ToString() + @" - " + DatosGenerales.t301_denominacion + "<br>");
            sb.Append("<label style='width:120px'>Proyecto técnico: </label>" + DatosGenerales.t301_idproyecto.ToString() + @" - " + DatosGenerales.t331_despt + "<br>");
            sb.Append("<label style='width:120px'>Fase: </label>" + DatosGenerales.t334_desfase + "<br>");
            sb.Append("<label style='width:120px'>Actividad: </label>" + DatosGenerales.t335_desactividad + "<br>");
            sb.Append("<label style='width:120px'>Tarea: </label>" + DatosGenerales.t332_idtarea.ToString() + @" - " + DatosGenerales.t332_destarea + "<br>");
            sb.Append("<label style='width:120px'>Asunto: </label><b>" + DatosGenerales.T600_idasunto.ToString() + @" - " + DatosGenerales.t600_desasunto + "</b><br><br>");
            sb.Append("<label style='width:120px'>Acción: </label><b>" + DatosGenerales.T601_idaccion + @" - " + DatosGenerales.T601_desaccion + "</b><br><br>");
            sb.Append("<b>Información de la acción:</b><br>");

            //sb.Append("<label style='width:120px'>Responsable: </label>" + DatosGenerales.Responsable + "<br>");
            if (DatosGenerales.T601_flimite == null)
                sAux = "";
            else
                sAux = DatosGenerales.T601_flimite.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Límite: </label>" + sAux + "<br>");

            if (DatosGenerales.T601_ffin == null)
                sAux = "";
            else
                sAux = DatosGenerales.T601_ffin.ToString().Substring(0, 10);

            sb.Append("<label style='width:120px'>F/Fin: </label>" + sAux + "<br>");
            sb.Append("<label style='width:120px'>Avance: </label>" + DatosGenerales.T601_avance + "<br><br>");
            //descripcion larga
            sb.Append("<b><label style='width:120px'>Descripción: </label></b>" + DatosGenerales.T601_desaccionlong + "<br><br>");
            //observaciones
            sb.Append("<b><label style='width:120px'>Observaciones: </label></b>" + DatosGenerales.T601_obs + "<br><br>");
            //Departamento
            sb.Append("<b><label style='width:120px'>Departamento: </label></b>" + DatosGenerales.T601_dpto + "<br><br>");

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

            foreach (Models.AccionRecursosT oRecurso in Integrantes)
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

            foreach (Models.AccionRecursosT oRecurso in Integrantes)
            {
                if (oRecurso.t605_notificar)
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
        ~AccionT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
