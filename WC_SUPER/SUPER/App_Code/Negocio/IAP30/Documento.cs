using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Documento
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Documento : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("abd04083-0d56-443d-8342-d036e0984bd5");
        private bool disposed = false;
		
		#endregion

        #region variables publicas
        public enum enumOrigenEdicion : byte
        {
            detalleTarea = 1,
            detalleAsuntoPE = 2,
            detalleAccionPE = 3,
            detalleAsuntoPT = 4,
            detalleAccionPT = 5,
            detalleAsuntoTA = 6,
            detalleAccionTA = 7
        }
        #endregion

        #region Constructor
		
        public Documento()
			: base()
        {
			//OpenDbConn();
        }
		
		public Documento(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas


        /*internal string ObtenerEstadoOrigenEdicion(enumOrigenEdicion enumProp, int idTarea)
        {

            OpenDbConn();

            int? t332_idtarea = null;

            switch (enumProp)
            {
                case enumOrigenEdicion.detalleTarea:
                    t332_idtarea = idTarea;
                    break;
                case enumOrigenEdicion.tareapreventa:
                    ta207_idtareapreventa = idpropietario;
                    break;
            }

            if (t332_idtarea != null)
            {
                DAL.DocutC3 cTP = new DAL.DocutC3(cDblib);
                return cTP.EstadoAccion((int)ta204_idaccionpreventa);

            }

            return "X"; //default (más restrictivo)

        }*/

        public List<Models.Documento> Catalogo(enumOrigenEdicion enumProp, Int32 idUsuarioAutorizado, Int32? idElemento)
        {
            OpenDbConn();

            //int? t332_idtarea = null;

            //switch (enumProp)
            //{
            //    case enumOrigenEdicion.detalleTarea:
            //        t332_idtarea = idElemento;
            //        break;                
            //}

            DAL.Documento cDocumento = new DAL.Documento(cDblib);
            return cDocumento.Catalogo(enumProp, idUsuarioAutorizado, idElemento);

        }

        internal int Insert(enumOrigenEdicion enumProp, Models.Documento oDocumento)
        {
            Guid methodOwnerID = new Guid("548b91d6-0710-4cde-9f7a-ec19de863862");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.Documento cDocumento = new DAL.Documento(cDblib);

                int idDocumento = cDocumento.Insert(enumProp, oDocumento);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idDocumento;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(enumOrigenEdicion enumProp, Models.Documento oDocumento)
        {
            Guid methodOwnerID = new Guid("fdd4b5d1-3490-4229-9177-4159f577f7dd");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.Documento cDocumento = new DAL.Documento(cDblib);

                int result = cDocumento.Update(enumProp, oDocumento);

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

        internal int Delete(enumOrigenEdicion enumProp, Int32 idDocumento)
        {
            Guid methodOwnerID = new Guid("95c245f3-cc51-43fc-85b2-5e1897869617");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.Documento cDocumento = new DAL.Documento(cDblib);

                int result = cDocumento.Delete(enumProp, idDocumento);

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

        internal Models.Documento Select(enumOrigenEdicion enumProp, Int32 idDocumento)
        {
            OpenDbConn();

            DAL.Documento cDocumento = new DAL.Documento(cDblib);
            return cDocumento.Select(enumProp, idDocumento);
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
        ~Documento()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
