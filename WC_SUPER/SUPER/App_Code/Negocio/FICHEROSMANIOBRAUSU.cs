using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FICHEROSMANIOBRAUSU
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T722_FICHEROSMANIOBRAUSU
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/10/2008 11:41:01	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FICHEROSMANIOBRAUSU
	{
		#region Propiedades y Atributos

		private byte _t722_idtipo;
		public byte t722_idtipo
		{
			get {return _t722_idtipo;}
			set { _t722_idtipo = value ;}
		}

        private int _t001_idficepi;
        public int t001_idficepi
		{
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
		}

		private byte[] _t722_fichero;
		public byte[] t722_fichero
		{
			get {return _t722_fichero;}
			set { _t722_fichero = value ;}
		}
		#endregion

		#region Constructor

		public FICHEROSMANIOBRAUSU() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T447_FICHEROSMANIOBRA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	03/10/2008 11:41:01
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, byte t722_idtipo, int t001_idficepi, byte[] t722_fichero)
		{
			SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t722_idtipo", SqlDbType.TinyInt, 1);
            aParam[0].Value = t722_idtipo;
            aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi;
			aParam[2] = new SqlParameter("@t722_fichero", SqlDbType.Binary, 2147483647);
			aParam[2].Value = t722_fichero;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FICHEROSMANIOBRAUSU_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FICHEROSMANIOBRAUSU_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T722_FICHEROSMANIOBRAUSU,
		/// y devuelve una instancia u objeto del tipo T722_FICHEROSMANIOBRAUSU
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	03/10/2008 11:41:01
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FICHEROSMANIOBRAUSU Select(SqlTransaction tr, byte t722_idtipo, int t001_idficepi) 
		{
			FICHEROSMANIOBRAUSU o = new FICHEROSMANIOBRAUSU();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t722_idtipo", SqlDbType.TinyInt, 1);
			aParam[0].Value = t722_idtipo;
            aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi;
			SqlDataReader dr;
			if (tr == null) 
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FICHEROSMANIOBRAUSU_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FICHEROSMANIOBRAUSU_S", aParam);
            
			if (dr.Read())
			{
				if (dr["t722_idtipo"] != DBNull.Value)
					o.t722_idtipo = byte.Parse(dr["t722_idtipo"].ToString());
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = (int)dr["t001_idficepi"];
				if (dr["t722_fichero"] != DBNull.Value)
					o.t722_fichero = (byte[])dr["t722_fichero"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FICHEROSMANIOBRAUSU"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
