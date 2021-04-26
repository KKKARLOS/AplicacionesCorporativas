using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : BONO_TRANSPORTE
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T655_BONO_TRANSPORTE
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	05/05/2011 17:31:41	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class BONO_TRANSPORTE
    {
        #region Propiedades y Atributos

        private int _t655_idBono;
        public int t655_idBono
        {
            get { return _t655_idBono; }
            set { _t655_idBono = value; }
        }

        private string _t655_denominacion;
        public string t655_denominacion
        {
            get { return _t655_denominacion; }
            set { _t655_denominacion = value; }
        }

        private string _t655_descripcion;
        public string t655_descripcion
        {
            get { return _t655_descripcion; }
            set { _t655_descripcion = value; }
        }

        private string _t655_estado;
        public string t655_estado
        {
            get { return _t655_estado; }
            set { _t655_estado = value; }
        }
        #endregion

        #region Constructor

        public BONO_TRANSPORTE()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T655_BONO_TRANSPORTE.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/05/2011 17:31:41
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(DateTime dtDesde, DateTime dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t657_desde", SqlDbType.SmallDateTime, 8);
            aParam[0].Value = dtDesde;
            aParam[1] = new SqlParameter("@t657_hasta", SqlDbType.SmallDateTime, 8);
            aParam[1].Value = dtHasta;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_BONO_TRANSPORTE_C2", aParam);
        }

        public static SqlDataReader ObtenerParaAsignacion(SqlTransaction tr, int t314_idusuario, bool bSoloVigentes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bSoloVigentes", SqlDbType.Bit, 1, bSoloVigentes);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_BONO_TRANSPORTE_ASIG", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_BONO_TRANSPORTE_ASIG", aParam);
        }

        #endregion
    }
}
