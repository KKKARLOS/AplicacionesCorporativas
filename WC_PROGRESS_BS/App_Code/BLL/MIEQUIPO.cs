using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using IB.Progress.Shared;
using IB.Progress.DAL;
using IB.Progress.Models;
using System.Data;

/// <summary>
/// Summary description for ROLIB
/// </summary>
namespace IB.Progress.BLL
{
    public class MIEQUIPO : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("92698c29-3768-466d-8c74-a12ffa212851");
        private bool disposed = false;

        #endregion

        #region Constructor

        public MIEQUIPO()
            : base()
        {
            //OpenDbConn();
        }

        public MIEQUIPO(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public Models.MIEQUIPO Catalogo(int t001_idficepi)
        {
            OpenDbConn();

            DAL.MIEQUIPO miequipo = new DAL.MIEQUIPO(cDblib);
            return miequipo.Catalogo(t001_idficepi);
        }

        public Models.MIEQUIPO CatalogoConfirmarMiequipo(int t001_idficepi)
        {
            OpenDbConn();

            DAL.MIEQUIPO miequipo = new DAL.MIEQUIPO(cDblib);
            return miequipo.CatalogoConfirmarMiequipo(t001_idficepi);
        }

        

        public Models.MIEQUIPO CatalogoAbrirEvaluacion(int t001_idficepi)
        {
            OpenDbConn();

            DAL.MIEQUIPO miequipo = new DAL.MIEQUIPO(cDblib);
            return miequipo.CatalogoAbrirEvaluacion(t001_idficepi);
        }


        public Models.MIEQUIPO IncorporacionesCAT(int t001_idficepi)
        {
            OpenDbConn();

            DAL.MIEQUIPO miequipo = new DAL.MIEQUIPO(cDblib);
            return miequipo.IncoporacionesCAT(t001_idficepi);
        }


        public List<Models.MIEQUIPO.profPendEval> CatEvalPend(int t001_idficepi)
        {
            OpenDbConn();

            DAL.MIEQUIPO miequipo = new DAL.MIEQUIPO(cDblib);
            return miequipo.CatEvalPend(t001_idficepi);
        }

        public List<Models.MIEQUIPO.profesional_CRol> CatCambioRol(int t001_idficepi)
        {
            OpenDbConn();

            DAL.MIEQUIPO miequipo = new DAL.MIEQUIPO(cDblib);
            return miequipo.CatCambioRol(t001_idficepi);
        }

        public int Update(int t001_idficepi)
        {
            Guid methodOwnerID = new Guid("766e8f17-07e4-474c-8502-972905aa1d1c");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.MIEQUIPO miequipo = new DAL.MIEQUIPO(cDblib);
                int result = miequipo.Update(t001_idficepi);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(104, "Ocurrió un error al intentar confirmar el equipo en base de datos.", ex);
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
        ~MIEQUIPO()
        {
            Dispose(false);
        }

        #endregion


    }

}
