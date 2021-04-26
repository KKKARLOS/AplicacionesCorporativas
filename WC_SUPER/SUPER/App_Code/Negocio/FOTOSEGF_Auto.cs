using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : FOTOSEGF
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T460_FOTOSEGF
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	14/01/2009 9:12:48	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class FOTOSEGF
    {
        #region Propiedades y Atributos

        private int _t460_idfotosegf;
        public int t460_idfotosegf
        {
            get { return _t460_idfotosegf; }
            set { _t460_idfotosegf = value; }
        }

        private string _t460_denominacion;
        public string t460_denominacion
        {
            get { return _t460_denominacion; }
            set { _t460_denominacion = value; }
        }

        private int _t460_orden;
        public int t460_orden
        {
            get { return _t460_orden; }
            set { _t460_orden = value; }
        }
        #endregion

        #region Constructor

        public FOTOSEGF()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T460_FOTOSEGF.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	14/01/2009 9:12:48
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t460_denominacion, int t460_orden, Nullable<decimal> t460_presupuesto, Nullable<double> t460_porcav, Nullable<decimal> t460_producido)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t460_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t460_denominacion;
            aParam[1] = new SqlParameter("@t460_orden", SqlDbType.Int, 4);
            aParam[1].Value = t460_orden;
            aParam[2] = new SqlParameter("@t460_presupuesto", SqlDbType.Money, 8);
            aParam[2].Value = t460_presupuesto;
            aParam[3] = new SqlParameter("@t460_porcav", SqlDbType.Float, 8);
            aParam[3].Value = t460_porcav;
            aParam[4] = new SqlParameter("@t460_producido", SqlDbType.Money, 8);
            aParam[4].Value = t460_producido;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FOTOSEGF_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FOTOSEGF_I", aParam));
        }

        #endregion
    }
}
