using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for MEDIO
/// </summary>
/// 
namespace GEMO.DAL
{
    public class MEDIO
    {
        #region Propiedades y Atributos

        private short _t134_idmedio;
        public short t134_idmedio
        {
            get { return _t134_idmedio; }
            set { _t134_idmedio = value; }
        }

        private string _t134_denominacion;
        public string t134_denominacion
        {
            get { return _t134_denominacion; }
            set { _t134_denominacion = value; }
        }
        #endregion

        #region Constructor

        public MEDIO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T134_MEDIO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:05:46
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t134_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t134_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t134_denominacion;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GEM_MEDIO_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GEM_MEDIO_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T134_MEDIO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:05:46
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, short t134_idmedio, string t134_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t134_idmedio", SqlDbType.SmallInt, 2);
            aParam[0].Value = t134_idmedio;
            aParam[1] = new SqlParameter("@t134_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t134_denominacion;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_MEDIO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_MEDIO_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T134_MEDIO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/04/2011 16:05:46
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, short t134_idmedio)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t134_idmedio", SqlDbType.SmallInt, 2);
            aParam[0].Value = t134_idmedio;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GEM_MEDIO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_MEDIO_D", aParam);
        }
        public static SqlDataReader Catalogo(string sTipoBusqueda, string t134_denominacion, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t134_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t134_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;

            //aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            //aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            //if (HttpContext.Current.Session["AdminActual"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_MEDIOS_CAT", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("SUP_SEGMENTO_CAT_USU", aParam);
        }
        public static SqlDataReader Catalogo2()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            //aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            //aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            //if (HttpContext.Current.Session["AdminActual"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_MEDIOS_CAT2", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("SUP_SEGMENTO_CAT_USU", aParam);
        }
    }
}