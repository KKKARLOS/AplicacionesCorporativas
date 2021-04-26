using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for CSN2P
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class CSN2P
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

        public static int UpdateSimple(SqlTransaction tr, int t487_idcsn2p, byte t487_orden)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
            aParam[0].Value = t487_idcsn2p;
            aParam[1] = new SqlParameter("@t487_orden", SqlDbType.TinyInt, 1);
            aParam[1].Value = t487_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CSN2P_USIM", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN2P_USIM", aParam);
        }

        public static SqlDataReader CatalogoDenominacion(string t487_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t487_denominacion", SqlDbType.Text, 30);
            aParam[0].Value = t487_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN2P_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN2P_CAT_USU", aParam);
        }

        #endregion
    }
}
