using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
//using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de EXPPROF
    /// </summary>
    public partial class EXPPROFACS
    {
        #region Propiedades y Atributos

        private string _t808_denominacion;
        public string t808_denominacion
        {
            get { return _t808_denominacion; }
            set { _t808_denominacion = value; }
        }

        private string _t808_descripcion;
        public string t808_descripcion
        {
            get { return _t808_descripcion; }
            set { _t808_descripcion = value; }
        }

        private bool _t808_enibermatica;
        public bool t808_enibermatica
        {
            get { return _t808_enibermatica; }
            set { _t808_enibermatica = value; }
        }

        private int? _t811_idcuenta_ori;
        public int? t811_idcuenta_ori
        {
            get { return _t811_idcuenta_ori; }
            set { _t811_idcuenta_ori = value; }
        }

        private int? _t811_idcuenta_para;
        public int? t811_idcuenta_para
        {
            get { return _t811_idcuenta_para; }
            set { _t811_idcuenta_para = value; }
        }

        private int? _t302_idcliente;
        public int? t302_idcliente
        {
            get { return _t302_idcliente; }
            set { _t302_idcliente = value; }
        }

        private int? _t313_idempresa;
        public int? t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }

        private string _denProyecto;
        public string denProyecto
        {
            get { return _denProyecto; }
            set { _denProyecto = value; }
        }
        private string _ctaOrigen;
        public string ctaOrigen
        {
            get { return _ctaOrigen; }
            set { _ctaOrigen = value; }
        }
        private string _ctaDestino;
        public string ctaDestino
        {
            get { return _ctaDestino; }
            set { _ctaDestino = value; }
        }
        private string _t302_denominacion;
        public string t302_denominacion
        {
            get { return _t302_denominacion; }
            set { _t302_denominacion = value; }
        }
        private string _t313_denominacion;
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }

        #endregion

        #region Metodos

        public static string getAreas(SqlTransaction tr, int t808_idexpprof, bool bModoLectura)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblConSec'");
            if (bModoLectura)
                sb.Append(" class='texto' style='width:420px; table-layout:fixed;' cellSpacing='0' border='0'>");
            else
                sb.Append(" class='texto MANO' style='width:420px;table-layout:fixed;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:400px;' />");
            sb.Append("</colgroup>");
            if (t808_idexpprof != -1)
            {
                SqlDataReader dr = SUPER.DAL.EXPPROFACS.getAreas(null, t808_idexpprof);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t809_idaconsect"].ToString() + "' bd='' ");
                    if (bModoLectura)
                        sb.Append("style='height:16px; display:table-row;' >");
                    else
                        sb.Append("style='height:16px; display:table-row;' onclick='mm(event);'>");

                    sb.Append("<td><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgFN.gif'></td>");
                    sb.Append("<td><nobr class='NBR W390'>" + dr["t809_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        public static void InsertEP(SqlTransaction tr, int t808_idexpprof, string sAcs)
        {

            SUPER.DAL.EXPPROFACS.InsertEP(tr, t808_idexpprof, sAcs);
        }

        #endregion
    }
}