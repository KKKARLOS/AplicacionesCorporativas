using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : LINEACRONOESTADO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T709_LINEACRONOESTADO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class LINEACRONOESTADO
	{
		#region Propiedades y Atributos

		private int _T709_IDLINEACRONOESTADO;
		public int T709_IDLINEACRONOESTADO
		{
			get {return _T709_IDLINEACRONOESTADO;}
			set { _T709_IDLINEACRONOESTADO = value ;}
		}

		private int _T708_IDLINEA;
		public int T708_IDLINEA
		{
			get {return _T708_IDLINEA;}
			set { _T708_IDLINEA = value ;}
		}

		private DateTime _T709_FECHA;
		public DateTime T709_FECHA
		{
			get {return _T709_FECHA;}
			set { _T709_FECHA = value ;}
		}

		private string _T710_IDESTADO;
        public string T710_IDESTADO
		{
			get {return _T710_IDESTADO;}
			set { _T710_IDESTADO = value ;}
		}

		private int? _T001_IDFICEPI;
		public int? T001_IDFICEPI
		{
			get {return _T001_IDFICEPI;}
			set { _T001_IDFICEPI = value ;}
		}
		#endregion

		#region Constructor

		public LINEACRONOESTADO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static SqlDataReader Catalogo(int t708_idlinea)
        {
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
            aParam[0].Value = t708_idlinea;

            return SqlHelper.ExecuteSqlDataReader("GEM_LINEACRONOESTADO_C", aParam);
        }

		#endregion
	}
}
