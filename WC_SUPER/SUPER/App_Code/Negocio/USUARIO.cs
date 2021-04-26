using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Web;
//Para usar ArraList
using System.Collections;

namespace SUPER.Capa_Negocio
{
    public partial class USUARIO
    {
        #region Propiedades y Atributos

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

        private DateTime _t314_falta;
        public DateTime t314_falta
        {
            get { return _t314_falta; }
            set { _t314_falta = value; }
        }

        private DateTime? _t314_fbaja;
        public DateTime? t314_fbaja
        {
            get { return _t314_fbaja; }
            set { _t314_fbaja = value; }
        }

        private int? _t313_idempresa;
        public int? t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private int? _t315_idproveedor;
        public int? t315_idproveedor
        {
            get { return _t315_idproveedor; }
            set { _t315_idproveedor = value; }
        }

        private int? _t303_idnodo;
        public int? t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        private decimal _t314_costejornada;
        public decimal t314_costejornada
        {
            get { return _t314_costejornada; }
            set { _t314_costejornada = value; }
        }

        private decimal _t314_costehora;
        public decimal t314_costehora
        {
            get { return _t314_costehora; }
            set { _t314_costehora = value; }
        }

        private bool _t314_nuevogasvi;
        public bool t314_nuevogasvi
        {
            get { return _t314_nuevogasvi; }
            set { _t314_nuevogasvi = value; }
        }

        private bool _t314_jornadareducida;
        public bool t314_jornadareducida
        {
            get { return _t314_jornadareducida; }
            set { _t314_jornadareducida = value; }
        }

        private float _t314_horasjor_red;
        public float t314_horasjor_red
        {
            get { return _t314_horasjor_red; }
            set { _t314_horasjor_red = value; }
        }

        private DateTime? _t314_fdesde_red;
        public DateTime? t314_fdesde_red
        {
            get { return _t314_fdesde_red; }
            set { _t314_fdesde_red = value; }
        }

        private DateTime? _t314_fhasta_red;
        public DateTime? t314_fhasta_red
        {
            get { return _t314_fhasta_red; }
            set { _t314_fhasta_red = value; }
        }

        private bool _t314_controlhuecos;
        public bool t314_controlhuecos
        {
            get { return _t314_controlhuecos; }
            set { _t314_controlhuecos = value; }
        }

        private bool _t314_mailiap;
        public bool t314_mailiap
        {
            get { return _t314_mailiap; }
            set { _t314_mailiap = value; }
        }

        private bool _t314_calculoJA;
        public bool t314_calculoJA
        {
            get { return _t314_calculoJA; }
            set { _t314_calculoJA = value; }
        }

        private int? _t450_idcategsuper;
        public int? t450_idcategsuper
        {
            get { return _t450_idcategsuper; }
            set { _t450_idcategsuper = value; }
        }

        private string _t314_loginhermes;
        public string t314_loginhermes
        {
            get { return _t314_loginhermes; }
            set { _t314_loginhermes = value; }
        }

        private string _t314_codcomercialsap;
        public string t314_codcomercialsap
        {
            get { return _t314_codcomercialsap; }
            set { _t314_codcomercialsap = value; }
        }

        private bool _t314_acs;
        public bool t314_acs
        {
            get { return _t314_acs; }
            set { _t314_acs = value; }
        }

        private string _t314_alias;
        public string t314_alias
        {
            get { return _t314_alias; }
            set { _t314_alias = value; }
        }

        private float _t314_margencesion;
        public float t314_margencesion
        {
            get { return _t314_margencesion; }
            set { _t314_margencesion = value; }
        }
        #endregion

        #region Constructor

        public USUARIO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static DataSet ObtenerDatosAcceso(string sIDRED, byte T000_CODIGO)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sIDRED", SqlDbType.VarChar, 15);
            aParam[0].Value = sIDRED;
            aParam[1] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 2);
            aParam[1].Value = T000_CODIGO;

            return SqlHelper.ExecuteDataset("SUP_ACCESOUSUARIO", aParam);
        }
        public static DataSet ObtenerDatosAccesoForaneo(string sIdFicepi, byte T000_CODIGO)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int t001_idficepi = -1;

            try { t001_idficepi = int.Parse(sIdFicepi); }
            catch { t001_idficepi = -1; }

            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 2);
            aParam[1].Value = T000_CODIGO;

            return SqlHelper.ExecuteDataset("SUP_FORANEO_ACCESOUSUARIO", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T314_USUARIO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	24/03/2009 15:50:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T314_USUARIO,
        /// y devuelve una instancia u objeto del tipo USUARIO
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	24/03/2009 15:50:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static USUARIO Select(SqlTransaction tr, int t314_idusuario)
        {
            USUARIO o = new USUARIO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIO_S", aParam);

            if (dr.Read())
            {
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                if (dr["t314_falta"] != DBNull.Value)
                    o.t314_falta = (DateTime)dr["t314_falta"];
                if (dr["t314_fbaja"] != DBNull.Value)
                    o.t314_fbaja = (DateTime)dr["t314_fbaja"];
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t315_idproveedor"] != DBNull.Value)
                    o.t315_idproveedor = int.Parse(dr["t315_idproveedor"].ToString());
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t314_costejornada"] != DBNull.Value)
                    o.t314_costejornada = decimal.Parse(dr["t314_costejornada"].ToString());
                if (dr["t314_costehora"] != DBNull.Value)
                    o.t314_costehora = decimal.Parse(dr["t314_costehora"].ToString());
                if (dr["t314_nuevogasvi"] != DBNull.Value)
                    o.t314_nuevogasvi = (bool)dr["t314_nuevogasvi"];
                if (dr["t314_jornadareducida"] != DBNull.Value)
                    o.t314_jornadareducida = (bool)dr["t314_jornadareducida"];
                if (dr["t314_horasjor_red"] != DBNull.Value)
                    o.t314_horasjor_red = float.Parse(dr["t314_horasjor_red"].ToString());
                if (dr["t314_fdesde_red"] != DBNull.Value)
                    o.t314_fdesde_red = (DateTime)dr["t314_fdesde_red"];
                if (dr["t314_fhasta_red"] != DBNull.Value)
                    o.t314_fhasta_red = (DateTime)dr["t314_fhasta_red"];
                if (dr["t314_controlhuecos"] != DBNull.Value)
                    o.t314_controlhuecos = (bool)dr["t314_controlhuecos"];
                if (dr["t314_mailiap"] != DBNull.Value)
                    o.t314_mailiap = (bool)dr["t314_mailiap"];
                if (dr["t314_calculoJA"] != DBNull.Value)
                    o.t314_calculoJA = (bool)dr["t314_calculoJA"];
                if (dr["t450_idcategsuper"] != DBNull.Value)
                    o.t450_idcategsuper = int.Parse(dr["t450_idcategsuper"].ToString());
                if (dr["t314_loginhermes"] != DBNull.Value)
                    o.t314_loginhermes = (string)dr["t314_loginhermes"];
                if (dr["t314_codcomercialsap"] != DBNull.Value)
                    o.t314_codcomercialsap = (string)dr["t314_codcomercialsap"];
                if (dr["t314_acs"] != DBNull.Value)
                    o.t314_acs = (bool)dr["t314_acs"];
                if (dr["t314_alias"] != DBNull.Value)
                    o.t314_alias = (string)dr["t314_alias"];
                if (dr["t314_margencesion"] != DBNull.Value)
                    o.t314_margencesion = float.Parse(dr["t314_margencesion"].ToString());

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de USUARIO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si un usuario tiene consumos imputados, para poder desasignarlo de un proyecto.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool bTieneConsumosEnMesesAbiertos(SqlTransaction tr, int t314_idusuario, int t305_idproyectosubnodo)
        {
            bool bConsumos = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;

            int nHoras;
            if (tr == null)
                nHoras = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIOCONSUMO_S", aParam));
            else
                nHoras = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIOCONSUMO_S", aParam));

            if (nHoras > 0)
                bConsumos = true;

            return bConsumos;
        }

        public static SqlDataReader ObtenerProfesionalesResponsablesProyecto(string sAp1, string sAp2, string sNombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 100);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 100);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 100);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFRESPONSABLE_PROYECTO", aParam);
        }
        public static SqlDataReader ObtenerSupervisores(string sAp1, string sAp2, string sNombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 100);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 100);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 100);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_SUPERVISORES_FICEPI", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesResponsablesContrato(string sAp1, string sAp2, string sNombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 100);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 100);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 100);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFRESPONSABLE_CONTRATO", aParam);
        }
        
        public static SqlDataReader ObtenerProfesionalesComercialContrato(string sAp1, string sAp2, string sNombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@sAp1", SqlDbType.VarChar, 100, sAp1),
                ParametroSql.add("@sAp2", SqlDbType.VarChar, 100, sAp2),
                ParametroSql.add("@sNombre", SqlDbType.VarChar, 100, sNombre),
                ParametroSql.add("@bMostrarBajas", SqlDbType.Bit, 1, bMostrarBajas)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_PROFCOMERCIAL_CONTRATO", aParam);
        }
        /// <summary>
        /// Obtiene profesiaonales mas un atributo indicando si el profesional que realiza la consulta tiene ámbito de visión sobre el curriculum
        /// </summary>
        /// <param name="sAp1"></param>
        /// <param name="sAp2"></param>
        /// <param name="sNombre"></param>
        /// <param name="bMostrarBajas"></param>
        /// <param name="bForaneos"></param>
        /// <param name="sTipoRecurso"></param>
        /// <returns></returns>
        public static SqlDataReader ObtenerProfesionalesCVT(int idFicepi, string sAp1, string sAp2, string sNombre, bool bMostrarBajas, bool bForaneos, string sTipoRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi),
                ParametroSql.add("@sAp1", SqlDbType.VarChar, 100, sAp1),
                ParametroSql.add("@sAp2", SqlDbType.VarChar, 100, sAp2),
                ParametroSql.add("@sNombre", SqlDbType.VarChar, 100, sNombre),
                ParametroSql.add("@bMostrarBajas", SqlDbType.Bit, 1, bMostrarBajas),
                ParametroSql.add("@bForaneos", SqlDbType.Bit, 1, bForaneos),
                ParametroSql.add("@t001_tiporecurso", SqlDbType.Char, 1, sTipoRecurso)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_CVT", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesFicepi(string sAp1, string sAp2, string sNombre, bool bMostrarBajas, bool bForaneos, string sTipoRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 100);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 100);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 100);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;
            aParam[4] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[4].Value = bForaneos;
            aParam[5] = new SqlParameter("@t001_tiporecurso", SqlDbType.Char, 1);
            aParam[5].Value = sTipoRecurso;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_FICEPI", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesEvaluados(string sAp1, string sAp2, string sNombre, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 100);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 100);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 100);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[3].Value = t001_idficepi;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_EVALUADOS", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesCRP(string sAp1, string sAp2, string sNombre, bool bMostrarBajas, Nullable<int> t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 100);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 100);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 100);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;
            aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFRESPONSABLE_CRP", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesCRPMulti(string sAp1, string sAp2, string sNombre, bool bMostrarBajas, string sIdNodo)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 100);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 100);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 100);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;
            aParam[4] = new SqlParameter("@sIdNodo", SqlDbType.VarChar, 4000);
            aParam[4].Value = sIdNodo;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFRESPONSABLE_CRP_MULTI", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesCalendario(string sApellido1, string sApellido2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 25);
            aParam[0].Value = sApellido1;
            aParam[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 25);
            aParam[1].Value = sApellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 20);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[3].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALCAL", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesCalendarioUsu(int nUsuario, string sApellido1, string sApellido2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 25);
            aParam[1].Value = sApellido1;
            aParam[2] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 25);
            aParam[2].Value = sApellido2;
            aParam[3] = new SqlParameter("@sNombre", SqlDbType.VarChar, 20);
            aParam[3].Value = sNombre;
            aParam[4] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[4].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALCAL_USU", aParam);
        }

        public static SqlDataReader ObtenerComercialesPreventa(string sAp1, string sAp2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 25);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 25);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 20);
            aParam[2].Value = sNombre;

            return SqlHelper.ExecuteSqlDataReader("SIC_CONJUNTOCOMERCIALESPREVENTA_C", aParam);
        }
        public static SqlDataReader ObtenerUsuarioActivos(string sAp1, string sAp2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 25);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 25);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 20);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            //aParam[3].Value = (bool)HttpContext.Current.Session["FORANEOS"];
            aParam[3].Value = false;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOSACTIVOS", aParam);
        }
        public static SqlDataReader ObtenerUsuarioActivos(string sAp1, string sAp2, string sNombre, bool bForaneos)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 25);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 25);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 20);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[3].Value = bForaneos;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOSACTIVOS", aParam);
        }
        public static SqlDataReader ObtenerUsuarioActivos(string sAp1, string sAp2, string sNombre, bool bForaneos, int NodoPSN)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 25);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 25);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 20);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[3].Value = bForaneos;
            aParam[4] = new SqlParameter("@NodoPSN", SqlDbType.Int, 4);
            aParam[4].Value = NodoPSN;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOSACTIVOS_NODOPSN", aParam);
        }
        /// <summary>
        /// Obtiene usuarios que tengan LoginHermes
        /// </summary>
        /// <param name="sAp1"></param>
        /// <param name="sAp2"></param>
        /// <param name="sNombre"></param>
        /// <param name="bMostrarBajas"></param>
        /// <returns></returns>
        public static SqlDataReader ObtenerComerciales(string sAp1, string sAp2, string sNombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 25);
            aParam[0].Value = sAp1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 25);
            aParam[1].Value = sAp2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 20);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_COMERCIALES_CAT", aParam);
        }

        public static SqlDataReader ObtenerProfesionalesIAP(int NumEmpleado, string sPerfil, string sAp1, string sAp2, string sNombre, int IdFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sPerfil", SqlDbType.VarChar, 2);
            aParam[2] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
            aParam[3] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
            aParam[4] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[5] = new SqlParameter("@IdFicepi", SqlDbType.Int, 4);
            aParam[6] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            

            aParam[0].Value = NumEmpleado;
            aParam[1].Value = sPerfil;
            aParam[2].Value = sAp1;
            aParam[3].Value = sAp2;
            aParam[4].Value = sNombre;
            aParam[5].Value = IdFicepi;
            aParam[6].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_IAP", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesPST(string sAp1, string sAp2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
            aParam[1] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);

            aParam[0].Value = sAp1;
            aParam[1].Value = sAp2;
            aParam[2].Value = sNombre;
            aParam[3].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_PST", aParam);
        }

        //Para exportaciones masivas a Excel
        public static SqlDataReader GetConsumosProf(int nUsuario, DateTime dtFechaFesde, DateTime dtFechaHasta, string slProfesionales)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@fDesde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dtFechaFesde;
            aParam[2] = new SqlParameter("@fHasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dtFechaHasta;
            aParam[3] = new SqlParameter("@sProfesionales", SqlDbType.VarChar, 8000);
            aParam[3].Value = slProfesionales;
            //15/04/2011 Mikel (por indicación de Andoni)
            //Hace falta discriminar pues aunque en la lista de profesionales ya se ha aplicado el filtro de cuales son accesibles por el usuario
            //puede ser que el usuario que está ejecutando la consulta tenga una visión reducida sobre un profesional
            //Es decir, puede ver los consumos de ese profesional en unos proyectos (los de su ámbito) pero no en otros
            //Sin embargo, si es administrador, tiene que poder ver todo
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO", aParam);
            //    return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO_USU", aParam);
        }
        public static SqlDataReader ProfesionalesTareas(int nUsuario, string sEstadoProyEcon, string @sEstadoProyTec, string @sEstadoTarea, string slProfesionales)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@sEstadoProyEcon", SqlDbType.Char, 1);
            aParam[1].Value = sEstadoProyEcon;
            aParam[2] = new SqlParameter("@sEstadoProyTec", SqlDbType.Char, 1);
            aParam[2].Value = @sEstadoProyTec;
            aParam[3] = new SqlParameter("@sEstadoTarea", SqlDbType.Char, 1);
            aParam[3].Value = @sEstadoTarea;
            aParam[4] = new SqlParameter("@sProfesionales", SqlDbType.VarChar, 8000);
            aParam[4].Value = slProfesionales;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_TAREAS_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONALES_TAREAS_USU", aParam);
        }

        public static string ObtenerFecUltImputac(SqlTransaction tr, int NumEmpleado)
        {
            string sResul = "";
            object objResul = null;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;

            if (tr == null)
                objResul = SqlHelper.ExecuteScalar("SUP_CONSUMOIAPMAXS", aParam);
            else
                objResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONSUMOIAPMAXS", aParam);

            if (objResul != null)
                sResul = ((DateTime)objResul).ToShortDateString();

            //int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSUMOIAPMAXU", NumEmpleado, (DateTime)objResul);

            return sResul;
        }
        public static string ObtenerFecUltImputacProy(SqlTransaction tr, int NumEmpleado, int nPSN)
        {
            string sResul = "";
            object objResul = null;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = nPSN;

            if (tr == null)
                objResul = SqlHelper.ExecuteScalar("SUP_CONSUMOIAP_PROY_MAXS", aParam);
            else
                objResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONSUMOIAP_PROY_MAXS", aParam);

            if (objResul != null)
                sResul = ((DateTime)objResul).ToShortDateString();

            return sResul;
        }
        public static SqlDataReader ObtenerFigurasUsuario(int t314_idusuario, int t001_idficepi, Nullable<int> nTipoItem, string sEstadosProy)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@nTipoItem", SqlDbType.Int, 4, nTipoItem);
            aParam[i++] = ParametroSql.add("@sEstadosProy", SqlDbType.VarChar, 10, sEstadosProy);

            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUSUARIO", aParam);
        }

        public static SqlDataReader ObtenerQueEsUsuarios(string sCodigo, Nullable<int> nTipoItem, string sCRs, Nullable<int> nSn, string sFigura, string sEstadosProy)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@Codigo", SqlDbType.VarChar, 8000);
            aParam[0].Value = sCodigo;
            aParam[1] = new SqlParameter("@nTipoItem", SqlDbType.Int, 4);
            aParam[1].Value = nTipoItem;
            aParam[2] = new SqlParameter("@CR", SqlDbType.VarChar, 8000);
            aParam[2].Value = sCRs;
            aParam[3] = new SqlParameter("@SN", SqlDbType.Int, 4);
            aParam[3].Value = nSn;
            aParam[4] = new SqlParameter("@Figura", SqlDbType.VarChar, 2);
            aParam[4].Value = sFigura;
            aParam[5] = new SqlParameter("@sEstadosProy", SqlDbType.VarChar, 10);
            aParam[5].Value = sEstadosProy;
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAS_USUARIOS", aParam);
        }

        public static DataSet ObtenerProfFICEPISUPER(string t001_apellido1, string t001_apellido2, string t001_nombre,
                                                     Nullable<int> t314_idusuario, bool bMostrarBajas)
        {
            if (t314_idusuario != null)
            {
                SqlParameter[] aParam2 = new SqlParameter[2];
                aParam2[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
                aParam2[0].Value = t314_idusuario;
                aParam2[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
                aParam2[1].Value = bMostrarBajas;
                //aParam2[2] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                //aParam2[2].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                return SqlHelper.ExecuteDataset("SUP_PROFMANTUSU_CAT2", aParam2);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[5];
                aParam[0] = new SqlParameter("@t001_apellido1", SqlDbType.VarChar, 50);
                aParam[0].Value = t001_apellido1;
                aParam[1] = new SqlParameter("@t001_apellido2", SqlDbType.VarChar, 50);
                aParam[1].Value = t001_apellido2;
                aParam[2] = new SqlParameter("@t001_nombre", SqlDbType.VarChar, 50);
                aParam[2].Value = t001_nombre;
                aParam[3] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
                aParam[3].Value = t314_idusuario;
                aParam[4] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
                aParam[4].Value = bMostrarBajas;
                //aParam[5] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                //aParam[5].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                return SqlHelper.ExecuteDataset("SUP_PROFMANTUSU_CAT", aParam);
            }
        }

        public static SqlDataReader ObtenerDatosProfUsuario(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_S_USU", aParam);
        }
        public static SqlDataReader ObtenerVacacionesAnoMes(int t314_idusuario, int iAno, int iMes)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@iAno", SqlDbType.Int, 4);
            aParam[1].Value = iAno;
            aParam[2] = new SqlParameter("@iMes", SqlDbType.Int, 4);
            aParam[2].Value = iMes;        
            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_VACATAS_ANOMES", aParam);
        }
        public static SqlDataReader ObtenerDatosProfFicepi(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_S_FIC", aParam);
        }
        public static string GetNombreProfesional(int t001_idficepi)
        {
            return SUPER.DAL.Profesional.GetNombre(t001_idficepi);
        }
        public static SqlDataReader GetDatosProfUsuario(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONAL_S", aParam);
        }

        public static int InsertarInterno(SqlTransaction tr,
                                    int t001_idficepi,
                                    string t314_alias,
                                    Nullable<int> t303_idnodo,
                                    int t313_idempresa,
                                    DateTime t314_falta,
                                    Nullable<DateTime> t314_fbaja,
                                    bool t314_nuevogasvi,
                                    string t314_loginhermes,
                                    string t314_codcomercialsap,
                                    bool t314_controlhuecos,
                                    bool t314_mailiap,
                                    decimal t314_costehora,
                                    decimal t314_costejornada,
                                    bool t314_jornadareducida,
                                    float t314_horasjor_red,
                                    Nullable<DateTime> t314_fdesde_red,
                                    Nullable<DateTime> t314_fhasta_red,
                                    bool t314_calculoJA,
                                    Nullable<int> t450_idcategsuper,
                                    bool t314_acs,
                                    float t314_margencesion,
                                    string t422_idmoneda,
                                    bool t314_noalertas,
                                    bool t314_contratorelevo

            )
        {
            SqlParameter[] aParam = new SqlParameter[24];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t314_alias", SqlDbType.VarChar, 30);
            aParam[1].Value = t314_alias;
            aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo;
            aParam[3] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[3].Value = t313_idempresa;
            aParam[4] = new SqlParameter("@t314_falta", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = t314_falta;
            aParam[5] = new SqlParameter("@t314_fbaja", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = t314_fbaja;
            aParam[6] = new SqlParameter("@t314_nuevogasvi", SqlDbType.Bit, 1);
            aParam[6].Value = t314_nuevogasvi;
            aParam[7] = new SqlParameter("@t314_loginhermes", SqlDbType.VarChar, 25);
            aParam[7].Value = t314_loginhermes;
            aParam[8] = new SqlParameter("@t314_codcomercialsap", SqlDbType.VarChar, 3);
            aParam[8].Value = t314_codcomercialsap;
            aParam[9] = new SqlParameter("@t314_controlhuecos", SqlDbType.Bit, 1);
            aParam[9].Value = t314_controlhuecos;
            aParam[10] = new SqlParameter("@t314_mailiap", SqlDbType.Bit, 1);
            aParam[10].Value = t314_mailiap;
            aParam[11] = new SqlParameter("@t314_costehora", SqlDbType.Money, 8);
            aParam[11].Value = t314_costehora;
            aParam[12] = new SqlParameter("@t314_costejornada", SqlDbType.Money, 8);
            aParam[12].Value = t314_costejornada;
            aParam[13] = new SqlParameter("@t314_jornadareducida", SqlDbType.Bit, 1);
            aParam[13].Value = t314_jornadareducida;
            aParam[14] = new SqlParameter("@t314_horasjor_red", SqlDbType.Real, 4);
            aParam[14].Value = t314_horasjor_red;
            aParam[15] = new SqlParameter("@t314_fdesde_red", SqlDbType.SmallDateTime, 4);
            aParam[15].Value = t314_fdesde_red;
            aParam[16] = new SqlParameter("@t314_fhasta_red", SqlDbType.SmallDateTime, 4);
            aParam[16].Value = t314_fhasta_red;
            aParam[17] = new SqlParameter("@t314_calculoJA", SqlDbType.Bit, 1);
            aParam[17].Value = t314_calculoJA;
            aParam[18] = new SqlParameter("@t450_idcategsuper", SqlDbType.Int, 4);
            aParam[18].Value = t450_idcategsuper;
            aParam[19] = new SqlParameter("@t314_acs", SqlDbType.Bit, 1);
            aParam[19].Value = t314_acs;
            aParam[20] = new SqlParameter("@t314_margencesion", SqlDbType.Real, 4);
            aParam[20].Value = t314_margencesion;
            aParam[21] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[21].Value = t422_idmoneda;
            aParam[22] = new SqlParameter("@t314_noalertas", SqlDbType.Bit, 1);
            aParam[22].Value = t314_noalertas;
            aParam[23] = new SqlParameter("@t314_contratorelevo", SqlDbType.Bit, 1);
            aParam[23].Value = t314_contratorelevo;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIO_INS_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIO_INS_I", aParam));
        }
        public static int ModificarInterno(SqlTransaction tr,
                                    int t314_idusuario,
                                    string t314_alias,
                                    Nullable<int> t303_idnodo,
                                    int t313_idempresa,
                                    DateTime t314_falta,
                                    Nullable<DateTime> t314_fbaja,
                                    bool t314_nuevogasvi,
                                    string t314_loginhermes,
                                    string t314_codcomercialsap,
                                    bool t314_controlhuecos,
                                    bool t314_mailiap,
                                    decimal t314_costehora,
                                    decimal t314_costejornada,
                                    bool t314_jornadareducida,
                                    float t314_horasjor_red,
                                    Nullable<DateTime> t314_fdesde_red,
                                    Nullable<DateTime> t314_fhasta_red,
                                    bool t314_calculoJA,
                                    Nullable<int> t450_idcategsuper,
                                    bool t314_acs,
                                    float t314_margencesion,
                                    string t422_idmoneda,
                                    bool t314_noalertas,
                                    bool t314_contratorelevo
            )
        {
            SqlParameter[] aParam = new SqlParameter[23];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_alias", SqlDbType.VarChar, 30);
            aParam[1].Value = t314_alias;
            aParam[2] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[2].Value = t313_idempresa;
            aParam[3] = new SqlParameter("@t314_falta", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = t314_falta;
            aParam[4] = new SqlParameter("@t314_fbaja", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = t314_fbaja;
            aParam[5] = new SqlParameter("@t314_nuevogasvi", SqlDbType.Bit, 1);
            aParam[5].Value = t314_nuevogasvi;
            aParam[6] = new SqlParameter("@t314_loginhermes", SqlDbType.VarChar, 25);
            aParam[6].Value = t314_loginhermes;
            aParam[7] = new SqlParameter("@t314_codcomercialsap", SqlDbType.VarChar, 3);
            aParam[7].Value = t314_codcomercialsap;
            aParam[8] = new SqlParameter("@t314_controlhuecos", SqlDbType.Bit, 1);
            aParam[8].Value = t314_controlhuecos;
            aParam[9] = new SqlParameter("@t314_mailiap", SqlDbType.Bit, 1);
            aParam[9].Value = t314_mailiap;
            aParam[10] = new SqlParameter("@t314_costehora", SqlDbType.Money, 8);
            aParam[10].Value = t314_costehora;
            aParam[11] = new SqlParameter("@t314_costejornada", SqlDbType.Money, 8);
            aParam[11].Value = t314_costejornada;
            aParam[12] = new SqlParameter("@t314_jornadareducida", SqlDbType.Bit, 1);
            aParam[12].Value = t314_jornadareducida;
            aParam[13] = new SqlParameter("@t314_horasjor_red", SqlDbType.Real, 4);
            aParam[13].Value = t314_horasjor_red;
            aParam[14] = new SqlParameter("@t314_fdesde_red", SqlDbType.SmallDateTime, 4);
            aParam[14].Value = t314_fdesde_red;
            aParam[15] = new SqlParameter("@t314_fhasta_red", SqlDbType.SmallDateTime, 4);
            aParam[15].Value = t314_fhasta_red;
            aParam[16] = new SqlParameter("@t314_calculoJA", SqlDbType.Bit, 1);
            aParam[16].Value = t314_calculoJA;
            aParam[17] = new SqlParameter("@t450_idcategsuper", SqlDbType.Int, 4);
            aParam[17].Value = t450_idcategsuper;
            aParam[18] = new SqlParameter("@t314_acs", SqlDbType.Bit, 1);
            aParam[18].Value = t314_acs;
            aParam[19] = new SqlParameter("@t314_margencesion", SqlDbType.Real, 4);
            aParam[19].Value = t314_margencesion;
            aParam[20] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[20].Value = t422_idmoneda;
            aParam[21] = new SqlParameter("@t314_noalertas", SqlDbType.Bit, 1);
            aParam[21].Value = t314_noalertas;
            aParam[22] = new SqlParameter("@t314_contratorelevo", SqlDbType.Bit, 1);
            aParam[22].Value = t314_contratorelevo;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIO_UPD_I", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_UPD_I", aParam);
        }
        public static int InsertarExterno(SqlTransaction tr,
                                    int t001_idficepi,
                                    string t314_alias,
                                    int t315_idproveedor,
                                    DateTime t314_falta,
                                    Nullable<DateTime> t314_fbaja,
                                    string t314_loginhermes,
                                    string t314_codcomercialsap,
                                    bool t314_controlhuecos,
                                    bool t314_mailiap,
                                    decimal t314_costehora,
                                    decimal t314_costejornada,
                                    bool t314_calculoJA,
                                    bool t314_acs,
                                    string t422_idmoneda,
                                    bool t314_noalertas
            )
        {
            SqlParameter[] aParam = new SqlParameter[15];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t314_alias", SqlDbType.VarChar, 30);
            aParam[1].Value = t314_alias;
            aParam[2] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[2].Value = t315_idproveedor;
            aParam[3] = new SqlParameter("@t314_falta", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = t314_falta;
            aParam[4] = new SqlParameter("@t314_fbaja", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = t314_fbaja;
            aParam[5] = new SqlParameter("@t314_loginhermes", SqlDbType.VarChar, 25);
            aParam[5].Value = t314_loginhermes;
            aParam[6] = new SqlParameter("@t314_codcomercialsap", SqlDbType.VarChar, 3);
            aParam[6].Value = t314_codcomercialsap;
            aParam[7] = new SqlParameter("@t314_controlhuecos", SqlDbType.Bit, 1);
            aParam[7].Value = t314_controlhuecos;
            aParam[8] = new SqlParameter("@t314_mailiap", SqlDbType.Bit, 1);
            aParam[8].Value = t314_mailiap;
            aParam[9] = new SqlParameter("@t314_costehora", SqlDbType.Money, 8);
            aParam[9].Value = t314_costehora;
            aParam[10] = new SqlParameter("@t314_costejornada", SqlDbType.Money, 8);
            aParam[10].Value = t314_costejornada;
            aParam[11] = new SqlParameter("@t314_calculoJA", SqlDbType.Bit, 1);
            aParam[11].Value = t314_calculoJA;
            aParam[12] = new SqlParameter("@t314_acs", SqlDbType.Bit, 1);
            aParam[12].Value = t314_acs;
            aParam[13] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[13].Value = t422_idmoneda;
            aParam[14] = new SqlParameter("@t314_noalertas", SqlDbType.Bit, 1);
            aParam[14].Value = t314_noalertas;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIO_INS_E", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIO_INS_E", aParam));
        }
        public static int ModificarExterno(SqlTransaction tr,
                                    int t314_idusuario,
                                    string t314_alias,
                                    Nullable<int> t315_idproveedor,
                                    DateTime t314_falta,
                                    Nullable<DateTime> t314_fbaja,
                                    string t314_loginhermes,
                                    string t314_codcomercialsap,
                                    bool t314_controlhuecos,
                                    bool t314_mailiap,
                                    decimal t314_costehora,
                                    decimal t314_costejornada,
                                    bool t314_calculoJA,
                                    bool t314_acs,
                                    string t422_idmoneda,
                                    bool t314_noalertas
            )
        {
            SqlParameter[] aParam = new SqlParameter[15];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_alias", SqlDbType.VarChar, 30);
            aParam[1].Value = t314_alias;
            aParam[2] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[2].Value = t315_idproveedor;
            aParam[3] = new SqlParameter("@t314_falta", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = t314_falta;
            aParam[4] = new SqlParameter("@t314_fbaja", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = t314_fbaja;
            aParam[5] = new SqlParameter("@t314_loginhermes", SqlDbType.VarChar, 25);
            aParam[5].Value = t314_loginhermes;
            aParam[6] = new SqlParameter("@t314_codcomercialsap", SqlDbType.VarChar, 3);
            aParam[6].Value = t314_codcomercialsap;
            aParam[7] = new SqlParameter("@t314_controlhuecos", SqlDbType.Bit, 1);
            aParam[7].Value = t314_controlhuecos;
            aParam[8] = new SqlParameter("@t314_mailiap", SqlDbType.Bit, 1);
            aParam[8].Value = t314_mailiap;
            aParam[9] = new SqlParameter("@t314_costehora", SqlDbType.Money, 8);
            aParam[9].Value = t314_costehora;
            aParam[10] = new SqlParameter("@t314_costejornada", SqlDbType.Money, 8);
            aParam[10].Value = t314_costejornada;
            aParam[11] = new SqlParameter("@t314_calculoJA", SqlDbType.Bit, 1);
            aParam[11].Value = t314_calculoJA;
            aParam[12] = new SqlParameter("@t314_acs", SqlDbType.Bit, 1);
            aParam[12].Value = t314_acs;
            aParam[13] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[13].Value = t422_idmoneda;
            aParam[14] = new SqlParameter("@t314_noalertas", SqlDbType.Bit, 1);
            aParam[14].Value = t314_noalertas;
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIO_UPD_E", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_UPD_E", aParam);
        }
        public static int InsertarForaneo(SqlTransaction tr,
                                    int t001_idficepi,
                                    string t314_alias,
                                    DateTime t314_falta,
                                    bool t314_controlhuecos,
                                    bool t314_mailiap,
                                    decimal t314_costehora,
                                    decimal t314_costejornada,
                                    bool t314_calculoJA,
                                    string t422_idmoneda,
                                    bool t314_noalertas
            )
        {
            SqlParameter[] aParam = new SqlParameter[10];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t314_alias", SqlDbType.VarChar, 30);
            aParam[1].Value = t314_alias;
            aParam[2] = new SqlParameter("@t314_falta", SqlDbType.SmallDateTime, 10);
            aParam[2].Value = t314_falta;
            aParam[3] = new SqlParameter("@t314_controlhuecos", SqlDbType.Bit, 1);
            aParam[3].Value = t314_controlhuecos;
            aParam[4] = new SqlParameter("@t314_mailiap", SqlDbType.Bit, 1);
            aParam[4].Value = t314_mailiap;
            aParam[5] = new SqlParameter("@t314_costehora", SqlDbType.Money, 8);
            aParam[5].Value = t314_costehora;
            aParam[6] = new SqlParameter("@t314_costejornada", SqlDbType.Money, 8);
            aParam[6].Value = t314_costejornada;
            aParam[7] = new SqlParameter("@t314_calculoJA", SqlDbType.Bit, 1);
            aParam[7].Value = t314_calculoJA;
            aParam[8] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[8].Value = t422_idmoneda;
            aParam[9] = new SqlParameter("@t314_noalertas", SqlDbType.Bit, 1);
            aParam[9].Value = t314_noalertas;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIO_INS_F", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIO_INS_F", aParam));
        }

        public static decimal CosteContratante(decimal nCosteRep, double nMargen)
        {
            return (100 + (decimal)nMargen) / 100 * nCosteRep;
        }

        public static SqlDataReader ObtenerUsuarioCambioEstructura(string sOpcion,
                                                                    string sApellido1,
                                                                    string sApellido2,
                                                                    string sNombre,
                                                                    Nullable<int> t303_idnodo,
                                                                    bool bMostrarBajas,
                                                                    string sValor)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@sOpcion", SqlDbType.Char, 1);
            aParam[0].Value = sOpcion;
            aParam[1] = new SqlParameter("@t001_apellido1", SqlDbType.VarChar, 25);
            aParam[1].Value = sApellido1;
            aParam[2] = new SqlParameter("@t001_apellido2", SqlDbType.VarChar, 25);
            aParam[2].Value = sApellido2;
            aParam[3] = new SqlParameter("@t001_nombre", SqlDbType.VarChar, 20);
            aParam[3].Value = sNombre;
            aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo;
            aParam[5] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[5].Value = bMostrarBajas;
            aParam[6] = new SqlParameter("@sValor", SqlDbType.VarChar, 8000);
            aParam[6].Value = sValor;

            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURA_USUARIO_CAT", aParam);
        }

        public static SqlDataReader ObtenerUsuariosCambioEstructuraParesDatos(string sParesDatos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sParesDatos", SqlDbType.VarChar, 8000);
            aParam[0].Value = sParesDatos;

            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURAUSUARIO_LISTA_CAT", aParam);
        }

        public static int UpdateNodo(SqlTransaction tr, int t314_idusuario, int t303_idnodo, int t464_anomes)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@t464_anomes", SqlDbType.Int, 4);
            aParam[2].Value = t464_anomes;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_NODO", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_U_NODO", aParam);
        }
        public static int UpdateCRP(SqlTransaction tr, int t314_idusuario, bool t314_crp)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_crp", SqlDbType.Bit, 1);
            aParam[1].Value = t314_crp;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_CRP", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_U_CRP", aParam);
        }


        public static SqlDataReader GetProfVisibles(int t314_idusuario, Nullable<int> t303_idnodo, string t001_apellido1, string t001_apellido2,
                                              string t001_nombre, bool bMostrarBajas)
        {

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                SqlParameter[] aParam = new SqlParameter[6];
                aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
                aParam[0].Value = t303_idnodo;
                aParam[1] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
                aParam[1].Value = t001_apellido1;
                aParam[2] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
                aParam[2].Value = t001_apellido2;
                aParam[3] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
                aParam[3].Value = t001_nombre;
                aParam[4] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
                aParam[4].Value = bMostrarBajas;
                aParam[5] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                aParam[5].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                return SqlHelper.ExecuteSqlDataReader("SUP_PROF_VISIBLES_ADM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[7];
                aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
                aParam[0].Value = t314_idusuario;
                aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
                aParam[1].Value = t303_idnodo;
                aParam[2] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
                aParam[2].Value = t001_apellido1;
                aParam[3] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
                aParam[3].Value = t001_apellido2;
                aParam[4] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
                aParam[4].Value = t001_nombre;
                aParam[5] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
                aParam[5].Value = bMostrarBajas;
                aParam[6] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
                aParam[6].Value = (bool)HttpContext.Current.Session["FORANEOS"];

                return SqlHelper.ExecuteSqlDataReader("SUP_PROF_VISIBLES_CAT", aParam);
            }

        }
        public static SqlDataReader GetProfJerar(int t314_idusuario, string t001_apellido1, string t001_apellido2,
                                              string t001_nombre, string CR, bool bMostrarBajas)
        {

            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido1;
            aParam[2] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_apellido2;
            aParam[3] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[3].Value = t001_nombre;
            aParam[4] = new SqlParameter("@CR", SqlDbType.VarChar, 8000);
            aParam[4].Value = CR;
            aParam[5] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[5].Value = bMostrarBajas;
            aParam[6] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[6].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_JERARQ", aParam);
        }
        public static SqlDataReader PARTE_ACTIVIDAD(
                   int t314_idusuario,
                   string t301_categoria,
                   string t305_cualidad,
                   DateTime dtFechaFesde,
                   DateTime dtFechaHasta,
                   string sProyectos,
                   string sClientes,
                   string sResponsables,
                   string sNaturalezas,
                   string sHorizontal,
                   string sModeloContrato,
                   string sContrato,
                   string sIDEstructura,
                   string sSectores,
                   string sSegmentos,
                   bool bComparacionLogica,
                   string sCNP,
                   string sCSN1P,
                   string sCSN2P,
                   string sCSN3P,
                   string sCSN4P,
                   string sProfesionales,
                   Nullable<bool> facturable,
                   string t422_idmoneda
               )
        {
            SqlParameter[] aParam = new SqlParameter[24];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[1].Value = t301_categoria;
            aParam[2] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[2].Value = t305_cualidad;
            aParam[3] = new SqlParameter("@fDesde", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = dtFechaFesde;
            aParam[4] = new SqlParameter("@fHasta", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = dtFechaHasta;
            aParam[5] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[5].Value = sProyectos;
            aParam[6] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[6].Value = sClientes;
            aParam[7] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[7].Value = sResponsables;
            aParam[8] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[8].Value = sNaturalezas;
            aParam[9] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[9].Value = sHorizontal;
            aParam[10] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[10].Value = sModeloContrato;
            aParam[11] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sContrato;
            aParam[12] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[12].Value = sIDEstructura;
            aParam[13] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[13].Value = sSectores;
            aParam[14] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSegmentos;
            aParam[15] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[15].Value = bComparacionLogica;
            aParam[16] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[16].Value = sCNP;
            aParam[17] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCSN1P;
            aParam[18] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN2P;
            aParam[19] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN3P;
            aParam[20] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN4P;
            aParam[21] = new SqlParameter("@sProfesionales", SqlDbType.VarChar, 8000);
            aParam[21].Value = sProfesionales;
            aParam[22] = new SqlParameter("@bFacturable", SqlDbType.Bit, 1);
            aParam[22].Value = facturable;
            aParam[23] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[23].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_PARTE_ACTIVIDAD_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PARTE_ACTIVIDAD_USU", aParam);
        }

        public static SqlDataReader GetProfCRVisibles(int t314_idusuario, int t303_idnodo, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[2].Value = mostrarBajas;
            

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_USU_NODO_VISI", aParam);
        }
        public static SqlDataReader GetProfProvVisibles(int t314_idusuario, int t315_idproveedor, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[1].Value = t315_idproveedor;
            aParam[2] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[2].Value = mostrarBajas;


            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_USU_PROV_VISI", aParam);
        }
        public static SqlDataReader GetProfGFVisibles(int t314_idusuario, int t342_idgrupo, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
            aParam[1].Value = t342_idgrupo;
            aParam[2] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[2].Value = mostrarBajas;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_USU_GF_VISI", aParam);
        }
        public static SqlDataReader GetProfPROYVisibles(int t314_idusuario, int t301_idproyecto, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t301_idproyecto;
            aParam[2] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[2].Value = mostrarBajas;
            aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[3].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_USU_PROY_VISI", aParam);
        }
        public static SqlDataReader GetProfMiEq(string t001_apellido1, string t001_apellido2,
                                              string t001_nombre, bool bMostrarBajas, int? t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;
            aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo;
            aParam[5] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[5].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_VISIBLES_MIEQ", aParam);
        }
        public static SqlDataReader GetProfAdm(string t001_apellido1, string t001_apellido2,
                                              string t001_nombre, bool bMostrarBajas, int? t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;
            aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo;
            aParam[5] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[5].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_VISIBLES_ADM", aParam);
        }
        public static SqlDataReader GetProfCVT(string t001_apellido1, string t001_apellido2, string t001_nombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;
            aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[4].Value = null;
            aParam[5] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[5].Value = false;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_VISIBLES_ADM", aParam);
        }
        public static SqlDataReader GetProfUSA(string t001_apellido1, string t001_apellido2, string t001_nombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = bMostrarBajas;
            aParam[4] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            //aParam[4].Value = (bool)HttpContext.Current.Session["FORANEOS"];
            //En principio no vamos a poder poner a Foraneos como Usuarios de Soporte Administrativo
            aParam[4].Value = false;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_VISIBLES_USA", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Devuelve una relación de usuarios que son SAT o SAA en proyectos abiertos
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader GetProfUSAReales(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_USA_CAT", aParam);
        }
        public static SqlDataReader GetProfActivos(bool bMostrarActivos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@bMostrarActivos", SqlDbType.Bit, 1);
            aParam[0].Value = bMostrarActivos;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_ACTIVOS_ADM", aParam);
        }

        public static SqlDataReader GetProfCRAdm(int t303_idnodo, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = mostrarBajas;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ADM_NODO_VISI", aParam);
        }
        public static SqlDataReader GetProfProvAdm(int t315_idproveedor, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t315_idproveedor", SqlDbType.Int, 4);
            aParam[0].Value = t315_idproveedor;
            aParam[1] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = mostrarBajas;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ADM_PROV_VISI", aParam);
        }
        public static SqlDataReader GetProfCRPNodo(int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_CRPNODO", aParam);
        }
        public static SqlDataReader GetProfesionalesNodo(int t303_idnodo, string sApellido1, string sApellido2, string sNombre)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@sApellido1", SqlDbType.VarChar, 50);
            aParam[1].Value = sApellido1;
            aParam[2] = new SqlParameter("@sApellido2", SqlDbType.VarChar, 50);
            aParam[2].Value = sApellido2;
            aParam[3] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[3].Value = sNombre;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TECNICOS_NODO", aParam);
        }

        public static SqlDataReader GetProfGFAdm(int t342_idgrupo, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
            aParam[0].Value = t342_idgrupo;
            aParam[1] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[1].Value = (bool)HttpContext.Current.Session["FORANEOS"];
            aParam[2] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[2].Value = mostrarBajas;
            
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ADM_GF_VISI", aParam);
        }
        public static SqlDataReader GetProfPROYAdm(int t301_idproyecto, bool mostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@mostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = mostrarBajas;
            aParam[2] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[2].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ADM_PROY_VISI", aParam);
        }
        public static int UpdateDiamante(int t314_idusuario, bool t314_diamante)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_diamante", SqlDbType.Bit, 1);
            aParam[1].Value = t314_diamante;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_DIA", aParam);
        }

        public static int UpdateResolucion(int nOpcion, int t314_idusuario, bool bResolucion1024)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@bResolucion1024", SqlDbType.Bit, 1);
            aParam[2].Value = bResolucion1024;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_RESOLUCION", aParam);
        }
        public static int UpdateBaseCalculo(int t314_idusuario, bool nOpcion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_calculoonline", SqlDbType.Bit, 1);
            aParam[1].Value = nOpcion;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_CALCULOONLINE", aParam);
        }
        public static int UpdateImportacionGasvi(int t314_idusuario, byte nOpcion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_importaciongasvi", SqlDbType.TinyInt, 1);
            aParam[1].Value = nOpcion;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_IMPGASVI", aParam);
        }
        public static int UpdateCorreosInformativos(int t314_idusuario, bool t314_recibirmails)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_recibirmails", SqlDbType.Bit, 1);
            aParam[1].Value = t314_recibirmails;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_CI", aParam);
        }
        public static int UpdateAcceso(int t314_idusuario, bool t314_accesohabilitado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_accesohabilitado", SqlDbType.Bit, 1);
            aParam[1].Value = t314_accesohabilitado;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_ACCESO", aParam);
        }
        public static int UpdateMensajeBienvenida(int t314_idusuario, int t314_nsegmb)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_nsegmb", SqlDbType.TinyInt, 1);
            aParam[1].Value = t314_nsegmb;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_MB", aParam);
        }
        public static int UpdatePeriodificacionProyectos(int t314_idusuario, bool bOpcion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_defectoperiodificacion", SqlDbType.Bit, 1);
            aParam[1].Value = bOpcion;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_PERPROY", aParam);
        }
        public static int UpdateMultiVentana(int t314_idusuario, bool bOpcion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t314_multiventana", SqlDbType.Bit, 1);
            aParam[1].Value = bOpcion;

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_MULVEN", aParam);
        }
        public static int UpdateObtencionEstructura(int t314_idusuario, bool nOpcion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t314_cargaestructura", SqlDbType.Bit, 1, nOpcion);

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_CARGAESTRUCTURA", aParam);
        }
        public static int UpdateMonedaVDP(int t314_idusuario, string t422_idmoneda_VDP)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_VDP", SqlDbType.VarChar, 5, t422_idmoneda_VDP);

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_VDP", aParam);
        }
        public static int UpdateMonedaVDC(int t314_idusuario, string t422_idmoneda_VDC)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_VDC", SqlDbType.VarChar, 5, t422_idmoneda_VDC);

            return SqlHelper.ExecuteNonQuery("SUP_USUARIO_U_VDC", aParam);
        }
        /// <summary>
        /// Actualiza la password de un usuario para el acceso a servicios de recuperación de información
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <param name="t314_password"></param>
        public static void GrabarPasswServicio(int t314_idusuario, string t314_password)
        {
            SUPER.Capa_Datos.USUARIO.GrabarPasswServicio(t314_idusuario, t314_password);
        }
        public static string GetPasswServicio(int t314_idusuario)
        {
            string sRes = "";
            SqlDataReader dr = SUPER.Capa_Datos.USUARIO.GetDatos(t314_idusuario);
            if (dr.Read())
            {
                sRes = dr["t314_password"].ToString();
            }
            dr.Close();
            dr.Dispose();
            return sRes;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el nº de proyectos visibles desde la visión económica
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int GetNumProyEco(SqlTransaction tr, int t314_idusuario)
        {
            int iNumProy = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                iNumProy = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROY_COUNT_ECO", aParam));
            else
                iNumProy = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROY_COUNT_ECO", aParam));

            return iNumProy;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el nº de proyectos visibles desde la visión técnica
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int GetNumProyTec(SqlTransaction tr, int t314_idusuario)
        {
            int iNumProy = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                iNumProy = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROY_COUNT_TEC", aParam));
            else
                iNumProy = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROY_COUNT_TEC", aParam));

            return iNumProy;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Establece si hay que cargar el array oculto de proyectos accesibles
        /// </summary>
        /// -----------------------------------------------------------------------------
        //public static string CargarCriterios(string sVision)
        //{
        //    string sRes="true";
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
        //        sRes = "false";
        //    else
        //    {
        //        if (sVision == "ECO")
        //        {
        //            if (HttpContext.Current.Session["OVF_ECO"].ToString() == "T")
        //                sRes = "false";
        //        }
        //        else
        //        {
        //            if (HttpContext.Current.Session["OVF_TEC"].ToString() == "T")
        //                sRes = "false";
        //        }
        //    }
        //    return sRes;
        //}
        //Para exportaciones masivas a Excel
        public static SqlDataReader GetUsuariosReceptores(string slCodRed)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@sProfesionales", SqlDbType.VarChar, 8000, slCodRed)   
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_PROF_MAIL", aParam);
        }

        public static SqlDataReader ObtenerUsuarioFiguras
        (
                string t301_estado,
                string t301_categoria,
                string t305_cualidad,
                string sClientes,

                string sFiguraItem,
                string sProfesionales,

                string sNaturalezas,
                string sHorizontal,
                string sModeloContrato,
                string sContrato,

                string sIDSubnodos,
                string sIDNodos,
                string sIDSuperNodo1,
                string sIDSuperNodo2,
                string sIDSuperNodo3,
                string sIDSuperNodo4,

                string sSectores,
                string sSegmentos,
                bool bComparacionLogica,
                string sCNP,
                string sCSN1P,
                string sCSN2P,
                string sCSN3P,
                string sCSN4P,
                string sPSN,
                string sNodosOfTec,
                string sGFuncionales
            )
        {
            SqlParameter[] aParam = new SqlParameter[27];

            aParam[0] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[0].Value = t301_estado;
            aParam[1] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[1].Value = t301_categoria;
            aParam[2] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[2].Value = t305_cualidad;
            aParam[3] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[3].Value = sClientes;

            aParam[4] = new SqlParameter("@sFiguraItem", SqlDbType.VarChar, 8000);
            aParam[4].Value = sFiguraItem;
            aParam[5] = new SqlParameter("@sProfesionales", SqlDbType.VarChar, 8000);
            aParam[5].Value = sProfesionales;

            aParam[6] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[6].Value = sNaturalezas;
            aParam[7] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[7].Value = sHorizontal;
            aParam[8] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[8].Value = sModeloContrato;
            aParam[9] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[9].Value = sContrato;
            aParam[10] = new SqlParameter("@sIDSubnodos", SqlDbType.VarChar, 8000);
            aParam[10].Value = sIDSubnodos;
            aParam[11] = new SqlParameter("@sIDNodos", SqlDbType.VarChar, 8000);
            aParam[11].Value = sIDNodos;
            aParam[12] = new SqlParameter("@sIDSuperNodo1", SqlDbType.VarChar, 8000);
            aParam[12].Value = sIDSuperNodo1;
            aParam[13] = new SqlParameter("@sIDSuperNodo2", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDSuperNodo2;
            aParam[14] = new SqlParameter("@sIDSuperNodo3", SqlDbType.VarChar, 8000);
            aParam[14].Value = sIDSuperNodo3;
            aParam[15] = new SqlParameter("@sIDSuperNodo4", SqlDbType.VarChar, 8000);
            aParam[15].Value = sIDSuperNodo4;

            aParam[16] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[16].Value = sSectores;
            aParam[17] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[17].Value = sSegmentos;
            aParam[18] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[18].Value = bComparacionLogica;
            aParam[19] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCNP;
            aParam[20] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN1P;
            aParam[21] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN2P;
            aParam[22] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCSN3P;
            aParam[23] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[23].Value = sCSN4P;
            aParam[24] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[24].Value = sPSN;
            aParam[25] = new SqlParameter("@sNodosOfTec", SqlDbType.VarChar, 8000);
            aParam[25].Value = sNodosOfTec;
            aParam[26] = new SqlParameter("@sGFuncionales", SqlDbType.VarChar, 8000);
            aParam[26].Value = sGFuncionales;

            //return SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAS_FIGURAS", aParam);
            return SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAS_FIGURAS_ADM", aParam);
        }
        public static string getUMCNodo(SqlTransaction tr, int NumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;

            if (tr == null)
                return (SqlHelper.ExecuteScalar("SUP_NODO_USER_UMC", aParam)).ToString();
            else
                return (SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODO_USER_UMC", aParam)).ToString();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si un usuario es SAT (usuario titular de soporte administrativo de proyecto económico).
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool bEsSAT(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIO_NUM_SAT", aParam)) > 0) ? true : false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIO_NUM_SAT", aParam)) > 0) ? true : false;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si un usuario es SAA (usuario alternativo de soporte administrativo de proyecto económico).
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool bEsSAA(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIO_NUM_SAA", aParam)) > 0) ? true : false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIO_NUM_SAA", aParam)) > 0) ? true : false;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si un usuario tiene figura BITACORICO en un proyecto económico
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool bEsBitacorico(SqlTransaction tr, int t314_idusuario, int t305_idproyectosubnodo)
        {
            bool bRes=false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
            object obj;
            if (tr == null)
                obj=SqlHelper.ExecuteScalar("SUP_USUARIO_BITACORICO", aParam);
            else
                obj=SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIO_BITACORICO", aParam);
            if (obj != null) bRes = true;
            return bRes;
        }
        public static DataSet ConsultaDisponibilidad
                (
                int t314_idusuario,
                int nDesde,
                int nHasta,
                string disponibilidad,
                bool bMisProyectos,
                string sRoles,
                string sSupervisores,
                string sCentroTrabajos,
                string sOficinas,
                string sProfesionales,
                string sResponsables,
                string sIDEstructura
            )
        {
            SqlParameter[] aParam = new SqlParameter[13];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@disponibilidad", SqlDbType.Char, 1);
            aParam[3].Value = disponibilidad;
            aParam[4] = new SqlParameter("@bMisProyectos", SqlDbType.Bit, 1);
            aParam[4].Value = bMisProyectos;           
            aParam[5] = new SqlParameter("@sRoles", SqlDbType.VarChar, 8000);
            aParam[5].Value = sRoles;
            aParam[6] = new SqlParameter("@sSupervisores", SqlDbType.VarChar, 8000);
            aParam[6].Value = sSupervisores;
            aParam[7] = new SqlParameter("@sCentroTrabajos", SqlDbType.VarChar, 8000);
            aParam[7].Value = sCentroTrabajos;
            aParam[8] = new SqlParameter("@sOficinas", SqlDbType.VarChar, 8000);
            aParam[8].Value = sOficinas;
            aParam[9] = new SqlParameter("@sProfesionales", SqlDbType.VarChar, 8000);
            aParam[9].Value = sProfesionales;
            aParam[10] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[10].Value = sResponsables;
            aParam[11] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[11].Value = sIDEstructura;
            aParam[12] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[12].Value = (bool)HttpContext.Current.Session["FORANEOS"];


            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteDataset("SUP_CONS_DISPONIBILIDAD_CRIT_ADMIN", aParam);
            else
                return SqlHelper.ExecuteDataset("SUP_CONS_DISPONIBILIDAD_CRIT", aParam);
        }
        public static SqlDataReader ImputacionesMensualesDetalle
                (
                string sProfesionales,
                int nDesde,
                int nHasta,
                string stipo
            )
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@Profesionales", SqlDbType.VarChar, 8000);
            aParam[0].Value = sProfesionales;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@tipo", SqlDbType.Char, 1);
            aParam[3].Value = stipo;
            return SqlHelper.ExecuteSqlDataReader("SUP_INFORIMPUMEN_DA", aParam);
        }

        public static ArrayList GetMinimoFechaAlta(SqlTransaction tr, int t314_idusuario)
        {
            ArrayList aFechas = new ArrayList();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIO.GetMinimoFechaAlta(tr, t314_idusuario);
            while (dr.Read())
            {
                string[] aDatosAux = new string[] {dr["Tipo"].ToString(), dr["Fecha"].ToString()};
                aFechas.Add(aDatosAux);
            }
            dr.Close();
            dr.Dispose();

            return aFechas;
        }


        public static int CopiarPermisos(SqlTransaction tr, int t314_idusuario_Origen, int t314_idusuario_Destino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario_old", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario_Origen;
            aParam[1] = new SqlParameter("@t314_idusuario_new", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_Destino;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_COPIA_PERMISOS", aParam);
        }
        /// <summary>
        /// Comprueba si existe una réplica con gestión en un nodo determinado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="nPE"></param>
        /// <param name="NumEmpleado"></param>
        /// <param name="nPSN"></param>
        /// <returns></returns>
        public static bool ExistenReplicaConGestionEnCR(SqlTransaction tr, int nPE, int nCR)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = nPE;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = nCR;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EXISTEREPLICAGESTIONENCR", aParam)) > 0) ? true : false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTEREPLICAGESTIONENCR", aParam)) > 0) ? true : false;
        }

        /// <summary>
        /// Dado un usuario obtiene el último mes cerrado IAP de su CR. Si no tiene CR coge el último mes cerrado IAP de la empresa
        /// Además reescribe la vble de sesión
        /// </summary>
        /// <param name="NumEmpleado"></param>
        /// <returns></returns>
        public static int GetUMCIAP(int NumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = NumEmpleado;

            int iUMC_IAP = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_UMC_IAP_S", aParam));

            if ((int)HttpContext.Current.Session["UMC_IAP"] != iUMC_IAP)
                HttpContext.Current.Session["UMC_IAP"] = iUMC_IAP;

            return iUMC_IAP;
        }
        #endregion
    }
}
