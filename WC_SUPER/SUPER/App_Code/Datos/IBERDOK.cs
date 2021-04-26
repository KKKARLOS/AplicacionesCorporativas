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
    /// Descripción breve de IBERDOK
    /// </summary>
    public class IBERDOK
    {
        public IBERDOK()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_IBERDOK_MODELOS_C", aParam);

        }
    }
}