using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCUT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T363_DOCUT
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 9:07:28	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUT
	{
		#region Propiedades y Atributos

		private int _t363_iddocut;
		public int t363_iddocut
		{
			get {return _t363_iddocut;}
			set { _t363_iddocut = value ;}
		}

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
		}

		private string _t363_descripcion;
		public string t363_descripcion
		{
			get {return _t363_descripcion;}
			set { _t363_descripcion = value ;}
		}

		private string _t363_weblink;
		public string t363_weblink
		{
			get {return _t363_weblink;}
			set { _t363_weblink = value ;}
		}

		private string _t363_nombrearchivo;
		public string t363_nombrearchivo
		{
			get {return _t363_nombrearchivo;}
			set { _t363_nombrearchivo = value ;}
		}

		private byte[] _t363_archivo;
		public byte[] t363_archivo
		{
			get {return _t363_archivo;}
			set { _t363_archivo = value ;}
		}

		private bool _t363_privado;
		public bool t363_privado
		{
			get {return _t363_privado;}
			set { _t363_privado = value ;}
		}

		private bool _t363_modolectura;
		public bool t363_modolectura
		{
			get {return _t363_modolectura;}
			set { _t363_modolectura = value ;}
		}

		private bool _t363_tipogestion;
		public bool t363_tipogestion
		{
			get {return _t363_tipogestion;}
			set { _t363_tipogestion = value ;}
		}

        private int _t314_idusuario_autor;
        public int t314_idusuario_autor
        {
            get { return _t314_idusuario_autor; }
            set { _t314_idusuario_autor = value; }
        }

        private DateTime _t363_fecha;
        public DateTime t363_fecha
        {
            get { return _t363_fecha; }
            set { _t363_fecha = value; }
        }
        
        private int _t314_idusuario_modif;
        public int t314_idusuario_modif
        {
            get { return _t314_idusuario_modif; }
            set { _t314_idusuario_modif = value; }
        }

        private DateTime _t363_fechamodif;
        public DateTime t363_fechamodif
        {
            get { return _t363_fechamodif; }
            set { _t363_fechamodif = value; }
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

		public DOCUT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T363_DOCUT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 9:07:28
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t332_idtarea , string t363_descripcion , string t363_weblink ,
                                 string t363_nombrearchivo, Nullable<long> idContentServer, bool t363_privado, 
                                 bool t363_modolectura, bool t363_tipogestion , int t314_idusuario_autor)
		{
            //if (t363_archivo.Length == 0) t363_archivo = null;

			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t363_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t363_descripcion;
			aParam[2] = new SqlParameter("@t363_weblink", SqlDbType.Text, 250);
			aParam[2].Value = t363_weblink;
			aParam[3] = new SqlParameter("@t363_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t363_nombrearchivo;

            //aParam[4] = new SqlParameter("@t363_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t363_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
			aParam[5] = new SqlParameter("@t363_privado", SqlDbType.Bit, 1);
			aParam[5].Value = t363_privado;
			aParam[6] = new SqlParameter("@t363_modolectura", SqlDbType.Bit, 1);
			aParam[6].Value = t363_modolectura;
			aParam[7] = new SqlParameter("@t363_tipogestion", SqlDbType.Bit, 1);
			aParam[7].Value = t363_tipogestion;
			aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[8].Value = t314_idusuario_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUT_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUT_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T363_DOCUT.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 9:07:28
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t363_iddocut, int t332_idtarea, string t363_descripcion, string t363_weblink,
                                 string t363_nombrearchivo, Nullable<long> idContentServer, bool t363_privado, bool t363_modolectura, 
                                 bool t363_tipogestion, int t314_idusuario_modif)
		{
            //if (t363_archivo.Length == 0) t363_archivo = null;

			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t363_iddocut", SqlDbType.Int, 4);
			aParam[0].Value = t363_iddocut;
			aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[1].Value = t332_idtarea;
			aParam[2] = new SqlParameter("@t363_descripcion", SqlDbType.Text, 250);
			aParam[2].Value = t363_descripcion;
			aParam[3] = new SqlParameter("@t363_weblink", SqlDbType.Text, 250);
			aParam[3].Value = t363_weblink;
			aParam[4] = new SqlParameter("@t363_nombrearchivo", SqlDbType.Text, 50);
			aParam[4].Value = t363_nombrearchivo;

            //aParam[5] = new SqlParameter("@t363_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t363_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
			aParam[6] = new SqlParameter("@t363_privado", SqlDbType.Bit, 1);
			aParam[6].Value = t363_privado;
			aParam[7] = new SqlParameter("@t363_modolectura", SqlDbType.Bit, 1);
			aParam[7].Value = t363_modolectura;
			aParam[8] = new SqlParameter("@t363_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t363_tipogestion;
            aParam[9] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[9].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T363_DOCUT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 9:07:28
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t363_iddocut)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t363_iddocut", SqlDbType.Int, 4);
			aParam[0].Value = t363_iddocut;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T363_DOCUT,
		/// y devuelve una instancia u objeto del tipo DOCUT
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 9:07:28
		/// </history>
		/// -----------------------------------------------------------------------------
		public static DOCUT Select(SqlTransaction tr, int t363_iddocut)//, bool bTraerArchivo) 
		{
			DOCUT o = new DOCUT();

			SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t363_iddocut", SqlDbType.Int, 4);
            aParam[0].Value = t363_iddocut;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUT_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUT_S", aParam);

			if (dr.Read())
			{
				if (dr["t363_iddocut"] != DBNull.Value)
					o.t363_iddocut = (int)dr["t363_iddocut"];
				if (dr["t332_idtarea"] != DBNull.Value)
					o.t332_idtarea = (int)dr["t332_idtarea"];
				if (dr["t363_descripcion"] != DBNull.Value)
					o.t363_descripcion = (string)dr["t363_descripcion"];
				if (dr["t363_weblink"] != DBNull.Value)
					o.t363_weblink = (string)dr["t363_weblink"];
				if (dr["t363_nombrearchivo"] != DBNull.Value)
					o.t363_nombrearchivo = (string)dr["t363_nombrearchivo"];
				//if (dr["t363_archivo"] != DBNull.Value)
				//	o.t363_archivo = (byte[])dr["t363_archivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t363_archivo = (byte[])dr["t363_archivo"];
                //}
				if (dr["t363_privado"] != DBNull.Value)
					o.t363_privado = (bool)dr["t363_privado"];
				if (dr["t363_modolectura"] != DBNull.Value)
					o.t363_modolectura = (bool)dr["t363_modolectura"];
				if (dr["t363_tipogestion"] != DBNull.Value)
					o.t363_tipogestion = (bool)dr["t363_tipogestion"];
                if (dr["t314_idusuario_autor"] != DBNull.Value)
                    o.t314_idusuario_autor = (int)dr["t314_idusuario_autor"];
                if (dr["t363_fecha"] != DBNull.Value)
                    o.t363_fecha = DateTime.Parse(dr["t363_fecha"].ToString());
                if (dr["t314_idusuario_modif"] != DBNull.Value)
                    o.t314_idusuario_modif = (int)dr["t314_idusuario_modif"];
                if (dr["t363_fechamodif"] != DBNull.Value)
                    o.t363_fechamodif = DateTime.Parse(dr["t363_fechamodif"].ToString());
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUT"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T363_DOCUT.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/11/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t332_idtarea, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUT_C2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T363_DOCUT visibles desde IAP
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/11/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo3(int t332_idtarea, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUT_C3", aParam);
        }

        #endregion
	}
}
