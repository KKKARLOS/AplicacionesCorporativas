using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;
using System.Data;

/// <summary>
/// Summary description for TareaPendientePreventa
/// </summary>
namespace IB.SUPER.SIC.BLL 
{
    public class TareaPendientePreventa : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("5fdde580-31ec-4b39-b52e-e6d3ebc1b9e0");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaPendientePreventa()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaPendientePreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
		
		internal List<Models.TareaPendientePreventa> Catalogo(Models.TareaPendientePreventa oTareaPendientePreventaFilter)
        {
            OpenDbConn();

			DAL.TareaPendientePreventa cTareaPendientePreventa = new DAL.TareaPendientePreventa(cDblib);
            return cTareaPendientePreventa.Catalogo(oTareaPendientePreventaFilter);

        }

        public void quitarNegritaTareaPendiente(int t001_idficepi, List<int> tablaconceptos, Nullable<int> ta204_idaccionpreventa, Nullable<int> ta207_idtareapreventa )
        {

            OpenDbConn();

            DAL.TareaPendientePreventa cTareaPendiente = new DAL.TareaPendientePreventa(cDblib);


            DataTable dtConceptos = new DataTable();
            dtConceptos.Columns.Add(new DataColumn("col_1", typeof(int)));
            
            //Recorremos la lista
            foreach (int oParticipante in tablaconceptos)
            {
                DataRow row = dtConceptos.NewRow();
                row["col_1"] = oParticipante;

                dtConceptos.Rows.Add(row);
            }


            cTareaPendiente.quitarNegritaTareaPendiente(t001_idficepi, dtConceptos, ta204_idaccionpreventa, ta207_idtareapreventa);
        }
		
		internal int Insert(Models.TareaPendientePreventa oTareaPendientePreventa)
        {
			Guid methodOwnerID = new Guid("7251e550-0f7f-4000-a3f6-4648ea4d9e16");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.TareaPendientePreventa cTareaPendientePreventa = new DAL.TareaPendientePreventa(cDblib);
                
                int idTareaPendientePreventa = cTareaPendientePreventa.Insert(oTareaPendientePreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTareaPendientePreventa;
            }
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }	
		}

        internal int Update(Models.TareaPendientePreventa oTareaPendientePreventa)
        {
			Guid methodOwnerID = new Guid("bf69ae01-7cf4-45e2-833e-8c0a14c9b733");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.TareaPendientePreventa cTareaPendientePreventa = new DAL.TareaPendientePreventa(cDblib);
				
				int result = cTareaPendientePreventa.Update(oTareaPendientePreventa);

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

        internal int Delete(Int32 ta208_idtareapendientepreventa)
        {
			Guid methodOwnerID = new Guid("f4f89475-09c5-4944-91c8-d80e2aae8dac");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
			
				DAL.TareaPendientePreventa cTareaPendientePreventa = new DAL.TareaPendientePreventa(cDblib);
				
				int result = cTareaPendientePreventa.Delete(ta208_idtareapendientepreventa);

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

        internal Models.TareaPendientePreventa Select(Int32 ta208_idtareapendientepreventa)
        {
			OpenDbConn();
			
            DAL.TareaPendientePreventa cTareaPendientePreventa = new DAL.TareaPendientePreventa(cDblib);
			return cTareaPendientePreventa.Select(ta208_idtareapendientepreventa);
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
        ~TareaPendientePreventa()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
