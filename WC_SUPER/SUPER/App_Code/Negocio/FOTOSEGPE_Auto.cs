using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FOTOSEGPE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T373_FOTOSEGPE
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	15/01/2009 9:17:01	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FOTOSEGPE
	{
		#region Propiedades y Atributos

		private int _t373_idfotope;
		public int t373_idfotope
		{
			get {return _t373_idfotope;}
			set { _t373_idfotope = value ;}
		}

		private DateTime _t373_fecha;
		public DateTime t373_fecha
		{
			get {return _t373_fecha;}
			set { _t373_fecha = value ;}
		}

		private int? _t373_anomes;
		public int? t373_anomes
		{
			get {return _t373_anomes;}
			set { _t373_anomes = value ;}
		}

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int? _t314_idusuario;
		public int? t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructor

		public FOTOSEGPE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T373_FOTOSEGPE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	15/01/2009 9:17:01
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, DateTime t373_fecha , Nullable<int> t373_anomes , int t305_idproyectosubnodo , Nullable<int> t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t373_fecha", SqlDbType.SmallDateTime, 4);
			aParam[0].Value = t373_fecha;
			aParam[1] = new SqlParameter("@t373_anomes", SqlDbType.Int, 4);
			aParam[1].Value = t373_anomes;
			aParam[2] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[2].Value = t305_idproyectosubnodo;
			aParam[3] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FOTOSEGPE_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FOTOSEGPE_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T373_FOTOSEGPE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	15/01/2009 9:17:01
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t373_idfotope)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t373_idfotope", SqlDbType.Int, 4);
			aParam[0].Value = t373_idfotope;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FOTOSEGPE_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FOTOSEGPE_D", aParam);
		}
		#endregion
	}
}
