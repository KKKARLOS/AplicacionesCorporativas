using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : CONCEPTOECO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T328_CONCEPTOECO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	30/04/2008 11:30:40	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class CONCEPTOECO
    {

        #region Propiedades y Atributos

        private byte _t328_idconceptoeco;
        public byte t328_idconceptoeco
        {
            get { return _t328_idconceptoeco; }
            set { _t328_idconceptoeco = value; }
        }

        private string _t328_denominacion;
        public string t328_denominacion
        {
            get { return _t328_denominacion; }
            set { _t328_denominacion = value; }
        }

        private byte _t328_orden;
        public byte t328_orden
        {
            get { return _t328_orden; }
            set { _t328_orden = value; }
        }

        private byte _t327_idsubgrupoeco;
        public byte t327_idsubgrupoeco
        {
            get { return _t327_idsubgrupoeco; }
            set { _t327_idsubgrupoeco = value; }
        }

        #endregion


        #region Constructores

        public CONCEPTOECO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion


        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla T328_CONCEPTOECO en función de una foreign key.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	30/04/2008 11:30:40
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByT327_idsubgrupoeco(SqlTransaction tr, byte t327_idsubgrupoeco, string sCualidad, bool bAdmin, bool bEsPIG, bool bEsReplicable, bool bEsMantenimiento)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t327_idsubgrupoeco", SqlDbType.TinyInt, 1);
            aParam[0].Value = t327_idsubgrupoeco;
            aParam[1] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[1].Value = sCualidad;
            aParam[2] = new SqlParameter("@bEsAdmin", SqlDbType.Bit, 1);
            aParam[2].Value = bAdmin;
            aParam[3] = new SqlParameter("@bEsPIG", SqlDbType.Bit, 1);
            aParam[3].Value = bEsPIG;
            aParam[4] = new SqlParameter("@bEsReplicable", SqlDbType.Bit, 1);
            aParam[4].Value = bEsReplicable;
            aParam[5] = new SqlParameter("@esMantenimiento", SqlDbType.Bit, 1);
            aParam[5].Value = bEsMantenimiento;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONCEPTOECO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONCEPTOECO_CAT", aParam);
        }
        public static SqlDataReader Obtener( Nullable<byte> t326_idgrupoeco, Nullable<byte> t327_idsubgrupoeco)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t326_idgrupoeco", SqlDbType.TinyInt, 2);
            aParam[0].Value = t326_idgrupoeco;
            aParam[1] = new SqlParameter("@t327_idsubgrupoeco", SqlDbType.TinyInt, 2);
            aParam[1].Value = t327_idsubgrupoeco;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONCEPTOS_ECONOMICOS", aParam);
        }

        public static decimal GetImporte(SqlTransaction tr, int idConcepto, int nPSN, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t328_idconceptoeco", SqlDbType.Int, 4, idConcepto),
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, nPSN),
                ParametroSql.add("@t325_anomes", SqlDbType.Int, 4, nAnomes)
            };
            if (tr == null)
                return Convert.ToDecimal(SqlHelper.ExecuteScalar("SUP_CONCEPTOECO_IMPORTE_S", aParam));
            else
                return Convert.ToDecimal(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONCEPTOECO_IMPORTE_S", aParam));
        }

        #endregion
    }
}
