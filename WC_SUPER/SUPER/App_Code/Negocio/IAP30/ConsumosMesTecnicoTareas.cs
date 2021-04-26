using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumosMesTecnicoTareas
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumosMesTecnicoTareas : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("090a856d-224f-49a0-8290-ba9b96e079e4");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumosMesTecnicoTareas()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumosMesTecnicoTareas(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ConsumosMesTecnicoTareas> Catalogo(int t314_idusuario, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

			DAL.ConsumosMesTecnicoTareas cConsumosMesTecnicoTareas = new DAL.ConsumosMesTecnicoTareas(cDblib);
            return cConsumosMesTecnicoTareas.Catalogo(t314_idusuario, dDesde, dHasta);
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
        ~ConsumosMesTecnicoTareas()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
