using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PERFILPROY
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T333_PERFILPROY
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/03/2008 12:13:37	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PERFILPROY
	{
		#region Propiedades y Atributos

		private int _t333_idperfilproy;
		public int t333_idperfilproy
		{
			get {return _t333_idperfilproy;}
			set { _t333_idperfilproy = value ;}
		}

		private string _t333_denominacion;
		public string t333_denominacion
		{
			get {return _t333_denominacion;}
			set { _t333_denominacion = value ;}
		}

		private decimal _t333_imptarifa;
        public decimal t333_imptarifa
		{
			get {return _t333_imptarifa;}
			set { _t333_imptarifa = value ;}
		}

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private short _t333_orden;
		public short t333_orden
		{
			get {return _t333_orden;}
			set { _t333_orden = value ;}
		}

		private bool _t333_estado;
		public bool t333_estado
		{
			get {return _t333_estado;}
			set { _t333_estado = value ;}
		}
		#endregion

		#region Constructores

		public PERFILPROY() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla T333_PERFILPROY en función de una foreign key.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	07/03/2008 12:13:37
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByT301_idproyecto(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PERFILPROY_SByT301_idproyecto", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PERFILPROY_SByT301_idproyecto", aParam);
        }

		#endregion
	}
}
