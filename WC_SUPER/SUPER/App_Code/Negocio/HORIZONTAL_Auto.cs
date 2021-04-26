using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : HORIZONTAL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T307_HORIZONTAL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:28	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class HORIZONTAL
	{
		#region Propiedades y Atributos

		private int _t307_idhorizontal;
		public int t307_idhorizontal
		{
			get {return _t307_idhorizontal;}
			set { _t307_idhorizontal = value ;}
		}

		private string _t307_denominacion;
		public string t307_denominacion
		{
			get {return _t307_denominacion;}
			set { _t307_denominacion = value ;}
		}
        private int? _t314_idusuario_responsable;
        public int? t314_idusuario_responsable
        {
            get { return _t314_idusuario_responsable; }
            set { _t314_idusuario_responsable = value; }
        }
        private string _RESPONSABLE;
        public string RESPONSABLE
        {
            get { return _RESPONSABLE; }
            set { _RESPONSABLE = value; }
        }
		#endregion

		#region Constructores

		public HORIZONTAL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T307_HORIZONTAL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:28
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t307_denominacion, Nullable<int> t314_idusuario_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t307_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t307_denominacion;
            aParam[1] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_responsable;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_HORIZONTAL_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HORIZONTAL_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T307_HORIZONTAL.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	22/07/2009 9:06:11
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t307_idhorizontal, string t307_denominacion, Nullable<int> t314_idusuario_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
            aParam[0].Value = t307_idhorizontal;
            aParam[1] = new SqlParameter("@t307_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t307_denominacion;
            aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario_responsable;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_HORIZONTAL_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HORIZONTAL_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T307_HORIZONTAL a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	22/07/2009 9:06:11
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t307_idhorizontal)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
            aParam[0].Value = t307_idhorizontal;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_HORIZONTAL_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HORIZONTAL_D", aParam);
        }
        /// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T307_HORIZONTAL,
		/// y devuelve una instancia u objeto del tipo HORIZONTAL
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:28
		/// </history>
		/// -----------------------------------------------------------------------------
        /// 
		public static HORIZONTAL Select(SqlTransaction tr, int t307_idhorizontal) 
		{
			HORIZONTAL o = new HORIZONTAL();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[0].Value = t307_idhorizontal;

			SqlDataReader dr;
			if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_HORIZONTAL_SR", aParam);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_HORIZONTAL_SR", aParam);

			if (dr.Read())
			{
				if (dr["t307_idhorizontal"] != DBNull.Value)
					o.t307_idhorizontal = int.Parse(dr["t307_idhorizontal"].ToString());
				if (dr["t307_denominacion"] != DBNull.Value)
					o.t307_denominacion = (string)dr["t307_denominacion"];
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["RESPONSABLE"] != DBNull.Value)
                    o.RESPONSABLE = (string)dr["RESPONSABLE"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de HORIZONTAL"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T307_HORIZONTAL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:28
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t307_idhorizontal, string t307_denominacion, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[0].Value = t307_idhorizontal;
			aParam[1] = new SqlParameter("@t307_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t307_denominacion;

			aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[2].Value = nOrden;
			aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[3].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_HORIZONTAL_C", aParam);
		}

		#endregion
	}
}
