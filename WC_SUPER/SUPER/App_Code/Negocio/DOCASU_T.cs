using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : DOCASU_T
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T602_DOCASU_T
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 12:40:36	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCASU_T
	{

		#region Propiedades y Atributos

		private int _t600_idasunto;
		public int t600_idasunto
		{
			get {return _t600_idasunto;}
			set { _t600_idasunto = value ;}
		}

		private byte[] _t602_archivo;
		public byte[] t602_archivo
		{
			get {return _t602_archivo;}
			set { _t602_archivo = value ;}
		}

		private int _t602_autor;
		public int t602_autor
		{
			get {return _t602_autor;}
			set { _t602_autor = value ;}
		}

		private int _t602_autormodif;
		public int t602_autormodif
		{
			get {return _t602_autormodif;}
			set { _t602_autormodif = value ;}
		}

		private string _t602_descripcion;
		public string t602_descripcion
		{
			get {return _t602_descripcion;}
			set { _t602_descripcion = value ;}
		}

		private DateTime _t602_fecha;
		public DateTime t602_fecha
		{
			get {return _t602_fecha;}
			set { _t602_fecha = value ;}
		}

		private DateTime _t602_fechamodif;
		public DateTime t602_fechamodif
		{
			get {return _t602_fechamodif;}
			set { _t602_fechamodif = value ;}
		}

		private int _t602_iddocasu;
		public int t602_iddocasu
		{
			get {return _t602_iddocasu;}
			set { _t602_iddocasu = value ;}
		}

		private bool _t602_modolectura;
		public bool t602_modolectura
		{
			get {return _t602_modolectura;}
			set { _t602_modolectura = value ;}
		}

		private string _t602_nombrearchivo;
		public string t602_nombrearchivo
		{
			get {return _t602_nombrearchivo;}
			set { _t602_nombrearchivo = value ;}
		}

		private bool _t602_privado;
		public bool t602_privado
		{
			get {return _t602_privado;}
			set { _t602_privado = value ;}
		}

		private bool _t602_tipogestion;
		public bool t602_tipogestion
		{
			get {return _t602_tipogestion;}
			set { _t602_tipogestion = value ;}
		}

		private string _t602_weblink;
		public string t602_weblink
		{
			get {return _t602_weblink;}
			set { _t602_weblink = value ;}
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

		public DOCASU_T() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T602_DOCASU_T.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t600_idasunto, Nullable<long> idContentServer, int t602_autor,  
                                string t602_descripcion , bool t602_modolectura , 
                                string t602_nombrearchivo , bool t602_privado , bool t602_tipogestion , string t602_weblink)
		{
            //if (t602_archivo.Length == 0) t602_archivo = null; 
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t602_autor", SqlDbType.Int, 4);
			aParam[2].Value = t602_autor;
			aParam[3] = new SqlParameter("@t602_autormodif", SqlDbType.Int, 4);
            aParam[3].Value = t602_autor;
			aParam[4] = new SqlParameter("@t602_descripcion", SqlDbType.Text, 50);
			aParam[4].Value = t602_descripcion;
			aParam[5] = new SqlParameter("@t602_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t602_modolectura;
			aParam[6] = new SqlParameter("@t602_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t602_nombrearchivo;
			aParam[7] = new SqlParameter("@t602_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t602_privado;
			aParam[8] = new SqlParameter("@t602_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t602_tipogestion;
			aParam[9] = new SqlParameter("@t602_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t602_weblink;

            //aParam[10] = new SqlParameter("@t602_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t602_archivo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			int returnValue;
			if (tr == null)
				returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCASU_T_I", aParam));
			else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCASU_T_I", aParam));
			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T602_DOCASU_T.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t600_idasunto, Nullable<long> idContentServer, int t602_autormodif, 
                                string t602_descripcion, int t602_iddocasu, 
                                bool t602_modolectura, string t602_nombrearchivo, bool t602_privado, bool t602_tipogestion, 
                                string t602_weblink)
		{
            //if (t602_archivo.Length == 0) t602_archivo = null;
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t602_autormodif", SqlDbType.Int, 4);
			aParam[2].Value = t602_autormodif;
			aParam[3] = new SqlParameter("@t602_descripcion", SqlDbType.Text, 50);
			aParam[3].Value = t602_descripcion;
			aParam[4] = new SqlParameter("@t602_iddocasu", SqlDbType.Int, 4);
			aParam[4].Value = t602_iddocasu;
			aParam[5] = new SqlParameter("@t602_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t602_modolectura;
			aParam[6] = new SqlParameter("@t602_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t602_nombrearchivo;
			aParam[7] = new SqlParameter("@t602_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t602_privado;
			aParam[8] = new SqlParameter("@t602_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t602_tipogestion;
			aParam[9] = new SqlParameter("@t602_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t602_weblink;
            //aParam[10] = new SqlParameter("@t602_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t602_archivo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_DOCASU_T_U", aParam);
			else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCASU_T_U", aParam);

			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T602_DOCASU_T a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t602_iddocasu)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t602_iddocasu", SqlDbType.Int, 4);
			aParam[0].Value = t602_iddocasu;

			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_DOCASU_T_D", aParam);
			else
				returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCASU_T_D", aParam);

			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T602_DOCASU_T,
		/// y devuelve una instancia u objeto del tipo DOCASU
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static DOCASU_T Select(SqlTransaction tr, int t602_iddocasu)//, bool bTraerArchivo) 
		{
			DOCASU_T o = new DOCASU_T();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t602_iddocasu", SqlDbType.Int, 4);
			aParam[0].Value = t602_iddocasu;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCASU_T_S", aParam);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCASU_T_S", aParam);

			if (dr.Read())
			{
				if (dr["t600_idasunto"] != DBNull.Value)
					o.t600_idasunto = (int)dr["t600_idasunto"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t602_archivo = (byte[])dr["t602_archivo"];
                //}
                if (dr["t602_autor"] != DBNull.Value)
					o.t602_autor = (int)dr["t602_autor"];
				if (dr["t602_autormodif"] != DBNull.Value)
					o.t602_autormodif = (int)dr["t602_autormodif"];
				if (dr["t602_descripcion"] != DBNull.Value)
					o.t602_descripcion = (string)dr["t602_descripcion"];
				if (dr["t602_fecha"] != DBNull.Value)
					o.t602_fecha = (DateTime)dr["t602_fecha"];
				if (dr["t602_fechamodif"] != DBNull.Value)
					o.t602_fechamodif = (DateTime)dr["t602_fechamodif"];
				if (dr["t602_iddocasu"] != DBNull.Value)
					o.t602_iddocasu = (int)dr["t602_iddocasu"];
				if (dr["t602_modolectura"] != DBNull.Value)
					o.t602_modolectura = (bool)dr["t602_modolectura"];
				if (dr["t602_nombrearchivo"] != DBNull.Value)
					o.t602_nombrearchivo = (string)dr["t602_nombrearchivo"];
				if (dr["t602_privado"] != DBNull.Value)
					o.t602_privado = (bool)dr["t602_privado"];
				if (dr["t602_tipogestion"] != DBNull.Value)
					o.t602_tipogestion = (bool)dr["t602_tipogestion"];
				if (dr["t602_weblink"] != DBNull.Value)
					o.t602_weblink = (string)dr["t602_weblink"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCASU_T"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T602_DOCASU_T.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t600_idasunto, int t602_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;
            aParam[1] = new SqlParameter("@t602_autor", SqlDbType.Int, 4);
            aParam[1].Value = t602_autor;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCASU_T_C2", aParam);

			return dr;
		}

		#endregion
	}
}
