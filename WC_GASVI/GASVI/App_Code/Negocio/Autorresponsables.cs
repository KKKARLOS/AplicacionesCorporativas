using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;

namespace GASVI.BLL
{
    public partial class AUTORRESPONSABLE
    {
        #region Metodos

        public static string CatalogoIntegrantes()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblIntegrantes' class='MM W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:339px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.AUTORRESPONSABLE.ObtenerIntegrantes();
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("onmousedown=\"DD(event);\" ");
                sb.Append("onClick='mm(event);' ");
                sb.Append("style='height:20px' onmouseover='TTip(event)'> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W340'>" + Utilidades.unescape(dr["nombre"].ToString()) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string obtenerPersonas(string sApellido1, string sApellido2, string sNombre, string sExcluidos)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = DAL.AUTORRESPONSABLE.CatalogoPersonas(Utilidades.unescape(sApellido1), Utilidades.unescape(sApellido2), Utilidades.unescape(sNombre), sExcluidos, false);
            sb.Append("<table id='tblPersonas' class='MAM W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:378px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("onClick='mm(event)' ");
                sb.Append("ondblclick='anadirConvocados();' ");
                sb.Append("onmouseover='TTip(event);' ");
                sb.Append("onmousedown=\"DD(event);\"' style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W350'>" + Utilidades.unescape(dr["nombre"].ToString()) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        
        public static void Grabar(string sDatos)
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
                            case "D":
                                DAL.AUTORRESPONSABLE.DeleteAutorresponsable(tr, int.Parse(aElem[1]));
                                break;
                            case "I":
                                DAL.AUTORRESPONSABLE.InsertAutorresponsable(tr, int.Parse(aElem[1]));
                                break;

                        }
                    }
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
