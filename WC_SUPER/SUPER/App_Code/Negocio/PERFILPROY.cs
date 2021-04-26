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
    /// Class	 : PERFILPROY
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T333_PERFILPROY
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	07/03/2008 12:13:37	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PERFILPROY
    {
        #region Metodos

        public static int Insert(SqlTransaction tr, string t333_denominacion, Nullable<decimal> t333_imptarifa, int t301_idproyecto, short t333_orden, bool t333_estado)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t333_denominacion", SqlDbType.Text, 30);
            aParam[0].Value = t333_denominacion;
            aParam[1] = new SqlParameter("@t333_imptarifa", SqlDbType.Money, 8);
            aParam[1].Value = t333_imptarifa;
            aParam[2] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[2].Value = t301_idproyecto;
            aParam[3] = new SqlParameter("@t333_orden", SqlDbType.SmallInt, 2);
            aParam[3].Value = t333_orden;
            aParam[4] = new SqlParameter("@t333_estado", SqlDbType.Bit, 1);
            aParam[4].Value = t333_estado;

            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PERFILPROY_I", aParam));
        }

        public static int Update(SqlTransaction tr, int t333_idperfilproy, string t333_denominacion, Nullable<decimal> t333_imptarifa, int t301_idproyecto, short t333_orden, bool t333_estado)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[0].Value = t333_idperfilproy;
            aParam[1] = new SqlParameter("@t333_denominacion", SqlDbType.Text, 30);
            aParam[1].Value = t333_denominacion;
            aParam[2] = new SqlParameter("@t333_imptarifa", SqlDbType.Money, 8);
            aParam[2].Value = t333_imptarifa;
            aParam[3] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[3].Value = t301_idproyecto;
            aParam[4] = new SqlParameter("@t333_orden", SqlDbType.SmallInt, 2);
            aParam[4].Value = t333_orden;
            aParam[5] = new SqlParameter("@t333_estado", SqlDbType.Bit, 1);
            aParam[5].Value = t333_estado;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PERFILPROY_U", aParam);
        }

        public static int Delete(SqlTransaction tr, int t333_idperfilproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[0].Value = t333_idperfilproy;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PERFILPROY_D", aParam);
        }

        public static SqlDataReader CatalogoPerfilesProyecto_By_PSN(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PERFILPROY_By_PSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PERFILPROY_By_PSN", aParam);
        }

        public static int Procesar1(SqlTransaction tr, int t305_idproyectosubnodo, string sTengan, string sEstados)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@sEstados", SqlDbType.Text, 15);
            aParam[1].Value = sEstados;
            string sProcAlm = "";
            switch (sTengan)
            {
                case "1":
                    sProcAlm = "SUP_PERFILPROY_PROC1";
                    break;
                case "2":
                    sProcAlm = "SUP_PERFILPROY_PROC2";
                    break;
                case "3":
                    sProcAlm = "SUP_PERFILPROY_PROC3";
                    break;
            }
            return SqlHelper.ExecuteNonQueryTransaccion(tr, sProcAlm, aParam);
        }
        public static int Procesar2(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario,
                                    Nullable<int> t333_idperfilproy, Nullable<int> idPerfilAux, string sTareas, string sTipo)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[2].Value = t333_idperfilproy;
            aParam[3] = new SqlParameter("@idperfilAux", SqlDbType.Int, 4);
            aParam[3].Value = idPerfilAux;
            aParam[4] = new SqlParameter("@sTareas", SqlDbType.Text, 8000);
            aParam[4].Value = sTareas;

            string sProcAlm = "";
            switch (sTipo)
            {
                case "1":
                case "2":
                    sProcAlm = "SUP_PERFILPROY_PROC5";
                    break;
                case "3":
                    sProcAlm = "SUP_PERFILPROY_PROC6";
                    break;
            }
            return SqlHelper.ExecuteNonQueryTransaccion(tr, sProcAlm, aParam);
        }
        public static int Procesar3(SqlTransaction tr, int t305_idproyectosubnodo, int t333_idperfilproy, string sEstados, string sProfesionales)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[1].Value = t333_idperfilproy;
            aParam[2] = new SqlParameter("@sProfesionales", SqlDbType.Text, 8000);
            aParam[2].Value = sProfesionales;
            aParam[3] = new SqlParameter("@sEstados", SqlDbType.Text, 15);
            aParam[3].Value = sEstados;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PERFILPROY_PROC4", aParam);
        }

        /// <summary>
        /// Verfica si es posible borrar un perfil
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t333_idperfilproy"></param>
        /// <returns></returns>
        public static int ComprobarBorrado(SqlTransaction tr, int t333_idperfilproy)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t333_idperfilproy", SqlDbType.Int, 4, t333_idperfilproy)
            };
            if (tr == null)
                return (int)SqlHelper.ExecuteScalar("SUP_PERFILPROY_D_consulta", aParam);
            else
                return (int)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PERFILPROY_D_consulta", aParam);
        }
        #endregion
    }
}
