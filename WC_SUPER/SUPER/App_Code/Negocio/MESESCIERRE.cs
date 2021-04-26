using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    public class MESESCIERRE
    {
        #region Atributos Y Propiedades
        #endregion

        public MESESCIERRE()
        {

        }

        #region Metodos

        public static string Obtener(int anno)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.DAL.MESESCIERRE.Obtener(null, anno);

            sb.Append(@"<table id='tblDatos' style='width:455px;text-align:center;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                            <col style='width:15px' />
                            <col style='width:100px' />
                            <col style='width:160px' />
                            <col style='width:90px;' />
                            <col style='width:90px;' />
                        </colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["anomes"] + "' bd='' ");
                sb.Append("flimiteof='" + ((dr["t637_fecha"] != DBNull.Value) ? ((DateTime)dr["t637_fecha"]).ToShortDateString() : "") + "' ");
                sb.Append("hlimiteof='" + ((dr["t637_fecha"] != DBNull.Value) ? ((DateTime)dr["t637_fecha"]).ToShortTimeString() : "") + "' ");
                sb.Append("flimiterespalertas='" + ((dr["t828_limitealertas"] != DBNull.Value) ? ((DateTime)dr["t828_limitealertas"]).ToShortDateString() : "") + "' ");
                sb.Append("fprevcierreeco='" + ((dr["t855_prevcierreeco"] != DBNull.Value) ? ((DateTime)dr["t855_prevcierreeco"]).ToShortDateString() : "") + "' ");
                sb.Append(" onclick='ms(this);'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style=\"text-align:left;\">" + Fechas.AnnomesAMesDescLarga(int.Parse(dr["anomes"].ToString())) + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString(); ;

        }

        public static string Grabar_old(string strDatos)
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

