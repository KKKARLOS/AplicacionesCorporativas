using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for CSN3P
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class CSN3P
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
        public static int UpdateSimple(SqlTransaction tr, int t489_idcsn3p, byte t489_orden)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
            aParam[0].Value = t489_idcsn3p;
            aParam[1] = new SqlParameter("@t489_orden", SqlDbType.TinyInt, 1);
            aParam[1].Value = t489_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CSN3P_USIM", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN3P_USIM", aParam);
        }
        public static SqlDataReader CatalogoDenominacion(string t489_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t489_denominacion", SqlDbType.Text, 30);
            aParam[0].Value = t489_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN3P_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN3P_CAT_USU", aParam);
        }

        #endregion
    }
}
