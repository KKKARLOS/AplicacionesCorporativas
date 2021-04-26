using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using IB.Progress.DAL;
using IB.Progress.Models;
using IB.Progress.Shared;

/// <summary>
/// Summary description for TRAMITACIONCAMBIOROL
/// </summary>
namespace IB.Progress.BLL 
{
    public class TramitacionCambioRol : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("a4ce6097-c067-4a3a-be5e-9c1f3bdbefed");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TramitacionCambioRol()
			: base()
        {
			//OpenDbConn();
        }
		
		public TramitacionCambioRol(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.TramitacionCambioRol> catalogoSolicitudes(int t001_idficepi)
        {
            OpenDbConn();

            DAL.TramitacionCambioRol cProfesional = new DAL.TramitacionCambioRol(cDblib);

            return cProfesional.catalogoSolicitudes(t001_idficepi);
        }


        public Models.TramitacionCambioRol getCountTiles(int t001_idficepi)
        {
            OpenDbConn();

            DAL.TramitacionCambioRol cProfesional = new DAL.TramitacionCambioRol(cDblib);

            return cProfesional.getCountTiles(t001_idficepi);
        }


        public List<Models.TramitacionCambioRol> getSolicitudesSegunEstado(char t940_resolucion, int t001_idficepi)
        {
            OpenDbConn();

            DAL.TramitacionCambioRol cProfesional = new DAL.TramitacionCambioRol(cDblib);

            return cProfesional.getSolicitudesSegunEstado(t940_resolucion, t001_idficepi);
        }


		public string Insert(IB.Progress.Models.TRAMITACIONCAMBIOROL_INS oTRAMITACIONCAMBIOROL)
        {
			Guid methodOwnerID = new Guid("fcdd9d71-5b97-4b47-b221-0287c3d0b499");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				IB.Progress.DAL.TramitacionCambioRol cTRAMITACIONCAMBIOROL = new IB.Progress.DAL.TramitacionCambioRol(cDblib);
                
                string idTRAMITACIONCAMBIOROL = cTRAMITACIONCAMBIOROL.Insert(oTRAMITACIONCAMBIOROL);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTRAMITACIONCAMBIOROL;
            }
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }	
		}


        public int CambioEstadoSolicitudCambioRol(int t940_idtramitacambiorol, char t940_resolucion, int t001_idficepi_ultmodificador)
        {
            Guid methodOwnerID = new Guid("cf1beb9e-7e9e-4994-a0d5-c016f827d917");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.TramitacionCambioRol cSolicitudes = new DAL.TramitacionCambioRol(cDblib);


                int idSolicitudes = cSolicitudes.CambioEstadoSolicitudCambioRol(t940_idtramitacambiorol, t940_resolucion, t001_idficepi_ultmodificador);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idSolicitudes;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(120, "Ocurrió un error al intentar aceptar la incorporación del profesional.", ex);
            }
        }

        public void CambioEstadoSolicitudROL(List<IB.Progress.Models.TramitacionCambioRol> oProfesional)
        {
            Guid methodOwnerID = new Guid("cf1beb9e-7e9e-4994-a0d5-c016f827d917");

            OpenDbConn();
            int idSolicitudes = 0;
            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.TramitacionCambioRol cSolicitudes = new DAL.TramitacionCambioRol(cDblib);

                foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
                {
                    idSolicitudes = cSolicitudes.CambioEstadoSolicitudCambioRol(item.t940_idtramitacambiorol, item.t940_resolucion, ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
                }                

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(120, "Ocurrió un error al intentar aceptar la incorporación del profesional.", ex);
            }
        }

        public void AceptarCambioRol(List<IB.Progress.Models.TramitacionCambioRol> oProfesional)
        {
            Guid methodOwnerID = new Guid("c47d884a-1a96-4e58-b774-4e19693ae77a");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                
                DAL.TramitacionCambioRol cSolicitudes= new DAL.TramitacionCambioRol(cDblib);

                foreach (IB.Progress.Models.TramitacionCambioRol item in oProfesional)
                {
                    cSolicitudes.UpdateSolicitudCambioRol(item.t940_idtramitacambiorol, item.t001_idficepi_interesado, item.t004_idrol_propuesto, item.t001_idficepi_aprobador);                        
                }

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
                
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(120, "Ocurrió un error al intentar aceptar el cambio de rol.", ex);
            }
        }


        public int NoAceptacion(int t940_idtramitacambiorol, string t940_motivorechazo, int t001_idficepi_aprobador)
        {
			Guid methodOwnerID = new Guid("3bf109fe-8e69-4e33-a303-42b8046ebc0d");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
			
				IB.Progress.DAL.TramitacionCambioRol cTRAMITACIONCAMBIOROL = new IB.Progress.DAL.TramitacionCambioRol(cDblib);

                int result = cTRAMITACIONCAMBIOROL.NoAceptacion(t940_idtramitacambiorol, t940_motivorechazo, t001_idficepi_aprobador);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
				
				return result;
			}
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }


        public int Delete(int t940_idtramitacambiorol)
        {
            Guid methodOwnerID = new Guid("c71bd9f3-10ed-43e1-9e24-cbcce10e8c43");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                IB.Progress.DAL.TramitacionCambioRol cTRAMITACIONCAMBIOROL = new IB.Progress.DAL.TramitacionCambioRol(cDblib);

                int result = cTRAMITACIONCAMBIOROL.Delete(t940_idtramitacambiorol);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        //public int DeleteSolicitudCambioRol(string t940_uid, string motivo)
        //{
        //    Guid methodOwnerID = new Guid("ccf1c355-6b08-42d2-87f5-a60798c32af4");

        //    OpenDbConn();

        //    if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

        //    try
        //    {

        //        IB.Progress.DAL.TRAMITACIONCAMBIOROL cTRAMITACIONCAMBIOROL = new IB.Progress.DAL.TRAMITACIONCAMBIOROL(cDblib);

        //        int result = cTRAMITACIONCAMBIOROL.Delete(t940_uid);

        //        //Finalizar transacción 
        //        if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

        //        //TODO ENVIAR CORREO CON MOTIVO
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        //rollback
        //        if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

        //        throw ex;
        //    }
        //}
        
		#endregion          
		
		#region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(IB.Progress.Shared.Database.GetConStr(), classOwnerID);
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
        ~TramitacionCambioRol()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
