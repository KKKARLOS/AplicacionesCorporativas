using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : DOCACC_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T412_DOCACC_PT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 12:39:48	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCACC_PT
	{

		#region Propiedades y Atributos

		private int _t410_idaccion;
		public int t410_idaccion
		{
			get {return _t410_idaccion;}
			set { _t410_idaccion = value ;}
		}

		private byte[] _t412_archivo;
		public byte[] t412_archivo
		{
			get {return _t412_archivo;}
			set { _t412_archivo = value ;}
		}

		private int _t412_autor;
		public int t412_autor
		{
			get {return _t412_autor;}
			set { _t412_autor = value ;}
		}

		private int _t412_autormodif;
		public int t412_autormodif
		{
			get {return _t412_autormodif;}
			set { _t412_autormodif = value ;}
		}

		private string _t412_descripcion;
		public string t412_descripcion
		{
			get {return _t412_descripcion;}
			set { _t412_descripcion = value ;}
		}

		private DateTime _t412_fecha;
		public DateTime t412_fecha
		{
			get {return _t412_fecha;}
			set { _t412_fecha = value ;}
		}

		private DateTime _t412_fechamodif;
		public DateTime t412_fechamodif
		{
			get {return _t412_fechamodif;}
			set { _t412_fechamodif = value ;}
		}

		private int _t412_iddocacc;
		public int t412_iddocacc
		{
			get {return _t412_iddocacc;}
			set { _t412_iddocacc = value ;}
		}

		private bool _t412_modolectura;
		public bool t412_modolectura
		{
			get {return _t412_modolectura;}
			set { _t412_modolectura = value ;}
		}

		private string _t412_nombrearchivo;
		public string t412_nombrearchivo
		{
			get {return _t412_nombrearchivo;}
			set { _t412_nombrearchivo = value ;}
		}

		private bool _t412_privado;
		public bool t412_privado
		{
			get {return _t412_privado;}
			set { _t412_privado = value ;}
		}

		private bool _t412_tipogestion;
		public bool t412_tipogestion
		{
			get {return _t412_tipogestion;}
			set { _t412_tipogestion = value ;}
		}

		private string _t412_weblink;
		public string t412_weblink
		{
			get {return _t412_weblink;}
			set { _t412_weblink = value ;}
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

		public DOCACC_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T412_DOCACC_PT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t410_idaccion, Nullable<long> idContentServer, int t412_autor, string t412_descripcion, 
                            bool t412_modolectura, string t412_nombrearchivo, bool t412_privado, bool t412_tipogestion, string t412_weblink)
		{
            //if (t412_archivo.Length == 0) t412_archivo = null;
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t410_idaccion;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t412_autor", SqlDbType.Int, 4);
			aParam[2].Value = t412_autor;
			aParam[3] = new SqlParameter("@t412_autormodif", SqlDbType.Int, 4);
            aParam[3].Value = t412_autor;
			aParam[4] = new SqlParameter("@t412_descripcion", SqlDbType.Text, 50);
			aParam[4].Value = t412_descripcion;
			aParam[5] = new SqlParameter("@t412_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t412_modolectura;
			aParam[6] = new SqlParameter("@t412_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t412_nombrearchivo;
			aParam[7] = new SqlParameter("@t412_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t412_privado;
			aParam[8] = new SqlParameter("@t412_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t412_tipogestion;
			aParam[9] = new SqlParameter("@t412_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t412_weblink;

            //aParam[10] = new SqlParameter("@t412_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t412_archivo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCACC_PT_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCACC_PT_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T412_DOCACC_PT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t410_idaccion, Nullable<long> idContentServer, int t412_autormodif, 
                                 string t412_descripcion, int t412_iddocacc, bool t412_modolectura, string t412_nombrearchivo, 
                                 bool t412_privado, bool t412_tipogestion, string t412_weblink)
		{
            //if (t412_archivo.Length == 0) t412_archivo = null;
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t410_idaccion;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t412_autormodif", SqlDbType.Int, 4);
			aParam[2].Value = t412_autormodif;
			aParam[3] = new SqlParameter("@t412_descripcion", SqlDbType.Text, 50);
			aParam[3].Value = t412_descripcion;
			aParam[4] = new SqlParameter("@t412_iddocacc", SqlDbType.Int, 4);
			aParam[4].Value = t412_iddocacc;
			aParam[5] = new SqlParameter("@t412_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t412_modolectura;
			aParam[6] = new SqlParameter("@t412_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t412_nombrearchivo;
			aParam[7] = new SqlParameter("@t412_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t412_privado;
			aParam[8] = new SqlParameter("@t412_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t412_tipogestion;
			aParam[9] = new SqlParameter("@t412_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t412_weblink;

            //aParam[10] = new SqlParameter("@t412_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t412_archivo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCACC_PT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCACC_PT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T412_DOCACC_PT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t412_iddocacc)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t412_iddocacc", SqlDbType.Int, 4);
			aParam[0].Value = t412_iddocacc;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCACC_PT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCACC_PT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T412_DOCACC_PT,
		/// y devuelve una instancia u objeto del tipo DOCACC
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCACC_PT Select(SqlTransaction tr, int t412_iddocacc)//, bool bTraerArchivo) 
		{
			DOCACC_PT o = new DOCACC_PT();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t412_iddocacc", SqlDbType.Int, 4);
			aParam[0].Value = t412_iddocacc;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCACC_PT_S", aParam);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCACC_PT_S", aParam);

			if (dr.Read())
			{
				if (dr["t410_idaccion"] != DBNull.Value)
					o.t410_idaccion = (int)dr["t410_idaccion"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t412_archivo = (byte[])dr["t412_archivo"];
                //}
                if (dr["t412_autor"] != DBNull.Value)
					o.t412_autor = (int)dr["t412_autor"];
				if (dr["t412_autormodif"] != DBNull.Value)
					o.t412_autormodif = (int)dr["t412_autormodif"];
				if (dr["t412_descripcion"] != DBNull.Value)
					o.t412_descripcion = (string)dr["t412_descripcion"];
				if (dr["t412_fecha"] != DBNull.Value)
					o.t412_fecha = (DateTime)dr["t412_fecha"];
				if (dr["t412_fechamodif"] != DBNull.Value)
					o.t412_fechamodif = (DateTime)dr["t412_fechamodif"];
				if (dr["t412_iddocacc"] != DBNull.Value)
					o.t412_iddocacc = (int)dr["t412_iddocacc"];
				if (dr["t412_modolectura"] != DBNull.Value)
					o.t412_modolectura = (bool)dr["t412_modolectura"];
				if (dr["t412_nombrearchivo"] != DBNull.Value)
					o.t412_nombrearchivo = (string)dr["t412_nombrearchivo"];
				if (dr["t412_privado"] != DBNull.Value)
					o.t412_privado = (bool)dr["t412_privado"];
				if (dr["t412_tipogestion"] != DBNull.Value)
					o.t412_tipogestion = (bool)dr["t412_tipogestion"];
				if (dr["t412_weblink"] != DBNull.Value)
					o.t412_weblink = (string)dr["t412_weblink"];
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
		/// Obtiene un catálogo de registros de la tabla T412_DOCACC_PT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t410_idaccion,int t412_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t410_idaccion;
			aParam[1] = new SqlParameter("@t412_autor", SqlDbType.Int, 4);
			aParam[1].Value = t412_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCACC_PT_C2", aParam);
		}

		#endregion
	}
}
