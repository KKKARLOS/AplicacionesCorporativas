using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for ConceptoTareaPendiente
/// </summary>
namespace IB.SUPER.SIC.BLL 
{
    public class ConceptoTareaPendiente : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("565018cd-2caa-4e61-bf48-db006145b2c4");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConceptoTareaPendiente()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConceptoTareaPendiente(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
		
		internal List<Models.ConceptoTareaPendiente> Catalogo(Models.ConceptoTareaPendiente oConceptoTareaPendienteFilter)
        {
            OpenDbConn();

			DAL.ConceptoTareaPendiente cConceptoTareaPendiente = new DAL.ConceptoTareaPendiente(cDblib);
            return cConceptoTareaPendiente.Catalogo(oConceptoTareaPendienteFilter);

        }
		
		internal int Insert(Models.ConceptoTareaPendiente oConceptoTareaPendiente)
        {
			Guid methodOwnerID = new Guid("7fad1727-093b-4828-bada-8354318615fc");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.ConceptoTareaPendiente cConceptoTareaPendiente = new DAL.ConceptoTareaPendiente(cDblib);
                
                int idConceptoTareaPendiente = cConceptoTareaPendiente.Insert(oConceptoTareaPendiente);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idConceptoTareaPendiente;
            }
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }	
		}

        internal int Update(Models.ConceptoTareaPendiente oConceptoTareaPendiente)
        {
			Guid methodOwnerID = new Guid("00594953-2631-4174-820f-094f86a6e68d");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.ConceptoTareaPendiente cConceptoTareaPendiente = new DAL.ConceptoTareaPendiente(cDblib);
				
				int result = cConceptoTareaPendiente.Update(oConceptoTareaPendiente);

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

        internal int Delete(Byte ta209_idconceptotareapendiente)
        {
			Guid methodOwnerID = new Guid("703a7f0c-e419-439b-90ab-6421961f1043");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
			
				DAL.ConceptoTareaPendiente cConceptoTareaPendiente = new DAL.ConceptoTareaPendiente(cDblib);
				
				int result = cConceptoTareaPendiente.Delete(ta209_idconceptotareapendiente);

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

        internal Models.ConceptoTareaPendiente Select(Byte ta209_idconceptotareapendiente)
        {
			OpenDbConn();
			
            DAL.ConceptoTareaPendiente cConceptoTareaPendiente = new DAL.ConceptoTareaPendiente(cDblib);
			return cConceptoTareaPendiente.Select(ta209_idconceptotareapendiente);
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
        ~ConceptoTareaPendiente()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
