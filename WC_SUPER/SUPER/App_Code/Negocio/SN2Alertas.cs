using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
using SUPER.BLL;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de SN2Alertas
    /// </summary>
    public partial class SN2Alertas
    {
        #region Propiedades y Atributos
        #endregion
        #region Constructor
        public SN2Alertas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion
        #region Metodos
        public static string Grabar(string strDatos)
        {
            #region Inicio Transacción

            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion
            try
            {
                #region Grabar
                string sAux = "";
                int? nPar1 = null;
                int? nPar2 = null;
                int? nPar3 = null;
                string[] aAlerta = Regex.Split(strDatos, "///");
                foreach (string sAlerta in aAlerta)
                {
                    string[] aDatosAlerta = Regex.Split(sAlerta, "##");
                    switch (aDatosAlerta[0])
                    {
                        case "U":
                            sAux = aDatosAlerta[2].Replace(".", "");
                            if (sAux != "") nPar1 = int.Parse(sAux);
                            else nPar1 = null;

                            sAux = aDatosAlerta[3].Replace(".", "");
                            if (sAux != "") nPar2 = int.Parse(sAux);
                            else nPar2 = null;

                            sAux = aDatosAlerta[4].Replace(".", "");
                            if (sAux != "") nPar3 = int.Parse(sAux);
                            else nPar3 = null;

                            DAL.SN2Alertas.Update(tr, int.Parse(aDatosAlerta[1].ToString()), nPar1, nPar2, nPar3);
                            break;
                    }
                }

                #endregion


                Conexion.CommitTransaccion(tr);
                return "OK@#@";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }
        public static string CatalogoById(int t392_idsupernodo2)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblAlertas' class='texto MANO' style='width:930px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:30px'/><col style='width:210px'/><col style='width:165px'/><col style='width:65px'/><col style='width:165px'/><col style='width:65px'/><col style='width:165px'/><col style='width:65px'/></colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = SUPER.DAL.SN2Alertas.CatalogoById(null, t392_idsupernodo2);
            while (dr.Read())
            {
                sb.Append("<tr id=" + dr["t824_idsn2alertas"].ToString());
                sb.Append(" bd='N' onclick='ms(this)' onkeyup='fm(event); aG(3)' style='height:20px;'>");

                sb.Append("<td style='text-align:right;padding-right:3px'>" + dr["t820_idalerta"].ToString() + "</td>");
                sb.Append("<td style='padding-left:3px;' class='tdcol'><nobr class='NBR W210' onmouseover='TTip(event)'>" + dr["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W160' onmouseover='TTip(event)'>" + dr["t820_parametro1"].ToString() + "</nobr></td>");
                if (dr["t820_parametro1"].ToString() == "")
                    sb.Append("<td class='tdcol'></td>");
                else
                    sb.Append("<td class='tdcol'><input type='text' class='txtNumL' SkinID='Numero' onkeypress='vtn(event);' onfocus='fn(this,8,0)' style='width:55px' value='" + Utilidades.getInt(dr["t824_parametro1"].ToString()) + "' maxlength='8'></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W160' onmouseover='TTip(event)'>" + dr["t820_parametro2"].ToString() + "</nobr></td>");
                if (dr["t820_parametro2"].ToString() == "")
                    sb.Append("<td class='tdcol'></td>");
                else
                    sb.Append("<td class='tdcol'><input type='text' class='txtNumL' SkinID='Numero' onkeypress='vtn(event);' onfocus='fn(this,8,0)' style='width:55px' value='" + Utilidades.getInt(dr["t824_parametro2"].ToString()) + "' maxlength='8'></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W160' onmouseover='TTip(event)'>" + dr["t820_parametro3"].ToString() + "</nobr></td>");
                if (dr["t820_parametro3"].ToString() == "")
                    sb.Append("<td class='tdcol'></td>");
                else
                    sb.Append("<td class='tdcol'><input type='text' class='txtNumL' SkinID='Numero' onkeypress='vtn(event);' onfocus='fn(this,8,0)' style='width:55px' value='" + Utilidades.getInt(dr["t824_parametro3"].ToString()) + "' maxlength='8'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion
    }
}