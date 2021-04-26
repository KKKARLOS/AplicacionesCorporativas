using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de Ficepi
    /// </summary>
    public class Ficepi
    {
        #region Metodos
        public static SqlDataReader CargarRecursos(SqlTransaction tr, string strApellido1, string strApellido2, string strNombre, int intColumna, int intOrden, string excluidos)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@Apellido1", SqlDbType.VarChar, 100);
            aParam[0].Value = strApellido1;
            aParam[1] = new SqlParameter("@Apellido2", SqlDbType.VarChar, 100);
            aParam[1].Value = strApellido2;
            aParam[2] = new SqlParameter("@Nombre", SqlDbType.VarChar, 100);
            aParam[2].Value = strNombre;
            aParam[3] = new SqlParameter("@INTCOLUMNA", SqlDbType.Int);
            aParam[3].Value = intColumna;
            aParam[4] = new SqlParameter("@INTORDEN", SqlDbType.Int);
            aParam[4].Value = intOrden;
            aParam[5] = new SqlParameter("@excluidos", SqlDbType.VarChar, 8000);
            aParam[5].Value = excluidos;
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("FRM_RECURSOS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "FRM_RECURSOS", aParam);

        }

        //public static SqlDataReader CargarRecursosValidador(SqlTransaction tr, string strApellido1, string strApellido2, string strNombre, int t001_idficepi_validador)
        //{
        //    SqlParameter[] aParam = new SqlParameter[3];
        //    aParam[0] = new SqlParameter("@Apellido1", SqlDbType.VarChar, 100);
        //    aParam[0].Value = strApellido1;
        //    aParam[1] = new SqlParameter("@Apellido2", SqlDbType.VarChar, 100);
        //    aParam[1].Value = strApellido2;
        //    aParam[2] = new SqlParameter("@Nombre", SqlDbType.VarChar, 100);
        //    aParam[2].Value = strNombre;
        //    aParam[3] = new SqlParameter("@t001_idficepi_validador", SqlDbType.Int, 4);
        //    aParam[3].Value = t001_idficepi_validador;

        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader("SUP_CVT_RECURSOS", aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_RECURSOS", aParam);

        //}
        public static SqlDataReader ObtenerCatalogo(SqlTransaction tr, string t001_apellido1, string t001_apellido2, string t001_nombre,
                                                    bool bSoloActivos)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 25, t001_nombre);
            aParam[i++] = ParametroSql.add("@bSoloActivos", SqlDbType.Bit, 1, bSoloActivos);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("FIC_PROFESIONAL_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "FIC_PROFESIONAL_CAT", aParam);
        }

        public static SqlDataReader ObtenerProfesionalesParaFigurasCVT(SqlTransaction tr, string t001_apellido1, string t001_apellido2, string t001_nombre, 
                                                    bool bSoloActivos, bool bCoste, bool bExternos, bool bBajas)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 25, t001_nombre);
            aParam[i++] = ParametroSql.add("@bSoloActivos", SqlDbType.Bit, 1, bSoloActivos);
            aParam[i++] = ParametroSql.add("@bCoste", SqlDbType.Bit, 1, bCoste);
            aParam[i++] = ParametroSql.add("@bExternos", SqlDbType.Bit, 1, bExternos);
            aParam[i++] = ParametroSql.add("@bBajas", SqlDbType.Bit, 1, bBajas);
    
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("FIC_PROFESIONAL_FIGURACVT_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "FIC_PROFESIONAL_CAT", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesAvisos(SqlTransaction tr, string t001_apellido1, string t001_apellido2, string t001_nombre, bool bSoloActivos)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 25, t001_nombre);
            aParam[i++] = ParametroSql.add("@bSoloActivos", SqlDbType.Bit, 1, bSoloActivos);
            aParam[i++] = ParametroSql.add("@bForaneos", SqlDbType.Bit, 1, (bool)HttpContext.Current.Session["FORANEOS"]);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONAL_AVISOS_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROFESIONAL_AVISOS_C", aParam);
        }

        public static SqlDataReader ObtenerProfesionalesAvisosExcepciones(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;

            aParam[i++] = ParametroSql.add("@bForaneos", SqlDbType.Bit, 1, (bool)HttpContext.Current.Session["FORANEOS"]);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONAL_AVISOS_EXCEP_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROFESIONAL_AVISOS_EXCEP_C", aParam);
        }
        public static void Update(SqlTransaction tr, int t001_idficepi, Nullable<bool> @t314_noalertas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t314_noalertas", SqlDbType.Bit, 1, t314_noalertas);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PROFESIONAL_NOALERTA", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROFESIONAL_NOALERTA", aParam);
        }

        public static void UpdateCal(SqlTransaction tr, int t001_idficepi, int t066_idcal)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t066_idcal", SqlDbType.Int, 4, t066_idcal);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("[SUP_FICEPI_UPD_CAL]", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FICEPI_UPD_CAL", aParam);
        }

        public static void UpdateNoCV(SqlTransaction tr, int t001_idficepi, bool t165_nocv, string t165_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t165_nocv", SqlDbType.Bit, 1, t165_nocv);
            aParam[i++] = ParametroSql.add("@t165_comentario", SqlDbType.Text, 16, t165_comentario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_FICEPI_CVCOMPLETADO_U_NOCV", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_FICEPI_CVCOMPLETADO_U_NOCV", aParam);
        }

        public static void UpdateForaneo(SqlTransaction tr, int t001_idficepi, string t001_apellido1, string t001_apellido2,
                                         string t001_nombre, string t001_sexo, string t001_telefono, int t066_idcal, string sMail)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1),
                ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2),
                ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 20, t001_nombre),
                ParametroSql.add("@t001_sexo", SqlDbType.Char, 1, t001_sexo),
                ParametroSql.add("@t001_telefono", SqlDbType.VarChar, 15, t001_telefono),
                ParametroSql.add("@t066_idcal", SqlDbType.Int, 4, t066_idcal),
                ParametroSql.add("@t001_email", SqlDbType.VarChar, 50, sMail)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FICEPIFORANEO_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FICEPIFORANEO_U", aParam);
        }


        public static SqlDataReader GetResponsableProgress(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_GET_RESPONSABLE_PROGRESS", aParam);
        }
        #endregion
    }
}
