using System;
using System.Collections;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using System.Collections.Generic;

/// <summary>
/// Summary description for TareaCTIAP
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TareaCTIAP : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("49d2ae6d-c48c-4c19-8aa2-bd3bcb128712");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaCTIAP()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaCTIAP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.TareaCTIAP> Catalogo(int t332_idtarea, DateTime t337_fecha)
        {
            OpenDbConn();

            DAL.TareaCTIAP cTareaCTIAP = new DAL.TareaCTIAP(cDblib);
            return cTareaCTIAP.Catalogo(t332_idtarea, t337_fecha);

        }

        public ArrayList getRTPT(int t332_idtarea)
        {
            OpenDbConn();

            DAL.TareaCTIAP cTareaCTIAP = new DAL.TareaCTIAP(cDblib);
            return cTareaCTIAP.getRTPT(t332_idtarea);

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
        ~TareaCTIAP()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
