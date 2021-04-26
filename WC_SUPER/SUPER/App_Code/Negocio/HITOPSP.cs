using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : HITOPSP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t349_HITOPSP, t352_HITOPE
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	23/11/2007 11:47:13	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class HITOPSP
	{
		#region Propiedades y Atributos

        private short _cod_une;
        public short cod_une
        {
            get { return _cod_une; }
            set { _cod_une = value; }
        }

        private int _num_proyecto;
        public int num_proyecto
        {
            get { return _num_proyecto; }
            set { _num_proyecto = value; }
        }
        
        private int _idhito;
		public int idhito
		{
			get {return _idhito;}
			set { _idhito = value ;}
		}

		private string _deshito;
		public string deshito
		{
			get {return _deshito;}
			set { _deshito = value ;}
		}

		private string _deshitolong;
		public string deshitolong
		{
			get {return _deshitolong;}
			set { _deshitolong = value ;}
		}

		private string _estado;
		public string estado
		{
			get {return _estado;}
			set { _estado = value ;}
		}

        private DateTime _fecha;
        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private byte _margen;
		public byte margen
		{
			get {return _margen;}
			set { _margen = value ;}
		}

		private short _orden;
		public short orden
		{
			get {return _orden;}
			set { _orden = value ;}
		}

        //private bool _manual;
        //public bool manual
        //{
        //    get {return _manual;}
        //    set { _manual = value ;}
        //}

        private bool _alerta;
        public bool alerta
        {
            get { return _alerta; }
            set { _alerta = value; }
        }
        private bool _ciclico;
        public bool ciclico
        {
            get { return _ciclico; }
            set { _ciclico = value; }
        }

        private string _modo;
        public string modo
        {
            get { return _modo; }
            set { _modo = value; }
        }

        private bool _hitoPE;
        public bool hitoPE
        {
            get { return _hitoPE; }
            set { _hitoPE = value; }
        }

        //Estado del proyecto económico
        private string _t301_estado;
        public string t301_estado
        {
            get { return _t301_estado; }
            set { _t301_estado = value; }
        }
        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        #endregion

		#region Constructores

		public HITOPSP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
        public static int Delete(SqlTransaction tr,string sTipoHito, int idhito)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdHito", SqlDbType.Int, 4);
            aParam[0].Value = idhito;

            if (sTipoHito == "HF")
            {
                if (tr == null)
                    return SqlHelper.ExecuteNonQuery("SUP_HITOPED", aParam);
                else
                    return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOPED", aParam);
            }
            else
            {
                if (tr == null)
                    return SqlHelper.ExecuteNonQuery("SUP_HITOPSPD", aParam);
                else
                    return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOPSPD", aParam);
            }
        }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t349_HITOPSP o t352_HITOPE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	23/11/2007 11:47:13
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, string sTipo, int idhito, string deshito, string deshitolong,
                                 string estado, Nullable<int> margen, Nullable<short> orden, bool alerta, bool bCiclico,
                                 int num_proyecto, DateTime fecha, bool hitoPE)
        {
            if (sTipo == "HT" || sTipo == "HM")
            {
                SqlParameter[] aParam = new SqlParameter[9];
                aParam[0] = new SqlParameter("@idhito", SqlDbType.Int, 4);
                aParam[0].Value = idhito;
                aParam[1] = new SqlParameter("@deshito", SqlDbType.Text, 50);
                aParam[1].Value = deshito;
                aParam[2] = new SqlParameter("@deshitolong", SqlDbType.Text, 2147483647);
                aParam[2].Value = deshitolong;
                aParam[3] = new SqlParameter("@estado", SqlDbType.Text, 1);
                aParam[3].Value = estado;
                aParam[4] = new SqlParameter("@margen", SqlDbType.TinyInt, 1);
                if (margen < 0) margen = null;
                aParam[4].Value = margen;
                aParam[5] = new SqlParameter("@orden", SqlDbType.SmallInt, 2);
                aParam[5].Value = orden;
                aParam[6] = new SqlParameter("@alerta", SqlDbType.Bit, 1);
                aParam[6].Value = alerta;
                aParam[7] = new SqlParameter("@ciclico", SqlDbType.Bit, 1);
                aParam[7].Value = bCiclico;
                aParam[8] = new SqlParameter("@hitoPE", SqlDbType.Bit, 1);
                aParam[8].Value = hitoPE;

                // Ejecuta la query y devuelve el numero de registros modificados.
                if (tr == null)
                    return SqlHelper.ExecuteNonQuery("SUP_HITOPSP_U", aParam);
                else
                    return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOPSP_U", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[9];
                aParam[0] = new SqlParameter("@idhito", SqlDbType.Int, 4);
                aParam[0].Value = idhito;
                aParam[1] = new SqlParameter("@deshito", SqlDbType.Text, 50);
                aParam[1].Value = deshito;
                aParam[2] = new SqlParameter("@deshitolong", SqlDbType.Text, 2147483647);
                aParam[2].Value = deshitolong;
                aParam[3] = new SqlParameter("@fecha", SqlDbType.SmallDateTime, 4);
                aParam[3].Value = fecha;
                aParam[4] = new SqlParameter("@orden", SqlDbType.SmallInt, 2);
                aParam[4].Value = orden;
                aParam[5] = new SqlParameter("@estado", SqlDbType.Text, 1);
                aParam[5].Value = estado;
                aParam[6] = new SqlParameter("@alerta", SqlDbType.Bit, 1);
                aParam[6].Value = alerta;
                aParam[7] = new SqlParameter("@ciclico", SqlDbType.Bit, 1);
                aParam[7].Value = alerta;
                aParam[8] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
                aParam[8].Value = num_proyecto;

                // Ejecuta la query y devuelve el numero de registros modificados.
                if (tr == null)
                    return SqlHelper.ExecuteNonQuery("SUP_HITOPE_U", aParam);
                else
                    return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOPE_U", aParam);
            }
        }
		
        /// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t349_HITOPSP,
		/// y devuelve una instancia u objeto del tipo HITOPSP
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	23/11/2007 11:47:13
		/// </history>
		/// -----------------------------------------------------------------------------
        public static HITOPSP Obtener(string sTipo, int idhito) 
		{
			HITOPSP o = new HITOPSP();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@idhito", SqlDbType.Int, 4);
			aParam[0].Value = idhito;

			SqlDataReader dr;
            if (sTipo == "HT" || sTipo == "HM")
            {
                dr = SqlHelper.ExecuteSqlDataReader("SUP_HITO_S1", aParam);
            }
            else
            {
                dr = SqlHelper.ExecuteSqlDataReader("SUP_HITO_S2", aParam);
            }
            if (dr.Read())
			{
				if (dr["idhito"] != DBNull.Value)
					o.idhito = (int)dr["idhito"];
				if (dr["deshito"] != DBNull.Value)
					o.deshito = (string)dr["deshito"];
				if (dr["deshitolong"] != DBNull.Value)
					o.deshitolong = (string)dr["deshitolong"];
				if (dr["estado"] != DBNull.Value)
					o.estado = (string)dr["estado"];
				if (dr["margen"] != DBNull.Value)
					o.margen = byte.Parse(dr["margen"].ToString());
				if (dr["orden"] != DBNull.Value)
                    o.orden = short.Parse(dr["orden"].ToString());
                if (sTipo == "HF") o.modo = "1";//"Fecha";
                else o.modo = "0";// "Cumplimiento ";
                if (dr["alerta"] != DBNull.Value)
                    o.alerta = (bool)dr["alerta"];
                if (dr["ciclico"] != DBNull.Value)
                    o.ciclico = (bool)dr["ciclico"];
                if (dr["t301_estado"] != DBNull.Value)
                    o.t301_estado = (string)dr["t301_estado"];
                //o.fecha.ToString() = "";
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
                if (sTipo == "HF")
                {
                    if (dr["fecha"] != DBNull.Value)
                        o.fecha = (DateTime)dr["fecha"];
                    //if (dr["cod_une"] != DBNull.Value)
                    //    o.cod_une = (short)dr["cod_une"];
                    //if (dr["num_proyecto"] != DBNull.Value)
                    //    o.num_proyecto = (int)dr["num_proyecto"];
                    o.hitoPE = false;
                }
                else
                {
                    //if (dr["hitoPE"].ToString() == "0")
                    if ((bool)dr["hitoPE"] == false)
                        o.hitoPE = false;
                    else
                    {
                        o.hitoPE = true;
                        o.modo = "0";// "Cumplimiento";
                    }
                }
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de HITOPSP"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}
        public static SqlDataReader CatalogoTareas(int idhito)
        {//Obtiene las tareas asociadas al hito
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nCodHito", SqlDbType.Int, 4);
            aParam[0].Value = idhito;

            return SqlHelper.ExecuteSqlDataReader("SUP_HITO_TAREA_CATA", aParam);
        }
        public static string CatalogoHitos(SqlTransaction tr, int idTarea)
        {//Obtiene las hitos asociados a la tarea
            string sRes="";
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nCodTarea", SqlDbType.Int, 4);
            aParam[0].Value = idTarea;

            if (tr == null)
            {
                dr = SqlHelper.ExecuteSqlDataReader("SUP_HITO_TAREA_CATA2", aParam);
            }
            else
            {
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_HITO_TAREA_CATA2", aParam);
            }
            while (dr.Read())
            {
                sRes += dr["t349_idhito"].ToString() + "@#@";
            }
            dr.Close();
            dr.Dispose();

            return sRes;
        }
        public static SqlDataReader CatalogoHitos(int iNumProy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int);
            aParam[0].Value = iNumProy;

            return SqlHelper.ExecuteSqlDataReader("SUP_HITOCATA1", aParam);
        }
        public static SqlDataReader CatalogoTareasPE(int iNumProy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int);
            aParam[0].Value = iNumProy;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREACATA2", aParam);
        }
        
        //Metodos sobre las tareas asociadas al hito
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T048_TAREAPSPHITO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	16/11/2006 11:15:27
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertTarea(SqlTransaction tr, int t349_idhito, int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t349_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t349_idhito;
            aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[1].Value = t332_idtarea;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_TAREAPSPHITO_I_SNE", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPHITO_I_SNE", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T048_TAREAPSPHITO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	16/11/2006 11:15:27
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int DeleteTarea(SqlTransaction tr, int t349_idhito, int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t349_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t349_idhito;
            aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[1].Value = t332_idtarea;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSPHITO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPHITO_D", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina registros de la tabla T048_TAREAPSPHITO a traves del código de tarea.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int DeleteTareas(SqlTransaction tr, int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSPHITO_D2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSPHITO_D2", aParam);
        }
        public static SqlDataReader CatalogoTareasHCM(int iT305IdProy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            //aParam[0] = new SqlParameter("@nCodUne", SqlDbType.SmallInt);
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int);
            //aParam[0].Value = iCodUne;
            aParam[0].Value = iT305IdProy;

            return SqlHelper.ExecuteSqlDataReader("SUP_GANTT_HMC", aParam);
        }
        #endregion
	}
}
