using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Cualificador
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Cualificador : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("3ad019c1-edb1-465f-82ce-6bb0d39371a2");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Cualificador()
			: base()
        {
			//OpenDbConn();
        }
		
		public Cualificador(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        internal List<Models.Cualificador> Catalogo(string sTipo, Int32 t303_idnodo)
        {
            OpenDbConn();

			DAL.Cualificador cCualificador = new DAL.Cualificador(cDblib);
            return cCualificador.Catalogo(sTipo, t303_idnodo);

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
        ~Cualificador()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
