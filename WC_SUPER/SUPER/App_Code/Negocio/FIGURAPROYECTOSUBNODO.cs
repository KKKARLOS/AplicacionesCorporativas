using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class FIGURAPROYECTOSUBNODO
    {
        #region Metodos

        public static SqlDataReader CatalogoFiguras(Nullable<int> t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPROYECTOSUBNODO_CF", aParam);
        }

        public static SqlDataReader CatalogoFigurasVirtuales(Nullable<int> t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPROYECTOSUBNODO_CF_VIRTUAL", aParam);
        }

        public static int DeleteUsuario(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPROYECTOSUBNODO_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPROYECTOSUBNODO_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, string t310_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t310_figura", SqlDbType.Char, 1);
            aParam[2].Value = t310_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPROYECTOSUBNODO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPROYECTOSUBNODO_D", aParam);
        }


        #endregion
    }
}
