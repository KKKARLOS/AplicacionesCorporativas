using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TERRITORIO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T007_TERRITORIO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/09/2009 12:07:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TERRITORIO
	{
		#region Propiedades y Atributos

		private short _T007_IDTERRFIS;
		public short T007_IDTERRFIS
		{
			get {return _T007_IDTERRFIS;}
			set { _T007_IDTERRFIS = value ;}
		}

		private string _T007_NOMTERRFIS;
		public string T007_NOMTERRFIS
		{
			get {return _T007_NOMTERRFIS;}
			set { _T007_NOMTERRFIS = value ;}
		}

		private decimal _T007_ITERDC;
		public decimal T007_ITERDC
		{
			get {return _T007_ITERDC;}
			set { _T007_ITERDC = value ;}
		}

		private decimal _T007_ITERMD;
		public decimal T007_ITERMD
		{
			get {return _T007_ITERMD;}
			set { _T007_ITERMD = value ;}
		}

		private decimal _T007_ITERDA;
		public decimal T007_ITERDA
		{
			get {return _T007_ITERDA;}
			set { _T007_ITERDA = value ;}
		}

		private decimal _T007_ITERDE;
		public decimal T007_ITERDE
		{
			get {return _T007_ITERDE;}
			set { _T007_ITERDE = value ;}
		}

		private decimal _T007_ITERK;
		public decimal T007_ITERK
		{
			get {return _T007_ITERK;}
			set { _T007_ITERK = value ;}
		}

		private string _T007_CODSAP;
		public string T007_CODSAP
		{
			get {return _T007_CODSAP;}
			set { _T007_CODSAP = value ;}
		}
		#endregion

		#region Constructor

		public TERRITORIO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T007_TERRITORIO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	03/09/2009 12:07:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<short> T007_IDTERRFIS, string T007_NOMTERRFIS, Nullable<decimal> T007_ITERDC, Nullable<decimal> T007_ITERMD, Nullable<decimal> T007_ITERDA, Nullable<decimal> T007_ITERDE, Nullable<decimal> T007_ITERK, string T007_CODSAP, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@T007_IDTERRFIS", SqlDbType.SmallInt, 2);
			aParam[0].Value = T007_IDTERRFIS;
			aParam[1] = new SqlParameter("@T007_NOMTERRFIS", SqlDbType.Text, 25);
			aParam[1].Value = T007_NOMTERRFIS;
			aParam[2] = new SqlParameter("@T007_ITERDC", SqlDbType.Money, 8);
			aParam[2].Value = T007_ITERDC;
			aParam[3] = new SqlParameter("@T007_ITERMD", SqlDbType.Money, 8);
			aParam[3].Value = T007_ITERMD;
			aParam[4] = new SqlParameter("@T007_ITERDA", SqlDbType.Money, 8);
			aParam[4].Value = T007_ITERDA;
			aParam[5] = new SqlParameter("@T007_ITERDE", SqlDbType.Money, 8);
			aParam[5].Value = T007_ITERDE;
			aParam[6] = new SqlParameter("@T007_ITERK", SqlDbType.Money, 8);
			aParam[6].Value = T007_ITERK;
			aParam[7] = new SqlParameter("@T007_CODSAP", SqlDbType.Text, 2);
			aParam[7].Value = T007_CODSAP;

			aParam[8] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[8].Value = nOrden;
			aParam[9] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[9].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_TERRITORIO_C", aParam);
		}

		#endregion
	}
}
