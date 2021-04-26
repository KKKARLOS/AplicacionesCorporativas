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
    /// Descripción breve de Curso
    /// </summary>
    public partial class Curso
    {
        #region Propiedades y Atributos

        private byte[] _T575_DOC;
        public byte[] T575_DOC
        {
            get { return _T575_DOC; }
            set { _T575_DOC = value; }
        }

        private string _T575_NDOC;
        public string T575_NDOC
        {
            get { return _T575_NDOC; }
            set { _T575_NDOC = value; }
        }

        private char _T839_IDESTADO;
        public char T839_IDESTADO
        {
            get { return _T839_IDESTADO; }
            set { _T839_IDESTADO = value; }
        }

        private string _T574_TITULO;
        public string T574_TITULO
        {
            get { return _T574_TITULO; }
            set { _T574_TITULO = value; }
        }

        private DateTime? _T574_FINICIO;
        public DateTime? T574_FINICIO
        {
            get { return _T574_FINICIO; }
            set { _T574_FINICIO = value; }
        }

        private DateTime? _T574_FFIN;
        public DateTime? T574_FFIN
        {
            get { return _T574_FFIN; }
            set { _T574_FFIN = value; }
        }

        private string _T574_CONTENIDO;
        public string T574_CONTENIDO
        {
            get { return _T574_CONTENIDO; }
            set { _T574_CONTENIDO = value; }
        }

        private int? _T172_IDPAIS;
        public int? T172_IDPAIS
        {
            get { return _T172_IDPAIS; }
            set { _T172_IDPAIS = value; }
        }
        private int? _T173_IDPROVINCIA;
        public int? T173_IDPROVINCIA
        {
            get { return _T173_IDPROVINCIA; }
            set { _T173_IDPROVINCIA = value; }
        }
        private int _T574_TIPO;
        public int T574_TIPO
        {
            get { return _T574_TIPO; }
            set { _T574_TIPO = value; }
        }

        private int? _T036_IDCODENTORNO;
        public int? T036_IDCODENTORNO
        {
            get { return _T036_IDCODENTORNO; }
            set { _T036_IDCODENTORNO = value; }
        }

        private string _T575_OBSERVA;
        public string T575_OBSERVA
        {
            get { return _T575_OBSERVA; }
            set { _T575_OBSERVA = value; }
        }

        private string _T575_MOTIVO;
        public string T575_MOTIVO
        {
            get { return _T575_MOTIVO; }
            set { _T575_MOTIVO = value; }
        }

        private string _T843_MOTIVO;
        public string T843_MOTIVO
        {
            get { return _T843_MOTIVO; }
            set { _T843_MOTIVO = value; }
        }

        private double _T574_HORAS;
        public double T574_HORAS
        {
            get { return _T574_HORAS; }
            set { _T574_HORAS = value; }
        }

        private string _DESPROVEEDOR;
        public string DESPROVEEDOR
        {
            get { return _DESPROVEEDOR; }
            set { _DESPROVEEDOR = value; }
        }

        private byte _T574_TECNICOC;
        public byte T574_TECNICOC
        {
            get { return _T574_TECNICOC; }
            set { _T574_TECNICOC = value; }
        }
        private int _T574_ORIGEN;
        public int T574_ORIGEN
        {
            get { return _T574_ORIGEN; }
            set { _T574_ORIGEN = value; }
        }

        private int _T574_IDCURSO;
        public int T574_IDCURSO
        {
            get { return _T574_IDCURSO; }
            set { _T574_IDCURSO = value; }
        }

        private int _T001_IDFICEPI;
        public int T001_IDFICEPI
        {
            get { return _T001_IDFICEPI; }
            set { _T001_IDFICEPI = value; }
        }

        private byte[] _T580_DOC;
        public byte[] T580_DOC
        {
            get { return _T580_DOC; }
            set { _T580_DOC = value; }
        }

        private string _T580_NDOC;
        public string T580_NDOC
        {
            get { return _T580_NDOC; }
            set { _T580_NDOC = value; }
        }
        private string _T50_OBSERVA;
        public string T580_OBSERVA
        {
            get { return _T50_OBSERVA; }
            set { _T50_OBSERVA = value; }
        }

        private bool _EsOnline;
        public bool EsOnline
        {
            get { return _EsOnline; }
            set { _EsOnline = value; }
        }

        private bool _VisibleCV;
        public bool VisibleCV
        {
            get { return _VisibleCV; }
            set { _VisibleCV = value; }
        }
        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        private string _Profesional;
        public string Profesional
        {
            get { return _Profesional; }
            set { _Profesional = value; }
        }
        private bool _BDOC;
        public bool BDOC
        {
            get { return _BDOC; }
            set { _BDOC = value; }
        }
        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        #endregion

        #region Constructor

        public Curso()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #endregion

        #region Metodos

        public static void Grabar(string t574_titulo, int t574_origen, Nullable<int> t574_tipo, DateTime? t574_finicio, DateTime? t574_ffin,
                                    Nullable<int> t173_idprovincia, float t574_horas, string t574_direccion, Nullable<int> t036_idcodentorno, 
                                    string t574_contenido, string t575_observaciones, int t574_tecnicoc, int t001_idficepi,  
                                    string sNombre, char t839_idestado, string t575_motivo, int t001_idficepiu, Nullable<int> t576_idcriteriom,
                                    bool bVisibleCV, Nullable<long> t2_iddocumento, string sEsMiCV)

        {
            int nFilasModificadas = 0;
            string sResul = "";
            bool bErrorControlado = false;

            #region Inicio Transacción
            int idCurso;
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
             #region Grabar
                //Existe Proveedor -->T574_IDCURSO T315_IDPROVEEDOR T315_ESTADO=1
                idCurso = DAL.Curso.Insert(tr, t574_titulo, t574_origen, t574_tipo, t574_finicio, t574_ffin, t173_idprovincia, t574_horas, 
                                          t574_direccion, t036_idcodentorno, t574_contenido, t574_tecnicoc, t001_idficepi, t001_idficepiu,
                                          t576_idcriteriom);
                nFilasModificadas = DAL.Curso.InsertAsis(tr, idCurso, t575_observaciones, t001_idficepi, sNombre, t839_idestado,
                                                         t575_motivo, t001_idficepiu, bVisibleCV, t2_iddocumento);
                
             #endregion

            if (nFilasModificadas == 0)
            {
                sResul = "Fila no actualizada";
                bErrorControlado = true;
                throw (new Exception(sResul));
            }

            if (sEsMiCV == "S" && (t839_idestado.ToString() == "O" || t839_idestado.ToString() == "P")) DAL.Curriculum.ActualizadoCV(tr, t001_idficepi);

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

        public static void GrabarImp(string t574_titulo, int t574_origen, Nullable<int> t574_tipo, DateTime? t574_finicio, DateTime? t574_ffin,
                                     Nullable<int> t173_idprovincia, float t574_horas, string t574_direccion, Nullable<int> t036_idcodentorno, 
                                     string t574_contenido, string t580_observaciones, int t574_tecnicoc, int t001_idficepi, 
                                     string sNombre, char t839_idestado, string t843_motivo, int t001_idficepiu, Nullable<int> t576_idcriteriom,
                                     bool bVisibleCV, Nullable<long> t2_iddocumento, string sEsMiCV)
        {
            int nFilasModificadas = 0;
            string sResul = "";
            bool bErrorControlado = false;

            #region Inicio Transacción
            int idCurso;
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
                #region Grabar
                //Existe Proveedor -->T574_IDCURSO T315_IDPROVEEDOR T315_ESTADO=1
                idCurso = DAL.Curso.Insert(tr, t574_titulo, t574_origen, t574_tipo, t574_finicio, t574_ffin, t173_idprovincia, t574_horas, 
                                           t574_direccion, t036_idcodentorno, t574_contenido, t574_tecnicoc, t001_idficepi, t001_idficepiu, 
                                           t576_idcriteriom);

                nFilasModificadas = DAL.Curso.InsertCurMonitor(tr, idCurso, t580_observaciones, t001_idficepi, sNombre, t839_idestado,
                                                                t843_motivo, t001_idficepiu, bVisibleCV, t2_iddocumento);
                //nFilasModificadas = DAL.Curso.InsertCurMonitor(tr, t001_idficepi, idCurso, t839_idestado, t843_motivo, t001_idficepiu);
                
                #endregion

                if (nFilasModificadas == 0)
                {
                    sResul = "Fila no actualizada";
                    bErrorControlado = true;
                    throw (new Exception(sResul));
                }

                if (sEsMiCV == "S" && (t839_idestado.ToString() == "O" || t839_idestado.ToString() == "P")) DAL.Curriculum.ActualizadoCV(tr, t001_idficepi);

                Conexion.CommitTransaccion(tr);

            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al actualizar el curso.", ex);
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

        public static void Update(int t574_idcurso, string t574_titulo, int t574_origen, Nullable<int> t574_tipo, DateTime? t574_finicio,
                                  DateTime? t574_ffin, Nullable<int> t173_idprovincia, float t574_horas, string t574_direccion, 
                                  Nullable<int> t036_idcodentorno, string t574_contenido, string t575_observaciones, int t574_tecnicoc, 
                                  int t001_idficepi, string sNombre, bool cambioDoc, char t839_idestado, string t575_motivo,
                                  int t001_idficepiu, char t839_idestado_ini, Nullable<int> t576_idcriteriom, bool bVisibleCV,
                                  Nullable<long> t2_iddocumento, string sEsMiCV)
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

            try
            {
                #region Update
                //Existe Proveedor -->T574_IDCURSO T315_IDPROVEEDOR T315_ESTADO=1
                DAL.Curso.Update(null, t574_idcurso, t574_titulo, t574_origen, t574_tipo, t574_finicio, t574_ffin, t173_idprovincia, t574_horas,
                                 t574_direccion, t036_idcodentorno, t574_contenido, t574_tecnicoc, t001_idficepi, t001_idficepiu, t576_idcriteriom);
                nFilasModificadas = DAL.Curso.UpdateAsis(tr, t574_idcurso, t575_observaciones, t001_idficepi, sNombre, t839_idestado,
                                                         cambioDoc, t575_motivo, t001_idficepiu, t839_idestado_ini, bVisibleCV, t2_iddocumento);

                #endregion

                if (nFilasModificadas == 0)
                {
                    sResul = "Fila no actualizada";
                    bErrorControlado = true;
                    throw (new Exception(sResul));
                }

                if (sEsMiCV == "S" && (t839_idestado.ToString() == "O" || t839_idestado.ToString() == "P")) DAL.Curriculum.ActualizadoCV(tr, t001_idficepi);

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

        public static void Update(int t574_idcurso, int t580_idcurmonitor, string t574_titulo, int t574_origen, Nullable<int> t574_tipo,
                                DateTime? t574_finicio, DateTime? t574_ffin, Nullable<int> t173_idprovincia, float t574_horas, 
                                string t574_direccion, Nullable<int> t036_idcodentorno, string t574_contenido, string t580_observaciones, 
                                int t574_tecnicoc, int t001_idficepi, string sNombre, bool cambioDoc, char t839_idestado,
                                string t843_motivo, int t001_idficepiu, char t839_idestado_ini, Nullable<int> t576_idcriteriom, bool bVisibleCV,
                                Nullable<long> t2_iddocumento, string sEsMiCV)
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

            try
            {
                #region Update
                //Existe Proveedor -->T574_IDCURSO T315_IDPROVEEDOR T315_ESTADO=1
                DAL.Curso.Update(null, t574_idcurso, t574_titulo, t574_origen, t574_tipo, t574_finicio, t574_ffin, t173_idprovincia,
                                 t574_horas, t574_direccion, t036_idcodentorno, t574_contenido, t574_tecnicoc, t001_idficepi,
                                 t001_idficepiu, t576_idcriteriom);
                //nFilasModificadas = DAL.Curso.UpdateCurMonitor(tr, t580_idcurmonitor, t001_idficepi, t574_idcurso, t839_idestado, t843_motivo, t001_idficepiu, t839_idestado_ini);
                nFilasModificadas = DAL.Curso.UpdateCurMonitor(tr, t580_idcurmonitor, t574_idcurso, t580_observaciones, t001_idficepi,
                                                    sNombre, t839_idestado, cambioDoc, t843_motivo, t001_idficepiu, t839_idestado_ini,
                                                    bVisibleCV, t2_iddocumento);
                
                #endregion

                if (nFilasModificadas == 0)
                {
                    sResul = "Fila no actualizada";
                    bErrorControlado = true;
                    throw (new Exception(sResul));
                }

                if (sEsMiCV == "S" && (t839_idestado.ToString() == "O" || t839_idestado.ToString() == "P")) DAL.Curriculum.ActualizadoCV(tr, t001_idficepi);

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

        public static List<ElementoLista> obtenerCboProvincia()
        {
            SqlDataReader dr = DAL.Curso.obtenerProvincia(null);
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["t173_idprovincia"].ToString(), dr["T173_DENOMINACION"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        
        //public static List<ElementoLista> obtenerTipoCurso()
        //{
        //    SqlDataReader dr = DAL.Curso.obtenerTipoCurso();
        //    List<ElementoLista> oLista = new List<ElementoLista>();
        //    string[] strTipo = null;
        //    if (dr.Read())
        //    {
        //        strTipo = Regex.Split(dr["TIPOCURSO"].ToString(), "@#@");
        //    }
        //    foreach (string oFun in strTipo)
        //    {
        //        string[] aValores = Regex.Split(oFun, "#/#");
        //        oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
        //    }
        //    dr.Close();
        //    dr.Dispose();
        //    return oLista;
        //}

        public static string MiCVFormacionRecibida(int idFicepi)
        {
            SqlDataReader dr = DAL.Curso.MiCVFormacionRecibida(null, idFicepi);
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<table id='tblDatosFormacionRecibida' style='width:930px;' class='MA'>
                        <colgroup>
                            <col style='width:25px;' />
                            <col style='width:20px;' />
                            <col style='width:320px;'/>
                            <col style='width:50px;' />
                            <col style='width:90px;' />
                            <col style='width:90px;' />
                            <col style='width:215px;' />
                            <col style='width:60px;' />
                            <col style='width:60px;' />
                        </colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id=" + dr["T574_IDCURSO"].ToString() + " est='" + dr["T839_IDESTADO"].ToString() + "'");
                sb.Append(" ori='" + dr["t574_origen"].ToString() + "' onclick='ms(this);'");
                sb.Append(" ondblclick='AnadirCurso(" + dr["T574_IDCURSO"].ToString() + ",\"" + dr["T839_IDESTADO"].ToString() + "\");'>");
                sb.Append("<td>");
                if (dr["t575_fPeticionBorrado"].ToString() != "")
                    sb.Append("<img src='../../../images/imgPetBorrado.png' title='Pdte de eliminar' />");
                else
                {
                    switch (dr["T839_IDESTADO"].ToString())
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
                }
                sb.Append("</td>");
                sb.Append("<td>");// class='" + estilo + "'
                if (dr["bDoc"].ToString() == "1")
                {
                    sb.Append("<img src=\"../../../images/imgTitulo.png\" width='16px' height='16px' style='cursor:pointer;' ");
                    sb.Append("onclick=\"descargar('CVTCUR',\'" + dr["T574_IDCURSO"].ToString() + "datos" + dr["T001_IDFICEPI"].ToString() + "\');\" ");
                    sb.Append("style='vertical-align:bottom;' title='Descargar acreditación del título' >");
                }
                sb.Append("</td>");
                sb.Append("<td><label class='NBR W320' onmouseover='TTip(event)'>" + dr["T574_TITULO"].ToString() + " </label></td>");
                sb.Append("<td align='right'><label style='margin-right:15px'>" + decimal.Round(decimal.Parse(dr["T574_HORAS"].ToString()), 2).ToString() + " </label></td>");
                sb.Append("<td><label class='NBR W85' onmouseover='TTip(event)'>" + dr["T173_DENOMINACION"].ToString() + " </label></td>");
                sb.Append("<td><label class='NBR W85' onmouseover='TTip(event)'>" + dr["T172_DENOMINACION"].ToString() + " </label></td>");
                sb.Append("<td><label class='NBR W215' onmouseover='TTip(event)'>" + dr["PROVEEDOR"].ToString() + "</label></td>");
                sb.Append("<td><label>" + ((dr["T574_FINICIO"].ToString() == "") ? "" : DateTime.Parse(dr["T574_FINICIO"].ToString()).ToShortDateString()) + " </label></td>");
                sb.Append("<td><label>" + ((dr["T574_FFIN"].ToString() == "") ? "" : DateTime.Parse(dr["T574_FFIN"].ToString()).ToShortDateString()) + " </label></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static string MiCVFormacionImpartida(int idFicepi)
        {
            SqlDataReader dr = DAL.Curso.MiCVFormacionImpartida(null, idFicepi);
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<table id='tblDatosFormacionImpartida' style='width:930px;' class='MA'>
                        <colgroup>
                            <col style='width:25px;' />
                            <col style='width:20px;' />
                            <col style='width:320px;'/>
                            <col style='width:50px;' />
                            <col style='width:90px;' />
                            <col style='width:90px;' />
                            <col style='width:215px;' />
                            <col style='width:60px;' />
                            <col style='width:60px;' />
                        </colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id=" + dr["T580_IDCURMONITOR"].ToString() + " est='" + dr["t839_idestado"].ToString() + "'" +
                          "ori='" + dr["t574_origen"].ToString() + "' style='height:20px;'" +
                          " onclick='ms(this);' ondblclick='AnadirCursoImpartido(" + dr["T580_IDCURMONITOR"].ToString() + ",\"V\");'>");
                sb.Append("<td>");
                if (dr["t580_fPeticionBorrado"].ToString() != "")
                    sb.Append("<img src='../../../images/imgPetBorrado.png' title='Pdte de eliminar' />");
                else
                {
                    switch (dr["T839_IDESTADO"].ToString())
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
                }
                sb.Append("</td>");
                sb.Append("<td>");// class='" + estilo + "'
                if (dr["bDoc"].ToString() == "1")
                {
                    sb.Append("<img src=\"../../../images/imgTitulo.png\" width='16px' height='16px' style='cursor:pointer;' ");
                    sb.Append("onclick=\"descargar('CVTCUR_IMP',\'" + dr["T574_IDCURSO"].ToString() + "datos" + dr["T001_IDFICEPI"].ToString() + "\');\" ");
                    sb.Append("style='vertical-align:bottom;' title='Descargar acreditación del título' >");
                }
                sb.Append("</td>");
                //sb.Append("<td><nobr class='NBR W290' onmouseover='TTip(event)'><label class='MA' ondblclick='AnadirCurMonitor(" + dr["T574_IDCURSO"].ToString() + ",\"" + dr["T839_IDESTADO"].ToString() + "\");'>" + dr["T574_TITULO"].ToString() + " </label></nobr></td>");
                sb.Append("<td><label class='NBR W320' onmouseover='TTip(event)'>" + dr["T574_TITULO"].ToString() + " </label></td>");
                sb.Append("<td align='right'><label style='margin-right:15px'>" + decimal.Round(decimal.Parse(dr["T574_HORAS"].ToString()), 2).ToString() + " </label></td>");
                sb.Append("<td><label class='NBR W90' onmouseover='TTip(event)'>" + dr["T173_DENOMINACION"].ToString() + " </label></td>");
                sb.Append("<td><label class='NBR W90' onmouseover='TTip(event)'>" + dr["T172_DENOMINACION"].ToString() + " </label></td>");
                sb.Append("<td><label class='NBR W215' onmouseover='TTip(event)'>" + dr["PROVEEDOR"].ToString() + "</label></td>");
                sb.Append("<td><label>" + ((dr["T574_FINICIO"].ToString() == "") ? "" : DateTime.Parse(dr["T574_FINICIO"].ToString()).ToShortDateString()) + " </label></td>");
                sb.Append("<td><label>" + ((dr["T574_FFIN"].ToString() == "") ? "" : DateTime.Parse(dr["T574_FFIN"].ToString()).ToShortDateString()) + " </label></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static Curso SelectDoc(SqlTransaction tr, int t574_idcurso, int t001_idficepi)
        {
            Curso o = new Curso();
            SqlDataReader dr = DAL.Curso.SelectDoc(t574_idcurso, t001_idficepi);
            if (dr.Read())
            {

                if (dr["T575_NDOC"] != DBNull.Value)
                    o.T575_NDOC = (string)dr["T575_NDOC"];
                //if (dr["T575_DOC"] != DBNull.Value)
                //    o.T575_DOC = (byte[])dr["T575_DOC"];
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            dr.Close();
            dr.Dispose();

            return o;
        }
        public static Curso SelectDoc2(SqlTransaction tr, int t574_idcurso, int t001_idficepi)
        {
            Curso o = new Curso();
            SqlDataReader dr = DAL.Curso.SelectDoc2(t574_idcurso, t001_idficepi);
            if (dr.Read())
            {

                if (dr["T580_NDOC"] != DBNull.Value)
                    o.T580_NDOC = (string)dr["T580_NDOC"];
                //if (dr["T580_DOC"] != DBNull.Value)
                //    o.T580_DOC = (byte[])dr["T580_DOC"];
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            dr.Close();
            dr.Dispose();

            return o;
        }

        public static void PonerDocumento(SqlTransaction tr, string sTipo, int idCurso, int t001_idficepi, string sDenDoc, Nullable<long> t2_iddocumento)
        {
            SUPER.DAL.Curso.UpdatearDoc(tr, sTipo, idCurso, t001_idficepi, sDenDoc, t2_iddocumento);
        }
        public static Curso Detalle(int idCurso,int idFicepi)
        {
            Curso o = new Curso();
            SqlDataReader dr = DAL.Curso.Detalle(null, idCurso, idFicepi);

            if (dr.Read())
            {
                if (dr["T839_IDESTADO"] != DBNull.Value)
                    o.T839_IDESTADO = char.Parse(dr["T839_IDESTADO"].ToString());
                //if (dr["T575_DOC"] != DBNull.Value)
                //    o.T575_DOC = (byte[])dr["T575_DOC"];
                if (dr["T575_NDOC"] != DBNull.Value)
                    o.T575_NDOC = (string)dr["T575_NDOC"];
                if (dr["T574_TITULO"] != DBNull.Value)
                    o.T574_TITULO = (string)dr["T574_TITULO"];
                if (dr["T574_FINICIO"] != DBNull.Value)
                    o.T574_FINICIO = ((DateTime)dr["T574_FINICIO"]);
                if (dr["T574_FFIN"] != DBNull.Value)
                    o.T574_FFIN = ((DateTime)dr["T574_FFIN"]);
                if (dr["T574_CONTENIDO"] != DBNull.Value)
                    o.T574_CONTENIDO = (string)dr["T574_CONTENIDO"];
                if (dr["T172_IDPAIS"] != DBNull.Value)
                    o.T172_IDPAIS = int.Parse(dr["T172_IDPAIS"].ToString());
                if (dr["T173_IDPROVINCIA"] != DBNull.Value)
                    o.T173_IDPROVINCIA = int.Parse(dr["T173_IDPROVINCIA"].ToString());                
                if (dr["T574_TIPO"] != DBNull.Value)
                    o.T574_TIPO = int.Parse(dr["T574_TIPO"].ToString());
                if (dr["T036_IDCODENTORNO"] != DBNull.Value)
                    o.T036_IDCODENTORNO = int.Parse(dr["T036_IDCODENTORNO"].ToString());
                if (dr["T575_OBSERVA"] != DBNull.Value)
                    o.T575_OBSERVA = (string)dr["T575_OBSERVA"];
                if (dr["T575_MOTIVO"] != DBNull.Value)
                    o.T575_MOTIVO = (string)dr["T575_MOTIVO"];
                if (dr["T574_HORAS"] != DBNull.Value)
                    o.T574_HORAS = double.Parse(dr["T574_HORAS"].ToString());
                if (dr["DESPROVEEDOR"] != DBNull.Value)
                    o.DESPROVEEDOR = (string)dr["DESPROVEEDOR"];
                if (dr["T574_TECNICOC"] != DBNull.Value)
                    o.T574_TECNICOC = (dr["T574_TECNICOC"].ToString() == "True") ? byte.Parse("1") : byte.Parse("0");
                if (dr["T574_ORIGEN"] != DBNull.Value)
                    o.T574_ORIGEN = int.Parse(dr["T574_ORIGEN"].ToString());
                o.EsOnline = false;
                if (dr["T574_ORIGEN"] != DBNull.Value)
                    if (dr["EsOnline"].ToString()=="1")
                        o.EsOnline = true;
                o.VisibleCV = (bool)dr["t575_visibleCV"];
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            else
            {

                throw (new NullReferenceException("No se ha obtenido ningún dato del Curso"));
            }
            dr.Close();
            dr.Dispose();
            return o;
        }

        public static Curso DetalleMonitor(int idCurMonitor)
        {
            Curso o = new Curso();
            SqlDataReader dr = DAL.Curso.DetalleMonitor(null, idCurMonitor);

            if (dr.Read())
            {
                if (dr["T001_IDFICEPI"] != DBNull.Value)
                    o.T001_IDFICEPI = int.Parse(dr["T001_IDFICEPI"].ToString());
                if (dr["T574_IDCURSO"] != DBNull.Value)
                    o.T574_IDCURSO = int.Parse(dr["T574_IDCURSO"].ToString());
                if (dr["T839_IDESTADO"] != DBNull.Value)
                    o.T839_IDESTADO = char.Parse(dr["T839_IDESTADO"].ToString());
                //if (dr["T580_DOC"] != DBNull.Value)
                //    o.T580_DOC = (byte[])dr["T580_DOC"];
                if (dr["T580_NDOC"] != DBNull.Value)
                    o.T580_NDOC = (string)dr["T580_NDOC"];
                if (dr["T574_TITULO"] != DBNull.Value)
                    o.T574_TITULO = (string)dr["T574_TITULO"];
                if (dr["T574_FINICIO"] != DBNull.Value)
                    o.T574_FINICIO = ((DateTime)dr["T574_FINICIO"]);
                if (dr["T574_FFIN"] != DBNull.Value)
                    o.T574_FFIN = ((DateTime)dr["T574_FFIN"]);
                if (dr["T574_CONTENIDO"] != DBNull.Value)
                    o.T574_CONTENIDO = (string)dr["T574_CONTENIDO"];
                if (dr["T172_IDPAIS"] != DBNull.Value)
                    o.T172_IDPAIS = int.Parse(dr["T172_IDPAIS"].ToString());
                if (dr["T173_IDPROVINCIA"] != DBNull.Value)
                    o.T173_IDPROVINCIA = int.Parse(dr["T173_IDPROVINCIA"].ToString()); 
                if (dr["T574_TIPO"] != DBNull.Value)
                    o.T574_TIPO = int.Parse(dr["T574_TIPO"].ToString());
                if (dr["T036_IDCODENTORNO"] != DBNull.Value)
                    o.T036_IDCODENTORNO = int.Parse(dr["T036_IDCODENTORNO"].ToString());
                if (dr["T580_OBSERVA"] != DBNull.Value)
                    o.T580_OBSERVA = (string)dr["T580_OBSERVA"];
                if (dr["T843_MOTIVO"] != DBNull.Value)
                    o.T843_MOTIVO = (string)dr["T843_MOTIVO"];
                if (dr["T574_HORAS"] != DBNull.Value)
                    o.T574_HORAS = double.Parse(dr["T574_HORAS"].ToString());
                if (dr["DESPROVEEDOR"] != DBNull.Value)
                    o.DESPROVEEDOR = (string)dr["DESPROVEEDOR"];
                if (dr["T574_TECNICOC"] != DBNull.Value)
                    o.T574_TECNICOC = (dr["T574_TECNICOC"].ToString() == "True") ? byte.Parse("1") : byte.Parse("0");
                if (dr["T574_ORIGEN"] != DBNull.Value)
                    o.T574_ORIGEN = int.Parse(dr["T574_ORIGEN"].ToString());
                o.EsOnline = false;
                if (dr["T574_ORIGEN"] != DBNull.Value)
                    if (dr["EsOnline"].ToString() == "1")
                        o.EsOnline = true;
                o.VisibleCV = (bool)dr["t580_visibleCV"];
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            else
            {

                throw (new NullReferenceException("No se ha obtenido ningún dato del Curso"));
            }
            dr.Close();
            dr.Dispose();
            return o;
        }

        public static string MiCVFormacionRecibidaHTML(int idFicepi, int bFiltros, string lft036_idcodentorno)
        {
            SqlDataReader dr = DAL.Curso.MiCVFormacionRecibidaHTML(null, idFicepi, bFiltros, lft036_idcodentorno);
            StringBuilder sb = new StringBuilder();
            //Para poner en rojo lo que no esté validado
            string sColor = "";

            if (dr.HasRows)
            {
                sb.Append("<table style='margin-left:40px; margin-top:25px; width:400px;'>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblCurRec' class='titulo2'>Recibidas</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
            }
            while (dr.Read())
            {
                //Para poner en rojo lo que no esté validado
                sColor = "";
                if (dr["ESTADO"].ToString() == "1")
                    sColor = "color:Red;";

                string docTit = "";
                if (dr["bDoc"].ToString() == "1")
                {
                    docTit = "<img src=\"../../../images/imgTitulo.png\" width='16px' height='16px' style='cursor:pointer;' ";
                    docTit += "onclick=\"descargar('CVTCUR',\'" + dr["T574_IDCURSO"].ToString() + "datos" + idFicepi.ToString() + "\');\" ";
                    docTit += "style='vertical-align:bottom;' title='Descargar acreditación del título' >";
                }

                sb.Append("<table style='width:590px; margin-left:60px; margin-top:15px;' cellpadding='1' cellspacing='0' border='0'>");
                sb.Append("<colgroup>");
                sb.Append(" <col style='width:80px;'/>");
                sb.Append(" <col style='width:135px;'/>");
                sb.Append(" <col style='width:70px;'/>");
                sb.Append(" <col style='width:120px;'/>");
                sb.Append(" <col style='width:70px;'/>");
                sb.Append(" <col style='width:115px;'/>");
                sb.Append("</colgroup>");
                //Fila 1
                sb.Append("<tr><td>");
                sb.Append("<label id='lblDenominacion' class='label' style='color:#336699;'>Curso:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append(docTit + "<nobr id='Denominacion' class='NBR W400 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:" + ((docTit != "") ? "4" : "0") + "px;'>" + dr["T574_TITULO"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 2
                sb.Append("<tr><td>");
                sb.Append("<label id='lblTipo' class='label' style='color:#336699;'>Tipo:</label>");
                sb.Append("</td><td colspan='3'>");
                sb.Append("<nobr id='Tipo' class='NBR W260 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["TIPO"].ToString() + "</nobr>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblModalidad' class='label' style='color:#336699;'>Modalidad:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='Modalidad' class='NBR W80 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["MODALIDAD"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 3
                sb.Append("<tr><td>");
                sb.Append("<label id='lblProveedor' class='label' style='color:#336699;'>Centro:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='Proveedor' class='NBR W400 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["PROVEEDOR"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 4
                sb.Append("<tr><td>");
                sb.Append("<label id='lblEntornoTecno' class='label' style='color:#336699;'>Ent.Tec\\Fun.:</label>");
                sb.Append("</td><td colspan='3'>");
                sb.Append("<nobr id='EntornoTecno' class='NBR W260 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T036_DESCRIPCION"].ToString() + "</nobr>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblProvincia' class='label' style='color:#336699;'>Provincia:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='Provincia' class='NBR W80 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T173_DENOMINACION"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 5
                sb.Append("<tr><td>");
                sb.Append("<label id='lblHoras' class='label' style='color:#336699;'>Horas:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='Horas' class='label' style='" + sColor + "'>" + dr["T574_HORAS"].ToString() + "</label>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblFInicio' class='label' style='color:#336699;'>Fecha Inicio:</label>");
                sb.Append("</td><td style='width:90px;'>");
                sb.Append("<label id='FInicio' class='label' style='" + sColor + "'>" + dr["T574_FINICIO"].ToString() + "</label>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblFFin' class='label' style='color:#336699;'>Fecha Fin:</label>");
                sb.Append("</td><td style='width:85px;'>");
                sb.Append("<label id='FFin' class='label' style='" + sColor + "'>" + dr["T574_FFIN"].ToString() + "</label>");
                sb.Append("</td></tr>");
                //Fila 6
                sb.Append("<tr><td>");
                sb.Append("<label id='lblContenido' class='label' style='color:#336699;'>Contenido:</label>");
                sb.Append("</td></tr><tr><td></td><td colspan='5'>");
                sb.Append("<asp:TextBox type='text' ID='ContenidoCurRec' SkinID='multi' TextMode='MultiLine' style='"+sColor+" overflow-y:auto; overflow-x:hidden; width:510px; resize:none;' Rows='4' ReadOnly='true' BackColor='Transparent' BorderColor='Transparent'>" + dr["T574_CONTENIDO"].ToString().Replace("\r\n", "<br>") + "</asp:TextBox>");
                sb.Append("</td></tr>");
                
                
                //Separador
                sb.Append("<tr><td colspan='6' style='border-bottom: 1px solid #336699;'></td></tr>");
                
                sb.Append("</table>");
            }

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string MiCVFormacionImpartidaHTML(int idFicepi, int bFiltros, string lft036_idcodentorno)
        {
            SqlDataReader dr = DAL.Curso.MiCVFormacionImpartidaHTML(null, idFicepi, bFiltros, lft036_idcodentorno);
            StringBuilder sb = new StringBuilder();
            //Para poner en rojo lo que no esté validado
            string sColor = "";

            if (dr.HasRows)
            {
                sb.Append("<table style='margin-left:40px; margin-top:25px; width:400px;'>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblCurImp1' class='titulo2'>Impartidas</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
            }
            while (dr.Read())
            {
                //Para poner en rojo lo que no esté validado
                sColor = "";
                if (dr["ESTADO"].ToString() == "1")
                    sColor = "color:Red;";

                string docTit = "";
                if (dr["bDoc"].ToString() == "1")
                {
                    docTit = "<img src=\"../../../images/imgTitulo.png\" width='16px' height='16px' style='cursor:pointer;' ";
                    docTit += "onclick=\"descargar('CVTCUR_IMP',\'" + dr["T574_IDCURSO"].ToString() + "datos" + idFicepi.ToString() + "\');\" ";
                    docTit += "style='vertical-align:bottom;' title='Descargar acreditación del curso impartido' >";
                }

                sb.Append("<table style='width:590px; margin-left:60px; margin-top:15px;' cellpadding='1' cellspacing='0' border='0'>");
                sb.Append("<colgroup>");
                sb.Append(" <col style='width:80px;'/>");
                sb.Append(" <col style='width:135px;'/>");
                sb.Append(" <col style='width:70px;'/>");
                sb.Append(" <col style='width:120px;'/>");
                sb.Append(" <col style='width:70px;'/>");
                sb.Append(" <col style='width:115px;'/>");

                sb.Append("</colgroup>");
                //Fila 1
                sb.Append("<tr><td>");
                sb.Append("<label id='lblDenominacion' class='label' style='color:#336699;'>Curso:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append(docTit + "<nobr id='Denominacion' class='NBR W400 label' onmouseover='TTip(event)' style='"+sColor+" margin-left:" + ((docTit != "") ? "4" : "0") + "px;'>" + dr["T574_TITULO"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 2
                sb.Append("<tr><td>");
                sb.Append("<label id='lblTipo' class='label' style='color:#336699;'>Tipo:</label>");
                sb.Append("</td><td colspan='3'>");
                sb.Append("<nobr id='Tipo' class='NBR W260 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["TIPO"].ToString() + "</nobr>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblModalidad' class='label' style='color:#336699;'>Modalidad:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='Modalidad' class='NBR W80 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["MODALIDAD"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 3
                sb.Append("<tr><td>");
                sb.Append("<label id='lblProveedor' class='label' style='color:#336699;'>Centro:</label>");
                sb.Append("</td><td colspan='5'>");
                sb.Append("<nobr id='Proveedor' class='NBR W400 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["PROVEEDOR"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 4
                sb.Append("<tr><td>");
                sb.Append("<label id='lblEntornoTecno' class='label' style='color:#336699;'>Ent.Tec\\Fun.:</label>");
                sb.Append("</td><td colspan='3'>");
                sb.Append("<nobr id='EntornoTecno' class='NBR W260 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T036_DESCRIPCION"].ToString() + "</nobr>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblProvincia' class='label' style='color:#336699;'>Provincia:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='Provincia' class='NBR W80 label' onmouseover='TTip(event)' style='" + sColor + "'>" + dr["T173_DENOMINACION"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                //Fila 5
                sb.Append("<tr><td>");
                sb.Append("<label id='lblHoras' class='label' style='color:#336699;'>Horas:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='Horas' class='label' style='" + sColor + "'>" + dr["T574_HORAS"].ToString() + "</label>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblFInicio' class='label' style='color:#336699;'>Fecha Inicio:</label>");
                sb.Append("</td><td style='width:90px;'>");
                sb.Append("<label id='FInicio' class='label' style='" + sColor + "'>" + dr["T574_FINICIO"].ToString() + "</label>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label id='lblFFin' class='label' style='color:#336699;'>Fecha Fin:</label>");
                sb.Append("</td><td style='width:85px;'>");
                sb.Append("<label id='FFin' class='label' style='" + sColor + "'>" + dr["T574_FFIN"].ToString() + "</label>");
                sb.Append("</td></tr>");
                //Fila 6
                sb.Append("<tr><td>");
                sb.Append("<label id='lblContenido' class='label' style='color:#336699;'>Contenido:</label>");
                sb.Append("</td></tr><tr><td></td><td colspan='5'>");
                sb.Append("<asp:TextBox type='text' ID='ContenidoCurRec' SkinID='multi' TextMode='MultiLine' style='"+sColor+" overflow-y:auto; overflow-x:hidden; width:510px; resize:none;' Rows='4' ReadOnly='true' BackColor='Transparent' BorderColor='Transparent'>" + dr["T574_CONTENIDO"].ToString().Replace("\r\n", "<br>") + "</asp:TextBox>");
                sb.Append("</td></tr>");

                //Separador
                sb.Append("<tr><td colspan='6' style='border-bottom: 1px solid #336699;'></td></tr>");

                sb.Append("</table>");
            }

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string BorrarAsistente(int t001_idficepi, string sCursos, int IdficepiEntrada)
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
                    string[] aReg = Regex.Split(sCursos, "##");
                    foreach (string oReg in aReg)
                    {
                        if (oReg == "") continue;
                        SUPER.DAL.Curso.DeleteAsis(null, int.Parse(oReg), t001_idficepi);
                    }
                    if (t001_idficepi == IdficepiEntrada) SUPER.DAL.Curriculum.ActualizadoCV(tr, t001_idficepi);

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
        public static string BorrarMonitor(string sCursos, int Idficepi, int IdficepiEntrada)
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
                    string[] aReg = Regex.Split(sCursos, "##");
                    foreach (string oReg in aReg)
                    {
                        if (oReg == "") continue;
                        SUPER.DAL.Curso.DeleteMonitor(null, int.Parse(oReg));
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

        public static void PedirBorradoRecibido(int t001_idficepi, int idCurso, int t001_idficepi_petbor, string sMotivo, string sDatosCorreo)
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
                    SUPER.DAL.Curso.PedirBorradoRecibido(null, idCurso, t001_idficepi, t001_idficepi_petbor, sMotivo);
                    SUPER.Capa_Negocio.Correo.EnviarPetBorrado("FR", sDatosCorreo, sMotivo);
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
        public static void PedirBorradoImpartido(int t580_idcurmonitor, int t001_idficepi_petbor, string sMotivo, string sDatosCorreo)
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
                SUPER.DAL.Curso.PedirBorradoImpartido(null, t580_idcurmonitor, t001_idficepi_petbor, sMotivo);
                SUPER.Capa_Negocio.Correo.EnviarPetBorrado("FI", sDatosCorreo, sMotivo);
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

        public static void SetVisibilidadCV_Impartido(SqlTransaction tr, int idcurso, bool bVisibleCV)
        {
            SUPER.DAL.Curso.SetVisibilidadCV_Impartido(tr, idcurso, bVisibleCV);
        }
        public static void SetVisibilidadCV_Recibido(SqlTransaction tr, int idcurso, int idFicepi, bool bVisibleCV)
        {
            SUPER.DAL.Curso.SetVisibilidadCV_Recibido(tr, idcurso, idFicepi, bVisibleCV);
        }

        /// <summary>
        /// Obtiene una lista de los cursos(impartidos y recibidos) cuyo código se pasa en sListaIds 
        /// + los cursos(impartidos y recibidos) cuya denominación está en sListaDens
        /// y existe algun profesional de slFicepis que lo tiene
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="sListaIds"></param>
        /// <param name="sListaDens"></param>
        /// <returns></returns>
        public static List<Curso> GetListaPorProfesional(string slFicepis, string sListaIds, string sListaDens)
        {
            List<Curso> oLista = new List<Curso>();
            Curso oElem;
            SqlDataReader dr = SUPER.DAL.Curso.GetListaPorProfesional(null, slFicepis.Replace(",", "##"), sListaIds, sListaDens.Replace(";", "##"));
            while (dr.Read())
            {
                oElem = new Curso();
                oElem.T574_IDCURSO = short.Parse(dr["T574_IDCURSO"].ToString());
                oElem.T574_TITULO = dr["T574_TITULO"].ToString();
                oLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return oLista;
        }
        /// <summary>
        /// Obtiene una lista con los datos de los cursos de los profesionales que se pasan como parametros
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="slCodigos"></param>
        /// <returns></returns>
        public static List<Curso> GetDocsExportacion(string slFicepis, string slCodigos)
        {
            List<Curso> oLista = new List<Curso>();
            Curso oElem;
            SqlDataReader dr = SUPER.DAL.Curso.GetDocsExportacion(null, slFicepis.Replace(",", "##"), slCodigos.Replace(",", "##"));
            while (dr.Read())
            {
                oElem = new Curso();
                //oElem.IdFicepiCert = int.Parse(dr["t001_idficepi"].ToString());
                oElem.T574_IDCURSO = short.Parse(dr["T574_IDCURSO"].ToString());
                oElem.Profesional = dr["Profesional"].ToString();
                oElem.T574_TITULO = dr["T574_TITULO"].ToString();
                oElem.T575_NDOC = dr["NDOC"].ToString();
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