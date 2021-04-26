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
    /// Class	 : MOTIVO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T423_MOTIVO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	28/02/2011 9:19:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class MOTIVO
    {
        #region Metodos

        public static List<ElementoLista> ObtenerMotivos(int t314_idusuario, string t431_idestado)
        {
            List<ElementoLista> oLista = new List<ElementoLista>();
            SqlDataReader dr = DAL.MOTIVO.ObtenerMotivosSolicitud(null, t314_idusuario, t431_idestado);
            while (dr.Read()){
                oLista.Add(new ElementoLista(dr["t423_idmotivo"].ToString(), 
                                            dr["t423_denominacion"].ToString(),
                                            dr["t175_idcc"].ToString(),
                                            dr["t175_denominacion"].ToString(),
                                            dr["t303_idnodo"].ToString(),
                                            dr["t303_denominacion"].ToString()
                                            ));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        public static List<ElementoLista> ObtenerMotivosNodoBeneficiario(int t314_idusuario, string t431_idestado, int t303_idnodo)
        {
            List<ElementoLista> oLista = new List<ElementoLista>();
            SqlDataReader dr = DAL.MOTIVO.ObtenerMotivosNodoBeneficiario(null, t314_idusuario, t431_idestado, t303_idnodo);
            while (dr.Read()){
                oLista.Add(new ElementoLista(dr["t423_idmotivo"].ToString(), 
                                            dr["t423_denominacion"].ToString(),
                                            dr["t175_idcc"].ToString(),
                                            dr["t175_denominacion"].ToString(),
                                            dr["t303_idnodo"].ToString(),
                                            dr["t303_denominacion"].ToString()
                                            ));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        
        public static string CatalogoMotivo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblMotivos' class='MANO W680' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:220px; ' />");
            sb.Append("    <col style='width:60px; ' />");
            sb.Append("    <col style='width:75px; ' />");
            sb.Append("    <col style='width:285px;' />");
            sb.Append("    <col style='width:20px; ' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.MOTIVO.ObtenerMotivos(null);

            while (dr.Read()){

                sb.Append("<tr id='" + dr["t423_idmotivo"].ToString() + "' ");
                sb.Append("idcencos='" + dr["t175_idcc"].ToString() + "' ");
                //sb.Append("descencos='" + Utilidades.unescape(dr["t317_denominacion"].ToString()) + "' ");
                sb.Append("bd='' ");
                sb.Append("leido='0' ");
                sb.Append("style='height:20px' ");
                sb.Append("onClick='ms(this); visualizarTablas(this);' ");
                sb.Append("onmouseover='TTip(event);' ");
                sb.Append("> ");
                sb.Append("<td style='padding:0px;padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding:0px; padding-left:2px; '><input id='descrip" + dr["t423_idmotivo"].ToString() + "' class='txtL' type='text' style='width:215px;' onkeyup='fm(event);activarGrabar();' value=\"");
                sb.Append(Utilidades.unescape(dr["t423_denominacion"].ToString()) + "\" />");
                sb.Append("<td style='padding:0px;padding-left:2px;'><input type='checkbox' style='width:15px;' id='chkEstado" + dr["t423_idmotivo"].ToString() + "' onclick='fm(event);activarGrabar();' class='checkTabla'");
                if ((bool)dr["t423_estado"]) sb.Append(" checked='true' ");
                sb.Append("/></td>");
                sb.Append("<td style='padding:0px;padding-right:10px;'><input id='cuenta" + dr["t423_idmotivo"].ToString() + "' type='text' style='width:65px;' class='txtNumL' onfocus='fn(this,9,0)' onkeyup='fm(event);activarGrabar();' value=\"");
                sb.Append(Utilidades.unescape(dr["t423_cuenta"].ToString()) + "\" />");
                if (dr["t423_idmotivo"].ToString() != "1")
                {
                    sb.Append("<td style='padding:0px;padding-left:2px;' id='cencos" + dr["t423_idmotivo"].ToString() + "' onclick='mostrarCenCos(this.parentNode);'");                
                    //sb.Append("<input id='cencos" + dr["t423_idmotivo"].ToString() + "' class='txtL' type='text' style='width:275px;' onclick='mostrarCenCos(this.parentNode.parentNode);' ReadOnly='true' value=\"");
                    if (dr["t175_idcc"].ToString() != "")
                        sb.Append("class=''>" + dr["t175_idcc"].ToString() + " - " + Utilidades.unescape(dr["t175_denominacion"].ToString()));
                    else sb.Append("class='OPC'>");
                    //sb.Append("\" />");
                }
                else sb.Append("<td style='padding:0px;padding-left:2px;'>");
                sb.Append("</td>");
                sb.Append("<td style='padding:0px;padding-left:2px;'>");
                if (dr["t423_idmotivo"].ToString() != "1")
                    sb.Append("<img id='gomaNodo' src='../../../Images/Botones/imgBorrar.gif' border='0' onclick='borrarCenCos(this.parentNode.parentNode);' style='cursor:pointer; vertical-align:middle;' runat='server'>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoAprobadores(string idMotivo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblAprobadores' class='MM W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:335px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.MOTIVO.CatalogoAprobadores(short.Parse(idMotivo));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("idmo='" + idMotivo + "' ");
                sb.Append("onmousedown=\"DD(event);\" ");
                sb.Append("onClick='mm(event);' ");
                sb.Append("style='height:20px' onmouseover='TTip(event)'> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W320'>" + Utilidades.unescape(dr["nombre"].ToString()) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string obtenerPersonas(string sApellido1, string sApellido2, string sNombre, string sExcluidos)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = DAL.MOTIVO.CatalogoPersonas(Utilidades.unescape(sApellido1), Utilidades.unescape(sApellido2), Utilidades.unescape(sNombre), sExcluidos, false);
            sb.Append("<table id='tblPersonas' class='MAM W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:350px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("onClick='mm(event)' ");
                sb.Append("ondblclick='anadirConvocados();' ");
                sb.Append("onmouseover='TTip(event);' ");
                sb.Append("onmousedown=\"DD(event);\"' style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W340'>" + Utilidades.unescape(dr["nombre"].ToString()) + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static void Grabar(string sMotivos, string sAprobadores)
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
                if (sMotivos != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aDatosMo = Regex.Split(sMotivos, "#sFin#");
                    for (int i = 0; i <= aDatosMo.Length - 1; i++)
                    {
                        string[] aElem = Regex.Split(aDatosMo[i], "#sCad#");
                        DAL.MOTIVO.UpdateMotivo(tr,
                                                short.Parse(aElem[0]),
                                                Utilidades.unescape(aElem[1].ToString()),
                                                short.Parse(aElem[2]),
                                                int.Parse(aElem[3].Replace(".","")),
                                                (aElem[4].ToString() == "") ? null : aElem[4].ToString());
                    }
                }
                if (sAprobadores != "")
                {
                    string[] aDatosAp = Regex.Split(sAprobadores, "#sFin#");
                    for (int i = 0; i <= aDatosAp.Length - 1; i++)
                    {
                        string[] aElem2 = Regex.Split(aDatosAp[i], "#sCad#");
                        switch (aElem2[0])
                        {
                            case "I":
                                DAL.MOTIVO.InsertAprobadores(tr, short.Parse(aElem2[1]), int.Parse(aElem2[2]));
                                break;
                            case "D":
                                DAL.MOTIVO.DeleteAprobadores(tr, short.Parse(aElem2[1]), int.Parse(aElem2[2]));
                                break;
                        }
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar los motivos y sus aprobadores.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }

        public static List<ElementoLista> ObtenerMotivosActivos()
        {
            List<ElementoLista> oLista = new List<ElementoLista>();
            SqlDataReader dr = DAL.MOTIVO.Catalogo(null, "", true, null, 2, 0);
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["t423_idmotivo"].ToString(), dr["t423_denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        #endregion
    }
}
