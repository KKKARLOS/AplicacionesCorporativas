using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
	public class TEXTODIALOGOALERTAS
	{
        public static void Insertar(SqlTransaction tr, int t831_iddialogoalerta, 
            Nullable<int> t314_idusuario_redactor, 
            string t832_texto, 
            string t832_posicion, 
            bool bUsuarioResponsableProy)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t831_iddialogoalerta", SqlDbType.Int, 4, t831_iddialogoalerta);
			aParam[i++] = ParametroSql.add("@t314_idusuario_redactor", SqlDbType.Int, 4, t314_idusuario_redactor);
			aParam[i++] = ParametroSql.add("@t832_texto", SqlDbType.Text, 16, t832_texto);
            aParam[i++] = ParametroSql.add("@t832_posicion", SqlDbType.Text, 1, t832_posicion);
            aParam[i++] = ParametroSql.add("@bUsuarioResponsableProy", SqlDbType.Bit, 1, bUsuarioResponsableProy);

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				SqlHelper.ExecuteScalar("SUP_TEXTODIALOGOALERTAS_INS", aParam);
			else
				SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TEXTODIALOGOALERTAS_INS", aParam);
		}
	}
}
