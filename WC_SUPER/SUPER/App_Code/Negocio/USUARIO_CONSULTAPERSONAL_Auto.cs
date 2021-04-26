using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : USUARIO_CONSULTAPERSONAL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T473_USUARIO_CONSULTAPERSONAL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	27/04/2009 11:31:21	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class USUARIO_CONSULTAPERSONAL
	{
		#region Propiedades y Atributos

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private int _t472_idconsulta;
		public int t472_idconsulta
		{
			get {return _t472_idconsulta;}
			set { _t472_idconsulta = value ;}
		}

		private bool _t473_estado;
		public bool t473_estado
		{
			get {return _t473_estado;}
			set { _t473_estado = value ;}
		}

		#endregion

		#region Constructor

		public USUARIO_CONSULTAPERSONAL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T473_USUARIO_CONSULTAPERSONAL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	27/04/2009 11:31:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t314_idusuario , int t472_idconsulta , bool t473_estado)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[1].Value = t472_idconsulta;
			aParam[2] = new SqlParameter("@t473_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t473_estado;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_USUARIO_CONSULTAPERSONAL_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_CONSULTAPERSONAL_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T473_USUARIO_CONSULTAPERSONAL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	27/04/2009 11:31:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t314_idusuario, int t472_idconsulta, bool t473_estado)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[1].Value = t472_idconsulta;
			aParam[2] = new SqlParameter("@t473_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t473_estado;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_USUARIO_CONSULTAPERSONAL_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_CONSULTAPERSONAL_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T473_USUARIO_CONSULTAPERSONAL a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	27/04/2009 11:31:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t314_idusuario, int t472_idconsulta)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[1].Value = t472_idconsulta;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_USUARIO_CONSULTAPERSONAL_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_CONSULTAPERSONAL_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T473_USUARIO_CONSULTAPERSONAL,
		/// y devuelve una instancia u objeto del tipo USUARIO_CONSULTAPERSONAL
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	27/04/2009 11:31:21
		/// </history>
		/// -----------------------------------------------------------------------------
		public static USUARIO_CONSULTAPERSONAL Select(SqlTransaction tr, int t314_idusuario, int t472_idconsulta) 
		{
			USUARIO_CONSULTAPERSONAL o = new USUARIO_CONSULTAPERSONAL();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[1].Value = t472_idconsulta;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_USUARIO_CONSULTAPERSONAL_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIO_CONSULTAPERSONAL_S", aParam);

			if (dr.Read())
			{
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t472_idconsulta"] != DBNull.Value)
					o.t472_idconsulta = int.Parse(dr["t472_idconsulta"].ToString());
				if (dr["t473_estado"] != DBNull.Value)
					o.t473_estado = (bool)dr["t473_estado"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de USUARIO_CONSULTAPERSONAL"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
