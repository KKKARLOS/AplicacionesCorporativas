using System;
using System.Collections.Generic;

using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Descripción breve de ConsultaFacturabilidad
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsultaFacturabilidad
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("1113B3E6-387C-4971-BF00-2D5BAAA4B830");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsultaFacturabilidad()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsultaFacturabilidad(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ConsultaFacturabilidad> Catalogo(int t001_idficepi, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

			DAL.ConsultaFacturabilidad cConsultaFacturabilidad = new DAL.ConsultaFacturabilidad(cDblib);
            return cConsultaFacturabilidad.Catalogo(t001_idficepi, dDesde, dHasta);
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
        ~ConsultaFacturabilidad()
        {
            Dispose(false);
        }
		
        #endregion
    }
}