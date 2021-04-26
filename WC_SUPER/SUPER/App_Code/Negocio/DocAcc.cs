using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DOCACC
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t387_DOCACC
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	16/11/2007 12:39:48	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DOCACC
	{

		#region Propiedades y Atributos

		private int _t383_idaccion;
		public int t383_idaccion
		{
			get {return _t383_idaccion;}
			set { _t383_idaccion = value ;}
		}

		private byte[] _t387_archivo;
		public byte[] t387_archivo
		{
			get {return _t387_archivo;}
			set { _t387_archivo = value ;}
		}

		private int _t387_autor;
		public int t387_autor
		{
			get {return _t387_autor;}
			set { _t387_autor = value ;}
		}

		private int _t387_autormodif;
		public int t387_autormodif
		{
			get {return _t387_autormodif;}
			set { _t387_autormodif = value ;}
		}

		private string _t387_descripcion;
		public string t387_descripcion
		{
			get {return _t387_descripcion;}
			set { _t387_descripcion = value ;}
		}

		private DateTime _t387_fecha;
		public DateTime t387_fecha
		{
			get {return _t387_fecha;}
			set { _t387_fecha = value ;}
		}

		private DateTime _t387_fechamodif;
		public DateTime t387_fechamodif
		{
			get {return _t387_fechamodif;}
			set { _t387_fechamodif = value ;}
		}

		private int _t387_iddocacc;
		public int t387_iddocacc
		{
			get {return _t387_iddocacc;}
			set { _t387_iddocacc = value ;}
		}

		private bool _t387_modolectura;
		public bool t387_modolectura
		{
			get {return _t387_modolectura;}
			set { _t387_modolectura = value ;}
		}

		private string _t387_nombrearchivo;
		public string t387_nombrearchivo
		{
			get {return _t387_nombrearchivo;}
			set { _t387_nombrearchivo = value ;}
		}

		private bool _t387_privado;
		public bool t387_privado
		{
			get {return _t387_privado;}
			set { _t387_privado = value ;}
		}

		private bool _t387_tipogestion;
		public bool t387_tipogestion
		{
			get {return _t387_tipogestion;}
			set { _t387_tipogestion = value ;}
		}

		private string _t387_weblink;
		public string t387_weblink
		{
			get {return _t387_weblink;}
			set { _t387_weblink = value ;}
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

		public DOCACC() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t387_DOCACC.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t383_idaccion, Nullable<long> idContentServer, int t387_autor, string t387_descripcion, 
                            bool t387_modolectura, string t387_nombrearchivo, bool t387_privado, bool t387_tipogestion, string t387_weblink)
		{
            //if (t387_archivo.Length == 0) t387_archivo = null; 
            
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t383_idaccion;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t387_autor", SqlDbType.Int, 4);
			aParam[2].Value = t387_autor;
			aParam[3] = new SqlParameter("@t387_autormodif", SqlDbType.Int, 4);
            aParam[3].Value = t387_autor;
			aParam[4] = new SqlParameter("@t387_descripcion", SqlDbType.Text, 50);
			aParam[4].Value = t387_descripcion;
			aParam[5] = new SqlParameter("@t387_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t387_modolectura;
			aParam[6] = new SqlParameter("@t387_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t387_nombrearchivo;
			aParam[7] = new SqlParameter("@t387_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t387_privado;
			aParam[8] = new SqlParameter("@t387_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t387_tipogestion;
			aParam[9] = new SqlParameter("@t387_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t387_weblink;

            //aParam[10] = new SqlParameter("@t387_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t387_archivo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCACC_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCACC_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t387_DOCACC.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t383_idaccion, Nullable<long> idContentServer, int t387_autormodif, 
                                 string t387_descripcion, int t387_iddocacc, bool t387_modolectura, string t387_nombrearchivo, 
                                 bool t387_privado, bool t387_tipogestion, string t387_weblink)
		{
            //if (t387_archivo.Length == 0) t387_archivo = null;
            
            SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t383_idaccion;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t387_autormodif", SqlDbType.Int, 4);
			aParam[2].Value = t387_autormodif;
			aParam[3] = new SqlParameter("@t387_descripcion", SqlDbType.Text, 50);
			aParam[3].Value = t387_descripcion;
			aParam[4] = new SqlParameter("@t387_iddocacc", SqlDbType.Int, 4);
			aParam[4].Value = t387_iddocacc;
			aParam[5] = new SqlParameter("@t387_modolectura", SqlDbType.Bit, 1);
			aParam[5].Value = t387_modolectura;
			aParam[6] = new SqlParameter("@t387_nombrearchivo", SqlDbType.Text, 250);
			aParam[6].Value = t387_nombrearchivo;
			aParam[7] = new SqlParameter("@t387_privado", SqlDbType.Bit, 1);
			aParam[7].Value = t387_privado;
			aParam[8] = new SqlParameter("@t387_tipogestion", SqlDbType.Bit, 1);
			aParam[8].Value = t387_tipogestion;
			aParam[9] = new SqlParameter("@t387_weblink", SqlDbType.Text, 250);
			aParam[9].Value = t387_weblink;

            //aParam[10] = new SqlParameter("@t387_archivo", SqlDbType.Binary, 2147483647);
            //aParam[10].Value = t387_archivo;
            // Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCACC_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCACC_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t387_DOCACC a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t387_iddocacc)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t387_iddocacc", SqlDbType.Int, 4);
			aParam[0].Value = t387_iddocacc;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCACC_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCACC_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t387_DOCACC,
		/// y devuelve una instancia u objeto del tipo DOCACC
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static DOCACC Select(SqlTransaction tr, int t387_iddocacc)//, bool bTraerArchivo) 
		{
			DOCACC o = new DOCACC();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t387_iddocacc", SqlDbType.Int, 4);
			aParam[0].Value = t387_iddocacc;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCACC_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCACC_S", aParam);

			if (dr.Read())
			{
				if (dr["t383_idaccion"] != DBNull.Value)
					o.t383_idaccion = (int)dr["t383_idaccion"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t387_archivo = (byte[])dr["t387_archivo"];
                //}
                if (dr["t387_autor"] != DBNull.Value)
					o.t387_autor = (int)dr["t387_autor"];
				if (dr["t387_autormodif"] != DBNull.Value)
					o.t387_autormodif = (int)dr["t387_autormodif"];
				if (dr["t387_descripcion"] != DBNull.Value)
					o.t387_descripcion = (string)dr["t387_descripcion"];
				if (dr["t387_fecha"] != DBNull.Value)
					o.t387_fecha = (DateTime)dr["t387_fecha"];
				if (dr["t387_fechamodif"] != DBNull.Value)
					o.t387_fechamodif = (DateTime)dr["t387_fechamodif"];
				if (dr["t387_iddocacc"] != DBNull.Value)
					o.t387_iddocacc = (int)dr["t387_iddocacc"];
				if (dr["t387_modolectura"] != DBNull.Value)
					o.t387_modolectura = (bool)dr["t387_modolectura"];
				if (dr["t387_nombrearchivo"] != DBNull.Value)
					o.t387_nombrearchivo = (string)dr["t387_nombrearchivo"];
				if (dr["t387_privado"] != DBNull.Value)
					o.t387_privado = (bool)dr["t387_privado"];
				if (dr["t387_tipogestion"] != DBNull.Value)
					o.t387_tipogestion = (bool)dr["t387_tipogestion"];
				if (dr["t387_weblink"] != DBNull.Value)
					o.t387_weblink = (string)dr["t387_weblink"];
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
		/// Obtiene un catálogo de registros de la tabla t387_DOCACC.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 12:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t383_idaccion, int t387_autor)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t383_idaccion;
			aParam[1] = new SqlParameter("@t387_autor", SqlDbType.Int, 4);
			aParam[1].Value = t387_autor;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCACC_C2", aParam);
		}

		#endregion
	}
}
