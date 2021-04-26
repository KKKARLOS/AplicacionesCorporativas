using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCUPT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T362_DOCUPT
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUPT
	{
		#region Propiedades y Atributos

		private int _t362_iddocupt;
		public int t362_iddocupt
		{
			get {return _t362_iddocupt;}
			set { _t362_iddocupt = value ;}
		}

		private int _t331_idpt;
		public int t331_idpt
		{
			get {return _t331_idpt;}
			set { _t331_idpt = value ;}
		}

		private string _t362_descripcion;
		public string t362_descripcion
		{
			get {return _t362_descripcion;}
			set { _t362_descripcion = value ;}
		}

		private string _t362_weblink;
		public string t362_weblink
		{
			get {return _t362_weblink;}
			set { _t362_weblink = value ;}
		}

		private string _t362_nombrearchivo;
		public string t362_nombrearchivo
		{
			get {return _t362_nombrearchivo;}
			set { _t362_nombrearchivo = value ;}
		}

		private byte[] _t362_archivo;
		public byte[] t362_archivo
		{
			get {return _t362_archivo;}
			set { _t362_archivo = value ;}
		}

		private bool _t362_privado;
		public bool t362_privado
		{
			get {return _t362_privado;}
			set { _t362_privado = value ;}
		}

		private bool _t362_modolectura;
		public bool t362_modolectura
		{
			get {return _t362_modolectura;}
			set { _t362_modolectura = value ;}
		}

		private bool _t362_tipogestion;
		public bool t362_tipogestion
		{
			get {return _t362_tipogestion;}
			set { _t362_tipogestion = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}

		private DateTime _t362_fecha;
		public DateTime t362_fecha
		{
			get {return _t362_fecha;}
			set { _t362_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t362_fechamodif;
		public DateTime t362_fechamodif
		{
			get {return _t362_fechamodif;}
			set { _t362_fechamodif = value ;}
		}

        private string _DesAutor;
        public string DesAutor
        {
            get { return _DesAutor; }
            set { _DesAutor = value; }
        }

        private string _DesAutorModif;
        public string DesAutorModif
        {
            get { return _DesAutorModif; }
            set { _DesAutorModif = value; }
        }
        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }
        #endregion

		#region Constructores

		public DOCUPT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T362_DOCUPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t331_idpt, string t362_descripcion, string t362_weblink, string t362_nombrearchivo,
                                 Nullable<long> idContentServer, bool t362_privado, bool t362_modolectura, bool t362_tipogestion, 
                                 int t314_idusuario_autor)
		{
            //if (t362_archivo.Length == 0) t362_archivo = null;

            SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t362_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t362_descripcion;
			aParam[2] = new SqlParameter("@t362_weblink", SqlDbType.Text, 250);
			aParam[2].Value = t362_weblink;
			aParam[3] = new SqlParameter("@t362_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t362_nombrearchivo;

            //aParam[4] = new SqlParameter("@t362_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t362_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t362_privado", SqlDbType.Bit, 1);
			aParam[5].Value = t362_privado;
			aParam[6] = new SqlParameter("@t362_modolectura", SqlDbType.Bit, 1);
			aParam[6].Value = t362_modolectura;
			aParam[7] = new SqlParameter("@t362_tipogestion", SqlDbType.Bit, 1);
			aParam[7].Value = t362_tipogestion;
			aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[8].Value = t314_idusuario_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUPT_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUPT_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T362_DOCUPT.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t362_iddocupt, int t331_idpt, string t362_descripcion, string t362_weblink,
                                 string t362_nombrearchivo, Nullable<long> idContentServer, bool t362_privado, bool t362_modolectura, 
                                 bool t362_tipogestion, int t314_idusuario_modif)
		{
            //if (t362_archivo.Length == 0) t362_archivo = null;

            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t362_iddocupt", SqlDbType.Int, 4);
			aParam[0].Value = t362_iddocupt;
			aParam[1] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[1].Value = t331_idpt;
			aParam[2] = new SqlParameter("@t362_descripcion", SqlDbType.Text, 50);
			aParam[2].Value = t362_descripcion;
			aParam[3] = new SqlParameter("@t362_weblink", SqlDbType.Text, 250);
			aParam[3].Value = t362_weblink;
			aParam[4] = new SqlParameter("@t362_nombrearchivo", SqlDbType.Text, 250);
			aParam[4].Value = t362_nombrearchivo;

            //aParam[5] = new SqlParameter("@t362_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t362_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t362_privado", SqlDbType.Bit, 1);
			aParam[6].Value = t362_privado;
			aParam[7] = new SqlParameter("@t362_modolectura", SqlDbType.Bit, 1);
			aParam[7].Value = t362_modolectura;
			aParam[8] = new SqlParameter("@t362_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t362_tipogestion;
            aParam[9] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[9].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUPT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUPT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T362_DOCUPT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t362_iddocupt)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t362_iddocupt", SqlDbType.Int, 4);
			aParam[0].Value = t362_iddocupt;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUPT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUPT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T362_DOCUPT,
		/// y devuelve una instancia u objeto del tipo DOCUPT
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCUPT Select(SqlTransaction tr, int t362_iddocupt)//, bool bTraerArchivo) 
		{
			DOCUPT o = new DOCUPT();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t362_iddocupt", SqlDbType.Int, 4);
			aParam[0].Value = t362_iddocupt;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUPT_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUPT_S", aParam);

			if (dr.Read())
			{
				if (dr["t362_iddocupt"] != DBNull.Value)
					o.t362_iddocupt = (int)dr["t362_iddocupt"];
				if (dr["t331_idpt"] != DBNull.Value)
					o.t331_idpt = (int)dr["t331_idpt"];
				if (dr["t362_descripcion"] != DBNull.Value)
					o.t362_descripcion = (string)dr["t362_descripcion"];
				if (dr["t362_weblink"] != DBNull.Value)
					o.t362_weblink = (string)dr["t362_weblink"];
				if (dr["t362_nombrearchivo"] != DBNull.Value)
					o.t362_nombrearchivo = (string)dr["t362_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t362_archivo = (byte[])dr["t362_archivo"];
                //}
                if (dr["t362_privado"] != DBNull.Value)
					o.t362_privado = (bool)dr["t362_privado"];
				if (dr["t362_modolectura"] != DBNull.Value)
					o.t362_modolectura = (bool)dr["t362_modolectura"];
				if (dr["t362_tipogestion"] != DBNull.Value)
					o.t362_tipogestion = (bool)dr["t362_tipogestion"];
				if (dr["t314_idusuario_autor"] != DBNull.Value)
					o.t314_idusuario_autor = (int)dr["t314_idusuario_autor"];
				if (dr["t362_fecha"] != DBNull.Value)
					o.t362_fecha = (DateTime)dr["t362_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = (int)dr["t314_idusuario_modif"];
				if (dr["t362_fechamodif"] != DBNull.Value)
					o.t362_fechamodif = (DateTime)dr["t362_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUPT"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T362_DOCUPT.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t331_idpt, int t314_idusuario_autor)
		{
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t331_idpt", SqlDbType.Int, 4, t331_idpt),
                ParametroSql.add("@t314_idusuario_autor", SqlDbType.Int, 4, t314_idusuario_autor)
            };
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUPT_C2", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la lista de documentos de los items dependientes del proyeto técnico.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Lista(SqlTransaction tr, int t331_idpt, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t331_idpt", SqlDbType.Int, 4, t331_idpt),
                ParametroSql.add("@t314_idusuario_autor", SqlDbType.Int, 4, t314_idusuario_autor)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUPT_LISTA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUPT_LISTA", aParam);
        }

        public static int UpdateIdDoc(SqlTransaction tr, int t362_iddocupt, long t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t362_iddocupt", SqlDbType.Int, 4, t362_iddocupt),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUPT_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUPT_U2", aParam);
        }

        #endregion
	}
}
