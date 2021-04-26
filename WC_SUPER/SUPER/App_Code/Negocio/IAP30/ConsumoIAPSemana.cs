using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using System.Data;
using SUPERANTIGUO = SUPER;

/// <summary>
/// Summary description for ConsumoIAPSemana
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class ConsumoIAPSemana : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("9add5c87-752d-410d-8731-89d359fabaa6");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ConsumoIAPSemana()
            : base()
        {
            //OpenDbConn();
        }

        public ConsumoIAPSemana(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas  

        public List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPSN(int nUsuario, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();
            //log4net.ILog cLog = SUPERANTIGUO.BLL.Log.logger;

            //cLog.Debug("Pasa por BLL");            

            cDblib.CommandTimeout = 120;
            DAL.ConsumoIAPSemana cConsumoIAPSemana = new DAL.ConsumoIAPSemana(cDblib);
            return cConsumoIAPSemana.ObtenerConsumosIAPSemanaPSN(nUsuario, dDesde.Date, dHasta.Date);

        }

        public List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaCompleto(int nUsuario, List<int> lPSN, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();
            //log4net.ILog cLog = SUPERANTIGUO.BLL.Log.logger;

            //cLog.Debug("Comienza a crear el dt");

            DataTable dtPSNs = new DataTable();
            dtPSNs.Columns.Add(new DataColumn("numero", typeof(int)));

            //Recorremos la lista
            foreach (int nPSN in lPSN)
            {
                DataRow row = dtPSNs.NewRow();
                row["numero"] = (int)nPSN;

                dtPSNs.Rows.Add(row);
            }
            //cLog.Debug("Termina de crear el dt");

            cDblib.CommandTimeout = 120;
            DAL.ConsumoIAPSemana cConsumoIAPSemana = new DAL.ConsumoIAPSemana(cDblib);
            return cConsumoIAPSemana.ObtenerConsumosIAPSemanaCompleto(nUsuario, dtPSNs, dDesde.Date, dHasta.Date);

        }       

        public List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPSN_D(int nUsuario, List<int> lPSN, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
        {
            OpenDbConn();
            //log4net.ILog cLog = SUPERANTIGUO.BLL.Log.logger;

            //cLog.Debug("Comienza a crear el dt");

            DataTable dtPSNs = new DataTable();
            dtPSNs.Columns.Add(new DataColumn("numero", typeof(int)));

            //Recorremos la lista
            foreach (int nPSN in lPSN)
            {
                DataRow row = dtPSNs.NewRow();
                row["numero"] = (int)nPSN;

                dtPSNs.Rows.Add(row);
            }
            //cLog.Debug("Termina de crear el dt");

            cDblib.CommandTimeout = 120;
            DAL.ConsumoIAPSemana cConsumoIAPSemana = new DAL.ConsumoIAPSemana(cDblib);
            return cConsumoIAPSemana.ObtenerConsumosIAPSemanaPSN_D(nUsuario, dtPSNs, dDesde.Date, dHasta.Date, soloPrimerNivel);

        }

        public List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPT_D(int nUsuario, int nPT, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
        {
            OpenDbConn();

            cDblib.CommandTimeout = 120;
            DAL.ConsumoIAPSemana cConsumoIAPSemana = new DAL.ConsumoIAPSemana(cDblib);
            return cConsumoIAPSemana.ObtenerConsumosIAPSemanaPT_D(nUsuario, nPT, dDesde.Date, dHasta.Date, soloPrimerNivel);

        }

        public List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaF(int nUsuario, int nFase, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
        {
            OpenDbConn();

            cDblib.CommandTimeout = 120;
            DAL.ConsumoIAPSemana cConsumoIAPSemana = new DAL.ConsumoIAPSemana(cDblib);
            return cConsumoIAPSemana.ObtenerConsumosIAPSemanaF(nUsuario, nFase, dDesde.Date, dHasta.Date, soloPrimerNivel);

        }

        public List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaA(int nUsuario, int nActividad, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

            cDblib.CommandTimeout = 120;
            DAL.ConsumoIAPSemana cConsumoIAPSemana = new DAL.ConsumoIAPSemana(cDblib);
            return cConsumoIAPSemana.ObtenerConsumosIAPSemanaA(nUsuario, nActividad, dDesde.Date, dHasta.Date);

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
        ~ConsumoIAPSemana()
        {
            Dispose(false);
        }

        #endregion


    }

}