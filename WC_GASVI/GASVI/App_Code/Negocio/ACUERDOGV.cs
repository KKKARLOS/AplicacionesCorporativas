using System;
using System.Data;
using System.Data.SqlClient;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
	public partial class ACUERDOGV
    {
        #region Propiedades
        
        #endregion

        public ACUERDOGV()
		{
			
		}

        public static string mostrarAcuerdos(Nullable<DateTime> dFecha)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblAcuerdos' class='MA W398'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px; padding-left:2px;' />");
            sb.Append("     <col style='width:60px; text-align:right'/>");
            sb.Append("     <col style='width:193px; padding-left:10px;'/>");
            sb.Append("     <col style='width:65px; padding-left:2px;'/>");
            sb.Append("     <col style='width:65px; padding-left:2px;'/>");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.ACUERDOGV.ObtenerAcuerdos(dFecha);
            while (dr.Read()){
                sb.Append("<tr id='" + dr["t666_idacuerdogv"].ToString() + "' ");
                sb.Append("idres='" + dr["t001_idficepi_responsable"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("nomres=\"" + dr["nombreResp"].ToString() + "\" ");
                sb.Append("idmod='" + dr["t001_idficepi_mod"].ToString() + "' ");
                sb.Append("fm='" + ((DateTime)dr["t666_fechamod"]).ToShortDateString() + "' ");
                sb.Append("idMoneda='" + dr["t422_idmoneda"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append("descrip='" + Utilidades.escape(dr["t666_descripcion"].ToString()) + "' ");
                if (dr["t666_descripcion"].ToString() != "" || dr["nombreResp"].ToString() != "")
                {
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                    sb.Append("body=[<label style='width:80px;'>Denominaci&oacute;n:</label>" + dr["t666_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                    sb.Append("<label style='width:80px;'>Responsable:</label>" + dr["nombreResp"].ToString().Replace((char)34, (char)39) + "<br>");
                    sb.Append("<label style='width:80px;vertical-align:top;'>Descripci&oacute;n:</label><label style='width:300px;'>" + dr["t666_descripcion"].ToString().Replace((char)34, (char)39) + "</label>] ");
                    sb.Append("hideselects=[off]\" ");
                }
                sb.Append("onClick='ms(this); visualizarTablas(this); iFilaAcuerdo=" + int.Parse(dr["t666_idacuerdogv"].ToString()) + "' ");
                sb.Append("ondblclick='modificarAcuerdo(this);'");                
                sb.Append(">");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='text-align: right'>" + int.Parse(dr["t666_idacuerdogv"].ToString()).ToString("#,###") + "</td>");

                sb.Append("<td style='padding-left:10px'><nobr class='NBR W180'>" + dr["t666_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((DateTime)dr["t666_fechainicio"]).ToShortDateString() + "</td>");
                sb.Append("<td>" + ((DateTime)dr["t666_fechafin"]).ToShortDateString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();        
        }

        public static string mostrarDatos(string sIdAc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(mostrarProfesionales(sIdAc) + "@#@" + mostrarProyectos(sIdAc) + "@#@" + mostrarCR(sIdAc));
            return sb.ToString();
        }

        public static string mostrarProfesionales(string sIdAc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblProf' class='MANO W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px;' />");
            sb.Append("     <col style='width:20px;' />");
            sb.Append("     <col style='width:363px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.ACUERDOGV.ObtenerProfesionales(int.Parse(sIdAc));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' ");
                sb.Append("idac=" + dr["t666_idacuerdogv"].ToString() + " ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("onClick='ms(this);' ");
                sb.Append(">");
                sb.Append("<td style='padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:2px;'></td>");
                sb.Append("<td style='padding-left:5px;'>" + dr["nombre"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string mostrarProyectos(string sIdAc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblProy' class='MANO W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px; ' />");
            sb.Append("     <col style='width:23px;' />");
            sb.Append("     <col style='width:360px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.ACUERDOGV.ObtenerProyectos(int.Parse(sIdAc));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' style='height:20px;' ");
                sb.Append("idac='" + dr["t666_idacuerdogv"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("resp='" + dr["responsable"].ToString() + "' ");
                sb.Append("cr='" + dr["t303_denominacion"].ToString() + "' ");
                sb.Append("onClick='ms(this)' ");

                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                sb.Append("body=[<label style='width:80px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:80px;vertical-align:top;'>C.R.:</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] ");
                sb.Append("hideselects=[off]\" ");

                sb.Append(">");
                sb.Append("<td style='padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:2px;'></td>");
                sb.Append("<td style='padding-left:5px;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string mostrarCR(string sIdAc)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCR' class='MANO W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px; ' />");
            sb.Append("     <col style='width:383px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.ACUERDOGV.ObtenerNodos(int.Parse(sIdAc));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' style='height:20px;' ");
                sb.Append("idac='" + dr["t666_idacuerdogv"].ToString() + "' ");
                sb.Append("onClick='ms(this)'");
                sb.Append(">");
                sb.Append("<td style='padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t303_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string obtenerProfesionales(string sAp1, string sAp2, string sNombre, string sExcluidos, string sBajas)
        {// Devuelve el código HTML para la lista de Profesionales
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatProf' class='W360' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:340px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");

            SqlDataReader dr = DAL.ACUERDOGV.CatalogoProfesionales(Utilidades.unescape(sAp1), 
                                                                    Utilidades.unescape(sAp2), 
                                                                    Utilidades.unescape(sNombre), 
                                                                    sExcluidos,
                                                                    (sBajas == "1") ? true : false);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                if (dr["t001_tiporecurso"].ToString() == "I")
                {
                    sb.Append("onDblClick='aceptarClick(this)' ");
                    sb.Append("class='MA' ");
                }
                sb.Append("style='height:20px' ");
                sb.Append("onmouseover='TTip(event)'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W320'>" + dr["nombre"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string obtenerNodos()
        {
            StringBuilder sb = new StringBuilder();
            string idNodo = "";
            sb.Append("<table id='tblCatNodo' class='MA W350' cursor='pointer' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:350px; ' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.ACUERDOGV.CatalogoNodos();
            while (dr.Read())
            {
                idNodo = dr["identificativo"].ToString();
                sb.Append("<tr id='" + idNodo);
                sb.Append("'ondblclick='aceptarClick(this)' style='height:16px;'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["denominacion"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return sb.ToString();
        }

        public static string obtenerProyectos(string sNodo, string sEstado, string sCategoria, string sIdCliente, string sIdResponsable,
                                string sNumPE, string sDesPE, string sTipoBusqueda)
        {
            StringBuilder sb = new StringBuilder();
            int? idNodo = null;
            int? idCliente = null;
            int? idResponsable = null;
            int? numPE = null;

            if (sNodo != "") idNodo = int.Parse(sNodo);
            if (sIdCliente != "") idCliente = int.Parse(sIdCliente);
            if (sIdResponsable != "") idResponsable = int.Parse(sIdResponsable);
            if (sNumPE != "" && sNumPE != "0") numPE = int.Parse(sNumPE);

            sb.Append("<table id='tblCatProy' class='MA' style='width:940px;' cellpadding ='0' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:20px;' />");
            sb.Append("     <col style='width:20px;' />");
            sb.Append("     <col style='width:80px;' />");
            sb.Append("     <col style='width:355px;' />");
            sb.Append("     <col style='width:220px;' />");
            sb.Append("     <col style='width:260px;' />");
            sb.Append("</colgroup>");
            SqlDataReader dr = DAL.ACUERDOGV.CatalogoProyectos(idNodo, sEstado, sCategoria, idCliente, idResponsable, numPE,
                        Utilidades.unescape(sDesPE), sTipoBusqueda);

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px' id='" + dr["t301_idproyecto"].ToString() + "' ");

                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("resp='" + dr["Responsable"].ToString() + "' ");
                sb.Append("cr='" + dr["t303_denominacion"].ToString() + "' ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:3px'>");
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                sb.Append("<td><nobr class='NBR W340' title=\"cssbody=[dvbdy] cssheader=[dvhdr] ");
                sb.Append("header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información]");
                sb.Append("body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] ");
                sb.Append("hideselects=[off]\">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W210'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W250'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string obtenerResponsables(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblCatRes' style='width:500px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:480px; padding-left:3px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.ACUERDOGV.CatalogoResponsablesProyecto(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), (sMostrarBajas == "1") ? true : false);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["idusuario"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' style='height:20px;'");

                if ((int)dr["es_responsable"] == 0)
                {
                    sb.Append("><td><img src='../../images/imgResponsable.gif' style='filter:progid:DXImageTransform.Microsoft.Alpha(opacity=30)' width='16px' height='16px' /></td>");
                    sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  ");
                    sb.Append("Información] body=[<label style='width:70px;'>Profesional:</label>");
                    sb.Append(dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>");
                    sb.Append(int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>");
                    sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>");
                    sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                    sb.Append("<label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39));
                    sb.Append("] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                }
                else
                {
                    sb.Append(" class='MA' ondblclick=\"aceptarClick(this)\"><td><img src='../../images/imgResponsable.gif' width='16px' height='16px' /></td>");
                    sb.Append("<td style='noWrap:true;' ondblclick=\"aceptarClick(this)\" ");
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  ");
                    sb.Append("Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39));
                    sb.Append("<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###"));
                    sb.Append("<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>");
                    sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>");
                    sb.Append(dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString());
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return sb.ToString();
        }

        public static string obtenerClientes(string sTipoBusqueda, string strCli)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:550px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='padding-left:5px;'>");
            sb.Append("</colgroup>");
            SqlDataReader dr = DAL.ACUERDOGV.CatalogoClienteByNombre(Utilidades.unescape(strCli), sTipoBusqueda);

            int i = 0;
            bool bExcede = false;
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' ");
                sb.Append("ondblclick='aceptarClick(this)' style='height:16px;'>");
                sb.Append("<td><img src='../../images/img" + dr["tipo"].ToString() + ".gif' ");
                if (dr["tipo"].ToString() == "M") sb.Append("style='margin-right:5px;'");
                else sb.Append("style='margin-left:15px; margin-right:5px;'");
                sb.Append("><nobr class='NBR W475'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
                i++;
                if (i > Constantes.nNumMaxTablaCatalogo)
                {
                    bExcede = true;
                    break;
                }
            }
            dr.Close();
            dr.Dispose();
            if (!bExcede)
            {
                sb.Append("</table>");
            }
            else
            {
                sb.Length = 0;
                sb.Append("EXCEDE");
            }
            return sb.ToString();
            
        }
        
        public static string Grabar(string sAc, string sProf, string sProy, string sCR)
        {
            string sResul = "", sElementosInsertados = "", sElementosInsertados2 = "", sElementosInsertados3 = "", sElementosInsertados4 = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            int nAux = 0, nDel = 0, nIdAc = -1;
            string sNewAc = "";
            bool bErrorControlado = false;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                #region Acuerdos
                if (sAc != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aAcGrabar = Regex.Split(sAc, "#sFin#");
                    for (int i = 0; i <= aAcGrabar.Length - 1; i++)
                    {
                        string[] aElemAc = Regex.Split(aAcGrabar[i], "#sCad#");
                        switch (aElemAc[0])
                        {
                            case "I":
                                nAux = DAL.ACUERDOGV.InsertAcuerdo(tr,
                                                                   Utilidades.unescape(aElemAc[2].ToString()),
                                                                   int.Parse(aElemAc[3]),
                                                                   Utilidades.unescape(aElemAc[4].ToString()),
                                                                   DateTime.Parse(aElemAc[5]),
                                                                   DateTime.Parse(aElemAc[6]),
                                                                   int.Parse(aElemAc[7]),
                                                                   DateTime.Parse(aElemAc[8]),
                                                                   aElemAc[9].ToString());
                                if (sElementosInsertados == "")
                                {
                                    sElementosInsertados = nAux.ToString();
                                    sNewAc = aElemAc[1] + "#sCad#" + nAux.ToString();
                                }
                                else
                                {
                                    sElementosInsertados += "#sCad#" + nAux.ToString();
                                    sNewAc += "#sFin#" + aElemAc[1] + "#sCad#" + nAux.ToString();
                                }
                                break;
                            case "D":
                                nDel = DAL.ACUERDOGV.DeleteAcuerdo(tr, int.Parse(aElemAc[1]));
                                if (nDel == 0)
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Operacion rechazada! Existen usuarios asignados a ese acuerdo."));
                                }
                                break;
                            case "U":
                                DAL.ACUERDOGV.UpdateAcuerdo(tr,
                                                            int.Parse(aElemAc[1]),
                                                            Utilidades.unescape(aElemAc[2].ToString()),
                                                            int.Parse(aElemAc[3]),
                                                            Utilidades.unescape(aElemAc[4].ToString()),
                                                            DateTime.Parse(aElemAc[5]),
                                                            DateTime.Parse(aElemAc[6]),
                                                            int.Parse(aElemAc[7]),
                                                            DateTime.Parse(aElemAc[8]),
                                                            aElemAc[9].ToString());
                                break;
                        }
                    }
                }
                sElementosInsertados = sNewAc + "@#@";
                #endregion

                #region Profesionales
                if (sProf != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aProfGrabar = Regex.Split(sProf, "#sFin#");
                    for (int i = 0, iCountLoop = aProfGrabar.Length - 1; i <= iCountLoop; i++)
                    {
                        string[] aElemProf = Regex.Split(aProfGrabar[i], "#sCad#");
                        switch (aElemProf[0])
                        {
                            case "I":
                                if (int.Parse(aElemProf[1]) >= 30000)// 30000 este es el valor mínimo que se le ha indicado a todas las filas nuevas
                                {
                                    string[] aNewAc = Regex.Split(sNewAc, "#sFin#");
                                    for (int j = 0, nCountLoop = aNewAc.Length - 1; j <= nCountLoop; j++)
                                    {
                                        string[] sElemNewAc = Regex.Split(aNewAc[j], "#sCad#");
                                        if (sElemNewAc[0] == aElemProf[1])
                                        {
                                            nIdAc = int.Parse(sElemNewAc[1]);
                                            break;
                                        }
                                    }
                                }
                                nAux = DAL.ACUERDOGV.InsertProfesional(tr,
                                                                      (nIdAc != -1) ? nIdAc : int.Parse(aElemProf[1]),
                                                                      int.Parse(aElemProf[2]));
                                nIdAc = -1;
                                if (sElementosInsertados2 == "") sElementosInsertados2 = aElemProf[2];
                                else sElementosInsertados2 += "#sCad#" + aElemProf[2];
                                
                                break;
                            case "D":
                                DAL.ACUERDOGV.DeleteProfesional(tr,
                                                                int.Parse(aElemProf[1]),
                                                                int.Parse(aElemProf[2]));
                                break;
                        }
                    }
                }
                sElementosInsertados += sElementosInsertados2;
                #endregion

                #region Proyectos
                if (sProy != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aProyGrabar = Regex.Split(sProy, "#sFin#");
                    for (int i = 0; i <= aProyGrabar.Length - 1; i++)
                    {
                        string[] aElemProy = Regex.Split(aProyGrabar[i], "#sCad#");
                        switch (aElemProy[0])
                        {
                            case "I":
                                if (int.Parse(aElemProy[1]) >= 30000)// 30000 este es el valor mínimo que se le ha indicado a todas las filas nuevas
                                {
                                    string[] aNewAc = Regex.Split(sNewAc, "#sFin#");
                                    for (int j = 0, nCountLoop = aNewAc.Length - 1; j <= nCountLoop; j++)
                                    {
                                        string[] sElemNewAc = Regex.Split(aNewAc[j], "#sCad#");
                                        if (sElemNewAc[0] == aElemProy[1])
                                        {
                                            nIdAc = int.Parse(sElemNewAc[1]);
                                            break;
                                        }
                                    }
                                }
                                nAux = DAL.ACUERDOGV.InsertProyecto(tr,
                                                                   (nIdAc != -1) ? nIdAc : int.Parse(aElemProy[1]),
                                                                   int.Parse(aElemProy[2]));
                                nIdAc = -1;
                                if (sElementosInsertados3 == "") sElementosInsertados3 = aElemProy[2];
                                else sElementosInsertados3 += "#sCad#" + aElemProy[2];
                                break;
                            case "D":
                                DAL.ACUERDOGV.DeleteProyecto(tr,
                                                            int.Parse(aElemProy[1]),
                                                            int.Parse(aElemProy[2]));
                                break;
                        }
                    }
                }
                sElementosInsertados += "@#@" + sElementosInsertados3;
                #endregion

                #region CR
                if (sCR != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aCRGrabar = Regex.Split(sCR, "#sFin#");
                    for (int i = 0; i <= aCRGrabar.Length - 1; i++)
                    {
                        string[] aElemCR = Regex.Split(aCRGrabar[i], "#sCad#");
                        switch (aElemCR[0])
                        {
                            case "I":
                                if (int.Parse(aElemCR[1]) >= 30000)// 30000 este es el valor mínimo que se le ha indicado a todas las filas nuevas
                                {
                                    string[] aNewAc = Regex.Split(sNewAc, "#sFin#");
                                    for (int j = 0, nCountLoop = aNewAc.Length - 1; j <= nCountLoop; j++)
                                    {
                                        string[] sElemNewAc = Regex.Split(aNewAc[j], "#sCad#");
                                        if (sElemNewAc[0] == aElemCR[1])
                                        {
                                            nIdAc = int.Parse(sElemNewAc[1]);
                                            break;
                                        }
                                    }
                                }
                                nAux = DAL.ACUERDOGV.InsertCR(tr,
                                                              (nIdAc != -1) ? nIdAc : int.Parse(aElemCR[1]),
                                                              int.Parse(aElemCR[2]));
                                nIdAc = -1;

                                if (sElementosInsertados4 == "") sElementosInsertados4 = aElemCR[2];
                                else sElementosInsertados4 += "#sCad#" + aElemCR[2];
                                break;
                            case "D":
                                DAL.ACUERDOGV.DeleteCR(tr,
                                                    int.Parse(aElemCR[1]),
                                                    int.Parse(aElemCR[2]));
                                break;
                        }
                    }
                }
                sElementosInsertados += "@#@" + sElementosInsertados4;
                #endregion

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al grabar el acuerdo.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return sElementosInsertados;
        }


        //////////////////////////Vista nuevo pago concertado///////////////////////////////////

        public static string obtenerAcuerdosParaAsignacion(string sIdUsuario, string sPSN)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatAcuerdos' class='MA W400' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:40px; text-align:right; padding-right:5px;' />");
            sb.Append("     <col style='width:360px; padding-left:2px;'/>");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.ACUERDOGV.obtenerAcuerdosParaAsignacion(int.Parse(sIdUsuario), (sPSN=="")? null: (int?)int.Parse(sPSN));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t666_idacuerdogv"].ToString() + "' ");
                sb.Append("idMoneda='" + dr["t422_idmoneda"].ToString() +"' ");
                sb.Append("style='height:20px;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                sb.Append("body=[<label style='width:90px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:90px;'>Inicio vigencia:</label>" + ((DateTime)dr["t666_fechainicio"]).ToShortDateString() + "<br>");
                sb.Append("<label style='width:90px;'>Fin vigencia:</label>" + ((DateTime)dr["t666_fechafin"]).ToShortDateString() + "<br>");
                sb.Append("<label style='width:90px;'>Moneda:</label>" + dr["t422_denominacion"].ToString() + "<br>");
                sb.Append("<label style='width:90px;'>Descripci&oacute;n:</label>" + dr["t666_descripcion"].ToString().Replace((char)34, (char)39) + "] ");
                sb.Append("hideselects=[off]\" ");
                sb.Append("ondblclick='aceptarClick(this)' ");
                sb.Append(">");
                sb.Append("<td>" + ((int)dr["t666_idacuerdogv"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W350'>" + dr["t666_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string obtenerOtrosPagos(int nUsuario, int t420_idreferencia)
        {
            SqlDataReader dr = DAL.CABECERAGV.CatalogoOtrosPagos(nUsuario, t420_idreferencia);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblOtrosPagos' style='width:280px;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:90px;'/>");
            sb.Append("     <col style='width:70px;'/>");
            sb.Append("     <col style='width:58px;'/>");
            sb.Append("     <col style='width:62px;'/>");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t420_idreferencia"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                if (dr["t431_denominacion"].ToString() != "" || dr["t420_concepto"].ToString() != "" || dr["t666_denominacion"].ToString() != "")
                {
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                    sb.Append("body=[<label style='width:70px;'>Estado:</label>" + dr["t431_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                    sb.Append("<label style='width:70px;'>Acuerdo:</label>" + dr["t666_idacuerdogv"].ToString() + " - " + dr["t666_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                    sb.Append("<label style='width:70px;'>Moneda:</label>" + dr["t422_denominacion"].ToString() + "] ");
                    sb.Append("hideselects=[off]\" ");
                }
                sb.Append(">");
                sb.Append("<td style='padding-left:10px;'>" + int.Parse(dr["t420_idreferencia"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td>" + ((DateTime)dr["t420_fTramitada"]).ToShortDateString() + "</td>");
                sb.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px'>" + decimal.Parse(dr["totaleuros"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return sb.ToString();
        }

    }
}