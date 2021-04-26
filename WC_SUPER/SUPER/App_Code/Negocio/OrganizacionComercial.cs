using System;
using System.Text;
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
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de OrganizacionComercial
    /// </summary>
    public class OrganizacionComercial
    {
        public OrganizacionComercial()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Obtiene el HTML de una tabla con la consulta de los datos de la TA212_ORGANIZACIONCOMERCIAL
        /// </summary>
        /// <returns></returns>
        public static string getHtmlNoLigadasNodo()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = null;
            dr = SUPER.DAL.OrganizacionComercial.NoLigadasNodo();
            sb.Append("<table id='tblDatos' class='texto MA' style='width:400px;'>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["ta212_idorganizacioncomercial"].ToString() + "' style='height:20px;' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;' title='" + dr["ta212_denominacion"].ToString() + "'>");
                sb.Append("<nobr class='NBR' style='width:395px;'>" + dr["ta212_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        /// <summary>
        /// Devuelve la denominación del primer nodo  con la misma organización comercial
        /// Si no existe, devuelve cadena vacía
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t303_idnodo"></param>
        /// <param name="ta212_idorganizacioncomercial"></param>
        /// <returns></returns>
        public static string NodosOrgCom(SqlTransaction tr, int t303_idnodo, Nullable<int> ta212_idorganizacioncomercial)
        {
            int idOrgCom = -1;
            if (ta212_idorganizacioncomercial != null)
                idOrgCom = (int)ta212_idorganizacioncomercial;

            return SUPER.DAL.OrganizacionComercial.NodosOrgCom(tr, t303_idnodo, idOrgCom);
        }

        public static SqlDataReader Catalogo(SqlTransaction tr, bool activa)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@ta212_activa", SqlDbType.Int, 4);
            aParam[0].Value = activa;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SIC_ORGANIZACIONCOMERCIAL_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SIC_ORGANIZACIONCOMERCIAL_C", aParam);
        }

    }
}