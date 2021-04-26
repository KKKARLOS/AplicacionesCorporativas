using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class ORGVENTASSAP
    {
        #region Metodos

        public static SqlDataReader Catalogo()
        { 
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

            return SqlHelper.ExecuteSqlDataReader("SUP_ORGVENTASSAP_SUP", aParam);
        }
        public static SqlDataReader CatalogoSAP()
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

            return SqlHelper.ExecuteSqlDataReader("SUP_ORGVENTASSAP_SAP", aParam);
        }
        #endregion
    }
}
