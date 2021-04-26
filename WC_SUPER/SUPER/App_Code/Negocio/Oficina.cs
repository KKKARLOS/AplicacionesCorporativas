using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : OFICINA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T010_OFICINA
	/// </summary>
	/// <history>
	/// 	Creado por [dotofean]	22/11/2006 9:37:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class OFICINA
	{
		#region Propiedades y Atributos

		private short _T010_IDOFICINA;
		public short T010_IDOFICINA
		{
			get {return _T010_IDOFICINA;}
			set { _T010_IDOFICINA = value ;}
		}

		private short _T009_IDCENTRAB;
		public short T009_IDCENTRAB
		{
			get {return _T009_IDCENTRAB;}
			set { _T009_IDCENTRAB = value ;}
		}

		private string _T010_DESOFICINA;
		public string T010_DESOFICINA
		{
			get {return _T010_DESOFICINA;}
			set { _T010_DESOFICINA = value ;}
		}

		private string _T010_DIRECCION;
		public string T010_DIRECCION
		{
			get {return _T010_DIRECCION;}
			set { _T010_DIRECCION = value ;}
		}

		private string _T010_TELEFONO;
		public string T010_TELEFONO
		{
			get {return _T010_TELEFONO;}
			set { _T010_TELEFONO = value ;}
		}

		private string _T010_FAX;
		public string T010_FAX
		{
			get {return _T010_FAX;}
			set { _T010_FAX = value ;}
		}

		private int _T010_IDMAGIC;
		public int T010_IDMAGIC
		{
			get {return _T010_IDMAGIC;}
			set { _T010_IDMAGIC = value ;}
		}

		private string _T010_PREFIJO;
		public string T010_PREFIJO
		{
			get {return _T010_PREFIJO;}
			set { _T010_PREFIJO = value ;}
		}

		private string _T010_MAILCENTRA;
		public string T010_MAILCENTRA
		{
			get {return _T010_MAILCENTRA;}
			set { _T010_MAILCENTRA = value ;}
		}
		#endregion

		#region Constructores

		public OFICINA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T010_OFICINA.
        /// </summary>
        /// <history>
        /// 	Creado por [dotofean]	22/11/2006 9:37:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<short> T010_IDOFICINA, string T010_DESOFICINA, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@T010_IDOFICINA", SqlDbType.SmallInt, 2);
            aParam[0].Value = T010_IDOFICINA;
            aParam[1] = new SqlParameter("@T010_DESOFICINA", SqlDbType.Text, 40);
            aParam[1].Value = T010_DESOFICINA;

            aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[2].Value = nOrden;
            aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[3].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_OFICINA_C", aParam);
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_OFICINA_CAT", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T010_OFICINA y T009_CENTRAB.
        /// </summary>
        /// <history>
        /// 	Creado por [doarhumi]	09/09/2009 9:37:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoCentro(byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[0].Value = nOrden;
            aParam[1] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[1].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_OFICINA_CENTRO_C", aParam);
        }

		#endregion
	}
}
