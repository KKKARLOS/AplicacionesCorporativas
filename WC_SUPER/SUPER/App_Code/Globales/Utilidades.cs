using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Datos;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve .
    /// </summary>
    public class Utilidades
    {
        public Utilidades()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }

        public static string CadenaConexion
        {
            get { return obtenerCadenaConexion(); }
        }
        private static string obtenerCadenaConexion()
        {
            string sConn = "";
            if (HttpContext.Current == null)
            {   //Para cuando se ejecuta el método en un segundo hilo y no hay httpcontext
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
                else if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "D")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();
                else if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "P")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionPruebas"].ToString();
                else
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion" + System.Configuration.ConfigurationManager.AppSettings["ENTORNO"]].ToString();

            }
            else if (HttpContext.Current.Cache.Get("CadenaConexion") == null)
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
                else if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "D")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();
                else if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "P")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionPruebas"].ToString();
                else
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion" + System.Configuration.ConfigurationManager.AppSettings["ENTORNO"]].ToString();

                HttpContext.Current.Cache.Insert("CadenaConexion", sConn, null, DateTime.Now.AddHours(24), TimeSpan.Zero);
            }
            else
            {
                sConn = (string)HttpContext.Current.Cache.Get("CadenaConexion");
            }

            //if (HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"] != null)
            //{
            //    //sConn = sConn.Replace("app=SUPER", "app=SUPER: " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString());
            //}
            return sConn;
        }
        public static string RootAplica()
        {
            string sResul = "";
            string[] sUrlAux = Regex.Split(System.Web.HttpContext.Current.Request.ServerVariables["URL"], "/");
            if (sUrlAux[1].ToUpper() != "SUPER") sResul = "/";
            else sResul = "/SUPER/";
            return sResul;
        }
        public static string TamanoArchivo(int nBytes)
        {
            string sResul = "";
            if (nBytes > 0)
            {
                if (nBytes > 1048576) //1MB
                {
                    double nMbs = (double)nBytes / 1048576;
                    sResul = "&nbsp;&nbsp;&nbsp;(" + nMbs.ToString("N") + " Mb.)";
                }
                else if (nBytes > 1024) //1KB
                {
                    double nKbs = (double)nBytes / 1024;
                    sResul = "&nbsp;&nbsp;&nbsp;(" + nKbs.ToString("N") + " Kb.)";
                }
                else
                {
                    sResul = "&nbsp;&nbsp;&nbsp;(" + nBytes.ToString() + " bytes)";
                }
            }
            return sResul;
        }

        public static bool isNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }
        public static bool isDate(object value)
        {
            try
            {
                if (value.ToString() == "") return false;

                DateTime d = System.Convert.ToDateTime(value.ToString());
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        public static string decodpar(string sCadena)
        {
            if (sCadena == null) return "";
            if (sCadena == "") return "";
            return Utilidades.unescape(Encoding.ASCII.GetString(System.Convert.FromBase64String(sCadena)));
        }

        //static public string EncodeTo64(string toEncode)
        //{
        //    return System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode));
        //}

        //static public string DecodeFrom64(string encodedData)
        //{
        //    return System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(encodedData));
        //}

        public static string escape(string sCadena)
        {
            if (sCadena == null) return "";  //El método EscapeDataString no acepta un null como input

            // lo comentado es para que si hubiera caractares de control que no se escapean sustituirlo por su valor hexadecimal
            //string[] UriRfc3986CharsToEscape = new[] { "!", "*", "'", "(", ")" };

            int nLongMax = 32766; //Longitud máxima permitida por el método EscapeDataString
            int nBloques = sCadena.Length / nLongMax;

            string sResultado = "";
            for (int i = 0; i <= nBloques; i++)
            {
                /*
                StringBuilder escaped = new StringBuilder(Uri.EscapeDataString(sCadena.Substring(i * nLongMax, Math.Min(sCadena.Length - (i * nLongMax), nLongMax))));
                for (int j = 0; j < UriRfc3986CharsToEscape.Length; j++)
                {
                    escaped.Replace(UriRfc3986CharsToEscape[j], Uri.HexEscape(UriRfc3986CharsToEscape[j][0]));
                }
                sResultado += escaped.ToString();
                */
                sResultado += System.Uri.EscapeDataString(sCadena.Substring(i * nLongMax, Math.Min(sCadena.Length - (i * nLongMax), nLongMax)));
            }




            return sResultado;
            //return System.Uri.EscapeDataString(sCadena);
        }
        public static string unescape(string sCadena)
        {
            if (sCadena == null) return "";  // El método UnescapeDataString no acepta un null como input
                                             // También puede dar problemas cuando a una cadena con caracteres especiales
                                             // y que no se ha escapeado en origen se le hace el unescape

            return System.Uri.UnescapeDataString(sCadena);
        }
        public static string CadenaParaTooltipExtendido(string sCadena)
        {
            return sCadena.Replace(((char)91).ToString(), "&#91;&#91;").Replace(((char)93).ToString(), "&#93;&#93;").Replace(((char)60).ToString(), "&#60;").Replace(((char)62).ToString(), "&#62;").Replace(((char)34).ToString(), "&#34;").Replace(((char)39).ToString(), "&#39;");//Se duplican los corchetes porque luego la función del boxover.js convierte las dobles en simples.
        }

        public static bool EsModuloAccesible(string sModulo)
        {
            bool bEsAccesible = false;
            if (HttpContext.Current.Cache.Get("ModuloAccesible") == null)
            {
                Hashtable htModulos = new Hashtable();
                SqlDataReader dr = ACCESOMODULO.Catalogo("", null, 1, 0);
                while (dr.Read())
                {
                    htModulos.Add(dr["t434_modulo"].ToString(), (bool)dr["t434_acceso"]);
                    if (dr["t434_modulo"].ToString() == sModulo) bEsAccesible = (bool)dr["t434_acceso"];
                }
                dr.Close();
                dr.Dispose();
                HttpContext.Current.Cache.Insert("ModuloAccesible", htModulos, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
            else
            {
                Hashtable htModulos = (Hashtable)HttpContext.Current.Cache.Get("ModuloAccesible");
                bEsAccesible = (bool)htModulos[sModulo];
                //bool bEsAccesibleAux = (bool)htModulos[sModulo];
                //if (!bEsAccesibleAux != null)
                //{
                //    bEsAccesible = (bool)bEsAccesibleAux;
                //}
            }
            return bEsAccesible;
        }

        public static string TraduccionMeteo(string sOrigen)
        {
            string sResultado = sOrigen;
            if (HttpContext.Current.Cache.Get("DiccionarioMeteo") == null)
            {
                Hashtable htTablas = new Hashtable();
                SqlDataReader dr = DICCIONARIOMETEO.Catalogo();
                bool bExiste = false;
                bool bHayFilas = false;
                while (dr.Read())
                {
                    htTablas.Add(dr["t497_origen"].ToString(), dr["t479_resultado"].ToString());
                    if (dr["t497_origen"].ToString() == sOrigen)
                    {
                        bExiste = true;
                        if (dr["t479_resultado"].ToString() != "")
                            sResultado = dr["t479_resultado"].ToString();
                    }
                }
                if (dr.HasRows) bHayFilas = true;
                dr.Close();
                dr.Dispose();

                if (!bExiste)
                {
                    DICCIONARIOMETEO.InsertSiNoExiste(null, sOrigen, "");
                    htTablas.Add(sOrigen, "");
                }
                if (bHayFilas)
                    HttpContext.Current.Cache.Insert("DiccionarioMeteo", htTablas, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            else
            {
                Hashtable htTablas = (Hashtable)HttpContext.Current.Cache.Get("DiccionarioMeteo");
                string sResultadoAux = (string)htTablas[sOrigen];
                if (sResultadoAux != null)
                {
                    if (sResultadoAux != "")
                        sResultado = sResultadoAux;
                }
                else
                {
                    htTablas.Add(sOrigen, "");
                    DICCIONARIOMETEO.InsertSiNoExiste(null, sOrigen, "");
                    HttpContext.Current.Cache.Remove("DiccionarioMeteo");
                    HttpContext.Current.Cache.Insert("DiccionarioMeteo", htTablas, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                }
            }
            return sResultado;
        }

        public static bool EstructuraActiva(string sNivel)
        {
            bool bActiva = false;
            if (HttpContext.Current.Cache.Get("EstructuraActiva") == null)
            {
                Hashtable htNivel = new Hashtable();
                List<Estructura> oLista = Estructura.ListaGlobal();
                foreach (Estructura oEstr in oLista)
                {
                    switch (oEstr.nCodigo)
                    {
                        case 6:
                            if (oEstr.bUtilizado) htNivel.Add("SN4", true);
                            else htNivel.Add("SN4", false);
                            break;
                        case 5:
                            if (oEstr.bUtilizado) htNivel.Add("SN3", true);
                            else htNivel.Add("SN3", false);
                            break;
                        case 4:
                            if (oEstr.bUtilizado) htNivel.Add("SN2", true);
                            else htNivel.Add("SN2", false);
                            break;
                        case 3:
                            if (oEstr.bUtilizado) htNivel.Add("SN1", true);
                            else htNivel.Add("SN1", false);
                            break;
                    }
                }
                bActiva = (bool)htNivel[sNivel];
                HttpContext.Current.Cache.Insert("EstructuraActiva", htNivel, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
            else
            {
                Hashtable htNivel = (Hashtable)HttpContext.Current.Cache.Get("EstructuraActiva");
                bActiva = (bool)htNivel[sNivel];
            }
            return bActiva;
        }

        public static string ObtenerDocumentos(string sTipo, int IdElem, string sPermiso, string sEstProy)
        {
            StringBuilder sb = new StringBuilder();
            string sTabla = "", sNomArchivo = "", sKey = "", sPathImg = "../../../images/", sLenColAutor = "100", sAnchoTabla = "850";
            string sMano = "MA";
            bool bModificable = false;
            int idUserAct = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            SqlDataReader dr = null;
            try
            {
                #region seleccion de tabla
                switch (sTipo)
                {
                    case "PE":
                        //dr = DOCUPE.Catalogo(null, IdElem, "", "", "", null, null, null, null, null, null, null, null, 3, 0);
                        dr = DOCUPE.Catalogo2(IdElem, idUserAct);
                        sTabla = "t368";
                        sKey = "t368_iddocupe";
                        sAnchoTabla = "960";
                        sLenColAutor = "150";
                        break;
                    case "PSN":
                        //dr = DOCUPE.Catalogo(null, IdElem, "", "", "", null, null, null, null, null, null, null, null, 3, 0);
                        dr = DOCUPE.Catalogo3(IdElem, idUserAct);
                        sTabla = "t368";
                        sKey = "t368_iddocupe";
                        sPathImg = "../../../../images/";
                        sMano = "MA";
                        break;
                    case "PEF":
                        dr = DOC_ACUERDO_PROY.Catalogo2(IdElem, idUserAct);
                        sTabla = "t640";
                        sKey = "t640_iddocfact";
                        sPathImg = "../../../../images/";
                        sMano = "MA";
                        break;
                    case "PT":
                        dr = DOCUPT.Catalogo2(IdElem, idUserAct);
                        sTabla = "t362";
                        sKey = "t362_iddocupt";
                        break;
                    case "F":
                        dr = DOCUF.Catalogo2(IdElem, idUserAct);
                        sTabla = "t364";
                        sKey = "t364_iddocuf";
                        break;
                    case "A":
                        dr = DOCUA.Catalogo2(IdElem, idUserAct);
                        sTabla = "t365";
                        sKey = "t365_iddocua";
                        break;
                    case "T":
                        dr = DOCUT.Catalogo2(IdElem, idUserAct);
                        sTabla = "t363";
                        sKey = "t363_iddocut";
                        break;
                    case "IAP_T":
                        dr = DOCUT.Catalogo3(IdElem, idUserAct);
                        sTabla = "t363";
                        sKey = "t363_iddocut";
                        sAnchoTabla = "800";
                        sLenColAutor = "50";
                        break;
                    case "HT":
                    case "HM":
                        dr = DOCUH.Catalogo2(IdElem, idUserAct);
                        sTabla = "t366";
                        sKey = "t366_iddocuh";
                        break;
                    case "HF":
                        dr = DOCUHE.Catalogo2(IdElem, idUserAct);
                        sTabla = "t367";
                        sKey = "t367_iddocuhe";
                        break;
                    case "AS_PE":
                        dr = DOCASU.Catalogo2(IdElem, idUserAct);
                        sTabla = "t386";
                        sKey = "t386_iddocasu";
                        sAnchoTabla = "920";
                        sLenColAutor = "170";
                        sPathImg = "../../../../../images/";
                        sMano = "MA";
                        break;
                    case "AS_PT":
                        dr = DOCASU_PT.Catalogo2(IdElem, idUserAct);
                        sTabla = "t411";
                        sKey = "t411_iddocasu";
                        sAnchoTabla = "920";
                        sLenColAutor = "170";
                        sPathImg = "../../../../../images/";
                        sMano = "MA";
                        break;
                    case "AS_T":
                        dr = DOCASU_T.Catalogo2(IdElem, idUserAct);
                        sTabla = "t602";
                        sKey = "t602_iddocasu";
                        sAnchoTabla = "920";
                        sLenColAutor = "170";
                        sPathImg = "../../../../../images/";
                        sMano = "MA";
                        break;
                    case "AC_PE":
                        dr = DOCACC.Catalogo2(IdElem, idUserAct);
                        sTabla = "t387";
                        sKey = "t387_iddocacc";
                        sAnchoTabla = "920";
                        sLenColAutor = "170";
                        sPathImg = "../../../../../images/";
                        sMano = "MA";
                        break;
                    case "AC_PT":
                        dr = DOCACC_PT.Catalogo2(IdElem, idUserAct);
                        sTabla = "t412";
                        sKey = "t412_iddocacc";
                        sAnchoTabla = "920";
                        sLenColAutor = "170";
                        sPathImg = "../../../../../images/";
                        sMano = "MA";
                        break;
                    case "AC_T":
                        dr = DOCACC_T.Catalogo2(IdElem, idUserAct);
                        sTabla = "t603";
                        sKey = "t603_iddocacc";
                        sAnchoTabla = "920";
                        sLenColAutor = "170";
                        sPathImg = "../../../../../images/";
                        sMano = "MA";
                        break;
                }
                #endregion
                if (sPermiso == "R")//Modo lectura
                {
                    #region Modo Lectura
                    sb.Append("<table id='tblDocumentos' class='texto mano' style='width: " + sAnchoTabla + "px text-align:left;; cursor:pointer;'>");
                    sb.Append("<colgroup><col style='width:290px;' /><col style='width:235px' /><col style='width:225px' /><col style='width:'" + sLenColAutor + "px' />");
                    if (sTipo == "PE") sb.Append("<col style='width:60px' />");
                    sb.Append("</colgroup>");
                    sb.Append("<tbody>");
                    while (dr.Read())
                    {
                        sNomArchivo = dr[sTabla + "_nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                        sb.Append("<tr style='height:20px;' id='" + dr[sKey].ToString() + "'");
                        sb.Append(" onclick='mm2(event);' sTipo='" + sTipo + "' sAutor='" + dr["t314_idusuario_autor"].ToString() + "' onmouseover='TTip(event)'>");
                        sb.Append("<td><nobr class='NBR' style='width:280px; padding-left:5px;'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");
                        if (sNomArchivo == "")
                            sb.Append("<td></td>");
                        else
                        {
                            //23/03/2010 Victor. El archivo siempre es descargable
                            sb.Append("<td><img src='" + sPathImg + "imgDescarga.gif' width='16px' height='16px' onclick=\"descargar('" + sTipo + "', this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                            sb.Append("&nbsp;<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
                        }

                        if (dr[sTabla + "_weblink"].ToString() == "")
                            sb.Append("<td></td>");
                        else
                        {
                            string sHTTP = "";
                            if (dr[sTabla + "_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                            sb.Append("<td><a href='" + sHTTP + dr[sTabla + "_weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr[sTabla + "_weblink"].ToString() + "</nobr></a></td>");
                        }
                        sb.Append("<td><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td>");

                        if (sTipo == "PE") sb.Append("<td><nobr style='width:60px;'>" + DateTime.Parse(dr[sTabla + "_fechamodif"].ToString()).ToShortDateString() + "</nobr></td></tr>");
                        else sb.Append("</tr>");
                    }
                    #endregion
                }
                else
                {
                    if (sEstProy == "C" || sEstProy == "H")//Proyecto cerrado o histórico
                    {
                        #region Proyecto Cerrado o Historico
                        sb.Append("<table id='tblDocumentos' class='texto MANO' style='width: " + sAnchoTabla + "px; cursor:pointer; text-align:left;'>");
                        sb.Append("<colgroup><col style='width:290px;' /><col style='width:235px' /><col style='width:225px' /><col style='width:" + sLenColAutor + "px' /></colgroup>");
                        sb.Append("<tbody>");
                        while (dr.Read())
                        {
                            sNomArchivo = dr[sTabla + "_nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                            sb.Append("<tr style='height:20px;' id='" + dr[sKey].ToString() + "'");
                            sb.Append(" onclick='mm2(event);' sTipo='" + sTipo + "' sAutor='" + dr["t314_idusuario_autor"].ToString() + "' onmouseover='TTip(event)'>");
                            sb.Append("<td><nobr class='NBR' style='width:280px; padding-left:5px;'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");
                            if (sNomArchivo == "")
                                sb.Append("<td></td>");
                            else
                            {
                                //23/03/2010 Victor. El archivo siempre es descargable
                                //Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                                //if ((!(bool)dr[sTabla + "_privado"]) || ((bool)dr[sTabla + "_privado"] && dr["t314_idusuario_autor"].ToString() == HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"].ToString()) || HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A")
                                sb.Append("<td><img src='" + sPathImg + "imgDescarga.gif' width='16px' height='16px' onclick=\"descargar('" + sTipo + "', this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                                //else
                                //    sb.Append("<td style=\"cursor:auto\"><img src='" + sPathImg + "imgSeparador.gif' width='16px' height='16px' style='vertical-align:bottom;'>");
                                sb.Append("&nbsp;<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
                            }

                            if (dr[sTabla + "_weblink"].ToString() == "")
                                sb.Append("<td></td>");
                            else
                            {
                                string sHTTP = "";
                                if (dr[sTabla + "_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                                sb.Append("<td><a href='" + sHTTP + dr[sTabla + "_weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr[sTabla + "_weblink"].ToString() + "</nobr></a></td>");
                            }
                            sb.Append("<td><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td>");

                            if (sTipo == "PE") sb.Append("<td><nobr style='width:60px;'>" + DateTime.Parse(dr[sTabla + "_fechamodif"].ToString()).ToShortDateString() + "</nobr></td></tr>");
                            else sb.Append("</tr>");
                        }
                        #endregion
                    }
                    else
                    {
                        #region Acceso total
                        sb.Append("<table id='tblDocumentos' class='texto MANO' style='width: " + sAnchoTabla + "px; text-align:left;'>");
                        sb.Append("<colgroup><col style='width:290px;' /><col style='width:235px' /><col style='width:225px' /><col style='width:" + sLenColAutor + "px' /></colgroup>");
                        sb.Append("<tbody>");
                        while (dr.Read())
                        {   //Si el archivo NO es sólo lectura, o si el usuario es el autor del archivo, o es administrador, se permite modificar.
                            if ((dr["t314_idusuario_autor"].ToString() == HttpContext.Current.Session["UsuarioActual"].ToString() ||
                                 SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) || (!(bool)dr[sTabla + "_modolectura"]))
                                bModificable = true;
                            else
                                bModificable = false;

                            sb.Append("<tr style='height:20px;' id='" + dr[sKey].ToString() + "' onclick='mm2(event);' sTipo='" + sTipo + "' sAutor='" + dr["t314_idusuario_autor"].ToString() + "' onmouseover='TTip(event)'>");

                            if (bModificable)
                            {
                                //sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR' style='width:280px; padding-left:5px;'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");
                                sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc('" + sTipo + "', this.parentNode.id)\"><nobr class='NBR' style='width:280px; padding-left:5px;'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");
                            }
                            else
                                sb.Append("<td><nobr class='NBR' style='width:280px; padding-left:5px;'>" + dr[sTabla + "_descripcion"].ToString() + "</nobr></td>");

                            if (dr[sTabla + "_nombrearchivo"].ToString() == "")
                            {
                                if (bModificable)
                                {
                                    //sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                                    sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc('" + sTipo + "', this.parentNode.id)\"></td>");
                                }
                                else
                                    sb.Append("<td></td>");
                            }
                            else
                            {
                                sNomArchivo = dr[sTabla + "_nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                                //23/03/2010 Victor. El archivo siempre es descargable
                                //Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                                sb.Append("<td><img src='" + sPathImg + "imgDescarga.gif' width='16px' height='16px'");
                                sb.Append(" onclick=\"descargar('" + sTipo + "', this.parentNode.parentNode.id);\" ");
                                sb.Append(" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                                sb.Append("&nbsp;");
                                if (bModificable)
                                {
                                    sb.Append("<nobr class='NBR " + sMano + "' style='width:205px;' ");
                                    //sb.Append(" ondblclick=\"modificarDoc(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                                    sb.Append(" ondblclick=\"modificarDoc('" + sTipo + "', this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                                }
                                else
                                    sb.Append("<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
                            }

                            if (dr[sTabla + "_weblink"].ToString() == "")
                            {
                                if (bModificable)
                                {
                                    //sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                                    sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc('" + sTipo + "', this.parentNode.id)\"></td>");
                                }
                                else
                                    sb.Append("<td></td>");
                            }
                            else
                            {
                                string sHTTP = "";
                                if (dr[sTabla + "_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                                sb.Append("<td><a href='" + sHTTP + dr[sTabla + "_weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr[sTabla + "_weblink"].ToString() + "</nobr></a></td>");
                            }

                            if (bModificable)
                            {
                                //sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
                                sb.Append("<td class='" + sMano + "' ondblclick=\"modificarDoc('" + sTipo + "', this.parentNode.id)\"><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td>");
                            }
                            else
                                sb.Append("<td><nobr class='NBR' style='width:" + sLenColAutor + "px;'>" + dr["autor"].ToString() + "</nobr></td>");

                            if (sTipo == "PE") sb.Append("<td><nobr style='width:60px;'>" + DateTime.Parse(dr[sTabla + "_fechamodif"].ToString()).ToShortDateString() + "</nobr></td></tr>");
                            else sb.Append("</tr>");

                        }
                        #endregion
                    }
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");
            }
            catch (Exception ex)
            {
                sb.Append("Error@#@" + Errores.mostrarError("Error al obtener documentos del elemento", ex));
            }
            return sb.ToString();
        }

        public static SqlDataReader ObtenerActividadSuper()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_ACTIVIDADSUPER", aParam);
        }

        public static void InsertUsuario(string sCODRED)
        {
            if (HttpContext.Current.Cache.Get("UsuariosConectados") == null)
            {
                Hashtable htUsuarios = new Hashtable();
                htUsuarios.Add(sCODRED, sCODRED);

                HttpContext.Current.Cache.Insert("UsuariosConectados", htUsuarios, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
            else
            {
                Hashtable htUsuarios = (Hashtable)HttpContext.Current.Cache.Get("UsuariosConectados");
                if (htUsuarios[sCODRED] == null)
                    htUsuarios.Add(sCODRED, sCODRED);
                HttpContext.Current.Cache.Insert("UsuariosConectados", htUsuarios, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
        }
        public static void DeleteUsuario(string sCODRED)
        {
            if (HttpContext.Current.Cache.Get("UsuariosConectados") != null)
            {
                Hashtable htUsuarios = (Hashtable)HttpContext.Current.Cache.Get("UsuariosConectados");
                htUsuarios.Remove(sCODRED);

                HttpContext.Current.Cache.Insert("UsuariosConectados", htUsuarios, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            }
        }
        /* Los profesionales tienen en T001_FICEPI.t001_botonfecha un parámetro que indica el tipo de funcionalidad
         * que desea para las cajas de texto donde se manejan fechas.
         * En función de esa configuración caunado se entra a una pantalla que contiene fechas, desde el Page_Load
         * se llama a esta función para cada uno de los campos fecha que existan para establecer su functionalidad
         * sTipo="I" -> la caja no es editable y el control calendario se muestra con click izquierdo
         * sTipo="D" -> la caja es editable y el control calendario se muestra con click derecho
         * */
        public static void SetEventosFecha(object oFecha)
        {
            TextBox txtFecha = (TextBox)oFecha;
            if (HttpContext.Current.Session["BTN_FECHA"].ToString() == "D")
            {
                txtFecha.Attributes.Add("onfocus", "focoFecha(event);");
                txtFecha.Attributes.Add("onmousedown", "mc1(event);");
                txtFecha.Attributes.Remove("onclick");
                txtFecha.Attributes.Remove("readonly");
            }
            else
            {
                txtFecha.Attributes.Add("readonly", "readonly");
                txtFecha.Attributes.Add("onclick", "mc(event)");
                txtFecha.Attributes.Remove("onfocus");
                txtFecha.Attributes.Remove("onmousedown");
            }
        }

        /// <summary>  
        /// Obtiene un byte array a partir de un fichero 
        /// </summary>  
        /// <param name="_FileName">Nombre del fichero</param>  
        /// <returns>Byte Array</returns>  
        public static byte[] FileToByteArray(string _FileName)  
        {  
             byte[] _Buffer = null;  
             //try 
             //{  
                 // Open file for reading  
                 System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);  
                 // attach filestream to binary reader  
                 System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);  
                 // get total byte length of the file  
                 long _TotalBytes = new System.IO.FileInfo(_FileName).Length;  
                 // read entire file into buffer  
                 _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);  
                 // close file reader  
                 _FileStream.Close();  
                 _FileStream.Dispose();  
                _BinaryReader.Close();  
             //}  
             //catch (Exception _Exception)  
             //{  
             //    Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());  
             //}  
             return _Buffer;  
        }
        public static void ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            //try
            //{
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                //return true;
            //}
            //catch (Exception _Exception)
            //{
            //    // Error
            //    Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            //}

            // error occured, return false
            //return false;
        }
        //public string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // Convert Image to byte[]
        //        image.Save(ms, format);
        //        byte[] imageBytes = ms.ToArray();

        //        // Convert byte[] to Base64 String
        //        string base64String = System.Convert.ToBase64String(imageBytes);
        //        return base64String;
        //    }
        //}
        public static string ImageToBase64(string sPath, System.Drawing.Imaging.ImageFormat format)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sPath);

            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = System.Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }


        public static string Entorno
        {
            get { return EntornoAplicacion(); }
        }
        private static string EntornoAplicacion()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
        }

        public static string ObtenerAccionesPendientesV2()
        {
            int nCountAcciones = 0;
            StringBuilder sb = new StringBuilder();
            int? idFicepi = null;
            int? idUser = null;

            if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString() ||
                HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString())
            {//Hay reconexión como otro
                if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString())
                    idFicepi = int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString());

                if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString())
                    idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            }
            else
            {
                idFicepi = int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString());
                idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            }
            //SqlDataReader dr = SUPER.Capa_Datos.USUARIO.ObtenerAccionesPendientesV2(null, ((int)HttpContext.Current.Session["UsuarioActual"] == 0) ? null : (int?)int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString()), int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()));
            SqlDataReader dr = SUPER.Capa_Datos.USUARIO.ObtenerAccionesPendientesV2(null, idUser, idFicepi);

            HttpContext.Current.Session["BloquearPGEByAcciones"] = false;
            HttpContext.Current.Session["BloquearPSTByAcciones"] = false;
            HttpContext.Current.Session["BloquearIAPByAcciones"] = false;

            while (dr.Read())
            {
                //for (int i = 0; i < 30; i++){ //Simulación de muchas acciones para ver el scroll.
                //Si hay alguna acción que bloquee el acceso a PGE.
                //Session["BloquearPGEByAcciones"] = true;
                //Session["BloquearPSTByAcciones"] = true;
                //Session["BloquearIAPByAcciones"] = true;
                if ((byte)dr["criticidad"] == 2)
                {
                    switch (dr["modulo"].ToString())
                    {
                        case "PGE": HttpContext.Current.Session["BloquearPGEByAcciones"] = true; break;
                        case "PST": HttpContext.Current.Session["BloquearPSTByAcciones"] = true; break;
                        case "IAP": HttpContext.Current.Session["BloquearIAPByAcciones"] = true; break;
                    }
                }
                sb.Append(@"<tr style='height:20px;' onclick='goAccion(" + dr["codigo"].ToString() + @")' >
                            <td><img src='" + HttpContext.Current.Session["strServer"].ToString() + @"Images/imgSeparador.gif' class='" + dr["class_flag"].ToString() + @"' /></td>
                            <td class='Link'>" + dr["denominacion"].ToString() + @"</td>
                        </tr>");
                //}
                nCountAcciones++;
            }
            dr.Close();
            dr.Dispose();
            //sHTMLAP = sb.ToString();

            return sb.ToString() + "@#@"
                    + nCountAcciones.ToString() + "@#@"
                    + (((bool)HttpContext.Current.Session["BloquearPGEByAcciones"]) ? "1" : "0") + "@#@"
                    + (((bool)HttpContext.Current.Session["BloquearPSTByAcciones"]) ? "1" : "0") + "@#@"
                    + (((bool)HttpContext.Current.Session["BloquearIAPByAcciones"]) ? "1" : "0");
        }

        public static string getInt(string sCadena)
        {
            string sRes="";
            if (sCadena !="")
            {
                sRes = (int.Parse(sCadena)).ToString("#,###");
            }
            return sRes;
        }
        /// <summary>
        /// Genera una cadena aleatoria
        /// </summary>
        /// <param name="size">Longitud de la cadena a crear</param>
        /// <returns></returns>
        public static string GenerarPassw(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static void RegistrarAcceso(int nPantallaAcceso)
        {
            try
            {
                SUPER.DAL.REGISTROUSOPANTALLASSUPER.Registrar(nPantallaAcceso, (int)HttpContext.Current.Session["IDFICEPI_ENTRADA"]);
            }
            catch (Exception ex)
            {
                Errores.mostrarError("Error al registrar acceso del profesional " + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() + " a la pantalla " + nPantallaAcceso.ToString(), ex);
            }
        }
        /// <summary>
        /// dadas dos cadenas de elementos separados por comas comprueba si los de la primera lista son un subconjunto de la segunda
        /// </summary>
        /// <param name="sLista1"></param>
        /// <param name="sLista2"></param>
        /// <returns></returns>
        public static bool EsSubconjunto(string sLista1, string sLista2)
        {
            bool bEsSubConjunto = true;
            string[] aL1 = Regex.Split(sLista1, ",");
            string[] aL2 = Regex.Split(sLista2, ",");
            foreach (string oElem1 in aL1)
            {
                if (oElem1 == "") continue;
                else
                {
                    if (!EstaEnLista(oElem1, aL2))
                    {
                        bEsSubConjunto=false;
                        break;
                    }
                }
            }
            return bEsSubConjunto;
        }
        /// <summary>
        /// Dado un elemento y una lista de elementos, obtiene si el elemento está en la lista
        /// </summary>
        /// <param name="sElem"></param>
        /// <param name="sLista"></param>
        /// <returns></returns>
        public static bool EstaEnLista(string sElem, string[] aLista)
        {
            bool bEstaEnLista = false;
            foreach (string oElem in aLista)
            {
                if (oElem == "") break;
                else
                {
                    if (oElem == sElem)
                    {
                        bEstaEnLista = true;
                        break;
                    }
                }
            }
            return bEstaEnLista;
        }
        /// <summary>
        /// Devuelve una cadena con el mensaje de error en accesos fallidos al Content-Server
        /// </summary>
        /// <param name="sAccion">W->Subir archivo, R->Descargar archivo</param>
        /// <param name="cex"></param>
        /// <returns></returns>
        public static string MsgErrorConserva(string sAccion, ConservaException cex)
        {
            string sRes = "";
            bool bCAU = true;

            switch (sAccion)
            {
                case "W"://Está intentando grabar un archivo en el Content-Server
                    sRes = "Error al almacenar el documento." + (char)10 + (char)10;
                    break;
                case "R"://Está intentando traer un archivo del Content-Server
                    sRes = "Error al descargar el documento." + (char)10 + (char)10;
                    break;
            }
            if (cex.InnerException == null)
            {
                switch (cex.ErrorCode)
                {
                    case 100:
                        bCAU = false;//No es un problema del Content-Server sino del fichero que intenta subir/descargar el usuario
                        sRes += "Debes indicar el nombre del documento." + (char)10 + (char)10;
                        break;
                    case 101:
                        bCAU = false;
                        sRes += "El documento no puede estar vacío." + (char)10 + (char)10;
                        break;
                    case 102:
                        bCAU = false;
                        sRes += "Se ha superado el tamaño de documento máximo permitido (";
                        sRes += System.Configuration.ConfigurationManager.AppSettings["TamMaxContentServer"] + ")" + (char)10 + (char)10;
                        break;
                    case 103://No se ha indicado el id del documento para leerlo del Content-Server
                        bCAU = false;
                        break;
                    default:
                        //sRes += cex.Message;
                        break;
                }
                sRes += "Error: " + cex.ErrorCode.ToString();
            }
            else
            {
                if (cex.InnerException.GetType().Name == "ConservaException")//cex.ErrorCode == 120
                {
                    ConservaException icex = (ConservaException)cex.InnerException;
                    sRes += "Error: " + icex.ErrorCode.ToString();
                    //sRes += icex.Message + "<br /><br />Póngase en contacto con el CAU.";
                }
                else
                {
                    //sRes += cex.InnerException.Message;
                    sRes += "Error: " + cex.ErrorCode.ToString();
                }
            }
            if (bCAU)
                sRes += (char)10 + (char)10 + "Póngase en contacto con el CAU.";

            return sRes;
        }
        /// <summary>
        /// Quita el último caracter de una cadena siempre que coincida con el 2º parámetros
        /// </summary>
        public static string QuitaUltimoCaracter(string sCad, string sCar)
        {
            string sRes = sCad;
            if (sCad != "")
            {
                if (sCar == sCad.Substring(sCad.Length - 1, 1))
                    sRes = sCad.Substring(0, sCad.Length - 1);
            }

            return sRes;
        }
        /// <summary>
        /// Indica si el profesional actual es Aministrador o SuperAdministrador de producción
        /// </summary>
        /// <returns></returns>
        public static bool EsAdminProduccion()
        {
            bool bRes = false;
            if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A" ||
                HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                bRes = true;
            return bRes;
        }
        public static bool EsAdminProduccionEntrada()
        {
            bool bRes = false;
            if (HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "A" ||
                HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "SA")
                bRes = true;
            return bRes;
        }
        public static bool EsSuperAdminProduccion()
        {
            bool bRes = false;
            if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                bRes = true;
            return bRes;
        }
        public static int GetUserActual()
        {
            int idUser = 0;
            if (HttpContext.Current.Session["UsuarioActual"].ToString() == "0")
                idUser = int.Parse(HttpContext.Current.Session["UsuarioActual_CVT"].ToString());
            else
                idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());

            return idUser;
        }
        /// <summary>
        /// Dada una lista de elementos obtiene un DataTable que podemos pasar como parametro a un proc.alm.
        /// </summary>
        /// <param name="aLista"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromListCod(string sLista, string sSeparador)
        {
            string[] sl = Regex.Split(sLista, sSeparador);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CODIGOINT", typeof(int)));

            for (int i = 0; i < sl.Length;i++)
            {
                if (sl[i].ToString() !="")
                { 
                    DataRow row = dt.NewRow();
                    row["CODIGOINT"] = int.Parse(sl[i].ToString());
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        #region Metodos para limpiar de caracteres extranos una cadena
        //http://www.pjb.com.au/comp/diacritics.html
        private static string[,] CharacterReplacements = { 
                { " ", "-"},
                { "&", "-"},
                { "?", "-"},
                { "!", "-"},
                { "%", "-"},
                { "+", "-"},
                { "#", "-"},
                { ":", "-"},
                { ";", "-"},
                { ".", "-"},
 
                { "¢", "c" },   //cent
                { "£", "P" },   //Pound
                { "€", "E" },   //Euro
                { "¥", "Y" },   //Yen
                { "°", "d" },   //degree
                { "¼", "1-4" }, //fraction one-quarter
                { "½", "1-2" }, //fraction half    
                { "¾", "1-3" }, //fraction three-quarters}
                { "@", "AT)"}, //at                                                  
                { "Œ", "OE" },  //OE ligature, French (in ISO-8859-15)        
                { "œ", "oe" },  //OE ligature, French (in ISO-8859-15)        
 
                {"Å","A" },  //ring
                {"Æ","AE"},  //diphthong
                {"Ç","C" },  //cedilla
                {"È","E" },  //grave accent
                {"É","E" },  //acute accent
                {"Ê","E" },  //circumflex accent
                {"Ë","E" },  //umlaut mark
                {"Ì","I" },  //grave accent
                {"Í","I" },  //acute accent
                {"Î","I" },  //circumflex accent
                {"Ï","I" },  //umlaut mark
                {"Ð","Eth"}, //Icelandic
                {"Ñ","N" },  //tilde
                {"Ò","O" },  //grave accent
                {"Ó","O" },  //acute accent
                {"Ô","O" },  //circumflex accent
                {"Õ","O" },  //tilde
                {"Ö","O" },  //umlaut mark
                {"Ø","O" },  //slash
                {"Ù","U" },  //grave accent
                {"Ú","U" },  //acute accent
                {"Û","U" },  //circumflex accent
                {"Ü","U" },  //umlaut mark
                {"Ý","Y" },  //acute accent
                {"Þ","eth"}, //Icelandic - http://en.wikipedia.org/wiki/Thorn_(letter)
                {"ß","ss"},  //German
 
                {"à","a" },  //grave accent
                {"á","a" },  //acute accent
                {"â","a" },  //circumflex accent
                {"ã","a" },  //tilde
                {"ä","ae"},  //umlaut mark
                {"å","a" },  //ring
                {"æ","ae"},  //diphthong
                {"ç","c" },  //cedilla
                {"è","e" },  //grave accent
                {"é","e" },  //acute accent
                {"ê","e" },  //circumflex accent
                {"ë","e" },  //umlaut mark
                {"ì","i" },  //grave accent
                {"í","i" },  //acute accent
                {"î","i" },  //circumflex accent
                {"ï","i" },  //umlaut mark
                {"ð","eth"}, //Icelandic
                {"ñ","n" },  //tilde
                {"ò","o" },  //grave accent
                {"ó","o" },  //acute accent
                {"ô","o" },  //circumflex accent
                {"õ","o" },  //tilde
                {"ö","oe"},  //umlaut mark
                {"ø","o" },  //slash
                {"ù","u" },  //grave accent
                {"ú","u" },  //acute accent
                {"û","u" },  //circumflex accent
                {"ü","ue"},  //umlaut mark
                {"ý","y" },  //acute accent
                {"þ","eth"}, //Icelandic - http://en.wikipedia.org/wiki/Thorn_(letter)
                {"ÿ","y" },  //umlaut mark
                };



        public static string RemoveNonWordChars(string source)
        {
            return RemoveNonWordChars(source, "");
        }
        public static string RemoveNonWordChars(string source, string replacement)
        {
            //\W is any non-word character (not [^a-zA-Z0-9_]).
            Regex regex = new Regex(@"[^a-zA-Z0-9-]+");
            return regex.Replace(source, replacement);
        }
        public static string CleanFileName(string filename)
        {
            string fileEnding = null;
            int index = filename.LastIndexOf(".");

            //removes the file ending.
            if (index != -1)
            {
                fileEnding = filename.Substring(index + 1);
                filename = filename.Substring(0, index);

                //remove based on the CharacterReplacements list
                for (int i = 0; i < CharacterReplacements.GetLength(0); i++)
                {
                    fileEnding = fileEnding.Replace(CharacterReplacements[i, 0], CharacterReplacements[i, 1]);
                }

                //remove everything that is left
                fileEnding = "." + RemoveNonWordChars(fileEnding);
            }

            //remove based on the CharacterReplacements list
            for (int i = 0; i < CharacterReplacements.GetLength(0); i++)
            {
                filename = filename.Replace(CharacterReplacements[i, 0], CharacterReplacements[i, 1]);
            }

            //remove everything that is left
            filename = RemoveNonWordChars(filename);

            return filename + fileEnding;
        }
        #endregion
    }
}
