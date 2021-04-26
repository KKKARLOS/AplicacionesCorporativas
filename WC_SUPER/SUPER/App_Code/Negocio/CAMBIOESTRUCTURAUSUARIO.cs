using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CAMBIOESTRUCTURAUSUARIO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T466_CAMBIOESTRUCTURAUSUARIO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/03/2009 15:37:50	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CAMBIOESTRUCTURAUSUARIO
	{
		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T466_CAMBIOESTRUCTURAUSUARIO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	11/03/2009 15:37:50
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insertar(SqlTransaction tr, int t314_idusuario, Nullable<int> t303_idnodo_destino, int t466_anomes, Nullable<bool> t466_procesado)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;
            aParam[2] = new SqlParameter("@t466_anomes", SqlDbType.Int, 4);
			aParam[2].Value = t466_anomes;
			aParam[3] = new SqlParameter("@t466_procesado", SqlDbType.Bit, 1);
			aParam[3].Value = t466_procesado;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAUSUARIO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T466_CAMBIOESTRUCTURAUSUARIO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/03/2009 15:37:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int DeleteAll(SqlTransaction tr)
		{
			SqlParameter[] aParam = new SqlParameter[0];

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAUSUARIO_DEL", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_DEL", aParam);
		}

        public static int Modificar(SqlTransaction tr, int t314_idusuario, Nullable<int> t303_idnodo_destino, int t466_anomes, Nullable<bool> t466_procesado, string t466_excepcion)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;
            aParam[2] = new SqlParameter("@t466_anomes", SqlDbType.Int, 4);
            aParam[2].Value = t466_anomes;
            aParam[3] = new SqlParameter("@t466_procesado", SqlDbType.Bit, 1);
            aParam[3].Value = t466_procesado;
            aParam[4] = new SqlParameter("@t466_excepcion", SqlDbType.Text, 2147483647);
            aParam[4].Value = t466_excepcion;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAUSUARIO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_U", aParam);
        }
        
        public static bool bHayAparcadas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CAMBIOESTRUCTURAUSUARIO_COUNT", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_COUNT", aParam)) == 0) ? false : true;
        }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T466_CAMBIOESTRUCTURAUSUARIO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	11/03/2009 15:37:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader CatalogoDestino()
		{
			SqlParameter[] aParam = new SqlParameter[0];

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURAUSUARIO_CAT", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprobación de que no hay consumos (IAP o CONSPERMES) en meses abiertos anteriores al mes valor
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static bool HayConsumosMesesAbiertos(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[2].Value = nAnomesValor;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CAMBIOESTRUCTURA_USUARIO_00", aParam))>0)?true:false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_00", aParam)) > 0) ? true : false;
        }

        public static DataSet Caso_2_1_1(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_1_1", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_1_1", aParam);
        }
        public static DataSet Caso_2_1_2(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_1_2", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_1_2", aParam);
        }
        public static DataSet Caso_2_1_3(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_1_3", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_1_3", aParam);
        }
        public static void Caso_2_2_1(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_2_1", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_2_1", aParam);
        }
        public static void Caso_2_2_2(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_2_2", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_2_2", aParam);
        }
        public static DataSet Caso_2_2_3(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_2_3", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_2_3", aParam);
        }
        public static void Caso_2_3_1(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_3_1", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_3_1", aParam);
        }
        public static DataSet Caso_2_3_2(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_3_2", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_3_2", aParam);
        }
        public static DataSet Caso_2_4(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen, int t303_idnodo_destino, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;
            aParam[2] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_destino;
            aParam[3] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[3].Value = nAnomesValor;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_4", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_4", aParam);
        }
        public static void Caso_2_5(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_5", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_5", aParam);
        }
        public static void Caso_2_6(SqlTransaction tr, int t314_idusuario, int t303_idnodo_origen)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_6", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_6", aParam);
        }
        public static void Caso_2_7(SqlTransaction tr, int t314_idusuario, int nAnomesValor)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nAnomesValor", SqlDbType.Int, 4);
            aParam[1].Value = nAnomesValor;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_7", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_USUARIO_CASO_2_7", aParam);
        }


        #endregion
    }
}
