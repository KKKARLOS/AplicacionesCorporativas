using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASCSN3P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T490_FIGURASCSN3P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	16/11/2009 17:20:08	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASCSN3P
	{
		#region Propiedades y Atributos

		private int _t489_idcsn3p;
		public int t489_idcsn3p
		{
			get {return _t489_idcsn3p;}
			set { _t489_idcsn3p = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t490_figura;
		public string t490_figura
		{
			get {return _t490_figura;}
			set { _t490_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURASCSN3P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T490_FIGURASCSN3P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	16/11/2009 17:20:08
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t489_idcsn3p , int t314_idusuario , string t490_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
			aParam[0].Value = t489_idcsn3p;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t490_figura", SqlDbType.Text, 1);
			aParam[2].Value = t490_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN3P_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN3P_I", aParam);
		}

		#endregion
	}
}
