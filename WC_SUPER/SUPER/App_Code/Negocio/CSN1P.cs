using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for CSN1P
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class CSN1P
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

        public static int UpdateSimple(SqlTransaction tr, int t485_idcsn1p, byte t485_orden)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
            aParam[0].Value = t485_idcsn1p;
            aParam[1] = new SqlParameter("@t485_orden", SqlDbType.TinyInt, 1);
            aParam[1].Value = t485_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CSN1P_USIM", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN1P_USIM", aParam);
        }
        public static SqlDataReader CatalogoDenominacion(string t485_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t485_denominacion", SqlDbType.Text, 30);
            aParam[0].Value = t485_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN1P_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CSN1P_CAT_USU", aParam);
        }

        #endregion
    }
}
