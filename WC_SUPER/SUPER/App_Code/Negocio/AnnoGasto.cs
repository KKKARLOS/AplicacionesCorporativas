using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security; //para gestion de roles
using SUPER.Capa_Datos;
using SUPER.Capa_Negocio;

using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace SUPER.BLL
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
            sb.Append("<table id='tblAnnoGasto' class='MANO' style='width:235px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("</colgroup>" + (char)13);

            SqlDataReader dr = SUPER.Capa_Datos.AnnoGasto.CatalogoAnnogasto(null);
            while (dr.Read())
            {
                int id = int.Parse(dr["t419_anno"].ToString());
                sb.Append("<tr id='" + id + "' bd='' onClick='mm(event)' style='height:20px'> ");
                sb.Append("<td style='cursor:pointer; padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td style='cursor:pointer; padding-left:2px;'><input id='anno" + id + "' type='text' style='width:60px; text-align:left' class='txtNumL' SkinID='Numero' onKeyUp='fm(event)' onkeypress='vtn2(event);' onchange='comprobarFecha(" + id + "); activarGrabar();' value=\"");
                sb.Append(id + "\" maxlength='25'></td>");
                sb.Append("<td style='cursor:pointer; padding-left:2px;'><input id='desde" + id + "' type='text' style='width:60px;' class='txtNumL' SkinID='Numero' Calendar='oCal' onclick='mc(event);' onchange='fm(event);activarGrabar();' readonly='true' goma='0' value=\"");
                if (dr["t419_fdesde"] != DBNull.Value)
                    sb.Append(((DateTime)dr["t419_fdesde"]).ToShortDateString() + "\"></td>");
                else sb.Append("\"></td>");
                sb.Append("<td style='cursor:pointer; padding-left:2px;'><input id='hasta" + id + "' type='text' style='width:60px;' class='txtNumL' SkinID='Numero' Calendar='oCal' onclick='mc(event);' onchange='fm(event);activarGrabar();' readonly='true' goma='0' value=\"");
                if (dr["t419_fhasta"] != DBNull.Value)
                    sb.Append(((DateTime)dr["t419_fhasta"]).ToShortDateString() + "\"></td>");
                else sb.Append("\"></td>");


                sb.Append("</tr>" + (char)13);
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
                                SUPER.Capa_Datos.AnnoGasto.InsertAnnoGasto(tr, int.Parse(aElem[2]), DateTime.Parse(aElem[3]), DateTime.Parse(aElem[4]), int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()), DateTime.Today);
                                break;
                            case "D":
                                SUPER.Capa_Datos.AnnoGasto.DeleteAnnoGasto(tr, int.Parse(aElem[1]));
                                break;
                            case "U":
                                SUPER.Capa_Datos.AnnoGasto.UpdateAnnoGasto(tr, int.Parse(aElem[1]), int.Parse(aElem[2]), DateTime.Parse(aElem[3]), DateTime.Parse(aElem[4]), int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()), DateTime.Today);
                                break;
                        }
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul =SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar los años gastos.", ex);
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
            SqlDataReader dr = SUPER.Capa_Datos.AnnoGasto.CatalogoAnnogasto(tr);
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