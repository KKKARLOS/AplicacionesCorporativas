using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASCSN4P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T492_FIGURASCSN4P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	16/11/2009 17:20:08	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASCSN4P
	{
		#region Propiedades y Atributos

		private int _t491_idcsn4p;
		public int t491_idcsn4p
		{
			get {return _t491_idcsn4p;}
			set { _t491_idcsn4p = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t492_figura;
		public string t492_figura
		{
			get {return _t492_figura;}
			set { _t492_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURASCSN4P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T492_FIGURASCSN4P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	16/11/2009 17:20:08
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t491_idcsn4p , int t314_idusuario , string t492_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
			aParam[0].Value = t491_idcsn4p;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t492_figura", SqlDbType.Text, 1);
			aParam[2].Value = t492_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASCSN4P_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASCSN4P_I", aParam);
		}

		#endregion
	}
}
