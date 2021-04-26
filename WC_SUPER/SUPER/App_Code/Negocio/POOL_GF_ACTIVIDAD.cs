using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : POOL_GF_ACTIVIDAD
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T407_POOL_GF_ACTIVIDAD
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	27/12/2007 9:49:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class POOL_GF_ACTIVIDAD
	{
		#region Propiedades y Atributos

		private int _t335_idactividad;
		public int t335_idactividad
		{
			get {return _t335_idactividad;}
			set { _t335_idactividad = value ;}
		}

		private int _t342_idgrupro;
		public int t342_idgrupro
		{
			get {return _t342_idgrupro;}
			set { _t342_idgrupro = value ;}
		}
		#endregion

		#region Constructores

		public POOL_GF_ACTIVIDAD() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T407_POOL_GF_ACTIVIDAD.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	27/12/2007 9:49:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t335_idactividad , int t342_idgrupro)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
			aParam[0].Value = t335_idactividad;
			aParam[1] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
			aParam[1].Value = t342_idgrupro;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_POOL_GF_ACTIVIDAD_I_SNE", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_GF_ACTIVIDAD_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T407_POOL_GF_ACTIVIDAD a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	27/12/2007 9:49:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t335_idactividad, int t342_idgrupro)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
			aParam[0].Value = t335_idactividad;
			aParam[1] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
			aParam[1].Value = t342_idgrupro;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_POOL_GF_ACTIVIDAD_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_GF_ACTIVIDAD_D", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de los grupos funcionales de la UNE marcando los de la tabla T407_POOL_GF_ACTIVIDAD.
        /// menos los GF que vengan heredados del PT y de la Fase (si existe)
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	27/12/2007 9:49:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int t331_idpt, int t334_idfase, int t335_idactividad)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[1].Value = t334_idfase;
            aParam[2] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[2].Value = t335_idactividad;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_POOL_GF_ACTIVIDAD_C", aParam);
        }
		#endregion
	}
}
