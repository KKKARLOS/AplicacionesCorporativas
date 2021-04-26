using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
using SUPER.BLL;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de NodoAlertas
    /// </summary>
    public partial class NodoAlertas
    {
        #region Propiedades y Atributos
        #endregion
        #region Constructor
        public NodoAlertas()
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

                            DAL.NodoAlertas.Update(tr, int.Parse(aDatosAlerta[1].ToString()), nPar1, nPar2, nPar3);
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
        public static string CatalogoByNodo(int t303_idnodo)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblAlertas' class='texto MANO' style='width:930px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:30px'/><col style='width:210px'/><col style='width:165px'/><col style='width:65px'/><col style='width:165px'/><col style='width:65px'/><col style='width:165px'/><col style='width:65px'/></colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = SUPER.DAL.NodoAlertas.CatalogoByNodo(null, t303_idnodo);
            while (dr.Read())
            {
                sb.Append("<tr id=" + dr["t826_idnodoalertas"].ToString());
                sb.Append(" bd='N' onclick='ms(this)' onkeyup='fm(event); aG(4)' style='height:20px;'>");

                sb.Append("<td style='text-align:right;padding-right:3px'>" + dr["t820_idalerta"].ToString() + "</td>");
                sb.Append("<td style='padding-left:3px;' class='tdcol'><nobr class='NBR W210' onmouseover='TTip(event)'>" + dr["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W160' onmouseover='TTip(event)'>" + dr["t820_parametro1"].ToString() + "</nobr></td>");
                if (dr["t820_parametro1"].ToString() == "")
                    sb.Append("<td class='tdcol'></td>");
                else
                    sb.Append("<td class='tdcol'><input type='text' class='txtNumL' SkinID='Numero' onkeypress='vtn(event);' onfocus='fn(this,8,0)' style='width:55px' value='" + Utilidades.getInt(dr["t826_parametro1"].ToString()) + "' maxlength='8'></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W160' onmouseover='TTip(event)'>" + dr["t820_parametro2"].ToString() + "</nobr></td>");
                if (dr["t820_parametro2"].ToString() == "")
                    sb.Append("<td class='tdcol'></td>");
                else
                    sb.Append("<td class='tdcol'><input type='text' class='txtNumL' SkinID='Numero' onkeypress='vtn(event);' onfocus='fn(this,8,0)' style='width:55px' value='" + Utilidades.getInt(dr["t826_parametro2"].ToString()) + "' maxlength='8'></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W160' onmouseover='TTip(event)'>" + dr["t820_parametro3"].ToString() + "</nobr></td>");
                if (dr["t820_parametro3"].ToString() == "")
                    sb.Append("<td class='tdcol'></td>");
                else
                    sb.Append("<td class='tdcol'><input type='text' class='txtNumL' SkinID='Numero' onkeypress='vtn(event);' onfocus='fn(this,8,0)' style='width:55px' value='" + Utilidades.getInt(dr["t826_parametro3"].ToString()) + "' maxlength='8'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        public static void TrasladarAlertaEstructuraParam(byte nOpcion, byte nNivel, int nCodigo)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region Abrir conexión y transacción
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
                SUPER.DAL.NodoAlertas.TrasladarAlertaEstructuraParam(tr, nOpcion, nNivel, nCodigo);
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al trasladar las alertas.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    throw (new Exception(sResul));
                }
            }
        }

        public static void Duplicar(SqlTransaction tr, int t303_idnodoOrigen, int t303_idnodoDestino)
        {
            //Por trigger, el nodo destino ya tiene una alertas. Si que primero las borro y luego las copio del nodo origen
            SUPER.DAL.NodoAlertas.DeleteByNodo(tr, t303_idnodoDestino);
            SUPER.DAL.NodoAlertas.Duplicar(tr, t303_idnodoOrigen, t303_idnodoDestino);
        }
        #endregion
    }
}