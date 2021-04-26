using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de EXPFICEPIENTORNO
    /// </summary>
    public partial class EXPFICEPIENTORNO
    {
        public EXPFICEPIENTORNO()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T815_EXPFICEPIENTORNO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	29/08/2012 13:53:26
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t813_idexpficepiperfil, int t036_idcodentorno)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			int i = 0;
            aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);

			if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_EXPFICEPIENTORNO_I", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPFICEPIENTORNO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
        /// Elimina un registro de la tabla T815_EXPFICEPIENTORNO a traves de la primary key.
		/// </summary>
		/// <history>
        /// 	Creado por [sqladmin]	29/08/2012 13:53:26
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t813_idexpficepiperfil, int t036_idcodentorno)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			int i = 0;
            aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPFICEPIENTORNO_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPFICEPIENTORNO_D", aParam);
		}

        #endregion
    }
}