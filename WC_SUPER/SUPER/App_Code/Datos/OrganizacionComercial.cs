using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de OrganizacionComercial
    /// </summary>
    public class OrganizacionComercial
    {
        public OrganizacionComercial()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Obtoene un catálogo de organizaciones comerciales activas y no ligadas a nodo
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader NoLigadasNodo()
        {
            SqlParameter[] aParam = new SqlParameter[] { };

            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_ORGCOM_SINNODO_C", aParam);

        }
        /// <summary>
        /// Devuelve la denominación del primer nodo  con la misma organización comercial
        /// Si no existe, devuelve cadena vacía
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t303_idnodo"></param>
        /// <param name="ta212_idorganizacioncomercial"></param>
        /// <returns></returns>
        public static string NodosOrgCom(SqlTransaction tr, int t303_idnodo, int ta212_idorganizacioncomercial)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo),
                ParametroSql.add("@ta212_idorganizacioncomercial", SqlDbType.Int, 4, ta212_idorganizacioncomercial)
            };
            string sRes = "";
            SqlDataReader dr = SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_ORGCOM_OTROSNODOS_C", aParam);
            if (dr.Read())
                sRes = dr["t303_denominacion"].ToString();
            dr.Close();
            dr.Dispose();
            
            return sRes;
        }
    }
}