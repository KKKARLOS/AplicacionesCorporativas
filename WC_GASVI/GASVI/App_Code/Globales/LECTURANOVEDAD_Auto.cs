using System;
using System.Data;
using System.Data.SqlClient;
using GASVI.DAL;

namespace GASVI.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
	/// Class	 : LECTURANOVEDAD
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T072_LECTURANOVEDAD
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	30/05/2007 12:42:48	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class LECTURANOVEDAD
	{

		#region Propiedades y Atributos

		private byte _TBL_CODIGO;
		public byte TBL_CODIGO
		{
			get {return _TBL_CODIGO;}
			set { _TBL_CODIGO = value ;}
		}

		private int _T001_IDFICEPI;
		public int T001_IDFICEPI
		{
			get {return _T001_IDFICEPI;}
			set { _T001_IDFICEPI = value ;}
		}
		#endregion


		#region Constructores

		public LECTURANOVEDAD() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion


		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T072_LECTURANOVEDAD.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	30/05/2007 12:42:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, byte TBL_CODIGO , int T001_IDFICEPI)
		{
			SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TBL_CODIGO", SqlDbType.TinyInt, 1, TBL_CODIGO);
            aParam[i++] = ParametroSql.add("@T001_IDFICEPI", SqlDbType.Int, 4, T001_IDFICEPI);

			if (tr == null)
                SqlHelper.ExecuteNonQuery("APL_LECTURANOVEDAD_I", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "APL_LECTURANOVEDAD_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T072_LECTURANOVEDAD.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	30/05/2007 12:42:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<byte> TBL_CODIGO, Nullable<int> T001_IDFICEPI, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@TBL_CODIGO", SqlDbType.TinyInt, 1, TBL_CODIGO);
            aParam[i++] = ParametroSql.add("@T001_IDFICEPI", SqlDbType.Int, 4, T001_IDFICEPI);
            aParam[i++] = ParametroSql.add("@nOrden", SqlDbType.TinyInt, 1, nOrden);
            aParam[i++] = ParametroSql.add("@nAscDesc", SqlDbType.TinyInt, 1, nAscDesc);

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("APL_LECTURANOVEDAD_C", aParam);
		}

		#endregion
	}
}
