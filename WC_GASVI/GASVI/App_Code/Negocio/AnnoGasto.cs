using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security; //para gestion de roles
using GASVI.DAL;
using Microsoft.JScript;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace GASVI.BLL
{
	public partial class AnnoGasto
    {
        #region Propiedades
        
        #endregion

        public AnnoGasto()
		{
			
		}

        public static string CatalogoAnnogasto()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblAnnoGasto' class='MANO' cellpadding='0' cellspacing='0' border='0' style='width:235px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; cursor:pointer;' />");
            sb.Append("    <col style='width:80px; cursor:pointer;' />");
            sb.Append("    <col style='width:70px; cursor:pointer;' />");
            sb.Append("    <col style='width:70px; cursor:pointer;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.AnnoGasto.CatalogoAnnogasto(null);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t419_anno"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("onClick='mm(event)'");
                sb.Append("style='height:20px'> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td><input id='anno" + dr["t419_anno"].ToString() + "' type='text' style='width:35px; text-align:left' class='txtNumL' SkinID='Numero' onKeyUp='fm(event)' onkeypress='vtn2(event);' onchange='comprobarFecha(" + int.Parse(dr["t419_anno"].ToString()) + "); activarGrabar();' value=\"");
                sb.Append(int.Parse(dr["t419_anno"].ToString()) + "\" maxlength='4'></td>");
                sb.Append("<td><input id='desde" + dr["t419_anno"].ToString() + "' type='text' style='width:60px;' class='txtNumL' SkinID='Numero' Calendar='oCal' onclick='mc(this);' onchange='fm(event);activarGrabar();' ReadOnly='true' goma='0' value=\"");
                if (dr["t419_fdesde"] != DBNull.Value)
                    sb.Append(((DateTime)dr["t419_fdesde"]).ToShortDateString() + "\"></td>");
                else sb.Append("\"></td>");
                sb.Append("<td><input id='hasta" + dr["t419_anno"].ToString() + "' type='text' style='width:60px;' class='txtNumL' SkinID='Numero' Calendar='oCal' onclick='mc(this);' onchange='fm(event);activarGrabar();' ReadOnly='true' goma='0' value=\"");
                if (dr["t419_fhasta"] != DBNull.Value)
                    sb.Append(((DateTime)dr["t419_fhasta"]).ToShortDateString() + "\"></td>");
                else sb.Append("\"></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
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
                            case "I":
                                DAL.AnnoGasto.InsertAnnoGasto(tr, int.Parse(aElem[2]), DateTime.Parse(aElem[3]), DateTime.Parse(aElem[4]), int.Parse(HttpContext.Current.Session["GVT_IDFICEPI"].ToString()), DateTime.Today);
                                break;
                            case "D":
                                DAL.AnnoGasto.DeleteAnnoGasto(tr, int.Parse(aElem[1]));
                                break;
                            case "U":
                                DAL.AnnoGasto.UpdateAnnoGasto(tr, int.Parse(aElem[1]), int.Parse(aElem[2]), DateTime.Parse(aElem[3]), DateTime.Parse(aElem[4]), int.Parse(HttpContext.Current.Session["GVT_IDFICEPI"].ToString()), DateTime.Today);
                                break;
                        }
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar los años gastos.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }

        public static Hashtable ObtenerHTAnnoGasto(SqlTransaction tr)
        {
            Hashtable htAnnoGasto = new Hashtable();
            SqlDataReader dr = DAL.AnnoGasto.CatalogoAnnogasto(tr);
            while (dr.Read())
            {
                htAnnoGasto.Add((int)((short)dr["t419_anno"]), new DateTime[] {
                                                                (DateTime)dr["t419_fdesde"],
                                                                (DateTime)dr["t419_fhasta"]
                                                                });

            }
            dr.Close();
            dr.Dispose();

            return htAnnoGasto;
        }

	}
}