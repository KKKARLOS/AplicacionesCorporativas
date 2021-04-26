using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GESTAR
    /// Class	 : DocDeficiencia
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T084_GESDOCDEFI
    /// </summary>
    /// <history>
    /// 	Creado por [DOPEOTCA]	23/05/2007 9:07:28	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class DOCDEFICIENCIA
    {

        #region Propiedades y Atributos

        private int _t084_iddocut;
        public int t084_iddocut
        {
            get { return _t084_iddocut; }
            set { _t084_iddocut = value; }
        }

        private int _t044_iddeficiencia;
        public int t044_iddeficiencia
        {
            get { return _t044_iddeficiencia; }
            set { _t044_iddeficiencia = value; }
        }

        private string _t084_descripcion;
        public string t084_descripcion
        {
            get { return _t084_descripcion; }
            set { _t084_descripcion = value; }
        }

        private string _t084_weblink;
        public string t084_weblink
        {
            get { return _t084_weblink; }
            set { _t084_weblink = value; }
        }

        private string _t084_nombrearchivo;
        public string t084_nombrearchivo
        {
            get { return _t084_nombrearchivo; }
            set { _t084_nombrearchivo = value; }
        }

        private bool _t084_privado;
        public bool t084_privado
        {
            get { return _t084_privado; }
            set { _t084_privado = value; }
        }

        private bool _t084_modolectura;
        public bool t084_modolectura
        {
            get { return _t084_modolectura; }
            set { _t084_modolectura = value; }
        }

        private int _t084_autor;
        public int t084_autor
        {
            get { return _t084_autor; }
            set { _t084_autor = value; }
        }

        private DateTime _t084_fecha;
        public DateTime t084_fecha
        {
            get { return _t084_fecha; }
            set { _t084_fecha = value; }
        }

        private int _t084_autormodif;
        public int t084_autormodif
        {
            get { return _t084_autormodif; }
            set { _t084_autormodif = value; }
        }

        private DateTime _t084_fechamodif;
        public DateTime t084_fechamodif
        {
            get { return _t084_fechamodif; }
            set { _t084_fechamodif = value; }
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

        public DOCDEFICIENCIA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion


        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T084_GESDOCDEFI.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t044_iddeficiencia, Nullable<long> idContentServer, string t084_descripcion, string t084_weblink, string t084_nombrearchivo, bool t084_privado, bool t084_modolectura, int t084_autor)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t044_iddeficiencia", SqlDbType.Int, 4);
            aParam[0].Value = t044_iddeficiencia;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t084_descripcion", SqlDbType.Text, 50);
            aParam[2].Value = t084_descripcion;
            aParam[3] = new SqlParameter("@t084_weblink", SqlDbType.Text, 250);
            aParam[3].Value = t084_weblink;
            aParam[4] = new SqlParameter("@t084_nombrearchivo", SqlDbType.Text, 250);
            aParam[4].Value = t084_nombrearchivo;
            aParam[5] = new SqlParameter("@t084_privado", SqlDbType.Bit, 1);
            aParam[5].Value = t084_privado;
            aParam[6] = new SqlParameter("@t084_modolectura", SqlDbType.Bit, 1);
            aParam[6].Value = t084_modolectura;
            aParam[7] = new SqlParameter("@t084_autor", SqlDbType.Int, 4);
            aParam[7].Value = t084_autor;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            int returnValue;
            if (tr == null)
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("GESTAR_DOCUD_I", aParam));
            else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GESTAR_DOCUD_I", aParam));
            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T084_GESDOCDEFI.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t084_iddocut, Nullable<long> idContentServer, int t044_iddeficiencia, string t084_descripcion, string t084_weblink, string t084_nombrearchivo, bool t084_privado, bool t084_modolectura, int t084_autormodif)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t084_iddocut", SqlDbType.Int, 4);
            aParam[0].Value = t084_iddocut;
            aParam[1] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[1].Value = idContentServer;
            aParam[2] = new SqlParameter("@t044_iddeficiencia", SqlDbType.Int, 4);
            aParam[2].Value = t044_iddeficiencia;
            aParam[3] = new SqlParameter("@t084_descripcion", SqlDbType.Text, 250);
            aParam[3].Value = t084_descripcion;
            aParam[4] = new SqlParameter("@t084_weblink", SqlDbType.Text, 250);
            aParam[4].Value = t084_weblink;
            aParam[5] = new SqlParameter("@t084_nombrearchivo", SqlDbType.Text, 50);
            aParam[5].Value = t084_nombrearchivo;
            aParam[6] = new SqlParameter("@t084_privado", SqlDbType.Bit, 1);
            aParam[6].Value = t084_privado;
            aParam[7] = new SqlParameter("@t084_modolectura", SqlDbType.Bit, 1);
            aParam[7].Value = t084_modolectura;
            aParam[8] = new SqlParameter("@t084_autormodif", SqlDbType.Int, 4);
            aParam[8].Value = t084_autormodif;

            // Ejecuta la query y devuelve el numero de registros modificados.

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DOCUD_U", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DOCUD_U", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T084_GESDOCDEFI a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t084_iddocut)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t084_iddocut", SqlDbType.Int, 4);
            aParam[0].Value = t084_iddocut;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DOCUD_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DOCUD_D", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T084_GESDOCDEFI,
        /// y devuelve una instancia u objeto del tipo DOCUT
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DOCDEFICIENCIA Select(SqlTransaction tr, int t084_iddocut, bool bTraerArchivo)
        {
            DOCDEFICIENCIA o = new DOCDEFICIENCIA();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t084_iddocut", SqlDbType.Int, 4);
            aParam[0].Value = t084_iddocut;
            aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            aParam[1].Value = bTraerArchivo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DOCUD_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_DOCUD_S", aParam);

            if (dr.Read())
            {
                if (dr["t084_iddocut"] != DBNull.Value)
                    o.t084_iddocut = (int)dr["t084_iddocut"];
                if (dr["t044_iddeficiencia"] != DBNull.Value)
                    o.t044_iddeficiencia = (int)dr["t044_iddeficiencia"];
                if (dr["t084_descripcion"] != DBNull.Value)
                    o.t084_descripcion = (string)dr["t084_descripcion"];
                if (dr["t084_weblink"] != DBNull.Value)
                    o.t084_weblink = (string)dr["t084_weblink"];
                if (dr["t084_nombrearchivo"] != DBNull.Value)
                    o.t084_nombrearchivo = (string)dr["t084_nombrearchivo"];
                if (dr["t084_privado"] != DBNull.Value)
                    o.t084_privado = (bool)dr["t084_privado"];
                if (dr["t084_modolectura"] != DBNull.Value)
                    o.t084_modolectura = (bool)dr["t084_modolectura"];
                if (dr["t084_autor"] != DBNull.Value)
                    o.t084_autor = (int)dr["t084_autor"];
                if (dr["t084_fecha"] != DBNull.Value)
                    o.t084_fecha = DateTime.Parse(dr["t084_fecha"].ToString());
                if (dr["t084_autormodif"] != DBNull.Value)
                    o.t084_autormodif = (int)dr["t084_autormodif"];
                if (dr["t084_fechamodif"] != DBNull.Value)
                    o.t084_fechamodif = DateTime.Parse(dr["t084_fechamodif"].ToString());
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
        /// Obtiene un catálogo de registros de la tabla T084_GESDOCDEFI.
        /// </summary>
        /// <history>
        /// 	Creado por [DOPEOTCA]	26/03/2007 9:07:28
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t044_iddeficiencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t044_iddeficiencia", SqlDbType.Int, 4);
            aParam[0].Value = t044_iddeficiencia;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DOCUD_C", aParam);

            return dr;
        }
        #endregion
    }
}
