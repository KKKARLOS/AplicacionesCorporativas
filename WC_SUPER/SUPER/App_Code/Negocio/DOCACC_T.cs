using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : DOCACC_T
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T603_DOCACC_T
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 12:39:48	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCACC_T
	{

		#region Propiedades y Atributos

		private int _t601_idaccion;
		public int t601_idaccion
		{
			get {return _t601_idaccion;}
			set { _t601_idaccion = value ;}
		}

		private byte[] _t603_archivo;
		public byte[] t603_archivo
		{
			get {return _t603_archivo;}
			set { _t603_archivo = value ;}
		}

		private int _t603_autor;
		public int t603_autor
		{
			get {return _t603_autor;}
			set { _t603_autor = value ;}
		}

		private int _t603_autormodif;
		public int t603_autormodif
		{
			get {return _t603_autormodif;}
			set { _t603_autormodif = value ;}
		}

		private string _t603_descripcion;
		public string t603_descripcion
		{
			get {return _t603_descripcion;}
			set { _t603_descripcion = value ;}
		}

		private DateTime _t603_fecha;
		public DateTime t603_fecha
		{
			get {return _t603_fecha;}
			set { _t603_fecha = value ;}
		}

		private DateTime _t603_fechamodif;
		public DateTime t603_fechamodif
		{
			get {return _t603_fechamodif;}
			set { _t603_fechamodif = value ;}
		}

		private int _t603_iddocacc;
		public int t603_iddocacc
		{
			get {return _t603_iddocacc;}
			set { _t603_iddocacc = value ;}
		}

		private bool _t603_modolectura;
		public bool t603_modolectura
		{
			get {return _t603_modolectura;}
			set { _t603_modolectura = value ;}
		}

		private string _t603_nombrearchivo;
		public string t603_nombrearchivo
		{
			get {return _t603_nombrearchivo;}
			set { _t603_nombrearchivo = value ;}
		}

		private bool _t603_privado;
		public bool t603_privado
		{
			get {return _t603_privado;}
			set { _t603_privado = value ;}
		}

		private bool _t603_tipogestion;
		public bool t603_tipogestion
		{
			get {return _t603_tipogestion;}
			set { _t603_tipogestion = value ;}
		}

		private string _t603_weblink;
		public string t603_weblink
		{
			get {return _t603_weblink;}
			set { _t603_weblink = value ;}
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

		public DOCACC_T() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T603_DOCACC_T.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t601_idaccion, Nullable<long> idContentServer, int t603_autor, string t603_descripcion, 
                            bool t603_modolectura, string t603_nombrearchivo, bool t603_privado, bool t603_tipogestion, string t603_weblink)
		{
            //if (t603_archivo.Length == 0) t603_archivo = null; 
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t601_idaccion;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t603_autor", SqlDbType.Int, 4);
			aParam[2].Value = t603_autor;
			aParam[3] = new SqlParameter("@t603_autormodif", SqlDbType.Int, 4);
            aParam[3].Value = t603_autor;
			aParam[4] = new SqlParameter("@t603_descripcion", SqlDbType.Text, 50);
			aParam[4].Value = t603_descripcion;
			aParam[5] = new SqlParameter("@t603_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t603_modolectura;
			aParam[6] = new SqlParameter("@t603_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t603_nombrearchivo;
			aParam[7] = new SqlParameter("@t603_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t603_privado;
			aParam[8] = new SqlParameter("@t603_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t603_tipogestion;
			aParam[9] = new SqlParameter("@t603_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t603_weblink;

            //aParam[10] = new SqlParameter("@t603_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t603_archivo;


			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCACC_T_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCACC_T_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T603_DOCACC_T.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t601_idaccion, Nullable<long> idContentServer, int t603_autormodif, string t603_descripcion, 
                                 int t603_iddocacc, bool t603_modolectura, string t603_nombrearchivo, bool t603_privado, 
                                 bool t603_tipogestion, string t603_weblink)
		{
            //if (t603_archivo.Length == 0) t603_archivo = null;
            SqlParameter[] aParam = new SqlParameter[11];
			aParam[0] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t601_idaccion;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t603_autormodif", SqlDbType.Int, 4);
			aParam[2].Value = t603_autormodif;
			aParam[3] = new SqlParameter("@t603_descripcion", SqlDbType.Text, 50);
			aParam[3].Value = t603_descripcion;
			aParam[4] = new SqlParameter("@t603_iddocacc", SqlDbType.Int, 4);
			aParam[4].Value = t603_iddocacc;
			aParam[5] = new SqlParameter("@t603_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t603_modolectura;
			aParam[6] = new SqlParameter("@t603_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t603_nombrearchivo;
			aParam[7] = new SqlParameter("@t603_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t603_privado;
			aParam[8] = new SqlParameter("@t603_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t603_tipogestion;
			aParam[9] = new SqlParameter("@t603_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t603_weblink;

            //aParam[10] = new SqlParameter("@t603_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t603_archivo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCACC_T_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCACC_T_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T603_DOCACC_T a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t603_iddocacc)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t603_iddocacc", SqlDbType.Int, 4);
			aParam[0].Value = t603_iddocacc;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCACC_T_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCACC_T_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T603_DOCACC_T,
		/// y devuelve una instancia u objeto del tipo DOCACC
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCACC_T Select(SqlTransaction tr, int t603_iddocacc)//, bool bTraerArchivo) 
		{
			DOCACC_T o = new DOCACC_T();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t603_iddocacc", SqlDbType.Int, 4);
			aParam[0].Value = t603_iddocacc;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCACC_T_S", aParam);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCACC_T_S", aParam);

			if (dr.Read())
			{
				if (dr["t601_idaccion"] != DBNull.Value)
					o.t601_idaccion = (int)dr["t601_idaccion"];
                //El archivo lo obtenemos de Atenea
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t603_archivo = (byte[])dr["t603_archivo"];
                //}
                if (dr["t603_autor"] != DBNull.Value)
					o.t603_autor = (int)dr["t603_autor"];
				if (dr["t603_autormodif"] != DBNull.Value)
					o.t603_autormodif = (int)dr["t603_autormodif"];
				if (dr["t603_descripcion"] != DBNull.Value)
					o.t603_descripcion = (string)dr["t603_descripcion"];
				if (dr["t603_fecha"] != DBNull.Value)
					o.t603_fecha = (DateTime)dr["t603_fecha"];
				if (dr["t603_fechamodif"] != DBNull.Value)
					o.t603_fechamodif = (DateTime)dr["t603_fechamodif"];
				if (dr["t603_iddocacc"] != DBNull.Value)
					o.t603_iddocacc = (int)dr["t603_iddocacc"];
				if (dr["t603_modolectura"] != DBNull.Value)
					o.t603_modolectura = (bool)dr["t603_modolectura"];
				if (dr["t603_nombrearchivo"] != DBNull.Value)
					o.t603_nombrearchivo = (string)dr["t603_nombrearchivo"];
				if (dr["t603_privado"] != DBNull.Value)
					o.t603_privado = (bool)dr["t603_privado"];
				if (dr["t603_tipogestion"] != DBNull.Value)
					o.t603_tipogestion = (bool)dr["t603_tipogestion"];
				if (dr["t603_weblink"] != DBNull.Value)
					o.t603_weblink = (string)dr["t603_weblink"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCACC"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T603_DOCACC_T.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t601_idaccion, int t603_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t601_idaccion;
			aParam[1] = new SqlParameter("@t603_autor", SqlDbType.Int, 4);
			aParam[1].Value = t603_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCACC_T_C2", aParam);
		}

		#endregion
	}
}
