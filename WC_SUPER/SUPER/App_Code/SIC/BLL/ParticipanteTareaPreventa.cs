using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;
using System.Web;

/// <summary>
/// Summary description for ParticipanteTareaPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class ParticipanteTareaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("a510ed34-e8f8-43b9-91ca-c37b13a43de7");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ParticipanteTareaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public ParticipanteTareaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        internal List<Models.ParticipanteTareaPreventa> Catalogo(Models.ParticipanteTareaPreventa oParticipanteTareaPreventaFilter)
        {
            OpenDbConn();

            DAL.ParticipanteTareaPreventa cParticipanteTareaPreventa = new DAL.ParticipanteTareaPreventa(cDblib);
            return cParticipanteTareaPreventa.Catalogo(oParticipanteTareaPreventaFilter);

        }

        internal int Insert(Models.ParticipanteTareaPreventa oParticipanteTareaPreventa)
        {
            Guid methodOwnerID = new Guid("521885ca-75bf-471c-af33-813d4b5dc803");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.ParticipanteTareaPreventa cParticipanteTareaPreventa = new DAL.ParticipanteTareaPreventa(cDblib);

                int idParticipanteTareaPreventa = cParticipanteTareaPreventa.Insert(oParticipanteTareaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idParticipanteTareaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public int Update(Models.ParticipanteTareaPreventa oParticipanteTareaPreventa)
        {
            Guid methodOwnerID = new Guid("2b6e2ba1-fe0f-4bff-a3ae-d03595b16f5b");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.ParticipanteTareaPreventa cParticipanteTareaPreventa = new DAL.ParticipanteTareaPreventa(cDblib);

                int result = cParticipanteTareaPreventa.Update(oParticipanteTareaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Delete(Int32 ta207_idtareapreventa, Int32 t001_idficepi_participante)
        {
            Guid methodOwnerID = new Guid("1207c024-c823-4158-8b84-9dc7c4bce1fe");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ParticipanteTareaPreventa cParticipanteTareaPreventa = new DAL.ParticipanteTareaPreventa(cDblib);

                int result = cParticipanteTareaPreventa.Delete(ta207_idtareapreventa, t001_idficepi_participante);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal Models.ParticipanteTareaPreventa Select(Int32 ta207_idtareapreventa, Int32 t001_idficepi_participante)
        {
            OpenDbConn();

            DAL.ParticipanteTareaPreventa cParticipanteTareaPreventa = new DAL.ParticipanteTareaPreventa(cDblib);
            return cParticipanteTareaPreventa.Select(ta207_idtareapreventa, t001_idficepi_participante);
        }

        public List<Models.TareaCatRequestFilter> CatalogoHistoricoParticipante(TareaCatHistoricoFilter rf)
        {
            OpenDbConn();


            DAL.ParticipanteTareaPreventa cAccionPreventa = new DAL.ParticipanteTareaPreventa(cDblib);
            return cAccionPreventa.CatParticipanteTareaPreventa((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], rf);
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
        ~ParticipanteTareaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
