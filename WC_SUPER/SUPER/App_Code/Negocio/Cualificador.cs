using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{

    /// <summary>
    /// Descripción breve de Cualificador
    /// </summary>
    public class Cualificador
    {
        #region Propiedades y Atributos complementarios
        private int _t055_idcalifOCFA;
        public int t055_idcalifOCFA
        {
            get { return _t055_idcalifOCFA; }
            set { _t055_idcalifOCFA = value; }
        }

        private string _t055_denominacion;
        public string t055_denominacion
        {
            get { return _t055_denominacion; }
            set { _t055_denominacion = value; }
        }

        #endregion
        public Cualificador()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static void Delete(SqlTransaction tr, int t055_idcalifOCFA)
        {
            SUPER.DAL.Cualificador.Delete(tr, t055_idcalifOCFA);
        }

        public static void Update(SqlTransaction tr, int t055_idcalifOCFA, string t055_denominacion, bool t055_defectoCCRR,
                                    bool t055_defectoPIG, Nullable<int> t329_idclaseeco)
        {
            SUPER.DAL.Cualificador.Update(tr, t055_idcalifOCFA, t055_denominacion, t055_defectoCCRR, t055_defectoPIG, t329_idclaseeco);
        }

        public static short Insert(SqlTransaction tr, string t055_denominacion, bool t055_defectoCCRR, bool t055_defectoPIG, 
                                    Nullable <int> t329_idclaseeco)
        {
            short iRes = SUPER.DAL.Cualificador.Insert(tr, t055_denominacion, t055_defectoCCRR, t055_defectoPIG, t329_idclaseeco);

            return iRes;
        }

        /// <summary>
        /// Obtiene el HTML de una tabla con la consulta de los datos de la T055_CALIFOCFA
        /// </summary>
        /// <returns></returns>
        public static string getHtmlConsulta()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = null;
            dr = SUPER.DAL.Cualificador.Catalogo();
            sb.Append("<table id='tblDatos' class='texto MA' style='width:390px;'>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t055_idcalifOCFA"].ToString() + "' style='height:20px;' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t055_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        /// <summary>
        /// Obtiene el HTML de una tabla con la consulta de los datos de la T055_CALIFOCFA para su mantenimiento
        /// </summary>
        /// <returns></returns>
        public static string getHtmlMantenimiento()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = null;
            dr = SUPER.DAL.Cualificador.Catalogo();
            sb.Append("<table id='tblDatos' class='texto' style='width: 900px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:405px;' /><col style='width:40px;' /><col style='width:40px;' /><col style='width:390px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t055_idcalifOCFA"].ToString() + "' clase='" + dr["t329_idclaseeco"].ToString() + "'");
                sb.Append(" bd='' onclick='mm(event)' style='height:20px;'>");
                sb.Append("<td></td>");//Imagen para el tipo de acción de BBDD
                sb.Append("<td style='padding-left:5px;'><input type='text' id='txtDesc' class='txtL' style='width:395px' value='" + dr["t055_denominacion"].ToString() + "' maxlength='50' onKeyUp='fm(event)'></td>");

                sb.Append("<td><input type='checkbox' style='width:13px;height:13px;' name='chkCR' id='chkCR' class='check' onclick='fm(event)' ");
                if ((bool)dr["t055_defectoCCRR"]) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("<td><input type='checkbox' style='width:13px;height:13px;' name='chkPIG' id='chkPIG' class='check' onclick='fm(event)' ");
                if ((bool)dr["t055_defectoPIG"]) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("<td onmouseover='TTip(event)'><span class='NBR' style='width:360px'>" + dr["t329_denominacion"].ToString() + "</span></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static Cualificador getDefectoParaNodos()
        {
            SUPER.DAL.Cualificador o1 = new SUPER.DAL.Cualificador();
            o1 = SUPER.DAL.Cualificador.getDefectoParaNodos();

            if (o1.t055_idcalifOCFA == -1)
                throw new Exception("Cualificador.getDefectoParaNodos -> No existe cualificador para nodos por defecto en la T055");

            Cualificador o2 = new Cualificador();
            o2.t055_idcalifOCFA = o1.t055_idcalifOCFA;
            o2.t055_denominacion = o1.t055_denominacion;

            return o2;
        }

        /// <summary>
        /// Obtiene el nº de cualificadores con nodo por defecto marcado
        /// </summary>
        /// <returns></returns>
        public static int getNumDefectoParaNodos(SqlTransaction tr)
        {
            
            return SUPER.DAL.Cualificador.getNumDefectoParaNodos(tr);
        }
        /// <summary>
        /// Obtiene el nº de cualificadores con PIG por defecto marcado
        /// </summary>
        /// <returns></returns>
        public static int getNumDefectoParaPIG(SqlTransaction tr)
        {
            return SUPER.DAL.Cualificador.getNumDefectoParaPIG(tr);
        }
    }
}