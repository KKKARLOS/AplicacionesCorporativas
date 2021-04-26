using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : CONDPAGO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la vista: 
    /// 
    /// </summary>
    /// <history>
    /// 	Creado por [dotofean]	22/11/2006 9:37:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class CONDPAGO
    {

        #region Propiedades y Atributos

        #endregion

        #region Constructores

        public CONDPAGO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados seg√∫n el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un cat·logo de registros de la vista Z_CONDICION_PAGO
        /// </summary>
        /// <history>
        /// 	Creado por [dopeotca]	22/11/2006 9:37:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            
            return SqlHelper.ExecuteSqlDataReader("SUP_CONDPAGO_C", aParam);
        }

        public static string CondicionPorDefecto(int t302_idcliente, string idovsap)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente;
            aParam[2] = new SqlParameter("@idovsap", SqlDbType.VarChar, 4);
            aParam[2].Value = idovsap;

            return SqlHelper.ExecuteScalar("SUP_CONDPAGODEF", aParam).ToString();
        }

        #endregion
    }
}