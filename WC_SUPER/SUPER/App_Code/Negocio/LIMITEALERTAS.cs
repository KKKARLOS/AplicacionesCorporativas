using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de FECHALIMITE
    /// </summary>
    public class LIMITEALERTAS
    {
        #region Atributos Y Propiedades
        #endregion

        public LIMITEALERTAS()
        {

        }

        #region Metodos

        public static string Obtener()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.LIMITEALERTAS.Obtener(null);

            sb.Append(@"<table id='tblDatos' style='width:265px;text-align:left' mantenimiento='1' cellpadding='0' cellspacing='0'>
                        <colgroup>
                            <col style='width:190px' />
                            <col style='width:75px' />
                        </colgroup>");
            string sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t828_anomes"] + "' bd='' " +  " onclick='ms(this);'>");
                sb.Append("<td align='center'>" + Fechas.AnnomesAFechaDescLarga(int.Parse(dr["t828_anomes"].ToString())) + "</td>");
                if (dr["t828_limitealertas"] != DBNull.Value) sFecha = DateTime.Parse(dr["t828_limitealertas"].ToString()).ToShortDateString();
                else sFecha = "";

                sb.Append("<td style='text-align:center' class='MA'>");

                if (HttpContext.Current.Session["BTN_FECHA"].ToString() == "I")
                    sb.Append("<input id='txtFF-" + dr["t828_anomes"] + "' type='text' class='txtFecL' style='width:63px;' value='" + sFecha + "' oValueOriginal='" + sFecha + "' Calendar='oCal' ondblclick='mc(event);' onchange='aG(this);' readonly />");
                else
                    sb.Append("<input id='txtFF-" + dr["t828_anomes"] + "' type='text' class='txtFecL' style='width:63px;' value='" + sFecha + "' oValueOriginal='" + sFecha + "' Calendar='oCal' onchange='aG(this);' onfocus='focoFecha(event);' onmousedown='mc1(event)'/>");

                sb.Append("</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;

        }

        public static string Grabar(string strDatos)
        {
            string sElementosInsertados = "";
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
                throw (new Exception("Error al abrir la conexión. " + ex.Message));
            }
            #endregion

            try
            {
                if (strDatos != "") //No se ha modificado nada 
                {
                    string[] aDatos = Regex.Split(strDatos, "///");
                    foreach (string oDatos in aDatos)
                    {
                        if (oDatos == "") continue;
                        string[] aValores = Regex.Split(oDatos, "##");

                        ///aValores[0] = bd
                        ///aValores[1] = t828_anomes
                        ///aValores[2] = t828_limitealertas

                        SUPER.Capa_Datos.LIMITEALERTAS.Update(tr, int.Parse(aValores[1]), (aValores[2] == "") ? null : (DateTime?)DateTime.Parse(aValores[2]));                        
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al actualizar la fecha límite de alertas para dialogos.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            sResul = sElementosInsertados;
            return "OK@#@" + sResul;
        }

        #endregion
    }
}

