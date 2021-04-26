using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FUNCIONESRECURSO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T357_FUNCIONESUSUARIO
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FUNCIONESRECURSO
	{
		#region Propiedades y Atributos

		private int _t356_idfuncion;
		public int t356_idfuncion
		{
			get {return _t356_idfuncion;}
			set { _t356_idfuncion = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructores

		public FUNCIONESRECURSO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T357_FUNCIONESUSUARIO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t356_idfuncion , int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t356_idfuncion", SqlDbType.Int, 4);
			aParam[0].Value = t356_idfuncion;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_FUNCIONESUSUARIO_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FUNCIONESUSUARIO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T357_FUNCIONESUSUARIO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	14/11/2007 9:21:18
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t356_idfuncion, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t356_idfuncion", SqlDbType.Int, 4);
			aParam[0].Value = t356_idfuncion;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FUNCIONESUSUARIO_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FUNCIONESUSUARIO_D", aParam);
		}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla V_SUP_FUNCIONESUSUARIO.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	16/11/2007 17:29:51
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t356_idfuncion, Nullable<int> t314_idusuario, string nombre, Nullable<short> t303_idnodo, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t356_idfuncion", SqlDbType.Int, 4);
            aParam[0].Value = t356_idfuncion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nombre", SqlDbType.Text, 73);
            aParam[2].Value = nombre;
            aParam[3] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
            aParam[3].Value = t303_idnodo;

            aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[4].Value = nOrden;
            aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[5].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FUNCIONESUSUARIO_C", aParam);

        }
        #endregion
	}
}
