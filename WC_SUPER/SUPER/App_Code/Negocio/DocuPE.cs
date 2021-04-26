using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCUPE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T368_DOCUPE
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCUPE
	{
		#region Propiedades y Atributos

		private int _t368_iddocupe;
		public int t368_iddocupe
		{
			get {return _t368_iddocupe;}
			set { _t368_iddocupe = value ;}
		}

		private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
		{
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
		}

		private string _t368_descripcion;
		public string t368_descripcion
		{
			get {return _t368_descripcion;}
			set { _t368_descripcion = value ;}
		}

		private string _t368_weblink;
		public string t368_weblink
		{
			get {return _t368_weblink;}
			set { _t368_weblink = value ;}
		}

		private string _t368_nombrearchivo;
		public string t368_nombrearchivo
		{
			get {return _t368_nombrearchivo;}
			set { _t368_nombrearchivo = value ;}
		}

		private byte[] _t368_archivo;
		public byte[] t368_archivo
		{
			get {return _t368_archivo;}
			set { _t368_archivo = value ;}
		}

		private bool _t368_privado;
		public bool t368_privado
		{
			get {return _t368_privado;}
			set { _t368_privado = value ;}
		}

		private bool _t368_modolectura;
		public bool t368_modolectura
		{
			get {return _t368_modolectura;}
			set { _t368_modolectura = value ;}
		}

		private bool _t368_tipogestion;
		public bool t368_tipogestion
		{
			get {return _t368_tipogestion;}
			set { _t368_tipogestion = value ;}
		}

		private int _t314_idusuario_autor;
		public int t314_idusuario_autor
		{
			get {return _t314_idusuario_autor;}
			set { _t314_idusuario_autor = value ;}
		}

		private DateTime _t368_fecha;
		public DateTime t368_fecha
		{
			get {return _t368_fecha;}
			set { _t368_fecha = value ;}
		}

		private int _t314_idusuario_modif;
		public int t314_idusuario_modif
		{
			get {return _t314_idusuario_modif;}
			set { _t314_idusuario_modif = value ;}
		}

		private DateTime _t368_fechamodif;
		public DateTime t368_fechamodif
		{
			get {return _t368_fechamodif;}
			set { _t368_fechamodif = value ;}
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

		public DOCUPE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T368_DOCUPE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t305_idproyectosubnodo, string t368_descripcion, string t368_weblink,
                                 string t368_nombrearchivo, Nullable<long> idContentServer, bool t368_privado, bool t368_modolectura, 
                                 bool t368_tipogestion, int t314_idusuario_autor)
		{
            //if (t368_archivo.Length == 0) t368_archivo = null;

            SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t368_descripcion", SqlDbType.Text, 50);
			aParam[1].Value = t368_descripcion;
			aParam[2] = new SqlParameter("@t368_weblink", SqlDbType.Text, 250);
			aParam[2].Value = t368_weblink;
			aParam[3] = new SqlParameter("@t368_nombrearchivo", SqlDbType.Text, 250);
			aParam[3].Value = t368_nombrearchivo;

            //aParam[4] = new SqlParameter("@t368_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t368_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t368_privado", SqlDbType.Bit, 1);
			aParam[5].Value = t368_privado;
			aParam[6] = new SqlParameter("@t368_modolectura", SqlDbType.Bit, 1);
			aParam[6].Value = t368_modolectura;
			aParam[7] = new SqlParameter("@t368_tipogestion", SqlDbType.Bit, 1);
			aParam[7].Value = t368_tipogestion;
			aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[8].Value = t314_idusuario_autor;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUPE_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUPE_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T368_DOCUPE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t368_iddocupe, int t305_idproyectosubnodo, string t368_descripcion, string t368_weblink,
                                 string t368_nombrearchivo, Nullable<long> idContentServer, bool t368_privado, bool t368_modolectura, 
                                 bool t368_tipogestion, int t314_idusuario_modif)
		{
            //if (t368_archivo.Length == 0) t368_archivo = null;

            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t368_iddocupe", SqlDbType.Int, 4);
			aParam[0].Value = t368_iddocupe;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
			aParam[2] = new SqlParameter("@t368_descripcion", SqlDbType.Text, 50);
			aParam[2].Value = t368_descripcion;
			aParam[3] = new SqlParameter("@t368_weblink", SqlDbType.Text, 250);
			aParam[3].Value = t368_weblink;
			aParam[4] = new SqlParameter("@t368_nombrearchivo", SqlDbType.Text, 250);
			aParam[4].Value = t368_nombrearchivo;

            //aParam[5] = new SqlParameter("@t368_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t368_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t368_privado", SqlDbType.Bit, 1);
			aParam[6].Value = t368_privado;
			aParam[7] = new SqlParameter("@t368_modolectura", SqlDbType.Bit, 1);
			aParam[7].Value = t368_modolectura;
			aParam[8] = new SqlParameter("@t368_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t368_tipogestion;
            aParam[9] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[9].Value = t314_idusuario_modif;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUPE_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUPE_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T368_DOCUPE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t368_iddocupe)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t368_iddocupe", SqlDbType.Int, 4);
			aParam[0].Value = t368_iddocupe;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUPE_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUPE_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T368_DOCUPE,
		/// y devuelve una instancia u objeto del tipo DOCUPE
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCUPE Select(SqlTransaction tr, int t368_iddocupe)//, bool bTraerArchivo) 
		{
			DOCUPE o = new DOCUPE();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t368_iddocupe", SqlDbType.Int, 4);
			aParam[0].Value = t368_iddocupe;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUPE_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUPE_S", aParam);

			if (dr.Read())
			{
				if (dr["t368_iddocupe"] != DBNull.Value)
					o.t368_iddocupe = (int)dr["t368_iddocupe"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
				if (dr["t368_descripcion"] != DBNull.Value)
					o.t368_descripcion = (string)dr["t368_descripcion"];
				if (dr["t368_weblink"] != DBNull.Value)
					o.t368_weblink = (string)dr["t368_weblink"];
				if (dr["t368_nombrearchivo"] != DBNull.Value)
					o.t368_nombrearchivo = (string)dr["t368_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t368_archivo = (byte[])dr["t368_archivo"];
                //}
                if (dr["t368_privado"] != DBNull.Value)
					o.t368_privado = (bool)dr["t368_privado"];
				if (dr["t368_modolectura"] != DBNull.Value)
					o.t368_modolectura = (bool)dr["t368_modolectura"];
				if (dr["t368_tipogestion"] != DBNull.Value)
					o.t368_tipogestion = (bool)dr["t368_tipogestion"];
				if (dr["t314_idusuario_autor"] != DBNull.Value)
					o.t314_idusuario_autor = (int)dr["t314_idusuario_autor"];
				if (dr["t368_fecha"] != DBNull.Value)
					o.t368_fecha = (DateTime)dr["t368_fecha"];
				if (dr["t314_idusuario_modif"] != DBNull.Value)
					o.t314_idusuario_modif = (int)dr["t314_idusuario_modif"];
				if (dr["t368_fechamodif"] != DBNull.Value)
					o.t368_fechamodif = (DateTime)dr["t368_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUPE"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        /// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T368_DOCUPE.
        /// Si el documento es privado solo lo saca si el autor coincide con el usuario actual
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:15:25
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t305_idproyectosubnodo, int t314_idusuario_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario_autor;
            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUPE_C2", aParam);
		}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T368_DOCUPE visibles desde PST
        /// Si el documento es privado solo lo saca si el autor coincide con el usuario actual
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	25/04/2016 13:15:25
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo3(int t305_idproyectosubnodo, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;
            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUPE_C3", aParam);
        }

        //public static SqlDataReader Catalogo(Nullable<int> t368_iddocupe, Nullable<int> t305_idproyectosubnodo, string t368_descripcion, string t368_weblink, string t368_nombrearchivo, byte?[] t368_archivo, Nullable<bool> t368_privado, Nullable<bool> t368_modolectura, Nullable<bool> t368_tipogestion, Nullable<int> t314_idusuario_autor, Nullable<DateTime> t368_fecha, Nullable<int> t314_idusuario_modif, Nullable<DateTime> t368_fechamodif, byte nOrden, byte nAscDesc)
        //{
        //    SqlParameter[] aParam = new SqlParameter[15];
        //    aParam[0] = new SqlParameter("@t368_iddocupe", SqlDbType.Int, 4);
        //    aParam[0].Value = t368_iddocupe;
        //    aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
        //    aParam[1].Value = t305_idproyectosubnodo;
        //    aParam[2] = new SqlParameter("@t368_descripcion", SqlDbType.Text, 50);
        //    aParam[2].Value = t368_descripcion;
        //    aParam[3] = new SqlParameter("@t368_weblink", SqlDbType.Text, 250);
        //    aParam[3].Value = t368_weblink;
        //    aParam[4] = new SqlParameter("@t368_nombrearchivo", SqlDbType.Text, 250);
        //    aParam[4].Value = t368_nombrearchivo;
        //    aParam[5] = new SqlParameter("@t368_archivo", SqlDbType.Binary, 2147483647);
        //    aParam[5].Value = t368_archivo;
        //    aParam[6] = new SqlParameter("@t368_privado", SqlDbType.Bit, 1);
        //    aParam[6].Value = t368_privado;
        //    aParam[7] = new SqlParameter("@t368_modolectura", SqlDbType.Bit, 1);
        //    aParam[7].Value = t368_modolectura;
        //    aParam[8] = new SqlParameter("@t368_tipogestion", SqlDbType.Bit, 1);
        //    aParam[8].Value = t368_tipogestion;
        //    aParam[9] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
        //    aParam[9].Value = t314_idusuario_autor;
        //    aParam[10] = new SqlParameter("@t368_fecha", SqlDbType.SmallDateTime, 8);
        //    aParam[10].Value = t368_fecha;
        //    aParam[11] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
        //    aParam[11].Value = t314_idusuario_modif;
        //    aParam[12] = new SqlParameter("@t368_fechamodif", SqlDbType.SmallDateTime, 8);
        //    aParam[12].Value = t368_fechamodif;

        //    aParam[13] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
        //    aParam[13].Value = nOrden;
        //    aParam[14] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
        //    aParam[14].Value = nAscDesc;

        //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
        //    return SqlHelper.ExecuteSqlDataReader("SUP_DOCUPE_C", aParam);
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la lista de documentos de los items dependientes del proyecto económico.
        /// Si el documento es privado solo lo saca si el autor coincide con el usuario actual
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Lista(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo),
                ParametroSql.add("@t314_idusuario_autor", SqlDbType.Int, 4, t314_idusuario_autor)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUPE_LISTA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUPE_LISTA", aParam);
        }

		#endregion
	}
}
