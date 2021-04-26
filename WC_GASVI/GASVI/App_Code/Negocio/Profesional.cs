using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security; //para gestion de roles
using GASVI.DAL;
using Microsoft.JScript;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace GASVI.BLL
{
	public partial class Profesional
    {
        #region Propiedades

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private bool _bIdentificado;
        public bool bIdentificado
        {
            get { return _bIdentificado; }
            set { _bIdentificado = value; }
        }

        private bool _bNuevoGasvi;
        public bool bNuevoGasvi
        {
            get { return _bNuevoGasvi; }
            set { _bNuevoGasvi = value; }
        }

        private bool _bMultiUsuario;
        public bool bMultiUsuario
        {
            get { return _bMultiUsuario; }
            set { _bMultiUsuario = value; }
        }

        private bool _bAdministrador;
        public bool bAdministrador
        {
            get { return _bAdministrador; }
            set { _bAdministrador = value; }
        }

        private int _t313_idempresa_defecto;
        public int t313_idempresa_defecto
        {
            get { return _t313_idempresa_defecto; }
            set { _t313_idempresa_defecto = value; }
        }

        private string _Empresa_defecto;
        public string Empresa_defecto
        {
            get { return _Empresa_defecto; }
            set { _Empresa_defecto = value; }
        }

        private int _t313_idempresa;
        public int t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private string _Empresa;
        public string Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }

        private string _Sexo;
        public string Sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; }
        }

        private bool _t001_logomovil_gasvi;
        public bool t001_logomovil_gasvi
        {
            get { return _t001_logomovil_gasvi; }
            set { _t001_logomovil_gasvi = value; }
        }

        private bool _t001_avisocambioes;
        public bool t001_avisocambioes
        {
            get { return _t001_avisocambioes; }
            set { _t001_avisocambioes = value; }
        }

        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }

        private byte? _t423_idmotivo;
        public byte? t423_idmotivo
        {
            get { return _t423_idmotivo; }
            set { _t423_idmotivo = value; }
        }

        private int _nCCIberper;
        public int nCCIberper
        {
            get { return _nCCIberper; }
            set { _nCCIberper = value; }
        }

        private int _nFilasFicepi;
        public int nFilasFicepi
        {
            get { return _nFilasFicepi; }
            set { _nFilasFicepi = value; }
        }

        #endregion

        public Profesional()
		{
			
		}

        public static Profesional Obtener(string IDRED)
        {
            Profesional oP = new Profesional();
            SqlDataReader dr = DAL.Profesional.ObtenerDatosLogin(IDRED);

            int nCountFicepi = 0;
            while (dr.Read())
            {
                oP.bIdentificado = true;
                oP.t001_idficepi = (int)dr["t001_idficepi"];
                oP.t314_idusuario = (int)dr["t314_idusuario"];
                oP.Nombre = dr["Profesional"].ToString();
                oP.bNuevoGasvi = ((int)dr["nuevogasvi"]==1)? true:false;
                oP.bMultiUsuario = ((int)dr["multiusuario"] == 1) ? true : false;
                oP.bAdministrador = ((int)dr["ADM"] == 1) ? true : false;
                //oP.t313_idempresa_defecto = (dr["t313_idempresa_defecto"] == DBNull.Value) ? 0 : int.Parse(dr["t313_idempresa_defecto"].ToString());
                oP.t313_idempresa_defecto = int.Parse(dr["idEmpresaDefecto"].ToString());
                oP.t313_idempresa = (dr["t313_idempresa"] == DBNull.Value) ? 0 : int.Parse(dr["t313_idempresa"].ToString());
                oP.Empresa = dr["Empresa"].ToString();
                oP.Sexo = dr["t001_sexo"].ToString();
                oP.t001_avisocambioes = (bool)dr["avisocambioestado"];
                oP.t422_idmoneda = dr["t422_idmoneda"].ToString();
                if (dr["t423_idmotivo"] != DBNull.Value)
                    oP.t423_idmotivo = (byte)dr["t423_idmotivo"];
                oP.nCCIberper = (int)dr["CentrosCoste"];
                nCountFicepi++;
            }
            dr.Close();
            dr.Dispose();

            oP.nFilasFicepi = nCountFicepi;

            //if (nCountFicepi > 1){
            //    throw (new Exception("Se ha identificado a más de un profesional con el código de red logado en el equipo."));
            //}
            //if (nCountFicepi < 1)
            //{
            //    throw (new Exception("Acceso no autorizado."));// Acceso exclusivo para empleados internos de Ibermática o colaboradores externos con figura asignada
            //}

            return oP;
		}
        public static bool bPerteneceDIS(int t001_idficepi)
        {
            return DAL.Profesional.bPerteneceDIS(null, t001_idficepi);
        }

        public static bool bPermiteBono(int t001_idficepi)
        {
            return DAL.Profesional.bPermiteBono(null, t001_idficepi);
        }

        public static bool bPermitePago(int t001_idficepi)
        {
            return DAL.Profesional.bPermitePago(null, t001_idficepi);
        }

        public static bool bNotasPendientes(int t001_idficepi)
        {
            return DAL.Profesional.bNotasPendientes(null, t001_idficepi);
        }
        public static int nNotasPendientes(int t001_idficepi)
        {
            return DAL.Profesional.nNotasPendientes(null, t001_idficepi);
        }
        public static int[] nDesgloseNotasPendientes(int t001_idficepi)
        {
            return DAL.Profesional.nDesgloseNotasPendientes(null, t001_idficepi);
        }
        public static int nNotasVisadas(int t001_idficepi)
        {
            return DAL.Profesional.nNotasVisadas(null, t001_idficepi);
        }
        public static int[] nDesgloseNotasVisadas(int t001_idficepi)
        {
            return DAL.Profesional.nDesgloseNotasVisadas(null, t001_idficepi);
        }
        public static int[] nDesgloseNotasVisadas(int t001_idficepi, string sAnoMesDesde, string sAnoMesHasta)
        {
            DateTime? dtDesde = null;
            DateTime? dtHasta = null;
            int iAux = 0;

            if (sAnoMesDesde != "")
            {
                iAux = int.Parse(sAnoMesDesde);
                dtDesde = Fechas.AnnomesAFecha(iAux);
            }
            if (sAnoMesHasta != "")
            {
                iAux = Fechas.AddAnnomes(int.Parse(sAnoMesHasta), 1);
                dtHasta = Fechas.AnnomesAFecha(iAux).AddDays(-1);
            }
            return DAL.Profesional.nDesgloseNotasVisadas(null, t001_idficepi, dtDesde, dtHasta);
        }

        public static void CargarRoles()
        {
            #region Identificación de Roles (figuras) del usuario

            if (!Roles.RoleExists("A")) Roles.CreateRole("A");
            if (!Roles.RoleExists("L")) Roles.CreateRole("L");
            if (!Roles.RoleExists("T")) Roles.CreateRole("T");
            if (!Roles.RoleExists("S")) Roles.CreateRole("S");
            if (!Roles.RoleExists("I")) Roles.CreateRole("I");
            if (!Roles.RoleExists("P")) Roles.CreateRole("P");
            //if (!Roles.RoleExists("DIS")) Roles.CreateRole("DIS");
            if (!Roles.RoleExists("REC")) Roles.CreateRole("REC");
            
            string sUserWindows = HttpContext.Current.User.Identity.Name.ToString();

            //Se borran los roles que pudiera tener el usuario.
            foreach (string Rol in Roles.GetRolesForUser(sUserWindows))
            {
                if (HttpContext.Current.User.IsInRole(Rol)) Roles.RemoveUserFromRole(sUserWindows, Rol);
            }

            //if (Profesional.bPerteneceDIS((int)HttpContext.Current.Session["GVT_IDFICEPI_ENTRADA"]) && !HttpContext.Current.User.IsInRole("DIS"))
            //    Roles.AddUserToRole(sUserWindows, "DIS");
            if (Profesional.bPerteneceDIS((int)HttpContext.Current.Session["GVT_IDFICEPI_ENTRADA"]) && !HttpContext.Current.User.IsInRole("REC"))
                Roles.AddUserToRole(sUserWindows, "REC");

            if ((bool)HttpContext.Current.Session["GVT_ADMIN_ENTRADA"] && !HttpContext.Current.User.IsInRole("REC"))
                Roles.AddUserToRole(sUserWindows, "REC");


            //Ahora se determinarían los perfiles que tiene el usuario para asignárselos.
            //Roles.AddUserToRole(sUserWindows, HttpContext.Current.Session["GVT_PERFIL"].ToString());
            SqlDataReader dr = DAL.USUARIO.ObtenerFigurasAcceso(null, (int)HttpContext.Current.Session["GVT_IDFICEPI"]);
            while (dr.Read())
            {
                if (!HttpContext.Current.User.IsInRole(dr["figura"].ToString()))
                {
                    Roles.AddUserToRole(sUserWindows, dr["figura"].ToString());
                }
            }
            dr.Close();
            dr.Dispose();

            #endregion
        }

        public static string ObtenerCatalogo(string strApellido1, string strApellido2, string strNombre){

            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = DAL.Profesional.ObtenerCatalogo(null, strApellido1, strApellido2, strNombre);

            sb.Append("<table id='tblDatos' class='MA' style='width:400px;text-align:left'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T001_CODRED"].ToString() + "' ");
                sb.Append("idUsuario='" + dr["num_empleado"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("onDblClick='SeleccionProfesional(this);' ");
                sb.Append("style='height:20px; noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["num_empleado"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W375'>" + dr["TECNICO"].ToString() + "</nobr></td></tr>" + (char)13);
                sb.Append("</tr>");

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static bool bPerteneceVariasEmpresas(int t314_idusuario)
        {
            return DAL.Profesional.bPerteneceVariasEmpresas(null, t314_idusuario);
        }
        public static bool bEmpresaVigente(int t314_idusuario, int idEmpresa)
        {
            bool bRes = false;
            ArrayList aEmpresas = Profesional.ObtenerEmpresasTerritorios(t314_idusuario);
            for (int i = 0; i < aEmpresas.Count; i++)
            {
               if(idEmpresa.ToString() == ((string[])aEmpresas[i])[0])
               {
                   bRes = true;
                   break;
               }
            }
            return bRes;
        }

        public static ArrayList ObtenerEmpresasTerritorios(int t314_idusuario)
        {
            int nElem = 0;
            ArrayList aEmpresas = new ArrayList();
            SqlDataReader dr = DAL.Profesional.ObtenerEmpresasTerritorios(null, t314_idusuario);
            while (dr.Read()){
                aEmpresas.Add(new string[] { dr["t313_idempresa"].ToString(), 
                                            dr["t313_denominacion"].ToString(), 
                                            dr["t007_idterrfis"].ToString(), 
                                            dr["t007_nomterrfis"].ToString(),
                                            dr["T007_ITERDC"].ToString(),
                                            dr["T007_ITERMD"].ToString(),
                                            dr["T007_ITERDA"].ToString(),
                                            dr["T007_ITERDE"].ToString(),
                                            dr["T007_ITERK"].ToString()                
                                            });
                nElem++;
            }
            dr.Close();
            dr.Dispose();
            if (nElem==0)
            {//Si no hay empresas en la T654, cogemos la empresa de la T314
                SqlDataReader dr2 = DAL.Profesional.ObtenerEmpresasTerritoriosProfesional(null, t314_idusuario);
                while (dr2.Read())
                {
                    aEmpresas.Add(new string[] { dr2["t313_idempresa"].ToString(), 
                                            dr2["t313_denominacion"].ToString(), 
                                            dr2["t007_idterrfis"].ToString(), 
                                            dr2["t007_nomterrfis"].ToString(),
                                            dr2["T007_ITERDC"].ToString(),
                                            dr2["T007_ITERMD"].ToString(),
                                            dr2["T007_ITERDA"].ToString(),
                                            dr2["T007_ITERDE"].ToString(),
                                            dr2["T007_ITERK"].ToString()                
                                            });
                }
                dr2.Close();
                dr2.Dispose();

            }
            return aEmpresas;
        }

        public static string ObtenerBeneficiariosConsulta(string strApellido1, string strApellido2, string strNombre, string sMostrarBajas)
        {

            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = DAL.Profesional.ObtenerBeneficiariosConsulta(null, strApellido1, strApellido2, strNombre, (sMostrarBajas == "1") ? true : false);

            sb.Append("<TABLE id='tblDatos' class='texto MA' style='WIDTH:400px;text-align:left' cellSpacing='0' cellPadding='0' border='0' width='100%'>");
            sb.Append("<colgroup><col style='width:20px;' /><col /></colgroup>");
            sb.Append("<tbody>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("onDblClick='aceptarClick(this);' style='height:20px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Beneficiario"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W375'>" + dr["Beneficiario"].ToString() + "</nobr></td></tr>" + (char)13);
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        //public static ArrayList ObtenerEmpresasProfesional(string sCodRed)
        //{
        //    ArrayList aEmpresas = new ArrayList();
        //    SqlDataReader dr = DAL.Profesional.ObtenerEmpresasProfesional(null, sCodRed);
        //    while (dr.Read())
        //    {
        //        aEmpresas.Add(new string[] { dr["t313_idempresa"].ToString(), dr["t313_denominacion"].ToString() });
        //    }
        //    dr.Close();
        //    dr.Dispose();

        //    return aEmpresas;
        //}

        public static string GetNombreEmpresa(int idEmpresa)
        {
            string sRes = "";

            SqlDataReader dr = GASVI.DAL.Profesional.GetDatosEmpresa(null, idEmpresa);
            if (dr.Read())
            {
                sRes = dr["t313_denominacion"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return sRes;
        }
    }
}