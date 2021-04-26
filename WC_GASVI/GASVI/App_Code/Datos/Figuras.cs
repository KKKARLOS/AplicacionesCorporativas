using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class Figuras
    {
        public static SqlDataReader CatalogoFiguras()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_TIPOFIGURAGASVI_CAT", aParam);
        }

        public static SqlDataReader ObtenerIntegrantes(string figura)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@figura", SqlDbType.Char, 1, figura);

            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_FIGURAGASVI_CAT", aParam);
        }

        public static SqlDataReader ObtenerIntegrantesNodos(int t001_idficepi, string sTipo, string figura)
        {
            SqlParameter[] aParam = null;
            int i = 0;
            if (sTipo == "L")
            {
                aParam = new SqlParameter[1];
                aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            }
            else
            {
                aParam = new SqlParameter[2];
                aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
                aParam[i++] = ParametroSql.add("@figura", SqlDbType.Char, 1, figura);
            }

            switch (sTipo) // Ejecuta la query y devuelve un string con el resultado según el tipo que sea
            {
                case "L":
                    return SqlHelper.ExecuteSqlDataReader("GVT_LIQUIDADOR_OFICINA_SEL", aParam);
                case "N":
                    return SqlHelper.ExecuteSqlDataReader("GVT_FIGURAFICEPINODO_SEL", aParam);
                case "1":
                    return SqlHelper.ExecuteSqlDataReader("GVT_FIGURAFICEPISN1_SEL", aParam);
                case "2":
                    return SqlHelper.ExecuteSqlDataReader("GVT_FIGURAFICEPISN2_SEL", aParam);
                case "3":
                    return SqlHelper.ExecuteSqlDataReader("GVT_FIGURAFICEPISN3_SEL", aParam);
                case "4":
                    return SqlHelper.ExecuteSqlDataReader("GVT_FIGURAFICEPISN4_SEL", aParam);
            }
            return null;
        }

        public static SqlDataReader ObtenerCatalogoNodos(string sTipo)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            switch (sTipo) // Ejecuta la query y devuelve un string con el resultado según el tipo que sea
            {
                
                case "L":
                    return SqlHelper.ExecuteSqlDataReader("GVT_OFICINAS_CAT", aParam);
                case "N":
                    return SqlHelper.ExecuteSqlDataReader("GVT_NODO_CAT", aParam);
                case "1":
                    return SqlHelper.ExecuteSqlDataReader("GVT_SUPERNODO1_CAT", aParam);
                case "2":
                    return SqlHelper.ExecuteSqlDataReader("GVT_SUPERNODO2_CAT", aParam);
                case "3":
                    return SqlHelper.ExecuteSqlDataReader("GVT_SUPERNODO3_CAT", aParam);
                case "4":
                    return SqlHelper.ExecuteSqlDataReader("GVT_SUPERNODO4_CAT", aParam);
            }
            return null;
        }

        public static SqlDataReader ObtenerTramitados(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_FIGURAGASVITRAMITADOR_SEL", aParam);
        }

        public static SqlDataReader CatalogoPersonasFiguras(string t001_apellido1, string t001_apellido2, string t001_nombre, string excluidos, bool todos)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 20, t001_nombre);
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, t001_apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, t001_apellido2);
            aParam[i++] = ParametroSql.add("@excluidos", SqlDbType.VarChar, 8000, excluidos);
            aParam[i++] = ParametroSql.add("@todos", SqlDbType.Bit, 1, todos);

            return SqlHelper.ExecuteSqlDataReader("GVT_FICEPI_SEL", aParam);

        }

        public static void InsertIntegranteFigura(SqlTransaction tr, string t418_idfigura, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t418_idfigura", SqlDbType.Char, 1, t418_idfigura);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_FIGURAGASVI_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_FIGURAGASVI_INS", aParam);
        }

        public static void DeleteIntegranteFigura(SqlTransaction tr, string figura, int t001_idficepi)
        {
            string nombreProcedure = "";
            SqlParameter[] aParam = null;
            int i = 0;
            switch (figura)
            {
                case "P":
                case "A":
                case "S":
                    aParam = new SqlParameter[2];
                    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
                    aParam[i++] = ParametroSql.add("@t418_idfigura", SqlDbType.Char, 1, figura);
                    break;
                case "L":
                case "T":
                case "N":
                case "1":
                case "2":
                case "3":
                case "4":
                    aParam = new SqlParameter[1];
                    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
                    break;
            }
            
            switch (figura)
            {
                case "P":
                case "A":
                case "S":
                    nombreProcedure = "GVT_FIGURAGASVI_DEL";
                    break;
                case "L":
                    nombreProcedure = "GVT_LIQUIDADOR_OFICINA_ALL_DEL";
                    break;
                case "T":
                    nombreProcedure = "GVT_FIGURAGASVITRAMITADOR_ALL_DEL";
                    break;
                case "N":
                    nombreProcedure = "GVT_FIGURAFICEPINODO_ALL_DEL";
                    break;
                case "1":
                    nombreProcedure = "GVT_FIGURAFICEPISN1_ALL_DEL";
                    break;
                case "2":
                    nombreProcedure = "GVT_FIGURAFICEPISN2_ALL_DEL";
                    break;
                case "3":
                    nombreProcedure = "GVT_FIGURAFICEPISN3_ALL_DEL";
                    break;
                case "4":
                    nombreProcedure = "GVT_FIGURAFICEPISN4_ALL_DEL";
                    break;
            }

            if (tr == null)
                SqlHelper.ExecuteNonQuery(nombreProcedure, aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, nombreProcedure, aParam);
        }

        public static void InsertElementoFigura(SqlTransaction tr, string sTipo, int t001_idficepi, int idficepiElemento, string figura){
            string nombreProcedure = "";
            SqlParameter[] aParam = null;
            int i = 0;
            switch(sTipo)
            {
                case "L":
                case "T":
                    aParam = new SqlParameter[2];
                    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
                    aParam[i++] = ParametroSql.add("@idficepiElemento", SqlDbType.Int, 4, idficepiElemento);
                    break;            
                case "N":
                case "1":
                case "2":
                case "3":
                case "4":
                    aParam = new SqlParameter[3];
                    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
                    aParam[i++] = ParametroSql.add("@idficepiElemento", SqlDbType.Int, 4, idficepiElemento);
                    aParam[i++] = ParametroSql.add("@figura", SqlDbType.Char, 1, figura);
                    break;
            }
            switch (sTipo) // Ejecuta la query y devuelve un string con el resultado según el tipo que sea
            {
                case "L":
                    nombreProcedure = "GVT_LIQUIDADOR_OFICINA_INS";
                    break;
                case "T":
                    nombreProcedure = "GVT_FIGURAGASVITRAMITADOR_INS";
                    break;
                case "N":
                    nombreProcedure = "GVT_FIGURAFICEPINODO_INS";
                    break;
                case "1":
                    nombreProcedure = "GVT_FIGURAFICEPISN1_INS";
                    break;
                case "2":
                    nombreProcedure = "GVT_FIGURAFICEPISN2_INS";
                    break;
                case "3":
                    nombreProcedure = "GVT_FIGURAFICEPISN3_INS";
                    break;
                case "4":
                    nombreProcedure = "GVT_FIGURAFICEPISN4_INS";
                    break;
            }

            if (tr == null)
                SqlHelper.ExecuteNonQuery(nombreProcedure, aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, nombreProcedure, aParam);
        }

        public static void DeleteElementoFigura(SqlTransaction tr, string sTipo, int t001_idficepi, int idficepiElemento, string figura)
        {
            string nombreProcedure = "";
            SqlParameter[] aParam = null;
            int i = 0;
            switch (sTipo)
            {
                case "L":
                case "T":
                    aParam = new SqlParameter[2];
                    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
                    aParam[i++] = ParametroSql.add("@idficepiElemento", SqlDbType.Int, 4, idficepiElemento);
                    break;
                case "N":
                case "1":
                case "2":
                case "3":
                case "4":
                    aParam = new SqlParameter[3];
                    aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
                    aParam[i++] = ParametroSql.add("@idficepiElemento", SqlDbType.Int, 4, idficepiElemento);
                    aParam[i++] = ParametroSql.add("@figura", SqlDbType.Char, 1, figura);
                    break;
            }
            switch (sTipo) // Ejecuta la query y devuelve un string con el resultado según el tipo que sea
            {
                case "L":
                    nombreProcedure = "GVT_LIQUIDADOR_OFICINA_DEL";
                    break;
                case "T":
                    nombreProcedure = "GVT_FIGURAGASVITRAMITADOR_DEL";
                    break;
                case "N":
                    nombreProcedure = "GVT_FIGURAFICEPINODO_DEL";
                    break;
                case "1":
                    nombreProcedure = "GVT_FIGURAFICEPISN1_DEL";
                    break;
                case "2":
                    nombreProcedure = "GVT_FIGURAFICEPISN2_DEL";
                    break;
                case "3":
                    nombreProcedure = "GVT_FIGURAFICEPISN3_DEL";
                    break;
                case "4":
                    nombreProcedure = "GVT_FIGURAFICEPISN4_DEL";
                    break;
            }

            if (tr == null)
                SqlHelper.ExecuteNonQuery(nombreProcedure, aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, nombreProcedure, aParam);
        }

	}
}