using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PLANIFAGENDA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T458_PLANIFAGENDA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	12/11/2008 9:45:20	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PLANIFAGENDA
    {
        #region Propiedades y Atributos

        private int _t458_idPlanif;
        public int t458_idPlanif
        {
            get { return _t458_idPlanif; }
            set { _t458_idPlanif = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private int _t001_idficepi_mod;
        public int t001_idficepi_mod
        {
            get { return _t001_idficepi_mod; }
            set { _t001_idficepi_mod = value; }
        }

        private DateTime _t458_fechamod;
        public DateTime t458_fechamod
        {
            get { return _t458_fechamod; }
            set { _t458_fechamod = value; }
        }

        private string _t458_asunto;
        public string t458_asunto
        {
            get { return _t458_asunto; }
            set { _t458_asunto = value; }
        }

        private string _t458_motivo;
        public string t458_motivo
        {
            get { return _t458_motivo; }
            set { _t458_motivo = value; }
        }

        private DateTime _t458_fechoraini;
        public DateTime t458_fechoraini
        {
            get { return _t458_fechoraini; }
            set { _t458_fechoraini = value; }
        }

        private DateTime _t458_fechorafin;
        public DateTime t458_fechorafin
        {
            get { return _t458_fechorafin; }
            set { _t458_fechorafin = value; }
        }

        private int? _t332_idtarea;
        public int? t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }

        private string _t458_privado;
        public string t458_privado
        {
            get { return _t458_privado; }
            set { _t458_privado = value; }
        }

        private string _t458_observaciones;
        public string t458_observaciones
        {
            get { return _t458_observaciones; }
            set { _t458_observaciones = value; }
        }
        #endregion

        #region Constructor

        public PLANIFAGENDA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T458_PLANIFAGENDA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	12/11/2008 9:45:20
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t001_idficepi, int t001_idficepi_mod, DateTime t458_fechamod, string t458_asunto, string t458_motivo, DateTime t458_fechoraini, DateTime t458_fechorafin, Nullable<int> t332_idtarea, string t458_privado, string t458_observaciones)
        {
            SqlParameter[] aParam = new SqlParameter[10];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t001_idficepi_mod", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi_mod;
            aParam[2] = new SqlParameter("@t458_fechamod", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t458_fechamod;
            aParam[3] = new SqlParameter("@t458_asunto", SqlDbType.Text, 50);
            aParam[3].Value = t458_asunto;
            aParam[4] = new SqlParameter("@t458_motivo", SqlDbType.Text, 2147483647);
            aParam[4].Value = t458_motivo;
            aParam[5] = new SqlParameter("@t458_fechoraini", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = t458_fechoraini;
            aParam[6] = new SqlParameter("@t458_fechorafin", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t458_fechorafin;
            aParam[7] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[7].Value = t332_idtarea;
            aParam[8] = new SqlParameter("@t458_privado", SqlDbType.Text, 2147483647);
            aParam[8].Value = t458_privado;
            aParam[9] = new SqlParameter("@t458_observaciones", SqlDbType.Text, 2147483647);
            aParam[9].Value = t458_observaciones;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANIFAGENDA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANIFAGENDA_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T458_PLANIFAGENDA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	12/11/2008 9:45:20
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t458_idPlanif, int t001_idficepi, int t001_idficepi_mod, DateTime t458_fechamod, string t458_asunto, string t458_motivo, DateTime t458_fechoraini, DateTime t458_fechorafin, Nullable<int> t332_idtarea, string t458_privado, string t458_observaciones)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@t458_idPlanif", SqlDbType.Int, 4);
            aParam[0].Value = t458_idPlanif;
            aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi;
            aParam[2] = new SqlParameter("@t001_idficepi_mod", SqlDbType.Int, 4);
            aParam[2].Value = t001_idficepi_mod;
            aParam[3] = new SqlParameter("@t458_fechamod", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = t458_fechamod;
            aParam[4] = new SqlParameter("@t458_asunto", SqlDbType.Text, 50);
            aParam[4].Value = t458_asunto;
            aParam[5] = new SqlParameter("@t458_motivo", SqlDbType.Text, 2147483647);
            aParam[5].Value = t458_motivo;
            aParam[6] = new SqlParameter("@t458_fechoraini", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t458_fechoraini;
            aParam[7] = new SqlParameter("@t458_fechorafin", SqlDbType.SmallDateTime, 4);
            aParam[7].Value = t458_fechorafin;
            aParam[8] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[8].Value = t332_idtarea;
            aParam[9] = new SqlParameter("@t458_privado", SqlDbType.Text, 2147483647);
            aParam[9].Value = t458_privado;
            aParam[10] = new SqlParameter("@t458_observaciones", SqlDbType.Text, 2147483647);
            aParam[10].Value = t458_observaciones;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PLANIFAGENDA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANIFAGENDA_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T458_PLANIFAGENDA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	12/11/2008 9:45:20
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t458_idPlanif)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t458_idPlanif", SqlDbType.Int, 4);
            aParam[0].Value = t458_idPlanif;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PLANIFAGENDA_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANIFAGENDA_D", aParam);
        }


        #endregion
    }
}
