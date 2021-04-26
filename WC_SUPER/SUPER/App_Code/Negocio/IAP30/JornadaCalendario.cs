using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using IB.SUPER.Shared;

/// <summary>
/// Summary description for JornadaCalendario
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class JornadaCalendario : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6bfaefb4-f207-4f27-99d9-9d2a447349e3");
        private bool disposed = false;

        #endregion

        #region Constructor

        public JornadaCalendario()
            : base()
        {
            //OpenDbConn();
        }

        public JornadaCalendario(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.JornadaCalendario> CatalogoJornadas(Int32 codUsu, Int32 codCal, Int32 mes, Int32 anno)
        {
            OpenDbConn();

            DAL.JornadaCalendario cJornadaCalendario = new DAL.JornadaCalendario(cDblib);
            return cJornadaCalendario.CatalogoJornadas(codUsu, codCal, mes, anno);

        }

        public DateTime anteriorPrimerHueco(Int32 codUsu, Int32 codCal, int UMC_IAP, string sAlta, string sBaja)
        {
            DateTime fAlta = IB.SUPER.Shared.Fechas.crearDateTime(sAlta);
            DateTime fBaja = DateTime.Now.AddYears(1);
            if (sBaja != null) fBaja = IB.SUPER.Shared.Fechas.crearDateTime(sBaja);
            DateTime fDesde = Fechas.getSigDiaUltMesCerrado(UMC_IAP);
            DateTime fHasta = fDesde.AddDays(62);
            DateTime anoMes = fDesde;
            DateTime diaAnteriorPrimerHueco = fDesde.AddDays(-1);
            bool hueco = false;

            for (int i = 0; i <= ((fHasta.Year - fDesde.Year) * 12) + fHasta.Month - fDesde.Month; i++ )
            {                
                List<Models.JornadaCalendario> lJornadas = CatalogoJornadas(codUsu, codCal, anoMes.Month, anoMes.Year);

                foreach (Models.JornadaCalendario jornada in lJornadas)
                {
                    if (jornada.dia_entero > fHasta || (jornada.esfuerzo == 0 && jornada.estilo_festivo != 1 && jornada.dia_festivo != 1 && jornada.dia_entero >= fAlta && jornada.dia_entero <= fBaja))
                    {
                        hueco = true;
                        break;
                    }
                    diaAnteriorPrimerHueco = jornada.dia_entero;
                }
                if (hueco) break;
                anoMes = anoMes.AddMonths(1);
            }

            return diaAnteriorPrimerHueco;

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
        ~JornadaCalendario()
        {
            Dispose(false);
        }

        #endregion


    }

}