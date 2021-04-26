using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CLIENTE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T302_CLIENTE
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:28	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CLIENTE
	{
        #region Propiedades y Atributos

        private int _t302_idcliente;
        public int t302_idcliente
        {
            get { return _t302_idcliente; }
            set { _t302_idcliente = value; }
        }

        private string _t302_denominacion;
        public string t302_denominacion
        {
            get { return _t302_denominacion; }
            set { _t302_denominacion = value; }
        }

        private string _t302_codigoexterno;
        public string t302_codigoexterno
        {
            get { return _t302_codigoexterno; }
            set { _t302_codigoexterno = value; }
        }

        private int? _t314_idusuario_responsable;
        public int? t314_idusuario_responsable
        {
            get { return _t314_idusuario_responsable; }
            set { _t314_idusuario_responsable = value; }
        }
        private string _RESPONSABLE;
        public string RESPONSABLE
        {
            get { return _RESPONSABLE; }
            set { _RESPONSABLE = value; }
        }
        private int _cod_sector;
        public int cod_sector
        {
            get { return _cod_sector; }
            set { _cod_sector = value; }
        }
        private string _Sector;
        public string Sector
        {
            get { return _Sector; }
            set { _Sector = value; }
        }
        private int _cod_segmento;
        public int cod_segmento
        {
            get { return _cod_segmento; }
            set { _cod_segmento = value; }
        }
        private string _Segmento;
        public string Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }
        private string _Ambito;
        public string Ambito
        {
            get { return _Ambito; }
            set { _Ambito = value; }
        }
        private string _Zona;
        public string Zona
        {
            get { return _Zona; }
            set { _Zona = value; }
        }
        private bool _t302_noalertas;
        public bool t302_noalertas
        {
            get { return _t302_noalertas; }
            set { _t302_noalertas = value; }
        }
        private bool _t302_cualificacionCVT;
        public bool t302_cualificacionCVT
        {
            get { return _t302_cualificacionCVT; }
            set { _t302_cualificacionCVT = value; }
        }
        private int? _prov_gest;
        public int? prov_gest
        {
            get { return _prov_gest; }
            set { _prov_gest = value; }
        }
        private int? _prov_fiscal;
        public int? prov_fiscal
        {
            get { return _prov_fiscal; }
            set { _prov_fiscal = value; }
        }
        private int? _pais_gest;
        public int? pais_gest
        {
            get { return _pais_gest; }
            set { _pais_gest = value; }
        }
        private int? _pais_fiscal;
        public int? pais_fiscal
        {
            get { return _pais_fiscal; }
            set { _pais_fiscal = value; }
        }

        #endregion

		#region Constructores

		public CLIENTE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T302_CLIENTE.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/07/2009 11:54:21
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t302_idcliente, Nullable<int> t314_idusuario_responsable, Nullable<bool> t302_noalertas, Nullable<bool> t302_cualificacionCVT, Nullable<int> prov_ges, Nullable<int> prov_fiscal, int cod_segmento)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[0].Value = t302_idcliente;
            aParam[1] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_responsable;
            aParam[2] = new SqlParameter("@t302_noalertas", SqlDbType.Bit, 1);
            aParam[2].Value = t302_noalertas;
            aParam[3] = new SqlParameter("@t302_cualificacionCVT", SqlDbType.Bit, 1);
            aParam[3].Value = t302_cualificacionCVT;
            aParam[4] = new SqlParameter("@t173_idprovincia_gest", SqlDbType.Int, 4);
            aParam[4].Value = prov_ges;
            aParam[5] = new SqlParameter("@t173_idprovincia_fiscal", SqlDbType.Int, 4);
            aParam[5].Value = prov_fiscal;
            aParam[6] = new SqlParameter("@t484_idsegmento", SqlDbType.Int, 4);
            aParam[6].Value = cod_segmento;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CLIENTE_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CLIENTE_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T302_CLIENTE,
        /// y devuelve una instancia u objeto del tipo CLIENTE
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/07/2009 11:54:21
        /// </history>
        /// -----------------------------------------------------------------------------
        public static CLIENTE Select(SqlTransaction tr, int t302_idcliente)
        {
            CLIENTE o = new CLIENTE();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[0].Value = t302_idcliente;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLIENTE_S", aParam);

            if (dr.Read())
            {
                if (dr["t302_idcliente"] != DBNull.Value)
                    o.t302_idcliente = int.Parse(dr["t302_idcliente"].ToString());
                if (dr["t302_denominacion"] != DBNull.Value)
                    o.t302_denominacion = (string)dr["t302_denominacion"];
                if (dr["t302_codigoexterno"] != DBNull.Value)
                    o.t302_codigoexterno = (string)dr["t302_codigoexterno"];
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["RESPONSABLE"] != DBNull.Value)
                    o.RESPONSABLE = (string)dr["RESPONSABLE"];
                if (dr["sector"] != DBNull.Value)
                    o.Sector = (string)dr["sector"];
                if (dr["segmento"] != DBNull.Value)
                    o.Segmento = (string)dr["segmento"];
                if (dr["ambito"] != DBNull.Value)
                    o.Ambito = (string)dr["ambito"];
                if (dr["zona"] != DBNull.Value)
                    o.Zona = (string)dr["zona"];
                if (dr["t302_noalertas"] != DBNull.Value)
                    o.t302_noalertas = (bool)dr["t302_noalertas"];
                if (dr["t302_cualificacionCVT"] != DBNull.Value)
                    o.t302_cualificacionCVT = (bool)dr["t302_cualificacionCVT"];
                if (dr["prov_gest"] != DBNull.Value)
                    o.prov_gest = int.Parse(dr["prov_gest"].ToString());
                if (dr["prov_fiscal"] != DBNull.Value)
                    o.prov_fiscal = int.Parse(dr["prov_fiscal"].ToString());
                if (dr["pais_gest"] != DBNull.Value)
                    o.pais_gest = int.Parse(dr["pais_gest"].ToString());
                if (dr["pais_fiscal"] != DBNull.Value)
                    o.pais_fiscal = int.Parse(dr["pais_fiscal"].ToString());
                if (dr["cod_sector"] != DBNull.Value)
                    o.cod_sector = int.Parse(dr["cod_sector"].ToString());
                if (dr["cod_segmento"] != DBNull.Value)
                    o.cod_segmento = int.Parse(dr["cod_segmento"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CLIENTE"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla T302_CLIENTE en función de una foreign key.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/07/2009 11:54:21
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByT314_idusuario_responsable(SqlTransaction tr, Nullable<int> t314_idusuario_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario_responsable;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_SByT314_idusuario_responsable", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLIENTE_SByT314_idusuario_responsable", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T302_CLIENTE.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/07/2009 11:54:21
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t302_idcliente, string t302_denominacion, string t302_codigoexterno, Nullable<int> t314_idusuario_responsable, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[0].Value = t302_idcliente;
            aParam[1] = new SqlParameter("@t302_denominacion", SqlDbType.Text, 100);
            aParam[1].Value = t302_denominacion;
            aParam[2] = new SqlParameter("@t302_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = t302_codigoexterno;
            aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[3].Value = t314_idusuario_responsable;

            aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[4].Value = nOrden;
            aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[5].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_C", aParam);
        }

        #endregion
	}
}
