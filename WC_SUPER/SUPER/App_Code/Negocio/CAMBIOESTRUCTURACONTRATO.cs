using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CAMBIOESTRUCTURACONTRATO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T468_CAMBIOESTRUCTURACONTRATO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/03/2009 15:38:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CAMBIOESTRUCTURACONTRATO
	{
		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T468_CAMBIOESTRUCTURACONTRATO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	18/03/2009 15:38:53
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insertar(SqlTransaction tr, int t306_idcontrato, Nullable<int> t303_idnodo_destino, 
                                    Nullable<int> t314_idusuario_responsable, Nullable<int> t314_idusuario_gestor, 
                                    Nullable<int> t302_cliente, Nullable<int> t314_idusuario_comercial,
                                    string t468_arrastraproy, string t468_arrastra_gestor, string t468_arrastra_cliente,
                                    Nullable<bool> t468_procesado, string t468_excepcion)
		{
			SqlParameter[] aParam = new SqlParameter[11];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;
            aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario_responsable;
            aParam[3] = new SqlParameter("@t314_idusuario_gestor", SqlDbType.Int, 4);
            aParam[3].Value = t314_idusuario_gestor;
            aParam[4] = new SqlParameter("@t302_cliente", SqlDbType.Int, 4);
            aParam[4].Value = t302_cliente;
            aParam[5] = new SqlParameter("@t314_idusuario_comercial", SqlDbType.Int, 4);
            aParam[5].Value = t314_idusuario_comercial;
            aParam[6] = new SqlParameter("@t468_arrastraproy", SqlDbType.Text, 1);
            aParam[6].Value = t468_arrastraproy;
            aParam[7] = new SqlParameter("@t468_arrastra_gestor", SqlDbType.Text, 1);
            aParam[7].Value = t468_arrastra_gestor;
            aParam[8] = new SqlParameter("@t468_arrastra_cliente", SqlDbType.Text, 1);
            aParam[8].Value = t468_arrastra_cliente;
            aParam[9] = new SqlParameter("@t468_procesado", SqlDbType.Bit, 1);
			aParam[9].Value = t468_procesado;
			aParam[10] = new SqlParameter("@t468_excepcion", SqlDbType.Text, 2147483647);
			aParam[10].Value = t468_excepcion;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURACONTRATO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T468_CAMBIOESTRUCTURACONTRATO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/03/2009 15:38:53
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static int Modificar(SqlTransaction tr, int t306_idcontrato, Nullable<int> t303_idnodo_destino, string t468_arrastraproy, Nullable<bool> t468_procesado, string t468_excepcion)
        //{
        //    SqlParameter[] aParam = new SqlParameter[5];
        //    aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
        //    aParam[0].Value = t306_idcontrato;
        //    aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
        //    aParam[1].Value = t303_idnodo_destino;
        //    aParam[2] = new SqlParameter("@t468_arrastraproy", SqlDbType.Text, 1);
        //    aParam[2].Value = t468_arrastraproy;
        //    aParam[3] = new SqlParameter("@t468_procesado", SqlDbType.Bit, 1);
        //    aParam[3].Value = t468_procesado;
        //    aParam[4] = new SqlParameter("@t468_excepcion", SqlDbType.Text, 2147483647);
        //    aParam[4].Value = t468_excepcion;

        //    // Ejecuta la query y devuelve el numero de registros modificados.
        //    if (tr == null)
        //        return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURACONTRATO_U", aParam);
        //    else
        //        return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_U", aParam);
        //}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T468_CAMBIOESTRUCTURACONTRATO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/03/2009 15:38:53
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int DeleteAll(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURACONTRATO_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_DEL", aParam);
        }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T468_CAMBIOESTRUCTURACONTRATO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/03/2009 15:38:53
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoDestino()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURACONTRATO_CAT", aParam);
        }

        public static bool bHayAparcadas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CAMBIOESTRUCTURACONTRATO_COUNT", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_COUNT", aParam)) == 0) ? false : true;
        }

        public static int Delete(SqlTransaction tr, int t306_idcontrato)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato)
            };

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURACONTRATO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_D", aParam);
        }

		#endregion
	}
}
