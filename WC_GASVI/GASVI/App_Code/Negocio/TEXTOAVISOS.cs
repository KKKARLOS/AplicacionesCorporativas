using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GASVI
    /// Class	 : TEXTOAVISOS
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T774_TEXTOAVISOSGASVI
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	14/12/2011 18:00:40	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class TEXTOAVISOS
    {
        #region Propiedades y Atributos

        private int _t774_idaviso;
        public int t774_idaviso
        {
            get { return _t774_idaviso; }
            set { _t774_idaviso = value; }
        }

        private string _t774_denominacion;
        public string t774_denominacion
        {
            get { return _t774_denominacion; }
            set { _t774_denominacion = value; }
        }

        private string _t774_titulo;
        public string t774_titulo
        {
            get { return _t774_titulo; }
            set { _t774_titulo = value; }
        }

        private string _t774_texto;
        public string t774_texto
        {
            get { return _t774_texto; }
            set { _t774_texto = value; }
        }

        private bool _t774_borrable;
        public bool t774_borrable
        {
            get { return _t774_borrable; }
            set { _t774_borrable = value; }
        }

        private DateTime? _t774_fiv;
        public DateTime? t774_fiv
        {
            get { return _t774_fiv; }
            set { _t774_fiv = value; }
        }

        private DateTime? _t774_ffv;
        public DateTime? t774_ffv
        {
            get { return _t774_ffv; }
            set { _t774_ffv = value; }
        }
        #endregion

        #region Constructor

        public TEXTOAVISOS()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static bool VerSiHay()
        {
            bool bHay = false;
            SqlDataReader dr = DAL.TEXTOAVISOS.ObtenerAvisos();
            if (dr.HasRows) bHay = true;
            return bHay;
        }

        public static string ObtenerAvisos()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div style='background-image:url(../../../../Images/imgFT16.gif); width:700px;'>");
            sb.Append("<table id='tblDatos' class='texto MA' name='tblDatos' style='WIDTH: 700px;' cellspacing='0' cellpadding='0' border='0'>");
            sb.Append("<colgroup><col style='width:495px;'/><col style='width:100px;'/><col style='width:100px;'/></colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.TEXTOAVISOS.ObtenerAvisos();
            //int i = 0;
            while (dr.Read())
            {

                sb.Append("<tr id='" + dr["t774_idaviso"].ToString() + "' bd=''");
                sb.Append(" texto=\"" + Utilidades.escape(dr["t774_texto"].ToString()) + "\"");
                //sb.Append(" texto='" + GlobalObject.escape(dr["t774_texto"].ToString()) + "'");
                sb.Append(" style='height:16px' onclick='mm(event);mostrarTexto(this)' ondblclick='mostrarDetalle(this.id)'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["t774_denominacion"].ToString() + "</td>");
                string sFecha = (dr["t774_fiv"].ToString() == "") ? "" : DateTime.Parse(dr["t774_fiv"].ToString()).ToShortDateString();
                sb.Append("<td  style='text-align:right;'>" + sFecha + "</td>");
                sFecha = (dr["t774_ffv"].ToString() == "") ? "" : DateTime.Parse(dr["t774_ffv"].ToString()).ToShortDateString();
                sb.Append("<td  style='text-align:right;'>" + sFecha + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table></div>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static void EliminarAviso(string strAviso)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                string[] aAviso = Regex.Split(strAviso, "##");
                foreach (string oAviso in aAviso)
                {
                    if (oAviso != "")
                    {
                        DAL.TEXTOAVISOS.Delete(tr, int.Parse(oAviso));
                    }
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al eliminar el aviso " + strAviso, ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }
        public static string Grabar(string sCodAviso, string strDatosBasicos)
        {
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            string sResul = "", sDesc, sTitulo, sDescLong;
            bool bBorrable = false;
            int iCodAviso = 0;
            DateTime? dIniV = null;
            DateTime? dFinV = null;
            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch 
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                #region Avisos
                    if (sCodAviso == "0") iCodAviso = -1;
                    else iCodAviso = int.Parse(sCodAviso);
                    if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
                    {
                        string[] aDatosTarea = Regex.Split(strDatosBasicos, "{sep}");
                        ///aDatosTarea[0] = Denominacion aviso
                        ///aDatosTarea[1] = Titulo aviso
                        ///aDatosTarea[2] = Texto libre
                        ///aDatosTarea[3] = chkBorrable
                        ///aDatosTarea[4] = txtValIni
                        ///aDatosTarea[5] = txtValFin
                        sDesc = Utilidades.unescape(aDatosTarea[0]);
                        sTitulo = Utilidades.unescape(aDatosTarea[1]);
                        sDescLong = Utilidades.unescape(aDatosTarea[2]);
                        if (aDatosTarea[3] == "1") bBorrable = true;
                        if (aDatosTarea[4] != "") dIniV = DateTime.Parse(aDatosTarea[4]);
                        if (aDatosTarea[5] != "") dFinV = DateTime.Parse(aDatosTarea[5]);

                        if (iCodAviso <= 0)
                            iCodAviso = DAL.TEXTOAVISOS.Insert(tr, sDesc, sTitulo, sDescLong, bBorrable, dIniV, dFinV);
                        else
                            DAL.TEXTOAVISOS.Update(tr, iCodAviso, sDesc, sTitulo, sDescLong, bBorrable, dIniV, dFinV);
                    }
                #endregion
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar en la tabla de T774_TEXTOAVISOSGASVI.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return iCodAviso.ToString();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T774_TEXTOAVISOSGASVI,
        /// y devuelve una instancia u objeto del tipo TEXTOAVISOS
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	14/12/2011 18:00:40
        /// </history>
        /// -----------------------------------------------------------------------------
        public static TEXTOAVISOS Select(SqlTransaction tr, int t774_idaviso)
        {
            TEXTOAVISOS o = new TEXTOAVISOS();

            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t774_idaviso", SqlDbType.Int, 4, t774_idaviso);

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GVT_TEXTOAVISOS_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_TEXTOAVISOS_S", aParam);

            if (dr.Read())
            {
                if (dr["t774_idaviso"] != DBNull.Value)
                    o.t774_idaviso = int.Parse(dr["t774_idaviso"].ToString());
                if (dr["t774_denominacion"] != DBNull.Value)
                    o.t774_denominacion = (string)dr["t774_denominacion"];
                if (dr["t774_titulo"] != DBNull.Value)
                    o.t774_titulo = (string)dr["t774_titulo"];
                if (dr["t774_texto"] != DBNull.Value)
                    o.t774_texto = (string)dr["t774_texto"];
                if (dr["t774_borrable"] != DBNull.Value)
                    o.t774_borrable = (bool)dr["t774_borrable"];
                if (dr["t774_fiv"] != DBNull.Value)
                    o.t774_fiv = (DateTime)dr["t774_fiv"];
                if (dr["t774_ffv"] != DBNull.Value)
                    o.t774_ffv = (DateTime)dr["t774_ffv"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de TEXTOAVISOS"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        #endregion
    }
}