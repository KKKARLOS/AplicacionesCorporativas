using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
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
			aParam[0] = new SqlParameter("@TBL_CODIGO", SqlDbType.TinyInt, 1);
			aParam[0].Value = TBL_CODIGO;
			aParam[1] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
			aParam[1].Value = T001_IDFICEPI;

			int returnValue;
			if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("APL_LECTURANOVEDAD_I", aParam);
			else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "APL_LECTURANOVEDAD_I", aParam);
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
			aParam[0] = new SqlParameter("@TBL_CODIGO", SqlDbType.TinyInt, 1);
			aParam[0].Value = TBL_CODIGO;
			aParam[1] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
			aParam[1].Value = T001_IDFICEPI;

			aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[2].Value = nOrden;
			aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[3].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("APL_LECTURANOVEDAD_C", aParam);

			return dr;
		}

		#endregion
	}
}
