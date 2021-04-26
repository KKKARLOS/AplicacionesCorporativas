using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using GASVI.DAL;

namespace GASVI.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GASVI
    /// Class	 : EXCEPCIONAUTO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T429_EXCEPCIONAUTO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	28/07/2011 14:19:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EXCEPCIONAUTO
    {
        #region Metodos

        public static string CatalogoMotivoExcepcionAuto(string sIdFicepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblMotivosExcepcionAuto' class='W450' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col style='width:175px; padding-left:2px;' />");
            sb.Append("    <col style='width:240px; padding-left:2px;' />");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.EXCEPCIONAUTO.ObtenerMotivosExcepcionAuto(int.Parse(sIdFicepi));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t423_idmotivo"].ToString() + "' ");
                sb.Append("idfr='" + dr["t001_idficepi_resp"].ToString() + "' ");
                sb.Append("idf='" + dr["t001_idficepi_prof"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("style='height:20px' ");
                sb.Append("onClick='ms(this);' ");
                sb.Append("onmouseover='TTip(event);' ");
                sb.Append("> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td>" + Utilidades.unescape(dr["t423_denominacion"].ToString()) + "</td>");
                sb.Append("<td onClick='mostrarAprobadores(this.parentNode);' style='cursor:pointer'><nobr class='NBR W240'>" + Utilidades.unescape(dr["nombreAprobador"].ToString()) + "</nobr></td>");
                if (dr["t001_idficepi_resp"] != DBNull.Value)
                    sb.Append("<td><img id='gomaApr" + dr["t423_idmotivo"].ToString() + "' src='../../../Images/Botones/imgBorrar.gif' border='0' onclick='borrarAprobador(this.parentNode.parentNode);' style='filter:progid:DXImageTransform.Microsoft.Alpha(opacity=100); cursor:pointer; vertical-align:middle;' runat='server'></td>");
                else
                    sb.Append("<td><img id='gomaApr" + dr["t423_idmotivo"].ToString() + "' src='../../../Images/Botones/imgBorrar.gif' border='0' style='filter:progid:DXImageTransform.Microsoft.Alpha(opacity=30); cursor:not-allowed; vertical-align:middle;' runat='server'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoIntegrantes()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblIntegrantes' class='MM W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:335px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.EXCEPCIONAUTO.CatalogoIntegrantes();
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi_prof"].ToString() + "' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("leido='0'");
                sb.Append("onmousedown=\"DD(event);\" ");
                sb.Append("onClick='ms(this); visualizarTablas(this);' ");
                sb.Append("style='height:20px' onmouseover='TTip(event);'> ");
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
            SqlDataReader dr = DAL.EXCEPCIONAUTO.CatalogoPersonas(Utilidades.unescape(sApellido1), Utilidades.unescape(sApellido2), Utilidades.unescape(sNombre), sExcluidos, false);
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

        public static void Grabar(string sDatos)
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
                if (sDatos != "")
                {
                    string[] aDatos = Regex.Split(sDatos, "#sFin#");
                    for (int i = 0, nCount=aDatos.Length; i <nCount; i++)
                    {
                        string[] aElem = Regex.Split(aDatos[i], "#sCad#");
                        switch (aElem[0])
                        {
                            case "I":
                                DAL.EXCEPCIONAUTO.InsertExcepcionAuto(tr, 
                                                                    int.Parse(aElem[1]), 
                                                                    short.Parse(aElem[2]),
                                                                    int.Parse(aElem[3]),
                                                                    int.Parse(HttpContext.Current.Session["GVT_IDFICEPI"].ToString()));
                                break;
                            case "D":
                                DAL.EXCEPCIONAUTO.DeleteExcepcionAuto(tr,
                                                                    int.Parse(aElem[1]),
                                                                    (aElem[2] == "") ? null: (short?)short.Parse(aElem[2]));
                                break;
                            case "U":
                                DAL.EXCEPCIONAUTO.UpdateExcepcionAuto(tr, 
                                                                    int.Parse(aElem[1]), 
                                                                    short.Parse(aElem[2]),
                                                                    int.Parse(aElem[3]),
                                                                    int.Parse(HttpContext.Current.Session["GVT_IDFICEPI"].ToString()));
                                break;
                        }
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar las modificaciones.", ex);
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
