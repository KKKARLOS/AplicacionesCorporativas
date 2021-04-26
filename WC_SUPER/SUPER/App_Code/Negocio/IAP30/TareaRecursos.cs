using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaRecursos
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TareaRecursos : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d44d26c8-ffe7-4559-8a64-711132fb56b6");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaRecursos()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaRecursos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public int SetFinalizacion(Models.TareaRecursos oTareaRecursos)
        {
            Guid methodOwnerID = new Guid("c1999b6d-1ff4-47c5-b14e-4f098e83173c");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TareaRecursos cTareaRecursos = new DAL.TareaRecursos(cDblib);

                int result = cTareaRecursos.SetFinalizacion(oTareaRecursos);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public Models.TareaRecursos ObtenerTareaRecurso(Int32 idtarea, int nUsuario)
        {
            OpenDbConn();

            DAL.TareaRecursos cTareaRecursos = new DAL.TareaRecursos(cDblib);
            return cTareaRecursos.ObtenerTareaRecurso(idtarea, nUsuario);
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
        ~TareaRecursos()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
