using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using SUPER.DAL;
using SUPER.Capa_Negocio;
using System.Data;
//Para usar ArraList
using System.Collections;

namespace SUPER.BLL
{
    public class Certificado
    {
        #region Constructor
        public Certificado()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion

        #region Propiedades y Atributos 

        private int _T582_IDCERTIFICADO;
        public int T582_IDCERTIFICADO
        {
            get { return _T582_IDCERTIFICADO; }
            set { _T582_IDCERTIFICADO = value; }
        }

        private int _T036_IDCODENTORNO;
        public int T036_IDCODENTORNO
        {
            get { return _T036_IDCODENTORNO; }
            set { _T036_IDCODENTORNO = value; }
        }

        private string _T582_ABREV;
        public string T582_ABREV
        {
            get { return _T582_ABREV; }
            set { _T582_ABREV = value; }
        }

        private bool _T582_VALIDO;
        public bool T582_VALIDO
        {
            get { return _T582_VALIDO; }
            set { _T582_VALIDO = value; }
        }

        private string _EstadoCertificado;
        public string EstadoCertificado
        {
            get { return _EstadoCertificado; }
            set { _EstadoCertificado = value; }
        }

        private string _T582_NOMBRE;
        public string T582_NOMBRE
        {
            get { return _T582_NOMBRE; }
            set { _T582_NOMBRE = value; }
        }

        private string _FOBTENCION;
        public string FOBTENCION
        {
            get { return _FOBTENCION; }
            set { _FOBTENCION = value; }
        }
        private string _FCADUCIDAD;
        public string FCADUCIDAD
        {
            get { return _FCADUCIDAD; }
            set { _FCADUCIDAD = value; }
        }

        private int _T576_IDCRITERIO;
        public int T576_IDCRITERIO
        {
            get { return _T576_IDCRITERIO; }
            set { _T576_IDCRITERIO = value; }
        }

        private string _MOTIVORT;
        public string MOTIVORT
        {
            get { return _MOTIVORT; }
            set { _MOTIVORT = value; }
        }

        private bool _BDOC;
        public bool BDOC
        {
            get { return _BDOC; }
            set { _BDOC = value; }
        }

        private byte[] _T593_DOC;
        public byte[] T593_DOC
        {
            get { return _T593_DOC; }
            set { _T593_DOC = value; }
        }

        private string _T593_NDOC;
        public string T593_NDOC
        {
            get { return _T593_NDOC; }
            set { _T593_NDOC = value; }
        }

        private int _IdFicepiCert;
        public int IdFicepiCert
        {
            get { return _IdFicepiCert; }
            set { _IdFicepiCert = value; }
        }

        private bool _Completado;
        public bool Completado
        {
            get { return _Completado; }
            set { _Completado = value; }
        }

        private bool _DocRechazado;
        public bool DocRechazado
        {
            get { return _DocRechazado; }
            set { _DocRechazado = value; }
        }

        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        private string _EntTecno;
        public string EntornoTecnologico
        {
            get { return _EntTecno; }
            set { _EntTecno = value; }
        }

        private string _EntCert;
        public string EntidadCertificadora
        {
            get { return _EntCert; }
            set { _EntCert = value; }
        }

        private string _Profesional;
        public string Profesional
        {
            get { return _Profesional; }
            set { _Profesional = value; }
        }

        #endregion

        #region Metodos
        public static string GetCertificados(SqlTransaction tr, Nullable<int> idEntidadCert, Nullable<int> idEntornoTecno, string sDenominacion)
        {
            SqlDataReader dr = SUPER.DAL.Certificado.GetCertificados(tr, idEntidadCert, idEntornoTecno, sDenominacion);
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<table id='tblCertificados' style='width:630px;' class='MA'>");
            while (dr.Read())
            {
                sb.Append("<tr id=" + dr["T582_IDCERTIFICADO"].ToString());
                sb.Append(" style='height:20px;' onclick='ms(this);' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td><nobr class='NBR W620' onmouseover='TTip(event)'>" + dr["t582_nombre"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }
        //Obtengo los certificados de un profesional
        public static string MiCVFormacionCertExam(int idFicepi, bool esEncargado)
        {
            SqlDataReader dr = SUPER.DAL.Certificado.MiCVFormacionCertExam(null, idFicepi, esEncargado);
            StringBuilder sb = new StringBuilder();
            string sOri="", sIdFicepiCert="", sComp="";

            sb.Append(@"<table id='tblDatosExamenCert' style='width:930px;' class='MA'>
                        <colgroup>
                            <col style='width:25px;' />
                            <col style='width:20px;' />
                            <col style='width:285px;' />
                            <col style='width:80px;' />
                            <col style='width:250px;' />
                            <col style='width:250px;' />
                            <col style='width:20px;' />
                        </colgroup>");
            while (dr.Read())
            {
                if (dr["origenCVT"] != DBNull.Value && (bool)dr["origenCVT"])
                    sOri = "1";
                else
                    sOri = "0";
                if (dr["Completado"] != DBNull.Value && (bool)dr["Completado"])
                    sComp = "1";
                else
                    sComp = "0";
                if (dr["idFicepiCert"] != DBNull.Value)
                    sIdFicepiCert = "1";
                else
                    sIdFicepiCert = "0";
                sb.Append("<tr id=" + dr["T582_IDCERTIFICADO"].ToString() + " est='" + dr["EstadoCertificado"].ToString() + "'" +
                    " ori='" + sOri + "' idFicCert='" + sIdFicepiCert + "' comp='" + sComp + "' docR='" + dr["DocRechazado"].ToString() + "'" +
                    " onclick='ms(this);' ondblclick=\"abrirDetalleCertificado(" + dr["T582_IDCERTIFICADO"].ToString() + ");\"");
                sb.Append("><td>");
                if (dr["t593_fPeticionBorrado"].ToString() != "")
                    sb.Append("<img src='../../../images/imgPetBorrado.png' title='Pdte de eliminar' />");
                else
                {
                    switch (dr["EstadoCertificado"].ToString())
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
                            if (dr["DocRechazado"].ToString()=="0")
                                sb.Append("<img src='../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa (Documento)' />");
                            else
                                sb.Append("<img src='../../../images/imgDocNoValido.png' title='La documentación acreditativa no es válida' />");
                            break;
                        case ("B"):
                            sb.Append("<img src='../../../images/imgBorrador.png' title='Datos en borrador' />");
                            break;
                        case ("E"):
                            sb.Append("<img src='../../../images/imgNO.gif' title='El certificado contiene algún examen para el que has solicitado su borrado' />");
                            break;
                        default:
                            if (sComp == "1")//Está completo
                            {
                                if (sIdFicepiCert == "0")//Es sugerido
                                    sb.Append("<img src='../../../images/imgPseudovalidado.png' title='Datos que tienes pendiente de completar, actualizar o modificar (Falta documento del certificado)' />");
                                //else No ponemos nada
                            }
                            else
                                sb.Append("<img src='../../../images/imgIncompleto.png' title='Certificación que podrías lograr, en caso de superar los exámenes que componen el certificado. No es obligatorio completarlo' />");
                            break;
                    }
                }
                sb.Append("</td>");

                sb.Append("<td>");
                if (dr["bCertificado"].ToString() != "0")
                {//Tiene documento asociado al certificado
                    sb.Append("<img style='cursor:pointer;' src='../../../images/imgCertificado.png' onclick='verDOC(\"CVTCERT\"," + dr["T582_IDCERTIFICADO"].ToString() + "," + idFicepi + ");' title='Descargar certificado' />");
                }
                sb.Append("</td>");

                //if (sOri == "1") 
                //if (dr["T839_IDESTADO"].ToString() == "Z")//Si el certificado está incompleto lo pongo en rojo
                if (sIdFicepiCert == "0")//Si el certificado es sugerido lo pongo en rojo
                        sb.Append("<td><nobr class='NBR W270' style='color:Red;' onmouseover='TTip(event)' >" + dr["T582_NOMBRE"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td><nobr class='NBR W270' onmouseover='TTip(event)' >" + dr["T582_NOMBRE"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((dr["FOBTENCION"].ToString() == "") ? "" : DateTime.Parse(dr["FOBTENCION"].ToString()).ToShortDateString()) + "</td>");
                sb.Append("<td><nobr class='NBR W240' onmouseover='TTip(event)'>" + dr["ECERTIFICADORA"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W240' onmouseover='TTip(event)'>" + dr["T036_DESCRIPCION"].ToString() + "</nobr></td>");
                sb.Append("<td><img style='cursor:pointer;' src='../../../images/imgCatalogo.png' onclick='getVias(" + dr["t582_idcertificado"].ToString() + "," + idFicepi + ");' title='Visualiza las vías del certificado' /></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }
        //Obtengo las vías de un certificado
        public static string GetVias(SqlTransaction tr, int t582_idCertificado, int t001_idficepi)
        {
            SqlDataReader dr = SUPER.DAL.Certificado.Vias(tr, t582_idCertificado, t001_idficepi);
            StringBuilder sb = new StringBuilder();
            int iViaAnt = -1, iViaAct = -1;

            sb.Append(@"<table id='tblDatosExamen' style='width:640px; text-align:left;'>
                        <colgroup>
                            <col style='width:20px;' />
                            <col style='width:60px;'/>
                            <col style='width:20px;' />
                            <col style='width:100px;' />
                            <col style='width:440px;' />
                        </colgroup>");
            while (dr.Read())
            {
                iViaAct = int.Parse(dr["t585_camino"].ToString());
                sb.Append("<tr style='height:20px;'>");
                //11/02/2014 No hace falta indicar si la vía es propuesta
                //if (dr["idFicepiCamino"].ToString()!="")//Es vía propuesta por el profesional
                //    sb.Append("<td><img style='cursor:pointer;' src='../../../images/imgInvitado.gif' /></td>");
                //else
                    sb.Append("<td></td>");

                if (iViaAnt != iViaAct)
                    sb.Append("<td>Vía " + dr["t585_camino"].ToString() + "</td>");
                else
                    sb.Append("<td></td>");

                if (dr["idFicepiExamen"].ToString() != "")//El profesional tiene el examen
                    sb.Append("<td><img style='cursor:pointer;' src='../../../images/imgValidar.png' /></td>");
                else
                    sb.Append("<td></td>");

                sb.Append("<td><nobr class='NBR W100' onmouseover='TTip(event)' >" + dr["t583_codigo"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W440' onmouseover='TTip(event)'>" + dr["t583_nombre"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
                iViaAnt = iViaAct;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static Certificado Select(int idCertificado, int t001_idficepi)
        {
            SqlDataReader dr = SUPER.DAL.Certificado.Select(idCertificado, t001_idficepi);
            Certificado o = new Certificado();
            if (dr.Read())
            {
                if (dr["T582_IDCERTIFICADO"] != DBNull.Value)
                    o.T582_IDCERTIFICADO = int.Parse(dr["T582_IDCERTIFICADO"].ToString());

                if (dr["EstadoCertificado"] != DBNull.Value)
                    o.EstadoCertificado = dr["EstadoCertificado"].ToString();
                else
                    o.EstadoCertificado = "B";

                if (dr["Completado"] != DBNull.Value)
                    o.Completado = (bool)dr["Completado"];

                if (dr["T582_NOMBRE"] != DBNull.Value)
                    o.T582_NOMBRE = dr["T582_NOMBRE"].ToString();
                if (dr["T582_ABREV"] != DBNull.Value)
                    o.T582_ABREV = dr["T582_ABREV"].ToString();
                if (dr["T582_VALIDO"] != DBNull.Value)
                    o.T582_VALIDO = Convert.ToBoolean(dr["T582_VALIDO"].ToString());
                if (dr["T576_IDCRITERIO"] != DBNull.Value)
                    o.T576_IDCRITERIO = (int)dr["T576_IDCRITERIO"];
                if (dr["t576_nombre"] != DBNull.Value)
                    o._EntCert = dr["t576_nombre"].ToString();

                if (dr["T036_IDCODENTORNO"] != DBNull.Value)
                    o.T036_IDCODENTORNO = (int)dr["T036_IDCODENTORNO"];
                if (dr["t036_descripcion"] != DBNull.Value)
                    o._EntTecno = dr["t036_descripcion"].ToString();

                if (dr["FOBTENCION"] != DBNull.Value)
                    o.FOBTENCION = (dr["FOBTENCION"].ToString() != "") ? DateTime.Parse(dr["FOBTENCION"].ToString()).ToShortDateString() : "";
                if (dr["FCADUCIDAD"] != DBNull.Value)
                    o.FCADUCIDAD = (dr["FCADUCIDAD"].ToString() != "") ? DateTime.Parse(dr["FCADUCIDAD"].ToString()).ToShortDateString() : "";
                if (dr["MotivoRechazo"] != DBNull.Value)
                    o.MOTIVORT = dr["MotivoRechazo"].ToString();
                if (dr["BDOC"] != DBNull.Value)
                    o.BDOC = Convert.ToBoolean(int.Parse(dr["BDOC"].ToString()));
                if (dr["T593_NDOC"] != DBNull.Value)
                    o.T593_NDOC = dr["T593_NDOC"].ToString();
                if (dr["IdFicepiCert"] != DBNull.Value)
                    o.IdFicepiCert = int.Parse(dr["IdFicepiCert"].ToString());
                if (dr["DocRechazado"] != DBNull.Value)
                    o.DocRechazado = Convert.ToBoolean(int.Parse(dr["DocRechazado"].ToString()));
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

        public static Certificado GetDatos(SqlTransaction tr, int t582_idCertificado)
        {
            Certificado o = new Certificado();
            SqlDataReader dr = SUPER.DAL.Certificado.GetCertificado(null, t582_idCertificado);
            if (dr.Read())
            {
                o.T582_IDCERTIFICADO=t582_idCertificado;
                o.T582_NOMBRE=dr["T582_NOMBRE"].ToString();
                o.T576_IDCRITERIO = int.Parse(dr["T576_IDCRITERIO"].ToString());
                o.T036_IDCODENTORNO = int.Parse(dr["T036_IDCODENTORNO"].ToString());
                o.EntidadCertificadora = dr["Entidad"].ToString();
                o.EntornoTecnologico = dr["Entorno"].ToString();
                o.T582_VALIDO = Convert.ToBoolean(dr["T582_VALIDO"].ToString());
                o.T582_ABREV = dr["T582_ABREV"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return o;
        }
        
        public static Certificado SelectDoc(SqlTransaction tr, int idCertificado, int idficepi)
        {
            SqlDataReader dr = SUPER.DAL.Certificado.SelectDoc(tr, idCertificado, idficepi);
            Certificado o = new Certificado();
            if (dr.Read())
            {
                //if (dr["T593_DOC"] != DBNull.Value)
                //    o.T593_DOC = (byte[])dr["T593_DOC"];
                if (dr["T593_NDOC"] != DBNull.Value)
                    o.T593_NDOC = dr["T593_NDOC"].ToString();
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

        public static string GetNombre(SqlTransaction tr, int t582_idCertificado)
        {
            SqlDataReader dr = SUPER.DAL.Certificado.Datos(tr, t582_idCertificado);
            string sDenominacion = "";
            if (dr.Read())
            {
                sDenominacion = dr["T582_NOMBRE"].ToString();
            }
            dr.Close();
            dr.Dispose();
            return sDenominacion;


        }

        public static string DatosExamenes(SqlTransaction tr, int t582_idCertificado, int camino, int t001_idficepi)
        {
            SqlDataReader dr = SUPER.DAL.Certificado.DatosExamenes(tr, t582_idCertificado, camino, t001_idficepi);
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append("<table id='tblDatosExamen' style='width:400px;'>");
            sb.Append("<colgroup><col style='width:40px;' /><col style='width:360px;'/></colgroup>");
            sb.Append("<tbody>");
            if (dr.Read())
            {
                
                sb.Append("<tr id='" + dr["T583_IDEXAMEN"].ToString() + "'>");
                sb.Append("<td style='padding-left:5px;'>");
                if (dr["T591_NDOC"].ToString() != "")
                {
                    sb.Append("<img id='imgDes" + i.ToString() + "' style='cursor:pointer;' src='../../../../Images/imgDescarga.gif' onclick='verDOCAux(\"CVTEXAMEN\"," + dr["T583_IDEXAMEN"].ToString() + ");'/>");
                }
                sb.Append("</td>");

                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W340' onmouseover='TTip(event)'>" + dr["T583_NOMBRE"].ToString() + "</nobr></td>");
                i++;
            }
           
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();


        }

        public static bool TieneExamenesValidados(int t001_idficepi, string sCertificados)
        {
            bool bRes = false;
            string[] aReg = Regex.Split(sCertificados, "##");
            foreach (string oReg in aReg)
            {
                if (oReg == "") continue;
                if (SUPER.DAL.Certificado.HayExamenValidado(t001_idficepi,int.Parse(oReg)))
                {
                    bRes = true;
                    break;
                }
            }
            return bRes;
        }
        public static string BorrarAsistente(string sAccion, int t001_idficepi, string sCertificados, int IdficepiEntrada)
        {
            string sRes = "OK@#@";
            try
            {
                //Si el certificado tiene algun examen validado, no permitimos borrar
                if (TieneExamenesValidados(t001_idficepi, sCertificados))
                {
                    sRes = "KO@#@S";
                }
                else
                {
                    SqlConnection oConn;
                    SqlTransaction tr;
                    string[] aReg = Regex.Split(sCertificados, "##");
                    //Si el certificado tiene algun examen asociado a otro certificado de ese usuario, se da opcion al usuario
                    //de si solo quiere borrar ese certificado o quiere borra tambien sus examenes
                    switch (sAccion)
                    {
                        case "PREGUNTAR":
                            bool bBorrar = true;
                            foreach (string oReg in aReg)
                            {
                                if (oReg == "") continue;
                                //Recorro los exámenes asociados al profesional del certificado que pertenezcan a otro certificado del profesional
                                //Si hay alguno, tengo que pedir confirmación de borrado del examen
                                ArrayList aExam = SUPER.DAL.Certificado.ExamenesAjenos(null, t001_idficepi, int.Parse(oReg));
                                if (aExam.Count > 0)
                                {
                                    bBorrar = false;
                                    sRes = "KO@#@CONF_BORR";
                                }
                            }
                            if (bBorrar)
                            {
                                #region Borrar Certificado y sus examnes
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
                                        //Borrar el certificado del profesional
                                        SUPER.DAL.Certificado.DeleteProf(tr, int.Parse(oReg), t001_idficepi);
                                        //Borrar los examenes del certificado del profesional
                                        SUPER.DAL.Certificado.BorrarExamenesProfesional(tr, int.Parse(oReg), t001_idficepi);
                                        //Borrar el certificado de las vías (si no es una vía validad por RRHH)
                                        SUPER.DAL.Certificado.BorrarVia(tr, int.Parse(oReg), t001_idficepi);
                                        //Además hay que borrar del maestro de exámenes si no es un examen validado por RRHH (t583_valido=0)
                                        SUPER.DAL.Certificado.BorrarExamenesNoValidos(tr, int.Parse(oReg));
                                        //Además hay que borrar del maestro de certificados si no es un certificado validado por RRHH (t582_valido=0)
                                        SUPER.DAL.Certificado.DeleteNoValido(tr, int.Parse(oReg));
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
                            break;
                        case "BORR_TODO":
                            #region Borrar Certificado y sus examnes
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
                                    SUPER.DAL.Certificado.DeleteProf(tr, int.Parse(oReg), t001_idficepi);
                                    ////Recorro los exámenes asociados al profesional del certificado siempre que no pertenezcan a otro certificado
                                    //ArrayList aExam = SUPER.DAL.Certificado.Examenes(tr, t001_idficepi, int.Parse(oReg));
                                    //foreach (string sIdExam in aExam)
                                    //{
                                    //    //Hay que borrar los exámenes asociados al profesional del certificado
                                    //    SUPER.DAL.Examen.DeleteAsistente(tr, int.Parse(sIdExam), t001_idficepi);
                                    //    //Además hay que borrar del maestro de exámenes si no es un examen validado por RRHH (t583_valido=0)
                                    //    SUPER.DAL.Examen.DeleteNoValido(tr, int.Parse(sIdExam));
                                    //}
                                    //Borrar los examenes del certificado del profesional
                                    SUPER.DAL.Certificado.BorrarExamenesProfesional(tr, int.Parse(oReg), t001_idficepi);
                                    //Borrar el certificado de las vías (si no es una vía validad por RRHH)
                                    SUPER.DAL.Certificado.BorrarVia(tr, int.Parse(oReg), t001_idficepi);
                                    //Además hay que borrar del maestro de exámenes si no es un examen validado por RRHH (t583_valido=0)
                                    SUPER.DAL.Certificado.BorrarExamenesNoValidos(tr, int.Parse(oReg));
                                    //Además hay que borrar del maestro de certificados si no es un certificado validado por RRHH (t582_valido=0)
                                    SUPER.DAL.Certificado.DeleteNoValido(tr, int.Parse(oReg));
                                }
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
                            break;
                        case "BORR_CERT":
                            #region Borrar solo certificado y los examenes que no pertenezcan a otro certificado
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
                                    SUPER.DAL.Certificado.DeleteProf(tr, int.Parse(oReg), t001_idficepi);
                                    //Recorro los exámenes asociados al profesional del certificado siempre que no pertenezcan a otro certificado
                                    ArrayList aExam2 = SUPER.DAL.Certificado.ExamenesPropios(tr, t001_idficepi, int.Parse(oReg));
                                    foreach (string sIdExam in aExam2)
                                    {
                                        //Hay que borrar los exámenes asociados al profesional del certificado
                                        SUPER.DAL.Examen.DeleteAsistente(tr, int.Parse(sIdExam), t001_idficepi);
                                        //Además hay que borrar del maestro de exámenes si no es un examen validado por RRHH (t583_valido=0)
                                        SUPER.DAL.Examen.DeleteNoValido(tr, int.Parse(sIdExam));
                                    }
                                    //Borrar el certificado de las vías (si no es una vía validada por RRHH)
                                    SUPER.DAL.Certificado.BorrarVia(tr, int.Parse(oReg), t001_idficepi);
                                    //Además hay que borrar del maestro de certificados si no es un certificado validado por RRHH (t582_valido=0)
                                    SUPER.DAL.Certificado.DeleteNoValido(tr, int.Parse(oReg));
                                }
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
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                sRes = "ERROR@#@" + ex.Message;
            }
            return sRes;
        }

        public static void PedirBorrado(int t001_idficepi, int idCertificado, int t001_idficepi_petbor, string sMotivo, string sDatosCorreo)
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
                SUPER.DAL.Certificado.PedirBorrado(null, idCertificado, t001_idficepi, t001_idficepi_petbor, sMotivo);
                SUPER.Capa_Negocio.Correo.EnviarPetBorrado("CE", sDatosCorreo, sMotivo);
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
        //public static void InsertarSinoExiste(SqlTransaction tr, int idCertificado, int idFicepi, string t593_ndoc, byte[] t593_doc,
        //                                      string t593_motivort, Nullable<bool> t593_origencvt)
        //{
        //    bool bInsertar = false;
        //    SqlDataReader dr = SUPER.DAL.Certificado.GetDatos(tr, idCertificado, idFicepi);
        //    if (!dr.Read())
        //        bInsertar = true;
        //    dr.Close();
        //    dr.Dispose();
        //    if (bInsertar)
        //    {
        //        SUPER.DAL.Certificado.Insertar(tr, idCertificado, idFicepi, t593_ndoc, t593_doc, null, t593_motivort, t593_origencvt);
        //    }
        //}
        public static void BorrarProfesional(SqlTransaction tr, int t582_idCertificado, int t001_idficepi)
        {
            SUPER.DAL.Certificado.DeleteProf(tr, t582_idCertificado, t001_idficepi);
        }
        /// <summary>
        /// Al insertar un certificado para un profesional, si lleva doc, el certificado debe quedar Pdte Validar. 
        /// Sino lleva doc -> Pdte. Anexar
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t593_ndoc"></param>
        /// <param name="t593_doc"></param>
        public static void InsertarProfesional(SqlTransaction tr, int t582_idcertificado, int t001_idficepi,
                                               string t593_ndoc, Nullable<long> idContentServer)
        {
            SUPER.DAL.Certificado.Insertar(tr, t582_idcertificado, t001_idficepi, t593_ndoc, null, "", true, idContentServer);
        }
        /// <summary>
        /// Se borran las vías que existieran para ese profesional en ese certificado
        /// Si el certificado es no válido (RRHH todavía no ha dado su visto bueno) se inserta la vía
        /// Sino, se comprueba si ese certificado tiene algún camino que coincida (o sea un superconjunto) con el que se quiere grabar
        ///     Si existe, no se hace nada
        ///     Sino, se inserta la vía
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="idCertificado"></param>
        /// <param name="slExamenes"></param>
        /// <param name="t001_idficepi"></param>
        public static void insertarVia(SqlTransaction tr, int idCertificado, string slExamenes, int t001_idficepi)
        {
            //Borrar las vías viejas (si no es una vía validada por RRHH)
            SUPER.DAL.Certificado.BorrarVia(tr, idCertificado, t001_idficepi);

            if (SUPER.DAL.Certificado.EsValido(idCertificado))
            {
                bool bExisteVia = false;
                //Compruebo las vías válidas del certificado
                SqlDataReader dr = SUPER.DAL.Certificado.GetVias(tr, idCertificado);
                while (dr.Read())
                {
                    if (dr["EXAMENES"].ToString() == slExamenes + ",")
                    {
                        bExisteVia = true;
                        break;
                    }
                    else
                    {//Comprobar que la vía a grabar no sea un subconjunto de otra ya existente
                        if (SUPER.Capa_Negocio.Utilidades.EsSubconjunto(slExamenes, dr["EXAMENES"].ToString()))
                        {
                            bExisteVia = true;
                            break;
                        }
                    }
                }
                dr.Close();
                dr.Dispose();
                if (!bExisteVia)
                    SUPER.DAL.Certificado.insertarVia(tr, idCertificado, slExamenes, t001_idficepi);
            }
            else
            {
                //Inserta la nueva vía
                SUPER.DAL.Certificado.insertarVia(tr, idCertificado, slExamenes, t001_idficepi);
            }
        }
        public static bool ViaCompletada(SqlTransaction tr, int idCertificado, string slExamenes)
        {
            bool bViaCompletada = false, bExamenNoEncontrado=false, bExamenEncontrado=false;
            string sVia = "";
            string[] aLExam = Regex.Split(slExamenes, ",");
            slExamenes = Utilidades.QuitaUltimoCaracter(slExamenes, ",");

            //Compruebo las vías válidas del certificado
            SqlDataReader dr = SUPER.DAL.Certificado.GetVias(tr, idCertificado);
            while (dr.Read() && !bViaCompletada)
            {
                bExamenNoEncontrado = false;
                bExamenEncontrado=false;
                sVia = Utilidades.QuitaUltimoCaracter(dr["EXAMENES"].ToString(), ",");
                string[] aLVia = Regex.Split(sVia, ",");
                //Para cada camimo de la vía, compruebo si tengo todos sus exámenes
                foreach (string oElemVia in aLVia)
                {
                    if (oElemVia == "") continue;
                    else
                    {
                        if (!Utilidades.EstaEnLista(oElemVia, aLExam))
                        {
                            bExamenNoEncontrado = true;
                            break;
                        }
                        else
                            bExamenEncontrado=true;
                    }
                }
                //Si alguna vía está completada, paramos el bucle
                if (bExamenEncontrado && !bExamenNoEncontrado)
                    bViaCompletada = true;
            }
            dr.Close();
            dr.Dispose();

            return bViaCompletada;
        }
        public static void DeleteNoValido(SqlTransaction tr, int idCertificado)
        {
            SUPER.DAL.Certificado.DeleteNoValido(tr, idCertificado);
        }
        /// <summary>
        /// Lista de examenes de un profesional en un certificado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static ArrayList Examenes(SqlTransaction tr, int t001_idficepi, int t582_idcertificado)
        {
            return SUPER.DAL.Certificado.Examenes(tr, t001_idficepi, t582_idcertificado);
        }
        /// <summary>
        /// Indica si un certificado tiene profesionales, es decir, si tiene registros en T593_FICEPICERT
        /// </summary>
        /// <param name="t582_idcertificado"></param>
        /// <returns></returns>
        public static bool TieneProfesionales(SqlTransaction tr, int t582_idcertificado)
        {
            bool bRes = false;
            if (SUPER.DAL.Certificado.TieneProfesionales(tr, t582_idcertificado))
                bRes = true;
            return bRes;
        }

        /// <summary>
        /// Revisa los examenes que tiene un profesional e inserta en T593_FICEPICERT aquellos certificados para los que:
        ///		1.- Tiene una via validada completa 
        ///		2.- Sea un certificado vigente
        ///		3.- Sea un certificado válido
        ///		4.- No esten ya en T593_FICEPICERT
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t593_origencvt"></param>
        public static void ConseguirAutomatico(SqlTransaction tr, int t001_idficepi, bool t593_origencvt)
        {
            SUPER.DAL.Certificado.ConseguirAutomatico(tr, t001_idficepi, t593_origencvt);
        }

        public static int tramitarFicepiCert(SqlTransaction tr, int t582_idcertificadoOld, int t582_idcertificadoNew, string t582_nombre,
                                             string t582_abrev, byte[] t593_doc, string t593_ndoc, Nullable<int> t576_idcriterio,
                                             Nullable<int> t036_idcodentorno, int t001_idficepi, bool cambioDoc, string t593_motivort,
                                             Nullable<long> t2_iddocumento, string sNomCertInicial)
        {
            int idCert = -1;
            if (t582_idcertificadoNew == -1)
            {//Certificado nuevo
                idCert = SUPER.DAL.Certificado.InsertarCertificado(tr, t582_nombre.Trim(), t582_abrev.Trim(), t036_idcodentorno, t576_idcriterio,
                                                                     t593_motivort, false);
            }
            else
                idCert = t582_idcertificadoNew;

            if (t582_idcertificadoOld != -1 && t582_idcertificadoOld != t582_idcertificadoNew)
            {//Se ha modificado el certificado
                if (SUPER.DAL.Certificado.EsValido(t582_idcertificadoOld))
                {//Si el certificado es válido borramos solo su asociación al profesional
                    SUPER.DAL.Certificado.DeleteProf(tr, t582_idcertificadoOld, t001_idficepi);
                }
                else
                {//Si el certificado sustituido no era válido lo borramos (y por cascada se borra su vía y el FicepiCert)
                    DeleteNoValido(tr, t582_idcertificadoOld);
                }
            }
            else
            {
                if (t582_idcertificadoNew != -1 && t582_idcertificadoOld == t582_idcertificadoNew)
                {//Si el certificado es No Válido, lo actualizamos
                    SUPER.DAL.Certificado.UpdateCertificado(tr, t582_idcertificadoNew, t582_nombre, t582_abrev, t036_idcodentorno, t576_idcriterio);
                }
            }
            //Si existe registro en T593_FICEPICERT, lo actualizo. Sino, lo inserto
            #region Obtengo el contenido del archivo y lo guardo en ATENEA
            if (cambioDoc && t593_ndoc.Trim() != "")
            {
                //Si he seleccionado un archivo, cargo el archivo en el ContenteServer y obtengo su identificador
                if (t2_iddocumento != null)
                {
                    if (sNomCertInicial == t593_ndoc)
                    {//Si el nombre del nuevo archivo es el mismo que el inicial
                        IB.Conserva.ConservaHelper.ActualizarContenidoDocumento((long)t2_iddocumento, t593_doc);
                    }
                    else
                    {//El archivo a cargar es dierente
                        IB.Conserva.ConservaHelper.ActualizarDocumento((long)t2_iddocumento, t593_doc, t593_ndoc);
                    }
                }
                else
                    t2_iddocumento = IB.Conserva.ConservaHelper.SubirDocumento(t593_ndoc, t593_doc);
            }
            #endregion

            if (SUPER.DAL.Certificado.LoTiene(tr,t001_idficepi,idCert))
            {
                SUPER.DAL.Certificado.UpdatearDoc(tr, idCert, t001_idficepi, t593_ndoc, t593_motivort, cambioDoc, t2_iddocumento);
            }
            else
            {
                SUPER.DAL.Certificado.Insertar(tr, idCert, t001_idficepi, t593_ndoc, null, t593_motivort, true, t2_iddocumento);
            }

            return idCert;
        }

        public static void PonerDocumento(SqlTransaction tr, int t001_idficepi, int t582_idcertificado, long t2_iddocumento, string t593_ndoc)
        {
            SUPER.DAL.Certificado.UpdatearDoc(tr, t582_idcertificado, t001_idficepi, t593_ndoc, "", true, t2_iddocumento);
        }
        public static void PonerMotivo(SqlTransaction tr, int t001_idficepi, int t582_idcertificado, string t593_motivort)
        {
            SUPER.DAL.Certificado.PonerMotivo(tr, t582_idcertificado, t001_idficepi, t593_motivort);
        }

        #region Exportación de certificados
        /// <summary>
        /// Dada una lista de denominaciones separadas por ; devuelve todos los códigos de certificados cuya enominación coincide
        /// en todo o en parte con cada una de las denominaciones
        /// </summary>
        /// <param name="sListaDenominaciones"></param>
        /// <returns></returns>
        //public static string GetIds(string sListaDenominaciones, string sSeparador)
        //{
        //    string sRes = "";

        //    string sAux = sListaDenominaciones.Replace(";", "##");
        //    SqlDataReader dr = SUPER.DAL.Certificado.GetIdsCertificado(null, sAux);
        //    while (dr.Read())
        //    {
        //        sRes += dr["t582_idcertificado"].ToString() + sSeparador;
        //    }
        //    dr.Close();
        //    dr.Dispose();

        //    return sRes;
        //}

        /// <summary>
        /// Obtiene una lista de los certificados cuyo código se pasa en sListaIds + los certificados cuya denominación está en sListaDens
        /// y existe algun profesional de slFicepis que lo tiene
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="sListaIds"></param>
        /// <param name="sListaDens"></param>
        /// <returns></returns>
        public static List<Certificado> GetListaPorProfesional(string slFicepis, string sListaIds, string sListaDens)
        {
            List<Certificado> oLista = new List<Certificado>();
            Certificado oElem;
            SqlDataReader dr =
                SUPER.DAL.Certificado.GetListaPorProfesional(null, slFicepis.Replace(",", "##"), sListaIds, sListaDens.Replace(";", "##"));
            while (dr.Read())
            {
                oElem = new Certificado();
                oElem.T582_IDCERTIFICADO = int.Parse(dr["T582_IDCERTIFICADO"].ToString());
                oElem.T582_NOMBRE = dr["T582_NOMBRE"].ToString();
                oLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return oLista;
        }
        /// <summary>
        /// Obtiene una lista con los datos de los certificados de los profesionales que se pasan como parametros
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="slCerts"></param>
        /// <returns></returns>
        public static List<Certificado> GetCertificadosExportacion(string slFicepis, string slCerts)
        {
           List<Certificado> oLista = new List<Certificado>();
           Certificado oElem;
           SqlDataReader dr = SUPER.DAL.Certificado.GetCertificadosExportacion(null, slFicepis.Replace(",", "##"), slCerts.Replace(",", "##"));
           while (dr.Read())
           {
               oElem = new Certificado();
               oElem.IdFicepiCert = int.Parse(dr["t001_idficepi"].ToString());
               oElem.T582_IDCERTIFICADO = int.Parse(dr["T582_IDCERTIFICADO"].ToString());
               oElem.Profesional = dr["Profesional"].ToString();
               oElem.T582_NOMBRE = dr["T582_NOMBRE"].ToString();
               oElem.T593_NDOC = dr["T593_NDOC"].ToString();
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
        #endregion
    }
}
