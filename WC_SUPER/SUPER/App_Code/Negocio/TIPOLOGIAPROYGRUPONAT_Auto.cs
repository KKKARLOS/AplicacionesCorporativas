using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TIPOLOGIAPROYGRUPONAT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T324_TIPOLOGIAPROYGRUPONAT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TIPOLOGIAPROYGRUPONAT
	{
		#region Propiedades y Atributos

		private int _t320_idtipologiaproy;
		public int t320_idtipologiaproy
		{
			get {return _t320_idtipologiaproy;}
			set { _t320_idtipologiaproy = value ;}
		}

		private int _t321_idgruponat;
		public int t321_idgruponat
		{
			get {return _t321_idgruponat;}
			set { _t321_idgruponat = value ;}
		}
		#endregion

		#region Constructores

		public TIPOLOGIAPROYGRUPONAT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T324_TIPOLOGIAPROYGRUPONAT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t320_idtipologiaproy , int t321_idgruponat)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
			aParam[0].Value = t320_idtipologiaproy;
			aParam[1] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[1].Value = t321_idgruponat;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_TIPOLOGIAPROYGRUPONAT_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOLOGIAPROYGRUPONAT_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T324_TIPOLOGIAPROYGRUPONAT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t320_idtipologiaproy, int t321_idgruponat)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
			aParam[0].Value = t320_idtipologiaproy;
			aParam[1] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[1].Value = t321_idgruponat;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TIPOLOGIAPROYGRUPONAT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOLOGIAPROYGRUPONAT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T324_TIPOLOGIAPROYGRUPONAT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t320_idtipologiaproy, int t321_idgruponat)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
			aParam[0].Value = t320_idtipologiaproy;
			aParam[1] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[1].Value = t321_idgruponat;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TIPOLOGIAPROYGRUPONAT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOLOGIAPROYGRUPONAT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T324_TIPOLOGIAPROYGRUPONAT,
		/// y devuelve una instancia u objeto del tipo TIPOLOGIAPROYGRUPONAT
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static TIPOLOGIAPROYGRUPONAT Select(SqlTransaction tr, int t320_idtipologiaproy, int t321_idgruponat) 
		{
			TIPOLOGIAPROYGRUPONAT o = new TIPOLOGIAPROYGRUPONAT();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
			aParam[0].Value = t320_idtipologiaproy;
			aParam[1] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[1].Value = t321_idgruponat;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_TIPOLOGIAPROYGRUPONAT_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TIPOLOGIAPROYGRUPONAT_S", aParam);

			if (dr.Read())
			{
				if (dr["t320_idtipologiaproy"] != DBNull.Value)
					o.t320_idtipologiaproy = int.Parse(dr["t320_idtipologiaproy"].ToString());
				if (dr["t321_idgruponat"] != DBNull.Value)
					o.t321_idgruponat = int.Parse(dr["t321_idgruponat"].ToString());

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de TIPOLOGIAPROYGRUPONAT"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T324_TIPOLOGIAPROYGRUPONAT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t320_idtipologiaproy, Nullable<int> t321_idgruponat, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
			aParam[0].Value = t320_idtipologiaproy;
			aParam[1] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
			aParam[1].Value = t321_idgruponat;

			aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[2].Value = nOrden;
			aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[3].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TIPOLOGIAPROYGRUPONAT_C", aParam);
		}

		#endregion
	}
}
