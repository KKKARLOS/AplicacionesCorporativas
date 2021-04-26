using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IB.SUPER.SIC.BLL
{
    public class Listas : IDisposable
    {

        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("DC3BDF43-B30F-461C-B6FB-6D03E59FFDD1");
        private bool disposed = false;

        public enum enumLista : byte
        {
            unidad_preventa = 1,
            area_preventa = 2,
            subarea_preventa = 3,
            tipoaccion_preventa = 4,
            tipodocumento_preventa = 5
        }

        #endregion

        #region Constructor

        public Listas()
            : base()
        {
            //OpenDbConn();
        }

        public Listas(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion


        #region Funciones públicas

        /// <summary>
        /// Obteniene una lista de valores en el formato clave-valor
        /// </summary>
        public List<IB.SUPER.APP.Models.KeyValue> GetList(enumLista lista, int? filtrarPor)
        {
            OpenDbConn();

            DAL.Listas cLista = new DAL.Listas(cDblib);

            string valueparam = "";

            switch (lista)
            {
                case enumLista.unidad_preventa:
                    valueparam = "TA199_UNIDADPREVENTA";
                    break;
                case enumLista.area_preventa:
                    valueparam = "TA200_AREAPREVENTA";
                    break;
                case enumLista.subarea_preventa:
                    valueparam = "TA201_SUBAREAPREVENTA";
                    break;
                case enumLista.tipoaccion_preventa:
                    valueparam = "TA205_TIPOACCIONPREVENTA";
                    break;
                case enumLista.tipodocumento_preventa:
                    valueparam = "TA211_TIPODOCUMENTO";
                    break;
            }

            return cLista.Select(valueparam, filtrarPor);
        }
        
        /// <summary>
        /// Obteniene una lista de valores en el formato clave-valor
        /// </summary>
        public List<IB.SUPER.APP.Models.KeyValue> GetListEstructura(enumLista lista, int? filtrarPor, string origenMenu)
        {
            OpenDbConn();

            DAL.Listas cLista = new DAL.Listas(cDblib);

            List<APP.Models.KeyValue> lst = new List<APP.Models.KeyValue>();
            List<Models.Estructura> lstaux = cLista.SelectEstructura(int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()), origenMenu);

            switch (lista)
            {
                case enumLista.unidad_preventa:
                    lst = (from e in lstaux
                           orderby e.ta199_denominacion
                           select new APP.Models.KeyValue(e.ta199_idunidadpreventa, e.ta199_denominacion))
                           .Distinct(new APP.Models.KeyValueComparer())
                           .ToList();
                    break;

                case enumLista.area_preventa:
                    lst = (from e in lstaux
                           orderby e.ta200_denominacion
                           where e.ta199_idunidadpreventa == (int)filtrarPor
                           select new APP.Models.KeyValue(e.ta200_idareapreventa, e.ta200_denominacion))
                           .Distinct(new APP.Models.KeyValueComparer())
                           .ToList();

                    break;

                case enumLista.subarea_preventa:
                    lst = (from e in lstaux
                           orderby e.ta201_denominacion
                           where e.ta200_idareapreventa == (int)filtrarPor
                           select new APP.Models.KeyValue(e.ta201_idsubareapreventa, e.ta201_denominacion))
                           .Distinct(new APP.Models.KeyValueComparer())
                           .ToList();

                    break;
            }

            return lst;
        }

        public List<Models.TipoAccionPreventa> GetListTipoAccionPreventa()
        {
            OpenDbConn();

            DAL.Listas cLista = new DAL.Listas(cDblib);

            return cLista.SelectTipoAccionPreventa();
        }

        public List<Models.TipoAccionPreventa> GetListTipoAccionFiltrada(string itemorigen, int ta206_iditemorigen)
        {
            OpenDbConn();

            DAL.Listas cLista = new DAL.Listas(cDblib);

            return cLista.SelectTipoAccionFiltrada(itemorigen, ta206_iditemorigen);
        }

        public List<Models.SubareaPreventa> GetListSubareas()
        {
            OpenDbConn();

            DAL.Listas cLista = new DAL.Listas(cDblib);

            return cLista.GetListSubareas();
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
        ~Listas()
        {
            Dispose(false);
        }

        #endregion
    }
}