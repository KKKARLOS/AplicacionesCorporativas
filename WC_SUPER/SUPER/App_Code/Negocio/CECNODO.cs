using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CECNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T381_CECNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	21/08/2009 13:13:00	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CECNODO
	{
		#region Metodos
        public static bool Existe(SqlTransaction tr, int t345_idcec, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;

            return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTE_CECNODO", aParam)) == 0) ? false : true;
        }

		#endregion
	}
}
