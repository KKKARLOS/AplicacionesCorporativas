using System;
using System.Collections;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AnnoGasto
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AnnoGasto : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("da310096-244b-4cac-878f-00f0bd100b07");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AnnoGasto()
			: base()
        {
			//OpenDbConn();
        }
		
		public AnnoGasto(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas		        
        public Hashtable ObtenerHTAnnoGasto()
        {
            Hashtable htAnnoGasto = new Hashtable();
            OpenDbConn();

            DAL.AnnoGasto cConsulta = new DAL.AnnoGasto(cDblib);
            List<Models.AnnoGasto> lst = cConsulta.Catalogo();
            foreach(Models.AnnoGasto oAnnoGasto in lst)
            {
                htAnnoGasto.Add((int)(oAnnoGasto.t419_anno), new DateTime[] {
                                                                oAnnoGasto.t419_fdesde,
                                                                oAnnoGasto.t419_fhasta
                                                                });
            }
            return htAnnoGasto;
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
        ~AnnoGasto()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
