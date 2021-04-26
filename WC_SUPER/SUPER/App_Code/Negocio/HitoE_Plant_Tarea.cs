using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : HITOE_PLANT_TAREA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t370_HITOE_PLANT_TAREA
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	19/11/2007 15:41:12	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class HITOE_PLANT_TAREA
	{
		#region Propiedades y Atributos

		private int _t369_idhito;
		public int t369_idhito
		{
			get {return _t369_idhito;}
			set { _t369_idhito = value ;}
		}

		private int _t339_iditems;
		public int t339_iditems
		{
			get {return _t339_iditems;}
			set { _t339_iditems = value ;}
		}
		#endregion

		#region Constructores

		public HITOE_PLANT_TAREA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t370_HITOE_PLANT_TAREA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 15:41:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t369_idhito , int t339_iditems)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t369_idhito", SqlDbType.Int, 4);
			aParam[0].Value = t369_idhito;
			aParam[1] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[1].Value = t339_iditems;

			if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_HITOE_PLANT_TAREA_I", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOE_PLANT_TAREA_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t370_HITOE_PLANT_TAREA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 15:41:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t369_idhito, int t339_iditems)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t369_idhito", SqlDbType.Int, 4);
			aParam[0].Value = t369_idhito;
			aParam[1] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
			aParam[1].Value = t339_iditems;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_HITOE_PLANT_TAREA_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOE_PLANT_TAREA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t370_HITOE_PLANT_TAREA en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 15:41:12
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt369_idhito(SqlTransaction tr, int t369_idhito) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t369_idhito", SqlDbType.Int, 4);
			aParam[0].Value = t369_idhito;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_HITOE_PLANT_TAREA_SByt369_idhito", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_HITOE_PLANT_TAREA_SByt369_idhito", aParam);
		}
		#endregion
	}
}
