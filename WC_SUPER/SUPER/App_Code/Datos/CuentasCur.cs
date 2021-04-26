using System.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    public partial class CUENTASCUR
    {
        #region Propiedades y Atributos

        #endregion

        #region Metodos

        public static SqlDataReader Catalogo(string sDenominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@denominacion", SqlDbType.VarChar, 50, sDenominacion);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("CU_CATALOGO", aParam);
        }
        public static int Insert(SqlTransaction tr, string cu_nombre, decimal cu_vn, bool cu_escliente)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@cu_nombre", SqlDbType.Text, 50);
            aParam[0].Value = cu_nombre;
            aParam[1] = new SqlParameter("@cu_vn", SqlDbType.Money, 8);
            aParam[1].Value = cu_vn;
            aParam[2] = new SqlParameter("@cu_escliente", SqlDbType.Bit, 1);
            aParam[2].Value = cu_escliente;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("CU_CTA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "CU_CTA_I", aParam));
        }
        public static int Insert(SqlTransaction tr, string cu_nombre, decimal cu_vn, bool cu_escliente, Nullable<DateTime> cu_fecha, Nullable<int> t484_idsegmento)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@cu_nombre", SqlDbType.Text, 50);
            aParam[0].Value = cu_nombre;
            aParam[1] = new SqlParameter("@cu_vn", SqlDbType.Money, 8);
            aParam[1].Value = cu_vn;
            aParam[2] = new SqlParameter("@cu_escliente", SqlDbType.Bit, 1);
            aParam[2].Value = cu_escliente;
            aParam[3] = new SqlParameter("@cu_fecha", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = cu_fecha;
            aParam[4] = new SqlParameter("@t484_idsegmento", SqlDbType.Int, 4);
            aParam[4].Value = t484_idsegmento;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("CU_CTA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "CU_CTA_I", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T450_CATEGSUPER.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/09/2009 13:17:04
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int cu_id, string cu_nombre, decimal cu_vn, bool cu_escliente)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@cu_id", SqlDbType.Int, 4);
            aParam[0].Value = cu_id;
            aParam[1] = new SqlParameter("@cu_nombre", SqlDbType.Text, 50);
            aParam[1].Value = cu_nombre;
            aParam[2] = new SqlParameter("@cu_vn", SqlDbType.Money, 8);
            aParam[2].Value = cu_vn;
            aParam[3] = new SqlParameter("@cu_escliente", SqlDbType.Bit, 1);
            aParam[3].Value = cu_escliente;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("CU_CTA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CU_CTA_U", aParam);
        }

        public static int Update(SqlTransaction tr, int cu_id, string cu_nombre, decimal cu_vn, bool cu_escliente, Nullable<DateTime> cu_fecha, Nullable<int> t484_idsegmento)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@cu_id", SqlDbType.Int, 4);
            aParam[0].Value = cu_id;
            aParam[1] = new SqlParameter("@cu_nombre", SqlDbType.Text, 50);
            aParam[1].Value = cu_nombre;
            aParam[2] = new SqlParameter("@cu_vn", SqlDbType.Money, 8);
            aParam[2].Value = cu_vn;
            aParam[3] = new SqlParameter("@cu_escliente", SqlDbType.Bit, 1);
            aParam[3].Value = cu_escliente;
            aParam[4] = new SqlParameter("@cu_fecha", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = cu_fecha;
            aParam[5] = new SqlParameter("@t484_idsegmento", SqlDbType.Int, 4);
            aParam[5].Value = t484_idsegmento;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("CU_CTA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CU_CTA_U", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T450_CATEGSUPER a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/09/2009 13:17:04
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int cu_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@cu_id", SqlDbType.Int, 4);
            aParam[0].Value = cu_id;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("CU_CTA_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CU_CTA_D", aParam);
        }

        #endregion
    }
}
