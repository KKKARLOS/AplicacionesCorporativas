using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using IB.Progress.DAL;
using IB.Progress.Models;
using IB.Progress.Shared;
using System.Data;


namespace IB.Progress.BLL
{
    public class VisionesAjenasArbol : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6a0c08e6-9222-49a1-b4dd-ab169cb20ed6");
        private bool disposed = false;

        #endregion

        #region Constructor

        public VisionesAjenasArbol()
            : base()
        {
            //OpenDbConn();
        }

        public VisionesAjenasArbol(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.VisionesAjenasArbol> catalogoVisionesAjenasArbol(string tabla, Nullable<int> t001_idficepi_visualizador)
        {
            OpenDbConn();

            DAL.VisionesAjenasArbol cVisionesAjenasArbol = new DAL.VisionesAjenasArbol(cDblib);

            return cVisionesAjenasArbol.catalogoVisionesAjenasArbol(tabla, t001_idficepi_visualizador);
        }

        public List<Models.VisionesAjenasArbol> catalogoVisionesAjenasArbol2(string tabla, Nullable<int> t001_idficepi_visualizador)
        {
            OpenDbConn();

            DAL.VisionesAjenasArbol cVisionesAjenasArbol = new DAL.VisionesAjenasArbol(cDblib);

            return cVisionesAjenasArbol.catalogoVisionesAjenasArbol2(tabla, t001_idficepi_visualizador);
        }

        public void DeleteVisualizador(int idficepiVisualizador)
        {

            OpenDbConn();

            try
            {                
                DAL.VisionesAjenasArbol cVision = new DAL.VisionesAjenasArbol(cDblib);

                cVision.Delete(idficepiVisualizador, null, "A");
                
            }
            catch (Exception ex)
            {
                throw new IBException(105, "Ocurrió un error al intentar borrar un visualizador.", ex);
            }
        }


        public void Delete(int idficepiVisualizador)
        {

            OpenDbConn();

            try
            {
                DAL.VisionesAjenasArbol cVision = new DAL.VisionesAjenasArbol(cDblib);

                cVision.Delete(idficepiVisualizador, null, "B");

            }
            catch (Exception ex)
            {
                throw new IBException(105, "Ocurrió un error al intentar borrar un visualizador.", ex);
            }
        }


        /// <summary>
        /// Operaciones CRUD 
        /// </summary>
        /// <param name="oCSV"></param>
        public void MantenerCatalogo(string tabla, List<IB.Progress.Models.VisionesAjenasArbol> oVisualizadores)
        {
            Guid methodOwnerID = new Guid("e93c88d6-8698-4cdb-a28d-416fe3d01648");
            OpenDbConn();
            if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);
            //Begin Transaction
          
            try
            {
                DAL.VisionesAjenasArbol cArbol= new DAL.VisionesAjenasArbol(cDblib);
                int idficepi_visualizador = 0;
                DataTable dtVisualizados = new DataTable();
                dtVisualizados.Columns.Add(new DataColumn("visualizado", typeof(int)));
                dtVisualizados.Columns.Add(new DataColumn("accion", typeof(char)));
               
                //Recorremos la lista
                foreach (IB.Progress.Models.VisionesAjenasArbol oVisualizador in oVisualizadores)
                {

                    DataRow row = dtVisualizados.NewRow();
                    row["visualizado"] = oVisualizador.Idficepi_visualizado;
                    if (oVisualizador.T949_accion != null)
                        row["accion"] = oVisualizador.T949_accion;

                    dtVisualizados.Rows.Add(row);
                                   
                    idficepi_visualizador = oVisualizador.Idficepi_visualizador;

                }                
                //HACEMOS INSERT     
                cArbol.Insert(tabla, idficepi_visualizador, dtVisualizados);
                
                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
               
            }
           
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(106, "Ocurrió un error al intentar grabar a los visualizadores.", ex);
            }
       

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
        ~VisionesAjenasArbol()
        {
            Dispose(false);
        }

        #endregion


    }

}
