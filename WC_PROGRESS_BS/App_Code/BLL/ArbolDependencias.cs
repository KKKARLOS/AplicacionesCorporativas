using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using IB.Progress.DAL;
using IB.Progress.Models;
using IB.Progress.Shared;
using System.Data;

/// <summary>
/// Summary description for Árbol de dependencias
/// </summary>
namespace IB.Progress.BLL
{
    public class ArbolDependencias : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6de2b1f3-71fe-4ff7-84f9-e4f1a01734f7");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ArbolDependencias()
            : base()
        {
            //OpenDbConn();
        }

        public ArbolDependencias(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.ArbolDependencias> catalogoArbolDependencias(int t001_idficepi)
        {
            OpenDbConn();

            DAL.ArbolDependencias cArbolDependencias = new DAL.ArbolDependencias(cDblib);

            return cArbolDependencias.catalogoArbolDependencias(t001_idficepi);
        }


        public List<Models.ArbolDependencias> catalogoArbolDependenciasALL(int t001_idficepi)
        {
            OpenDbConn();

            DAL.ArbolDependencias cArbolDependencias = new DAL.ArbolDependencias(cDblib);

            return cArbolDependencias.catalogoArbolDependenciasAll(t001_idficepi);
        }


        public List<Models.ArbolDependencias> SelectPotencialidad(int t001_idficepi)
        {
            OpenDbConn();

            DAL.ArbolDependencias cArbolDependencias = new DAL.ArbolDependencias(cDblib);

            return cArbolDependencias.SelectPotencialidad(t001_idficepi);
        }

        public List<Models.ArbolDependencias> SelectYO(int t001_idficepi)
        {
            OpenDbConn();

            DAL.ArbolDependencias cArbolDependencias = new DAL.ArbolDependencias(cDblib);

            return cArbolDependencias.SelectYO(t001_idficepi);
        }

        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(IB.Progress.Shared.Database.GetConStr(), classOwnerID);
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
        ~ArbolDependencias()
        {
            Dispose(false);
        }

        #endregion


    }

}
