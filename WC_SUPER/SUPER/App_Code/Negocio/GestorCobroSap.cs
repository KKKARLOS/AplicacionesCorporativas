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
    /// Descripción breve de GestorSAP
    /// </summary>
    public class GestorSAP
    {
        #region Atributos Y Propiedades
        #endregion

        public GestorSAP()
        {

        }

        #region Metodos

        public static string obtenerProfesionales()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.GestorSAP.ObtenerProfesionales(null);

            sb.Append(@"<table id='tblDatos' style='width:480px;text-align:left' mantenimiento='1' cellpadding='0' cellspacing='0'>
                        <colgroup>
                            <col style='width:50px' />
                            <col style='width:430px' />
                        </colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["gestorcobro_sap"]+ "/" + dr["t314_idusuario"].ToString() + "' bd='' sap='" + dr["gestorcobro_sap"].ToString() + "' onclick='ms(this);' style='height:20px'>");
                sb.Append("<td align='center'>" + dr["gestorcobro_sap"].ToString() + "</td>");
                sb.Append("<td><div class='NBR MA' style='width:425px' ondblclick='getGestor(this)'>" + dr["Gestor"].ToString() + "</div></td>");
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
                        ///aValores[1] = t314_idusuario
                        ///aValores[2] = gestor
                        string[] aId = Regex.Split(aValores[1], "/");
                        SUPER.Capa_Datos.GestorSAP.Update(tr, int.Parse(aId[1]), aValores[2]);
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al actualizar las alertas del profesional.", ex);
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

