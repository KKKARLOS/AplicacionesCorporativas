using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de ZONA
    /// </summary>
    public class ZONA
    {
        public ZONA()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

		#region Metodos


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T482_ZONA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t482_denominacion, string t482_codigoexterno, int t481_idambito)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t482_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t482_denominacion;
            aParam[1] = new SqlParameter("@t482_codigoexterno", SqlDbType.Text, 15);
            aParam[1].Value = t482_codigoexterno;
            aParam[2] = new SqlParameter("@t481_idambito", SqlDbType.Int, 4);
            aParam[2].Value = t481_idambito;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ZONA_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ZONA_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T482_ZONA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t482_idzona, string t482_denominacion, string t482_codigoexterno, int t481_idambito)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t482_idzona", SqlDbType.Int, 4);
            aParam[0].Value = t482_idzona;
            aParam[1] = new SqlParameter("@t482_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t482_denominacion;
            aParam[2] = new SqlParameter("@t482_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = t482_codigoexterno;
            aParam[3] = new SqlParameter("@t481_idambito", SqlDbType.Int, 4);
            aParam[3].Value = t481_idambito;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ZONA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ZONA_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T484_SEGMENTO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t482_idzona)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t482_idzona", SqlDbType.Int, 4);
            aParam[0].Value = t482_idzona;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ZONA_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ZONA_D", aParam);
        }
		#endregion
    }
}