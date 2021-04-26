using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;
using GEMO.DAL;

/// <summary>
/// Summary description for AVISOS
/// </summary>
namespace GEMO.BLL
{
    public class AVISOS
    {
        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.AVISOS.Catalogo();

            sb.Append("<table id='tblDatos' class='MANO' style='WIDTH: 500px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:490px;' /></colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["ID"].ToString() + "' bd='' onclick='mm(event)' style='height:20px'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td><input type='text' class='txtL' style='width:480px' value=\"" + dr["DENOMINACION"].ToString() + "\" maxlength='30' onKeyUp='fm(event)'></td>");
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

            #region abrir conexi�n y transacci�n
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexi�n."));
            }
            #endregion

            try
            {
                if (strDatos != "") //No se ha modificado nada
                {			
					string[] aAvisos = Regex.Split(strDatos, "///");
                    foreach (string oAvisos in aAvisos)
					{
                        if (oAvisos == "") continue;
                        string[] aValores = Regex.Split(oAvisos, "##");
						//0. Opcion BD. "I", "U", "D"
						//1. Id del aviso
						//2. Descripcion

						switch (aValores[0])
						{
							case "I":
                                nAux = GEMO.DAL.AVISOS.Insert(tr, Utilidades.unescape(aValores[2]));
								if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
								else sElementosInsertados += "//" + nAux.ToString();
								break;
							case "U":
                                GEMO.DAL.AVISOS.Update(tr, short.Parse(aValores[1]), Utilidades.unescape(aValores[2]));
								break;
							case "D":
                                GEMO.DAL.AVISOS.Delete(tr, short.Parse(aValores[1]));
								break;
						}
					}
					Conexion.CommitTransaccion(tr);
				}				
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar la lista de avisos.", ex);
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