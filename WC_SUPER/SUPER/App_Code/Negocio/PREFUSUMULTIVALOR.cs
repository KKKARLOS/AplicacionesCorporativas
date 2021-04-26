using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class PREFUSUMULTIVALOR
    {
        #region Metodos

        public static void Insertar(SqlTransaction tr, int t462_idPrefUsuario, byte t441_concepto, string t441_valor, string t441_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t462_idPrefUsuario", SqlDbType.Int, 4, t462_idPrefUsuario),
                ParametroSql.add("@t441_concepto", SqlDbType.TinyInt, 2, t441_concepto),
                ParametroSql.add("@t441_valor", SqlDbType.VarChar, 15, t441_valor),
                ParametroSql.add("@t441_denominacion", SqlDbType.VarChar, 100, t441_denominacion)
            };

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                Convert.ToInt32(SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIOMULTI_INS", aParam));
            else
                Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIOMULTI_INS", aParam));
        }
        public static void Insertar(SqlTransaction tr, int t462_idPrefUsuario, byte t441_concepto, string t441_valor, string t441_denominacion, byte t441_orden)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t462_idPrefUsuario", SqlDbType.Int, 4, t462_idPrefUsuario),
                ParametroSql.add("@t441_concepto", SqlDbType.TinyInt, 2, t441_concepto),
                ParametroSql.add("@t441_valor", SqlDbType.VarChar, 15, t441_valor),
                ParametroSql.add("@t441_denominacion", SqlDbType.VarChar, 100, t441_denominacion),
                ParametroSql.add("@t441_orden", SqlDbType.TinyInt, 2, t441_orden)
            };

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                Convert.ToInt32(SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIOMULTI_INS", aParam));
            else
                Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIOMULTI_INS", aParam));
        }
        public static void InsertarCVT(SqlTransaction tr, int t462_idPrefUsuario, byte t441_concepto, string t441_valor, string t441_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t462_idPrefUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t462_idPrefUsuario;
            aParam[1] = new SqlParameter("@t441_concepto", SqlDbType.TinyInt, 2);
            aParam[1].Value = t441_concepto;
            aParam[2] = new SqlParameter("@t441_valor", SqlDbType.VarChar, 50);
            aParam[2].Value = t441_valor;
            aParam[3] = new SqlParameter("@t441_denominacion", SqlDbType.VarChar, 200);
            aParam[3].Value = t441_denominacion;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                Convert.ToInt32(SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIOMULTI_CVT_I", aParam));
            else
                Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIOMULTI_CVT_I", aParam));
        }
        public static SqlDataReader Obtener(SqlTransaction tr, Nullable<int> t462_idPrefUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t462_idPrefUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t462_idPrefUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PREFUSUMULTIVALOR_DATOSRES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PREFUSUMULTIVALOR_DATOSRES", aParam);
        }
        public static SqlDataReader ObtenerCVT(SqlTransaction tr, Nullable<int> t462_idPrefUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t462_idPrefUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t462_idPrefUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PREFUSUMULTIVALOR_CVT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PREFUSUMULTIVALOR_CVT", aParam);
        }
        public static ArrayList SelectSubnodosAmbito(SqlTransaction tr, ArrayList aSubnodosAux, int nNivelEstructura, int nID)
        {
            SqlDataReader dr = null;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nNivelEstructura", SqlDbType.Int, 4);
            aParam[0].Value = nNivelEstructura;
            aParam[1] = new SqlParameter("@nID", SqlDbType.Int, 4);
            aParam[1].Value = nID;

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PREFUSUMULTIVALOR_SUBNODOS", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PREFUSUMULTIVALOR_SUBNODOS", aParam);

            bool bEncontrado=false;
            while (dr.Read())
            {
                bEncontrado=false;
                for (int i=0; i<aSubnodosAux.Count; i++){
                    if (aSubnodosAux[i].ToString() == dr["t304_idsubnodo"].ToString())
                    {
                        bEncontrado = true;
                        break;
                    }
                }
                if (!bEncontrado) aSubnodosAux.Add(dr["t304_idsubnodo"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return aSubnodosAux;
        }

        public static ArrayList SelectNodosAmbito(SqlTransaction tr, ArrayList aNodosAux, int nNivelEstructura, int nID)
        {
            SqlDataReader dr = null;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nNivelEstructura", SqlDbType.Int, 4);
            aParam[0].Value = nNivelEstructura;
            aParam[1] = new SqlParameter("@nID", SqlDbType.Int, 4);
            aParam[1].Value = nID;

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PREFUSUMULTIVALOR_NODOS", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PREFUSUMULTIVALOR_NODOS", aParam);

            bool bEncontrado = false;
            while (dr.Read())
            {
                bEncontrado = false;
                for (int i = 0; i < aNodosAux.Count; i++)
                {
                    if (aNodosAux[i].ToString() == dr["t303_idnodo"].ToString())
                    {
                        bEncontrado = true;
                        break;
                    }
                }
                if (!bEncontrado) aNodosAux.Add(dr["t303_idnodo"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return aNodosAux;
        }

        #endregion
    }
}
