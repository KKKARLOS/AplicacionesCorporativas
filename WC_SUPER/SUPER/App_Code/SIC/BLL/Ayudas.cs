using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IB.SUPER.SIC.BLL
{
    public class Ayudas
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("BAF46DA5-A612-48AE-89CC-6FAE4629A9A7");
        private bool disposed = false;

        public enum enumAyuda : byte
        {
            cuentasCRM = 1,
            accionesPreventa = 2,

            SIC_AYUDA1UNIDADESPREVENTA_CAT = 20,
            SIC_AYUDA1AREASPREVENTA_CAT = 21,
            SIC_AYUDA1SUBAREASPREVENTA_CAT = 22,
            SIC_AYUDA1TEMORIGEN_O_CAT = 23,
            SIC_AYUDA1TEMORIGEN_E_CAT = 24,
            SIC_AYUDA1TEMORIGEN_P_CAT = 25,
            SIC_AYUDA1TEMORIGEN_S_CAT = 26,

            SIC_AYUDA2UNIDADESPREVENTA_CAT = 30,
			SIC_AYUDA2AREASPREVENTA_CAT = 31,
            SIC_AYUDA2SUBAREASPREVENTA_CAT = 32,
            SIC_AYUDA2TEMORIGEN_O_CAT = 33,
            SIC_AYUDA2TEMORIGEN_E_CAT = 34,
            SIC_AYUDA2TEMORIGEN_P_CAT = 35,
            SIC_AYUDA2TEMORIGEN_S_CAT = 36,

            SIC_AYUDA3UNIDADESPREVENTA_CAT = 40,
            SIC_AYUDA3AREASPREVENTA_CAT = 41,
            SIC_AYUDA3SUBAREASPREVENTA_CAT = 42,
            SIC_AYUDA3ITEMORIGEN_O_CAT = 43,
            SIC_AYUDA3ITEMORIGEN_E_CAT = 44,
            SIC_AYUDA3ITEMORIGEN_P_CAT = 45,

            SIC_AYUDA4UNIDADESPREVENTA_CAT = 46,
            SIC_AYUDA4AREASPREVENTA_CAT = 47,
            SIC_AYUDA4SUBAREASPREVENTA_CAT = 48
            

        }

        #endregion

        #region Constructor

        public Ayudas()
            : base()
        {
            //OpenDbConn();
        }

        public Ayudas(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
        #endregion


        #region Funciones públicas

        /// <summary>
        /// Obteniene una lista de valores en el formato clave-valor
        /// </summary>
        public List<IB.SUPER.APP.Models.KeyValue2> Buscar(enumAyuda ayuda, string filtro, bool admin)
        {
            OpenDbConn();

            DAL.Ayudas cAyudas = new DAL.Ayudas(cDblib);

            switch (ayuda)
            {
                case enumAyuda. SIC_AYUDA1UNIDADESPREVENTA_CAT:                
                case enumAyuda. SIC_AYUDA1AREASPREVENTA_CAT:
                case enumAyuda. SIC_AYUDA1SUBAREASPREVENTA_CAT:
                case enumAyuda. SIC_AYUDA1TEMORIGEN_O_CAT:
                case enumAyuda. SIC_AYUDA1TEMORIGEN_E_CAT:
                case enumAyuda. SIC_AYUDA1TEMORIGEN_P_CAT:
                case enumAyuda. SIC_AYUDA1TEMORIGEN_S_CAT:

                case enumAyuda. SIC_AYUDA2UNIDADESPREVENTA_CAT:
                case enumAyuda. SIC_AYUDA2AREASPREVENTA_CAT:
                case enumAyuda. SIC_AYUDA2SUBAREASPREVENTA_CAT:
                case enumAyuda. SIC_AYUDA2TEMORIGEN_O_CAT:
                case enumAyuda. SIC_AYUDA2TEMORIGEN_E_CAT:
                case enumAyuda. SIC_AYUDA2TEMORIGEN_P_CAT:
                case enumAyuda. SIC_AYUDA2TEMORIGEN_S_CAT:

                case enumAyuda.SIC_AYUDA4UNIDADESPREVENTA_CAT:
                case enumAyuda.SIC_AYUDA4AREASPREVENTA_CAT:
                case enumAyuda.SIC_AYUDA4SUBAREASPREVENTA_CAT:
                

                
                case enumAyuda. SIC_AYUDA3ITEMORIGEN_O_CAT:
                case enumAyuda. SIC_AYUDA3ITEMORIGEN_E_CAT:
                case enumAyuda. SIC_AYUDA3ITEMORIGEN_P_CAT:

                    return cAyudas.BuscarConFicepi(ayuda, filtro, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], admin);
                    break;


                case enumAyuda .SIC_AYUDA3UNIDADESPREVENTA_CAT:
                case enumAyuda .SIC_AYUDA3AREASPREVENTA_CAT:
                case enumAyuda .SIC_AYUDA3SUBAREASPREVENTA_CAT:

                    return cAyudas.Buscar(ayuda, filtro);
                    break;

                case enumAyuda.cuentasCRM:
                case enumAyuda.accionesPreventa:

                    return cAyudas.Buscar(ayuda, filtro);
                    break;


                default:
                    return new List<APP.Models.KeyValue2>();

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
        ~Ayudas()
        {
            Dispose(false);
        }

        #endregion
    }
}
