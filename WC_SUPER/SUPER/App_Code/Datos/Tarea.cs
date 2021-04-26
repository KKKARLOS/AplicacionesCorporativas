using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    public partial class Tarea
    {
        public Tarea()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #region Metodos
        //public static SqlDataReader GetCriteriosConsultaCampos(int t314_idusuario, int nFilasMax)
        //{
        //    SqlParameter[] aParam = new SqlParameter[]{
        //        ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
        //        ParametroSql.add("@nFilasMax", SqlDbType.Int, 4, nFilasMax),
        //        ParametroSql.add("@entorno", SqlDbType.Char, 1, System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper())
        //    };
        //    return SqlHelper.ExecuteSqlDataReader("SUP_INF_TAREA_CRITERIOS", aParam);
        //}
        #endregion
    }
}