using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : SUPERNODO4
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T394_SUPERNODO4
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	10/06/2008 12:52:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class SUPERNODO4
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

        public static SUPERNODO4 Obtener(SqlTransaction tr, int t394_idsupernodo4)
        {
            SUPERNODO4 o = new SUPERNODO4();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
            aParam[0].Value = t394_idsupernodo4;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO4_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO4_O", aParam);

            if (dr.Read())
            {
                if (dr["t394_idsupernodo4"] != DBNull.Value)
                    o.t394_idsupernodo4 = int.Parse(dr["t394_idsupernodo4"].ToString());
                if (dr["t394_denominacion"] != DBNull.Value)
                    o.t394_denominacion = (string)dr["t394_denominacion"];
                if (dr["t394_estado"] != DBNull.Value)
                    o.t394_estado = (bool)dr["t394_estado"];
                if (dr["t394_activoqeq"] != DBNull.Value)
                    o.activoqeq = (bool)dr["t394_activoqeq"];
                if (dr["t394_orden"] != DBNull.Value)
                    o.t394_orden = int.Parse(dr["t394_orden"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
                if (dr["t394_denominacion_CSN4P"] != DBNull.Value)
                    o.t394_denominacion_CSN4P = (string)dr["t394_denominacion_CSN4P"];
                if (dr["t394_obligatorio_CSN4P"] != DBNull.Value)
                    o.t394_obligatorio_CSN4P = (bool)dr["t394_obligatorio_CSN4P"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO4"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static SqlDataReader ObtenerSuperNodo4UsuarioSegunVisionProyectosTEC(SqlTransaction tr, int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
        {
            SqlParameter[] aParam = null;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                aParam = new SqlParameter[1];
                aParam[0] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[0].Value = bSoloActivos;
            }
            else
            {
                aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
                aParam[1].Value = bMostrarBitacoricos;
                aParam[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
                aParam[2].Value = bSoloActivos;
            }


            if (tr == null)
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO4_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO4_PROY_USUARIO_TEC", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO4_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO4_PROY_USUARIO_TEC", aParam);
        }

        public static SqlDataReader ObtenerSuperNodo4Usuario(SqlTransaction tr, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO4_USU", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO4_USU", aParam);
        }
        public static SqlDataReader ObtenerCualificadores(int nUsuario, int nIdEstructura, bool bActivo)
        {

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                SqlParameter[] aParam = new SqlParameter[2];

                aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
                aParam[0].Value = nIdEstructura;
                aParam[1] = new SqlParameter("@t491_activo", SqlDbType.Bit, 1);
                aParam[1].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESSUPERNODO4_ADM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
                aParam[1].Value = nIdEstructura;
                aParam[2] = new SqlParameter("@t491_activo", SqlDbType.Bit, 1);
                aParam[2].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESSUPERNODO4_USU", aParam);
            }
        }

        public static SqlDataReader CatalogoAdm()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO4_ADM", aParam);
        }
        public static int Update(SqlTransaction tr, int t394_idsupernodo4, string t394_denominacion_csn4p, bool t394_obligatorio_csn4p)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
            aParam[0].Value = t394_idsupernodo4;
            aParam[1] = new SqlParameter("@t394_denominacion_csn4p", SqlDbType.Text, 15);
            aParam[1].Value = t394_denominacion_csn4p;
            aParam[2] = new SqlParameter("@t394_obligatorio_csn4p", SqlDbType.Bit, 1);
            aParam[2].Value = t394_obligatorio_csn4p;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO4_UCU", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO4_UCU", aParam);
        }
        #endregion
    }
}
