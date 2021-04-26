using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de PerfilExper
    /// </summary>
    public partial class PerfilExper
    {
        public PerfilExper()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader getPerfiles(SqlTransaction tr, byte tipo)
        {
            return SUPER.DAL.PerfilExper.GetPerfilesExpPer(tr, tipo);
        }

        #region Combos
        public static List<ElementoLista> GetPerfilesConsultas()
        {
            SqlDataReader dr = DAL.PerfilExper.GetPerfilesConsultas(null);
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["codigo"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        #endregion
        public static SqlDataReader GetPerfilesConsultas(SqlTransaction tr, byte tipo)
        {
            return SUPER.DAL.PerfilExper.GetPerfilesConsultas(tr);
        }
        public static string catalogo(SqlTransaction tr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCatalogo' style='width:700px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:430px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append(" <col style='width:100px;' />");
            sb.Append(" <col style='width:100px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");


            SqlDataReader dr = DAL.PerfilExper.catalogo(null);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T035_IDCODPERFIL"].ToString() + "' chk='" + dr["T035_RH"] + "' nivel='" + dr["T035_NIVEL"].ToString() + "'  style='height:22px;' class='MANO' bd='N'>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'>" + dr["T035_DESCRIPCION"].ToString() + "</td>");
                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("<td>" + dr["T035_ABREVIADO"].ToString() + "</td>");
                sb.Append("<td>" + dr["T035_NIVEL"].ToString() + "</td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }

        public static string Grabar(string strDatos)
        {
            string sDenominacionDelete = "";
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

                string sIdPerfil = "";
                string[] aPerfil = Regex.Split(strDatos, "@perfil@");
                foreach (string sPerfil in aPerfil)
                {
                    string[] aDatosPerfil = Regex.Split(sPerfil, "@dato@");
                    sAccion = aDatosPerfil[0];
                    sDenominacionDelete = "";
                    switch (aDatosPerfil[0])
                    {
                        case "I":
                            //aDatosPerfil[1]-->IDPerfil
                            //aDatosPerfil[2]-->Descripcion
                            //aDatosPerfil[3]-->Abreviatura
                            //aDatosPerfil[4]-->RH
                            //aDatosPerfil[5]-->Nivel
                            sIdPerfil += DAL.PerfilExper.Insert(tr, aDatosPerfil[2].ToString(), aDatosPerfil[3].ToString(), (aDatosPerfil[4].ToString() == "true") ? byte.Parse("1") : byte.Parse("0"), int.Parse(aDatosPerfil[5])) + "//";
                            break;
                        case "U":
                            DAL.PerfilExper.Update(tr, int.Parse(aDatosPerfil[1].ToString()), aDatosPerfil[2].ToString(), aDatosPerfil[3].ToString(), (aDatosPerfil[4].ToString() == "true") ? byte.Parse("1") : byte.Parse("0"), int.Parse(aDatosPerfil[5]));
                            break;
                        case "D":
                            sDenominacionDelete = aDatosPerfil[2];
                            DAL.PerfilExper.Delete(tr, int.Parse(aDatosPerfil[1].ToString()));
                            break;
                    }
                }

                #endregion


                Conexion.CommitTransaccion(tr);
                return "OK@#@" + sIdPerfil;

            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (Errores.EsErrorIntegridad(ex) && sAccion == "D")
                {
                    throw new Exception("ErrorControlado##EC##No se puede eliminar el perfil \"" + sDenominacionDelete + "\" por tener elementos relacionados.");
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

    }
}