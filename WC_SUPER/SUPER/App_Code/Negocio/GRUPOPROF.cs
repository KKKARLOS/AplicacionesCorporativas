using System;
using System.Web.SessionState;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
using System.Text;
/// <summary>
/// Mantenimiento de Grupo Funcionales y de los profesionales que los componen
/// </summary>
namespace SUPER.Capa_Negocio
{
    public class GrupoProf
    {
    #region Atributos privados

    private string _sIdGP;
    private string _sDesGP;

    #endregion
    #region Propiedades públicas

    public string sIdGP
    {
        get { return _sIdGP; }
        set { _sIdGP = value; }
    }

    public string sDesGP
    {
        get { return _sDesGP; }
        set { _sDesGP = value; }
    }

    #endregion

    public GrupoProf()
	{
		//
		// TODO: Add constructor logic here
		//
	}
        #region Insertar

        public static string InsertarGrupo(SqlTransaction tr, string sDesc)
        {
            object nResul;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sDesc", SqlDbType.VarChar, 50);
            aParam[0].Value = sDesc;

            if (tr == null) nResul = SqlHelper.ExecuteScalar("SUP_GP_I", aParam);
            else nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GP_I", aParam);
            return nResul.ToString();
        }
        public static int InsertarProfesional(SqlTransaction tr, string sCodGrupo, int t001_idficepi)
        {
            object nResul;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sIdGrupo", SqlDbType.UniqueIdentifier,16);
            if (sCodGrupo == null) aParam[0].Value = null;
            else aParam[0].Value = new Guid(sCodGrupo);

            aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi;

            if (tr == null) nResul = SqlHelper.ExecuteScalar("SUP_GP_PROFI", aParam);
            else nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GP_PROFI", aParam);
            return int.Parse(nResul.ToString());
        }

        #endregion

        #region Modificar
        public static void ModificarGrupo(SqlTransaction tr, string sCodGrupo, string sDesc)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sIdGP", SqlDbType.UniqueIdentifier, 16);
            if (sCodGrupo == null) aParam[0].Value = null;
            else aParam[0].Value = new Guid(sCodGrupo);

            aParam[1] = new SqlParameter("@sDesc", SqlDbType.VarChar, 50);
            aParam[1].Value = sDesc;

            if (tr == null) SqlHelper.ExecuteScalar("SUP_GP_U", aParam);
            else SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GP_U", aParam);
        }
        #endregion

        #region Borrar
        public static void BorrarGrupo(SqlTransaction tr, string sCodGrupo)
        {
            int nResul;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sCodigo", SqlDbType.UniqueIdentifier, 16);
            if (sCodGrupo == null) aParam[0].Value = null;
            else aParam[0].Value = new Guid(sCodGrupo);

            if (tr == null) nResul = SqlHelper.ExecuteNonQuery("SUP_GP_D", aParam);
            else nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GP_D", aParam);
            return;
        }
        public static void BorrarProfesional(SqlTransaction tr, string sCodGrupo, int t001_idficepi)
        {
            int nResul;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sIdGP", SqlDbType.UniqueIdentifier, 16);
            if (sCodGrupo == null) aParam[0].Value = null;
            else aParam[0].Value = new Guid(sCodGrupo);
            aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi;
            if (tr == null) nResul = SqlHelper.ExecuteNonQuery("SUP_GP_PROFD", aParam);
            else nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GP_PROFD", aParam);
            return;
        }
        #endregion
        public void Obtener(string sIdGP)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sIdGP", SqlDbType.UniqueIdentifier, 16);
            if (sIdGP == null) aParam[0].Value = null;
            else aParam[0].Value = new Guid(sIdGP);
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_GP_S", aParam);

            if (dr.Read())
            {
                this.sIdGP = sIdGP;
                this.sDesGP = dr["nombre"].ToString();
            }
            dr.Close();
            dr.Dispose();
        }
        public static string Integrantes(string t295_uidequipo)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML

            sb.Append(@"<table id='tblOpciones2' class='texto' style='width: 430px;'>
                        <colgroup><col style='width:20px;' /><col style='width:410px;'/></colgroup>");
            #endregion
            sb.Append("<tbody id='tbodyDestino'>");
            SqlDataReader dr = SUPER.Capa_Datos.GrupoProf.Integrantes(t295_uidequipo);
            while (dr.Read())
            {
                //sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' bd='' onclick='mm(event)' style='height:20px' onmousedown='DD(event)' ");
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' bd='' style='height:20px' ");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["DENOMINACION"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["IDENTIFICADOR"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append(">");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W405'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        public static string Catalogo(int iFicepiEntrada)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML

            sb.Append(@"<table id='tblDatos' class='texto MA' style='width: 430px;'mantenimiento='1'>
                        <colgroup><col style='width:15px;' /><col style='width:415px;'/></colgroup>");
            #endregion
            SqlDataReader dr = SUPER.Capa_Datos.GrupoProf.Catalogo(iFicepiEntrada);
            while (dr.Read())
            {
                sb.Append("<tr bd='' id='" + dr["IDENTIFICADOR"].ToString() + "'  onclick='mm(event);mostrarIntegrantes(this.id);'");
                sb.Append(" ondblclick='Detalle(this)' ");
                sb.Append(" style='height:20px;'>");
                sb.Append("<td><img src='../../../../../../images/imgFN.gif'></td>");
                sb.Append("<td><nobr class='NBR W415' onmouseover='TTip(event)'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        public static string Seleccionar(int iFicepiEntrada)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML

            sb.Append(@"<table id='tblDatos' class='texto MA' style='width: 450px;'mantenimiento='1'>
                        <colgroup><col style='width:450px;'/></colgroup>");
            #endregion
            SqlDataReader dr = SUPER.Capa_Datos.GrupoProf.Catalogo(iFicepiEntrada);
            while (dr.Read())
            {
                sb.Append("<tr bd='' id='" + dr["IDENTIFICADOR"].ToString() + "' onclick='ms(this);'");
                sb.Append(" ondblclick='aceptarClick(this.rowIndex)' ");
                sb.Append(" style='height:16px;'>");
                sb.Append("<td><nobr class='NBR W445' onmouseover='TTip(event)'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            //return "OK@#@" + sb.ToString();
            return sb.ToString();
        }
        public static string Eliminar(string strGrupo)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            SqlDataReader dr = null;

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
                string[] aGP = Regex.Split(strGrupo, "##");
                string sDenomGrupo = "";
                ArrayList aListDestinatarios = new ArrayList();
                foreach (string oGP in aGP)
                {
                    if (oGP != "")
                    {
                        //Leo integrantes para notificar correo
                        dr = SUPER.Capa_Datos.GrupoProf.Integrantes(oGP);
                        while (dr.Read())
                        {
                            aListDestinatarios.Add(dr["t001_codred"].ToString());
                        }

                        dr.Close();
                        dr.Dispose();
                        //Leo denominación grupo
                        GrupoProf miGP = new GrupoProf();
                        miGP.Obtener(oGP);
                        sDenomGrupo = miGP.sDesGP;
                        miGP = null;

                        GrupoProf.BorrarGrupo(tr, oGP);
                    }
                }

                Conexion.CommitTransaccion(tr);

                string sTexto = "";
                string sAsunto = "";
                string sTO = "";

                sAsunto = "Borrado de equipo de profesionales";
                //sTexto = @" <LABEL class='TITULO'>Informarle que el profesional " + HttpContext.Current.Session["APELLIDO1"] + " " + HttpContext.Current.Session["APELLIDO2"] + ", " + HttpContext.Current.Session["NOMBRE"] + ", ha procedido a borrar el grupo '" + sDenomGrupo + "' del que eres integrante. </LABEL>";
                sTexto = @" <LABEL class='TITULO'>El profesional " + HttpContext.Current.Session["APELLIDO1"] + " " + HttpContext.Current.Session["APELLIDO2"] + ", " + HttpContext.Current.Session["NOMBRE"] + ", ha procedido a borrar el grupo '" + sDenomGrupo + "' del que eres integrante. </LABEL>";

                ArrayList aListCorreo = new ArrayList();

                for (int i = 0; i < aListDestinatarios.Count; i++)
                {
                    sTO = (string)aListDestinatarios[i];
                    string[] aMail = { sAsunto, sTexto, sTO, "" };
                    aListCorreo.Add(aMail);
                }

                Correo.EnviarCorreosCAUDEF(aListCorreo);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al eliminar el grupo de profesionales.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }

            // Enviar correos a los integrantes del grupo

            return "OK@#@" + sResul;
        }  
        public static string Catalogo()
        {
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = SUPER.Capa_Datos.GrupoProf.Catalogo();

            sb.Append(@"<table id='tblDatos' class='texto MA' style='width: 430px;'mantenimiento='1'>
                        <colgroup><col style='width:15px;' /><col style='width:415px;'/></colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr bd='' id='" + dr["IDENTIFICADOR"].ToString() + "' onclick='mostrarIntegrantes(this.id);ms(this);'");
                sb.Append(" ondblclick='Detalle(this)' ");
                sb.Append(" style='height:20px;'>");
                sb.Append("<td><img src='../../../../../../images/imgFN.gif'></td>");
                sb.Append("<td><nobr class='NBR W415' onmouseover='TTip(event)'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");           
            dr.Close();
            dr.Dispose();
            return "OK@#@" + sb.ToString();
        }
        public static string DetalleIntegrantes(string sCodGP)
        {// Devuelve el código HTML del catalogo de personas que son integrantes del grupo de profesionales que se pasa como parametro
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = SUPER.Capa_Datos.GrupoProf.Integrantes(sCodGP);

            sb.Append("<table id='tblOpciones2' class='texto MM' style='WIDTH: 390px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:360px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' bd='' onClick='mm(event)' style='height:20px' onmousedown='DD(event)' ");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["DENOMINACION"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["IDENTIFICADOR"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("codred='" + dr["T001_CODRED"].ToString() + "' ");
                sb.Append(">");
                sb.Append("<td></td><td></td>");
                sb.Append("<td><nobr class='NBR W355'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close();
            dr.Dispose();
            //return "OK@#@" + sb.ToString();
            return sb.ToString();
        }

        public static string ObtenerPersonas(string sAP1, string sAP2, string sNom, string sCR)
        {// Devuelve el código HTML para la lista de candidatos
            StringBuilder sb = new StringBuilder();
            string sCod, sDes, sV1, sV2, sV3;

            sV1 = Utilidades.unescape(sAP1);
            sV2 = Utilidades.unescape(sAP2);
            sV3 = Utilidades.unescape(sNom);

            SqlDataReader dr = Recurso.ObtenerProfesionales(sV1, sV2, sV3);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup><tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sCod = dr["t001_idficepi"].ToString();
                sDes = dr["TECNICO"].ToString();

                sb.Append("<tr id='" + sCod + "'");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["TECNICO"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["NUM_EMPLEADO"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" onClick='mm(event)' onDblClick='anadirConvocados();' onmousedown='DD(event)' style='height:20px' ");
                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append("><td></td><td><span class='NBR W325'>" + sDes + "</span></td></tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close(); dr.Dispose();
            return "OK@#@" + sb.ToString();
        }
        public static string Grabar(string sGrupoFuncional, string sIntegrantes)
        {   //En el primer parametro de entrada tenemos codGf#descGF 
            //y en el segundo una lista de codigos de personas separados por comas (persona#responsable,)
            string sCad, sResul = "", sDesGP, sOperacion, sCodGP,sCodRed;
            bool bAlta = false;
            int iEmpleado, iPos;
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            try
            {
                //Recojo el código de GF 
                iPos = sGrupoFuncional.IndexOf("#");
                sCodGP = sGrupoFuncional.Substring(0, iPos);
                sDesGP = Utilidades.unescape(sGrupoFuncional.Substring(iPos + 1));
                //Abro transaccion
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);

                if (sCodGP != "")
                {
                    GrupoProf.ModificarGrupo(null, sCodGP, sDesGP);
                }
                else
                {
                    bAlta = true;
                    sCodGP = GrupoProf.InsertarGrupo(null, sDesGP);
                    HttpContext.Current.Session["nIdGrupo"] = sCodGP;
                    GrupoProf.InsertarProfesional(tr, sCodGP, int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()));
                }
                ArrayList aListDestinatarios = new ArrayList();

                //Borrar los integrantes existentes
                //GrupoFun.BorrarIntegrantes(tr,iCodGF);
                if (sIntegrantes == "")
                {//Tenemos lista vacía. No hacemos nada
                }
                else
                {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aTareas = Regex.Split(sIntegrantes, @",");

                    for (int i = 0; i < aTareas.Length - 1; i++)
                    {
                        sCad = aTareas[i];
                        if (sCad != "")
                        {
                            string[] aElem = Regex.Split(sCad, @"##");
                            sOperacion = aElem[0];
                            iEmpleado = System.Convert.ToInt32(aElem[1]);
                            sCodRed = aElem[2];
                            if (bAlta && int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()) == iEmpleado) continue;

                            //GrupoFun.InsertarIntegrante(tr, iCodGF, iEmpleado, iResponsable);
                            switch (sOperacion)
                            {
                                case "D":
                                    GrupoProf.BorrarProfesional(tr, sCodGP, iEmpleado);
                                    aListDestinatarios.Add(sCodRed);
                                    break;

                                case "I":
                                    GrupoProf.InsertarProfesional(tr, sCodGP, iEmpleado);
                                    break;
                            }//switch
                        }
                    }//for
                }
                //Cierro transaccion
                Conexion.CommitTransaccion(tr);

                string sTexto = "";
                string sAsunto = "";
                string sTO = "";

                sAsunto = "Borrado de un integrante de un grupo de profesionales";
                sTexto = @" <LABEL class='TITULO'>Informarle que el profesional " + HttpContext.Current.Session["APELLIDO1"] + " " + HttpContext.Current.Session["APELLIDO2"] + ", " + HttpContext.Current.Session["NOMBRE"] + ", te ha borrado del grupo '" + sDesGP + "' del que eres integrante. </LABEL>";

                ArrayList aListCorreo = new ArrayList();

                for (int i = 0; i < aListDestinatarios.Count; i++)
                {
                    sTO = (string)aListDestinatarios[i];
                    string[] aMail = { sAsunto, sTexto, sTO, "" };
                    aListCorreo.Add(aMail);
                }

                Correo.EnviarCorreosCAUDEF(aListCorreo);
                sCad = DetalleIntegrantes(sCodGP);
                sResul = "OK@#@" + sCodGP + "@#@" + sCad;
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }
    }
}
    