using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class CONSULTAPERSONAL
    {
        #region Metodos

        public static SqlDataReader EjecutarConsulta(string sProdAlm, object[] aParametros)
        {
            return SqlHelper.ExecuteSqlDataReader(sProdAlm, aParametros);
        }
        public static DataSet EjecutarConsultaDS(string sProdAlm, object[] aParametros)
        {
            return SqlHelper.ExecuteDatasetCP(Utilidades.CadenaConexion, sProdAlm, 300, aParametros);
        }
        public static SqlDataReader ObtenerCatalogo(SqlTransaction tr, bool t472_estado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t472_estado", SqlDbType.Bit, 1);
            aParam[0].Value = t472_estado;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAPERSONAL_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSULTAPERSONAL_CAT", aParam);
        }

        public static int GetNumConsultas(bool t472_estado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t472_estado", SqlDbType.Int, 4);
            aParam[0].Value = t472_estado;

            return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CONSULTAPERSONAL_COUNT", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el nº de usuarios que están usando una consulta
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	31/08/2009 15:10:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int numUsuarios(int t472_idconsulta)
        {
            int iRes;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
            aParam[0].Value = t472_idconsulta;

            iRes = Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CONSULTAPERSONAL_Count_USUARIO", aParam));
            return iRes;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Dada una consulta devuelve los profesionales que tiene asignados
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	03/09/2009 15:10:01
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader GetProf(SqlTransaction tr, int t472_idconsulta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
            aParam[0].Value = t472_idconsulta;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAPERSONAL_USUARIO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSULTAPERSONAL_USUARIO", aParam);
        }

        public static bool ExisteClave(SqlTransaction tr, string t472_clavews, int t472_idconsulta)
        {
            bool bExiste = false;
            if (t472_clavews.Trim() != "")
            {
                SqlDataReader dr = SUPER.DAL.ConsultaPersonal.GetByClaveWS(tr, t472_clavews.Trim());
                if (dr.Read())
                {
                    if (t472_idconsulta != int.Parse(dr["t472_idconsulta"].ToString()))
                        bExiste = true;
                }
                dr.Close();
                dr.Dispose();
            }
            return bExiste;
        }
        #endregion
    }
}
