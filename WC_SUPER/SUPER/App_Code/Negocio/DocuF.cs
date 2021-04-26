using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCUF
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T364_DOCUF
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUF
	{
		#region Propiedades y Atributos

		private int _t364_iddocuf;
		public int t364_iddocuf
		{
			get {return _t364_iddocuf;}
			set { _t364_iddocuf = value ;}
		}

		private int _t334_idfase;
		public int t334_idfase
		{
			get {return _t334_idfase;}
			set { _t334_idfase = value ;}
		}

		private string _t364_descripcion;
		public string t364_descripcion
		{
			get {return _t364_descripcion;}
			set { _t364_descripcion = value ;}
		}

		private string _t364_weblink;
		public string t364_weblink
		{
			get {return _t364_weblink;}
			set { _t364_weblink = value ;}
		}

		private string _t364_nombrearchivo;
		public string t364_nombrearchivo
		{
			get {return _t364_nombrearchivo;}
			set { _t364_nombrearchivo = value ;}
		}

		private byte[] _t364_archivo;
		public byte[] t364_archivo
		{
			get {return _t364_archivo;}
			set { _t364_archivo = value ;}
		}

		private bool _t364_privado;
		public bool t364_privado
		{
			get {return _t364_privado;}
			set { _t364_privado = value ;}
		}

		private bool _t364_modolectura;
		public bool t364_modolectura
		{
			get {return _t364_modolectura;}
			set { _t364_modolectura = value ;}
		}

		private bool _t364_tipogestion;
		public bool t364_tipogestion
		{
			get {return _t364_tipogestion;}
			set { _t364_tipogestion = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}

		private DateTime _t364_fecha;
		public DateTime t364_fecha
		{
			get {return _t364_fecha;}
			set { _t364_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t364_fechamodif;
		public DateTime t364_fechamodif
		{
			get {return _t364_fechamodif;}
			set { _t364_fechamodif = value ;}
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

		public DOCUF() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T364_DOCUF.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t334_idfase, string t364_descripcion, string t364_weblink, string t364_nombrearchivo,
                                 Nullable<long> idContentServer, bool t364_privado, bool t364_modolectura, bool t364_tipogestion, 
                                 int t314_idusuario_autor)
		{
            //if (t364_archivo.Length == 0) t364_archivo = null;

            SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
			aParam[0].Value = t334_idfase;
			aParam[1] = new SqlParameter("@t364_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t364_descripcion;
			aParam[2] = new SqlParameter("@t364_weblink", SqlDbType.Text, 250);
			aParam[2].Value = t364_weblink;
			aParam[3] = new SqlParameter("@t364_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t364_nombrearchivo;

            //aParam[4] = new SqlParameter("@t364_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t364_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t364_privado", SqlDbType.Bit, 1);
			aParam[5].Value = t364_privado;
			aParam[6] = new SqlParameter("@t364_modolectura", SqlDbType.Bit, 1);
			aParam[6].Value = t364_modolectura;
			aParam[7] = new SqlParameter("@t364_tipogestion", SqlDbType.Bit, 1);
			aParam[7].Value = t364_tipogestion;
			aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[8].Value = t314_idusuario_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUF_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUF_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T364_DOCUF.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t364_iddocuf, int t334_idfase, string t364_descripcion, string t364_weblink,
                                 string t364_nombrearchivo, Nullable<long> idContentServer, bool t364_privado, bool t364_modolectura, 
                                 bool t364_tipogestion, int t314_idusuario_modif)
		{
            //if (t364_archivo.Length == 0) t364_archivo = null;

            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t364_iddocuf", SqlDbType.Int, 4);
			aParam[0].Value = t364_iddocuf;
			aParam[1] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
			aParam[1].Value = t334_idfase;
			aParam[2] = new SqlParameter("@t364_descripcion", SqlDbType.Text, 50);
			aParam[2].Value = t364_descripcion;
			aParam[3] = new SqlParameter("@t364_weblink", SqlDbType.Text, 250);
			aParam[3].Value = t364_weblink;
			aParam[4] = new SqlParameter("@t364_nombrearchivo", SqlDbType.Text, 250);
			aParam[4].Value = t364_nombrearchivo;

            //aParam[5] = new SqlParameter("@t364_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t364_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t364_privado", SqlDbType.Bit, 1);
			aParam[6].Value = t364_privado;
			aParam[7] = new SqlParameter("@t364_modolectura", SqlDbType.Bit, 1);
			aParam[7].Value = t364_modolectura;
			aParam[8] = new SqlParameter("@t364_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t364_tipogestion;
            aParam[9] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[9].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUF_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUF_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T364_DOCUF a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t364_iddocuf)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t364_iddocuf", SqlDbType.Int, 4);
			aParam[0].Value = t364_iddocuf;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUF_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUF_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T364_DOCUF,
		/// y devuelve una instancia u objeto del tipo DOCUF
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCUF Select(SqlTransaction tr, int t364_iddocuf)//, bool bTraerArchivo) 
		{
			DOCUF o = new DOCUF();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t364_iddocuf", SqlDbType.Int, 4);
			aParam[0].Value = t364_iddocuf;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUF_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUF_S", aParam);

			if (dr.Read())
			{
				if (dr["t364_iddocuf"] != DBNull.Value)
					o.t364_iddocuf = (int)dr["t364_iddocuf"];
				if (dr["t334_idfase"] != DBNull.Value)
					o.t334_idfase = (int)dr["t334_idfase"];
				if (dr["t364_descripcion"] != DBNull.Value)
					o.t364_descripcion = (string)dr["t364_descripcion"];
				if (dr["t364_weblink"] != DBNull.Value)
					o.t364_weblink = (string)dr["t364_weblink"];
				if (dr["t364_nombrearchivo"] != DBNull.Value)
					o.t364_nombrearchivo = (string)dr["t364_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t364_archivo = (byte[])dr["t364_archivo"];
                //}
                if (dr["t364_privado"] != DBNull.Value)
					o.t364_privado = (bool)dr["t364_privado"];
				if (dr["t364_modolectura"] != DBNull.Value)
					o.t364_modolectura = (bool)dr["t364_modolectura"];
				if (dr["t364_tipogestion"] != DBNull.Value)
					o.t364_tipogestion = (bool)dr["t364_tipogestion"];
				if (dr["t314_idusuario_autor"] != DBNull.Value)
					o.t314_idusuario_autor = (int)dr["t314_idusuario_autor"];
				if (dr["t364_fecha"] != DBNull.Value)
					o.t364_fecha = (DateTime)dr["t364_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = (int)dr["t314_idusuario_modif"];
				if (dr["t364_fechamodif"] != DBNull.Value)
					o.t364_fechamodif = (DateTime)dr["t364_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUF"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T364_DOCUF.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t334_idfase, int t314_idusuario_autor)
		{
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = t334_idfase;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;
            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUF_C2", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la lista de documentos de los items dependientes de la fase.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Lista(SqlTransaction tr, int t334_idfase, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = t334_idfase;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUF_LISTA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUF_LISTA", aParam);
        }

		#endregion
	}
}
