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
    /// Class	 : SUPERNODO2
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T392_SUPERNODO2
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	10/06/2008 12:52:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class SUPERNODO2
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
        public static SUPERNODO2 Obtener(SqlTransaction tr, int t392_idsupernodo2)
        {
            SUPERNODO2 o = new SUPERNODO2();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
            aParam[0].Value = t392_idsupernodo2;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO2_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO2_O", aParam);

            if (dr.Read())
            {
                if (dr["t392_idsupernodo2"] != DBNull.Value)
                    o.t392_idsupernodo2 = int.Parse(dr["t392_idsupernodo2"].ToString());
                if (dr["t392_denominacion"] != DBNull.Value)
                    o.t392_denominacion = (string)dr["t392_denominacion"];
                if (dr["t392_estado"] != DBNull.Value)
                    o.t392_estado = (bool)dr["t392_estado"];

                if (dr["t392_activoqeq"] != DBNull.Value)
                    o.activoqeq = (bool)dr["t392_activoqeq"];

                if (dr["t393_idsupernodo3"] != DBNull.Value)
                    o.t393_idsupernodo3 = int.Parse(dr["t393_idsupernodo3"].ToString());
                if (dr["t392_orden"] != DBNull.Value)
                    o.t392_orden = int.Parse(dr["t392_orden"].ToString());
                if (dr["t314_idusuario_responsable"] != DBNull.Value)
                    o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
                if (dr["t392_denominacion_CSN2P"] != DBNull.Value)
                    o.t392_denominacion_CSN2P = (string)dr["t392_denominacion_CSN2P"];
                if (dr["t392_obligatorio_CSN2P"] != DBNull.Value)
                    o.t392_obligatorio_CSN2P = (bool)dr["t392_obligatorio_CSN2P"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO2"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static SqlDataReader ObtenerSuperNodo2UsuarioSegunVisionProyectosTEC(SqlTransaction tr, int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
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
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO2_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETVISION_SUPERNODO2_PROY_USUARIO_TEC", aParam);
            else
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO2_PROY_USUARIO_TEC_ADM", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETVISION_SUPERNODO2_PROY_USUARIO_TEC", aParam);
        }
        public static SqlDataReader ObtenerSuperNodo2Usuario(SqlTransaction tr, int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO2_USU", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO2_USU", aParam);
        }
        public static SqlDataReader ObtenerCualificadores(int nUsuario, int nIdEstructura, bool bActivo)
        {

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                SqlParameter[] aParam = new SqlParameter[2];

                aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
                aParam[0].Value = nIdEstructura;
                aParam[1] = new SqlParameter("@t487_activo", SqlDbType.Bit, 1);
                aParam[1].Value = bActivo;

                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESSUPERNODO2_ADM", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[3];
                aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                aParam[0].Value = nUsuario;
                aParam[1] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
                aParam[1].Value = nIdEstructura;
                aParam[2] = new SqlParameter("@t487_activo", SqlDbType.Bit, 1);
                aParam[2].Value = bActivo;


                return SqlHelper.ExecuteSqlDataReader("SUP_CUALIFICADORESSUPERNODO2_USU", aParam);
            }
        }
        public static SqlDataReader CatalogoSuperNodo2Usuario(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO2_CAT", aParam);

        }
        public static SqlDataReader CatalogoAdm()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO2_ADM", aParam);
        }
        public static int Update(SqlTransaction tr, int t392_idsupernodo2, string t392_denominacion_csn2p, bool t392_obligatorio_csn2p)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
            aParam[0].Value = t392_idsupernodo2;
            aParam[1] = new SqlParameter("@t392_denominacion_csn2p", SqlDbType.Text, 15);
            aParam[1].Value = t392_denominacion_csn2p;
            aParam[2] = new SqlParameter("@t392_obligatorio_csn2p", SqlDbType.Bit, 1);
            aParam[2].Value = t392_obligatorio_csn2p;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO2_UCU", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO2_UCU", aParam);
        }
        #endregion
    }
}
