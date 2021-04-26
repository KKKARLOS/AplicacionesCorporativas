using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.JScript;
using GEMO.DAL;

/// <summary>
/// Summary description for MEDIO
/// </summary>
namespace GEMO.BLL
{
    public class MEDIO
    {
        public static string Obtener(string t134_denominacion, string sTipoBusqueda, Nullable<int> t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MAM' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='padding-left:5px;'></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = GEMO.DAL.MEDIO.Catalogo(t134_denominacion, sTipoBusqueda, t001_idficepi);

            int i = 0;
            bool bExcede = false;
            while (dr.Read())
            {
                i++;
                sb.Append("<tr id='" + dr["ID"].ToString() + "' "); 
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;' onmouseover='TTip(event)'>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);

                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();
            if (!bExcede)
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
            }
            else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }
            return sb.ToString();
        }
        public static string Obtener2(string sTS)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='padding-left:5px;'></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = GEMO.DAL.MEDIO.Catalogo2();

            int i = 0;
            bool bExcede = false;
            while (dr.Read())
            {
                i++;
                sb.Append("<tr id='" + dr["ID"].ToString() + "' onclick=");

                if (sTS == "S") sb.Append("'ms(this)'");
                else sb.Append("'mm(event)'");

                sb.Append(" ondblclick='aceptarClick(this.rowIndex)' style='height:20px;' onmouseover='TTip(event)'>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);

                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();
            if (!bExcede)
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
            }
            else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }
            return sb.ToString();
        }
        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.MEDIO.Catalogo2();

            sb.Append("<table id='tblDatos' class='MANO' style='WIDTH: 500px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:490px;' /></colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["ID"].ToString() + "' bd='' onclick='mm(event)' style='height:20px'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td><input type='text' class='txtL' style='width:480px' value=\"" + dr["DENOMINACION"].ToString() + "\" maxlength='50' onKeyUp='fm(event)'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string Grabar(string strDatos)
        {
            string sElementosInsertados = "";
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            int nAux = 0;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                if (strDatos != "") //No se ha modificado nada
                {			
					string[] aMedios = Regex.Split(strDatos, "///");
					foreach (string oMedio in aMedios)
					{
						if (oMedio == "") continue;
						string[] aValores = Regex.Split(oMedio, "##");
						//0. Opcion BD. "I", "U", "D"
						//1. Id del Medio
						//2. Descripcion

						switch (aValores[0])
						{
							case "I":
                                nAux = GEMO.DAL.MEDIO.Insert(tr, Utilidades.unescape(aValores[2]));
								if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
								else sElementosInsertados += "//" + nAux.ToString();
								break;
							case "U":
                                GEMO.DAL.MEDIO.Update(tr, short.Parse(aValores[1]), Utilidades.unescape(aValores[2]));
								break;
							case "D":
								GEMO.DAL.MEDIO.Delete(tr, short.Parse(aValores[1]));
								break;
						}
					}
					Conexion.CommitTransaccion(tr);
				}				
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar la lista de medios.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul!="")
                    throw (new Exception(sResul));
            }
            sResul = sElementosInsertados;			
            return sResul;
        }
    }
}