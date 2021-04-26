using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using IB.Progress.Shared;
using IB.Progress.DAL;
using IB.Progress.Models;
using System.Data;


namespace IB.Progress.BLL
{
    public class ColectivoFormulario : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d268f090-46af-4335-8f02-a36e6b4ee690");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ColectivoFormulario()
            : base()
        {
            //OpenDbConn();
        }

        public ColectivoFormulario(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas



        public Models.ColectivoFormulario catalogo()
        {
            OpenDbConn();

            DAL.ColectivoFormulario cColectivoFormulario = new DAL.ColectivoFormulario(cDblib);

            return cColectivoFormulario.Catalogo();
        }


        public Models.ColectivoFormulario ColectivoForzado()
        {
            OpenDbConn();

            DAL.ColectivoFormulario cColectivoFormulario = new DAL.ColectivoFormulario(cDblib);

            return cColectivoFormulario.ColectivoForzado();
        }



        


        public int Update(int t941_idcolectivo, int t934_idmodeloformulario)
        {
            //Guid methodOwnerID = new Guid("7491758b-0f4f-4700-83a4-7b7a2f00eed0");

            OpenDbConn();

            //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ColectivoFormulario cFormulario = new DAL.ColectivoFormulario(cDblib);

                int result = cFormulario.Update(t941_idcolectivo, t934_idmodeloformulario);

                //Finalizar transacción 
                //if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                throw new IBException(103, "Ocurrió un error al actualizar pantalla de Colectivo/formulario.", ex);
            }
        }


        public void UpdateColectivoForzado(List<IB.Progress.Models.Colectivo> lista)
        {
            Guid methodOwnerID = new Guid("ba344d53-f0ab-48cd-a12f-fcb276b6debe");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ColectivoFormulario cColectivo = new DAL.ColectivoFormulario(cDblib);

                DataTable dtColectivo = new DataTable();
                dtColectivo.Columns.Add(new DataColumn("col_1", typeof(int)));
                dtColectivo.Columns.Add(new DataColumn("col_2", typeof(int)));
                dtColectivo.Columns.Add(new DataColumn("col_3", typeof(int)));

                //Recorremos la lista
                foreach (IB.Progress.Models.Colectivo oColectivo in lista)
                {

                    DataRow row = dtColectivo.NewRow();
                    row["col_1"] = oColectivo.t001_idficepi;
                    row["col_2"] = oColectivo.t941_idcolectivo;
                    row["col_3"] = DBNull.Value;

                    dtColectivo.Rows.Add(row);

                }
                //HACEMOS INSERT     
                cColectivo.UpdateColectivoForzado(dtColectivo);
                

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(103, "Ocurrió un error al actualizar pantalla mantenimiento de colectivo forzado.", ex);
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
        ~ColectivoFormulario()
        {
            Dispose(false);
        }

        #endregion


    }

}
