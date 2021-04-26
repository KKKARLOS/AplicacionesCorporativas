using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IB.Progress.Shared;

namespace IB.Progress.BLL
{
    public class TramitarSalidas : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("b845f130-e2e4-4fd5-a15e-e54f96432dba");
        private bool disposed = false;

        #endregion

        #region Constructor

        public TramitarSalidas()
            : base()
        {
            //OpenDbConn();
        }

        public TramitarSalidas(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<string> Insert(List<int> listaProfesionales, int t001_idficepi_respdestino, string t937_comentario_resporigen)
        {
            Guid methodOwnerID = new Guid("b537a01d-9bec-4d7e-961c-63ae909caa5f");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TramitarSalidas cTramitarSalidas = new DAL.TramitarSalidas(cDblib);

                List<string> idTramitarSalidas = cTramitarSalidas.Insert(string.Join(",", listaProfesionales), t001_idficepi_respdestino, t937_comentario_resporigen);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTramitarSalidas;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(105, "No se ha podido insertar al profesional.", ex);
            }
        }


        public void Update (List<string> idpeticiones, int t001_idficepi)
        {
            Guid methodOwnerID = new Guid("b8bfa821-ca92-4a50-825a-3755b0b4ab38");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TramitarSalidas cTramitarSalidas = new DAL.TramitarSalidas(cDblib);
                cTramitarSalidas.Update(string.Join(",", idpeticiones), t001_idficepi);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(105, "No se ha podido anular la salida en trámite.", ex);
            }
        }

        public int SolicitarMediacion(int idpeticion)
        {
            Guid methodOwnerID = new Guid("c61db175-12fe-438d-b8db-0a9112d763e4");
            
            OpenDbConn();
            
            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);
            
            try
            {
                DAL.TramitarSalidas cTramitarSalidas = new DAL.TramitarSalidas(cDblib);

                int idTramitarSalidas = cTramitarSalidas.SolicitarMediacion(idpeticion);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTramitarSalidas;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(105, "No se ha podido solicitar mediación a RRHH", ex);
            }
        }


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
        ~TramitarSalidas()
        {
            Dispose(false);
        }

        #endregion


    }
}