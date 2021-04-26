using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using GASVI.DAL;

namespace GASVI.BLL
{
	public partial class USUARIO
    {
        #region Propiedades

        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }

        private int _t313_idempresa_defecto;
        public int t313_idempresa_defecto
        {
            get { return _t313_idempresa_defecto; }
            set { _t313_idempresa_defecto = value; }
        }

        private int _t313_idempresa;
        public int t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private string _t313_denominacion;
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }

        private int _t303_idnodo;
        public int t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        private string _t303_denominacion;
        public string t303_denominacion
        {
            get { return _t303_denominacion; }
            set { _t303_denominacion = value; }
        }

        private OFICINA _oOficinaLiquidadora;
        public OFICINA oOficinaLiquidadora
        {
            get { return _oOficinaLiquidadora; }
            set { _oOficinaLiquidadora = value; }
        }

        private TERRITORIO _oTerritorio;
        public TERRITORIO oTerritorio
        {
            get { return _oTerritorio; }
            set { _oTerritorio = value; }
        }

        private DIETAKM _oDietaKm;
        public DIETAKM oDietaKm
        {
            get { return _oDietaKm; }
            set { _oDietaKm = value; }
        }

        private int? _t010_idoficina_base;
        public int? t010_idoficina_base
        {
            get { return _t010_idoficina_base; }
            set { _t010_idoficina_base = value; }
        }

        private bool _bAutorresponsable;
        public bool bAutorresponsable
        {
            get { return _bAutorresponsable; }
            set { _bAutorresponsable = value; }
        }

        private int _nCCIberper;
        public int nCCIberper
        {
            get { return _nCCIberper; }
            set { _nCCIberper = value; }
        }

        #endregion

        public USUARIO()
		{
			
		}

        public static USUARIO Obtener(int t314_idusuario)
		{
            int idEmpresaDefecto = GASVI.DAL.Configuracion.GetEmpresaDefecto(null, t314_idusuario);
            USUARIO oU = new USUARIO();
            SqlDataReader dr;
            if (idEmpresaDefecto!=0 && Profesional.bEmpresaVigente(t314_idusuario, idEmpresaDefecto))
                dr = DAL.USUARIO.ObtenerDatosNuevaNota(null, t314_idusuario, idEmpresaDefecto);
            else
                dr = DAL.USUARIO.ObtenerDatosNuevaNota(null, t314_idusuario);

            if (dr.Read())
            {
                oU.t314_idusuario = (int)dr["t314_idusuario"];
                oU.t001_idficepi = (int)dr["t001_idficepi"];
                oU.Nombre = dr["Profesional"].ToString();
                oU.t422_idmoneda = dr["t422_idmoneda"].ToString();
                oU.t313_idempresa_defecto = (int)dr["t313_idempresa_defecto"];
                oU.t313_idempresa = (int)dr["t313_idempresa"];
                oU.t313_denominacion = dr["t313_denominacion"].ToString();
                oU.t303_idnodo = (int)dr["t303_idnodo"];
                oU.t303_denominacion = dr["t303_denominacion"].ToString();
                oU.oTerritorio = new TERRITORIO(byte.Parse(dr["t007_idterrfis"].ToString()),
                                                    dr["t007_nomterrfis"].ToString(),
                                                    decimal.Parse(dr["t007_iterdc"].ToString()),
                                                    decimal.Parse(dr["t007_itermd"].ToString()),
                                                    decimal.Parse(dr["t007_iterda"].ToString()),
                                                    decimal.Parse(dr["t007_iterde"].ToString()),
                                                    decimal.Parse(dr["t007_iterk"].ToString())
                                                );
                oU.oOficinaLiquidadora = new OFICINA((short)dr["t010_idoficina_liquidadora"],
                                                    dr["t010_desoficina"].ToString());
                if (dr["t069_iddietakm"] != DBNull.Value)
                    oU.oDietaKm = new DIETAKM((byte)dr["t069_iddietakm"],
                                                    dr["T069_descripcion"].ToString(),
                                                    decimal.Parse(dr["T069_icdc"].ToString()),
                                                    decimal.Parse(dr["T069_icmd"].ToString()),
                                                    decimal.Parse(dr["T069_icda"].ToString()),
                                                    decimal.Parse(dr["T069_icde"].ToString()),
                                                    decimal.Parse(dr["T069_ick"].ToString())
                                                );
                if (dr["t010_idoficina_base"] != DBNull.Value)
                    oU.t010_idoficina_base = (int?)int.Parse(dr["t010_idoficina_base"].ToString());

                oU.bAutorresponsable = ((int)dr["autorresponsable"] == 1) ? true : false;
                //oU.nCCIberper = (int)dr["CentrosCoste"];
                oU.nCCIberper = int.Parse(dr["CentrosCoste"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return oU;
		}

        public static string ObtenerBeneficiarios(int t001_idficepi,
            bool bRolTramitador,
            bool bRolSuperTramitador,
            string t001_nombre,
            string t001_apellido1,
            string t001_apellido2,
            string sMostrarBajas,
            bool bAdministrador)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblBeneficiarios' class='MA' style='width:620px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px; padding-left:2px;' />");
            sb.Append(" <col style='width:300px;' />");
            sb.Append(" <col style='width:300px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.USUARIO.ObtenerBeneficiarios(null, t001_idficepi,
                                                                bRolTramitador,
                                                                bRolSuperTramitador,
                                                                Utilidades.unescape(t001_nombre),
                                                                Utilidades.unescape(t001_apellido1),
                                                                Utilidades.unescape(t001_apellido2),
                                                                (sMostrarBajas=="1")? true:false,
                                                                bAdministrador);
            while (dr.Read())
            {
                sb.Append("<tr ");
                sb.Append("id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("style='height:20px;' onclick='ms(this);' ondblclick='aceptarClick(this);' >");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W290'>" + dr["Beneficiario"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W290'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

	}
}