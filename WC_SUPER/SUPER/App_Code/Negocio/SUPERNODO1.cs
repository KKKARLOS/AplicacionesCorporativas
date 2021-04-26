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
    /// Class	 : SUPERNODO1
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T391_SUPERNODO1
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	10/06/2008 12:52:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class SUPERNODO1
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

        public static SUPERNODO1 Obtener(SqlTransaction tr, int t391_idsupernodo1)
        {
            SUPERNODO1 o = new SUPERNODO1();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO1_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO1_O", aParam);

            if (dr.Read())
            {
                if (dr["t391_idsupernodo1"] != DBNull.Value)
                    o.t391_idsupernodo1 = int.Parse(dr["t391_idsupernodo1"].ToString());
                if (dr["t391_denominacion"] != DBNull.Value)
                    o.t391_denominacion = (string)dr["t391_denominacion"];
                if (dr["t392_idsupernodo2"] != DBNull.Value)
                    o.t392_idsupernodo2 = int.Parse(dr["t392_idsupernodo2"].ToString());
                if (dr["t391_estado"] != DBNull.Value)
                    o.t391_estado = (bool)dr["t391_estado"];

                if (dr["t391_activoqeq"] != DBNull.Value)
                    o.activoqeq = (bool)dr["t391_activoqeq"];
                
                if (dr["t391_orden"] != DBNull.Value)
                    o.t391_orden = int.Parse(dr["t391_orden"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
                if (dr["t391_denominacion_CSN1P"] != DBNull.Value)
                    o.t391_denominacion_CSN1P = (string)dr["t391_denominacion_CSN1P"];
                if (dr["t391_obligatorio_CSN1P"] != DBNull.Value)
                    o.t391_obligatorio_CSN1P = (bool)dr["t391_obligatorio_CSN1P"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO1"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static SqlDataReader ObtenerSuperNodo1UsuarioSegunVisionProyectosTEC(SqlTransaction tr, int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
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
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO1_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO1_PROY_USUARIO_TEC", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO1_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO1_PROY_USUARIO_TEC", aParam);
        }
        public static SqlDataReader ObtenerSuperNodo1Usuario(SqlTransaction tr, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO1_USU", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO1_USU", aParam);
        }
        public static SqlDataReader ObtenersSuperNodo1Usuario(SqlTransaction tr, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO1_USU", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO1_USU", aParam);
        }
        public static SqlDataReader ObtenerCualificadores(int nUsuario, int nIdEstructura, bool bActivo)
        {

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                SqlParameter[] aParam = new SqlParameter[2];

                aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
                aParam[0].Value = nIdEstructura;
                aParam[1] = new SqlParameter("@t485_activo", SqlDbType.Bit, 1);
                aParam[1].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESSUPERNODO1_ADM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
                aParam[1].Value = nIdEstructura;
                aParam[2] = new SqlParameter("@t485_activo", SqlDbType.Bit, 1);
                aParam[2].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESSUPERNODO1_USU", aParam);
            }
        }
        public static SqlDataReader CatalogoAdm()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO1_ADM", aParam);
        }
        public static int Update(SqlTransaction tr, int t391_idsupernodo1, string t391_denominacion_csn1p, bool t391_obligatorio_csn1p)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;
            aParam[1] = new SqlParameter("@t391_denominacion_csn1p", SqlDbType.Text, 15);
            aParam[1].Value = t391_denominacion_csn1p;
            aParam[2] = new SqlParameter("@t391_obligatorio_csn1p", SqlDbType.Bit, 1);
            aParam[2].Value = t391_obligatorio_csn1p;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO1_UCU", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO1_UCU", aParam);
        }
        #endregion
    }
}
