using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class AGRUPACIONPROYECTO
	{
		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T623_AGRUPACIONPROYECTO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2010 12:27:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t622_idagrupacion , string sProyectos)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
			aParam[0].Value = t622_idagrupacion;
            aParam[1] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 100);
            aParam[1].Value = sProyectos;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_AGRUPACIONPROYECTO_INS", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AGRUPACIONPROYECTO_INS", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T623_AGRUPACIONPROYECTO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2010 12:27:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int DeleteByAgrupacion(SqlTransaction tr, int t622_idagrupacion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
			aParam[0].Value = t622_idagrupacion;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_AGRUPACIONPROYECTO_DBY_AGR", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AGRUPACIONPROYECTO_DBY_AGR", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T623_AGRUPACIONPROYECTO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2010 12:27:13
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader ObtenerCatalogo(int t622_idagrupacion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
			aParam[0].Value = t622_idagrupacion;

            return SqlHelper.ExecuteSqlDataReader("SUP_AGRUPACIONPROYECTO_CAT", aParam);
		}

        public static string ExisteMismosProyectos(SqlTransaction tr, string sProyectos)
        {
            string sReturn = "";
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 100);
            aParam[0].Value = sProyectos;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_AGRUPACIONPROYECTO_EXI", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_AGRUPACIONPROYECTO_EXI", aParam);

            if (dr.Read())
            {
                if (dr["t622_idagrupacion"].ToString() != "0")
                {
                    sReturn = ((int)dr["t622_idagrupacion"]).ToString("#,###") + " - " + dr["t622_denominacion"].ToString();
                }
            }
            dr.Close();
            dr.Dispose();

            return sReturn;
        }

		#endregion
	}
}
