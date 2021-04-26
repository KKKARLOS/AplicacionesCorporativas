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
    /// Descripción breve de obtenerPST.
	/// </summary>
    public partial class obtenerPST : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores;
        public string strTablaHtml;

		private void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsCallback)
            {
                try
                {
                    if (Session["IDRED"] == null)
                    {
                        try
                        {
                            Response.Redirect("~/SesionCaducadaModal.aspx", true);
                        }
                        catch (System.Threading.ThreadAbortException) { return; }
                    }

                    string sCR = Request.QueryString["nCR"].ToString();
                    string sCli = Request.QueryString["sIdCli"].ToString();
                    this.hdnCR.Value = sCR;
                    this.hdnIdCliente.Value = sCli;
                    if (sCli == "")
                    {
                        this.chkCliente.Checked = false;
                        this.chkClienteNulo.Checked = false;
                        this.chkClienteNulo.Visible = false;
                        this.chkCliente.Enabled = false;
                    }
                    if (this.hdnIdCliente.Value != "272"){
                        string sAux = ObtenerOpcion(this.hdnIdCliente.Value, sCR,"",true, null);
                        int nPos = sAux.IndexOf("<table");
                        if (nPos == -1) nPos = 0;
                        strTablaHtml = sAux.Substring(nPos, sAux.Length - nPos);
                    }
                }
                catch (Exception ex)
                {
                    strErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }

            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }

        public void RaiseCallbackEvent(string eventArg)
        {
            string sResultado = "";
            string[] aArgs = Regex.Split(eventArg, "@#@");
            sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
            switch (aArgs[0])
            {
                case ("obtener"):
                    sResultado += ObtenerOpcion(aArgs[1], aArgs[2], aArgs[3], ((aArgs[4] != "true")? null :(bool?)bool.Parse(aArgs[4])), (aArgs[1] != "") ? (bool?)bool.Parse(aArgs[5]) : null);
                    break;
            }
            _callbackResultado = sResultado;
        }
        public string GetCallbackResult()
        {
            return _callbackResultado;
        }

        private string ObtenerOpcion_Old(string sIdCli, string sCR, string cboArea, Nullable<bool> activas)
        {
            string sResul = "", sFecha;
            int? iCodCli=null, iCR=null;
            try
            {
                StringBuilder sb = new StringBuilder();

                if (sIdCli != "") iCodCli = int.Parse(sIdCli);
                if (sCR != "") iCR = int.Parse(sCR);
                sb.Append("<table id='tblOpciones' class='texto MA' style='width: 850px;'>");
                sb.Append("<colgroup><col style='width:200px;'><col style='width:275px'><col style='width:250px'><col style='width:125px;'></colgroup>");
                //Solo PST en estado activo
                SqlDataReader dr = PST.Catalogo(null, iCR, "", "", activas, iCodCli, null, "", null, cboArea, null);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t346_idpst"].ToString() + "' des=\"" + Utilidades.escape(dr["t346_despst"].ToString()) + "\" cli=\"" + Utilidades.escape(dr["nom_cliente"].ToString()) + "\">");
                    sb.Append("<td style='padding-left:5px;'>" + dr["t346_codpst"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t346_despst"].ToString() + "</td>");
                    sb.Append("<td>"+ dr["nom_cliente"].ToString() +"</td>");
                    if (dr["t346_fecharef"].ToString() == "") sFecha = "";
                    else sFecha = DateTime.Parse(dr["t346_fecharef"].ToString()).ToShortDateString();
                    sb.Append("<td align='center'>" + sFecha + "</td></tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");

                sResul = "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener las ordenes de trabajo codificadas", ex);
            }
            return sResul;
        }
        private string ObtenerOpcion(string sIdCli, string sCR, string cboArea, Nullable<bool> activas, Nullable<bool> clienteNulo)
        {
            string sResul = "", sFecha;
            int? iCodCli = null, iCR = null;
            try
            {
                StringBuilder sb = new StringBuilder();

                if (sIdCli != "") iCodCli = int.Parse(sIdCli);
                if (sCR != "") iCR = int.Parse(sCR);
                sb.Append("<table id='tblOpciones' class='texto MA' style='width: 850px;'>");
                sb.Append("<colgroup><col style='width:200px;'><col style='width:275px'><col style='width:250px'><col style='width:125px;'></colgroup>");
                SqlDataReader dr = PST.Catalogo(null, iCR, "", "", activas, iCodCli, null, "", null, cboArea, clienteNulo);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t346_idpst"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)'>");
                    if ((bool)dr["t346_estado"])
                        sb.Append("<td style='padding-left:5px;'>" + dr["t346_codpst"].ToString() + "</td>");
                    else
                        sb.Append("<td style='padding-left:5px; color:red'>" + dr["t346_codpst"].ToString() + "</td>");
                    sb.Append("<td title='" + dr["t346_despst"].ToString() + "'><nobr class='NBR W270'>" + dr["t346_despst"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W245'>" + dr["nom_cliente"].ToString() + "</nobr></td>");
                    if (dr["t346_fecharef"].ToString() == "") sFecha = "";
                    else sFecha = DateTime.Parse(dr["t346_fecharef"].ToString()).ToShortDateString();
                    sb.Append("<td align='center'>" + sFecha + "</td></tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</table>");

                sResul = "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener las ordenes de trabajo codificadas", ex);
            }
            return sResul;
        }

	}
}
