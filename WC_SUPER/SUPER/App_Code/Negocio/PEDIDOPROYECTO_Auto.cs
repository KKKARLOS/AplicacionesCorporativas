using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PEDIDOPROYECTO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T480_PEDIDOPROYECTO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	01/07/2009 8:35:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PEDIDOPROYECTO
	{
		#region Propiedades y Atributos

		private int _t480_idpedido;
		public int t480_idpedido
		{
			get {return _t480_idpedido;}
			set { _t480_idpedido = value ;}
		}

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private string _t480_tipopedido;
		public string t480_tipopedido
		{
			get {return _t480_tipopedido;}
			set { _t480_tipopedido = value ;}
		}

		private string _t480_pedido;
		public string t480_pedido
		{
			get {return _t480_pedido;}
			set { _t480_pedido = value ;}
		}

		private DateTime? _t480_fechapedido;
		public DateTime? t480_fechapedido
		{
			get {return _t480_fechapedido;}
			set { _t480_fechapedido = value ;}
		}

		private string _t480_comentario;
		public string t480_comentario
		{
			get {return _t480_comentario;}
			set { _t480_comentario = value ;}
		}
		#endregion

		#region Constructor

		public PEDIDOPROYECTO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T480_PEDIDOPROYECTO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	01/07/2009 8:35:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t301_idproyecto , string t480_tipopedido , string t480_pedido , Nullable<DateTime> t480_fechapedido , string t480_comentario)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t480_tipopedido", SqlDbType.Text, 1);
			aParam[1].Value = t480_tipopedido;
			aParam[2] = new SqlParameter("@t480_pedido", SqlDbType.Text, 15);
			aParam[2].Value = t480_pedido;
			aParam[3] = new SqlParameter("@t480_fechapedido", SqlDbType.SmallDateTime, 4);
			aParam[3].Value = t480_fechapedido;
			aParam[4] = new SqlParameter("@t480_comentario", SqlDbType.Text, 50);
			aParam[4].Value = t480_comentario;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PEDIDOPROYECTO_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PEDIDOPROYECTO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T480_PEDIDOPROYECTO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	01/07/2009 8:35:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t480_idpedido, int t301_idproyecto, string t480_tipopedido, string t480_pedido, Nullable<DateTime> t480_fechapedido, string t480_comentario)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t480_idpedido", SqlDbType.Int, 4);
			aParam[0].Value = t480_idpedido;
			aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[1].Value = t301_idproyecto;
			aParam[2] = new SqlParameter("@t480_tipopedido", SqlDbType.Text, 1);
			aParam[2].Value = t480_tipopedido;
			aParam[3] = new SqlParameter("@t480_pedido", SqlDbType.Text, 15);
			aParam[3].Value = t480_pedido;
			aParam[4] = new SqlParameter("@t480_fechapedido", SqlDbType.SmallDateTime, 4);
			aParam[4].Value = t480_fechapedido;
			aParam[5] = new SqlParameter("@t480_comentario", SqlDbType.Text, 50);
			aParam[5].Value = t480_comentario;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PEDIDOPROYECTO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PEDIDOPROYECTO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T480_PEDIDOPROYECTO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	01/07/2009 8:35:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t480_idpedido)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t480_idpedido", SqlDbType.Int, 4);
			aParam[0].Value = t480_idpedido;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PEDIDOPROYECTO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PEDIDOPROYECTO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T480_PEDIDOPROYECTO en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	01/07/2009 8:35:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT301_idproyecto(SqlTransaction tr, int t301_idproyecto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_PEDIDOPROYECTO_SByT301_idproyecto", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PEDIDOPROYECTO_SByT301_idproyecto", aParam);
		}

		#endregion
	}
}
