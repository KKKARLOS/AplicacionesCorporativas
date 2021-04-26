using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class CENCOS
	{
		#region Metodos

		public static SqlDataReader CatalogoByNodo(SqlTransaction tr, int t303_idnodo) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_CENCOS_CbyNodo", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CENCOS_CbyNodo", aParam);
		}

		#endregion
	}
}
