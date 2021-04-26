using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.DAL;
using SUPER.Capa_Negocio;


namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de Idioma
    /// </summary>
    public partial class Idioma
    {
        #region Propiedades y Atributos

        private int _t020_idcodidioma;
        public int t020_idcodidioma
        {
            get { return _t020_idcodidioma; }
            set { _t020_idcodidioma = value; }
        }

        private string _t021_titulo;
        public string t021_titulo
        {
            get { return _t021_titulo; }
            set { _t021_titulo = value; }
        }

        private string _Profesional;
        public string Profesional
        {
            get { return _Profesional; }
            set { _Profesional = value; }
        }

        private string _NDOC;
        public string NDOC
        {
            get { return _NDOC; }
            set { _NDOC = value; }
        }

        private bool _BDOC;
        public bool BDOC
        {
            get { return _BDOC; }
            set { _BDOC = value; }
        }

        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        #endregion

        #region Constructor

        public Idioma()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #endregion

        #region Metodos

        public static List<ElementoLista> obtenerCboIdioma(int t001_idficepi)
        {
            SqlDataReader dr = DAL.Idioma.obtenerIdioma(t001_idficepi);
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["T020_IDCODIDIOMA"].ToString(), dr["T020_DESCRIPCION"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static List<ElementoLista> obtenerCboNivelIdioma()
        {
            SqlDataReader dr = DAL.Idioma.obtenerNivelIdioma();
            List<ElementoLista> oLista = new List<ElementoLista>();
            string[] strNivel = null;

            if (dr.Read())
            {
                strNivel = Regex.Split(dr["NIVELES"].ToString(), "@#@");
            }
            foreach (string oFun in strNivel)
            {
                string[] aValores = Regex.Split(oFun, "#/#");
                if (aValores[0] != "")
                    oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static string MiCvIdiomas(int idFicepi)
        {
            SqlDataReader dr = DAL.Idioma.MiCvIdiomas(null, idFicepi);
            StringBuilder sb = new StringBuilder();
            int i=0;

            sb.Append(@"<table id='tblDatosIdiomas' style='width:930px;' class='MA' >
                        <colgroup>
                            <col style='width:25px;' />
                            <col style='width:295px;' />
                            <col style='width:30px;' />
                            <col style='width:30px;' />
                            <col style='width:30px;' />
                            <col style='width:430px;' />
                            <col style='width:90px;' />
                        </colgroup>");

            while (dr.Read())
            {
                i++;
                //La propiedad t indica el tipo de fila: I->idioma, T->Título
                sb.Append("<tr est=\"" + dr["ESTADO"].ToString() + "\" id=" + dr["T020_IDCODIDIOMA"].ToString() + "//" + i.ToString() + " t='I' onclick='mmIdioma(event);' ondblclick='mantIdioma(" + idFicepi + "," + dr["T020_IDCODIDIOMA"].ToString() + ")'>");
                //ESTADO me indica si  TIENETITULO
                sb.Append("<td>");
                //switch (dr["ESTADO"].ToString())
                //{
                //    case ("T"): sb.Append("<img src='../../../images/imgPenCumplimentar.png' title='Datos pendientes de cumplimentar' />"); break;
                //    case ("P"): sb.Append("<img src='../../../images/imgPenValidar.png' title='Datos pendientes de validar' />"); break;
                //}
                sb.Append("</td>");
                sb.Append("<td>" + dr["T020_DESCRIPCION"].ToString() + "</td>");
                //sb.Append("<td>" + dr["T013_LECTURA"].ToString() + "</td>");
                //sb.Append("<td>" + dr["T013_ESCRITURA"].ToString() + "</td>");
                //sb.Append("<td>" + dr["T013_ORAL"].ToString() + "</td>");
                sb.Append("<td>" + ((dr["T013_LECTURA"].ToString() == "") ? "" : "<img src='../../../Images/imgNivel" + dr["T013_LECTURA"].ToString() + ".png' />") + "</td>");
                sb.Append("<td>" + ((dr["T013_ESCRITURA"].ToString() == "") ? "" : "<img src='../../../Images/imgNivel" + dr["T013_ESCRITURA"].ToString() + ".png' />") + "</td>");
                sb.Append("<td>" + ((dr["T013_ORAL"].ToString() == "") ? "" : "<img src='../../../Images/imgNivel" + dr["T013_ORAL"].ToString() + ".png' />") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                if (dr["ESTADO"].ToString() != "X")
                {
                    string motivoT = "";

                    SqlDataReader dr2 = DAL.TituloIdiomaFic.Catalogo(idFicepi, int.Parse(dr["T020_IDCODIDIOMA"].ToString()));
                    while (dr2.Read())
                    {
                        i++;
                        sb.Append("<tr title='" + motivoT + "' id='" + dr2["T021_IDTITULOIDIOMA"].ToString() + "//" + i.ToString() + "'");
                        sb.Append(" t='T'  bd='' ndoc='" + dr2["T021_NDOC"].ToString() + "' id2=" + dr["T020_IDCODIDIOMA"].ToString());
                        sb.Append(" ondblclick='AnadirTitulo(" + dr["T020_IDCODIDIOMA"].ToString() + ",\"" + dr2["t839_idestado"].ToString() + "\"," + dr2["T021_IDTITULOIDIOMA"].ToString() + ")' ");
                        sb.Append("est='" + dr2["t839_idestado"].ToString() + "' >");
                        //Doc
                        sb.Append("<td>");
                        sb.Append("</td>");
                        sb.Append("<td colspan='3'></td><td>");
                        switch (dr2["t839_idestado"].ToString())
                        {
                            //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
                            //case ("O"):
                            //case ("P"): sb.Append("<img src='../../../images/imgPenValidar.png' title='Datos pendientes de validar por la organización' />"); break;
                            //case ("R"): sb.Append("<img src='../../../images/imgRechazar.png' title='Este dato es únicamente visible por ti' />"); break;
                            case ("S"):
                            case ("T"): sb.Append("<img src='../../../images/imgPenCumplimentar.png' title='Datos que tienes pendiente de completar, actualizar o modificar' />"); break;
                            case ("B"): sb.Append("<img src='../../../images/imgBorrador.png' title='Datos en borrador' />"); break;
                            case ("X"):
                            case ("Y"):
                                sb.Append("<img src='../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa' />"); break;
                        }    
                        sb.Append("</td>");
                        sb.Append("<td class='MA'>");
                        if (dr2["bDoc"].ToString() == "1")
                        {
                            sb.Append("<img src=\"../../../images/imgTitulo.png\" width='16px' height='16px' ");
                            sb.Append("onclick=\"descargar('TIF', " + dr2["T021_IDTITULOIDIOMA"] + ");\" ");
                            sb.Append("style='cursor:pointer; vertical-align:middle;' title=\"Descargar " + dr2["T021_NDOC"].ToString() + "\">");
                        }
                        else
                        {
                            sb.Append("<img src=\"../../../images/imgSeparador.gif\" width='16px' height='16px' style='vertical-align:middle;' />");
                        }

                        sb.Append("<nobr class='NBR W400' onmouseover='TTip(event)' style='vertical-align:middle;margin-left:3px;' >" + dr2["T021_TITULO"].ToString() + " </nobr></td>");
                        sb.Append("<td>" + dr2["FECHAO"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    dr2.Close();
                    dr2.Dispose();
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return sb.ToString();
        }

        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' style='width:500px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;'/><col style='width:485px;'/></colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.Idioma.CatalogoIdiomas(null);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T020_IDCODIDIOMA"].ToString() + "' class='MANO' bd='N'>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("<td>" + dr["T020_DESCRIPCION"].ToString() + "</td>");
                sb.Append("</tr>");                
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }
        public static SqlDataReader Catalogo(SqlTransaction tr)
        {
            return DAL.Idioma.CatalogoIdiomas(tr);
        }

        public static string Grabar(string strDatos)
        {
            string sDenominacionDelete = "";
            string sAccion = "";
            #region Inicio Transacción

            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion

            try
            {
                int nAux = 0;
                string sElementosInsertados = "";
                string sResul = "";
                string[] aIdioma = Regex.Split(strDatos, "@fila@");
                foreach (string sIdioma in aIdioma)
	            {
		            string [] aDatosIdioma= Regex.Split(sIdioma,"@dato@");
                    sAccion = aDatosIdioma[0];
                    sDenominacionDelete = "";
                    switch (aDatosIdioma[0])
                    {
                        case "I":
                            nAux = DAL.Idioma.Insert(tr, aDatosIdioma[2]);
                            if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                            else sElementosInsertados += "//" + nAux.ToString();
                            break;
                        case "U":
                            DAL.Idioma.Update(tr, Convert.ToInt32(aDatosIdioma[1]), aDatosIdioma[2]);
                            break;
                        case "D":
                            sDenominacionDelete = aDatosIdioma[2];
                            DAL.Idioma.Delete(tr, Convert.ToInt32(aDatosIdioma[1]));
                            break;
                    }
	            }

                Conexion.CommitTransaccion(tr);
                sResul = sElementosInsertados;
                return sResul;
            }

            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (Errores.EsErrorIntegridad(ex) && sAccion == "D")
                {
                    throw new Exception("ErrorControlado##EC##No se puede eliminar el idioma \"" + sDenominacionDelete + "\" por tener elementos relacionados.");
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            
        }

        public static string MiCvIdiomasHTML(int idFicepi, int bFiltros, Nullable<int> t020_idcodidioma, Nullable<int> nivelidioma)
        {
            SqlDataReader dr = DAL.Idioma.MiCvIdiomasHTML(null, idFicepi, bFiltros, t020_idcodidioma, nivelidioma);
            StringBuilder sb = new StringBuilder();
            int codidioma=0;
            int i=0;
            int aux = 0;
            //Para poner en rojo lo que no esté validado
            string sColor = "";

            //if (dr.HasRows)
            //{
            //    sb.Append("<table style='margin-left:40px; margin-top:25px; width:625px;'>");
            //    sb.Append("<tr><td>");
            //    sb.Append("<label id='lblIdiomas' class='titulo1'>Idiomas</label>");
            //    sb.Append("</td></tr>");
            //    sb.Append("</table>");
            //    aux = 1;
            //}
            while (dr.Read())
            {
                //Para poner en rojo lo que no esté validado
                sColor = "";
                if (dr["ESTADO"].ToString() == "1")
                    sColor = "color:Red;";

                if (i == 0 || codidioma != int.Parse(dr["T020_IDCODIDIOMA"].ToString()))
                {
                    codidioma = int.Parse(dr["T020_IDCODIDIOMA"].ToString());
                    sb.Append("<table style='margin-left:60px; margin-top:15px; width:590px;' cellpadding='1' cellspacing='0' border='0'>");
                    sb.Append("<colgroup>");
                    sb.Append(" <col style='width:80px;'/>");
                    sb.Append(" <col style='width:135px;'/>");
                    sb.Append(" <col style='width:80px;'/>");
                    sb.Append(" <col style='width:100px;'/>");
                    sb.Append(" <col style='width:50px;'/>");
                    sb.Append(" <col style='width:145px;'/>");
                    sb.Append("</colgroup>");
                    //Separador (Siempre que haya mas de un idioma)
                    if (i != 0)
                    {
                        sb.Append("<tr><td colspan='6' style='border-bottom: 1px solid #336699;'></td></tr>");
                        sb.Append("<tr style='height:5px;'><td colspan='6'></td></tr>");
                    }
                    //Fila 1
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblDescripcion' class='label W90' style='color:#336699;'>Idioma:</label>");
                    sb.Append("</td><td colspan='5'>");
                    //16/10/2014 Víctor dice que la parte de la cabecera del idioma (que no se valida) a de aparecer siempre en negro
                    //sb.Append("<nobr id='Descripcion' class='NBR W400 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T020_DESCRIPCION"].ToString() + "</nobr>");
                    sb.Append("<nobr id='Descripcion' class='NBR W400 label' onmouseover='TTip(event)' >" + dr["T020_DESCRIPCION"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    //Fila 2
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblNLectura' class='label W90' style='color:#336699;'>Nivel Lectura:</label>");
                    sb.Append("</td><td>");
                    //sb.Append("<label id='NLectura' class='label' style='" + sColor + "'>" + dr["T013_LECTURA"].ToString() + "</label>");
                    sb.Append("<label id='NLectura' class='label'>" + dr["T013_LECTURA"].ToString() + "</label>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<label id='lblNEscritura' class='label W90' style='color:#336699;'>Nivel Escritura:</label>");
                    sb.Append("</td><td>");
                    //sb.Append("<label id='NEscritura' class='label' style='" + sColor + "'>" + dr["T013_ESCRITURA"].ToString() + "</label>");
                    sb.Append("<label id='NEscritura' class='label'>" + dr["T013_ESCRITURA"].ToString() + "</label>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<label id='lblNOral' class='label W90' style='color:#336699;'>Nivel Oral:</label>");
                    sb.Append("</td><td>");
                    //sb.Append("<label id='NOral' class='label' style='" + sColor + " margin-left:10px'>" + dr["T013_ORAL"].ToString() + "</label>");
                    sb.Append("<label id='NOral' class='label' style='margin-left:10px'>" + dr["T013_ORAL"].ToString() + "</label>");
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                }
                string docTit = "";
                if (dr["bDoc"].ToString() == "1")
                {
                    docTit = "<img src=\"../../../images/imgTitulo.png\" width='16px' height='16px' ";
                    docTit += "onclick=\"descargar('TIF', " + dr["T021_IDTITULOIDIOMA"] + ");\" ";
                    docTit += "style='cursor:pointer;' title=\"Descargar titulo del idioma\">";
                }
                if (dr["T021_TITULO"].ToString() != "")
                {
                    sb.Append("<table style='margin-left:80px; margin-top:15px; width:530px;' cellpadding='1' cellspacing='0' border='0'>");
                    sb.Append("<colgroup>");
                    sb.Append(" <col style='width:80px;'/>");
                    sb.Append(" <col style='width:250px;'/>");
                    sb.Append(" <col style='width:80px;'/>");
                    sb.Append(" <col style='width:120px;'/>");
                    sb.Append("</colgroup>");
                    sb.Append("<tr><td colspan='4' style='border-bottom: 1px dotted #336699;'></td></tr>");
                      sb.Append("<tr><td>");
                    sb.Append("<label id='lblTitulo' class='label W90' style='color:#336699;'>Titulo:</label>");
                    sb.Append("</td><td colspan='3'>");
                    sb.Append(docTit + "<nobr id='Titulo' class='NBR W320 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:" + ((docTit != "") ? "4" : "0") + "px;'>" + dr["T021_TITULO"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblCentro' class='label W90' style='color:#336699;'>Centro:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='Centro' class='label' style='" + sColor + "'>" + dr["T021_CENTRO"].ToString() + "</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='lblFObtencion' class='label W90' style='color:#336699;'>Fecha Obtención:</label>");
                    sb.Append("</td><td>");

                    string fobtencion = "";
                    if (dr["T021_FECHA"] != DBNull.Value)
                        fobtencion = dr["T021_FECHA"].ToString();

                    sb.Append("<label id='FObtencion' class='label' style='"+sColor+" margin-left:20px'>" + fobtencion + "</label>");
                    sb.Append("</td></tr>");

                    sb.Append("</table>");
                }
                i++;

            }
            if (aux == 1)
            {
                sb.Append("<table style='margin-left:60px; margin-top:15px; width:590px;'>");
                sb.Append("<tr><td colspan='6' style='border-bottom: 1px solid #336699;'></td></tr>");
                sb.Append("</table>");
            }
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// Obtiene una lista de los TITULOS DE IDIOMA cuyo código de idioma se pasa en sListaIds + 
        /// los TITULOS DE IDIOMA cuya denominación está en sListaDens y existe algun profesional de slFicepis que lo tiene
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="sListaIds"></param>
        /// <param name="sListaDens"></param>
        /// <returns></returns>
        public static List<Idioma> GetListaPorProfesional(string slFicepis, string sListaIds, string sListaDens)
        {
            List<Idioma> oLista = new List<Idioma>();
            Idioma oElem;
            SqlDataReader dr =
                SUPER.DAL.Idioma.GetListaPorProfesional(null, slFicepis.Replace(",", "##"), sListaIds, sListaDens.Replace(";", "##"));
            while (dr.Read())
            {
                oElem = new Idioma();
                oElem.t020_idcodidioma = int.Parse(dr["t020_idcodidioma"].ToString());
                oElem.t021_titulo = dr["t021_titulo"].ToString();
                oLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return oLista;
        }
        /// <summary>
        /// Obtiene una lista con los datos de los titulos de idiomas de los profesionales que se pasan como parametros
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="slDenominaciones"></param>
        /// <returns></returns>
        public static List<Idioma> GetDocsExportacion(string slFicepis, string slDenominaciones)
        {
            List<Idioma> oLista = new List<Idioma>();
            Idioma oElem;
            SqlDataReader dr = SUPER.DAL.Idioma.GetDocsExportacion(null, slFicepis.Replace(",", "##"), slDenominaciones.Replace(",", "##"));
            while (dr.Read())
            {
                oElem = new Idioma();
                //oElem.IdFicepiCert = int.Parse(dr["t001_idficepi"].ToString());
                oElem.t020_idcodidioma = short.Parse(dr["t020_idcodidioma"].ToString());
                oElem.Profesional = dr["Profesional"].ToString();
                oElem.t021_titulo = dr["t021_titulo"].ToString();
                oElem.NDOC = dr["T021_NDOC"].ToString();
                if (dr["t2_iddocumento"].ToString() != "")
                {
                    oElem.t2_iddocumento = long.Parse(dr["t2_iddocumento"].ToString());
                    //No cargo aquí el contenido porque me puedo quedar sin memoria
                    //oElem.T593_DOC = IB.Conserva.ConservaHelper.ObtenerDocumento((long)dr["t2_iddocumento"]).content;
                    oLista.Add(oElem);
                }
                //Marco a false porque luego en función de si se puede recuperar el documento lo pondré a true
                oElem.BDOC = false;
            }
            dr.Close();
            dr.Dispose();

            return oLista;
        }
        #endregion
    }
}