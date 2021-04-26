using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASCDP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T477_FIGURASCDP
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	23/06/2009 12:50:30	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASCDP
	{
		#region Propiedades y Atributos

		private int _t476_idcdp;
		public int t476_idcdp
		{
			get {return _t476_idcdp;}
			set { _t476_idcdp = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t477_figura;
		public string t477_figura
		{
			get {return _t477_figura;}
			set { _t477_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURASCDP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T477_FIGURASCDP.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	23/06/2009 12:50:30
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t476_idcnp, int t314_idusuario, string t477_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[0].Value = t476_idcnp;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t477_figura", SqlDbType.Text, 1);
			aParam[2].Value = t477_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASCDP_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCDP_I", aParam);
		}

		#endregion
	}
}
