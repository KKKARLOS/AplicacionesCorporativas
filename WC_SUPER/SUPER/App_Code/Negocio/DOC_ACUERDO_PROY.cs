using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for DOC_ACUERDO_PROY
/// </summary>
/// 
namespace SUPER.Capa_Negocio
{
    public partial class DOC_ACUERDO_PROY
    {
        #region Propiedades y Atributos

        private int _t640_iddocfact;
        public int t640_iddocfact
        {
            get { return _t640_iddocfact; }
            set { _t640_iddocfact = value; }
        }

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private string _t640_descripcion;
        public string t640_descripcion
        {
            get { return _t640_descripcion; }
            set { _t640_descripcion = value; }
        }

        private string _t640_weblink;
        public string t640_weblink
        {
            get { return _t640_weblink; }
            set { _t640_weblink = value; }
        }

        private string _t640_nombrearchivo;
        public string t640_nombrearchivo
        {
            get { return _t640_nombrearchivo; }
            set { _t640_nombrearchivo = value; }
        }

        private byte[] _t640_archivo;
        public byte[] t640_archivo
        {
            get { return _t640_archivo; }
            set { _t640_archivo = value; }
        }

        private bool _t640_privado;
        public bool t640_privado
        {
            get { return _t640_privado; }
            set { _t640_privado = value; }
        }

        private bool _t640_modolectura;
        public bool t640_modolectura
        {
            get { return _t640_modolectura; }
            set { _t640_modolectura = value; }
        }

        private bool _t640_tipogestion;
        public bool t640_tipogestion
        {
            get { return _t640_tipogestion; }
            set { _t640_tipogestion = value; }
        }

        private int _t314_idusuario_autor;
        public int t314_idusuario_autor
        {
            get { return _t314_idusuario_autor; }
            set { _t314_idusuario_autor = value; }
        }

        private DateTime _t640_fecha;
        public DateTime t640_fecha
        {
            get { return _t640_fecha; }
            set { _t640_fecha = value; }
        }

        private int _t314_idusuario_modif;
        public int t314_idusuario_modif
        {
            get { return _t314_idusuario_modif; }
            set { _t314_idusuario_modif = value; }
        }

        private DateTime _t640_fechamodif;
        public DateTime t640_fechamodif
        {
            get { return _t640_fechamodif; }
            set { _t640_fechamodif = value; }
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

        public DOC_ACUERDO_PROY()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T640_DOC_ACUERDO_PROY.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	22/12/2010 9:38:00
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t301_idproyecto, string t640_descripcion, string t640_weblink, string t640_nombrearchivo,
                                Nullable<long> idContentServer, bool t640_privado, bool t640_modolectura, bool t640_tipogestion, 
                                int t314_idusuario_autor)
        {
            //if (t640_archivo.Length == 0) t640_archivo = null;
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t640_descripcion", SqlDbType.Text, 50);
            aParam[1].Value = t640_descripcion;
            aParam[2] = new SqlParameter("@t640_weblink", SqlDbType.Text, 250);
            aParam[2].Value = t640_weblink;
            aParam[3] = new SqlParameter("@t640_nombrearchivo", SqlDbType.Text, 250);
            aParam[3].Value = t640_nombrearchivo;

            //aParam[4] = new SqlParameter("@t640_archivo", SqlDbType.Binary, 2147483647);
            //aParam[4].Value = t640_archivo;

            aParam[4] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[4].Value = idContentServer;
            aParam[5] = new SqlParameter("@t640_privado", SqlDbType.Bit, 1);
            aParam[5].Value = t640_privado;
            aParam[6] = new SqlParameter("@t640_modolectura", SqlDbType.Bit, 1);
            aParam[6].Value = t640_modolectura;
            aParam[7] = new SqlParameter("@t640_tipogestion", SqlDbType.Bit, 1);
            aParam[7].Value = t640_tipogestion;
            aParam[8] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[8].Value = t314_idusuario_autor;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOC_ACUERDO_PROY_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOC_ACUERDO_PROY_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T640_DOC_ACUERDO_PROY.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	22/12/2010 9:38:00
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t640_iddocfact, int t301_idproyecto, string t640_descripcion, string t640_weblink,
                                string t640_nombrearchivo, Nullable<long> idContentServer, bool t640_privado, bool t640_modolectura, 
                                bool t640_tipogestion, int t314_idusuario_modif)
        {
            //if (t640_archivo.Length == 0) t640_archivo = null;
            SqlParameter[] aParam = new SqlParameter[10];
            aParam[0] = new SqlParameter("@t640_iddocfact", SqlDbType.Int, 4);
            aParam[0].Value = t640_iddocfact;
            aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[1].Value = t301_idproyecto;
            aParam[2] = new SqlParameter("@t640_descripcion", SqlDbType.Text, 50);
            aParam[2].Value = t640_descripcion;
            aParam[3] = new SqlParameter("@t640_weblink", SqlDbType.Text, 250);
            aParam[3].Value = t640_weblink;
            aParam[4] = new SqlParameter("@t640_nombrearchivo", SqlDbType.Text, 250);
            aParam[4].Value = t640_nombrearchivo;

            //aParam[5] = new SqlParameter("@t640_archivo", SqlDbType.Binary, 2147483647);
            //aParam[5].Value = t640_archivo;

            aParam[5] = new SqlParameter("@t2_iddocumento", SqlDbType.BigInt, 8);
            aParam[5].Value = idContentServer;
            aParam[6] = new SqlParameter("@t640_privado", SqlDbType.Bit, 1);
            aParam[6].Value = t640_privado;
            aParam[7] = new SqlParameter("@t640_modolectura", SqlDbType.Bit, 1);
            aParam[7].Value = t640_modolectura;
            aParam[8] = new SqlParameter("@t640_tipogestion", SqlDbType.Bit, 1);
            aParam[8].Value = t640_tipogestion;
            aParam[9] = new SqlParameter("@t314_idusuario_modif", SqlDbType.Int, 4);
            aParam[9].Value = t314_idusuario_modif;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOC_ACUERDO_PROY_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOC_ACUERDO_PROY_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T640_DOC_ACUERDO_PROY a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	22/12/2010 9:38:00
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t640_iddocfact)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t640_iddocfact", SqlDbType.Int, 4);
            aParam[0].Value = t640_iddocfact;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOC_ACUERDO_PROY_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOC_ACUERDO_PROY_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T640_DOC_ACUERDO_PROY,
        /// y devuelve una instancia u objeto del tipo DOC_ACUERDO_PROY
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	22/12/2010 9:38:00
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DOC_ACUERDO_PROY Select(SqlTransaction tr, int t640_iddocfact)//, bool bTraerArchivo)
        {
            DOC_ACUERDO_PROY o = new DOC_ACUERDO_PROY();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t640_iddocfact", SqlDbType.Int, 4);
            aParam[0].Value = t640_iddocfact;
            //aParam[1] = new SqlParameter("@bArchivo", SqlDbType.Bit, 1);
            //aParam[1].Value = bTraerArchivo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_DOC_ACUERDO_PROY_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOC_ACUERDO_PROY_S", aParam);

            if (dr.Read())
            {
                if (dr["t640_iddocfact"] != DBNull.Value)
                    o.t640_iddocfact = int.Parse(dr["t640_iddocfact"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t640_descripcion"] != DBNull.Value)
                    o.t640_descripcion = (string)dr["t640_descripcion"];
                if (dr["t640_weblink"] != DBNull.Value)
                    o.t640_weblink = (string)dr["t640_weblink"];
                if (dr["t640_nombrearchivo"] != DBNull.Value)
                    o.t640_nombrearchivo = (string)dr["t640_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t640_archivo = (byte[])dr["t640_archivo"];
                //}
                if (dr["t640_privado"] != DBNull.Value)
                    o.t640_privado = (bool)dr["t640_privado"];
                if (dr["t640_modolectura"] != DBNull.Value)
                    o.t640_modolectura = (bool)dr["t640_modolectura"];
                if (dr["t640_tipogestion"] != DBNull.Value)
                    o.t640_tipogestion = (bool)dr["t640_tipogestion"];
                if (dr["t314_idusuario_autor"] != DBNull.Value)
                    o.t314_idusuario_autor = int.Parse(dr["t314_idusuario_autor"].ToString());
                if (dr["t640_fecha"] != DBNull.Value)
                    o.t640_fecha = (DateTime)dr["t640_fecha"];
                if (dr["t314_idusuario_modif"] != DBNull.Value)
                    o.t314_idusuario_modif = int.Parse(dr["t314_idusuario_modif"].ToString());
                if (dr["t640_fechamodif"] != DBNull.Value)
                    o.t640_fechamodif = (DateTime)dr["t640_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autorModif"] != DBNull.Value)
                    o.DesAutorModif = (string)dr["autorModif"];
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de DOC_ACUERDO_PROY"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Borra los registros de la tabla T640_DOC_ACUERDO_PROY en función de una foreign key.
        /// </summary>
        /// <remarks>
        /// 	Creado por [sqladmin]	22/12/2010 9:38:00
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void DeleteByPE(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_DOC_ACUERDO_PROY_DByPE", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOC_ACUERDO_PROY_DByPE", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T640_DOC_ACUERDO_PROY.
        /// Si el documento es privado solo lo saca si el autor coincide con el usuario actual
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	22/12/2010 13:15:25
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t301_idproyecto, int t314_idusuario_autor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t314_idusuario_autor", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_autor;
            return SqlHelper.ExecuteSqlDataReader("SUP_DOC_ACUERDO_PROY_C2", aParam);
        }
        #endregion
    }
}