using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : SUBGRUPONAT
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T322_SUBGRUPONAT
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	10/12/2009 11:17:41	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class SUBGRUPONAT
    {
        #region Propiedades y Atributos

        private int _t322_idsubgruponat;
        public int t322_idsubgruponat
        {
            get { return _t322_idsubgruponat; }
            set { _t322_idsubgruponat = value; }
        }

        private string _t322_denominacion;
        public string t322_denominacion
        {
            get { return _t322_denominacion; }
            set { _t322_denominacion = value; }
        }

        private int _t321_idgruponat;
        public int t321_idgruponat
        {
            get { return _t321_idgruponat; }
            set { _t321_idgruponat = value; }
        }

        private int _t322_orden;
        public int t322_orden
        {
            get { return _t322_orden; }
            set { _t322_orden = value; }
        }

        private bool _t322_estado;
        public bool t322_estado
        {
            get { return _t322_estado; }
            set { _t322_estado = value; }
        }
        #endregion

        #region Constructor

        public SUBGRUPONAT()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T322_SUBGRUPONAT.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 11:17:41
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t322_denominacion, int t321_idgruponat, int t322_orden, bool t322_estado)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t322_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t322_denominacion;
            aParam[1] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
            aParam[1].Value = t321_idgruponat;
            aParam[2] = new SqlParameter("@t322_orden", SqlDbType.Int, 4);
            aParam[2].Value = t322_orden;
            aParam[3] = new SqlParameter("@t322_estado", SqlDbType.Bit, 1);
            aParam[3].Value = t322_estado;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SUBGRUPONAT_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SUBGRUPONAT_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T322_SUBGRUPONAT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 11:17:41
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t322_idsubgruponat, string t322_denominacion, int t321_idgruponat, int t322_orden, bool t322_estado)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
            aParam[0].Value = t322_idsubgruponat;
            aParam[1] = new SqlParameter("@t322_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t322_denominacion;
            aParam[2] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
            aParam[2].Value = t321_idgruponat;
            aParam[3] = new SqlParameter("@t322_orden", SqlDbType.Int, 4);
            aParam[3].Value = t322_orden;
            aParam[4] = new SqlParameter("@t322_estado", SqlDbType.Bit, 1);
            aParam[4].Value = t322_estado;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SUBGRUPONAT_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUBGRUPONAT_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T322_SUBGRUPONAT a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 11:17:41
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t322_idsubgruponat)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
            aParam[0].Value = t322_idsubgruponat;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SUBGRUPONAT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUBGRUPONAT_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T322_SUBGRUPONAT,
        /// y devuelve una instancia u objeto del tipo SUBGRUPONAT
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 11:17:41
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SUBGRUPONAT Select(SqlTransaction tr, int t322_idsubgruponat)
        {
            SUBGRUPONAT o = new SUBGRUPONAT();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
            aParam[0].Value = t322_idsubgruponat;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SUBGRUPONAT_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBGRUPONAT_S", aParam);

            if (dr.Read())
            {
                if (dr["t322_idsubgruponat"] != DBNull.Value)
                    o.t322_idsubgruponat = int.Parse(dr["t322_idsubgruponat"].ToString());
                if (dr["t322_denominacion"] != DBNull.Value)
                    o.t322_denominacion = (string)dr["t322_denominacion"];
                if (dr["t321_idgruponat"] != DBNull.Value)
                    o.t321_idgruponat = int.Parse(dr["t321_idgruponat"].ToString());
                if (dr["t322_orden"] != DBNull.Value)
                    o.t322_orden = int.Parse(dr["t322_orden"].ToString());
                if (dr["t322_estado"] != DBNull.Value)
                    o.t322_estado = (bool)dr["t322_estado"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SUBGRUPONAT"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T322_SUBGRUPONAT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	10/12/2009 11:17:41
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t322_idsubgruponat, string t322_denominacion, Nullable<int> t321_idgruponat, Nullable<int> t322_orden, Nullable<bool> t322_estado, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
            aParam[0].Value = t322_idsubgruponat;
            aParam[1] = new SqlParameter("@t322_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t322_denominacion;
            aParam[2] = new SqlParameter("@t321_idgruponat", SqlDbType.Int, 4);
            aParam[2].Value = t321_idgruponat;
            aParam[3] = new SqlParameter("@t322_orden", SqlDbType.Int, 4);
            aParam[3].Value = t322_orden;
            aParam[4] = new SqlParameter("@t322_estado", SqlDbType.Bit, 1);
            aParam[4].Value = t322_estado;

            aParam[5] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[5].Value = nOrden;
            aParam[6] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[6].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_SUBGRUPONAT_C", aParam);
        }

        #endregion
    }
}
