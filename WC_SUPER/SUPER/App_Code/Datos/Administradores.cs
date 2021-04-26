using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
/// <summary>
/// Descripción breve de Administradores
/// </summary>
/// 
namespace SUPER.DAL
{
    public class Administradores
    {
        public Administradores()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Devuelve una lista con los administradores de SUPER
        /// </summary>
        /// <param name="t399_entorno"></param>
        /// <returns></returns>
        public static SqlDataReader CatalogoSUPER(string t399_entorno)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t399_entorno", SqlDbType.VarChar, 5, t399_entorno)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_ADMINISTRADORES_CAT2", aParam);
        }
    }
}