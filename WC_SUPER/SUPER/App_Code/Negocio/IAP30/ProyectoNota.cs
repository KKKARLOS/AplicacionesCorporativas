using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoNota
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProyectoNota : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d360d02c-dfdd-441b-9e33-117763f6e701");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ProyectoNota()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProyectoNota(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ProyectoNota> Catalogo(int t314_idusuario)
        {
            OpenDbConn();

            DAL.ProyectoNota cProyectoNota = new DAL.ProyectoNota(cDblib);
            return cProyectoNota.Catalogo(t314_idusuario);

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
        ~ProyectoNota()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
