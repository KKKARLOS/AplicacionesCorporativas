using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.DAL;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de Titulacion
    /// </summary>
    public class Titulacion
    {
        #region Propiedades y Atributos

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

        private bool _T019_TITULREC;
        public bool T019_TITULREC
        {
            get { return _T019_TITULREC; }
            set { _T019_TITULREC = value; }
        }

        private byte[] _DOC;
        public byte[] DOC
        {
            get { return _DOC; }
            set { _DOC = value; }
        }

        private string _NDOC;
        public string NDOC
        {
            get { return _NDOC; }
            set { _NDOC = value; }
        }
        private string _NDOCEXPTE;
        public string NDOCEXPTE
        {
            get { return _NDOCEXPTE; }
            set { _NDOCEXPTE = value; }
        }

        private bool _T019_ESTADO;
        public bool T019_ESTADO
        {
            get { return _T019_ESTADO; }
            set { _T019_ESTADO = value; }
        }

        private byte _t019_tipo;
        public byte t019_tipo
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

        private int? _t001_idficepi_i;
        public int? t001_idficepi_i
        {
            get { return _t001_idficepi_i; }
            set { _t001_idficepi_i = value; }
        }

        private string _Creador;
        public string Creador
        {
            get { return _Creador; }
            set { _Creador = value; }
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
        public Titulacion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #endregion

        public static Titulacion SelectDoc(SqlTransaction tr, int idTitulacionFicepi, string tipo)
        {

            SqlDataReader dr = DAL.Titulacion.SelectDoc(tr, idTitulacionFicepi, tipo);
            Titulacion o = new Titulacion();
            if (dr.Read())
            {
                //if (dr["DOC"] != DBNull.Value)
                //    o.DOC = (byte[])dr["DOC"];
                if (dr["NDOC"] != DBNull.Value)
                    o.NDOC = dr["NDOC"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
                if (dr["t2_iddocumento_expdte"] != DBNull.Value)
                    o.t2_iddocumentoExpte = (long)dr["t2_iddocumento_expdte"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato"));
            }
            dr.Close();
            dr.Dispose();

            return o;


        }
        /// <summary>
        /// Obtiene los dos documentos asociados a una titulación académica-> Título y Expediente
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="idTitulacionFicepi"></param>
        /// <returns></returns>
        public static Titulacion SelectDocs(SqlTransaction tr, int idTitulacionFicepi)
        {

            SqlDataReader dr = DAL.Titulacion.SelectDocs(tr, idTitulacionFicepi);
            Titulacion o = new Titulacion();
            if (dr.Read())
            {
                if (dr["T012_NDOCTITULO"] != DBNull.Value)
                    o.NDOC = dr["T012_NDOCTITULO"].ToString();
                if (dr["t012_ndocexpdte"] != DBNull.Value)
                    o.NDOCEXPTE = dr["t012_ndocexpdte"].ToString();
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
                if (dr["t2_iddocumento_expdte"] != DBNull.Value)
                    o.t2_iddocumentoExpte = (long)dr["t2_iddocumento_expdte"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato"));
            }
            dr.Close();
            dr.Dispose();

            return o;
        }

        public static string Catalogo(string t019_descripcion, Nullable<byte> t019_estado, string sTipoBusqueda, bool bExcluirRH)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' class='MANO' style='width:910px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:440px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("<col style='width:70px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.Titulacion.Catalogo(null, t019_descripcion, t019_estado, sTipoBusqueda, bExcluirRH);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T019_IDCODTITULO"].ToString() + "' ");
                sb.Append("tipo='" + dr["T019_TIPO"].ToString() + "' ");
                sb.Append("modalidad='" + dr["T019_MODALIDAD"].ToString() + "' ");
                sb.Append("chkV='" + (((bool)dr["T019_ESTADO"])? "1":"0") + "' ");
                sb.Append("chkT='" +(((bool)dr["T019_TIC"])? "1":"0") + "' ");
                sb.Append("chk='" + (((bool)dr["t019_rh"]) ? "1" : "0") + "' ");
                sb.Append("bd='N'>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("<td>" + dr["T019_DESCRIPCION"].ToString() + "</td>");
                sb.Append("<td>" + dr["TIPO"].ToString() + "</td>");
                sb.Append("<td>" + dr["MODALIDAD"].ToString() + "</td>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();

        }

        /// <summary>
        /// Obtiene las Titulaciones independientemente de si están asignadas o no
        /// </summary>
        /// <param name="t019_descripcion"></param>
        /// <param name="t019_estado"></param>
        /// <returns></returns>
        public static string CatalogoSimple(string t019_descripcion, Nullable<byte> t019_estado, string sTipoBusqueda, bool bExcluirRH)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MANO' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.Titulacion.Catalogo(null, t019_descripcion, t019_estado, sTipoBusqueda, bExcluirRH);
            while (dr.Read())
            {
                //sb.Append("<tr id='" + dr["T019_IDCODTITULO"].ToString() + "' onclick='ms(this)' onmousedown='DD(event)' ondblclick='insertarItem(this);'>");
                sb.Append("<tr id='" + dr["T019_IDCODTITULO"].ToString() + "' onclick='ms(this)'>");
                sb.Append("<td><nobr class='NBR W430'>" + dr["T019_DESCRIPCION"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();

        }
        public static string tipoHTML()
        {
            SqlDataReader dr = DAL.TituloFicepi.obtenerTipos();
            StringBuilder sb = new StringBuilder();
            string[] strTipos = null;

            sb.Append("<select name='cboTipo' id='cboTipo' class='combo' style='width:120px;' onChange=\"mfa(this.parentNode.parentNode,'U')\">");
            if (dr.Read())
            {
                strTipos = Regex.Split(dr["TIPOS"].ToString(), "@#@");

                foreach (string oFun in strTipos)
                {
                    string[] aValores = Regex.Split(oFun, "#/#");
                    if (aValores[0] != "")
                        sb.Append("<option value=\"" + aValores[0].ToString() + "\">" + aValores[1].ToString() + "</option>");
                }
            }
            sb.Append("</select>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string modalidadHTML()
        {
            SqlDataReader dr = DAL.TituloFicepi.obtenerModalidades();
            StringBuilder sb = new StringBuilder();
            string[] strTipos = null;

            sb.Append("<select name='cboModalidad' id='cboModalidad' class='combo' style='width:120px;' onChange=\"mfa(this.parentNode.parentNode,'U')\">");
            if (dr.Read())
            {
                strTipos = Regex.Split(dr["MODALIDADES"].ToString(), "@#@");
                sb.Append("<option value=''></option>");
                foreach (string oFun in strTipos)
                {
                    string[] aValores = Regex.Split(oFun, "#/#");
                    if (aValores[0] != "")
                        sb.Append("<option value=\"" + aValores[0].ToString() + "\">" + aValores[1].ToString() + "</option>");
                }
            }
            sb.Append("</select>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string Grabar(string strDatos)
        {
            string sDen = "";
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
                #region Grabar
                short nAux = 0;
                string sElementosInsertados = "";
                string[] aTitulacion = Regex.Split(strDatos, "@fila@");
                foreach (string sTitulacion in aTitulacion)
                {
                    if (sTitulacion != "")
                    {
                        string[] aDatosTitulacion = Regex.Split(sTitulacion, "@dato@");
                        sAccion = aDatosTitulacion[0];
                        sDen = aDatosTitulacion[2].ToString();
                        switch (aDatosTitulacion[0])
                        {
                            case "I":
                                nAux = SUPER.BLL.Titulacion.Insertar(tr, aDatosTitulacion[2].ToString(),
                                                                    int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString()), 
                                                                    (aDatosTitulacion[6] == "1") ? true : false, 
                                                                    byte.Parse(aDatosTitulacion[3]), 
                                                                    (aDatosTitulacion[4] == "") ? null : (byte?)byte.Parse(aDatosTitulacion[4]),
                                                                    (aDatosTitulacion[5] == "1") ? true : false);

                                if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                                else sElementosInsertados += "//" + nAux.ToString();
                                break;
                            case "U":
                                DAL.Titulacion.Update(tr, int.Parse(aDatosTitulacion[1].ToString()), aDatosTitulacion[2].ToString(), 
                                                     (aDatosTitulacion[6]=="1")?true:false,
                                                     int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString()), 
                                                     byte.Parse(aDatosTitulacion[3]), 
                                                     (aDatosTitulacion[4] == "") ? null : (byte?)byte.Parse(aDatosTitulacion[4]), 
                                                     (aDatosTitulacion[5]=="1")?true:false, (aDatosTitulacion[7]=="1")?true:false);
                                break;
                            case "D":
                                //sDenominacionDelete = aDatosTitulacion[2];
                                DAL.Titulacion.Delete(tr, int.Parse(aDatosTitulacion[1].ToString()));
                                break;
                        }
                    }
                }

                Conexion.CommitTransaccion(tr);
                return "OK@#@" + sElementosInsertados;

                #endregion
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (Errores.EsErrorIntegridad(ex))
                {
                    if (sAccion == "D")
                        throw new Exception("ErrorControlado##EC##No se puede eliminar la titulación \"" + sDen + "\" por tener elementos relacionados.");
                    else
                        throw new Exception("ErrorControlado##EC##No se puede grabar la titulación \"" + sDen + "\" porque ya existe esa denominación.");
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

        public static Titulacion Select(int idTitulo)
        {
            SqlDataReader dr = DAL.Titulacion.Obtener(null, idTitulo);
            Titulacion o = new Titulacion();
            if (dr.Read())
            {
                if (dr["T019_IDCODTITULO"] != DBNull.Value)
                    o.T019_IDCODTITULO = short.Parse(dr["T019_IDCODTITULO"].ToString());
                if (dr["T019_DESCRIPCION"] != DBNull.Value)
                    o.T019_DESCRIPCION = dr["T019_DESCRIPCION"].ToString();
                if (dr["T019_TITULREC"] != DBNull.Value)
                    o.T019_TITULREC = bool.Parse(dr["T019_TITULREC"].ToString());
                if (dr["T019_ESTADO"] != DBNull.Value)
                    o.T019_ESTADO = (bool)dr["T019_ESTADO"];
                if (dr["t019_tipo"] != DBNull.Value)
                    o.t019_tipo = byte.Parse(dr["t019_tipo"].ToString());
                if (dr["t019_modalidad"] != DBNull.Value)
                    o.t019_modalidad = byte.Parse(dr["t019_modalidad"].ToString());
                if (dr["t019_tic"] != DBNull.Value)
                    o.t019_tic = bool.Parse(dr["T019_TIC"].ToString());
                if (dr["t001_idficepi_i"] != DBNull.Value)
                    o.t001_idficepi_i = (int?)int.Parse(dr["t001_idficepi_i"].ToString());
                if (dr["Creador"] != DBNull.Value)
                    o.Creador = dr["Creador"].ToString();
            }

            return o;
        }

        /// <summary>
        /// Obtiene los Profesionales Asociados asociados a un título
        /// </summary>
        /// <returns></returns>
        public static string ProfesionalesAsociados(int t019_idcodtitulo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblProf' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.Titulacion.ProfAsociados(null, t019_idcodtitulo);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "'>");
                sb.Append("<td><nobr class='NBR W430'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();

        }
        /// <summary>
        /// Antes de insertar comprueba que no haya otra denominacion igual.
        /// Si no existe devuelve el id del registro insertado
        /// Sino devuelve el id del registro encontrado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t019_descripcion"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t019_estado"></param>
        /// <param name="t019_tipo"></param>
        /// <param name="t019_modalidad"></param>
        /// <param name="t019_tic"></param>
        public static short Insertar(SqlTransaction tr, string t019_descripcion, int t001_idficepi, bool t019_estado, byte t019_tipo, 
                                    Nullable<byte> t019_modalidad, bool t019_tic)
        {
            short idTitulacion = SUPER.DAL.Titulacion.GetSerializable(tr, t019_descripcion);
            if (idTitulacion == -1)
            {
                idTitulacion=SUPER.DAL.Titulacion.Insert(tr, t019_descripcion, t001_idficepi, t019_estado, t019_tipo, t019_modalidad, t019_tic);
            }
            return idTitulacion;
        }
        /// <summary>
        /// Borra una titulación si no existen profesionales que la tengan asignada
        /// El 05/12/2013 nos indica María que si al intentar borrar existe algún elemento relacionado, que no muestr el error
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t019_idcodtitulo"></param>
        public static void BorrarNoUsada(SqlTransaction tr, short t019_idcodtitulo)
        {
            //bool bBorrar = true;
            //SqlDataReader dr = DAL.Titulacion.ProfAsociados(tr, t019_idcodtitulo);
            //if (dr.Read())
            //    bBorrar = false;
            //dr.Close();
            //dr.Dispose();
            //if (bBorrar)
            //{
            //    SUPER.DAL.Titulacion.Delete(tr, t019_idcodtitulo);
            //}
            try
            {
                SUPER.DAL.Titulacion.Delete(tr, t019_idcodtitulo);
            }
            catch
            {
                // El 05/12/2013 nos indica María que si al intentar borrar existe algún elemento relacionado, que no muestre el error
            }
        }
        public static string ElementosAsociadoAReasignar(int t019_idcodtitulo)
        {
            return Curriculum.ElementosAsociadoAReasignar(DAL.Titulacion.ElementosAsociadoAReasignar(null, t019_idcodtitulo));
        }

        /// <summary>
        /// Obtiene una lista de los titulos cuyo código se pasa en sListaIds + los titulos cuya denominación está en sListaDens
        /// y existe algun profesional de slFicepis que lo tiene
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="sListaIds"></param>
        /// <param name="sListaDens"></param>
        /// <returns></returns>
        public static List<Titulacion> GetListaPorProfesional(string slFicepis, string sListaIds, string sListaDens)
        {
            List<Titulacion> oLista = new List<Titulacion>();
            Titulacion oElem;
            SqlDataReader dr = SUPER.DAL.Titulacion.GetListaPorProfesional(null, slFicepis.Replace(",", "##"), sListaIds, sListaDens.Replace(";", "##"));
            while (dr.Read())
            {
                oElem = new Titulacion();
                oElem.T019_IDCODTITULO = short.Parse(dr["T019_IDCODTITULO"].ToString());
                oElem.T019_DESCRIPCION = dr["T019_DESCRIPCION"].ToString();
                oLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return oLista;
        }
        /// <summary>
        /// Obtiene una lista con los datos de los titulaciones academicas de los profesionales que se pasan como parametros
        /// </summary>
        /// <param name="slFicepis"></param>
        /// <param name="slCodigos"></param>
        /// <returns></returns>
        public static List<Titulacion> GetDocsExportacion(string slFicepis, string slCodigos)
        {
            List<Titulacion> oLista = new List<Titulacion>();
            Titulacion oElem;
            SqlDataReader dr = SUPER.DAL.Titulacion.GetDocsExportacion(null, slFicepis.Replace(",", "##"), slCodigos.Replace(",", "##"));
            while (dr.Read())
            {
                oElem = new Titulacion();
                //oElem.IdFicepiCert = int.Parse(dr["t001_idficepi"].ToString());
                oElem.T019_IDCODTITULO = short.Parse(dr["T019_IDCODTITULO"].ToString());
                oElem.Profesional = dr["Profesional"].ToString();
                oElem.T019_DESCRIPCION = dr["T019_DESCRIPCION"].ToString();
                oElem.NDOC = dr["T012_NDOCTITULO"].ToString();
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
    }
}
