using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DesgloseCalendario
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class DesgloseCalendario : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("fd1ae25f-3266-404b-85b5-6f46f6af2292");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public DesgloseCalendario()
			: base()
        {
			//OpenDbConn();
        }
		
		public DesgloseCalendario(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.DesgloseCalendario> ObtenerHoras(int nIdCal, int nAnno)
        {
            OpenDbConn();

            DAL.DesgloseCalendario cDesgloseCalendario = new DAL.DesgloseCalendario(cDblib);
            return cDesgloseCalendario.ObtenerHoras(nIdCal, nAnno);

        }

        public List<Models.DesgloseCalendario> ObtenerHorasRango(int nIdCal, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

            DAL.DesgloseCalendario cDesgloseCalendario = new DAL.DesgloseCalendario(cDblib);
            return cDesgloseCalendario.ObtenerHorasRango(nIdCal, dDesde.Date, dHasta.Date);

        }

        public List<Models.DesgloseCalendario> DetalleCalendarioSemana(int nIdCal, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

            DAL.DesgloseCalendario cDesgloseCalendario = new DAL.DesgloseCalendario(cDblib);
            return cDesgloseCalendario.DetalleCalendarioSemana(nIdCal, dDesde.Date, dHasta.Date);

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
        ~DesgloseCalendario()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
