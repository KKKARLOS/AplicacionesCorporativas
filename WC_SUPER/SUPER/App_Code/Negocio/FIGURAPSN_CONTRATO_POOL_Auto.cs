using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
    /// Class	 : FIGURAPSN_CONTRATO_POOL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T690_FIGURAPSN_CONTRATO_DEFECTO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	14/01/2011 12:57:40	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_CONTRATO_POOL
	{
		#region Propiedades y Atributos

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t690_figura;
		public string t690_figura
		{
			get {return _t690_figura;}
			set { _t690_figura = value ;}
		}
		#endregion

		#region Constructor

        public FIGURAPSN_CONTRATO_POOL()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T643_FIGURAPSN_NODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	14/01/2011 12:57:40
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t314_idusuario, string t690_figura)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t690_figura", SqlDbType.Text, 1);
            aParam[1].Value = t690_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_CONTRATO_POOL_I", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_CONTRATO_POOL_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T643_FIGURAPSN_NODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	14/01/2011 12:57:40
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t314_idusuario, string t690_figura)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t690_figura", SqlDbType.Text, 1);
            aParam[1].Value = t690_figura;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_CONTRATO_POOL_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_CONTRATO_POOL_D", aParam);
		}

		#endregion
	}
}
