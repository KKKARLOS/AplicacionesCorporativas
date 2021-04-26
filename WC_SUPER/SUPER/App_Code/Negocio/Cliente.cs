using System;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
	public partial class CLIENTE
	{
        #region Propiedades y Atributos complementarios

        private string _NIF;
        public string NIF
        {
            get { return _NIF; }
            set { _NIF = value; }
        }

        private string _direccion;
        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private string _denominacion_SAP;
        public string denominacion_SAP
        {
            get { return _denominacion_SAP; }
            set { _denominacion_SAP = value; }
        }

        #endregion

		#region Metodos

        public static CLIENTE ObtenerResponsablePago(SqlTransaction tr, int t302_idcliente_destfact, string t621_idovsap)
        {
            CLIENTE o = new CLIENTE();

            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t302_idcliente_destfact", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente_destfact;
            aParam[2] = new SqlParameter("@t621_idovsap", SqlDbType.VarChar, 4);
            aParam[2].Value = t621_idovsap;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_RESPPAGO", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLIENTE_RESPPAGO", aParam);

            if (dr.Read())
            {
                if (dr["t302_idcliente"] != DBNull.Value)
                    o.t302_idcliente = int.Parse(dr["t302_idcliente"].ToString());
                if (dr["t302_denominacion"] != DBNull.Value)
                    o.t302_denominacion = (string)dr["t302_denominacion"];
                if (dr["NIF"] != DBNull.Value)
                    o.NIF = (string)dr["NIF"];
                if (dr["direccion"] != DBNull.Value)
                    o.direccion = (string)dr["direccion"];
                if (dr["denominacion_SAP"] != DBNull.Value)
                    o.denominacion_SAP = (string)dr["denominacion_SAP"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CLIENTE"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static SqlDataReader SelectByNombre(string t302_denominacion, string sTipoBusqueda, bool bSoloActivos, bool bInternos, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t302_denominacion", SqlDbType.Text, 100);
            aParam[0].Value = t302_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusqueda", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
            aParam[2].Value = bSoloActivos;
            aParam[3] = new SqlParameter("@bInternos", SqlDbType.Bit, 1);
            aParam[3].Value = bInternos;
            aParam[4] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[4].Value = t314_idusuario;
            //Por indicación de Victor el 19/02/2010 comentamos la selección de cliente por visibilidad
            //de forma que la select se hace sobre todos los clientes
            //if (t314_idusuario == null)
            //    return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_ByNombre", aParam);
            //else
            //{
            //    if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")
            //        return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_ByNombre", aParam);
            //    else
            //        return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_ByNombre_USU", aParam);
            //}
            return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_ByNombre", aParam);
        }
        public static SqlDataReader SelectByNombreSAP(string t302_denominacion, string sTipoBusqueda, bool bSoloActivos, bool bInternos, string sTipoCliente)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t302_denominacion", SqlDbType.Text, 100);
            aParam[0].Value = t302_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusqueda", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@bSoloActivos", SqlDbType.Bit, 1);
            aParam[2].Value = bSoloActivos;
            aParam[3] = new SqlParameter("@bInternos", SqlDbType.Bit, 1);
            aParam[3].Value = bInternos;
            aParam[4] = new SqlParameter("@sTipoCliente", SqlDbType.Char, 1);
            aParam[4].Value = sTipoCliente;

            return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_ByNombre_SAP", aParam);
        }

        public static SqlDataReader DeUnResponsable(Nullable<int> idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_RESPON", aParam);
        }
        public static SqlDataReader ObtenerDestinatariosDeFactura(int t302_idcliente_respago, string t621_idovsap)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t302_idcliente_respago", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente_respago;
            aParam[2] = new SqlParameter("@t621_idovsap", SqlDbType.VarChar, 4);
            aParam[2].Value = t621_idovsap;

            return SqlHelper.ExecuteSqlDataReader("SUP_CLIENTE_DESTFACT", aParam);
        }
        public static string ObtenerDireccion(int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteScalar("SUP_CLIENTE_DIRECCION", aParam).ToString();
        }
        public static int ObtenerClientePIG(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CLIENTEPIG", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CLIENTEPIG", aParam));
        }   

        public static bool EsSolicitanteSAP(SqlTransaction tr, int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CLIENTE_SOLICITANTE", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CLIENTE_SOLICITANTE", aParam)) == 0) ? false : true;
        }
        public static bool EstaBloqueadoSAP(SqlTransaction tr, int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[1].Value = t302_idcliente;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CLIENTE_BLOQUEADO", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CLIENTE_BLOQUEADO", aParam)) == 0) ? false : true;
        }
        public static string ObtenerClientes(string sTipoBusqueda, string strCli)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.CLIENTE.ObtenerClientes(Utilidades.unescape(strCli), sTipoBusqueda, true, false, null);

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 450px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:450px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' ");

                sb.Append("onclick='mm(event);' ondblclick='insertarItem(this);' onmousedown='DD(event)' style='height:20px'>");
                sb.Append("<td style='padding-left:5px;'><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/img" + dr["tipo"].ToString() + ".gif' ");
                if (dr["tipo"].ToString() == "M") sb.Append("style='margin-right:5px;'");
                else sb.Append("style='margin-left:15px;margin-right:5px;'");
                sb.Append("><nobr class='NBR W410'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");

                //sb.Append("<td style='padding-left:5px;'><nobr class='NBR W445'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        public static string ObtenerClientesAvisosExcepciones()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.Capa_Datos.CLIENTE.ObtenerClientesAvisosExcepciones(null);

            sb.Append("<table id='tblDatos2' style='width: 450px;' class='texto MM' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:435px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' ");

                sb.Append("onclick='mm(event);' ondblclick='aceptarClick(this.rowIndex);' onmousedown='DD(event)' style='height:20px'>");
                sb.Append("<td style='padding-left:2px;'><img src='" + HttpContext.Current.Session["strServer"].ToString() + "images/imgFN.gif'></td>");
                sb.Append("<td><nobr class='NBR W435'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;

        }

        public static string Grabar(string strDatos)
        {
            string sElementosInsertados = "";

            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión. " + ex.Message));
            }
            #endregion

            try
            {
                if (strDatos != "") //No se ha modificado nada 
                {
                    string[] aDatos = Regex.Split(strDatos, "///");
                    foreach (string oDatos in aDatos)
                    {
                        if (oDatos == "") continue;
                        string[] aValores = Regex.Split(oDatos, "##");

                        ///aValores[0] = bd
                        ///aValores[1] = t001_idficepi

                        switch (aValores[0])
                        {
                            case "I":
                                SUPER.Capa_Datos.CLIENTE.Update(tr, int.Parse(aValores[1]), true);
                                if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                                else sElementosInsertados += "//" + aValores[1];
                                break;
                            case "D":
                                SUPER.Capa_Datos.CLIENTE.Update(tr, int.Parse(aValores[1]), false);
                                break;
                        }

                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al actualizar las alertas del profesional.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            sResul = sElementosInsertados;
            return "OK@#@" + sResul;
        }

        #endregion
	}
}
