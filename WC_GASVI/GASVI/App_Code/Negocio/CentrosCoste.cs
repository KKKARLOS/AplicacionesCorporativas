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
using System.Collections;

namespace GASVI.BLL
{
    public partial class CentrosCoste
    {
        #region Propiedades

        #endregion

        public CentrosCoste()
        {

        }

        public static string CatalogoEstructura()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblEstrutura' class='W600' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:200px; padding-left:4px;' />");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:150px; padding-left:4px;' />");
            sb.Append("    <col style='width:170px; padding-left:2px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("</colgroup>" + (char)13);

            SqlDataReader dr = DAL.CentrosCoste.CatalogoEstructura();
            int i = 0;
            string sTootTip = "";
            while (dr.Read())
            {

                if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["t303_denominacion"].ToString();

                sb.Append("<tr id='" + i + "' ");
                sb.Append("idnodo='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("idsubnodo='" + dr["t304_idsubnodo"].ToString() + "' ");
                sb.Append("representativo='" + dr["t303_representativo"].ToString() + "' ");
                sb.Append("idcencos='");
                if (dr["t175_idcc"] != DBNull.Value)
                    sb.Append(dr["t175_idcc"].ToString());
                sb.Append("' ");
                sb.Append("bd='' ");
                if (int.Parse(dr["t303_representativo"].ToString()) == 1 || int.Parse(dr["t304_idsubnodo"].ToString()) != 0)
                    sb.Append("onClick='ms(this); CatalogoCentrosCoste(" + int.Parse(dr["t303_idnodo"].ToString()) + "," + int.Parse(dr["t304_idsubnodo"].ToString()) + ");' ");
                sb.Append("style='height:20px; ");
                if (int.Parse(dr["t303_representativo"].ToString()) == 1 ||  int.Parse(dr["t304_idsubnodo"].ToString()) != 0)
                    sb.Append("cursor:pointer;'>");
                else 
                    sb.Append("'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td></td>");
                sb.Append("<td>");
                if (int.Parse(dr["t303_representativo"].ToString()) == 1 ||  int.Parse(dr["t304_idsubnodo"].ToString()) == 0)
                    sb.Append("<nobr class='NBR W190' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../Images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr>");
                sb.Append("</td>");
                sb.Append("<td></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W140'>" + dr["t304_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W160'>");
                if (int.Parse(dr["t303_representativo"].ToString()) == 1 || int.Parse(dr["t304_idsubnodo"].ToString()) != 0)
                {
                    if (dr["t175_idcc"] != DBNull.Value)
                        sb.Append(dr["t175_idcc"].ToString() + " - " + dr["t175_denominacion"].ToString());
                }
                sb.Append("</nobr></td>");
                if (int.Parse(dr["t303_representativo"].ToString()) == 1 || int.Parse(dr["t304_idsubnodo"].ToString()) != 0)
                {
                    if (dr["t175_idcc"] != DBNull.Value)
                        sb.Append("<td><img id='gomaCenCos" + i + "' src='../../../Images/Botones/imgBorrar.gif' border='0' onclick='borrarCenCos("+ i +");' style='filter:progid:DXImageTransform.Microsoft.Alpha(opacity=100); cursor:pointer; vertical-align:middle;' runat='server'></td>");
                    else
                        sb.Append("<td><img id='gomaCenCos" + i + "' src='../../../Images/Botones/imgBorrar.gif' border='0' style='filter:progid:DXImageTransform.Microsoft.Alpha(opacity=30); cursor:not-allowed; vertical-align:middle;' runat='server'></td>");
                }
                else sb.Append("<td></td>");
                sb.Append("</tr>" + (char)13);
                i++;
                sTootTip = "";
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoCenCos(string sDatos)
        {
            string[] aDatos = Regex.Split(sDatos, "#sCad#");
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatCenCos' class='W300 MA'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:60px; padding-left:2px;' />");
            sb.Append("    <col style='width:240px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.CentrosCoste.CatalogoCenCos((int.Parse(aDatos[0]) == 0) ? null : (int?)int.Parse(aDatos[0]),
                                                              (int.Parse(aDatos[1]) == 0) ? null : (int?)int.Parse(aDatos[1]));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t175_idcc"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("ondblclick='anadirCenCos(\"" + dr["t175_idcc"].ToString() + "\", \"" + dr["t175_denominacion"].ToString() + "\")' ");
                //sb.Append("class='MA' ");
                sb.Append("onmouseover='TTip(event)' ");
                sb.Append("style='height:20px;'> ");
                sb.Append("<td><nobr class='NBR W50'>" + dr["t175_idcc"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W230'>" + dr["t175_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoCenCosAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatCenCos' class='W350'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:60px; ' />");
            sb.Append("    <col style='width:290px; ' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.CentrosCoste.CatalogoCenCos(null, null);
            string sTootTip = "";
            while (dr.Read())
            {
                if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["t303_denominacion"].ToString();

                sb.Append("<tr id='" + dr["t175_idcc"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("ondblclick='ac(this);' ");
                sb.Append("class='MA' ");
                sb.Append("onmouseover='TTip(event)' ");
                sb.Append("style='height:16px;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../Images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W60'>" + dr["t175_idcc"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W280'>" + dr["t175_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)13);
                sTootTip = "";
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        //public static void Grabar(string sDatos)
        //{
        //    if (sDatos != "")
        //    {
        //        //Con la cadena generamos una lista y la recorremos para grabar cada elemento
        //        string[] aDatos = Regex.Split(sDatos, "#sFin#");
        //        for (int i = 0; i <= aDatos.Length - 1; i++)
        //        {
        //            string[] aElem = Regex.Split(aDatos[i], "#sCad#");
        //            DAL.CentrosCoste.UpdateCenCos(null,
        //                                          int.Parse(aElem[0]),
        //                                          short.Parse(aElem[1])
        //                                          );
        //        }
        //    }
        //}

        

        public static string ObtenerNodosCCIberper(string sBeneficiario)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:400px'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:400px; padding-left:3px;' />");
            sb.Append("</colgroup>" + (char)13);

            SqlDataReader dr = DAL.CentrosCoste.ObtenerNodosCCIberper(null, int.Parse(sBeneficiario));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t175_idcc"].ToString() + "' ");
                sb.Append("des_cencos='" + Utilidades.escape(dr["t175_denominacion"].ToString()) + "' ");
                sb.Append("idnodo='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("sMotivosEx='" + dr["sMotivosEx"].ToString() + "' ");
                sb.Append("onClick='ms(this);' ondblclick='aceptarClick(this);'");
                sb.Append("style='height:20px;'>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W390'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)13);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string CatalogoCenCosEstructura()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder resultado = new StringBuilder();
            SqlDataReader dr = DAL.CentrosCoste.CatalogoCenCosEstructura();

            sb.Append("[");
            sb.Append("{title: 'Arrastrar aquí para desasignar un centro de coste', key:'-1'},");
            while (dr.Read())
            {
                sb.Append("{title: \"" + dr["t175_denominacion"].ToString() + "\", key:\"CC@#sep#@" + dr["t175_idcc"].ToString() + "@#sep#@7@#sep#@" + ((dr["t175_estadogasvi"].ToString() == "True") ? 1 : 0) + "\"},");
            }

            resultado.Append(sb.ToString().Substring(0, sb.Length -1));
            if (resultado.ToString() != "") //si no hay centros de coste
                resultado.Append("]");
            return resultado.ToString();
            
        }
        
        public static void GrabarEstadoGasvi(string idCenCos, short estadoGasvi)
        {
            DAL.CentrosCoste.UpdateCenCos(null, idCenCos, estadoGasvi);
        }

        public static void GrabarEstrucCenCos(string nodeKey, string idCC)
        {
            string[] aNodeKey = Regex.Split(nodeKey, "@#sep#@");
            string[] aIdCC = Regex.Split(idCC, "@#sep#@");
            Nullable<int> idNodo = null;
            Nullable<int> idSubNodo = null;
            if (aNodeKey[0] == "ND")
                idNodo = int.Parse(aNodeKey[3]);
            else if (aNodeKey[0] == "SN")
                idSubNodo = int.Parse(aNodeKey[3]);

            DAL.CentrosCoste.UpdateEstrucCenCos(null, idNodo, idSubNodo, aIdCC[1]);
        }
    }
}