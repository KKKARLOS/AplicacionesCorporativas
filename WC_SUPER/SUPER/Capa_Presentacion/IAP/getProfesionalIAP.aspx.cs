using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

namespace SUPER.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerRecurso.
	/// </summary>
    public partial class getProfesionalIAP : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string sPerfil = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }

        public void RaiseCallbackEvent(string eventArg)
        {
            string sResultado = "";
            //1º Si hubiera argumentos, se recogen y tratan.
            //string MisArg = eventArg;
            string[] aArgs = Regex.Split(eventArg, "@#@");
            sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
            

            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("profesionales"):
                    sResultado += obtenerRecursosReconexion(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                    break;
                case ("setUsuarioIAP"):
                    sResultado += establecerUsuarioIAP(aArgs[1]);
                    break;
            }

            //3º Damos contenido a la variable que se envía de vuelta al cliente.
            _callbackResultado = sResultado;
        }
        public string GetCallbackResult()
        {
            //Se envía el resultado al cliente.
            return _callbackResultado;
        }

        private string obtenerRecursosReconexion(string sPerfil, string sAp1, string sAp2, string sNombre)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 396px;text-align:left;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:376px;'/></colgroup>");
            sb.Append("<tbody>");
            try
            {
                SqlDataReader dr = USUARIO.ObtenerProfesionalesIAP((int)Session["UsuarioActual"], sPerfil, Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()));
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    //sb.Append("ondblclick=\"aceptarClick(this.rowIndex)\" style='noWrap:true;height:20px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                    sb.Append("ondblclick=\"aceptarClick(this.rowIndex)\" style='noWrap:true;height:20px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W360'>" + dr["profesional"].ToString() + "</nobr></td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");
                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener los profesionales.", ex);
            }
        }
        private string establecerUsuarioIAP(string sUsuario)
        {
            string sResul = "";

            try
            {
                Recurso objRec = new Recurso();
                bool bIdentificado = objRec.ObtenerRecursoBaja(null, int.Parse(sUsuario));

                if (bIdentificado)
                {
                    //VARIABLES NECESARIAS PARA IAP
                    Session["IDFICEPI_IAP"] = objRec.IdFicepi;
                    Session["NUM_EMPLEADO_IAP"] = objRec.IdUsuario;
                    //Session["DES_EMPLEADO_IAP"] = objRec.Apellido1 + " " + objRec.Apellido2 + ", " + objRec.Nombre;
                    Session["DES_EMPLEADO_IAP"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                    Session["IDRED_IAP"] = objRec.sCodRed;
                    Session["JORNADA_REDUCIDA"] = objRec.JornadaReducida;
                    Session["CONTROLHUECOS"] = objRec.ControlHuecos;
                    Session["IDCALENDARIO_IAP"] = objRec.IdCalendario;
                    Session["DESCALENDARIO_IAP"] = objRec.DesCalendario;
                    Session["COD_CENTRO"] = objRec.CodCentro;
                    Session["DES_CENTRO"] = objRec.DesCentro;
                    Session["FEC_ULT_IMPUTACION"] = (objRec.FecUltImputacion.HasValue) ? ((DateTime)objRec.FecUltImputacion.Value).ToShortDateString() : null;
                    Session["FEC_ALTA"] = objRec.FecAlta.ToShortDateString();
                    Session["FEC_BAJA"] = (objRec.FecBaja.HasValue) ? ((DateTime)objRec.FecBaja.Value).ToShortDateString() : null;

                    Session["UMC_IAP"] = (objRec.UMCIAP.HasValue) ? (int?)objRec.UMCIAP.Value : DateTime.Now.AddMonths(-1).Year * 100 + DateTime.Now.AddMonths(-1).Month;
                    Session["NHORASRED"] = objRec.nHorasJorRed;
                    Session["FECDESRED"] = (objRec.FecDesdeJorRed.HasValue) ? ((DateTime)objRec.FecDesdeJorRed.Value).ToShortDateString() : null;
                    Session["FECHASRED"] = (objRec.FecHastaJorRed.HasValue) ? ((DateTime)objRec.FecHastaJorRed.Value).ToShortDateString() : null;
                    Session["aSemLab"] = objRec.sSemanaLaboral;
                    Session["SEXOUSUARIO"] = objRec.sSexo;
                    Session["TIPORECURSO"] = objRec.sTipoRecurso;
                    //FIN VARIABLES NECESARIAS PARA IAP

                    sResul = "OK@#@";
                    sResul += Session["NUM_EMPLEADO_IAP"].ToString() + "///";
                    sResul += Utilidades.escape(Session["DES_EMPLEADO_IAP"].ToString()) + "///";
                    sResul += Session["IDCALENDARIO_IAP"] + "///";
                    sResul += Utilidades.escape(Session["DESCALENDARIO_IAP"].ToString()) + "///";
                    sResul += ((bool)Session["CONTROLHUECOS"]) ? "1///" : "0///";
                    sResul += Session["FEC_ULT_IMPUTACION"] + "///";
                    sResul += Session["UMC_IAP"] + "///";
                    sResul += objRec.sSemanaLaboral + "///";
                    sResul += Session["IDFICEPI_IAP"] + "///";
                    sResul += Session["TIPORECURSO"] + "///";
                    sResul += Session["SEXOUSUARIO"];
                }
                else
                {
                    sResul = "Error@#@No se ha encontrado usuario " + sUsuario;
                }
            }
            catch (Exception ex)
            {
                //sResul = "Error@#@" + ex.ToString();
                sResul = "Error@#@" + Errores.mostrarError("No se han podido obtener los datos del usuario", ex);
            }
            return sResul;
        }

	}
}
