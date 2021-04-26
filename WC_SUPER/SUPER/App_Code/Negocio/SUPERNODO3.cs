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
    /// Class	 : SUPERNODO3
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T393_SUPERNODO3
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	10/06/2008 12:52:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class SUPERNODO3
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

        public static SUPERNODO3 Obtener(SqlTransaction tr, int t393_idsupernodo3)
        {
            SUPERNODO3 o = new SUPERNODO3();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO3_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO3_O", aParam);

            if (dr.Read())
            {
                if (dr["t393_idsupernodo3"] != DBNull.Value)
                    o.t393_idsupernodo3 = int.Parse(dr["t393_idsupernodo3"].ToString());
                if (dr["t393_denominacion"] != DBNull.Value)
                    o.t393_denominacion = (string)dr["t393_denominacion"];
                if (dr["t394_idsupernodo4"] != DBNull.Value)
                    o.t394_idsupernodo4 = int.Parse(dr["t394_idsupernodo4"].ToString());
                if (dr["t393_estado"] != DBNull.Value)
                    o.t393_estado = (bool)dr["t393_estado"];

                if (dr["t393_activoqeq"] != DBNull.Value)
                    o.activoqeq = (bool)dr["t393_activoqeq"];
                
                if (dr["t393_orden"] != DBNull.Value)
                    o.t393_orden = int.Parse(dr["t393_orden"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
                if (dr["t393_denominacion_CSN3P"] != DBNull.Value)
                    o.t393_denominacion_CSN3P = (string)dr["t393_denominacion_CSN3P"];
                if (dr["t393_obligatorio_CSN3P"] != DBNull.Value)
                    o.t393_obligatorio_CSN3P = (bool)dr["t393_obligatorio_CSN3P"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO3"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static SqlDataReader ObtenerSuperNodo3UsuarioSegunVisionProyectosTEC(SqlTransaction tr, int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
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
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO3_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO3_PROY_USUARIO_TEC", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO3_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO3_PROY_USUARIO_TEC", aParam);
        }
        public static SqlDataReader ObtenerSuperNodo3Usuario(SqlTransaction tr, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO3_USU", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO3_USU", aParam);
        }
        public static SqlDataReader ObtenerCualificadores(int nUsuario, int nIdEstructura, bool bActivo)
        {

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                SqlParameter[] aParam = new SqlParameter[2];

                aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
                aParam[0].Value = nIdEstructura;
                aParam[1] = new SqlParameter("@t489_activo", SqlDbType.Bit, 1);
                aParam[1].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESSUPERNODO3_ADM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
                aParam[1].Value = nIdEstructura;
                aParam[2] = new SqlParameter("@t489_activo", SqlDbType.Bit, 1);
                aParam[2].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader( "SUP_CUALIFICADORESSUPERNODO3_USU", aParam);
            }
        }

        public static SqlDataReader CatalogoAdm()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO3_ADM", aParam);
        }
        public static int Update(SqlTransaction tr, int t393_idsupernodo3, string t393_denominacion_csn3p, bool t393_obligatorio_csn3p)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
            aParam[0].Value = t393_idsupernodo3;
            aParam[1] = new SqlParameter("@t393_denominacion_csn3p", SqlDbType.Text, 15);
            aParam[1].Value = t393_denominacion_csn3p;
            aParam[2] = new SqlParameter("@t393_obligatorio_csn3p", SqlDbType.Bit, 1);
            aParam[2].Value = t393_obligatorio_csn3p;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO3_UCU", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO3_UCU", aParam);
        }
        #endregion
    }
}
