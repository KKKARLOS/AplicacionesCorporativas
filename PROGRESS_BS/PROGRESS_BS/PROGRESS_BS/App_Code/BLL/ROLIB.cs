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
    public class ROLIB : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("e3ebdb65-880e-4adf-8844-7863932b4584");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ROLIB()
			: base()
        {
			//OpenDbConn();
        }
		
		public ROLIB(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
		
		public List<Models.ROLIB> Catalogo()
        {
            OpenDbConn();

			DAL.ROLIB cROLIB = new DAL.ROLIB(cDblib);
            return cROLIB.Catalogo();

        }


        public List<Models.ROLIB> CatHistoricoRoles(string t001_apellido1, string @t001_apellido2, string @t001_nombre, int desde, int hasta)
        {
            OpenDbConn();

            DAL.ROLIB historico = new DAL.ROLIB(cDblib);
            return historico.CatHistoricoRoles(t001_apellido1, t001_apellido2, t001_nombre, desde, hasta);
        }


        public int Update(List<short> listaRoles)
        {
			Guid methodOwnerID = new Guid("6118ac41-89fd-49a0-a11e-361d06575226");
			
			OpenDbConn();
			
			if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.ROLIB cROLIB = new DAL.ROLIB(cDblib);
                //Pendiente de poder pasar datatables a los procedimientos. Limitaciones del dblib.dll
                //DataTable dtAprobadores = new DataTable();
                //dtAprobadores.Columns.Add();
                //foreach (short idrol in listaRoles)
                //{
                //    dtAprobadores.Rows.Add(idrol);
                //}
                //int result = cROLIB.Update(dtAprobadores);
                int result = cROLIB.Update(string.Join(",", listaRoles));

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
				
				return result;
			}
            catch(Exception ex){
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(103, "Ocurrió un error al actualizar los roles aprobadores en base de datos.", ex);
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
        ~ROLIB()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
