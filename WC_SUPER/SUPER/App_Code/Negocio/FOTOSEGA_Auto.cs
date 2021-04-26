using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FOTOSEGA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T461_FOTOSEGA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	14/01/2009 9:16:21	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FOTOSEGA
    {
        #region Propiedades y Atributos

        private int _t461_idfotosega;
        public int t461_idfotosega
        {
            get { return _t461_idfotosega; }
            set { _t461_idfotosega = value; }
        }

        private int? _t460_idfotosegf;
        public int? t460_idfotosegf
        {
            get { return _t460_idfotosegf; }
            set { _t460_idfotosegf = value; }
        }

        private string _t461_denominacion;
        public string t461_denominacion
        {
            get { return _t461_denominacion; }
            set { _t461_denominacion = value; }
        }

        private int _t461_orden;
        public int t461_orden
        {
            get { return _t461_orden; }
            set { _t461_orden = value; }
        }
        #endregion

        #region Constructor

        public FOTOSEGA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T461_FOTOSEGA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	14/01/2009 9:16:21
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, Nullable<int> t460_idfotosegf, string t461_denominacion, int t461_orden, Nullable<decimal> t461_presupuesto, Nullable<double> t461_porcav, Nullable<decimal> t461_producido)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t460_idfotosegf", SqlDbType.Int, 4);
            aParam[0].Value = t460_idfotosegf;
            aParam[1] = new SqlParameter("@t461_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t461_denominacion;
            aParam[2] = new SqlParameter("@t461_orden", SqlDbType.Int, 4);
            aParam[2].Value = t461_orden;
            aParam[3] = new SqlParameter("@t461_presupuesto", SqlDbType.Money, 8);
            aParam[3].Value = t461_presupuesto;
            aParam[4] = new SqlParameter("@t461_porcav", SqlDbType.Float, 8);
            aParam[4].Value = t461_porcav;
            aParam[5] = new SqlParameter("@t461_producido", SqlDbType.Money, 8);
            aParam[5].Value = t461_producido;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FOTOSEGA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FOTOSEGA_I", aParam));
        }

        #endregion
    }
}
