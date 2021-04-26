using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : EMPRESA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T313_EMPRESA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	26/02/2008 11:08:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EMPRESA
    {
        #region Metodos

        //public static SqlDataReader CATALOGO(Nullable<short> T313_IDEMPRESA, string T313_DENOMINACION, byte nOrden, byte nAscDesc)
        //{
        //    SqlParameter[] aParam = new SqlParameter[4];
        //    aParam[0] = new SqlParameter("@T313_IDEMPRESA", SqlDbType.SmallInt, 2);
        //    aParam[0].Value = T313_IDEMPRESA;
        //    aParam[1] = new SqlParameter("@T313_DENOMINACION", SqlDbType.VarChar, 50);
        //    aParam[1].Value = T313_DENOMINACION;

        //    aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
        //    aParam[2].Value = nOrden;
        //    aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
        //    aParam[3].Value = nAscDesc;

        //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
        //    return SqlHelper.ExecuteSqlDataReader("SUP_EMPRESA_CA", aParam);
        //}

        //public static SqlDataReader Catalogo(Nullable<int> t313_idempresa, string t313_denominacion, string t302_codigoexterno, Nullable<bool> t313_ute, Nullable<float> t313_horasanuales, Nullable<float> t313_interesGF, string t313_CCIF, string t313_CCICE, Nullable<int> T069_iddietakm, byte nOrden, byte nAscDesc)
        //{
            //SqlParameter[] aParam = new SqlParameter[11];
            //aParam[0] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            //aParam[0].Value = t313_idempresa;
            //aParam[1] = new SqlParameter("@t313_denominacion", SqlDbType.Text, 50);
            //aParam[1].Value = t313_denominacion;
            //aParam[2] = new SqlParameter("@t302_codigoexterno", SqlDbType.Text, 15);
            //aParam[2].Value = t302_codigoexterno;
            //aParam[3] = new SqlParameter("@t313_ute", SqlDbType.Bit, 1);
            //aParam[3].Value = t313_ute;
            //aParam[4] = new SqlParameter("@t313_horasanuales", SqlDbType.Real, 4);
            //aParam[4].Value = t313_horasanuales;
            //aParam[5] = new SqlParameter("@t313_interesGF", SqlDbType.Real, 4);
            //aParam[5].Value = t313_interesGF;
            //aParam[6] = new SqlParameter("@t313_CCIF", SqlDbType.Text, 4);
            //aParam[6].Value = t313_CCIF;
            //aParam[7] = new SqlParameter("@t313_CCICE", SqlDbType.Text, 4);
            //aParam[7].Value = t313_CCICE;
            //aParam[8] = new SqlParameter("@T069_iddietakm", SqlDbType.Int, 4);
            //aParam[8].Value = T069_iddietakm;

            //aParam[9] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            //aParam[9].Value = nOrden;
            //aParam[10] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            //aParam[10].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            //return SqlHelper.ExecuteSqlDataReader("SUP_EMPRESA_C", aParam);
        //}

        public static SqlDataReader Catalogo(Nullable<bool> t313_activo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t313_activo", SqlDbType.Bit, 1);
            aParam[0].Value = t313_activo; 

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_EMPRESA_C", aParam);
        }

        #endregion
    }
}
