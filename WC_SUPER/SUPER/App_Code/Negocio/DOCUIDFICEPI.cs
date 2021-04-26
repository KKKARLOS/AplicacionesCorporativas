using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : DOCUIDFICEPI
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T184_FICEPIDOCUCV
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	07/04/2011	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class DOCUIDFICEPI
    {
        #region Propiedades y Atributos

        private int _t184_iddocucv;
        public int t184_iddocucv
        {
            get { return _t184_iddocucv; }
            set { _t184_iddocucv = value; }
        }

        private int _t001_idficepi_prof;
        public int t001_idficepi_prof
        {
            get { return _t001_idficepi_prof; }
            set { _t001_idficepi_prof = value; }
        }

        private string _t184_descripcion;
        public string t184_descripcion
        {
            get { return _t184_descripcion; }
            set { _t184_descripcion = value; }
        }

        private string _t184_nombrearchivo;
        public string t184_nombrearchivo
        {
            get { return _t184_nombrearchivo; }
            set { _t184_nombrearchivo = value; }
        }
 
        private byte[] _t184_archivo;
        public byte[] t184_archivo
        {
            get { return _t184_archivo; }
            set { _t184_archivo = value; }
        }

        private int _t001_idficepi_autor;
        public int t001_idficepi_autor
        {
            get { return _t001_idficepi_autor; }
            set { _t001_idficepi_autor = value; }
        }

        private DateTime _t184_fcreacion;
        public DateTime t184_fcreacion
        {
            get { return _t184_fcreacion; }
            set { _t184_fcreacion = value; }
        }

        private int _t001_idficepi_modif;
        public int t001_idficepi_modif
        {
            get { return _t001_idficepi_modif; }
            set { _t001_idficepi_modif = value; }
        }

        private DateTime _t184_fmodif;
        public DateTime t184_fmodif
        {
            get { return _t184_fmodif; }
            set { _t184_fmodif = value; }
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

        public DOCUIDFICEPI()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T184_FICEPIDOCUCV.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t001_idficepi_prof, string t184_descripcion, string t184_nombrearchivo, 
                                 int t001_idficepi_autor, Nullable<long> idContentServer)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t001_idficepi_prof", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi_prof;
            aParam[1] = new SqlParameter("@t184_descripcion", SqlDbType.VarChar, 50);
            aParam[1].Value = t184_descripcion;
            aParam[2] = new SqlParameter("@t184_nombrearchivo", SqlDbType.VarChar, 250);
            aParam[2].Value = t184_nombrearchivo;
            //aParam[3] = new SqlParameter("@t184_archivo", SqlDbType.Binary, 2147483647);
            //aParam[3].Value = t184_archivo;
            aParam[3] = new SqlParameter("@t001_idficepi_autor", SqlDbType.Int, 4);
            aParam[3].Value = t001_idficepi_autor;
            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUIDFICEPI_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUIDFICEPI_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T658_DOCUEC.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t184_iddocucv, string t184_descripcion, string t184_nombrearchivo,
                                int t001_idficepi_modif, Nullable<long> idContentServer)                        
        {
            //if (t184_archivo.Length == 0) t184_archivo = null;

            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t184_iddocucv", SqlDbType.Int, 4);
            aParam[0].Value = t184_iddocucv;
            aParam[1] = new SqlParameter("@t184_descripcion", SqlDbType.VarChar, 50);
            aParam[1].Value = t184_descripcion;
            aParam[2] = new SqlParameter("@t184_nombrearchivo", SqlDbType.VarChar, 250);
            aParam[2].Value = t184_nombrearchivo;
            //aParam[3] = new SqlParameter("@t184_archivo", SqlDbType.Binary, 2147483647);
            //aParam[3].Value = t184_archivo;
            aParam[3] = new SqlParameter("@t001_idficepi_modif", SqlDbType.Int, 4);
            aParam[3].Value = t001_idficepi_modif;
            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUIDFICEPI_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUIDFICEPI_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T658_DOCUEC a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t184_iddocucv)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t184_iddocucv", SqlDbType.Int, 4);
            aParam[0].Value = t184_iddocucv;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUIDFICEPI_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUIDFICEPI_D", aParam);
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
        public static DOCUIDFICEPI Select(SqlTransaction tr, int t184_iddocucv)
        {
            DOCUIDFICEPI o = new DOCUIDFICEPI();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t184_iddocucv", SqlDbType.Int, 4);
            aParam[0].Value = t184_iddocucv;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCUIDFICEPI_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUIDFICEPI_O", aParam);

            if (dr.Read())
            {
                if (dr["t184_iddocucv"] != DBNull.Value)
                    o.t184_iddocucv = int.Parse(dr["t184_iddocucv"].ToString());
                if (dr["t001_idficepi_prof"] != DBNull.Value)
                    o.t001_idficepi_prof = int.Parse(dr["t001_idficepi_prof"].ToString());
                if (dr["t184_descripcion"] != DBNull.Value)
                    o.t184_descripcion = (string)dr["t184_descripcion"];
                if (dr["t184_nombrearchivo"] != DBNull.Value)
                    o.t184_nombrearchivo = (string)dr["t184_nombrearchivo"];
                //if (dr["t184_archivo"] != DBNull.Value)
                //    o.t184_archivo = (byte[])dr["t184_archivo"];
                if (dr["t001_idficepi_autor"] != DBNull.Value)
                    o.t001_idficepi_autor = int.Parse(dr["t001_idficepi_autor"].ToString());
                if (dr["t184_fcreacion"] != DBNull.Value)
                    o._t184_fcreacion = (DateTime)dr["t184_fcreacion"];
                if (dr["t001_idficepi_modif"] != DBNull.Value)
                    o.t001_idficepi_modif = int.Parse(dr["t001_idficepi_modif"].ToString());
                if (dr["t184_fmodif"] != DBNull.Value)
                    o.t184_fmodif = (DateTime)dr["t184_fmodif"];
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

        public static SqlDataReader Catalogo(int t001_idficepi_prof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi_prof", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi_prof;

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCUIDFICEPI_CAT", aParam);
        }
 
        #endregion
    }
}
