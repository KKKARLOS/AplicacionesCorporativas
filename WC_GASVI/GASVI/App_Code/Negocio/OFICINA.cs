using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
    public class OFICINA
    {
        #region Propiedades

        private int _t010_idoficina;
        public int t010_idoficina
        {
            get { return _t010_idoficina; }
            set { _t010_idoficina = value; }
        }

        private string _t010_desoficina;
        public string t010_desoficina
        {
            get { return _t010_desoficina; }
            set { _t010_desoficina = value; }
        }

        #endregion

        public OFICINA()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public OFICINA(int nOficina, string sDenominacion)
        {
            this.t010_idoficina = nOficina;
            this.t010_desoficina = sDenominacion;
        }

        public static string mostrarOficinas()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblOficinas' mantenimiento='1' class='MANO' cellpadding='0' style='width:600;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px;' />");
            sb.Append("     <col style='width:290px;' />");
            sb.Append("     <col style='width:295px;' />");
            sb.Append("</colgroup>");

            string strOfiLiq = catalogoOficinasLiquidadoras();
            string[] aOfiLiq = Regex.Split(strOfiLiq, "#sFin#");
            SqlDataReader dr = DAL.OFICINA.ObtenerOficinas();
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t010_idoficina"].ToString() + "' style='height:20px; padding:0px' ");
                sb.Append("idOfLiq='" + dr["t010_idoficina_liquidadora"].ToString() + "' ");
                sb.Append("onClick='activarCombo(this.id);' ");
                sb.Append(">");
                sb.Append("<td style='padding-left:5px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t010_desoficina"].ToString() + "</td>");
                /*****************************/
                sb.Append("<td style='padding-left:5px;'>");
                sb.Append("<input type='text' id='txtOfiLiq" + dr["t010_idoficina"].ToString() + "' class='txtL' style='width:210px; margin-bottom:2px;' value='" + dr["oficinaLiquidadora"].ToString() + "'>");
                sb.Append("<select class='combo' id='cboOfiLiq" + dr["t010_idoficina"].ToString() + "' style='width:0px; visibility:hidden;' onChange='actualizarCampos(parentNode.parentNode, this); activarGrabar();'> ");

                for (int i = 0, nCount = aOfiLiq.Length; i < nCount; i++)
                {
                    string[] aElem = Regex.Split(aOfiLiq[i], "#sCad#");
                    sb.Append("<option value='" + aElem[0] + "'");
                    if (dr["t010_idoficina_liquidadora"].ToString() == aElem[0]) sb.Append(" selected='selected'");
                    sb.Append(">" + aElem[1] + "</option>");
                }
                sb.Append("</select></td>");
                /*****************************/
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string catalogoOficinasLiquidadoras(){
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = DAL.OFICINA.ObtenerOficinas();
            sb.Append("");
            while (dr.Read())
            {
                sb.Append(dr["t010_idoficina"].ToString() + "#sCad#" + dr["t010_desoficina"].ToString() + "#sFin#");
            }
            dr.Close();
            dr.Dispose();
            if (sb.ToString() != "")
                return sb.ToString().Substring(0, sb.ToString().Length - 6);
            else return sb.ToString();
        
        }

        public static void Grabar(string sDatos)
        {
            if (sDatos != "")
            {
                string[] aDatos = Regex.Split(sDatos, "#sFin#");
                for (int i = 0; i <= aDatos.Length - 1; i++)
                {
                    string[] aElem = Regex.Split(aDatos[i], "#sCad#");
                    DAL.OFICINA.UpdateOficinaLiquidadora(null, int.Parse(aElem[0]), int.Parse(aElem[1]));
                }
            }
        }
    }

}