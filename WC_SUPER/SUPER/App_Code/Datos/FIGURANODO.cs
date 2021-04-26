using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de FIGURANODO
    /// </summary>
    public class FIGURANODO
    {
        public FIGURANODO()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader FigurasUsuario(int t303_idnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURANODO_LISTA", aParam);
        }
    }
}