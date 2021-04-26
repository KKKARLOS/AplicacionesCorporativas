using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : MODALIDADCONTRATO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T316_MODALIDADCONTRATO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class MODALIDADCONTRATO
	{
		#region Propiedades y Atributos

		private byte _t316_idmodalidad;
		public byte t316_idmodalidad
		{
			get {return _t316_idmodalidad;}
			set { _t316_idmodalidad = value ;}
		}

		private string _t316_denominacion;
		public string t316_denominacion
		{
			get {return _t316_denominacion;}
			set { _t316_denominacion = value ;}
		}
		#endregion

		#region Constructores

		public MODALIDADCONTRATO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T316_MODALIDADCONTRATO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, byte t316_idmodalidad , string t316_denominacion)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[0].Value = t316_idmodalidad;
			aParam[1] = new SqlParameter("@t316_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t316_denominacion;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_MODALIDADCONTRATO_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MODALIDADCONTRATO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T316_MODALIDADCONTRATO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, byte t316_idmodalidad, string t316_denominacion)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[0].Value = t316_idmodalidad;
			aParam[1] = new SqlParameter("@t316_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t316_denominacion;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_MODALIDADCONTRATO_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MODALIDADCONTRATO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T316_MODALIDADCONTRATO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, byte t316_idmodalidad)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[0].Value = t316_idmodalidad;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_MODALIDADCONTRATO_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MODALIDADCONTRATO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T316_MODALIDADCONTRATO,
		/// y devuelve una instancia u objeto del tipo MODALIDADCONTRATO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static MODALIDADCONTRATO Select(SqlTransaction tr, byte t316_idmodalidad) 
		{
			MODALIDADCONTRATO o = new MODALIDADCONTRATO();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[0].Value = t316_idmodalidad;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_MODALIDADCONTRATO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_MODALIDADCONTRATO_S", aParam);

			if (dr.Read())
			{
				if (dr["t316_idmodalidad"] != DBNull.Value)
					o.t316_idmodalidad = byte.Parse(dr["t316_idmodalidad"].ToString());
				if (dr["t316_denominacion"] != DBNull.Value)
					o.t316_denominacion = (string)dr["t316_denominacion"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de MODALIDADCONTRATO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T316_MODALIDADCONTRATO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<byte> t316_idmodalidad, string t316_denominacion, bool bTodos,
                                             byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t316_idmodalidad", SqlDbType.TinyInt, 1);
			aParam[0].Value = t316_idmodalidad;
            aParam[1] = new SqlParameter("@t316_denominacion", SqlDbType.Text, 30);
            aParam[1].Value = t316_denominacion;
            aParam[2] = new SqlParameter("@bTodos", SqlDbType.Bit, 1);
            aParam[2].Value = bTodos;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_MODALIDADCONTRATO_C", aParam);
		}

		#endregion
	}
}
