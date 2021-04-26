using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : USUARIOGV
	/// 
	/// -----------------------------------------------------------------------------
	public partial class USUARIOGV
	{
		#region Metodos

        public static SqlDataReader ObtenerDatosNuevaNota(SqlTransaction tr, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_DATOSUSUARIO_O", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_DATOSUSUARIO_O", aParam);
		}

        public static SqlDataReader ObtenerEmpresasTerritorios(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_EMPRESASPROFESIONAL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_EMPRESASPROFESIONAL", aParam);
        }

		#endregion
	}
}
