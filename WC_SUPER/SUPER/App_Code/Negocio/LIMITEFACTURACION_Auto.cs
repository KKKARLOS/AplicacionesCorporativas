using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : LIMITEFACTURACION
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T637_LIMITEFACTURACION
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/12/2010 9:30:45	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class LIMITEFACTURACION
	{
		#region Propiedades y Atributos

		private int _t637_anomes;
		public int t637_anomes
		{
			get {return _t637_anomes;}
			set { _t637_anomes = value ;}
		}

		private DateTime _t637_fecha;
		public DateTime t637_fecha
		{
			get {return _t637_fecha;}
			set { _t637_fecha = value ;}
		}
		#endregion

		#region Constructor

		public LIMITEFACTURACION() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T637_LIMITEFACTURACION.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2010 9:30:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t637_anomes , DateTime t637_fecha)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t637_anomes", SqlDbType.Int, 4);
			aParam[0].Value = t637_anomes;
			aParam[1] = new SqlParameter("@t637_fecha", SqlDbType.SmallDateTime, 4);
			aParam[1].Value = t637_fecha;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_LIMITEFACTURACION_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_LIMITEFACTURACION_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T637_LIMITEFACTURACION.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2010 9:30:45
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t637_anomes, Nullable<DateTime> t637_fecha)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t637_anomes", SqlDbType.Int, 4);
			aParam[0].Value = t637_anomes;
			aParam[1] = new SqlParameter("@t637_fecha", SqlDbType.SmallDateTime, 4);
			aParam[1].Value = t637_fecha;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_LIMITEFACTURACION_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_LIMITEFACTURACION_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T637_LIMITEFACTURACION a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2010 9:30:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t637_anomes)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t637_anomes", SqlDbType.Int, 4);
			aParam[0].Value = t637_anomes;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_LIMITEFACTURACION_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_LIMITEFACTURACION_D", aParam);
		}

		#endregion
	}
}
