using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using GEMO.DAL;

/// <summary>
/// Summary description for PROFESIONALES
/// </summary>
namespace GEMO.BLL
{
    public class PROFESIONALES
    {
        #region Propiedades
        
        private int _IdFicepi;
        public int IdFicepi
        {
            get { return _IdFicepi; }
            set { _IdFicepi = value; }
        }

        private string _Profesional;
        public string Profesional
        {
            get { return _Profesional; }
            set { _Profesional = value; }
        }
        
        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _Apellido1;
        public string Apellido1
        {
            get { return _Apellido1; }
            set { _Apellido1 = value; }
        }

        private string _Apellido2;
        public string Apellido2
        {
            get { return _Apellido2; }
            set { _Apellido2 = value; }
        }
        private bool _bIdentificado;
        public bool bIdentificado
        {
            get { return _bIdentificado; }
            set { _bIdentificado = value; }
        }

        private string _Administrador;
        public string Administrador
        {
            get { return _Administrador; }
            set { _Administrador = value; }
        }
        private string _t001_botonfecha;
        public string t001_botonfecha
        {
            get { return _t001_botonfecha; }
            set { _t001_botonfecha = value; }
        }
        #endregion

        public PROFESIONALES()
		{
			
		}
        public static string Beneficiarios(string sAp1, string sAp2, string sNombre, bool sMostrarBajas)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='MAM' style='WIDTH: 350px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:350px; padding-left:3px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = GEMO.DAL.PROFESIONALES.Beneficiarios(sAp1, sAp2, sNombre, sMostrarBajas);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' onmouseover='TTip(event)' ");
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["profesional"].ToString() + "</nobr></td>");

                sb.Append("</tr>" + (char)10);
            }

            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();
            return sResul;
        }

        public static string Responsables(string sAp1, string sAp2, string sNombre, bool sMostrarBajas)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='MAM' style='WIDTH: 350px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:350px; padding-left:3px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = GEMO.DAL.PROFESIONALES.Responsables(sAp1, sAp2, sNombre, sMostrarBajas);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' onmouseover='TTip(event)' ");
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["profesional"].ToString() + "</nobr></td>");

                sb.Append("</tr>" + (char)10);
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();
            return sResul;
        }

        public static string obtenerProfesionales2(string sAp1, string sAp2, string sNombre, int iOpcion)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='MAM' style='WIDTH: 400px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:400px; padding-left:3px;' />");
            sb.Append("</colgroup>");


            SqlDataReader dr = GEMO.DAL.PROFESIONALES.ObtenerProfesionales(sAp1, sAp2, sNombre, iOpcion);
            
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' onmouseover='TTip(event)' ");
                sb.Append("onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td><nobr class='NBR W370'>" + dr["profesional"].ToString() + "</nobr></td>");

                sb.Append("</tr>" + (char)10);
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();
            return sResul;
        }

        public static string obtenerProfesionales(string sAp1, string sAp2, string sNombre, int iOpcion)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = GEMO.DAL.PROFESIONALES.ObtenerProfesionales(sAp1, sAp2, sNombre, iOpcion);

            sb.Append("<table id='tblOpciones' class='MAM' style='width: 400px;text-align:left'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:380px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' style='height:20px' ");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("ondblclick='insertarFigura(this);' ");
                sb.Append("><td></td><td><nobr class='NBR W370'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();// +"@@" + sbBa.ToString();
            return sResul;
        }
        public static string Reconexion(string sAp1, string sAp2, string sNombre)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = GEMO.DAL.PROFESIONALES.ObtenerProfesionales(sAp1, sAp2, sNombre, 0);

            sb.Append("<table id='tblDatos' class='texto MA' style='width:400px;text-align:left'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:380px;'</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T001_CODRED"].ToString() + "'");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("ondblclick='SeleccionProfesional(this);' onclick='ms(this)' style='height:20px;noWrap:true;'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W370'>" + dr["Profesional"].ToString() + "</nobr></td></tr>" + (char)13);
                sb.Append("</tr>");

            }

            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();
            return sResul;
        }

        public static string obtenerProfesionalesMedios(string sAp1, string sAp2, string sNombre)
        {
            string sResul = "";
            StringBuilder sbAlta = new StringBuilder();
            StringBuilder sbBaja = new StringBuilder();

            SqlDataReader dr = GEMO.DAL.PROFESIONALES.ObtenerProfesionalesMedios(sAp1, sAp2, sNombre);

            sbAlta.Append("<table id='tblDatosAlta' class='mano' style='WIDTH: 430px;'>");
            sbAlta.Append("<colgroup><col style='width:400px;' /><col style='width:30px;' /></colgroup>");

            sbBaja.Append("<table id='tblDatosBaja' class='mano' style='WIDTH: 430px;'>");
            sbBaja.Append("<colgroup><col style='width:430px;' /></colgroup>");

            while (dr.Read())
            {
                if (dr["baja"].ToString() == "0")
                {
                    sbAlta.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                    sbAlta.Append("onclick='ms(this);getMedios(this)' ");
                    sbAlta.Append("style='height:20px' >");
                    sbAlta.Append("<td style='padding-left:5px;'><nobr class='NBR W400'>" + dr["Profesional"].ToString() + "<nobr></td>");
                    sbAlta.Append("<td style='padding-right:3px;text-align:right;'>" + dr["t004_tier"].ToString() + "</td>");
                    sbAlta.Append("</tr>");
                }
                else
                {
                    sbBaja.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                    sbBaja.Append("onclick='ms(this);getMedios(this)' ");
                    sbBaja.Append("style='height:20px' >");
                    sbBaja.Append("<td style='padding-left:5px;'><nobr class='NBR W420'>" + dr["Profesional"].ToString() + "<nobr></td>");
                    sbBaja.Append("</tr>");
                }
            }
            dr.Close();
            dr.Dispose();

            sbAlta.Append("</table>");
            sbBaja.Append("</table>");

            sResul = sbAlta.ToString() + "@#@" + sbBaja.ToString();
            return sResul;
        }	
        public static PROFESIONALES Obtener(string IDRED)
        {
            PROFESIONALES oP = new PROFESIONALES();
            SqlDataReader dr = DAL.PROFESIONALES.ObtenerDatosLogin(IDRED);

            if (dr.Read())
            {
                oP.bIdentificado = true;
                oP.IdFicepi = (int)dr["t001_idficepi"];
                oP.Profesional = dr["Profesional"].ToString();
                oP.Nombre = dr["NOMBRE"].ToString();
                oP.Apellido1 = dr["APELLIDO1"].ToString();
                oP.Apellido2 = dr["APELLIDO2"].ToString();
                oP.Administrador = dr["ADM"].ToString();
                oP.t001_botonfecha = dr["t001_botonfecha"].ToString();
            }
            dr.Close();
            dr.Dispose();
            return oP;

        }
        public static bool bPerteneceDIS(int t001_idficepi)
        {
            return DAL.PROFESIONALES.bPerteneceDIS(null, t001_idficepi);
        }
        public static void CargarRoles(int t001_idficepi, string sAdmin)
        {
            #region Creación dinámica de roles
            // Roles Administrador y especiales
            if (!Roles.RoleExists("A")) Roles.CreateRole("A");
            if (!Roles.RoleExists("DIS")) Roles.CreateRole("DIS");
            if (!Roles.RoleExists("REC")) Roles.CreateRole("REC");

            // Invitado
            if (!Roles.RoleExists("I")) Roles.CreateRole("I");
            // Controlador
            if (!Roles.RoleExists("C")) Roles.CreateRole("C");
            // Facturador
            if (!Roles.RoleExists("F")) Roles.CreateRole("F");
            // Medios
            if (!Roles.RoleExists("M")) Roles.CreateRole("M");
            // Interesado
            if (!Roles.RoleExists("U")) Roles.CreateRole("U");

            #endregion

            string sUserWindows = HttpContext.Current.User.Identity.Name.ToString();
            //Se borran los roles que pudiera tener el usuario.
            //foreach (string Rol in Roles.GetRolesForUser(User.Identity.Name))
            foreach (string Rol in Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name))
            {
                //if (Rol == "REC" && HttpContext.Current.Session["IDFICEPI_ENTRADA"] != null) continue;
                if (HttpContext.Current.User.IsInRole(Rol)) Roles.RemoveUserFromRole(HttpContext.Current.User.Identity.Name.ToString(), Rol);
            }

            //Obtengo las diferentes figuras del usuario y genero los roles 
            //Si es administrador o superadministrador no me molesto en consultar la base de datos

            if (sAdmin != "")
            {
                if (!HttpContext.Current.User.IsInRole("A"))
                    Roles.AddUserToRole(sUserWindows, "A");
            }

            if (PROFESIONALES.bPerteneceDIS(int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString())))
            {
                if (!HttpContext.Current.User.IsInRole("DIS"))
                    Roles.AddUserToRole(sUserWindows, "DIS");

                HttpContext.Current.Session["DIS"] = "S";
            }

            if (HttpContext.Current.Session["Admin"].ToString() == "A")
            {
                if (!HttpContext.Current.User.IsInRole("REC"))
                    Roles.AddUserToRole(sUserWindows, "REC");
            }

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GEM_FIGURAS", aParam);
            //string sFiguras = "";
            while (dr.Read())
            {
                if (!HttpContext.Current.User.IsInRole(dr["figura"].ToString()))
                {
                    Roles.AddUserToRole(sUserWindows, dr["figura"].ToString());
                }
                //sFiguras+= dr["figura"].ToString().Trim() + ",";
            }
            //if (sFiguras.Length > 0) sFiguras = sFiguras.Substring(0, sFiguras.Length - 1);
                            
            //HttpContext.Current.Session["FIGURAS"] = sFiguras;

            dr.Close();
            dr.Dispose();
        }
    }
}