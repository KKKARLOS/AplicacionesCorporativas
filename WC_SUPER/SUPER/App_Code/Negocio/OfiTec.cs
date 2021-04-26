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
using System.Collections.Generic;
//
using SUPER.Capa_Negocio;
using SUPER.Capa_Datos;
//Para usar StringBuilder
using System.Text;
namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for OfiTec
    /// </summary>
    public class OfiTec
    {
        public OfiTec()
        {
            // TODO: Add constructor logic here
        }
        //Metodos
        public static SqlDataReader CatalogoIntegrantes(int nCodCR)
        {//Obtención del catalogo de integrantes de una Oficina Técnica
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nCodCR", SqlDbType.Int);
            aParam[0].Value = nCodCR;

            return SqlHelper.ExecuteSqlDataReader("SUP_OTSCATA", aParam);
        }
        public static void BorrarIntegrantes(int iCodUne)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nCodigo", SqlDbType.Int, 4);
            aParam[0].Value = iCodUne;

            SqlHelper.ExecuteNonQuery("SUP_OTD", aParam);
        }
        public static void BorrarIntegrante(SqlTransaction tr, int iCodUne, int iNumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nEmpleado", SqlDbType.Int, 4);
            aParam[0].Value = iCodUne;
            aParam[1].Value = iNumEmpleado;
            SqlHelper.ExecuteScalarTransaccion(tr, "SUP_OT_D", aParam);
        }
        public static void InsertarIntegrante(SqlTransaction tr, int iCodUne, int iNumEmpleado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nCodigo", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nEmpleado", SqlDbType.Int, 4);
            aParam[0].Value = iCodUne;
            aParam[1].Value = iNumEmpleado; 
            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_OTI", aParam);
        }
    }
}