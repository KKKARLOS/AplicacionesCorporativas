using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : EMPRESA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T313_EMPRESA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	10/12/2009 15:19:06	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EMPRESA
    {
        #region Propiedades y Atributos

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

        private string _t302_codigoexterno;
        public string t302_codigoexterno
        {
            get { return _t302_codigoexterno; }
            set { _t302_codigoexterno = value; }
        }

        private bool _t313_estado;
        public bool t313_estado
        {
            get { return _t313_estado; }
            set { _t313_estado = value; }
        }

        private bool _t313_ute;
        public bool t313_ute
        {
            get { return _t313_ute; }
            set { _t313_ute = value; }
        }

        private float _t313_horasanuales;
        public float t313_horasanuales
        {
            get { return _t313_horasanuales; }
            set { _t313_horasanuales = value; }
        }

        private float _t313_interesGF;
        public float t313_interesGF
        {
            get { return _t313_interesGF; }
            set { _t313_interesGF = value; }
        }

        private string _t313_CCIF;
        public string t313_CCIF
        {
            get { return _t313_CCIF; }
            set { _t313_CCIF = value; }
        }

        private string _t313_CCICE;
        public string t313_CCICE
        {
            get { return _t313_CCICE; }
            set { _t313_CCICE = value; }
        }

        private int? _T069_iddietakm;
        public int? T069_iddietakm
        {
            get { return _T069_iddietakm; }
            set { _T069_iddietakm = value; }
        }
        private string _t069_descripcion;
        public string t069_descripcion
        {
            get { return _t069_descripcion; }
            set { _t069_descripcion = value; }
        }
        #endregion

        #region Constructor

        public EMPRESA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T313_EMPRESA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 15:19:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t313_denominacion, string t302_codigoexterno, bool t313_ute, float t313_horasanuales, float t313_interesGF, string t313_CCIF, string t313_CCICE, Nullable<int> T069_iddietakm, bool t313_estado)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t313_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t313_denominacion;
            aParam[1] = new SqlParameter("@t302_codigoexterno", SqlDbType.Text, 15);
            aParam[1].Value = t302_codigoexterno;
            aParam[2] = new SqlParameter("@t313_ute", SqlDbType.Bit, 1);
            aParam[2].Value = t313_ute;
            aParam[3] = new SqlParameter("@t313_horasanuales", SqlDbType.Real, 4);
            aParam[3].Value = t313_horasanuales;
            aParam[4] = new SqlParameter("@t313_interesGF", SqlDbType.Real, 4);
            aParam[4].Value = t313_interesGF;
            aParam[5] = new SqlParameter("@t313_CCIF", SqlDbType.Text, 4);
            aParam[5].Value = t313_CCIF;
            aParam[6] = new SqlParameter("@t313_CCICE", SqlDbType.Text, 4);
            aParam[6].Value = t313_CCICE;
            aParam[7] = new SqlParameter("@T069_iddietakm", SqlDbType.Int, 4);
            aParam[7].Value = T069_iddietakm;
            aParam[8] = new SqlParameter("@t313_estado", SqlDbType.Bit, 1);
            aParam[8].Value = t313_estado;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EMPRESA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EMPRESA_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T313_EMPRESA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 15:19:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t313_idempresa, string t313_denominacion, string t302_codigoexterno, bool t313_ute, float t313_horasanuales, float t313_interesGF, string t313_CCIF, string t313_CCICE, Nullable<int> T069_iddietakm, bool t313_estado)
        {
            SqlParameter[] aParam = new SqlParameter[10];
            aParam[0] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[0].Value = t313_idempresa;
            aParam[1] = new SqlParameter("@t313_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t313_denominacion;
            aParam[2] = new SqlParameter("@t302_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = t302_codigoexterno;
            aParam[3] = new SqlParameter("@t313_ute", SqlDbType.Bit, 1);
            aParam[3].Value = t313_ute;
            aParam[4] = new SqlParameter("@t313_horasanuales", SqlDbType.Real, 4);
            aParam[4].Value = t313_horasanuales;
            aParam[5] = new SqlParameter("@t313_interesGF", SqlDbType.Real, 4);
            aParam[5].Value = t313_interesGF;
            aParam[6] = new SqlParameter("@t313_CCIF", SqlDbType.Text, 4);
            aParam[6].Value = t313_CCIF;
            aParam[7] = new SqlParameter("@t313_CCICE", SqlDbType.Text, 4);
            aParam[7].Value = t313_CCICE;
            aParam[8] = new SqlParameter("@T069_iddietakm", SqlDbType.Int, 4);
            aParam[8].Value = T069_iddietakm;
            aParam[9] = new SqlParameter("@t313_estado", SqlDbType.Bit, 1);
            aParam[9].Value = t313_estado;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_EMPRESA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_EMPRESA_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T313_EMPRESA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 15:19:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t313_idempresa)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[0].Value = t313_idempresa;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_EMPRESA_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_EMPRESA_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T313_EMPRESA,
        /// y devuelve una instancia u objeto del tipo EMPRESA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 15:19:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static EMPRESA Select(SqlTransaction tr, int t313_idempresa)
        {
            EMPRESA o = new EMPRESA();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[0].Value = t313_idempresa;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_EMPRESA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_EMPRESA_S", aParam);

            if (dr.Read())
            {
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];
                if (dr["t302_codigoexterno"] != DBNull.Value)
                    o.t302_codigoexterno = (string)dr["t302_codigoexterno"];
                if (dr["t313_estado"] != DBNull.Value)
                    o.t313_estado = (bool)dr["t313_estado"];
                if (dr["t313_ute"] != DBNull.Value)
                    o.t313_ute = (bool)dr["t313_ute"];
                if (dr["t313_horasanuales"] != DBNull.Value)
                    o.t313_horasanuales = float.Parse(dr["t313_horasanuales"].ToString());
                if (dr["t313_interesGF"] != DBNull.Value)
                    o.t313_interesGF = float.Parse(dr["t313_interesGF"].ToString());
                if (dr["t313_CCIF"] != DBNull.Value)
                    o.t313_CCIF = (string)dr["t313_CCIF"];
                if (dr["t313_CCICE"] != DBNull.Value)
                    o.t313_CCICE = (string)dr["t313_CCICE"];
                if (dr["T069_iddietakm"] != DBNull.Value)
                    o.T069_iddietakm = int.Parse(dr["T069_iddietakm"].ToString());
                if (dr["t069_descripcion"] != DBNull.Value)
                    o.t069_descripcion = (string)dr["t069_descripcion"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de EMPRESA"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T313_EMPRESA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 15:19:06
        /// </history>
        /// -----------------------------------------------------------------------------
        //public static SqlDataReader Catalogo(Nullable<int> t313_idempresa, string t313_denominacion, string t302_codigoexterno, Nullable<bool> t313_ute, Nullable<float> t313_horasanuales, Nullable<float> t313_interesGF, string t313_CCIF, string t313_CCICE, Nullable<int> T069_iddietakm, byte nOrden, byte nAscDesc)
        //{
        //    SqlParameter[] aParam = new SqlParameter[11];
        //    aParam[0] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
        //    aParam[0].Value = t313_idempresa;
        //    aParam[1] = new SqlParameter("@t313_denominacion", SqlDbType.Text, 50);
        //    aParam[1].Value = t313_denominacion;
        //    aParam[2] = new SqlParameter("@t302_codigoexterno", SqlDbType.Text, 15);
        //    aParam[2].Value = t302_codigoexterno;
        //    aParam[3] = new SqlParameter("@t313_ute", SqlDbType.Bit, 1);
        //    aParam[3].Value = t313_ute;
        //    aParam[4] = new SqlParameter("@t313_horasanuales", SqlDbType.Real, 4);
        //    aParam[4].Value = t313_horasanuales;
        //    aParam[5] = new SqlParameter("@t313_interesGF", SqlDbType.Real, 4);
        //    aParam[5].Value = t313_interesGF;
        //    aParam[6] = new SqlParameter("@t313_CCIF", SqlDbType.Text, 4);
        //    aParam[6].Value = t313_CCIF;
        //    aParam[7] = new SqlParameter("@t313_CCICE", SqlDbType.Text, 4);
        //    aParam[7].Value = t313_CCICE;
        //    aParam[8] = new SqlParameter("@T069_iddietakm", SqlDbType.Int, 4);
        //    aParam[8].Value = T069_iddietakm;

        //    aParam[9] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
        //    aParam[9].Value = nOrden;
        //    aParam[10] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
        //    aParam[10].Value = nAscDesc;

        //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
        //    return SqlHelper.ExecuteSqlDataReader("SUP_EMPRESA_C", aParam);
        //}

        #endregion
    }
}