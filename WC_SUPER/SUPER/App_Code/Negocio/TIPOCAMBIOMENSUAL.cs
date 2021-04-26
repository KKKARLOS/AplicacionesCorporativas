using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class TIPOCAMBIOMENSUAL
    {
        #region Metodos
        public static SqlDataReader Catalogo(string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[0].Value = t422_idmoneda;
            return SqlHelper.ExecuteSqlDataReader("SUP_HISTORICO_MONEDASUPER", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T699_TIPOCAMBIOMENSUAL.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/01/2012 17:34:53
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, string t422_idmoneda, int t699_anomes, decimal t699_tipocambio)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.Text, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t699_anomes", SqlDbType.Int, 4, t699_anomes);
            aParam[i++] = ParametroSql.add("@t699_tipocambio", SqlDbType.Money, 4, t699_tipocambio);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TIPOCAMBIOMENSUAL_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOCAMBIOMENSUAL_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Dada una serie y nº de factura obtiene los tipos de cambio activos de todas las monedas para el año mes de la factura
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	20/03/2012 17:34:53
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader ListaMes(string serie, string numero)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t376_seriefactura", SqlDbType.VarChar, 5, serie);
            aParam[i++] = ParametroSql.add("@t376_numerofactura", SqlDbType.Int, 4, numero);
            return SqlHelper.ExecuteSqlDataReader("SUP_TIPOCAMBIOMENSUAL_CAT", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Dada un anomes obtiene los tipos de cambio activos de todas las monedas para ese anomes
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	20/03/2012 17:34:53
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader ListaMes(int iAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_anomes", SqlDbType.Int, 4, iAnomes);
            return SqlHelper.ExecuteSqlDataReader("SUP_TIPOCAMBIOMENSUAL_CAT2", aParam);
        }

		#endregion
	}
}
