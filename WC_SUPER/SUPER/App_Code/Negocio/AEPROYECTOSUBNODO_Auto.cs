using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AEPROYECTOSUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T318_AEPROYECTOSUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	08/07/2008 12:10:45	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AEPROYECTOSUBNODO
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t341_idae;
		public int t341_idae
		{
			get {return _t341_idae;}
			set { _t341_idae = value ;}
		}

		private int _t340_idvae;
		public int t340_idvae
		{
			get {return _t340_idvae;}
			set { _t340_idvae = value ;}
		}
		#endregion

		#region Constructor

		public AEPROYECTOSUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T318_AEPROYECTOSUBNODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	08/07/2008 12:10:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t305_idproyectosubnodo , int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[1].Value = t340_idvae;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_AEPROYECTOSUBNODO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEPROYECTOSUBNODO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T318_AEPROYECTOSUBNODO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	08/07/2008 12:10:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t305_idproyectosubnodo, int t341_idae, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[1].Value = t341_idae;
            aParam[2] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
            aParam[2].Value = t340_idvae;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_AEPROYECTOSUBNODO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEPROYECTOSUBNODO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T318_AEPROYECTOSUBNODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	08/07/2008 12:10:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t305_idproyectosubnodo, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[1].Value = t340_idvae;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_AEPROYECTOSUBNODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AEPROYECTOSUBNODO_D", aParam);
		}

		#endregion
	}
}
