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
    /// <summary>
    /// Summary description for ConsultasPSP
    /// </summary>
    public class ConsultasPSP
    {
        public ConsultasPSP()
        {

        }

        #region Metodos

        public static SqlDataReader ObtenerParteActividadCriterios(int t314_idusuario, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[1].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_PARTE_ACTIVIDAD_CRITERIOS", aParam);
        }
        public static SqlDataReader ObtenerProyectos(int t314_idusuario, int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOS_MASIVO", aParam);
        }
        #endregion
    }
}
