using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de Ficepi
    /// </summary>
    public class Ficepi
    {
        #region Atributos Y Propiedades
        #endregion

        public Ficepi()
        {
            
        }

        #region Metodos
        public static string obtenerProfesionales(string sAp1, string sAp2, string sNombre, string excluidos)
        {
            string sResul = "";
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 396px;'>");

            SqlDataReader dr = Capa_Datos.Ficepi.CargarRecursos(null, sAp1, sAp2, sNombre, 2, 0, excluidos);

            while (dr.Read())
            {

                strBuilder.Append("<tr style=height:18px; codredresp='" + dr["T001_CODREDRESP"].ToString() + "' centrab='" + dr["T009_DESCENTRAB"].ToString() + "' id='" + dr["T001_IDFICEPI"].ToString() + "' codred='" + dr["T001_CODRED"].ToString() + "' onclick=\"ms(this)\" ondblclick=\"aceptarClick(this.rowIndex)\">");

                if (dr["DESCRIPCION"].ToString().Length > 60) strBuilder.Append("<td  title='" + dr["DESCRIPCION"].ToString() + "'>");
                else strBuilder.Append("<td style='padding-left:5px'>");
                strBuilder.Append("<nobr  class='NBR 390' onmouseover='TTip(event)'>" + dr["DESCRIPCION"].ToString() + "</nobr></td>");
                strBuilder.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            strBuilder.Append("</table>");
            sResul = strBuilder.ToString();

            return sResul;
        }

        public static string obtenerProfesionalesConsultas(string sAp1, string sAp2, string sNombre, string excluidos)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<table id='tblDatos' class='texto' style='WIDTH: 700px;'>");
            strBuilder.Append("<colgroup>");
            strBuilder.Append(" <col style='width:20px;' />");
            strBuilder.Append(" <col style='width:680px;' />");;
            strBuilder.Append("</colgroup>");
            SqlDataReader dr = Capa_Datos.Ficepi.CargarRecursos(null, sAp1, sAp2, sNombre, 2, 0, excluidos);

            while (dr.Read())
            {

                strBuilder.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "' style='height:20px;cursor:pointer;' >");
                strBuilder.Append("<td><input type='checkbox'/></td>");
                strBuilder.Append("<td style='padding-left:3px;'><nobr  class='NBR 680' onmouseover='TTip(event)' onclick=\"verHTML(this.parentNode.parentNode.id)\">" + dr["DESCRIPCION"].ToString() + "</nobr></td>");
                strBuilder.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            strBuilder.Append("</table>");
            return strBuilder.ToString();
        }

        public static string ObtenerProfesionalesMantCV(string t001_apellido1, string t001_apellido2, string t001_nombre, bool bSoloActivos)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='MANO' style='WIDTH: 450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:430px;' /></colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = SUPER.Capa_Datos.Ficepi.ObtenerCatalogo(null, Utilidades.unescape(t001_apellido1), Utilidades.unescape(t001_apellido2), Utilidades.unescape(t001_nombre), bSoloActivos);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("onclick='ponerId(this.id)'>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("<td onmouseover='TTip(event);'><nobr class='NBR W420'>" + dr["PROFESIONAL"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }

        public static string obtenerProfesionales(string sAp1, string sAp2, string sNombre, bool bSoloActivos)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.Ficepi.ObtenerProfesionalesAvisos(null, Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), bSoloActivos);

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                sb.Append(" style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W420' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='addItem(this)'>" + dr["profesional"].ToString() + "</nobr></td>");

                sb.Append("</tr>" + (char)10);
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        public static string obtenerProfesionalesExcepciones()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.Ficepi.ObtenerProfesionalesAvisosExcepciones(null);

            sb.Append("<table id='tblDatos2' style='width: 450px;' class='texto MM' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:11px;' /><col style='width:19px' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' bd='' style='height:20px;' onmousedown='DD(event);' onclick='mm(event)'>");
                sb.Append("<td style='padding-left:2px;'><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgFN.gif'></td>");
                sb.Append("<td style='text-align:center;'>");

                if (dr["t001_sexo"].ToString() == "V")
                {
                    //sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuIV.gif'>");
                    switch (dr["tipo"].ToString())
                    {
                        case "P":
                            sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuPV.gif'>");
                            break;
                        case "E":
                            sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuEV.gif'>");
                            break;
                        case "F":
                            sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuFV.gif'>");
                            break;
                    }
                }
                else
                {
                    //sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuIM.gif'>");
                    switch (dr["tipo"].ToString())
                    {
                        case "P":
                            sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuPM.gif'>");
                            break;
                        case "E":
                            sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuEM.gif'>");
                            break;
                        case "F":
                            sb.Append("<img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgUsuFM.gif'>");
                            break;
                    }
                }

                sb.Append("</td><td><div class='NBR' style='width:415px'>" + dr["Profesional"].ToString() + "</div></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;

        }
        public static string ObtenerProfesionalesFigurasCVT(string sAp1, string sAp2, string sNombre, bool bCoste, bool bExterno, bool bBaja)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<table id='tblDatos' class='MAM' style='width:470px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                            <col style='width:20px' />
                            <col style='width:390px' />
                            <col style='width:20px' />
                            <col style='width:20px' />
                            <col style='width:20px' />
                        </colgroup>");

            SqlDataReader dr = Capa_Datos.Ficepi.ObtenerProfesionalesParaFigurasCVT(null, Utilidades.unescape(sAp1), 
                                                                Utilidades.unescape(sAp2),
                                                                Utilidades.unescape(sNombre), true, bCoste, bExterno, bBaja);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' bd='' ");
                sb.Append("tipo ='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo ='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja ='" + dr["baja"].ToString() + "' ");
                sb.Append("coste ='" + dr["VerCoste"].ToString() + "' ");
                sb.Append("externo ='" + dr["VerExternos"].ToString() + "' ");
                sb.Append("verbaja ='" + dr["VerBajas"].ToString() + "'> ");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W380'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td></td><td></td><td></td>");
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
                        ///aValores[1] = t001_idficepi
   
						switch (aValores[0])
						{
							case "I":
								SUPER.Capa_Datos.Ficepi.Update(tr, int.Parse(aValores[1]), true);
								if (sElementosInsertados == "") sElementosInsertados = aValores[1];
								else sElementosInsertados += "//" + aValores[1];
								break;
							case "D":
								SUPER.Capa_Datos.Ficepi.Update(tr, int.Parse(aValores[1]), false);
								break;
						}						
						
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
                if (sResul!="")
                    throw (new Exception(sResul));
            }
            sResul = sElementosInsertados;
            return "OK@#@" + sResul;
        }

        public static void GrabarNoCV(string sDatosCV)
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
                string[] aDatosCV = Regex.Split(sDatosCV, "{sepreg}");
                foreach (string sDatos in aDatosCV)
                {
                    if (sDatos == "") continue;
                    string[] aValores = Regex.Split(sDatos, "{sep}");
                    ///aValores[0] = idFicepi
                    ///aValores[1] = nocv 
                    ///aValores[2] = comentario
                    SUPER.Capa_Datos.Ficepi.UpdateNoCV(tr, int.Parse(aValores[0]),
                        (aValores[1]=="1")? true:false,
                        Utilidades.unescape(aValores[2])
                        );
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar los datos de No CV.", ex);
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

        public static void UpdateCal(int idficepi, int idcal)
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
                SUPER.Capa_Datos.Ficepi.UpdateCal(tr, idficepi, idcal);
               

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al modificar el id calendario.", ex);
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

        public static string GetResponsableProgress(int t001_idficepi)
        {
            string sRes = "";
            SqlDataReader dr = SUPER.Capa_Datos.Ficepi.GetResponsableProgress(t001_idficepi);
            if (dr.Read())
            {
                //sRes = dr["EVALUADOR"].ToString() + "@#@" + dr["NOMBRECOMEVALUADOR"].ToString();
                sRes = dr["EVALUADOR"].ToString() + "@#@" + dr["NOMBRECOMEVALUADOR"].ToString();
            }
            dr.Close();
            dr.Dispose();
            return sRes;
        }
        /// <summary>
        /// Carga del catálogo de búsqueda de profesionales en la pestaña Básica de la consulta de CVT
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <param name="sAp1"></param>
        /// <param name="sAp2"></param>
        /// <param name="sNombre"></param>
        /// <param name="sMostrarBajas"></param>
        /// <returns></returns>
        public static string getProfConsBasicaCVT(int t001_idficepi, string sAp1, string sAp2, string sNombre, string sMostrarBajas)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 396px;'>");

            //SqlDataReader dr = Capa_Datos.Ficepi.CargarRecursos(null, sAp1, sAp2, sNombre, 2, 0, excluidos);
            SqlDataReader dr = USUARIO.ObtenerProfesionalesCVT(t001_idficepi,
                                                                Utilidades.unescape(sAp1), Utilidades.unescape(sAp2),
                                                                Utilidades.unescape(sNombre),
                                                                (sMostrarBajas == "1") ? true : false, true, null);

            while (dr.Read())
            {

                sb.Append("<tr style=height:18px; id='" + dr["T001_IDFICEPI"].ToString() + "' ");
                if (dr["vision"].ToString() == "1")
                    sb.Append("onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'");
                else
                    sb.Append("onclick='msgNoVision()'");
                sb.Append(" >");
                if (dr["profesional"].ToString().Length > 60)
                    sb.Append("<td  title='" + dr["profesional"].ToString() + "'>");
                else sb.Append("<td style='padding-left:5px'>");
                if (dr["vision"].ToString() == "1")
                    sb.Append("<nobr class='NBR 390' onmouseover='TTip(event)'>" + dr["profesional"].ToString() + "</nobr></td>");
                else
                    sb.Append("<nobr class='NBR 390' style='color:gray;' onmouseover='TTip(event)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            sResul = sb.ToString();

            return sResul;
        }

        #endregion
	}
}
