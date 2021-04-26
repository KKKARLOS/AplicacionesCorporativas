using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ACCIONTAREAS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t390_ACCIONTAREAS
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	16/11/2007 16:21:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCIONTAREAS
	{

		#region Propiedades y Atributos

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
		}

		private int _t383_idaccion;
		public int t383_idaccion
		{
			get {return _t383_idaccion;}
			set { _t383_idaccion = value ;}
		}
		#endregion

		#region Constructores

		public ACCIONTAREAS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t390_ACCIONTAREAS.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 16:21:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t332_idtarea , int t383_idaccion)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[1].Value = t383_idaccion;

			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_ACCIONTAREAS_I_SNE", aParam);
			else
				returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCIONTAREAS_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t390_ACCIONTAREAS a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 16:21:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t383_idaccion, int t332_idtarea)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t383_idaccion;
			aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[1].Value = t332_idtarea;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCIONTAREAS_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCIONTAREAS_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t390_ACCIONTAREAS en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 16:21:02
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt383_idaccion(SqlTransaction tr, int t383_idaccion) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t383_idaccion;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCIONTAREAS_SByt383_idaccion", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCIONTAREAS_SByt383_idaccion", aParam);
		}

		#endregion
	}
}
