using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCUA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T365_DOCUA
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUA
	{
		#region Propiedades y Atributos

		private int _t365_iddocua;
		public int t365_iddocua
		{
			get {return _t365_iddocua;}
			set { _t365_iddocua = value ;}
		}

		private int _t335_idactividad;
		public int t335_idactividad
		{
			get {return _t335_idactividad;}
			set { _t335_idactividad = value ;}
		}

		private string _t365_descripcion;
		public string t365_descripcion
		{
			get {return _t365_descripcion;}
			set { _t365_descripcion = value ;}
		}

		private string _t365_weblink;
		public string t365_weblink
		{
			get {return _t365_weblink;}
			set { _t365_weblink = value ;}
		}

		private string _t365_nombrearchivo;
		public string t365_nombrearchivo
		{
			get {return _t365_nombrearchivo;}
			set { _t365_nombrearchivo = value ;}
		}

		private byte[] _t365_archivo;
		public byte[] t365_archivo
		{
			get {return _t365_archivo;}
			set { _t365_archivo = value ;}
		}

		private bool _t365_privado;
		public bool t365_privado
		{
			get {return _t365_privado;}
			set { _t365_privado = value ;}
		}

		private bool _t365_modolectura;
		public bool t365_modolectura
		{
			get {return _t365_modolectura;}
			set { _t365_modolectura = value ;}
		}

		private bool _t365_tipogestion;
		public bool t365_tipogestion
		{
			get {return _t365_tipogestion;}
			set { _t365_tipogestion = value ;}
		}

		private int _t365_autor;
		public int t365_autor
		{
			get {return _t365_autor;}
			set { _t365_autor = value ;}
		}

		private DateTime _t365_fecha;
		public DateTime t365_fecha
		{
			get {return _t365_fecha;}
			set { _t365_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t365_fechamodif;
		public DateTime t365_fechamodif
		{
			get {return _t365_fechamodif;}
			set { _t365_fechamodif = value ;}
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

		public DOCUA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T365_DOCUA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t335_idactividad, string t365_descripcion, string t365_weblink,
                                 string t365_nombrearchivo, Nullable<long> idContentServer, bool t365_privado, bool t365_modolectura, 
                                 bool t365_tipogestion, int t365_autor)
		{
            //if (t365_archivo.Length == 0) t365_archivo = null;

            SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
			aParam[0].Value = t335_idactividad;
			aParam[1] = new SqlParameter("@t365_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t365_descripcion;
			aParam[2] = new SqlParameter("@t365_weblink", SqlDbType.Text, 250);
			aParam[2].Value = t365_weblink;
			aParam[3] = new SqlParameter("@t365_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t365_nombrearchivo;

            //aParam[4] = new SqlParameter("@t365_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t365_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t365_privado", SqlDbType.Bit, 1);
			aParam[5].Value = t365_privado;
			aParam[6] = new SqlParameter("@t365_modolectura", SqlDbType.Bit, 1);
			aParam[6].Value = t365_modolectura;
			aParam[7] = new SqlParameter("@t365_tipogestion", SqlDbType.Bit, 1);
			aParam[7].Value = t365_tipogestion;
            aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[8].Value = t365_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUA_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUA_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T365_DOCUA.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t365_iddocua, int t335_idactividad, string t365_descripcion, string t365_weblink,
                                 string t365_nombrearchivo, Nullable<long> idContentServer, bool t365_privado, bool t365_modolectura, 
                                 bool t365_tipogestion, int t314_idusuario_modif)
		{
            //if (t365_archivo.Length == 0) t365_archivo = null;

            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t365_iddocua", SqlDbType.Int, 4);
			aParam[0].Value = t365_iddocua;
			aParam[1] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
			aParam[1].Value = t335_idactividad;
			aParam[2] = new SqlParameter("@t365_descripcion", SqlDbType.Text, 50);
			aParam[2].Value = t365_descripcion;
			aParam[3] = new SqlParameter("@t365_weblink", SqlDbType.Text, 250);
			aParam[3].Value = t365_weblink;
			aParam[4] = new SqlParameter("@t365_nombrearchivo", SqlDbType.Text, 250);
			aParam[4].Value = t365_nombrearchivo;

            //aParam[5] = new SqlParameter("@t365_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t365_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t365_privado", SqlDbType.Bit, 1);
			aParam[6].Value = t365_privado;
			aParam[7] = new SqlParameter("@t365_modolectura", SqlDbType.Bit, 1);
			aParam[7].Value = t365_modolectura;
			aParam[8] = new SqlParameter("@t365_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t365_tipogestion;
            aParam[9] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[9].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUA_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUA_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T365_DOCUA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t365_iddocua)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t365_iddocua", SqlDbType.Int, 4);
			aParam[0].Value = t365_iddocua;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUA_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T365_DOCUA,
		/// y devuelve una instancia u objeto del tipo DOCUA
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCUA Select(SqlTransaction tr, int t365_iddocua)//, bool bTraerArchivo) 
		{
			DOCUA o = new DOCUA();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t365_iddocua", SqlDbType.Int, 4);
			aParam[0].Value = t365_iddocua;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUA_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUA_S", aParam);

			if (dr.Read())
			{
				if (dr["t365_iddocua"] != DBNull.Value)
					o.t365_iddocua = (int)dr["t365_iddocua"];
				if (dr["t335_idactividad"] != DBNull.Value)
					o.t335_idactividad = (int)dr["t335_idactividad"];
				if (dr["t365_descripcion"] != DBNull.Value)
					o.t365_descripcion = (string)dr["t365_descripcion"];
				if (dr["t365_weblink"] != DBNull.Value)
					o.t365_weblink = (string)dr["t365_weblink"];
				if (dr["t365_nombrearchivo"] != DBNull.Value)
					o.t365_nombrearchivo = (string)dr["t365_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t365_archivo = (byte[])dr["t365_archivo"];
                //}
                if (dr["t365_privado"] != DBNull.Value)
					o.t365_privado = (bool)dr["t365_privado"];
				if (dr["t365_modolectura"] != DBNull.Value)
					o.t365_modolectura = (bool)dr["t365_modolectura"];
				if (dr["t365_tipogestion"] != DBNull.Value)
					o.t365_tipogestion = (bool)dr["t365_tipogestion"];
                if (dr["t314_idusuario_autor"] != DBNull.Value)
                    o.t365_autor = (int)dr["t314_idusuario_autor"];
				if (dr["t365_fecha"] != DBNull.Value)
					o.t365_fecha = (DateTime)dr["t365_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = (int)dr["t314_idusuario_modif"];
				if (dr["t365_fechamodif"] != DBNull.Value)
					o.t365_fechamodif = (DateTime)dr["t365_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUA"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T365_DOCUA.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t335_idactividad, int t314_idusuario_autor)
		{
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = t335_idactividad;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUA_C2", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la lista de documentos de los items dependientes de la actividad.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Lista(SqlTransaction tr, int t335_idactividad, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = t335_idactividad;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUA_LISTA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUA_LISTA", aParam);
        }

        #endregion
	}
}
