using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IB.Progress.Shared;

namespace IB.Progress.BLL
{
    public class FormacionDemandada : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("fe6b38bd-7173-4d4c-b772-0bef826f753a");
        private bool disposed = false;

        #endregion

        #region Constructor

        public FormacionDemandada()
            : base()
        {
            //OpenDbConn();
        }

        public FormacionDemandada(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public Models.FormacionDemandada catFormacionDemandada(Int32 idficepi, Int32 desde, Int32 hasta, Nullable<Int16> t941_idcolectivo)
        {
            OpenDbConn();

            DAL.FormacionDemandada cFormacionDemandada = new DAL.FormacionDemandada(cDblib);

            return cFormacionDemandada.catFormacionDemandada(idficepi, desde, hasta, t941_idcolectivo);
        }

        //public List<Models.FormacionDemandada> catFormacionDemandadaExcel(Int32 idficepi, Int32 desde, Int32 hasta, Nullable<Int16> t941_idcolectivo)
        //{
        //    OpenDbConn();

        //    DAL.FormacionDemandada cFormacionDemandada = new DAL.FormacionDemandada(cDblib);

        //    return cFormacionDemandada.catFormacionDemandadaExcel(idficepi, desde, hasta, t941_idcolectivo);
        //}
        
        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Database.GetConStr(), classOwnerID);
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
        ~FormacionDemandada()
        {
            Dispose(false);
        }

        #endregion


    }
}