using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : EXCLUIDOS_MIEQ
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T977_EXCLUIDOS_MIEQ_TPL
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	15/02/2010 16:17:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EXCLUIDOS_MIEQ
    {
        #region Propiedades y Atributos

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        #endregion

        #region Constructor

        public EXCLUIDOS_MIEQ()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static string Obtener()
        {
            StringBuilder sb = new StringBuilder();


            SqlDataReader dr = SUPER.DAL.EXCLUIDOS_MIEQ.Catalogo();
            sb.Append("<TABLE id='tblDatos2' style='width: 450px;' class='texto MM' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width: 20px' /><col style='width: 420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' bd='' style='height:20px;' tipo='" + dr["tipo"].ToString() + "'");
                sb.Append(" onmousedown='DD(event)' onclick='mm(event)'>");
                sb.Append("<td style='padding-left:1px'><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td align='center'>");
                if (dr["t001_sexo"].ToString() == "V")
                {
                    switch (dr["tipo"].ToString())
                    {
                        case "P":
                            sb.Append("<img src='../../../../images/imgUsuPV.gif'>");
                            break;
                        case "E":
                            sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                            break;
                        case "F":
                            sb.Append("<img src='../../../../images/imgUsuFV.gif'>");
                            break;
                    }
                }
                else
                {
                    switch (dr["tipo"].ToString())
                    {
                        case "P":
                            sb.Append("<img src='../../../../images/imgUsuPM.gif'>");
                            break;
                        case "E":
                            sb.Append("<img src='../../../../images/imgUsuEM.gif'>");
                            break;
                        case "F":
                            sb.Append("<img src='../../../../images/imgUsuFM.gif'>");
                            break;
                    }
                }
                sb.Append("</td>");
                sb.Append("<td style='text-align:left;' ><nobr class='NBR' style='width:415px'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string obtenerProfesionales(string sAp1, string sAp2, string sNombre)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();
            //try
            //{
            SqlDataReader dr = SUPER.Capa_Negocio.USUARIO.GetProfMiEq(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                        false, null);

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH:450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:430px;text-align:left' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "'");
                sb.Append(" tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "'");
                //sb.Append(" onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W410' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='addItem(this)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            sResul = sb.ToString();
            //}
            //catch (System.Exception objError)
            //{
            //    sResul = "Error@#@" + Errores.mostrarError("Error al leer los profesionales ", objError);
            //}
            return sResul;
        }
        public static string Grabar(string strProfesionales)
        {
            string sResul = "", sElementosInsertados = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion
            try
            {
                #region Datos Profesionales
                if (strProfesionales != "")//No se ha modificado nada de la pestaña de Figuras
                {
                    string[] aProfesionales = Regex.Split(strProfesionales, "///");
                    foreach (string oProfesional in aProfesionales)
                    {
                        if (oProfesional == "") continue;
                        string[] aValores = Regex.Split(oProfesional, "##");
                        ///aValores[0] = bd
                        ///aValores[1] = idFicepi

                        switch (aValores[0])
                        {
                            case "I":
                                SUPER.DAL.EXCLUIDOS_MIEQ.Insert(tr, int.Parse(aValores[1]));
                                if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                                else sElementosInsertados += "//" + aValores[1];
                                break;
                            case "D":
                                SUPER.DAL.EXCLUIDOS_MIEQ.Delete(tr, int.Parse(aValores[1]));
                                break;
                        }
                    }
                }

                #endregion

                Conexion.CommitTransaccion(tr);
                sResul = sElementosInsertados;
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del profesional", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        #endregion
    }
}
