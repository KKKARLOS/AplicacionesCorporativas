using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for TipoDocumento
/// </summary>
namespace IB.SUPER.SIC.BLL 
{
    public class TipoDocumento : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("12039ae4-9b50-40f2-85da-73e77880fc9b");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TipoDocumento()
			: base()
        {
			//OpenDbConn();
        }
		
		public TipoDocumento(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
		
		internal List<Models.TipoDocumento> Catalogo(Models.TipoDocumento oTipoDocumentoFilter)
        {
            OpenDbConn();

			DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);
            return cTipoDocumento.Catalogo(oTipoDocumentoFilter);

        }
		
		internal int Insert(Models.TipoDocumento oTipoDocumento)
        {
			Guid methodOwnerID = new Guid("d5c7e134-54e7-45dc-8725-5f9030f61108");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);
                
                int idTipoDocumento = cTipoDocumento.Insert(oTipoDocumento);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTipoDocumento;
            }
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }	
		}

        internal int Update(Models.TipoDocumento oTipoDocumento)
        {
			Guid methodOwnerID = new Guid("dc9a160a-65f5-43c2-a854-95d38b1d7606");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);
				
				int result = cTipoDocumento.Update(oTipoDocumento);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
				
				return result;
			}
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Delete(Byte ta211_idtipodocumento)
        {
			Guid methodOwnerID = new Guid("2d46cbc9-b60d-458d-8373-6389a539a70c");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
			
				DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);
				
				int result = cTipoDocumento.Delete(ta211_idtipodocumento);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
				
				return result;
			}
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal Models.TipoDocumento Select(Byte ta211_idtipodocumento)
        {
			OpenDbConn();
			
            DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);
			return cTipoDocumento.Select(ta211_idtipodocumento);
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
        ~TipoDocumento()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
