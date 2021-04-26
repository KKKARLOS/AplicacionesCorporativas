using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class HISTORIALGASTOSFINANCIEROS
	{
		#region Metodos

		public static void Insert(SqlTransaction tr, int t303_idnodo , int t469_anomes , DateTime t469_fecha , float t469_interesGF , int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t469_anomes", SqlDbType.Int, 4);
			aParam[1].Value = t469_anomes;
			aParam[2] = new SqlParameter("@t469_fecha", SqlDbType.SmallDateTime, 4);
			aParam[2].Value = t469_fecha;
			aParam[3] = new SqlParameter("@t469_interesGF", SqlDbType.Real, 4);
			aParam[3].Value = t469_interesGF;
			aParam[4] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_GASTOSFINANCIEROSHISTORIAL_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GASTOSFINANCIEROSHISTORIAL_I", aParam);
		}

		public static SqlDataReader CatalogoPorNodo(SqlTransaction tr, int t303_idnodo) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_GASTOSFINANCIEROSHISTORIAL_CAT", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GASTOSFINANCIEROSHISTORIAL_CAT", aParam);
		}

		#endregion
	}
}
