using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : OFIEMPCAL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T068_OFIEMPCAL
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	22/11/2006 10:11:23	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class OFIEMPCAL
	{
		#region Propiedades y Atributos

		private short _T010_IDOFICINA;
		public short T010_IDOFICINA
		{
			get {return _T010_IDOFICINA;}
			set { _T010_IDOFICINA = value ;}
		}

		private short _T313_IDEMPRESA;
		public short T313_IDEMPRESA
		{
			get {return _T313_IDEMPRESA;}
			set { _T313_IDEMPRESA = value ;}
		}

		private int _t066_idcal;
		public int t066_idcal
		{
			get {return _t066_idcal;}
			set { _t066_idcal = value ;}
		}
		#endregion

		#region Constructores

		public OFIEMPCAL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T068_OFIEMPCAL.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	22/11/2006 10:11:23
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, short T010_IDOFICINA, short T313_IDEMPRESA, Nullable<int> t066_idcal)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@T010_IDOFICINA", SqlDbType.SmallInt, 2);
			aParam[0].Value = T010_IDOFICINA;
			aParam[1] = new SqlParameter("@T313_IDEMPRESA", SqlDbType.SmallInt, 2);
			aParam[1].Value = T313_IDEMPRESA;
			aParam[2] = new SqlParameter("@t066_idcal", SqlDbType.Int, 4);
			aParam[2].Value = t066_idcal;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_OFIEMPCAL_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_OFIEMPCAL_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T068_OFIEMPCAL.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	22/11/2006 10:11:23
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<short> T313_IDEMPRESA, Nullable<short> T010_IDOFICINA, Nullable<byte> nCount)
		{
            if (T313_IDEMPRESA == 0) T313_IDEMPRESA = null;
            if (T010_IDOFICINA == 0) T010_IDOFICINA = null;
			SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@T313_IDEMPRESA", SqlDbType.SmallInt, 2);
            aParam[0].Value = T313_IDEMPRESA;
			aParam[1] = new SqlParameter("@T010_IDOFICINA", SqlDbType.SmallInt, 2);
			aParam[1].Value = T010_IDOFICINA;
            aParam[2] = new SqlParameter("@nCount", SqlDbType.TinyInt, 1);
            aParam[2].Value = nCount;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_OFIEMPCAL_C", aParam);
		}

		#endregion
	}
}
