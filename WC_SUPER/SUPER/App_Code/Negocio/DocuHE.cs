using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCUHE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T367_DOCUHE
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUHE
	{
		#region Propiedades y Atributos

		private int _t367_iddocuhe;
		public int t367_iddocuhe
		{
			get {return _t367_iddocuhe;}
			set { _t367_iddocuhe = value ;}
		}

		private int _t352_idhito;
		public int t352_idhito
		{
			get {return _t352_idhito;}
			set { _t352_idhito = value ;}
		}

		private string _t367_descripcion;
		public string t367_descripcion
		{
			get {return _t367_descripcion;}
			set { _t367_descripcion = value ;}
		}

		private string _t367_weblink;
		public string t367_weblink
		{
			get {return _t367_weblink;}
			set { _t367_weblink = value ;}
		}

		private string _t367_nombrearchivo;
		public string t367_nombrearchivo
		{
			get {return _t367_nombrearchivo;}
			set { _t367_nombrearchivo = value ;}
		}

		private byte[] _t367_archivo;
		public byte[] t367_archivo
		{
			get {return _t367_archivo;}
			set { _t367_archivo = value ;}
		}

		private bool _t367_privado;
		public bool t367_privado
		{
			get {return _t367_privado;}
			set { _t367_privado = value ;}
		}

		private bool _t367_modolectura;
		public bool t367_modolectura
		{
			get {return _t367_modolectura;}
			set { _t367_modolectura = value ;}
		}

		private bool _t367_tipogestion;
		public bool t367_tipogestion
		{
			get {return _t367_tipogestion;}
			set { _t367_tipogestion = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}

		private DateTime _t367_fecha;
		public DateTime t367_fecha
		{
			get {return _t367_fecha;}
			set { _t367_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t367_fechamodif;
		public DateTime t367_fechamodif
		{
			get {return _t367_fechamodif;}
			set { _t367_fechamodif = value ;}
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

		public DOCUHE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T367_DOCUHE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t352_idhito, string t367_descripcion, string t367_weblink, string t367_nombrearchivo,
                                 Nullable<long> idContentServer, bool t367_privado, bool t367_modolectura, bool t367_tipogestion, 
                                 int t314_idusuario_autor)
		{
            //if (t367_archivo.Length == 0) t367_archivo = null;

            SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t352_idhito", SqlDbType.Int, 4);
			aParam[0].Value = t352_idhito;
			aParam[1] = new SqlParameter("@t367_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t367_descripcion;
			aParam[2] = new SqlParameter("@t367_weblink", SqlDbType.Text, 250);
			aParam[2].Value = t367_weblink;
			aParam[3] = new SqlParameter("@t367_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t367_nombrearchivo;

            //aParam[4] = new SqlParameter("@t367_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t367_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t367_privado", SqlDbType.Bit, 1);
			aParam[5].Value = t367_privado;
			aParam[6] = new SqlParameter("@t367_modolectura", SqlDbType.Bit, 1);
			aParam[6].Value = t367_modolectura;
			aParam[7] = new SqlParameter("@t367_tipogestion", SqlDbType.Bit, 1);
			aParam[7].Value = t367_tipogestion;
			aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[8].Value = t314_idusuario_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUHE_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUHE_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T367_DOCUHE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t367_iddocuhe, int t352_idhito, string t367_descripcion, string t367_weblink,
                                 string t367_nombrearchivo, Nullable<long> idContentServer, bool t367_privado, bool t367_modolectura, 
                                 bool t367_tipogestion, int t314_idusuario_modif)
		{
            //if (t367_archivo.Length == 0) t367_archivo = null;

            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t367_iddocuhe", SqlDbType.Int, 4);
			aParam[0].Value = t367_iddocuhe;
			aParam[1] = new SqlParameter("@t352_idhito", SqlDbType.Int, 4);
			aParam[1].Value = t352_idhito;
			aParam[2] = new SqlParameter("@t367_descripcion", SqlDbType.Text, 50);
			aParam[2].Value = t367_descripcion;
			aParam[3] = new SqlParameter("@t367_weblink", SqlDbType.Text, 250);
			aParam[3].Value = t367_weblink;
			aParam[4] = new SqlParameter("@t367_nombrearchivo", SqlDbType.Text, 250);
			aParam[4].Value = t367_nombrearchivo;

            //aParam[5] = new SqlParameter("@t367_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t367_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t367_privado", SqlDbType.Bit, 1);
			aParam[6].Value = t367_privado;
			aParam[7] = new SqlParameter("@t367_modolectura", SqlDbType.Bit, 1);
			aParam[7].Value = t367_modolectura;
			aParam[8] = new SqlParameter("@t367_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t367_tipogestion;
            aParam[9] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[9].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUHE_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUHE_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T367_DOCUHE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t367_iddocuhe)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t367_iddocuhe", SqlDbType.Int, 4);
			aParam[0].Value = t367_iddocuhe;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUHE_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUHE_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T367_DOCUHE,
		/// y devuelve una instancia u objeto del tipo DOCUHE
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCUHE Select(SqlTransaction tr, int t367_iddocuhe)//, bool bTraerArchivo) 
		{
			DOCUHE o = new DOCUHE();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t367_iddocuhe", SqlDbType.Int, 4);
			aParam[0].Value = t367_iddocuhe;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUHE_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUHE_S", aParam);

			if (dr.Read())
			{
				if (dr["t367_iddocuhe"] != DBNull.Value)
					o.t367_iddocuhe = (int)dr["t367_iddocuhe"];
				if (dr["t352_idhito"] != DBNull.Value)
					o.t352_idhito = (int)dr["t352_idhito"];
				if (dr["t367_descripcion"] != DBNull.Value)
					o.t367_descripcion = (string)dr["t367_descripcion"];
				if (dr["t367_weblink"] != DBNull.Value)
					o.t367_weblink = (string)dr["t367_weblink"];
				if (dr["t367_nombrearchivo"] != DBNull.Value)
					o.t367_nombrearchivo = (string)dr["t367_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t367_archivo = (byte[])dr["t367_archivo"];
                //}
                if (dr["t367_privado"] != DBNull.Value)
					o.t367_privado = (bool)dr["t367_privado"];
				if (dr["t367_modolectura"] != DBNull.Value)
					o.t367_modolectura = (bool)dr["t367_modolectura"];
				if (dr["t367_tipogestion"] != DBNull.Value)
					o.t367_tipogestion = (bool)dr["t367_tipogestion"];
				if (dr["t314_idusuario_autor"] != DBNull.Value)
					o.t314_idusuario_autor = (int)dr["t314_idusuario_autor"];
				if (dr["t367_fecha"] != DBNull.Value)
					o.t367_fecha = (DateTime)dr["t367_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = (int)dr["t314_idusuario_modif"];
				if (dr["t367_fechamodif"] != DBNull.Value)
					o.t367_fechamodif = (DateTime)dr["t367_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUHE"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T367_DOCUHE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t352_idhito, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t352_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t352_idhito;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUHE_C2", aParam);
        }
        public static SqlDataReader Catalogo3(int t352_idhito)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t352_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t352_idhito;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUHE_C3", aParam);
        }

		#endregion
	}
}
