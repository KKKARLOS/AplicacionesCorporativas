using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaIAP
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TareaIAP : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("95f7bd17-347f-416a-b6f7-993d4bff0feb");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaIAP()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaIAP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public Models.TareaIAP Select(Int32 idTarea)//, Int32 nUsuario
        {
            OpenDbConn();

            DAL.TareaIAP cTareaIAP = new DAL.TareaIAP(cDblib);

            //string sModoAcceso = cTareaIAP.getAcceso(idTarea, nUsuario);
            //if (sModoAcceso == "N")
            //    throw new Exception("La tarea no existe o no tienes acceso");

            return cTareaIAP.SelectBitacora(idTarea);
        }
        public Models.TareaIAP Select(Int32 idTarea, Int32 nUsuario)
        {
            OpenDbConn();

            DAL.TareaIAP cTareaIAP = new DAL.TareaIAP(cDblib);

            return cTareaIAP.Select(idTarea, nUsuario);
        }
        public Models.TareaIAP SelectBitacora(Int32 idTarea, Int32 nUsuario)
        {
            OpenDbConn();

            DAL.TareaIAP cTareaIAP = new DAL.TareaIAP(cDblib);

            string sModoAcceso = cTareaIAP.getAcceso(idTarea, nUsuario);
            if (sModoAcceso == "N")
                throw new Exception("La tarea no existe o no tienes acceso");

            return cTareaIAP.SelectBitacora(idTarea);
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
        ~TareaIAP()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
