using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
namespace SUPER.Capa_Negocio
{

    /// <summary>
    /// Summary description for USUARIOPSN_BONOTRANS
    /// </summary>
    public partial class USUARIOPSN_BONOTRANS
    {
        public static SqlDataReader ObtenerProfesionales(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPSN_BONOTRANS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIOPSN_BONOTRANS_CAT", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T665_USUARIOPSN_BONOTRANS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/05/2011 17:40:12
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(Nullable<int> t655_idbono, int t305_idproyectosubnodo, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t655_idbono", SqlDbType.Int, 4);
            aParam[0].Value = t655_idbono;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[2].Value = nOrden;
            aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[3].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (t655_idbono == null)
            {
                return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPSN_BONOTRANS_C3", aParam);
            }
            else
            {
                return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPSN_BONOTRANS_C2", aParam);
            }
        }
    }
}