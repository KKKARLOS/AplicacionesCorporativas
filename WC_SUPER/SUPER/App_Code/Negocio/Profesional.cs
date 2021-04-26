using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
//using System.Data.SqlClient;
using SUPER.Capa_Datos;
using SUPER.BLL;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{

    public partial class Profesional
    {
        #region Propiedades y Atributos



        #endregion

        #region Constructor

        public Profesional()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #endregion

        #region Metodos

        public static string CatalogoProfesionales(string nombre, string apellido1, string apellido2, Nullable<int> t001_cvexclusion)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' style='width:700px;' >");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:500px;' />");
            sb.Append(" <col style='width:180px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            int i = 0;

            SqlDataReader dr = DAL.Profesional.CatalogoProfesionales(null, nombre, apellido1, apellido2, t001_cvexclusion);
            while (dr.Read())
            {
                sb.Append("<tr id='" + i.ToString() + "' idficepi='" + dr["T001_IDFICEPI"].ToString() + "' exclusion='" + dr["T001_CVEXCLUSION"].ToString() + "'  style='height:22px;' class='MANO' bd='N'>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'>" + dr["PROFESIONAL"].ToString() + "</td>");
                sb.Append("<td style='padding-left:3px;'>" + dr["CVEXCLUSION"].ToString() + "</td>");
                sb.Append("</tr>");
                i++;
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }

        public static void Grabar(string strDatos)
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
            #region Grabar

                string[] aDatos = Regex.Split(strDatos, "@profesional@");
                foreach (string sDato in aDatos)
                {
                    string[] aDatosProfesional = Regex.Split(sDato, "@dato@");
                    switch (aDatosProfesional[0])
                    {
                        case "U":
                            DAL.Profesional.Update(tr, int.Parse(aDatosProfesional[1]), (aDatosProfesional[2] == "") ? null : (int?)int.Parse(aDatosProfesional[2]));
                            break;
                        
                    }
                }

            #endregion


            Conexion.CommitTransaccion(tr);
            
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

        public static DataSet ObtenerControlAbsentismo(int annomes, string sCentros, string sEvaluadores, string sPSN)
        {
            DataSet ds = SUPER.DAL.Profesional.ObtenerControlAbsentismo(null, annomes, sCentros, sEvaluadores, sPSN);
            
            //Se guarda el resultado en la caché (identificando al usuario) con una caducidad de 10 minutos
            //para que esté disponible para la exportación a Excel.
            HttpContext.Current.Cache.Insert("CacheControlAbsentismo_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString(), ds, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);

            return ds;
        }
        public static bool EstaDeBaja(int idFicepi)
        {
            bool bBaja = true;

            int iAux = SUPER.DAL.Profesional.EsBaja(idFicepi);
            if (iAux == 0)
                bBaja = false;
            return bBaja;
        }

        #endregion

        #region Perfil mercado

        public static string Catalogo(int idficepi)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            SqlDataReader dr = DAL.Profesional.Catalogo(null,
                idficepi);

            sb.Append("<table id='tblDatosI' class='texto MANO' style='width: 960px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:320px;' /><col style='width:310px;' /><col style='width:310px;' /></colgroup>");

            sb2.Append("<table id='tblDatosE' class='texto MANO' style='width: 960px;'>");
            sb2.Append("<colgroup><col style='width:20px;' /><col style='width:320px;' /><col style='width:310px;' /><col style='width:310px;' /></colgroup>");
            while (dr.Read())
            {
                if (dr["TIPORECURSO"].ToString() == "I")
                {
                    sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("perMer='" + dr["IDPERFILM"].ToString() + "' ");
                    sb.Append("style='height:22px' >");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR' style='width:310px;'>" + dr["Profesional"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR' style='width:300px;'>" + dr["T004_DESROL"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR' style='width:300px;'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr></td>");
                    sb.Append("</tr>");
                }
                else
                {
                    sb2.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                    sb2.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb2.Append("perMer='" + dr["IDPERFILM"].ToString() + "' ");
                    sb2.Append("style='height:22px' >");
                    sb2.Append("<td></td>");
                    sb2.Append("<td><nobr class='NBR' style='width:310px;'>" + dr["Profesional"].ToString() + "</nobr></td>");
                    sb2.Append("<td><nobr class='NBR' style='width:300px;'></nobr></td>");
                    sb2.Append("<td><nobr class='NBR' style='width:300px;'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr></td>");
                    sb2.Append("</tr>");
                }

            }

            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            sb2.Append("</table>");

           return sb.ToString() + "@#@" + sb2.ToString();
        }

        public static string ComboPerfilesMercado()
        {
            SqlDataReader dr = DAL.Profesional.ComboPerfilesMercado();
            StringBuilder sbCombo = new StringBuilder();
            while (dr.Read())
            {
                sbCombo.Append("<option value='" + dr["T035_IDCODPERFIL"].ToString() + "'>" + dr["T035_DESCRIPCION"].ToString() + "</option>");
            }
            return sbCombo.ToString();
        }

        public static void GrabarPerfilM(int idFicepi, int idCodPM)
        {
            DAL.Profesional.GrabarPerfilMercado(null, idFicepi, idCodPM);
        }

        #endregion

    }
}

