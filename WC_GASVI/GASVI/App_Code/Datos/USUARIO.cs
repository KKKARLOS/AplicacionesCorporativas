using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
	/// Class	 : MONEDA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T422_MONEDA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/03/2011 10:02:40	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class USUARIO
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
        public static SqlDataReader ObtenerDatosNuevaNota(SqlTransaction tr, int t314_idusuario, int t313_idempresa)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_DATOSUSUARIO_O2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_DATOSUSUARIO_O2", aParam);
        }

        public static SqlDataReader ObtenerFigurasAcceso(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_FIGURASACCESO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_FIGURASACCESO", aParam);
        }

        public static SqlDataReader ObtenerBeneficiarios(SqlTransaction tr, 
            int t001_idficepi,
            bool bRolTramitador,
            bool bRolSuperTramitador,
            string t001_nombre,
            string t001_apellido1,
            string t001_apellido2,
            bool bMostrarBajas,
            bool bAdministrador)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@bRolTramitador", SqlDbType.Bit, 1, bRolTramitador);
            aParam[i++] = ParametroSql.add("@bRolSuperTramitador", SqlDbType.Bit, 1, bRolSuperTramitador);
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 20, t001_nombre);
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@bMostrarBajas", SqlDbType.Bit, 1, bMostrarBajas);
            aParam[i++] = ParametroSql.add("@bAdministrador", SqlDbType.Bit, 1, bAdministrador);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_BENEFICIARIO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_BENEFICIARIO", aParam);
        }
        #endregion
	}
}
