using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de Nodo
    /// </summary>
    public class NODO
    {
        #region Metodos
        public static SqlDataReader ObtenerNodos(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_NODO_C1", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODO_C1", aParam);
        }

        public static SqlDataReader ObtenerNodosExcepciones(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_NODOS_AVISOS_EXCEP_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODOS_AVISOS_EXCEP_C", aParam);
        }
        public static void Update(SqlTransaction tr, int t303_idnodo, Nullable<bool> t303_noalertas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);
            aParam[i++] = ParametroSql.add("@t303_noalertas", SqlDbType.Bit, 1, t303_noalertas);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_NODO_NOALERTA", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_NOALERTA", aParam);
        }

        public static void TraspasarConsumosIAP(SqlTransaction tr, int anomesacerrar, int t314_idusuario, bool bCerrarIAPNodos, bool bTraspasarEsfuerzos, DataTable tbl_nodos)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@anomesacerrar", SqlDbType.Int, 4, anomesacerrar);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bCerrarIAPNodos", SqlDbType.Bit, 1, bCerrarIAPNodos);
            aParam[i++] = ParametroSql.add("@bTraspasarEsfuerzos", SqlDbType.Bit, 1, bTraspasarEsfuerzos);
            aParam[i++] = ParametroSql.add("@TABNODOS", SqlDbType.Structured, tbl_nodos);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_TRASPASOESFUERZOS_MASIVO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TRASPASOESFUERZOS_MASIVO", aParam);
        }

        #endregion
    }
}

