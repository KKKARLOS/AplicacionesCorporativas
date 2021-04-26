using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASCSN2P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T488_FIGURASCSN2P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	16/11/2009 17:20:08	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASCSN2P
	{
		#region Propiedades y Atributos

		private int _t487_idcsn2p;
		public int t487_idcsn2p
		{
			get {return _t487_idcsn2p;}
			set { _t487_idcsn2p = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t488_figura;
		public string t488_figura
		{
			get {return _t488_figura;}
			set { _t488_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURASCSN2P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T488_FIGURASCSN2P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	16/11/2009 17:20:08
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t487_idcsn2p , int t314_idusuario , string t488_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
			aParam[0].Value = t487_idcsn2p;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t488_figura", SqlDbType.Text, 1);
			aParam[2].Value = t488_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN2P_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN2P_I", aParam);
		}

		#endregion
	}
}
