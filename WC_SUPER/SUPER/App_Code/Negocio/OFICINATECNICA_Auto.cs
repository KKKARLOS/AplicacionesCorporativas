using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : OFICINATECNICA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T348_OFICINATECNICA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/12/2007 16:20:15	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class OFICINATECNICA
	{
		#region Propiedades y Atributos

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructores

		public OFICINATECNICA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T348_OFICINATECNICA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	03/12/2007 16:20:15
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t303_idnodo , int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("PRU_OFICINATECNICA_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "PRU_OFICINATECNICA_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T348_OFICINATECNICA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	03/12/2007 16:20:15
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t303_idnodo, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("PRU_OFICINATECNICA_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "PRU_OFICINATECNICA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T348_OFICINATECNICA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	03/12/2007 16:20:15
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t303_idnodo, Nullable<int> t314_idusuario, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[2].Value = nOrden;
			aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[3].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("PRU_OFICINATECNICA_C", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Borra los registros de la tabla T348_OFICINATECNICA en función de una foreign key.
        /// </summary>
        /// <remarks>
        /// 	Creado por [sqladmin]	03/12/2007 16:58:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void DeleteByT303_idnodo(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("PRU_OFICINATECNICA_DByT303_idnodo", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "PRU_OFICINATECNICA_DByT303_idnodo", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Borra los registros de la tabla T348_OFICINATECNICA en función de una foreign key.
        /// </summary>
        /// <remarks>
        /// 	Creado por [sqladmin]	03/12/2007 16:58:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void DeleteByT314_idusuario(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("PRU__OFICINATECNICA_DByT314_idusuario", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "PRU__OFICINATECNICA_DByT314_idusuario", aParam);
        }

		#endregion
	}
}
