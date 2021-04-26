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
{
    /// <summary>
    /// Descripción breve de TituloFicepi
    /// </summary>
    public class TituloFicepi
    {
        #region Propiedades y Atributos

        private int _T012_IDTITULOFICEPI;
        public int T012_IDTITULOFICEPI
        {
            get { return _T012_IDTITULOFICEPI; }
            set { _T012_IDTITULOFICEPI = value; }
        }

        private short _T019_IDCODTITULO;
        public short T019_IDCODTITULO
        {
            get { return _T019_IDCODTITULO; }
            set { _T019_IDCODTITULO = value; }
        }

        private string _T019_DESCRIPCION;
        public string T019_DESCRIPCION
        {
            get { return _T019_DESCRIPCION; }
            set { _T019_DESCRIPCION = value; }
        }
        
        private int _T001_IDFICEPI;
        public int T001_IDFICEPI
        {
            get { return _T001_IDFICEPI; }
            set { _T001_IDFICEPI = value; }
        }

        private byte? _t019_tipo;
        public byte? t019_tipo
        {
            get { return _t019_tipo; }
            set { _t019_tipo = value; }
        }

        private byte? _t019_modalidad;
        public byte? t019_modalidad
        {
            get { return _t019_modalidad; }
            set { _t019_modalidad = value; }
        }

        private bool _t019_tic;
        public bool t019_tic
        {
            get { return _t019_tic; }
            set { _t019_tic = value; }
        }

        private bool _T012_FINALIZADO;
        public bool T012_FINALIZADO
        {
            get { return _T012_FINALIZADO; }
            set { _T012_FINALIZADO = value; }
        }
        
        private string _T012_ESPECIALIDAD;
        public string T012_ESPECIALIDAD
        {
            get { return _T012_ESPECIALIDAD; }
            set { _T012_ESPECIALIDAD = value; }
        }

        private string _T012_CENTRO;
        public string T012_CENTRO
        {
            get { return _T012_CENTRO; }
            set { _T012_CENTRO = value; }
        }

        private short? _T012_INICIO;
        public short? T012_INICIO
        {
            get { return _T012_INICIO; }
            set { _T012_INICIO = value; }
        }

        private short? _T012_FIN;
        public short? T012_FIN
        {
            get { return _T012_FIN; }
            set { _T012_FIN = value; }
        }

        private byte[] _T012_DOCTITULO;
        public byte[] T012_DOCTITULO
        {
            get { return _T012_DOCTITULO; }
            set { _T012_DOCTITULO = value; }
        }

        private string _T012_NDOCTITULO;
        public string T012_NDOCTITULO
        {
            get { return _T012_NDOCTITULO; }
            set { _T012_NDOCTITULO = value; }
        }

        private byte[] _T012_DOCEXPDTE;
        public byte[] T012_DOCEXPDTE
        {
            get { return _T012_DOCEXPDTE; }
            set { _T012_DOCEXPDTE = value; }
        }

        private string _T012_NDOCEXPDTE;
        public string T012_NDOCEXPDTE
        {
            get { return _T012_NDOCEXPDTE; }
            set { _T012_NDOCEXPDTE = value; }
        }

        private string _T839_IDESTADO;
        public string T839_IDESTADO
        {
            get { return _T839_IDESTADO; }
            set { _T839_IDESTADO = value; }
        }

        private bool _T019_ESTADO;
        public bool T019_ESTADO
        {
            get { return _T019_ESTADO; }
            set { _T019_ESTADO = value; }
        }

        private string _t597_motivort;
        public string t597_motivort
        {
            get { return _t597_motivort; }
            set { _t597_motivort = value; }
        }

        private string _T012_OBSERVA;
        public string T012_OBSERVA
        {
            get { return _T012_OBSERVA; }
            set { _T012_OBSERVA = value; }
        }

        private DateTime _T012_FECHAU;
        public DateTime T012_FECHAU
        {
            get { return _T012_FECHAU; }
            set { _T012_FECHAU = value; }
        }

        private int _T001_IDFICEPIU;
        public int T001_IDFICEPIU
        {
            get { return _T001_IDFICEPIU; }
            set { _T001_IDFICEPIU = value; }
        }

        private string _PROFESIONAL;
        public string PROFESIONAL
        {
            get { return _PROFESIONAL; }
            set { _PROFESIONAL = value; }
        }
        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }
        private long? _t2_iddocumentoExpte;
        public long? t2_iddocumentoExpte
        {
            get { return _t2_iddocumentoExpte; }
            set { _t2_iddocumentoExpte = value; }
        }

        #endregion

        #region Constructor
        public TituloFicepi()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion

        #region Metodos

            public static List<ElementoLista> obtenerTipos()
            {
                SqlDataReader dr = DAL.TituloFicepi.obtenerTipos();
                List<ElementoLista> oLista = new List<ElementoLista>();
                string[] strTipos = null;

                if (dr.Read())
                {
                    strTipos = Regex.Split(dr["TIPOS"].ToString(), "@#@");
                }
                foreach (string oFun in strTipos)
                {
                    string[] aValores = Regex.Split(oFun, "#/#");
                    oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
                }
                dr.Close();
                dr.Dispose();
                return oLista;
            }

            public static List<ElementoLista> obtenerModalidades()
            {
                SqlDataReader dr = DAL.TituloFicepi.obtenerModalidades();
                List<ElementoLista> oLista = new List<ElementoLista>();
                string[] strModalidades = null;

                if (dr.Read())
                {
                    strModalidades = Regex.Split(dr["MODALIDADES"].ToString(), "@#@");
                }
                foreach (string oFun in strModalidades)
                {
                    string[] aValores = Regex.Split(oFun, "#/#");
                    oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
                }
                dr.Close();
                dr.Dispose();
                return oLista;
            }

            public static List<ElementoLista> obtenerAños()
            {
                SqlDataReader dr = DAL.TituloFicepi.obtenerAños();
                List<ElementoLista> oLista = new List<ElementoLista>();
                string[] strModalidades = null;

                if (dr.Read())
                {
                    strModalidades = Regex.Split(dr["AÑOS"].ToString(), "@#@");
                }
                foreach (string oFun in strModalidades)
                {
                    oLista.Add(new ElementoLista(oFun.ToString(), oFun.ToString()));
                }
                dr.Close();
                dr.Dispose();
                return oLista;
            }

            public static void Grabar(Nullable<int> idTituloFicepi, string sTitulo, short idcodtitulo, int idficepi, Nullable<byte> tipo, 
                                      Nullable<byte> modalidad, bool tic, bool finalizado, string especialidad, string centro,
                                      Nullable<short> inicio, Nullable<short> fin, string ndoctitulo, string ndocexpdte, 
                                      string observa, char estado, string motivort, int idficepiu, bool cambioDoc, 
                                      bool cambioExp, char estadoIni, string opcion, short idcodtituloIni,
                                      Nullable<long> idContentServer, Nullable<long> idContentServerExpte, string sEsMiCV)
            {
                int nFilasModificadas = 0;
                string sResul = "";
                bool bErrorControlado = false;

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

                try {
                    if (idTituloFicepi==null)
                    {
                        if (idcodtitulo == -1)
                            idcodtitulo = SUPER.BLL.Titulacion.Insertar(null, sTitulo, idficepi, bool.Parse("false"), (byte)tipo, (byte?)modalidad, tic);
                        //DAL.TituloFicepi.Insert(tr, idcodtitulo, idficepi, tipo, modalidad, tic, finalizado, especialidad, centro, inicio, fin, doctitulo, ndoctitulo, docexpdte, ndocexpdte, estado, motivort, observa, idficepiu);
                        nFilasModificadas = DAL.TituloFicepi.Insert(tr, idcodtitulo, idficepi, finalizado, especialidad, centro, inicio, fin,
                                                                    ndoctitulo, ndocexpdte, estado, motivort, observa, idficepiu, 
                                                                    idContentServer, idContentServerExpte);//, tipo, modalidad, tic
                    }
                    else
                    {
                        switch (opcion)
                        {
                            case "1":
                                //Titulacion Validada y no Cambiada
                                //Update de tituloficepi sin cambiar codtitulo
                            //    nFilasModificadas = DAL.TituloFicepi.Update(tr, (int)idTituloFicepi, idcodtitulo, idficepi, tipo, modalidad, tic, finalizado, especialidad, centro, inicio, fin, doctitulo, ndoctitulo, docexpdte, ndocexpdte, estado, motivort, observa, idficepiu, cambioDoc, cambioExp, estadoIni);
                                nFilasModificadas = DAL.TituloFicepi.Update(tr, (int)idTituloFicepi, idcodtitulo, idficepi, finalizado,
                                                                            especialidad, centro, inicio, fin, ndoctitulo, ndocexpdte,  
                                                                            estado, motivort, observa, idficepiu, cambioDoc,
                                                                            cambioExp, estadoIni, idContentServer, idContentServerExpte);//, tipo, modalidad, tic
                                break;
                            case "2":
                                //Titulacion Validada y Cambiada por otra validada
                                //Update de tituloficepi cambiando codtitulo
                            //    nFilasModificadas = DAL.TituloFicepi.Update(tr, (int)idTituloFicepi, idcodtitulo, idficepi, tipo, modalidad, tic, finalizado, especialidad, centro, inicio, fin, doctitulo, ndoctitulo, docexpdte, ndocexpdte, estado, motivort, observa, idficepiu, cambioDoc, cambioExp, estadoIni);
                                nFilasModificadas = DAL.TituloFicepi.Update(tr, (int)idTituloFicepi, idcodtitulo, idficepi, finalizado,
                                                                            especialidad, centro, inicio, fin, ndoctitulo, ndocexpdte, 
                                                                            estado, motivort, observa, idficepiu, cambioDoc,
                                                                            cambioExp, estadoIni, idContentServer, idContentServerExpte);//, tipo, modalidad, tic
                                break;
                            case "3":
                                //Titulacion Validada y Cambiada por otra no validada
                                //Insertar Nueva Titulacion y update tituloficepi 
                                {
                                    idcodtitulo = SUPER.BLL.Titulacion.Insertar(null, sTitulo, idficepi, bool.Parse("false"), (byte)tipo, (byte?)modalidad, tic);
                            //        nFilasModificadas = DAL.TituloFicepi.Update(tr, (int)idTituloFicepi, idcodtitulo, idficepi, tipo, modalidad, tic, finalizado, especialidad, centro, inicio, fin, doctitulo, ndoctitulo, docexpdte, ndocexpdte, estado, motivort, observa, idficepiu, cambioDoc, cambioExp, estadoIni);
                                    nFilasModificadas = DAL.TituloFicepi.Update(tr, (int)idTituloFicepi, idcodtitulo, idficepi, finalizado,
                                                                                especialidad, centro, inicio, fin, ndoctitulo, ndocexpdte, 
                                                                                estado, motivort, observa, idficepiu,
                                                                                cambioDoc, cambioExp, estadoIni, idContentServer, idContentServerExpte);//, tipo, modalidad, tic
                                }
                                break;
                            case "4":
                                //Titulacion No Validada y Cambiada por otra no validada
                                //Update Titulacion
                                DAL.Titulacion.Update(tr, idcodtitulo, sTitulo, bool.Parse("false"), idficepi, (byte)tipo, (byte?)modalidad, tic, true);
                                nFilasModificadas = 1;
                                break;
                            case "5":
                                //Paso de titulación No Validada a Validada
                                //Update TituloFicepi y borra la titulación no validad si no está en uso
                                nFilasModificadas = DAL.TituloFicepi.Update(tr, (int)idTituloFicepi, idcodtitulo, idficepi, finalizado, 
                                                                especialidad, centro, inicio, fin, ndoctitulo, ndocexpdte,
                                                                estado, motivort, observa, idficepiu, cambioDoc, cambioExp, estadoIni,
                                                                idContentServer, idContentServerExpte);
                                if (idcodtituloIni != -1)
                                    SUPER.BLL.Titulacion.BorrarNoUsada(tr, idcodtituloIni);
                                break;
                        }
                    }

                    if (nFilasModificadas == 0)
                    {
                        sResul = "Fila no actualizada";
                        bErrorControlado = true;
                        throw (new Exception(sResul));
                    }
                    if (sEsMiCV == "S" && (estado.ToString() == "O" || estado.ToString() == "P")) DAL.Curriculum.ActualizadoCV(tr, idficepi);

                    Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    Conexion.CerrarTransaccion(tr);
                    if (bErrorControlado) sResul = ex.Message;
                    else sResul = Errores.mostrarError("Error al actualizar la titulación.", ex);
                }
                finally
                {
                    Conexion.Cerrar(oConn);
                    if (sResul != "")
                    {
                        if (bErrorControlado) sResul = "ErrorControlado##EC##" + sResul;
                        throw (new Exception(sResul));
                    }
                }
            }

            public static TituloFicepi Select(int idTituloFicepi)
            {
                SqlDataReader dr = DAL.TituloFicepi.Select(idTituloFicepi);
                TituloFicepi o = new TituloFicepi();
                if (dr.Read())
                {
                    if (dr["T012_IDTITULOFICEPI"] != DBNull.Value)
                        o.T012_IDTITULOFICEPI = int.Parse(dr["T012_IDTITULOFICEPI"].ToString());
                    if (dr["T019_IDCODTITULO"] != DBNull.Value)
                        o.T019_IDCODTITULO = short.Parse(dr["T019_IDCODTITULO"].ToString());
                    if (dr["T019_DESCRIPCION"] != DBNull.Value)
                        o.T019_DESCRIPCION = dr["T019_DESCRIPCION"].ToString();
                    if (dr["T001_IDFICEPI"] != DBNull.Value)
                        o.T001_IDFICEPI = int.Parse(dr["T001_IDFICEPI"].ToString());
                    if (dr["t019_tipo"] != DBNull.Value)
                        o.t019_tipo = byte.Parse(dr["t019_tipo"].ToString());
                    if (dr["t019_modalidad"] != DBNull.Value)
                        o.t019_modalidad = byte.Parse(dr["t019_modalidad"].ToString());
                    if (dr["t019_tic"] != DBNull.Value)
                        o.t019_tic = bool.Parse(dr["T019_TIC"].ToString());
                    if (dr["T012_FINALIZADO"] != DBNull.Value)
                        o.T012_FINALIZADO = bool.Parse(dr["T012_FINALIZADO"].ToString());
                    if (dr["T012_ESPECIALIDAD"] != DBNull.Value)
                        o.T012_ESPECIALIDAD = dr["T012_ESPECIALIDAD"].ToString();
                    if (dr["T012_CENTRO"] != DBNull.Value)
                        o.T012_CENTRO = dr["T012_CENTRO"].ToString();
                    if (dr["T012_INICIO"] != DBNull.Value)
                        o.T012_INICIO = short.Parse(dr["T012_INICIO"].ToString());
                    if (dr["T012_FIN"] != DBNull.Value)
                        o.T012_FIN = short.Parse(dr["T012_FIN"].ToString());
                    //if (dr["T012_DOCTITULO"] != DBNull.Value)
                    //    o.T012_DOCTITULO = (byte[])dr["T012_DOCTITULO"];
                    if (dr["T012_NDOCTITULO"] != DBNull.Value)
                        o.T012_NDOCTITULO = dr["T012_NDOCTITULO"].ToString();
                    //if (dr["T012_DOCEXPDTE"] != DBNull.Value)
                    //    o.T012_DOCEXPDTE = (byte[])dr["T012_DOCEXPDTE"];
                    if (dr["T012_NDOCEXPDTE"] != DBNull.Value)
                        o.T012_NDOCEXPDTE = dr["T012_NDOCEXPDTE"].ToString();
                    if (dr["T839_IDESTADO"] != DBNull.Value)
                        o.T839_IDESTADO = dr["T839_IDESTADO"].ToString();
                    if (dr["T019_ESTADO"] != DBNull.Value)
                        o.T019_ESTADO = (bool)dr["T019_ESTADO"];
                    if (dr["t597_motivort"] != DBNull.Value)
                        o.t597_motivort = dr["t597_motivort"].ToString();
                    if (dr["T012_OBSERVA"] != DBNull.Value)
                        o.T012_OBSERVA = dr["T012_OBSERVA"].ToString();
                    if (dr["T012_FECHAU"] != DBNull.Value)
                        o.T012_FECHAU = DateTime.Parse(dr["T012_FECHAU"].ToString());
                    if (dr["T001_IDFICEPIU"] != DBNull.Value)
                        o.T001_IDFICEPIU = int.Parse(dr["T001_IDFICEPIU"].ToString());
                    if (dr["PROFESIONAL"] != DBNull.Value)
                        o.PROFESIONAL = dr["PROFESIONAL"].ToString();
                    if (dr["t2_iddocumento"] != DBNull.Value)
                        o.t2_iddocumento = (long)dr["t2_iddocumento"];
                    if (dr["t2_iddocumento_expdte"] != DBNull.Value)
                        o.t2_iddocumentoExpte = (long)dr["t2_iddocumento_expdte"];
                }

                return o;
            }

            public static string MiCvTitulacion(int idFicepi)
            {
                SqlDataReader dr = DAL.TituloFicepi.MiCvTitulacion(null, idFicepi);
                StringBuilder sb = new StringBuilder();
                int i = 0;

                sb.Append(@"<table id='tblDatosTitulacion' style='width:930px;' class='MA'>
                            <colgroup>
                                <col style='width:25px;' />
                                <col style='width:20px;' />
                                <col style='width:35px;' />
                                <col style='width:50px;'/>
                                <col style='width:50px;'/>
                                <col style='width:750px;' />
                            <colgroup>");
                while (dr.Read())
                {
                    sb.Append("<tr id=" + dr["ID"].ToString() + " est='" + dr["t839_idestado"].ToString() + "'" +
                        " onclick='ms(this);' ondblclick='AnadirTitulacion(" + dr["ID"].ToString() + ",\"" + dr["t839_idestado"].ToString() + "\");'>");
                    sb.Append("<td>");
                    switch (dr["t839_idestado"].ToString())
                    {
                        //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
                        //case ("O"):
                        //case ("P"):
                        //    sb.Append("<img src='../../../images/imgPenValidar.png' title='Datos pendientes de validar por la organización' />"); break;
                        //case ("R"): sb.Append("<img src='../../../images/imgRechazar.png' title='Este dato es únicamente visible por ti' />"); break;
                        case ("S"):
                        case ("T"): 
                            sb.Append("<img src='../../../images/imgPenCumplimentar.png' title='Datos que tienes pendiente de completar, actualizar o modificar' />"); break;
                        case ("B"): sb.Append("<img src='../../../images/imgBorrador.png' title='Datos en borrador' />"); break;
                        case ("X"):
                        case ("Y"):
                            sb.Append("<img src='../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa' />"); break;

                    }
                    sb.Append("</td>");
                    sb.Append("<td>");
                    if (dr["bDocTit"].ToString() == "1")
                    {
                        sb.Append("<img id='imgDes" + i.ToString() + "' style='cursor:pointer;' src='../../../images/imgTitulo.png' onclick='descargar(\"CVTDOCTIT\",\"" + dr["ID"].ToString() + "\");' title='Descargar acreditación del título' />");
                    }
                    sb.Append("</td>");
                    sb.Append("<td>");
                    if (dr["bDocEx"].ToString() == "1")
                    {
                        sb.Append("<img id='imgDes" + i.ToString() + i.ToString() + "' style='cursor:pointer;' src='../../../images/imgExpediente.png' onclick='descargar(\"CVTDOCEX\",\"" + dr["ID"].ToString() + "\");' title='Descargar expediente académico' />");
                    }
                    sb.Append("</td>");
                    sb.Append("<td>" + dr["T012_INICIO"].ToString() + "</td>");
                    sb.Append("<td>" + dr["T012_FIN"].ToString() + "</td>");
                    sb.Append("<td><nobr class='NBR W680' onmouseover='TTip(event)'>" + dr["T019_DESCRIPCION"].ToString() + "</nobr></td>");
                    sb.Append("</tr>");
                    i++;
                }
                sb.Append("</table>");
                dr.Close();
                dr.Dispose();

                return sb.ToString();
            }

            public static string MiCvTitulacionHTML(int idFicepi, int bFiltros, string t019_descripcion, Nullable<int> t019_idcodtitulo, 
                                                    Nullable<int> t019_tipo, Nullable<bool> t019_tic, Nullable<int> t019_modalidad)
            {
                SqlDataReader dr = 
                    DAL.TituloFicepi.MiCvTitulacionHTML(null, idFicepi, bFiltros, t019_descripcion, t019_idcodtitulo, t019_tipo, t019_tic, t019_modalidad);
                StringBuilder sb = new StringBuilder();
                //Para poner en rojo lo que no esté validado
                string sColor = "";

                //if (dr.HasRows)
                //{
                //    sb.Append("<table style='margin-left:40px; margin-top:25px; width:625px;'>");
                //    sb.Append("<tr><td>");
                //    sb.Append("<label id='lblFAcad' class='titulo1'>Formación Academica</label>");
                //    sb.Append("</td></tr>");
                //    sb.Append("</table>");
                //}
                while (dr.Read())
                {
                    string docEx = "";
                    string docTit = "";
                    if (dr["bDocTit"].ToString() == "1")
                    {
                        docTit="<img style='cursor:pointer;' src='../../../images/imgTitulo.png' onclick='descargar(\"CVTDOCTIT\",\"" + dr["T012_IDTITULOFICEPI"].ToString() + "\");' title='Descargar acreditación del título' />";
                    }
                    //Para poner en rojo lo que no esté validado
                    sColor = "";
                    if (dr["ESTADO"].ToString() == "1")
                        sColor = "color:Red;";
                    sb.Append("</td>");
                    sb.Append("<td>");
                    if (dr["bDocEx"].ToString() == "1")
                    {
                        docEx = "<img style='cursor:pointer;' src='../../../images/imgExpediente.png' onclick='descargar(\"CVTDOCEX\",\"" + dr["T012_IDTITULOFICEPI"].ToString() + "\");' title='Descargar expediente académico' />";
                    }

                    sb.Append("<table style='width:590px; margin-left:60px; margin-top:15px;' cellpadding='1' cellspacing='0' border='0'>");
                    sb.Append("<colgroup>");
                    sb.Append(" <col style='width:80px;'/>");
                    sb.Append(" <col style='width:135px;'/>");
                    sb.Append(" <col style='width:70px;'/>");
                    sb.Append(" <col style='width:110px;'/>");
                    sb.Append(" <col style='width:30px;'/>");
                    sb.Append(" <col style='width:165px;'/>");
                    sb.Append("</colgroup>");
                    //Fila 1
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblDescripcion' class='label' style='color:#336699;'>Titulación:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append(docTit + docEx + "<nobr id='descripcion' class='NBR W400 label' onmouseover='TTip(event)' style='" + sColor + "margin-left:" + ((docTit + docEx != "") ? "4" : "0") + "px;'>" + dr["T019_DESCRIPCION"].ToString() + "</nobr>");
                    sb.Append("</td></tr>");
                    //Fila 2
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblTipo' class='label' style='color:#336699;'>Tipo:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='Tipo' class='label' style='" + sColor + "'>" + dr["T019_TIPO"].ToString() + "</label>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<label id='lblModalidad' class='label' style='color:#336699;'>Modalidad:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='Modalidad' class='label' style='" + sColor + "'>" + dr["T019_MODALIDAD"].ToString() + "</label>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<label id='lblTic' class='label' style='color:#336699;'>Tic:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='Tic' class='label' style='" + sColor + "'>" + dr["T019_TIC"].ToString() + "</label>");
                    sb.Append("</td></tr>");
                    //Fila 3
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblEspecialidad' class='label' style='color:#336699;'>Especialidad:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<label id='Especialidad' class='label' style='" + sColor + "'>" + dr["T012_ESPECIALIDAD"].ToString() + "</label>");
                    sb.Append("</td></tr>");
                    //Fila 4
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblCentro' class='label' style='color:#336699;'>Centro:</label>");
                    sb.Append("</td><td colspan='5'>");
                    sb.Append("<label id='Centro' class='label' style='" + sColor + "'>" + dr["T012_CENTRO"].ToString() + "</label>");
                    sb.Append("</td></tr>");
                    //Fila 5
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblFInicio' class='label' style='color:#336699;'>Fecha Inicio:</label>");
                    sb.Append("</td><td>");
                    sb.Append("<label id='FInicio' class='label' style='" + sColor + "'>" + dr["T012_INICIO"].ToString() + "</label>");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<label id='lblFFin' class='label' style='color:#336699;'>Fecha Fin:</label>");
                    sb.Append("</td><td>");
                    string fin = (dr["T012_FIN"].ToString() != "0") ? dr["T012_FIN"].ToString() : "";
                    sb.Append("<label id='FFin' class='label' style='" + sColor + "'>" + fin + "</label>");
                    sb.Append("</td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                    //Separador
                    sb.Append("<tr><td colspan='6' class='W390' style='border-bottom: 1px solid #336699;'></td></tr>");
                    sb.Append("</table>");
                }

                dr.Close();
                dr.Dispose();
                return sb.ToString();
            }

            public static void Reasignar(SqlTransaction tr, int idOrigen, int idDestino)
            {
                SUPER.DAL.TituloFicepi.Reasignar(tr, idOrigen, idDestino);
            }

            public static string Borrar(string sTitulos, int Idficepi, int IdficepiEntrada)
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
                        string[] aReg = Regex.Split(sTitulos, "##");
                        foreach (string oReg in aReg)
                        {
                            if (oReg == "") continue;
                            SUPER.DAL.TituloFicepi.Delete(tr, int.Parse(oReg));
                        }
                        if (Idficepi == IdficepiEntrada)
                            SUPER.DAL.Curriculum.ActualizadoCV(tr, Idficepi);

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

            public static void PonerDocumento(SqlTransaction tr, string sTipo, int t021_idtituloidioma, int t001_idficepi, 
                                              string sDenDoc, Nullable<long> idDoc)
            {
                if (sTipo=="TIT")
                    SUPER.DAL.TituloFicepi.UpdatearDoc(tr, t021_idtituloidioma, t001_idficepi, sDenDoc, idDoc);
                else
                    SUPER.DAL.TituloFicepi.UpdatearExpte(tr, t021_idtituloidioma, t001_idficepi, sDenDoc, idDoc);
            }

        #endregion
    }
}
