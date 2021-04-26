using System.Data.SqlClient;
using System.Data;
using System;
using IB.Progress.Shared;
using System.Web.Security;
using System.Web;
using System.Collections.Generic;

namespace IB.Progress.BLL
{
    public class Profesional : IDisposable
    {
        #region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("e3ebdb65-880e-4adf-8844-1863932b4989");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Profesional()
			: base()
        {
			//OpenDbConn();
        }
		
		public Profesional(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
        #endregion

        #region Funciones públicas

        public Models.Profesional Obtener(string t001_codred)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);
            return cProfesional.ObtenerDatosLogin(t001_codred);
        }

        public Models.Aplicacion Acceso()
        {
            OpenDbConn();

            DAL.Aplicacion cAplicacion = new DAL.Aplicacion(cDblib);
            return cAplicacion.Acceso();
        }

        public List<Models.Profesional> getSeleccionarEvaluador(int t001_evaluado_actual, int t001_evaluador_actual, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getSeleccionarEvaluador(t001_evaluado_actual, t001_evaluador_actual, t001_apellido1, t001_apellido2, t001_nombre);
        }


        public List<Models.Profesional> getFicepi(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.ObtenerFicepi(t001_apellido1, t001_apellido2, t001_nombre);
        }


        public List<Models.Profesional> getFicepi_Evaluadordestino(int t001_idficepi_interesado, int t001_idficepi_evaluadoractual, string  t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getFicepi_Evaluadordestino(t001_idficepi_interesado, t001_idficepi_evaluadoractual, t001_apellido1, t001_apellido2, t001_nombre);
        }

        public Models.Profesional validaProgress(int t001_idficepi, int t001_evalprogress)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.validaProgress(t001_idficepi, t001_evalprogress);
            
        }

       
        public void CargarRoles(Models.Profesional oProf)
        {
            #region Creación dinámica de roles
            //Roles Progress
            if (!Roles.RoleExists("SADM")) Roles.CreateRole("SADM");
            if (!Roles.RoleExists("AADM")) Roles.CreateRole("AADM");
            if (!Roles.RoleExists("PBAS")) Roles.CreateRole("PBAS");
            if (!Roles.RoleExists("PEVA")) Roles.CreateRole("PEVA");
            if (!Roles.RoleExists("PAPR")) Roles.CreateRole("PAPR");
            if (!Roles.RoleExists("PPFE")) Roles.CreateRole("PPFE");
            if (!Roles.RoleExists("PVIS")) Roles.CreateRole("PVIS");
            if (!Roles.RoleExists("REC")) Roles.CreateRole("REC");
            #endregion

            //Se borran los roles que pudiera tener el usuario.
            foreach (string Rol in Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name))
            {
                Roles.RemoveUserFromRole(HttpContext.Current.User.Identity.Name, Rol);
            }
            
            #region Asignar Roles
            OpenDbConn();

            List<string> rolesProfesional = new List<string>();
            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);
            rolesProfesional = cProfesional.ObtenerRoles(oProf.t001_idficepi);


            foreach (string rol in rolesProfesional) {
                if (!HttpContext.Current.User.IsInRole(rol))
                {
                    Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, rol);
                }
            }

            if (HttpContext.Current.Session["PROFESIONAL_ENTRADA"] != null && HttpContext.Current.Session["PROFESIONAL"] != HttpContext.Current.Session["PROFESIONAL_ENTRADA"])
            {
                Models.Profesional oProfEntrada = (Models.Profesional)HttpContext.Current.Session["PROFESIONAL_ENTRADA"];
                if (oProfEntrada.roles.Contains("REC"))
                {                    
                    if (!HttpContext.Current.User.IsInRole("REC")) Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "REC");
                }
            }
            
            HttpContext.Current.Session["ROLES"] = rolesProfesional;
            oProf.roles = rolesProfesional;

            #endregion
        }


        public List<Models.Profesional> FICEVALUADORESDEPENDIENTES(int idficepi)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.FICEVALUADORESDEPENDIENTES(idficepi);
        }

        public List<Models.Profesional> getEvaluadores(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.ObtenerEvaluadores(t001_apellido1, t001_apellido2, t001_nombre);
        }


        public List<Models.Profesional> getEvaluadoresEstadisticas(int t001_idficepi, string perfilApl, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.ObtenerEvaluadoresEstadisticas(t001_idficepi, perfilApl, t001_apellido1, t001_apellido2, t001_nombre);
        }


        public List<Models.Profesional> getFic(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getFic(t001_apellido1, t001_apellido2, t001_nombre);
        }


        public List<Models.Profesional> getFicProfesionales(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getFicProfesionales(t001_apellido1, t001_apellido2, t001_nombre);
        }



        public List<Models.Profesional> getEvaluadosDeMiEquipo(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getEvaluadosDeMiEquipo(idficepi, t001_apellido1, t001_apellido2, t001_nombre);
        }


        public List<Models.Profesional> getEvaluadoresDeMiEquipo(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getEvaluadoresDeMiEquipo(idficepi, t001_apellido1, t001_apellido2, t001_nombre);
        }

        public List<Models.Profesional> getFicProfesionales_Reconexion(int idficepi_entrada, int idficepi_encurso, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getFicProfesionales_Reconexion(idficepi_entrada,idficepi_encurso, t001_apellido1, t001_apellido2, t001_nombre);
        }
       
        public List<Models.Profesional> getProfesionalesVisualizadores(int t001_idficepi_visualizador)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getProfesionalesVisualizadores(t001_idficepi_visualizador);
        }


        public List<Models.Profesional> getProfesionalesVisualizadores2(int t001_idficepi_visualizador)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getProfesionalesVisualizadores2(t001_idficepi_visualizador);
        }

        public List<Models.Profesional> EvaluadoresArbol(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.EvaluadoresArbol(idficepi, t001_apellido1, t001_apellido2, t001_nombre);
        }

        public List<Models.Profesional> getEvaluadoresDescendientes(int idficepi)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getEvaluadoresDescendientes(idficepi);
        }

        public List<Models.Profesional> getDescendientes(int t001_idficepi, string perfilApl, string t001_apellido1, string t001_apellido2, string t001_nombre, short evaluadoroevaluado)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.ObtenerDescendientes(t001_idficepi, perfilApl, t001_apellido1, t001_apellido2, t001_nombre, evaluadoroevaluado);
        }

        public List<Models.Profesional> getAscendientesHastaAprobador(int t001_idficepi)
        {
            OpenDbConn();

            DAL.Profesional cProfesional = new DAL.Profesional(cDblib);

            return cProfesional.getAscendientesHastaAprobador(t001_idficepi);
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
        ~Profesional()
        {
            Dispose(false);
        }
        #endregion
    }
}