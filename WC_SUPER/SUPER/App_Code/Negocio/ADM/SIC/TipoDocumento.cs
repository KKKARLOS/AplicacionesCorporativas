using System;
using System.Collections.Generic;
using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;
//Para el RegEx
using System.Text.RegularExpressions;
using System.Data;

/// <summary>
/// Summary description for TipoDocumento
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL 
{
    public class TipoDocumento : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("12039ae4-9b50-40f2-85da-73e77880fc9b");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TipoDocumento()
			: base()
        {
			//OpenDbConn();
        }
		
		public TipoDocumento(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.TipoDocumento> Catalogo()
        {
            OpenDbConn();

            DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);
            return cTipoDocumento.Catalogo();

        }

        internal List<Models.TipoDocumento> GrabarListaTiposDocumento(List<Models.TipoDocumento> lstTiposDocumento)
        {
            Guid methodOwnerID = new Guid("f672bc44-f3fc-4102-8b50-2d3452b19198");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try

            {
                //Datatable de tipos de tareas preventa
                DataTable dtTipoDocumentos = new DataTable();
                dtTipoDocumentos.Columns.Add(new DataColumn("ta211_idtipodocumento", typeof(int)));
                dtTipoDocumentos.Columns.Add(new DataColumn("ta211_denominacion", typeof(string)));
                dtTipoDocumentos.Columns.Add(new DataColumn("ta211_orden", typeof(int)));
                dtTipoDocumentos.Columns.Add(new DataColumn("ta211_activo", typeof(bool)));

                foreach (Models.TipoDocumento oTipoDocumento in lstTiposDocumento)
                {
                    DataRow rowAccion = dtTipoDocumentos.NewRow();
                    rowAccion["ta211_idtipodocumento"] = oTipoDocumento.ta211_idtipodocumento;
                    rowAccion["ta211_denominacion"] = oTipoDocumento.ta211_denominacion;
                    rowAccion["ta211_orden"] = oTipoDocumento.ta211_orden;
                    rowAccion["ta211_activo"] = oTipoDocumento.ta211_estadoactiva;
                    dtTipoDocumentos.Rows.Add(rowAccion);
                }


                DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);

                List<Models.TipoDocumento> lstTipoDocumento = cTipoDocumento.GrabarDocumentos(dtTipoDocumentos);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return lstTiposDocumento;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal Models.TipoDocumento Select(Int16 ta211_idtipodocumento)
        {
            OpenDbConn();

            DAL.TipoDocumento cTipoDocumento = new DAL.TipoDocumento(cDblib);
            return cTipoDocumento.Select(ta211_idtipodocumento);
        }

        public List<Models.TipoDocumento> Grabar(string sCadena)
        {
            //string sDesc = "";
            Models.TipoDocumento cTipoDocumento;
            List<Models.TipoDocumento> lstTiposDocumento = new List<Models.TipoDocumento>();
            bool bConTransaccion = false;
            int orden = 0;
            Guid methodOwnerID = new Guid("5562DB82-40CE-4F2E-9FE4-4261A276A6A0");
            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);

            try
            {
                string[] aFun = System.Text.RegularExpressions.Regex.Split(sCadena, "///");
                bool bActiva = false;

                foreach (string oFun in aFun)
                {
                    string[] aValores = System.Text.RegularExpressions.Regex.Split(oFun, "##");
                    //0. Opcion BD. "I", "U", "D"
                    //1. ID Cualificador
                    //2. Descripcion
                    //3. Partida
                    //4. ON
                    //5. SUPER
                    //sDesc = Utilidades.unescape(aValores[2]);

                    if (aValores[3] == "1") bActiva = true; else bActiva = false;
                    //sDesc = aValores[2];

                    if (aValores[0] != "D")
                    {
                        cTipoDocumento = new Models.TipoDocumento();
                        cTipoDocumento.ta211_idtipodocumento = short.Parse(aValores[1]);
                        cTipoDocumento.ta211_denominacion = aValores[2];
                        cTipoDocumento.ta211_orden = orden++;
                        cTipoDocumento.ta211_estadoactiva = bActiva;

                        lstTiposDocumento.Add(cTipoDocumento);

                    }

                    /*switch (aValores[0])
                    {
                        case "I":
                            this.Insert(cAccion);
                            break;
                        case "U":
                            cAccion.ta205_idtipoaccionpreventa = short.Parse(aValores[1]);
                            this.Update(cAccion);
                            break;
                        case "D":
                            this.Delete(short.Parse(aValores[1]));
                            break;
                    }*/
                }

                List<Models.TipoDocumento> lstCatalogoResultado = this.GrabarListaTiposDocumento(lstTiposDocumento);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return lstCatalogoResultado;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
            finally
            {
                this.Dispose();
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
        ~TipoDocumento()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
