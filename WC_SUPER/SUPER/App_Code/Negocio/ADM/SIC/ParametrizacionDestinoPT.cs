using System;
using System.Collections.Generic;


using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for ParametrizacionDestinoPT
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
{
    public class ParametrizacionDestinoPT : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("0bb1f855-c74e-43c5-be1c-1e164fe88824");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ParametrizacionDestinoPT()
            : base()
        {
            //OpenDbConn();
        }

        public ParametrizacionDestinoPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.ParametrizacionDestinoPT> Catalogo(Nullable<bool> bSoloActivas, Nullable<int> idOC,
                                                              Nullable<int> idFicepi, bool bMostrarProfBaja)
        {
            OpenDbConn();

            DAL.ParametrizacionDestinoPT cParametrizacionDestinoPT = new DAL.ParametrizacionDestinoPT(cDblib);
            return cParametrizacionDestinoPT.Catalogo(bSoloActivas, idOC, idFicepi, bMostrarProfBaja);

        }

        public List<Models.ParametrizacionDestinoPT> CatPrametrizaciones()
        {
            OpenDbConn();

            DAL.ParametrizacionDestinoPT cParametrizacionDestinoPT = new DAL.ParametrizacionDestinoPT(cDblib);
            return cParametrizacionDestinoPT.CatParametrizaciones();

        }

        internal int Insert(Models.ParametrizacionDestinoPT oParametrizacionDestinoPT)
        {
            Guid methodOwnerID = new Guid("e3ca5a92-252e-44cb-a268-e5ace5bb892d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.ParametrizacionDestinoPT cParametrizacionDestinoPT = new DAL.ParametrizacionDestinoPT(cDblib);

                int idParametrizacionDestinoPT = cParametrizacionDestinoPT.Insert(oParametrizacionDestinoPT);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idParametrizacionDestinoPT;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        //public int Update(Models.ParametrizacionDestinoPT oParametrizacionDestinoPT)
        //{
        //    Guid methodOwnerID = new Guid("b04e7dda-c769-4161-bb55-77b2bf02e057");

        //    OpenDbConn();

        //    if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

        //    try
        //    {
        //        DAL.ParametrizacionDestinoPT cParametrizacionDestinoPT = new DAL.ParametrizacionDestinoPT(cDblib);

        //        int result = cParametrizacionDestinoPT.Update(oParametrizacionDestinoPT);

        //        //Finalizar transacción 
        //        if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        //rollback
        //        if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

        //        throw ex;
        //    }
        //}

        internal int Delete(Int32 ta212_idorganizacioncomercial, Int32 t001_idficepi_comercial)
        {
            Guid methodOwnerID = new Guid("47203473-5d52-4309-9ce9-445eab498858");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ParametrizacionDestinoPT cParametrizacionDestinoPT = new DAL.ParametrizacionDestinoPT(cDblib);

                int result = cParametrizacionDestinoPT.Delete(ta212_idorganizacioncomercial, t001_idficepi_comercial);

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

        //internal Models.ParametrizacionDestinoPT Select(Int32 ta212_idorganizacioncomercial, Int32 t001_idficepi_comercial)
        //{
        //    OpenDbConn();

        //    DAL.ParametrizacionDestinoPT cParametrizacionDestinoPT = new DAL.ParametrizacionDestinoPT(cDblib);
        //    return cParametrizacionDestinoPT.Select(ta212_idorganizacioncomercial, t001_idficepi_comercial);
        //}


        public void GrabarLista(List<Models.ParametrizacionDestinoPT> Lista)
        {
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("E465FE3F-8D65-4169-B50D-886F3C1B10E2");
            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);

            DAL.ParametrizacionDestinoPT bDestinoPT = new DAL.ParametrizacionDestinoPT(cDblib);
            try
            {
                foreach (Models.ParametrizacionDestinoPT oElem in Lista)
                {
                    switch (oElem.bd)
                    {
                        case "I":
                            bDestinoPT.Insert(oElem);
                            break;
                        case "U":
                            bDestinoPT.Update(oElem);
                            break;
                        case "D":
                            bDestinoPT.Delete(oElem.ta212_idorganizacioncomercial, oElem.t001_idficepi_comercial);
                            break;
                    }
                }
                if (bConTransaccion)
                    cDblib.commitTransaction(methodOwnerID);
            }
            catch (Exception ex)
            {
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);
                throw new Exception(ex.Message);
            }
            finally
            {
                //bDestinoPT.Dispose();
            }
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
        ~ParametrizacionDestinoPT()
        {
            Dispose(false);
        }

        #endregion


    }

}
