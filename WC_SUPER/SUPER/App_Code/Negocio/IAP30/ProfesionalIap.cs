using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for ProfesionalIap
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProfesionalIap : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6a77054d-9e3c-4c70-9741-62d4daa81385");
        private bool disposed = false;

        public Hashtable mapeoParametros (String tipoBusqueda, String nombre, String apellido1, String apellido2, Boolean bajas, String nodo)
        {
            Hashtable mapaParametros = new Hashtable();
            switch (tipoBusqueda)
            {
                case "PROFESIONALES":
                    mapaParametros.Add("num_empleado", Int32.Parse(HttpContext.Current.Session["UsuarioActual"].ToString()));
                    mapaParametros.Add("sPerfil", HttpContext.Current.Session["perfil_iap"].ToString());
                    mapaParametros.Add("sApellido1", apellido1);
                    mapaParametros.Add("sApellido2", apellido2);
                    mapaParametros.Add("sNombre", nombre);
                    mapaParametros.Add("idficepi", Int32.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()));
                    mapaParametros.Add("bForaneos", true);
                    break;

                case "RESPONSABLES_PROYECTO":
                    mapaParametros.Add("sAp1", apellido1);
                    mapaParametros.Add("sAp2", apellido2);
                    mapaParametros.Add("sNombre", nombre);
                    mapaParametros.Add("bMostrarBajas", bajas);
                  
                    break;

                case "PROFESIONALES_AGENDA":
                    mapaParametros.Add("num_empleado", Int32.Parse(HttpContext.Current.Session["UsuarioActual"].ToString()));
                    mapaParametros.Add("sPerfil", "A");
                    mapaParametros.Add("sApellido1", apellido1);
                    mapaParametros.Add("sApellido2", apellido2);
                    mapaParametros.Add("sNombre", nombre);
                    mapaParametros.Add("idficepi", Int32.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()));
                    break;
                case "USUARIOS":
                    mapaParametros.Add("sAp1", apellido1);
                    mapaParametros.Add("sAp2", apellido2);
                    mapaParametros.Add("sNombre", nombre);
                    mapaParametros.Add("bMostrarBajas", bajas);
                    mapaParametros.Add("t303_idnodo", null);
                    mapaParametros.Add("bForaneos", true);
                    break;
            }

            return mapaParametros;
        }
		
		#endregion
		
        #region Constructor
		
        public ProfesionalIap()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProfesionalIap(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ProfesionalIap> Catalogo(String tipoBusqueda, String nombre, String apellido1, String apellido2, Boolean bajas, String nodo)
        {
            Hashtable mapaParametros = new Hashtable();

            OpenDbConn();
            mapaParametros = mapeoParametros(tipoBusqueda, nombre, apellido1, apellido2, bajas, nodo);
            DAL.ProfesionalIap cProfesionalIap = new DAL.ProfesionalIap(cDblib);
            return cProfesionalIap.Catalogo(tipoBusqueda, mapaParametros);
            
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
        ~ProfesionalIap()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
