using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PLANTILLAPOSICIONFAC
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T630_PLANTILLAPOSICIONFAC
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/11/2010 10:31:35	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PLANTILLAPOSICIONFAC
	{
		#region Propiedades y Atributos

		private int _t629_idplantillaof;
		public int t629_idplantillaof
		{
			get {return _t629_idplantillaof;}
			set { _t629_idplantillaof = value ;}
		}

		private int _t630_posicion;
		public int t630_posicion
		{
			get {return _t630_posicion;}
			set { _t630_posicion = value ;}
		}

        private string _t630_denominacion;
        public string t630_denominacion
		{
            get { return _t630_denominacion; }
            set { _t630_denominacion = value; }
		}

		private string _t630_descripcion;
		public string t630_descripcion
		{
			get {return _t630_descripcion;}
			set { _t630_descripcion = value ;}
		}
        
		private float _t630_unidades;
		public float t630_unidades
		{
			get {return _t630_unidades;}
			set { _t630_unidades = value ;}
		}

		private decimal _t630_preciounitario;
		public decimal t630_preciounitario
		{
			get {return _t630_preciounitario;}
			set { _t630_preciounitario = value ;}
		}

		private float _t630_dto_porcen;
		public float t630_dto_porcen
		{
			get {return _t630_dto_porcen;}
			set { _t630_dto_porcen = value ;}
		}

		private decimal _t630_dto_importe;
		public decimal t630_dto_importe
		{
			get {return _t630_dto_importe;}
			set { _t630_dto_importe = value ;}
		}
		#endregion

		#region Constructor

		public PLANTILLAPOSICIONFAC() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T630_PLANTILLAPOSICIONFAC.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:35
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t629_idplantillaof, int t630_posicion, string t630_denominacion, string t630_descripcion, float t630_unidades, decimal t630_preciounitario, float t630_dto_porcen, decimal t630_dto_importe)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[0].Value = t629_idplantillaof;
			aParam[1] = new SqlParameter("@t630_posicion", SqlDbType.Int, 4);
			aParam[1].Value = t630_posicion;
            aParam[2] = new SqlParameter("@t630_denominacion", SqlDbType.VarChar, 40);
            aParam[2].Value = t630_denominacion;
            aParam[3] = new SqlParameter("@t630_descripcion", SqlDbType.Text, 2147483647);
			aParam[3].Value = t630_descripcion;
			aParam[4] = new SqlParameter("@t630_unidades", SqlDbType.Real, 4);
			aParam[4].Value = t630_unidades;
			aParam[5] = new SqlParameter("@t630_preciounitario", SqlDbType.Money, 8);
			aParam[5].Value = t630_preciounitario;
			aParam[6] = new SqlParameter("@t630_dto_porcen", SqlDbType.Real, 4);
			aParam[6].Value = t630_dto_porcen;
			aParam[7] = new SqlParameter("@t630_dto_importe", SqlDbType.Money, 8);
			aParam[7].Value = t630_dto_importe;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PLANTILLAPOSICIONFAC_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLAPOSICIONFAC_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T630_PLANTILLAPOSICIONFAC a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:35
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t629_idplantillaof, int t630_posicion)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[0].Value = t629_idplantillaof;
			aParam[1] = new SqlParameter("@t630_posicion", SqlDbType.Int, 4);
			aParam[1].Value = t630_posicion;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PLANTILLAPOSICIONFAC_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLAPOSICIONFAC_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T630_PLANTILLAPOSICIONFAC,
		/// y devuelve una instancia u objeto del tipo PLANTILLAPOSICIONFAC
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:35
		/// </history>
		/// -----------------------------------------------------------------------------
		public static PLANTILLAPOSICIONFAC Select(SqlTransaction tr, int t629_idplantillaof, int t630_posicion) 
		{
			PLANTILLAPOSICIONFAC o = new PLANTILLAPOSICIONFAC();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[0].Value = t629_idplantillaof;
			aParam[1] = new SqlParameter("@t630_posicion", SqlDbType.Int, 4);
			aParam[1].Value = t630_posicion;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLAPOSICIONFAC_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLAPOSICIONFAC_S", aParam);

			if (dr.Read())
			{
				if (dr["t629_idplantillaof"] != DBNull.Value)
					o.t629_idplantillaof = int.Parse(dr["t629_idplantillaof"].ToString());
				if (dr["t630_posicion"] != DBNull.Value)
					o.t630_posicion = int.Parse(dr["t630_posicion"].ToString());
				if (dr["t630_descripcion"] != DBNull.Value)
					o.t630_descripcion = (string)dr["t630_descripcion"];
				if (dr["t630_unidades"] != DBNull.Value)
					o.t630_unidades = float.Parse(dr["t630_unidades"].ToString());
				if (dr["t630_preciounitario"] != DBNull.Value)
					o.t630_preciounitario = decimal.Parse(dr["t630_preciounitario"].ToString());
				if (dr["t630_dto_porcen"] != DBNull.Value)
					o.t630_dto_porcen = float.Parse(dr["t630_dto_porcen"].ToString());
				if (dr["t630_dto_importe"] != DBNull.Value)
					o.t630_dto_importe = decimal.Parse(dr["t630_dto_importe"].ToString());

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de PLANTILLAPOSICIONFAC"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
