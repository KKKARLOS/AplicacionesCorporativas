using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCASU
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t386_DOCASU
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	16/11/2007 12:40:36	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCASU
	{

		#region Propiedades y Atributos

		private int _t382_idasunto;
		public int t382_idasunto
		{
			get {return _t382_idasunto;}
			set { _t382_idasunto = value ;}
		}

		private byte[] _t386_archivo;
		public byte[] t386_archivo
		{
			get {return _t386_archivo;}
			set { _t386_archivo = value ;}
		}

		private int _t386_autor;
		public int t386_autor
		{
			get {return _t386_autor;}
			set { _t386_autor = value ;}
		}

		private int _t386_autormodif;
		public int t386_autormodif
		{
			get {return _t386_autormodif;}
			set { _t386_autormodif = value ;}
		}

		private string _t386_descripcion;
		public string t386_descripcion
		{
			get {return _t386_descripcion;}
			set { _t386_descripcion = value ;}
		}

		private DateTime _t386_fecha;
		public DateTime t386_fecha
		{
			get {return _t386_fecha;}
			set { _t386_fecha = value ;}
		}

		private DateTime _t386_fechamodif;
		public DateTime t386_fechamodif
		{
			get {return _t386_fechamodif;}
			set { _t386_fechamodif = value ;}
		}

		private int _t386_iddocasu;
		public int t386_iddocasu
		{
			get {return _t386_iddocasu;}
			set { _t386_iddocasu = value ;}
		}

		private bool _t386_modolectura;
		public bool t386_modolectura
		{
			get {return _t386_modolectura;}
			set { _t386_modolectura = value ;}
		}

		private string _t386_nombrearchivo;
		public string t386_nombrearchivo
		{
			get {return _t386_nombrearchivo;}
			set { _t386_nombrearchivo = value ;}
		}

		private bool _t386_privado;
		public bool t386_privado
		{
			get {return _t386_privado;}
			set { _t386_privado = value ;}
		}

		private bool _t386_tipogestion;
		public bool t386_tipogestion
		{
			get {return _t386_tipogestion;}
			set { _t386_tipogestion = value ;}
		}

		private string _t386_weblink;
		public string t386_weblink
		{
			get {return _t386_weblink;}
			set { _t386_weblink = value ;}
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

		public DOCASU() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t386_DOCASU.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t382_idasunto, Nullable<long> idContentServer, int t386_autor,  
                                string t386_descripcion , bool t386_modolectura , 
                                string t386_nombrearchivo , bool t386_privado , bool t386_tipogestion , string t386_weblink)
		{
            //if (t386_archivo.Length == 0) t386_archivo = null; 
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t386_autor", SqlDbType.Int, 4);
			aParam[2].Value = t386_autor;
			aParam[3] = new SqlParameter("@t386_autormodif", SqlDbType.Int, 4);
            aParam[3].Value = t386_autor;
			aParam[4] = new SqlParameter("@t386_descripcion", SqlDbType.Text, 50);
			aParam[4].Value = t386_descripcion;
			aParam[5] = new SqlParameter("@t386_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t386_modolectura;
			aParam[6] = new SqlParameter("@t386_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t386_nombrearchivo;
			aParam[7] = new SqlParameter("@t386_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t386_privado;
			aParam[8] = new SqlParameter("@t386_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t386_tipogestion;
			aParam[9] = new SqlParameter("@t386_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t386_weblink;


            //aParam[10] = new SqlParameter("@t386_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t386_archivo;
            // Ejecuta la query y devuelve el valor del nuevo Identity.
			int returnValue;
			if (tr == null)
				returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCASU_I", aParam));
			else
				returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCASU_I", aParam));
			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t386_DOCASU.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t382_idasunto, Nullable<long> idContentServer, int t386_autormodif, 
                                string t386_descripcion, int t386_iddocasu, bool t386_modolectura, string t386_nombrearchivo, 
                                bool t386_privado, bool t386_tipogestion, string t386_weblink)
		{
            //if (t386_archivo.Length == 0) t386_archivo = null;
            
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t386_autormodif", SqlDbType.Int, 4);
			aParam[2].Value = t386_autormodif;
			aParam[3] = new SqlParameter("@t386_descripcion", SqlDbType.Text, 50);
			aParam[3].Value = t386_descripcion;
			aParam[4] = new SqlParameter("@t386_iddocasu", SqlDbType.Int, 4);
			aParam[4].Value = t386_iddocasu;
			aParam[5] = new SqlParameter("@t386_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t386_modolectura;
			aParam[6] = new SqlParameter("@t386_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t386_nombrearchivo;
			aParam[7] = new SqlParameter("@t386_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t386_privado;
			aParam[8] = new SqlParameter("@t386_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t386_tipogestion;
			aParam[9] = new SqlParameter("@t386_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t386_weblink;

            //aParam[10] = new SqlParameter("@t386_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t386_archivo;
            
            // Ejecuta la query y devuelve el numero de registros modificados.
			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_DOCASU_U", aParam);
			else
				returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCASU_U", aParam);

			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t386_DOCASU a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t386_iddocasu)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t386_iddocasu", SqlDbType.Int, 4);
			aParam[0].Value = t386_iddocasu;

			int returnValue;
			if (tr == null)
				returnValue = SqlHelper.ExecuteNonQuery("SUP_DOCASU_D", aParam);
			else
				returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCASU_D", aParam);

			return returnValue;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t386_DOCASU,
		/// y devuelve una instancia u objeto del tipo DOCASU
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static DOCASU Select(SqlTransaction tr, int t386_iddocasu)//, bool bTraerArchivo) 
		{
			DOCASU o = new DOCASU();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t386_iddocasu", SqlDbType.Int, 4);
			aParam[0].Value = t386_iddocasu;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCASU_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCASU_S", aParam);

			if (dr.Read())
			{
				if (dr["t382_idasunto"] != DBNull.Value)
					o.t382_idasunto = (int)dr["t382_idasunto"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t386_archivo = (byte[])dr["t386_archivo"];
                //}
                if (dr["t386_autor"] != DBNull.Value)
					o.t386_autor = (int)dr["t386_autor"];
				if (dr["t386_autormodif"] != DBNull.Value)
					o.t386_autormodif = (int)dr["t386_autormodif"];
				if (dr["t386_descripcion"] != DBNull.Value)
					o.t386_descripcion = (string)dr["t386_descripcion"];
				if (dr["t386_fecha"] != DBNull.Value)
					o.t386_fecha = (DateTime)dr["t386_fecha"];
				if (dr["t386_fechamodif"] != DBNull.Value)
					o.t386_fechamodif = (DateTime)dr["t386_fechamodif"];
				if (dr["t386_iddocasu"] != DBNull.Value)
					o.t386_iddocasu = (int)dr["t386_iddocasu"];
				if (dr["t386_modolectura"] != DBNull.Value)
					o.t386_modolectura = (bool)dr["t386_modolectura"];
				if (dr["t386_nombrearchivo"] != DBNull.Value)
					o.t386_nombrearchivo = (string)dr["t386_nombrearchivo"];
				if (dr["t386_privado"] != DBNull.Value)
					o.t386_privado = (bool)dr["t386_privado"];
				if (dr["t386_tipogestion"] != DBNull.Value)
					o.t386_tipogestion = (bool)dr["t386_tipogestion"];
				if (dr["t386_weblink"] != DBNull.Value)
					o.t386_weblink = (string)dr["t386_weblink"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCASU"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla t386_DOCASU.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:40:36
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t382_idasunto, int t386_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;
			aParam[1] = new SqlParameter("@t386_autor", SqlDbType.Int, 4);
			aParam[1].Value = t386_autor;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCASU_C2", aParam);

			return dr;
		}

		#endregion
	}
}
