using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T341_AE
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AE 
	{
		#region Propiedades y Atributos
         
		private int _t341_idae;
		public int t341_idae
		{
			get {return _t341_idae;}
			set { _t341_idae = value ;}
		}

		private string _t341_nombre;
		public string t341_nombre
		{
			get {return _t341_nombre;}
			set { _t341_nombre = value ;}
		}

		private bool _t341_estado;
		public bool t341_estado
		{
			get {return _t341_estado;}
			set { _t341_estado = value ;}
		}

		private int _t341_orden;
		public int t341_orden
		{
			get {return _t341_orden;}
			set { _t341_orden = value ;}
		}

		private bool _t341_obligatorio;
		public bool t341_obligatorio
		{
			get {return _t341_obligatorio;}
			set { _t341_obligatorio = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _cod_cliente;
		public int cod_cliente
		{
			get {return _cod_cliente;}
			set { _cod_cliente = value ;}
		}

        private string _nom_cliente;
        public string nom_cliente
        {
            get { return _nom_cliente; }
            set { _nom_cliente = value; }
        }
        #endregion 

		#region Constructores

		public AE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T341_AE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t341_nombre, bool t341_estado, int t341_orden, bool t341_obligatorio, 
                                 int t303_idnodo, Nullable<int> cod_cliente, string sAmbito)
            {
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t341_nombre", SqlDbType.VarChar, 30);
			aParam[0].Value = t341_nombre;
            //aParam[1] = new SqlParameter("@t341_ambito", SqlDbType.Text, 1);
            //aParam[1].Value = t341_ambito;
			aParam[1] = new SqlParameter("@t341_estado", SqlDbType.Bit, 1);
			aParam[1].Value = t341_estado;
			aParam[2] = new SqlParameter("@t341_orden", SqlDbType.Int, 4);
			aParam[2].Value = t341_orden;
			aParam[3] = new SqlParameter("@t341_obligatorio", SqlDbType.Bit, 1);
			aParam[3].Value = t341_obligatorio;
			aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[4].Value = t303_idnodo;
            aParam[5] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
            aParam[5].Value = cod_cliente;
            aParam[6] = new SqlParameter("@t341_ambito", SqlDbType.Char, 1);
            aParam[6].Value = sAmbito;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AE_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AE_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T341_AE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t341_idae, string t341_nombre, bool t341_estado, int t341_orden, bool t341_obligatorio,
                                 int t303_idnodo, Nullable<int> cod_cliente)
{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
			aParam[0].Value = t341_idae;
			aParam[1] = new SqlParameter("@t341_nombre", SqlDbType.Text, 30);
			aParam[1].Value = t341_nombre;
            //aParam[2] = new SqlParameter("@t341_ambito", SqlDbType.Text, 1);
            //aParam[2].Value = t341_ambito;
			aParam[2] = new SqlParameter("@t341_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t341_estado;
			aParam[3] = new SqlParameter("@t341_orden", SqlDbType.Int, 4);
			aParam[3].Value = t341_orden;
			aParam[4] = new SqlParameter("@t341_obligatorio", SqlDbType.Bit, 1);
			aParam[4].Value = t341_obligatorio;
			aParam[5] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[5].Value = t303_idnodo;
			aParam[6] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
			aParam[6].Value = cod_cliente;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AE_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AE_U", aParam);
		}
        //public static int UpdateOrden(SqlTransaction tr, int t341_idae, int t341_orden)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
        //    aParam[0].Value = t341_idae;
        //    aParam[1] = new SqlParameter("@t341_orden", SqlDbType.Int, 4);
        //    aParam[1].Value = t341_orden;
        //    // Ejecuta la query y devuelve el numero de registros modificados.
        //    if (tr == null)
        //        return SqlHelper.ExecuteNonQuery("SUP_AE_ORDEN_U", aParam);
        //    else
        //        return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AE_ORDEN_U", aParam);
        //}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T341_AE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t341_idae)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
			aParam[0].Value = t341_idae;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AE_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AE_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T341_AE,
		/// y devuelve una instancia u objeto del tipo AE
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static AE Select(int t341_idae) 
		{
			AE o = new AE();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
			aParam[0].Value = t341_idae;

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_AE_S", aParam);

			if (dr.Read())
			{
				if (dr["t341_idae"] != DBNull.Value)
					o.t341_idae = (int)dr["t341_idae"];
				if (dr["t341_nombre"] != DBNull.Value)
					o.t341_nombre = (string)dr["t341_nombre"];
                //if (dr["t341_ambito"] != DBNull.Value)
                //    o.t341_ambito = (string)dr["t341_ambito"];
				if (dr["t341_estado"] != DBNull.Value)
					o.t341_estado = (bool)dr["t341_estado"];
				if (dr["t341_orden"] != DBNull.Value)
					o.t341_orden = (int)dr["t341_orden"];
				if (dr["t341_obligatorio"] != DBNull.Value)
					o.t341_obligatorio = (bool)dr["t341_obligatorio"];
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
				if (dr["cod_cliente"] != DBNull.Value)
					o.cod_cliente = (int)dr["cod_cliente"];

            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de AE"));
			}

            dr.Close();
            dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T341_AE en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader  SelectByCod_cliente(Nullable<int> cod_cliente, string sAmbito) 
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
			aParam[0].Value = cod_cliente;
            aParam[1] = new SqlParameter("@sAmbito", SqlDbType.Char, 1);
            aParam[1].Value = sAmbito;

            return SqlHelper.ExecuteSqlDataReader("SUP_AE_SByCod_cliente", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T341_AE en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByt303_idnodo(int t303_idnodo, string sAmbito) 
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@sAmbito", SqlDbType.Char, 1);
            aParam[1].Value = sAmbito;

            return SqlHelper.ExecuteSqlDataReader("SUP_AE_SByt303_idnodo", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T341_AE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 8:38:39
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t341_idae, string t341_nombre, Nullable<bool> t341_estado, Nullable<int> t341_orden, Nullable<bool> t341_obligatorio, Nullable<int> t303_idnodo, Nullable<int> cod_cliente, string sAmbito, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[10];
            aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[0].Value = t341_idae;
            aParam[1] = new SqlParameter("@t341_nombre", SqlDbType.Text, 30);
            aParam[1].Value = t341_nombre;
            aParam[2] = new SqlParameter("@t341_estado", SqlDbType.Bit, 1);
            aParam[2].Value = t341_estado;
            aParam[3] = new SqlParameter("@t341_orden", SqlDbType.Int, 4);
            aParam[3].Value = t341_orden;
            aParam[4] = new SqlParameter("@t341_obligatorio", SqlDbType.Bit, 1);
            aParam[4].Value = t341_obligatorio;
            aParam[5] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[5].Value = t303_idnodo;
            aParam[6] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
            aParam[6].Value = cod_cliente;
            aParam[7] = new SqlParameter("@t341_ambito", SqlDbType.Char, 1);
            aParam[7].Value = sAmbito;

            aParam[8] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[8].Value = nOrden;
            aParam[9] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[9].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AE_C", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla t341_AE, en función de un código
        /// de une, y lo devuelve ordenado por "order" y "nombre".
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	27/09/2006 17:04:24
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoByUne(int t303_idnodo, string sAmbito, Nullable<int> cod_cliente)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@sAmbito", SqlDbType.Char, 1);
            aParam[1].Value = sAmbito;
            aParam[2] = new SqlParameter("@cod_cliente", SqlDbType.Int, 4);
            aParam[2].Value = cod_cliente;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AE_SByUneDes", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla t341_AE junto con las descripciones
        /// de las foreing keys, y devuelve una instancia u objeto del tipo AE
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	
        /// </history>
        /// -----------------------------------------------------------------------------
        public static AE SelectDescFK(int t341_idae)
        {
            AE o = new AE();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[0].Value = t341_idae;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_AE_SDes", aParam);

            if (dr.Read())
            {
                if (dr["t341_idae"] != DBNull.Value)
                    o.t341_idae = (int)dr["t341_idae"];
                if (dr["t341_nombre"] != DBNull.Value)
                    o.t341_nombre = (string)dr["t341_nombre"];
                //if (dr["t341_ambito"] != DBNull.Value)
                //    o.t341_ambito = (string)dr["t341_ambito"];
                if (dr["t341_estado"] != DBNull.Value)
                    o.t341_estado = (bool)dr["t341_estado"];
                if (dr["t341_orden"] != DBNull.Value)
                    o.t341_orden = int.Parse(dr["t341_orden"].ToString());
                if (dr["t341_obligatorio"] != DBNull.Value)
                    o.t341_obligatorio = (bool)dr["t341_obligatorio"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["cod_cliente"] != DBNull.Value)
                    o.cod_cliente = (int)dr["cod_cliente"];
                if (dr["nom_cliente"] != DBNull.Value)
                    o.nom_cliente = (string)dr["nom_cliente"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de AE"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el nº de registros que se están usando de un atributo estadístico
        /// pasando por parámetro el código de atributo estadístico.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	11/06/2007 15:10:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int numAEusados(int t341_idae)
        {
            int iRes = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
            aParam[0].Value = t341_idae;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            iRes = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AEPTPSP_Count", aParam));
            if (iRes <= 0)
            {
                //Si no esta usado a nivel de proyecto técnico miro si se usa a nivel de tarea
                iRes = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AETAREAPSP_Count", aParam));
            }
            return iRes;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T355_AEPTPSP,
        /// pasando por parámetro el código de Proyecto Tecnico.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/11/2007 15:10:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoByPT(int t331_idpt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AEPTPSP_CByIdPt", aParam);
        }
        public static SqlDataReader CatalogoByPSN(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AEPROYECTOSUBNODO_ByPSN", aParam);
        }
        #endregion
	}
}
