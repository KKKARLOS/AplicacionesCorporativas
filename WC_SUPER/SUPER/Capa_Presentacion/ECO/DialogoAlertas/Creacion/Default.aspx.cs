using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sPSN = "", sEsInterlocutor = "false", sEsGestor = "false";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback){
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            sPSN = Utilidades.decodpar(Request.QueryString["idpsn"].ToString());

            //if (!DIALOGOALERTAS.TienePermisoCreacion(int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()), int.Parse(Session["UsuarioActual"].ToString()))){
            //    btnNuevo.Disabled = true;
            //    sNuevo = "false";
            //}
            CargarAlertas(int.Parse(sPSN));

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("addDialogo"):
                sResultado += addDialogo(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    protected void CargarAlertas(int nPSN)
    {
        SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerAsuntosCreacion(null, nPSN, (int)Session["UsuarioActual"]);
        while (dr.Read())
        {
            if (sEsGestor == "false" && (bool)dr["Gestor"])
            {
                sEsGestor = "true";
            }
            if (sEsInterlocutor == "false" && (bool)dr["Interlocutor"])
            {
                sEsInterlocutor = "true";
            }
            cboAsunto.Items.Add(new ListItem(dr["t820_denominacion"].ToString(), dr["t820_idalerta"].ToString()));
        }
        if (sEsInterlocutor == "true" && cboAsunto.Items.Count == 1){
            lblAsunto.Text = cboAsunto.Items[cboAsunto.SelectedIndex].Text;
            lblAsunto.Visible = true;
            cboAsunto.Style.Add("display", "none");
            lblReqAsunto.Visible = false;
        }
        else if (sEsGestor == "true")
        {
            cboAsunto.Items.Insert(0, new ListItem("", ""));
        }

        dr.Close();
        dr.Dispose();
    }
    protected string addDialogo(string sAsunto, string sMes, string sMensaje, string sPSN, string sInterlocutor, string sFLR)
    {
        try
        {
            DIALOGOALERTAS.crearDialogo(byte.Parse(sAsunto), (sMes == "") ? null : (int?)int.Parse(sMes), sMensaje,
                int.Parse(sPSN), (sInterlocutor == "1") ? "R" : "D", (sFLR == "") ? null : (DateTime?)DateTime.Parse(sFLR));
            return "OK";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al crear el diálogo.", ex);
        }
    }
}
