using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;

/// <summary>
/// Descripción breve de Usuario
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class Usuario : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("295CF181-D9B0-4D36-96E6-60521F7694BB");
        private bool disposed = false;

        #endregion


        #region Constructor
        public Usuario()
            : base()
        {
        }

        public Usuario(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
        #endregion

        
        /// <summary>
        /// Establece las variables de sesion necesarias para un usuario que viene del CRM a partir del login hermes
        /// </summary>
        /// <param name="login_hermes"></param>
        public void Autenticar(string login_hermes)
        {
            if (login_hermes.Trim().Length == 0) throw new Exception("Usuario no autorizado.");

            OpenDbConn();

            DAL.Usuario cUsuario = new DAL.Usuario(cDblib);

            Models.UsuarioCRM oUsuario = cUsuario.Autenticar(login_hermes);

            if (oUsuario.IDFICEPI_PC_ACTUAL == 0)
                throw new Exception("Usuario no autorizado.");

            //Establecer variables de sesion
            System.Web.HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"] = oUsuario.IDFICEPI_PC_ACTUAL;
            System.Web.HttpContext.Current.Session["IDFICEPI_ENTRADA"] = oUsuario.IDFICEPI_ENTRADA;
            System.Web.HttpContext.Current.Session["APELLIDO1"] = oUsuario.APELLIDO1;
            System.Web.HttpContext.Current.Session["APELLIDO2"] = oUsuario.APELLIDO2;
            System.Web.HttpContext.Current.Session["NOMBRE"] = oUsuario.NOMBRE;
            System.Web.HttpContext.Current.Session["APELLIDO1_ENTRADA"] = oUsuario.APELLIDO1;
            System.Web.HttpContext.Current.Session["APELLIDO2_ENTRADA"] = oUsuario.APELLIDO2;
            System.Web.HttpContext.Current.Session["NOMBRE_ENTRADA"] = oUsuario.NOMBRE;
            System.Web.HttpContext.Current.Session["IDRED"] = oUsuario.IDRED;
            System.Web.HttpContext.Current.Session["DES_EMPLEADO"] = oUsuario.DES_EMPLEADO_ENTRADA;
            System.Web.HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"] = oUsuario.DES_EMPLEADO_ENTRADA;

            //Asignar rol "CRM"
            if (!Roles.RoleExists("COMS")) Roles.CreateRole("COMS");
            if (!HttpContext.Current.User.IsInRole("COMS")) Roles.AddUserToRole(System.Web.HttpContext.Current.User.Identity.Name, "COMS");


        }

        public int ObtenerIdItemPorRowId(string rowid, string itemorigen) {

            OpenDbConn();

            DAL.Usuario cUsuario = new DAL.Usuario(cDblib);

            return cUsuario.ObtenerIdItemPorRowId(rowid, itemorigen);


        }

        public Models.PerfilesEdicion obtenerPerfilesEdicionUsuario(System.Security.Principal.IPrincipal User, bool soyLider, int ta201_idsubareapreventa)
        {

            OpenDbConn();

            Models.PerfilesEdicion oPE = new Models.PerfilesEdicion();

            //ficepi
            oPE.idficepi = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());

            //Lider
            oPE.soyLider = soyLider;

            //Administrador
            if (User.IsInRole("A") || User.IsInRole("SA")) oPE.soyAdministrador = true;

            //Super editor
            if (oPE.soyAdministrador || oPE.soyLider) oPE.soySuperEditor = true;

            //Figura área
            if (User.IsInRole("RAPREV") || User.IsInRole("DAPREV") || User.IsInRole("CAPREV") || User.IsInRole("IAPREV")) oPE.soyFiguraArea = true;

            //Figura subárea
            if (User.IsInRole("RSAPREV") || User.IsInRole("DSAPREV") || User.IsInRole("CSAPREV")) oPE.soyFiguraSubarea = true;

            //Figura subarea actual y posible lider
            DAL.SubareaPreventa cSubarea = new DAL.SubareaPreventa(cDblib);
            Models.SubareaPreventa oSubarea = cSubarea.Select(ta201_idsubareapreventa);

            if (oSubarea != null && oSubarea.t001_idficepi_responsable == oPE.idficepi)
                oPE.soyFiguraSubareaActual = true;

            DAL.FiguraSubareaPreventa cFSP = new DAL.FiguraSubareaPreventa(cDblib);
            List<Models.FiguraSubareaPreventa> lstFSP = cFSP.ObtenerFigurasSubareaUsuario(ta201_idsubareapreventa, oPE.idficepi);

            foreach (Models.FiguraSubareaPreventa o in lstFSP)
            {
                if (o.ta203_figura == "L")
                    oPE.soyPosibleLider = true;
                else
                    oPE.soyFiguraSubareaActual = true;
            }

            //Figura area actual
            if (oSubarea != null)
            {
                DAL.AreaPreventa cArea = new DAL.AreaPreventa(cDblib);
                Models.AreaPreventa oArea = cArea.Select(oSubarea.ta200_idareapreventa);

                if (oArea != null && oArea.t001_idficepi_responsable == oPE.idficepi)
                    oPE.soyFiguraAreaActual = true;

                DAL.FiguraAreaPreventa cFAP = new DAL.FiguraAreaPreventa(cDblib);
                List<Models.FiguraAreaPreventa> lstFAP = cFAP.ObtenerFigurasAreaUsuario(oArea.ta200_idareapreventa, oPE.idficepi);

                if (lstFAP.Count > 0)
                    oPE.soyFiguraAreaActual = true;
            }

            //comercial
            if (User.IsInRole("COMS")) oPE.soyComercial = true;

            return oPE;
        }

        public string obtenerNombreProveedor(string t314_loginhermes)
        {
            OpenDbConn();

            DAL.Usuario cUsuario = new DAL.Usuario(cDblib);
            return cUsuario.obtenerNombreComercial(t314_loginhermes);

        }

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
        ~Usuario()
        {
            Dispose(false);
        }

        #endregion
    }

}