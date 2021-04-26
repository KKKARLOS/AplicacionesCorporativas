using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : Cliente
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: cliente
	/// </summary>
	/// <history>
	/// 	Creado por [DOTOFEAN]	05/10/2006 9:59:41	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class HORIZONTAL
	{

		#region Propiedades y Atributos

		#endregion

		#region Constructores


		#endregion

		#region Metodos

        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_HORIZONTAL_CT", aParam);
        }
        public static SqlDataReader DeUnResponsable(Nullable<int> idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_HORIZONTAL_RESPON", aParam);
        }
        public static SqlDataReader CatalogoDenominacion(string t307_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t307_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t307_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_HORIZONTAL_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_HORIZONTAL_CAT_USU", aParam);
        }

		#endregion
	}
}
