using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GEMO.DAL;
using Microsoft.JScript;
using System.Text;
using System.Text.RegularExpressions;

namespace GEMO.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : LINEA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T708_LINEA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public class LINEA
	{
		#region Constructor

		public LINEA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
        public static string obtenerProfesionales(string sAp1, string sAp2, string sNombre)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = GEMO.DAL.PROFESIONALES.ObtenerProfesionales(sAp1, sAp2, sNombre,0);

            sb.Append("<table id='tblOpciones' class='MAM' style='WIDTH: 400px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:380px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' style='height:22px;' ");
                sb.Append("onClick='mm(event)' onmousedown='DD(event)' insertarFigura='convocar(this);' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                //sb.Append("baja='" + dr["baja"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("><td></td><td><nobr class='NBR W370' onDblClick='insertarFigura(this.parentElement.parentElement)'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            sResul = sb.ToString();// +"@@" + sbBa.ToString();
            return sResul;
        }

        public static string generarExcel(
                                        Nullable<long> iNumLinea, 
                                        Nullable<int> iNumExt, 
                                        string sIMEI, 
                                        string sICC,
                                        Nullable<short> shPrefijo,
                                        string sEmpresas, 
                                        string sResponsables, 
                                        string sBeneficiarios,
                                        string sDepartamentos, 
                                        string sEstados, 
                                        string sMedios
                                    )
        {
            StringBuilder sb = new StringBuilder();
            int numContLineas = 0;

            /////tabla de líneas

            sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
            sb.Append("	<tr align='center'>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Estado</td>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Cod.País</td>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Línea</td>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Extensión </td>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Responsable</td>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Beneficiario / Departamento</td>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Modelo</td>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Proveedor</td>");
            sb.Append("	</tr>");

            SqlDataReader dr = GEMO.DAL.LINEA.Busqueda(iNumLinea, iNumExt, sIMEI, sICC, shPrefijo, sEmpresas, sResponsables, sBeneficiarios, sDepartamentos, sEstados, sMedios, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()));

            while (dr.Read())
            {
                sb.Append("<tr>");
                switch (dr["t710_idestado"].ToString().Trim())
                {
                    case ("X"):
                        {
                            sb.Append("<td style='align:right;'>Preactiva</td>");
                            break;
                        }
                    case ("A"):
                        sb.Append("<td style='align:right;'>Activa</td>");
                        break;
                    case ("B"):
                        {
                            sb.Append("<td style='align:right;'>Bloqueada</td>");
                            break;
                        }
                    case ("Y"):
                        {
                            sb.Append("<td style='align:right;'>Preinactiva</td>");
                            break;
                        }
                    case ("I"):
                        {
                            sb.Append("<td style='align:right;'>Inactiva</td>");
                            break;
                        }
                }

                sb.Append("<td style='padding-left:10px'>" + dr["t708_prefintern"].ToString() + "</td>");
                sb.Append("<td style='padding-left:5px; padding-right:5px;'>" + dr["t708_numlinea"].ToString() + "</td>");
                if (int.Parse(dr["t708_numext"].ToString()) == 0) sb.Append("<td></td>");
                else sb.Append("<td style='padding-left:5px; padding-right:2px;'>" + dr["t708_numext"].ToString() + "</td>");
                sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Responsable"].ToString() + "</nobr></td>");
                if (dr["Beneficiario"].ToString() != "")
                    sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Beneficiario"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Departamento"].ToString() + "</nobr></td>");

                sb.Append("<td>" + dr["t708_modelo"].ToString().Trim() + "</td>");
                sb.Append("<td>" + dr["t063_desproveedor"].ToString().Trim() + "</td>");
                sb.Append("</tr>");

                numContLineas++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("<tr>");
            sb.Append("        <td style='background-color: #BCD4DF; padding-left:3px;'>Nº de líneas: " + numContLineas.ToString() + "</td>");
            sb.Append("        <td style='background-color: #BCD4DF;'></td>");
            sb.Append("        <td style='background-color: #BCD4DF;'></td>");
            sb.Append("        <td style='background-color: #BCD4DF;'></td>");
            sb.Append("        <td style='background-color: #BCD4DF;'></td>");
            sb.Append("        <td style='background-color: #BCD4DF;'></td>");
            sb.Append("        <td style='background-color: #BCD4DF;'></td>");
            sb.Append("        <td style='background-color: #BCD4DF;'></td>");
            sb.Append("	</tr>");
            sb.Append("</table>");

            //return sb.ToString() + "@#@" + numContLineas;
            //return "OK@#@" + sb.ToString();
            string sIdCache = "EXCEL_CACHE_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            HttpContext.Current.Session[sIdCache] = sb.ToString(); ;
            //return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); 
            return "cacheado@#@" + sIdCache + "@#@" + sb.ToString(); 
        }
        public static string Busqueda(
                                        Nullable<long> iNumLinea, 
                                        Nullable<int> iNumExt, 
                                        string sIMEI, 
                                        string sICC,
                                        Nullable<short> shPrefijo,
                                        string sEmpresas, 
                                        string sResponsables, 
                                        string sBeneficiarios,
                                        string sDepartamentos, 
                                        string sEstados, 
                                        string sMedios
                                    )
        {
            StringBuilder sb = new StringBuilder();
            int numContLineas = 0;

            /////tabla de líneas
            sb.Append("<table id='tblDatos' class='MA' style='width:954px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px' />");
            sb.Append("    <col style='width:40px' />");
            sb.Append("    <col style='width:104px;' />"); //Línea
            sb.Append("    <col style='width:80px;' />"); //Extensión
            sb.Append("    <col style='width:355px;' />"); //Responsable
            sb.Append("    <col style='width:355px;' />"); //Beneficiario / Departamento
            sb.Append("</colgroup>");

            SqlDataReader dr = GEMO.DAL.LINEA.Busqueda(iNumLinea, iNumExt, sIMEI, sICC, shPrefijo, sEmpresas, sResponsables, sBeneficiarios, sDepartamentos, sEstados, sMedios, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t708_idlinea"].ToString() + "' ");
                sb.Append("estado='" + dr["t710_idestado"].ToString().Trim() + "' ");
                sb.Append("modelo=\"" + dr["t708_modelo"].ToString().Trim() + "\" ");
                sb.Append("proveedor=\"" + Utilidades.escape(dr["t063_desproveedor"].ToString().Trim()) + "\" ");
                sb.Append("bd='' onclick=\"mm(event);\" ondblclick=\"Detalle(this);\"");
                sb.Append("onmouseover='TTip(event)' ");
                sb.Append("style='height:20px;'>");

                sb.Append("<td></td>"); //para la imagen

                //if (dr["t708_prefintern"].ToString() != "")
                //    sb.Append("<td style='padding-left:10px'>" + short.Parse(dr["t708_prefintern"].ToString()).ToString("000") + "</td>");
                //else
                    sb.Append("<td style='padding-left:10px'>" + dr["t708_prefintern"].ToString() + "</td>");

                    sb.Append("<td style='padding-left:5px; padding-right:5px;'>" + dr["t708_numlinea"].ToString() + "</td>");

                if (int.Parse(dr["t708_numext"].ToString()) == 0) sb.Append("<td></td>");
                else sb.Append("<td style='padding-left:5px; padding-right:2px;'>" + dr["t708_numext"].ToString() + "</td>");

                sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Responsable"].ToString() + "</nobr></td>");
                if (dr["Beneficiario"].ToString() != "")
                    sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Beneficiario"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Departamento"].ToString() + "</nobr></td>");

                sb.Append("</tr>");
                numContLineas++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString() + "@#@" + numContLineas;
        }
        public static string Responsables()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.Responsables();

            sb.Append("<table id='tblResponsables' class='MA' style='WIDTH: 400px;'>");
            sb.Append("<colgroup><col style='width:400px; padding-left:3px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("style='height:20px;noWrap:true;'>");
                //sb.Append("<td onclick='setResponsable(this.parentElement);msse(this.parentElement);'  style='border-right: solid 1px #569BBD;'><nobr class='NBR W380'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("<td onclick='setResponsable(this.parentElement);ms(this.parentElement);'  style='border-right: solid 1px #569BBD;'><nobr class='NBR W380'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return  sb.ToString();
        }
        public static string Responsables(string sAp1, string sAp2, string sNombre)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.Responsables(sAp1, sAp2, sNombre);

            sb.Append("<table id='tblResponsables' class='MANO' style='WIDTH: 440px;'>");
            sb.Append("<colgroup><col style='width:440px; padding-left:3px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("style='height:20px;noWrap:true;'>");
                //sb.Append("<td onclick='setResponsable(this.parentElement);msse(this.parentElement);'  style='border-right: solid 1px #569BBD;'><nobr class='NBR W430'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("<td onclick='setResponsable(this.parentElement);ms(this.parentElement);'  style='border-right: solid 1px #569BBD;'><nobr class='NBR W430'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string Responsables(string sAp1, string sAp2, string sNombre, bool sMostrarBajas)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.Responsables(sAp1, sAp2, sNombre, sMostrarBajas);

            sb.Append("<table id='tblResponsables' class='MANO' style='WIDTH: 500px;'>");
            sb.Append("<colgroup><col style='width:500px; padding-left:3px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("style='height:20px;noWrap:true;'>");
                //sb.Append("<td onclick='setResponsable(this.parentElement);msse(this.parentElement);'  style='border-right: solid 1px #569BBD;'><nobr class='NBR W430'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("<td onclick='setResponsable(this.parentElement);ms(this.parentElement);'  style='border-right: solid 1px #569BBD;'><nobr class='NBR W430'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string ResponsablesLineas(int iIdFicepi)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.ResponsablesLineas(iIdFicepi);

            sb.Append("<table id='tblDatos' class='MAM' style='WIDTH: 440px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:87px; padding-left:3px;' />");
            sb.Append("    <col style='width:330px;' />");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t708_idlinea"].ToString() + "' ");
                sb.Append("estado='" + dr["t710_idestado"].ToString().Trim() + "' ");
                sb.Append("responsable_origen='" + dr["t001_responsable"].ToString() + "' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W80' ondblclick='insertarLinea(this.parentElement.parentElement)'>" + dr["t708_numlinea"].ToString() + "</nobr></td>");
                if (dr["Beneficiario"].ToString() != "") sb.Append("<td><nobr class='NBR W320' ondblclick='insertarLinea(this.parentElement.parentElement)' style='noWrap:true;'>" + dr["Beneficiario"].ToString() + "</nobr></td>");
                else sb.Append("<td><nobr class='NBR W320' ondblclick='insertarLinea(this.parentElement.parentElement)' style='noWrap:true;'>" + dr["Departamento"].ToString() + "</nobr></td>");
                    
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string ResponsablesLineasFactura(int iIdFicepi)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.ResponsablesLineasFactura(iIdFicepi);

            sb.Append("<table id='tblDatos' class='MA' style='WIDTH: 500px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:87px; padding-left:3px;' />");
            sb.Append("    <col style='width:60px; padding-left:3px;' />");
            sb.Append("    <col style='width:330px;' />");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t708_idlinea"].ToString() + "' ");
                sb.Append("estado='" + dr["t710_idestado"].ToString().Trim() + "' ");
                sb.Append("responsable_origen='" + dr["t001_responsable"].ToString() + "' ");
                sb.Append("beneficiario='" + dr["t001_beneficiario"].ToString() + "' ");
                
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W80' ondblclick='aceptarClick(this.parentElement.parentElement.rowIndex)'>" + dr["t708_numlinea"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W60' ondblclick='aceptarClick(this.parentElement.parentElement.rowIndex)'>" + dr["t708_numext"].ToString() + "</nobr></td>");
                if (dr["Beneficiario"].ToString() != "") sb.Append("<td><nobr class='NBR W320' ondblclick='aceptarClick(this.parentElement.parentElement.rowIndex)' style='noWrap:true;'>" + dr["Beneficiario"].ToString() + "</nobr></td>");
                else sb.Append("<td><nobr class='NBR W320' ondblclick='aceptarClick(this.parentElement.parentElement.rowIndex)' style='noWrap:true;'>" + dr["Departamento"].ToString() + "</nobr></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string Recuperar()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.REASIGLINEA.CatalogoDestinoResp(null, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()));

            sb.Append("<table id='tblDatos2' class='MM' style='WIDTH: 440px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:87px;padding-left:3px;' />");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("    <col style='width:30px;text-align:left' />");
            sb.Append("</colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t708_idlinea"].ToString() + "' ");
                sb.Append("estado='" + dr["t710_idestado"].ToString().Trim() + "' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>" + (char)10);
                sb.Append("<td>" + dr["t708_numlinea"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W290' style='noWrap:true;'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("<td></td></tr>" + (char)10);
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string Usuario(string sEstados, string sResponsable, string sBeneficiario)
        {
            StringBuilder sb = new StringBuilder();
            int numContLineas = 0;

            /////tabla de líneas

            sb.Append("<table id='tblDatos' ");
            if (HttpContext.Current.User.IsInRole("C")) sb.Append("class='MA' ");
            else sb.Append("class='MANO' ");

            sb.Append("style='width:954px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px' />");
            sb.Append("    <col style='width:40px' />");
            sb.Append("    <col style='width:104px;' />"); //Línea
            sb.Append("    <col style='width:80px; padding-left:5px; padding-right:2px;' />"); //Extensión
            sb.Append("    <col style='width:355px; padding-left:5px; padding-right:2px;' />"); //Responsable
            sb.Append("    <col style='width:355px; padding-left:5px; padding-right:2px;' />"); //Beneficiario / Departamento
            sb.Append("</colgroup>");

            SqlDataReader dr = GEMO.DAL.LINEA.Usuario(int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()),sEstados, sResponsable, sBeneficiario);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t708_idlinea"].ToString() + "' ");
                sb.Append("estado='" + dr["t710_idestado"].ToString().Trim() + "' ");
                sb.Append("bd='' onclick=\"mm(event);\"");
                if (HttpContext.Current.User.IsInRole("C")) sb.Append(" ondblclick=\"Detalle(this);\"");
                sb.Append("onmouseover='TTip(event)' ");
                sb.Append("style='height:20px;'>");

                sb.Append("<td></td>"); //para la imagen
                //if (dr["t708_prefintern"].ToString() != "")
                //    sb.Append("<td style='padding-left:10px'>" + short.Parse(dr["t708_prefintern"].ToString()).ToString("000") + "</td>");
                //else
                sb.Append("<td style='padding-left:10px'>" + dr["t708_prefintern"].ToString() + "</td>");

                sb.Append("<td style='padding-left:5px; padding-right:5px;'>" + dr["t708_numlinea"].ToString() + "</td>");

                if (int.Parse(dr["t708_numext"].ToString()) == 0) sb.Append("<td></td>");
                else sb.Append("<td style='padding-left:5px; padding-right:2px;'>" + dr["t708_numext"].ToString() + "</td>");

                sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Responsable"].ToString() + "</nobr></td>");
                if (dr["Beneficiario"].ToString()!="")
                    sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Beneficiario"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td style='padding-left:5px; padding-right:2px;'><nobr class='NBR W335'>" + dr["Departamento"].ToString() + "</nobr></td>");                  
                    
                sb.Append("</tr>");
                numContLineas++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString() + "@#@" + numContLineas;
        }
        public static void Eliminar(string args)
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
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                string[] aArgs = Regex.Split(args, "///");
                for (int i = 0; i < aArgs.Length; i++)
                {
                    GEMO.DAL.LINEA.Delete(tr, int.Parse(aArgs[0]));
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al borrar la lista de líneas.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }
        public static void ActualizarFacturadasNoInventariadas(string args)
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
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                GEMO.DAL.LINEA.ActualizarFacturadasNoInventariadas(tr, args);
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al registrar las líneas facturadas no inventariadas.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }
        public static void ActualizarActivasSinFactura(string args)
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
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                GEMO.DAL.LINEA.ActualizarActivasSinFactura(tr, args);
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al actualizar el estado de las línea a pre-inactiva.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }
        public static string Grabar   (
                                    byte byteNueva,                                 // 0=UPDATE 1=INSERT
                                    int iID,                                        // t708_idlinea
                                    Nullable<short> shPrefIntern,                   // t708_prefintern
                                    long iNumLinea,                                 // t708_numlinea
                                    int iNumExt,                                    // t708_numext
                                    Nullable<int> iIdempresa,                       // t313_idempresa 
                                    Nullable<byte> bTipoTarjeta,                    // t712_idtarjeta
                                    Nullable<byte> bIdProveedor,                    // t063_idproveedor
                                    Nullable<short> shIdMedio,                      // t134_idmedio
                                    string sModelo,                                 // t708_modelo
                                    string sIMEI,                                   // t708_IMEI 
                                    string sICC,                                    // t708_ICC 
                                    string sObserva,                                // t708_observa
                                    string sEstado,                                 // t710_idestado 
                                    Nullable<short> shIdTarifa,                     // t711_idtarifa
                                    Nullable<int> iResponsable,                     // t001_responsable 
                                    string sTipoUso,                                // t708_tipouso  
                                    Nullable<int> iBeneficiario,                    // t001_beneficiario 
                                    string sDepartamento,                           // t708_departamento 
                                    bool bQEQ                                       // t708_QEQ 
                                    )
        {
            int nID = -1;
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
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion
            try
            {
                if (byteNueva == 1)
                {
                    nID = GEMO.DAL.LINEA.Insert
                                    (
                                    tr,
                                    shPrefIntern,                                   // t708_prefintern                              
                                    iNumLinea,                                      // t708_numlinea
                                    iNumExt,                                        // t708_numext
                                    iIdempresa,                                     // t313_idempresa 
                                    bTipoTarjeta,                                   // t712_idtarjeta
                                    bIdProveedor,                                   // t063_idproveedor
                                    shIdMedio,                                      // t134_idmedio
                                    sModelo,                                        // t708_modelo
                                    sIMEI,                                          // t708_IMEI 
                                    sICC,                                           // t708_ICC 
                                    sObserva,                                       // t708_observa
                                    sEstado,                                        // t710_idestado 
                                    shIdTarifa,                                     // t711_idtarifa
                                    iResponsable,                                   // t001_responsable 
                                    sTipoUso,                                       // t708_tipouso  
                                    iBeneficiario,                                  // t001_beneficiario 
                                    sDepartamento,                                  // t708_departamento 
                                    bQEQ                                            // t708_QEQ 
                                    );
                }
                else //update
                {
                    GEMO.DAL.LINEA.Update(
                                    tr,
                                    iID,
                                    shPrefIntern,                                   // t708_prefintern  
                                    iNumLinea,                                      // t708_numlinea
                                    iNumExt,                                        // t708_numext
                                    iIdempresa,                                     // t313_idempresa 
                                    bTipoTarjeta,                                   // t712_idtarjeta
                                    bIdProveedor,                                   // t063_idproveedor
                                    shIdMedio,                                      // t134_idmedio
                                    sModelo,                                        // t708_modelo
                                    sIMEI,                                          // t708_IMEI 
                                    sICC,                                           // t708_ICC 
                                    sObserva,                                       // t708_observa
                                    sEstado,                                        // t710_idestado 
                                    shIdTarifa,                                     // t711_idtarifa
                                    iResponsable,                                   // t001_responsable 
                                    sTipoUso,                                       // t708_tipouso  
                                    iBeneficiario,                                  // t001_beneficiario 
                                    sDepartamento,                                  // t708_departamento 
                                    bQEQ                                            // t708_QEQ 
                                    );
                    nID = iID;
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar la linea ("+iNumLinea.ToString()+")", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return nID.ToString("#,###");
        }

        public static void Procesar(string strDatos)
        {
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            string sResul = "";

            int idLin = 0, responsable_origen = 0, responsable_destino = 0;

            try
            {
                GEMO.DAL.REASIGLINEA.Delete(null, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()));

                string[] aDatos = Regex.Split(strDatos, "///");
                foreach (string oLinea in aDatos)
                {
                    try
                    {
                        if (oLinea == "") continue;
                        string[] aLinea = Regex.Split(oLinea, "##");
                        ///aLinea[0] = idLin
                        ///aLinea[1] = responsable_origen
                        ///aLinea[2] = responsable_destino
                        ///aLinea[3] = procesado

                        idLin = int.Parse(aLinea[0]);
                        responsable_origen = int.Parse(aLinea[1]);
                        responsable_destino = int.Parse(aLinea[2]);

                        GEMO.DAL.REASIGLINEA.Insert(null, idLin, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()), responsable_destino, false, "");

                        if (aLinea[3] == "1" || aLinea[1] == aLinea[2])
                        {
                            GEMO.DAL.REASIGLINEA.Update(null, idLin, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()), responsable_destino, true, "");
                            continue;
                        }
                        #region abrir conexión y transacción
                        try
                        {
                            oConn = Conexion.Abrir();
                            tr = Conexion.AbrirTransaccionSerializable(oConn);
                        }
                        catch (Exception ex)
                        {
                            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                            throw (new Exception("Error al abrir la conexión."));
                        }
                        #endregion

                        GEMO.DAL.LINEA.UpdateReasigRes(tr, idLin, responsable_destino);

                        //update proceso OK
                        GEMO.DAL.REASIGLINEA.Update(null, idLin, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()), responsable_destino, true, "");

                        Conexion.CommitTransaccion(tr);
                    }
                    catch (Exception ex)
                    {
                        Conexion.CerrarTransaccion(tr);
                        //update proceso KO
                        GEMO.DAL.REASIGLINEA.Update(null, idLin, int.Parse(HttpContext.Current.Session["IDFICEPI"].ToString()), responsable_destino, false, ex.Message);
                        sResul = "Error@#@" + Errores.mostrarError("Error al procesar la reasignación.", ex);
                    }
                    finally
                    {
                        Conexion.Cerrar(oConn);
                        if (sResul!="")
                            throw (new Exception(sResul));
                    }

                }// fin foreach
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al procesar la reasignación.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }
        public static string FacturadasNoInventariadas()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.FacturadasNoInventariadas();

            sb.Append("<table id='tblDatos1' class='MANO' style='WIDTH: 430px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:230px;' />");
            sb.Append("    <col style='width:200px;text-align:right;' />");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["TEL_NU_TELEFONO"].ToString() + "' ");
                sb.Append("onclick=\"mm(event);\" ");
                sb.Append("style='height:20px' >");
                sb.Append("<td>" + dr["TEL_NU_TELEFONO"].ToString() + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["TEL_SUMA_TOTAL"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string ActivasSinFactura()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.ActivasSinFactura();

            sb.Append("<table id='tblDatos2' class='MA' style='WIDTH: 430px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:430px;' />");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T708_idlinea"].ToString() + "'");
                sb.Append(" onclick=\"mm(event);\" ");
                sb.Append(" onDblClick='detalle(this.id);'");
                sb.Append("style='height:20px' >");
                sb.Append("<td>" + dr["T708_numlinea"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string InactivasYBloqueadasConFactura()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.InactivasYBloqueadasConFactura();

            sb.Append("<table id='tblDatos3' class='MA' style='WIDTH: 430px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:230px;' />");
            sb.Append("    <col style='width:200px;text-align:right;' />");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T708_idlinea"].ToString() + "' ");
                sb.Append(" onclick=\"ms(this);\" ");
                sb.Append(" onDblClick='detalle(this.id);'");                    
                sb.Append("style='height:20px' >");
                sb.Append("<td>" + dr["TEL_NU_TELEFONO"].ToString() + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["TEL_SUMA_TOTAL"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string DescuadresVarios(int iAnnomes , int iProveedor )
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.LINEA.DescuadresVarios(iAnnomes , iProveedor );

            sb.Append("<table id='tblDatos4' class='MA' style='WIDTH: 430px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:230px;' />");
            sb.Append("    <col style='width:200px;text-align:right;' />");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T708_idlinea"].ToString() + "' ");
                sb.Append(" per='" + dr["perfil"].ToString() + "' ");
                sb.Append(" tpc='" + dr["tarifa_plana_cont"].ToString() + "' ");
                sb.Append(" gem='" + dr["gemo"].ToString() + "' ");
                sb.Append(" est='" + dr["estado"].ToString() + "' ");
                sb.Append(" uli='" + dr["usuario_linea"].ToString() + "' ");
                sb.Append(" ufa='" + dr["usuario_factura"].ToString() + "' ");
                sb.Append(" tda='" + dr["tarifa_datos"].ToString() + "' ");
                sb.Append(" den='" + dr["t134_denominacion"].ToString() + "' ");
                sb.Append(" med='" + dr["t134_idmedio"].ToString() + "' ");
                sb.Append(" onclick=\"ms(this);\" ");
                sb.Append(" onDblClick='detalle(this.id);'"); 
                sb.Append("style='height:20px' >");
                sb.Append("<td>" + dr["linea"].ToString() + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["TARIFA_PLANA_FACT"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
		#endregion
	}



}
