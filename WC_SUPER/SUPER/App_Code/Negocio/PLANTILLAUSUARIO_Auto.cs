using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PLANTILLAUSUARIO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T633_PLANTILLAUSUARIO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	24/11/2010 12:53:15	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PLANTILLAUSUARIO
	{
		#region Propiedades y Atributos

		private int _t629_idplantillaof;
		public int t629_idplantillaof
		{
			get {return _t629_idplantillaof;}
			set { _t629_idplantillaof = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructor

		public PLANTILLAUSUARIO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T633_PLANTILLAUSUARIO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	24/11/2010 12:53:16
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t629_idplantillaof , int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[0].Value = t629_idplantillaof;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_PLANTILLAUSUARIO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLAUSUARIO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T633_PLANTILLAUSUARIO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	24/11/2010 12:53:16
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t314_idusuario, int t629_idplantillaof)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[1].Value = t629_idplantillaof;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PLANTILLAUSUARIO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLAUSUARIO_D", aParam);
		}

		#endregion
	}
}
