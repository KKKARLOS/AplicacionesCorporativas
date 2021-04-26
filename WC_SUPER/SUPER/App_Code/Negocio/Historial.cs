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
    /// Descripción breve de Historial
    /// </summary>
    public class Historial
    {
        #region Propiedades y Atributos

        //private string _IdEstado;
        //public string IdEstado
        //{
        //    get { return _IdEstado; }
        //    set { _IdEstado = value; }
        //}

        //private DateTime _Fecha;
        //public DateTime Fecha
        //{
        //    get { return _Fecha; }
        //    set { _Fecha = value; }
        //}

        //private string _Autor;
        //public string Autor
        //{
        //    get { return _Autor; }
        //    set { _Autor = value; }
        //}

        //private string _Texto;
        //public string Texto
        //{
        //    get { return _Texto; }
        //    set { _Texto = value; }
        //}
        #endregion
        public Historial()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        //public Historial(string IdEstado, DateTime Fecha, string Autor, string Texto)
        //{
        //    this.IdEstado = IdEstado;
        //    this.Fecha = Fecha;
        //    this.Autor = Autor;
        //    this.Texto = Texto;
        //}
        public static string ObtenerHistorial(string sTabla, int idKey, Nullable<int> idKey2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table id='tblDatosHistorial' style='width:600px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                            <col style='width:150px' />
                            <col style='width:100px;' />
                            <col style='width:350px;' />
                        </colgroup>");

            SqlDataReader dr = SUPER.DAL.Historial.ObtenerHistorial(null, sTabla, idKey, idKey2);
            int i = 0;
            string sFecha = "";
            while (dr.Read())
            {
                sb.Append("<tr valign='top' class='"+ ((i % 2 == 0)? "FA":"FB") +"'>");

                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W145' onmouseover='TTip(event)'>" + dr["Estado"].ToString() + "</nobr></td>");
                sFecha = ((DateTime)dr["Fecha"]).ToString();
                sb.Append("<td>" + sFecha.Substring(0, sFecha.Length - 3) + "</td>");

                sb.Append("<td>" + dr["Profesional"].ToString());
                if (dr["Motivo"].ToString() != "")
                {
                    sb.Append("<br><blockquote style='margin-left:30px;'>" + dr["motivo"].ToString().Replace(((char)13).ToString() + ((char)10).ToString(), "<br>") + "</blockquote>");
                }
                sb.Append("</td>");
                sb.Append("</tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        /// <summary>
        /// Si el último registro del historial es un elemento Pdte de validar, devuelve su texto,
        /// sino devuelve cadena vacía
        /// </summary>
        /// <param name="sTabla"></param>
        /// <param name="idKey"></param>
        /// <param name="idKey2"></param>
        /// <returns></returns>
        public static string GetMsgPdteValidar(string sTabla, int idKey, Nullable<int> idKey2)
        {
            string sRes = "";
            if (idKey != -1)
            {
                SUPER.DAL.Historial miHistAux = SUPER.DAL.Historial.GetPdteValidar(null, sTabla, idKey, idKey2);
                if (miHistAux.Texto != "")
                {
                    if (miHistAux.IdEstado != "-1")
                    {
                        //if (miHistAux.Autor != "")
                        //{
                            //sRes = "El " + miHistAux.Fecha.ToShortDateString();
                            //sRes += ", " + miHistAux.Autor + " indicó que se debes realizar la siguiente acción:\n\n";
                            sRes = "Observaciones sobre el registro:\n\n";
                            sRes += miHistAux.Texto;
                        //}
                        //else
                        //{
                        //    sRes = "Debes realizar la siguiente acción:\n\n";
                        //    sRes += miHistAux.Texto;
                        //}
                    }
                }
            }
            return sRes;
        }

    }
}
