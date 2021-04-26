using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class PROYECTO
    {
        #region Metodos

        public static SqlDataReader ObtenerCatalogoCreacionNota(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETPROYECTOSCREARNOTA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETPROYECTOSCREARNOTA_CAT", aParam);

        }

        public static SqlDataReader ObtenerCatalogoConsulta(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETPROYECTOSCONSULTA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETPROYECTOSCONSULTA", aParam);

        }

        public static SqlDataReader CatalogoProyectosConSolicitudes(Nullable<int> idNodo, 
            string sEstado, 
            string sCategoria,
            Nullable<int> idCliente, 
            Nullable<int> idResponsable, 
            Nullable<int> numPE, 
            string sDesPE,
            string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idNodo", SqlDbType.Int, 4, idNodo);
            aParam[i++] = ParametroSql.add("@sEstado", SqlDbType.Char, 1, sEstado);
            aParam[i++] = ParametroSql.add("@sCategoria", SqlDbType.Char, 1, sCategoria);
            aParam[i++] = ParametroSql.add("@idCliente", SqlDbType.Int, 4, idCliente);
            aParam[i++] = ParametroSql.add("@idResponsable", SqlDbType.Int, 4, idResponsable);
            aParam[i++] = ParametroSql.add("@numPE", SqlDbType.Int, 4, numPE);
            aParam[i++] = ParametroSql.add("@sDesPE", SqlDbType.VarChar, 70, sDesPE);
            aParam[i++] = ParametroSql.add("@sTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda);

            return SqlHelper.ExecuteSqlDataReader("GVT_GETPROYECTOS_SOLICITUD", aParam);
        }
       
        public static SqlDataReader ObtenerTooltipProyectoUsuario(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETDATOSPROYECTOUSUARIO_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETDATOSPROYECTOUSUARIO_S", aParam);

        }
        
        #endregion
    }
}
