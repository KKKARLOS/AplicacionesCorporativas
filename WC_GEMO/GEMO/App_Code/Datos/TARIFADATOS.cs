using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : TARIFADATOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T711_TARIFADATOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TARIFADATOS
	{
		#region Propiedades y Atributos

		private short _t711_idtarifa;
		public short t711_idtarifa
		{
			get {return _t711_idtarifa;}
			set { _t711_idtarifa = value ;}
		}

		private string _t711_denominacion;
		public string t711_denominacion
		{
			get {return _t711_denominacion;}
			set { _t711_denominacion = value ;}
		}

		private byte _t063_idproveedor;
		public byte t063_idproveedor
		{
			get {return _t063_idproveedor;}
			set { _t063_idproveedor = value ;}
		}

		private string _t711_codtarifaprov;
		public string t711_codtarifaprov
		{
			get {return _t711_codtarifaprov;}
			set { _t711_codtarifaprov = value ;}
		}

		private double _t711_precio;
		public double t711_precio
		{
			get {return _t711_precio;}
			set { _t711_precio = value ;}
		}
/*
		private double _t711_desde_acep;
		public double t711_desde_acep
		{
			get {return _t711_desde_acep;}
			set { _t711_desde_acep = value ;}
		}

		private double _t711_hasta_acep;
		public double t711_hasta_acep
		{
			get {return _t711_hasta_acep;}
			set { _t711_hasta_acep = value ;}
		}
 */
		#endregion

		#region Constructor

		public TARIFADATOS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T711_TARIFADATOS,
        /// y devuelve una instancia u objeto del tipo TARIFADATOS
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	20/04/2011 10:07:11
        /// </history>
        /// -----------------------------------------------------------------------------
        public static TARIFADATOS Select(SqlTransaction tr, short t711_idtarifa)
        {
            TARIFADATOS o = new TARIFADATOS();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t711_idtarifa", SqlDbType.SmallInt, 2);
            aParam[0].Value = t711_idtarifa;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GEM_TARIFADATOS_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GEM_TARIFADATOS_S", aParam);

            if (dr.Read())
            {
                if (dr["t711_idtarifa"] != DBNull.Value)
                    o.t711_idtarifa = (short)dr["t711_idtarifa"];
                if (dr["t711_denominacion"] != DBNull.Value)
                    o.t711_denominacion = (string)dr["t711_denominacion"];
                if (dr["t063_idproveedor"] != DBNull.Value)
                    o.t063_idproveedor = byte.Parse(dr["t063_idproveedor"].ToString());
                if (dr["t711_codtarifaprov"] != DBNull.Value)
                    o.t711_codtarifaprov = (string)dr["t711_codtarifaprov"];
                if (dr["t711_precio"] != DBNull.Value)
                    o.t711_precio = double.Parse(dr["t711_precio"].ToString());
                /*
                if (dr["t711_desde_acep"] != DBNull.Value)
                    o.t711_desde_acep = double.Parse(dr["t711_desde_acep"].ToString());
                if (dr["t711_hasta_acep"] != DBNull.Value)
                    o.t711_hasta_acep = double.Parse(dr["t711_hasta_acep"].ToString());
                */
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de TARIFADATOS"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T711_TARIFADATOS.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t711_denominacion, byte t063_idproveedor, string t711_codtarifaprov, double t711_precio) // double t711_desde_acep, double t711_hasta_acep)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t711_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t711_denominacion;
			aParam[1] = new SqlParameter("@t063_idproveedor", SqlDbType.TinyInt, 1);
			aParam[1].Value = t063_idproveedor;
			aParam[2] = new SqlParameter("@t711_codtarifaprov", SqlDbType.Text, 10);
			aParam[2].Value = t711_codtarifaprov;
			aParam[3] = new SqlParameter("@t711_precio", SqlDbType.Float, 8);
			aParam[3].Value = t711_precio;
            /*
            aParam[4] = new SqlParameter("@t711_desde_acep", SqlDbType.Float, 8);
			aParam[4].Value = t711_desde_acep;
            aParam[5] = new SqlParameter("@t711_hasta_acep", SqlDbType.Float, 8);
			aParam[5].Value = t711_hasta_acep;
            */

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("GEM_TARIFADATOS_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GEM_TARIFADATOS_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T711_TARIFADATOS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, short t711_idtarifa, string t711_denominacion, byte t063_idproveedor, string t711_codtarifaprov, double t711_precio) // double t711_desde_acep, double t711_hasta_acep)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t711_idtarifa", SqlDbType.SmallInt, 2);
			aParam[0].Value = t711_idtarifa;
			aParam[1] = new SqlParameter("@t711_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t711_denominacion;
			aParam[2] = new SqlParameter("@t063_idproveedor", SqlDbType.TinyInt, 1);
			aParam[2].Value = t063_idproveedor;
			aParam[3] = new SqlParameter("@t711_codtarifaprov", SqlDbType.Text, 10);
			aParam[3].Value = t711_codtarifaprov;
            aParam[4] = new SqlParameter("@t711_precio", SqlDbType.Float, 8);
			aParam[4].Value = t711_precio;
            /*
            aParam[5] = new SqlParameter("@t711_desde_acep", SqlDbType.Float, 8);
			aParam[5].Value = t711_desde_acep;
            aParam[6] = new SqlParameter("@t711_hasta_acep", SqlDbType.Float, 8);
			aParam[6].Value = t711_hasta_acep;
            */

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("GEM_TARIFADATOS_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_TARIFADATOS_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T711_TARIFADATOS a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, short t711_idtarifa)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t711_idtarifa", SqlDbType.SmallInt, 2);
			aParam[0].Value = t711_idtarifa;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("GEM_TARIFADATOS_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_TARIFADATOS_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T711_TARIFADATOS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/04/2011 16:23:02
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_TARIFADATOS_C", aParam);
        }
		#endregion
	}
}
