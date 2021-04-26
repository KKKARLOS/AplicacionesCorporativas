using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    public partial class USUARIOGV
    {
        #region Propiedades

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

        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
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

        private OFICINAGV _oOficinaLiquidadora;
        public OFICINAGV oOficinaLiquidadora
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

        #endregion

        public USUARIOGV()
		{
			
		}

        public static USUARIOGV Obtener(int t314_idusuario)
		{
            USUARIOGV oU = new USUARIOGV();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIOGV.ObtenerDatosNuevaNota(null, t314_idusuario);

            if (dr.Read())
            {
                oU.t314_idusuario = (int)dr["t314_idusuario"];
                oU.Nombre = dr["Profesional"].ToString();
                oU.t422_idmoneda = dr["t422_idmoneda"].ToString();
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
                oU.oOficinaLiquidadora = new OFICINAGV((short)dr["t010_idoficina_liquidadora"],
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
            }
            dr.Close();
            dr.Dispose();

            return oU;
		}

        public static ArrayList ObtenerEmpresasTerritorios(int t314_idusuario)
        {
            ArrayList aEmpresas = new ArrayList();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIOGV.ObtenerEmpresasTerritorios(null, t314_idusuario);
            while (dr.Read())
            {
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
            }
            dr.Close();
            dr.Dispose();

            return aEmpresas;
        }

    }
}
