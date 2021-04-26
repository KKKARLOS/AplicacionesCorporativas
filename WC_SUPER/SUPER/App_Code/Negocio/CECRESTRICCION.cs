using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : CECRESTRICCION
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T319_CECRESTRICCION
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	21/08/2009 13:16:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CECRESTRICCION
	{

		#region Metodos
        public static bool Existe(SqlTransaction tr,  int t345_idcec, int t303_idnodo, int t435_idvcec)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;
            aParam[2] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
            aParam[2].Value = t435_idvcec;

            return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTE_CECRESTRICCION", aParam)) == 0) ? false : true;
        }

		#endregion
	}
}
