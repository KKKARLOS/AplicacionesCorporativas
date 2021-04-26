using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de TIPODATOCAMPO
    /// </summary>
    public class TIPODATOCAMPO
    {
       #region Metodos
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_TIPODATOCAMPOS_C", aParam);
        }
		#endregion
    }
}