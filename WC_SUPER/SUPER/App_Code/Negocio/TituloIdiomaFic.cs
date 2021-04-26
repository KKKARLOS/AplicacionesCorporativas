using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.DAL;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de EntornoTecno
    /// </summary>
    public partial class TituloIdiomaFic
    {
        #region Propiedades y Atributos

        private int _T020_IDCODIDIOMA;
        public int T020_IDCODIDIOMA
        {
            get { return _T020_IDCODIDIOMA; }
            set { _T020_IDCODIDIOMA = value; }
        }

        private string _T021_TITULO;
        public string T021_TITULO
        {
            get { return _T021_TITULO; }
            set { _T021_TITULO = value; }
        }

        private string _T021_CENTRO;
        public string T021_CENTRO
        {
            get { return _T021_CENTRO; }
            set { _T021_CENTRO = value; }
        }

        private string _T021_OBSERVA;
        public string T021_OBSERVA
        {
            get { return _T021_OBSERVA; }
            set { _T021_OBSERVA = value; }
        }

        private string _T835_MOTIVORT;
        public string T835_MOTIVORT
        {
            get { return _T835_MOTIVORT; }
            set { _T835_MOTIVORT = value; }
        }

        private string _T021_NDOC;
        public string T021_NDOC
        {
            get { return _T021_NDOC; }
            set { _T021_NDOC = value; }
        }

        //private string _T021_USUTICKS;
        //public string T021_USUTICKS
        //{
        //    get { return _T021_USUTICKS; }
        //    set { _T021_USUTICKS = value; }
        //}

        private int _T001_IDFICEPI;
        public int T001_IDFICEPI
        {
            get { return _T001_IDFICEPI; }
            set { _T001_IDFICEPI = value; }
        }

        private char _t839_idestado;
        public char t839_idestado
        {
            get { return _t839_idestado; }
            set { _t839_idestado = value; }
        }

        private DateTime? _T021_FECHA;
        public DateTime? T021_FECHA
        {
            get { return _T021_FECHA; }
            set { _T021_FECHA = value; }
        }

        private byte[] _T021_DOC;
        public byte[] T021_DOC
        {
            get { return _T021_DOC; }
            set { _T021_DOC = value; }
        }

        private string _T020_DESCRIPCION;
        public string T020_DESCRIPCION
        {
            get { return _T020_DESCRIPCION; }
            set { _T020_DESCRIPCION = value; }
        }
        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        #endregion

        #region Constructor

        public TituloIdiomaFic()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #endregion

        #region Metodos

        public static string Catalogo(int t001_idficepi, int t020_idcodidioma)
        {
            SqlDataReader dr = DAL.TituloIdiomaFic.Catalogo(t001_idficepi, t020_idcodidioma);

            string motivoT = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<table  id='tblDatos' class='texto MA' style='WIDTH:485px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:25px;'/>");
            sb.Append("<col style='width:40px;'/>");
            sb.Append("<col style='width:230px;'/>");
            sb.Append("<col style='width:120px;'/>");
            sb.Append("<col style='width:70px;'/>");
            sb.Append("</colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;' title='" + motivoT + "' id='" + dr["T021_IDTITULOIDIOMA"].ToString() + "'  bd='' ndoc='" + dr["T021_NDOC"].ToString() + "' onClick='mm(event);' ");
                sb.Append("estado='" + dr["t839_idestado"].ToString() + "' ondblclick='AnadirTitulo(" + t020_idcodidioma + ",\"" + dr["t839_idestado"].ToString() + "\"," + dr["T021_IDTITULOIDIOMA"].ToString() + ",\"" + dr["T020_DESCRIPCION"].ToString() + "\")' >");
                sb.Append("<td>");
                switch (dr["t839_idestado"].ToString())
                {
                    //06/08/2015 PPOO nos pide que no figuren las leyendas Pdte Validar ni Info privada
                    //case ("O"):
                    //case ("P"): sb.Append("<img src='../../../../images/imgPenValidar.png' title='Datos pendientes de validar por la organización' />"); break;
                    //case ("R"): sb.Append("<img src='../../../../images/imgRechazar.png' title='Este dato es únicamente visible por ti' />"); break;
                    case ("S"):
                    case ("T"): sb.Append("<img src='../../../../images/imgPenCumplimentar.png' title='Datos que tienes pendiente de completar, actualizar o modificar' />"); break;
                    case ("B"): sb.Append("<img src='../../../../images/imgBorrador.png' title='Datos en borrador' />"); break;
                    case ("X"):
                    case ("Y"):
                        sb.Append("<img src='../../../../images/imgPseudovalidado.png' title='Pendiente de adjuntar la documentación acreditativa' />"); break;
                }
                sb.Append("</td>");
                //Doc
                sb.Append("<td>");
                if (dr["bDoc"].ToString() == "1")
                {
                    sb.Append("<img src=\"../../../../images/imgTitulo.png\" ");
                    sb.Append("onclick=\"descargar('TIF', " + dr["T021_IDTITULOIDIOMA"] + ");\" ");
                    sb.Append(" title=\"Descargar " + dr["T021_NDOC"].ToString() + "\" style='cursor:pointer;'>");//style='vertical-align:bottom;'
                }
                sb.Append("</td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W220' onmouseover='TTip(event);'>" + dr["T021_TITULO"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W110' onmouseover='TTip(event);'>" + dr["T021_CENTRO"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:3px;'>" + dr["FECHAO"].ToString() + "</td>");
                
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string MiCVCatalogo(int t001_idficepi, int t020_idcodidioma)
        {
            string colorEstado = "";
            string motivoT = "";

            SqlDataReader dr = DAL.TituloIdiomaFic.Catalogo(t001_idficepi, t020_idcodidioma);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatosTitulos' style='WIDTH:510px; margin-left:40px;' border='1'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px;'/>");
            sb.Append("<col style='width:420px;'/>");
            sb.Append("<col style='width:70px;'/>");
            sb.Append("</colgroup>");


            while (dr.Read())
            {
                switch (dr["t839_idestado"].ToString())
                {
                    case ("T"):
                        colorEstado = "color:Blue"; //Pdte Tecnico
                        motivoT = dr["T021_MOTIVORT"].ToString();
                        break;
                    case ("P"):
                        colorEstado = "color:Orange"; //Pdte RRHH
                        break;
                    default:
                        colorEstado = "";
                        break;
                }
                sb.Append("<tr style='" + colorEstado + "; height:20px;' title='" + motivoT + "' id='" + dr["T021_IDTITULOIDIOMA"].ToString() + "'  bd='' ndoc='" + dr["T021_NDOC"].ToString() + "' ");
                sb.Append("est='" + dr["t839_idestado"].ToString() + "' >");
                //Doc
                sb.Append("<td>");
                if (dr["bDoc"].ToString() == "1")
                {
                    sb.Append("<img src=\"../../../images/imgDescarga.gif\" width='16px' height='16px' ");
                    sb.Append("onclick=\"descargar('TIF', " + dr["T021_IDTITULOIDIOMA"] + ");\" ");
                    sb.Append("style='cursor:pointer;' title=\"Descargar " + dr["T021_NDOC"].ToString() + "\">");
                }   
                
                sb.Append("</td>");
                sb.Append("<td style='cursor:pointer; padding-left:3px;' onclick='AnadirTitulo(" + t020_idcodidioma + ",\"" + dr["t839_idestado"].ToString() + "\"," + dr["T021_IDTITULOIDIOMA"].ToString() + ")'><nobr class='NBR W400' onmouseover='TTip(event)'>" + dr["T021_TITULO"].ToString() + " </nobr></td>");
                sb.Append("<td style='padding-left:3px;'>" + dr["FECHAO"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return sb.ToString();
        }

        public static int Grabar(Nullable<int> t021_idtituloidioma, string t021_titulo, Nullable<DateTime> t021_fecha, string t021_observa, 
                                 string t021_centro, int t001_idficepi, int t020_idcodidioma, string sNombre, 
                                 bool cambioDoc, char t839_idestado, string motivoRT, int t001_idficepiu, char t839_idestado_ini,
                                Nullable<long> t2_iddocumento, string sEsMiCV)
        {
            int nFilasModificadas = 0;
            string sResul = "";
            bool bErrorControlado = false;
            try
            {
                if (t021_idtituloidioma != null)
                {
                    nFilasModificadas = DAL.TituloIdiomaFic.Update(t021_idtituloidioma, t021_titulo, t021_fecha, t021_observa, t021_centro, 
                                                                    t001_idficepi, t020_idcodidioma, sNombre, cambioDoc,
                                                                    t839_idestado, motivoRT, t001_idficepiu, t839_idestado_ini, t2_iddocumento);
                }
                else
                {
                    //Si no existe registro en la tabla de niveles de lectura, escritura y oral, lo inserto
                    //Este caso solo ocurrirá cuando un encargado de CV u otra figura de CurVit esté introduciendo un CV
                    //que no sea el suyo
                    SqlDataReader drAux = SUPER.Capa_Datos.IdiomaFic.Detalle(t001_idficepi, t020_idcodidioma);
                    if (!drAux.Read())
                        SUPER.Capa_Datos.IdiomaFic.Insert(t001_idficepi, t020_idcodidioma,null,null,null);
                    drAux.Close();
                    drAux.Dispose();
                    nFilasModificadas = DAL.TituloIdiomaFic.Insert(t021_titulo, t021_fecha, t021_observa, t021_centro, t839_idestado, 
                                                                   t001_idficepi, t020_idcodidioma, sNombre, motivoRT, t001_idficepiu,
                                                                   t2_iddocumento);
                }
                if (nFilasModificadas == 0)
                {
                    sResul = "Fila no actualizada";
                    bErrorControlado = true;
                    throw (new Exception(sResul));
                }
                if (sEsMiCV == "S" && (t839_idestado.ToString() == "O" || t839_idestado.ToString() == "P")) 
                    DAL.Curriculum.ActualizadoCV(null, t001_idficepi);

                return nFilasModificadas;
            }
            catch (Exception ex) {
                if (bErrorControlado) sResul = ex.Message;
                else
                {
                    string sTexto = @"Error al " + ((t021_idtituloidioma == null)?"insertar":"modificar") + @" el título.";
                    //sTexto += "<br>t021_idtituloidioma = " + t021_idtituloidioma.ToString();
                    //sTexto += "<br>t021_titulo = " + t021_titulo.ToString();
                    //sTexto += "<br>t020_idcodidioma = " + t020_idcodidioma.ToString();
                    //sTexto += "<br>t001_idficepi = " + t001_idficepi.ToString();
                    sResul = Errores.mostrarError(sTexto, ex);
                }
                return nFilasModificadas;
            }
            finally
            {
                if (sResul != "")
                {
                    if (bErrorControlado) sResul = "ErrorControlado##EC##" + sResul;
                    throw (new Exception(sResul));
                }
            }
        }

        public static string Delete(string idTitulo)
        {
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
            string[] strTitulos = Regex.Split(idTitulo, "@titulo@");
            foreach (string sTitulo in strTitulos)
            {
                if (sTitulo != "")
                {
                    DAL.TituloIdiomaFic.Delete(tr, int.Parse(sTitulo)).ToString();
                }
            }
            
            Conexion.CommitTransaccion(tr);
            return "OK@#@";

            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }

        public static TituloIdiomaFic Detalle(int t021_idtituloidioma)
        {
            TituloIdiomaFic o = new TituloIdiomaFic();
            SqlDataReader dr = DAL.TituloIdiomaFic.Detalle(t021_idtituloidioma);
            if (dr.Read())
            {
                if (dr["T021_TITULO"] != DBNull.Value)
                    o.T021_TITULO= (string)dr["T021_TITULO"];
                if (dr["T021_FECHA"] != DBNull.Value)
                    o.T021_FECHA= (DateTime)dr["T021_FECHA"];
                if (dr["T021_OBSERVA"] != DBNull.Value)
                    o.T021_OBSERVA = (string)dr["T021_OBSERVA"];
                if (dr["T021_CENTRO"] != DBNull.Value)
                    o.T021_CENTRO = (string)dr["T021_CENTRO"];
                if (dr["T021_NDOC"] != DBNull.Value)
                    o.T021_NDOC = (string)dr["T021_NDOC"];
                if (dr["T021_MOTIVORT"] != DBNull.Value)
                    o.T835_MOTIVORT = (string)dr["T021_MOTIVORT"].ToString();
                if (dr["T020_DESCRIPCION"] != DBNull.Value)
                    o.T020_DESCRIPCION = (string)dr["T020_DESCRIPCION"];
                if (dr["t839_idestado"] != DBNull.Value)
                    o.t839_idestado = dr["t839_idestado"].ToString().ToCharArray()[0];
                //if (dr["T021_DOC"] != DBNull.Value)
                //    o.T021_DOC = (byte[])dr["T021_DOC"];
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de Idioma"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static TituloIdiomaFic SelectDoc(SqlTransaction tr,int t021_idtituloidioma)
        {
            TituloIdiomaFic o = new TituloIdiomaFic();
            SqlDataReader dr = DAL.TituloIdiomaFic.Detalle(t021_idtituloidioma);
            if (dr.Read())
            {

                if (dr["T021_NDOC"] != DBNull.Value)
                    o.T021_NDOC = (string)dr["T021_NDOC"];
                //if (dr["T021_DOC"] != DBNull.Value)
                //    o.T021_DOC = (byte[])dr["T021_DOC"];
                if (dr["t2_iddocumento"] != DBNull.Value)
                    o.t2_iddocumento = (long)dr["t2_iddocumento"];
            }
            dr.Close();
            dr.Dispose();

            return o;
        }
        public static void PonerDocumento(SqlTransaction tr, int t021_idtituloidioma, int t001_idficepi, string sDenDoc, Nullable<long> t2_iddocumento)
        {
            SUPER.DAL.TituloIdiomaFic.UpdatearDoc(tr, t021_idtituloidioma, t001_idficepi, sDenDoc, t2_iddocumento);
        }

        #endregion
    }
}