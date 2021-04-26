using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class CLASEECO
    {
        #region Metodos

        public static SqlDataReader SelectActivasByT328_idconceptoeco(SqlTransaction tr, byte t328_idconceptoeco, string sCualidad, bool bAdmin, bool bEsReplicable, string idsNegativos)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t328_idconceptoeco", SqlDbType.TinyInt, 1);
            aParam[0].Value = t328_idconceptoeco;
            aParam[1] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[1].Value = sCualidad;
            aParam[2] = new SqlParameter("@bEsAdmin", SqlDbType.Bit, 1);
            aParam[2].Value = bAdmin;
            aParam[3] = new SqlParameter("@bEsReplicable", SqlDbType.Bit, 1);
            aParam[3].Value = bEsReplicable;
            aParam[4] = new SqlParameter("@idsNegativos", SqlDbType.VarChar, 100);
            aParam[4].Value = idsNegativos;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CLASEECO_ACTIVAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLASEECO_ACTIVAS", aParam);
        }
        public static SqlDataReader ObtenerClasesClonables(SqlTransaction tr, bool bAdmin)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@bEsAdmin", SqlDbType.Bit, 1);
            aParam[0].Value = bAdmin;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CLASESCLONABLES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLASESCLONABLES", aParam);
        }
        public static SqlDataReader ObtenerClasesBorrables(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CLASESBORRADODATO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLASESBORRADODATO", aParam);
        }
        public static string getArbol(SqlTransaction tr, int t329_idclaseeco)
        {
            string sRes = "";
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
            aParam[0].Value = t329_idclaseeco;

            //if (tr == null)
            //    return SqlHelper.ExecuteSqlDataReader("SUP_CLASEECO_DENARBOL", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLASEECO_DENARBOL", aParam);
            object obj;
            if (tr == null)
                obj = SqlHelper.ExecuteScalar("SUP_CLASEECO_DENARBOL", aParam);
            else
                obj = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CLASEECO_DENARBOL", aParam);
            if (obj != null)
            {
                sRes = obj.ToString();
            }
            return sRes;
        }

        /// <summary>
        /// Obtiene la clases que se pueden seleccionar en el mantenimiento de cualificadores
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static SqlDataReader GetClasesMtoCualificadores(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[] { };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CLASEECO_INGRESOS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLASEECO_INGRESOS", aParam);
        }

        #endregion
    }
}
