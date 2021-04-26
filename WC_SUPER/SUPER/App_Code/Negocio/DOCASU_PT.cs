using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : DOCASU_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T411_DOCASU_PT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 12:40:36	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCASU_PT
	{

		#region Propiedades y Atributos

		private int _t409_idasunto;
		public int t409_idasunto
		{
			get {return _t409_idasunto;}
			set { _t409_idasunto = value ;}
		}

		private byte[] _t411_archivo;
		public byte[] t411_archivo
		{
			get {return _t411_archivo;}
			set { _t411_archivo = value ;}
		}

		private int _t411_autor;
		public int t411_autor
		{
			get {return _t411_autor;}
			set { _t411_autor = value ;}
		}

		private int _t411_autormodif;
		public int t411_autormodif
		{
			get {return _t411_autormodif;}
			set { _t411_autormodif = value ;}
		}

		private string _t411_descripcion;
		public string t411_descripcion
		{
			get {return _t411_descripcion;}
			set { _t411_descripcion = value ;}
		}

		private DateTime _t411_fecha;
		public DateTime t411_fecha
		{
			get {return _t411_fecha;}
			set { _t411_fecha = value ;}
		}

		private DateTime _t411_fechamodif;
		public DateTime t411_fechamodif
		{
			get {return _t411_fechamodif;}
			set { _t411_fechamodif = value ;}
		}

		private int _t411_iddocasu;
		public int t411_iddocasu
		{
			get {return _t411_iddocasu;}
			set { _t411_iddocasu = value ;}
		}

		private bool _t411_modolectura;
		public bool t411_modolectura
		{
			get {return _t411_modolectura;}
			set { _t411_modolectura = value ;}
		}

		private string _t411_nombrearchivo;
		public string t411_nombrearchivo
		{
			get {return _t411_nombrearchivo;}
			set { _t411_nombrearchivo = value ;}
		}

		private bool _t411_privado;
		public bool t411_privado
		{
			get {return _t411_privado;}
			set { _t411_privado = value ;}
		}

		private bool _t411_tipogestion;
		public bool t411_tipogestion
		{
			get {return _t411_tipogestion;}
			set { _t411_tipogestion = value ;}
		}

		private string _t411_weblink;
		public string t411_weblink
		{
			get {return _t411_weblink;}
			set { _t411_weblink = value ;}
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

		public DOCASU_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T411_DOCASU_PT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t409_idasunto, Nullable<long> idContentServer, int t411_autor,  
                                string t411_descripcion , bool t411_modolectura , 
                                string t411_nombrearchivo , bool t411_privado , bool t411_tipogestion , string t411_weblink)
		{
            //if (t411_archivo.Length == 0) t411_archivo = null; 
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t411_autor", SqlDbType.Int, 4);
			aParam[2].Value = t411_autor;
			aParam[3] = new SqlParameter("@t411_autormodif", SqlDbType.Int, 4);
            aParam[3].Value = t411_autor;
			aParam[4] = new SqlParameter("@t411_descripcion", SqlDbType.Text, 50);
			aParam[4].Value = t411_descripcion;
			aParam[5] = new SqlParameter("@t411_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t411_modolectura;
			aParam[6] = new SqlParameter("@t411_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t411_nombrearchivo;
			aParam[7] = new SqlParameter("@t411_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t411_privado;
			aParam[8] = new SqlParameter("@t411_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t411_tipogestion;
			aParam[9] = new SqlParameter("@t411_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t411_weblink;

            //aParam[10] = new SqlParameter("@t411_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t411_archivo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			int returnValue;
			if (tr == null)
				returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCASU_PT_I", aParam));
			else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCASU_PT_I", aParam));
			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T411_DOCASU_PT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t409_idasunto, Nullable<long> idContentServer, int t411_autormodif, 
                                string t411_descripcion, int t411_iddocasu, 
                                bool t411_modolectura, string t411_nombrearchivo, bool t411_privado, bool t411_tipogestion, 
                                string t411_weblink)
		{
            //if (t411_archivo.Length == 0) t411_archivo = null;			
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t411_autormodif", SqlDbType.Int, 4);
			aParam[2].Value = t411_autormodif;
			aParam[3] = new SqlParameter("@t411_descripcion", SqlDbType.Text, 50);
			aParam[3].Value = t411_descripcion;
			aParam[4] = new SqlParameter("@t411_iddocasu", SqlDbType.Int, 4);
			aParam[4].Value = t411_iddocasu;
			aParam[5] = new SqlParameter("@t411_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t411_modolectura;
			aParam[6] = new SqlParameter("@t411_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t411_nombrearchivo;
			aParam[7] = new SqlParameter("@t411_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t411_privado;
			aParam[8] = new SqlParameter("@t411_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t411_tipogestion;
			aParam[9] = new SqlParameter("@t411_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t411_weblink;

            //aParam[10] = new SqlParameter("@t411_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t411_archivo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_DOCASU_PT_U", aParam);
			else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCASU_PT_U", aParam);

			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T411_DOCASU_PT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t411_iddocasu)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t411_iddocasu", SqlDbType.Int, 4);
			aParam[0].Value = t411_iddocasu;

			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_DOCASU_PT_D", aParam);
			else
				returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCASU_PT_D", aParam);

			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T411_DOCASU_PT,
		/// y devuelve una instancia u objeto del tipo DOCASU
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static DOCASU_PT Select(SqlTransaction tr, int t411_iddocasu)//, bool bTraerArchivo) 
		{
			DOCASU_PT o = new DOCASU_PT();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t411_iddocasu", SqlDbType.Int, 4);
			aParam[0].Value = t411_iddocasu;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCASU_PT_S", aParam);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCASU_PT_S", aParam);

			if (dr.Read())
			{
				if (dr["t409_idasunto"] != DBNull.Value)
					o.t409_idasunto = (int)dr["t409_idasunto"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t411_archivo = (byte[])dr["t411_archivo"];
                //}
                if (dr["t411_autor"] != DBNull.Value)
					o.t411_autor = (int)dr["t411_autor"];
				if (dr["t411_autormodif"] != DBNull.Value)
					o.t411_autormodif = (int)dr["t411_autormodif"];
				if (dr["t411_descripcion"] != DBNull.Value)
					o.t411_descripcion = (string)dr["t411_descripcion"];
				if (dr["t411_fecha"] != DBNull.Value)
					o.t411_fecha = (DateTime)dr["t411_fecha"];
				if (dr["t411_fechamodif"] != DBNull.Value)
					o.t411_fechamodif = (DateTime)dr["t411_fechamodif"];
				if (dr["t411_iddocasu"] != DBNull.Value)
					o.t411_iddocasu = (int)dr["t411_iddocasu"];
				if (dr["t411_modolectura"] != DBNull.Value)
					o.t411_modolectura = (bool)dr["t411_modolectura"];
				if (dr["t411_nombrearchivo"] != DBNull.Value)
					o.t411_nombrearchivo = (string)dr["t411_nombrearchivo"];
				if (dr["t411_privado"] != DBNull.Value)
					o.t411_privado = (bool)dr["t411_privado"];
				if (dr["t411_tipogestion"] != DBNull.Value)
					o.t411_tipogestion = (bool)dr["t411_tipogestion"];
				if (dr["t411_weblink"] != DBNull.Value)
					o.t411_weblink = (string)dr["t411_weblink"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCASU_PT"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T411_DOCASU_PT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t409_idasunto, int t411_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;
			aParam[1] = new SqlParameter("@t411_autor", SqlDbType.Int, 4);
			aParam[1].Value = t411_autor;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCASU_PT_C2", aParam);

			return dr;
		}

        public static int UpdateIdDoc(SqlTransaction tr, int t411_iddocasu, long t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t411_iddocasu", SqlDbType.Int, 4, t411_iddocasu),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCASUPT_DOC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCASUPT_DOC_U", aParam);
        }
        #endregion
	}
}
