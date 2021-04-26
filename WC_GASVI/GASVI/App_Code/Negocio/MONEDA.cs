using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
    public partial class MONEDA
    {
        #region Metodos

        public static List<ElementoLista> ObtenerMonedas(bool bSoloActivas)
        {
            List<ElementoLista> oLista = new List<ElementoLista>();
            SqlDataReader dr = DAL.MONEDA.ObtenerCatalogo(bSoloActivas);
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["t422_idmoneda"].ToString(), dr["t422_denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static string ObtenerTodasMonedas()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblMonedas' class='MANO' style='width:300px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px;' />");
            sb.Append("    <col style='width:235px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("</colgroup>");
            string sCod = "";

            SqlDataReader dr = DAL.MONEDA.ObtenerCatalogo(false); //Pasamos el valor 0 porque queremos TODAS las monedas
            while (dr.Read())
            {
                sCod = dr["t422_idmoneda"].ToString();
                sb.Append("<tr id='" + sCod + "' ");
                sb.Append("bd='' ");
                sb.Append("onClick='mm(event);'");
                sb.Append("style='height:20px'> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td>" + dr["t422_denominacion"].ToString() + "</td>");
                sb.Append("<td style='text-align:center'><input type='checkbox' style='width:15px; cursor:pointer'");
                sb.Append(" name='chkActiva" + sCod + "' id='chkActiva" + sCod + "' class='checkTabla'");
                if (dr["t422_estado"].ToString() == "True")
                    sb.Append(" checked");
                //sb.Append(" onclick=\"mfa(this.parentNode.parentNode,'U'); modificarCombo(this.parentNode.parentNode);\"></td>");
                sb.Append(" onclick=\"mfa(this.parentNode.parentNode,'U');\"></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static void Grabar(string sDatos)
        {
            if (sDatos != "")
            {
                //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aDatos = Regex.Split(sDatos, "#sFin#");
                for (int i = 0; i <= aDatos.Length - 1; i++)
                {
                    string[] aElem = Regex.Split(aDatos[i], "#sCad#");
                    switch (aElem[0])
                    {
                        case "U":
                            DAL.MONEDA.UpdateMoneda(null, aElem[1], int.Parse(aElem[2]));
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
