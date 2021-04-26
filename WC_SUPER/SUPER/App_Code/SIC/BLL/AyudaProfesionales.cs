using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IB.SUPER.SIC.BLL
{
    public class AyudaProfesionales : IDisposable
    {

        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("79929FE8-1B7F-45AC-9A5A-0F5BBDF658DD");
        private bool disposed = false;

        #endregion

        #region Constructor

        public AyudaProfesionales()
            : base()
        {
        }

        public AyudaProfesionales(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion


        #region Funciones públicas

        /// <summary>
        /// Obteniene la lista de ficepi que machean los criterios de busqueda
        /// </summary>
        public List<Models.ProfesionalSimple> FicepiGeneral(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.FicepiGeneral(t001_nombre, t001_apellido1, t001_apellido2);
        }

        /// <summary>
        /// Obtiene los lideres para un subarea
        /// </summary>
        public List<Models.ProfesionalSimple> LideresSubarea(int ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.LideresSubarea(ta201_idsubareapreventa);
        }

        /// <summary>
        /// Obtiene todos los posibles lideres de las subareas activas
        /// </summary>
        public List<Models.ProfesionalSimple> Lideres(string proc, string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.Lideres(proc, t001_nombre, t001_apellido1, t001_apellido2);
        }

        public List<Models.ProfesionalSimple> LideresPreventaConAmbitoVision(string proc, string t001_nombre, string t001_apellido1, string t001_apellido2, bool admin)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.LideresPreventaConAmbitoVision(proc, t001_nombre, t001_apellido1, t001_apellido2, admin);
        }


        /// <summary>
        /// Obtiene todos los posibles lideres de las subareas activas
        /// </summary>
        public List<Models.ProfesionalSimple> LideresAmbitoVision(string t001_nombre, string t001_apellido1, string t001_apellido2, bool admin)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.LideresAmbitoVision(t001_nombre, t001_apellido1, t001_apellido2, admin);
        }


        /// <summary>
        /// Obteniene la lista de ficepi que machean los criterios de busqueda
        /// </summary>
        public List<Models.ProfesionalSimple> Comerciales(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.Comerciales(t001_nombre, t001_apellido1, t001_apellido2);
        }

        /// <summary>
        /// Obteniene la lista de ficepi que machean los criterios de busqueda
        /// </summary>
        public List<Models.ProfesionalSimple> PromotoresAccion(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.PromotoresAccion(t001_nombre, t001_apellido1, t001_apellido2);
        }

        public List<Models.ProfesionalSimple> UsuariosSuper(string idred, int t000_codigo)
        {
            OpenDbConn();

            DAL.AyudaProfesionales cAP = new DAL.AyudaProfesionales(cDblib);

            return cAP.UsuariosSuper(idred, t000_codigo);
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
        ~AyudaProfesionales()
        {
            Dispose(false);
        }

        #endregion
    }
}