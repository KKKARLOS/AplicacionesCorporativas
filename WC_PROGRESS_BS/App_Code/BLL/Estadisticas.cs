using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IB.Progress.Shared;


namespace IB.Progress.BLL
{
    /// <summary>
    /// Descripción breve de Estadisticas
    /// </summary>
    public class Estadisticas
    {
       
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("243d1e6b-4e5f-48a3-8fea-ef4808bb571f");
        private bool disposed = false;

        #endregion

       #region Constructor

        public Estadisticas()
            : base()
        {
            //OpenDbConn();
        }

        public Estadisticas(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        /// <summary>
        /// Obtiene los valores referentes a las estadísticas, tanto en la situación actual (t932_idfoto = null) como en una foto (t932_idfoto)
        /// </summary>
        public Models.Estadisticas Valores(Int16? t932_idfoto, Int32 desde, Int32 hasta, DateTime t001_fecantigu, short profundidad, int t001_idficepi, short? t941_idcolectivo)
        {
            OpenDbConn();

            DAL.Estadisticas estadisticas = new DAL.Estadisticas(cDblib);
           
            return estadisticas.Foto(t932_idfoto, desde, hasta, t001_fecantigu, profundidad, t001_idficepi, t941_idcolectivo);
        }


        /// <summary>
        /// Obtiene los valores referentes a las otras estadísticas (Sólo Admón)
        /// </summary>
        public Models.OtrasEstadisticas OtrosValores(Int32 desde, Int32 hasta, DateTime t001_fecantigu, short profundidad, int? t001_idficepi, short? t941_idcolectivo,
            Char estado, String t930_denominacionCR, int? t935_idcategoriaprofesional)
        {
            OpenDbConn();

            DAL.Estadisticas estadisticas = new DAL.Estadisticas(cDblib);

            return estadisticas.OtrosValores(desde, hasta, t001_fecantigu, profundidad, t001_idficepi, t941_idcolectivo, estado, t930_denominacionCR, t935_idcategoriaprofesional);
        }


        public Models.OtrasEstadisticasRRHH OtrosValoresRRHH(Int32 desde, Int32 hasta, DateTime t001_fecantigu, Char? estado, short? idcolectivo_evaluaciones, String t930_denominacionCR, int? idnodo_evaluadores, Int32 desde_colectivos, Int32 hasta_colectivos, DateTime t001_fecantigu_colectivos, int? idnodos_colectivos, int? idcolectivo_colectivos,    int? t001_idficepi)
           
        {
            OpenDbConn();

            DAL.Estadisticas estadisticas = new DAL.Estadisticas(cDblib);

            return estadisticas.OtrosValoresRRHH( desde,  hasta,  t001_fecantigu,  estado,  idcolectivo_evaluaciones, t930_denominacionCR,  idnodo_evaluadores,  desde_colectivos,  hasta_colectivos,  t001_fecantigu_colectivos, idnodos_colectivos, idcolectivo_colectivos,  t001_idficepi);
        }


        /// <summary>
        /// Obtiene el año de la valoración más antigua
        /// </summary>
        public int obtenerMinAnyoValoracion()
        {
            OpenDbConn();

            DAL.Estadisticas estadisticas = new DAL.Estadisticas(cDblib);
            return estadisticas.MinAnyoValoracion();

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
        ~Estadisticas()
        {
            Dispose(false);
        }

        #endregion

    }
}