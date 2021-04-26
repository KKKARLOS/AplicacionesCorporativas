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
    /// Class	 : MODALIDADCONTRATO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T316_MODALIDADCONTRATO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	19/12/2007 15:07:29	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class MODALIDADCONTRATO
    {
        #region Metodos

        public static SqlDataReader CatalogoDenominacion(string t316_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t316_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t316_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_MODALIDADCONTRATO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_MODALIDADCONTRATO_CAT_USU", aParam);
        }
        public static SqlDataReader Catalogo(int t303_idnodo, bool bTodos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@bTodos", SqlDbType.Bit, 1);
            aParam[1].Value = bTodos;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_MODALIDADCONTRATO_SOP", aParam);
        }
        #endregion
    }
}
