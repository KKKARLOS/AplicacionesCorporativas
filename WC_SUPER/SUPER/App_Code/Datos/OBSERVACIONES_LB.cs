using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER.BLL
	/// Class	 : OBSERVACIONES_LB
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T693_OBSERVACIONES_LB
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/06/2014 9:09:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class OBSERVACIONES_LB
	{
    	#region Metodos

		public static int Insert(SqlTransaction tr, int t305_idproyectosubnodo, int t001_idficepi, bool t693_automatico , string t693_observaciones)
		{
			SqlParameter[] aParam = new SqlParameter[]{
			    ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo),
			    ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
			    ParametroSql.add("@t693_automatico", SqlDbType.Bit, 1, t693_automatico),
			    ParametroSql.add("@t693_observaciones", SqlDbType.Text, 2147483647, t693_observaciones)
            };

			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_OBSERVACIONES_LB_INS", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_OBSERVACIONES_LB_INS", aParam));
		}

		public static int Update(SqlTransaction tr, int t693_idobservacion, string t693_observaciones)
		{
			SqlParameter[] aParam = new SqlParameter[]{
			    ParametroSql.add("@t693_idobservacion", SqlDbType.Int, 4, t693_idobservacion),
			    ParametroSql.add("@t693_observaciones", SqlDbType.Text, 2147483647, t693_observaciones)
            };

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_OBSERVACIONES_LB_UPD", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_OBSERVACIONES_LB_UPD", aParam);
		}

		public static int Delete(SqlTransaction tr, int t693_idobservacion)
		{
			SqlParameter[] aParam = new SqlParameter[]{
			    ParametroSql.add("@t693_idobservacion", SqlDbType.Int, 4, t693_idobservacion)
            };

            if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_OBSERVACIONES_LB_DEL", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_OBSERVACIONES_LB_DEL", aParam);
		}

        public static SqlDataReader Catalogo(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
			    ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_OBSERVACIONES_LB_CAT", aParam);
        }
        public static DataSet CatalogoDS(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{
			    ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };

            return SqlHelper.ExecuteDataset("SUP_OBSERVACIONES_LB_CAT", aParam);
        }

		#endregion
	}
}
