using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class ACUERDOGV
    {
        public static SqlDataReader obtenerAcuerdosParaAsignacion(int t314_idusuario, Nullable<int> t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_ACUERDOGV_CATASIG", aParam);
        }

        public static SqlDataReader ObtenerAcuerdos(Nullable<DateTime> dFecha)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@dFecha", SqlDbType.SmallDateTime, 4, dFecha);
            //aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_ACUERDOGV_CAT", aParam);
        }

        public static SqlDataReader ObtenerProfesionales(int t666_idacuerdogv)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_ACUERDOGV_FICEPI_SEL", aParam);
        }

        public static SqlDataReader ObtenerProyectos(int t666_idacuerdogv)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_ACUERDOGV_PROYECTO_SEL", aParam);
        }

        public static SqlDataReader ObtenerNodos(int t666_idacuerdogv)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_ACUERDOGV_NODO_SEL", aParam);
        }

        public static SqlDataReader CatalogoProfesionales(string t001_apellido1, string t001_apellido2, string t001_nombre, string excluidos, bool todos)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 20, t001_nombre);
            aParam[i++] = ParametroSql.add("@excluidos", SqlDbType.VarChar, 8000, excluidos);
            aParam[i++] = ParametroSql.add("@todos", SqlDbType.Bit, 1, todos);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("GVT_FICEPI_SEL", aParam);
        }

        public static SqlDataReader CatalogoNodos()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("GVT_NODO_CAT", aParam);
        }

        public static SqlDataReader CatalogoProyectos(Nullable<int> idNodo, string sEstado, string sCategoria,
                                    Nullable<int> idCliente, Nullable<int> idResponsable, Nullable<int> numPE, string sDesPE,
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

            return SqlHelper.ExecuteSqlDataReader("GVT_GETPROYECTOS_SUPER", aParam);
        }

        public static SqlDataReader CatalogoResponsablesProyecto(string t001_apellido1, string t001_apellido2, string t001_nombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 100, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 100, t001_apellido2);
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 100, t001_nombre);
            aParam[i++] = ParametroSql.add("@bMostrarBajas", SqlDbType.Bit, 1, bMostrarBajas);

            return SqlHelper.ExecuteSqlDataReader("GVT_PROFRESPONSABLE_PROYECTO", aParam);
        }

        public static SqlDataReader CatalogoClienteByNombre(string t302_denominacion, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t302_denominacion", SqlDbType.Text, 100, t302_denominacion);
            aParam[i++] = ParametroSql.add("@sTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda);

            return SqlHelper.ExecuteSqlDataReader("GVT_CLIENTE_ByNombre", aParam);
        }


        public static int DeleteAcuerdo(SqlTransaction tr, int t666_idacuerdogv)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery("GVT_ACUERDOGV_DEL", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ACUERDOGV_DEL", aParam));
        }

        public static int InsertAcuerdo(SqlTransaction tr, string t666_denominacion, int t001_idficepi_responsable, string t666_descripcion,
                                        DateTime t666_fechainicio, DateTime t666_fechafin, 
                                        int t001_idficepi_mod, DateTime t666_fechamod, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_denominacion", SqlDbType.VarChar, 50, t666_denominacion);
            aParam[i++] = ParametroSql.add("@t001_idficepi_responsable", SqlDbType.Int, 4, t001_idficepi_responsable);
            aParam[i++] = ParametroSql.add("@t666_descripcion", SqlDbType.Text, 16, t666_descripcion);
            aParam[i++] = ParametroSql.add("@t666_fechainicio", SqlDbType.SmallDateTime, 4, t666_fechainicio);
            aParam[i++] = ParametroSql.add("@t666_fechafin", SqlDbType.SmallDateTime, 4, t666_fechafin);
            aParam[i++] = ParametroSql.add("@t001_idficepi_mod", SqlDbType.Int, 4, t001_idficepi_mod);
            aParam[i++] = ParametroSql.add("@t666_fechamod", SqlDbType.SmallDateTime, 4, t666_fechamod);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_ACUERDOGV_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_ACUERDOGV_INS", aParam));
        }

        public static void UpdateAcuerdo(SqlTransaction tr, int t666_idacuerdogv, string t666_denominacion, int t001_idficepi_responsable, string t666_descripcion,
                                        DateTime t666_fechainicio, DateTime t666_fechafin,
                                        int t001_idficepi_mod, DateTime t666_fechamod, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t666_denominacion", SqlDbType.VarChar, 50, t666_denominacion);
            aParam[i++] = ParametroSql.add("@t001_idficepi_responsable", SqlDbType.Int, 4, t001_idficepi_responsable);
            aParam[i++] = ParametroSql.add("@t666_descripcion", SqlDbType.Text, 16, t666_descripcion);
            aParam[i++] = ParametroSql.add("@t666_fechainicio", SqlDbType.SmallDateTime, 4, t666_fechainicio);
            aParam[i++] = ParametroSql.add("@t666_fechafin", SqlDbType.SmallDateTime, 4, t666_fechafin);
            aParam[i++] = ParametroSql.add("@t001_idficepi_mod", SqlDbType.Int, 4, t001_idficepi_mod);
            aParam[i++] = ParametroSql.add("@t666_fechamod", SqlDbType.SmallDateTime, 4, t666_fechamod);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ACUERDOGV_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ACUERDOGV_UPD", aParam);
        }

        public static void DeleteProfesional(SqlTransaction tr, int t666_idacuerdogv, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ACUERDOGV_FICEPI_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ACUERDOGV_FICEPI_DEL", aParam);
        }

        public static int InsertProfesional(SqlTransaction tr, int t666_idacuerdogv, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_ACUERDOGV_FICEPI_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_ACUERDOGV_FICEPI_INS", aParam));
        }

        public static void DeleteProyecto(SqlTransaction tr, int t666_idacuerdogv, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ACUERDOGV_PROYECTO_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ACUERDOGV_PROYECTO_DEL", aParam);
        }

        public static int InsertProyecto(SqlTransaction tr, int t666_idacuerdogv, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_ACUERDOGV_PROYECTO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_ACUERDOGV_PROYECTO_INS", aParam));
        }

        public static void DeleteCR(SqlTransaction tr, int t666_idacuerdogv, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ACUERDOGV_NODO_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ACUERDOGV_NODO_DEL", aParam);
        }

        public static int InsertCR(SqlTransaction tr, int t666_idacuerdogv, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_ACUERDOGV_NODO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_ACUERDOGV_NODO_INS", aParam));
        }

    }
}