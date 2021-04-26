using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ACCIONTAREAS_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T415_ACCIONTAREASPT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 16:21:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCIONTAREAS_PT
	{

		#region Propiedades y Atributos

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
		}

		private int _t410_idaccion;
		public int t410_idaccion
		{
			get {return _t410_idaccion;}
			set { _t410_idaccion = value ;}
		}
		#endregion

		#region Constructores

		public ACCIONTAREAS_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T415_ACCIONTAREASPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 16:21:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t332_idtarea , int t410_idaccion)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[1].Value = t410_idaccion;

			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_ACCIONTAREAS_PT_I_SNE", aParam);
			else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCIONTAREAS_PT_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T415_ACCIONTAREASPT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 16:21:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t410_idaccion, int t332_idtarea)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t410_idaccion;
			aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[1].Value = t332_idtarea;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCIONTAREAS_PT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCIONTAREAS_PT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T415_ACCIONTAREASPT en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 16:21:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt410_idaccion(SqlTransaction tr, int t410_idaccion) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t410_idaccion;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCIONTAREAS_PT_SByT410_idaccion", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCIONTAREAS_PT_SByT410_idaccion", aParam);
		}

		#endregion
	}
}
