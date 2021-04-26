using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : POSICIONFAC
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T611_POSICIONFAC
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/10/2010 8:29:22	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class POSICIONFAC
	{
		#region Propiedades y Atributos

		private int _t610_idordenfac;
		public int t610_idordenfac
		{
			get {return _t610_idordenfac;}
			set { _t610_idordenfac = value ;}
		}

		private int _t611_posicion;
		public int t611_posicion
		{
			get {return _t611_posicion;}
			set { _t611_posicion = value ;}
		}

        private int? _t611_posicion_pedsap;
        public int? t611_posicion_pedsap
        {
            get { return _t611_posicion_pedsap; }
            set { _t611_posicion_pedsap = value; }
        }

        private int? _t611_posicion_facsap;
        public int? t611_posicion_facsap
        {
            get { return _t611_posicion_facsap; }
            set { _t611_posicion_facsap = value; }
        }

        private string _t611_estado;
		public string t611_estado
		{
			get {return _t611_estado;}
			set { _t611_estado = value ;}
		}

		private DateTime? _t611_fdormida;
		public DateTime? t611_fdormida
		{
			get {return _t611_fdormida;}
			set { _t611_fdormida = value ;}
		}

		private DateTime? _t611_fprocesada;
		public DateTime? t611_fprocesada
		{
			get {return _t611_fprocesada;}
			set { _t611_fprocesada = value ;}
		}

		private DateTime? _t611_ffacturada;
		public DateTime? t611_ffacturada
		{
			get {return _t611_ffacturada;}
			set { _t611_ffacturada = value ;}
		}

		private DateTime? _t611_fcontabilizada;
		public DateTime? t611_fcontabilizada
		{
			get {return _t611_fcontabilizada;}
			set { _t611_fcontabilizada = value ;}
		}

		private DateTime? _t611_fanulada;
		public DateTime? t611_fanulada
		{
			get {return _t611_fanulada;}
			set { _t611_fanulada = value ;}
		}

		private DateTime? _t611_fborrada;
		public DateTime? t611_fborrada
		{
			get {return _t611_fborrada;}
			set { _t611_fborrada = value ;}
		}

		private string _t611_descripcion;
		public string t611_descripcion
		{
			get {return _t611_descripcion;}
			set { _t611_descripcion = value ;}
		}

		private string _t611_seriefactura;
		public string t611_seriefactura
		{
			get {return _t611_seriefactura;}
			set { _t611_seriefactura = value ;}
		}

		private int? _t611_numfactura;
		public int? t611_numfactura
		{
			get {return _t611_numfactura;}
			set { _t611_numfactura = value ;}
		}

		private float _t611_unidades;
		public float t611_unidades
		{
			get {return _t611_unidades;}
			set { _t611_unidades = value ;}
		}

		private decimal _t611_preciounitario;
		public decimal t611_preciounitario
		{
			get {return _t611_preciounitario;}
			set { _t611_preciounitario = value ;}
		}

		private string _t611_denominacion;
		public string t611_denominacion
		{
			get {return _t611_denominacion;}
			set { _t611_denominacion = value ;}
		}

		private float _t611_dto_porcen;
		public float t611_dto_porcen
		{
			get {return _t611_dto_porcen;}
			set { _t611_dto_porcen = value ;}
		}

		private decimal _t611_dto_importe;
		public decimal t611_dto_importe
		{
			get {return _t611_dto_importe;}
			set { _t611_dto_importe = value ;}
		}
		#endregion

		#region Constructor

		public POSICIONFAC() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T611_POSICIONFAC a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2010 8:29:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t610_idordenfac, int t611_posicion)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
			aParam[0].Value = t610_idordenfac;
			aParam[1] = new SqlParameter("@t611_posicion", SqlDbType.Int, 4);
			aParam[1].Value = t611_posicion;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_POSICIONFAC_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POSICIONFAC_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T611_POSICIONFAC,
		/// y devuelve una instancia u objeto del tipo POSICIONFAC
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2010 8:29:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static POSICIONFAC Select(SqlTransaction tr, int t610_idordenfac, int t611_posicion) 
		{
			POSICIONFAC o = new POSICIONFAC();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
			aParam[0].Value = t610_idordenfac;
			aParam[1] = new SqlParameter("@t611_posicion", SqlDbType.Int, 4);
			aParam[1].Value = t611_posicion;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_POSICIONFAC_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_POSICIONFAC_S", aParam);

			if (dr.Read())
			{
				if (dr["t610_idordenfac"] != DBNull.Value)
					o.t610_idordenfac = int.Parse(dr["t610_idordenfac"].ToString());
				if (dr["t611_posicion"] != DBNull.Value)
					o.t611_posicion = int.Parse(dr["t611_posicion"].ToString());
                if (dr["t611_posicion_pedsap"] != DBNull.Value)
                    o.t611_posicion_pedsap = int.Parse(dr["t611_posicion_pedsap"].ToString());
                if (dr["t611_posicion_facsap"] != DBNull.Value)
                    o.t611_posicion_facsap = int.Parse(dr["t611_posicion_facsap"].ToString());
                if (dr["t611_estado"] != DBNull.Value)
					o.t611_estado = (string)dr["t611_estado"];
				if (dr["t611_fdormida"] != DBNull.Value)
					o.t611_fdormida = (DateTime)dr["t611_fdormida"];
				if (dr["t611_fprocesada"] != DBNull.Value)
					o.t611_fprocesada = (DateTime)dr["t611_fprocesada"];
				if (dr["t611_ffacturada"] != DBNull.Value)
					o.t611_ffacturada = (DateTime)dr["t611_ffacturada"];
				if (dr["t611_fcontabilizada"] != DBNull.Value)
					o.t611_fcontabilizada = (DateTime)dr["t611_fcontabilizada"];
				if (dr["t611_fanulada"] != DBNull.Value)
					o.t611_fanulada = (DateTime)dr["t611_fanulada"];
				if (dr["t611_fborrada"] != DBNull.Value)
					o.t611_fborrada = (DateTime)dr["t611_fborrada"];
				if (dr["t611_descripcion"] != DBNull.Value)
					o.t611_descripcion = (string)dr["t611_descripcion"];
				if (dr["t611_seriefactura"] != DBNull.Value)
					o.t611_seriefactura = (string)dr["t611_seriefactura"];
				if (dr["t611_numfactura"] != DBNull.Value)
					o.t611_numfactura = int.Parse(dr["t611_numfactura"].ToString());
				if (dr["t611_unidades"] != DBNull.Value)
					o.t611_unidades = float.Parse(dr["t611_unidades"].ToString());
				if (dr["t611_preciounitario"] != DBNull.Value)
					o.t611_preciounitario = decimal.Parse(dr["t611_preciounitario"].ToString());
				if (dr["t611_denominacion"] != DBNull.Value)
					o.t611_denominacion = (string)dr["t611_denominacion"];
				if (dr["t611_dto_porcen"] != DBNull.Value)
					o.t611_dto_porcen = float.Parse(dr["t611_dto_porcen"].ToString());
				if (dr["t611_dto_importe"] != DBNull.Value)
					o.t611_dto_importe = decimal.Parse(dr["t611_dto_importe"].ToString());

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de POSICIONFAC"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
