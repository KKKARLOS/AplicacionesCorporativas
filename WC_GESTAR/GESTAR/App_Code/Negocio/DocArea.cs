using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GESTAR
    /// Class	 : DOCAREA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T083_GESDOCAREA
    /// </summary>
    /// <history>
    /// 	Creado por [DOPEOTCA]	23/05/2007 9:07:28	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class DOCAREA
    {

        #region Propiedades y Atributos

        private int _t083_iddocut;
        public int t083_iddocut
        {
            get { return _t083_iddocut; }
            set { _t083_iddocut = value; }
        }

        private int _t042_idarea;
        public int t042_idarea
        {
            get { return _t042_idarea; }
            set { _t042_idarea = value; }
        }

        private string _t083_descripcion;
        public string t083_descripcion
        {
            get { return _t083_descripcion; }
            set { _t083_descripcion = value; }
        }

        private string _t083_weblink;
        public string t083_weblink
        {
            get { return _t083_weblink; }
            set { _t083_weblink = value; }
        }

        private string _t083_nombrearchivo;
        public string t083_nombrearchivo
        {
            get { return _t083_nombrearchivo; }
            set { _t083_nombrearchivo = value; }
        }

        private bool _t083_privado;
        public bool t083_privado
        {
            get { return _t083_privado; }
            set { _t083_privado = value; }
        }

        private bool _t083_modolectura;
        public bool t083_modolectura
        {
            get { return _t083_modolectura; }
            set { _t083_modolectura = value; }
        }

        private int _t083_autor;
        public int t083_autor
        {
            get { return _t083_autor; }
            set { _t083_autor = value; }
        }

        private DateTime _t083_fecha;
        public DateTime t083_fecha
        {
            get { return _t083_fecha; }
            set { _t083_fecha = value; }
        }

        private int _t083_autormodif;
        public int t083_autormodif
        {
            get { return _t083_autormodif; }
            set { _t083_autormodif = value; }
        }

        private DateTime _t083_fechamodif;
        public DateTime t083_fechamodif
        {
            get { return _t083_fechamodif; }
            set { _t083_fechamodif = value; }
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

        public DOCAREA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion


        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T083_GESDOCAREA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t042_idarea, Nullable<long> idContentServer, string t083_descripcion, string t083_weblink, string t083_nombrearchivo, bool t083_privado, bool t083_modolectura, int t083_autor)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t042_idarea", SqlDbType.Int, 4);
            aParam[0].Value = t042_idarea;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t083_descripcion", SqlDbType.Text, 50);
            aParam[2].Value = t083_descripcion;
            aParam[3] = new SqlParameter("@t083_weblink", SqlDbType.Text, 250);
            aParam[3].Value = t083_weblink;
            aParam[4] = new SqlParameter("@t083_nombrearchivo", SqlDbType.Text, 250);
            aParam[4].Value = t083_nombrearchivo;
            aParam[5] = new SqlParameter("@t083_privado", SqlDbType.Bit, 1);
            aParam[5].Value = t083_privado;
            aParam[6] = new SqlParameter("@t083_modolectura", SqlDbType.Bit, 1);
            aParam[6].Value = t083_modolectura;
            aParam[7] = new SqlParameter("@t083_autor", SqlDbType.Int, 4);
            aParam[7].Value = t083_autor;
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            int returnValue;
            if (tr == null)
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("GESTAR_DOCUA_I", aParam));
            else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GESTAR_DOCUA_I", aParam));
            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T083_GESDOCAREA.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t083_iddocut, Nullable<long> idContentServer, int t042_idarea, string t083_descripcion, string t083_weblink, string t083_nombrearchivo, bool t083_privado, bool t083_modolectura, int t083_autormodif)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t083_iddocut", SqlDbType.Int, 4);
            aParam[0].Value = t083_iddocut;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t042_idarea", SqlDbType.Int, 4);
            aParam[2].Value = t042_idarea;
            aParam[3] = new SqlParameter("@t083_descripcion", SqlDbType.Text, 250);
            aParam[3].Value = t083_descripcion;
            aParam[4] = new SqlParameter("@t083_weblink", SqlDbType.Text, 250);
            aParam[4].Value = t083_weblink;
            aParam[5] = new SqlParameter("@t083_nombrearchivo", SqlDbType.Text, 50);
            aParam[5].Value = t083_nombrearchivo;
            aParam[6] = new SqlParameter("@t083_privado", SqlDbType.Bit, 1);
            aParam[6].Value = t083_privado;
            aParam[7] = new SqlParameter("@t083_modolectura", SqlDbType.Bit, 1);
            aParam[7].Value = t083_modolectura;
            aParam[8] = new SqlParameter("@t083_autormodif", SqlDbType.Int, 4);
            aParam[8].Value = t083_autormodif;

            // Ejecuta la query y devuelve el numero de registros modificados.

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DOCUA_U", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DOCUA_U", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T083_GESDOCAREA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t083_iddocut)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t083_iddocut", SqlDbType.Int, 4);
            aParam[0].Value = t083_iddocut;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DOCUA_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DOCUA_D", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T083_GESDOCAREA,
        /// y devuelve una instancia u objeto del tipo DOCUT
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DOCAREA Select(SqlTransaction tr, int t083_iddocut, bool bTraerArchivo)
        {
            DOCAREA o = new DOCAREA();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t083_iddocut", SqlDbType.Int, 4);
            aParam[0].Value = t083_iddocut;
            aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            aParam[1].Value = bTraerArchivo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DOCUA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_DOCUA_S", aParam);

            if (dr.Read())
            {
                if (dr["t083_iddocut"] != DBNull.Value)
                    o.t083_iddocut = (int)dr["t083_iddocut"];
                if (dr["t042_idarea"] != DBNull.Value)
                    o.t042_idarea = (int)dr["t042_idarea"];
                if (dr["t083_descripcion"] != DBNull.Value)
                    o.t083_descripcion = (string)dr["t083_descripcion"];
                if (dr["t083_weblink"] != DBNull.Value)
                    o.t083_weblink = (string)dr["t083_weblink"];
                if (dr["t083_nombrearchivo"] != DBNull.Value)
                    o.t083_nombrearchivo = (string)dr["t083_nombrearchivo"];
                if (dr["t083_privado"] != DBNull.Value)
                    o.t083_privado = (bool)dr["t083_privado"];
                if (dr["t083_modolectura"] != DBNull.Value)
                    o.t083_modolectura = (bool)dr["t083_modolectura"];
                if (dr["t083_autor"] != DBNull.Value)
                    o.t083_autor = (int)dr["t083_autor"];
                if (dr["t083_fecha"] != DBNull.Value)
                    o.t083_fecha = DateTime.Parse(dr["t083_fecha"].ToString());
                if (dr["t083_autormodif"] != DBNull.Value)
                    o.t083_autormodif = (int)dr["t083_autormodif"];
                if (dr["t083_fechamodif"] != DBNull.Value)
                    o.t083_fechamodif = DateTime.Parse(dr["t083_fechamodif"].ToString());
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de DOCUT"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T083_GESDOCAREA.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------

        public static SqlDataReader Catalogo( Nullable<int> t042_idarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@t042_idarea", SqlDbType.Int, 4);
            aParam[0].Value = t042_idarea;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DOCUA_C", aParam);

            return dr;
        }
        #endregion
    }
}
