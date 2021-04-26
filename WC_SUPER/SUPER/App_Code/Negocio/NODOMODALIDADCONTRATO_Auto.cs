using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : NODOMODALIDADCONTRATO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T636_NODOMODALIDADCONTRATO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	14/12/2010 10:31:51	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class NODOMODALIDADCONTRATO
	{
		#region Propiedades y Atributos

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private byte _t316_idmodalidad;
		public byte t316_idmodalidad
		{
			get {return _t316_idmodalidad;}
			set { _t316_idmodalidad = value ;}
		}
		#endregion

		#region Constructor

		public NODOMODALIDADCONTRATO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T636_NODOMODALIDADCONTRATO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	14/12/2010 10:31:51
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t303_idnodo , byte t316_idmodalidad)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[1].Value = t316_idmodalidad;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_NODOMODALIDADCONTRATO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODOMODALIDADCONTRATO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T636_NODOMODALIDADCONTRATO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	14/12/2010 10:31:51
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t303_idnodo, byte t316_idmodalidad)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[1].Value = t316_idmodalidad;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NODOMODALIDADCONTRATO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODOMODALIDADCONTRATO_D", aParam);
		}

		#endregion
	}
}
