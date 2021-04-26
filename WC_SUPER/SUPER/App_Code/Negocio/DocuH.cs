using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCUH
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T366_DOCUH
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUH
	{
		#region Propiedades y Atributos

		private int _t366_iddocuh;
		public int t366_iddocuh
		{
			get {return _t366_iddocuh;}
			set { _t366_iddocuh = value ;}
		}

		private int _t349_idhito;
		public int t349_idhito
		{
			get {return _t349_idhito;}
			set { _t349_idhito = value ;}
		}

		private string _t366_descripcion;
		public string t366_descripcion
		{
			get {return _t366_descripcion;}
			set { _t366_descripcion = value ;}
		}

		private string _t366_weblink;
		public string t366_weblink
		{
			get {return _t366_weblink;}
			set { _t366_weblink = value ;}
		}

		private string _t366_nombrearchivo;
		public string t366_nombrearchivo
		{
			get {return _t366_nombrearchivo;}
			set { _t366_nombrearchivo = value ;}
		}

		private byte[] _t366_archivo;
		public byte[] t366_archivo
		{
			get {return _t366_archivo;}
			set { _t366_archivo = value ;}
		}

		private bool _t366_privado;
		public bool t366_privado
		{
			get {return _t366_privado;}
			set { _t366_privado = value ;}
		}

		private bool _t366_modolectura;
		public bool t366_modolectura
		{
			get {return _t366_modolectura;}
			set { _t366_modolectura = value ;}
		}

		private bool _t366_tipogestion;
		public bool t366_tipogestion
		{
			get {return _t366_tipogestion;}
			set { _t366_tipogestion = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}

		private DateTime _t366_fecha;
		public DateTime t366_fecha
		{
			get {return _t366_fecha;}
			set { _t366_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t366_fechamodif;
		public DateTime t366_fechamodif
		{
			get {return _t366_fechamodif;}
			set { _t366_fechamodif = value ;}
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

		public DOCUH() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T366_DOCUH.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t349_idhito, string t366_descripcion, string t366_weblink, string t366_nombrearchivo,
                                 Nullable<long> idContentServer, bool t366_privado, bool t366_modolectura, bool t366_tipogestion, 
                                 int t314_idusuario_autor)
		{
            //if (t366_archivo.Length == 0) t366_archivo = null;

            SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t349_idhito", SqlDbType.Int, 4);
			aParam[0].Value = t349_idhito;
			aParam[1] = new SqlParameter("@t366_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t366_descripcion;
			aParam[2] = new SqlParameter("@t366_weblink", SqlDbType.Text, 250);
			aParam[2].Value = t366_weblink;
			aParam[3] = new SqlParameter("@t366_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t366_nombrearchivo;

            //aParam[4] = new SqlParameter("@t366_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t366_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t366_privado", SqlDbType.Bit, 1);
			aParam[5].Value = t366_privado;
			aParam[6] = new SqlParameter("@t366_modolectura", SqlDbType.Bit, 1);
			aParam[6].Value = t366_modolectura;
			aParam[7] = new SqlParameter("@t366_tipogestion", SqlDbType.Bit, 1);
			aParam[7].Value = t366_tipogestion;
			aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[8].Value = t314_idusuario_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUH_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUH_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T366_DOCUH.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t366_iddocuh, int t349_idhito, string t366_descripcion, string t366_weblink,
                                 string t366_nombrearchivo, Nullable<long> idContentServer, bool t366_privado, bool t366_modolectura, 
                                 bool t366_tipogestion, int t314_idusuario_modif)
		{
            //if (t366_archivo.Length == 0) t366_archivo = null;

            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t366_iddocuh", SqlDbType.Int, 4);
			aParam[0].Value = t366_iddocuh;
			aParam[1] = new SqlParameter("@t349_idhito", SqlDbType.Int, 4);
			aParam[1].Value = t349_idhito;
			aParam[2] = new SqlParameter("@t366_descripcion", SqlDbType.Text, 50);
			aParam[2].Value = t366_descripcion;
			aParam[3] = new SqlParameter("@t366_weblink", SqlDbType.Text, 250);
			aParam[3].Value = t366_weblink;
			aParam[4] = new SqlParameter("@t366_nombrearchivo", SqlDbType.Text, 250);
			aParam[4].Value = t366_nombrearchivo;

            //aParam[5] = new SqlParameter("@t366_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t366_archivo;

            aParam[6] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[6].Value = idContentServer;
            aParam[7] = new SqlParameter("@t366_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t366_privado;
			aParam[8] = new SqlParameter("@t366_modolectura", SqlDbType.Bit, 1);
			aParam[8].Value = t366_modolectura;
			aParam[9] = new SqlParameter("@t366_tipogestion", SqlDbType.Bit, 1);
			aParam[9].Value = t366_tipogestion;
            aParam[10] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[10].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUH_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUH_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T366_DOCUH a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t366_iddocuh)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t366_iddocuh", SqlDbType.Int, 4);
			aParam[0].Value = t366_iddocuh;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUH_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUH_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T366_DOCUH,
		/// y devuelve una instancia u objeto del tipo DOCUH
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCUH Select(SqlTransaction tr, int t366_iddocuh)//, bool bTraerArchivo) 
		{
			DOCUH o = new DOCUH();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t366_iddocuh", SqlDbType.Int, 4);
			aParam[0].Value = t366_iddocuh;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUH_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUH_S", aParam);

			if (dr.Read())
			{
				if (dr["t366_iddocuh"] != DBNull.Value)
					o.t366_iddocuh = (int)dr["t366_iddocuh"];
				if (dr["t349_idhito"] != DBNull.Value)
					o.t349_idhito = (int)dr["t349_idhito"];
				if (dr["t366_descripcion"] != DBNull.Value)
					o.t366_descripcion = (string)dr["t366_descripcion"];
				if (dr["t366_weblink"] != DBNull.Value)
					o.t366_weblink = (string)dr["t366_weblink"];
				if (dr["t366_nombrearchivo"] != DBNull.Value)
					o.t366_nombrearchivo = (string)dr["t366_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t366_archivo = (byte[])dr["t366_archivo"];
                //}
                if (dr["t366_privado"] != DBNull.Value)
					o.t366_privado = (bool)dr["t366_privado"];
				if (dr["t366_modolectura"] != DBNull.Value)
					o.t366_modolectura = (bool)dr["t366_modolectura"];
				if (dr["t366_tipogestion"] != DBNull.Value)
					o.t366_tipogestion = (bool)dr["t366_tipogestion"];
				if (dr["t314_idusuario_autor"] != DBNull.Value)
					o.t314_idusuario_autor = (int)dr["t314_idusuario_autor"];
				if (dr["t366_fecha"] != DBNull.Value)
					o.t366_fecha = (DateTime)dr["t366_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = (int)dr["t314_idusuario_modif"];
				if (dr["t366_fechamodif"] != DBNull.Value)
					o.t366_fechamodif = (DateTime)dr["t366_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUH"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T366_DOCUH.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t349_idhito, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t349_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t349_idhito;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUH_C2", aParam);
        }
        public static SqlDataReader Catalogo3(int t349_idhito)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t349_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t349_idhito;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUH_C3", aParam);
        }

		#endregion
	}
}
