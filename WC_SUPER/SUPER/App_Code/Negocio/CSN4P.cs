using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for CSN3P
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class CSN4P
    {
        #region Propiedades y Atributos

        private string _DesResponsable;
        public string DesResponsable
        {
            get { return _DesResponsable; }
            set { _DesResponsable = value; }
        }
        #endregion
        #region Metodos
        public static int UpdateSimple(SqlTransaction tr, int t491_idcsn4p, byte t491_orden)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
            aParam[0].Value = t491_idcsn4p;
            aParam[1] = new SqlParameter("@t491_orden", SqlDbType.TinyInt, 1);
            aParam[1].Value = t491_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CSN4P_USIM", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN4P_USIM", aParam);
        }

        public static SqlDataReader CatalogoDenominacion(string t491_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t491_denominacion", SqlDbType.Text, 30);
            aParam[0].Value = t491_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN4P_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN4P_CAT_USU", aParam);
        }

        #endregion
    }
}
