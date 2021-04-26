using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CECPROYECTO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T436_CECPROYECTO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	10/12/2008 16:49:59	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CECPROYECTO
	{
		#region Propiedades y Atributos

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private int _t345_idcec;
		public int t345_idcec
		{
			get {return _t345_idcec;}
			set { _t345_idcec = value ;}
		}

		private int _t435_idvcec;
		public int t435_idvcec
		{
			get {return _t435_idvcec;}
			set { _t435_idvcec = value ;}
		}
		#endregion

		#region Constructor

		public CECPROYECTO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T436_CECPROYECTO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2008 16:49:59
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t301_idproyecto , int t435_idvcec)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
			aParam[1].Value = t435_idvcec;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CECPROYECTO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECPROYECTO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T436_CECPROYECTO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2008 16:49:59
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t301_idproyecto, int t345_idcec, int t435_idvcec)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
			aParam[1].Value = t345_idcec;
			aParam[2] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
			aParam[2].Value = t435_idvcec;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CECPROYECTO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECPROYECTO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T436_CECPROYECTO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/12/2008 16:49:59
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t301_idproyecto, int t435_idvcec)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
			aParam[1].Value = t435_idvcec;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CECPROYECTO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CECPROYECTO_D", aParam);
		}

		#endregion
	}
}
