using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de Cualificador
    /// </summary>
    public class Cualificador
    {
        #region Propiedades y Atributos complementarios
        private int _t055_idcalifOCFA;
        public int t055_idcalifOCFA
        {
            get { return _t055_idcalifOCFA; }
            set { _t055_idcalifOCFA = value; }
        }

        private string _t055_denominacion;
        public string t055_denominacion
        {
            get { return _t055_denominacion; }
            set { _t055_denominacion = value; }
        }

        #endregion

        public Cualificador()
        {
            t055_idcalifOCFA = -1;
        }
        public static SqlDataReader Catalogo()
        {
            //SqlParameter[] aParam = new SqlParameter[0];
            SqlParameter[] aParam = new SqlParameter[]{};

            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADOR_C", aParam);

        }
        public static Cualificador getDefectoParaNodos()
        {
            Cualificador o = new Cualificador();

            SqlParameter[] aParam = new SqlParameter[]{};
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADOR_NODO_DEFECTO", aParam);
            if (dr.Read())
            {
                if (dr["t055_idcalifOCFA"] != DBNull.Value)
                    o.t055_idcalifOCFA = int.Parse(dr["t055_idcalifOCFA"].ToString());
                o.t055_denominacion = dr["t055_denominacion"].ToString();
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static Cualificador getDefectoParaPIG()
        {
            Cualificador o = new Cualificador();

            SqlParameter[] aParam = new SqlParameter[] { };
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADOR_PIG_DEFECTO", aParam);
            if (dr.Read())
            {
                if (dr["t055_idcalifOCFA"] != DBNull.Value)
                    o.t055_idcalifOCFA = int.Parse(dr["t055_idcalifOCFA"].ToString());
                o.t055_denominacion = dr["t055_denominacion"].ToString();
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        /// <summary>
        /// Obtiene el nº de cualificadores con nodo por defecto marcado
        /// </summary>
        /// <returns></returns>
        public static int getNumDefectoParaNodos(SqlTransaction tr)
        {
            int iCont = 0;
            SqlParameter[] aParam = new SqlParameter[] { };
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CUALIFICADOR_NODO_DEFECTO", aParam);
            while (dr.Read())
            {
                iCont++;
            }

            dr.Close();
            dr.Dispose();

            return iCont;
        }
        /// <summary>
        /// Obtiene el nº de cualificadores con PIG por defecto marcado
        /// </summary>
        /// <returns></returns>
        public static int getNumDefectoParaPIG(SqlTransaction tr)
        {
            int iCont = 0;
            SqlParameter[] aParam = new SqlParameter[] { };
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CUALIFICADOR_PIG_DEFECTO", aParam);
            while (dr.Read())
            {
                iCont++;
            }

            dr.Close();
            dr.Dispose();

            return iCont;
        }

        public static void Delete(SqlTransaction tr, int t055_idcalifOCFA)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t055_idcalifOCFA", SqlDbType.SmallInt, 2, t055_idcalifOCFA)
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CUALIFICADOR_D", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CUALIFICADOR_D", aParam);
        }

        public static void Update(SqlTransaction tr, int t055_idcalifOCFA, string t055_denominacion, bool t055_defectoCCRR,
                                  bool t055_defectoPIG, Nullable<int> t329_idclaseeco)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t055_idcalifOCFA", SqlDbType.Int, 4, t055_idcalifOCFA),
                ParametroSql.add("@t055_denominacion", SqlDbType.VarChar, 50, t055_denominacion),
                ParametroSql.add("@t055_defectoCCRR", SqlDbType.Bit, 1, t055_defectoCCRR),
                ParametroSql.add("@t055_defectoPIG", SqlDbType.Bit, 1, t055_defectoPIG),
                ParametroSql.add("@t329_idclaseeco", SqlDbType.Int, 4, t329_idclaseeco),
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CUALIFICADOR_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CUALIFICADOR_U", aParam);
        }

        public static short Insert(SqlTransaction tr, string t055_denominacion, bool t055_defectoCCRR, bool t055_defectoPIG, 
                                    Nullable<int> t329_idclaseeco)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t055_denominacion", SqlDbType.VarChar, 50, t055_denominacion),
                ParametroSql.add("@t055_defectoCCRR", SqlDbType.Bit, 1, t055_defectoCCRR),
                ParametroSql.add("@t055_defectoPIG", SqlDbType.Bit, 1, t055_defectoPIG),
                ParametroSql.add("@t329_idclaseeco", SqlDbType.Int, 4, t329_idclaseeco),
            };

            if (tr == null)
                return Convert.ToInt16(SqlHelper.ExecuteScalar("SUP_CUALIFICADOR_I", aParam));
            else
                return Convert.ToInt16(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CUALIFICADOR_I", aParam));
        }

    }
}