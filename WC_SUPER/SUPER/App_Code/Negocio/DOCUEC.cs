using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : DOCUEC
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T658_DOCUEC
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	07/04/2011	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class DOCUEC
    {
        #region Propiedades y Atributos

        private int _t658_iddocuec;
        public int t658_iddocuec
        {
            get { return _t658_iddocuec; }
            set { _t658_iddocuec = value; }
        }

        private int _t639_idcomunicacion;
        public int t639_idcomunicacion
        {
            get { return _t639_idcomunicacion; }
            set { _t639_idcomunicacion = value; }
        }

        private string _t658_descripcion;
        public string t658_descripcion
        {
            get { return _t658_descripcion; }
            set { _t658_descripcion = value; }
        }
        private string _t658_weblink;
        public string t658_weblink
        {
            get { return _t658_weblink; }
            set { _t658_weblink = value; }
        }

        private string _t658_nombrearchivo;
        public string t658_nombrearchivo
        {
            get { return _t658_nombrearchivo; }
            set { _t658_nombrearchivo = value; }
        }

        private byte[] _t658_archivo;
        public byte[] t658_archivo
        {
            get { return _t658_archivo; }
            set { _t658_archivo = value; }
        }

        private int _t314_idusuario_autor;
        public int t314_idusuario_autor
        {
            get { return _t314_idusuario_autor; }
            set { _t314_idusuario_autor = value; }
        }

        private DateTime _t658_fecha;
        public DateTime t658_fecha
        {
            get { return _t658_fecha; }
            set { _t658_fecha = value; }
        }

        private int _t314_idusuario_modif;
        public int t314_idusuario_modif
        {
            get { return _t314_idusuario_modif; }
            set { _t314_idusuario_modif = value; }
        }

        private DateTime _t658_fechamodif;
        public DateTime t658_fechamodif
        {
            get { return _t658_fechamodif; }
            set { _t658_fechamodif = value; }
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

        #region Constructor

        public DOCUEC()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T658_DOCUEC.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, Nullable<int> t639_idcomunicacion, string t658_descripcion, string t658_weblink,
                                 string t658_nombrearchivo, Nullable<long> idContentServer, int t314_idusuario_autor, string t658_usuticks)
        {
            //if (t658_archivo.Length == 0) t658_archivo = null; 
            
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t639_idcomunicacion", SqlDbType.Int, 4);
            aParam[0].Value = t639_idcomunicacion;
            aParam[1] = new SqlParameter("@t658_descripcion", SqlDbType.VarChar, 50);
            aParam[1].Value = t658_descripcion;
            aParam[2] = new SqlParameter("@t658_weblink", SqlDbType.Text, 250);
            aParam[2].Value = t658_weblink;
            aParam[3] = new SqlParameter("@t658_nombrearchivo", SqlDbType.VarChar, 250);
            aParam[3].Value = t658_nombrearchivo;

            //aParam[4] = new SqlParameter("@t658_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t658_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[5].Value = t314_idusuario_autor;
            aParam[6] = new SqlParameter("@t658_usuticks", SqlDbType.VarChar, 50);
            aParam[6].Value = t658_usuticks;


            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUEC_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUEC_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T658_DOCUEC.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t658_iddocuec, int t639_idcomunicacion, string t658_descripcion, string t658_weblink,
                                 string t658_nombrearchivo, Nullable<long> idContentServer, int t314_idusuario_modif)
        {
            //if (t658_archivo.Length == 0) t658_archivo = null;

            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t658_iddocuec", SqlDbType.Int, 4);
            aParam[0].Value = t658_iddocuec;
            aParam[1] = new SqlParameter("@t639_idcomunicacion", SqlDbType.Int, 4);
            aParam[1].Value = t639_idcomunicacion;
            aParam[2] = new SqlParameter("@t658_descripcion", SqlDbType.VarChar, 50);
            aParam[2].Value = t658_descripcion;
            aParam[3] = new SqlParameter("@t658_weblink", SqlDbType.Text, 250);
            aParam[3].Value = t658_weblink;
            aParam[4] = new SqlParameter("@t658_nombrearchivo", SqlDbType.VarChar, 250);
            aParam[4].Value = t658_nombrearchivo;

            //aParam[5] = new SqlParameter("@t658_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t658_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[6].Value = t314_idusuario_modif;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUEC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUEC_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T658_DOCUEC a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t658_iddocuec)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t658_iddocuec", SqlDbType.Int, 4);
            aParam[0].Value = t658_iddocuec;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUEC_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUEC_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T658_DOCUEC,
        /// y devuelve una instancia u objeto del tipo DOCUEC
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DOCUEC Select(SqlTransaction tr, int t658_iddocuec)//, bool bTraerArchivo)
        {
            DOCUEC o = new DOCUEC();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t658_iddocuec", SqlDbType.Int, 4);
            aParam[0].Value = t658_iddocuec;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUEC_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUEC_O", aParam);

            if (dr.Read())
            {
                if (dr["t658_iddocuec"] != DBNull.Value)
                    o.t658_iddocuec = int.Parse(dr["t658_iddocuec"].ToString());
                if (dr["t639_idcomunicacion"] != DBNull.Value)
                    o.t639_idcomunicacion = int.Parse(dr["t639_idcomunicacion"].ToString());
                if (dr["t658_descripcion"] != DBNull.Value)
                    o.t658_descripcion = (string)dr["t658_descripcion"];
                if (dr["t658_weblink"] != DBNull.Value)
                    o.t658_weblink = (string)dr["t658_weblink"];
                if (dr["t658_nombrearchivo"] != DBNull.Value)
                    o.t658_nombrearchivo = (string)dr["t658_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t658_archivo = (byte[])dr["t658_archivo"];
                //}
                if (dr["t314_idusuario_autor"] != DBNull.Value)
                    o.t314_idusuario_autor = int.Parse(dr["t314_idusuario_autor"].ToString());
                if (dr["t658_fecha"] != DBNull.Value)
                    o.t658_fecha = (DateTime)dr["t658_fecha"];
                if (dr["t314_idusuario_modif"] != DBNull.Value)
                    o.t314_idusuario_modif = int.Parse(dr["t314_idusuario_modif"].ToString());
                if (dr["t658_fechamodif"] != DBNull.Value)
                    o.t658_fechamodif = (DateTime)dr["t658_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUEC"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static SqlDataReader Catalogo(int t639_idcomunicacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t639_idcomunicacion", SqlDbType.Int, 4);
            aParam[0].Value = t639_idcomunicacion;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUEC_CAT", aParam);
        }
        public static SqlDataReader CatalogoByUsuTicks(string t658_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t658_usuticks", SqlDbType.VarChar, 50);
            aParam[0].Value = t658_usuticks;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUEC_ByUsuTicks_CAT", aParam);
        }

        #endregion
    }
}
