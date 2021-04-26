using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SUPER.Capa_Datos
{
	public partial class DOCDIALOGO
	{
        #region Propiedades y Atributos

        private int _t837_id;
        public int t837_id
        {
            get { return _t837_id; }
            set { _t837_id = value; }
        }

        private int _t831_iddialogoalerta;
        public int t831_iddialogoalerta
        {
            get { return _t831_iddialogoalerta; }
            set { _t831_iddialogoalerta = value; }
        }

        private string _t837_descripcion;
        public string t837_descripcion
        {
            get { return _t837_descripcion; }
            set { _t837_descripcion = value; }
        }

        private string _t837_weblink;
        public string t837_weblink
        {
            get { return _t837_weblink; }
            set { _t837_weblink = value; }
        }

        private string _t837_nombrearchivo;
        public string t837_nombrearchivo
        {
            get { return _t837_nombrearchivo; }
            set { _t837_nombrearchivo = value; }
        }

        private byte[] _t837_archivo;
        public byte[] t837_archivo
        {
            get { return _t837_archivo; }
            set { _t837_archivo = value; }
        }

        private int _t001_idficepi_autor;
        public int t001_idficepi_autor
        {
            get { return _t001_idficepi_autor; }
            set { _t001_idficepi_autor = value; }
        }

        private DateTime _t837_fecha;
        public DateTime t837_fecha
        {
            get { return _t837_fecha; }
            set { _t837_fecha = value; }
        }

        private int _t001_idficepi_modif;
        public int t001_idficepi_modif
        {
            get { return _t001_idficepi_modif; }
            set { _t001_idficepi_modif = value; }
        }

        private DateTime _t837_fechamodif;
        public DateTime t837_fechamodif
        {
            get { return _t837_fechamodif; }
            set { _t837_fechamodif = value; }
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

        public DOCDIALOGO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

		#region Metodos

		public static int Insert(SqlTransaction tr, int t831_iddialogoalerta , string t837_descripcion , string t837_weblink ,
                                 string t837_nombrearchivo, Nullable<long> idContentServer, int t001_idficepi_autor)
		{
            //if (t837_archivo.Length == 0) t837_archivo = null;
            
            SqlParameter[] aParam = new SqlParameter[6];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
			aParam[i++] = ParametroSql.add("@t837_descripcion", SqlDbType.Text, 50, t837_descripcion);
			aParam[i++] = ParametroSql.add("@t837_weblink", SqlDbType.Text, 250, t837_weblink);
			aParam[i++] = ParametroSql.add("@t837_nombrearchivo", SqlDbType.Text, 250, t837_nombrearchivo);
            //aParam[i++] = ParametroSql.add("@t837_archivo", SqlDbType.Binary, 2147483647, t837_archivo);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, idContentServer);
            aParam[i++] = ParametroSql.add("@t001_idficepi_autor", SqlDbType.Int, 4, t001_idficepi_autor);

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCDIALOGO_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCDIALOGO_I", aParam));
		}

		public static int Update(SqlTransaction tr, int t837_id, int t831_iddialogoalerta, string t837_descripcion, string t837_weblink,
                                 string t837_nombrearchivo, Nullable<long> idContentServer, int t001_idficepi_modif)
		{
            //if (t837_archivo.Length == 0) t837_archivo = null;
            
			SqlParameter[] aParam = new SqlParameter[8];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t837_id", SqlDbType.Int, 4, t837_id);
			aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
			aParam[i++] = ParametroSql.add("@t837_descripcion", SqlDbType.Text, 50, t837_descripcion);
			aParam[i++] = ParametroSql.add("@t837_weblink", SqlDbType.Text, 250, t837_weblink);
			aParam[i++] = ParametroSql.add("@t837_nombrearchivo", SqlDbType.Text, 250, t837_nombrearchivo);
            //aParam[i++] = ParametroSql.add("@t837_archivo", SqlDbType.Binary, 2147483647, t837_archivo);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, idContentServer);
			aParam[i++] = ParametroSql.add("@t001_idficepi_modif", SqlDbType.Int, 4, t001_idficepi_modif);

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_DOCDIALOGO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCDIALOGO_U", aParam);
		}

		public static int Delete(SqlTransaction tr, int t837_id)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t837_id", SqlDbType.Int, 4, t837_id);

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_DOCDIALOGO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCDIALOGO_D", aParam);
		}

        public static DOCDIALOGO Select(SqlTransaction tr, int t837_id)//, bool bTraerArchivo) 
		{
			DOCDIALOGO o = new DOCDIALOGO();

			SqlParameter[] aParam = new SqlParameter[1];
			int i = 0;
            aParam[i++] = ParametroSql.add("@t837_id", SqlDbType.Int, 4, t837_id);
            //aParam[i++] = ParametroSql.add("@bArchivo", SqlDbType.Bit, 1, bTraerArchivo);

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_DOCDIALOGO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCDIALOGO_S", aParam);

			if (dr.Read())
			{
				if (dr["t837_id"] != DBNull.Value)
					o.t837_id = int.Parse(dr["t837_id"].ToString());
				if (dr["t831_iddialogoalerta"] != DBNull.Value)
					o.t831_iddialogoalerta = int.Parse(dr["t831_iddialogoalerta"].ToString());
				if (dr["t837_descripcion"] != DBNull.Value)
					o.t837_descripcion = (string)dr["t837_descripcion"];
				if (dr["t837_weblink"] != DBNull.Value)
					o.t837_weblink = (string)dr["t837_weblink"];
				if (dr["t837_nombrearchivo"] != DBNull.Value)
					o.t837_nombrearchivo = (string)dr["t837_nombrearchivo"];
                //El archivo lo obtenemos de Atenea si tiene id de documento y sino del campo image
                //if (bTraerArchivo)
                //{
                //    if (dr["t2_iddocumento"].ToString() == "")
                //        o.t837_archivo = (byte[])dr["t837_archivo"];
                //}
                if (dr["t001_idficepi_autor"] != DBNull.Value)
					o.t001_idficepi_autor = int.Parse(dr["t001_idficepi_autor"].ToString());
				if (dr["t837_fecha"] != DBNull.Value)
					o.t837_fecha = (DateTime)dr["t837_fecha"];
				if (dr["t001_idficepi_modif"] != DBNull.Value)
					o.t001_idficepi_modif = int.Parse(dr["t001_idficepi_modif"].ToString());
				if (dr["t837_fechamodif"] != DBNull.Value)
					o.t837_fechamodif = (DateTime)dr["t837_fechamodif"];
                if (dr["autor"] != DBNull.Value)
                    o.DesAutor = (string)dr["autor"];
                if (dr["autormodif"] != DBNull.Value)
                    o.DesAutorModif = dr["autormodif"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de DOCDIALOGO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        public static SqlDataReader CatalogoDocs(int t831_iddialogoalerta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);

            return SqlHelper.ExecuteSqlDataReader("SUP_DOCDIALOGO_CAT", aParam);
        }



		#endregion
	}
}
