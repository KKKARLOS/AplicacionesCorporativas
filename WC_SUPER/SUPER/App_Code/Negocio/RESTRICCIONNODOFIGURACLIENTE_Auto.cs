using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : RESTRICCIONNODOFIGURACLIENTE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T451_RESTRICCIONNODOFIGURACLIENTE
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	25/05/2010 16:01:50	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class RESTRICCIONNODOFIGURACLIENTE
	{
		#region Propiedades y Atributos

		private int _t302_idcliente;
		public int t302_idcliente
		{
			get {return _t302_idcliente;}
			set { _t302_idcliente = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}
		#endregion

		#region Constructor

		public RESTRICCIONNODOFIGURACLIENTE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T451_RESTRICCIONNODOFIGURACLIENTE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	25/05/2010 16:01:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t302_idcliente , int t314_idusuario , int t303_idnodo)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[2].Value = t303_idnodo;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_RESTRICCIONNODOFIGURACLIENTE_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RESTRICCIONNODOFIGURACLIENTE_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T451_RESTRICCIONNODOFIGURACLIENTE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	25/05/2010 16:01:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t302_idcliente, int t314_idusuario, int t303_idnodo)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[2].Value = t303_idnodo;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_RESTRICCIONNODOFIGURACLIENTE_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RESTRICCIONNODOFIGURACLIENTE_D", aParam);
		}

		#endregion
	}
}
