using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
    /// Class	 : T330_USUARIOPROYECTOSUBNodo
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T330_USUARIOPROYECTOSUBNodo
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	19/11/2007 17:08:15	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class recursos_asoc_proy
	{
		#region Propiedades y Atributos

		private short _cod_une_proy;
		public short cod_une_proy
		{
			get {return _cod_une_proy;}
			set { _cod_une_proy = value ;}
		}

		private int _cod_recurso;
		public int cod_recurso
		{
			get {return _cod_recurso;}
			set { _cod_recurso = value ;}
		}

        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private int _num_proyecto;
        public int num_proyecto
        {
            get { return _num_proyecto; }
            set { _num_proyecto = value; }
        }

        private decimal _coste_recurso;
		public decimal coste_recurso
		{
			get {return _coste_recurso;}
			set { _coste_recurso = value ;}
		}

        private decimal _coste_recurso_rep;
        public decimal coste_recurso_rep
        {
            get { return _coste_recurso_rep; }
            set { _coste_recurso_rep = value; }
        }

		private bool _deriva;
		public bool deriva
		{
			get {return _deriva;}
			set { _deriva = value ;}
		}

        //private string _puesto_trabajo;
        //public string puesto_trabajo
        //{
        //    get {return _puesto_trabajo;}
        //    set { _puesto_trabajo = value ;}
        //}

        //private string _cod_nivel_proy;
        //public string cod_nivel_proy
        //{
        //    get {return _cod_nivel_proy;}
        //    set { _cod_nivel_proy = value ;}
        //}

		private DateTime _fecha_alta_proy;
		public DateTime fecha_alta_proy
		{
			get {return _fecha_alta_proy;}
			set { _fecha_alta_proy = value ;}
		}

		private DateTime _fecha_baja_proy;
		public DateTime fecha_baja_proy
		{
			get {return _fecha_baja_proy;}
			set { _fecha_baja_proy = value ;}
		}

		private int _t333_idperfilproy;
		public int t333_idperfilproy
		{
			get {return _t333_idperfilproy;}
			set { _t333_idperfilproy = value ;}
		}
		#endregion

		#region Constructores

        public recursos_asoc_proy()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T330_USUARIOPROYECTOSUBNodo
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 17:08:15
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static int Insert(SqlTransaction tr, short cod_une_proy, int cod_recurso, int num_proyecto, double coste_recurso, string tipo_recurso, bool deriva, string puesto_trabajo, string cod_nivel_proy, DateTime fecha_alta_proy, Nullable<DateTime> fecha_baja_proy, Nullable<int> t333_idperfilproy)
        public static int Insert(SqlTransaction tr,  int cod_recurso , int num_proyecto , double coste_recurso , bool deriva , DateTime fecha_alta_proy , Nullable<DateTime> fecha_baja_proy , Nullable<int> t333_idperfilproy)
{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@cod_recurso", SqlDbType.Int, 4);
			aParam[0].Value = cod_recurso;
			aParam[1] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
			aParam[1].Value = num_proyecto;
			aParam[2] = new SqlParameter("@coste_recurso", SqlDbType.Float, 8);
			aParam[2].Value = coste_recurso;
            //aParam[3] = new SqlParameter("@tipo_recurso", SqlDbType.Text, 2);
            //aParam[3].Value = tipo_recurso;
			aParam[3] = new SqlParameter("@deriva", SqlDbType.Bit, 1);
			aParam[3].Value = deriva;
            //aParam[6] = new SqlParameter("@puesto_trabajo", SqlDbType.Text, 1);
            //aParam[6].Value = puesto_trabajo;
            //aParam[7] = new SqlParameter("@cod_nivel_proy", SqlDbType.Text, 2);
            //aParam[7].Value = cod_nivel_proy;
			aParam[4] = new SqlParameter("@fecha_alta_proy", SqlDbType.SmallDateTime, 8);
			aParam[4].Value = fecha_alta_proy;
			aParam[5] = new SqlParameter("@fecha_baja_proy", SqlDbType.SmallDateTime, 8);
			aParam[5].Value = fecha_baja_proy;
			aParam[6] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
			aParam[6].Value = t333_idperfilproy;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_T330_USUARIOPROYECTOSUBNodo_I", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_T330_USUARIOPROYECTOSUBNodo_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T330_USUARIOPROYECTOSUBNodo
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 17:08:15
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static int Update(SqlTransaction tr, short cod_une_proy, int cod_recurso, int num_proyecto, decimal coste_recurso, string tipo_recurso, bool deriva, string puesto_trabajo, string cod_nivel_proy, DateTime fecha_alta_proy, DateTime fecha_baja_proy, Nullable<int> t333_idperfilproy)
        public static int Update(SqlTransaction tr, int cod_recurso, int num_proyecto, decimal coste_recurso, bool deriva, DateTime fecha_alta_proy, DateTime fecha_baja_proy, Nullable<int> t333_idperfilproy)

        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@cod_recurso", SqlDbType.Int, 4);
            aParam[0].Value = cod_recurso;
            aParam[1] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
            aParam[1].Value = num_proyecto;
            aParam[2] = new SqlParameter("@coste_recurso", SqlDbType.Float, 8);
            aParam[2].Value = coste_recurso;
            aParam[3] = new SqlParameter("@deriva", SqlDbType.Bit, 1);
            aParam[3].Value = deriva;
            aParam[4] = new SqlParameter("@fecha_alta_proy", SqlDbType.SmallDateTime, 8);
            aParam[4].Value = fecha_alta_proy;
            aParam[5] = new SqlParameter("@fecha_baja_proy", SqlDbType.SmallDateTime, 8);
            aParam[5].Value = fecha_baja_proy;
            aParam[6] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[6].Value = t333_idperfilproy;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_T330_USUARIOPROYECTOSUBNodo_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_T330_USUARIOPROYECTOSUBNodo_U", aParam);
        }
        //public static int Update2(SqlTransaction tr, short cod_une_proy, int cod_recurso, int num_proyecto, double coste_recurso, string tipo_recurso, bool deriva, string puesto_trabajo, string cod_nivel_proy, Nullable<int> t333_idperfilproy)
        public static int Update2(SqlTransaction tr, int cod_recurso, int num_proyecto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@cod_recurso", SqlDbType.Int, 4);
            aParam[0].Value = cod_recurso;
            aParam[1] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
            aParam[1].Value = num_proyecto;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_T330_USUARIOPROYECTOSUBNodo_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_T330_USUARIOPROYECTOSUBNodo_U2", aParam);
        }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T330_USUARIOPROYECTOSUBNodo a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 17:08:15
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, short cod_une_proy, int num_proyecto, int cod_recurso)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
			aParam[0].Value = num_proyecto;
			aParam[1] = new SqlParameter("@cod_recurso", SqlDbType.Int, 4);
			aParam[1].Value = cod_recurso;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_T330_USUARIOPROYECTOSUBNodo_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_T330_USUARIOPROYECTOSUBNodo_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T330_USUARIOPROYECTOSUBNodo,
		/// y devuelve una instancia u objeto del tipo asoc_proy
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 17:08:15
		/// </history>
		/// -----------------------------------------------------------------------------
        public static recursos_asoc_proy Select(short cod_une_proy, int num_proyecto, int cod_recurso) 
		{
            recursos_asoc_proy o = new recursos_asoc_proy();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
			aParam[0].Value = num_proyecto;
			aParam[1] = new SqlParameter("@cod_recurso", SqlDbType.Int, 4);
			aParam[1].Value = cod_recurso;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_T330_USUARIOPROYECTOSUBNodo_S", aParam);

			if (dr.Read())
			{
				if (dr["cod_une_proy"] != DBNull.Value)
					o.cod_une_proy = short.Parse(dr["t303_idnodo"].ToString());
				if (dr["cod_recurso"] != DBNull.Value)
					o.cod_recurso = (int)dr["cod_recurso"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
                if (dr["num_proyecto"] != DBNull.Value)
                    o.num_proyecto = (int)dr["num_proyecto"];
                if (dr["coste_recurso"] != DBNull.Value)
                    o.coste_recurso = (decimal)dr["coste_recurso"];
                if (dr["coste_recurso_rep"] != DBNull.Value)
                    o.coste_recurso_rep = (decimal)dr["coste_recurso_rep"];
                if (dr["t330_deriva"] != DBNull.Value)
                    o.deriva = (bool)dr["t330_deriva"];
                //if (dr["puesto_trabajo"] != DBNull.Value)
                //    o.puesto_trabajo = (string)dr["puesto_trabajo"];
                //if (dr["cod_nivel_proy"] != DBNull.Value)
                //    o.cod_nivel_proy = (string)dr["cod_nivel_proy"];
                if (dr["t330_falta"] != DBNull.Value)
                    o.fecha_alta_proy = (DateTime)dr["t330_falta"];
                if (dr["t330_fbaja"] != DBNull.Value)
                    o.fecha_baja_proy = (DateTime)dr["t330_fbaja"];
				if (dr["t333_idperfilproy"] != DBNull.Value)
					o.t333_idperfilproy = (int)dr["t333_idperfilproy"];
            
            }
			else
			{
                throw (new NullReferenceException("No se ha obtenido ningun dato de T330_USUARIOPROYECTOSUBNodo"));
			}

            dr.Close();
            dr.Dispose();
            
            return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T330_USUARIOPROYECTOSUBNodo
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	19/11/2007 17:08:15
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<short> cod_une_proy, Nullable<int> cod_recurso, Nullable<int> num_proyecto, Nullable<decimal> coste_recurso, Nullable<decimal> coste_recurso_rep, Nullable<bool> deriva, string puesto_trabajo, string cod_nivel_proy, Nullable<DateTime> fecha_alta_proy, Nullable<DateTime> fecha_baja_proy, Nullable<int> t333_idperfilproy, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@cod_recurso", SqlDbType.Int, 4);
			aParam[0].Value = cod_recurso;
			aParam[1] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
			aParam[1].Value = num_proyecto;
			aParam[2] = new SqlParameter("@coste_recurso", SqlDbType.Float, 8);
			aParam[2].Value = coste_recurso;
            aParam[2] = new SqlParameter("@coste_recurso_rep", SqlDbType.Float, 8);
            aParam[2].Value = coste_recurso_rep;
            aParam[4] = new SqlParameter("@deriva", SqlDbType.Bit, 1);
			aParam[4].Value = deriva;
			aParam[5] = new SqlParameter("@fecha_alta_proy", SqlDbType.SmallDateTime, 8);
			aParam[5].Value = fecha_alta_proy;
			aParam[6] = new SqlParameter("@fecha_baja_proy", SqlDbType.SmallDateTime, 8);
			aParam[6].Value = fecha_baja_proy;
			aParam[7] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
			aParam[7].Value = t333_idperfilproy;

			aParam[8] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[8].Value = nOrden;
			aParam[9] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[9].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_T330_USUARIOPROYECTOSUBNodo_C", aParam);
		}

		#endregion
	}
}
