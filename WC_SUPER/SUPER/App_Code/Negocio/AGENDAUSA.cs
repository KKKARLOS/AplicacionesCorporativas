using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AGENDAUSA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T641_AGENDAUSA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	17/01/2011 11:39:48	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AGENDAUSA
	{
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T639_ESPACIOCOMUNICACION.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/01/2011 13:04:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AGENDAUSA_C2", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T641_AGENDAUSA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	17/01/2011 11:39:48
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t641_idagendausa, string t641_consumos, string t641_produccion, string t641_facturacion, string t641_otros)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t641_idagendausa", SqlDbType.Int, 4);
            aParam[0].Value = t641_idagendausa;
            aParam[1] = new SqlParameter("@t641_consumos", SqlDbType.Text, 2147483647);
            aParam[1].Value = t641_consumos;
            aParam[2] = new SqlParameter("@t641_produccion", SqlDbType.Text, 2147483647);
            aParam[2].Value = t641_produccion;
            aParam[3] = new SqlParameter("@t641_facturacion", SqlDbType.Text, 2147483647);
            aParam[3].Value = t641_facturacion;
            aParam[4] = new SqlParameter("@t641_otros", SqlDbType.Text, 2147483647);
            aParam[4].Value = t641_otros;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_AGENDAUSA_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AGENDAUSA_U2", aParam);
        }


        public static void CrearAgendaUSAAuto(SqlTransaction tr, int t314_idusuario, int anomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@anomes", SqlDbType.Int, 4, anomes);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CREARAGENDAUSA_AUTO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CREARAGENDAUSA_AUTO", aParam);
        }

        #endregion
    }
}
