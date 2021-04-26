using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security; //para gestion de roles
using GASVI.DAL;
using Microsoft.JScript;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GASVI.BLL
{
	public partial class BonoTransporte
    {
        #region Propiedades
        
        #endregion

        public BonoTransporte()
		{
			
		}

        public static string mostrarBonos(string sEstado)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblBonos' class='MA W398' cellpadding='0' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px;' />");
            sb.Append("     <col style='width:255px;'/>");
            sb.Append("     <col style='width:128px;'/>");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.BonoTransporte.ObtenerBonos(sEstado);
            while (dr.Read()){
                sb.Append("<tr id='" + dr["t655_idBono"].ToString() + "' ");
                sb.Append("leido='0' ");
                sb.Append("style='height:20px;' ");
                sb.Append("idMoneda='" + dr["t422_idmoneda"].ToString() + "' ");
                sb.Append("titulo=\"" + Utilidades.escape(dr["t655_descripcion"].ToString()) + "\" ");
                if (dr["t655_descripcion"].ToString() != "")
                {
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                    sb.Append("body=[" + Utilidades.CadenaParaTooltipExtendido(dr["t655_descripcion"].ToString()) + "]\" ");
                }
                sb.Append("onClick='ms(this); visualizarTablas(this); iFilaBono=" + int.Parse(dr["t655_idBono"].ToString()) + "' ");
                sb.Append("ondblclick='modificarBono(this);'");                
                sb.Append(">");
                sb.Append("<td style='padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:2px;'>" + dr["t655_denominacion"].ToString() + "</td>");
                switch (dr["t655_estado"].ToString()) {
                    case "A":
                        sb.Append("<td style='padding-left:2px;'>Activo</td>");
                        break;
                    case "B":
                        sb.Append("<td style='padding-left:2px;'>Bloqueado</td>");
                        break;
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();        
        }
        
        public static string mostrarDatos(string sIdBono){
            StringBuilder sb = new StringBuilder();
            sb.Append(mostrarImportes(sIdBono) + "@#@" + mostrarBonosOficinas(sIdBono));
            return sb.ToString();        
        }

        public static string mostrarImportes(string sIdBono)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblImportes' class='MANO W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px;' />");
            sb.Append("     <col style='width:95px; />");
            sb.Append("     <col style='width:95px;' />");
            sb.Append("     <col style='width:95px;' />");
            sb.Append("     <col style='width:95px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.BonoTransporte.SelectImportes(int.Parse(sIdBono));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t656_idImporte"].ToString() + "' style='height:20px;' ");
                sb.Append("desde='" + dr["t656_desde"].ToString() + "' ");
                sb.Append("hasta='" + dr["t656_hasta"].ToString() + "' ");
                sb.Append("onClick='ms(this);' ");
                sb.Append(">");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td  style='text-align:right;'><input id='precio" + dr["t656_idImporte"].ToString() + "' type='text' style='width:60px;' class='txtNumL' onfocus='fn(this,3,2)' onchange='modificarArrayImporte(1,this.parentNode.parentNode);' value=\"");
                sb.Append(decimal.Parse(dr["t656_importe"].ToString()).ToString("N") + "\" />");
                //sb.Append("<label>&nbsp;&euro;</label></td>");
                sb.Append("<td style='padding-left:5px'>" + dr["t422_idmoneda"].ToString() + "</td>");
                sb.Append("<td onclick='getPeriodoImporte(this.parentNode);' style='padding-left:2px'>");
                sb.Append("<input id='desde" + dr["t656_idImporte"].ToString() + "' type='text' style='width:90px;' class='txtL' ReadOnly='true' value=\"");
                //if (dr["t656_desde"] != DBNull.Value)
                //    sb.Append(((DateTime)dr["t656_desde"]).ToShortDateString() + "\"></td>");
                //else 
                sb.Append("\"></td>");
                sb.Append("<td onclick='getPeriodoImporte(this.parentNode);' style='padding-left:2px'>");
                sb.Append("<input id='hasta" + dr["t656_idImporte"].ToString() + "' type='text' style='width:90px;' class='txtL' ReadOnly='true' value=\"");
                //if (dr["t656_hasta"] != DBNull.Value)
                //    sb.Append(((DateTime)dr["t656_hasta"]).ToShortDateString() + "\"></td>");
                //else 
                sb.Append("\"></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string mostrarBonosOficinas(string sIdBono)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblBonosOficinas' class='MM W406' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:15px;' />");
            sb.Append("     <col style='width:201px;' />");
            sb.Append("     <col style='width:95px;' />");
            sb.Append("     <col style='width:95px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.BonoTransporte.SelectBonoOficinas(int.Parse(sIdBono));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t657_idBonoOficina"].ToString() + "' style='height:20px;' ");
                sb.Append("oficina='" + dr["t010_idoficina"].ToString() + "' ");
                sb.Append("desde='" + dr["t657_desde"].ToString() + "' ");
                sb.Append("hasta='" + dr["t657_hasta"].ToString() + "' ");
                sb.Append("onClick='ms(this);' ");
                sb.Append("onmousedown=\"DD(event);\" ");
                sb.Append(">");

                sb.Append("<td style='padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t010_desoficina"].ToString() + "</td>");
                sb.Append("<td onclick='getPeriodoOficina(this.parentNode);' style='padding-left:2px;'>");
                sb.Append("<input id='desdeO" + dr["t657_idBonoOficina"].ToString() + "' type='text' style='width:90px;' onchange='comprobarFechas();' class='txtL' ReadOnly='true' value=\"");
                //if (dr["t657_desde"] != DBNull.Value)
                //    sb.Append(((DateTime)dr["t657_desde"]).ToShortDateString() + "\"></td>");
                //else 
                sb.Append("\"></td>");
                sb.Append("<td onclick='getPeriodoOficina(this.parentNode);' style='padding-left:2px;'>");
                sb.Append("<input id='hastaO" + dr["t657_idBonoOficina"].ToString() + "' type='text' style='width:90px;' onchange='comprobarFechas();' class='txtL' ReadOnly='true' value=\"");
                //if (dr["t657_hasta"] != DBNull.Value)
                //    sb.Append(((DateTime)dr["t657_hasta"]).ToShortDateString() + "\"></td>");
                //else 
                sb.Append("\"></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string mostrarOficinas()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblOficinas' class='MAM W390' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:310px;'/>");
            sb.Append("     <col style='width:80px; padding-left:2px;'/>");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.BonoTransporte.ObtenerOficinas();
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t010_idoficina"].ToString() + "' style='height:20px;'");
                sb.Append("onClick='mm(event)' ");
                sb.Append("ondblclick='anadirConvocados();'");
                sb.Append("onmouseover='TTip(event);' ");
                sb.Append("onmousedown=\"DD(event);\"'");
                sb.Append(">");
                sb.Append("<td style='padding-left:4px;'>" + dr["t010_desoficina"].ToString() + "</td>");
                sb.Append("<td style='padding-left:2px;'>" + int.Parse(dr["bonos"].ToString())+ "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string Grabar(string sBonos, string sImportes, string sOficinas)
        {
            string sResul = "", sElementosInsertados = "", sElementosInsertados2 = "", sElementosInsertados3 = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            int nAux = 0, nDel = 0, nIdBono = -1, nFechas = 0;
            string sNewBonos = "";
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
                #region Bonos
                if (sBonos != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aBonosGrabar = Regex.Split(sBonos, "#sFin#");
                    for (int i = 0; i <= aBonosGrabar.Length - 1; i++)
                    {
                        string[] aElemBono = Regex.Split(aBonosGrabar[i], "#sCad#");
                        switch (aElemBono[0])
                        {
                            case "I":
                                nAux = DAL.BonoTransporte.InsertBonoTransporte(tr, Utilidades.unescape(aElemBono[2]), aElemBono[4], Utilidades.unescape(aElemBono[3]), aElemBono[5].ToString());
                                if (sElementosInsertados == "")
                                {
                                    sElementosInsertados = nAux.ToString();
                                    sNewBonos = aElemBono[1] + "#sCad#" + nAux.ToString();
                                }
                                else
                                {
                                    sElementosInsertados += "#sCad#" + nAux.ToString();
                                    sNewBonos += "#sFin#" + aElemBono[1] + "#sCad#" + nAux.ToString();
                                }
                                break;
                            case "D":
                                nDel = DAL.BonoTransporte.DeleteBonoTransporte(tr, int.Parse(aElemBono[1]));
                                if (nDel == 0)
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Operacion rechazada! Existen usuarios asignados a ese bono."));
                                }
                                break;
                            case "U":
                                DAL.BonoTransporte.UpdateBonoTransporte(tr, int.Parse(aElemBono[1]), Utilidades.unescape(aElemBono[2]), aElemBono[4], Utilidades.unescape(aElemBono[3]), aElemBono[5].ToString());
                                break;
                        }
                    }
                }
                //sElementosInsertados += "@#@";
                sElementosInsertados = sNewBonos + "@#@";
                #endregion

                #region Importes
                if (sImportes != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aImportesGrabar = Regex.Split(sImportes, "#sFin#");
                    for (int i = 0, iCountLoop = aImportesGrabar.Length - 1; i <= iCountLoop; i++)
                    {
                        string[] aElemImporte = Regex.Split(aImportesGrabar[i], "#sCad#");
                        switch (aElemImporte[0])
                        {
                            case "I":
                                if (int.Parse(aElemImporte[1]) >= 30000)// 30000 este es el valor mínimo que se le ha indicado a todas las filas nuevas
                                {
                                    string[] aNewBonos = Regex.Split(sNewBonos, "#sFin#");
                                    for (int j=0, nCountLoop=aNewBonos.Length-1; j<=nCountLoop; j++)
                                    {
                                        string[] sElemNewBonos = Regex.Split(aNewBonos[j], "#sCad#");
                                        if(sElemNewBonos[0] == aElemImporte[1]){
                                            nIdBono = int.Parse(sElemNewBonos[1]);
                                            break;
                                        }
                                    }
                                }
                                ////Comprobamos que las fechas que queremos introducir no se solapen con alguna otra que ya existiera.
                                nFechas = DAL.BonoTransporte.FechasImporte(tr,
                                                                            (aElemImporte[3] == "") ? 0 : int.Parse(aElemImporte[2]),
                                                                            (nIdBono != -1) ? nIdBono : int.Parse(aElemImporte[1]),
                                                                            (aElemImporte[4] == "") ? 0 : int.Parse(aElemImporte[4]),
                                                                            (aElemImporte[5] == "") ? 0 : int.Parse(aElemImporte[5]));
                                if (nFechas == 0)
                                {
                                    nAux = DAL.BonoTransporte.InsertBonoImporte(tr,
                                                                                (nIdBono != -1) ? nIdBono : int.Parse(aElemImporte[1]),
                                                                                (aElemImporte[3] == "") ? 0 : decimal.Parse(aElemImporte[3]),
                                                                                (aElemImporte[4] == "") ? 0 : int.Parse(aElemImporte[4]),
                                                                                (aElemImporte[5] == "") ? 0 : int.Parse(aElemImporte[5]));
                                    nIdBono = -1;
                                    if (sElementosInsertados2 == "") sElementosInsertados2 = nAux.ToString();
                                    else sElementosInsertados2 += "#sCad#" + nAux.ToString();
                                }
                                else {
                                    bErrorControlado = true;
                                    throw (new Exception("Operacion rechazada! Denegado por solapamiento de fechas."));
                                }
                                break;
                            case "D":
                                DAL.BonoTransporte.DeleteBonoImporte(tr, int.Parse(aElemImporte[2]));
                                break;
                            case "U":
                                ////Comprobamos que las fechas que queremos introducir no se solapen con alguna otra que ya existiera.
                                nFechas = DAL.BonoTransporte.FechasImporte(tr,
                                                                            (aElemImporte[3] == "") ? 0 : int.Parse(aElemImporte[2]),
                                                                            (nIdBono != -1) ? nIdBono : int.Parse(aElemImporte[1]),
                                                                            (aElemImporte[4] == "") ? 0 : int.Parse(aElemImporte[4]),
                                                                            (aElemImporte[5] == "") ? 0 : int.Parse(aElemImporte[5]));
                                if (nFechas == 0)
                                {
                                    DAL.BonoTransporte.UpdateBonoImporte(tr, 
                                                                            int.Parse(aElemImporte[1]), int.Parse(aElemImporte[2]),
                                                                            (aElemImporte[3] == "") ? 0 : decimal.Parse(aElemImporte[3]),
                                                                            (aElemImporte[4] == "") ? 0 : int.Parse(aElemImporte[4]),
                                                                            (aElemImporte[5] == "") ? 0 : int.Parse(aElemImporte[5]));
                                }
                                else
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Operacion rechazada! Denegado por solapamiento de fechas."));
                                }
                                break;
                        }
                    }
                }
                sElementosInsertados += sElementosInsertados2;
                #endregion

                #region Oficinas
                if (sOficinas != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aOficinasGrabar = Regex.Split(sOficinas, "#sFin#");
                    for (int i = 0; i <= aOficinasGrabar.Length - 1; i++)
                    {
                        string[] aElemOficina = Regex.Split(aOficinasGrabar[i], "#sCad#");
                        switch (aElemOficina[0])
                        {
                            case "I":
                                if (int.Parse(aElemOficina[1]) >= 30000)// 30000 este es el valor mínimo que se le ha indicado a todas las filas nuevas
                                {
                                    string[] aNewBonos = Regex.Split(sNewBonos, "#sFin#");
                                    for (int j = 0, nCountLoop = aNewBonos.Length - 1; j <= nCountLoop; j++)
                                    {
                                        string[] sElemNewBonos = Regex.Split(aNewBonos[j], "#sCad#");
                                        if (sElemNewBonos[0] == aElemOficina[1])
                                        {
                                            nIdBono = int.Parse(sElemNewBonos[1]);
                                            break;
                                        }
                                    }
                                }
                                nFechas = DAL.BonoTransporte.FechasOficinas(tr,
                                                                            int.Parse(aElemOficina[2]),
                                                                            (nIdBono != -1) ? nIdBono : int.Parse(aElemOficina[1]),
                                                                            (aElemOficina[4] == "") ? 0 : int.Parse(aElemOficina[4]),
                                                                            (aElemOficina[5] == "") ? 0 : int.Parse(aElemOficina[5]),
                                                                            int.Parse(aElemOficina[3]));
                                if (nFechas == 0)
                                {
                                    nAux = DAL.BonoTransporte.InsertBonoOficina(tr,
                                                                                (nIdBono != -1) ? nIdBono : int.Parse(aElemOficina[1]),
                                                                                int.Parse(aElemOficina[3]),
                                                                                (aElemOficina[4] == "") ? 0 : int.Parse(aElemOficina[4]),
                                                                                (aElemOficina[5] == "") ? 0 : int.Parse(aElemOficina[5]));
                                    nIdBono = -1;
                                    if (sElementosInsertados3 == "") sElementosInsertados3 = nAux.ToString();
                                    else sElementosInsertados3 += "#sCad#" + nAux.ToString();
                                }
                                else
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Operacion rechazada! Denegado por solapamiento de fechas."));
                                }
                                break;
                            case "D":
                                DAL.BonoTransporte.DeleteBonoOficina(tr, int.Parse(aElemOficina[2]));
                                break;
                            case "U":
                                nFechas = DAL.BonoTransporte.FechasOficinas(tr,
                                                                            int.Parse(aElemOficina[2]),
                                                                            (nIdBono != -1) ? nIdBono : int.Parse(aElemOficina[1]), 
                                                                            (aElemOficina[4] == "") ? 0 : int.Parse(aElemOficina[4]),
                                                                            (aElemOficina[5] == "") ? 0 : int.Parse(aElemOficina[5]),
                                                                            int.Parse(aElemOficina[3]));
                                if (nFechas == 0)
                                {
                                    DAL.BonoTransporte.UpdateBonoOficina(tr,
                                                                        int.Parse(aElemOficina[1]),
                                                                        int.Parse(aElemOficina[2]), int.Parse(aElemOficina[3]),
                                                                        (aElemOficina[4] == "") ? 0 : int.Parse(aElemOficina[4]),
                                                                        (aElemOficina[5] == "") ? 0 : int.Parse(aElemOficina[5]));
                                }
                                else
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Operacion rechazada! Denegado por solapamiento de fechas."));
                                }
                                break;
                        }
                    }
                }
                sElementosInsertados += "@#@" + sElementosInsertados3;
                #endregion

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al grabar el bono transporte.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return sElementosInsertados;
        }

	}
}