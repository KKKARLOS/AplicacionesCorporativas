using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : RECALCULOCOSTES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T608_RECALCULOCOSTES
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	14/02/2011 17:38:16	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class RECALCULOCOSTES
	{
		#region Propiedades y Atributos

		private int _t001_idiberper;
		public int t001_idiberper
		{
			get {return _t001_idiberper;}
			set { _t001_idiberper = value ;}
		}

		private decimal _t608_costeanual;
		public decimal t608_costeanual
		{
			get {return _t608_costeanual;}
			set { _t608_costeanual = value ;}
		}
		#endregion

		#region Constructor

		public RECALCULOCOSTES() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T608_RECALCULOCOSTES.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	14/02/2011 17:38:16
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t001_idiberper , decimal t608_costeanual)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t001_idiberper", SqlDbType.Int, 4);
			aParam[0].Value = t001_idiberper;
			aParam[1] = new SqlParameter("@t608_costeanual", SqlDbType.Money, 8);
			aParam[1].Value = t608_costeanual;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_RECALCULOCOSTES_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RECALCULOCOSTES_I", aParam);
		}


		#endregion
	}
}
