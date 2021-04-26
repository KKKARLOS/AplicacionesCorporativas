using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.Progress.Shared
{
    /// <summary>
    /// Descripción breve de DbConn
    /// </summary>
    public class DbConn
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("4A5F7C59-7E84-4F3C-ABB0-E1A0E43A293E");
        private bool disposed = false;

        #endregion
        
        #region constructor

        public DbConn()
            : base()
        {
            OpenDbConn();
        }

        #endregion

        #region dblib pública

        public sqldblib.SqlServerSP dblibclass {
            get { return cDblib; }
        }

        #endregion

        #region Conexion base de datos y dispose

        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);

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

        ~DbConn()
        {
            Dispose(false);
        }
		
        #endregion
    }
}