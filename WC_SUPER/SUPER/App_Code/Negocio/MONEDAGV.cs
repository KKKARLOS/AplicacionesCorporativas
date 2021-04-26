using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SUPER.Capa_Datos;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for MONEDAGV
/// </summary>

namespace SUPER.BLL
{
     public partial class MONEDAGV
     {
         #region Metodos

         public static List<ElementoLista> ObtenerMonedas(bool bSoloActivas)
         {
             List<ElementoLista> oLista = new List<ElementoLista>();
             SqlDataReader dr = SUPER.Capa_Datos.MONEDAGV.ObtenerCatalogo(bSoloActivas);
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

             SqlDataReader dr = SUPER.Capa_Datos.MONEDAGV.ObtenerCatalogo(false); //Pasamos el valor 0 porque queremos TODAS las monedas
             while (dr.Read())
             {
                 sCod = dr["t422_idmoneda"].ToString();
                 sb.Append("<tr id='" + sCod + "' bd='' onclick='mm(event);' style='height:20px'> ");
                 sb.Append("<td style='cursor:pointer; padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");

                 sb.Append("<td style='cursor:pointer; padding-left:2px;'>" + dr["t422_denominacion"].ToString() + "</td>");
                 sb.Append("<td style='cursor:pointer; padding-left:2px;'><input type='checkbox' style='width:15px; cursor:pointer'");
                 sb.Append(" name='chkActiva" + sCod + "' id='chkActiva" + sCod + "' class='checkTabla'");
                 if (dr["t422_estado"].ToString() == "True")
                     sb.Append(" checked");
                 sb.Append(" onclick=\"mfa(this.parentNode.parentNode,'U'); modificarCombo(this.parentNode.parentNode);\"></td>");
                 sb.Append("</tr>");
             }
             dr.Close();
             dr.Dispose();
             sb.Append("</table>");

             return sb.ToString();
         }

         public static void Grabar(string sDatos, string sDefecto)
         {
             string sResul = "";
             SqlConnection oConn = null;
             SqlTransaction tr = null;

             #region abrir conexión y transacción
             try
             {
                 oConn = Conexion.Abrir();
                 tr = Conexion.AbrirTransaccion(oConn);
             }
             catch (Exception)
             {
                 if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                 throw (new Exception("Error al abrir la conexión."));
             }
             #endregion

             try
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
                                 SUPER.Capa_Datos.MONEDAGV.UpdateMoneda(tr, aElem[1], int.Parse(aElem[2]));
                                 break;
                         }
                     }
                 }
                 if (sDefecto != "")
                 {
                     string[] aDefecto = Regex.Split(sDefecto, "#sCad#");
                     SUPER.Capa_Datos.MONEDAGV.UpdateDefectoMoneda(tr, aDefecto[0], aDefecto[1]);
                 }
                 Conexion.CommitTransaccion(tr);
             }
             catch (Exception ex)
             {
                 Conexion.CerrarTransaccion(tr);
                 sResul = Errores.mostrarError("Error al grabar las modificaciones.", ex);
             }
             finally
             {
                 Conexion.Cerrar(oConn);
                 if (sResul != "")
                     throw (new Exception(sResul));
             }
         }

         #endregion
     }
}
