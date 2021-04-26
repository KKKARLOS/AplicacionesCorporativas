using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class CDP
    {
        #region Propiedades y Atributos

        private string _DesResponsable;
        public string DesResponsable
        {
            get { return _DesResponsable; }
            set { _DesResponsable = value; }
        }
        #endregion

        #region Metodos

        public static int UpdateSimple(SqlTransaction tr, int t476_idcnp, byte t476_orden)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[0].Value = t476_idcnp;
            aParam[1] = new SqlParameter("@t476_orden", SqlDbType.TinyInt, 1);
            aParam[1].Value = t476_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CDP_USIM", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CDP_USIM", aParam);
        }

        public static CDP Obtener(SqlTransaction tr, int t476_idcnp)
        {
            CDP o = new CDP();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
            aParam[0].Value = t476_idcnp;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CDP_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CDP_O", aParam);

            if (dr.Read())
            {
                if (dr["t476_idcnp"] != DBNull.Value)
                    o.t476_idcnp = int.Parse(dr["t476_idcnp"].ToString());
                if (dr["t476_denominacion"] != DBNull.Value)
                    o.t476_denominacion = (string)dr["t476_denominacion"];
                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["t476_activo"] != DBNull.Value)
                    o.t476_activo = (bool)dr["t476_activo"];
                if (dr["t476_orden"] != DBNull.Value)
                    o.t476_orden = byte.Parse(dr["t476_orden"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CDP"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static SqlDataReader CatalogoDenominacion(string t476_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t476_denominacion", SqlDbType.Text, 70);
            aParam[0].Value = t476_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CDP_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CDP_CAT_USU", aParam);
        }

        public static SqlDataReader ObtenerCualificadorProyecto(string sTipo, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sTipo", SqlDbType.Char, 2);
            aParam[0].Value = sTipo;
            aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESPROY_C", aParam);
        }

        #endregion
    }
}
