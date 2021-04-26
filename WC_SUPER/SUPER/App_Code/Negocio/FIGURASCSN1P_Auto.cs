using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASCSN1P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T486_FIGURASCSN1P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	16/11/2009 17:20:08	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASCSN1P
	{
		#region Propiedades y Atributos

		private int _t485_idcsn1p;
		public int t485_idcsn1p
		{
			get {return _t485_idcsn1p;}
			set { _t485_idcsn1p = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t486_figura;
		public string t486_figura
		{
			get {return _t486_figura;}
			set { _t486_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURASCSN1P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T486_FIGURASCSN1P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	16/11/2009 17:20:08
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t485_idcsn1p , int t314_idusuario , string t486_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
			aParam[0].Value = t485_idcsn1p;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t486_figura", SqlDbType.Text, 1);
			aParam[2].Value = t486_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN1P_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN1P_I", aParam);
		}

		#endregion
	}
}
