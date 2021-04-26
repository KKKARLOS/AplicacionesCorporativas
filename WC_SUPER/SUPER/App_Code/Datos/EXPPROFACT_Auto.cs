using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : EXPPROFACT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T817_EXPPROFACT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	30/07/2012 13:53:26	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class EXPPROFACT
	{
		#region Propiedades y Atributos

		private int _t808_idexpprof;
		public int t808_idexpprof
		{
			get {return _t808_idexpprof;}
			set { _t808_idexpprof = value ;}
		}

		private int _t810_idacontecno;
		public int t810_idacontecno
		{
			get {return _t810_idacontecno;}
			set { _t810_idacontecno = value ;}
		}
		#endregion

		#region Constructor

		public EXPPROFACT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T817_EXPPROFACT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 13:53:26
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t808_idexpprof , int t810_idacontecno)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
			aParam[i++] = ParametroSql.add("@t810_idacontecno", SqlDbType.Int, 4, t810_idacontecno);

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFACT_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFACT_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T817_EXPPROFACT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 13:53:26
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t808_idexpprof, int t810_idacontecno)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
			aParam[i++] = ParametroSql.add("@t810_idacontecno", SqlDbType.Int, 4, t810_idacontecno);

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFACT_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFACT_D", aParam);
		}

		#endregion
	}
}
