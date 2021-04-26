using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Tecnicos
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Tecnicos : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("e136e593-17eb-4f03-a988-491f7aa04e77");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Tecnicos()
			: base()
        {
			//OpenDbConn();
        }
		
		public Tecnicos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.Tecnicos> Catalogo(Models.Tecnicos oTecnicos)
        {
            OpenDbConn();

            DAL.Tecnicos cTecnicos = new DAL.Tecnicos(cDblib);
            List<Models.Tecnicos> oLista = cTecnicos.Catalogo(oTecnicos);
            return oLista;
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
        ~Tecnicos()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
