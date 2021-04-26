using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
using SUPER.BLL;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de EntornoTecno
    /// </summary>
    public partial class EntornoTecno
    {
        #region Propiedades y Atributos

        

        #endregion

        #region Constructor

        public EntornoTecno()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #endregion

        #region Metodos

        public static string Catalogo(string t036_descripcion, Nullable<byte> t036_estado, string sTipoBusqueda)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append("<table id='tblCatalogo' style='width:370px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:300px;' />");
            sb.Append(" <col style='width:50px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");


            SqlDataReader dr = DAL.EntornoTecno.Catalogo(null, t036_descripcion, t036_estado, sTipoBusqueda);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T036_IDCODENTORNO"].ToString() + "' style='height:22px;' class='MANO' bd='N' chk=" + dr["T036_ESTADO"].ToString() + ">");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:5px;'><span class='txtL'>" + dr["T036_DESCRIPCION"].ToString() + "</span></td>");//si se quita el span al hacer scroll se mueve de sitio
                sb.Append("<td></td>");
                sb.Append("</tr>");
                i++;
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();

        }

        /// <summary>
        /// OBTIENE LOS ENTORNOS TECNOLOGICOS INDEPENDIENTEMENTE DE SI ESTAN ASIGNADOS O NO
        /// </summary>
        /// <param name="t036_descripcion"></param>
        /// <param name="t036_estado"></param>
        /// <returns></returns>
        public static string CatalogoSimple(string t036_descripcion, Nullable<byte> t036_estado, string sTipoBusqueda)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MM' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tbody>");
            SqlDataReader dr = DAL.EntornoTecno.Catalogo(null, t036_descripcion, t036_estado, sTipoBusqueda);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T036_IDCODENTORNO"].ToString() + "' onclick='ms(this)' onmousedown='DD(event)'>");
                sb.Append("<td><nobr class='NBR W430'>" + dr["T036_DESCRIPCION"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();

        }
        /// <summary>
        /// Catálogo para acceso desde Experiencia profesional
        /// </summary>
        /// <returns></returns>
        public static string Catalogo2()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px; border-collapse: collapse; ' cellspacing='0' border='0'>");
            sb.Append("<tbody>");

            //SqlDataReader dr = DAL.EntornoTecno.Catalogo(null, "", 1, "C");
            SqlDataReader dr = DAL.EntornoTecno.Catalogo(null, "", null, "C");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T036_IDCODENTORNO"].ToString() + "' style='noWrap:true; height:16px;' onclick='mm(event)' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='width:20px;'></td>");
                sb.Append("<td>" + dr["T036_DESCRIPCION"].ToString() + "</td>");
                sb.Append("</tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtiene los entornos tecnológicos asociados al perfil de un profesional en una experiencia profesional
        /// </summary>
        /// <returns></returns>
        public static string CatalogoByProf(int t813_idexpficepiperfil)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append("<table id='tblET' style='width:470px;' class='MANO' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:450px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.EntornoTecno.CatalogoByProf(null, t813_idexpficepiperfil);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T036_IDCODENTORNO"].ToString() + "' style='height:16px;' bd='N' onclick='mm(event);'>");
                sb.Append("<td><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgFN.gif'></td>");
                sb.Append("<td>" + dr["T036_DESCRIPCION"].ToString() + "</td>");//si se quita el span al hacer scroll se mueve de sitio
                sb.Append("</tr>");
                i++;
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return sb.ToString();

        }
        /// <summary>
        /// Obtiene los Profesionales Asociados asociados a un entorno tecnológico
        /// </summary>
        /// <returns></returns>
        public static string ProfesionalesAsociados(int t036_idcodentorno)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblProf' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.EntornoTecno.ProfAsociados(null, t036_idcodentorno);
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
        public static string ElementosAsociadoAReasignar(int t036_idcodentorno)
        {
            return Curriculum.ElementosAsociadoAReasignar(DAL.EntornoTecno.ElementosAsociadoAReasignar(null, t036_idcodentorno));
        }

        public static string CatalogoPlantilla(int t819_idplantillacvt)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append("<table id='tblET' style='width:470px;' class='MANO' mantenimiento=''>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:450px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.EntornoTecno.CatalogoPlantilla(null, t819_idplantillacvt);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T036_IDCODENTORNO"].ToString() + "' style='height:16px;' bd='N' onclick='mm(event);'>");
                sb.Append("<td><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgFN.gif'></td>");
                sb.Append("<td>" + dr["T036_DESCRIPCION"].ToString() + "</td>");//si se quita el span al hacer scroll se mueve de sitio
                sb.Append("</tr>");
                i++;
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
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

                string sIdEntorno = "";
                string[] aCuenta = Regex.Split(strDatos, "@entorno@");
                foreach (string sCuenta in aCuenta)
                {
                    if (sCuenta != "")
                    {
                        string[] aDatosCuenta = Regex.Split(sCuenta, "@dato@");
                        sAccion = aDatosCuenta[0];
                        sDen = aDatosCuenta[2].ToString();
                        switch (aDatosCuenta[0])
                        {
                            case "I":
                                sIdEntorno += DAL.EntornoTecno.Insert(tr, aDatosCuenta[2].ToString(), (aDatosCuenta[3].ToString() == "true") ? byte.Parse("1") : byte.Parse("0")) + "//";
                                break;
                            case "U":
                                DAL.EntornoTecno.Update(tr, int.Parse(aDatosCuenta[1].ToString()), aDatosCuenta[2].ToString(), (aDatosCuenta[3].ToString() == "true") ? byte.Parse("1") : byte.Parse("0"));
                                break;
                            case "D":
                                //sDenominacionDelete = aDatosCuenta[2];
                                DAL.EntornoTecno.Delete(tr, int.Parse(aDatosCuenta[1].ToString()));
                                break;
                        }
                    }
                }

                #endregion


                Conexion.CommitTransaccion(tr);
                return "OK@#@" + sIdEntorno;
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (Errores.EsErrorIntegridad(ex))
                {
                    if (sAccion == "D")
                        throw new Exception("ErrorControlado##EC##No se puede eliminar el entorno tecnologico\\funcional \"" + sDen + "\" por tener elementos relacionados.");
                    else
                        throw new Exception("ErrorControlado##EC##No se puede grabar el entorno tecnologico\\funcional \"" + sDen + "\" porque ya existe esa denominación.");
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

        public static List<ElementoLista> obtenerCboEntorno(int tipo)
        {
            SqlDataReader dr = DAL.EntornoTecno.obtenerEntorno(tipo);//1 Todos
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["T036_IDCODENTORNO"].ToString(), dr["T036_DESCRIPCION"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        /// <summary>
        /// Es igual que obtenerCboEntorno pero añade un elemento inicial vacío con código -1
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static List<ElementoLista> obtenerCboEntorno2(int tipo)
        {
            SqlDataReader dr = DAL.EntornoTecno.obtenerEntorno(tipo);//1 Todos
            List<ElementoLista> oLista = new List<ElementoLista>();
            oLista.Add(new ElementoLista("-1", ""));
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["T036_IDCODENTORNO"].ToString(), dr["T036_DESCRIPCION"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static SqlDataReader getEntornos(int tipo)
        {
            return SUPER.DAL.EntornoTecno.obtenerEntorno(tipo);
        }

        #endregion
    }
}