using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : VIAPAGO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la vista: Z_VIA_PAGO
    /// </summary>
    /// <history>
    /// 	Creado por [dotofean]	22/11/2006 9:37:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class VIAPAGO
    {

        #region Propiedades y Atributos

        #endregion

        #region Constructores

        public VIAPAGO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados seg√∫n el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un cat·logo de registros de la vista Z_VIA_PAGO
        /// </summary>
        /// <history>
        /// 	Creado por [dopeotca]	22/11/2006 9:37:14
        /// </history>
        /// -----------------------------------------------------------------------------
        //public static SqlDataReader Cliente(SqlTransaction tr, int t305_idproyectosubnodo, int t302_idcliente)
        public static SqlDataReader Cliente(SqlTransaction tr, string t302_codigoexterno, int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            //aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t302_codigoexterno", SqlDbType.VarChar, 15);
            aParam[1].Value = t302_codigoexterno;
            aParam[2] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[2].Value = t302_idcliente;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_VIAPAGO_CLI", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_VIAPAGO_CLI", aParam);
        }
        public static SqlDataReader Catalogo(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_VIAPAGO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_VIAPAGO_CAT", aParam);
        }
        #endregion
    }
}