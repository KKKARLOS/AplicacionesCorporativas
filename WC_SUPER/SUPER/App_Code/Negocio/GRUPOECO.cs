using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : GRUPOECO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T326_GRUPOECO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	16/04/2008 9:18:07	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class GRUPOECO
	{

		#region Propiedades y Atributos

		private byte _t326_idgrupoeco;
		public byte t326_idgrupoeco
		{
			get {return _t326_idgrupoeco;}
			set { _t326_idgrupoeco = value ;}
		}

		private string _t326_denominacion;
		public string t326_denominacion
		{
			get {return _t326_denominacion;}
			set { _t326_denominacion = value ;}
		}

		private byte _t326_orden;
		public byte t326_orden
		{
			get {return _t326_orden;}
			set { _t326_orden = value ;}
		}
        private string _t326_tipogrupo;
        public string t326_tipogrupo
		{
            get { return _t326_tipogrupo; }
            set { _t326_tipogrupo = value; }
		}

		#endregion

		#region Constructores

		public GRUPOECO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T326_GRUPOECO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	16/04/2008 9:18:07
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<byte> t326_idgrupoeco, string t326_denominacion, Nullable<byte> t326_orden, string t326_tipogrupo, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t326_idgrupoeco", SqlDbType.TinyInt, 1);
			aParam[0].Value = t326_idgrupoeco;
			aParam[1] = new SqlParameter("@t326_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t326_denominacion;
			aParam[2] = new SqlParameter("@t326_orden", SqlDbType.TinyInt, 1);
			aParam[2].Value = t326_orden;
            aParam[3] = new SqlParameter("@t326_tipogrupo", SqlDbType.Char, 1);
            aParam[3].Value = t326_tipogrupo;
            
			aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[4].Value = nOrden;
			aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[5].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_GRUPOECO_CAT", aParam);
		}
        //Para mostrar el subarbol de un grupo economico
        public static SqlDataReader GetEstructuraEconomica(byte idGrupo, bool bMostrarInactivos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@idGrupo", SqlDbType.TinyInt, 1);
            aParam[0].Value = idGrupo;
            aParam[1] = new SqlParameter("@bMostrarInactivos", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarInactivos;

            return SqlHelper.ExecuteSqlDataReader("SUP_GET_ESTRUCTURA_ECO", aParam);
        }

		#endregion
	}
}
