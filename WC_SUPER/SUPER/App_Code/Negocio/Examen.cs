using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.DAL;
using System.Text.RegularExpressions;
using System.Text;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{/// <summary>
    /// Descripción breve de Curvit
    /// </summary>
    public class Examen
    {
        #region Constructor
        public Examen()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion

        #region Propiedades

        private int _T583_IDEXAMEN;
        public int T583_IDEXAMEN
        {
            get { return _T583_IDEXAMEN; }
            set { _T583_IDEXAMEN = value; }
        }

        private string _T583_NOMBRE;
        public string T583_NOMBRE
        {
            get { return _T583_NOMBRE; }
            set { _T583_NOMBRE = value; }
        }

        private string _T583_CODIGO;
        public string T583_CODIGO
        {
            get { return _T583_CODIGO; }
            set { _T583_CODIGO = value; }
        }

        private bool _T583_VIGENTE;
        public bool T583_VIGENTE
        {
            get { return _T583_VIGENTE; }
            set { _T583_VIGENTE = value; }
        }

        //private string _T583_DURACION;
        //public string T583_DURACION
        //{
        //    get { return _T583_DURACION; }
        //    set { _T583_DURACION = value; }
        //}

        //private string _T583_OBSERVA;
        //public string T583_OBSERVA
        //{
        //    get { return _T583_OBSERVA; }
        //    set { _T583_OBSERVA = value; }
        //}

        //private int _T036_IDCODENTORNO;
        //public int T036_IDCODENTORNO
        //{
        //    get { return _T036_IDCODENTORNO; }
        //    set { _T036_IDCODENTORNO = value; }
        //}

        private string _T036_DESCRIPCION;
        public string T036_DESCRIPCION
        {
            get { return _T036_DESCRIPCION; }
            set { _T036_DESCRIPCION = value; }
        }

        //private int _T576_IDCRITERIO;
        //public int T576_IDCRITERIO
        //{
        //    get { return _T576_IDCRITERIO; }
        //    set { _T576_IDCRITERIO = value; }
        //}

        //private string _ORIGENDESC;
        //public string ORIGENDESC
        //{
        //    get { return _ORIGENDESC; }
        //    set { _ORIGENDESC = value; }
        //}

        private string _T576_NOMBRE;
        public string T576_NOMBRE
        {
            get { return _T576_NOMBRE; }
            set { _T576_NOMBRE = value; }
        }


        private DateTime? _FOBTENCION;
        public DateTime? FOBTENCION
        {
            get { return _FOBTENCION; }
            set { _FOBTENCION = value; }
        }

        private DateTime? _FCADUCIDAD;
        public DateTime? FCADUCIDAD
        {
            get { return _FCADUCIDAD; }
            set { _FCADUCIDAD = value; }
        }

        private byte[] _T591_DOC;
        public byte[] T591_DOC
        {
            get { return _T591_DOC; }
            set { _T591_DOC = value; }
        }

        private string _T591_NDOC;
        public string T591_NDOC
        {
            get { return _T591_NDOC; }
            set { _T591_NDOC = value; }
        }

        private int _T001_IDFICEPI;
        public int T001_IDFICEPI
        {
            get { return _T001_IDFICEPI; }
            set { _T001_IDFICEPI = value; }
        }

        private int _BDOC;
        public int BDOC
        {
            get { return _BDOC; }
            set { _BDOC = value; }
        }

        private char _T839_IDESTADO;
        public char T839_IDESTADO
        {
            get { return _T839_IDESTADO; }
            set { _T839_IDESTADO = value; }
        }

        private string _T595_MOTIVORT;
        public string T595_MOTIVORT
        {
            get { return _T595_MOTIVORT; }
            set { _T595_MOTIVORT = value; }
        }

        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }
        #endregion

        #region Metodos
        
        public static Examen SelectDoc(SqlTransaction tr, int idExamen, int idficepi)
        {

            SqlDataReader dr = SUPER.DAL.Examen.SelectDoc(tr, idExamen, idficepi);
            Examen o = new Examen();
            if (dr.Read())
            {
                //if (dr["T591_DOC"] != DBNull.Value)
                //    o.T591_DOC = (byte[])dr["T591_DOC"];
                if (dr["T591_NDOC"] != DBNull.Value)
                    o.T591_NDOC = dr["T591_NDOC"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato"));
            }

            dr.Close();
            dr.Dispose();

            return o;


        }

        public static string GetNombre(SqlTransaction tr, int t583_idexamen)
        {
            SqlDataReader dr = SUPER.DAL.Examen.Datos(tr, t583_idexamen);
            string sDenominacion = "";
            if (dr.Read())
            {
                sDenominacion = dr["T583_NOMBRE"].ToString();
            }
            dr.Close();
            dr.Dispose();
            return sDenominacion;


        }
        //Obtengo los certificado a los que pertenece un exámen. Las válidas + las propias
        public static string GetCertificados(SqlTransaction tr, int t583_idexamen, int t001_idficepi)
        {
            SqlDataReader dr = SUPER.DAL.Examen.GetCertificados(tr, t583_idexamen, t001_idficepi);
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<table id='tblDatosExamen' style='width:640px; text-align:left;'>
                        <colgroup>
                            <col style='width:320px;'/>
                            <col style='width:150px;' />
                            <col style='width:150px;' />
                            <col style='width:20px;' />
                        </colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;'>");

                if (dr["idFicepiCertificado"].ToString() != "")//El profesional tiene el certificado
                {
                    sb.Append("<td><nobr class='NBR W320' onmouseover='TTip(event)' >" + dr["t582_nombre"].ToString() + "</nobr></td>");
                }
                else
                    sb.Append("<td><nobr class='NBR W320' onmouseover='TTip(event)' style='color:Red;' >" + dr["t582_nombre"].ToString() + "</nobr></td>");

                
                sb.Append("<td><nobr class='NBR W150' onmouseover='TTip(event)'>" + dr["t576_nombre"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W150' onmouseover='TTip(event)'>" + dr["t036_descripcion"].ToString() + "</nobr></td>");
                sb.Append("<td><img style='cursor:pointer;' src='../../../images/imgCatalogo.png' onclick='getVias(" + dr["t582_idcertificado"].ToString() + "," + t001_idficepi.ToString() + ");' title='Visualiza las vías del certificado' /></td></tr>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }
        //Obtengo los exámenes de un profesional
        public static string MiCVFormacionExam(int idFicepi)//, bool esEncargado)
        {
            SqlDataReader dr = SUPER.DAL.Examen.MiCVFormacionExam(null, idFicepi);
            StringBuilder sb = new StringBuilder();
            string sOri = "";
            sb.Append(@"<table id='tblDatosExamen' style='width:930px;' class='MANO'>
                        <colgroup>
                            <col style='width:30px;' />
                            <col style='width:500px;'/>
                            <col style='width:70px;' />
                            <col style='width:70px;' />
                            <col style='width:20px;' />
                            <col style='width:220px;' />
                            <col style='width:20px;' />
                        </colgroup>");
            while (dr.Read())
            {
                if (dr["origenCVT"] != DBNull.Value && (bool)dr["origenCVT"])
                    sOri = "1";
                else
                    sOri = "0";
                sb.Append("<tr id=" + dr["t583_idexamen"].ToString() + " est='" + dr["EstadoExamen"].ToString() + "'" +
                          " ori='" + sOri + "' f='" + idFicepi.ToString() + 
                          "' onclick='ms(this);' style='height:20px;'>");
                #region Celda Estado
                sb.Append("<td>");
                if (dr["t591_fPeticionBorrado"].ToString() != "")
                    sb.Append("<img src='../../../images/imgPetBorrado.png' title='Pdte de eliminar' />");
                else
                {
                    switch (dr["EstadoExamen"].ToString())
                    {
                        //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
                        //case ("O"):
                        //case ("P"):
                        //    sb.Append("<img src='../../../images/imgPenValidar.png' title='Datos pendientes de validar por la organización (Documento)' />");
                        //    break;
                        //case ("R"): sb.Append("<img src='../../../images/imgRechazar.png' title='Este dato es únicamente visible por ti (Documento)' />");
                        //    break;
                        case ("S"):
                        case ("T"):
                            sb.Append("<img src='../../../images/imgPenCumplimentar.png' title='Datos pendientes de cumplimentar (Documento)' />");
                            break;
                        case ("X"):
                        case ("Y"):
                            sb.Append("<img src='../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa (Documento)' />");
                            break;
                        case ("B"):
                            sb.Append("<img src='../../../images/imgBorrador.png' title='Datos en borrador' />");
                            break;
                        //default:
                        //    break;
                    }
                }
                sb.Append("</td>");
                #endregion
                sb.Append("<td><nobr class='NBR W500' onmouseover='TTip(event)' >" + dr["T583_NOMBRE"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((dr["FOBTENCION"].ToString() == "") ? "" : DateTime.Parse(dr["FOBTENCION"].ToString()).ToShortDateString()) + "</td>");
                sb.Append("<td>" + ((dr["FCADUCIDAD"].ToString() == "") ? "" : DateTime.Parse(dr["FCADUCIDAD"].ToString()).ToShortDateString()) + "</td>");
                sb.Append("<td>");
                if (dr["bDocExamen"].ToString() != "0")
                {//Tiene documento asociado al examen
                    //sb.Append("<img style='cursor:pointer;' src='../../../images/imgCertificado.png' onclick='verDOC(\"CVTCERT\"," + dr["t583_idexamen"].ToString() + "," + idFicepi + ");' title='Descargar examen' />");
                    sb.Append("<img style='cursor:pointer;' src='../../../images/imgCertificado.png' onclick='verDOC(\"CVTEXAMEN\"," + dr["t583_idexamen"].ToString() + "," + idFicepi + ");' title='Descargar examen' />");
                }
                sb.Append("</td>");
                sb.Append("<td><nobr class='NBR W220' onmouseover='TTip(event)'>" + dr["t591_ndoc"].ToString() + "</nobr></td>");
                sb.Append("<td><img style='cursor:pointer;' src='../../../images/imgCatalogo.png' onclick='verCert(" + dr["t583_idexamen"].ToString() + "," + idFicepi + ");' title='Visualiza certificados que contienen el examen' /></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }
        
        public static string CatalogoFicepi(int idCertificado, int t001_idficepi)
        {
            string sFecObt = "", sFecCad = "";
            StringBuilder sb = new StringBuilder();
            string sOrigenCVT = "0";
            sb.Append(@"<table id='tblDatosExamen' style='width:440px;' class='MA'>
                        <colgroup>
                        <col style='width:20px;' />
                        <col style='width:145px;' />
                        <col style='width:65px;' />
                        <col style='width:65px;' />
                        <col style='width:20px;' />
                        <col style='width:135px;' />
                        </colgroup><tbody>");
            if (t001_idficepi != -1 && idCertificado != -1)
            {
                SqlDataReader dr = SUPER.DAL.Examen.CatalogoFicepi(null, idCertificado, t001_idficepi);
                while (dr.Read())
                {
                    #region Llenar filas
                    if ((bool)dr["t591_origenCVT"])
                        sOrigenCVT = "1";
                    sFecObt = dr["FOBTENCION"].ToString();
                    sFecCad = dr["FCADUCIDAD"].ToString();
                    if (sFecObt != "")
                        sFecObt = sFecObt.Substring(0, 10);
                    if (sFecCad != "")
                        sFecCad = sFecCad.Substring(0, 10);
                    sb.Append("<tr id='" + dr["T583_IDEXAMEN"].ToString() + "' idInicial='" + dr["T583_IDEXAMEN"].ToString() + "' bd=''"
                        + " estado='" + dr["T839_IDESTADO"].ToString()
                        + "' fo='" + sFecObt + "' fc='" + sFecCad + "' bDoc='" + dr["BDOC"].ToString()
                        + "' t2id='" + dr["t2_iddocumento"].ToString()
                        + "' nDocIni='" + Utilidades.escape(dr["T591_NDOC"].ToString())
                        + "' ori='" + sOrigenCVT
                        + "' nDoc='" + dr["T591_NDOC"].ToString() + "' cambioDoc='0' url=''"
                        + " motivo='" + Utilidades.escape(dr["T595_MOTIVORT"].ToString())
                        + "' style='height:18px' onclick='ms(this);' ondblclick=\"abrirDetalleExamen(this);\">");
                    sb.Append("<td>");
                    if (dr["t591_fPeticionBorrado"].ToString() != "")
                        sb.Append("<img src='../../../../images/imgPetBorrado.png' title='Pdte de eliminar' />");
                    else
                    {
                        switch (dr["T839_IDESTADO"].ToString())
                        {
                            //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
                            //case ("O"):
                            //case ("P"): sb.Append("<img src='../../../../images/imgPenValidar.png' title='Datos pendientes de validar por la organización' />"); break;
                            //case ("R"): sb.Append("<img src='../../../images/imgRechazar.png' title='Este dato es únicamente visible por ti' />"); break;
                            case ("O"):
                            case ("P"):
                            case ("R"):
                                sb.Append("<img src='../../../../images/imgFN.gif' title='Datos pendientes de validar por la organización' />"); break;
                            case ("S"):
                            case ("T"): sb.Append("<img src='../../../../images/imgPenCumplimentar.png' title='Datos que tienes pendiente de completar, actualizar o modificar' />"); break;
                            case ("B"): sb.Append("<img src='../../../../images/imgBorrador.png' title='Datos en borrador' />"); break;
                            case ("X"):
                            case ("Y"):
                                sb.Append("<img src='../../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa' />"); break;
                        }
                    }
                    sb.Append("</td>");


                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W140' onmouseover='TTip(event)'>" + dr["T583_NOMBRE"].ToString() + "</nobr></td>");
                    sb.Append("<td>" + sFecObt + "</td>");
                    sb.Append("<td>" + sFecCad + "</td>");
                    sb.Append("<td>");
                    if (dr["BDOC"].ToString() != "0")
                    {
                        sb.Append("<img style='cursor:pointer;' src='../../../../images/imgCertificado.png' onclick='verDOCExam(\"CVTEXAMEN\"," + dr["T583_IDEXAMEN"].ToString() + "," + t001_idficepi + ");' title='Descargar examen' />");
                    }
                    sb.Append("</td>");

                    sb.Append("<td style=' padding-left:3px;'><nobr class='NBR W130' onmouseover='TTip(event)'>" + dr["T591_NDOC"].ToString() + "</nobr></td>");
                    #endregion
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }

        public static string MiCVFormacionCertExamHTML(int idFicepi, int bFiltros, Nullable<int> t582_idcertificado, string t582_nombre, string lft036_idcodentorno, Nullable<int> origenConsulta)
        {
            SqlDataReader dr = SUPER.DAL.Examen.MiCVFormacionCertExamHTML(null, idFicepi, bFiltros, t582_idcertificado, t582_nombre, lft036_idcodentorno, origenConsulta);
            StringBuilder sb = new StringBuilder();
            //Para poner en rojo lo que no esté validado
            string sColor = "";

            //if (dr.HasRows)
            //{
            //    sb.Append("<table style='margin-left:40px; margin-top:25px; width:625px;'>");
            //    sb.Append("<tr><td>");
            //    sb.Append("<label id='lblCertExam' class='titulo1'>Certificados/Examenes</label>");
            //    sb.Append("</td></tr>");
            //    sb.Append("</table>");
            //}
            while (dr.Read())
            {
                string docExa = "";
                string docCert = "";

                if (dr["bExamen"].ToString() != "0")
                {
                    docExa = "<img style='cursor:pointer;' src='../../../images/imgExpediente.png' onclick='descargar(\"CVTEXAMEN\",\"" + dr["ID"].ToString() + "datos" + idFicepi.ToString() + "\");' title='Descargar examen' />";
                }

                if (dr["bCertificado"].ToString() != "0")
                {
                    docCert = "<img style='cursor:pointer;' src='../../../images/imgCertificado.png' onclick='descargar(\"CVTCERT\",\"" + dr["ID"].ToString() + "datos" + idFicepi.ToString() + "\");' title='Descargar certificado' />";
                }

                //Para poner en rojo lo que no esté validado
                sColor = "";
                if (dr["ESTADO"].ToString() == "1")
                    sColor = "color:Red;";

                sb.Append("<table style='width:590px; margin-left:60px; margin-top:15px;' cellpadding='1' cellspacing='0' border='0'>");
                sb.Append("<colgroup>");
                sb.Append(" <col style='width:100px;'/>");
                sb.Append(" <col style='width:115px;'/>");
                sb.Append(" <col style='width:40px;'/>");
                sb.Append(" <col style='width:140px;'/>");
                sb.Append(" <col style='width:60px;'/>");
                sb.Append(" <col style='width:135px;'/>");
                sb.Append("</colgroup>");
                //Fila 1
                sb.Append("<tr><td>");
                sb.Append("<label id='lblDenominacion' class='label' style='color:#336699;'>Denominación:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append(docExa + docCert + "<nobr id='Denominacion' class='NBR W400 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:" + ((docExa + docCert != "") ? "4" : "0") + "px;'>" + dr["TITULO"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 2
                sb.Append("<tr><td>");
                sb.Append("<label id='lblEntCertificadora' class='label' style='color:#336699;'>Ent. Certificadora:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='EntCertificadora' class='NBR W400 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["PROVEEDOR"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblEntorno' class='label' style='color:#336699;' title='Entorno tecnológico'>Ent.Tec\\Fun.:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='Entorno' class='NBR W400 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["CODIGO"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 2
                sb.Append("<tr><td>");
                sb.Append("<label id='lblFObtencion' class='label' style='color:#336699;'>Fecha Obtención:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='FObtencion' class='label' style='" + sColor + "'>" + dr["FOBTENCION"].ToString() + "</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='lblFCaducidad' class='label W140' style='color:#336699;'>Fecha Caducidad:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='FCaducidad' style='"+sColor+" margin-left:50px;' class='label W50'>" + dr["FCADUCIDAD"].ToString() + "</label>");
                sb.Append("</td><td>");
                string tipo = "";
                if (dr["CASO"].ToString() != "3")
                    tipo = "Examen";
                else
                    tipo = "Certificado";

                sb.Append("<label id='lblTipo' class='label' style='color:#336699;'>Tipo:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='Tipo' class='label' style='" + sColor + "'>" + tipo + "</label>");
                sb.Append("</td>");
                sb.Append("</tr>");
                //Separador
                sb.Append("<tr><td colspan='6' class='W390' style='border-bottom: 1px solid #336699;'></td></tr>");
                sb.Append("</table>");
            }

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static List<ElementoLista> obtenerEntCert(int tipo, int activo)
        {
            SqlDataReader dr = SUPER.DAL.Examen.obtenerEntCert(tipo, activo);
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["IDCRITERIO"].ToString(), dr["NOMBRE"].ToString()));
            }
            return oLista;
        }
        /// <summary>
        /// Es igual que obtenerEntCert pero añade un elemento inicial vacío con código -1
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public static List<ElementoLista> obtenerEntCert2(int tipo, int activo)
        {
            SqlDataReader dr = SUPER.DAL.Examen.obtenerEntCert(tipo, activo);
            List<ElementoLista> oLista = new List<ElementoLista>();
            oLista.Add(new ElementoLista("-1", ""));
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["IDCRITERIO"].ToString(), dr["NOMBRE"].ToString()));
            }

            return oLista;
        }

        //public static void ExamenInsert(string nombre, byte[] doc, string ndoc, Nullable<int> t001_idficepi, Nullable<int> idExamen, Nullable<DateTime> fechaO, Nullable<DateTime> fechaC, string t839_idestado, string t596_motivort, Nullable<int> t582_idcertificado, Nullable<int> t840_idcvcertificadoficepi, int t001_idficepiu)
        //{
        //    SUPER.DAL.Examen.ExamenInsert(null,
        //                                      nombre,
        //                                      doc,
        //                                      ndoc,
        //                                      t001_idficepi,
        //                                      idExamen,
        //                                      fechaO,
        //                                      fechaC,
        //                                      t839_idestado,
        //                                      t596_motivort,
        //                                      t582_idcertificado,
        //                                      t840_idcvcertificadoficepi,
        //                                      t001_idficepiu);
        //}

        //public static void CVExamenUpdate(int idcvexamenficepi, string nombre, byte[] doc, string ndoc, Nullable<int> t001_idficepi, Nullable<int> idExamen, Nullable<DateTime> fechaO, Nullable<DateTime> fechaC, string t839_idestado, string t839_idestado_ini, string t596_motivort, Nullable<int> t582_idcertificado, Nullable<int> t840_idcvcertificadoficepi, int t001_idficepiu, bool cambioDoc)
        //{
        //    SUPER.DAL.Examen.ExamenUpdate(null,
        //                                      idcvexamenficepi,
        //                                      nombre,
        //                                      doc,
        //                                      ndoc,
        //                                      t001_idficepi,
        //                                      idExamen,
        //                                      fechaO,
        //                                      fechaC,
        //                                      t839_idestado,
        //                                      t839_idestado_ini,
        //                                      t596_motivort,
        //                                      t582_idcertificado,
        //                                      t840_idcvcertificadoficepi,
        //                                      t001_idficepiu,
        //                                      cambioDoc);
        //}

        public static string BorrarAsistente(string sAccion, int t001_idficepi, int idExamen)
        {
            string sRes = "OK@#@";
            try
            {
                #region Inicio Transacción
                SqlConnection oConn;
                SqlTransaction tr;
                try
                {
                    oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                    tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
                }
                catch (Exception ex)
                {
                    throw (new Exception("Error al abrir la conexion", ex));
                }

                #endregion
                try
                {
                    //switch (sAccion)
                    //{
                    //    case "BORR_EXAM":
                    //        SUPER.DAL.Examen.DeleteAsistente(tr, idExamen, t001_idficepi);
                    //        //Además hay que borrar del maestro de exámenes si no es un examen validado por RRHH (t583_valido=0)
                    //        SUPER.DAL.Examen.DeleteNoValido(tr, idExamen);
                    //        break;
                    //    case "BORR_TODO":

                    //        break;
                    //}                    
                    SUPER.DAL.Examen.DeleteAsistente(tr, idExamen, t001_idficepi);
                    //Borrar el examen de la vía, sino es una vía validada por RRHH
                    SUPER.DAL.Examen.BorrarVia(tr, idExamen, t001_idficepi);
                    //Además hay que borrar del maestro de exámenes si no es un examen validado por RRHH (t583_valido=0)
                    SUPER.DAL.Examen.DeleteNoValido(tr, idExamen);
                    
                    SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                    throw ex;
                }
                finally
                {
                    SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                }
            }
            catch (Exception ex)
            {
                sRes = "ERROR@#@" + ex.Message;
            }
            return sRes;
        }

        public static string BorrarAsistentes(int t001_idficepi, string sExamenes, int IdficepiEntrada)
        {
            string sRes = "OK@#@";
            int idExamen = -1;
            try
            {
                SqlConnection oConn;
                SqlTransaction tr;
                string[] aReg = Regex.Split(sExamenes, "##");
                #region Borrar examenes
                #region Inicio Transacción
                try
                {
                    oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                    tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
                }
                catch (Exception ex)
                {
                    throw (new Exception("Error al abrir la conexion", ex));
                }

                #endregion
                try
                {
                    foreach (string oReg in aReg)
                    {
                        if (oReg == "") continue;
                        idExamen = int.Parse(oReg);
                        SUPER.DAL.Examen.DeleteAsistente(tr, idExamen, t001_idficepi);
                        //Borrar el examen de la vía, sino es una vía validada por RRHH
                        SUPER.DAL.Examen.BorrarVia(tr, idExamen, t001_idficepi);
                        //Además hay que borrar del maestro de exámenes si no es un examen validado por RRHH (t583_valido=0)
                        SUPER.DAL.Examen.DeleteNoValido(tr, idExamen);
                    }
                    if (t001_idficepi == IdficepiEntrada)
                        SUPER.DAL.Curriculum.ActualizadoCV(tr, t001_idficepi);

                    SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                    throw ex;
                }
                finally
                {
                    SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                }
                #endregion
            }
            catch (Exception ex)
            {
                sRes = "ERROR@#@" + ex.Message;
            }
            return sRes;
        }

        public static void PedirBorrado(int t001_idficepi, int idExamen, int t001_idficepi_petbor, string sMotivo, string sDatosCorreo)
        {
            //string sRes = "OK@#@";
            //try
            //{
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }

            #endregion
            try
            {
                SUPER.DAL.Examen.PedirBorrado(null, idExamen, t001_idficepi, t001_idficepi_petbor, sMotivo);
                SUPER.Capa_Negocio.Correo.EnviarPetBorrado("EX", sDatosCorreo, sMotivo);
                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            //}
            //catch (Exception ex)
            //{
            //    sRes = "ERROR@#@" + ex.Message;
            //}
            //return sRes;
        }
        /*public static bool ExisteEnOtroCertificado(SqlTransaction tr, int t001_idficepi, int idCertificado, int idExamen)
        {

            return SUPER.DAL.Examen.ExisteEnOtroCertificado(tr, t001_idficepi, idCertificado, idExamen);
        }
        */

        /// <summary>
        ///Inserta un registro en T591_FICEPIEXAMEN
        ///Si el examen es nuevo lo inserta en T583_EXAMEN
        ///Si hay cambio de examen
        ///      Borra el examen viejo de las vias (T585_EXAMENCERT) si es un examen no válido (t583_valido=0) o vía no válida (t001_idficepi!=null)
        ///      Borra el examen viejo de T591_FICEPIEXAMEN
        ///      Borra el examen viejo de T583_EXAMEN si es un examen no válido (t583_valido=0)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="idExamenOld"></param>
        /// <param name="idExamenNew"></param>
        /// <param name="nombre"></param>
        /// <param name="doc"></param>
        /// <param name="ndoc"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="fechaO"></param>
        /// <param name="fechaC"></param>
        /// <param name="t839_idestado"></param>
        /// <param name="t595_motivort"></param>
        /// <param name="t001_idficepiu"></param>
        /// <param name="bCambioDoc"></param>
        /// <param name="t583_valido"></param>
        /// <param name="t591_origenCVT"></param>
        /// <returns></returns>
        //public static int InsertarProfesional(SqlTransaction tr, int idExamenOld, int idExamenNew, string nombre, byte[] doc, string ndoc,
        //                                       int t001_idficepi, Nullable<DateTime> fechaO, Nullable<DateTime> fechaC, char t839_idestado,
        //                                       bool bCambioDoc, bool t583_valido, bool t591_origenCVT,
        //                                       Nullable<long> t2_iddocumento, string sNomExamenInicial)
        //{
        //    int iRes = idExamenOld;
        //    #region Asociación del examen al profesional
        //    if (idExamenNew == -1)
        //    {   //Inserto en EXAMEN el nuevo examen
        //        idExamenNew = SUPER.DAL.Examen.Insertar(tr, nombre, t583_valido);
        //        iRes = idExamenNew;
        //    }
        //    else
        //        iRes = idExamenNew;
        //    #region Obtengo el contenido del archivo y lo guardo en ATENEA
        //    if (bCambioDoc && ndoc.Trim() != "")
        //    {
        //        //Si he seleccionado un archivo, cargo el archivo en el ContenteServer y obtengo su identificador
        //        if (t2_iddocumento != null)
        //        {
        //            if (sNomExamenInicial == ndoc)
        //            {//Si el nombre del nuevo archivo es el mismo que el inicial
        //                IB.Conserva.ConservaHelper.ActualizarContenidoDocumento((long)t2_iddocumento, doc);
        //            }
        //            else
        //            {//El archivo a cargar es dierente
        //                IB.Conserva.ConservaHelper.ActualizarDocumento((long)t2_iddocumento, doc, ndoc);
        //            }
        //        }
        //        else
        //            t2_iddocumento = IB.Conserva.ConservaHelper.SubirDocumento(ndoc, doc);
        //    }
        //    #endregion

        //    if (idExamenOld == -1)//Inserto en FICEPIEXAMEN el nuevo examen
        //        SUPER.DAL.Examen.InsertarProf(tr, t001_idficepi, idExamenNew, nombre, fechaO, fechaC, t839_idestado, t591_origenCVT);
        //    else
        //    {
        //        if (idExamenNew == idExamenOld)
        //        {   //Update FICEPIEXAMEN
        //            SUPER.DAL.Examen.ModificarProf(tr, t001_idficepi, idExamenNew, fechaO, fechaC, t839_idestado);
        //        }
        //        else
        //        {   //Borra el examen de las vías no validadas
        //            SUPER.DAL.Examen.BorrarVia(tr, idExamenOld, t001_idficepi);
        //            //Inserto en FICEPIEXAMEN el nuevo examen
        //            SUPER.DAL.Examen.InsertarProf(tr, t001_idficepi, idExamenNew, nombre, fechaO, fechaC, t839_idestado, t591_origenCVT);
        //            //Borro de FICEPIEXAMEN el examen viejo
        //            SUPER.DAL.Examen.DeleteAsistente(tr, idExamenOld, t001_idficepi);
        //            //Si el examen viejo era NO VALIDO, se borra el examen
        //            SUPER.DAL.Examen.DeleteNoValido(tr, idExamenOld);
        //        }
        //    }
        //    #endregion

        //    return iRes;
        //}

        /// <summary>
        /// Asocia un identificador de documento en Atenea al examen de un profesional
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t583_idexamen"></param>
        /// <param name="t2_iddocumento"></param>
        public static void PonerDocumento(SqlTransaction tr, int t001_idficepi, int t583_idexamen, long t2_iddocumento, string t591_ndoc)
        {
            SUPER.DAL.Examen.PonerDocumento(tr, t001_idficepi, t583_idexamen, t2_iddocumento, t591_ndoc);
        }
        /// <summary>
        ///Asocia un examne a un profresional. Inserta un registro en T591_FICEPIEXAMEN
        ///Si hay cambio de examen
        ///      Borra el examen viejo de las vias (T585_EXAMENCERT) si es un examen no válido (t583_valido=0) o vía no válida (t001_idficepi!=null)
        ///      Borra el examen viejo de T591_FICEPIEXAMEN
        ///      Borra el examen viejo de T583_EXAMEN si es un examen no válido (t583_valido=0)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="idExamenOld"></param>
        /// <param name="idExamenNew"></param>
        /// <param name="nombre"></param>
        /// <param name="doc"></param>
        /// <param name="ndoc"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="fechaO"></param>
        /// <param name="fechaC"></param>
        /// <param name="t839_idestado"></param>
        /// <param name="t595_motivort"></param>
        /// <param name="t001_idficepiu"></param>
        /// <param name="bCambioDoc"></param>
        /// <param name="t583_valido"></param>
        /// <param name="t591_origenCVT"></param>
        /// <returns></returns>
        public static int AsignarProfesional(SqlTransaction tr, int t001_idficepi, int idExamenOld, int idExamenNew,  
                                             string ndoc, string sUsuTick, Nullable<DateTime> fechaO, Nullable<DateTime> fechaC, 
                                             char t839_idestado, bool t591_origenCVT, string sMotivo)
        {
            int iRes = idExamenNew;
            long? idDocAtenea = null;
            if (sUsuTick.Trim() != "")//Recupero el idDocumento de la tabla Temporal
            {
                SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(tr, sUsuTick);
                idDocAtenea = oDoc.t2_iddocumento;
            }
            
            if (idExamenOld == -1)//Inserto en FICEPIEXAMEN el nuevo examen
            {
                SUPER.DAL.Examen.InsertarProf(tr, t001_idficepi, idExamenNew, ndoc, fechaO, fechaC, t839_idestado,
                                              t591_origenCVT, idDocAtenea, sMotivo);
            }
            else
            {
                if (idExamenNew == idExamenOld)
                {   //Update FICEPIEXAMEN
                    SUPER.DAL.Examen.ModificarProf(tr, t001_idficepi, idExamenNew, fechaO, fechaC, t839_idestado);
                    if (idDocAtenea != null)
                    {//Ha habido cambio de documento
                        SUPER.DAL.Examen.PonerDocumento(tr, t001_idficepi, idExamenNew, (long)idDocAtenea, ndoc);
                    }
                }
                else
                {   //Borra el examen de las vías no validadas
                    SUPER.DAL.Examen.BorrarVia(tr, idExamenOld, t001_idficepi);
                    if (idDocAtenea == null)
                    {
                        //RECUPERO LOS DATOS DEL DOCUMENTO PARA ASIGNÁRSELOS AL NUEVO REGISTRO
                        Examen oExamOld = SelectDoc(tr, idExamenOld, t001_idficepi);
                        idDocAtenea = oExamOld.t2_iddocumento;
                    }
                    //Inserto en FICEPIEXAMEN el nuevo examen
                    SUPER.DAL.Examen.InsertarProf(tr, t001_idficepi, idExamenNew, ndoc, fechaO, fechaC, t839_idestado,
                                                  t591_origenCVT, idDocAtenea, sMotivo);
                    //Borro de FICEPIEXAMEN el examen viejo. Lo hago en este orden (PRIMERO INSERTAR Y LUEGO BORRAR)  
                    //porque si primero borrara el examen viejo y el certificado sólo tuviera ese examen, por trigger sobre FICEPIEXAMEN 
                    //se borraría también el registro en FICEPICERT
                    SUPER.DAL.Examen.DeleteAsistente(tr, idExamenOld, t001_idficepi);
                    //Si el examen viejo era NO VALIDO, se borra el examen
                    SUPER.DAL.Examen.DeleteNoValido(tr, idExamenOld);
                }
            }
            if (sUsuTick.Trim() != "")
            {   //Marco el documento como asignado para que el trigger no lo borre de Atenea
                if (idDocAtenea != null)
                    SUPER.DAL.DocuAux.Asignar(tr, (long)idDocAtenea);
                //Borro el documento de la tabla temporal
                SUPER.DAL.DocuAux.BorrarDocumento(tr, "E", sUsuTick);
            }

            return iRes;
        }

        #endregion
    }
}
