using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : ACONSECT
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T809_ACONSECT
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	08/08/2012 10:05:13	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class ACONSECT
    {
        #region Propiedades y Atributos

        private int _t809_idaconsect;
        public int t809_idaconsect
        {
            get { return _t809_idaconsect; }
            set { _t809_idaconsect = value; }
        }

        private string _t809_denominacion;
        public string t809_denominacion
        {
            get { return _t809_denominacion; }
            set { _t809_denominacion = value; }
        }
        #endregion

        #region Constructor

        public ACONSECT()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T809_ACONSECT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	08/08/2012 10:05:13
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t809_idaconsect, string t809_denominacion, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t809_idaconsect", SqlDbType.Int, 4, t809_idaconsect);
            aParam[i++] = ParametroSql.add("@t809_denominacion", SqlDbType.Text, 50, t809_denominacion);

            aParam[i++] = ParametroSql.add("@nOrden", SqlDbType.TinyInt, 1, nOrden);
            aParam[i++] = ParametroSql.add("@nAscDesc", SqlDbType.TinyInt, 1, nAscDesc);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_ACONSECT_C", aParam);
        }

        #endregion
    }
}
