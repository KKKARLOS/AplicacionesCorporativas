using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.JScript;
using GEMO.DAL;

namespace GEMO.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : TARIFADATOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T711_TARIFADATOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public class TARIFADATOS
	{
		#region Metodos
        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.TARIFADATOS.Catalogo();

            sb.Append("<table id='tblDatos' class='MA' style='WIDTH: 600px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:300px;padding-left:5px;'/><col style='width:290px;'/></colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["ID"].ToString() + "'  bd='' onclick=\"mm(event)\" ondblclick=\"Detalle(this)\" style='height:16px;' >");
                sb.Append("<td><img src='../../../../../images/imgFN.gif'></td>");
                sb.Append("<td><nobr class='NBR W290'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W280'>" + dr["PROVEEDOR"].ToString() + "</nobr></td>");           
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
            //int nAux = 0;

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
                    string[] aTarifaDatos = Regex.Split(strDatos, "///");
                    foreach (string oTarifaDatos in aTarifaDatos)
                    {
                        if (oTarifaDatos == "") continue;
                        string[] aValores = Regex.Split(oTarifaDatos, "##");
                        //0. Opcion BD. "I", "U", "D"
                        //1. Id del Medio
                        //2. Descripcion

                        switch (aValores[0])
                        {
                            //case "I":
                            //    nAux = GEMO.DAL.MEDIO.Insert(tr, Utilidades.unescape(aValores[2]));
                            //    if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                            //    else sElementosInsertados += "//" + nAux.ToString();
                            //    break;
                            //case "U":
                            //    GEMO.DAL.MEDIO.Update(tr, short.Parse(aValores[1]), Utilidades.unescape(aValores[2]));
                            //    break;
                            case "D":
                                GEMO.DAL.TARIFADATOS.Delete(tr, short.Parse(aValores[1]));
                                break;
                        }
                    }
                    Conexion.CommitTransaccion(tr);
                }
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar la lista de tarifa de datos.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            sResul = sElementosInsertados;
            return sResul;
        }

        public static void Eliminar(string args)
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
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
				string[] aArgs = Regex.Split(args, "///");
				for (int i = 0; i < aArgs.Length; i++)
				{
                    GEMO.DAL.TARIFADATOS.Delete(tr, short.Parse(aArgs[i].ToString()));
				}			
			
				Conexion.CommitTransaccion(tr);
			
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al eliminar las tarifas de datos.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul!="")
                    throw (new Exception(sResul));
            }
        }
        public static string Grabar   (
                                    byte byteNueva,                                 // 0=UPDATE 1=INSERT
                                    short iID,                                      // t711_idtarifa
                                    string sDenominacion,                           // t711_denominacion
                                    byte bIdProveedor,                              // t063_idproveedor
                                    string sCodTarProv,                             // t711_CodTarProv
                                    double dPrecio                                // t711_precio
                                    //double dDesdeAcep,                             // t711_desde_acep
                                    //double dHastaAcep                              // t711_hasta_acep
                                    )
        {
            int nID = -1;
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

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
                if (byteNueva == 1)
                {
                    nID = GEMO.DAL.TARIFADATOS.Insert
                                    (
                                    tr,
                                    sDenominacion,                                  // t711_denominacion
                                    bIdProveedor,                                   // t063_idproveedor
                                    sCodTarProv,                                    // t711_CodTarProv 
                                    dPrecio                                        // t711_precio
                                    //dDesdeAcep,                                     // t711_desde_acep
                                    //dHastaAcep                                      // t711_hasta_acep
                                    );
                }
                else //update
                {
                    GEMO.DAL.TARIFADATOS.Update(
                                    tr,
                                    iID,
                                    sDenominacion,                                  // t711_denominacion
                                    bIdProveedor,                                   // t063_idproveedor
                                    sCodTarProv,                                    // t711_CodTarProv 
                                    dPrecio                                        // t711_precio
                                    //dDesdeAcep,                                     // t711_desde_acep
                                    //dHastaAcep                                      // t711_hasta_acep
                                    );
                    nID = iID;
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar la línea.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return nID.ToString("##,###");
        }

        #endregion
	}
}
