using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SUBGRUPOECO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T327_SUBGRUPOECO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	16/04/2008 9:18:44	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SUBGRUPOECO
	{
		#region Propiedades y Atributos

		private byte _t327_idsubgrupoeco;
		public byte t327_idsubgrupoeco
		{
			get {return _t327_idsubgrupoeco;}
			set { _t327_idsubgrupoeco = value ;}
		}

		private string _t327_denominacion;
		public string t327_denominacion
		{
			get {return _t327_denominacion;}
			set { _t327_denominacion = value ;}
		}

		private byte _t327_orden;
		public byte t327_orden
		{
			get {return _t327_orden;}
			set { _t327_orden = value ;}
		}

		private byte _t326_idgrupoeco;
		public byte t326_idgrupoeco
		{
			get {return _t326_idgrupoeco;}
			set { _t326_idgrupoeco = value ;}
		}
		#endregion

		#region Constructores

		public SUBGRUPOECO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T327_SUBGRUPOECO en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	16/04/2008 9:18:44
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT326_idgrupoeco(SqlTransaction tr, byte t326_idgrupoeco, bool bEsMantenimiento) 
		{
			SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t326_idgrupoeco", SqlDbType.TinyInt, 1);
            aParam[0].Value = t326_idgrupoeco;
            aParam[1] = new SqlParameter("@esMantenimiento", SqlDbType.Bit, 1);
            aParam[1].Value = bEsMantenimiento;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUBGRUPOECO_CAT", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBGRUPOECO_CAT", aParam);
		}

		#endregion
	}
}
