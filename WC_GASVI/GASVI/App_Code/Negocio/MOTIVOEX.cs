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
    /// -----------------------------------------------------------------------------
    /// Project	 : GASVI
    /// Class	 : MOTIVOEX
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T424_MOTIVOEX
    /// </summary>
    /// <history>
    /// 	Creado por DOATAUBE	06/07/2011	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class MOTIVOEX
    {
        #region Metodos

        public static string CatalogoSN2()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = DAL.MOTIVOEX.ObtenerSN2();
            sb.Append("<table id='tblSN2' class='MAM W398' cellpadding='0' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:398px; ' />");
            sb.Append("</colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["identificativo"].ToString() + "' ");
                sb.Append("onClick='ms(this)' ");
                sb.Append("ondblclick='anadirConvocados(1);' ");
                sb.Append("onmouseover='TTip(event);' ");
                sb.Append("onmousedown='DD(event);' style='height:20px'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W390'>" + Utilidades.unescape(dr["denominacion"].ToString()) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoSN2Ex(bool bSoloActivos)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblSN2Ex' class='MM W398' cellpadding='0' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px;' />");
            sb.Append("    <col style='width:383px;' />");
            sb.Append("</colgroup>");
            string sColor="";
            SqlDataReader dr = DAL.MOTIVOEX.ObtenerSN2Ex(bSoloActivos);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t392_idsupernodo2"].ToString() + "' ");
                sb.Append("bd='' onmousedown='DD(event);' ");
                sb.Append("onClick='ms(this); visualizarTablas(this)' ");
                sb.Append("style='");
                if ((bool)dr["t392_estado"]) sColor = "";
                else sColor = "color:red;";
                sb.Append(sColor+"height:20px' onmouseover='TTip(event)'> ");
                sb.Append("<td style='padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W380'>" + Utilidades.unescape(dr["t392_denominacion"].ToString()) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoMotivo()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = DAL.MOTIVOEX.ObtenerMotivos(true);
            sb.Append("<table id='tblMotivos' class='MAM W398' cellpadding='0' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:398px;' />");
            sb.Append("</colgroup>");
            while (dr.Read())
            {
                if (dr["t423_idmotivo"].ToString() != "6")
                {
                    sb.Append("<tr id='" + dr["t423_idmotivo"].ToString() + "' ");
                    sb.Append("onClick='ms(this)' ");
                    sb.Append("ondblclick='anadirConvocados(2);' ");
                    sb.Append("onmouseover='TTip(event);' ");
                    sb.Append("onmousedown='DD(event);' style='height:20px'>");
                    sb.Append("<td style='padding-left:5px'><nobr class='NBR W340'>" + Utilidades.unescape(dr["t423_denominacion"].ToString()) + "</nobr></td>");
                    sb.Append("</tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoMotivosEx(string idSN2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblMotivosEx' class='MM W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col style='width:383px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.MOTIVOEX.ObtenerMotivosEx(int.Parse(idSN2));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t423_idmotivo"].ToString() + "' ");
                sb.Append("idsn2='" + idSN2 + "' ");
                sb.Append("bd='' onmousedown='DD(event);' ");
                sb.Append("onClick='ms(this);' ");
                sb.Append("style='height:20px' onmouseover='TTip(event)'> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td><nobr class='NBR W380'>" + Utilidades.unescape(dr["t423_denominacion"].ToString()) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        
        public static void Grabar(string sSN2Ex, string sMotivosEx)
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
                if (sSN2Ex != "")
                {
                    string[] aDatos = Regex.Split(sSN2Ex, "#sFin#");
                    for (int i = 0; i <= aDatos.Length - 1; i++)
                    {
                        string[] aElem = Regex.Split(aDatos[i], "#sCad#");
                        switch (aElem[0])
                        {
                            case "D":
                                DAL.MOTIVOEX.DeleteSN2MotivoExAll(tr, int.Parse(aElem[1]));
                                break;
                        }
                    }
                }

                if (sMotivosEx != "")
                {
                    string[] aDatos2 = Regex.Split(sMotivosEx, "#sFin#");
                    for (int i = 0; i <= aDatos2.Length - 1; i++)
                    {
                        string[] aElem2 = Regex.Split(aDatos2[i], "#sCad#");
                        switch (aElem2[0])
                        {
                            case "D":
                                DAL.MOTIVOEX.DeleteSN2MotivoEx(tr, int.Parse(aElem2[1]), short.Parse(aElem2[2]));
                                break;
                            case "I":
                                DAL.MOTIVOEX.InsertSN2MotivoEx(tr, int.Parse(aElem2[1]), short.Parse(aElem2[2]));
                                break;
                        }
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar los motivos de excepción.", ex);
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
