using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : ACONTECNO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T810_ACONTECNO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	08/08/2012 10:05:13	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class ACONTECNO
    {
        #region Propiedades y Atributos

        private int _t810_idacontecno;
        public int t810_idacontecno
        {
            get { return _t810_idacontecno; }
            set { _t810_idacontecno = value; }
        }

        private string _t810_denominacion;
        public string t810_denominacion
        {
            get { return _t810_denominacion; }
            set { _t810_denominacion = value; }
        }
        #endregion

        #region Constructor

        public ACONTECNO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T810_ACONTECNO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	08/08/2012 10:05:13
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t810_idacontecno, string t810_denominacion, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t810_idacontecno", SqlDbType.Int, 4, t810_idacontecno);
            aParam[i++] = ParametroSql.add("@t810_denominacion", SqlDbType.Text, 50, t810_denominacion);

            aParam[i++] = ParametroSql.add("@nOrden", SqlDbType.TinyInt, 1, nOrden);
            aParam[i++] = ParametroSql.add("@nAscDesc", SqlDbType.TinyInt, 1, nAscDesc);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_ACONTECNO_C", aParam);
        }

        #endregion
    }
}
