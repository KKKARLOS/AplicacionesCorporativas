using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ACCIONRECURSOS_T
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T605_ACCIONRECURSOST
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 9:58:43	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCIONRECURSOS_T
	{

		#region Propiedades y Atributos

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private int _t601_idaccion;
		public int t601_idaccion
		{
			get {return _t601_idaccion;}
			set { _t601_idaccion = value ;}
		}

		private bool _t605_notificar;
		public bool t605_notificar
		{
			get {return _t605_notificar;}
			set { _t605_notificar = value ;}
		}
		#endregion

		#region Constructores

		public ACCIONRECURSOS_T() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T605_ACCIONRECURSOST.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:58:43
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t314_idusuario , int t601_idaccion , bool t605_notificar)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[1].Value = t601_idaccion;
			aParam[2] = new SqlParameter("@t605_notificar", SqlDbType.Bit, 1);
			aParam[2].Value = t605_notificar;

			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_ACCIONRECURSOS_T_I_SNE", aParam);
			else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCIONRECURSOS_T_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T605_ACCIONRECURSOST.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:58:43
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t314_idusuario, int t601_idaccion, bool t605_notificar)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[1].Value = t601_idaccion;
			aParam[2] = new SqlParameter("@t605_notificar", SqlDbType.Bit, 1);
			aParam[2].Value = t605_notificar;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCIONRECURSOS_T_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCIONRECURSOS_T_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T605_ACCIONRECURSOST a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:58:43
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t601_idaccion, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t601_idaccion;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCIONRECURSOS_T_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCIONRECURSOS_T_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T605_ACCIONRECURSOST en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:58:43
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt601_idaccion(SqlTransaction tr, int t601_idaccion) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t601_idaccion;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCIONRECURSOS_T_SByT601_idaccion", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCIONRECURSOS_T_SByT601_idaccion", aParam);
		}

		#endregion
	}
}
