using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para usar ArraList
using System.Collections;

namespace SUPER.Capa_Negocio
{
    public partial class FIGURANODO
    {
        #region Metodos
        public static SqlDataReader Figuras(Nullable<int> t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURANODO_F", aParam);
        }
        //public static SqlDataReader FigurasUsuario(Nullable<int> t303_idnodo, Nullable<int> t314_idusuario)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
        //    aParam[0].Value = t303_idnodo;
        //    aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
        //    aParam[1].Value = t314_idusuario;

        //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
        //    return SqlHelper.ExecuteSqlDataReader("SUP_FIGURANODO_FU", aParam);
        //}
        public static int DeleteUsuario(SqlTransaction tr, int t303_idnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURANODO_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURANODO_DEL", aParam);
        }
        public static int Delete(SqlTransaction tr, int t303_idnodo, int t314_idusuario, string t308_figura)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t308_figura", SqlDbType.Char, 1);
            aParam[2].Value = t308_figura;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURANODO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURANODO_D", aParam);
        }

        public static int Duplicar(SqlTransaction tr, int t303_idnodoOrigen, int t303_idnodoDestino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@idNodoOri", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodoOrigen;
            aParam[1] = new SqlParameter("@idNodoDes", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodoDestino;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FIGURANODO_I_COP", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FIGURANODO_I_COP", aParam));
        }

        /// <summary>
        /// Obtiene un ArrayList con las figuras de un usuario en un nodo(incluido si es responsable)
        /// </summary>
        /// <param name="t303_idnodo"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static ArrayList Lista(int t303_idnodo, int t314_idusuario)
        {
            ArrayList aFig = new ArrayList();
            SqlDataReader dr=SUPER.DAL.FIGURANODO.FigurasUsuario(t303_idnodo,t314_idusuario);
            while (dr.Read())
                aFig.Add(dr["FIGURA"].ToString());
            dr.Close();
            dr.Dispose();

            return aFig;
        }

        #endregion
    }
}
