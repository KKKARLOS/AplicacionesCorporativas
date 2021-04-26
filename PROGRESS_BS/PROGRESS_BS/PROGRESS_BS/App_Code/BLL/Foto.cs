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
/// Summary description for Foto
/// </summary>
namespace IB.Progress.BLL
{
    public class Foto : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;

        private Guid classOwnerID = new Guid("267C7F7B-EEAF-478C-BC8A-35CC1D50FCE7");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Foto()
            : base()
        {
            //OpenDbConn();
        }

        public Foto(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.Foto> Catalogo()
        {
            OpenDbConn();

            DAL.Foto cFoto = new DAL.Foto(cDblib);
            return cFoto.Catalogo();

        }

        public int Insert(int idficepi, string t932_denominacion)
        {
            Guid methodOwnerID = new Guid("60CF1DBB-CFAD-47B6-95EF-763B87C15699");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.Foto cFoto = new DAL.Foto(cDblib);

                int idFoto = cFoto.Insert(idficepi, t932_denominacion);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idFoto;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(105, "Ocurrió un error al intentar crear una foto.", ex);
            }
        }


        public void Delete(int t932_idfoto)
        {
            
            OpenDbConn();
            
            try
            {
                DAL.Foto cFoto = new DAL.Foto(cDblib);

                cFoto.Delete(t932_idfoto);

            }
            catch (Exception ex)
            {               
                throw new IBException(105, "Ocurrió un error al intentar borrar una foto.", ex);
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
        ~Foto()
        {
            Dispose(false);
        }

        #endregion


    }

}
