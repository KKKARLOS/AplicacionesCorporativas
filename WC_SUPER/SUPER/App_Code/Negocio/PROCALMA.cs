using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using SUPER.DAL;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de PROCALMA
    /// </summary>
    public class PROCALMA
    {
        public PROCALMA()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static DataTable ObtenerCatalogoProcedimientos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("t203_idprocalma", typeof(int)));
            dt.Columns.Add(new DataColumn("t203_denominacion", typeof(string)));

            SqlDataReader dr = SUPER.DAL.PROCALMA.Catalogo(null);

            try
            {
                while (dr.Read())
                {
                    DataRow oRow = dt.NewRow();
                    oRow["t203_idprocalma"] = (int)dr["t203_idprocalma"];
                    oRow["t203_denominacion"] = dr["t203_denominacion"].ToString();
                    dt.Rows.Add(oRow);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        public static SqlParameter[] ObtenerParametrosPA(string spName){
            return SUPER.Capa_Datos.SqlHelperParameterCache.GetSpParameterSet(SUPER.Capa_Negocio.Utilidades.CadenaConexion, spName);
        }

        public static void Ejecutar(string sProc, SqlParameter[] parametros)
        {
            SUPER.DAL.PROCALMA.Ejecutar(sProc, parametros);
        }

    }
}
