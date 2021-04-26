using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.DAL;

namespace SUPER.BLL
{
    public partial class HISTORICOEJECUTAALERTAS
    {
        #region Metodos

        public static string CatalogoUltimasEjecuciones()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                #region Cabecera tabla HTML
                sb.Append(@"<table id='tblDatos' style='width:480px;' cellpadding='0' cellspacing='0' border='0' mantenimiento='1'>
                        <colgroup>
                            <col style='width:90px;' />
			                <col style='width:270px;' />
			                <col style='width:120px;' />
                        </colgroup>");
                #endregion

                SqlDataReader dr = SUPER.DAL.HISTORICOEJECUTAALERTAS.CatalogoUltimasEjecuciones(null);
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px;'>");
                    sb.Append("<td style='padding-left:3px;'>" + dr["Mes"].ToString() + "</td>");
                    sb.Append("<td><nobr class='NBR W250' onmouseover='TTip(event)'>" + dr["Profesional"].ToString() + "</nobr></td>");
                    sb.Append("<td style='text-align:center;'>" + dr["t181_fejecucion"].ToString().Substring(0,dr["t181_fejecucion"].ToString().Length-3)  + "</td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");
                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener las alertas", ex);
            }
        }

        #endregion
    }
}
